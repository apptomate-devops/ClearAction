/*
' Copyright (c) 2018  ClearAction
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
using DotNetNuke.Framework;

using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.JavaScriptLibraries;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections.Generic;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Tabs;
using System.Text.RegularExpressions;
using ClearAction.Modules.WebCast.Components;
using ClearAction.Modules.WebCast.Components.Entity;

namespace ClearAction.Modules.WebCast
{
    public class ModuleBase : PortalModuleBase
    {
        /// <summary>
        /// ComponentID 
        /// </summary>
        public int ComponentID
        {
            get
            {
                int iData = GetQueryStringValue("ComponentID", -1);
                switch (iData)
                {
                    case 0:
                        iData = GetPortalSettings(Components.Util.CommunityCalls, -1);
                        break;
                    case 1:
                        iData = GetPortalSettings(Components.Util.WebCastConversion, -1);
                        break;


                }

                return iData;
            }

        }


        public string GetAPIKey
        {
            get
            {
                return ConfigurationManager.AppSettings["FileStackApiKey"];
            }
        }
        private bool _CandEdit;
        public bool CanEdit
        {
            get
            {
                return _CandEdit;
            }

            set
            {
                _CandEdit = value;
            }
        }
        public bool IsAuthorize(int iAuthorid)
        {
            if (IsUserAdmin)
            {
                return true;
            }

            if (UserInfo.UserID == iAuthorid)
            {
                return true;
            }

            return false;
        }

        public bool IsUserAdmin
        {
            get
            {
                try
                {


                    if (DotNetNuke.Security.Permissions.ModulePermissionController.CanEditModuleContent(this.ModuleConfiguration))
                    {
                        return true;
                    }

                    if (UserInfo.IsInRole(PortalSettings.AdministratorRoleName) || UserInfo.IsSuperUser)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                }

                return false;
            }
        }



        #region "User Profile"
        public string GetAvatarUrl(int userID, int avatarWidth, int avatarHeight)
        {
            DotNetNuke.Entities.Portals.PortalSettings p = HttpContext.Current.Items["PortalSettings"] as DotNetNuke.Entities.Portals.PortalSettings;
            if (p == null)
            {
                return string.Empty;
            }

            UserInfo user = new DotNetNuke.Entities.Users.UserController().GetUser(PortalId, userID);
            string imgUrl = p.PortalAlias.ToString() + "/images/thumbnail.jpg";
            if (user != null)
            {
                imgUrl = user.Profile.PhotoURL;
            }

            return imgUrl;
        }
        public string GetBackgroundImage(int iAuthorid)
        {
            return "background:url('" + GetAvatarUrl(iAuthorid, 48, 48) + "') no-repeat 0 0;";
        }
        public string GetDisplayName(int iUserID)
        {
            var oUser = UserController.GetUserById(this.PortalId, iUserID);
            return oUser == null ? "" : oUser.DisplayName;
        }
        public string GetDisplayNameLink(int iUserID)
        {
            var oUser = UserController.GetUserById(this.PortalId, iUserID);
            if(oUser!=null)
            {
                return string.Format("<a href='/MyProfile?Userid={0}' class='tips' data-tip={1}>{1}</a>'", oUser.UserID, oUser.DisplayName); 
            }
            return "";
        }
        #endregion  
        #region "Methods"

        public string ReadTemplate(string FileName)
        {
            string strFilePath = DotNetNuke.Common.Globals.ApplicationMapPath + string.Format("/DesktopModules/ClearAction_WebCast/Templates/{0}.html", FileName);

            try
            {
                if (System.IO.File.Exists(strFilePath))
                    return System.IO.File.ReadAllText(strFilePath);
            }
            catch (Exception exc)
            {


            }

            return "";
        }


        public string GetModuleSettings(string Key, string defaultvalue)
        {
            try
            {
                if (Settings.Contains(Key))
                    defaultvalue = Convert.ToString(Settings[Key]);
            }
            catch (Exception)
            {


            }
            return defaultvalue;
        }

        public int GetModuleSettings(string Key, int defaultvalue)
        {
            try
            {
                Int32.TryParse(GetModuleSettings(Key, ""), out defaultvalue);
            }
            catch (Exception)
            {


            }
            return defaultvalue;
        }

        public string GetPortalSettings(string Key, string defaultvalue)
        {
            try
            {
                defaultvalue = PortalController.GetPortalSetting(Key, PortalId, defaultvalue);

            }
            catch (Exception)
            {


            }
            return defaultvalue;
        }
        public int GetPortalSettings(string Key, int defaultvalue)
        {
            try
            {
                Int32.TryParse(GetPortalSettings(Key, ""), out defaultvalue);
            }
            catch (Exception)
            {


            }
            return defaultvalue;
        }
        public int GetQueryStringValue(string Key, int iDefault)
        {
            try
            {
                if (Request.QueryString[Key] != null)
                {
                    return Convert.ToInt32(Request.QueryString[Key]);
                }
            }
            catch (Exception exc)
            {
            }

            return iDefault;
        }

        public string GetQueryStringValue(string Key, string iDefault)
        {
            try
            {
                if (Request.QueryString[Key] != null)
                {
                    return Convert.ToString(Request.QueryString[Key]);
                }
            }
            catch (Exception exc)
            {
            }

            return iDefault;
        }




      

        public string ReplaceCategoryToken(List<Components.PostCategoryRelationInfo> oCat, string strTemplate)
        {
            if (oCat == null)
            {
                return strTemplate;
            }

            string tmpData = strTemplate;
            System.Text.StringBuilder tmpfinal = new System.Text.StringBuilder();
            if (oCat.Count > 0)
            {
                foreach (Components.PostCategoryRelationInfo oPostCategory in oCat)
                {
                    var oCategory = new Components.DBController().GetGlobalCategoryID(oPostCategory.CategoryId);
                    if (oCategory != null)
                    {
                        strTemplate = tmpData;
                        int iCategory = GetQueryStringValue("CategoryId", -1);
                        if (iCategory == oCategory.CategoryId)
                        {

                            strTemplate = strTemplate.Replace("{CATEGORYNAME}", string.Format("<strong>{0}</strong>", oCategory.CategoryName));

                        }
                        else
                            strTemplate = strTemplate.Replace("{CATEGORYNAME}", oCategory.CategoryName);
                        strTemplate = strTemplate.Replace("{CATEGORYURL}", DotNetNuke.Common.Globals.NavigateURL(this.TabId, "", "CategoryID=" + oCategory.CategoryId.ToString()));
                        strTemplate = strTemplate.Replace("{CATEGORYID}", oCategory.CategoryId.ToString());
                        tmpfinal.Append(strTemplate);
                    }
                }
            }

            return tmpfinal.ToString();
        }
        public string ReplaceToken(Components.WCC_PostInfo oWccinfo, string strTemplate)
        {
            try
            {
                if (oWccinfo == null)
                    return "";

                if (IsUserAdmin)
                {
                    string strEditlink = DotNetNuke.Common.Globals.NavigateURL(this.TabId, "Edit", "mid=" + ModuleId, Components.Util.PostKey + "=" + oWccinfo.WCCId.ToString());

                    strTemplate = strTemplate.Replace("{EDIT}", String.Format("<a href='{0}'><img alt='Edit' src='//{1}/images/Edit.gif'></img></a>", strEditlink, PortalAlias.HTTPAlias.ToString()));
                    strTemplate = oWccinfo.IsPublish == false ? strTemplate.Replace("{PUBLISHSTATUS}", "<span style='color:red'>UN-PUBLISHED</span>") : strTemplate.Replace("{PUBLISHSTATUS}", "");



                }
                else
                {
                    strTemplate = strTemplate.Replace("{PUBLISHSTATUS}", "");
                    strTemplate = strTemplate.Replace("{EDIT}", "");
                }

                string strCategory = ReadTemplate("CategoryTemplate");

                string strSeparator = "&nbsp;&nbsp;&nbsp;¦&nbsp;&nbsp;&nbsp;";

                strTemplate = strTemplate.Replace("{EVENTDATE}", Components.Util.DisplayCulutureDate(oWccinfo.EventDate));
                strTemplate = strTemplate.Replace("{EVENTTIME}", Components.Util.DisplayCulutureTime(oWccinfo.EventTime));
                //Chnages done by @SP
                //strTemplate = strTemplate.Replace("{EVENTDATETIME}", Components.Util.DisplayCultureDateTime(oWccinfo.EventDate, oWccinfo.EventTime));
                strTemplate = oWccinfo.EventDate > DateTime.Now ? strTemplate.Replace("{EVENTDATETIME}", Components.Util.DisplayCultureDateTime(oWccinfo.EventDate, oWccinfo.EventTime) + " ET (i.e. NYC)") : strTemplate.Replace("{EVENTDATETIME}", string.Empty);


                strTemplate = strTemplate.Replace("{PUBLISH_ONDATETIME}", Components.Util.DisplayCulutureDate(oWccinfo.CreatedOnDate));
                strTemplate = strTemplate.Replace("{EDIT_ONDATE}", Components.Util.DisplayCulutureDate(oWccinfo.UpdatedOnDate));

                strTemplate = strTemplate.Replace("{EVENTDATETIME_TIMEZONE}", Components.Util.DisplayCultureDateTime(oWccinfo.EventDate, oWccinfo.EventTime) + " ET");
                strTemplate = strTemplate.Replace("{URL}", BuildQuery(GetQueryStringValue("CategoryId", -1), GetQueryStringValue("SortBy", -1), oWccinfo.WCCId, "", GetQueryStringValue("IsMyVault", 1), GetQueryStringValue("Show", 0)));
                strTemplate = strTemplate.Replace("{EVENTDATETIMECST}", Components.Util.DisplayCulutureDateTime(oWccinfo.EventDate, true) + ", " + Components.Util.DisplayCulutureTime(oWccinfo.EventTime));
                strTemplate = strTemplate.Replace("{EVENTTIMEZONE}", "ET");
                var oCreatedby = UserController.GetUserById(this.PortalId, oWccinfo.CreatedByUserID);




                if (oCreatedby != null)
                {
                    strTemplate = strTemplate.Replace("{TITLE}", oCreatedby.Profile.Title);
                    strTemplate = strTemplate.Replace("{AUTHORIMAGE_TAG}", string.Format("<a href='/MyProfile/UserName/{0}'  class='tip'  data-tip='{2}'><img src='{1}' target='_blank' style='height:48px;width:48px'></img></a>", oWccinfo.CreatedByUserID, oCreatedby.Profile.PhotoURL, oCreatedby.DisplayName));
                    strTemplate = strTemplate.Replace("{AUTHORNAME_TAG}", String.Format("<a href='/MyProfile/UserName/{0}' class='tip'  data-tip='{1}'>{1}</a>", oCreatedby.Username, "By " + oCreatedby.DisplayName));
                }

                if (strTemplate.Contains("{SHORTDESCRIPTION:"))
                {
                    string strData = strTemplate.Substring(strTemplate.IndexOf("{SHORTDESCRIPTION:"), 10);

                }

                strTemplate = strTemplate.Replace("{COMPONENTID}", BuildQuery(GetQueryStringValue("CategoryId", -1), GetQueryStringValue("SortBy", -1), -1, GetQueryStringValue("q", ""), GetQueryStringValue("IsMyValut", 0), GetQueryStringValue("Show", 0), oWccinfo.ComponentID));
                if (GetQueryStringValue("ComponentID", -1) == oWccinfo.ComponentID)
                    strTemplate = strTemplate.Replace("{COMPONENTNAME}", oWccinfo.ComponentID == 4 ? string.Format("<strong>{0}</strong>", "Community Calls") : string.Format("<strong>{0}</strong>", "Webcast Conversations"));
                else
                    strTemplate = strTemplate.Replace("{COMPONENTNAME}", oWccinfo.ComponentID == 4 ? "Community Calls" : "Webcast Conversations");




                //Register/view link

                DBController objDBController = new DBController();

                IQueryable<Webinar_User_RegistrationDetail> objWebinar_User_RegistrationDetail = objDBController.GetWebinarUserRegistrationForEvent(this.UserInfo.UserID, oWccinfo.WCCId, oWccinfo.ComponentID);

                if (oWccinfo.ISVISIBLEREGiSTER)
                {
                    strTemplate = strTemplate.Replace("{ISVISIBLEREGiSTER}", "display:block");
                    strTemplate = strTemplate.Replace("{ISVISIBLEVIEW}", "display:none");
                    //      otempdata = !string.IsNullOrEmpty(oWccinfo.WebApiKey) ? otempdata.Replace("{REGISTERLINK}", string.Format("https://register.gotowebinar.com/register/{0}?firstName={1}&lastName={2}&email={3}", oWccinfo.WebApiKey, UserInfo.FirstName, UserInfo.LastName, UserInfo.Email)) : otempdata.Replace("{REGISTERLINK}", string.Format("{0}?firstName={1}&lastName={2}&email={3}", oWccinfo.RegistrationLink, UserInfo.FirstName, UserInfo.LastName, UserInfo.Email));

                    if (!string.IsNullOrEmpty(oWccinfo.WebApiKey))
                    {
                        strTemplate = strTemplate.Replace("{REGISTERLINK}", string.Format("https://register.gotowebinar.com/register/{0}?firstName={1}&lastName={2}&email={3}", oWccinfo.WebApiKey, UserInfo.FirstName, UserInfo.LastName, UserInfo.Email));


                        if (objWebinar_User_RegistrationDetail.Count() <= 0)
                        {
                            strTemplate = strTemplate.Replace("{WEBINARKEY}", oWccinfo.WebApiKey);
                            strTemplate = strTemplate.Replace("{REGISTERCOMPONENTID}", oWccinfo.ComponentID.ToString());
                            strTemplate = strTemplate.Replace("{REGISTERLOGINUSERID}", this.UserInfo.UserID.ToString());
                            strTemplate = strTemplate.Replace("{WCCID}", oWccinfo.WCCId.ToString());

                            strTemplate = strTemplate.Replace("{LINKCLASSNAME}", "gotoRegisterLink");

                            strTemplate = strTemplate.Replace("{ONCLICKEVENTMETHOD}", "onclick='GotoWebinarRegister(this)'");

                            strTemplate = strTemplate.Replace("{LINKTEXTCONTENT}", "Register for live event");

                        }
                        else
                        {

                            strTemplate = strTemplate.Replace("{WEBINARKEY}", oWccinfo.WebApiKey);
                            strTemplate = strTemplate.Replace("{REGISTERCOMPONENTID}", oWccinfo.ComponentID.ToString());
                            strTemplate = strTemplate.Replace("{REGISTERLOGINUSERID}", this.UserInfo.UserID.ToString());
                            strTemplate = strTemplate.Replace("{WCCID}", oWccinfo.WCCId.ToString());

                            strTemplate = strTemplate.Replace("{LINKCLASSNAME}", "gotoAfterRegisterLink");

                            strTemplate = strTemplate.Replace("{ONCLICKEVENTMETHOD}", "");

                            strTemplate = strTemplate.Replace("{LINKTEXTCONTENT}", "Already Registered   for join <a target='_blank' class='gotoJoinLInk' href='" + objWebinar_User_RegistrationDetail .FirstOrDefault().UserWebinarJoinURL+ "'>Click here<a>");

                        }
                       
                    }
                    else if (!string.IsNullOrEmpty(oWccinfo.RegistrationLink))
                        //strTemplate = strTemplate.Replace("{REGISTERLINK}", string.Format("{0}?firstName={1}&lastName={2}&email={3}", oWccinfo.RegistrationLink, UserInfo.FirstName, UserInfo.LastName, UserInfo.Email));
                        strTemplate = strTemplate.Replace("{REGISTERLINK}", "");
                    else
                        strTemplate = strTemplate.Replace("{REGISTERLINK}", "");


                }
                else
                {
                    strTemplate = strTemplate.Replace("{ISVISIBLEREGiSTER}", "display:none");
                    strTemplate = strTemplate.Replace("{ISVISIBLEVIEW}", "display:block");

                    strTemplate = strTemplate.Replace("{EXPIREDVIEWLINK}", oWccinfo.ExpiredviewLink);

                }


                //Category Replace
                strCategory = ReplaceCategoryToken(oWccinfo.GetCategory, strCategory);
                strTemplate = strTemplate.Replace("{CATEGORIES}", strCategory);

                strTemplate = strTemplate.Replace("{MODULEPATH}", ModulePath);



                //Context Menu
                string strStatus = "<img src='{0}'></img> {1} ";
                var oStatus = oWccinfo.Status == null ? "" : Convert.ToString(oWccinfo.Status);
                if (oStatus.ToLower() == "completed")
                {
                    strTemplate = strTemplate.Replace("{STATUS}", string.Format(strStatus, ResolveUrl("/DesktopModules/Blog/Images/img_17.png"), " Done"));

                }
                else if (oStatus.ToLower() == "to-do")
                {
                    strTemplate = strTemplate.Replace("{STATUS}", string.Format(strStatus, ResolveUrl("/DesktopModules/Blog/Images/img_18.png"), " To-Do"));
                }
                else
                    strTemplate = strTemplate.Replace("{STATUS}", "");

                string strUserMenu = "<img style=\'float:right;cursor:pointer;\' src=\'/Images/more_icon.png\' class=\'CA_UserMenu\' CID=\'{0}\'  ComponentId=\'{2}\' IsSelfAssigned=\'{1}\' IsMyValut=\'{3}\' ></img>";
                bool ShowUserContextMenu;
                //  Previous implementation which hides the context menu i)n case status ='Completed' but we still need it
                // ShowUserContextMenu = Boolean.Parse(IIf(oBlog.Status Is Nothing, True, (IIf(((oBlog.IsSelfAssigned = 1) And (oBlog.Status = "To-Do")), True, False))).ToString())
                ShowUserContextMenu = bool.Parse((((oWccinfo.IsSelfAssigned == 1)
                                || (oWccinfo.Status == null)) ? true : false).ToString());


                strUserMenu = strUserMenu.Replace("{0}", oWccinfo.WCCId.ToString());
                strUserMenu = strUserMenu.Replace("{1}", Convert.ToString(oWccinfo.IsSelfAssigned));
                strUserMenu = strUserMenu.Replace("{2}", oWccinfo.ComponentID.ToString());
                strUserMenu = strUserMenu.Replace("{3}", oWccinfo.Status == null ? "0" : "1");
                strTemplate = strTemplate.Replace("{UserContextMenu}", strUserMenu);
                strTemplate = strTemplate.Replace("{SingleSaveVisibility}", "block");
                if (oWccinfo.IsSelfAssigned == 1)
                {
                    strTemplate = strTemplate.Replace("{MyVaultText}", "REMOVE FROM MY VAULT");
                    strTemplate = strTemplate.Replace("{MyVaultFunction}", ("CA_RemoveFromMyVault(" + (oWccinfo.WCCId.ToString() + ")")));
                }
                else
                {
                    strTemplate = strTemplate.Replace("{MyVaultText}", "SAVE TO MY VAULT");
                    strTemplate = strTemplate.Replace("{MyVaultFunction}", ("CA_Assign2Me(" + (oWccinfo.WCCId.ToString() + ")")));
                    //  
                }



                //Template for likes/comment/recomand

                var oController = new Components.DBController();
                string RecommendCount = Convert.ToString(oWccinfo.LikesCount);
                //strTemplate = strTemplate.Replace("{LIKESCOUNT}", RecommendCount + " Recommends" + strSeparator);
                strTemplate = strTemplate.Replace("{LIKESCOUNT}", RecommendCount + " Recommends");
                strTemplate = strTemplate.Replace("{VIEWCOUNT}", Convert.ToString(oWccinfo.ViewCount) + " Views");
                strTemplate = strTemplate.Replace("{COMMENTCOUNT}", Convert.ToString(oWccinfo.CommentCount));
                strTemplate = strTemplate.Replace("{ItemID}", oWccinfo.WCCId.ToString());
                strTemplate = strTemplate.Replace("{RECOMMENDCOUNT}", RecommendCount);
                strTemplate = strTemplate.Replace("{UNRECOMMENDCOUNT}", Convert.ToString(oWccinfo.UnLikesCount));



                strTemplate = Components.Util.ReplaceToken_WccPost(strTemplate, oWccinfo);
            }
            catch (Exception ex)
            {


            }
            return strTemplate;
        }

        #endregion


        #region "RedirectUrl"



        private List<string> CreateParameter(int iCategoryId, int sSortyBy, int iPostD, string QueryString, int IsMyVault, int iShow, int ComponentID)
        {
            List<string> additionalParameters = new List<string>();
            additionalParameters.Add("CategoryId=" + iCategoryId);
            additionalParameters.Add("SortBy=" + sSortyBy);
            additionalParameters.Add("ComponentID=" + ComponentID);
            if (!string.IsNullOrEmpty(QueryString))
                additionalParameters.Add("q=" + QueryString);
            additionalParameters.Add("Show=" + iShow);
            additionalParameters.Add("IsMyVault=" + IsMyVault);
            if (iPostD > 0)
                additionalParameters.Add(Components.Util.PostKey + "=" + iPostD);
            return additionalParameters;
        }
        public string BuildQuery(int iWCCID)
        {
            List<string> additionalParameters = new List<string>();
            additionalParameters.Add(Components.Util.PostKey + "=" + iWCCID);
            return NavigateUrl(this.TabId, "", additionalParameters);
        }
        public string BuildQuery(int iWCCID, int iCommentID)
        {
            List<string> additionalParameters = new List<string>();
            additionalParameters.Add(Components.Util.PostKey + "=" + iWCCID);
            additionalParameters.Add("CommentID=" + iCommentID);
            return NavigateUrl(this.TabId, "", additionalParameters);
        }
        public string BuildQuery(int iCategoryId, int sSortyBy, int iTopicId, int iShow)
        {
            return NavigateUrl(this.TabId, "", CreateParameter(iCategoryId, sSortyBy, iTopicId, "", -1, iShow, ComponentID));
        }
        public string BuildQuery(int iCategoryId, int sSortyBy, int iTopicId, string Search, int IsMyVault, int iShow)
        {
            return NavigateUrl(this.TabId, "", CreateParameter(iCategoryId, sSortyBy, iTopicId, Search, IsMyVault, iShow, ComponentID));
        }

        public string BuildQuery(int iCategoryId, int sSortyBy, int iTopicId, string Search, int IsMyVault, int iShow, int ComponentID)
        {
            return NavigateUrl(this.TabId, "", CreateParameter(iCategoryId, sSortyBy, iTopicId, Search, IsMyVault, iShow, ComponentID));
        }
        private static string NavigateUrl(int tabId, string controlKey, params string[] additionalParameters)
        {
            return NavigateUrl(tabId, controlKey, string.Empty, -1, additionalParameters);
        }
        private static string NavigateUrl(int tabId, string controlKey, List<string> additionalParameters)
        {

            string[] parameters = new string[additionalParameters.Count];
            for (int i = 0; i < additionalParameters.Count; i++)
            {
                parameters[i] = additionalParameters[i];
            }
            return NavigateUrl(tabId, controlKey, parameters);
        }

        private static string NavigateUrl(int tabId, string controlKey, string pageName, int portalId, params string[] additionalParameters)
        {
            var currParams = additionalParameters.ToList();

            // TODO: Figure out what these parameters are
            var asgvParam = HttpContext.Current.Request.Params["asgv"];
            if (!string.IsNullOrWhiteSpace(asgvParam))
                currParams.Add("asgv=" + asgvParam);

            var asgParam = HttpContext.Current.Request.Params["asg"];
            if (!string.IsNullOrWhiteSpace(asgParam))
                currParams.Add("asg=" + asgParam);

            var s = DotNetNuke.Common.Globals.NavigateURL(tabId, controlKey, currParams.ToArray());
            if (portalId == -1 || string.IsNullOrWhiteSpace(pageName))
                return s;

            var tc = new TabController();
            var ti = tc.GetTab(tabId, portalId, false);
            var sURL = currParams.Aggregate(DotNetNuke.Common.Globals.ApplicationURL(tabId), (current, p) => current + ("&" + p));

            var portalSettings = (DotNetNuke.Entities.Portals.PortalSettings)(HttpContext.Current.Items["PortalSettings"]);
            pageName = CleanStringForUrl(pageName);
            s = DotNetNuke.Common.Globals.FriendlyUrl(ti, sURL, pageName, portalSettings);
            return s;
        }

        public static string CleanStringForUrl(string text)
        {
            text = text.Trim();
            text = text.Replace(":", string.Empty);
            text = Regex.Replace(text, @"[^\w]", "-");
            text = Regex.Replace(text, @"([-]+)", "-");
            if (text.EndsWith("-"))
                text = text.Substring(0, text.Length - 1);

            return text;
        }

        #endregion
    }
}