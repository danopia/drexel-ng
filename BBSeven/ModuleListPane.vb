Public Class ModuleListPane
    Inherits ShiftingPanes.GroupedMenuListPane(Of BBVista.Module)

    Protected Friend WithEvents Worker As New ComponentModel.BackgroundWorker
    Public Source As BBVista.Course

    Public Sub SetCourse(Course As BBVista.Course)
        Me.IsThrobbing = True
        Worker.RunWorkerAsync(Course)
        Me.Source = Course
    End Sub

    Private Sub Worker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        e.Result = CType(e.Argument, BBVista.Course).FetchSidebar
    End Sub

    Private Sub Worker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        Me.SetSources(e.Result)
        Me.IsThrobbing = False
    End Sub

    Public Overrides Sub BuildItem(Source As BBVista.Module, Control As ShiftingPanes.MenuListItem(Of BBVista.Module))
        Dim _label As New Label
        Dim _icon As New Image
        Dim _image As New BitmapImage
        Dim _stack As New StackPanel

        _label.Content = Source.Label
        _label.Padding = New Thickness(3)
        _label.VerticalAlignment = Windows.VerticalAlignment.Center
        _label.FontWeight = IIf(Source.Unread, FontWeights.Bold, FontWeights.Normal)

        _image.BeginInit()
        _image.UriSource = New Uri("http://learning.dcollege.net" & Source.Icon)
        _image.EndInit()

        _icon.Source = _image
        _icon.Stretch = Stretch.None
        _icon.Width = 20
        _icon.Height = 20

        _stack.Orientation = Orientation.Horizontal
        _stack.Children.Add(_icon)
        _stack.Children.Add(_label)

        Control.Content = _stack
    End Sub

    Private Sub ModuleListPane_OnSelectionChanged(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.OnSelectionChanged
        If Me.SelectedItem Is Nothing Then
            Me.View.PopPane()
        ElseIf Me.SelectedItem.Source.Label = "Discussions" Then
            Dim Pane As New DiscussionsListPane
            Pane.SetCourse(Source)
            Me.View.PutPaneAfter(Me, Pane)
        ElseIf Me.SelectedItem.Source.Label = "My Grades" Then
            Dim Pane As New GradesPane
            Pane.SetCourse(Source)
            Me.View.PutPaneAfter(Me, Pane)
        End If
    End Sub
End Class
