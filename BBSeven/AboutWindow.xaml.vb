Imports System.Deployment.Application

Public Class AboutWindow

    Private Sub Window_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Copyright.Text = My.Application.Info.Copyright

        Try
            Version.Content = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString
        Catch ex As Exception
            Version.Content = "Development"
        End Try
    End Sub

    Private Sub CloseBtn_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles CloseBtn.Click
        FadeOut.Begin(Me)
    End Sub

    Private Sub FadeOut_Completed(sender As Object, e As System.EventArgs) Handles FadeOut.Completed
        Me.Close()
    End Sub
End Class
