﻿Imports System.Windows.Controls

Public Class ListPane(Of x, y As {New, ListItem(Of x)})
    Inherits Pane

    Protected Friend _stack As New StackPanel
    Protected Friend _scroller As New ScrollViewer

    Public Sub New()
        MyBase.New()

        _scroller.Content = _stack
        Children.Add(_scroller)
    End Sub

    Public Sub SetSources(SourceList As List(Of x))
        _stack.Children.Clear()
        For Each Source As x In SourceList
            Dim Control As New y
            Control.Source = Source
            _stack.Children.Add(Control)
        Next
    End Sub

End Class

Public MustInherit Class ListPane(Of x)
    Inherits ListPane(Of x, ListItem(Of x))

    Public MustOverride Sub BuildItem(Source As x, Control As ListItem(Of x))

    Public Shadows Sub SetSources(SourceList As List(Of x))
        _stack.Children.Clear()
        For Each Source As x In SourceList
            Dim Control As New ListItem(Of x)
            BuildItem(Source, Control)
            _stack.Children.Add(Control)
        Next
    End Sub

End Class
