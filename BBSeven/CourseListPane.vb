Public Class CourseListPane
    Inherits ShiftingPanes.MenuListPane(Of BBVista.Course, CourseListItem)

    Protected Friend WithEvents Worker As New ComponentModel.BackgroundWorker

    Public Sub SetSession(Session As BBVista.Session)
        Me.IsThrobbing = True
        Worker.RunWorkerAsync(Session)
    End Sub

    Private Sub Worker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        e.Result = CType(e.Argument, BBVista.Session).FetchCourseList
    End Sub

    Private Sub Worker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        Me.SetSources(CType(e.Result, ICollection(Of BBVista.Course)))
        Me.IsThrobbing = False
    End Sub
End Class
