Imports System.Windows.Media.Animation

Class MainWindow
    'Private Lock = False
    'Private myCourseListItem As CourseListItem
    Private myCourseList As New CourseListPane
    Protected Friend _moduleList As ModuleListPane

    Private Sub ModuleList_MouseUp(ByVal sender As Object, ByVal e As EventArgs)
        'Dim Pane As New Button
        'Pane.Content = "We must go deeper!"
        'ShiftingView.PutPaneAfter(sender, Pane)

        'AddHandler Pane.Click, AddressOf ModuleList_MouseUp
    End Sub

    Private Sub BackButton_Expanded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles BackButton.Expanded
        BackButton.IsExpanded = False
        ShiftingView.PopPane()
    End Sub

    Private Sub myCourseList_OnSelectCourse(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
        'If myCourseListItem IsNot Nothing And myCourseListItem IsNot sender Then
        '    Lock = True
        '    myCourseListItem.Button.IsExpanded = False
        'End If

        'myCourseListItem = sender
        'Lock = False

        If myCourseList.SelectedItem Is Nothing Then
            ShiftingView.PopPane()
        Else
            _moduleList = New ModuleListPane
            _moduleList.SetCourse(myCourseList.SelectedItem.Source)
            ShiftingView.PutPaneAfter(myCourseList, _moduleList)
            
            AddHandler _moduleList.OnSelectionChanged, AddressOf myModuleList_OnSelectModule
        End If
    End Sub

    Private Sub myModuleList_OnSelectModule(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
        If _moduleList.SelectedItem Is Nothing Then
            ShiftingView.PopPane()
        ElseIf _moduleList.SelectedItem.Source.Label = "Discussions" Then
            Dim Pane As New DiscussionsListPane
            Pane.SetCourse(myCourseList.SelectedItem.Source)
            ShiftingView.PutPaneAfter(_moduleList, Pane)
        Else
            'Dim Pane As New Button
            'Pane.Content = "We must go deeper!"
            'ShiftingView.PutPaneAfter(_moduleList, Pane)

            'AddHandler Pane.Click, AddressOf ModuleList_MouseUp
        End If
    End Sub

    Private Sub ShiftingView_LevelChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShiftingView.LevelChanged
        BackButton.IsEnabled = ShiftingView.SecondaryPane IsNot Nothing
    End Sub

    Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        myCourseList.SetSession(State.Session)
        ShiftingView.PushPane(myCourseList)
        UserLabel.Content = State.Session.FullName

        AddHandler myCourseList.OnSelectionChanged, AddressOf myCourseList_OnSelectCourse
    End Sub

    Private Sub Gear_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles Gear.Click
        Gear.ContextMenu.PlacementTarget = Gear
        Gear.ContextMenu.IsOpen = True
    End Sub

    Private Sub SettingsButton_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles SettingsButton.Click
        Dim Window As New SettingsWindow
        Window.ShowDialog()
    End Sub
End Class
