Public Class ListItem(Of x)
    Inherits AnimatedControl

    Protected Friend _source As x = Nothing

    Public Property Source As x
        Get
            Return _source
        End Get
        Set(value As x)
            _source = value
            SetSource(value)
        End Set
    End Property

    Public Overridable Sub SetSource(Source As x)
        Me.Content = Source
    End Sub
End Class
