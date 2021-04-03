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
Imports DotNetNuke.Entities.Modules

Imports DotNetNuke.Modules.Blog.Common.Globals
Imports DotNetNuke.Modules.Blog.Entities.Blogs
Imports DotNetNuke.Modules.Blog.Entities.Terms
Imports DotNetNuke.Modules.Blog.Entities.Posts
Imports DotNetNuke.Modules.Blog.DAL2.BlogContent

Namespace Controls
    Public Class TopHeader
        Inherits BlogModuleBase



        Public _IsMyVault As Integer
        Private _CandEdit As Boolean
        Public Property CanEdit As Boolean
            Get
                Return _CandEdit
            End Get
            Set(value As Boolean)
                _CandEdit = value
            End Set
        End Property

        Public Property IsMyVault As Integer
            Get
                If ((GetQueryStringValue("", -99) <> -99) Or (Session("CA_Blog_IsMyVault") IsNot Nothing)) Then
                    _IsMyVault = Integer.Parse(Session("CA_Blog_IsMyVault").ToString())
                Else
                    _IsMyVault = iIsAll
                End If
                Return _IsMyVault
            End Get
            Set(value As Integer)
                _IsMyVault = value
            End Set
        End Property

        Public ReadOnly Property CurrentStatus As Integer
            Get
                Dim iReturn As Integer = 0
                Try
                    If Session("CA_Blog_Status") IsNot Nothing Then
                        iReturn = Integer.Parse(Session("CA_Blog_Status").ToString())
                    Else
                        iReturn = 0
                    End If
                Catch ex As Exception
                End Try

                Return iReturn
            End Get
        End Property

        Private _tID As Integer
        Public Property tID As Integer
            Get
                Return _tID
            End Get
            Set(value As Integer)
                _tID = value
            End Set
        End Property

        Public Sub UpdatePostCount(ByVal icounter As String)
            lblTopicCount.Text = "Posts " & icounter 'String.Format("{0}", icounter)
        End Sub

        Protected Sub btnShowAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShowAll.Click
            Session("CA_Blog_IsMyVault") = iIsAll
            Session("CA_Blog_CategoryID") = -1
            Response.Redirect(BuildQuery(UserId, -1, GetQueryStringValue("SortBy", 0), -1))
        End Sub

        Protected Sub btnMyVault_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMyVault.Click
            IsMyVault = 1
            Session("CA_Blog_IsMyVault") = iIsMyValut
            Session("CA_Blog_CategoryID") = -1
            Response.Redirect(BuildQuery(UserId, GetQueryStringValue("CategoryID", -1), GetQueryStringValue("SortBy", 0), -1))
        End Sub


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            '  ViewSettings.TemplateSettings.ReadValue("pagesize", _pageSize)
            '   Request.Params.ReadValue("Page", _reqPage)

            If GetQueryStringValue("CategoryId", -99) = -99 Then
                Session("CA_Blog_Status") = Nothing
                Session("CA_Blog_IsMyVault") = Nothing
                Session("CA_Blog_CategoryID") = Nothing
                Session("CA_Blog_IsMyVault") = Nothing
            End If

            If Not IsPostBack Then
                SetUI()




                If CanEdit Then
                    hyperlinkNew.Visible = True
                Else
                    hyperlinkNew.Visible = False
                End If
                hyperlinkNew.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(tID, "PostEdit", "mid=381", "Blog=1", DAL2.BlogContent.Util.PostKey & "=-1")
                '  txtSearch.Attributes.Add("onkeydown", "if(event.keyCode == 13){document.getElementById('" + lnkSearch.ClientID + "').click();}")

                txtSearch.Text = Server.UrlDecode(GetQueryStringValue("q", ""))
                '  End If
            End If



        End Sub
        Protected Sub dataListGlobalCat_ItemDataBound(ByVal sender As Object, ByVal e As DataListItemEventArgs) Handles dataListGlobalCat.ItemDataBound
            If (e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem) Then
                Dim lnkButton As LinkButton = CType(e.Item.FindControl("LinkButton1"), LinkButton)
                lnkButton.Text = "<div class='table'><div class='table-cell'>" & (CType(e.Item.DataItem, DAL2.BlogContent.BlogGlobalCategory)).DisplayName & "</div></div>"
                lnkButton.Attributes.Add("CatID", (CType(e.Item.DataItem, DAL2.BlogContent.BlogGlobalCategory)).CategoryId.ToString())
                lnkButton.CommandArgument = (CType(e.Item.DataItem, DAL2.BlogContent.BlogGlobalCategory)).CategoryId.ToString()
                lnkButton.CommandName = "LoadTopic"
            End If
        End Sub

        Private Sub lnkSearch_Click(sender As Object, e As EventArgs) Handles lnkSearch.Click
            If txtSearch.Text.Trim() <> "" Then
                Session("CA_Blog_SearchKey") = txtSearch.Text.Trim()
                Dim iGlobalCategory As Integer = -1
                If Not Session("CA_Blog_CategoryID") Is Nothing Then
                    iGlobalCategory = Convert.ToInt32(Session("CA_Blog_CategoryID"))
                End If

                Response.Redirect(BuildQuery(UserId, GetQueryStringValue("CategoryId", iGlobalCategory), GetQueryStringValue("SortBy", 0), -1, Server.UrlEncode(txtSearch.Text.Trim())))
                ' Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId, "bSearch", "mid=" & GetModuleId(), "q=" + HttpUtility.UrlEncode(txtSearch.Text.Trim())), True)
            Else
                Session("CA_Blog_SearchKey") = ""
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(TabId), True)
            End If
        End Sub

        Private Sub SetUI()
            Dim controller As New DAL2.BlogContent.BlogContentController()
            '   Dim oGlobalCategory As List(Of BlogGlobalCategory) = controller.GetGlobalCategory(DAL2.BlogContent.Util.ComponentID).ToList()
            Dim oGlobalCategory As List(Of BlogGlobalCategory) = controller.GetGlobalCategory(-1).ToList()
            dataListGlobalCat.DataSource = oGlobalCategory
            dataListGlobalCat.DataBind()
        End Sub
        Protected Sub btnAllCat_Click(sender As Object, e As EventArgs) Handles btnAllCat.Click
            Session("CA_Blog_CategoryID") = "-1"
            Response.Redirect(BuildQuery(UserId, -1, GetQueryStringValue("SortBy", 0), -1))
        End Sub
        Protected Sub dataListGlobalCat_ItemCommand(ByVal source As Object, ByVal e As DataListCommandEventArgs)
            If e.CommandName.ToLower() = "loadtopic" Then
                Dim iCatID As Integer = Integer.Parse(e.CommandArgument.ToString())
                Session("CA_Blog_CategoryID") = e.CommandArgument.ToString()
                Response.Redirect(BuildQuery(UserId, iCatID, GetQueryStringValue("SortBy", 0), -1))
            End If
        End Sub
        'Protected Sub ddGlobalCategory_SelectedIndexChanged(sender As Object, e As EventArgs)

        '    Dim oSelected As String = ddGlobalCategory.SelectedValue
        '    If oSelected = "-2" Or oSelected = "-3" Or oSelected = "-4" Then
        '        If oSelected = "-2" Then
        '            oSelected = "1"
        '        End If
        '        If oSelected = "-3" Then
        '            oSelected = "2"
        '        End If
        '        If oSelected = "-4" Then
        '            oSelected = "0"
        '            Response.Redirect(BuildQuery(UserId, -4, GetQueryStringValue("SortBy", 0), -1, Convert.ToInt32(oSelected)))
        '        End If
        '        Response.Redirect(BuildQuery(UserId, GetQueryStringValue("CategoryID", -1), GetQueryStringValue("SortBy", 0), -1, Convert.ToInt32(oSelected)))
        '    Else
        '        Response.Redirect(BuildQuery(UserId, CategoryFilter, SortBy, -1, 0))
        '    End If





        'End Sub
        'Protected Sub ddSortBy_SelectedIndexChanged(sender As Object, e As EventArgs)
        '    Response.Redirect(BuildQuery(UserId, GetQueryStringValue("CategoryId", CategoryFilter), SortBy, -1, GetQueryStringValue("HasSeen", 0)))
        'End Sub
    End Class
End Namespace