Public Class SettingsPane
    Inherits ShiftingPanes.MenuListPane(Of SettingsModule)

    Public Sub New()
        SetSources(New SettingsModule() {New StylePane, New InterfacePane})
    End Sub

    Public Overrides Sub BuildItem(Source As SettingsModule, Control As ShiftingPanes.MenuListItem(Of SettingsModule))
        Dim _title As New Label
        Dim _desc As New Label
        Dim _stack As New StackPanel

        _title.Content = Source.Title
        _title.FontSize = 18

        _desc.Content = Source.Description
        Dim Brush As New SolidColorBrush(Colors.Black)
        Brush.Opacity = 0.75
        _desc.Foreground = Brush

        _stack.Children.Add(_title)
        _stack.Children.Add(_desc)

        Control.Content = _stack
    End Sub

    Private Sub SettingsPane_OnSelectionChanged(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.OnSelectionChanged
        If Me.SelectedItem Is Nothing Then
            Me.View.PopPane()
        Else
            Dim Pane As New ShiftingPanes.Pane
            Pane.Children.Add(Me.SelectedItem.Source)
            Me.View.PutPaneAfter(Me, Pane)
        End If
    End Sub
End Class
