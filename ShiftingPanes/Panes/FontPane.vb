Imports System.Windows.Controls
Imports System.Windows
Imports System.Windows.Media

Public Class FontPane
    Inherits MenuListPane(Of FontFamily)

    Public Sub New()
        SetSources(Fonts.SystemFontFamilies)
    End Sub

    Public Overrides Function BuildItem(Source As System.Windows.Media.FontFamily) As MenuListItem(Of FontFamily)
        Dim Item As New MenuListItem(Of FontFamily)(Source)

        Dim _label As New Windows.Controls.Label

        _label.Content = Source.Source
        _label.FontFamily = Source
        _label.Padding = New Windows.Thickness(1)

        Item.Content = _label

        Return Item
    End Function
End Class
