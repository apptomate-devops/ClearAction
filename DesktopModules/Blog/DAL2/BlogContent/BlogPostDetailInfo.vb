
Imports System.Linq
Imports DotNetNuke.Common.Globals
Imports DotNetNuke.Modules.Blog.Common.Globals
Imports DotNetNuke.Modules.Blog.Entities.Blogs
Imports DotNetNuke.Modules.Blog.Entities.Terms
Imports DotNetNuke.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Imports System.Text
Imports DotNetNuke

Namespace DAL2.BlogContent

    <TableName("Blog_Posts")>
    <PrimaryKey("ContentItemId")>
    Public Class BlogPostDetailInfo

        Private m_ContentItemId As Integer
        Public Property ContentItemId() As Integer
            Get
                Return m_ContentItemId
            End Get
            Set
                m_ContentItemId = Value
            End Set
        End Property

        Private m_BlogID As Integer
        Public Property BlogID() As Integer
            Get
                Return m_BlogID
            End Get
            Set
                m_BlogID = Value
            End Set
        End Property

        Private m_Title As String
        Public Property Title() As String

            Get
                Return m_Title
            End Get
            Set
                m_Title = Value
            End Set
        End Property

        Private m_Summary As String
        Public Property Summary() As String

            Get
                Return m_Summary
            End Get
            Set
                m_Summary = Value
            End Set
        End Property

        Private m_Image As String
        Public Property Image() As String

            Get
                Return m_Image
            End Get
            Set
                m_Image = Value
            End Set
        End Property

        Private m_Published As Boolean
        Public Property Published() As Boolean

            Get
                Return m_Published
            End Get
            Set
                m_Published = Value
            End Set
        End Property

        Private m_PublishedOnDate As DateTime
        Public Property PublishedOnDate() As DateTime

            Get
                Return m_PublishedOnDate
            End Get
            Set
                m_PublishedOnDate = Value
            End Set
        End Property

        Private m_AllowComments As Boolean
        Public Property AllowComments() As Boolean

            Get
                Return m_AllowComments
            End Get
            Set
                m_AllowComments = Value
            End Set
        End Property

        Private m_DisplayCopyright As Boolean
        Public Property DisplayCopyright() As Boolean

            Get
                Return m_DisplayCopyright
            End Get
            Set
                m_DisplayCopyright = Value
            End Set
        End Property

        Private m_Copyright As String
        Public Property Copyright() As String

            Get
                Return m_Copyright
            End Get
            Set
                m_Copyright = Value
            End Set
        End Property

        Private m_Locale As String
        Public Property Locale() As String

            Get
                Return m_Locale
            End Get
            Set
                m_Locale = Value
            End Set
        End Property

        Private m_ViewCount As Integer
        Public Property ViewCount() As Integer

            Get
                Return m_ViewCount
            End Get
            Set
                m_ViewCount = Value
            End Set
        End Property

        Private m_CategoryID As Integer
        Public Property CategoryID() As Integer

            Get
                Return m_CategoryID
            End Get
            Set
                m_CategoryID = Value
            End Set
        End Property

        Private m_CreatedByUserID As Integer
        Public Property CreatedByUserID() As Integer

            Get
                Return m_CreatedByUserID
            End Get
            Set
                m_CreatedByUserID = Value
            End Set
        End Property

        Private m_CreatedOnDate As DateTime
        Public Property CreatedOnDate() As DateTime

            Get
                Return m_CreatedOnDate
            End Get
            Set
                m_CreatedOnDate = Value
            End Set
        End Property


        Private m_UpdatedByUserID As Integer
        Public Property UpdatedByUserID() As Integer

            Get
                Return m_UpdatedByUserID
            End Get
            Set
                m_UpdatedByUserID = Value
            End Set
        End Property

        Private m_UpdatedOnDate As DateTime
        Public Property UpdatedOnDate() As DateTime

            Get
                Return m_UpdatedOnDate
            End Get
            Set
                m_UpdatedOnDate = Value
            End Set
        End Property



    End Class

End Namespace
