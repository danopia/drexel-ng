Public Class DiscussionTopicMessage
    Inherits ShiftingPanes.MenuListItem(Of BBVista.Message)

    Protected Friend _forum As BBVista.Forum
    Protected Friend _topic As BBVista.Topic
    Protected Friend _message As BBVista.Message

    Protected Friend _layout As New StackPanel
    Protected Friend _subject As New Label
    Protected Friend _author As New Label
    Protected Friend _date As New Label
    Protected Friend _body As New Label

    Public Overrides Sub SetSource(Source As BBVista.Message)
        _message = Source

        _subject.Content = Source.Subject
        _subject.Padding = New Thickness(3)

        _author.Content = "Author: " & Source.Author
        _author.Padding = New Thickness(3)

        _date.Content = "Posted: " & Source.Date
        _date.Padding = New Thickness(3)

        '_body.Content = Web.HttpUtility.HtmlDecode(Source.Body.Replace("<br/>", vbNewLine))
        '_body.Padding = New Thickness(3)
        Dim Body As New TextBlock
        Body.Text = Web.HttpUtility.HtmlDecode(Source.Body.Replace("<br/>", vbNewLine))
        Body.TextWrapping = TextWrapping.Wrap
        Body.Padding = New Thickness(3)
        
        _layout.Children.Add(_subject)
        _layout.Children.Add(_author)
        _layout.Children.Add(_date)
        _layout.Children.Add(Body)

        Dim Border As New Border
        Border.Child = _layout
        Border.Padding = New Thickness(0, 10, 0, 10)
        Border.BorderThickness = New Thickness(0, 1, 0, 1)
        Border.BorderBrush = Brushes.Gray

        Me.Content = Border
    End Sub
End Class
