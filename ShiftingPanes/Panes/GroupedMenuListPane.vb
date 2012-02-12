Imports System.Windows.Controls

Public MustInherit Class GroupedMenuListPane(Of x)
    Inherits MenuListPane(Of x)

    Public Overloads Sub SetSources(SourceList As Dictionary(Of String, List(Of x)))
        _stack.Children.Clear()
        For Each SourceGroup As Generic.KeyValuePair(Of String, List(Of x)) In SourceList

            Dim _groupStack As New StackPanel
            Dim _groupExpander As New Expander
            _groupExpander.Content = _groupStack
            _groupExpander.Header = SourceGroup.Key
            _groupExpander.IsExpanded = True
            _stack.Children.Add(_groupExpander)

            For Each Source As x In SourceGroup.Value
                Dim Control As New ShiftingPanes.MenuListItem(Of x)(Source)
                BuildItem(Source, Control)

                AddHandler Control.OnSelect, AddressOf OnItemSelect
                AddHandler Control.OnDeselect, AddressOf OnItemDeselect

                _groupStack.Children.Add(Control)
            Next
        Next
    End Sub
End Class
