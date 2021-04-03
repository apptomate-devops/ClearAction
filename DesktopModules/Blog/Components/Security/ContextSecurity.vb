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

Imports DotNetNuke.Security.Permissions
Imports DotNetNuke.Modules.Blog.Entities
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Modules.Blog.Common.Globals
Imports DotNetNuke.Modules.Blog.Entities.Blogs
Imports DotNetNuke.Modules.Blog.Security.Security
Imports DotNetNuke.Services.Tokens

Namespace Security

 Public Class ContextSecurity
  Implements IPropertyAccess

#Region " Private Members "
  Private _canAdd As Boolean = False
  Private _canEdit As Boolean = False
  Private _canApprove As Boolean = False
  Private _canAddComment As Boolean = False
  Private _canApproveComment As Boolean = False
  Private _canAutoApproveComment As Boolean = False
  Private _canViewComment As Boolean = False
  Private _userIsAdmin As Boolean = False
  Private _isBlogger As Boolean = False
  Private _isEditor As Boolean = False
  Private _userId As Integer = -1
#End Region

#Region " Constructor "
  Public Sub New(moduleId As Integer, tabId As Integer, blog As BlogInfo, user As UserInfo)
   _userId = user.UserID
   If blog IsNot Nothing Then
    IsOwner = CBool(blog.CreatedByUserID = user.UserID)
    _canAdd = blog.CanAdd
    _canEdit = blog.CanEdit
    _canApprove = blog.CanApprove
    _canViewComment = blog.Permissions.CurrentUserHasPermission("VIEWCOMMENT")
    _canApproveComment = blog.Permissions.CurrentUserHasPermission("APPROVECOMMENT")
    _canAutoApproveComment = blog.Permissions.CurrentUserHasPermission("AUTOAPPROVECOMMENT")
    _canAddComment = blog.Permissions.CurrentUserHasPermission("ADDCOMMENT")
   Else
    Using ir As IDataReader = Data.DataProvider.Instance().GetUserPermissionsByModule(moduleId, user.UserID)
     Do While ir.Read
      Dim permissionId As Integer = CInt(ir.Item("PermissionId"))
      Dim hasPermission As Integer = CInt(ir.Item("HasPermission"))
      If hasPermission > 0 Then
       Select Case permissionId
        Case BlogPermissionTypes.ADD
         _canAdd = True
        Case BlogPermissionTypes.EDIT
         _canEdit = True
        Case BlogPermissionTypes.APPROVE
         _canApprove = True
       End Select
      End If
     Loop
    End Using
   End If
   LoggedIn = CBool(user.UserID > -1)
   _userIsAdmin = DotNetNuke.Security.PortalSecurity.IsInRole(DotNetNuke.Entities.Portals.PortalSettings.Current.AdministratorRoleName)
   Dim mc As New DotNetNuke.Entities.Modules.ModuleController
   Dim objMod As New DotNetNuke.Entities.Modules.ModuleInfo
   objMod = mc.GetModule(moduleId, tabId, False)
   If objMod IsNot Nothing Then
    _isBlogger = ModulePermissionController.HasModulePermission(objMod.ModulePermissions, BloggerPermission)
    _isEditor = ModulePermissionController.HasModulePermission(objMod.ModulePermissions, "EDIT")
   End If
  End Sub
#End Region

