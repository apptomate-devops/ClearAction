Imports System.Linq
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Entities.Portals
Imports DotNetNuke.Modules.Blog.Common.Globals
Imports DotNetNuke.Modules.Blog.Templating
Imports DotNetNuke.Modules.Blog.Entities.Blogs
Imports DotNetNuke.Modules.Blog.DAL2.BlogContent
Imports DotNetNuke.Modules.Blog.Entities.Posts
Imports DotNetNuke.Modules.Blog.Entities.Terms
Imports DotNetNuke.Modules.Blog.Entities.Comments
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Modules.Blog.Entities.Comments.CommentsController

Public Class PostDetail
    Inherits BlogModuleBase

#Region "Properties"
    Private ReadOnly Property IsUserAdmin() As Boolean
        Get
            If UserInfo.IsInRole(PortalSettings.AdministratorRoleName) OrElse UserInfo.IsSuperUser Then
                Return True
            End If
            Return False
        End Get
    End Property
    Private ReadOnly Property CategoryFilter() As Integer
        Get

            If ddGlobalCategory.SelectedItem IsNot Nothing Then
                Return Convert.ToInt32(ddGlobalCategory.SelectedValue)
            End If
            Return -1
        End Get
    End Property
    Public ReadOnly Property GetUserEmail As String
        Get
            Return UserInfo.Email.ToString()
        End Get

    End Property
    Public ReadOnly Property GetUser As String
        Get
            Return UserInfo.UserID.ToString()
        End Get

    End Property
    Private ReadOnly Property GetContentID As Integer
        Get

            Try
                Return Convert.ToInt32(Request.QueryString("ContentItemId"))

            Catch ex As Exception

            End Try
            Return -1
        End Get

    End Property
#End Region

    Private Sub AssignTo(ocontorl As System.Web.UI.WebControls.Image, iuser As Integer)

        Dim iBlogUser As UserInfo = DotNetNuke.Entities.Users.UserController.GetUserById(Me.PortalId, iuser)
        If Not iBlogUser Is Nothing Then
            ocontorl.ImageUrl = iBlogUser.Profile.PhotoURL
            ocontorl.AlternateText = iBlogUser.DisplayName
        End If
    End Sub


    Private Sub BindData()
        If GetContentID > 0 Then

            Dim oCtrl As New BlogContentController()
            Dim oBlogContentObject As BlogPostInfo = oCtrl.GetBlogContentById(GetContentID)
            If oBlogContentObject Is Nothing Then
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId))

            End If
            lblTitle.Text = oBlogContentObject.Title
            lblAuthorName.Text = oBlogContentObject.GetAuthorDisplayName
            lblPostedDate.Text = oBlogContentObject.PublishedOnDate.ToString("MMM d, yyyy h:mm tt")
            lblEditedDateTime.Text = oBlogContentObject.UpdatedOnDate.ToString("MMM d- yyyy h:mm tt")
            lblEditAuthor.Text = oBlogContentObject.GetAuthorDisplayName
            lblSummary.Text = Server.HtmlDecode(oBlogContentObject.Summary)
            AssignTo(imgAuthor, oBlogContentObject.CreatedByUserID)
            lblCategoryName.Text = oBlogContentObject.GetBlogGlobalCategory.CategoryName
            lblComments.Text = oCtrl.GetCommentsbyContentID_Count(oBlogContentObject.ContentItemId).ToString()
            hdnBlogId.Value = oBlogContentObject.BlogID.ToString()

            hdnPostId.Value = oBlogContentObject.ContentItemId.ToString()



            'Dim ocmt As New BlogContentController()
            'Dim oComment As DAL2.BlogContent.PostCommentInfo = ocmt.GetComments(GetContentID, UserId)

            'lblComments.Text = oComment.Comment



        Else
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId))
        End If


    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            BindDropDown()
            BindData()

            ' txtComments.Attributes.Add("onkeydown", "If(event.keycode==13){document.getElementById('" + btnSubmitComment.ClientID + "').click();}")

        End If
    End Sub

    'Public ReadOnly Property PermissionToAddComment As Boolean
    '    Get
    '        Return BlogContext.Security.LoggedIn
    '    End Get

    'End Property
    Private Sub BindDropDown()
        Dim controller As New DAL2.BlogContent.BlogContentController()

        Dim oGlobalCategory As List(Of BlogGlobalCategory) = controller.GetGlobalCategory(IsUserAdmin).ToList()
        ddGlobalCategory.DataSource = oGlobalCategory
        ddGlobalCategory.DataTextField = "CategoryName"
        ddGlobalCategory.DataValueField = "CategoryId"
        ddGlobalCategory.DataBind()

        ddGlobalCategory.Items.Insert(0, New System.Web.UI.WebControls.ListItem("Category", "-1"))

    End Sub

    Protected Sub btnSubmitComment_Click(sender As Object, e As EventArgs)
        Dim oFullCommentDTO As New FullCommentDTO
        With oFullCommentDTO
            .Author = UserInfo.Username
            .BlogId = Convert.ToInt32(hdnBlogId.Value)
            .PostId = GetContentID
            .Email = UserInfo.Email
            .ParentId = -1
            .Website = ""
            .Comment = txtComments.Text


        End With
        Dim iBlogController As New CommentsController
        iBlogController.AddComment(oFullCommentDTO)
        Response.Redirect(DotNetNuke.Common.Globals.NavigateURL())

    End Sub
End Class