Public Class GradesModule
    Protected Friend ReadOnly _course As Course

    Protected Friend Sub New(Course As Course)
        _course = Course
    End Sub

    Public Function FetchGrades() As List(Of Grade)
        Dim HTML = _course.GetPage("ctbDispatch.dowebct?insView=/membergradebookMyGradesView.dowebct&desView=/membergradebookMyGradesView.dowebct&studentView=/membergradebookMyGradesView.dowebct&toolName=My%20Grades&tab=view&courseMapDisplayName=mygrades.CTBCourseMapDisplayName")
        FetchGrades = New List(Of Grade)

        For Each Match As Text.RegularExpressions.Match In RegEx.Matches(HTML, "<tr>.<td scope=""row"" nowrap=""nowrap""><b>..([^:]+):..?</b></td>..(?:\W+<td> &nbsp; </td><td> &nbsp; </td><td> &nbsp; |.<td>..([0-9.]+).\W+&nbsp;\(out of ([0-9.]+)\)..</td>.<td>..<img[^>]+></td>.\W+<td>(?: &nbsp; |([^<]+))|\W+<td>([0-9.]+)</td>..<td>&nbsp;</td>..<td> &nbsp; )</td>...</tr>")
            FetchGrades.Add(New Grade(Match))
        Next
    End Function
End Class
