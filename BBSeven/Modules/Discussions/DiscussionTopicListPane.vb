Public Class DiscussionTopicListPane
    Inherits ShiftingPanes.GroupedMenuListPane(Of BBVista.Topic)

    Protected Friend WithEvents Worker As New ComponentModel.BackgroundWorker
    Protected Friend _course As BBVista.Course
    Protected Friend _forum As BBVista.Forum

    Public Sub SetForum(Forum As BBVista.Forum)
        Me.IsThrobbing = True
        _course = Forum.Course
        _forum = Forum
        Worker.RunWorkerAsync(_forum)
    End Sub

    Private Sub Worker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        e.Result = CType(e.Argument, BBVista.Forum).FetchTopics
    End Sub

    Private Sub Worker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        Me.SetSources(e.Result)
        Me.IsThrobbing = False
    End Sub

    Private Sub SelectionChanged(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.OnSelectionChanged
        If SelectedItem Is Nothing Then
            View.PopPane()
        Else
            Dim Pane As New DiscussionTopicPane
            Pane.SetTopic(SelectedItem.Source)
            View.PutPaneAfter(Me, Pane)
        End If
    End Sub

    Public Overrides Function BuildItem(Source As BBVista.Topic) As ShiftingPanes.MenuListItem(Of BBVista.Topic)
        Dim Item As New ShiftingPanes.MenuListItem(Of BBVista.Topic)(Source)

        Dim _layout As New Grid
        Dim _label As New Label
        Dim _count As New Label
        Dim _unread As New Label

        _label.Content = Source.Title
        _label.Padding = New Thickness(3)
        _label.VerticalAlignment = Windows.VerticalAlignment.Center
        Grid.SetColumn(_label, 0)

        _count.Content = Source.Messages
        _count.Padding = New Thickness(3)
        _count.VerticalAlignment = Windows.VerticalAlignment.Center
        Grid.SetColumn(_count, 1)

        _unread.Content = Source.Unread
        _unread.Padding = New Thickness(3)
        _unread.VerticalAlignment = Windows.VerticalAlignment.Center
        Grid.SetColumn(_unread, 2)

        If Source.Unread > 0 Then
            _label.FontWeight = FontWeights.Bold
        End If

        Dim Col As New ColumnDefinition()
        Col.Width = New GridLength(1, GridUnitType.Star)
        _layout.ColumnDefinitions.Add(Col)

        Col = New ColumnDefinition()
        Col.Width = GridLength.Auto
        _layout.ColumnDefinitions.Add(Col)

        Col = New ColumnDefinition()
        Col.Width = GridLength.Auto
        _layout.ColumnDefinitions.Add(Col)

        _layout.Children.Add(_label)
        _layout.Children.Add(_count)
        _layout.Children.Add(_unread)

        Item.Content = _layout
        Return Item
    End Function
End Class
