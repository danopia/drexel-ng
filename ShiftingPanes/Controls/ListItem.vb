Public Class ListItem(Of x)
    Inherits AnimatedControl

    Public Source As x = Nothing

    Public Sub New()
    End Sub

    Public Sub New(Source As x)
        Me.Source = Source
    End Sub
End Class
