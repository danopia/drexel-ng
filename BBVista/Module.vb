Public Class [Module]
    Public ReadOnly Course As Course
    Public ReadOnly Path, Icon, Label As String
    Public ReadOnly Unread As Boolean

    Protected Friend Sub New(ByVal Course As Course, ByVal Match As Text.RegularExpressions.Match)
        Me.Course = Course

        Path = Match.Groups(1).Value
        Unread = Match.Groups(2).Value.Length > 0
        Icon = Match.Groups(3).Value
        Label = Web.HttpUtility.HtmlDecode(Match.Groups(4).Value)
    End Sub
End Class
