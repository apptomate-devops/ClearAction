using DotNetNuke.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DotNetNuke.Modules.ActiveForums.DAL2.Attachment;

namespace DotNetNuke.Modules.ActiveForums.DAL2
{

    public class Dal2Controller
    {



        IDataContext ctx;
        IRepository<Topics> repo;

        #region "Topics"

        public Topics GetTopics(int contentId)
        {
            var content = repo.GetById(contentId);
            return content;
        }

        private IQueryable<Topics> GetAllTopics(bool IsAdmin)
        {
            IQueryable<Topics> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Topics>();
                if (IsAdmin)
                    t = rep.Get().AsQueryable();
                else
                    t = rep.Find("WHERE IsLocked=@0 AND IsApproved=@1 AND IsRejected=@2 AND IsDeleted=@3 AND IsArchived=@4", false, true, false, false, false).AsQueryable().Where(x => x.AnnounceEnd < System.DateTime.Now);
            }
            return t;


        }
        public IQueryable<Topics> GetAllTopics(bool IsAdmin, int CategoryId, int SortBy, out int TotalRecord)
        {
            IQueryable<Topics> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Topics>();
                if (CategoryId == -1)
                    t = GetAllTopics(IsAdmin);
                else
                    t = GetAllTopics(IsAdmin).Where(x => x.CategoryId == CategoryId);
            }
            TotalRecord = t.Count();
            switch (SortBy)
            {
                case 0:
                case -1:
                    t = t.OrderByDescending(x => x.GetContent.DateCreated);
                    break;
                case 1:
                    t = t.OrderBy(x => x.GetContent.Subject);
                    break;
                case 2:
                    t = t.OrderByDescending(x => x.GetContent.Subject);
                    break;
                case 3:
                    t = t.OrderByDescending(x => x.GetContent.ContentLikes);
                    break;

                case 4:
                    t = t.OrderByDescending(x => x.ReplyCount); break;
                case 5:
                    t = t.OrderBy(x => x.GetContent.DateCreated); break;
                case 6:
                    t = t.Where(x => x.GetContent.DateCreated.Date == System.DateTime.Now.Date);

                    break;
                case 7:
                    t = t.Where(x => x.GetContent.DateCreated.Date > System.DateTime.Now.AddDays(-7).Date).Where(x => x.GetContent.DateCreated.Date <= System.DateTime.Now.Date);
                    break;
                case 8:
                    t = t.Where(x => x.GetContent.DateCreated.Date > System.DateTime.Now.AddDays(-30).Date).Where(x => x.GetContent.DateCreated.Date <= System.DateTime.Now.Date);
                    break;
            }
            return t;


        }
        public IQueryable<Topics> GetAllTopics(bool IsAdmin, int CategoryId, out int TotalRecord)
        {
            return GetAllTopics(IsAdmin, CategoryId, 0, out TotalRecord);

        }

        public IQueryable<Topics> GetAllForumTopics(bool IsAdmin, int CategoryId, int SortingOrder)
        {
            //  Return data.Values.AsQueryable()
            IQueryable<Topics> t = GetAllForumTopics(IsAdmin, CategoryId);

            switch (SortingOrder)
            {
                case 0:
                case -1:
                    t = t.OrderByDescending(x => x.GetContent.DateCreated);
                    break;
                case 1:
                    t = t.OrderBy(x => x.GetContent.Subject);
                    break;
                case 2:
                    t = t.OrderByDescending(x => x.GetContent.Subject);
                    break;
                case 3:
                    t = t.OrderByDescending(x => x.GetContent.ContentLikes);
                    break;

                case 4:
                    t = t.OrderByDescending(x => x.ReplyCount); break;
                case 5:
                    t = t.OrderBy(x => x.GetContent.DateCreated); break;
                case 6:
                    t = t.Where(x => x.GetContent.DateCreated.Date == System.DateTime.Now.Date);

                    break;
                case 7:
                    t = t.Where(x => x.GetContent.DateCreated.Date > System.DateTime.Now.AddDays(-7).Date).Where(x => x.GetContent.DateCreated.Date <= System.DateTime.Now.Date);
                    break;
                case 8:
                    t = t.Where(x => x.GetContent.DateCreated.Date > System.DateTime.Now.AddDays(-30).Date).Where(x => x.GetContent.DateCreated.Date <= System.DateTime.Now.Date);
                    break;
            }
            return t;

        }
        public IQueryable<Topics> GetAllForumTopics(bool IsAdmin, int TopicID, int CategoryId, int SortingOrder)
        {
            //  Return data.Values.AsQueryable()

            IQueryable<Topics> t = GetAllForumTopics(IsAdmin, CategoryId, SortingOrder);
            if (TopicID > 0)
                t = t.Where(x => x.TopicId == TopicID);

            return t;

        }
        public IQueryable<Topics> GetAllForumTopics(bool IsAdmin, int CategoryId)
        {
            //  Return data.Values.AsQueryable()
            IQueryable<Topics> t = default(IQueryable<Topics>);
            using (IDataContext ctx = DataContext.Instance())
            {
                IRepository<Topics> rep = ctx.GetRepository<Topics>();
                if (IsAdmin)
                {
                    t = rep.Get().AsQueryable();
                }
                else
                {
                    t = rep.Find("WHERE IsLocked=@0 AND IsApproved=@1 AND IsRejected=@2 AND IsDeleted=@3 AND IsArchived=@4", false, true, false, false, false).AsQueryable().Where(x => x.AnnounceEnd < System.DateTime.Now);

                }


            }


            if (CategoryId != -1)
            {
                using (IDataContext ctx = DataContext.Instance())
                {

                    IRepository<Topics> rep = ctx.GetRepository<Topics>();
                    t = ctx.ExecuteQuery<Topics>(CommandType.StoredProcedure, "Forum_PublishCategoryByCategoryID", CategoryId).AsQueryable();


                }

            }


            return t;

        }


        public IQueryable<CA_TopicsEx> GetAllForumTopics(int UserId, int CategoryId, string Status, int LoggedInUser, string SearchKey, int SortingOrder)
        {
            IQueryable<CA_TopicsEx> t = default(IQueryable<CA_TopicsEx>);
            using (IDataContext ctx = DataContext.Instance())
            {
                t = ctx.ExecuteQuery<CA_TopicsEx>(CommandType.StoredProcedure, "CA_Forum_GetUserForumsAll", UserId, CategoryId, Status, LoggedInUser, SearchKey).AsQueryable();
            }

            switch (SortingOrder)
            {
                case 0:
                case -1:
                    t = t.OrderByDescending(x => x.GetContent.DateCreated);
                    break;
                case 1:
                    t = t.OrderBy(x => x.GetContent.Subject);
                    break;
                case 2:
                    t = t.OrderByDescending(x => x.GetContent.Subject);
                    break;
                case 3:
                    t = t.OrderByDescending(x => x.GetContent.ContentLikes);
                    break;

                case 4:
                    t = t.OrderByDescending(x => x.ReplyCount); break;
                case 5:
                    t = t.OrderByDescending(x => x.GetContent.DateCreated); break;
                case 6:
                    t = t.Where(x => x.GetContent.DateCreated.Date == System.DateTime.Now.Date);

                    break;
                case 7:
                    t = t.Where(x => x.GetContent.DateCreated.Date > System.DateTime.Now.AddDays(-7).Date).Where(x => x.GetContent.DateCreated.Date <= System.DateTime.Now.Date);
                    break;
                case 8:
                    t = t.Where(x => x.GetContent.DateCreated.Date > System.DateTime.Now.AddDays(-30).Date).Where(x => x.GetContent.DateCreated.Date <= System.DateTime.Now.Date);
                    break;
            }
            return t;
        }

        #endregion

        #region "Topics - Category"

        public GlobalCategory GetGlobalCategory(bool IsAdmin, int CategoryID)
        {
            //IQueryable<GlobalCategory> t = GetGlobalCategory(IsAdmin);

            return GetGlobalCategoryID(CategoryID);


        }

        public IQueryable<GlobalCategory> GetGlobalCategory(bool IsAdmin)
        {
            IQueryable<GlobalCategory> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<GlobalCategory>();
                t = rep.Find("WHERE IsActive=@0", true).AsQueryable();

            }
            return t.OrderBy(x => x.OptionOrder);


        }

        public IQueryable<GlobalCategory> GetGlobalCategory(int ComponentID)
        {
            IQueryable<GlobalCategory> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<GlobalCategory>();

                t = rep.Find("WHERE IsActive=@0 AND (ComponentId=@1 OR ComponentId=-1)", true, ComponentID).AsQueryable();

            }
            return t.OrderBy(x => x.OptionOrder);


        }
        public IQueryable<GlobalCategory> GetGlobalCategory(int ComponentID, bool IsPublic)
        {
            IQueryable<GlobalCategory> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                if (IsPublic)
                    return GetGlobalCategory(ComponentID);

                var rep = ctx.GetRepository<GlobalCategory>();

                t = rep.Find("WHERE IsActive=@0 AND (ComponentId=@1)", true, ComponentID).AsQueryable();

            }
            return t.OrderBy(x => x.OptionOrder);


        }
        /// <summary>
        /// GetSingle Active Global Category
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public GlobalCategory GetGlobalCategoryID(int CategoryID)
        {
            try
            {
                GlobalCategory t = null;
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<GlobalCategory>();

                    t = rep.Find("WHERE IsActive=@0 AND CategoryId=@1", true, CategoryID).SingleOrDefault();

                }
                return t;

            }
            catch (Exception ex)
            {


            }
            return null;

        }


        /// <summary>
        /// Component is not longer usable on Global: 
        /// </summary>
        /// <param name="oCategory"></param>
        //public IQueryable<GlobalCategory> GetGlobalCategory(int ComponentID)
        //{
        //    IQueryable<GlobalCategory> t;
        //    using (IDataContext ctx = DataContext.Instance())
        //    {
        //        var rep = ctx.GetRepository<GlobalCategory>();
        //        if (ComponentID == -1)
        //            t = rep.Find("WHERE IsActive=@0 ", true).AsQueryable();
        //        else
        //            t = rep.Find("WHERE IsActive=@0 AND ComponentID=@1", true, ComponentID).AsQueryable();
        //    }
        //    return t;


        //}

        public void TopicCategoryRelation(TopicCategoryRelationInfo oCategory)
        {
            try
            {
                TopicCategoryRelationInfo oCategoryTemp = GetTopicCategoryRelation(oCategory.CategoryId, oCategory.TopicId);
                if (oCategoryTemp == null)
                {
                    AddTopicCategoryRelation(oCategory);
                }
                else
                {
                    oCategory.TopicCategoryId = oCategoryTemp.TopicCategoryId;
                    UpdateTopicCategoryRelation(oCategory);
                }

            }
            catch (Exception ex)
            {
            }


        }


        public TopicCategoryRelationInfo GetTopicCategoryRelation(int oCategoryID, int PostId)
        {
            try
            {
                TopicCategoryRelationInfo t = default(TopicCategoryRelationInfo);
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<TopicCategoryRelationInfo> rep = ctx.GetRepository<TopicCategoryRelationInfo>();
                    t = rep.Find("WHERE CategoryId=@0 AND TopicId=@1", oCategoryID, PostId).SingleOrDefault();
                }
                return t;

            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public IQueryable<TopicCategoryRelationInfo> GetTopicCategoryRelation(int TopicId)
        {
            try
            {
                IQueryable<TopicCategoryRelationInfo> t;
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<TopicCategoryRelationInfo> rep = ctx.GetRepository<TopicCategoryRelationInfo>();
                    t = rep.Find("Where TopicId=@0", TopicId).AsQueryable();
                }
                return t;
            }
            catch (Exception ex)
            {
            }
            return null;
        }





        public List<TopicCategoryRelationInfo> GetTopicCategoryRelationOnlyActive(int topicid, bool IsActive)
        {
            try
            {
                List<TopicCategoryRelationInfo> t = new List<TopicCategoryRelationInfo>();
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<TopicCategoryRelationInfo> rep = ctx.GetRepository<TopicCategoryRelationInfo>();
                    t = rep.Find("WHERE TopicId=@0 and IsActive=@1", topicid, true).ToList();

                }
                return t;

            }
            catch (Exception ex)
            {
            }
            return null;

        }


        public List<TopicCategoryRelationInfo> GetTopicCategoryRelation(int topicid, bool IsActive)
        {
            try
            {
                List<TopicCategoryRelationInfo> t = new List<TopicCategoryRelationInfo>();
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<TopicCategoryRelationInfo> rep = ctx.GetRepository<TopicCategoryRelationInfo>();
                    t = rep.Find("WHERE TopicId=@0 and IsActive=@1 ", topicid, true).ToList();

                }
                return t;

            }
            catch (Exception ex)
            {
            }
            return null;

        }
        public List<TopicCategoryRelationInfo> GetTopicCategoryRelation(int topicid, bool IsActive, bool IsGlobalCategory)
        {
            try
            {
                List<TopicCategoryRelationInfo> t = new List<TopicCategoryRelationInfo>();
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<TopicCategoryRelationInfo> rep = ctx.GetRepository<TopicCategoryRelationInfo>();
                    t = rep.Find("WHERE TopicId=@0 and IsActive=@1 AND IsGlobalCategory=@2 ", topicid, true, IsGlobalCategory).ToList();

                }
                return t;

            }
            catch (Exception ex)
            {
            }
            return null;

        }

        public void AddTopicCategoryRelation(TopicCategoryRelationInfo oPostCategory)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<TopicCategoryRelationInfo> rep = ctx.GetRepository<TopicCategoryRelationInfo>();
                    rep.Insert(oPostCategory);
                }

            }
            catch (Exception ex)
            {
            }
        }

        public void UpdateTopicCategoryRelation(TopicCategoryRelationInfo oPostCategory)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<TopicCategoryRelationInfo> rep = ctx.GetRepository<TopicCategoryRelationInfo>();
                    rep.Update(oPostCategory);
                }

            }
            catch (Exception ex)
            {
            }
        }




        #endregion

        #region "Replies"

        public IQueryable<Replies> GetTopicReply(bool IsAdmin, int TopicID)
        {
            IQueryable<Replies> t = GetTopicFromQueryReply(TopicID);
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Replies>();

            }

            if (IsAdmin)
                return t.Where(x => x.ReplyToId == TopicID).Where(x => x.IsRejected == false).Where(x => x.IsDeleted == false).Where(x => x.IsApproved == true).OrderByDescending(x => x.DateCreated);
            else
                return t.Where(x => x.ReplyToId == TopicID).Where(x => x.IsRejected == false).Where(x => x.IsDeleted == false).Where(x => x.IsApproved == true).AsQueryable().OrderByDescending(x => x.DateCreated);


        }
        public int GetTopicReplyCount(int TopicID)
        {
            IQueryable<Replies> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Replies>();
                t = ctx.ExecuteQuery<Replies>(System.Data.CommandType.StoredProcedure, "ClearAction_GetReplyByTopic", TopicID).AsQueryable();
            }
            return t.Count();
        }

        public IQueryable<Replies> GetDisucssionReply(bool IsAdmin, int TopicID, int ReplyTo)
        {
            IQueryable<Replies> t = GetTopicFromQueryReply(TopicID);
            //using (IDataContext ctx = DataContext.Instance())
            //{
            //    var rep = ctx.GetRepository<Replies>();

            //}
            if (IsAdmin)
                return t.Where(x => x.TopicId == TopicID).Where(x => x.ReplyToId == ReplyTo).OrderByDescending(x => x.DateCreated);
            else
                return t.Where(x => x.TopicId == TopicID).Where(x => x.ReplyToId == ReplyTo).Where(x => x.IsDeleted == false).Where(x => x.IsApproved == true).AsQueryable().OrderByDescending(x => x.DateCreated);


        }

        public int GetDiscussionCount(int ReplyTo)
        {

            IQueryable<Replies> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Replies>();

                t = rep.Find("WHERE ReplyToId=@0", ReplyTo).AsQueryable();
            }

            if (t != null)
                return t.Count();
            return 0;

        }

        private IQueryable<Replies> GetTopicFromQueryReply(int TopicID)
        {
            IQueryable<Replies> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Replies>();
                t = ctx.ExecuteQuery<Replies>(System.Data.CommandType.StoredProcedure, "ClearAction_GetReplyByTopic", TopicID).AsQueryable();
                //    t =rep.Find("ClearAction_GetReplyByTopic", TopicID).AsQueryable();
            }
            return t;


        }



        public void ReplyDelete(int ForumID, int TopicId, int ReplyID, int DeleteBehaviour)
        {

            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Replies>();
                ctx.Execute(System.Data.CommandType.StoredProcedure, "activeforums_Reply_Delete", ForumID, TopicId, ReplyID, 2);

            }



        }


        #endregion

        #region "Topics Attachement"
        public IQueryable<Attach> GetSharedFiles(bool IsAdmin, int ContentId, out int TotalRecord)
        {
            IQueryable<Attach> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Attach>();
                t = GetSharedFile(IsAdmin).Where(x => x.ContentId == ContentId);
            }
            TotalRecord = t.Count();

            return t;


        }
        private IQueryable<Attach> GetSharedFile(bool IsAdmin)
        {
            IQueryable<Attach> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Attach>();
                if (IsAdmin)
                    t = rep.Get().AsQueryable();
                else
                    t = rep.Find("WHERE AllowDownload=@0", true).AsQueryable().Where(x => x.DateUpdated <= System.DateTime.Now.Date);
            }
            return t;


        }
        #endregion

        #region "Likes / Stats"
        public Stats GetSats()
        {
            Stats t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<GlobalCategory>();
                t = ctx.ExecuteSingleOrDefault<Stats>(System.Data.CommandType.StoredProcedure, "ClearAction_GetStats");
            }
            return t;


        }

        public string GetEmail()
        {
            string t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<GlobalCategory>();
                t = ctx.ExecuteSingleOrDefault<string>(System.Data.CommandType.StoredProcedure, "ClearAction_GetEmailSubscripion");
            }
            return t;


        }

        public string GetSubject(int contentId)
        {
            string t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<GlobalCategory>();
                t = ctx.ExecuteSingleOrDefault<string>(System.Data.CommandType.StoredProcedure, "ClearAction_GetContentSubject", contentId);
            }
            return t;


        }

        public string GetAuthor(int  contentId)
        {
            string t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<GlobalCategory>();
                t = ctx.ExecuteSingleOrDefault<string>(System.Data.CommandType.StoredProcedure, "ClearAction_GetContentAuthor", contentId);
            }
            return t;


        }
        public bool LikeIsLikeByUser(int contentId, int UserID)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                IRepository<Likes> rep = ctx.GetRepository<Likes>();
                Likes olike = rep.Find("Where PostId = @0 AND UserId = @1", contentId, UserID).FirstOrDefault();
                if (olike != null)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region "Active Members"
        public List<Author> GetActiveMemberList(int TopicID)
        {
            try
            {
                IQueryable<Author> t;
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<Author> rep = ctx.GetRepository<Author>();
                    t = ctx.ExecuteQuery<Author>(CommandType.StoredProcedure, "activeforums_GetActiveTopicMember", TopicID).AsQueryable();
                }

                if (t != null)
                    return t.OrderByDescending(x => x.ResponseCount).ToList();
            }
            catch (Exception ex)
            {
            }


            return null;
        }
        #endregion  
        #region "Tags#

        public List<string> GetAutoCompleteTag(string strKeyWord)
        {
            IQueryable<string> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<string>();

                t = ctx.ExecuteQuery<string>(CommandType.StoredProcedure, "CA_ListofSuggestedTaxonmoy", 1, strKeyWord).AsQueryable();
            }
            if (t != null)
                return t.ToList();


            return null;
        }

        public List<Tags> GetTags(int iTopicID)
        {
            IQueryable<Tags> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Tags>();

                t = ctx.ExecuteQuery<Tags>(CommandType.StoredProcedure, "activeforums_GetTagsByTopic", iTopicID).AsQueryable();
            }
            if (t != null)
                return t.OrderBy(x => x.TagName).ToList();
            return null;
        }
        #endregion
        #region "File Stack to Forum"
        public void AddFileStack(FileStackRefernceInfo oFileStack)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                IRepository<FileStackRefernceInfo> rep = ctx.GetRepository<FileStackRefernceInfo>();
                rep.Insert(oFileStack);
            }
        }

        //public void UpdatewithContentID(int iContentID)
        //{
        //    string strText = "Update Activeforum_FileStackRefernce SET ContentId=@0  WHERE ContentId=-1";
        //    using (IDataContext ctx = DataContext.Instance())
        //    {
        //        IRepository<FileStackRefernceInfo> rep = ctx.GetRepository<FileStackRefernceInfo>();
        //        ctx.ExecuteQuery(CommandType.Text, strText, iContentID);
        //    }

        //}
        public void UpdateFileStack(FileStackRefernceInfo oFileStack)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                IRepository<FileStackRefernceInfo> rep = ctx.GetRepository<FileStackRefernceInfo>();
                rep.Update(oFileStack);
            }
        }

        public List<FileStackRefernceInfo> GetFileStack(int iContentID)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                IRepository<FileStackRefernceInfo> rep = ctx.GetRepository<FileStackRefernceInfo>();
                return rep.Find("Where ContentId=@0", iContentID).ToList();
            }
            return null;
        }

      
        #endregion
    }
}