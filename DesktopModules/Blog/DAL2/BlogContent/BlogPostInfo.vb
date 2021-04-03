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
Imports DotNetNuke.Common.Globals
Imports DotNetNuke.Modules.Blog.Common.Globals
Imports DotNetNuke.Modules.Blog.Entities.Blogs
Imports DotNetNuke.Modules.Blog.Entities.Terms
Imports DotNetNuke.ComponentModel.DataAnnotations
Imports System.Collections.Generic
Imports System.Text
Imports DotNetNuke
Imports DotNetNuke.Modules.Blog.DAL2.BlogContent.BlogContentController

Namespace DAL2.BlogContent
    <TableName("Blog_Posts")>
    <PrimaryKey("ContentItemId")>
    Partial Public Class BlogPostInfo

        Private m_Status As String
        <ReadOnlyColumn>
        Public Property Status() As String
            Get
                Return m_Status
            End Get
            Set(value As String)
                m_Status = value
            End Set
        End Property
        Private m_IsAssigned As Integer
        <ReadOnlyColumn>
        Public Property IsAssigned() As Integer
            Get
                Return m_IsAssigned
            End Get
            Set(value As Integer)
                m_IsAssigned = value
            End Set
        End Property
        Private m_IsSelfAssigned As Integer
        <ReadOnlyColumn>
        Public Property IsSelfAssigned() As Integer
            Get
                Return m_IsSelfAssigned
            End Get
            Set(value As Integer)
                m_IsSelfAssigned = value
            End Set
        End Property
        Private m_ContentItemId As Integer
        Public Property ContentItemId() As Integer
            Get
                Return m_ContentItemId
            End Get
            Set
                m_ContentItemId = Value
            End Set
        End Property

        Private m_BlogID As Integer
        Public Property BlogID() As Integer
            Get
                Return m_BlogID
            End Get
            Set
                m_BlogID = Value
            End Set
        End Property

        Private m_PostInfo As PostContentInfo

        <ReadOnlyColumn>
        Public Property GetPostInfo() As PostContentInfo
            Get
                If ContentItemId > 0 Then
                    m_PostInfo = New BlogContentController().GetBlogById(ContentItemId)
                    If m_PostInfo IsNot Nothing Then

                        Return m_PostInfo
                    End If

                End If
                Return Nothing
            End Get
            Set(value As PostContentInfo)
                m_PostInfo = New BlogContentController().GetBlogById(ContentItemId)
            End Set
        End Property

        Private m_TagList As IQueryable(Of CA_TaxonomyTerms)
        <ReadOnlyColumn>
        Public Property GetTagsByItemID() As IQueryable(Of CA_TaxonomyTerms)
            Get
                If ContentItemId > 0 Then
                    m_TagList = New BlogContentController().GetTagsByContentItemID(ContentItemId)
                    If m_TagList IsNot Nothing Then
                        Return m_TagList
                    End If
                End If
                Return Nothing
            End Get
            Set(value As IQueryable(Of CA_TaxonomyTerms))
                m_TagList = New BlogContentController().GetTagsByContentItemID(ContentItemId)
            End Set
        End Property

        Private m_GetAuthorDisplayName As String

        <ReadOnlyColumn>
        Public ReadOnly Property GetAuthorDisplayName() As String
            Get
                Return ""
            End Get

        End Property


        Private m_ShortDescription As String
        Public Property ShortDescription() As String

            Get
                Return m_ShortDescription
            End Get
            Set
                m_ShortDescription = Value
            End Set
        End Property

        Private m_Title As String
        Public Property Title() As String

            Get
                Return m_Title
            End Get
            Set
                m_Title = Value
            End Set
        End Property

        Private m_IsClassicArticle As Boolean
        Public Property IsClassicArticle() As Boolean
            Get
                Return m_IsClassicArticle
            End Get
            Set(value As Boolean)
                m_IsClassicArticle = value
            End Set
        End Property

        Private m_Summary As String
        Public Property Summary() As String

            Get
                Return m_Summary
            End Get
            Set
                m_Summary = Value
            End Set
        End Property

        Private m_Image As String
        Public Property Image() As String

            Get
                Return m_Image
            End Get
            Set
                m_Image = Value
            End Set
        End Property

        Private m_filestackurl As String
        Public Property filestackurl() As String

            Get
                Return m_filestackurl
            End Get
            Set
                m_filestackurl = Value
            End Set
        End Property

        Private m_Published As Boolean
        Public Property Published() As Boolean

            Get
                Return m_Published
            End Get
            Set
                m_Published = Value
            End Set
        End Property

        Private m_PublishedOnDate As DateTime
        Public Property PublishedOnDate() As DateTime

            Get
                Return m_PublishedOnDate
            End Get
            Set
                m_PublishedOnDate = Value
            End Set
        End Property

        Private m_AllowComments As Boolean
        Public Property AllowComments() As Boolean

            Get
                Return m_AllowComments
            End Get
            Set
                m_AllowComments = Value
            End Set
        End Property

        Private m_DisplayCopyright As Boolean
        Public Property DisplayCopyright() As Boolean

            Get
                Return m_DisplayCopyright
            End Get
            Set
                m_DisplayCopyright = Value
            End Set
        End Property

        Private m_Copyright As String
        Public Property Copyright() As String

            Get
                Return m_Copyright
            End Get
            Set
                m_Copyright = Value
            End Set
        End Property

        Private m_Locale As String
        Public Property Locale() As String

            Get
                Return m_Locale
            End Get
            Set
                m_Locale = Value
            End Set
        End Property

        Private m_ViewCount As Integer
        Public Property ViewCount() As Integer

            Get
                Return m_ViewCount
            End Get
            Set
                m_ViewCount = Value
            End Set
        End Property

        Private m_CategoryID As Integer
        Public Property CategoryID() As Integer

            Get
                Return m_CategoryID
            End Get
            Set
                m_CategoryID = Value
            End Set
        End Property


        Private m_CreatedByUserID As Integer
        Public Property CreatedByUserID() As Integer

            Get
                Return m_CreatedByUserID
            End Get
            Set
                m_CreatedByUserID = Value
            End Set
        End Property

        Private m_CreatedOnDate As DateTime
        Public Property CreatedOnDate() As DateTime

            Get
                Return m_CreatedOnDate
            End Get
            Set
                m_CreatedOnDate = Value
            End Set
        End Property


        Private m_UpdatedByUserID As Integer
        Public Property UpdatedByUserID() As Integer

            Get
                Return m_UpdatedByUserID
            End Get
            Set
                m_UpdatedByUserID = Value
            End Set
        End Property

        Private m_UpdatedOnDate As DateTime
        Public Property UpdatedOnDate() As DateTime

            Get
                Return m_UpdatedOnDate
            End Get
            Set
                m_UpdatedOnDate = Value
            End Set
        End Property

        <ReadOnlyColumn>
        Public ReadOnly Property GetBlogGlobalCategory() As List(Of PostCategoryRelationInfo)

            Get
                If ContentItemId = -1 Then
                    Return Nothing
                End If
                Dim oCategory As List(Of PostCategoryRelationInfo) = New DAL2.BlogContent.BlogContentController().GetCateogryByPostID(ContentItemId, False).OrderBy(Function(x) x.CategoryId).ToList()

                Return oCategory
            End Get

        End Property
        <ReadOnlyColumn>
        Public ReadOnly Property GetBlogGlobalCategoryActiveOnly() As IQueryable(Of PostCategoryRelationInfo)

            Get
                If ContentItemId = -1 Then
                    Return Nothing
                End If
                Dim oCategory As IQueryable(Of PostCategoryRelationInfo) = New DAL2.BlogContent.BlogContentController().GetCateogryByPostID(ContentItemId, True).Distinct().Where(Function(x) x.IsActive = True).OrderBy(Function(x) x.CategoryId).ToList().AsQueryable()

                Return oCategory.AsQueryable()
            End Get

        End Property


        <ReadOnlyColumn>
        Public ReadOnly Property LikesCount(ItemType As String) As Integer

            Get
                If ContentItemId = -1 Then
                    Return 0
                End If
                Return Convert.ToInt32(New DAL2.BlogContent.BlogContentController().GetForPostCount(ContentItemId, ItemType))


            End Get

        End Property

        <ReadOnlyColumn>
        Public ReadOnly Property UnLikesCount(ItemType As String) As Integer

            Get
                If ContentItemId = -1 Then
                    Return 0
                End If
                Return New DAL2.BlogContent.BlogContentController().GetUnlikeForPost(ContentItemId, "Post").Count
            End Get

        End Property

        <ReadOnlyColumn>
        Public ReadOnly Property CommentCount() As Integer

            Get
                If ContentItemId = -1 Then
                    Return 0
                End If
                Return New DAL2.BlogContent.BlogContentController().GetCommentsbyContentID_Count(ContentItemId)



            End Get

        End Property





        <ReadOnlyColumn>
        Public ReadOnly Property GetBlogDetailUrl() As String

            Get
                '  Dim iTabID As Integer = DotNetNuke.Entities.Tabs.TabController.Instance.GetTabByName("Insights", 0).TabID
                Return Util.GetFormattedUrl(Util.GetBlogParam(ContentItemId, True, Title))


            End Get

        End Property









    End Class
End Namespace