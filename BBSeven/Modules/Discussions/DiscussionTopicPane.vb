Public Class DiscussionTopicPane
    Inherits ShiftingPanes.GroupedMenuListPane(Of BBVista.Message, DiscussionTopicMessage)

    Protected Friend WithEvents Worker As New ComponentModel.BackgroundWorker
    Protected Friend _course As BBVista.Course
    Protected Friend _forum As BBVista.Forum
    Protected Friend _topic As BBVista.Topic

    Public Sub SetTopic(Topic As BBVista.Topic)
        Me.IsThrobbing = True
        _course = Topic.Forum.Course
        _forum = Topic.Forum
        _topic = Topic
        Worker.RunWorkerAsync(_topic)
    End Sub

    Private Sub Worker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        e.Result = CType(e.Argument, BBVista.Topic).FetchMessages
    End Sub

    Private Sub Worker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        Me.SetSources(e.Result)
        Me.IsThrobbing = False
    End Sub
End Class
