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

Imports DotNetNuke.Entities.Portals
Imports DotNetNuke.Services.Localization.Localization
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Common.Globals
Imports System.Linq
Imports DotNetNuke.Modules.Blog.Entities.Blogs
Imports DotNetNuke.Modules.Blog.Entities.Posts
Imports DotNetNuke.Services.Localization
Imports Telerik.Web.UI
Imports DotNetNuke.Modules.Blog.Common.Globals
Imports DotNetNuke.Modules.Blog.DAL2.BlogContent

Imports DotNetNuke.Modules.Blog.Integration
Imports DotNetNuke.Modules.Blog.Entities.Terms

Imports System.Web
Imports DotNetNuke.Modules.Blog.DAL2.BlogContent.BlogContentController

Public Class PostEdit
    Inherits BlogModuleBase

#Region " Public Properties "


    'Public ReadOnly Property GetFileStack As String
    '    Get
    '        Return Configuration.WebConfigurationManager.AppSettings("FileStackApiKey")
    '    End Get

    'End Property

    Public ReadOnly Property FilePath As String
        Get
            Return PortalSettings.HomeDirectory & ModuleConfiguration.DesktopModule.FriendlyName & "/"
        End Get
    End Property


#End Region

#Region " Private Members "

    Private m_oTags As ArrayList
    Private m_BlogPostInfo As DAL2.BlogContent.BlogPostInfo
#End Region

