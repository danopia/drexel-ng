Imports System.Windows.Controls
Imports System.Windows.Media.Animation
Imports System.Windows

Public Class ShiftingView
    Inherits AnimatedControl

    Protected Friend WithEvents MainCanvas As New System.Windows.Controls.Grid

    Public Event LevelChanged(ByVal sender As Object, ByVal e As EventArgs)

    Protected Friend _stack As New List(Of Pane)()
    Protected Friend _recycling As New List(Of Pane)()

    Protected Friend _nextID = 0

    Public ReadOnly Property PrimaryPane As Pane
        Get
            If _stack.Count < 1 Then Return Nothing
            Return _stack(_stack.Count - 1)
        End Get
    End Property

    Public ReadOnly Property SecondaryPane As Pane
        Get
            If _stack.Count < 2 Then Return Nothing
            Return _stack(_stack.Count - 2)
        End Get
    End Property

    Public ReadOnly Property TertiaryPane As Pane
        Get
            If _stack.Count < 3 Then Return Nothing
            Return _stack(_stack.Count - 3)
        End Get
    End Property

    Public Sub PushPane(ByVal NewPane As Pane)
        NewPane.HorizontalAlignment = Windows.HorizontalAlignment.Left
        NewPane.Name = "Pane" & _nextID
        NewPane._view = Me
        Me.RegisterName(NewPane.Name, NewPane)
        _nextID += 1
        MainCanvas.Children.Add(NewPane)

        If PrimaryPane Is Nothing Then
            NewPane.Width = Me.ActualWidth
        Else
            NewPane.Width = Me.ActualWidth - 200

            Dim Animation = SetupAnimation()
            Animation.Animate(Pane.WidthProperty, PrimaryPane.ActualWidth, 200, PrimaryPane.Name)
            Animation.Animate(Pane.MarginProperty, New Thickness(PrimaryPane.Margin.Left, 0, 0, 0), New Thickness(0, 0, 0, 0), PrimaryPane.Name)

            Animation.Animate(Pane.MarginProperty, New Thickness(Me.ActualWidth, 0, 0, 0), New Thickness(200, 0, 0, 0), NewPane.Name)

            If SecondaryPane IsNot Nothing Then
                Animation.Animate(Pane.MarginProperty, New Thickness(SecondaryPane.Margin.Left, 0, 0, 0), New Thickness(-200, 0, 0, 0), SecondaryPane.Name)
            End If

            Animation.Start()
        End If

        _stack.Add(NewPane)

        RaiseEvent LevelChanged(Me, New EventArgs())
    End Sub

    Public Sub ReplacePane(ByVal NewPane As Pane)
        NewPane.HorizontalAlignment = Windows.HorizontalAlignment.Left
        NewPane.Name = "Pane" & _nextID
        NewPane._view = Me
        Me.RegisterName(NewPane.Name, NewPane)
        _nextID += 1
        MainCanvas.Children.Add(NewPane)

        NewPane.Width = Me.ActualWidth - IIf(SecondaryPane Is Nothing, 0, 200)

        Dim Animation = SetupAnimation()
        Animation.Animate(Pane.MarginProperty, New Thickness(PrimaryPane.Margin.Left, 0, 0, 0), New Thickness(Me.ActualWidth, 0, 0, 0), PrimaryPane.Name)
        Animation.Animate(Pane.MarginProperty, New Thickness(Me.ActualWidth, 0, 0, 0), New Thickness(IIf(SecondaryPane Is Nothing, 0, 200), 0, 0, 0), NewPane.Name)
        Animation.Start()

        _recycling.Add(PrimaryPane)
        _stack.RemoveAt(_stack.Count - 1)
        _stack.Add(NewPane)
    End Sub

    Public Sub PopPane()
        If PrimaryPane Is Nothing Then
            Return

        ElseIf SecondaryPane Is Nothing Then
            Dim Animation = SetupAnimation()
            Animation.Animate(Pane.MarginProperty, New Thickness(PrimaryPane.Margin.Left, 0, 0, 0), New Thickness(Me.ActualWidth, 0, 0, 0), PrimaryPane.Name)
            Animation.Start()

        Else
            Dim Animation = SetupAnimation()

            Animation.Animate(Pane.MarginProperty, New Thickness(PrimaryPane.Margin.Left, 0, 0, 0), New Thickness(Me.ActualWidth, 0, 0, 0), PrimaryPane.Name)

            If TertiaryPane Is Nothing Then
                Animation.Animate(Pane.WidthProperty, SecondaryPane.Width, Me.ActualWidth, SecondaryPane.Name)
            Else
                Animation.Animate(Pane.WidthProperty, SecondaryPane.Width, Me.ActualWidth - 200, SecondaryPane.Name)
                Animation.Animate(Pane.MarginProperty, New Thickness(SecondaryPane.Margin.Left, 0, 0, 0), New Thickness(200, 0, 0, 0), SecondaryPane.Name)

                Animation.Animate(Pane.MarginProperty, New Thickness(TertiaryPane.Margin.Left, 0, 0, 0), New Thickness(0, 0, 0, 0), TertiaryPane.Name)
            End If

            Animation.Start()
        End If

        _recycling.Add(PrimaryPane)
        _stack.RemoveAt(_stack.Count - 1)

        RaiseEvent LevelChanged(Me, New EventArgs())
    End Sub

    Public Sub SetPane(ByVal Level As Integer, ByVal NewPane As Pane)
        If Level > _stack.Count + 1 Then
            MsgBox("Requested level too deep")
        ElseIf Level = _stack.Count + 1 Then
            PushPane(NewPane)
        ElseIf Level = _stack.Count Then
            ReplacePane(NewPane)
        Else
            MsgBox("Going back in the previous time-space continium rip is not yet supported")
        End If
    End Sub

    Public Sub PutPaneAfter(ByVal Parent As Pane, ByVal NewPane As Pane)
        Do Until Parent Is PrimaryPane Or Parent Is SecondaryPane
            PopPane()
        Loop

        If Parent Is SecondaryPane Then
            ReplacePane(NewPane)
        ElseIf Parent Is PrimaryPane Then
            PushPane(NewPane)
        End If
    End Sub

    Public Sub New()
        Me.Content = MainCanvas
    End Sub

    Private Sub ShiftingView_SizeChanged(ByVal sender As Object, ByVal e As System.Windows.SizeChangedEventArgs) Handles Me.SizeChanged
        If PrimaryPane Is Nothing Then Return


    End Sub
End Class
