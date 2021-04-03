using DotNetNuke.Data;
using DotNetNuke.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClearAction.Modules.Scheduler;
namespace ClearAction.Modules.Connections
{
    public partial class DbController
    {


        #region "Favoriate Section"
        private IQueryable<UserFavoriteInfo> GetUserfavoriteRecord(int cUserId)
        {

            IQueryable<UserFavoriteInfo> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<UserFavoriteInfo>();
                t = rep.Find("WHERE cUserId=@0", cUserId).AsQueryable();


            }
            return t;

        }
        public IList<UserFavoriteInfo> GetUserfavoriteRecord(int cUserId, bool OnlyFaviourate)
        {

            IQueryable<UserFavoriteInfo> t = GetUserfavoriteRecord(cUserId);
            if (t != null)
                return t.OrderBy(x => x.RelatedUserID).ToList();
            return t.ToList();

        }

        public bool GetUserfavoriteRecord(int cUserId, int targetUser)
        {

            var t = GetUserfavoriteRecord(cUserId, true);
            UserFavoriteInfo odata = null;
            if (t != null)
            {
                odata = t.Where(x => x.RelatedUserID == targetUser).SingleOrDefault();
                if (odata != null)
                    return odata.IsFav;
            }
            return false;
        }


        private void AddFavoriateRecord(UserFavoriteInfo odata)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserFavoriteInfo>();
                    rep.Insert(odata);


                }
            }
            catch (Exception ex)
            {


            }
        }

        private void UpdateFavoriateRecord(UserFavoriteInfo odata)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserFavoriteInfo>();
                    rep.Update(odata);


                }
            }
            catch (Exception ex)
            {


            }
        }

        /// <summary>
        /// Check for connection is fav or not
        /// </summary>
        /// <param name="cUserId"></param>
        /// <param name="targetUser"></param>
        /// <param name="_IsFav"></param>
        /// <returns></returns>
        public bool FavoriteRecord(int cUserId, int targetUser, bool _IsFav)
        {

            var t = GetUserfavoriteRecord(cUserId, true);
            UserFavoriteInfo odata = null;
            if (t != null)
            {
                odata = t.Where(x => x.RelatedUserID == targetUser).SingleOrDefault();
                if (odata != null)
                {
                    odata.IsFav = _IsFav;
                    UpdateFavoriateRecord(odata);

                }
                else
                {
                    odata = new UserFavoriteInfo()
                    {

                        cUserId = cUserId,
                        IsFav = _IsFav,
                        RelatedUserID = targetUser
                    };
                    AddFavoriateRecord(odata);

                }
            }
            return true;
        }
        #endregion


        #region "Active Connection"

        private void AddUserToFriend(UserRelationships oUserReleation)
        {

            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserRelationships>();
                    rep.Insert(oUserReleation);


                }
            }
            catch (Exception ex)
            {


            }
        }

        private void UpdateUserToFriend(UserRelationships oUserReleation)
        {

            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserRelationships>();
                    rep.Update(oUserReleation);


                }
            }
            catch (Exception ex)
            {


            }
        }
        private IQueryable<UserRelationships> GetUserRelationShip(int iUserID, int iToUser)
        {
            IQueryable<UserRelationships> t = null;
            try
            {

                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserRelationships>();
                    t = ctx.ExecuteQuery<UserRelationships>(System.Data.CommandType.StoredProcedure, "CA_GetConnectionsUser", iUserID, iToUser).AsQueryable();

                }
            }
            catch (Exception ex)
            {


            }
            return t;

        }


        /// <summary>
        /// User id 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="Status">0:null, 1: Pending, 2: Friends</param>
        /// <returns></returns>
        public IQueryable<UserRelationships> GetRequestByStatus(int iUserID, int Status)
        {
            IQueryable<UserRelationships> t = null;
            try
            {

                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserRelationships>();
                    t = rep.Find("WHERE RelatedUserID=@0 AND Status=@1", iUserID, Status).AsQueryable();

                }
            }
            catch (Exception ex)
            {


            }
            return t;

        }

        private IQueryable<UserRelationships> GetTotalRecord(int iUserID)
        {
            IQueryable<UserRelationships> t = null;
            try
            {

                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserRelationships>();
                    t = ctx.ExecuteQuery<UserRelationships>(System.Data.CommandType.StoredProcedure, "CA_GetConnectionsByUserID", iUserID).AsQueryable();

                }
            }
            catch (Exception ex)
            {


            }
            return t;

        }


        public int TotalConnection(int iUserID)
        {
            var oQuerable = GetTotalRecord(iUserID);
            if (oQuerable == null)
                return 0;
            return oQuerable.Count();
        }

        public int AddFriend(int PortalId, UserInfo oUser, UserInfo oRelatedUserinfo)
        {

            var oQuerablelist = GetUserRelationShip(oUser.UserID, oRelatedUserinfo.UserID);

            if (oQuerablelist != null)
            {
                var oUserRelationship = oQuerablelist.SingleOrDefault();

                if (oUserRelationship != null)
                {
                    oUserRelationship.Status = 1;
                    UpdateUserToFriend(oUserRelationship);

                }
                else
                {
                    oUserRelationship = new UserRelationships()
                    {
                        CreatedByUserID = oUser.UserID,
                        CreatedOnDate = System.DateTime.Now,
                        LastModifiedByUserID = oUser.UserID,
                        LastModifiedOnDate = System.DateTime.Now,
                        RelatedUserID = oRelatedUserinfo.UserID,
                        UserID = oUser.UserID,
                        RelationshipID = 1,//1 : For friends, 2: for followers
                        Status = 1,
                        UserRelationshipID = -1

                    };
                    AddUserToFriend(oUserRelationship);
                    return 1;
                }



            }


            return 0;
        }

        #endregion


        public string GetCompanyName(int CompanyID)
        {


            string strResponse = string.Empty;
            IQueryable<string> t = null;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<string>();

                t = rep.Find("SELECT CompanyName FROM ClearAction_GlobalCompany Where  CompanyId=@0", CompanyID).AsQueryable();

            }
            if (t != null)
                return t.SingleOrDefault();
            return strResponse;



        }

        public List<MessageInfo> GetMessage(int iUserID)
        {
            using (IDataContext ctx = DataContext.Instance())
            {

                return ctx.ExecuteQuery<MessageInfo>(System.Data.CommandType.StoredProcedure, "ClearAction_GetCoreMessage", iUserID).ToList();

            }

        }
        public List<MessageInfo> GetMessage(int ToAccount, int FromAccount )
        {
            using (IDataContext ctx = DataContext.Instance())
            {

                return ctx.ExecuteQuery<MessageInfo>(System.Data.CommandType.StoredProcedure, "ClearAction_GetCoreMessageBoth", ToAccount,FromAccount).ToList();

            }

        }

        public void AddMessage(MessageInfo oMessage)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                //var rep = ctx.GetRepository<MessageInfo>();

                ctx.ExecuteQuery<MessageInfo>(System.Data.CommandType.StoredProcedure, "ClearAction_CoreMessaging_SaveMessage", oMessage.MessageID, oMessage.PortalID, oMessage.To, oMessage.From, oMessage.Subject, oMessage.Body, oMessage.ConversationID, oMessage.ReplyAllAllowed, oMessage.SenderUserID, oMessage.CreatedByUserID, oMessage.ToAccount).ToList();

            }

        }

        
    }
}