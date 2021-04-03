using DotNetNuke.Data;
using DotNetNuke.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClearAction.Modules.Alert.Entity;
namespace ClearAction.Modules.Alert.Components
{
    public partial class DbController
    {
        #region "Block Related Methods"


        public void BlockUserInsert(UserBlock oUserBlock)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserBlock>();
                    rep.Insert(oUserBlock);


                }
            }
            catch (Exception ex)
            {


            }
        }

        public void BlockUserDelete(UserBlock oblock)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserBlock>();
                    rep.Delete(oblock);


                }
            }
            catch (Exception ex)
            {


            }
        }

        public List<UserBlock> GetBlockListByUser(int fromUserID)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<UserBlock>();

                return rep.Find("WHERE FromUserId=@0", fromUserID).ToList();


            }
        }



        public List<UserBlock> GetBlockedListofUser(int ToUserID)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<UserBlock>();

                return rep.Find("WHERE ToUserID=@0", ToUserID).ToList();


            }
        }
        public UserBlock GetBlockListDetail(int KeyID)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<UserBlock>();

                return rep.GetById(KeyID);


            }
        }
        #endregion


        #region "Alert Related methods"

        public UserAlert AlertInsert(UserAlert oUserAlert)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserAlert>();
                    rep.Insert(oUserAlert);


                }
            }
            catch (Exception ex)
            {


            }
            return oUserAlert;
        }
        public int AlertHistoryInsert(UserAlertHistory oUserAlertHistory)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserAlertHistory>();
                    rep.Insert(oUserAlertHistory);


                }
                return 1;
            }
            catch (Exception ex)
            {


            }
            return 0;
        }
        public UserAlert AlertUpdate(UserAlert oUserAlert)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserAlert>();
                    rep.Update(oUserAlert);
                }
            }
            catch (Exception ex)
            {


            }
            return oUserAlert;
        }


        public void AlertHistoryUpdate(UserAlertHistory oUserAlertHistory)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserAlertHistory>();
                    rep.Update(oUserAlertHistory);


                }
            }
            catch (Exception ex)
            {


            }
        }
        public List<UserAlert> AlertGets()
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserAlert>();
                    return rep.Get().ToList();


                }
            }
            catch (Exception ex)
            {


            }
            return null;
        }

        public UserAlert AlertGet(int AlertID)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserAlert>();
                    return rep.GetById(AlertID);


                }
            }
            catch (Exception ex)
            {


            }
            return null;
        }


        public List<UserAlertHistory> AlertStatusGet(int AlertID)
        {
            IQueryable<UserAlertHistory> t;
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserAlertHistory>();
                    t = rep.Find("Where AlertId=@0", AlertID).AsQueryable();


                }

                if (t != null)
                    return t.OrderBy(x => x.StatusId).ToList();

            }
            catch (Exception ex)
            {


            }
            return null;
        }
        public UserAlertHistory AlertStatusGetByID(int LogAlertLogId)
        {

            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<UserAlertHistory>();
                    return rep.GetById(LogAlertLogId);

                }



            }
            catch (Exception ex)
            {


            }
            return null;
        }
        #endregion


        #region "Alert Category"
        public AlertCategory Alert_CategoryInsert(AlertCategory alertCategory)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<AlertCategory>();
                    rep.Insert(alertCategory);


                }
            }
            catch (Exception ex)
            {


            }
            return alertCategory;
        }

        public AlertCategory Alert_CategoryUpdate(AlertCategory alertCategory)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<AlertCategory>();
                    rep.Update(alertCategory);


                }
            }
            catch (Exception ex)
            {


            }
            return alertCategory;
        }


        public List<AlertCategory> Alert_CategoryGet()
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<AlertCategory>();
                    return rep.Get().OrderByDescending(x => x.CategoryName).ToList();


                }
            }
            catch (Exception ex)
            {


            }
            return null;
        }

        public AlertCategory Alert_CategoryGetId(int iCategoryId)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<AlertCategory>();
                    return rep.GetById(iCategoryId);


                }
            }
            catch (Exception ex)
            {


            }
            return null;
        }

        #endregion


        #region "Alert Status"
        public AlertStatus Alert_StatusInsert(AlertStatus alertStatus)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<AlertStatus>();
                    rep.Insert(alertStatus);


                }
            }
            catch (Exception ex)
            {


            }
            return alertStatus;
        }

        public AlertStatus Alert_StatusUpdate(AlertStatus alertStatus)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<AlertStatus>();
                    rep.Update(alertStatus);


                }
            }
            catch (Exception ex)
            {


            }
            return alertStatus;
        }


        public List<AlertStatus> Alert_StatusGet()
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<AlertStatus>();
                    return rep.Get().OrderByDescending(x => x.StatusKey).ToList();


                }
            }
            catch (Exception ex)
            {


            }
            return null;
        }

        public AlertStatus Alert_StatusGetId(int istatusId)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<AlertStatus>();
                    return rep.GetById(istatusId);


                }
            }
            catch (Exception ex)
            {


            }
            return null;
        }

        #endregion
    }
}