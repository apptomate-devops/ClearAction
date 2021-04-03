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
Imports System.Web.Script.Serialization
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Entities.Portals
Imports DotNetNuke.Modules.Blog.DAL2.BlogContent
Imports DotNetNuke.Modules.Blog.Entities.Blogs
Imports DotNetNuke.Modules.Blog.Entities.Comments
Imports DotNetNuke.Modules.Blog.Entities.Posts
Imports DotNetNuke.Modules.Blog.Entities.Terms
Imports DotNetNuke.Modules.Blog.Templating
Imports DotNetNuke.Services.Localization

Public Class Blog
    Inherits BlogModuleBase
    Implements IActionable



    Private ReadOnly Property ContentItemId() As Integer
        Get
            Dim iOut As Integer = -1
            Try
                Int32.TryParse(Convert.ToString(Request.QueryString("ContentItemId")), iOut)


            Catch exc As Exception
            End Try
            Return iOut
        End Get
    End Property
    Private ReadOnly Property SortBy() As Integer
        Get

            If ddSortBy.SelectedItem IsNot Nothing Then
                Return Convert.ToInt32(ddSortBy.SelectedValue)
            End If
            Return 0
        End Get
    End Property
    Public ReadOnly Property BlogPostUrl As String
        Get

            Return DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", DAL2.BlogContent.Util.PostKey & "=[[ID]]")
        End Get
    End Property
    Public ReadOnly Property WebServicePath() As String
        Get
            Dim Prefix As String = "http://"
            If Request.IsSecureConnection Then
                Prefix = "https://"
            End If

            Return Prefix + PortalAlias.HTTPAlias.ToString()
        End Get
    End Property


    Public Property CurrentPage() As Integer
        Get
            If ViewState("PageNumber") IsNot Nothing Then
                Return Convert.ToInt32(ViewState("PageNumber"))
            Else
                Return 0
            End If
        End Get
        Set
            ViewState("PageNumber") = Value
        End Set
    End Property

    Private _TotalRecord As Integer
    Private Property TotalRecord() As Integer
        Get
            If ViewState("TotalRecord") IsNot Nothing Then
                Return Convert.ToInt32(ViewState("TotalRecord"))
            Else
                Return 0
            End If
        End Get
        Set
            ViewState("TotalRecord") = Value
        End Set
    End Property

    Public Function IsAuthorize(iAuthorid As Integer) As Boolean
        If IsUserAdmin Then
            Return True
        End If
        If UserInfo.UserID = iAuthorid Then
            Return True
        End If
        Return False
    End Function
    Public Function IsCommentAuthorize(iAuthorid As Integer) As Boolean

        If UserInfo.UserID = iAuthorid Then
            Return True
        End If
        Return False
    End Function


    'Public Function GetBackgroundImage(ByVal iAuthorID As Integer) As String
    '    Dim oUser As DotNetNuke.Entities.Users.UserInfo = GetUser(iAuthorID)
    '    If Not oUser Is Nothing Then
    '        Return oUser.Profile.PhotoURL
    '    End If
    '    Return ""
    'End Function
    Private ReadOnly Property PageSize() As Integer
        Get

            Return 20
        End Get
    End Property


    Private Sub AddLikes(ByVal iContentID As Integer, ItemType As String)

        Dim Ctrl As New DAL2.BlogContent.BlogContentController()
        Ctrl.Post(iContentID, UserInfo.UserID, ItemType)
    End Sub

    Private Sub DeleteComment(ByVal iContentID As Integer)

        Dim Ctrl As New DAL2.BlogContent.BlogContentController()
        Ctrl.DeleteLikes(iContentID, UserInfo.UserID, "Comment")

        ''DELETE COMMMENTS

        CommentsController.DeleteComment(iContentID)

    End Sub

    Private Sub DeleteCommentAttachment(ByVal iContentID As Integer)

        Dim Ctrl As New DAL2.BlogContent.BlogContentController()


        Ctrl.DeleteCommentAttachment(iContentID)

        'DELETE COMMMENT ATTACHMENT



    End Sub

    Private Sub BindData(HasSeen As Integer)
        btnAllBlog.CssClass = "btnctrl"
        btntodo.CssClass = "btnctrl"
        btnDoneBlog.CssClass = "btnctrl"
        If GetQueryStringValue("HasSeen", 0) = 1 Then
            btntodo.CssClass = "btnctrlactive"
        ElseIf GetQueryStringValue("HasSeen", 0) = 2 Then
            btnDoneBlog.CssClass = "btnctrlactive"
        ElseIf GetQueryStringValue("HasSeen", 0) = 0 Then
            '  btnAllBlog.CssClass = "btnctrlactive"
            'Else
            '    btnAllBlog.CssClass = "btnctrlactives"

        End If
        Dim strSubOption As String = "All"
        If (Session("CA_Blog_Status") IsNot Nothing) Then
            Dim iStatus As Integer = Integer.Parse(Session("CA_Blog_Status").ToString())
            If (iStatus = 0) Then
                strSubOption = "All"
            ElseIf (iStatus = 1) Then
                strSubOption = "To-Do"
            Else
                strSubOption = "Completed"
            End If
            ''strSubOption = ((iStatus == 0) ? "All" : ((iStatus == 1) ? "To-Do" : "Completed"))
        End If
        Dim IsMyVault As Integer = iIsAll
        If (Session("CA_Blog_IsMyVault") IsNot Nothing) Then
            IsMyVault = Integer.Parse(Session("CA_Blog_IsMyVault").ToString())
        End If

        Dim CatID As Integer = -1
        If (Session("CA_Blog_CategoryID") IsNot Nothing) Then

            CatID = Integer.Parse(Session("CA_Blog_CategoryID").ToString())
        End If
        Dim SearchKey As String = Server.UrlDecode(GetQueryStringValue("q", ""))
        pnlSearch.Visible = True
        If SearchKey = "" Then
            SearchKey = ""
            ' 
            Session("CA_Blog_SearchKey") = ""
        Else
            pnlSearch.Visible = False

        End If

        Dim ocontroller As New DAL2.BlogContent.BlogContentController()

        Dim oblog As List(Of DAL2.BlogContent.BlogPostInfo)
        If IsUserAdmin Then
            'oblog = ocontroller.GetAllBlogPosts(IsUserAdmin, GetQueryStringValue("CategoryID", -1), GetQueryStringValue("SortBy", 0)).ToList()
            oblog = ocontroller.GetAllBlogPosts(True, UserId, GetQueryStringValue("CategoryID", -1), strSubOption, UserId, SearchKey, GetQueryStringValue("SortBy", 4)).ToList()
        Else
            Try
                If IsMyVault = iIsAll Then
                    oblog = ocontroller.GetAllBlogPosts(False, -1, GetQueryStringValue("CategoryID", -1), strSubOption, Integer.Parse(IIf((IsMyVault = 0), UserInfo.UserID, -1).ToString()), SearchKey, GetQueryStringValue("SortBy", 4)).ToList()
                Else

                    oblog = ocontroller.GetAllBlogPosts(False, UserId, GetQueryStringValue("CategoryID", -1), strSubOption, Integer.Parse(IIf((IsMyVault = 0), UserInfo.UserID, -1).ToString()), SearchKey, GetQueryStringValue("SortBy", 4)).ToList()

                End If
            Catch ex As Exception
            End Try
        End If

        topheader.UpdatePostCount(Convert.ToString(oblog.Count))
        If GetQueryStringValue("SortBy", -1) > -2 Then
            ddSortBy.Items.FindByValue(GetQueryStringValue("SortBy", -1).ToString()).Selected = If(ddSortBy.Items.FindByValue(GetQueryStringValue("SortBy", -1).ToString()) IsNot Nothing, True, False)
        End If

        If oblog Is Nothing Or oblog.Count = 0 Then
            pnlListings.Visible = True
            Paging.Visible = False
            pnlDetails.Visible = False
            ltrlCtrl.Text = ReadTemplate("NoRecord")
            divfilter.Attributes.Add("style", "display:none")
            Div1.Attributes.Add("style", "display:none")

        Else
            TotalRecord = oblog.ToList().Count

            pnlListings.Visible = True
            pnlDetails.Visible = False
            Dim StartRow As Integer = ((CurrentPage - 1) * PageSize)

            oblog = oblog.Skip(StartRow).Take(PageSize).ToList()

            Dim paging As Integer = Convert.ToInt32(TotalRecord / PageSize + (If((TotalRecord Mod PageSize) > 0, 1, 0)))
            lblPagerInfo.Text = String.Format("{0} of {1}", (CurrentPage).ToString(), paging)
            lblPaingTop.Text = lblPagerInfo.Text
            SetPageButtons()

            Dim strTempate As String = ReadTemplate("BlogListings")
            Dim strData As System.Text.StringBuilder = New StringBuilder()
            For Each oblogPostInfo As DAL2.BlogContent.BlogPostInfo In oblog
                strData.Append(ReplaceToken(oblogPostInfo, strTempate, False))
            Next

            ltrlCtrl.Text = strData.ToString()



            hdnCurrentPage.Value = "0"
            LoadPreviousPost(-1)
        End If
    End Sub


    Private Function SaveComments(ByVal strBody As String, ByVal iParentId As Integer) As Integer
        Try
            'Have permission to add comment
            'If BlogContext.Security.CanAddComment = False Then
            '    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId))
            'End If
            Dim iContentid As Integer = GetQueryStringValue(DAL2.BlogContent.Util.PostKey, 1)
            'Is Blog post selected



            Dim CurrentUserid As Integer = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID
            Dim user As DotNetNuke.Entities.Users.UserInfo = DotNetNuke.Entities.Users.UserController.Instance.GetUserById(0, CurrentUserid)
            Dim DAL3 As New BlogContentController()
            Dim toEmail As String = DAL3.GetMail()
            Dim ContentSubject As String = DAL3.GetBlogComponentSubject(DAL2.BlogContent.Util.PostKey)

            Dim Subject As String = ""
            Dim BodyContent As String = ""
            Dim attach As String()


            Subject = user.DisplayName + ":Commented On Post in Forum"
            BodyContent = user.DisplayName + ":Commented the Posts in Insights.      " + "The Subject heading of the post:" + ContentSubject

            DotNetNuke.Services.Mail.Mail.SendMail("members@clearaction.com", toEmail, "", "", toEmail, DotNetNuke.Services.Mail.MailPriority.High, Subject, DotNetNuke.Services.Mail.MailFormat.Text, System.Text.Encoding.UTF32, BodyContent, attach, "", "", "", "", True)

















            If GetQueryStringValue("ContentItemId", 0) = 0 Then
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId))
            End If
            Dim oCommentInfo As New CommentInfo
            With oCommentInfo
                .Approved = True
                .Author = UserInfo.UserID.ToString()
                .ContentItemId = iContentid
                .CreatedByUserID = UserInfo.UserID
                .CreatedOnDate = System.DateTime.Now
                .DisplayName = UserInfo.DisplayName
                .Reports = 1
                .Email = UserInfo.Email
                .Username = UserInfo.Username
                .Website = ""
                .ParentId = iParentId
                .LastModifiedByUserID = UserInfo.UserID
                .LastModifiedOnDate = System.DateTime.Now
                .Comment = Common.Globals.RemoveHtmlTags(strBody)
                .ParentId = iParentId
            End With

            If Not String.IsNullOrEmpty(hdnFileName.Value) And Not String.IsNullOrEmpty(hdnFileUrl.Value) Then
                oCommentInfo.attachUrl = hdnFileUrl.Value
                oCommentInfo.AttachName = hdnFileName.Value
                hdnFileUrl.Value = ""
                hdnFileName.Value = ""
            End If

            '   Dim ctrlController As CommentsController
            Dim iCommentID As Integer = CommentsController.AddComment(oCommentInfo, UserInfo.UserID, iParentId)
            BindComments(iContentid)
            Dim oController As New DAL2.BlogContent.BlogContentController()
            'update component tables
            oController.UpdateBlogComponentManager(UserId, GetQueryStringValue("ContentItemId", -1))


            Return iCommentID
        Catch ex As Exception

        End Try

    End Function

    Private Function UpdateComment(ByVal strBody As String, ByVal CommentID As Integer) As Integer
        Try
            'Have permission to add comment
            'If BlogContext.Security.CanAddComment = False Then
            '    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId))
            'End If

            Dim oController As New DAL2.BlogContent.BlogContentController()

            Dim oCommentInfo As PostCommentInfo = oController.GetCommentByCommentID(CommentID)

            If oCommentInfo Is Nothing Then
                Return CommentID

            End If
            oCommentInfo.Comment = strBody
            oCommentInfo.LastModifiedByUserID = UserInfo.UserID
            oCommentInfo.LastModifiedOnDate = System.DateTime.Now

            '   Dim ctrlController As CommentsController
            oController.UpdateComment(oCommentInfo)

            'update component tables
            ' oController.UpdateBlogComponentManager(UserId, GetQueryStringValue("ContentItemId", -1))
            Return CommentID
        Catch ex As Exception

        End Try

    End Function


    Private Function SaveCommentsReplies(ByVal strBody As String, ByVal iParentId As Integer, ByVal AttachmentURL As String, ByVal AttachmentName As String) As Integer
        Try
            'Have permission to add comment
            'If BlogContext.Security.CanAddComment = False Then
            '    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId))
            'End If

            Dim iContentid As Integer = GetQueryStringValue(DAL2.BlogContent.Util.PostKey, 1)
            'Is Blog post selected
            If GetQueryStringValue("ContentItemId", 0) = 0 Then
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId))
            End If
            Dim oCommentInfo As New CommentInfo
            With oCommentInfo
                .Approved = True
                .Author = UserInfo.UserID.ToString()
                .ContentItemId = iContentid
                .CreatedByUserID = UserInfo.UserID
                .CreatedOnDate = System.DateTime.Now
                .DisplayName = UserInfo.DisplayName
                .Reports = 1
                .Email = UserInfo.Email
                .Username = UserInfo.Username
                .Website = ""
                .ParentId = iParentId
                .LastModifiedByUserID = UserInfo.UserID
                .LastModifiedOnDate = System.DateTime.Now
                .Comment = Common.Globals.RemoveHtmlTags(strBody)
                .ParentId = iParentId
            End With

            If Not String.IsNullOrEmpty(AttachmentURL) And Not String.IsNullOrEmpty(AttachmentName) Then
                oCommentInfo.attachUrl = AttachmentURL
                oCommentInfo.AttachName = AttachmentName

            End If

            '   Dim ctrlController As CommentsController
            Dim icomment As Integer = CommentsController.AddComment(oCommentInfo, UserInfo.UserID, iParentId)
            BindComments(iContentid)
            Dim oController As New DAL2.BlogContent.BlogContentController()
            'update component tables
            oController.UpdateBlogComponentManager(UserId, GetQueryStringValue("ContentItemId", -1))
            Return icomment
        Catch ex As Exception

        End Try

    End Function





