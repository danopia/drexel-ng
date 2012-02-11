Public Class DiscussionTopicListPane
    Inherits ShiftingPanes.GroupedMenuListPane(Of BBVista.Topic, DiscussionTopicListItem)

    Protected Friend WithEvents Worker As New ComponentModel.BackgroundWorker
    Protected Friend _course As BBVista.Course
    Protected Friend _forum As BBVista.Forum

    Public Sub SetForum(Forum As BBVista.Forum)
        Me.IsThrobbing = True
        _course = Forum.Course
        _forum = Forum
        Worker.RunWorkerAsync(_forum)
    End Sub

    Private Sub Worker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        e.Result = CType(e.Argument, BBVista.Forum).FetchTopics
    End Sub

    Private Sub Worker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        Me.SetSources(e.Result)
        Me.IsThrobbing = False
    End Sub

    Private Sub SelectionChanged(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.OnSelectionChanged
        If SelectedItem Is Nothing Then
            View.PopPane()
        Else
            Dim Pane As New DiscussionTopicPane
            Pane.SetTopic(SelectedItem._topic)
            View.PutPaneAfter(Me, Pane)
        End If
    End Sub
End Class
