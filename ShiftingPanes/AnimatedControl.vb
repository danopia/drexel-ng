Imports System.Windows
Imports System.Windows.Controls

Public Class AnimatedControl
    Inherits ContentControl

    Protected Friend Function SetupAnimation() As AnimationSet
        Return New AnimationSet(Me)
    End Function
End Class