#Region "Paging Buttons"

    Private Sub SetPageButtons()

        NavImageLast.Enabled = False
        NavImageNext.Enabled = False
        NavImagePrev.Enabled = False
        NavFirstBtn.Enabled = False

        NavImageLast1.Enabled = False
        NavImageNext1.Enabled = False
        NavImagePrev1.Enabled = False
        NavFirstBtn1.Enabled = False

        If CurrentPage = 1 AndAlso TotalRecord > CurrentPage * PageSize Then
            NavImageNext.Enabled = True
            NavImageLast.Enabled = True
            NavImageNext1.Enabled = True
            NavImageLast1.Enabled = True
        End If

        If ((CurrentPage * PageSize) + 1 >= TotalRecord) Then
            NavImagePrev.Enabled = True
            NavFirstBtn.Enabled = True
            NavImagePrev1.Enabled = True
            NavFirstBtn1.Enabled = True

        End If
        If ((CurrentPage * PageSize) + 1 <= TotalRecord AndAlso CurrentPage > 1) Then
            NavImageNext.Enabled = True
            NavImageLast.Enabled = True
            NavImagePrev.Enabled = True
            NavFirstBtn.Enabled = True


            NavImageNext1.Enabled = True
            NavImageLast1.Enabled = True
            NavImagePrev1.Enabled = True
            NavFirstBtn1.Enabled = True
        End If

    End Sub

    Protected Sub NavFirstBtn_Click(sender As Object, e As ImageClickEventArgs) Handles NavFirstBtn.Click, NavFirstBtn1.Click
        CurrentPage = 1
        BindData(1)
    End Sub

    Protected Sub NavImagePrev_Click(sender As Object, e As ImageClickEventArgs) Handles NavImagePrev.Click, NavImagePrev1.Click
        CurrentPage = CurrentPage - 1
        BindData(1)
    End Sub

    Protected Sub NavImageNext_Click(sender As Object, e As ImageClickEventArgs) Handles NavImageNext.Click, NavImageNext1.Click

        CurrentPage = CurrentPage + 1
        BindData(1)

    End Sub

    Protected Sub NavImageLast_Click(sender As Object, e As ImageClickEventArgs) Handles NavImageLast.Click, NavImageLast1.Click
        CurrentPage = Convert.ToInt32(TotalRecord / PageSize)
        BindData(1)
    End Sub

