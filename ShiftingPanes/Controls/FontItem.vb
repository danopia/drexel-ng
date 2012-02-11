Public Class FontItem
    Inherits MenuListItem(Of Windows.Media.FontFamily)

    Protected Friend _font As Windows.Media.FontFamily
    Protected Friend _label As New Windows.Controls.Label

    Public Overrides Sub SetSource(Source As Windows.Media.FontFamily)
        _font = Source

        _label.Content = _font.Source
        _label.FontFamily = _font
        _label.Padding = New Windows.Thickness(1)

        Me.Content = _label
    End Sub
End Class
