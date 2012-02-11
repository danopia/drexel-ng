Imports System.Windows.Media.Animation
Imports System.ComponentModel

Public Class LoginWindow
    Protected Friend WithEvents Worker As New BackgroundWorker

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        If Worker.IsBusy Then Return
        Worker.RunWorkerAsync(New String() {My.Resources.BaseURL, TextBox2.Text, TextBox3.Password})

        MyThrobbable.IsThrobbing = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button2.Click
        If MyThrobbable.IsThrobbing Then
            MyThrobbable.IsThrobbing = False
        Else
            Me.Close()
        End If
    End Sub

    Private Sub ServerExpander_Collapsed(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles ServerExpander.Collapsed
        AnimateWidth(Me.MinWidth)
    End Sub

    Private Sub ServerExpander_Expanded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles ServerExpander.Expanded
        AnimateWidth(Me.MaxWidth)
    End Sub

    Protected Friend Sub AnimateWidth(ByVal NewWidth As Double)
        Dim myDoubleAnimation As New DoubleAnimation()
        myDoubleAnimation.From = Me.Width
        myDoubleAnimation.To = NewWidth
        myDoubleAnimation.EasingFunction = New QuadraticEase

        Dim myDoubleAnimation2 As New DoubleAnimation
        myDoubleAnimation2.From = Me.Left
        myDoubleAnimation2.To = Me.Left - ((NewWidth - Me.Width) / 2)
        myDoubleAnimation2.EasingFunction = New QuadraticEase

        Dim myStoryboard As New Storyboard()
        myStoryboard.Duration = New Duration(TimeSpan.FromSeconds(1))

        myStoryboard.Children.Add(myDoubleAnimation)
        Storyboard.SetTargetProperty(myDoubleAnimation, New PropertyPath(Control.WidthProperty))

        myStoryboard.Children.Add(myDoubleAnimation2)
        Storyboard.SetTargetProperty(myDoubleAnimation2, New PropertyPath(Window.LeftProperty))

        myStoryboard.Begin(Me)
    End Sub

    Public Sub New()
        InitializeComponent()

        Me.Width = Me.MinWidth
    End Sub

    Private Sub ServerBox_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles ServerBox.SelectionChanged
        If (ServerText IsNot Nothing) Then ServerText.Text = CType(ServerBox.SelectedItem, ComboBoxItem).Tag
    End Sub

    Private Sub Worker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        Dim Args As String() = e.Argument
        State.Session = New BBVista.Session(Args(0), Args(1), Args(2))
    End Sub

    Private Sub Worker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        Dim Main As New MainWindow
        Main.Show()
        Me.Close()
    End Sub
End Class
