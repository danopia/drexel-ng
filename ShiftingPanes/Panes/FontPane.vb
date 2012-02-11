Imports System.Windows.Controls
Imports System.Windows

Public Class FontPane
    Inherits MenuListPane(Of Media.FontFamily, FontItem)

    Public Sub New()
        SetSources(Media.Fonts.SystemFontFamilies)
    End Sub
End Class
