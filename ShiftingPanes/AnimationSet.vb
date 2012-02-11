Imports System.Windows.Media.Animation
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media

Public Class AnimationSet
    Protected Friend _parent As FrameworkElement
    Protected Friend WithEvents _storyboard As New Storyboard

    Protected Friend Sub New(ByRef Parent As FrameworkElement, Optional Duration As Double = 1)
        _parent = Parent
        Me.Duration = Duration
    End Sub

    Public Property Duration As Double
        Get
            Return _storyboard.Duration.TimeSpan.TotalSeconds
        End Get
        Set(ByVal value As Double)
            _storyboard.Duration = New Windows.Duration(TimeSpan.FromSeconds(value))
        End Set
    End Property

    Public Sub Start()
        _storyboard.Begin(_parent)
    End Sub

    Public Sub Animate(ByVal [Property] As DependencyProperty, ByVal From As Double, ByVal [To] As Double, Optional ByVal Target As String = Nothing)
        Dim myAnimation As New DoubleAnimation()
        myAnimation.From = From
        myAnimation.To = [To]
        myAnimation.EasingFunction = New QuadraticEase
        myAnimation.Duration = _storyboard.Duration

        _storyboard.Children.Add(myAnimation)
        If Target IsNot Nothing Then Storyboard.SetTargetName(myAnimation, Target)
        Storyboard.SetTargetProperty(myAnimation, New PropertyPath([Property]))
    End Sub

    Public Sub Animate(ByVal [Property] As DependencyProperty, ByVal From As Thickness, ByVal [To] As Thickness, Optional ByVal Target As String = Nothing)
        Dim myAnimation As New ThicknessAnimation()
        myAnimation.From = From
        myAnimation.To = [To]
        myAnimation.EasingFunction = New QuadraticEase
        myAnimation.Duration = _storyboard.Duration

        _storyboard.Children.Add(myAnimation)
        If Target IsNot Nothing Then Storyboard.SetTargetName(myAnimation, Target)
        Storyboard.SetTargetProperty(myAnimation, New PropertyPath([Property]))
    End Sub

    Public Sub Animate(ByVal [Property] As DependencyProperty, ByVal From As Color, ByVal [To] As Color, Optional ByVal Target As String = Nothing)
        Dim myAnimation As New ColorAnimation()
        myAnimation.From = From
        myAnimation.To = [To]
        myAnimation.EasingFunction = New QuadraticEase
        myAnimation.Duration = _storyboard.Duration

        _storyboard.Children.Add(myAnimation)
        If Target IsNot Nothing Then Storyboard.SetTargetName(myAnimation, Target)
        Storyboard.SetTargetProperty(myAnimation, New PropertyPath([Property]))
    End Sub
End Class
