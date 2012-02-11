Imports System.Windows.Controls
Imports System.Windows
Imports System.Windows.Media

Public Class MenuListItem(Of x)
    Inherits ListItem(Of x)

    Public Event OnSelect(ByVal sender As System.Object, ByVal e As RoutedEventArgs)
    Public Event OnDeselect(ByVal sender As System.Object, ByVal e As RoutedEventArgs)

    Protected Friend WithEvents _button As New Expander
    Protected Friend _grid As New Grid
    Protected Friend _content As Object
    Protected Friend _brush As New SolidColorBrush(Colors.LightBlue)

    Private Sub Button_Expanded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles _button.Expanded
        OpacityTo(0.75)
        RaiseEvent OnSelect(Me, New RoutedEventArgs())
    End Sub

    Private Sub Button_Collapsed(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles _button.Collapsed
        OpacityTo(0)
        RaiseEvent OnDeselect(Me, New RoutedEventArgs())
    End Sub

    Protected Friend Sub OpacityTo(Opacity As Double)
        Dim Animation As New AnimationSet(Me, 0.5)
        Animation.Animate(SolidColorBrush.OpacityProperty, _brush.Opacity, Opacity, "BGBrush")
        Animation.Start()
    End Sub

    Private Sub MenuListItem_MouseEnter(sender As Object, e As System.Windows.Input.MouseEventArgs) Handles Me.MouseEnter
        If Not IsSelected Then OpacityTo(0.5)
    End Sub
    Private Sub MenuListItem_MouseLeave(sender As Object, e As System.Windows.Input.MouseEventArgs) Handles Me.MouseLeave
        If Not IsSelected Then OpacityTo(0)
    End Sub
    Private Sub MenuListItem_MouseLeftButtonDown(sender As Object, e As System.Windows.Input.MouseButtonEventArgs) Handles Me.MouseLeftButtonDown
        OpacityTo(1)
    End Sub

    Private Sub CourseListItem_MouseLeftButtonUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Me.MouseLeftButtonUp
        If Not _button.IsEnabled Then
            OpacityTo(IIf(IsSelected, 0.75, 0.5))
            Return
        End If

        If _button.IsExpanded Then
            _button.IsExpanded = False
            OpacityTo(0.5)
        Else
            _button.IsExpanded = True
            OpacityTo(0.75)
        End If
    End Sub

    Public Sub New()
        MyBase.New()

        _button.ExpandDirection = ExpandDirection.Right
        _button.Width = 23
        _button.Height = 23

        Dim Column As New ColumnDefinition()
        Column.Width = New GridLength(1, GridUnitType.Star)
        _grid.ColumnDefinitions.Add(Column)

        Column = New ColumnDefinition()
        Column.Width = GridLength.Auto
        _grid.ColumnDefinitions.Add(Column)

        _grid.Margin = New Thickness(3)
        _grid.Children.Add(_button)
        Grid.SetColumn(_button, 1)

        MyBase.Content = _grid

        _brush.Opacity = 0
        _grid.Background = _brush
        NameScope.SetNameScope(Me, New NameScope()) ' TODO: animatable's responsibility?
        Me.RegisterName("BGBrush", _brush)
    End Sub

    Public Overloads Property Content As Object
        Get
            Return _content
        End Get
        Set(value As Object)
            If _content IsNot Nothing Then _grid.Children.Remove(_content)
            _content = value
            _grid.Children.Add(_content)
        End Set
    End Property

    Public Property IsSelected As Boolean
        Get
            Return _button.IsExpanded
        End Get
        Set(value As Boolean)
            _button.IsExpanded = value
        End Set
    End Property

End Class
