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



Imports DotNetNuke.Modules.Blog.Data

Namespace Entities.Blogs

 Partial Public Class BlogsController

  Public Shared Function AddBlog(ByRef objBlog As BlogInfo, createdByUser As Integer) As Integer

   objBlog.BlogID = CType(DataProvider.Instance().AddBlog(objBlog.AutoApprovePingBack, objBlog.ModuleID, objBlog.AutoApproveTrackBack, objBlog.Copyright, objBlog.Description, objBlog.EnablePingBackReceive, objBlog.EnablePingBackSend, objBlog.EnableTrackBackReceive, objBlog.EnableTrackBackSend, objBlog.FullLocalization, objBlog.Image, objBlog.IncludeAuthorInFeed, objBlog.IncludeImagesInFeed, objBlog.Locale, objBlog.MustApproveGhostPosts, objBlog.OwnerUserId, objBlog.PublishAsOwner, objBlog.Published, objBlog.Syndicated, objBlog.SyndicationEmail, objBlog.Title, createdByUser), Integer)

   ' localization
   For Each l As String In objBlog.TitleLocalizations.Locales
    DataProvider.Instance().SetBlogLocalization(objBlog.BlogID, l, objBlog.TitleLocalizations(l), objBlog.DescriptionLocalizations(l))
   Next

   Return objBlog.BlogID

  End Function

  Public Shared Sub UpdateBlog(objBlog As BlogInfo, updatedByUser As Integer)

   DataProvider.Instance().UpdateBlog(objBlog.AutoApprovePingBack, objBlog.ModuleID, objBlog.AutoApproveTrackBack, objBlog.BlogID, objBlog.Copyright, objBlog.Description, objBlog.EnablePingBackReceive, objBlog.EnablePingBackSend, objBlog.EnableTrackBackReceive, objBlog.EnableTrackBackSend, objBlog.FullLocalization, objBlog.Image, objBlog.IncludeAuthorInFeed, objBlog.IncludeImagesInFeed, objBlog.Locale, objBlog.MustApproveGhostPosts, objBlog.OwnerUserId, objBlog.PublishAsOwner, objBlog.Published, objBlog.Syndicated, objBlog.SyndicationEmail, objBlog.Title, updatedByUser)

   ' localization
   For Each l As String In objBlog.TitleLocalizations.Locales
    DataProvider.Instance().SetBlogLocalization(objBlog.BlogID, l, objBlog.TitleLocalizations(l), objBlog.DescriptionLocalizations(l))
   Next

  End Sub

  Public Shared Sub DeleteBlog(blogID As Int32)

   DataProvider.Instance().DeleteBlog(blogID)

  End Sub

 End Class
End Namespace

