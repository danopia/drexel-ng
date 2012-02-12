Imports ShiftingPanes

Public Class GradesPane
    Inherits MenuListPane(Of BBVista.Grade)

    Protected Friend WithEvents Worker As New ComponentModel.BackgroundWorker
    Protected Friend _course As BBVista.Course
    Protected Friend _module As BBVista.GradesModule

    Public Sub SetCourse(Course As BBVista.Course)
        Me.IsThrobbing = True
        _course = Course
        _module = Course.GetGrades
        Worker.RunWorkerAsync(_module)
    End Sub

    Private Sub Worker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles Worker.DoWork
        e.Result = CType(e.Argument, BBVista.GradesModule).FetchGrades
    End Sub

    Private Sub Worker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Worker.RunWorkerCompleted
        Me.SetSources(e.Result)
        Me.IsThrobbing = False
    End Sub

    Public Overrides Sub BuildItem(Source As BBVista.Grade, Control As ShiftingPanes.MenuListItem(Of BBVista.Grade))
        Dim _layout As New Grid
        Dim _name As New Label
        Dim _grade As New Label
        Dim _desc As New TextBlock

        _name.Content = Source.Name
        _name.Padding = New Thickness(3)
        _name.VerticalAlignment = Windows.VerticalAlignment.Top
        Grid.SetColumn(_name, 0)

        If Source.Grade < 0 Then
            _name.Opacity = 0.5
        Else
            _grade.Content = String.Format("{0} (out of {1})", Source.Grade, Source.OutOf)
            _grade.Padding = New Thickness(3)
            _grade.VerticalAlignment = Windows.VerticalAlignment.Top
        End If
        Grid.SetColumn(_grade, 1)


        If (Source.Comments.Length > 0) Then
            _desc.Text = Source.Comments
            _desc.TextWrapping = TextWrapping.Wrap
            _desc.Padding = New Thickness(53, 3, 3, 3)
            _desc.VerticalAlignment = Windows.VerticalAlignment.Top
            Grid.SetRow(_desc, 1)
            Grid.SetColumnSpan(_desc, 2)
            _layout.Children.Add(_desc)
        End If


        Dim Col As New ColumnDefinition()
        Col.Width = New GridLength(1, GridUnitType.Star)
        _layout.ColumnDefinitions.Add(Col)

        Col = New ColumnDefinition()
        Col.Width = New GridLength(1, GridUnitType.Star)
        _layout.ColumnDefinitions.Add(Col)


        Dim Row As New RowDefinition()
        Row.Height = GridLength.Auto
        _layout.RowDefinitions.Add(Row)

        Row = New RowDefinition()
        Row.Height = GridLength.Auto
        _layout.RowDefinitions.Add(Row)


        _layout.Children.Add(_name)
        _layout.Children.Add(_grade)

        Control.Content = _layout
    End Sub
End Class
