Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

Imports System.Net.Http
Imports DotNetNuke.Web.Api
Imports System.Web.Http
Imports System.Net
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Services.Social.Messaging.Internal
Imports DotNetNuke.Services.Social.Messaging.Internal.Views
Imports DotNetNuke.Services.Social.Notifications
Imports DotNetNuke.Services.Social.Messaging
Imports DotNetNuke.Common.Utilities

Imports DotNetNuke.Services.Search.Entities
Imports DotNetNuke.Services.Search.Internals
Imports DotNetNuke.Web.InternalServices.Views.Search
Imports DotNetNuke.Framework
Imports DotNetNuke.Services.Search.Controllers


Imports System.IO
Imports DotNetNuke.Security.Roles
Imports DotNetNuke.Services.FileSystem
Imports System.Web.UI.WebControls
Imports System.Text
Imports System.Web.UI
Imports DotNetNuke.Entities.Profile
Imports System.Text.RegularExpressions
Imports System.Web.Script.Serialization
Imports DotNetNuke.Modules.Blog.Services

Namespace API
    <BlogAuthorizeAttribute(Services.SecurityAccessLevel.Anonymous)>
    Partial Public Class ClearActionController
        Inherits BlogControllerBase

        <HttpGet>
        Public Function HelloWorld() As HttpResponseMessage
            Try
                Dim helloWorld__1 As String = "Hello World!"
                Return Request.CreateResponse(HttpStatusCode.OK, helloWorld__1)
            Catch ex As System.Exception
                'Log to DotNetNuke and reply with Error
                Exceptions.LogException(ex)
                Return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
        <HttpGet>
        Public Function GetLikeContentID(ContentID As String) As HttpResponseMessage
            Try
                Dim js As New JavaScriptSerializer()
                Dim tmpLikes As Integer = js.Deserialize(Of Integer)(ContentID)
                Dim CurrentUserid As Integer = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID

                If CurrentUserid > 0 Then
                    Dim dal2 As New DAL2.BlogContent.BlogContentController
                    If dal2.LikeIsLikeByUser(tmpLikes, CurrentUserid) Then
                        Return Request.CreateResponse(HttpStatusCode.OK, "1")
                    End If

                End If

                Return Request.CreateResponse(HttpStatusCode.OK, "0")

            Catch ex As System.Exception
                'Log to DotNetNuke and reply with Error

                Return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function

        <HttpGet>
        Public Function GetLikeContentIDCount(ContentPostID As String, ItemType As String) As HttpResponseMessage
            Try
                Dim js As New JavaScriptSerializer()
                Dim tmpLikes As Integer = js.Deserialize(Of Integer)(ContentPostID)
                Dim CurrentUserid As Integer = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID

                If CurrentUserid > 0 Then
                    Dim dal2 As New DAL2.BlogContent.BlogContentController
                    ''Update the list
                    Dim oUserlike As String = dal2.Post(tmpLikes, CurrentUserid, ItemType)
                    Dim ilist As String = dal2.GetForPostCount(tmpLikes, ItemType)
                    Dim iUnLikeCount As String = dal2.GetUnlikeForPost(tmpLikes, ItemType).Count.ToString()
                    Dim dto As New LikesDTO
                    dto.CommentID = tmpLikes
                    dto.UserID = CurrentUserid
                    dto.LikesCount = ilist
                    dto.UserLiked = oUserlike
                    dto.UnLikeCount = iUnLikeCount

                    If (dto.UserLiked = "AddedLike") Then
                        dto.ImageName = "forum-recommended.png"
                    Else
                        dto.ImageName = "forum-recommend.png"
                    End If
                    Return Request.CreateResponse(HttpStatusCode.OK, js.Serialize(dto))

                End If

                Return Request.CreateResponse(HttpStatusCode.OK, "0")

            Catch ex As System.Exception
                'Log to DotNetNuke and reply with Error

                Return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
        <HttpGet>
        Public Function UnLikeThePost(ContentPostID As String, ItemType As String) As HttpResponseMessage
            Try
                Dim js As New JavaScriptSerializer()
                Dim tmpLikes As Integer = js.Deserialize(Of Integer)(ContentPostID)
                Dim CurrentUserid As Integer = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID

                If CurrentUserid > 0 Then
                    Dim dal2 As New DAL2.BlogContent.BlogContentController
                    ''Update the list
                    Dim oUserlike As String = dal2.UnLikePost(tmpLikes, CurrentUserid)
                    Dim ilist As String = dal2.GetForPostCount(tmpLikes, ItemType)
                    Dim iUnLikeCount As String = dal2.GetUnlikeForPost(tmpLikes, ItemType).Count.ToString()
                    Dim dto As New LikesDTO
                    dto.CommentID = tmpLikes
                    dto.UserID = CurrentUserid
                    dto.LikesCount = ilist
                    dto.UserLiked = oUserlike
                    dto.UnLikeCount = iUnLikeCount

                    Return Request.CreateResponse(HttpStatusCode.OK, js.Serialize(dto))

                End If

                Return Request.CreateResponse(HttpStatusCode.OK, "0")

            Catch ex As System.Exception
                'Log to DotNetNuke and reply with Error

                Return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function




        <HttpGet>
        Public Function GetFileStackData(ContentPostID As String) As HttpResponseMessage
            Try
                Dim js As New JavaScriptSerializer()
                Dim tmpLikes As Integer = js.Deserialize(Of Integer)(ContentPostID)
                Dim CurrentUserid As Integer = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID

                If CurrentUserid > 0 Then
                    Dim dal2 As New DAL2.BlogContent.BlogContentController
                    Dim olist As List(Of FileStackRefernceInfo) = dal2.GetFilestackByContentItemID(tmpLikes)


                    Dim lTempDTolist As New List(Of FileStackRefernceDTO)
                    For Each ofile As FileStackRefernceInfo In olist
                        Dim dto As New FileStackRefernceDTO
                        dto.ContentItemID = ofile.ContentItemID
                        dto.FileId = ofile.FileId
                        dto.FileName = ofile.FileName
                        dto.filestackurl = ofile.filestackurl
                        dto.UploadedBy = ofile.UploadedBy
                        lTempDTolist.Add(dto)
                    Next




                    Return Request.CreateResponse(HttpStatusCode.OK, js.Serialize(lTempDTolist))

                End If

                Return Request.CreateResponse(HttpStatusCode.OK, "0")

            Catch ex As System.Exception
                'Log to DotNetNuke and reply with Error

                Return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function



        <HttpGet>
        Public Function DeleteFileStack(FileName As String) As HttpResponseMessage
            Try
                Dim js As New JavaScriptSerializer()

                Dim CurrentUserid As Integer = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID

                If CurrentUserid > 0 Then
                    Dim dal2 As New DAL2.BlogContent.BlogContentController
                    dal2.DeleteFileStackReference(FileName, CurrentUserid)

                End If

                Return Request.CreateResponse(HttpStatusCode.OK, "0")

            Catch ex As System.Exception
                'Log to DotNetNuke and reply with Error

                Return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function

        <HttpGet>
        Public Function GetBlogList(ByVal CurrentPage As String, ByVal PageSize As String) As HttpResponseMessage
            Try
                Dim js As New JavaScriptSerializer()
                Dim iCurrentPage As Integer = js.Deserialize(Of Integer)(CurrentPage)
                Dim iPageSize As Integer = js.Deserialize(Of Integer)(PageSize)


                Dim CurrentUserid As Integer = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID

                If CurrentUserid > 0 Then

                    Dim iTotal As Integer = 0
                    Dim dal2 As New DAL2.BlogContent.BlogContentController
                    Dim oData As List(Of DAL2.BlogContent.BlogPostInfo) = dal2.GetBlogList(-1, iCurrentPage, iPageSize, iTotal)



                    If oData Is Nothing Then
                        Return Request.CreateResponse(HttpStatusCode.OK, "0")
                    End If
                    Return Request.CreateResponse(HttpStatusCode.OK, js.Serialize(oData))

                End If

                Return Request.CreateResponse(HttpStatusCode.OK, "0")

            Catch ex As System.Exception
                'Log to DotNetNuke and reply with Error

                Return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function


        <HttpGet>
        Public Function SaveFileStackData(ContentPostID As String, ByVal FileName As String, ByVal contentType As String, ByVal size As Integer, ByVal statckurl As String) As HttpResponseMessage
            Try
                Dim js As New JavaScriptSerializer()
                Dim oFileStack As New FileStackRefernceInfo
                If String.IsNullOrEmpty(statckurl) Or String.IsNullOrEmpty(FileName) Then

                    Return Request.CreateResponse(HttpStatusCode.OK, "Invalid Input")
                End If


                oFileStack.ContentItemID = Convert.ToInt32(ContentPostID)
                oFileStack.FileId = -1
                oFileStack.FileName = FileName
                oFileStack.filestackurl = statckurl

                Dim CurrentUserid As Integer = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID

                If CurrentUserid > 0 Then
                    Dim dal2 As New DAL2.BlogContent.BlogContentController

                    oFileStack.UploadedBy = CurrentUserid
                    dal2.AddToFilestack(oFileStack)
                    '[SFix] - return file name and CDN URL

                    Return Request.CreateResponse(HttpStatusCode.OK, "" + oFileStack.FileName + "~" + oFileStack.filestackurl + "|")

                End If

                Return Request.CreateResponse(HttpStatusCode.OK, "0")

            Catch ex As System.Exception
                'Log to DotNetNuke and reply with Error

                Return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function

        <DnnAuthorize>
        <HttpGet>
        Public Function GetAttachmentByCommentID(ByVal PortalId As Integer, ByVal ContentPostID As String) As HttpResponseMessage
            Try

                Dim CurrentUserid As Integer = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID
                If CurrentUserid < 0 Then
                    Return Request.CreateErrorResponse(HttpStatusCode.NonAuthoritativeInformation, "You are not authorized to access")
                End If
                Dim js As JavaScriptSerializer = New JavaScriptSerializer()
                Dim tmpLikes As Integer = js.Deserialize(Of Integer)(ContentPostID)
                Dim DBController As New DAL2.BlogContent.BlogContentController
                Dim olist As List(Of DAL2.BlogContent.Blog_Attachments) = DBController.GetAttachementByCommentID(tmpLikes)
                Dim lTempDTolist As List(Of AttachmentDTO) = New List(Of AttachmentDTO)()
                For Each ofile As DAL2.BlogContent.Blog_Attachments In olist
                    Dim dto As AttachmentDTO = New AttachmentDTO()
                    dto.ContentId = ofile.CommentID
                    dto.ContentItemID = ofile.ContentItemID
                    dto.FileName = ofile.FileName
                    dto.FileUrl = ofile.FileUrl
                    dto.FileSize = ofile.FileSize
                    dto.ContentType = ofile.ContentType
                    dto.UserID = ofile.UserID
                    dto.DateAdded = DAL2.BlogContent.Util.HumanFriendlyDate(ofile.DateAdded)
                    lTempDTolist.Add(dto)
                Next

                Return Request.CreateResponse(HttpStatusCode.OK, js.Serialize(lTempDTolist))
            Catch ex As System.Exception
                Return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function

        <DnnAuthorize()>
        <HttpGet()>
        Public Function DeleteAttachmentsFromURL(ByVal fileurl As String) As HttpResponseMessage
            Dim js As JavaScriptSerializer = New JavaScriptSerializer
            Dim statusFlag As Boolean = True
            Try
                Dim dbController As New DAL2.BlogContent.BlogContentController()
                dbController.DeleteFromURL(fileurl)

            Catch ex As System.Exception
                statusFlag = False
            End Try

            Return Request.CreateResponse(js.Serialize(statusFlag))

        End Function
        <DnnAuthorize>
        <HttpGet>
        Public Function AddAttachmentByComment(ByVal ContentID As Integer, ByVal CommentID As Integer, ByVal FileName As String, ByVal contentType As String, size As Integer, statckurl As String) As HttpResponseMessage
            Try

                Dim CurrentUserid As Integer = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID
                If CurrentUserid < 0 Then
                    Return Request.CreateErrorResponse(HttpStatusCode.NonAuthoritativeInformation, "You are not authorized to access")
                End If
                If FileName = "" Or statckurl = "" Then
                    Return Request.CreateResponse(HttpStatusCode.OK, "Invalid Input")
                End If
                Dim js As JavaScriptSerializer = New JavaScriptSerializer()

                Dim DBController As New DAL2.BlogContent.BlogContentController

                Dim oAttach As New DAL2.BlogContent.Blog_Attachments
                With oAttach
                    .AttachId = -1
                    .CommentID = CommentID
                    .ContentItemID = ContentID
                    .ContentType = contentType
                    .DateAdded = System.DateTime.Now
                    .FileName = FileName
                    .FileSize = size
                    .FileUrl = statckurl
                    .UserID = CurrentUserid



                End With
                DBController.AddAttachement(oAttach)
                Return Request.CreateResponse(HttpStatusCode.OK, "Success")
            Catch ex As System.Exception
                Return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message)
            End Try
        End Function
    End Class


    Public Class AttachmentDTO

        Public Property AttachId As Integer

        Public Property ContentItemID As Integer

        Public Property ContentId As Integer

        Public Property UserID As Integer

        Public Property FileName As String

        Public Property DateAdded As String

        Public Property ContentType As String

        Public Property FileSize As Integer

        Public Property FileUrl As String
    End Class

    Public Class DTOPost

        Private _ContentItemID As Integer
        Public Property ContentItemID As Integer
            Get
                Return _ContentItemID
            End Get
            Set(value As Integer)
                _ContentItemID = value
            End Set
        End Property

        Private _Title As String
        Public Property Title As String
            Get
                Return _Title
            End Get
            Set(value As String)
                _Title = value
            End Set
        End Property

        Private _PostUrll As String
        Public Property PostUrl As String
            Get
                Return _PostUrll
            End Get
            Set(value As String)
                _PostUrll = value
            End Set
        End Property
    End Class
End Namespace