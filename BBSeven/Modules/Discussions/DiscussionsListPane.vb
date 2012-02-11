Imports ShiftingPanes

Public Class DiscussionsListPane
    Inherits ShiftingPanes.GroupedMenuListPane(Of BBVista.Forum, DiscussionsListItem)

    Protected Friend WithEvents Worker As New ComponentModel.BackgroundWorker
    Protected Friend _course As BBVista.Course
    Protected Friend _module As BBVista.DiscussionModule

    Public Sub SetCourse(Course As BBVista.Course)
        Me.IsThrobbing = True
        _course = Course
        _module = Course.GetDisc
        Worker.RunWorkerAsync(_module)
    End Sub

    Private Sub Worker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        e.Result = CType(e.Argument, BBVista.DiscussionModule).FetchTopics
    End Sub

    Private Sub Worker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        Me.SetSources(e.Result)
        Me.IsThrobbing = False
    End Sub

    Private Sub SelectionChanged(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.OnSelectionChanged
        If SelectedItem Is Nothing Then
            View.PopPane()
        Else
            Dim Pane As New DiscussionTopicListPane
            Pane.SetForum(SelectedItem._forum)
            View.PutPaneAfter(Me, Pane)
        End If
    End Sub
End Class
