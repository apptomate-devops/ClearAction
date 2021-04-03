
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DotNetNuke.Entities.Users;
using DotNetNuke.Instrumentation;
using DotNetNuke.Security;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Security.Roles;
using DotNetNuke.Security.Roles.Internal;
using DotNetNuke.Web.Api;
using DotNetNuke.Entities.Users.Social;
using System.Web.Script.Serialization;
using ClearAction.Modules.Connections.Components;
namespace ClearAction.Modules.Connections
{

    //[DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
    [DnnAuthorize]
    public class AlertController : DnnApiController
    {

        #region "Private methods"

        private int AddAlertHistory(int iAlertID, int iStatusid)
        {
            var controller = new DbController();
            var oAlertViewerHistory = new UserAlertHistory()
            {
                AlertId = iAlertID,
                CreatedBy = GetCurrentUser.UserID,
                CreatedOn = System.DateTime.Now,
                Notes = "",
                StatusId = iStatusid
            };

            return controller.AlertHistoryInsert(oAlertViewerHistory);
            //   return 1;
        }
        private int UpdateAlert(UserAlert oviewer, int iStatus)
        {
            var controller = new DbController();
            var oAlertViewer = new UserAlert()
            {
                Id = oviewer.Id,
                CategoryId = oviewer.CategoryId,
                Context = oviewer.Context,
                StatusId = iStatus,
                RecipentId = oviewer.RecipentId

            };
            oAlertViewer = controller.AlertUpdate(oAlertViewer);

            return AddAlertHistory(oAlertViewer.Id, iStatus);
        }
        private UserInfo GetCurrentUser
        {
            get
            {
                return DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo();
            }
        }