#End Region

#Region " Private Members "
    Private _urlParameters As New List(Of String)
    Private _pageSize As Integer = -1
    Private _totalRecords As Integer = 0
    Private _reqPage As Integer = 1
    Private _usePaging As Boolean = False
#End Region

#Region " Event Handlers "

    Protected Sub btnAllBlog_Click(sender As Object, e As EventArgs) Handles btnAllBlog.Click
        Session("CA_Blog_Status") = 0



        If (GetQueryStringValue("q", "") <> "") Then
            Response.Redirect(BuildQuery(UserId, GetQueryStringValue("CategoryId", -1), SortBy, -1, GetQueryStringValue("q", "")))
        Else
            Response.Redirect(BuildQuery(UserId, GetQueryStringValue("CategoryId", -1), GetQueryStringValue("SortBy", -1), -1))

        End If
    End Sub

    Protected Sub btntodo_Click(sender As Object, e As EventArgs) Handles btntodo.Click
        Session("CA_Blog_Status") = 1


        If (GetQueryStringValue("q", "") <> "") Then
            Response.Redirect(BuildQuery(UserId, GetQueryStringValue("CategoryId", -1), SortBy, -1, GetQueryStringValue("q", "")))
        Else
            Response.Redirect(BuildQuery(UserId, GetQueryStringValue("CategoryId", -1), GetQueryStringValue("SortBy", -1), -1))

        End If
    End Sub

    Protected Sub btnDoneBlog_Click(sender As Object, e As EventArgs) Handles btnDoneBlog.Click
        Session("CA_Blog_Status") = 2

        If (GetQueryStringValue("q", "") <> "") Then
            Response.Redirect(BuildQuery(UserId, GetQueryStringValue("CategoryId", -1), SortBy, -1, GetQueryStringValue("q", "")))
        Else
            Response.Redirect(BuildQuery(UserId, GetQueryStringValue("CategoryId", -1), GetQueryStringValue("SortBy", -1), -1))

        End If


    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs) Handles lnkSave.Click

        If String.IsNullOrEmpty(txtComments.Text) Then
            Return
        End If
        Dim icomment As Integer = SaveComments(txtComments.Text, -1)
        SaveAttachment(icomment)
        txtComments.Text = ""
        '  bindSingleRecord(GetQueryStringValue("ContentItemId", -1))

        Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", DAL2.BlogContent.Util.PostKey & "=" & GetQueryStringValue(DAL2.BlogContent.Util.PostKey, -1), "CommentID=" + icomment.ToString()))



    End Sub
    Protected Sub ddGlobalCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        CurrentPage = 1
        BindData(1)
    End Sub


    Protected Sub rptComments_ItemCommand(source As Object, e As RepeaterCommandEventArgs) Handles rptComments.ItemCommand
        Dim hCommentId As HiddenField = CType(e.Item.FindControl("CommentID"), HiddenField)
        Dim icomment As Integer = Convert.ToInt32(hCommentId.Value)
        If e.CommandName = "SaveDisscussion" Then


            If Not hCommentId Is Nothing Then
                Dim txtInnerQuickReply As TextBox = CType(e.Item.FindControl("txtInnerQuickReply"), TextBox)

                If txtInnerQuickReply.Text <> "" Then
                    'SaveComments(txtInnerQuickReply.Text, Convert.ToInt32(hCommentId.Value))

                    icomment = SaveCommentsReplies(txtInnerQuickReply.Text, Convert.ToInt32(hCommentId.Value), "", "")
                    SaveAttachment(icomment)
                End If

                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", DAL2.BlogContent.Util.PostKey & "=" & GetQueryStringValue("ContentItemID", -1), "CommentID=" + icomment.ToString()))

                '   Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", "ContentItemID=" & GetQueryStringValue("ContentItemID", -1)))
            End If
        ElseIf e.CommandName = "EditReply" Then
            Dim txtInput As TextBox = CType(e.Item.FindControl("txtReplyEdit"), TextBox)
            If txtInput.Text <> "" Then
                UpdateComment(txtInput.Text, icomment)
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", DAL2.BlogContent.Util.PostKey & "=" & GetQueryStringValue("ContentItemID", -1), "CommentID=" + icomment.ToString()))
            End If


        ElseIf e.CommandName = "like" Then
            AddLikes(Convert.ToInt32(hCommentId.Value), "Comment")


        ElseIf e.CommandName = "delete" Then
            DeleteComment(Convert.ToInt32(hCommentId.Value))

        ElseIf e.CommandName = "deleteAttachment" Then
            DeleteCommentAttachment(Convert.ToInt32(hCommentId.Value))

        End If





        Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", DAL2.BlogContent.Util.PostKey & "=" & GetQueryStringValue(DAL2.BlogContent.Util.PostKey, -1), "CommentID=" + icomment.ToString()))
    End Sub

    Protected Sub rptComments_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptComments.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim eCommentID As HiddenField = CType(e.Item.FindControl("hdnCommentId"), HiddenField)

            Dim oController As New DAL2.BlogContent.BlogContentController()


            Dim rptInnercomments As Repeater = CType(e.Item.FindControl("rptInnerComments"), Repeater)

            If Not rptInnercomments Is Nothing Then
                Dim olist As List(Of PostCommentInfo) = oController.GetCommentByContentID(False, GetQueryStringValue("ContentItemID", -1), Convert.ToInt32(eCommentID.Value))
                rptInnercomments.DataSource = olist
                rptInnercomments.DataBind()
                Dim imgdelete As ImageButton = CType(e.Item.FindControl("imgdelete"), ImageButton)
                If olist Is Nothing Or olist.Count > 0 Then

                    If Not imgdelete Is Nothing Then
                        imgdelete.Visible = False

                    End If
                Else
                    imgdelete.Visible = IsCommentAuthorize(CType(e.Item.DataItem, PostCommentInfo).CreatedByUserID)


                End If

            End If



        End If
    End Sub
    Protected Sub rptInnerComments_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim eCommentID As HiddenField = CType(e.Item.FindControl("CommentID"), HiddenField)

            If Not eCommentID Is Nothing Then
                Dim oController As New DAL2.BlogContent.BlogContentController()


                Dim rptInnercomments As Repeater = CType(e.Item.FindControl("rptInnerComments2"), Repeater)




                If Not rptInnercomments Is Nothing Then
                    Dim olist As List(Of PostCommentInfo) = oController.GetCommentByContentID(False, GetQueryStringValue("ContentItemID", -1), Convert.ToInt32(eCommentID.Value))
                    rptInnercomments.DataSource = olist
                    rptInnercomments.DataBind()
                    Dim imgdelete As ImageButton = CType(e.Item.FindControl("imgdelete"), ImageButton)
                    imgdelete.Visible = IsCommentAuthorize(CType(e.Item.DataItem, PostCommentInfo).CreatedByUserID)
                    If olist Is Nothing Or olist.Count > 0 Then
                        imgdelete.Visible = False
                    End If

                End If


            End If

        End If
    End Sub
    Protected Sub rptInnerComments2_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim eCommentID As HiddenField = CType(e.Item.FindControl("CommentID"), HiddenField)



        End If
    End Sub

    Private Sub cmdBlog_Click(sender As Object, e As EventArgs) Handles btnAddBlog.Click

        Response.Redirect(EditUrl("Blog", "1", "PostEdit", DAL2.BlogContent.Util.PostKey & "=-1"), False)
        'If BlogContext.BlogId <> -1 Then
        '    Response.Redirect(EditUrl("Blog", BlogContext.BlogId.ToString, "PostEdit"), False)
        'Else
        '    If BlogContext.Security.IsEditor Then
        '        Dim b1 As BlogInfo = BlogsController.GetBlogsByModule(Settings.ModuleId, UserId, BlogContext.Locale).Values.First
        '        Response.Redirect(EditUrl("Blog", b1.BlogID.ToString, "PostEdit", DAL2.BlogContent.Util.PostKey & "=-1"), False)
        '    Else
        '        Dim b1 As BlogInfo = BlogsController.GetBlogsByModule(Settings.ModuleId, UserId, BlogContext.Locale).Values.FirstOrDefault(Function(b) b.OwnerUserId = UserId Or b.CanAdd Or (b.CanEdit And BlogContext.ContentItemId > -1))

        '    End If
        'End If
    End Sub

    Protected Sub rptInnerComments_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        Dim hCommentId As HiddenField = CType(e.Item.FindControl("CommentID"), HiddenField)
        Dim icomment As Integer = Convert.ToInt32(hCommentId.Value)
        If e.CommandName = "SaveDisscussion" Then

            If Not hCommentId Is Nothing Then
                Dim txtInnerQuickReply As TextBox = CType(e.Item.FindControl("txtInnerQuickReply"), TextBox)


                If txtInnerQuickReply.Text <> "" Then
                    'SaveComments(txtInnerQuickReply.Text, Convert.ToInt32(hCommentId.Value))

                    icomment = SaveCommentsReplies(txtInnerQuickReply.Text, Convert.ToInt32(hCommentId.Value), "", "")
                    SaveAttachment(icomment)

                End If
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", DAL2.BlogContent.Util.PostKey & "=" & GetQueryStringValue("ContentItemID", -1), "CommentID=" + icomment.ToString()))

                '   Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", "ContentItemID=" & GetQueryStringValue("ContentItemID", -1)))
            End If
        End If

        If e.CommandName = "EditReply" Then
            Dim txtInput As TextBox = CType(e.Item.FindControl("txtReplyChildEdit"), TextBox)
            If txtInput.Text <> "" Then
                UpdateComment(txtInput.Text, icomment)
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", DAL2.BlogContent.Util.PostKey & "=" & GetQueryStringValue("ContentItemID", -1), "CommentID=" + icomment.ToString()))
            End If

        End If

        If e.CommandName = "like" Then
            AddLikes(Convert.ToInt32(e.CommandArgument), "Comment")
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", DAL2.BlogContent.Util.PostKey & "=" & GetQueryStringValue(DAL2.BlogContent.Util.PostKey, "-1")))
        ElseIf e.CommandName = "delete" Then
            DeleteComment(Convert.ToInt32(e.CommandArgument))
        ElseIf e.CommandName = "deleteAttachment" Then
            DeleteCommentAttachment(Convert.ToInt32(e.CommandArgument))

        End If

        Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", DAL2.BlogContent.Util.PostKey & "=" & GetQueryStringValue("ContentItemID", -1)))
    End Sub

    Protected Sub rptInnerComments2_ItemCommand(source As Object, e As RepeaterCommandEventArgs)

        Dim hCommentId As HiddenField = CType(e.Item.FindControl("hdnCommentId2"), HiddenField)
        Dim icomment As Integer = Convert.ToInt32(hCommentId.Value)
        If e.CommandName = "like" Then
            AddLikes(Convert.ToInt32(e.CommandArgument), "Comment")
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", DAL2.BlogContent.Util.PostKey & "=" & GetQueryStringValue(DAL2.BlogContent.Util.PostKey, "-1")))
        ElseIf e.CommandName = "delete" Then
            DeleteComment(Convert.ToInt32(e.CommandArgument))
        ElseIf e.CommandName = "deleteAttachment" Then
            DeleteCommentAttachment(Convert.ToInt32(e.CommandArgument))

        ElseIf e.CommandName = "EditReply" Then
            Dim txtInput As TextBox = CType(e.Item.FindControl("txtReplyChildEdit2"), TextBox)
            If txtInput.Text <> "" Then
                UpdateComment(txtInput.Text, icomment)
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", DAL2.BlogContent.Util.PostKey & "=" & GetQueryStringValue("ContentItemID", -1), "CommentID=" + icomment.ToString()))
            End If

        End If



        Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", DAL2.BlogContent.Util.PostKey & "=" & GetQueryStringValue("ContentItemID", -1)))
    End Sub

    Private Sub Page_Init1(sender As Object, e As EventArgs) Handles Me.Init
        Integration.BlogModuleController.CheckupOnImportedFiles(ModuleId)


    End Sub
    Public Sub PageSeo(ByVal oBlogPost As DAL2.BlogContent.BlogPostInfo)

        ' Dim oPage As Page = CType(DotNetNuke.Framework.CDefault,Me.Page)

    End Sub

    Public Sub bindSingleRecord(ByVal iContentItemId As Integer)
        Dim oController As New DAL2.BlogContent.BlogContentController()
        Dim iBlog As DAL2.BlogContent.BlogPostInfo = oController.GetBlogContentById(iContentItemId, UserInfo.UserID)
        'If iBlog Is Nothing Then
        '    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId))
        'End If
        pnlDetails.Visible = True
        pnlListings.Visible = False
        Paging.Visible = False


        Dim strTemplate As String = ReadTemplate("BlogDetail")
        ltrlCtrlDetail.Text = ReplaceToken(iBlog, strTemplate, True)
        ltrlBlogHeader.Text = ReplaceToken(iBlog, ReadTemplate("BlogListingSubTemplate"), False).ToString()

        BindComments(iBlog.ContentItemId)
        lnkSave.Attributes.Add("currentPostContenetId", iBlog.ContentItemId.ToString())

        'Dim strkey As String = "BlogContentVisit_" + GetQueryStringValue("ContentItemId", -1).ToString()

        'If Request.Cookies(strkey) Is Nothing Then
        '    Dim myCookie As HttpCookie = New HttpCookie(strkey)
        '    myCookie.Value = GetQueryStringValue("ContentItemId", -1).ToString()
        '    myCookie.Expires = System.DateTime.Now.AddYears(10)
        '    Response.Cookies.Add(myCookie)

        'End If

        iBlog.ViewCount = iBlog.ViewCount + 1
        'Update the blog view count
        oController.UpdateBlogContent(iBlog)


        'update component tables
        If Not Request.QueryString("UpdateStatus") Is Nothing Then
            pnlTopHeader.Visible = False
            pnlListings.Visible = False
        End If
        Try
            oController.UpdateBlogComponentManager(UserId, iBlog.ContentItemId)

            AddOpenGraphMetaTags(iBlog)
        Catch ex As Exception
            ' nothing to do 
        End Try



        'Bind Active member and List of attachment


        Dim olist As List(Of DAL2.BlogContent.UserData) = oController.RecentMemberByBlogContentID(iBlog.ContentItemId)
        rptActiveMember.DataSource = olist
        rptActiveMember.DataBind()
        lblMemberCount.Text = "0"
        If Not olist Is Nothing Then
            lblMemberCount.Text = olist.Count.ToString()
        End If

        Dim olistfiles As List(Of Blog_Attachments) = oController.GetAttachmentByPostID(iBlog.ContentItemId).OrderBy(Function(x) x.UserID).ToList()
        Dim iCurrentID As Integer = -1
        strTemplate = ReadTemplate("BlogSharedFiles")
        Dim strData As System.Text.StringBuilder = New System.Text.StringBuilder

        If olistfiles Is Nothing Then
            lblNum.Text = "0"
        Else
            lblNum.Text = olistfiles.Count.ToString()

            For Each oattach As Blog_Attachments In olistfiles

                Dim strTemp As String = strTemplate

                Convert.ToString(olist.Count)
                If (iCurrentID <> oattach.UserID) Then
                    strTemp = strTemp.Replace("{DISPLAY}", "block")
                Else
                    strTemp = strTemp.Replace("{DISPLAY}", "none")
                End If

                iCurrentID = oattach.UserID
                strTemp = strTemp.Replace("{DISPLAYNAME}", GetDisplayName(oattach.UserID))
                strTemp = strTemp.Replace("{FILEURL}", oattach.FileUrl)
                strTemp = strTemp.Replace("{ATTACHID}", oattach.AttachId.ToString())
                strTemp = strTemp.Replace("{FILENAME}", oattach.FileName.ToString())
                strTemp = strTemp.Replace("{DATEADDED}", DAL2.BlogContent.Util.HumanFriendlyDate(oattach.DateAdded))
                strData.Append(strTemp)
            Next
            ltrlFiles.Text = strData.ToString

        End If




    End Sub
    Private Sub BindCommentsAuthors()

        Dim lstAuthor As List(Of Integer) = New List(Of Integer)()
        Dim sAuthorList As New StringBuilder
        Dim oController As New DAL2.BlogContent.BlogContentController()
        Dim oComments As List(Of PostCommentInfo) = oController.GetCommentAuthor().ToList()
        Dim strTemplate As String = ReadTemplate("CommentAuthors")
        For Each opostComment As PostCommentInfo In oComments

            If Not lstAuthor.Contains(opostComment.CreatedByUserID) Then
                'Filtter only for active user
                If Not DotNetNuke.Entities.Users.UserController.GetUserById(PortalId, opostComment.CreatedByUserID) Is Nothing Then
                    lstAuthor.Add(opostComment.CreatedByUserID)
                    Dim sReplacedData As String = ReplaceUserTokens(strTemplate, opostComment.AuthorDetails, True)
                    sReplacedData = sReplacedData.Replace("{URL}", GetBlogDetailUrl(opostComment.ContentItemId, True, ""))
                    sAuthorList.Append(sReplacedData)
                End If

            End If



        Next
        ltrAuthorlist.Text = sAuthorList.ToString()
    End Sub
    Private Sub BindComments(ByVal iContentId As Integer)
        Dim oController As New DAL2.BlogContent.BlogContentController()
        Dim oComments As List(Of PostCommentInfo) = oController.GetCommentByContentID(IsUserAdmin, iContentId, -1)
        rptComments.DataSource = oComments
        rptComments.DataBind()




    End Sub

    Public Sub LoadPreviousPost(ByVal iContentItemid As Integer)
        Dim oController As New DAL2.BlogContent.BlogContentController()
        Dim total As Integer = 0
        Dim oBlogList As List(Of DAL2.BlogContent.BlogPostInfo) = oController.GetBlogList(iContentItemid, 0, 15, total)
        hdnTotalRecord.Value = total.ToString()
        Dim strText As New StringBuilder
        If Not oBlogList Is Nothing Then
            For Each oblog As DAL2.BlogContent.BlogPostInfo In oBlogList
                strText.Append(String.Format("<li><a href='{0}'>{1}</a></li>", GetBlogDetailUrl(oblog.ContentItemId, True, oblog.Title), oblog.Title))
            Next
        End If
        ltrPreviousPost.Text = strText.ToString()
    End Sub

    Protected Sub ddSortBy_SelectedIndexChanged(sender As Object, e As EventArgs)
        If (GetQueryStringValue("q", "") <> "") Then
            Response.Redirect(BuildQuery(UserId, GetQueryStringValue("CategoryId", -1), SortBy, -1, GetQueryStringValue("q", "")))
        Else
            Response.Redirect(BuildQuery(UserId, GetQueryStringValue("CategoryId", -1), SortBy, -1))

        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            '   SetUI()
            Try
                Context.Response.Cache.SetCacheability(HttpCacheability.NoCache)
                topheader.tID = Me.TabId
                If DotNetNuke.Security.Permissions.ModulePermissionController.CanEditModuleContent(Me.ModuleConfiguration) Or HasModulePermission("BLOGGER") Then
                    btnAddBlog.Visible = True
                    topheader.CanEdit = True
                Else
                    btnAddBlog.Visible = False
                    topheader.CanEdit = False
                End If
            Catch exc As Exception
                DotNetNuke.Services.Exceptions.ProcessModuleLoadException(Me, exc)
            End Try

            If GetQueryStringValue(DAL2.BlogContent.Util.PostKey, -1) = -1 Then
                CurrentPage = 1
                BindData(0)


            Else

                bindSingleRecord(GetQueryStringValue(DAL2.BlogContent.Util.PostKey, -1))

            End If


            BindCommentsAuthors()
        End If

    End Sub
