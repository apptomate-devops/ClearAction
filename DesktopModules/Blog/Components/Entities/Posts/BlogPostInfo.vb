'
' DNN Connect - http://dnn-connect.org
' Copyright (c) 2015
' by DNN Connect
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'

Imports System.Linq
Imports DotNetNuke.Common.Globals
Imports DotNetNuke.Modules.Blog.Common.Globals
Imports DotNetNuke.Modules.Blog.Entities.Blogs
Imports DotNetNuke.Modules.Blog.Entities.Terms
Imports DotNetNuke.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Imports System.Text

Namespace Entities.Posts
    <TableName("Blog_Posts")>
    <PrimaryKey("ContentItemId")>
    Partial Public Class BlogPostInfo


        Public Property ContentItemId() As Integer
            Get
                Return m_ContentItemId
            End Get
            Set
                m_ContentItemId = Value
            End Set
        End Property
        Private m_ContentItemId As Integer


        Public Property BlogID() As Integer
            Get
                Return m_BlogID
            End Get
            Set
                m_BlogID = Value
            End Set
        End Property
        Private m_BlogID As Integer

        Public Property Title() As String

            Get
                Return m_Title
            End Get
            Set
                m_Title = Value
            End Set
        End Property

        Private m_ShortDescription As String
        Public Property ShortDescription() As String

            Get
                Return m_ShortDescription
            End Get
            Set
                m_ShortDescription = Value
            End Set
        End Property

        Private m_Title As String

        Public Property Summary() As String

            Get
                Return m_Summary
            End Get
            Set
                m_Summary = Value
            End Set
        End Property
        Private m_Summary As String

        Public Property Image() As String

            Get
                Return m_Image
            End Get
            Set
                m_Image = Value
            End Set
        End Property
        Private m_Image As String

        Public Property Published() As Boolean

            Get
                Return m_Published
            End Get
            Set
                m_Published = Value
            End Set
        End Property
        Private m_Published As Boolean

        Public Property PublishedOnDate() As DateTime

            Get
                Return m_PublishedOnDate
            End Get
            Set
                m_PublishedOnDate = Value
            End Set
        End Property
        Private m_PublishedOnDate As DateTime

        Public Property AllowComments() As Boolean

            Get
                Return m_AllowComments
            End Get
            Set
                m_AllowComments = Value
            End Set
        End Property
        Private m_AllowComments As Boolean

        Public Property DisplayCopyright() As Boolean

            Get
                Return m_DisplayCopyright
            End Get
            Set
                m_DisplayCopyright = Value
            End Set
        End Property
        Private m_DisplayCopyright As Boolean


        Public Property Copyright() As String

            Get
                Return m_Copyright
            End Get
            Set
                m_Copyright = Value
            End Set
        End Property
        Private m_Copyright As String

        Public Property Locale() As String

            Get
                Return m_Locale
            End Get
            Set
                m_Locale = Value
            End Set
        End Property
        Private m_Locale As String

        Public Property ViewCount() As Integer

            Get
                Return m_ViewCount
            End Get
            Set
                m_ViewCount = Value
            End Set
        End Property
        Private m_ViewCount As Integer

        Public Property CategoryID() As Integer

            Get
                Return m_CategoryID
            End Get
            Set
                m_CategoryID = Value
            End Set
        End Property
        Private m_CategoryID As Integer

    End Class
End Namespace