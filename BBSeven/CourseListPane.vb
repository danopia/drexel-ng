Public Class CourseListPane
    Inherits ShiftingPanes.MenuListPane(Of BBVista.Course)

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

    Public Overrides Sub BuildItem(Source As BBVista.Course, Control As ShiftingPanes.MenuListItem(Of BBVista.Course))
        Dim _topLabel As New Label
        _topLabel.Content = String.Format("{0}-{1}-{2} {3}", Source.Name, Source.Number, Source.Section, Source.Junk)
        _topLabel.Padding = New Thickness(1)

        Dim _bottomLabel As New Label
        _bottomLabel.Content = Source.Title
        _bottomLabel.Padding = New Thickness(1)

        Dim _stack As New StackPanel
        _stack.Children.Add(_topLabel)
        _stack.Children.Add(_bottomLabel)
        Control.Content = _stack

        If Source.Message IsNot Nothing Then _topLabel.FontWeight = FontWeights.Bold

        If Source.BasePath Is Nothing Then
            _topLabel.Foreground = Brushes.Gray
            _bottomLabel.Foreground = Brushes.Gray
            Control.IsExpandable = False
        Else
            _topLabel.Foreground = Brushes.Black
            _bottomLabel.Foreground = Brushes.Black
            Control.IsExpandable = True
        End If
    End Sub
End Class
