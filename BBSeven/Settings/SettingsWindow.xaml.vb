Public Class SettingsWindow

    Private Sub ApplyButton_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ApplyButton.Click

    End Sub

    Private Sub OKButton_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

    Private Sub CancelButton_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles CancelButton.Click
        Me.Close()
    End Sub

    Private Sub SettingsWindow_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        View.PushPane(New SettingsPane)
    End Sub
End Class
