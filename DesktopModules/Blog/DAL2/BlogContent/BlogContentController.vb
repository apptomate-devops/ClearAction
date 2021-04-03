Imports DotNetNuke.Modules.Blog.Data
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Data
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DotNetNuke.Modules.Blog.Security
Imports DotNetNuke.ComponentModel.DataAnnotations

Namespace DAL2.BlogContent


    Public Class BlogContentController

        ' Private data As New Dictionary(Of Integer, BlogGlobalCategory)

#Region "Add Post Category Relation"

        Public Sub PostCategoryRelation(ByVal oPostCategory As List(Of PostCategoryRelationInfo))
            Try
                For Each oCategory As PostCategoryRelationInfo In oPostCategory

                    PostCategoryRelation(oCategory)


                Next
            Catch ex As Exception

            End Try


        End Sub

        Public Sub PostCategoryRelation(ByVal oCategory As PostCategoryRelationInfo)
            Try
                Dim oCategoryTemp As PostCategoryRelationInfo = GetPostCategoryRelation(oCategory.CategoryId, oCategory.ContentItemId)
                If oCategoryTemp Is Nothing Then
                    AddPostCategoryRelation(oCategory)
                Else
                    oCategory.PostCategoryId = oCategoryTemp.PostCategoryId
                    UpdatePostCategoryRelation(oCategory)
                End If
            Catch ex As Exception

            End Try


        End Sub

        Public Function GetCateogryByPostID(ByVal ContentPostID As Integer, IsonlyActive As Boolean) As IQueryable(Of PostCategoryRelationInfo)
            Try
                Dim t As IQueryable(Of PostCategoryRelationInfo)
                Using ctx As IDataContext = DataContext.Instance()
                    Dim rep As IRepository(Of PostCategoryRelationInfo) = ctx.GetRepository(Of PostCategoryRelationInfo)()
                    t = rep.Find("WHERE ContentItemId=@0", ContentPostID).Distinct().AsQueryable()
                End Using


                If Not t Is Nothing Then
                    If IsonlyActive Then
                        t = t.Where(Function(x) x.IsActive = True)
                    End If

                Else
                    Return Nothing
                End If
                Return t.Distinct()
            Catch ex As Exception

            End Try
            Return Nothing
        End Function
        Public Sub AddPostCategoryRelation(ByVal oPostCategory As PostCategoryRelationInfo)
            Try
                Using ctx As IDataContext = DataContext.Instance()
                    Dim rep As IRepository(Of PostCategoryRelationInfo) = ctx.GetRepository(Of PostCategoryRelationInfo)()
                    rep.Insert(oPostCategory)
                End Using
            Catch ex As Exception

            End Try
        End Sub
        Public Function GetPostCategoryRelation(ByVal oCategoryID As Integer, ByVal PostId As Integer) As PostCategoryRelationInfo
            Try
                Dim t As PostCategoryRelationInfo
                Using ctx As IDataContext = DataContext.Instance()
                    Dim rep As IRepository(Of PostCategoryRelationInfo) = ctx.GetRepository(Of PostCategoryRelationInfo)()
                    t = rep.Find("WHERE CategoryId=@0 AND ContentItemId=@1", oCategoryID, PostId).SingleOrDefault()
                End Using
                Return t
            Catch ex As Exception

            End Try
            Return Nothing
        End Function

        Public Sub UpdatePostCategoryRelation(ByVal oPostCategory As PostCategoryRelationInfo)
            Try
                Using ctx As IDataContext = DataContext.Instance()
                    Dim rep As IRepository(Of PostCategoryRelationInfo) = ctx.GetRepository(Of PostCategoryRelationInfo)()
                    rep.Update(oPostCategory)
                End Using
            Catch ex As Exception

            End Try
        End Sub
        Public Sub DeletePostCategoryRelation(ByVal oPostCategory As PostCategoryRelationInfo)
            Try


                Using ctx As IDataContext = DataContext.Instance()
                    Dim rep As IRepository(Of PostCategoryRelationInfo) = ctx.GetRepository(Of PostCategoryRelationInfo)()
                    rep.Delete(oPostCategory)
                End Using
            Catch ex As Exception

            End Try
        End Sub
#End Region

#Region "Global Category"
        Public Function GetGlobalCategory(CategoryID As Integer) As IQueryable(Of BlogGlobalCategory)
            Dim t As IQueryable(Of BlogGlobalCategory)
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of BlogGlobalCategory) = ctx.GetRepository(Of BlogGlobalCategory)()

                t = rep.Find("WHERE IsActive=@0 AND (ComponentId=@1 OR ComponentId=@2)", True, -1, CategoryID).AsQueryable()
                '   End If
            End Using
            Return t.OrderBy(Function(x) x.OptionOrder)
        End Function


        '  End Function
        Public Function GetGlobalCategoryByCategoryID(CategoryID As Integer) As BlogGlobalCategory
            Dim t As BlogGlobalCategory
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of BlogGlobalCategory) = ctx.GetRepository(Of BlogGlobalCategory)()

                t = rep.Find("WHERE IsActive=@0 and CategoryId=@1", True, CategoryID).SingleOrDefault()

            End Using
            Return t


        End Function
