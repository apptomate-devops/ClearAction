
Imports System.Linq
Imports DotNetNuke.Common.Globals
Imports DotNetNuke.Modules.Blog.Common.Globals
Imports DotNetNuke.Modules.Blog.Entities.Blogs
Imports DotNetNuke.Modules.Blog.Entities.Terms
Imports DotNetNuke.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Imports System.Text
Imports DotNetNuke.Modules.Blog.Security

Namespace DAL2.BlogContent
    <TableName("Blog_Attachments")>
    <PrimaryKey("AttachId")>
    <Cacheable("blog_attachment", CacheItemPriority.BelowNormal)>
    Public Class Blog_Attachments

        Private m_AttachId As Integer
        Public Property AttachId() As Integer
            Get
                Return m_AttachId
            End Get
            Set
                m_AttachId = Value
            End Set
        End Property

        Private m_CommentID As Integer
        Public Property CommentID() As Integer
            Get
                Return m_CommentID
            End Get
            Set
                m_CommentID = Value
            End Set
        End Property

        Private m_ContentItemID As Integer
        Public Property ContentItemID() As Integer
            Get
                Return m_ContentItemID
            End Get
            Set
                m_ContentItemID = Value
            End Set
        End Property

        Private m_UserID As Integer
        Public Property UserID() As Integer
            Get
                Return m_UserID
            End Get
            Set
                m_UserID = Value
            End Set
        End Property


        Private m_FileName As String
        Public Property FileName() As String
            Get
                Return m_FileName
            End Get
            Set
                m_FileName = Value
            End Set
        End Property

        Private m_DateAdded As DateTime
        Public Property DateAdded() As DateTime
            Get
                Return m_DateAdded
            End Get
            Set
                m_DateAdded = Value
            End Set
        End Property



        Private m_ContentType As String
        Public Property ContentType() As String
            Get
                Return m_ContentType
            End Get
            Set
                m_ContentType = Value
            End Set
        End Property

        Private m_FileSize As Integer
        Public Property FileSize() As Integer
            Get
                Return m_FileSize
            End Get
            Set
                m_FileSize = Value
            End Set
        End Property


        Private m_FileUrl As String
        Public Property FileUrl() As String
            Get
                Return m_FileUrl
            End Get
            Set
                m_FileUrl = Value
            End Set
        End Property


    End Class


    Public Class AttachmentData

        Public Property CommentId As String

        Public Property currentCommentId As String

        Public Property attachmentURL As String

        Public Property size As String

        Public Property contenttype As String

        Public Property filename As String
    End Class


End Namespace

