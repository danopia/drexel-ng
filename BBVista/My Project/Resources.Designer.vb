﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.239
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("BBVista.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to &lt;li&gt;&lt;a href=&quot;(?:/webct/urw/([^&quot;]+)|javascript:alert\(&apos;([^&quot;]+)&apos;\))&quot; target=&quot;APPLICATION_FRAME&quot; title=&quot;My Role: ([^&quot;]+)&quot;&gt;(.+?) - (?:([A-Z]+)-([A-Z0-9]+)-([A-Z0-9]+)(?: ([0-9]+))?|([^&lt;]+))&lt;/a&gt;(?:&lt;p&gt;Section Instructor: ([^&lt;]+)&lt;/p&gt;|&lt;p /&gt;)(?:&lt;p class=&quot;mynews&quot;&gt;(&lt;a href=&quot;([^&quot;]+)&quot;[^&gt;]+&gt;&lt;img[^&gt;]+title=&quot;([^&quot;]+)&quot; /&gt;&lt;/a&gt;)+&lt;/p&gt;)?&lt;/li&gt;.
        '''</summary>
        Friend ReadOnly Property CourseListRegex() As String
            Get
                Return ResourceManager.GetString("CourseListRegex", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to &lt;li&gt;&lt;a href=&quot;/webct/urw/(tp[0-9]+.lc[0-9]+/ctbDispatch.dowebct?[^&quot;]+)&quot;[^&gt;]+&gt;(&lt;img[^&gt;]+&gt;)?&lt;img src=&quot;([^&quot;]+)&quot;[^&gt;]+&gt;&amp;nbsp;([^&lt;]+)&lt;img[^&gt;]+/&gt;(?:&lt;img[^&gt;]+/&gt;)?&lt;/a&gt;&lt;/li&gt;.
        '''</summary>
        Friend ReadOnly Property ModuleListItemRegex() As String
            Get
                Return ResourceManager.GetString("ModuleListItemRegex", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to &lt;ul title=&quot;([^&quot;]+)&quot;&gt;(.+?)&lt;/ul&gt;.
        '''</summary>
        Friend ReadOnly Property ModuleListSectionRegex() As String
            Get
                Return ResourceManager.GetString("ModuleListSectionRegex", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
