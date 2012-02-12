Public Class InterfacePane
    Implements SettingsModule

    Public ReadOnly Property Description As String Implements SettingsModule.Description
        Get
            Return "Modify various behaviors"
        End Get
    End Property

    Public ReadOnly Property Title As String Implements SettingsModule.Title
        Get
            Return "Interface"
        End Get
    End Property
End Class