#End Region

#Region " Open Graph Meta Tags "
    Private Sub AddOpenGraphMetaTags(ByVal oBlog As DAL2.BlogContent.BlogPostInfo)
        Dim URL As String = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host
        DirectCast(Me.Page, DotNetNuke.Framework.CDefault).Title = "Insights | " & System.Text.RegularExpressions.Regex.Replace(oBlog.Title, "[^a-zA-Z0-9]", " ")
        DirectCast(Me.Page, DotNetNuke.Framework.CDefault).MetaKeywords &= "Insights, " & System.Text.RegularExpressions.Regex.Replace(oBlog.Title, "[^a-zA-Z0-9]", " ").Replace(" ", ",") & " ClearAction Team"

        Page.Header.Controls.Add(New LiteralControl(String.Format("<meta id=""ogurl"" property=""og:url"" content=""{0}"" />", oBlog.GetBlogDetailUrl())))
        Page.Header.Controls.Add(New LiteralControl(String.Format("<meta content=""{0}"" name=""twitter:url"">", oBlog.GetBlogDetailUrl())))
        '  Page.Header.Controls.Add(New LiteralControl(String.Format("<meta id=""ogtitle"" property=""og:title"" content=""{0}"" />", CleanStringForXmlAttribute(BlogContext.Post.LocalizedTitle))))
        ' Page.Header.Controls.Add(New LiteralControl(String.Format("<meta content=""{0}"" name=""twitter:title"">", CleanStringForXmlAttribute(BlogContext.Post.LocalizedTitle))))
        Dim description As String = BlogContext.Post.ShortDescription '= CleanStringForXmlAttribute(DotNetNuke.Common.Utilities.HtmlUtils.Clean(BlogContext.Post.LocalizedSummary, False))
        If (Not String.IsNullOrEmpty(description)) Then
            Page.Header.Controls.Add(New LiteralControl(String.Format("<meta id=""ogdescription"" property=""og:description"" content=""{0}"" />", description)))
            Page.Header.Controls.Add(New LiteralControl(String.Format("<meta content=""{0}"" name=""twitter:description"">", description)))
        End If
        If Not String.IsNullOrEmpty(BlogContext.Post.Image) Then
            '   Dim strPath As String = String.Format("{0}?TabId={1}&ModuleId={2}&Blog={3}&Post={4}&w=1200&h=630&c=1&key={5}", glbImageHandlerPath, TabId.ToString, Settings.ModuleId.ToString, BlogContext.BlogId.ToString, BlogContext.ContentItemId.ToString, BlogContext.Post.Image)
            '       Page.Header.Controls.Add(New LiteralControl(String.Format("<meta id=""ogimage"" property=""og:image"" content=""{0}"" />", URL + ResolveUrl(strPath))))
            '      Page.Header.Controls.Add(New LiteralControl(String.Format("<meta content=""{0}"" name=""twitter:image"">", URL + ResolveUrl(strPath))))
            '    Page.Header.Controls.Add(New LiteralControl(String.Format("<meta content=""summary_large_image"" name=""twitter:card"">")))
        End If
        '   Page.Header.Controls.Add(New LiteralControl(String.Format("<meta id=""ogsitename"" property=""og:site_name"" content=""{0}"" />", CleanStringForXmlAttribute(PortalSettings.PortalName))))
        If Not String.IsNullOrEmpty(Settings.FacebookAppId) Then
            Page.Header.Controls.Add(New LiteralControl(String.Format("<meta id=""fbappid"" property=""fb:app_id"" content=""{0}"" />", Settings.FacebookAppId)))
        End If
        Page.Header.Controls.Add(New LiteralControl(String.Format("<meta id=""ogtype"" property=""og:type"" content=""{0}"" />", "article")))
        If Not String.IsNullOrEmpty(BlogContext.Post.Locale) Then
            Page.Header.Controls.Add(New LiteralControl(String.Format("<meta id=""oglocale"" property=""og:locale"" content=""{0}"" />", BlogContext.Post.Locale.Replace("-", "_"))))
        ElseIf Not String.IsNullOrEmpty(BlogContext.Blog.Locale) Then
            Page.Header.Controls.Add(New LiteralControl(String.Format("<meta id=""oglocale"" property=""og:locale"" content=""{0}"" />", BlogContext.Blog.Locale.Replace("-", "_"))))
        Else
            Page.Header.Controls.Add(New LiteralControl(String.Format("<meta id=""oglocale"" property=""og:locale"" content=""{0}"" />", PortalSettings.DefaultLanguage.Replace("-", "_"))))
        End If
        ' Page.Header.Controls.Add(New LiteralControl(String.Format("<meta id=""ogupdatedtime"" property=""og:updated_time"" content=""{0}"" />", BlogContext.Post.LastModifiedOnDate.ToString("u"))))
        'If Settings.FacebookProfileIdProperty <> -1 Then
        '    Dim author As DotNetNuke.Entities.Users.UserInfo = BlogContext.Author
        '    If author Is Nothing Then
        '        author = BlogContext.Post.CreatedByUserID(PortalId)
        '    End If
        '    If author IsNot Nothing Then
        '        Dim pp As DotNetNuke.Entities.Profile.ProfilePropertyDefinition = author.Profile.ProfileProperties.GetById(Settings.FacebookProfileIdProperty)
        '        If pp IsNot Nothing AndAlso Not String.IsNullOrEmpty(pp.PropertyValue) Then
        '            Page.Header.Controls.Add(New LiteralControl(String.Format("<meta property=""fb:profile_id"" content=""{0}"" />", pp.PropertyValue)))
        '        End If
        '    End If
        'End If
    End Sub
