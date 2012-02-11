Public Class Forum
    Public ReadOnly Course As Course
    Public ReadOnly Label, Path, UnreadPath As String
    Public ReadOnly Messages, Unread As Integer

    Protected Friend Sub New(ByVal Course As Course, ByVal Match As Text.RegularExpressions.Match)
        Me.Course = Course

        Path = Match.Groups(1).Value
        Label = Match.Groups(2).Value
        Messages = Integer.Parse(Match.Groups(3).Value)

        If Match.Groups(6).Value.Length > 0 Then
            UnreadPath = Match.Groups(4).Value & "," & Match.Groups(5).Value
            Unread = Integer.Parse(Match.Groups(6).Value)
        End If
    End Sub

    Public Function FetchTopics() As List(Of Topic)
        FetchTopics = New List(Of Topic)

        Dim HTML = Course.GetPage(Path)
        For Each RowMatch As Text.RegularExpressions.Match In RegEx.Matches(HTML, "<tr id=""row([0-9]+)""[^>]+>(.+?)</tr>")
            Dim Row = RowMatch.Groups(2).Value
            FetchTopics.Add(New Topic(Me, Row))
        Next

    End Function
End Class
