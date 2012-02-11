Imports System.Net

Public Class SmartWebClient
    Inherits WebClient
    Private ReadOnly m_container As New CookieContainer

    Protected Overrides Function GetWebRequest(ByVal address As Uri) As WebRequest
        Dim request As WebRequest = MyBase.GetWebRequest(address)
        Dim webRequest As HttpWebRequest = request
        If (WebRequest IsNot Nothing) Then
            webRequest.CookieContainer = m_container
        End If
        Return request
    End Function

    Public Function GetString(Path As String) As String
        Return Text.Encoding.UTF8.GetString(DownloadData(Path))
    End Function
End Class