#End Region

#Region " Public Methods/Function "

    Public Function GetAttachmentDisplayCSS(ByVal TotalAttachment As Integer) As String
        If TotalAttachment = 0 Then
            Return String.Format("display:none")
        End If
        Return String.Format("display:inline")
    End Function
    Public Function GetImageName(ByVal contentID As Integer) As String
        Dim oController As New DAL2.BlogContent.BlogContentController
        Dim IsLike As Boolean = oController.LikeIsLikeByUser(contentID, UserId)
        If IsLike Then
            Return "forum-recommended.png"
        End If
        Return "forum-recommend.png"

    End Function

    Public Function IsAuthorizeToDeleteAttachment(iAuthorid As Integer, AttachmentText As String) As Boolean
        If IsUserAdmin Then

            If AttachmentText = "" Then
                Return False
            End If
            Return True

        End If

        If UserInfo.UserID = iAuthorid Then

            If AttachmentText = "" Then
                Return False
            End If

            Return True
        End If


        Return False

    End Function



    Public Function IsAuthorizeToDeleteAttachmentDevider(iAuthorid As Integer, AttachmentText As String) As String

        If IsUserAdmin Then

            If AttachmentText = "" Then
                Return ""
            End If
            Return "<br>"

        End If

        If UserInfo.UserID = iAuthorid Then

            If AttachmentText = "" Then
                Return ""
            End If

            Return "<br>"
        End If


        Return ""

    End Function

    Private Sub AddWLWManifestLink()
        If Context.Items("WLWManifestLinkAdded") Is Nothing Then
            Dim link As New HtmlLink()
            link.Attributes.Add("rel", "wlwmanifest")
            link.Attributes.Add("type", "application/wlwmanifest+xml")
            If ViewSettings.BlogModuleId = -1 Then
                '    link.Attributes.Add("href", ResolveUrl(ManifestFilePath(TabId, ModuleId)))
                '   Else
                '       link.Attributes.Add("href", ResolveUrl(ManifestFilePath(TabId, ViewSettings.BlogModuleId)))
            End If
            Page.Header.Controls.Add(link)
            Context.Items("WLWManifestLinkAdded") = True
        End If
    End Sub

    Private Sub AddPingBackLink()
        If Context.Items("PingBackLinkAdded") Is Nothing Then
            Dim pingbackUrl As String = Services.BlogRouteMapper.GetRoute(Services.BlogRouteMapper.ServiceControllers.Comments, "Pingback")
            pingbackUrl &= String.Format("?tabId={0}&moduleId={1}&blogId={2}&postId={3}", TabId, BlogContext.BlogModuleId, BlogContext.BlogId, BlogContext.ContentItemId)
            Dim link As New HtmlGenericControl("link")
            link.Attributes.Add("rel", "pingback")
            link.Attributes.Add("href", pingbackUrl)
            Page.Header.Controls.Add(link)
            Context.Items("PingBackLinkAdded") = True
        End If
    End Sub

    Private Sub AddTrackBackBlurb()
        If Context.Items("TrackBackBlurbAdded") Is Nothing Then
            Dim trackbackUrl As String = Services.BlogRouteMapper.GetRoute(Services.BlogRouteMapper.ServiceControllers.Comments, "Trackback")
            trackbackUrl &= String.Format("?tabId={0}&moduleId={1}&blogId={2}&postId={3}", TabId, BlogContext.BlogModuleId, BlogContext.BlogId, BlogContext.ContentItemId)
            Dim postUrl As String = BlogContext.Post.PermaLink(PortalSettings)
            Dim sb As New StringBuilder
            sb.AppendLine("<!--")
            sb.AppendLine(" <rdf:RDF xmlns:rdf=""http://www.w3.org/1999/02/22-rdf-syntax-ns#""")
            sb.AppendLine("  xmlns:dc=""http://purl.org/dc/elements/1.1/""")
            sb.AppendLine("  xmlns:trackback=""http://madskills.com/public/xml/rss/module/trackback/"">")
            sb.AppendFormat("  <rdf:Description rdf:about=""{0}""" & vbCrLf, postUrl)
            sb.AppendFormat("  dc:identifier=""{0}"" dc:Title=""{1}""" & vbCrLf, postUrl, BlogContext.Post.LocalizedTitle)
            sb.AppendFormat("  trackback:ping=""{0}"" />" & vbCrLf, trackbackUrl) ' trackback url
            sb.AppendLine(" </rdf:RDF>")
            sb.AppendLine("-->")
            litTrackback.Text = sb.ToString
            Context.Items("TrackBackBlurbAdded") = True
        End If
    End Sub
