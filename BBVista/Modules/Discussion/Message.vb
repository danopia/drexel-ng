Public Class Message
    Public ReadOnly Forum As Forum
    Public ReadOnly Course As Course
    Public ReadOnly Topic As Topic

    Public ReadOnly Subject, Author, [Date], Body As String
    Public ReadOnly MessageID As UInt64

    Protected Friend Sub New(topic As Topic, Match As Text.RegularExpressions.Match)
        Me.Topic = topic
        Me.Forum = topic.Forum
        Me.Course = topic.Forum.Course

        '<b>Subject:</b> Re:External Link Errors
        '<b>Topic:</b> Assignment 1
        '<b>Author:</b> Zachary Schoenstadt
        '<b>Date:</b> January 16, 2012 8:16 PM

        Dim HeaderHTML = Match.Groups(1).Value
        Dim Headers As New Dictionary(Of String, String)
        For Each HeaderMatch As Text.RegularExpressions.Match In RegEx.Matches(HeaderHTML, "<b>([^<]+):</b> (.+?)" & vbLf)
            Headers.Add(HeaderMatch.Groups(1).Value, HeaderMatch.Groups(2).Value)
        Next

        Subject = Web.HttpUtility.HtmlDecode(Headers("Subject"))
        Author = Web.HttpUtility.HtmlDecode(Headers("Author"))
        [Date] = Web.HttpUtility.HtmlDecode(Headers("Date"))

        Body = Match.Groups(2).Value
    End Sub
End Class
