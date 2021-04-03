
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

namespace ClearAction.Modules.Connections
{

    //[DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
    [DnnAuthorize]
    public class ConnectionsController : DnnApiController
    {

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
        public HttpResponseMessage GetMember(int userId, string SortBy)
        {
            try
            {
                var users = new List<UserInfo>();
                var user = UserController.GetUserById(PortalSettings.PortalId, userId);
                users.Add(user);

                return Request.CreateResponse(HttpStatusCode.OK, GetMembers(users, SortBy, 0, 10, "-1", "0"));
            }
            catch (Exception exc)
            {
                // Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        [HttpGet]
        public HttpResponseMessage AdvancedSearch(int userId, int groupId, int pageIndex, int pageSize, string searchTerm1, string searchTerm2, string searchTerm3, string searchTerm4, string SortBy, int TotalCount, string ViewType, string FilterBy)
        {
            try
            {
                if (userId < 0) userId = PortalSettings.UserId;

                var searchField1 = "DisplayName";
                var searchField2 = "Email";
                var searchField3 = "City";
                var searchField4 = "Country";
                var propertyNames = "";
                var propertyValues = "";
                AddSearchTerm(ref propertyNames, ref propertyValues, searchField1, searchTerm1);
                AddSearchTerm(ref propertyNames, ref propertyValues, searchField2, searchTerm2);
                AddSearchTerm(ref propertyNames, ref propertyValues, searchField3, searchTerm3);
                AddSearchTerm(ref propertyNames, ref propertyValues, searchField4, searchTerm4);
                JavaScriptSerializer js = new JavaScriptSerializer();

                return Request.CreateResponse(HttpStatusCode.OK, GetMembers(
                                                                    GetUsers(userId, groupId, searchTerm1, pageIndex, pageSize, propertyNames, propertyValues, ref TotalCount), SortBy, pageIndex, pageSize, ViewType, FilterBy));
            }
            catch (Exception exc)
            {
                // Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }
        [HttpGet]
        public HttpResponseMessage BasicSearch(int groupId, string searchTerm, int pageIndex, int pageSize, string SortBy, int TotalCount, string ViewType, string FilterBy)
        {
            try
            {

                var users = GetUsers(PortalSettings.UserId, groupId, string.IsNullOrEmpty(searchTerm) ? string.Empty : searchTerm.Trim(), pageIndex, pageSize, "", "", ref TotalCount);
                return Request.CreateResponse(HttpStatusCode.OK, GetMembers(users, SortBy, pageIndex, pageSize, ViewType, FilterBy));
            }
            catch (Exception exc)
            {
                //  Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }


        [HttpGet]
        public HttpResponseMessage GetSuggestions(int groupId, string displayName)
        {
            try
            {


                var oCurrentUser = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo();

                var oUser = UserController.Instance.GetUsersBasicSearch(PortalSettings.PortalId, 0, 100, "DisplayName", true, "DisplayName", string.IsNullOrEmpty(displayName) ? string.Empty : displayName);

                var names = (from UserInfo user in oUser
                             select new { label = user.DisplayName, value = user.DisplayName, userId = user.UserID })
                                .ToList();
                names = names.OrderBy(x => x.label).ToList();


                return Request.CreateResponse(HttpStatusCode.OK, names);
            }
            catch (Exception exc)
            {
                //  Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage AcceptFriend(FriendDTO postData)
        {
            try
            {
                var friend = UserController.GetUserById(PortalSettings.PortalId, postData.FriendId);
                FriendsController.Instance.AcceptFriend(friend);
                return Request.CreateResponse(HttpStatusCode.OK, new { Result = "success" });
            }
            catch (Exception exc)
            {
                ///Logger.Error(exc);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        [HttpPost]
        //     [ValidateAntiForgeryToken]
        public HttpResponseMessage AddFriend(FriendDTO postData)
        {
            try
            {
                var friend = UserController.GetUserById(PortalSettings.PortalId, postData.FriendId);
                // FriendsController.Instance.AddFriend(friend);
                var oDBController = new DbController();
                oDBController.AddFriend(PortalSettings.PortalId, GetCurrentUser, friend);

                string Body = ReadTemplate("ConnectionRequest");
                Body = ReplaceToken(Body, InitilizeMember(GetCurrentUser), true);
                Body = ReplaceToken(Body, InitilizeMember(friend), false);

                //  DotNetNuke.Services.Mail.Mail.SendEmail(PortalSettings.Email, friend.Email, "New message from Clear Action", Body);

                SendEmailOrMessage(GetCurrentUser.UserID, friend.UserID, "New message from Clear Action", Body, "", "", "1");
                return Request.CreateResponse(HttpStatusCode.OK, new { Result = "success" });
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }



        [HttpPost]
        //     [ValidateAntiForgeryToken]
        public HttpResponseMessage AddFaviourate(FriendDTO postData)
        {
            try
            {
                var friend = UserController.GetUserById(PortalSettings.PortalId, postData.FriendId);
                if (friend != null)
                {

                    var oCount = new DbController().GetUserfavoriteRecord(GetCurrentUser.UserID, true);
                    if (oCount != null && oCount.Count >= HelperController.MaxFav)
                        return Request.CreateResponse(HttpStatusCode.OK, new { Result = "limitcount" });
                    var odata = new DbController().FavoriteRecord(GetCurrentUser.UserID, postData.FriendId, true);
                    if (odata)
                        return Request.CreateResponse(HttpStatusCode.OK, new { Result = "success" });

                }

                return Request.CreateResponse(HttpStatusCode.OK, new { Result = "Failed" });
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        [HttpPost]
        //     [ValidateAntiForgeryToken]
        public HttpResponseMessage RemoveFaviourate(FriendDTO postData)
        {
            try
            {
                var friend = UserController.GetUserById(PortalSettings.PortalId, postData.FriendId);
                if (friend != null)
                {


                    var odata = new DbController().FavoriteRecord(GetCurrentUser.UserID, postData.FriendId, false);
                    if (odata)
                        return Request.CreateResponse(HttpStatusCode.OK, new { Result = "success" });

                }

                return Request.CreateResponse(HttpStatusCode.OK, new { Result = "Failed" });
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Follow(FollowDTO postData)
        {
            try
            {
                var follow = UserController.GetUserById(PortalSettings.PortalId, postData.FollowId);
                FollowersController.Instance.FollowUser(follow);
                return Request.CreateResponse(HttpStatusCode.OK, new { Result = "success" });
            }
            catch (Exception exc)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        [HttpPost]
        //    [ValidateAntiForgeryToken]
        public HttpResponseMessage RemoveFriend(FriendDTO postData)
        {
            try
            {

                var friend = UserController.GetUserById(PortalSettings.PortalId, postData.FriendId);
                FriendsController.Instance.DeleteFriend(friend);
                //Delete from Connection will remove user from Fav as well.

                return RemoveFaviourate(postData);
                //   return Request.CreateResponse(HttpStatusCode.OK, new { Result = "success" });
            }
            catch (Exception exc)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }



        [HttpGet]
        // [ValidateAntiForgeryToken]
        public HttpResponseMessage SendMessage(int FromUserID, int ToUserID, string Subject, string Body, string Filename, string AttachLink)
        {
            try
            {



                var oFromUser = UserController.Instance.GetUserById(0, FromUserID);
                var oToUser = UserController.Instance.GetUserById(0, ToUserID);
                if (oToUser == null || oFromUser == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Result = "Invalid Operation" });



                int iConversionid = -1;

                var lToUserMessage = new DbController().GetMessage(FromUserID).Where(x => x.ToAccount == oToUser.UserID).Take(1).ToList();
                if (lToUserMessage != null && lToUserMessage.Count > 0)
                    iConversionid = lToUserMessage.SingleOrDefault().ConversationID;
                if (iConversionid == -1)
                {
                    var lfromUserMessage = new DbController().GetMessage(ToUserID).Where(x => x.ToAccount == oFromUser.UserID).Take(1).SingleOrDefault();
                    if (lfromUserMessage != null)
                        iConversionid = lfromUserMessage.ConversationID;

                }


                var oMessageinfo = new MessageInfo()
                {
                    Archived = false,
                    Body = Body,
                    Context = "",
                    ConversationID = iConversionid,
                    CreatedByUserID = oFromUser.UserID,
                    CreatedOnDate = System.DateTime.Now,
                    EmailSent = false,
                    EmailSentDate = System.DateTime.Now,
                    ExpirationDate = System.DateTime.Now,
                    From = oFromUser.DisplayName,
                    IncludeDismissAction = true,
                    LastModifiedByUserID = oFromUser.UserID,
                    LastModifiedOnDate = System.DateTime.Now,
                    MessageID = -1,
                    NotificationTypeID = 26,
                    PortalID = PortalSettings.PortalId,
                    Read = false,
                    ReplyAllAllowed = true,
                    SenderUserID = oFromUser.UserID,
                    Subject = Subject,
                    To = oToUser.DisplayName,
                    ToAccount = oToUser.UserID,
                    UserId = oFromUser.UserID
                };
                if (!string.IsNullOrEmpty(Filename) && !string.IsNullOrEmpty(AttachLink))
                {
                    oMessageinfo.Body = oMessageinfo.Body.Contains("<Attach>") == true ? oMessageinfo.Body.Replace("<Attach>", "") : oMessageinfo.Body;
                    oMessageinfo.Body += " <Attach>" + string.Format(@"<a href=""{0}"" target=""_blank""  onclick=""window.open('{0}')"" >{1}</a>", AttachLink, Filename);
                    oMessageinfo.Context = Filename + "&&" + AttachLink;

                }
                new DbController().AddMessage(oMessageinfo);


                return Request.CreateResponse(HttpStatusCode.OK, new { Result = "success" });
                //   DotNetNuke.Services.Messaging.Data.Message m = new DotNetNuke.Services.Messaging.Data.Message();


            }
            catch (Exception exc)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }


        [HttpGet]
        public HttpResponseMessage GetConnectionRequest(int SortBy)
        {

            var oCurrent = GetCurrentUser;
            var odata = new DbController().GetRequestByStatus(oCurrent.UserID, 1);
            List<Member> lstMember = new List<Member>();
            foreach (UserRelationships o in odata)
            {
                var omember = InitilizeMember(UserController.GetUserById(PortalSettings.PortalId, o.UserID));
                omember.ConnectionRequestdate = o.Requestdateformat;
                lstMember.Add(omember);

            }

            lstMember = lstMember.OrderBy(x => x.OrderValue == SortBy).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, lstMember);


        }

        [HttpGet]
        public HttpResponseMessage GetMessage(int friendId)
        {
            try
            {


                var odata = UserController.Instance.GetUserById(PortalSettings.PortalId, friendId);
                var member = InitilizeMember(odata);

                if (member == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Result = "Invalid Operation" });

                return Request.CreateResponse(HttpStatusCode.OK, member);
                //   DotNetNuke.Services.Messaging.Data.Message m = new DotNetNuke.Services.Messaging.Data.Message();


            }
            catch (Exception exc)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }
        [HttpGet]
        // [ValidateAntiForgeryToken]
        public HttpResponseMessage SendEmail(int FromUserID, int ToUserID, string Subject, string Body, string Filename, string AttachLink)
        {
            try
            {

                return SendEmailOrMessage(FromUserID, ToUserID, Subject, Body, Filename, AttachLink, "0");


            }
            catch (Exception exc)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public HttpResponseMessage BlockUser(FriendDTO postData)
        {
            try
            {
                var oCurrentUser = GetCurrentUser;
                var friend = UserController.GetUserById(PortalSettings.PortalId, postData.FriendId);
                if (friend == null || GetCurrentUser.UserID == -1)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Result = "Error" });


                var oBlock = new UserBlock()
                {
                    CreatedDateTime = System.DateTime.Now,
                    FromUserId = oCurrentUser.UserID,
                    RequestID = -1,
                    ToUserID = friend.UserID
                };



                new DbController().BlockUserInsert(oBlock);

                //Send Email to Admin on blocking of user

                string strSubject = "User Block Notification from :" + oCurrentUser.DisplayName;
                string strBody = string.Format("Dear Admin,<br/> I'm sending request to block <a href='{0}/MyProfile/UserName/{1}'>{2}</a> on Clear action.<br/><br/><br/> Thank you, <a href='{0}/MyProfile/UserName/{2}'>{3}</a>", PortalSettings.PortalAlias.HTTPAlias.ToString(), friend.Username, friend.DisplayName, oCurrentUser.Username, oCurrentUser.DisplayName);

                var oMessage = new DotNetNuke.Services.Social.Messaging.Message()
                {
                    Body = strBody,
                    // Subject=strub
                };


                return Request.CreateResponse(HttpStatusCode.OK, new { Result = "success" });

                //return SendEmailOrMessage(FromUserID, ToUserID, Subject, Body, Filename, AttachLink, "0");


            }
            catch (Exception exc)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }


        [HttpGet]
        // [ValidateAntiForgeryToken]
        public HttpResponseMessage SendEmailOrMessage(int FromUserID, int ToUserID, string Subject, string Body, string Filename, string AttachLink, string sEmail)
        {
            try
            {
                var oFromUser = UserController.Instance.GetUserById(0, FromUserID);
                var oToUser = UserController.Instance.GetUserById(0, ToUserID);
                if (oToUser == null || oFromUser == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Result = "Invalid Operation" });

                var oEmailSchedular = new EmailScheduler()
                {
                    AttachFileLink = AttachLink,
                    AttachFileName = Filename,
                    //Body = EmailParsing(Body, AttachLink, Filename, oToUser, oFromUser),
                    Body = Body,
                    CreateOnDate = System.DateTime.Now,
                    IsSent = false,
                    ReceiverEmail = oToUser.Email,
                    RecieverName = oToUser.DisplayName,
                    SenderEmail = oFromUser.Email,
                    SenderName = oFromUser.DisplayName,
                    SentOnDate = System.DateTime.Now,
                    Subject = Subject
                };

                new DbController().AddEmail(oEmailSchedular);


                if (!string.IsNullOrEmpty(AttachLink))
                    Body = Body + string.Format("<br/> <b>Attachment</b><br/><br/><a href='{0}' taret='_blank'>{1}</a>", AttachLink, Filename);


                // DotNetNuke.Services.Mail.Mail.SendMail(oFromUser.Email, oToUser.Email, "", Subject, EmailParsing(Body, AttachLink, Filename, oToUser, oFromUser), "", "HTML", "", "", "", "");

                return Request.CreateResponse(HttpStatusCode.OK, new { Result = "success" });
            }
            catch (Exception exc)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage IngoreFriend(FriendDTO postData)
        {
            try
            {
                var friend = UserController.GetUserById(PortalSettings.PortalId, postData.FriendId);
                FriendsController.Instance.DeleteFriend(friend);
                return Request.CreateResponse(HttpStatusCode.OK, new { Result = "success" });
            }
            catch (Exception exc)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage UnFollow(FollowDTO postData)
        {
            try
            {
                var follow = UserController.GetUserById(PortalSettings.PortalId, postData.FollowId);
                FollowersController.Instance.UnFollowUser(follow);
                return Request.CreateResponse(HttpStatusCode.OK, new { Result = "success" });
            }
            catch (Exception exc)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc);
            }
        }


        #endregion

        #region "Email Template"
        public string ReadTemplate(string FileName)
        {
            string strFilePath = DotNetNuke.Common.Globals.ApplicationMapPath + string.Format("/DesktopModules/ClearAction_Connections/Templates/{0}.html", FileName);
            if (System.IO.File.Exists(strFilePath))
                return System.IO.File.ReadAllText(strFilePath);
            return "";
        }

        public string ReplaceToken(string data, Member member, bool issender)
        {

            if (issender)
                data = data.Replace("SENDER:", "");
            else
                data = data.Replace("TO:", "");

            data = data.Replace("{EMAIL}", member.Email);
            data = data.Replace("{DISPLAYNAME}", member.DisplayName);
            data = data.Replace("{PHONE}", member.Phone);
            data = data.Replace("{TITLE}", member.JobTitle);
            data = data.Replace("{COMPANYNAME}", member.CompanyName);
            data = data.Replace("{PROFILEURL}", string.Format("//{0}/MyProfile/UserName/{1}", PortalSettings.PortalAlias.HTTPAlias, member.UserName));
            data = data.Replace("{PROFILERELATIVEURL}", string.Format("MyProfile/UserName/{0}", member.UserName));
            data = data.Replace("{LOCATION}", member.Location);
            data = data.Replace("{PORTALURL}", DotNetNuke.Common.Globals.NavigateURL(PortalSettings.HomeTabId));
            data = data.Replace("{PIC}", member.PhotoURL);
            return data;
        }
        public string EmailParsing(string strEmailBody, string attachLink, string attachFilename, UserInfo oToUser, UserInfo oFromUser)
        {

            string strTemplate = ReadTemplate("EmailTemplate");
            var oToMember = InitilizeMember(oToUser);
            var oFromMember = InitilizeMember(oFromUser);

            string data = ReplaceToken(strTemplate, oToMember, false);
            data = ReplaceToken(data, oFromMember, true);
            data = data.Replace("{BODY}", strEmailBody);
            if (string.IsNullOrEmpty(attachLink))
                data = data.Replace("{ATTACH}", "");
            else
                data = data.Replace("{ATTACH}", string.Format("<a href='{0}'>{1}</a>", attachLink, attachFilename));
            return data;

        }

        private IEnumerable<Member> DoSort(IEnumerable<Member> oFilteruser, string iSortBy)
        {
            switch (iSortBy)
            {
                case "0":
                case "1":
                    oFilteruser = oFilteruser.OrderByDescending(x => x.CreatedonDate);
                    break;

                case "2":
                    oFilteruser = oFilteruser.OrderBy(x => x.LastName);
                    break;

                case "3":
                    oFilteruser = oFilteruser.OrderByDescending(x => x.LastName);
                    break;

                case "4":
                    oFilteruser = oFilteruser.OrderByDescending(x => x.LastName); break;

                case "5":
                    oFilteruser = oFilteruser.OrderByDescending(x => x.CompanyId); break;

            }
            return oFilteruser;
        }

        private UserInfo GetCurrentUser
        {
            get
            {
                return DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo();
            }
        }



        #endregion
        private Member InitilizeMember(UserInfo ouser)
        {

            var oCurrentUser = GetCurrentUser;

            List<MessageInfo> _GetMessage = new DbController().GetMessage(GetCurrentUser.UserID);
            //  List<UserBlock> _GetBlockedUser = new DbController().GetBlockListByUser(GetCurrentUser.UserID);
            var omember = new Member(ouser, PortalSettings, oCurrentUser, _GetMessage, null);
            return omember;
        }

        private IList<Member> GetMembers(IEnumerable<UserInfo> users, string iSortBy, int PageIndex, int PageSize, string iViewType, string FilterType)
        {


            var oCurrentUser = GetCurrentUser;// DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo();
            var oFilteruser = users.Select(user => InitilizeMember(user));

            var oCurrentMember = InitilizeMember(oCurrentUser);

            //Do Filter Friends/Roles/Company/All

            switch (FilterType)
            {
                case "1":
                    oFilteruser = oFilteruser.Where(x => (x.FriendStatus == 2 || x.FriendStatus == 1));
                    break;
                case "2":
                    oFilteruser = oFilteruser.Where(x => x.RoleSimilarToMine == true);
                    break;
                case "3":
                    oFilteruser = oFilteruser.Where(x => x.CompanySimilarToMine == true);
                    break;
                case "4":
                    oFilteruser = oFilteruser.Where(x => x.FriendStatus == 0);
                    break;

                default:

                    break;
            }



            if (iViewType == "1")
            {

                //Add Those user that having same company in tempholder
                List<Member> oTempHolder = new List<Member>();

                //First sort basis of companyid
                oFilteruser = DoSort(oFilteruser, "5");

                //Then do sort basis of last name
                iSortBy = iSortBy == "0" ? "4" : iSortBy;
                oFilteruser = DoSort(oFilteruser, iSortBy);



                foreach (Member m in oFilteruser.ToList())
                {
                    if (oCurrentMember.CompanyId == m.CompanyId)
                        oTempHolder.Add(m);
                }

                //Iterate the loop again to bind another user


                foreach (Member m in oFilteruser.ToList())
                {
                    if (!oTempHolder.Contains(m))
                        oTempHolder.Add(m);
                }
                oFilteruser = oTempHolder;//.Skip(PageIndex * PageSize).Take(PageSize);

            }

            //Default sorting based on U
            oFilteruser = DoSort(oFilteruser, iSortBy);
            //Order Status
            //1 . Fav connection
            //2.  Waiting Connection
            //3.  Connection
            //4. Pending Connection

            oFilteruser = oFilteruser.OrderByDescending(x => x.OrderValue);
            oFilteruser = oFilteruser.Skip(PageIndex * PageSize).Take(PageSize);
            return oFilteruser.ToList();

        }
        private IEnumerable<UserInfo> GetUsers(int userId, int groupId, string searchTerm, int pageIndex, int pageSize, string propertyNames, string propertyValues, ref int totalRecord)
        {
            var portalId = PortalSettings.PortalId;
            var isAdmin = PortalSettings.UserInfo.IsInRole(PortalSettings.AdministratorRoleName);

            var filterBy = "";// GetSetting(ActiveModule.ModuleSettings, "FilterBy", String.Empty);
            var filterValue = "";// GetSetting(ActiveModule.ModuleSettings, "FilterValue", String.Empty);

            if (filterBy == "Group" && filterValue == "-1" && groupId > 0)
            {
                filterValue = groupId.ToString();
            }

            var sortField = "CreatedOnDate";
            sortField = "";

            totalRecord = -1;
            // pageSize = 250000;

            //if (sortField.Equals("CreatedOnDate", StringComparison.InvariantCultureIgnoreCase))
            //{
            //    sortField = "UserId";
            //}

            var sortOrder = "ASC";// GetSetting(ActiveModule.TabModuleSettings, "SortOrder", "ASC");

            var excludeHostUsers = true;// Boolean.Parse(GetSetting(ActiveModule.TabModuleSettings, "ExcludeHostUsers", "false"));
            var isBasicSearch = false;
            if (String.IsNullOrEmpty(propertyNames))
            {
                isBasicSearch = true;
                // AddSearchTerm(ref propertyNames, ref propertyValues, "DisplayName", searchTerm);
            }

            IList<UserInfo> users;
            switch (filterBy)
            {
                case "User":
                    users = new List<UserInfo> { UserController.GetUserById(portalId, userId) };
                    break;
                case "Group":
                    if (groupId == -1)
                    {
                        groupId = Int32.Parse(filterValue);
                    }
                    if (CanViewGroupMembers(portalId, groupId))
                    {
                        users = UserController.Instance.GetUsersAdvancedSearch(portalId, userId, -1,
                                                                                       Int32.Parse(filterValue),
                                                                                       -1, isAdmin, pageIndex, pageSize,
                                                                                       sortField, (sortOrder == "ASC"),
                                                                                       propertyNames, propertyValues);

                    }
                    else
                    {
                        users = new List<UserInfo>();
                    }
                    break;
                case "Relationship":
                    users = UserController.Instance.GetUsersAdvancedSearch(portalId, userId, userId, -1,
                                                                           Int32.Parse(filterValue), isAdmin, pageIndex, pageSize,
                                                                           sortField, (sortOrder == "ASC"),
                                                                           propertyNames, propertyValues);
                    break;
                case "ProfileProperty":
                    var propertyValue = "";// GetSetting(ActiveModule.ModuleSettings, "FilterPropertyValue", String.Empty);
                    AddSearchTerm(ref propertyNames, ref propertyValues, filterValue, propertyValue);

                    users = UserController.Instance.GetUsersAdvancedSearch(portalId, userId, -1, -1,
                                                                           -1, isAdmin, pageIndex, pageSize,
                                                                           sortField, (sortOrder == "ASC"),
                                                                           propertyNames, propertyValues);
                    break;
                default:
                    if (isBasicSearch)
                    {
                        if (string.IsNullOrEmpty(searchTerm))
                        {
                            var arUser = UserController.GetUsers(portalId);
                            users = arUser.Cast<UserInfo>().ToList();

                        }
                        else
                        {

                            users = UserController.Instance.GetUsersBasicSearch(portalId, pageIndex, pageSize, "DisplayName", true, "DisplayName", searchTerm);
                        }
                    }
                    else
                    {
                        users = UserController.Instance.GetUsersAdvancedSearch(portalId, PortalSettings.UserId, -1, -1,
                                                                              -1, isAdmin, pageIndex, pageSize,
                                                                              sortField, (sortOrder == "ASC"),
                                                                              propertyNames, propertyValues);
                    }





                    break;
            }
            if (excludeHostUsers)
            {
                return FilterExcludedUsers(users);
            }
            return users;
        }
        private IEnumerable<UserInfo> FilterExcludedUsers(IEnumerable<UserInfo> users)
        {
            return users.Where(u => !u.IsSuperUser).Select(u => u).ToList();
        }
        private static void AddSearchTerm(ref string propertyNames, ref string propertyValues, string name, string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                propertyNames += name + ",";
                propertyValues += value + ",";
            }
        }


        private bool CanViewGroupMembers(int portalId, int groupId)
        {
            return true;

        }



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