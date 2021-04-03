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

Namespace Entities.Comments

    Partial Public Class CommentsController

        Public Shared Function AddComment(ByRef objComment As CommentInfo, createdByUser As Integer) As Integer

            objComment.CommentID = CType(DataProvider.Instance().AddComment(objComment.Approved, objComment.Author, objComment.Comment, objComment.ContentItemId, objComment.Email, objComment.ParentId, objComment.Website, createdByUser, objComment.attachUrl, objComment.AttachName), Integer)
            Return objComment.CommentID

        End Function

        Public Shared Function AddComment(ByRef objComment As CommentInfo, createdByUser As Integer, ByVal iParentId As Integer) As Integer

            objComment.CommentID = CType(DataProvider.Instance().AddComment(objComment.Approved, objComment.Author, objComment.Comment, objComment.ContentItemId, objComment.Email, objComment.ParentId, objComment.Website, createdByUser, objComment.attachUrl, objComment.AttachName), Integer)

            If iParentId > 0 Then
                DataProvider.Instance().UpdateComment(objComment.Approved, objComment.Author, objComment.Comment, objComment.CommentID, objComment.ContentItemId, objComment.Email, objComment.ParentId, objComment.Website, objComment.LastModifiedByUserID, objComment.attachUrl, objComment.AttachName)


            End If
            Return objComment.CommentID

        End Function

        Public Shared Sub UpdateComment(objComment As CommentInfo, updatedByUser As Integer)

            DataProvider.Instance().UpdateComment(objComment.Approved, objComment.Author, objComment.Comment, objComment.CommentID, objComment.ContentItemId, objComment.Email, objComment.ParentId, objComment.Website, updatedByUser)

        End Sub

        Public Shared Sub DeleteComment(commentID As Int32)

            DataProvider.Instance().DeleteComment(commentID)

        End Sub

    End Class
End Namespace

