Public Class DiscussionModule
    Protected Friend ReadOnly _course As Course

    Protected Friend Sub New(Course As Course)
        _course = Course
    End Sub

    Public Function FetchTopics() As Dictionary(Of String, List(Of Forum))
        Dim HTML = _course.GetPage("ctbDispatch.dowebct?insView=/discussionHomepageView.dowebct&desView=/discussionHomepageView.dowebct&studentView=/discussionHomepageView.dowebct&toolName=Discussion&tab=view&courseMapDisplayName=discussion.CTBCourseMapDisplayName")
        FetchTopics = New Dictionary(Of String, List(Of Forum))

        'Dim Parts = HTML.Split("<h2  style=""display:inline"">")

        Dim Matches As New List(Of String)
        For Each GroupMatch As Text.RegularExpressions.Match In RegEx.Matches(HTML, "<h2  .+?>.+?<table class=""discussioncategory"" summary="""">")
            Matches.Add(GroupMatch.Value)
        Next

        If Matches.Count = 0 Then
            Matches.Add("<a href=""/webct/editAreaView.dowebct?FAIL"">Default Category</a>" & HTML)
        End If
        For Each GroupMatch As String In Matches
            Dim Label = RegEx.Match(GroupMatch, "<a href=""/webct/editAreaView.dowebct?([^""]+)"">([^<]+)</a>")(2).Value

            Dim List As New List(Of Forum)
            For Each Match As Text.RegularExpressions.Match In RegEx.Matches(GroupMatch, "<h3><a href=""javascript:isPreview\('/webct/urw/[^/]+/([^""]+)'\)"" title=""[^""]+"">([^<]+)</a></h3>.+?\(([0-9]+) Messages?(?:../.<strong><a href=""Javascript:newMsgCompileView\('([0-9]+)','([0-9]+)'\);"">([0-9]+) New</strong></a>)?.\)")
                List.Add(New Forum(_course, Match))
            Next

            FetchTopics.Add(Label, List)
        Next
    End Function
End Class
