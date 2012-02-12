Public Class Grade
    Public ReadOnly Name As String, Grade As Double, OutOf As Double, Comments As String

    Public Sub New(Match As System.Text.RegularExpressions.Match)
        Me.Name = Match.Groups(1).Value
        Grade = -1
        If Match.Groups(2).Value.Length > 0 Then Double.TryParse(Match.Groups(2).Value, Me.Grade)
        OutOf = -1
        If Match.Groups(3).Value.Length > 0 Then Double.TryParse(Match.Groups(3).Value, Me.OutOf)
        Me.Comments = Match.Groups(4).Value
    End Sub
End Class
