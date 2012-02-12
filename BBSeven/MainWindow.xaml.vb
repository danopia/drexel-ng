Imports System.Windows.Media.Animation

Class MainWindow
    Private myCourseList As New CourseListPane

    Private Sub BackButton_Expanded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles BackButton.Expanded
        BackButton.IsExpanded = False
        ShiftingView.PopPane()
    End Sub

    Private Sub ShiftingView_LevelChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShiftingView.LevelChanged
        BackButton.IsEnabled = ShiftingView.SecondaryPane IsNot Nothing
    End Sub

    Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        myCourseList.SetSession(State.Session)
        ShiftingView.PushPane(myCourseList)
        UserLabel.Content = State.Session.FullName
    End Sub

    Private Sub Gear_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles Gear.Click
        Gear.ContextMenu.PlacementTarget = Gear
        Gear.ContextMenu.IsOpen = True
    End Sub

    Private Sub SettingsButton_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles SettingsButton.Click
        Dim Window As New SettingsWindow
        Window.ShowDialog()
    End Sub

    Private Sub AboutButton_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles AboutButton.Click
        Dim Window As New AboutWindow
        Window.ShowDialog()
    End Sub
End Class
