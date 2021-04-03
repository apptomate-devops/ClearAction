using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Web.Configuration;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Membership;
using DotNetNuke.Security;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Authentication;
using DotNetNuke.Common;
using System.Collections;
using DotNetNuke.Entities.Controllers;
using DotNetNuke.Entities.Modules;
namespace ClearAction.Modules.Login
{
    /// <summary>
    /// To manage the ajax call for Login related methods
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class RequestHandler : System.Web.Services.WebService
    {


        private string GetUrl(int portalID, string text)
        {
            object setting = UserModuleBase.GetSetting(portalID, "Redirect_AfterLogin");
            var pSettings = PortalController.Instance.GetPortalSettings(portalID);
            if (setting != null)
            {
                if (Convert.ToInt32(setting) != Null.NullInteger)
                {
                    text = Globals.NavigateURL(Convert.ToInt32(setting));
                }

            }
            return text;
        }

        [WebMethod]
        public string UserLogin(string username, string password, string portalID)
        {
            string status = string.Empty;
            var appInsights = new Microsoft.ApplicationInsights.TelemetryClient();
            appInsights.InstrumentationKey = HostController.Instance.GetString("AppInsights.InstrumentationKey");

            int nPortalID = Int32.Parse(portalID);
            try
            {
                UserLoginStatus userLoginStatus = UserLoginStatus.LOGIN_FAILURE;
                string text = new PortalSecurity().InputFilter(username, PortalSecurity.FilterFlag.NoMarkup | PortalSecurity.FilterFlag.NoScripting | PortalSecurity.FilterFlag.NoAngleBrackets);

                if (PortalController.GetPortalSettingAsBoolean("Registration_UseEmailAsUserName", nPortalID, false))
                {
                    UserInfo userByEmail = UserController.GetUserByEmail(nPortalID, text);
                    if (userByEmail != null)
                    {
                        text = userByEmail.Username;
                    }
                }

                PortalController portalController = new PortalController();
                Dictionary<string, string> portalSettings = portalController.GetPortalSettings(nPortalID);

                string portalName = "ClearAction";

                UserInfo userInfo = UserController.ValidateUser(nPortalID, text, password, "DNN", string.Empty, portalName, this.IPAddress, ref userLoginStatus);
                bool authenticated = Null.NullBoolean;
                string message = Null.NullString;
                if (userLoginStatus == UserLoginStatus.LOGIN_USERNOTAPPROVED)
                {
                    message = "UserNotAuthorized";
                }
                else
                {
                    if (userLoginStatus == UserLoginStatus.LOGIN_SUCCESS || userLoginStatus == UserLoginStatus.LOGIN_SUPERUSER)
                    {
                        authenticated = true;

                        UserInfo objUser = new UserInfo();
                        UserLoginStatus loginStatus = UserLoginStatus.LOGIN_FAILURE;
                        objUser = UserController.ValidateUser(0, username, password, "DNN", "", portalName, AuthenticationLoginBase.GetIPAddress(), ref loginStatus);
                        if (objUser != null)
                        {
                            // set telemetry
                            appInsights.Context.User.AuthenticatedUserId = objUser.Username;
                            if (userLoginStatus == UserLoginStatus.LOGIN_SUCCESS || userLoginStatus == UserLoginStatus.LOGIN_SUPERUSER)
                            {
                                UserController.UserLogin(0, objUser, portalName, AuthenticationLoginBase.GetIPAddress(), true);
                            }
                        }

                    }
                    else
                        authenticated = false;
                }

                if (authenticated)
                {
                    status = "success";
                }
                else
                    status = "failure";


                //  return SolveSpaceID;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return status;
        }

        [WebMethod]
        public string UserLogin2(string username, string password, string portalID, string redirecturl)
        {
            string status = string.Empty;
            var appInsights = new Microsoft.ApplicationInsights.TelemetryClient();
            appInsights.InstrumentationKey = HostController.Instance.GetString("AppInsights.InstrumentationKey");

            int nPortalID = Int32.Parse(portalID);
            try
            {
                UserLoginStatus userLoginStatus = UserLoginStatus.LOGIN_FAILURE;
                string text = new PortalSecurity().InputFilter(username, PortalSecurity.FilterFlag.NoMarkup | PortalSecurity.FilterFlag.NoScripting | PortalSecurity.FilterFlag.NoAngleBrackets);

                if (PortalController.GetPortalSettingAsBoolean("Registration_UseEmailAsUserName", nPortalID, false))
                {
                    UserInfo userByEmail = UserController.GetUserByEmail(nPortalID, text);
                    if (userByEmail != null)
                    {
                        text = userByEmail.Username;
                    }
                }

                PortalController portalController = new PortalController();
                Dictionary<string, string> portalSettings = portalController.GetPortalSettings(nPortalID);

                string portalName = "ClearAction";

                UserInfo userInfo = UserController.ValidateUser(nPortalID, text, password, "DNN", string.Empty, portalName, this.IPAddress, ref userLoginStatus);
                bool authenticated = Null.NullBoolean;
                string message = Null.NullString;
                if (userLoginStatus == UserLoginStatus.LOGIN_USERNOTAPPROVED)
                {
                    message = "UserNotAuthorized";
                }
                else
                {
                    if (userLoginStatus == UserLoginStatus.LOGIN_SUCCESS || userLoginStatus == UserLoginStatus.LOGIN_SUPERUSER)
                    {
                        authenticated = true;

                        UserInfo objUser = new UserInfo();
                        UserLoginStatus loginStatus = UserLoginStatus.LOGIN_FAILURE;
                        objUser = UserController.ValidateUser(0, username, password, "DNN", "", portalName, AuthenticationLoginBase.GetIPAddress(), ref loginStatus);
                        if (objUser != null)
                        {
                            // set telemetry
                            appInsights.Context.User.AuthenticatedUserId = objUser.Username;
                            if (userLoginStatus == UserLoginStatus.LOGIN_SUCCESS || userLoginStatus == UserLoginStatus.LOGIN_SUPERUSER)
                            {
                                UserController.UserLogin(0, objUser, portalName, AuthenticationLoginBase.GetIPAddress(), true);
                            }
                        }

                    }
                    else
                        authenticated = false;
                }

                if (authenticated)
                {
                    System.Web.HttpContext.Current.Response.Redirect(GetUrl(Convert.ToInt32(portalID), redirecturl), true);
                }
                else
                    status = "failure";

                return status;
                //  return SolveSpaceID;
            }
            catch (Exception ex)
            {
                //return ex.Message;
                return "failure";
            }

           // return status;
        }

        [WebMethod]
        public string ResetPassword(string username, string portalID)
        {
            string status = string.Empty;

            int nPortalID = Int32.Parse(portalID);
            try
            {

                UserInfo userInfo;

                int _userCount = 0; ;
                ArrayList usersByEmail = UserController.GetUsersByEmail(nPortalID, username, 0, 2147483647, ref _userCount);
                if (usersByEmail != null && usersByEmail.Count == 1)
                {
                    userInfo = (UserInfo)usersByEmail[0];
                }
                else
                {
                    userInfo = UserController.GetUserByName(nPortalID, username);
                }

                UserController userController = new UserController();
                UserController.ResetPasswordToken(userInfo, true);

                //return "success";
            }
            catch (Exception ex)
            {
                //return ex.Message;

                return "failure";
            }

            return "success";
        }


        public string IPAddress
        {
            get
            {
                return AuthenticationLoginBase.GetIPAddress();
            }
        }





    }
}
