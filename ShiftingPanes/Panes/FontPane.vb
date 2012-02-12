Imports System.Windows.Controls
Imports System.Windows
Imports System.Windows.Media

Public Class FontPane
    Inherits MenuListPane(Of FontFamily)

    Public Sub New()
        SetSources(Fonts.SystemFontFamilies)
    End Sub

    Public Overrides Sub BuildItem(Source As FontFamily, Control As ShiftingPanes.MenuListItem(Of FontFamily))
        Dim _label As New Windows.Controls.Label

        _label.Content = Source.Source
        _label.FontFamily = Source
        _label.Padding = New Windows.Thickness(1)

        Control.Content = _label
    End Sub
End Class
