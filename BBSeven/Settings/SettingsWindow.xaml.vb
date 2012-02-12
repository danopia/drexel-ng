Public Class SettingsWindow

    Protected Friend Sub ApplySettings()
        My.Settings.FontSize = CType(My.Application.Resources.Item("settings"), MySettings).FontSize
    End Sub

    Protected Friend Sub RevertSettings()
        CType(My.Application.Resources.Item("settings"), MySettings).FontSize = My.Settings.FontSize
    End Sub

    Private Sub ApplyButton_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ApplyButton.Click
        ApplySettings()
        My.Settings.Save()
    End Sub

    Private Sub OKButton_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles OKButton.Click
        ApplySettings()
        My.Settings.Save()
        Me.Close()
    End Sub

    Private Sub CancelButton_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles CancelButton.Click
        My.Settings.Reload()
        RevertSettings()
        Me.Close()
    End Sub

    Private Sub SettingsWindow_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        View.PushPane(New SettingsPane)
    End Sub
End Class
