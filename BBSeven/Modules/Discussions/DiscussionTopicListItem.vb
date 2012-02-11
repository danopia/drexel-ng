Public Class DiscussionTopicListItem
    Inherits ShiftingPanes.MenuListItem(Of BBVista.Topic)

    Protected Friend _forum As BBVista.Forum
    Protected Friend _topic As BBVista.Topic

    Protected Friend _layout As New Grid
    Protected Friend _label As New Label
    Protected Friend _count As New Label
    Protected Friend _unread As New Label

    Public Overrides Sub SetSource(Source As BBVista.Topic)
        _topic = Source

        _label.Content = Source.Title
        _label.Padding = New Thickness(3)
        _label.VerticalAlignment = Windows.VerticalAlignment.Center
        Grid.SetColumn(_label, 0)

        _count.Content = Source.Messages
        _count.Padding = New Thickness(3)
        _count.VerticalAlignment = Windows.VerticalAlignment.Center
        Grid.SetColumn(_count, 1)

        _unread.Content = Source.Unread
        _unread.Padding = New Thickness(3)
        _unread.VerticalAlignment = Windows.VerticalAlignment.Center
        Grid.SetColumn(_unread, 2)

        If Source.Unread > 0 Then
            _label.FontWeight = FontWeights.Bold
        End If

        Dim Col As New ColumnDefinition()
        Col.Width = New GridLength(1, GridUnitType.Star)
        _layout.ColumnDefinitions.Add(Col)

        Col = New ColumnDefinition()
        Col.Width = GridLength.Auto
        _layout.ColumnDefinitions.Add(Col)

        Col = New ColumnDefinition()
        Col.Width = GridLength.Auto
        _layout.ColumnDefinitions.Add(Col)

        _layout.Children.Add(_label)
        _layout.Children.Add(_count)
        _layout.Children.Add(_unread)

        Me.Content = _layout
    End Sub
End Class
