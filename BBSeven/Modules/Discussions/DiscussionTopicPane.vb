﻿Public Class DiscussionTopicPane
    Inherits ShiftingPanes.GroupedMenuListPane(Of BBVista.Message)

    Protected Friend WithEvents Worker As New ComponentModel.BackgroundWorker
    Protected Friend _course As BBVista.Course
    Protected Friend _forum As BBVista.Forum
    Protected Friend _topic As BBVista.Topic

    Public Sub SetTopic(Topic As BBVista.Topic)
        Me.IsThrobbing = True
        _course = Topic.Forum.Course
        _forum = Topic.Forum
        _topic = Topic
        Worker.RunWorkerAsync(_topic)
    End Sub

    Private Sub Worker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        e.Result = CType(e.Argument, BBVista.Topic).FetchMessages
    End Sub

    Private Sub Worker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        Me.SetSources(e.Result)
        Me.IsThrobbing = False
    End Sub

    Public Overrides Sub BuildItem(Source As BBVista.Message, Control As ShiftingPanes.MenuListItem(Of BBVista.Message))
        Dim _layout As New StackPanel
        Dim _subject As New Label
        Dim _author As New Label
        Dim _date As New Label
        Dim _body As New Label

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

        Dim Binding As New Binding()
        Binding.Mode = BindingMode.TwoWay
        Binding.Source = My.Application.Resources.Item("settings")
        Binding.Path = New PropertyPath("FontSize")
        Body.SetBinding(TextBlock.FontSizeProperty, Binding)
        '  New Data.Binding("StaticResource settings}, Path=FontSize, Mode=TwoWay}"

        _layout.Children.Add(_subject)
        _layout.Children.Add(_author)
        _layout.Children.Add(_date)
        _layout.Children.Add(Body)

        Dim Border As New Border
        Border.Child = _layout
        Border.Padding = New Thickness(0, 10, 0, 10)
        Border.BorderThickness = New Thickness(0, 1, 0, 1)
        Border.BorderBrush = Brushes.Gray

        Control.Content = Border
    End Sub
End Class
