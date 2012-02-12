Public MustInherit Class MenuListPane(Of x)
    Inherits ListPane(Of x, MenuListItem(Of x))

    Protected Friend _selected As MenuListItem(Of x) = Nothing
    Public ReadOnly Property SelectedItem As MenuListItem(Of x)
        Get
            Return _selected
        End Get
    End Property

    Public Event OnSelectionChanged(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)

    Protected Friend Sub OnItemSelect(sender As Object, e As System.Windows.RoutedEventArgs)
        If _selected IsNot sender Then
            Dim old As MenuListItem(Of x) = _selected
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

    Public MustOverride Sub BuildItem(Source As x, Control As MenuListItem(Of x))

    Public Shadows Sub SetSources(SourceList As ICollection(Of x))
        _stack.Children.Clear()
        For Each Source As x In SourceList
            Dim Control As New ShiftingPanes.MenuListItem(Of x)(Source)
            BuildItem(Source, Control)

            AddHandler Control.OnSelect, AddressOf OnItemSelect
            AddHandler Control.OnDeselect, AddressOf OnItemDeselect

            _stack.Children.Add(Control)
        Next
    End Sub
End Class