#Region " Event Handlers "
    Protected Overloads Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init

        Try

            ctlTags.ModuleConfiguration = ModuleConfiguration

            'ctlCategories.ModuleConfiguration = ModuleConfiguration
            'ctlCategories.VocabularyId = Settings.VocabularyId

            'If BlogContext.Blog Is Nothing Then
            '    Dim blogList As IEnumerable(Of BlogInfo) = Nothing
            '    blogList = BlogsController.GetBlogsByModule(Settings.ModuleId, UserId, BlogContext.Locale).Values.Where(Function(b)
            '                                                                                                                Return b.OwnerUserId = UserId
            '                                                                                                            End Function)
            '    If blogList.Count > 0 Then
            '        Response.Redirect(EditUrl("Blog", blogList(0).BlogID.ToString, "PostEdit"), False)
            '    Else
            '        blogList = BlogsController.GetBlogsByModule(Settings.ModuleId, UserId, BlogContext.Locale).Values.Where(Function(b)
            '                                                                                                                    Return (b.CanAdd And BlogContext.Security.CanAddPost) Or (b.CanEdit And BlogContext.Security.CanEditPost And BlogContext.ContentItemId > -1)
            '                                                                                                                End Function)
            '        If blogList.Count > 0 Then
            '            Response.Redirect(EditUrl("Blog", blogList(0).BlogID.ToString, "PostEdit"), False)
            '        Else
            '            Throw New Exception("Could not find a blog for you to post to")
            '        End If
            '    End If
            'End If

            txtTitle.DefaultLanguage = BlogContext.Blog.Locale
            txtDescription.DefaultLanguage = BlogContext.Blog.Locale
            ' txtShortDescription.DefaultLanguage = BlogContext.Blog.Locale
            txtTitle.ShowTranslations = BlogContext.Blog.FullLocalization
            txtDescription.ShowTranslations = BlogContext.Blog.FullLocalization
            '    txtShortDescription.ShowTranslations = BlogContext.Blog.FullLocalization

            ' Summary
            Select Case Settings.SummaryModel
                Case SummaryType.HtmlIndependent
                    txtDescription.ShowRichTextBox = True
                Case SummaryType.HtmlPrecedesPost
                    txtDescription.ShowRichTextBox = True
                    'lblSummaryPrecedingWarning.Visible = True
                Case Else ' plain text
                    txtDescription.ShowRichTextBox = False
            End Select

        Catch
        End Try

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            If Not IsUserAdmin Then
                Response.Redirect(NavigateURL(Me.PortalId), True)

            End If

            hdnContentItemID.Value = GetQueryStringValue("ContentItemID", "-1")
            ' Categories
            If Not IsPostBack Then
                BindDropDown()
                BindTags()
                CustomValidator1.ErrorMessage = String.Format("<div class=""dnnFormMessage dnnFormError"">{0}</div>", "Category is required field")
                CustomValidator1.ValidationGroup = "valBlogPostEdit"
            End If
            If Not Page.IsPostBack Then

                DotNetNuke.UI.Utilities.ClientAPI.AddButtonConfirm(cmdDelete, "Are you sure want to delete Blog? ")


                'If Settings.VocabularyId < 1 Then
                '    pnlCategories.Visible = False
                'End If
                ' Clear the tags and categories cache
                Entities.Terms.TermsController.GetTermsByVocabulary(ModuleId, Settings.VocabularyId, Threading.Thread.CurrentThread.CurrentCulture.Name, True)
                Entities.Terms.TermsController.GetTermsByVocabulary(ModuleId, 1, Threading.Thread.CurrentThread.CurrentCulture.Name, True)

                If BlogContext.IsMultiLingualSite And Not BlogContext.Blog.FullLocalization Then
                    ddLocale.DataSource = DotNetNuke.Services.Localization.LocaleController.Instance.GetLocales(PortalId).Values.OrderBy(Function(t) t.NativeName)
                    ddLocale.DataValueField = "Code"
                    ddLocale.DataBind()
                    ddLocale.Items.Insert(0, New ListItem(LocalizeString("DefaultLocale"), "En-US"))
                    rowLocale.Visible = True
                    If BlogContext.Locale <> BlogContext.Blog.Locale Then
                        Try
                            ddLocale.Items.FindByValue(BlogContext.Locale).Selected = True
                        Catch ex As Exception
                        End Try
                    End If
                Else
                    rowLocale.Visible = False
                End If

                ' Buttons
                If BlogContext.BlogId > -1 Then
                    hlCancel.NavigateUrl = NavigateURL(TabId, "", "Blog=" & BlogContext.BlogId.ToString)
                ElseIf BlogContext.ContentItemId > -1 Then
                    hlCancel.NavigateUrl = NavigateURL(TabId, "", "Post=" & BlogContext.ContentItemId.ToString)
                Else
                    hlCancel.NavigateUrl = ModuleContext.NavigateUrl(ModuleContext.TabId, "", False, "")
                End If
                cmdDelete.Visible = CBool(BlogContext.Post IsNot Nothing)

                If Not BlogContext.Post Is Nothing Then

                    Dim PostBody As New PostBodyAndSummary(BlogContext.Post, Settings.SummaryModel, True, Settings.AutoGenerateMissingSummary, Settings.AutoGeneratedSummaryLength)

                    Try

                        'If oBlogPost.CategoryID > 0 Then
                        '    ddGlobalCategory.Items.FindByValue(oBlogPost.CategoryID.ToString()).Selected = True
                        'End If
                        If Not BlogContext.Post.PostTags Is Nothing Then
                            'BlogContext.Post.PostTags =
                            ctlTags.Terms = BlogContext.Post.PostTags
                        End If
                    Catch ex As Exception
                    End Try
                    ' Content

                    Dim oBlogPost As DAL2.BlogContent.BlogPostInfo = New DAL2.BlogContent.BlogContentController().GetBlogContentById(True, GetQueryStringValue("ContentItemId", -1))
                    If Not oBlogPost Is Nothing Then
                        txtTitle.DefaultText = HttpUtility.HtmlDecode(oBlogPost.Title)
                        chkIsClassicArticle.Checked = oBlogPost.IsClassicArticle

                        txtTitle.InitialBind()
                        txtDescription.DefaultText = oBlogPost.Summary
                        ' chkPublished.Checked = oBlogPost.Published
                        txtDescription.InitialBind()

                        ' Publishing
                        chkPublished.Checked = oBlogPost.Published
                        chkAllowComments.Checked = oBlogPost.AllowComments
                        chkDisplayCopyright.Checked = oBlogPost.DisplayCopyright
                        If chkDisplayCopyright.Checked Then
                            pnlCopyright.Visible = True
                            txtCopyright.Text = oBlogPost.Copyright
                        End If
                        ' Date
                        litTimezone.Text = BlogContext.UiTimeZone.DisplayName
                        Dim publishDate As DateTime = UtcToLocalTime(oBlogPost.PublishedOnDate, BlogContext.UiTimeZone)
                        dpPostDate.Culture = Threading.Thread.CurrentThread.CurrentUICulture
                        dpPostDate.SelectedDate = publishDate
                        tpPostTime.Culture = Threading.Thread.CurrentThread.CurrentUICulture
                        tpPostTime.SelectedDate = publishDate
                        txtShortDescription.Text = oBlogPost.ShortDescription

                        If Not oBlogPost.GetBlogGlobalCategory Is Nothing Then
                            For Each oCategory As PostCategoryRelationInfo In oBlogPost.GetBlogGlobalCategory
                                For Each eitem As ListItem In rdbCategory.Items
                                    If oCategory.IsActive Then
                                        If eitem.Value = oCategory.CategoryId.ToString() Then
                                            eitem.Selected = True
                                        End If
                                    End If
                                Next
                            Next
                        End If

                        If Not String.IsNullOrEmpty(oBlogPost.Image) Then
                            imgPostImage.ImageUrl = oBlogPost.filestackurl
                            hdnImageUrl.Value = oBlogPost.filestackurl
                            imgPostImage.Visible = True
                            hdnImage.Value = oBlogPost.Image
                        Else
                            imgPostImage.Visible = False
                            '  cmdImageRemove.Visible = False
                        End If
                    Else
                        If GetQueryStringValue("ContentItemId", -1) <> -1 Then
                            Response.Redirect(NavigateURL(Me.TabId))

                        End If
                    End If
                End If

                ' Security
                If BlogContext.Blog.MustApproveGhostPosts AndAlso Not BlogContext.Security.CanApprovePost Then
                    chkPublished.Checked = False
                    chkPublished.Enabled = False
                End If
            End If
        Catch exc As Exception
            ProcessModuleLoadException(Me, exc)
        End Try

    End Sub

    Private Sub BindTags()
        Dim controller As New BlogContentController()
        Dim lstTags As IQueryable(Of CA_TaxonomyTerms) = controller.GetTagList(-1)
        dlTags.DataSource = lstTags.ToList()
        dlTags.DataBind()
    End Sub

    Private Sub BindDropDown()
        Dim controller As New DAL2.BlogContent.BlogContentController()

        Dim oGlobalCategory As List(Of BlogGlobalCategory) = controller.GetGlobalCategory(DAL2.BlogContent.Util.ComponentID).ToList()

        rdbCategory.DataSource = oGlobalCategory
        rdbCategory.DataTextField = "CategoryName"
        rdbCategory.DataValueField = "CategoryId"
        rdbCategory.DataBind()
    End Sub

    Protected Sub dlTags_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs)
        If e.Item.ItemType = System.Web.UI.WebControls.ListItemType.Item OrElse e.Item.ItemType = System.Web.UI.WebControls.ListItemType.AlternatingItem Then
            Dim TagID As Integer = (CType(e.Item.DataItem, CA_TaxonomyTerms)).TermID
            Dim lblName As Label = CType(e.Item.FindControl("lblName"), Label)
            lblName.Text = (CType(e.Item.DataItem, CA_TaxonomyTerms)).Name
            Dim dlChild As DataList = CType(e.Item.FindControl("dlTagChildren"), DataList)
            Dim controller As New BlogContentController()
            dlChild.DataSource = controller.GetTagList(TagID)
            dlChild.DataBind()
        End If
    End Sub

    Protected Sub dlTagChildren_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim TagID As Integer = (CType(e.Item.DataItem, CA_TaxonomyTerms)).TermID
            Dim lblName As CheckBox = CType(e.Item.FindControl("lblChildName"), CheckBox)
            If (CType(e.Item.DataItem, CA_TaxonomyTerms)).VocabularyName.Trim() = "Single" Then
                lblName.InputAttributes.Add("class", "CA_Single")
            End If
            Dim strItemName As String = (CType(e.Item.DataItem, CA_TaxonomyTerms)).Name
            Dim ContenItemId As Int32 = GetQueryStringValue("ContentItemId", -1)
            If (ContenItemId > 0) Then
                Dim oCtrl As New BlogContentController
                Dim lstTags As IQueryable(Of CA_TaxonomyTerms) = oCtrl.GetTagsByContentItemID(ContenItemId)
                For Each oTerm As CA_TaxonomyTerms In lstTags
                    If (oTerm.Name = strItemName) Then
                        lblName.Checked = True
                    End If
                Next
            End If

            lblName.Text = strItemName
        End If
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Try
            If Page.IsValid = True Then
                If Not IsUserAdmin Then
                    Response.Redirect(NavigateURL(Me.TabId))
                End If

                If txtDescription.DefaultText = "" Then
                    DotNetNuke.UI.Skins.Skin.AddModuleMessage(Me, "Blog Body can't be left blank.", UI.Skins.Controls.ModuleMessage.ModuleMessageType.BlueInfo)
                    Return
                End If
                If GetQueryStringValue("ContentItemId", -1) > -1 Then
                    If BlogContext.Post Is Nothing Then
                        BlogContext.Post = Entities.Posts.PostsController.GetPost(GetQueryStringValue("ContentItemId", -1), Me.ModuleId, "en-Us")
                    End If
                End If

                If BlogContext.Post Is Nothing Then
                    BlogContext.Post = New PostInfo
                    BlogContext.Post.Blog = BlogContext.Blog
                    BlogContext.Post.UpdatedByUserID = UserInfo.UserID
                    BlogContext.Post.UpdatedOnDate = System.DateTime.Now
                    BlogContext.Post.CreatedByUserID = UserInfo.UserID
                    BlogContext.Post.CreatedOnDate = System.DateTime.Now
                    BlogContext.Post.PublishedOnDate = System.DateTime.Now

                Else
                    BlogContext.Post.UpdatedByUserID = UserInfo.UserID
                    BlogContext.Post.UpdatedOnDate = System.DateTime.Now
                End If

                Dim firstPublish As Boolean = CBool((Not BlogContext.Post.Published) And chkPublished.Checked)

                ' Contents and summary
                BlogContext.Post.Summary = txtDescription.DefaultText
                BlogContext.Post.Locale = BlogContext.Blog.Locale
                BlogContext.Post.BlogID = BlogContext.BlogId
                BlogContext.Post.Title = txtTitle.DefaultText
                BlogContext.Post.ShortDescription = txtShortDescription.Text
                BlogContext.Post.IsClassicArticle = chkIsClassicArticle.Checked
                BlogContext.Post.CategoryID = -1 'Convert.ToInt32(ddGlobalCategory.SelectedValue)

                BlogContext.Post.TitleLocalizations = txtTitle.GetLocalizedTexts

                ' Publishing
                BlogContext.Post.Published = chkPublished.Checked

                BlogContext.Post.AllowComments = chkAllowComments.Checked
                BlogContext.Post.DisplayCopyright = chkDisplayCopyright.Checked
                BlogContext.Post.Copyright = txtCopyright.Text

                ' Categories, Tags
                If BlogContext.IsMultiLingualSite And Not BlogContext.Blog.FullLocalization Then
                    BlogContext.Post.Locale = ddLocale.SelectedValue
                Else
                    BlogContext.Post.Locale = ""
                End If

                ' Image
                Dim saveDir As String = GetPostDirectoryMapPath(BlogContext.Post)
                Dim savedFile As String = ""

                Dim terms As New List(Of TermInfo)
                ctlTags.CreateMissingTerms()
                terms.AddRange(ctlTags.Terms)
                '        terms.AddRange(ctlCategories.SelectedCategories)
                BlogContext.Post.Terms.Clear()
                BlogContext.Post.Terms.AddRange(ctlTags.Terms)

                'Start: Added by Kusum 21-Dec-17 to insert the related tags
                Dim relatedTags As New List(Of TermInfo)
                If Request.Form("rtagIDs") <> Nothing Then

                    Dim rTagsIDs As String
                    rTagsIDs = Request.Form("rtagIDs")

                    Dim count As Integer
                    Dim strArr() As String
                    strArr = rTagsIDs.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    For count = 0 To strArr.Length - 1
                        Dim strData() As String
                        strData = strArr(count).Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

                        Dim relateTag As TermInfo = New TermInfo()
                        relateTag.Name = strData(0)
                        relateTag.TermId = Integer.Parse(strData(1))
                        relatedTags.Add(relateTag)
                    Next

                End If

                If relatedTags.Count > 0 Then
                    BlogContext.Post.Terms.AddRange(relatedTags)
                End If

                BlogContext.Post.filestackurl = hdnImageUrl.Value

                If hdnImage.Value = "" Then

                    BlogContext.Post.Image = hdnImageEdit.Value
                Else
                    BlogContext.Post.Image = hdnImage.Value

                End If

                ' Add if new, otherwise update
                Dim publishingUserId As Integer = UserId
                If BlogContext.Blog.PublishAsOwner Then publishingUserId = BlogContext.Blog.OwnerUserId
                If BlogContext.ContentItemId = -1 Then

                    'BlogContext.Post.CreatedByUserID = UserId
                    'BlogContext.Post.CreatedOnDate = System.DateTime.Now
                    'BlogContext.Post.UpdatedByUserID = UserId
                    'BlogContext.Post.UpdatedOnDate = System.DateTime.Now

                    BlogContext.Post.ContentItemId = PostsController.AddPost(BlogContext.Post, publishingUserId)
                    BlogContext.ContentItemId = BlogContext.Post.ContentItemId
                    'If savedFile <> "" Then ' move file if it was saved
                    '    saveDir = GetPostDirectoryMapPath(BlogContext.Post)
                    '    IO.Directory.CreateDirectory(saveDir)
                    '    Dim dest As String = saveDir & BlogContext.Post.Image & IO.Path.GetExtension(fileImage.FileName).ToLower
                    '    IO.File.Move(savedFile, dest)
                    'End If
                Else
                    'BlogContext.Post.CreatedByUserID = UserId
                    'BlogContext.Post.CreatedOnDate = System.DateTime.Now

                    PostsController.UpdatePost(BlogContext.Post, publishingUserId)
                End If

                If firstPublish Then
                    PostsController.PublishPost(BlogContext.Post, UserInfo.UserID)
                    'ElseIf BlogContext.Blog.MustApproveGhostPosts And UserId <> BlogContext.Blog.OwnerUserId Then

                    '    Dim title As String = Localization.GetString("ApprovePostNotifyBody", SharedResourceFileName)
                    '    Dim summary As String = "<a target='_blank' href='" + BlogContext.Post.PermaLink(PortalSettings) + "'>" + BlogContext.Post.Title + "</a>"
                    '    NotificationController.PostPendingApproval(BlogContext.Blog, BlogContext.Post, ModuleContext.PortalId, summary, title)
                End If
                Dim ctrlDal2 As New BlogContentController()
                Dim lstPostCategory As New List(Of PostCategoryRelationInfo)

                For Each IRadioItem As ListItem In rdbCategory.Items
                    Dim oCategoryItem As New PostCategoryRelationInfo
                    With oCategoryItem
                        .CategoryId = Convert.ToInt32(IRadioItem.Value)
                        .ContentItemId = BlogContext.Post.ContentItemId
                        .IsActive = IRadioItem.Selected
                    End With
                    ctrlDal2.PostCategoryRelation(oCategoryItem)
                Next
                ' Save Attachment(s)
                Dim aryStack As String()
                aryStack = hdAttachments.Value.Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries)
                For Each strStack As String In aryStack
                    Dim oFileStack As New FileStackRefernceInfo
                    Dim objInfo As String() = strStack.Split(New String() {"~"}, StringSplitOptions.RemoveEmptyEntries)
                    oFileStack.ContentItemID = Convert.ToInt32(BlogContext.ContentItemId)
                    oFileStack.FileId = -1
                    oFileStack.FileName = objInfo(0)
                    oFileStack.filestackurl = objInfo(1)
                    Dim dalAttach As New DAL2.BlogContent.BlogContentController
                    dalAttach.AddToFilestack(oFileStack)
                Next

