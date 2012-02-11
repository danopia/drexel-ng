Public Class MenuListPane(Of x, y As {New, MenuListItem(Of x)})
    Inherits ListPane(Of x, y)

    Protected Friend _selected As y = Nothing
    Public ReadOnly Property SelectedItem As y
        Get
            Return _selected
        End Get
    End Property

    Public Event OnSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)

    Protected Friend Sub OnItemSelect(sender As Object, e As System.Windows.RoutedEventArgs)
        If _selected IsNot sender Then
            Dim old As y = _selected
            _selected = sender
            If old IsNot Nothing Then old.IsSelected = False

            RaiseEvent OnSelectionChanged(Me, e)
        End If
    End Sub
    Protected Friend Sub OnItemDeselect(sender As Object, e As System.Windows.RoutedEventArgs)
        If _selected Is sender Then
            _selected = Nothing
            RaiseEvent OnSelectionChanged(Me, e)
        End If
    End Sub

    Public Shadows Sub SetSources(SourceList As ICollection(Of x))
        _stack.Children.Clear()
        For Each Source As x In SourceList
            Dim Control As New y
            Control.Source = Source

            AddHandler Control.OnSelect, AddressOf OnItemSelect
            AddHandler Control.OnDeselect, AddressOf OnItemDeselect

            _stack.Children.Add(Control)
        Next
    End Sub
End Class