#End Region

#Region "Blog Post"

        Public Function GetBlogList(ByVal iCurrentBlogId As Integer, ByVal PageCount As Integer, ByVal PageSize As Integer, ByRef oTotalCount As Integer) As List(Of BlogPostInfo)
            Dim t As IQueryable(Of BlogPostInfo) = GetAllBlogPosts(False, -1, 0)
            If t Is Nothing Then
                Return Nothing
            End If
            ' t = t.OrderBy(Function(x) x.ContentItemId <> iCurrentBlogId)
            '    Dim skip As Integer = PageSize * PageCount
            oTotalCount = t.Count()
            Return t.Skip(PageSize * PageCount).Take(PageSize).ToList()
        End Function
        Public Function GetAllBlogPosts(IsAdmin As Boolean, CategoryId As Integer) As IQueryable(Of BlogPostInfo)
            '  Return data.Values.AsQueryable()
            Dim t As IQueryable(Of BlogPostInfo)
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of BlogPostInfo) = ctx.GetRepository(Of BlogPostInfo)()
                'removed filteration for admin
                If IsAdmin Then
                    t = rep.Get().AsQueryable()
                Else
                    t = rep.Find("Where Published=@0", True).AsQueryable()
                End If


            End Using

            If CategoryId > 0 Then

                Using ctx As IDataContext = DataContext.Instance()

                    Dim rep As IRepository(Of BlogPostInfo) = ctx.GetRepository(Of BlogPostInfo)()
                    t = ctx.ExecuteQuery(Of BlogPostInfo)(CommandType.StoredProcedure, "Blog_PublishCategoryByCategoryID", CategoryId).AsQueryable()


                End Using

            End If


            Return t

        End Function

        Public Function GetAllBlogPosts(IsAdmin As Boolean, CategoryId As Integer, ByVal SortingOrder As Integer) As IQueryable(Of BlogPostInfo)
            '  Return data.Values.AsQueryable()
            Dim t As IQueryable(Of BlogPostInfo) = GetAllBlogPosts(IsAdmin, CategoryId)

            Select Case SortingOrder
                Case 0
                    t = t.OrderByDescending(Function(x) x.CreatedOnDate)
                Case 1
                    t = t.OrderByDescending(Function(x) x.Title)
                Case 2
                    t = t.OrderByDescending(Function(x) x.LikesCount("Post"))
                Case 3
                    t = t.OrderBy(Function(x) x.Title).OrderByDescending(Function(x) x.CommentCount)
                Case 4
                    t = t.OrderByDescending(Function(x) x.CreatedOnDate)
                Case 5
                    t = t.Where(Function(x) x.CreatedOnDate >= System.DateTime.Now).OrderBy(Function(x) x.Title)
                Case 6
                    t = t.Where(Function(x) x.CreatedOnDate >= System.DateTime.Now.AddDays(-7)).OrderBy(Function(x) x.Title)
                Case 7
                    t = t.Where(Function(x) x.CreatedOnDate >= System.DateTime.Now.AddDays(30)).OrderBy(Function(x) x.Title)

            End Select

            Return t

        End Function

        Public Function GetAllBlogPost(IsAdmin As Boolean, HasSeen As Integer, CategoryId As Integer) As IQueryable(Of BlogPostInfo)
            '  Return data.Values.AsQueryable()
            Dim t As IQueryable(Of BlogPostInfo)
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of BlogPostInfo) = ctx.GetRepository(Of BlogPostInfo)()
                'removed filteration for admin
                If IsAdmin Then
                    t = rep.Get().AsQueryable()
                Else
                    t = rep.Find("Where Published=@0 and HasSeen=@1 and CategoryId=@2", True, HasSeen, CategoryId).AsQueryable()
                End If


            End Using

            If CategoryId > 0 Then

                Using ctx As IDataContext = DataContext.Instance()

                    Dim rep As IRepository(Of BlogPostInfo) = ctx.GetRepository(Of BlogPostInfo)()
                    t = ctx.ExecuteQuery(Of BlogPostInfo)(CommandType.StoredProcedure, "Blog_PublishCategoryByCategoryID", CategoryId).AsQueryable()

                End Using
            End If



            Return t

        End Function

        '        Public Function GetAllBlogPosts(Userid As Integer, HasSeen As Integer, IsAdmin As Boolean, CategoryId As Integer, ByVal SortingOrder As Integer) As IQueryable(Of BlogPostInfo)
        Public Function GetAllBlogPosts(IsAdmin As Boolean, Userid As Integer, CategoryID As Integer, Status As String, LoggedInUser As Integer, SearchKey As String, SortingOrder As Integer) As IQueryable(Of BlogPostInfo)
            '  Return data.Values.AsQueryable()
            Dim t As IQueryable(Of BlogPostInfo) ' = GetAllBlogPost(IsAdmin, Integer.Parse(IIf(Status = "To Do", 0, 1).ToString()), CategoryID)
            '  If Userid <> -1 Then
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of BlogPostInfo) = ctx.GetRepository(Of BlogPostInfo)()
                t = ctx.ExecuteQuery(Of BlogPostInfo)(CommandType.StoredProcedure, "Blog_BlogByUserAssignment", IIf(IsAdmin, -1, Userid), CategoryID, Status, LoggedInUser, SearchKey).AsQueryable()
            End Using
            'End If
            If CategoryID > 0 Then
                If (t IsNot Nothing) Then
                    t = t.Where(Function(x) x.GetBlogGlobalCategoryActiveOnly.Any(Function(y) y.CategoryId = CategoryID))
                    ' .Where(Function(x) x.GetBlogGlobalCategory.Where(Function(y) y.CategoryId = CategoryId))
                End If
            End If
            If (t IsNot Nothing) Then
                Select Case SortingOrder
                    Case -1
                        t = t.OrderByDescending(Function(x) x.CreatedOnDate)
                    Case 0
                        t = t.OrderBy(Function(x) x.Title)
                    Case 1
                        t = t.OrderByDescending(Function(x) x.Title)
                    Case 2
                        t = t.OrderByDescending(Function(x) x.LikesCount("Post"))
                    Case 3
                        t = t.OrderBy(Function(x) x.Title).OrderByDescending(Function(x) x.CommentCount)
                    Case 4
                        t = t.OrderByDescending(Function(x) x.CreatedOnDate)
                    Case 5
                        t = t.Where(Function(x) x.CreatedOnDate >= System.DateTime.Now).OrderBy(Function(x) x.Title)
                    Case 6
                        t = t.Where(Function(x) x.CreatedOnDate >= System.DateTime.Now.AddDays(-7)).OrderBy(Function(x) x.Title)
                    Case 7
                        t = t.Where(Function(x) x.CreatedOnDate >= System.DateTime.Now.AddDays(30)).OrderBy(Function(x) x.Title)

                End Select
            End If
            Return t

        End Function

        Private Function GetBlogs(CreatedByUserID As Integer) As IQueryable(Of PostContentInfo)
            '  Return data.Values.AsQueryable()
            Dim t As IQueryable(Of PostContentInfo)
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of PostContentInfo) = ctx.GetRepository(Of PostContentInfo)()

                t = rep.Find("Where CreatedByUserID=@0", CreatedByUserID).AsQueryable()

            End Using

            Return t


        End Function

        Public Function GetBlogById(UserID As Integer) As PostContentInfo
            '  Return data.Values.AsQueryable()
            Dim t As IQueryable(Of PostContentInfo) = GetBlogs(UserID)

            If t Is Nothing Then
                Return t.Where(Function(x) x.Published = True).SingleOrDefault()

            End If
            Return Nothing

        End Function

        Public Function GetBlogContentById(ContentID As Integer, UserID As Integer) As BlogPostInfo
            '  Return data.Values.AsQueryable()
            Dim t As IQueryable(Of BlogPostInfo)

            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of BlogPostInfo) = ctx.GetRepository(Of BlogPostInfo)()
                ' t = rep.Find("Where Published=@0 AND ContentItemId=@1", True, ContentID).AsQueryable()
                t = ctx.ExecuteQuery(Of BlogPostInfo)(CommandType.StoredProcedure, "Blog_BlogWithUserStatus", ContentID, UserID).AsQueryable()
            End Using

            If t IsNot Nothing Then
                Return t.SingleOrDefault()
            End If
            Return Nothing

        End Function

        Public Function GetBlogContentById(ByVal isAdmin As Boolean, ContentID As Integer) As BlogPostInfo
            '  Return data.Values.AsQueryable()
            Dim t As IQueryable(Of BlogPostInfo)

            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of BlogPostInfo) = ctx.GetRepository(Of BlogPostInfo)()
                If isAdmin = False Then
                    t = rep.Find("Where Published=@0 AND ContentItemId=@1", True, ContentID).AsQueryable()
                Else
                    t = rep.Find("Where ContentItemId=@0", ContentID).AsQueryable()
                End If


            End Using

            If t IsNot Nothing Then
                Return t.SingleOrDefault()
            End If
            Return Nothing

        End Function

        Public Sub UpdateBlogContent(oblog As BlogPostInfo)
            '  Return data.Values.AsQueryable()


            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of BlogPostInfo) = ctx.GetRepository(Of BlogPostInfo)()
                Try
                    rep.Update(oblog)
                Catch ex As Exception

                End Try
            End Using
        End Sub

        Public Sub UpdateBlogComponentManager(ByVal userid As Integer, ByVal iContentid As Integer)
            '  Return data.Values.AsQueryable()


            Using ctx As IDataContext = DataContext.Instance()
                '    Dim rep As IRepository(Of UserComponentsInfo) = ctx.GetRepository(Of UserComponentsInfo)()
                ctx.Execute(CommandType.StoredProcedure, "Blog_UpdateUserComponent", userid, iContentid)

            End Using





        End Sub

        Public Function GetBlogComponentSubject(ByVal iContentid As String) As String
            '  Return data.Values.AsQueryable()
            Dim t As String

            Using ctx As IDataContext = DataContext.Instance()
                '    Dim rep As IRepository(Of UserComponentsInfo) = ctx.GetRepository(Of UserComponentsInfo)()

                t = ctx.ExecuteSingleOrDefault(Of String)(CommandType.StoredProcedure, "Clearaction_GetBlogSubject", iContentid)

            End Using


            Return t


        End Function
        Public Function GetMail() As String
            '  Return data.Values.AsQueryable()
            Dim t As String

            Using ctx As IDataContext = DataContext.Instance()
                '    Dim rep As IRepository(Of UserComponentsInfo) = ctx.GetRepository(Of UserComponentsInfo)()

                t = ctx.ExecuteSingleOrDefault(Of String)(CommandType.StoredProcedure, "ClearAction_GetEmailSubscripion")

            End Using


            Return t


        End Function
        Private Function GetBlogPost(CreatedByUserID As Integer) As IQueryable(Of BlogPostDetailInfo)
            '  Return data.Values.AsQueryable()
            Dim t As IQueryable(Of BlogPostDetailInfo)
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of BlogPostDetailInfo) = ctx.GetRepository(Of BlogPostDetailInfo)()

                t = rep.Find("Where CreatedByUserID=@0", CreatedByUserID).AsQueryable()

            End Using

            Return t


        End Function

        Public Function GetBlogPostById(UserID As Integer) As BlogPostDetailInfo
            '  Return data.Values.AsQueryable()
            Dim t As IQueryable(Of BlogPostDetailInfo) = GetBlogPost(UserID)

            If t Is Nothing Then
                Return t.SingleOrDefault()

            End If
            Return Nothing

        End Function

        Public Function GetBlogPostByUser(IsAdmin As Boolean, ItemId As Integer) As IQueryable(Of BlogPostInfo)
            '  Return data.Values.AsQueryable()
            Dim t As IQueryable(Of BlogPostInfo)
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of BlogPostInfo) = ctx.GetRepository(Of BlogPostInfo)()
                'removed filteration for admin
                If IsAdmin Then
                    t = rep.Get().AsQueryable()
                Else
                    t = rep.Find("Where Published=@0", True).AsQueryable()
                End If


            End Using

            If ItemId <> -1 Then

                Using ctx As IDataContext = DataContext.Instance()

                    Dim rep As IRepository(Of BlogPostInfo) = ctx.GetRepository(Of BlogPostInfo)()
                    t = ctx.ExecuteQuery(Of BlogPostInfo)(CommandType.StoredProcedure, "Blog_GetPostByUser", ItemId).AsQueryable()


                End Using

            End If
            Return Nothing
        End Function

        Public Function GetBlogPostByItemID(ItemID As Integer) As IQueryable(Of BlogPostInfo)
            '  Return data.Values.AsQueryable()
            Dim t As IQueryable(Of BlogPostInfo)
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of BlogPostInfo) = ctx.GetRepository(Of BlogPostInfo)()
                'removed filteration for admin

                t = rep.Find("Where ContentItemId=@0", ItemID).AsQueryable()



            End Using
            Return t

        End Function

        Public Function GetUserStatus(iContentId As Integer, UserID As Integer) As UserComponentsInfo
            Dim t As IQueryable(Of UserComponentsInfo)
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of UserComponentsInfo) = ctx.GetRepository(Of UserComponentsInfo)()

                t = rep.Find("Where ItemID=@0 AND UserID=@1 AND ComponentID=2", iContentId, UserID).AsQueryable()

            End Using

            Try
                Return t.SingleOrDefault()
            Catch ex As Exception

            End Try
            Return Nothing

        End Function

        ''' <summary>
        ''' return the list of all the tags as parent/children , pass -1 for root level class
        ''' </summary>
        ''' <param name="TagID"></param>
        ''' <returns></returns>
        Public Function GetTagList(ByVal TagID As Integer) As IQueryable(Of CA_TaxonomyTerms)
            Dim t As IQueryable(Of CA_TaxonomyTerms)
            Using ctx As IDataContext = DataContext.Instance()
                t = ctx.ExecuteQuery(Of CA_TaxonomyTerms)(System.Data.CommandType.StoredProcedure, "CA_GetTags", TagID).AsQueryable()
            End Using

            Return t.OrderBy(Function(x) x.Name)
        End Function

        Public Function GetTagsByContentItemID(ByVal ContentItemID As Integer) As IQueryable(Of CA_TaxonomyTerms)
            Dim t As IQueryable(Of CA_TaxonomyTerms)
            Using ctx As IDataContext = DataContext.Instance()
                t = ctx.ExecuteQuery(Of CA_TaxonomyTerms)(System.Data.CommandType.StoredProcedure, "CA_GetTagsByContentItemID", ContentItemID).AsQueryable()
            End Using
            Return t
        End Function

        Public Sub ClearTagsByContentItemID(ByVal ItemID As Integer)
            Using ctx As IDataContext = DataContext.Instance()
                ctx.Execute(System.Data.CommandType.StoredProcedure, "CA_ClearTagsByContentItemID", ItemID)
            End Using
        End Sub

        Public Sub UpdateTagByContentItemID(ByVal ItemID As Integer, ByVal TagID As Integer)
            Using ctx As IDataContext = DataContext.Instance()
                ctx.Execute(System.Data.CommandType.StoredProcedure, "CA_UpdateTagsByContentItemID", ItemID, TagID)
            End Using
        End Sub
        Public Function SearchForTags(ByVal StrTagName As String) As IQueryable(Of CA_TaxonomyTerms)
            '   Dim t As IQueryable(Of Tags)
            Try
                Using ctx As IDataContext = DataContext.Instance()
                    'Dim rep As New ctx.GetRepository(Of Tags)
                    Return ctx.ExecuteQuery(Of CA_TaxonomyTerms)(CommandType.StoredProcedure, "CA_GetTagByName", StrTagName).AsQueryable()
                End Using
            Catch exc As Exception
                Return Nothing
            End Try
        End Function
        Public Sub SavePostTags(ItemId As Integer, ByVal lstTags As List(Of Tags))
            Dim oCtrl As New BlogContentController
            oCtrl.ClearTagsByContentItemID(ItemId)
            For Each oTag As Tags In lstTags
                oCtrl.UpdateTagByContentItemID(ItemId, oTag.TagId)
            Next
        End Sub
        Private Function UpdateTag(ByVal o As CA_TaxonomyTerms) As CA_TaxonomyTerms
            Try
                Using ctx As IDataContext = DataContext.Instance()
                    Dim rep As IRepository(Of CA_TaxonomyTerms) = ctx.GetRepository(Of CA_TaxonomyTerms)()
                    rep.Update(o)
                End Using
            Catch ex As Exception
            End Try

            Return o
        End Function
#End Region


#Region "Likes"
        Public Function GetForPostCount(postId As Integer, ItemType As String) As String
            Dim t As List(Of Likes) = GetForPost(postId, ItemType)
            If t Is Nothing Then
                Return "0"
            End If
            Return t.Count.ToString()

        End Function
        Public Function GetForPost(postId As Integer, ItemType As String) As List(Of Likes)
            Dim t As IQueryable(Of Likes)
            Using ctx As IDataContext = DataContext.Instance()


                Dim rep As IRepository(Of Likes) = ctx.GetRepository(Of Likes)()
                t = rep.Find("WHERE PostId = @0 AND Checked = 1 and ItemType=@1", postId, ItemType).AsQueryable()
            End Using
            Return t.ToList()
        End Function
        Public Function GetUnlikeForPost(postId As Integer, ItemType As String) As List(Of Likes)
            Dim t As IQueryable(Of Likes)
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of Likes) = ctx.GetRepository(Of Likes)()
                t = rep.Find("WHERE PostId = @0 AND Checked = 0 and ItemType=@1", postId, ItemType).AsQueryable()
            End Using
            Return t.ToList()
        End Function

        Public Function LikeIsLikeByUser(ByVal contentId As Integer, ByVal UserID As Integer) As Boolean
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of Likes) = ctx.GetRepository(Of Likes)()
                Dim olike As Likes = rep.Find("Where PostId = @0 AND UserId = @1", contentId, UserID).FirstOrDefault()
                If olike IsNot Nothing Then
                    Return True
                End If
            End Using
            Return False
        End Function
        Public Function Post(contentId As Integer, userId As Integer, ItemType As String) As String
            '  Dim t As IQueryable(Of )
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of Likes) = ctx.GetRepository(Of Likes)()
                Dim olike As Likes = rep.Find("Where PostId = @0  AND UserId = @1 and ItemType=@2", contentId, userId, ItemType).FirstOrDefault()

                If olike IsNot Nothing Then
                    olike.ItemType = ItemType
                    If olike.Checked Then
                        olike.Checked = False
                        rep.Delete(olike)
                    Else
                        olike.Checked = True
                        rep.Update(olike)
                    End If

                    Return "RemovedLike"
                Else
                    olike = New Likes()
                    olike.PostId = contentId
                    olike.UserId = userId
                    olike.Checked = True
                    olike.ItemType = ItemType
                    rep.Insert(olike)
                End If
                Return "AddedLike"
            End Using
        End Function

        Public Sub DeleteLikes(contentId As Integer, userId As Integer, ItemType As String)
            '  Dim t As IQueryable(Of )
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of Likes) = ctx.GetRepository(Of Likes)()
                Dim olike As Likes = rep.Find("Where PostId = @0 AND UserId = @1 and ItemType=@2", contentId, userId, ItemType).FirstOrDefault()

                If olike IsNot Nothing Then

                    rep.Delete(olike)
                End If
            End Using
        End Sub








        Public Sub DeleteCommentAttachment(contentId As Integer)

            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of PostCommentInfo) = ctx.GetRepository(Of PostCommentInfo)()
                Dim ObjBlogComment As PostCommentInfo = rep.Find("Where CommentID=@0", contentId).FirstOrDefault()

                If ObjBlogComment IsNot Nothing Then

                    ObjBlogComment.attachUrl = ""
                    ObjBlogComment.AttachName = ""

                    rep.Update(ObjBlogComment)

                End If

            End Using

        End Sub
















        Public Function UnLikePost(contentId As Integer, userId As Integer) As String
            '  Dim t As IQueryable(Of )
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of Likes) = ctx.GetRepository(Of Likes)()
                Dim olike As Likes = rep.Find("Where PostId = @0 and UserId = @1 and ItemType='Post'", contentId, userId).FirstOrDefault()

                If olike Is Nothing Then
                    olike = New Likes()
                    olike.PostId = contentId
                    olike.UserId = userId
                    olike.Checked = False
                    olike.ItemType = "Post"
                    rep.Insert(olike)
                Else
                    ' olike = New Likes()
                    olike.PostId = contentId
                    olike.UserId = userId
                    olike.Checked = False
                    olike.ItemType = "Post"
                    rep.Update(olike)
                End If
                Return "AddedLike"
            End Using
        End Function

        Public Function GetSearchResult(ModuleId As Integer, DisplayLocale As String, Userid As Integer, UserIsAdmin As Boolean, SearchText As String) As List(Of BlogPostInfo)

            Dim t As IQueryable(Of BlogPostInfo)
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of BlogPostInfo) = ctx.GetRepository(Of BlogPostInfo)()

                t = ctx.ExecuteQuery(Of BlogPostInfo)(CommandType.StoredProcedure, "Blog_SearchPosts", ModuleId, 1, DisplayLocale, Userid, UserIsAdmin, SearchText, True, True, True, "en-US", System.DateTime.Now, -1, -1, 0, "LASTMODIFIEDONDATE DESC").AsQueryable()

            End Using
            If Not t Is Nothing Then
                Return t.ToList()
            End If
            Return Nothing
        End Function
#End Region

#Region "Blog comments"
        Public Function GetComments(CommentID As Integer, userId As Integer) As PostCommentInfo

            Dim t As IQueryable(Of PostCommentInfo)

            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of PostCommentInfo) = ctx.GetRepository(Of PostCommentInfo)()
                t = rep.Find("Where CommentID=@0 AND ContentItemId=@1 ", CommentID, userId).AsQueryable()

            End Using

            If t IsNot Nothing Then

                Return t.SingleOrDefault()

            End If
            Return Nothing

        End Function

        Public Function UpdateComment(ByVal oPostCommentInfo As PostCommentInfo) As PostCommentInfo

            Dim t As IQueryable(Of PostCommentInfo)

            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of PostCommentInfo) = ctx.GetRepository(Of PostCommentInfo)()
                rep.Update(oPostCommentInfo)

            End Using

            Return oPostCommentInfo

        End Function
        Public Function GetCommentByContentID(iIsAdmin As Boolean, ContentItemId As Integer) As List(Of PostCommentInfo)

            Dim t As IQueryable(Of PostCommentInfo)

            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of PostCommentInfo) = ctx.GetRepository(Of PostCommentInfo)()
                If iIsAdmin Then
                    t = rep.Find("Where  ContentItemId=@0", ContentItemId).AsQueryable()
                Else
                    t = rep.Find("Where  ContentItemId=@0 AND Approved=@1", ContentItemId, True).AsQueryable()
                End If


            End Using

            If t IsNot Nothing Then
                t = t.OrderByDescending(Function(x) x.CreatedOnDate)

                Return t.ToList()
            End If
            Return Nothing


        End Function
        Public Function GetCommentByContentID(iIsAdmin As Boolean, ContentItemId As Integer, ParentId As Integer) As List(Of PostCommentInfo)

            Dim t As IQueryable(Of PostCommentInfo)

            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of PostCommentInfo) = ctx.GetRepository(Of PostCommentInfo)()
                If iIsAdmin Then
                    t = rep.Find("Where  ContentItemId=@0", ContentItemId).AsQueryable()
                Else
                    t = rep.Find("Where  ContentItemId=@0 AND Approved=@1", ContentItemId, True).AsQueryable()
                End If


            End Using

            If t IsNot Nothing Then
                If ParentId = -1 Then
                    t = t.Where(Function(x) x.ParentId = -1)
                Else
                    t = t.Where(Function(x) x.ParentId = ParentId)
                End If
                t = t.OrderByDescending(Function(x) x.CreatedOnDate)

                Return t.ToList()
            End If
            Return Nothing


        End Function
        Public Function GetCommentByCommentID(ByVal iCommentID As Integer) As PostCommentInfo
            Try
                Using ctx As IDataContext = DataContext.Instance()


                    Dim rep As IRepository(Of PostCommentInfo) = ctx.GetRepository(Of PostCommentInfo)()
                    Return rep.GetById(iCommentID)
                End Using
            Catch ex As Exception

            End Try
            Return Nothing
        End Function
        Public Function GetCommentsbyContentId(iContentId As Integer) As IQueryable(Of PostCommentInfo)
            Dim t As IQueryable(Of PostCommentInfo)
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of PostCommentInfo) = ctx.GetRepository(Of PostCommentInfo)()

                t = rep.Find("Where Approved=@0 AND ContentItemId=@1", True, iContentId).AsQueryable()

            End Using

            Return t.OrderByDescending(Function(x) x.LastModifiedOnDate)


        End Function

        Public Function GetCommentsbyContentID_Count(iContentId As Integer) As Int32
            Dim t As IQueryable(Of PostCommentInfo) = GetCommentsbyContentId(iContentId)
            If t Is Nothing Then
                Return 0
            End If

            Return t.Count


        End Function

        Public Function GetProfileResponseByQuestion(ByVal iQuestionID As Integer, ByVal iUserID As Integer) As String

            'String QuestionID = PortalController.GetPortalSetting(Key, Portalid, String.Empty)

            Dim t As IQueryable(Of DTOResponse)
            Using ctx As IDataContext = DataContext.Instance()

                t = ctx.ExecuteQuery(Of DTOResponse)(CommandType.StoredProcedure, "CA_GetQuestionResponseByUserID", iUserID, iQuestionID).AsQueryable() '.SingleOrDefault()
            End Using
            If Not t Is Nothing Then
                If t.SingleOrDefault() Is Nothing Then
                    Return ""
                End If
                Return t.SingleOrDefault().ResponseText
            End If
            Return ""

        End Function

        Public Function GetCommentAuthor() As IQueryable(Of PostCommentInfo)
            Dim t As IQueryable(Of PostCommentInfo)
            Using ctx As IDataContext = DataContext.Instance()
                Dim rep As IRepository(Of PostCommentInfo) = ctx.GetRepository(Of PostCommentInfo)()

                t = rep.Find("Where Approved=@0", True).AsQueryable()

            End Using

            Return t.Where(Function(x) x.CreatedOnDate >= System.DateTime.Now.AddDays(-14)).OrderByDescending(Function(x) x.LastModifiedOnDate)
        End Function


        Public Sub AddAttachement(ByVal oData As Blog_Attachments)
            Try
                Using ctx As IDataContext = DataContext.Instance()


                    Dim rep As IRepository(Of Blog_Attachments) = ctx.GetRepository(Of Blog_Attachments)()
                    rep.Insert(oData)
                End Using
            Catch ex As Exception

            End Try

        End Sub
        Public Sub DeleteFromURL(ByVal FileStackUrl As String)
            Try
                Dim ctx As IDataContext = DataContext.Instance
                ctx.Execute(System.Data.CommandType.Text, "DELETE FROM Blog_Attachments Where FileUrl=@0", FileStackUrl)
            Catch ex As Exception

            End Try

        End Sub
        Public Sub UpdateAttachement(ByVal oData As Blog_Attachments)
            Try
                Using ctx As IDataContext = DataContext.Instance()


                    Dim rep As IRepository(Of Blog_Attachments) = ctx.GetRepository(Of Blog_Attachments)()
                    rep.Update(oData)
                End Using
            Catch ex As Exception

            End Try

        End Sub

        Public Function GetAttachementByCommentID(ByVal iCommentID As Integer) As List(Of Blog_Attachments)
            Try
                Using ctx As IDataContext = DataContext.Instance()


                    Dim rep As IRepository(Of Blog_Attachments) = ctx.GetRepository(Of Blog_Attachments)()
                    Return rep.Find("Where CommentID=@0", iCommentID).ToList()
                End Using
            Catch ex As Exception

            End Try
            Return Nothing
        End Function

        Public Function GetAttachmentByPostID(ByVal BlogPostID As Integer) As List(Of Blog_Attachments)
            Try
                Using ctx As IDataContext = DataContext.Instance()


                    Dim rep As IRepository(Of Blog_Attachments) = ctx.GetRepository(Of Blog_Attachments)()
                    Return rep.Find("Where ContentItemID=@0", BlogPostID).ToList()
                End Using
            Catch ex As Exception

            End Try
            Return Nothing
        End Function
        Public Function GetAttachmentDetails(ByVal AttachID As Integer) As Blog_Attachments
            Try
                Using ctx As IDataContext = DataContext.Instance()


                    Dim rep As IRepository(Of Blog_Attachments) = ctx.GetRepository(Of Blog_Attachments)()
                    Return rep.GetById(Of Integer)(AttachID)
                End Using
            Catch ex As Exception

            End Try
            Return Nothing
        End Function



        Public Function RecentMemberByBlogContentID(ByVal wccid As Integer) As List(Of UserData)
            Using idx As IDataContext = DataContext.Instance()
                Return idx.ExecuteQuery(Of UserData)(System.Data.CommandType.StoredProcedure, "Blog_GetActiveTopicMemberByComment", wccid).ToList()
            End Using
        End Function

#End Region



#Region "File Stack Storage API"
        Public Sub AddToFilestack(ByVal oData As FileStackRefernceInfo)
            Try
                Using ctx As IDataContext = DataContext.Instance()


                    Dim rep As IRepository(Of FileStackRefernceInfo) = ctx.GetRepository(Of FileStackRefernceInfo)()
                    rep.Insert(oData)
                End Using
            Catch ex As Exception

            End Try

        End Sub

        Public Sub UpdateToFilestack(ByVal oData As FileStackRefernceInfo)
            Try
                Using ctx As IDataContext = DataContext.Instance()


                    Dim rep As IRepository(Of FileStackRefernceInfo) = ctx.GetRepository(Of FileStackRefernceInfo)()
                    rep.Update(oData)
                End Using
            Catch ex As Exception

            End Try

        End Sub

        Public Function GetFilestackByContentItemID(ByVal ContentItemID As Integer) As List(Of FileStackRefernceInfo)
            Try
                Using ctx As IDataContext = DataContext.Instance()


                    Dim rep As IRepository(Of FileStackRefernceInfo) = ctx.GetRepository(Of FileStackRefernceInfo)()
                    Return rep.Find("Where ContentItemID=@0", ContentItemID).ToList()
                End Using
            Catch ex As Exception

            End Try
            Return Nothing
        End Function


        Public Function DeleteFileStackReference(ByVal filestackurl As String, ByVal CurrentUserid As Integer) As Int32
            Try
                Using ctx As IDataContext = DataContext.Instance()

                    ctx.ExecuteQuery(Of Int16)(CommandType.StoredProcedure, "Blog_DeleteFileStackRefernce", filestackurl, CurrentUserid)

                End Using
            Catch ex As Exception

            End Try
            Return -1
        End Function
#End Region

        <TableName("ContentItems_Tags")>
        <PrimaryKey("TermID")>
        Public Class CA_TaxonomyTerms

            Public Property TermID As Integer

            Public Property Name As String

            Public Property ParentTermID As Integer
            <ReadOnlyColumn>
            Public Property VocabularyName As String
        End Class


        Public Class Tags

            Public Property TagId As Integer

            Public Property Portalid As Integer

            Public Property ModuleId As Integer

            Public Property TagName As String
            Public Property Name As String
            Public Property Clicks As Integer

            Public Property Items As Integer

            Public Property Priority As Integer

            Public Property ContentItemID As Integer
            Public Property TermID As Integer
        End Class

    End Class
End Namespace

