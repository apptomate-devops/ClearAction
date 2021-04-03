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

Imports DotNetNuke.Modules.Blog.Entities.Blogs
Imports System.Linq
Imports DotNetNuke.Modules.Blog.Entities.Posts

Public Class SearchResult
    Inherits BlogModuleBase

    Private _totalPosts As Integer = -1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            If GetQueryStringValue("q", "") = "" Then
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(Me.TabId))

            End If
            DoSearch()
        End If


    End Sub

    Private Sub DoSearch()

        Dim oSearchResult As List(Of DAL2.BlogContent.BlogPostInfo) = New DAL2.BlogContent.BlogContentController().GetSearchResult(GetModuleId(), "en-Us", UserInfo.UserID, IsUserAdmin, GetQueryStringValue("q", ""))
        If oSearchResult Is Nothing Or oSearchResult.Count = 0 Then
            ltrlSearch.Text = ReadTemplate("NoRecord")
            Return
        End If

        Dim sTemplate As String = ReadTemplate("BlogSearch")
        Dim strData As New System.Text.StringBuilder
        For Each oSearch As DAL2.BlogContent.BlogPostInfo In oSearchResult
            strData.Append(ReplaceToken(oSearch, sTemplate, False))
        Next

        ltrlSearch.Text = strData.ToString()

        lblSearchheading.Text = String.Format(" Found <strong>{0}</strong> itmes for your search <strong>{1}</strong> ", oSearchResult.Count.ToString(), GetQueryStringValue("q", ""))

    End Sub


End Class