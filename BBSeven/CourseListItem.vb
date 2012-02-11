Public Class CourseListItem
    Inherits ShiftingPanes.MenuListItem(Of BBVista.Course)

    Protected Friend _course As BBVista.Course

    Public Overrides Sub SetSource(Source As BBVista.Course)
        _course = Source

        Dim _topLabel As New Label
        _topLabel.Content = String.Format("{0}-{1}-{2} {3}", _course.Name, _course.Number, _course.Section, _course.Junk)
        _topLabel.Padding = New Thickness(1)

        Dim _bottomLabel As New Label
        _bottomLabel.Content = _course.Title
        _bottomLabel.Padding = New Thickness(1)

        Dim _stack As New StackPanel
        _stack.Children.Add(_topLabel)
        _stack.Children.Add(_bottomLabel)
        Me.Content = _stack


        If _course.Message IsNot Nothing Then _topLabel.FontWeight = FontWeights.Bold

        If _course.BasePath Is Nothing Then
            _topLabel.Foreground = Brushes.Gray
            _bottomLabel.Foreground = Brushes.Gray
            _button.IsEnabled = False
            _button.Cursor = Cursors.No
            Me.Cursor = Nothing
        Else
            _topLabel.Foreground = Brushes.Black
            _bottomLabel.Foreground = Brushes.Black
            _button.IsEnabled = True
            _button.Cursor = Nothing
            Me.Cursor = Cursors.Hand
        End If
    End Sub
End Class
