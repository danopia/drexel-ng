Module RegEx
    Public Function Match(ByVal Body As String, ByVal Pattern As String) As System.Text.RegularExpressions.GroupCollection
        Dim Regex As New System.Text.RegularExpressions.Regex(Pattern, Text.RegularExpressions.RegexOptions.Singleline)
        Dim MyMatch = Regex.Match(Body)
        If Not MyMatch.Success Then Return Nothing
        Return MyMatch.Groups
    End Function
    Public Function Matches(ByVal Body As String, ByVal Pattern As String) As System.Text.RegularExpressions.MatchCollection
        Dim Regex As New System.Text.RegularExpressions.Regex(Pattern, Text.RegularExpressions.RegexOptions.Singleline)
        Return Regex.Matches(Body)
    End Function
End Module
