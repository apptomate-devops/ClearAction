/*
' Copyright (c) 2017 ClearAction
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Roles;
using DotNetNuke.Entities.Portals;
using System;
namespace ClearAction.Modules.Dashboard.Components
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for ClearAction_Dashboard
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class DashboardController//ModuleSearchBase, IPortable, IUpgradeable
    {

        public string ExeculdeRoles
        {
            get
            {
                string strConfigVaulue = System.Configuration.ConfigurationManager.AppSettings["Exculde_Role"];
                if (string.IsNullOrEmpty(strConfigVaulue))
                    strConfigVaulue = "Administrators, Registered Users, Subscribers, Translator (en-US),Unverified Users";
                return strConfigVaulue;
            }
        }


        public List<UserInfo> GetUsersByCurrentRoleName(int iPortalid, string CurrentRoleName)
        {
            var oListofuser = RoleController.Instance.GetUsersByRole(iPortalid, CurrentRoleName);
            oListofuser = oListofuser.Where(x => x.IsDeleted == false || x.IsSuperUser == false).ToList();

            return RemoveSuperUserAndSelf(oListofuser.ToList()).ToList();
        }
        public string GetcurrentUserRoleName(int iPortalid, int iUserID)
        {
            string[] strRole = UserController.GetUserById(iPortalid, iUserID).Roles;
            string strRoleToExecludes = ExeculdeRoles;
            string struserrole = string.Empty;
            foreach (string strrolename in strRole)
            {
                if (!strRoleToExecludes.Contains(strrolename))
                {
                    struserrole = strrolename;
                }


            }
            return struserrole;
        }

        public string GetResponseText(int Portalid, int iUserID, string Key)
        {

            string QuestionID = PortalController.GetPortalSetting(Key, Portalid, string.Empty);
            if (string.IsNullOrEmpty(QuestionID))
                return "";

            var oData = new DBController().GetQuestionResponseText(iUserID, Convert.ToInt32(QuestionID)).SingleOrDefault();
            if (oData != null)
            {
                return oData.ResponseText;
            }
            return "";
        }
        public string GetResponseText(int Portalid, int iUserID, string Key, bool IsCompany)
        {

            string QuestionID = GetResponseText(Portalid, iUserID, Key);
            if (string.IsNullOrEmpty(QuestionID))
                return "";


            try
            {
                return new DBController().GetCompanyName(Convert.ToInt32(QuestionID));
            }
            catch (Exception ex)
            {


            }
            return "";
        }

        private IEnumerable<UserInfo> FilterExcludedUsers(IEnumerable<UserInfo> users)
        {
            return users.Where(x => !x.IsDeleted && !x.IsSuperUser && !(x.Membership.LastLoginDate == x.Membership.CreatedDate)).Select(u => u).ToList();
        }
        private UserInfo GetActiveUser(int Portalid, int iUserID)
        {

            var oUser = GetUserByUserID(Portalid, iUserID);
            //Don't add deleted/super user or admin account on that list
            if (oUser == null)
                return null;
            if (oUser.IsDeleted || oUser.IsSuperUser || oUser.IsInRole("Administrator") || (oUser.Membership.LastLoginDate == oUser.Membership.CreatedDate))
                return null;
            return oUser;
        }
        private UserInfo GetUserByUserID(int Portalid, int iUserID)
        {
            if (iUserID < 0)
                return null;

            Portalid = Portalid > 0 ? Portalid : 0;


            var oUser = new UserController().GetUser(Portalid, iUserID);

            return oUser;
        }
        /// <summary>
        /// Get User Connection MyExchange
        /// Modify: Anuj 
        /// Removing deleted user and super user
        /// </summary>
        /// <param name="Portalid"></param>
        /// <param name="UserID"></param>
        /// <param name="NoOfRecord"></param>
        /// <returns></returns>
        public List<UserInfo> GetConnectionList(int Portalid, int UserID, int NoOfRecord)
        {
            var oData = new DBController().UserFavoriteInfo(UserID).ToList();


            List<UserInfo> lstListofUser = new List<UserInfo>();


            List<int> lstUserID = new List<int>();
            int icounter = 0;

            //Bind fav record first
            foreach (UserFavoriteInfo o in oData)
            {
                var oUser = GetActiveUser(Portalid, o.RelatedUserID);
                if (oUser != null && icounter <= NoOfRecord)
                {
                    lstListofUser.Add(oUser);
                    icounter++;
                }
            }

            //Bind from connection remaining records
            if (lstListofUser.Count < NoOfRecord)
            {
                //Get from connections
                var oTempdata = GetConnectionList(Portalid, UserID, NoOfRecord, true);
                if (oTempdata != null)
                {
                    foreach (int iUser in oTempdata)
                    {

                        var oUser = GetActiveUser(Portalid, iUser);
                        if (!lstListofUser.Contains(oUser))
                        {
                            if (oUser != null && icounter < NoOfRecord)
                            {
                                icounter++;
                                lstListofUser.Add(oUser);
                            }
                        }
                    }



                }
            }

            return lstListofUser;
        }

        /// <summary>
        /// Get Social connection
        /// </summary>
        /// <param name="Portalid"></param>
        /// <param name="UserID"></param>
        /// <param name="NoOfRecord"></param>
        /// <param name="ConnectionOnly"></param>
        /// <returns></returns>
        private List<int> GetConnectionList(int Portalid, int UserID, int NoOfRecord, bool ConnectionOnly)
        {
            var oData = new DBController().GetUserConnections(UserID).ToList();
            List<int> lstUserID = new List<int>();
            foreach (UserRelationshipInfo o in oData)
            {
                if (o.RelatedUserID == UserID)
                {
                    if (!lstUserID.Contains(o.UserID))
                        lstUserID.Add(o.UserID);
                }
                else
                {
                    if (!lstUserID.Contains(o.UserID))
                        lstUserID.Add(o.RelatedUserID);
                }
            }
            return lstUserID;


        }

        public string CheckUserIsFriendofCurrentuser(int toUserID)
        {
            var iCurrentUser = UserController.Instance.GetCurrentUserInfo().UserID;
            var oData = new DBController().GetUserConnections(toUserID).Where(
               (x => x.RelatedUserID == iCurrentUser
               ||
               x.UserID == iCurrentUser)


                ).Where(x => x.Status == 2).SingleOrDefault();

            if (oData == null) return "false";
            return "true";
        }



        private List<UserInfo> RemoveSuperUserAndSelf(List<UserInfo> users)
        {
            var iCurrentUser = UserController.Instance.GetCurrentUserInfo().UserID;
            return users.Where(u => !u.IsSuperUser).Where(u => u.UserID != iCurrentUser).Select(u => u).ToList().OrderBy(x => x.LastName).ToList();
        }

        public List<UserInfo> GetSimilarToCompany(int Portalid, int UserID, int NoOfRecord)
        {

            //Get Current User Company Answer from section
            string strCompanyName = PortalController.GetPortalSetting("CompanyName", Portalid, string.Empty);
            if (string.IsNullOrEmpty(strCompanyName))
                return null;

            var lstUserID = new DBController().GetListOfUserIDSimilarByCompany(UserID, Convert.ToInt32(strCompanyName));

            if (lstUserID == null || lstUserID.Count == 0)
                return null;
            List<UserInfo> lstListofUser = new List<UserInfo>();
            int iCount = 0;
            foreach (DTOUser oDtoUser in lstUserID)
            {
                //Validate and add to list
                var oUser = GetActiveUser(Portalid, oDtoUser.UserID);
                if (oUser != null && iCount <= NoOfRecord)
                {
                    lstListofUser.Add(oUser); iCount++;
                }
            }



            return lstListofUser.Take(NoOfRecord).ToList();

        }



    }
}