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
Imports System
Imports DotNetNuke.Web.Client.ClientResourceManagement
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Framework
Imports DotNetNuke.Modules.Blog.Common.Globals
Imports DotNetNuke.Modules.Blog.Entities.Terms
Imports DotNetNuke.Modules.Blog.Templating
Imports DotNetNuke.Services.Localization
Imports DotNetNuke.UI.Utilities
Imports DotNetNuke.Web.Client
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Framework.JavaScriptLibraries
Imports System.Linq
Imports DotNetNuke.Entities.Portals
Imports System.Web
Namespace Common


    Public Class BlogModuleBase
        Inherits PortalModuleBase



        Public Shared iIsMyValut As Int32 = 0
        Public Shared iIsAll As Int32 = 1

#Region " Private Members "
#End Region
        Public ReadOnly Property GetFileStack As String
            Get
                Return Configuration.WebConfigurationManager.AppSettings("FileStackApiKey")
            End Get

        End Property
#Region "Query String Related Methods"
        Public Function GetQueryStringValue(Key As String, iDefault As Integer) As Integer
            Try
                If Request.QueryString(Key) IsNot Nothing Then
                    Return Convert.ToInt32(Request.QueryString(Key))
                End If


            Catch exc As Exception
            End Try
            Return iDefault
        End Function
        Public Function GetQueryStringValue(Key As String, iDefault As String) As String
            Try
                If Request.QueryString(Key) IsNot Nothing Then
                    Return Convert.ToString(Request.QueryString(Key))
                End If


            Catch exc As Exception
            End Try
            Return iDefault
        End Function
        Public Function BuildQuery(iUserId As Integer, iCategoryId As Integer, sSortyBy As Integer, iContentItemId As Integer) As String

            Dim additionalParameters As New List(Of String)()
            '   additionalParameters.Add("UserId=" & iUserId)
            additionalParameters.Add("CategoryId=" & iCategoryId)
            additionalParameters.Add("SortBy=" & sSortyBy)
            additionalParameters.Add("ContentItemId=" & iContentItemId)
            '    additionalParameters.Add("HasSeen=" & HasSeen)


            Dim parameters As String() = New String(additionalParameters.Count - 1) {}
            For i As Integer = 0 To additionalParameters.Count - 1
                parameters(i) = additionalParameters(i)
            Next

            Return DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", parameters)
        End Function

        Public Function BuildQuery(iUserId As Integer, iCategoryId As Integer, sSortyBy As Integer, iContentItemId As Integer, ByVal strQuery As String) As String

            Dim additionalParameters As New List(Of String)()
            '   additionalParameters.Add("UserId=" & iUserId)
            additionalParameters.Add("CategoryId=" & iCategoryId)
            additionalParameters.Add("SortBy=" & sSortyBy)
            additionalParameters.Add("ContentItemId=" & iContentItemId)
            additionalParameters.Add("q=" & strQuery)
            '    additionalParameters.Add("HasSeen=" & HasSeen)


            'Dim parameters As String() = New String(additionalParameters.Count - 1) {}
            'For i As Integer = 0 To additionalParameters.Count - 1
            '    parameters(i) = additionalParameters(i)
            'Next

            Return DAL2.BlogContent.Util.GetFormattedUrl(Me.TabId, additionalParameters)
        End Function