        private HttpResponseMessage ChangeStatus(int AlertID, int Statusid)
        {
            try
            {
                var controller = new DbController();
                var oCurrentuser = GetCurrentUser;
                if (AlertID > 0 && oCurrentuser.UserID > 0)
                {
                    var otemp = controller.AlertGet(AlertID);
                    if (otemp == null)
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request");

                    if (UpdateAlert(otemp, (int)AlertEnum.Status.DELETED) > 0)
                        return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }

                return Request.CreateResponse(HttpStatusCode.OK, "No Action");
            }
            catch (Exception exc)
            {
                // Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        #endregion  
        #region "HTTP Request"


        [HttpGet]
        public HttpResponseMessage HelloWorld()
        {
            try
            {

                return Request.CreateResponse(HttpStatusCode.OK, "Hello World");
            }
            catch (Exception exc)
            {
                // Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        [HttpGet]
        public HttpResponseMessage HelloName(string strName)
        {
            try
            {

                return Request.CreateResponse(HttpStatusCode.OK, "Hello World" + strName);
            }
            catch (Exception exc)
            {
                // Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }
        [HttpGet]
        public HttpResponseMessage Admin_AddCategory(string CategoryName)
        {
            if (GetCurrentUser.UserID < 0)
                return Request.CreateResponse(HttpStatusCode.OK, "Un-Authorized");

            if (string.IsNullOrEmpty(CategoryName))
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request");
            var controller = new DbController();
            controller.Alert_CategoryInsert(new AlertCategory { CategoryId = -1, CategoryName = CategoryName });

            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }
       

        /// <summary>
        /// Admin access to 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public HttpResponseMessage Admin_ListCategory()
        {
            if (GetCurrentUser.UserID < 0)
                return Request.CreateResponse(HttpStatusCode.OK, "Un-Authorized");

            if(GetCurrentUser.IsInRole("Administrator")|| UserInfo.IsSuperUser)
            {
                var controller = new DbController();
                return Request.CreateResponse(HttpStatusCode.OK, new JavaScriptSerializer().Serialize(controller.Alert_CategoryGet()));
            }
            return Request.CreateResponse(HttpStatusCode.BadGateway, "Unauthorized");

        }
        [HttpGet]
        public HttpResponseMessage Admin_AddStatus(string StatusKey, string StatusValue)
        {
            if (GetCurrentUser.UserID < 0)
                return Request.CreateResponse(HttpStatusCode.OK, "Un-Authorized");
            if (GetCurrentUser.IsInRole("Administrator") || UserInfo.IsSuperUser)
            {
                if (string.IsNullOrEmpty(StatusKey) || string.IsNullOrEmpty(StatusValue))
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request");
                StatusKey = StatusKey.Trim();
                var controller = new DbController();
                controller.Alert_StatusInsert(new AlertStatus { StatusId = -1, StatusKey = StatusKey, StatusValue = StatusValue });

            }


            return Request.CreateResponse(HttpStatusCode.BadGateway, "Unauthorized");
        }
        [HttpGet]
        public HttpResponseMessage Admin_ListStatus()
        {
            if (GetCurrentUser.UserID < 0)
                return Request.CreateResponse(HttpStatusCode.OK, "Un-Authorized");
            if (GetCurrentUser.IsInRole("Administrator") || UserInfo.IsSuperUser)
            {
                var controller = new DbController();

                return Request.CreateResponse(HttpStatusCode.OK, new JavaScriptSerializer().Serialize(controller.Alert_StatusGet()));
            }
            return Request.CreateResponse(HttpStatusCode.BadGateway, "Unauthorized");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iRecepintid"></param>
        /// <param name="iCategoryId"></param>
        /// <param name="Body"></param>
        /// <returns></returns>

        [HttpGet]
        public HttpResponseMessage AddAlert(int iRecepintid, int iCategoryId, string Body)
        {
            try
            {
                var controller = new DbController();
                var user = UserController.GetUserById(PortalSettings.PortalId, iRecepintid);
                var oCurrentuser = GetCurrentUser;
                if (GetCurrentUser.UserID < 0)
                    return Request.CreateResponse(HttpStatusCode.OK, "Un-Authorized");

                var oAlertViewer = new UserAlert()
                {
                    Id = -1,
                    CategoryId = iCategoryId,
                    Context = Body,
                    StatusId = 1, // Initial will always be 1:: Related with FK relationship
                    RecipentId = iRecepintid

                };
                oAlertViewer = controller.AlertInsert(oAlertViewer);

                //Add Status to that alert

                var iDatabaseResponse = AddAlertHistory(oAlertViewer.Id, oAlertViewer.StatusId);

                return Request.CreateResponse(HttpStatusCode.OK, iDatabaseResponse > 0 ? "Success" : "Some Issue");
            }
            catch (Exception exc)
            {
                // Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }


        [HttpGet]
        public HttpResponseMessage MarkAsDelivered(int AlertID)
        {
            return ChangeStatus(AlertID, (int)AlertEnum.Status.DELIVERED);
        }

        [HttpGet]
        public HttpResponseMessage MarkAsViewed(int AlertID)
        {
            return ChangeStatus(AlertID, (int)AlertEnum.Status.VIEWED);
        }
        [HttpGet]
        public HttpResponseMessage MarkAsDismissed(int AlertID)
        {
            return ChangeStatus(AlertID, (int)AlertEnum.Status.DISMISSED);
        }
        [HttpGet]
        public HttpResponseMessage MarkAsDeleted(int AlertID)
        {
            return ChangeStatus(AlertID, (int)AlertEnum.Status.DELETED);
        }

        [HttpGet]
        public HttpResponseMessage UpdateAlertStatus(int AlertID, int StatusID)
        {
            return ChangeStatus(AlertID, StatusID);
        }


        [HttpGet]
        public HttpResponseMessage GetAlert(int AlertID)
        {
            try
            {
                var controller = new DbController();
                var oCurrentuser = GetCurrentUser;
                if (AlertID > 0 && oCurrentuser.UserID > 0)
                {
                    var otemp = controller.AlertGet(AlertID);
                    if (otemp == null)
                        return Request.CreateResponse(HttpStatusCode.OK, "Invalid RecordId");

                    var olist = controller.AlertGet(AlertID);

                    return Request.CreateResponse(HttpStatusCode.OK, new JavaScriptSerializer().Serialize(olist));
                }

                return Request.CreateResponse(HttpStatusCode.OK, "No Action");
            }
            catch (Exception exc)
            {
                // Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        /// <summary>
        /// Simply listing of records by currentpage,pagesize
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAlerts(int CurrentPage, int PageSize)
        {
            try
            {
                var controller = new DbController();
                var oCurrentuser = GetCurrentUser;
                if (oCurrentuser.UserID > 0)
                {

                    var olist = controller.AlertGets().Skip(CurrentPage * PageSize).Take(PageSize);

                    return Request.CreateResponse(HttpStatusCode.OK, new JavaScriptSerializer().Serialize(olist));
                }

                return Request.CreateResponse(HttpStatusCode.OK, "No Action");
            }
            catch (Exception exc)
            {
                // Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        /// <summary>
        /// To-do: Some more query need to test :Ajit
        /// </summary>
        /// <param name="IReceipitd"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAlertByRecipentID(int IReceipitd, int CurrentPage, int PageSize)
        {
            try
            {
                var controller = new DbController();
                var oCurrentuser = GetCurrentUser;
                if (oCurrentuser.UserID > 0)
                {

                    var olist = controller.AlertGets().Skip(CurrentPage * PageSize).Take(PageSize);

                    return Request.CreateResponse(HttpStatusCode.OK, new JavaScriptSerializer().Serialize(olist));
                }

                return Request.CreateResponse(HttpStatusCode.OK, "No Action");
            }
            catch (Exception exc)
            {
                // Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }




        /// <summary>
        /// Under construction: More filteration and testing need: Ajit 27/02
        /// </summary>
        /// <param name="iSenderBy"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAlertBySenderID(int iSenderBy, int CurrentPage, int PageSize)
        {
            try
            {
                var controller = new DbController();
                var oCurrentuser = GetCurrentUser;
                if (oCurrentuser.UserID > 0)
                {

                    var olist = controller.AlertGets().Skip(CurrentPage * PageSize).Take(PageSize);

                    return Request.CreateResponse(HttpStatusCode.OK, new JavaScriptSerializer().Serialize(olist));
                }

                return Request.CreateResponse(HttpStatusCode.OK, "No Action");
            }
            catch (Exception exc)
            {
                // Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAlertByCategory(int iCategoryID, int CurrentPage, int PageSize)
        {
            try
            {
                var controller = new DbController();
                var oCurrentuser = GetCurrentUser;
                if (oCurrentuser.UserID > 0)
                {

                    var olist = controller.AlertGets().Skip(CurrentPage * PageSize).Take(PageSize);

                    return Request.CreateResponse(HttpStatusCode.OK, new JavaScriptSerializer().Serialize(olist));
                }

                return Request.CreateResponse(HttpStatusCode.OK, "No Action");
            }
            catch (Exception exc)
            {
                // Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAlertByStatusID(int StatusID, int CurrentPage, int PageSize)
        {
            try
            {
                var controller = new DbController();
                var oCurrentuser = GetCurrentUser;
                if (oCurrentuser.UserID > 0)
                {

                    var olist = controller.AlertGets().Skip(CurrentPage * PageSize).Take(PageSize);

                    return Request.CreateResponse(HttpStatusCode.OK, new JavaScriptSerializer().Serialize(olist));
                }

                return Request.CreateResponse(HttpStatusCode.OK, "No Action");
            }
            catch (Exception exc)
            {
                // Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        #endregion




        #region DTO

        public class FollowDTO
        {
            public int FollowId { get; set; }
        }

        public class FriendDTO
        {
            public int FriendId { get; set; }
        }

        #endregion
    }
}