/*
' Copyright (c) 2018  Josh
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Controllers;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security;
using DotNetNuke.Security.Membership;
using DotNetNuke.Services.Authentication;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using System;
using System.Collections.Generic;
using System.Web;

namespace ClearAction.Modules.Login
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from ClearAction_LoginModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Login : ClearAction_LoginModuleBase//, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    diverror.Visible = false;


                    //   List<AuthenticationInfo> authSystems = AuthenticationController.GetEnabledAuthenticationServices();
                    //   AuthenticationLoginBase defaultLoginControl = null;
                    //  var defaultAuthProvider = PortalController.GetPortalSetting("DefaultAuthProvider", PortalId, "DNN");
                    //                    AuthenticationType = "DNN";
                   // hdnCurrentDateTime.Value = System.DateTime.Now.ToShortDateString();

                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
       
        protected string iTabId
        {
            get
            {
                object setting = UserModuleBase.GetSetting(base.PortalId, "Redirect_AfterLogin");
                if (setting != null)
                    return setting.ToString();
                return PortalSettings.HomeTabId.ToString();
            }

        }

        /// <summary>
        /// Returns the redirect url for user after login
        /// </summary>
        protected string RedirectURL
        {
            get
            {
                string text = "";
                object setting = UserModuleBase.GetSetting(base.PortalId, "Redirect_AfterLogin");
                if (base.Request.QueryString["returnurl"] != null)
                {
                    text = HttpUtility.UrlDecode(base.Request.QueryString["returnurl"]);
                    text = UrlUtils.ValidReturnUrl(text);
                }
                if (base.Request.Cookies["returnurl"] != null)
                {
                    text = HttpUtility.UrlDecode(base.Request.Cookies["returnurl"].Value);
                    text = UrlUtils.ValidReturnUrl(text);
                }
                if (base.Request.Params["appctx"] != null)
                {
                    text = HttpUtility.UrlDecode(base.Request.Params["appctx"]);
                    text = UrlUtils.ValidReturnUrl(text);
                }
                string hTTPAlias = base.PortalAlias.HTTPAlias;
                StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase;
                bool flag = text == "/" || (hTTPAlias.Contains("/") && text.Equals(hTTPAlias.Substring(hTTPAlias.IndexOf("/", comparisonType)), comparisonType));
                if (string.IsNullOrEmpty(text) | flag)
                {
                    if (Convert.ToInt32(setting) != Null.NullInteger)
                    {
                        text = Globals.NavigateURL(Convert.ToInt32(setting));
                    }
                    else if (base.PortalSettings.LoginTabId != -1 && base.PortalSettings.HomeTabId != -1)
                    {
                        text = Globals.NavigateURL(base.PortalSettings.HomeTabId);
                    }
                    else
                    {
                        text = Globals.NavigateURL();
                    }
                }
                return text;
            }
        }
        public string IPAddress
        {
            get
            {
                return AuthenticationLoginBase.GetIPAddress();
            }
        }
        ///// <summary>
        ///// Gets and sets the current UserToken
        ///// </summary>UserToken
        //protected string UserToken
        //{
        //    get
        //    {
        //        var userToken = "";
        //        if (ViewState["UserToken"] != null)
        //        {
        //            userToken = Convert.ToString(ViewState["UserToken"]);
        //        }
        //        return userToken;
        //    }
        //    set
        //    {
        //        ViewState["UserToken"] = value;
        //    }
        //}


        //private void ValidateUser(UserInfo objUser, bool ignoreExpiring, string strRedirectlink)
        //{
        //    UserValidStatus validStatus = UserValidStatus.VALID;
        //    string strMessage = Null.NullString;
        //    DateTime expiryDate = Null.NullDate;
        //    bool okToShowPanel = true;

        //    validStatus = UserController.ValidateUser(objUser, PortalId, ignoreExpiring);

        //    if (PasswordConfig.PasswordExpiry > 0)
        //    {
        //        expiryDate = objUser.Membership.LastPasswordChangeDate.AddDays(PasswordConfig.PasswordExpiry);
        //    }

        //    //Check if the User has valid Password/Profile
        //    switch (validStatus)
        //    {
        //        case UserValidStatus.VALID:
        //            //check if the user is an admin/host and validate their IP

        //            //Set the Page Culture(Language) based on the Users Preferred Locale
        //            if ((objUser.Profile != null))
        //            {
        //                Localization.SetLanguage(objUser.Profile.PreferredLocale);
        //            }
        //            else
        //            {
        //                Localization.SetLanguage(PortalSettings.DefaultLanguage);
        //            }

        //            //Set the Authentication Type used 
        //            AuthenticationController.SetAuthenticationType("DNN");

        //            //Complete Login
        //            UserController.UserLogin(PortalId, objUser, PortalSettings.PortalName, AuthenticationLoginBase.GetIPAddress(), false);

        //            //redirect browser
        //            var redirectUrl = strRedirectlink;

        //            //Clear the cookie
        //            HttpContext.Current.Response.Cookies.Set(new HttpCookie("returnurl", "")
        //            {
        //                Expires = DateTime.Now.AddDays(-1),
        //                Path = (!string.IsNullOrEmpty(Globals.ApplicationPath) ? Globals.ApplicationPath : "/")
        //            });

        //            Response.Redirect(redirectUrl, true);
        //            break;
        //        case UserValidStatus.PASSWORDEXPIRED:
        //            strMessage = string.Format(Localization.GetString("PasswordExpired", LocalResourceFile), expiryDate.ToLongDateString());

        //            break;
        //        case UserValidStatus.PASSWORDEXPIRING:
        //            strMessage = string.Format(Localization.GetString("PasswordExpiring", LocalResourceFile), expiryDate.ToLongDateString());

        //            break;
        //        case UserValidStatus.UPDATEPASSWORD:
        //            var portalAlias = Globals.AddHTTP(PortalSettings.PortalAlias.HTTPAlias);
        //            var redirTo = string.Format("{0}/default.aspx?ctl=PasswordReset&resetToken={1}&forced=true", portalAlias, objUser.PasswordResetToken);
        //            Response.Redirect(redirTo);
        //            break;
        //        case UserValidStatus.UPDATEPROFILE:
        //            //Save UserID in ViewState so that can update profile later.

        //            break;
        //    }
        //    if (!okToShowPanel) 

        //        DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, strMessage, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);
        //}

        public int ProfileTabID = 36;
        public void UserLogin(string username, string password, string portalID)
        {
            string status = string.Empty;
            var appInsights = new Microsoft.ApplicationInsights.TelemetryClient();
            appInsights.InstrumentationKey = HostController.Instance.GetString("AppInsights.InstrumentationKey");

          //  int nPortalID = Int32.Parse(portalID);
            try
            {

                //New Code

                UserLoginStatus loginStatus = UserLoginStatus.LOGIN_FAILURE;
                UserInfo objUser = UserController.ValidateUser(PortalId,
                                                               txtUsername.Value,
                                                               txtPassword.Value,
                                                               "DNN",
                                                               "",
                                                               PortalSettings.PortalName,
                                                               AuthenticationLoginBase.GetIPAddress(),
                                                               ref loginStatus);
                if (loginStatus == UserLoginStatus.LOGIN_SUCCESS)
                {
                    //Assocate alternate Login with User and proceed with Login
                  //  AuthenticationController.AddUserAuthentication(objUser.UserID, AuthenticationType, objUser.DisplayName);
                    //   appInsights.Context.User.AuthenticatedUserId = objUser.Username;
                    try
                    {
                        if (objUser != null)
                        {
                            var oprofilevalue = objUser.Profile.GetProperty("ProfileFinished");

                            string strurl = DotNetNuke.Common.Globals.NavigateURL(ProfileTabID);


                            if (oprofilevalue != null)
                            {
                                if (oprofilevalue.PropertyValue == "1")
                                {
                                    if (!RedirectURL.EndsWith("rofile"))
                                        strurl = RedirectURL;
                                    else
                                        strurl = DotNetNuke.Common.Globals.NavigateURL(PortalSettings.HomeTabId);
                                }
                                else
                                    strurl = PortalAlias.HTTPAlias;
                            }
                            else if (RedirectURL != null)
                            {
                                strurl = RedirectURL;
                            }
                            else { 
                                strurl = DotNetNuke.Common.Globals.NavigateURL(PortalSettings.HomeTabId);
                            }

                            // set telemetry
                            appInsights.Context.User.AuthenticatedUserId = objUser.UserID.ToString();
                            UserController.UserLogin(PortalId, objUser, PortalSettings.PortalName, AuthenticationLoginBase.GetIPAddress(), true);

                            Response.Redirect(strurl, false);

                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                    catch (Exception ex)
                    {
                        DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, ex.Message, DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
            
                    }

                }
                else if (loginStatus == UserLoginStatus.LOGIN_SUPERUSER)
                {
                    var strurl = DotNetNuke.Common.Globals.NavigateURL(PortalSettings.HomeTabId);
                    appInsights.Context.User.AuthenticatedUserId = objUser.UserID.ToString();
                    UserController.UserLogin(0, objUser, PortalSettings.PortalName, AuthenticationLoginBase.GetIPAddress(), true);

                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.HomeTabId),false);

                }
                else if (loginStatus == UserLoginStatus.LOGIN_USERLOCKEDOUT)
                {
                    txtUsername.Focus();
                    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Warning", "Your account has been locked. Please do contact with website administrator",DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);
                    btnSigninSecond.Enabled = false;
                    diverror.Visible = true;
                     
                }
                else
                {
                    txtUsername.Focus();
                    errMsg.InnerText = "Please remember that passwords are case sensitive.";
                    diverror.Visible = true;
                   // return null;
                }

                //Code Ended here




                //UserLoginStatus userLoginStatus = UserLoginStatus.LOGIN_FAILURE;
                //string text = new PortalSecurity().InputFilter(username, PortalSecurity.FilterFlag.NoMarkup | PortalSecurity.FilterFlag.NoScripting | PortalSecurity.FilterFlag.NoAngleBrackets);

                ////if (PortalController.GetPortalSettingAsBoolean("Registration_UseEmailAsUserName", nPortalID, false))
                ////{
                ////    UserInfo userByEmail = UserController.GetUserByEmail(nPortalID, text);
                ////    if (userByEmail != null)
                ////    {
                ////        text = userByEmail.Username;
                ////    }
                ////}

                //PortalController portalController = new PortalController();
                //Dictionary<string, string> portalSettings = portalController.GetPortalSettings(nPortalID);

                //string portalName = "ClearAction";

                //UserInfo userInfo = UserController.ValidateUser(nPortalID, text, password, "DNN", string.Empty, portalName, this.IPAddress, ref userLoginStatus);
                //if (UserInfo == null || UserInfo.IsDeleted || !UserInfo.Membership.Approved)
                //    return null;

                //bool authenticated = Null.NullBoolean;
                //string message = Null.NullString;
                //if (userLoginStatus == UserLoginStatus.LOGIN_USERNOTAPPROVED)
                //{
                //    message = "UserNotAuthorized";
                //}
                //else
                //{
                //    if (userLoginStatus == UserLoginStatus.LOGIN_SUCCESS || userLoginStatus == UserLoginStatus.LOGIN_SUPERUSER)
                //    {
                //        authenticated = true;
                //        UserController.UserLogin(0, userInfo, portalName, AuthenticationLoginBase.GetIPAddress(), true);
                //        //UserInfo objUser = new UserInfo();
                //        //UserLoginStatus loginStatus = UserLoginStatus.LOGIN_FAILURE;
                //        //objUser = UserController.ValidateUser(0, username, password, "DNN", "", portalName, AuthenticationLoginBase.GetIPAddress(), ref loginStatus);
                //        //if (userInfo != null)
                //        //{
                //        //    // set telemetry
                //        //    appInsights.Context.User.AuthenticatedUserId = objUser.Username;
                //        //    if (userLoginStatus == UserLoginStatus.LOGIN_SUCCESS || userLoginStatus == UserLoginStatus.LOGIN_SUPERUSER)
                //        //    {

                //        //    }
                //        //}

                //    }
                //    else
                //        authenticated = false;
                //}

                //return authenticated ? userInfo : null;



                //  return SolveSpaceID;
            }
            catch (Exception ex)
            {
                /// return ex.Message;
            }

      
        }


        protected void btnSigninSecond_Click(object sender, EventArgs e)
        {
            try
            {
                UserLogin(txtUsername.Value, txtPassword.Value, PortalId.ToString());
               
            }
            catch (Exception ex)
            {


            }
        }

       
    }
}