
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

    <TableName("Blog_Comments")>
    <PrimaryKey("CommentID")>
    <Cacheable("Blog_Comments", CacheItemPriority.Normal)>
    Public Class PostCommentInfo


        Private m_CommentID As Integer
        Public Property CommentID() As Integer
            Get
                Return m_CommentID
            End Get
            Set
                m_CommentID = Value
            End Set
        End Property

        Private m_ContentItemId As Integer
        Public Property ContentItemId() As Integer
            Get
                Return m_ContentItemId
            End Get
            Set
                m_ContentItemId = Value
            End Set
        End Property

        Private m_ParentId As Integer
        Public Property ParentId() As Integer

            Get
                Return m_ParentId
            End Get
            Set
                m_ParentId = Value
            End Set
        End Property

        Private m_Comment As String
        Public Property Comment() As String

            Get
                Return m_Comment
            End Get
            Set
                m_Comment = Value
            End Set
        End Property

        Private m_attachUrl As String
        Public Property attachUrl() As String

            Get
                Return m_attachUrl
            End Get
            Set
                m_attachUrl = Value
            End Set
        End Property


        Private m_AttachName As String
        Public Property AttachName() As String

            Get
                Return m_AttachName
            End Get
            Set
                m_AttachName = Value
            End Set
        End Property


        Private m_Approved As Boolean
        Public Property Approved() As Boolean

            Get
                Return m_Approved
            End Get
            Set
                m_Approved = Value
            End Set
        End Property

        Private m_Author As String
        Public Property Author() As String

            Get
                Return m_Author
            End Get
            Set
                m_Author = Value
            End Set
        End Property

        Private m_Website As String
        Public Property Website() As String

            Get
                Return m_Website
            End Get
            Set
                m_Website = Value
            End Set
        End Property

        Private m_Email As String
        Public Property Email() As String

            Get
                Return m_Email
            End Get
            Set
                m_Email = Value
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

        Private m_LastModifiedByUserID As Integer
        Public Property LastModifiedByUserID() As Integer

            Get
                Return m_LastModifiedByUserID
            End Get
            Set
                m_LastModifiedByUserID = Value
            End Set
        End Property

        Private m_LastModifiedOnDate As DateTime
        Public Property LastModifiedOnDate() As DateTime

            Get
                Return m_LastModifiedOnDate
            End Get
            Set
                m_LastModifiedOnDate = Value
            End Set
        End Property

        <ReadOnlyColumn>
        Public Property AttachFileLink() As String

            Get
                If String.IsNullOrEmpty(attachUrl) Then
                    Return ""
                End If
                Return String.Format("<a href='{0}' target='_blank'>{1}</a>", attachUrl, AttachName)
            End Get
            Set
                m_attachUrl = Value
            End Set
        End Property


        Private _U As DotNetNuke.Entities.Users.UserInfo
        <ReadOnlyColumn>
        Public ReadOnly Property AuthorDetails() As DotNetNuke.Entities.Users.UserInfo

            Get

                If CreatedByUserID > 0 Then
                    _U = DotNetNuke.Entities.Users.UserController.GetUserById(0, CreatedByUserID)
                    If _U Is Nothing Then
                        Return Nothing
                    End If
                    Return _U
                End If
                Return Nothing
            End Get

        End Property

        <ReadOnlyColumn>
        Public ReadOnly Property AuthorLinkUrl() As String

            Get
                Dim oData As DotNetNuke.Entities.Users.UserInfo = AuthorDetails
                If Not oData Is Nothing Then
                    Return String.Format("<a href='/MyProfile/Username/{0}' title='click to view {1} profile'>{1}</a>", oData.Username, oData.DisplayName)
                End If
                Return ""
            End Get

        End Property


        <ReadOnlyColumn>
        Public ReadOnly Property DisplayName() As String

            Get
                If AuthorDetails Is Nothing Then
                    Return ""
                End If
                Return AuthorDetails.DisplayName

            End Get

        End Property
        <ReadOnlyColumn>
        Public ReadOnly Property GetCreatedDateHumanFriendly As String
            Get
                If IsDate(CreatedOnDate) Then
                    Return Util.HumanFriendlyDate(CreatedOnDate)
                End If
                Return ""
            End Get
        End Property


        <ReadOnlyColumn>
        Public ReadOnly Property GetLikes() As List(Of Likes)

            Get
                Dim ilst As List(Of Likes) = New DAL2.BlogContent.BlogContentController().GetForPost(CommentID, "Comment")
                Return ilst

            End Get

        End Property


        <ReadOnlyColumn>
        Public ReadOnly Property GetLikesCount() As Integer

            Get
                If Not GetLikes Is Nothing Then
                    Return GetLikes.Count
                End If
                Return 0

            End Get

        End Property
        <ReadOnlyColumn>
        Public ReadOnly Property ReplyCount() As Integer
            Get
                Dim oReplies As List(Of PostCommentInfo) = New DAL2.BlogContent.BlogContentController().GetCommentByContentID(False, ContentItemId, CommentID)
                If oReplies Is Nothing Then
                    Return 0
                End If
                Return oReplies.Count()
            End Get
        End Property




        <ReadOnlyColumn>
        Public ReadOnly Property Attachment() As List(Of Blog_Attachments)
            Get
                Dim oAttach As List(Of Blog_Attachments) = New DAL2.BlogContent.BlogContentController().GetAttachementByCommentID(CommentID)
                If oAttach Is Nothing Then
                    Return Nothing
                End If
                Return oAttach
            End Get
        End Property



        <ReadOnlyColumn>
        Public ReadOnly Property TotalAttachment As Integer
            Get
                Dim oAttach As List(Of Blog_Attachments) = Attachment
                If oAttach Is Nothing Then
                    Return 0
                End If
                Return oAttach.Count
            End Get
        End Property

    End Class





    Public Class UserData

        Public Property AuthorId As Integer

        Public Property DisplayName As String

        Public Property Username As String
    End Class
End Namespace