#End Region

#Region " Template Data Retrieval "
    Private Sub vtContents_GetData(ByVal DataSource As String, ByVal Parameters As Dictionary(Of String, String), ByRef Replacers As System.Collections.Generic.List(Of GenericTokenReplace), ByRef Arguments As System.Collections.Generic.List(Of String()), callingObject As Object) 'Handles vtContents.GetData

        Select Case DataSource.ToLower

            Case "blogs"

                Dim blogList As IEnumerable(Of BlogInfo) = BlogsController.GetBlogsByModule(BlogContext.BlogModuleId, UserId, BlogContext.Locale).Values.Where(Function(b) b.Published = True).OrderBy(Function(b) b.Title)
                Parameters.ReadValue("pagesize", _pageSize)
                If _pageSize > 0 Then
                    _usePaging = True
                    Dim startRec As Integer = ((_reqPage - 1) * _pageSize) + 1
                    Dim endRec As Integer = _reqPage * _pageSize
                    Dim i As Integer = 1
                    For Each b As BlogInfo In blogList
                        If i >= startRec And i <= endRec Then
                            If BlogContext.ParentModule IsNot Nothing Then
                                b.ParentTabID = BlogContext.ParentModule.TabID
                            End If
                            Replacers.Add(New BlogTokenReplace(Me, b))
                        End If
                        i += 1
                    Next
                Else
                    For Each b As BlogInfo In blogList
                        If BlogContext.ParentModule IsNot Nothing Then
                            b.ParentTabID = BlogContext.ParentModule.TabID
                        End If
                        Replacers.Add(New BlogTokenReplace(Me, b))
                    Next
                End If

            Case "posts"

                Parameters.ReadValue("pagesize", _pageSize)
                EnsurePostList(_pageSize)
                For Each e As PostInfo In PostList
                    If BlogContext.ParentModule IsNot Nothing Then
                        e.ParentTabID = BlogContext.ParentModule.TabID
                    End If
                    Replacers.Add(New BlogTokenReplace(Me, e))
                Next

            Case "postspager"

                Parameters.ReadValue("pagesize", _pageSize)
                EnsurePostList(_pageSize)
                Dim pagerType As String = "allpages"
                Parameters.ReadValue("pagertype", pagerType)
                Dim remdr As Integer = 0
                Dim nrPages As Integer = Math.DivRem(_totalRecords, _pageSize, remdr)
                If remdr > 0 Then
                    nrPages += 1
                End If
                If nrPages < 2 Then
                Else
                    Replacers.Add(New BlogTokenReplace(Me))
                    Select Case pagerType.ToLower
                        Case "allpages"
                            For i As Integer = 1 To nrPages
                                Dim s As String() = {"page=" & i.ToString, "pageiscurrent=" & CBool(i = _reqPage).ToString, "pagename=" & i.ToString, "pagetype=number"}
                                Arguments.Add(s)
                            Next
                        Case "somepages"
                            If _reqPage > 3 Then
                                Dim s As String() = {"page=1", "pageiscurrent=False", "pagename=1", "pagetype=firstpage"}
                                Arguments.Add(s)
                            End If
                            For i As Integer = Math.Max(_reqPage - 2, 1) To Math.Min(_reqPage + 2, nrPages)
                                Dim s As String() = {"page=" & i.ToString, "pageiscurrent=" & CBool(i = _reqPage).ToString, "pagename=" & i.ToString, "pagetype=number"}
                                Arguments.Add(s)
                            Next
                            If _reqPage < nrPages - 3 Then
                                Dim s As String() = {"page=" & nrPages.ToString, "pageiscurrent=False", "pagename=" & nrPages.ToString, "pagetype=lastpage"}
                                Arguments.Add(s)
                            End If
                        Case "newerolder"
                            If _reqPage > 1 Then
                                Dim s As String() = {"page=" & (_reqPage - 1).ToString, "pageiscurrent=False", "pagename=" & LocalizeString("Newer"), "pagetype=previous"}
                                Arguments.Add(s)
                            End If
                            If _reqPage < nrPages Then
                                Dim s As String() = {"page=" & (_reqPage + 1).ToString, "pageiscurrent=False", "pagename=" & LocalizeString("Older"), "pagetype=next"}
                                Arguments.Add(s)
                            End If
                    End Select
                End If

            Case "terms"

                If callingObject IsNot Nothing AndAlso TypeOf callingObject Is PostInfo Then
                    For Each t As TermInfo In CType(callingObject, PostInfo).Terms
                        Replacers.Add(New BlogTokenReplace(Me, BlogContext.Post, t))
                    Next
                ElseIf BlogContext.Post IsNot Nothing Then
                    For Each t As TermInfo In BlogContext.Post.Terms
                        Replacers.Add(New BlogTokenReplace(Me, BlogContext.Post, t))
                    Next
                Else
                    For Each t As TermInfo In TermsController.GetTermsByModule(BlogContext.BlogModuleId, BlogContext.Locale)
                        Replacers.Add(New BlogTokenReplace(Me, Nothing, t))
                    Next
                End If
                _usePaging = False

            Case "allterms"

                For Each t As TermInfo In TermsController.GetTermsByModule(BlogContext.BlogModuleId, BlogContext.Locale)
                    Replacers.Add(New BlogTokenReplace(Me, Nothing, t))
                Next
                _usePaging = False

            Case "keywords", "tags"

                If callingObject IsNot Nothing AndAlso TypeOf callingObject Is PostInfo Then
                    For Each t As TermInfo In CType(callingObject, PostInfo).PostTags
                        If BlogContext.ParentModule IsNot Nothing Then
                            t.ParentTabID = BlogContext.ParentModule.TabID
                        End If
                        Replacers.Add(New BlogTokenReplace(Me, BlogContext.Post, t))
                    Next
                ElseIf BlogContext.Post IsNot Nothing Then
                    For Each t As TermInfo In BlogContext.Post.PostTags
                        If BlogContext.ParentModule IsNot Nothing Then
                            t.ParentTabID = BlogContext.ParentModule.TabID
                        End If
                        Replacers.Add(New BlogTokenReplace(Me, BlogContext.Post, t))
                    Next
                Else
                    For Each t As TermInfo In TermsController.GetTermsByModule(BlogContext.BlogModuleId, BlogContext.Locale).Where(Function(x) x.VocabularyId = 1).ToList
                        If BlogContext.ParentModule IsNot Nothing Then
                            t.ParentTabID = BlogContext.ParentModule.TabID
                        End If
                        Replacers.Add(New BlogTokenReplace(Me, Nothing, t))
                    Next
                End If
                _usePaging = False

            Case "allkeywords", "alltags"

                For Each t As TermInfo In TermsController.GetTermsByModule(BlogContext.BlogModuleId, BlogContext.Locale).Where(Function(x) x.VocabularyId = 1).ToList
                    If BlogContext.ParentModule IsNot Nothing Then
                        t.ParentTabID = BlogContext.ParentModule.TabID
                    End If
                    Replacers.Add(New BlogTokenReplace(Me, Nothing, t))
                Next
                _usePaging = False

            Case "categories"

                If callingObject IsNot Nothing AndAlso TypeOf callingObject Is PostInfo Then
                    For Each t As TermInfo In CType(callingObject, PostInfo).PostCategories
                        Replacers.Add(New BlogTokenReplace(Me, BlogContext.Post, t))
                    Next
                ElseIf BlogContext.Post IsNot Nothing Then
                    For Each t As TermInfo In BlogContext.Post.PostCategories
                        Replacers.Add(New BlogTokenReplace(Me, BlogContext.Post, t))
                    Next
                ElseIf ViewSettings.Categories = "" Then
                    For Each t As TermInfo In TermsController.GetTermsByModule(BlogContext.BlogModuleId, BlogContext.Locale).Where(Function(x) x.VocabularyId <> 1).ToList
                        Replacers.Add(New BlogTokenReplace(Me, Nothing, t))
                    Next
                Else
                    For Each t As TermInfo In TermsController.GetTermsByModule(BlogContext.BlogModuleId, BlogContext.Locale).Where(Function(x) ViewSettings.CategoryList.Contains(x.VocabularyId)).ToList
                        Replacers.Add(New BlogTokenReplace(Me, Nothing, t))
                    Next
                End If
                _usePaging = False

            Case "allcategories"

                For Each t As TermInfo In TermsController.GetTermsByVocabulary(BlogContext.BlogModuleId, Settings.VocabularyId, BlogContext.Locale).Values
                    If BlogContext.ParentModule IsNot Nothing Then
                        t.ParentTabID = BlogContext.ParentModule.TabID
                    End If
                    Replacers.Add(New BlogTokenReplace(Me, Nothing, t))
                Next
                _usePaging = False

            Case "selectcategories"

                For Each t As TermInfo In TermsController.GetTermsByModule(BlogContext.BlogModuleId, BlogContext.Locale).Where(Function(x) ViewSettings.CategoryList.Contains(x.TermId)).ToList
                    If BlogContext.ParentModule IsNot Nothing Then
                        t.ParentTabID = BlogContext.ParentModule.TabID
                    End If
                    Replacers.Add(New BlogTokenReplace(Me, Nothing, t))
                Next
                _usePaging = False

            Case "comments"

                If callingObject IsNot Nothing AndAlso TypeOf callingObject Is PostInfo Then
                    For Each c As CommentInfo In CommentsController.GetCommentsByContentItem(CType(callingObject, PostInfo).ContentItemId, False, UserId)
                        Replacers.Add(New BlogTokenReplace(Me, BlogContext.Post, c))
                    Next
                    _usePaging = False
                ElseIf BlogContext.Post IsNot Nothing Then
                    For Each c As CommentInfo In CommentsController.GetCommentsByContentItem(BlogContext.Post.ContentItemId, False, UserId)
                        Replacers.Add(New BlogTokenReplace(Me, BlogContext.Post, c))
                    Next
                    _usePaging = False
                Else
                    Parameters.ReadValue("pagesize", _pageSize)
                    If _pageSize < 1 Then _pageSize = 10 ' we will not list "all Posts"
                    For Each c As CommentInfo In CommentsController.GetCommentsByModule(BlogContext.BlogModuleId, UserId, _reqPage - 1, _pageSize, "CREATEDONDATE DESC", _totalRecords).Values
                        Replacers.Add(New BlogTokenReplace(Me, BlogContext.Post, c))
                    Next
                End If

            Case "allcomments"

                Parameters.ReadValue("pagesize", _pageSize)
                Dim loadPosts As Boolean = False
                Parameters.ReadValue("loadposts", loadPosts)
                If _pageSize < 1 Then _pageSize = 10 ' we will not list "all Posts"
                For Each c As CommentInfo In CommentsController.GetCommentsByModule(BlogContext.BlogModuleId, UserId, _reqPage - 1, _pageSize, "CREATEDONDATE DESC", _totalRecords).Values
                    If loadPosts Then
                        Replacers.Add(New BlogTokenReplace(Me, PostsController.GetPost(c.ContentItemId, BlogContext.BlogModuleId, BlogContext.Locale), c))
                    Else
                        Replacers.Add(New BlogTokenReplace(Me, Nothing, c))
                    End If
                Next

            Case "calendar", "blogcalendar"

                For Each bci As BlogCalendarInfo In BlogsController.GetBlogCalendar(BlogContext.BlogModuleId, BlogContext.BlogId, BlogContext.ShowLocale)
                    If BlogContext.ParentModule IsNot Nothing Then
                        bci.ParentTabID = BlogContext.ParentModule.TabID
                    End If
                    Replacers.Add(New BlogTokenReplace(Me, bci))
                Next

            Case "authors", "allauthors"

                Dim blogToShow As Integer = BlogContext.BlogId
                If DataSource.ToLower = "allauthors" Then blogToShow = -1
                Dim sort As String = ""
                Parameters.ReadValue("sort", sort)
                Select Case sort.ToLower
                    Case "username"
                        For Each u As PostAuthor In PostsController.GetAuthors(BlogContext.BlogModuleId, blogToShow).OrderBy(Function(t) t.Username)
                            Replacers.Add(New BlogTokenReplace(Me, New LazyLoadingUser(u)))
                        Next
                    Case "email"
                        For Each u As PostAuthor In PostsController.GetAuthors(BlogContext.BlogModuleId, blogToShow).OrderBy(Function(t) t.Email)
                            Replacers.Add(New BlogTokenReplace(Me, New LazyLoadingUser(u)))
                        Next
                    Case "firstname"
                        For Each u As PostAuthor In PostsController.GetAuthors(BlogContext.BlogModuleId, blogToShow).OrderBy(Function(t) t.FirstName)
                            Replacers.Add(New BlogTokenReplace(Me, New LazyLoadingUser(u)))
                        Next
                    Case "displayname"
                        For Each u As PostAuthor In PostsController.GetAuthors(BlogContext.BlogModuleId, blogToShow).OrderBy(Function(t) t.DisplayName)
                            Replacers.Add(New BlogTokenReplace(Me, New LazyLoadingUser(u)))
                        Next
                    Case Else ' last name
                        For Each u As PostAuthor In PostsController.GetAuthors(BlogContext.BlogModuleId, blogToShow)
                            If BlogContext.ParentModule IsNot Nothing Then
                                u.ParentTabID = BlogContext.ParentModule.TabID
                            End If
                            Replacers.Add(New BlogTokenReplace(Me, New LazyLoadingUser(u)))
                        Next
                End Select

        End Select

    End Sub