#Region "Updating Tags information to the Insights"
                ' Sachin 19-Feb-2018 >> Update Tags information 
                Dim tagForm As String = ""
                If (hdTagsSelected.Value <> String.Empty) Then
                    tagForm = hdTagsSelected.Value.Trim()
                    Dim controller As New BlogContentController()
                    ' clear the previous tags entry
                    controller.ClearTagsByContentItemID(BlogContext.Post.ContentItemId)
                    ' get the array of assigned new tags
                    Dim Tags() As String = tagForm.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                    ' iterate through the newly assigned tag list
                    For Each strTag As String In Tags
                        ' look for the tag info object
                        Dim oTag As IQueryable(Of CA_TaxonomyTerms) = controller.SearchForTags(strTag)
                        ' if its a valid Tag
                        If Not oTag Is Nothing Then
                            ' Dim lstTags As IQueryable(Of CA_TaxonomyTerms) = controller.GetTagsByContentItemID(BlogContext.Post.ContentItemId)
                            ' add or update the tag attached with the current content item id
                            Try
                                controller.UpdateTagByContentItemID(BlogContext.Post.ContentItemId, oTag.First().TermID)
                            Catch ex As Exception

                            End Try

                        End If
                    Next
                End If
#End Region

                ' ctrlDal2.PostCategoryRelation(lstPostCategory)
                Response.Redirect(NavigateURL(TabId, "", DAL2.BlogContent.Util.PostKey & "=" & BlogContext.ContentItemId.ToString), False)

            End If
        Catch exc As Exception
            ProcessModuleLoadException(Me, exc)
        End Try
        BindDropDown()
    End Sub

    Protected Sub cmdDelete_Click(sender As Object, e As EventArgs) Handles cmdDelete.Click
        Try
            DeleteAllFiles()
            PostsController.DeletePost(GetQueryStringValue("ContentItemId", -1), 1, ModuleContext.PortalId, Settings.VocabularyId)
            Response.Redirect(NavigateURL(TabId), False)
        Catch exc As Exception    'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Protected Sub valPost_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles valPost.ServerValidate
        ' args.IsValid =tre teBlogPost.DefaultText.Length > 0
    End Sub

    Protected Sub chkDisplayCopyright_CheckedChanged(sender As Object, e As EventArgs) Handles chkDisplayCopyright.CheckedChanged
        pnlCopyright.Visible = chkDisplayCopyright.Checked
        If pnlCopyright.Visible Then
            txtCopyright.Text = CreateCopyRight()
        End If
    End Sub

    'Private Sub cmdImageRemove_Click(sender As Object, e As EventArgs) Handles cmdImageRemove.Click

    '    If BlogContext.Post IsNot Nothing Then

    '        If hdnImage.Value <> "" Then
    '            ' remove old images
    '            Dim saveDir As String = PortalSettings.HomeDirectoryMapPath & String.Format("\Blog\Files\{0}\{1}\", 1, GetQueryStringValue("ContentItemId", -1))
    '            Dim imagesToDelete As New List(Of String)
    '            Dim d As New IO.DirectoryInfo(saveDir)
    '            For Each f As IO.FileInfo In d.GetFiles
    '                If f.Name.StartsWith(hdnImage.Value) Then
    '                    imagesToDelete.Add(f.FullName)
    '                End If
    '            Next
    '            For Each f As String In imagesToDelete
    '                Try
    '                    IO.File.Delete(f)
    '                Catch ex As Exception
    '                End Try
    '            Next
    '        End If
    '        BlogContext.Post.Image = ""
    '        Dim oController As New DAL2.BlogContent.BlogContentController
    '        Dim oBlogPost As DAL2.BlogContent.BlogPostInfo = oController.GetBlogContentById(True, GetQueryStringValue("ContentItemId", -1))
    '        oBlogPost.Image = ""
    '        hdnImage.Value = ""
    '        oController.UpdateBlogContent(oBlogPost)
    '    End If
    '    imgPostImage.Visible = False
    '    cmdImageRemove.Visible = False

    'End Sub
#End Region

#Region " Private Methods "
    Private Function CreateCopyRight() As String
        Return GetString("msgCopyright", LocalResourceFile) & Date.UtcNow.Year & " " & BlogContext.Blog.DisplayName
    End Function
#End Region

#Region " Upload Feature Methods "
    Private Sub DeleteAllFiles()
        Try
            System.IO.Directory.Delete(FileController.getPostDir(FilePath, BlogContext.Post), True)
        Catch

        End Try
    End Sub



#End Region

End Class