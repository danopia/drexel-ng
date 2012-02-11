Public Class ModuleListItem
    Inherits ShiftingPanes.MenuListItem(Of BBVista.Module)

    Protected Friend _course As BBVista.Course
    Protected Friend _module As BBVista.Module

    Protected Friend _label As New Label
    Protected Friend _icon As New Image
    Protected Friend _image As New BitmapImage
    Protected Friend _stack As New StackPanel

    Public Overrides Sub SetSource(Source As BBVista.Module)
        _module = Source
        '_course = _module.Course

        _label.Content = _module.Label
        _label.Padding = New Thickness(3)
        _label.VerticalAlignment = Windows.VerticalAlignment.Center
        _label.FontWeight = IIf(_module.Unread, FontWeights.Bold, FontWeights.Normal)

        _image.BeginInit()
        _image.UriSource = New Uri("http://learning.dcollege.net" & _module.Icon)
        _image.EndInit()

        _icon.Source = _image
        _icon.Stretch = Stretch.None
        _icon.Width = 20
        _icon.Height = 20

        _stack.Orientation = Orientation.Horizontal
        _stack.Children.Add(_icon)
        _stack.Children.Add(_label)
        Me.Content = _stack
    End Sub
End Class