#End Region

#Region " Post List Stuff "
    Private Property PostList As IEnumerable(Of PostInfo) = Nothing
    Private Sub EnsurePostList(pageSize As Integer)

        If PostList Is Nothing Then
            If pageSize < 1 Then pageSize = 10 ' we will not list "all Posts"
            Dim publishValue As Integer = 1
            If Not String.IsNullOrEmpty(BlogContext.SearchString) Then
                If BlogContext.SearchUnpublished Then publishValue = -1
                If String.IsNullOrEmpty(BlogContext.Categories) Then
                    If BlogContext.Term Is Nothing Then
                        PostList = PostsController.SearchPosts(Settings.ModuleId, BlogContext.BlogId, BlogContext.Locale, BlogContext.SearchString, BlogContext.SearchTitle, BlogContext.SearchContents, publishValue, BlogContext.ShowLocale, BlogContext.EndDate, -1, _reqPage - 1, pageSize, "PUBLISHEDONDATE DESC", _totalRecords, UserId, BlogContext.Security.UserIsAdmin).Values
                    Else
                        PostList = PostsController.SearchPostsByTerm(Settings.ModuleId, BlogContext.BlogId, BlogContext.Locale, BlogContext.TermId, BlogContext.SearchString, BlogContext.SearchTitle, BlogContext.SearchContents, publishValue, BlogContext.ShowLocale, BlogContext.EndDate, -1, _reqPage - 1, pageSize, "PUBLISHEDONDATE DESC", _totalRecords, UserId, BlogContext.Security.UserIsAdmin).Values
                    End If
                Else
                    PostList = PostsController.SearchPostsByCategory(Settings.ModuleId, BlogContext.BlogId, BlogContext.Locale, BlogContext.Categories, BlogContext.SearchString, BlogContext.SearchTitle, BlogContext.SearchContents, publishValue, BlogContext.ShowLocale, BlogContext.EndDate, -1, _reqPage - 1, pageSize, "PUBLISHEDONDATE DESC", _totalRecords, UserId, BlogContext.Security.UserIsAdmin).Values
                End If
            ElseIf String.IsNullOrEmpty(BlogContext.Categories) Then
                publishValue = -1
                If ViewSettings.HideUnpublishedBlogsViewMode AndAlso PortalSettings.UserMode = PortalSettings.Mode.View Then publishValue = 1
                If ViewSettings.HideUnpublishedBlogsEditMode AndAlso PortalSettings.UserMode = PortalSettings.Mode.Edit Then publishValue = 1
                If BlogContext.Term Is Nothing Then
                    PostList = PostsController.GetPosts(Settings.ModuleId, BlogContext.BlogId, BlogContext.Locale, publishValue, BlogContext.ShowLocale, BlogContext.EndDate, BlogContext.AuthorId, False, _reqPage - 1, pageSize, "PUBLISHEDONDATE DESC", _totalRecords, UserId, BlogContext.Security.UserIsAdmin).Values
                Else
                    PostList = PostsController.GetPostsByTerm(Settings.ModuleId, BlogContext.BlogId, BlogContext.Locale, BlogContext.TermId, publishValue, BlogContext.ShowLocale, BlogContext.EndDate, BlogContext.AuthorId, _reqPage - 1, pageSize, "PUBLISHEDONDATE DESC", _totalRecords, UserId, BlogContext.Security.UserIsAdmin).Values
                End If
            Else
                publishValue = -1
                If ViewSettings.HideUnpublishedBlogsViewMode AndAlso PortalSettings.UserMode = PortalSettings.Mode.View Then publishValue = 1
                If ViewSettings.HideUnpublishedBlogsEditMode AndAlso PortalSettings.UserMode = PortalSettings.Mode.Edit Then publishValue = 1
                PostList = PostsController.GetPostsByCategory(Settings.ModuleId, BlogContext.BlogId, BlogContext.Locale, BlogContext.Categories, publishValue, BlogContext.ShowLocale, BlogContext.EndDate, BlogContext.AuthorId, _reqPage - 1, pageSize, "PUBLISHEDONDATE DESC", _totalRecords, UserId, BlogContext.Security.UserIsAdmin).Values
            End If
            _usePaging = True
        End If
    End Sub
