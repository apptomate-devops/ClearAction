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

Imports DotNetNuke.Entities.Users

Namespace Security

 ''' <summary>
 ''' This class makes the UserInfo more robust and adds the much missed IsAdministrator to this 
 ''' object so we don't have to keep on looking this up.
 ''' </summary>
 ''' <remarks></remarks>
 <Serializable()>
 Public Class BlogUser
  Inherits UserInfo

#Region " Private Members "
#End Region

#Region " Constructors "
  Public Sub New()
   LoadUser(Nothing)
  End Sub

  Public Sub New(ByRef user As UserInfo)
   LoadUser(user)
  End Sub

  Private Sub LoadUser(user As UserInfo)
   If user Is Nothing Then
    user = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo
   End If

   AffiliateID = user.AffiliateID
   DisplayName = Common.Globals.GetAString(user.DisplayName)
   Email = Common.Globals.GetAString(user.Email)
   FirstName = Common.Globals.GetAString(user.FirstName)
   IsSuperUser = user.IsSuperUser
   LastName = Common.Globals.GetAString(user.LastName)
   Membership = user.Membership
   PortalID = user.PortalID
   Profile = user.Profile
   Roles = user.Roles
   MyBase.UserID = user.UserID
   UserID = user.UserID
   Username = Common.Globals.GetAString(user.Username)

   If IsSuperUser Then
    IsAdministrator = True
   ElseIf UserID > -1 Then
    Dim objPortals As New DotNetNuke.Entities.Portals.PortalController
    Dim objPortal As DotNetNuke.Entities.Portals.PortalInfo = objPortals.GetPortal(PortalID)
    If IsInRole(objPortal.AdministratorRoleName) Then
     IsAdministrator = True
    End If
   End If

   If UserID = -1 Then
    Try
     PortalID = DotNetNuke.Entities.Portals.PortalController.Instance.GetCurrentPortalSettings.PortalId
    Catch ex As Exception
     Exit Sub
    End Try
   End If

  End Sub
#End Region

#Region " Public Properties "
  Public Property IsAdministrator As Boolean = False
#End Region

#Region " Public Shared Methods "
  Public Shared Function GetCurrentUser() As BlogUser
   Dim dnnUser As UserInfo = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo
   Dim cacheKey As String = String.Format("BlogUser{0}-{1}", dnnUser.PortalID, dnnUser.UserID)
   Dim du As BlogUser = Nothing
   Try
    du = CType(DotNetNuke.Common.Utilities.DataCache.GetCache(cacheKey), BlogUser)
   Catch
   End Try
   If du Is Nothing Then
    du = New BlogUser(dnnUser)
    DotNetNuke.Common.Utilities.DataCache.SetCache(cacheKey, du)
   End If
   Return du
  End Function

  Public Shared Function GetUser(portalId As Integer, userId As Integer) As BlogUser
   Dim cacheKey As String = String.Format("BlogUser{0}-{1}", portalId, userId)
   Dim du As BlogUser = Nothing
   Try
    du = CType(DotNetNuke.Common.Utilities.DataCache.GetCache(cacheKey), BlogUser)
   Catch
   End Try
   If du Is Nothing Then
    Dim dnnUser As UserInfo = DotNetNuke.Entities.Users.UserController.GetUserById(portalId, userId)
    If dnnUser Is Nothing Then
     dnnUser = New UserInfo
     dnnUser.PortalID = portalId
    End If
    du = New BlogUser(dnnUser)
    DotNetNuke.Common.Utilities.DataCache.SetCache(cacheKey, du)
   End If
   Return du
  End Function

  Public Shared Function GetUser(user As UserInfo) As BlogUser
   Dim cacheKey As String = String.Format("BlogUser{0}-{1}", user.PortalID, user.UserID)
   Dim du As BlogUser = Nothing
   Try
    du = CType(DotNetNuke.Common.Utilities.DataCache.GetCache(cacheKey), BlogUser)
   Catch
   End Try
   If du Is Nothing Then
    du = New BlogUser(user)
    DotNetNuke.Common.Utilities.DataCache.SetCache(cacheKey, du)
   End If
   Return du
  End Function
#End Region

 End Class
End Namespace