#End Region
#Region "Tokens"
        Public Function ReplaceCategoryToken(ByVal oCat As List(Of DAL2.BlogContent.PostCategoryRelationInfo), ByVal strTemplate As String) As String
            If oCat Is Nothing Then
                Return strTemplate
            End If
            Dim tmpData As String = strTemplate
            Dim tmpfinal As New System.Text.StringBuilder
            If oCat.Count > 0 Then
                For Each oPostCategory As DAL2.BlogContent.PostCategoryRelationInfo In oCat

                    Dim oCategory As DAL2.BlogContent.BlogGlobalCategory = New DAL2.BlogContent.BlogContentController().GetGlobalCategoryByCategoryID(oPostCategory.CategoryId)

                    If Not oCategory Is Nothing Then
                        strTemplate = tmpData
                        strTemplate = strTemplate.Replace("{CATEGORYNAME}", oCategory.CategoryName)
                        strTemplate = strTemplate.Replace("{CATEGORYURL}", DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", "CategoryID=" & oCategory.CategoryId.ToString()))
                        strTemplate = strTemplate.Replace("{CATEGORYID}", oCategory.CategoryId.ToString())
                        tmpfinal.Append(strTemplate)
                    End If

                Next
            End If


            Return tmpfinal.ToString()
        End Function

        Public Function ReplaceTagsToken(ByVal oBlogPost As DAL2.BlogContent.BlogPostInfo, ByVal strTemplate As String) As String
            If oBlogPost Is Nothing Then
                Return strTemplate
            End If
            Dim oCat As List(Of TermInfo) = TermsController.GetTermsByPost(oBlogPost.ContentItemId, Me.ModuleId, "en-Us")


            Dim tmpData As String = strTemplate
            Dim tmpfinal As String = String.Empty
            If oCat.Count > 0 Then
                For Each oTerms As TermInfo In oCat



                    If Not oTerms Is Nothing Then
                        strTemplate = tmpData
                        strTemplate = strTemplate.Replace("{TAG}", oTerms.Name)
                        strTemplate = strTemplate.Replace("{TAGURL}", DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "bSearch", "mid=" & ModuleId, "q=" & oTerms.Name))
                        '   strTemplate = strTemplate.Replace("{CATEGORYID}", oCategory.CategoryId.ToString())

                        tmpfinal = strTemplate & tmpfinal
                    End If

                Next
            End If


            Return tmpfinal
        End Function

        Public Function FormatDateTime(ByVal cCreateDAte As DateTime, ByVal showtime As Boolean) As String

            Return DAL2.BlogContent.Util.HumanFriendlyDate(cCreateDAte, True, showtime)

        End Function

        Public Function GetQuestionID(ByVal strkey As String, ByVal defaultkey As String) As Integer
            Return Convert.ToInt32(PortalController.GetPortalSetting(strkey, PortalId, defaultkey))
        End Function
        Public Function ReplaceUserTokens(ByVal strTemplate As String, ByVal iCreatebyuser As UserInfo, ByVal IsDetail As Boolean) As String

            If iCreatebyuser Is Nothing Then
                Return strTemplate
            End If

            If Not iCreatebyuser Is Nothing Then
                Dim strImageUrl As String = iCreatebyuser.Profile.PhotoURL
                If iCreatebyuser.IsSuperUser And iCreatebyuser.UserID = 40 Then
                    strImageUrl = "https://exchange.clearaction.com/Portals/_default/Users/001/01/1/logo-bright.png?ver=2018-01-23-221148-487"
                End If

                'Dim folderManager As DotNetNuke.Services.FileSystem.IFolderManager = DotNetNuke.Services.FileSystem.FolderManager.Instance

                'Dim folderInfo As DotNetNuke.Services.FileSystem.IFolderInfo = folderManager.GetUserFolder(iCreatebyuser)

                'Dim FolderPath As String = folderInfo.PhysicalPath + strImageUrl
                If String.IsNullOrEmpty(strImageUrl) Then
                    strImageUrl = " / images / no_avatar.gif"

                End If

                If (iCreatebyuser.DisplayName.Trim() <> "") Then
                    strTemplate = strTemplate.Replace("{AUTHORNAME}", IIf(IsDetail = True, "", "By ").ToString() + iCreatebyuser.DisplayName)
                    strTemplate = strTemplate.Replace("{AUTHORNAME_TAG}", String.Format("<a href='/MyProfile/UserName/{0}' class='tip'  data-tip='{1}'>{1}</a>", iCreatebyuser.Username, IIf(IsDetail = True, "", "By ").ToString() + iCreatebyuser.DisplayName))
                Else
                    strTemplate = strTemplate.Replace("{AUTHORNAME}", "")
                End If

                strTemplate = strTemplate.Replace("{AUTHOR_TITLE}", New DAL2.BlogContent.BlogContentController().GetProfileResponseByQuestion(GetQuestionID("Title", "5"), iCreatebyuser.UserID))

                strTemplate = strTemplate.Replace("{AUTHORIMAGE_TAG}", String.Format("<a href='/MyProfile/UserName/{0}'  class='tip'  data-tip='{2}'><img src='{1}' target='_blank' style='height:48px;width:48px'></img></a>", iCreatebyuser.Username, strImageUrl, iCreatebyuser.DisplayName))


                strTemplate = strTemplate.Replace("{AUTHORIMAGE}", strImageUrl)



                Dim ExcludeRoles As String = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings("Exculde_Role"))
                Dim strTemp As String = ""
                Dim iCount As Integer = iCreatebyuser.Roles.Length
                For i As Integer = 0 To iCount - 1
                    Dim strrole As String = iCreatebyuser.Roles(i).ToString()
                    If (ExcludeRoles.Contains(strrole) = False) Then
                        If (strrole.Equals(iCreatebyuser.Roles(iCount - 1))) Then
                            strTemp = strTemp + strrole
                        Else
                            strTemp = strTemp + strrole + ", "
                        End If
                    End If
                Next
                strTemplate = strTemplate.Replace("{ROLE}", "Influencer Role: " + strTemp)
            Else
                strTemplate = strTemplate.Replace("{AUTHORNAME}", "")
                'strTemplate = strTemplate.Replace("{TITLE}", "")
                strTemplate = strTemplate.Replace("{AUTHORIMAGE}", "/images/no_avatar.gif")
                strTemplate = strTemplate.Replace("{ROLE}", "")
                strTemplate = strTemplate.Replace("{AUTHOR_TITLE}", "")
            End If

            Return strTemplate

        End Function

        Public Shared Function TruncateAtWord(ByVal input As String, ByVal length As Integer, ByVal strUrl As String) As String
            If input Is Nothing OrElse input.Length < length Then Return input
            Dim iNextSpace As Integer = input.LastIndexOf(" ", length, StringComparison.Ordinal)
            Return String.Format("{0} <a  class='tip' data-tip='click continue reading for more' href='{1}'>...</a>", input.Substring(0, If((iNextSpace > 0), iNextSpace, length)).Trim(), strUrl)
        End Function

        Public Function ReplaceToken(ByVal oBlog As DAL2.BlogContent.BlogPostInfo, ByVal strTemplate As String, ByVal IsDetail As Boolean) As String
            If oBlog Is Nothing Then
                Return ""

            End If
            If String.IsNullOrEmpty(strTemplate) Then
                Return ""
            End If
            Dim strSeparator As String = "&nbsp;&nbsp;&nbsp;¦&nbsp;&nbsp;&nbsp;"

            If IsUserAdmin Then
                Dim strEditlink As String = DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "PostEdit", "mid=" & ModuleId, "Blog=1", DAL2.BlogContent.Util.PostKey & "=" & oBlog.ContentItemId.ToString())
                strTemplate = strTemplate.Replace("{PUBLISHSTATUS}", "")
                If oBlog.Published Then

                    strTemplate = strTemplate.Replace("{PUBLISHSTATUS}", "PUBLISHED")
                Else

                    strTemplate = strTemplate.Replace("{PUBLISHSTATUS}", "<span style='color:red'>UN-PUBLISHED</span>")

                End If

                strTemplate = strTemplate.Replace("{EDIT}", String.Format("<a href='{0}'><img alt='Edit' src='//{1}/images/Edit.gif'></img></a>", strEditlink, PortalAlias.HTTPAlias.ToString()))
            Else
                strTemplate = strTemplate.Replace("{PUBLISHSTATUS}", "")
                strTemplate = strTemplate.Replace("{EDIT}", "")
            End If


            Dim strBlogUrl As String = GetBlogDetailUrl(oBlog.ContentItemId, False, oBlog.Title)
            Dim iCreatebyuser As UserInfo = GetUser(oBlog.CreatedByUserID)
            If Not iCreatebyuser Is Nothing Then
                strTemplate = ReplaceUserTokens(strTemplate, iCreatebyuser, True)
                If (iCreatebyuser.DisplayName.Trim() <> "") Then
                    strTemplate = strTemplate.Replace("{AUTHORNAME}", IIf(IsDetail = True, "", "By ").ToString() + iCreatebyuser.DisplayName)
                    strTemplate = strTemplate.Replace("{AUTHORNAME_TAG}", String.Format("<a href='/MyProfile/UserName/{0}' class='tip'  data-tip='{1}'>{1}</a>", iCreatebyuser.Username, IIf(IsDetail = True, "", "By ").ToString() + iCreatebyuser.DisplayName))
                Else
                    strTemplate = strTemplate.Replace("{AUTHORNAME}", "")
                End If

                '  strTemplate = strTemplate.Replace("{TITLE}", iCreatebyuser.Profile.Title)

                strTemplate = strTemplate.Replace("{AUTHORIMAGE_TAG}", String.Format("<a href='/MyProfile/UserName/{0}'  class='tip'  data-tip='{2}'><img src='{1}' target='_blank' style='height:48px;width:48px'></img></a>", iCreatebyuser.Username, IIf(iCreatebyuser.Profile.PhotoURL.ToString() = "", "/images/no_avatar.gif", iCreatebyuser.Profile.PhotoURL).ToString(), iCreatebyuser.DisplayName))

                strTemplate = strTemplate.Replace("{AUTHORIMAGE}", IIf(iCreatebyuser.Profile.PhotoURL.ToString() = "", "/images/no_avatar.gif", iCreatebyuser.Profile.PhotoURL).ToString())
                Dim ExcludeRoles As String = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings("Exculde_Role"))
                Dim strTemp As String = ""
                Dim iCount As Integer = iCreatebyuser.Roles.Length
                For i As Integer = 0 To iCount - 1
                    Dim strrole As String = iCreatebyuser.Roles(i).ToString()
                    If (ExcludeRoles.Contains(strrole) = False) Then
                        If (strrole.Equals(iCreatebyuser.Roles(iCount - 1))) Then
                            strTemp = strTemp + strrole
                        Else
                            strTemp = strTemp + strrole + ", "
                        End If
                    End If
                Next
                strTemplate = strTemplate.Replace("{ROLE}", "Influencer Role: " + strTemp)
            Else
                strTemplate = strTemplate.Replace("{AUTHORNAME}", "")
                'strTemplate = strTemplate.Replace("{TITLE}", "")
                strTemplate = strTemplate.Replace("{AUTHORIMAGE}", "/images/no_avatar.gif")
                strTemplate = strTemplate.Replace("{ROLE}", "")
            End If

            Dim iUdatedbyUser As UserInfo = GetUser(oBlog.UpdatedByUserID)
            If Not iUdatedbyUser Is Nothing Then
                strTemplate = strTemplate.Replace("{EDIT_AUTHORNAME}", "&nbsp;&nbsp;Edited by " + iUdatedbyUser.DisplayName)
                strTemplate = strTemplate.Replace("{EDIT_AUTHORNAME_TAG}", String.Format("<a href='/MyProfile/UserName/{0}' class='tip'  data-tip='{1}'>{1}</a>", iUdatedbyUser.Username, IIf(IsDetail = True, "", "By ").ToString() + iUdatedbyUser.DisplayName))

                strTemplate = strTemplate.Replace("{EDIT_ONDATE}", FormatDateTime(oBlog.UpdatedOnDate, False))
                strTemplate = strTemplate.Replace("{EDIT_ONDATETIME}", FormatDateTime(oBlog.UpdatedOnDate, True))
                strTemplate = strTemplate.Replace("{EDIT_TITLE}", iUdatedbyUser.Profile.Title)
                strTemplate = strTemplate.Replace("{EDIT_AUTHORIMAGE}", IIf(iUdatedbyUser.Profile.PhotoURL = "", "/images/no_avatar.gif", iUdatedbyUser.Profile.PhotoURL).ToString())
                Dim ExcludeRoles As String = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings("Exculde_Role"))
                Dim strTemp As String = ""
                Dim iCount As Integer = iUdatedbyUser.Roles.Length
                For i As Integer = 0 To iCount - 1
                    Dim strrole As String = iUdatedbyUser.Roles(i).ToString()
                    If (ExcludeRoles.Contains(strrole) = False) Then
                        If (strrole.Equals(iUdatedbyUser.Roles(iCount - 1))) Then
                            strTemp = strTemp + strrole
                        Else
                            strTemp = strTemp + strrole + ", "
                        End If
                    End If
                Next

                '    Each strrole As String In iUdatedbyUser.Roles

                'Next
                strTemplate = strTemplate.Replace("{EDIT_ROLE}", strTemp)
            Else
                strTemplate = strTemplate.Replace("{EDIT_AUTHORNAME}", "")
                strTemplate = strTemplate.Replace("{EDIT_ONDATE}", "")
                strTemplate = strTemplate.Replace("{EDIT_TITLE}", "")
                strTemplate = strTemplate.Replace("{EDIT_AUTHORIMAGE}", "/images/no_avatar.gif")
                strTemplate = strTemplate.Replace("{EDIT_ROLE}", "")
            End If



            strTemplate = strTemplate.Replace("{MODULEPATH}", ModulePath)
            strTemplate = strTemplate.Replace("{HOME}", DotNetNuke.Common.Globals.NavigateURL(Me.TabId))
            Dim RecommendCount As String = Convert.ToString(oBlog.LikesCount("Post"))
            'strTemplate = strTemplate.Replace("{LIKESCOUNT}", RecommendCount + " Recommends" + strSeparator)
            strTemplate = strTemplate.Replace("{LIKESCOUNT}", RecommendCount + " Recommends")
            strTemplate = strTemplate.Replace("{VIEWCOUNT}", Convert.ToString(oBlog.ViewCount) + " Views")
            strTemplate = strTemplate.Replace("{COMMENTCOUNT}", Convert.ToString(oBlog.CommentCount))
            strTemplate = strTemplate.Replace("{ItemID}", oBlog.ContentItemId.ToString())
            strTemplate = strTemplate.Replace("{RECOMMENDCOUNT}", RecommendCount)
            strTemplate = strTemplate.Replace("{UNRECOMMENDCOUNT}", Convert.ToString(oBlog.UnLikesCount("Post")))
            '   strTemplate = strTemplate.Replace("{VIEWCOUNT}", Convert.ToString(oBlog.))
            strTemplate = strTemplate.Replace("{TITLE}", oBlog.Title)
            If (oBlog.CreatedOnDate <> Nothing) Then
                strTemplate = strTemplate.Replace("{PUBLISH_ONDATE}", strSeparator + FormatDateTime(oBlog.CreatedOnDate, False) + strSeparator)
                strTemplate = strTemplate.Replace("{PUBLISH_ONDATETIME}", FormatDateTime(oBlog.CreatedOnDate, True))
            Else
                strTemplate = strTemplate.Replace("{PUBLISH_ONDATE}", "")
                strTemplate = strTemplate.Replace("{PUBLISH_ONDATETIME}", "")
            End If


            strTemplate = strTemplate.Replace("{VIEWCOUNTS}", oBlog.ViewCount.ToString())
            strTemplate = strTemplate.Replace("{FILESTACKAPI}", System.Web.Configuration.WebConfigurationManager.AppSettings("FileStackApiKey"))

            If String.IsNullOrEmpty(oBlog.filestackurl) = False Then
                strTemplate = strTemplate.Replace("{FILESTACKURL}", oBlog.filestackurl)
            Else
                strTemplate = strTemplate.Replace("{FILESTACKURL}", "")
            End If




            If String.IsNullOrEmpty(oBlog.filestackurl) = False Then
                Dim tempImageName As String = oBlog.filestackurl
                Dim iStart As Integer = tempImageName.LastIndexOf("https://cdn.filestackcontent.com") + "https://cdn.filestackcontent.com".Length
                If (iStart > 0) Then
                    tempImageName = tempImageName.Insert(iStart, "/resize=width:700")
                End If
                strTemplate = strTemplate.Replace("{IMAGES}", String.Format("<img src='{0}' class='img-responsive' alt='{1}'></img>", tempImageName, oBlog.Title))
            Else
                strTemplate = strTemplate.Replace("{IMAGES}", "")
            End If



            If String.IsNullOrEmpty(oBlog.filestackurl) = False Then
                Dim tempImageName As String = oBlog.filestackurl
                Dim iStart As Integer = tempImageName.LastIndexOf("https://cdn.filestackcontent.com") + "https://cdn.filestackcontent.com".Length
                If (iStart > 0) Then
                    tempImageName = tempImageName.Insert(iStart, "/resize=width:700")
                End If
                strTemplate = strTemplate.Replace("{IMAGEURL}", String.Format("<img src='{0}' class='img-responsive' alt='{1}'></img>", tempImageName, oBlog.Title))
            Else
                strTemplate = strTemplate.Replace("{IMAGEURL}", "")
            End If


            'strTemplate = strTemplate.Replace("{IMAGES}", oBlog.Image)
            strTemplate = strTemplate.Replace("{URL}", strBlogUrl)
            strTemplate = strTemplate.Replace("{BODY}", Server.HtmlDecode(oBlog.Summary))
            strTemplate = strTemplate.Replace("{BlogPosts}", Server.HtmlDecode(oBlog.Summary))


            If Not IsDetail Then
                If Not String.IsNullOrEmpty(oBlog.ShortDescription) Then

                    strTemplate = strTemplate.Replace("{ShortDescription}", TruncateAtWord(Server.HtmlDecode(oBlog.ShortDescription), 200, strBlogUrl))

                Else
                    '   Dim strShortDescription As String = String.Empty
                    '  Dim iLength As Integer = oBlog.Summary.Length
                    If Not String.IsNullOrEmpty(oBlog.Summary) Then

                        strTemplate = strTemplate.Replace("{ShortDescription}", TruncateAtWord(Server.HtmlDecode(oBlog.Summary), 200, strBlogUrl))
                    End If

                End If
            End If



            Dim strCategory As String = String.Empty
            Dim lstBlogCats As IQueryable(Of DAL2.BlogContent.PostCategoryRelationInfo) = oBlog.GetBlogGlobalCategoryActiveOnly.AsQueryable().Take(3)
            strCategory = ReplaceCategoryToken(lstBlogCats.ToList(), ReadTemplate("CategoryTemplate"))
            If (strCategory.Trim() <> "") Then
                strTemplate = strTemplate.Replace("{CATEGORIES}", strCategory)
            Else
                strTemplate = strTemplate.Replace("{CATEGORIES}", "")
            End If
            strTemplate = strTemplate.Replace("{TAGS}", ReplaceTagsToken(oBlog, ReadTemplate("TagTemplate")))


            ''replace user status token
            '' Below code is C H A N G E D  by S A C H I N for Blog User's activity status
            ' Dim oUserComponent As DAL2.BlogContent.UserComponentsInfo = New DAL2.BlogContent.BlogContentController().GetUserStatus(oBlog.ContentItemId, UserId)
            Dim strStatus As String = "<img src='{0}'></img> {1} " + strSeparator
            If Not oBlog Is Nothing Then
                If oBlog.Status = "Completed" Then
                    strTemplate = strTemplate.Replace("{STATUS}", String.Format(strStatus, ResolveUrl("/DesktopModules/Blog/Images/img_17.png"), " Done")) ' "<img src='../DesktopModules/Blog/Images/{0}'></img> {1}"
                ElseIf oBlog.Status = "To-Do" Then
                    strTemplate = strTemplate.Replace("{STATUS}", String.Format(strStatus, ResolveUrl("/DesktopModules/Blog/Images/img_18.png"), " To Do"))
                Else
                    strTemplate = strTemplate.Replace("{STATUS}", "")
                End If
            Else
                ' dont show any image indicator in case it is not in TODO or is not yet Completed
                'strTemplate = strTemplate.Replace("{STATUS}", String.Format(strStatus, ResolveUrl("/DesktopModules/Blog/Images/img_18.png"), " To Do"))
            End If

            ' Below code is A D D E D  by S A C H I N  for Blog's User Context Menu for Add2MyVault
            ' User Context Menu for Non-Recommended Items
            Dim strUserMenu As String = "<img style='float:right;cursor:pointer;' src='//{2}/DesktopModules/Blog/images/more_icon.png' class='CA_UserMenu' CID='{0}' IsSelfAssigned='{1}'></img>"
            Dim ShowUserContextMenu As Boolean
            ' Previous implementation which hides the context menu i)n case status ='Completed' but we still need it
            'ShowUserContextMenu = Boolean.Parse(IIf(oBlog.Status Is Nothing, True, (IIf(((oBlog.IsSelfAssigned = 1) And (oBlog.Status = "To-Do")), True, False))).ToString())

            ShowUserContextMenu = True 'Boolean.Parse(IIf(((oBlog.IsSelfAssigned = 1) Or (oBlog.Status Is Nothing)), True, False).ToString())
            If (ShowUserContextMenu) Then
                strUserMenu = strUserMenu.Replace("{0}", oBlog.ContentItemId.ToString())
                strUserMenu = strUserMenu.Replace("{1}", oBlog.IsSelfAssigned.ToString())
                strUserMenu = strUserMenu.Replace("{2}", PortalAlias.HTTPAlias.ToString())
                strTemplate = strTemplate.Replace("{UserContextMenu}", strUserMenu)
                strTemplate = strTemplate.Replace("{SingleSaveVisibility}", "block")
                If (oBlog.IsSelfAssigned = 1) Then
                    strTemplate = strTemplate.Replace("{MyVaultText}", "REMOVE FROM MY VAULT")
                    strTemplate = strTemplate.Replace("{MyVaultFunction}", "CA_RemoveFromMyVault(" + oBlog.ContentItemId.ToString() + ")")
                Else
                    strTemplate = strTemplate.Replace("{MyVaultText}", "SAVE TO MY VAULT")
                    strTemplate = strTemplate.Replace("{MyVaultFunction}", "CA_Assign2Me(" + oBlog.ContentItemId.ToString() + ")")
                    ' 
                End If
            Else
                strTemplate = strTemplate.Replace("{UserContextMenu}", "")
                strTemplate = strTemplate.Replace("{SingleSaveVisibility}", "none")
            End If

            ' IsClasic Article status
            If (oBlog.IsClassicArticle) Then
                strTemplate = strTemplate.Replace("{ImgIsClassicArticle}", "<li>
                        <img src='/images/insights-classic-icon.png' alt='Classic Article'>Classic Article
                    </li> ")
                strTemplate = strTemplate.Replace("{IsClassicArticle}", "<img src='/images/insights-classic-icon.png' alt='Classic Article'> Classic Article - ")
            Else
                strTemplate = strTemplate.Replace("{ImgIsClassicArticle}", "")
                strTemplate = strTemplate.Replace("{IsClassicArticle}", "")
            End If

            Return strTemplate 'ReplaceCategoryToken(oBlog.GetBlogGlobalCategoryActiveOnly, strTemplate)
        End Function
#End Region
#Region "Public Common Methods"
        Public Function ReadTemplate(FileName As String) As String
            Dim strFilePath As String = Server.MapPath("DesktopModules") + String.Format("/Blog/Templates/{0}.html", FileName)

            Try
                If System.IO.File.Exists(strFilePath) Then
                    Return System.IO.File.ReadAllText(strFilePath)
                End If

            Catch exc As Exception
            End Try

            Return ""
        End Function

        Public Function GetModuleId() As Integer
            Dim Moduledefing As DotNetNuke.Entities.Modules.ModuleInfo = New DotNetNuke.Entities.Modules.ModuleController().GetModuleByDefinition(If(Me.PortalId = 1, 0, Me.PortalId), "DNNBlog.Blog")
            If Moduledefing IsNot Nothing Then
                Return Moduledefing.ModuleID
            End If
            Return 318
        End Function
        Public Function GetCategoryUrl(ByVal CategoryID As Integer) As String
            Return DotNetNuke.Common.Globals.NavigateURL(Me.TabId, "", "CategoryId=" & CategoryID.ToString())
        End Function
#End Region

#Region "Author or user Related"
        Public Function GetAvatarUrl(userID As Integer, avatarWidth As Integer, avatarHeight As Integer) As String
            Dim p As DotNetNuke.Entities.Portals.PortalSettings = TryCast(HttpContext.Current.Items("PortalSettings"), DotNetNuke.Entities.Portals.PortalSettings)
            If p Is Nothing Then
                Return String.Empty
            End If

            'GIF files when reduced using DNN class losses its animation, so for gifs send them as is
            Dim user As UserInfo = New DotNetNuke.Entities.Users.UserController().GetUser(PortalId, userID)
            Dim imgUrl As String = p.PortalAlias.ToString() & "/images/thumbnail.jpg"
            If user IsNot Nothing Then
                imgUrl = user.Profile.PhotoURL
            End If
            Return imgUrl
            '  return string.Format(Common.Globals.UserProfilePicRelativeUrl(), userID, avatarWidth, avatarHeight);
        End Function
        Public Function GetBackgroundImage(iAuthorid As Integer) As String
            Return "background:url('" + GetAvatarUrl(iAuthorid, 48, 48) + "') no-repeat 0 0;"
        End Function

        Public Function GetUser(ByVal iuserid As Integer) As DotNetNuke.Entities.Users.UserInfo
            Dim oUserInfo As UserInfo = UserController.GetUserById(0, iuserid)
            If oUserInfo Is Nothing Then
                Return Nothing
            End If
            Return oUserInfo
        End Function

        Public Function GetDisplayName(ByVal iuserid As Integer) As String
            Dim odata As UserInfo = GetUser(iuserid)
            If odata Is Nothing Then
                Return ""
            End If
            Return odata.DisplayName
        End Function

        Public ReadOnly Property IsUserAdmin() As Boolean
            Get

                Try
                    If HasModulePermission("BLOGGER") Then
                        Return True
                    End If
                    If DotNetNuke.Security.Permissions.ModulePermissionController.CanEditModuleContent(Me.ModuleConfiguration) Then
                        Return True
                    End If


                    If UserInfo.IsInRole(PortalSettings.AdministratorRoleName) OrElse UserInfo.IsSuperUser Then
                        Return True
                    End If
                Catch ex As Exception

                End Try
                Return False
            End Get
        End Property

#End Region
#Region "URL Related"
        Public Function GetBlogDetailUrl(ByVal iContentItemID As Integer, UpdateStatus As Boolean, ByVal Title As String) As String


            Return DAL2.BlogContent.Util.GetFormattedUrl(Me.TabId, DAL2.BlogContent.Util.GetBlogParam(iContentItemID, UpdateStatus, Title))
        End Function

#End Region




#Region " Properties "
        Private _blogContext As BlogContextInfo
        Public Property BlogContext() As BlogContextInfo
            Get
                If _blogContext Is Nothing Then
                    _blogContext = BlogContextInfo.GetBlogContext(Context, Me)
                End If
                Return _blogContext
            End Get
            Set(ByVal value As BlogContextInfo)
                _blogContext = value
            End Set
        End Property

        Private _settings As ModuleSettings
        Public Shadows Property Settings() As ModuleSettings
            Get
                If _settings Is Nothing Then
                    If ViewSettings.BlogModuleId = -1 Then
                        _settings = ModuleSettings.GetModuleSettings(ModuleConfiguration.ModuleID)
                    Else
                        _settings = ModuleSettings.GetModuleSettings(ViewSettings.BlogModuleId)
                    End If
                End If
                Return _settings
            End Get
            Set(ByVal value As ModuleSettings)
                _settings = value
            End Set
        End Property

        Private _categories As Dictionary(Of String, TermInfo)
        Public Property Categories() As Dictionary(Of String, TermInfo)
            Get
                If _categories Is Nothing Then
                    _categories = TermsController.GetTermsByVocabulary(ModuleId, Settings.VocabularyId, BlogContext.Locale)
                End If
                Return _categories
            End Get
            Set(ByVal value As Dictionary(Of String, TermInfo))
                _categories = value
            End Set
        End Property

        Private _viewSettings As ViewSettings
        Public Property ViewSettings() As ViewSettings
            Get
                If _viewSettings Is Nothing Then _viewSettings = ViewSettings.GetViewSettings(TabModuleId)
                Return _viewSettings
            End Get
            Set(ByVal value As ViewSettings)
                _viewSettings = value
            End Set
        End Property

        Public Shadows ReadOnly Property Page As CDefault
            Get
                Return CType(MyBase.Page, CDefault)
            End Get
        End Property

        Private _BlogModuleMapPath As String = ""
        Public ReadOnly Property BlogModuleMapPath As String
            Get
                If String.IsNullOrEmpty(_BlogModuleMapPath) Then
                    _BlogModuleMapPath = Server.MapPath("~/DesktopModules/Blog") & "\"
                End If
                Return _BlogModuleMapPath
            End Get
        End Property
#End Region

#Region " Event Handlers "
        Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

            If Context.Items("BlogModuleBaseInitialized") Is Nothing Then

                JavaScript.RequestRegistration(CommonJs.jQuery)
                JavaScript.RequestRegistration(CommonJs.jQueryUI)
                Dim script As New StringBuilder
                script.AppendLine("<script type=""text/javascript"">")
                script.AppendLine("//<![CDATA[")
                script.AppendLine(String.Format("var appPath='{0}'", DotNetNuke.Common.ApplicationPath))
                script.AppendLine("//]]>")
                script.AppendLine("</script>")
                ClientAPI.RegisterClientScriptBlock(Page, "blogAppPath", script.ToString)
                AddBlogService()

                Context.Items("BlogModuleBaseInitialized") = True
            End If

        End Sub
#End Region

#Region " Public Methods "
        Public Sub AddBlogService()

            If Context.Items("BlogServiceAdded") Is Nothing Then

                JavaScript.RequestRegistration(CommonJs.DnnPlugins)
                ServicesFramework.Instance.RequestAjaxScriptSupport()
                ServicesFramework.Instance.RequestAjaxAntiForgerySupport()
                AddJavascriptFile("dotnetnuke.blog.js", 70)

                ' Load initialization snippet
                Dim scriptBlock As String = ReadFile(DotNetNuke.Common.ApplicationMapPath & "\DesktopModules\Blog\js\dotnetnuke.blog.pagescript.js")
                Dim tr As New BlogTokenReplace(BlogContext.BlogModuleId)
                tr.AddResources("~/DesktopModules/Blog/App_LocalResources/SharedResources.resx")
                scriptBlock = tr.ReplaceTokens(scriptBlock)
                scriptBlock = "<script type=""text/javascript"">" & vbCrLf & "//<![CDATA[" & vbCrLf & scriptBlock & vbCrLf & "//]]>" & vbCrLf & "</script>"
                Page.ClientScript.RegisterClientScriptBlock([GetType], "BlogServiceScript", scriptBlock)

                Context.Items("BlogServiceAdded") = True
            End If

        End Sub

        Public Sub AddJavascriptFile(jsFilename As String, priority As Integer)
            Page.AddJavascriptFile(Settings.Version, jsFilename, priority)
        End Sub

        Public Sub AddJavascriptFile(jsFilename As String, name As String, version As String, priority As Integer)
            Page.AddJavascriptFile(Settings.Version, jsFilename, name, version, priority)
        End Sub

        Public Sub AddCssFile(cssFilename As String)
            Page.AddCssFile(Settings.Version, cssFilename)
        End Sub

        Public Sub AddCssFile(cssFilename As String, name As String, version As String)
            Page.AddCssFile(Settings.Version, cssFilename, name, version)
        End Sub

        Public Function LocalizeJSString(resourceKey As String) As String
            Return ClientAPI.GetSafeJSString(LocalizeString(resourceKey))
        End Function

        Public Function LocalizeJSString(resourceKey As String, resourceFile As String) As String
            Return ClientAPI.GetSafeJSString(Localization.GetString(resourceKey, resourceFile))
        End Function
#End Region

    End Class

End Namespace