#End Region

#Region " Overrides "
    Public Overrides Sub DataBind()

        'Dim tmgr As New TemplateManager(PortalSettings, ViewSettings.Template)
        'With vtContents
        '    .TemplatePath = tmgr.TemplatePath
        '    .TemplateRelPath = tmgr.TemplateRelPath
        '    .TemplateMapPath = tmgr.TemplateMapPath
        '    .DefaultReplacer = New BlogTokenReplace(Me)
        'End With
        'vtContents.DataBind()


        '   ctlManagement.Visible = CBool(ViewSettings.BlogModuleId = -1) OrElse ViewSettings.ShowManagementPanel

        'btnAddBlog.Visible = True



        'If PortalSettings.UserMode = PortalSettings.Mode.View AndAlso ViewSettings.ShowManagementPanelViewMode = False Then
        '    '   ctlManagement.Visible = False
        '    btnAddBlog.Visible = False
        'End If

    End Sub
#End Region

#Region " IActionable "


    Public ReadOnly Property ModuleActions As Actions.ModuleActionCollection Implements IActionable.ModuleActions
        Get
            Dim MyActions As New Actions.ModuleActionCollection
            'If IsEditable Or BlogContext.Security.IsBlogger Then
            '    MyActions.Add(GetNextActionID, Localization.GetString(ModuleActionType.EditContent, LocalResourceFile), ModuleActionType.EditContent, "", "", EditUrl("Manage"), False, DotNetNuke.Security.SecurityAccessLevel.View, True, False)
            'End If
            'If IsEditable Then
            '    MyActions.Add(GetNextActionID, LocalizeString("TemplateSettings"), ModuleActionType.EditContent, "", "", EditUrl("TemplateSettings"), False, DotNetNuke.Security.SecurityAccessLevel.Edit, True, False)
            'End If
            If IsEditable Then
                MyActions.Add(GetNextActionID, "Import Blog", ModuleActionType.EditContent, "", "", EditUrl("BlogImport"), False, DotNetNuke.Security.SecurityAccessLevel.Edit, True, False)
            End If
            Return MyActions
        End Get
    End Property


    ''' <summary>
    ''' Save attachement from Jquery jason
    ''' </summary>
    ''' <param name="iCommentID"></param>
    Private Sub SaveAttachment(ByVal iCommentID As Integer)
        If TextBoxAttachmentData.Text = "" Then
            Return
        End If
        Dim js As JavaScriptSerializer = New JavaScriptSerializer()
        Dim AttachmentDataDataList As List(Of AttachmentData) = js.Deserialize(Of List(Of AttachmentData))(TextBoxAttachmentData.Text)
        For Each item As AttachmentData In AttachmentDataDataList
            Dim oAttachInfo As Blog_Attachments = New Blog_Attachments()
            With oAttachInfo

                .AttachId = -1
                .CommentID = iCommentID
                .ContentType = item.contenttype
                .DateAdded = System.DateTime.Now
                .FileName = item.filename
                .FileSize = If(String.IsNullOrEmpty(item.size) = True, 0, Convert.ToInt32(item.size))
                .FileUrl = item.attachmentURL
                .UserID = UserId
                .ContentItemID = GetQueryStringValue(Util.PostKey, -1)

            End With
            Dim DBController As New DAL2.BlogContent.BlogContentController()
            DBController.AddAttachement(oAttachInfo)
        Next
    End Sub





#End Region

End Class
