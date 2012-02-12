Public Class StylePane
    Implements SettingsModule

    Public ReadOnly Property Description As String Implements SettingsModule.Description
        Get
            Dim A = FamilyRadio1.GetBindingExpression(Label.FontSizeProperty)
            Return "Customize the application's fonts and colors"
        End Get
    End Property

    Public ReadOnly Property Title As String Implements SettingsModule.Title
        Get
            Return "Appearance"
        End Get
    End Property
End Class
