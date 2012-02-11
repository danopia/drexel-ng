Imports System.Windows.Controls

Public Class Throbbable
    Inherits Grid
    Protected Friend _throbber As Throbber = Nothing

    Public Property IsThrobbing As Boolean
        Set(ByVal value As Boolean)
            If _throbber Is Nothing Then
                If value Then
                    _throbber = New Throbber
                    Me.Children.Add(_throbber)
                End If
            ElseIf Not value Then
                Me.Children.Remove(_throbber)
                _throbber = Nothing
            End If
        End Set
        Get
            Return _throbber IsNot Nothing
        End Get
    End Property
End Class
