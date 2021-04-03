/*
' Copyright (c) 2017  Ajit jha
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using DotNetNuke.Entities.Modules;
using System.Collections;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Users;

namespace ClearAction.Modules.Connections
{
    public class ConnectionsModuleBase : PortalModuleBase
    {

        protected int OrderBy
        {
            get
            {

                int iOrderBy = -1;

                try
                {
                    int.TryParse(Convert.ToString(Request.QueryString["OrderBy"]), out iOrderBy);
                }
                catch (Exception exc)
                {


                }

                return iOrderBy;

            }
        }

        /// <summary>
        /// ViewType= 1 : People in My Company, 2: People in MyConnection ,3: People in RoleSimilar to Mine
        /// </summary>
        protected int ViewType
        {
            get
            {

                int iViewType = 1;

                try
                {
                    int.TryParse(Convert.ToString(Request.QueryString["ViewType"]), out iViewType);
                }
                catch (Exception exc)
                {


                }

                return iViewType;

            }
        }
        protected string FilterBy
        {
            get { return GetSetting(ModuleContext.Configuration.ModuleSettings, "FilterBy", "None"); }
        }
        public int ProfileUserId
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.Params["UserId"]))
                {
                    return Int32.Parse(Request.Params["UserId"]);
                }

                return UserController.Instance.GetCurrentUserInfo().UserID;
            }
        }

        protected bool DisablePrivateMessage
        {
            get
            {
                return PortalSettings.DisablePrivateMessage && !UserInfo.IsSuperUser
                    && !UserInfo.IsInRole(PortalSettings.AdministratorRoleName);

            }
        }
        protected string ViewProfileUrl
        {
            get
            {
                return DotNetNuke.Common.Globals.NavigateURL(ModuleContext.PortalSettings.UserTabId, "", "userId=PROFILEUSER");
            }
        }
        protected int GroupId
        {
            get
            {
                int groupId = Null.NullInteger;
                if (!string.IsNullOrEmpty(Request.Params["GroupId"]))
                {
                    groupId = Int32.Parse(Request.Params["GroupId"]);
                }
                return groupId;
            }
        }
        protected int PageSize
        {
            get
            {
                return 10000;// GetSettingAsInt32(ModuleContext.Configuration.TabModuleSettings, "PageSize", Settings.DefaultPageSize);
            }
        }
        public bool DisplayModule
        {
            get
            {
                return !(ProfileUserId == ModuleContext.PortalSettings.UserId && FilterBy == "User") && ModuleContext.PortalSettings.UserId > -1;
            }
        }
        public string ProfileResourceFile
        {
            get { return "~/DesktopModules/Admin/Security/App_LocalResources/Profile.ascx"; }
        }
        protected string ProfileUrlUserToken
        {
            get
            {
                return "PROFILEUSER";
            }
        }
        public string GetSetting(Hashtable settings, string key, string defaultValue)
        {
            string setting = defaultValue;
            if (settings[key] != null)
            {
                setting = Convert.ToString(settings[key]);
            }
            return setting;
        }

        public int GetSettingAsInt32(Hashtable settings, string key, int defaultValue)
        {
            int setting = defaultValue;
            if (settings[key] != null)
            {
                setting = Convert.ToInt32(settings[key]);
            }
            return setting;
        }

        public int ItemId
        {
            get
            {
                var qs = Request.QueryString["tid"];
                if (qs != null)
                    return Convert.ToInt32(qs);
                return -1;
            }

        }
    }
}