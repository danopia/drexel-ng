Public Class ModuleListPane
    Inherits ShiftingPanes.GroupedMenuListPane(Of BBVista.Module, ModuleListItem)

    Protected Friend WithEvents Worker As New ComponentModel.BackgroundWorker

    Public Sub SetCourse(Course As BBVista.Course)
        Me.IsThrobbing = True
        Worker.RunWorkerAsync(Course)
    End Sub

    Private Sub Worker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        e.Result = CType(e.Argument, BBVista.Course).FetchSidebar
    End Sub

    Private Sub Worker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        Me.SetSources(e.Result)
        Me.IsThrobbing = False
    End Sub
End Class
