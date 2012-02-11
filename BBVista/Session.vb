Imports System.Net
Imports System.Text

Public Class Session
    Protected Friend client As New SmartWebClient
    Protected username As String
    Public ReadOnly FullName As String

    Public Sub New(ByVal URL As String, ByVal Username As String, ByVal Password As String)
        Dim HTML = client.GetString(URL)

        Dim Data As New Collections.Specialized.NameValueCollection
        Data.Add("glcid", "URN:X-WEBCT-VISTA-V1:a3180395-9076-1e64-0119-dc64cc050853")
        Data.Add("insId", "5116011")
        Data.Add("gotoid", "null")
        Data.Add("insName", "Drexel University")
        Data.Add("timeZoneOffset", "0")
        Data.Add("webctid", Username)
        Data.Add("password", Password)

        Me.username = Username

        Dim Post = UploadValues(Data)
        Dim Buffer = client.UploadData("http://learning.dcollege.net/webct/authenticateUser.dowebct", "POST", Post)
        HTML = Encoding.UTF8.GetString(Buffer)

        If HTML.Contains("Single Sign On Authentication has failed.") Then
            Throw New Exception("Log in failed")
        End If

        client.BaseAddress = "https://learning.dcollege.net/webct/urw/"

        HTML = client.GetString("tp0.lc5116011/adminFS.dowebct")
        FullName = RegEx.Match(HTML, "Welcome, (.+)\. Today is")(1).Value
    End Sub

    Public Function FetchCourseList() As Collections.Generic.List(Of Course)
        Dim HTML = client.GetString("tp0.lc5116011/populateMyWebCT.dowebct")
        Dim Matches = RegEx.Matches(HTML, "<li><a href=""[^""]+"" target=""APPLICATION_FRAME""[^>]+>.+?</li>")
        FetchCourseList = New Collections.Generic.List(Of Course)

        Try
            For Each Match As System.Text.RegularExpressions.Match In Matches
                FetchCourseList.Add(New Course(Me, Match.ToString))
            Next
        Catch ex As Exception
            MsgBox("ERRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRROR")
        End Try
    End Function

    Public Sub DoLogout()
        client.GetString("tp0.lc5116011/logout.dowebct?insId=5116011&insName=Drexel%20University&glcid=URN:X-WEBCT-VISTA-V1:a3180395-9076-1e64-0119-dc64cc050853")
    End Sub




    Private Function UploadValues(ByVal data As Collections.Specialized.NameValueCollection) As Byte()
        Const uploadValuesContentType = "application/x-www-form-urlencoded"
        client.Headers(HttpRequestHeader.ContentType) = uploadValuesContentType
        Dim delimiter = String.Empty
        Dim sb = New Text.StringBuilder()
        For Each name In data.AllKeys
            Dim values = data.GetValues(name)
            If (values Is Nothing) Then Continue For
            For Each value In values
                sb.Append(String.Format("{0}{1}={2}", delimiter, System.Web.HttpUtility.UrlEncode(name), System.Web.HttpUtility.UrlEncode(value)))
                delimiter = "&"
            Next
        Next
        Return Encoding.UTF8.GetBytes(sb.ToString())
    End Function
End Class
