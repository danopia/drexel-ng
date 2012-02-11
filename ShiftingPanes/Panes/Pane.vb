Imports System.Windows.Controls

Public Class Pane
    Inherits Throbbable
    Protected Friend _view As ShiftingView

    Public ReadOnly Property View As ShiftingView
        Get
            Return _view
        End Get
    End Property
End Class