#Region " Public Properties "
  Public Property IsOwner As Boolean = False
  Public Property LoggedIn As Boolean = False

  Public ReadOnly Property CanEditPost() As Boolean
   Get
    Return _canEdit Or IsOwner Or UserIsAdmin
   End Get
  End Property
  Public Function CanEditThisPost(post As Posts.PostInfo) As Boolean
            Try
                If CanEditPost Then Return True
                If post Is Nothing Then Return False
                If post.Blog.MustApproveGhostPosts AndAlso Not CanApprovePost Then
                    If post.CreatedByUserID = _userId And Not post.Published Then Return True
                Else
                    If post.CreatedByUserID = _userId Then Return True
                End If
            Catch ex As Exception

            End Try
            Return False
  End Function

  Public ReadOnly Property CanAddPost() As Boolean
   Get
    Return _canAdd Or IsOwner Or UserIsAdmin
   End Get
  End Property

  Public ReadOnly Property CanAddComment() As Boolean
   Get
    Return _canAddComment Or IsOwner Or UserIsAdmin
   End Get
  End Property

  Public ReadOnly Property CanViewComments() As Boolean
   Get
    Return _canViewComment Or IsOwner Or UserIsAdmin
   End Get
  End Property

  Public ReadOnly Property CanApprovePost() As Boolean
   Get
    Return _canApprove Or IsOwner Or UserIsAdmin
   End Get
  End Property

  Public ReadOnly Property CanApproveComment() As Boolean
   Get
    Return _canApproveComment Or IsOwner Or UserIsAdmin
   End Get
  End Property

  Public ReadOnly Property CanAutoApproveComment() As Boolean
   Get
    Return _canAutoApproveComment Or IsOwner Or UserIsAdmin
   End Get
  End Property

  Public ReadOnly Property CanDoSomethingWithPosts As Boolean
   Get
    Return _canEdit Or _canAdd Or _canApprove Or _isBlogger Or UserIsAdmin
   End Get
  End Property

  Public ReadOnly Property UserIsAdmin As Boolean
   Get
    Return _userIsAdmin
   End Get
  End Property

  Public ReadOnly Property IsBlogger As Boolean
   Get
    Return _isBlogger
   End Get
  End Property

  Public ReadOnly Property IsEditor As Boolean
   Get
    Return _isEditor
   End Get
  End Property
#End Region

#Region " IPropertyAccess Implementation "
  Public Function GetProperty(strPropertyName As String, strFormat As String, formatProvider As System.Globalization.CultureInfo, AccessingUser As DotNetNuke.Entities.Users.UserInfo, AccessLevel As DotNetNuke.Services.Tokens.Scope, ByRef PropertyNotFound As Boolean) As String Implements DotNetNuke.Services.Tokens.IPropertyAccess.GetProperty
   Dim OutputFormat As String = String.Empty
   Dim portalSettings As DotNetNuke.Entities.Portals.PortalSettings = DotNetNuke.Entities.Portals.PortalController.Instance.GetCurrentPortalSettings
   If strFormat = String.Empty Then
    OutputFormat = "D"
   Else
    OutputFormat = strFormat
   End If
   Select Case strPropertyName.ToLower
    Case "isowner"
     Return IsOwner.ToString
    Case "isowneryesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(IsOwner, formatProvider)
    Case "caneditpost"
     Return CanEditPost.ToString
    Case "caneditpostyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(CanEditPost, formatProvider)
    Case "canaddpost"
     Return CanAddPost.ToString
    Case "canaddpostyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(CanAddPost, formatProvider)
    Case "canaddcomment"
     Return CanAddComment.ToString
    Case "canaddcommentyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(CanAddComment, formatProvider)
    Case "canviewcomments"
     Return CanViewComments.ToString
    Case "canviewcommentsyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(CanViewComments, formatProvider)
    Case "canapprovepost"
     Return CanApprovePost.ToString
    Case "canapprovepostyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(CanApprovePost, formatProvider)
    Case "canapprovecomment"
     Return CanApproveComment.ToString
    Case "canapprovecommentyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(CanApproveComment, formatProvider)
    Case "canautoapprovecomment"
     Return CanAutoApproveComment.ToString
    Case "canautoapprovecommentyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(CanAutoApproveComment, formatProvider)
    Case "candosomethingwithposts"
     Return CanDoSomethingWithPosts.ToString
    Case "candosomethingwithpostsyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(CanDoSomethingWithPosts, formatProvider)
    Case "userisadmin"
     Return UserIsAdmin.ToString
    Case "userisadminyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(UserIsAdmin, formatProvider)
    Case "isblogger"
     Return IsBlogger.ToString
    Case "isbloggeryesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(IsBlogger, formatProvider)
    Case "iseditor"
     Return IsEditor.ToString
    Case "iseditoryesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(IsEditor, formatProvider)
    Case "loggedin"
     Return LoggedIn.ToString
    Case "loggedinyesno"
     Return PropertyAccess.Boolean2LocalizedYesNo(LoggedIn, formatProvider)
    Case Else
     PropertyNotFound = True
   End Select
   Return DotNetNuke.Common.Utilities.Null.NullString
  End Function

  Public ReadOnly Property Cacheability() As DotNetNuke.Services.Tokens.CacheLevel Implements DotNetNuke.Services.Tokens.IPropertyAccess.Cacheability
   Get
    Return CacheLevel.fullyCacheable
   End Get
  End Property
#End Region

 End Class
End Namespace