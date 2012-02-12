Public Class Course
    Protected Session As Session
    Public ReadOnly BasePath, Role, Title, Name, Number, Section, Junk, Instructors, Message As String

    Protected Friend Sub New(ByVal Session As Session, ByVal BaseHTML As String)
        Me.Session = Session

        Try
            Dim Groups = RegEx.Match(BaseHTML, My.Resources.CourseListRegex)
            Me.BasePath = IIf(Groups(1).Value.Length > 0, Groups(1).Value.Split("/"c)(0), Nothing)
            Me.Role = Groups(3).Value
            Me.Title = IIf(Groups(4).Value.Length > 0, Groups(4).Value, Groups(8).Value)
            Try
                Me.Name = IIf(Groups(5).Value.Length > 0, Groups(5).Value, Nothing)
                Me.Number = IIf(Groups(6).Value.Length > 0, Groups(6).Value, Nothing)
                Me.Section = IIf(Groups(7).Value.Length > 0, Groups(7).Value, Nothing)
                Me.Junk = IIf(Groups(8).Value.Length > 0, Groups(8).Value, Nothing)
                Me.Instructors = IIf(Groups(10).Value.Length > 0, Groups(10).Value, Nothing)
                Me.Message = IIf(Groups(13).Value.Length > 0, Groups(13).Value, Nothing)
            Catch ex As Exception
                MsgBox("2")
                MsgBox(Me.Title)
                MsgBox(BaseHTML)
            End Try
        Catch ex As Exception
            MsgBox("3")
            MsgBox(BaseHTML)
        End Try
    End Sub

    Public Function FetchSidebar() As Dictionary(Of String, List(Of [Module]))
        Dim HTML = GetPage("courseMenu.dowebct?tab=view&forward=studentCourseView.dowebct")
        FetchSidebar = New Dictionary(Of String, List(Of [Module]))

        For Each Match As Text.RegularExpressions.Match In RegEx.Matches(HTML, My.Resources.ModuleListSectionRegex)
            Dim Modules As New List(Of [Module])

            For Each Match2 As Text.RegularExpressions.Match In RegEx.Matches(Match.Groups(2).Value, My.Resources.ModuleListItemRegex)
                Dim [Module] As New [Module](Me, Match2)
                Modules.Add([Module])
            Next

            FetchSidebar.Add(Match.Groups(1).Value, Modules)
        Next
    End Function

    Protected Friend Function GetPage(ByVal Path As String) As String
        Return Session.client.GetString(Me.BasePath & "/" & Path & "&lcid=" & Me.BasePath.Split("."c)(1))
    End Function

    Public Function GetDisc() As DiscussionModule
        Return New DiscussionModule(Me)
    End Function
    Public Function GetGrades() As GradesModule
        Return New GradesModule(Me)
    End Function
End Class

''HTML = Session.client.GetString("tp2466051976111.lc2459371910121/courseFS.dowebct?tab=view&forward=studentCourseView.dowebct&lcid=2459371910121")

''tp2461733138041.lc2398232989131/viewtabtoolframe.dowebct?tab=view&forward=studentCourseView.dowebct&lcid=2398232989131

''HTML = Session.client.GetString("tp2466051976111.lc2459371910121/studentCourseView.dowebct?tab=view&forward=studentCourseView.dowebct&lcid=2459371910121")
''this one is the main course view

'HTML = Session.client.GetString("tp2466051976111.lc2459371910121/courseMenu.dowebct?tab=view&forward=studentCourseView.dowebct&lcid=2459371910121")
''sidebar

'Return

''discussions
'HTML = Session.client.GetString("tp2466051976111.lc2459371910121/ctbDispatch.dowebct?insView=/discussionHomepageView.dowebct&desView=/discussionHomepageView.dowebct&studentView=/discussionHomepageView.dowebct&toolName=Discussion&tab=view&courseMapDisplayName=discussion.CTBCourseMapDisplayName")

''click default topic
'HTML = Session.client.GetString("tp2466051976111.lc2459371910121/newMessageThread.dowebct?discussionaction=mDisplay&topicid=2466087128111&areaid=2466051976111&fromTopic=true&homePage=true")

''profile
'HTML = Session.client.GetString("tp2466051976111.lc2459371910121/displayContextMenuOption.dowebct?menuOption=Profiles&personID=2025189359011&menuName=discussionMemberMenuForNonGradeableTopics")

''view single message
'HTML = Session.client.GetString("tp2466051976111.lc2459371910121/newMessageThread.dowebct?discussionaction=viewMessage&messageid=2473127536061&topicid=2466087128111&refreshPage=false&sourcePage=")

''view thread
'HTML = Session.client.GetString("tp2466051976111.lc2459371910121/compiledMessageThreadView.dowebct?compileaction=displayCompleteThread&topicid=2466087128111&messageid=2472466340141")

''HTML = Session.client.GetString("tp2466051976111.lc2459371910121/newMessageThread.dowebct?discussionaction=mDisplay&view=Threaded&topicid=2466087128111&areaid=&msgListing=&exp=")

''new message form
'HTML = Session.client.GetString("tp2466051976111.lc2459371910121/createMessage.dowebct?todo=view&actionType=compose&topicid=2466087128111&areaid=0&msgListing=")

''reply form
'HTML = Session.client.GetString("tp2466051976111.lc2459371910121/createMessage.dowebct?todo=view&actionType=reply&areaid=-1&topicid=2466087128111&messageid=2473127536061&donotrefresh=")


''grades
'HTML = Session.client.GetString("tp2466051976111.lc2459371910121/ctbDispatch.dowebct?insView=/membergradebookMyGradesView.dowebct&desView=/membergradebookMyGradesView.dowebct&studentView=/membergradebookMyGradesView.dowebct&toolName=My%20Grades&tab=view&courseMapDisplayName=mygrades.CTBCourseMapDisplayName")
''HTML = Session.client.GetString("tp2466051976111.lc2459371910121/courseMenu.dowebct?tab=view&refreshTopFrame&t1326557685865")
''HTML = Session.client.GetString("tp2466051976111.lc2459371910121/courseFS.dowebct?noToolFrameUpdate=true&tab=view&t1326557685314")


''assignments
'HTML = Session.client.GetString("tp2466051976111.lc2459371910121/ctbDispatch.dowebct?desView=/projectsView.dowebct&insView=/projectsView.dowebct%3FinstructorView%3Dtrue&studentView=/viewSubmissions.dowebct&toolName=Projects&tab=view&courseMapDisplayName=projects.CTBCourseMapDisplayName")

''view assignment
'HTML = Session.client.GetString("tp2466051976111.lc2459371910121/editAssignmentSubmission.dowebct?projectId=2466094012111")


