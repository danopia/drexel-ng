Public Class Topic
    Public ReadOnly Forum As Forum
    Public ReadOnly Course As Course

    Public ReadOnly Title As String
    Public ReadOnly MessageID, TopicID As UInt64
    Public ReadOnly Messages As Integer = 1
    Public ReadOnly Unread As Integer = 0

    Protected Friend Sub New(Forum As Forum, HTML As String)
        Me.Forum = Forum
        Me.Course = Forum.Course

        Dim Match = RegEx.Match(HTML, "<a href=""javascript:isMsgPreview\('newMessageThread.dowebct\?discussionaction=viewMessage&messageid=([0-9]+)&topicid=([0-9]+)&refreshPage=(?:true|false)&sourcePage=',[0-9]+\)"" title=""Link opens in a new window"">.(.+?)</a>")
        MessageID = UInt64.Parse(Match(1).Value)
        TopicID = UInt64.Parse(Match(2).Value)
        Title = Web.HttpUtility.HtmlDecode(Match(3).Value)

        Match = RegEx.Match(HTML, "<td>..([0-9]+)(?:..\(<b>	<a[^>]+>.([0-9]+).Unread.</a></b>\))?...</td>")
        If Match IsNot Nothing Then
            Messages = Match(1).Value
            Unread = IIf(Match(2).Value.Length > 0, Match(2).Value, 0)
        End If
    End Sub

    Public Function FetchMessages() As List(Of Message)
        Dim Path = String.Format("compiledMessageThreadView.dowebct?compileaction=displayCompleteThread&topicid={0}&messageid={1}", TopicID, MessageID)
        Dim HTML = Course.GetPage(Path)

        FetchMessages = New List(Of Message)
        For Each Match As Text.RegularExpressions.Match In RegEx.Matches(HTML, "<div class=""entrydiv"">...?<table[^>]+>(.+?)</table>.<div class=""entrytext"">.<div class=""userhtml"">...(.+?)..</div>.+?<div class=""clearfix"">...<div class=""replytomessage"">.<a class=""actionlight"" href=""Javascript:replyMessage\('([0-9]+)','([0-9]+)','([0-9]+)'\);"" title=""Link opens in a new window"">Reply</a>")
            FetchMessages.Add(New Message(Me, Match))
        Next
    End Function
End Class
