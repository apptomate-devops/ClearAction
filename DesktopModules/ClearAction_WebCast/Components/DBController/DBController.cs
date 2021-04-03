using ClearAction.Modules.WebCast.Components.Entity;
using DotNetNuke.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System.Threading;
using System.Threading.Tasks;

namespace ClearAction.Modules.WebCast.Components
{
    public partial class DBController
    {



        #region "Category Related"

        public IQueryable<ComponentMaster> GetComponentMaster()
        {
            IQueryable<ComponentMaster> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<ComponentMaster>();
                t = rep.Get().AsQueryable();

            }
            return t.OrderBy(x => x.ComponentName);


        }
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
        #endregion


        #region "Update User Compoenent
        public void UpdateUserComponentStatus(int iItemId, int iUserId, int iComponentID)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {

                    ctx.ExecuteQuery<int>(System.Data.CommandType.StoredProcedure, "WCC_UpdateUserComponent", iUserId, iItemId, iComponentID);
                }

            }
            catch (Exception exc)
            {


            }
        }
        #endregion
        #region "WebCast Related"
        //public async Task<Event> geturl(WCC_PostInfo oData)
        //{
        //    IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder
        //                        .Create("88a4e5f9-c6dc-41e6-956c-a4221670d53d")
        //                        .Build();
        //    // Create an authentication provider by passing in a client application and graph scopes.
        //    string[] scopes = new string[] { "https://graph.microsoft.com/Calendars.ReadWrite" };

        //    DeviceCodeProvider authProvider = new DeviceCodeProvider(publicClientApplication, scopes);
        //    // Create a new instance of GraphServiceClient with the authentication provider.
        //    GraphServiceClient graphClient = new GraphServiceClient(authProvider);

        //    DateTime dt = oData.EventDate;
        //    DateTime dt2 = oData.EventDate;
        //    DateTime dt1 = oData.EventTime;
        //    dt.Add(dt1.TimeOfDay);
        //    dt2.Add(dt1.TimeOfDay);
        //    var onlineMeet = new Event
        //    {
        //        Start = new DateTimeTimeZone
        //        {
        //            DateTime = dt.ToString(),
        //            TimeZone = "UTC"
        //        },
        //        End = new DateTimeTimeZone
        //        {
        //            DateTime = dt2.ToString(),
        //            TimeZone = "UTC"
        //        },

        //        Subject = oData.Title,
        //        AdditionalData = new Dictionary<string, object>()
        //                 {
        //                     {"isOnlineMeeting", "true"},
        //                     {"onlineMeetingProvider", "teamsForBusiness"}
        //                 }
        //    };

        //    var result = await graphClient.Me.Events
        //       .Request()
        //       .AddAsync(onlineMeet);
        //    return result as Event;
        //}
        public void AddWCC_Post(WCC_PostInfo oData)
        {

            
                IDataContext ctx = DataContext.Instance();
           // Task<Microsoft.Graph.Event> task = Task.Run(async () => await geturl(oData));

            // Build a client application.
            //oData.RegistrationLink = task.Result.OnlineMeetingUrl;
                var rep = ctx.GetRepository<WCC_PostInfo>();
                rep.Insert(oData);
               
           
        }

        public void DeleteWCC_Post(int WPostID)
        {

            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<WCC_PostInfo>();
                    ctx.ExecuteQuery<int>(System.Data.CommandType.StoredProcedure, "WCC_Delete", WPostID);
                }
            }
            catch (Exception exc)
            {


            }
        }


        public void UpdateWCC_Post(WCC_PostInfo oData)
        {

            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<WCC_PostInfo>();
                    rep.Update(oData);

                }
            }
            catch (Exception exc)
            {


            }
        }

        public WCC_PostInfo Get_Post(int oWCC)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<WCC_PostInfo>();
                return rep.GetById(oWCC);

            }

        }

        public List<WCC_PostInfo> Get_Posts()
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<WCC_PostInfo>();
                return rep.Get().ToList();

            }

        }
        private IQueryable<WCC_PostInfo> GetFromDB(int UserId, int CatId, string OptionId, int LoggedinUser, string SearchText, int ComponentID)
        {
            IQueryable<WCC_PostInfo> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<WCC_PostInfo>();
                t = ctx.ExecuteQuery<WCC_PostInfo>(System.Data.CommandType.StoredProcedure, "WCC_GetEventByUserAssignment", UserId, CatId, OptionId, LoggedinUser, SearchText, ComponentID).AsQueryable();

            }
            return t;
        }







        public int GetPostCount()
        {
            IQueryable<WCC_PostInfo> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<WCC_PostInfo>();
                t = rep.Get().Where(x => x.IsPublish == true).AsQueryable();

            }
            if (t == null)
                return 0;
            return t.Count();
        }
        public WCC_PostInfo Get_Post(int UserId, int CatId, string OptionId, int LoggedinUser, string SearchText, int ComponentID, int iWccID)
        {
            IQueryable<WCC_PostInfo> t = GetFromDB(UserId, CatId, OptionId, LoggedinUser, SearchText, ComponentID);

            if (t != null)
                return t.Where(x => x.WCCId == iWccID).OrderByDescending(y => y.EventDate).SingleOrDefault();
            return null;
        }
        public List<WCC_PostInfo> Get_Posts(int UserId, int CatId, string OptionId, int LoggedinUser, string SearchText, int ComponentID, bool IsAdmin, int PageSize, int CurrentPage, int orderby, out int iTotal)
        {
            IQueryable<WCC_PostInfo> t = GetFromDB(UserId, CatId, OptionId, LoggedinUser, SearchText, ComponentID);




            switch (orderby)
            {
                case -1:

                    t = t.OrderByDescending(x => x.EventDate);
                    var tfutureevent = t.Where(x => x.EventDate >= System.DateTime.Now).OrderByDescending(x => x.EventDate);

                    var tpastevent = t.Where(x => x.EventDate < System.DateTime.Now).OrderByDescending(x => x.EventDate);
                    t = tfutureevent.Concat(tpastevent);


                    iTotal = t.ToList().Count;
                    t = t.Skip(PageSize * (CurrentPage - 1)).Take(PageSize);

                    return t.ToList();


                case 0:
                    t = t.OrderBy(x => x.Title);
                    break;

                case 1:
                    t = t.OrderByDescending(x => x.Title);

                    break;
                case 2:
                    t = t.OrderBy(x => x.PresenterName);
                    break;
                case 3:
                    t = t.OrderByDescending(x => x.PresenterName); break;

                case 4:
                    t = t.OrderByDescending(x => x.EventDate); break;

                case 5:
                    t = t.OrderBy(x => x.EventDate); break;


                case 6:
                    t = t.Where(x => x.EventDate.Date == System.DateTime.Now.Date);
                    t = t.OrderByDescending(x => x.Title); break;
                case 7:
                    t = t.Where(x => x.EventDate.Date >= System.DateTime.Now.AddDays(-7).Date && x.EventDate.Date <= System.DateTime.Now.Date);

                    t = t.OrderByDescending(x => x.EventDate); break;
                case 8:
                    t = t.Where(x => x.EventDate.Date <= System.DateTime.Now.AddDays(7).Date && x.EventDate.Date >= System.DateTime.Now.Date);
                    //  t = t.Where(x => x.EventDate.Date <= System.DateTime.Now.AddDays(30).Date && x.EventDate.Date >= System.DateTime.Now.Date);
                    t = t.OrderBy(x => x.Title); break;
                case 9:
                    t = t.OrderByDescending(x => x.CommentCount); break;
                case 10:
                    t = t.Where(x => x.EventDate.Date.Month == System.DateTime.Now.Month && x.EventDate.Date.Year == System.DateTime.Now.Date.Year);
                    t = t.OrderByDescending(x => x.Title); break;
                case 11:
                    t = t.Where(x => x.EventDate.Date >= System.DateTime.Now.Date);
                    t = t.OrderBy(x => x.EventDate); break;
                case 12:
                    t = t.Where(x => x.EventDate.Date < System.DateTime.Now.Date);
                    t = t.OrderByDescending(x => x.EventDate); break;
                default:
                    t = t.OrderByDescending(x => x.EventDate); break;

            }
            iTotal = t.ToList().Count;
            t = t.Skip(PageSize * (CurrentPage - 1)).Take(PageSize);
        
            return t.ToList();
        }
        #endregion

        #region "Global User component Email"
        public int GetEmailNotificationFromComponenet(int iUserID, int iComponentId, int iMaxRecord)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteScalar<int>(System.Data.CommandType.StoredProcedure, "CA_GetUserComponentNotified", iUserID, iComponentId, iMaxRecord);
            }

        }

        #endregion  

        #region "Global Category WCC"

        public IQueryable<PostCategoryRelationInfo> GetTopicCategoryRelation(int WCCIdId)
        {
            try
            {
                IQueryable<PostCategoryRelationInfo> t;
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<PostCategoryRelationInfo> rep = ctx.GetRepository<PostCategoryRelationInfo>();
                    t = rep.Find("Where WCCId=@0", WCCIdId).AsQueryable();
                }
                return t;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public PostCategoryRelationInfo GetTopicCategoryRelation(int oCategoryID, int PostId)
        {
            try
            {
                PostCategoryRelationInfo t = default(PostCategoryRelationInfo);
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<PostCategoryRelationInfo> rep = ctx.GetRepository<PostCategoryRelationInfo>();
                    t = rep.Find("WHERE CategoryId=@0 AND WCCId=@1", oCategoryID, PostId).SingleOrDefault();
                }
                return t;

            }
            catch (Exception ex)
            {
            }
            return null;
        }
        public List<PostCategoryRelationInfo> GetTopicCategoryRelationOnlyActive(int WCCIdId, bool IsActive)
        {
            try
            {
                List<PostCategoryRelationInfo> t = new List<PostCategoryRelationInfo>();
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<PostCategoryRelationInfo> rep = ctx.GetRepository<PostCategoryRelationInfo>();
                    t = rep.Find("WHERE WCCId=@0 and IsActive=@1", WCCIdId, true).ToList();

                }
                return t;

            }
            catch (Exception ex)
            {
            }
            return null;

        }
        private void AddWCCCategoryRelation(PostCategoryRelationInfo oPostCategory)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<PostCategoryRelationInfo> rep = ctx.GetRepository<PostCategoryRelationInfo>();
                    rep.Insert(oPostCategory);
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void UpdateWCCCategoryRelation(PostCategoryRelationInfo oPostCategory)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<PostCategoryRelationInfo> rep = ctx.GetRepository<PostCategoryRelationInfo>();
                    rep.Update(oPostCategory);
                }

            }
            catch (Exception ex)
            {
            }
        }


        public void WCCCategoryRelation_AddUpdate(PostCategoryRelationInfo oCategory)
        {
            try
            {
                PostCategoryRelationInfo oCategoryTemp = GetTopicCategoryRelation(oCategory.CategoryId, oCategory.WCCId);
                if (oCategoryTemp == null)
                {
                    AddWCCCategoryRelation(oCategory);
                }
                else
                {
                    oCategory.WPostCategoryId = oCategoryTemp.WPostCategoryId;
                    UpdateWCCCategoryRelation(oCategory);
                }

            }
            catch (Exception ex)
            {
            }


        }
        #endregion


        #region "Likes/Reply/Comments"

        /// <summary>
        /// Add Comments and sub-comment
        /// </summary>
        /// <param name="oComent">Comment class</param>
        /// <returns></returns>
        public Comments AddComments(Comments oComent)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<Comments> rep = ctx.GetRepository<Comments>();
                    rep.Insert(oComent);
                }
            }
            catch (Exception ex)
            {


            }
            return oComent;
        }
        public void UpdateComments(Comments oComent)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<Comments> rep = ctx.GetRepository<Comments>();
                    rep.Update(oComent);
                }
            }
            catch (Exception ex)
            {


            }
        }
        public Comments GetCommentByID(int iCommentID)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<Comments> rep = ctx.GetRepository<Comments>();
                    return rep.GetById(iCommentID);
                }
            }
            catch (Exception ex)
            {


            }
            return null;
        }
        public List<Comments> GetComments(int PostID)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<Comments> rep = ctx.GetRepository<Comments>();
                    return rep.Find("WHERE WCCId=@0", PostID).ToList();
                }
            }
            catch (Exception ex)
            {


            }
            return null;
        }

        //Delete commments
        public void DeleteComment(int PostID)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    ctx.ExecuteQuery<int>(System.Data.CommandType.StoredProcedure, "WCC_DeleteComment", PostID);
                }
            }
            catch (Exception ex)
            {


            }
            //            return null;
        }

        public string GetCommentSubject(string Postkey)
        {
            string t = "";
                     
                using (IDataContext ctx = DataContext.Instance())
                {
                 t= ctx.ExecuteSingleOrDefault<string>(System.Data.CommandType.StoredProcedure, "WCC_GetInsightSubject", Postkey);
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
        public string GetPresenter(string Postkey)
        {
            string t = "";

            using (IDataContext ctx = DataContext.Instance())
            {
                t = ctx.ExecuteSingleOrDefault<string>(System.Data.CommandType.StoredProcedure, "WCC_GetInsightPresenter", Postkey);
            }


            return t;
        }



        public bool SaveWebinarUserRegistrationDetails(int UserId, int WCCId, int ComponentID, string WebinarKey, string UserWebinarRegistrationKey, string UserWebinarJoinURL)
        {
            bool status = true;

            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    ctx.ExecuteQuery<int?>(System.Data.CommandType.StoredProcedure, "Save_Webinar_UserRegistration_Details", UserId, WCCId, ComponentID, WebinarKey, UserWebinarRegistrationKey, UserWebinarJoinURL);
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }



        public IQueryable<Webinar_User_RegistrationDetail> GetWebinarUserRegistrationDetails()
        {
            IQueryable<Webinar_User_RegistrationDetail> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Webinar_User_RegistrationDetail>();
                t = ctx.ExecuteQuery<Webinar_User_RegistrationDetail>(System.Data.CommandType.StoredProcedure, "Get_Webinar_UserRegistrationDetails").AsQueryable();
            }
            return t;
        }



        public IQueryable<Webinar_User_RegistrationDetail> GetWebinarUserRegistrationForEvent(int UserId,int WCCId,int ComponentID )
        {
            IQueryable<Webinar_User_RegistrationDetail> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Webinar_User_RegistrationDetail>();
                t = ctx.ExecuteQuery<Webinar_User_RegistrationDetail>(System.Data.CommandType.StoredProcedure, "Get_WebinarUserRegistration_For_Event", UserId, WCCId, ComponentID).AsQueryable();
            }
            return t;
        }


        public bool UpdateWebinarUserAttendanceStatus(int? UserId, int? WCCId, int? ComponentID, string WebinarKey, string UserWebinarRegistrationKey)
        {
            bool status = true;

            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    ctx.ExecuteQuery<int?>(System.Data.CommandType.StoredProcedure, "Update_Webinar_User_Attendance_Status", UserId, WCCId, ComponentID, WebinarKey, UserWebinarRegistrationKey);
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }


        public List<Comments> GetCommentByEventID(bool iIsAdmin, int WCCId, int ParentId)
        {
            IQueryable<Comments> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                IRepository<Comments> rep = ctx.GetRepository<Comments>();
                if (iIsAdmin)
                {
                    t = rep.Find("Where  WCCId=@0", WCCId).AsQueryable();
                }
                else
                {
                    t = rep.Find("Where  WCCId=@0 AND Approved=@1", WCCId, true).AsQueryable();
                }
            }

            if (t != null)
            {
                if (ParentId == -1)
                {
                    t = t.Where(x => x.ParentId == -1);
                }
                else
                {
                    t = t.Where(x => x.ParentId == ParentId);
                }

                t = t.OrderByDescending(x => x.CreatedOnDate);
                return t.ToList();
            }

            return null;
        }


        public string GetForPostCount(int postId, string ItemType)
        {
            List<Likes> t = GetForPost(postId, ItemType);
            if (t == null)
            {
                return "0";
            }

            return t.Count.ToString();
        }




        public List<Likes> GetForPost(int postId, string ItemType)
        {
            IQueryable<Likes> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                IRepository<Likes> rep = ctx.GetRepository<Likes>();
                t = rep.Find("WHERE PostId = @0 AND Checked = 1 and ItemType=@1", postId, ItemType).AsQueryable();
            }

            return t.ToList();
        }

        public List<Likes> GetUnlikeForPost(int postId, string ItemType)
        {
            IQueryable<Likes> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                IRepository<Likes> rep = ctx.GetRepository<Likes>();
                t = rep.Find("WHERE PostId = @0 AND Checked = 0 and ItemType=@1", postId, ItemType).AsQueryable();
            }

            return t.ToList();
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

        public string Post(int contentId, int userId, string ItemType)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                IRepository<Likes> rep = ctx.GetRepository<Likes>();
                Likes olike = rep.Find("Where PostId = @0  AND UserId = @1 and ItemType=@2", contentId, userId, ItemType).FirstOrDefault();
                if (olike != null)
                {
                    olike.ItemType = ItemType;
                    if (olike.Checked)
                    {
                        olike.Checked = false;
                        rep.Delete(olike);
                    }
                    else
                    {
                        olike.Checked = true;
                        rep.Update(olike);
                    }

                    return "RemovedLike";
                }
                else
                {
                    olike = new Likes();
                    olike.PostId = contentId;
                    olike.UserId = userId;
                    olike.Checked = true;
                    olike.ItemType = ItemType;
                    rep.Insert(olike);
                }

                return "AddedLike";
            }
        }

        public void DeleteLikes(int contentId, int userId, string ItemType)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                IRepository<Likes> rep = ctx.GetRepository<Likes>();
                Likes olike = rep.Find("Where PostId = @0 AND UserId = @1 and ItemType=@2", contentId, userId, ItemType).FirstOrDefault();
                if (olike != null)
                {
                    rep.Delete(olike);
                }
            }
        }

        public string UnLikePost(int contentId, int userId)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                IRepository<Likes> rep = ctx.GetRepository<Likes>();
                Likes olike = rep.Find("Where PostId = @0 and UserId = @1 and ItemType='Post'", contentId, userId).FirstOrDefault();
                if (olike == null)
                {
                    olike = new Likes();
                    olike.PostId = contentId;
                    olike.UserId = userId;
                    olike.Checked = false;
                    olike.ItemType = "Post";
                    rep.Insert(olike);
                }
                else
                {
                    olike.PostId = contentId;
                    olike.UserId = userId;
                    olike.Checked = false;
                    olike.ItemType = "Post";
                    rep.Update(olike);
                }

                return "AddedLike";
            }
        }
        #endregion


        #region "Attach File"
        public void DeleteFromURL(string FileStackUrl)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    ctx.Execute(System.Data.CommandType.Text, "DELETE FROM WCC_Attachments Where FileUrl=@0", FileStackUrl);
                }
            }
            catch (Exception ex)
            {


            }

        }
        public void SaveEditReplyAttachments(int contentId, string fileName, string content, string fileurl)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    ctx.Execute(System.Data.CommandType.StoredProcedure, "WCC_SaveEditReplyAttachments", contentId, fileName, content, fileurl);
                }
            }
            catch (Exception ex)
            {


            }
        }
        public AttachmentInfo AddAttachment(AttachmentInfo attachmentInfo)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    IRepository<AttachmentInfo> rep = ctx.GetRepository<AttachmentInfo>();
                    rep.Insert(attachmentInfo);
                }
            }
            catch (Exception ex)
            {


            }
            return attachmentInfo;
        }
        public AttachmentInfo UpdateAttachment(AttachmentInfo attachmentInfo)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<AttachmentInfo>();
                    rep.Update(attachmentInfo);
                }
            }
            catch (Exception ex)
            {


            }
            return attachmentInfo;
        }
        public void DeleteAttachment(AttachmentInfo attachmentInfo)
        {
            //  return attachmentInfo;
        }

        public List<AttachmentInfo> GetAttachmentByPost(int iWccID)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<AttachmentInfo>();
                    return rep.Find("WHERE WCCId=@0", iWccID).ToList();
                }
            }
            catch (Exception ex)
            {


            }
            return null;

        }


        public List<AttachmentInfo> GetAttachmentByComment(int iCommentID)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<AttachmentInfo>();
                    return rep.Find("WHERE CommentID=@0", iCommentID).ToList();
                }
            }
            catch (Exception ex)
            {


            }
            return null;

        }
        #endregion


        #region "Members and files"

        public List<UserData> RecentMemberByEvent(int wccid)
        {
            using (IDataContext idx = DataContext.Instance())
            {
                return idx.ExecuteQuery<UserData>(System.Data.CommandType.StoredProcedure, "WCCEvents_GetActiveTopicMember", wccid).ToList();

            }
        }
        #endregion

    }
}