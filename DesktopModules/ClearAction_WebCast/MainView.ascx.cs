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
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using System.Web.UI.WebControls;
using System.Linq;
using DotNetNuke.Entities.Users;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.Script.Serialization;
using ClearAction.Modules.WebCast.Components;
using DotNetNuke.Data;
using System.Net.Mail;

namespace ClearAction.Modules.WebCast
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from ClearAction_WebCastModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class MainView : ModuleBase//, IActionable
    {




        #region "Public Properties"

        public string WebServicePath
        {
            get
            {
                string Prefix = "http://";
                if (Request.IsSecureConnection)
                {
                    Prefix = "https://";
                }

                return Prefix + PortalAlias.HTTPAlias.ToString();
            }
        }





        public int CurrentCatID
        {
            get
            {

                return GetQueryStringValue("CategoryId", -1);
            }

        }

        public int SortBy
        {
            get
            {
                return GetQueryStringValue("SortBy", -1);
            }

        }
        private int IsMyVault
        {
            get
            {
                return GetQueryStringValue("IsMyVault", 1);
            }


        }



        private int SubOption
        {
            get
            {
                return GetQueryStringValue("Show", 0);
            }


        }

        public string SortInfo()
        {
            switch (SortBy)
            {
                case -1:
                    return "created date";
                case 0:
                    return "title";
                case 2:
                    return "presenter (a-z)";
                case 3: return "presenter (z-a)";
                case 4:
                    return "event date";

            }
            return "date";
        }
        #endregion

        #region "Public Data Bind Methods"
        private string CreateBreadData()
        {
            System.Text.StringBuilder strNav = new System.Text.StringBuilder();

            strNav.Append(string.Format("<a href='{0}'>{1}</a>&nbsp;>&nbsp;", DotNetNuke.Common.Globals.NavigateURL(this.TabId), "Digital Events"));
            strNav.Append(string.Format("<a href='{0}'>{1}</a>&nbsp;>&nbsp;", BuildQuery(CurrentCatID, SortBy, -1, "", IsMyVault, SubOption, ComponentID), IsMyVault == 1 ? "My Vault" : "Show All"));
            strNav.Append(string.Format("<a href='{0}'>{1}</a>&nbsp;>&nbsp;", BuildQuery(CurrentCatID, SortBy, -1, "", IsMyVault, SubOption, ComponentID), CurrentCatID == -1 ? "All Categories" : new Components.DBController().GetGlobalCategoryID(CurrentCatID).CategoryName));
            //  strNav.Append(string.Format("{0}", ComponentID == -1 ? "All Events" : (ComponentID == Components.Util.CommunityID ? "Community Calls" : "Webcast Conversations")));
            return strNav.ToString();
        }
        public void CreateBreadcumb()
        {
            System.Text.StringBuilder strNav = new System.Text.StringBuilder();
            strNav.Append(CreateBreadData());
            strNav.Append(string.Format("{0}", ComponentID == -1 ? "All Events" : (ComponentID == Components.Util.CommunityID ? "Community Calls" : "Webcast Conversations")));
            hyperlinkBreadcumb.Text = strNav.ToString();
        }

        public string GetImageName(int contentID)
        {
            var oController = new Components.DBController();
            bool IsLike = oController.LikeIsLikeByUser(contentID, UserId);
            if (IsLike)
            {
                return "forum-recommended.png";
            }

            return "forum-recommend.png";
        }

        private int SaveCommentDB(int iParentID, string sComentText)
        {
            var oComment = new Components.Comments()
            {
                Approved = true,
                Author = UserInfo.DisplayName,
                Comment = sComentText,
                CommentID = -1,
                CreatedByUserID = UserInfo.UserID,
                CreatedOnDate = System.DateTime.Now,
                Email = UserInfo.Email,
                LastModifiedByUserID = UserInfo.UserID,
                LastModifiedOnDate = System.DateTime.Now,
                ParentId = iParentID,
                WCCId = GetQueryStringValue(Components.Util.PostKey, -1)
            };
            oComment = new Components.DBController().AddComments(oComment);
            return oComment == null ? -1 : oComment.CommentID;
        }

        private void AddComments(int iParentID, string sCommentText)
        {
            try
            {
                int iCommentID = SaveCommentDB(iParentID, sCommentText);
                string url = BuildQuery(GetQueryStringValue(Components.Util.PostKey, -1), iCommentID);
                PostUrl(url);
            }
            catch (Exception exc)
            { }
        }

        private void BindComments(int iwccID)
        {
            var oController = new Components.DBController();
            List<Components.Comments> oComments = oController.GetCommentByEventID(IsUserAdmin, iwccID, -1);
            rptComments.DataSource = oComments;
            rptComments.DataBind();
        }

        public void BindData()
        {
            var oController = new Components.DBController();

            //Hide details box for comments and other
            pnlDetail.Visible = false;

            System.Text.StringBuilder strdata = new System.Text.StringBuilder();
            string strTemplate = string.Empty;

            var iTotal = 0;
            int iRecordId = GetQueryStringValue(Components.Util.PostKey, -1);
            if (iRecordId > 0)
            {
                //Hide controls that don't need to show on detail 
                pnlListings.Visible = false;
                pnlDetail.Visible = true;
                Paging.Visible = false;
                var odata = oController.Get_Post(-1, CurrentCatID, "All", UserId, "", -1, iRecordId);

                strTemplate = ReadTemplate("WCCDetail");
                bool ShowRecord = true;

                if (odata != null)
                {
                    if (IsUserAdmin)
                        strdata.Append(ReplaceToken(odata, strTemplate));
                    else if (odata.IsPublish)
                    {
                        strdata.Append(ReplaceToken(odata, strTemplate));
                    }
                    else
                        ShowRecord = false; //Make sure it will show record : Change 02/11

                    //Update UserComponenet
                    if (odata != null)
                    {
                        //Update hasseen flag for Analytics by Vishal Patel
                        ClearAction.Modules.WebCast.Data.SqlDataProvider oCtrl = new ClearAction.Modules.WebCast.Data.SqlDataProvider();
                        oCtrl.UpdateTopicSeenStatus(UserId, iRecordId, odata.ComponentID);
                        //Update to-do, done and view counter

                        //oController.UpdateUserComponentStatus(odata.WCCId, UserInfo.UserID, odata.ComponentID); // Comment by Fali for status is updating issue.

                        odata.ViewCount = odata.ViewCount + 1;
                        oController.UpdateWCC_Post(odata);

                        //Show Comments and box
                        pnlDetail.Visible = true;
                        BindComments(iRecordId);

                        // Bind Breadcumb

                        System.Text.StringBuilder strBreadcumb = new System.Text.StringBuilder();
                        strBreadcumb.Append(CreateBreadData());
                        strBreadcumb.Append(string.Format("<a href='{0}'>{1}</a>&nbsp;>&nbsp;", BuildQuery(CurrentCatID, SortBy, -1, "", IsMyVault, SubOption, ComponentID), ComponentID == -1 ? "All Events" : (ComponentID == Components.Util.CommunityID ? "Community Calls" : "Webcast Conversations")));
                        strBreadcumb.Append(string.Format("{0}", odata.Title));
                        ltrlDetailBreadcumb.Text = strBreadcumb.ToString();

                        //string strBreadcubm = CreateBreadData();

                        //Send Email notification to user if they do complete
                        if (GetModuleSettings("SENDEMAILNOTIFICATION", "0").ToLower() == "true")
                        {
                            int iMaxRecord = GetModuleSettings(Components.Util.MAX_SELFASSIGNED_EVENT, 0);
                            var oTotalCount = oController.GetEmailNotificationFromComponenet(UserInfo.UserID, odata.ComponentID, iMaxRecord);

                            //If unnotified and Maxcount same send email notification and above stored procedure already mark all as Notified

                            if (oTotalCount == iMaxRecord)
                            {
                                //sendEmail notification

                                DotNetNuke.Services.Social.Messaging.Message m = new DotNetNuke.Services.Social.Messaging.Message();
                                var oFromUser = UserController.Instance.GetUserById(0, PortalSettings.AdministratorId);


                                //To Do: Chanage Body and subject
                                m.Body = "Congrts You have finished your to-do on digital events - DEV";

                                m.SenderUserID = PortalSettings.AdministratorId;

                                m.Subject = "Congrts Email Notification";
                                m.NotificationTypeID = 26;
                                m.From = oFromUser.Email;
                                m.To = UserInfo.Email;

                                IList<UserInfo> lst = new List<UserInfo>();
                                lst.Add(UserInfo);
                                DotNetNuke.Services.Social.Messaging.MessagingController.Instance.SendMessage(m, null, lst, null, oFromUser);

                            }
                        }

                        //Attributes for action menus
                        lnkSave.Attributes.Add("currentPostContenetId", iRecordId.ToString());
                        imgUploadImagesTop.Attributes.Add("cid", iRecordId.ToString());

                        //List of Recent Comment by User
                        // Commented by @SP
                        //var olist = oController.RecentMemberByEvent(iRecordId);
                        //rptActiveMember.DataSource = olist;
                        //rptActiveMember.DataBind();
                        //lblMemberCount.Text = olist == null ? "0" : Convert.ToString(olist.Count); 

                        //Bind list of Attachements

                        // Commented by @SP
                        //var olistfiles = oController.GetAttachmentByPost(iRecordId).OrderBy(x => x.UserID);
                        ////   rptSharedFiles.DataSource = olistfiles;

                        //// rptSharedFiles.DataBind();


                        //int iCurrentID = -1;
                        //strTemplate = ReadTemplate("WCCSharedFiles");
                        //System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                        //foreach (AttachmentInfo oattach in olistfiles)
                        //{
                        //    string strTemp = strTemplate;

                        //    if (iCurrentID != oattach.UserID)
                        //    {
                        //        strTemp = strTemp.Replace("{DISPLAY}", "block");
                        //    }
                        //    else
                        //    {
                        //        strTemp = strTemp.Replace("{DISPLAY}", "none");
                        //    }
                        //    iCurrentID = oattach.UserID;
                        //    strTemp = strTemp.Replace("{DISPLAYNAME}", GetDisplayName(oattach.UserID));
                        //    strTemp = Components.Util.ReplaceToken_Attach(strTemp, oattach);
                        //    stringBuilder.Append(strTemp);
                        //}
                        //ltrlFiles.Text = stringBuilder.ToString();
                        //lblNum.Text = olist == null ? "0" : Convert.ToString(olistfiles.Count());
                    }
                }
                else
                {
                    strdata.Append(ReadTemplate("NoRecord"));
                }

                //Display Records/no data on UI
                ltrDetail.Text = ShowRecord == false ? ReadTemplate("NoRecord") : strdata.ToString();
            }
            else
            {
                // var iListRecord = oController.Get_Posts((IsMyVault == 1 ? this.UserId : -1), CurrentCatID, strSubOption, (IsMyVault == 1 ? -1 : this.UserId), strSearchText, -1, IsUserAdmin, PageSize, CurrentPage, SortBy, out iTotal);
                strTemplate = ReadTemplate("WCCListings");
                TotalRecord = iTotal;
                ltrlCtrl.Visible = false;

                pnlCC.Visible = false;
                pnlWCC.Visible = false;
                Paging.Visible = false;
                string strParseRecord = string.Empty;
                //if (ComponentID == -1)
                //{
                //    strParseRecord = GetRecordByComponentID(Components.Util.CommunityID, strTemplate, out iTotal);
                //    if (iTotal != 0)
                //    {
                //        ltrCCctrl.Text = strParseRecord;
                //        pnlCC.Visible = true;
                //    }
                //    TotalRecord = iTotal;
                //    hyperlinkCommunity.Visible = iTotal > 5 ? true : false;

                //    strParseRecord = GetRecordByComponentID(Components.Util.WebCastID, strTemplate, out iTotal);
                //    if (iTotal != 0)
                //    {
                //        ltrWcctrl.Text = strParseRecord;
                //        pnlWCC.Visible = true;
                //    }
                //    if (iTotal > TotalRecord)
                //        TotalRecord = iTotal;
                //    hyperWebCastCalls.Visible = iTotal > 5 ? true : false;
                //    //TotalRecord = iTotal + TotalRecord;
                //    //  TotalRecord = Convert.ToInt32(TotalRecord / 2);
                //}
                //else
                //{
                //    if (ComponentID == 4)
                //    {
                //        strParseRecord = GetRecordByComponentID(Components.Util.CommunityID, strTemplate, out iTotal);
                //        if (iTotal != 0)
                //        {
                //            ltrCCctrl.Text = strParseRecord;
                //            pnlCC.Visible = true;
                //            TotalRecord = iTotal;// + TotalRecord;
                //        }

                //    }
                //    else
                //    {
                //        strParseRecord = GetRecordByComponentID(Components.Util.WebCastID, strTemplate, out iTotal);
                //        if (iTotal != 0)
                //        {
                //            ltrWcctrl.Text = strParseRecord;
                //            pnlWCC.Visible = true;
                //            TotalRecord = iTotal;// + TotalRecord;
                //        }
                //    }

                //    if (TotalRecord > PageSize)
                //    {
                //        SetPageButtons();
                //        var paging = TotalRecord / PageSize + ((TotalRecord % PageSize) > 0 ? 1 : 0);
                //        lblPagerInfo.Text = string.Format("{0} of {1}", CurrentPage.ToString(), paging);
                //        Paging.Visible = true;
                //    }
                //}

                strParseRecord = GetRecordByComponentID(ComponentID, strTemplate, out iTotal);
                if (iTotal != 0)
                {
                    ltrCCctrl.Text = strParseRecord;
                    pnlCC.Visible = true;
                    TotalRecord = iTotal;// + TotalRecord;
                }


                if (TotalRecord > PageSize)
                {
                    SetPageButtons();
                    var paging = TotalRecord / PageSize + ((TotalRecord % PageSize) > 0 ? 1 : 0);
                    lblPagerInfo.Text = string.Format("{0} of {1}", CurrentPage.ToString(), paging);
                    Paging.Visible = true;
                }

                //lblHeader.Text = string.Format("<h2>All {0}, by {1}</h2>", ComponentID == -1 ? "Digital Events" : (ComponentID == Components.Util.CommunityID ? "Community Calls" : "Webcast Conversations"), SortInfo());
                if (TotalRecord == 0)
                {
                    ltrlCtrl.Text = ReadTemplate("NoRecord");
                    pnlCC.Visible = false;
                    pnlWCC.Visible = false;
                    pnlDetail.Visible = false;
                    ltrlCtrl.Visible = true;
                    //lblHeader.Visible = false;
                }
                else
                    ltrlCtrl.Text = strdata.ToString();// : strTemplate;
            }
        }

        private string GetRecordByComponentID(int iComponentID, string strTemplate, out int iTotal)
        {
            var oController = new Components.DBController();

            string strSearchText = Server.UrlDecode(GetQueryStringValue("q", ""));
            iTotal = 0;
            string strSubOption = "All";
            switch (SubOption)
            {
                case 1:
                    strSubOption = "To-Do"; break;
                case 0:
                    strSubOption = "All";
                    break;
                default:
                    strSubOption = "Completed";
                    break;

            }
            var iListRecord = oController.Get_Posts((IsMyVault == 1 ? this.UserId : -1), CurrentCatID, strSubOption, (IsMyVault == 1 ? -1 : this.UserId), strSearchText, iComponentID, IsUserAdmin, PageSize, CurrentPage, SortBy, out iTotal);

            // iTotal = iListRecord.Count;


            System.Text.StringBuilder strdata = new System.Text.StringBuilder();



            foreach (Components.WCC_PostInfo odata in iListRecord)
            {
                // if (ComponentID == -1)
                strdata.Append(ReplaceToken(odata, strTemplate));

            }
            if ((iListRecord == null || iListRecord.Count == 0) && iComponentID != -1)
            {
                return string.Format("Sorry, No record found for Digial Events : <b>{0}</b> on current Page", iComponentID == Components.Util.CommunityID ? "Community Calls" : "WebCast Calls");
            }
            return strdata.ToString();

        }

        //private string GetRecords(string strTemplate, out int iTotal)
        //{
        //    var oController = new Components.DBController();

        //    string strSearchText = Server.UrlDecode(GetQueryStringValue("q", ""));
        //    iTotal = 0;
        //    string strSubOption = "All";
        //    switch (SubOption)
        //    {
        //        case 1:
        //            strSubOption = "To-Do"; break;
        //        case 0:
        //            strSubOption = "All";
        //            break;
        //        default:
        //            strSubOption = "Completed";
        //            break;

        //    }
        //    var iListRecord = oController.Get_Posts((IsMyVault == 1 ? this.UserId : -1), CurrentCatID, strSubOption, (IsMyVault == 1 ? -1 : this.UserId), strSearchText, -1, IsUserAdmin, PageSize, CurrentPage, SortBy, out iTotal)
        //                                 .OrderByDescending(e => e.EventDate)
        //                                 .ThenByDescending(e => e.EventTime)
        //                                 .ToList();

        //    System.Text.StringBuilder strdata = new System.Text.StringBuilder();
        //    foreach (Components.WCC_PostInfo odata in iListRecord)
        //    {
        //        strdata.Append(ReplaceToken(odata, strTemplate));
        //    }

        //    if (iListRecord == null || iListRecord.Count == 0)
        //    {
        //        return string.Format("Sorry, No record found for Events on current Page");
        //    }
        //    return strdata.ToString();

        //}

        public void BindCategory()
        {
            var controller = new Components.DBController();
            var oGlobalCategory = controller.GetGlobalCategory(-1).ToList();
            dataListGlobalCat.DataSource = oGlobalCategory;

            dataListGlobalCat.DataBind();
        }

        private void SetPageButtons()
        {
            NavImageLast.Enabled = NavImageNext.Enabled = NavImagePrev.Enabled = NavFirstBtn.Enabled = false;
            //  NavImageLast1.Enabled = NavImageNext1.Enabled = NavImagePrev1.Enabled = NavFirstBtn1.Enabled = false;
            NavImageLast.CssClass = NavImageNext.CssClass = NavImagePrev.CssClass = NavFirstBtn.CssClass = "disabled";
            //   NavImageLast1.CssClass = NavImageNext1.CssClass = NavImagePrev1.CssClass = NavFirstBtn1.CssClass = "disabled";
            if (CurrentPage == 1)
            {
                NavImageNext.Enabled = true;
                NavImageLast.Enabled = true;
                NavImageLast.CssClass = NavImageNext.CssClass = "forum-prev";
                // NavImageNext1.Enabled = true;
                //  NavImageLast1.Enabled = true;
                //  NavImageLast1.CssClass = NavImageNext1.CssClass = "forum-prev";
            }
            if ((CurrentPage * PageSize) + 1 > TotalRecord)
            {
                NavImagePrev.Enabled = true;
                NavFirstBtn.Enabled = true;

                NavFirstBtn.CssClass = NavImagePrev.CssClass = "forum-prev";


                //  NavImagePrev1.Enabled = true;
                //  NavFirstBtn1.Enabled = true;

                //   NavFirstBtn1.CssClass = NavImagePrev1.CssClass = "forum-prev";
            }

            if ((CurrentPage * PageSize) + 1 <= TotalRecord && CurrentPage > 1)
            {
                NavImageNext.Enabled = true;
                NavImageLast.Enabled = true;
                NavImagePrev.Enabled = true;
                NavFirstBtn.Enabled = true;
                NavImageLast.CssClass = NavImageNext.CssClass = NavImagePrev.CssClass = NavFirstBtn.CssClass = "forum-prev";



                //  NavImageNext1.Enabled = true;
                //  NavImageLast1.Enabled = true;
                //   NavImagePrev1.Enabled = true;
                //   NavFirstBtn1.Enabled = true;
                //    NavImageLast1.CssClass = NavImageNext1.CssClass = NavImagePrev1.CssClass = NavFirstBtn1.CssClass = "forum-prev";

            }
        }

        #endregion

        #region "Events"
        private void MakeTotalEvent()
        {
            if (Session["TotalEvent"] == null)
            {
                var oData = new Components.DBController().GetPostCount();
                Session["TotalEvent"] = oData.ToString();

            }

            lblTopicCount.Text = string.Format("Events {0}", Session["TotalEvent"] == null ? "0" : Convert.ToString(Session["TotalEvent"]));
        }

        private void Initialize()
        {
            BindCategory();
            MakeTotalEvent();

            //hyperlinkCommunity.NavigateUrl = BuildQuery(CurrentCatID, SortBy, -1, "", IsMyVault, SubOption, Components.Util.CommunityID);
            //hyperlinkCommunity.Visible = ComponentID != Components.Util.CommunityID ? true : false;
            //hyperWebCastCalls.NavigateUrl = BuildQuery(CurrentCatID, SortBy, -1, "", IsMyVault, SubOption, Components.Util.WebCastID);
            //hyperWebCastCalls.Visible = ComponentID != Components.Util.WebCastID ? true : false;
            hyperlinkNew.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(this.TabId, "edit", "mid=" + ModuleId.ToString(), Components.Util.PostKey + "=-1");
            hyperlinkNew.Visible = IsUserAdmin;

            txtSearch.Text = Server.UrlDecode(GetQueryStringValue("q", ""));

            if (GetQueryStringValue("SortBy", -1) > -1)
                ddSortBy.Items.FindByValue(GetQueryStringValue("SortBy", "-1")).Selected = true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    Initialize();
                    CreateBreadcumb();
                    BindData();
                }
            }
            catch (Exception exc) //Module failed to load
            {
                //Session["FirstimeError"]="true";

                Exceptions.ProcessModuleLoadException(this, exc);

                if (Session["FirstimeError"] == null)
                    Session["FirstimeError"] = "true";

                if (!IsUserAdmin && Session["FirstimeError"] != null)
                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.HomeTabId));
            }
        }
        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = DotNetNuke.Common.Globals.PreventSQLInjection(txtSearch.Text);
            Response.Redirect(BuildQuery(CurrentCatID, SortBy, -1, Server.UrlEncode(txtSearch.Text), IsMyVault, SubOption));
        }
        protected void NavFirstBtn_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            CurrentPage = 0;
            BindData();
        }

        protected void NavImagePrev_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (CurrentPage > 1)
                CurrentPage = CurrentPage - 1;
            BindData();
        }

        protected void NavImageNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            // if (CurrentPage * PageSize < TotalRecord)
            CurrentPage = CurrentPage + 1;
            BindData();
        }

        protected void NavImageLast_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            CurrentPage = Convert.ToInt32(TotalRecord / PageSize);
            BindData();
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {

            string txtBody = txtComments.Text;

            //string txtAttachmentBody = UploadedAttachmentSavePost.Text;

            //if (!string.IsNullOrEmpty(txtAttachmentBody))
            //{
            //    txtBody = "" + txtAttachmentBody + "" + txtBody + "";
            //}
            int iCommentID = SaveCommentDB(-1, txtBody);
            string txtTextBoxAttachmentData = TextBoxAttachmentData.Text;

            SaveAttachment(iCommentID);
            int CurrentUserid = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID;
            DotNetNuke.Entities.Users.UserInfo user = DotNetNuke.Entities.Users.UserController.Instance.GetUserById(0, CurrentUserid);
            DBController dal2 = new DBController();
            string toEmail = dal2.GetEmail();
            string ContentSubject = dal2.GetCommentSubject(Components.Util.PostKey);
            string ContentAuthor = dal2.GetPresenter(Components.Util.PostKey);
            string Subject = ""; string BodyContent = "";

            Subject = user.DisplayName + ":Commented On Post in Event";
            BodyContent = user.DisplayName + ":Commented " + ContentAuthor + " Posts in Event.      " + "The Subject heading of the post:" + ContentSubject;

            DotNetNuke.Services.Mail.Mail.SendMail("members@clearaction.com", toEmail, "", "", toEmail, DotNetNuke.Services.Mail.MailPriority.High, Subject, DotNetNuke.Services.Mail.MailFormat.Text, System.Text.Encoding.UTF32, BodyContent, new string[0], "", "", "", "", true);

            //JavaScriptSerializer js = new JavaScriptSerializer();

            //List<AttachmentData> AttachmentDataDataList = js.Deserialize<List<AttachmentData>>(txtTextBoxAttachmentData);

            //foreach (AttachmentData item in AttachmentDataDataList)
            //{

            //    var oAttachInfo = new AttachmentInfo()
            //    {
            //        AttachId = -1,
            //        CommentID = iCommentID,
            //        ContentType = item.contenttype,
            //        DateAdded = System.DateTime.Now,
            //        FileName = item.filename,
            //        FileSize = string.IsNullOrEmpty(item.size) == true ? 0 : Convert.ToInt32(item.size),
            //        FileUrl = item.attachmentURL,
            //        UserID = UserId,
            //        WCCId = GetQueryStringValue(Util.PostKey, -1)

            //    };

            //    new DBController().AddAttachment(oAttachInfo);


            //}

            string url = BuildQuery(GetQueryStringValue(Components.Util.PostKey, -1), iCommentID);
            PostUrl(url);



        }

        private void PostUrl(string strUrl)
        {
            Response.Redirect(strUrl, true);
        }
        protected void btnMyVault_Click(object sender, EventArgs e)
        {

            PostUrl(BuildQuery(CurrentCatID, SortBy, -1, Server.UrlEncode(txtSearch.Text), 1, SubOption));
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {


            PostUrl(BuildQuery(CurrentCatID, SortBy, -1, Server.UrlEncode(txtSearch.Text), 0, SubOption));
        }

        protected void btnAllCat_Click(object sender, EventArgs e)
        {


            PostUrl(BuildQuery(-1, SortBy, -1, Server.UrlEncode(txtSearch.Text), IsMyVault, SubOption));
        }


        protected void ddSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iSortby = 0;
            if (ddSortBy.SelectedIndex > 0)
                iSortby = Convert.ToInt32(ddSortBy.SelectedValue);
            if (iSortby == -2)
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(this.TabId));
            PostUrl(BuildQuery(CurrentCatID, iSortby, -1, Server.UrlEncode(txtSearch.Text), 0, SubOption));
        }

        protected void dataListGlobalCat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                HyperLink lnkButton = (HyperLink)e.Item.FindControl("LinkButton1");
                var oCategory = (Components.GlobalCategory)e.Item.DataItem;


                lnkButton.Text = "<div class='table'><div class='table-cell'>" + oCategory.DisplayName + "</div></div>";
                lnkButton.Attributes.Add("CatID", oCategory.CategoryId.ToString());

                lnkButton.NavigateUrl = BuildQuery(oCategory.CategoryId, SortBy, -1, Server.UrlEncode(txtSearch.Text), IsMyVault, SubOption);


            }
        }


        protected void rptComments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField eCommentID = (HiddenField)e.Item.FindControl("hdnCommentId");
                if (eCommentID != null)
                {

                    Repeater rptInnercomments = (Repeater)e.Item.FindControl("rptInnerComments");
                    if (rptInnercomments != null)
                    {
                        Label lblReplyCount = (Label)e.Item.FindControl("lblReplyCount");

                        BindInnerLevelComment(Convert.ToInt32(eCommentID.Value), rptInnercomments, lblReplyCount);
                        if (lblReplyCount.Text != "0")
                        {
                            //Disable Delete Button
                            //  LinkButton LinkButtonDeleteReply = (LinkButton)e.Item.FindControl("LinkButtonDeleteReply");
                            //   LinkButtonDeleteReply.Visible = false;


                            ImageButton imgdelete = (ImageButton)e.Item.FindControl("imgdelete");
                            imgdelete.Visible = false;
                        }
                    }
                }


            }
        }

        private void BindInnerLevelComment(int ParentCommentID, Repeater rptInnercomments, Label lReply)
        {
            var oController = new Components.DBController();//.BlogContentController();

            if (rptInnercomments != null)
            {

                var ocommentList = oController.GetCommentByEventID(false, GetQueryStringValue(Components.Util.PostKey, -1), ParentCommentID);
                rptInnercomments.DataSource = ocommentList;
                rptInnercomments.DataBind();
                if (lReply != null)
                    lReply.Text = ocommentList == null ? "0" : ocommentList.Count.ToString();
            }
        }

        protected void rptInnerComments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField eCommentID = (HiddenField)e.Item.FindControl("hdnInnerCommentID");
                Repeater rptInnercomments = (Repeater)e.Item.FindControl("rptInnerCommentlevel2");
                if (eCommentID != null)
                {

                    if (rptInnercomments != null)
                    {
                        Label lblReplyCount = (Label)e.Item.FindControl("lblReplyinnerCount");
                        BindInnerLevelComment(Convert.ToInt32(eCommentID.Value), rptInnercomments, lblReplyCount);

                        //Disable Delete Button
                        //    LinkButton LinkButtonDeleteReply = (LinkButton)e.Item.FindControl("LinkButtonDeleteReply");
                        //    LinkButtonDeleteReply.Visible = false;


                        ImageButton imgdelete = (ImageButton)e.Item.FindControl("imgdelete");
                        imgdelete.Visible = false;
                    }

                    Button btn = (Button)e.Item.FindControl("lnkSve");
                    if (btn != null)
                    {
                        btn.Attributes.Add("currentPostContenetId", eCommentID.Value);
                    }
                }
            }
        }

        private void SaveAttachment(int iCommentID)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            List<AttachmentData> AttachmentDataDataList = js.Deserialize<List<AttachmentData>>(TextBoxAttachmentData.Text);

            foreach (AttachmentData item in AttachmentDataDataList)
            {

                var oAttachInfo = new AttachmentInfo()
                {
                    AttachId = -1,
                    CommentID = iCommentID,
                    ContentType = item.contenttype,
                    DateAdded = System.DateTime.Now,
                    FileName = item.filename,
                    FileSize = string.IsNullOrEmpty(item.size) == true ? 0 : Convert.ToInt32(item.size),
                    FileUrl = item.attachmentURL,
                    UserID = UserId,
                    WCCId = GetQueryStringValue(Util.PostKey, -1)

                };

                new DBController().AddAttachment(oAttachInfo);


            }
        }
        protected void rptComments_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField hCommentId = (HiddenField)e.Item.FindControl("CommentID");
            int iCommentid = hCommentId != null ? Convert.ToInt32(hCommentId.Value) : -1;
            if (e.CommandName == "SaveDisscussion")
            {
                if (hCommentId != null)
                {




                    TextBox txtInnerQuickReply = (TextBox)e.Item.FindControl("txtInnerQuickReply");
                    //  TextBox TextBoxAttachmentData = (TextBox)e.Item.FindControl("TextBoxAttachmentData");
                    if (txtInnerQuickReply.Text != "")
                    {

                        int iCommentID = SaveCommentDB(Convert.ToInt32(hCommentId.Value), txtInnerQuickReply.Text);

                        SaveAttachment(iCommentID);


                        //    string url = BuildQuery(GetQueryStringValue(Components.Util.PostKey, -1), iCommentID);
                        PostUrl(BuildQuery(GetQueryStringValue(Components.Util.PostKey, -1), iCommentID));



                    }
                }
            }
            else if (e.CommandName == "EditReply")
            {
                var oCommandInofo = new DBController().GetCommentByID(iCommentid);
                TextBox txtInnerQuickReply = (TextBox)e.Item.FindControl("txtQuickReplyEditOne");
                if (oCommandInofo != null)
                {
                    oCommandInofo.Comment = txtInnerQuickReply.Text;
                    new DBController().UpdateComments(oCommandInofo);
                }
            }
            else if (e.CommandName == "like")
            {
                AddLikes(Convert.ToInt32(hCommentId.Value), "Comment");
            }
            else if (e.CommandName == "delete" || e.CommandName == "deleteInnerReply")
            {
                DeleteComment(Convert.ToInt32(hCommentId.Value));
            }

            string url = BuildQuery(GetQueryStringValue(Components.Util.PostKey, -1), iCommentid);
            PostUrl(url);
        }


        protected void rptInnerCommentlevel2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField hCommentId = (HiddenField)e.Item.FindControl("hdnInnerCommentID");
            int iCommentid = hCommentId != null ? Convert.ToInt32(hCommentId.Value) : -1;
            if (e.CommandName == "EditReply")
            {
                var oCommandInofo = new DBController().GetCommentByID(iCommentid);
                TextBox txtInnerQuickReply = (TextBox)e.Item.FindControl("txtQuickReplyEditOne");
                if (oCommandInofo != null)
                {
                    oCommandInofo.Comment = txtInnerQuickReply.Text;
                    new DBController().UpdateComments(oCommandInofo);
                }
            }
            else if (e.CommandName == "delete" || e.CommandName == "deleteReplyOne")
            {
                DeleteComment(Convert.ToInt32(e.CommandArgument));
                string url1 = BuildQuery(GetQueryStringValue(Components.Util.PostKey, -1));
                PostUrl(url1);
            }
            string url = BuildQuery(GetQueryStringValue(Components.Util.PostKey, -1), iCommentid);
            PostUrl(url);
        }

        protected void rptInnerComments_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            HiddenField hCommentId = (HiddenField)e.Item.FindControl("hdnInnerCommentID");
            int iCommentid = hCommentId != null ? Convert.ToInt32(hCommentId.Value) : -1;
            if (e.CommandName == "SaveDisscussion")
            {
                if (hCommentId != null)
                {
                    TextBox txtInnerQuickReply = (TextBox)e.Item.FindControl("txtInnerQuickReply");
                    if (txtInnerQuickReply.Text != "")
                    {
                        // AddComments(Convert.ToInt32(hCommentId.Value), txtInnerQuickReply.Text);

                        iCommentid = SaveCommentDB(iCommentid, txtInnerQuickReply.Text);

                        SaveAttachment(iCommentid);

                    }
                }
            }

            else if (e.CommandName == "EditReply")
            {
                var oCommandInofo = new DBController().GetCommentByID(iCommentid);
                TextBox txtInnerQuickReply = (TextBox)e.Item.FindControl("txtQuickReplyEditOne");
                if (oCommandInofo != null)
                {
                    oCommandInofo.Comment = txtInnerQuickReply.Text;
                    new DBController().UpdateComments(oCommandInofo);
                }
            }

            else if (e.CommandName == "like")
            {
                AddLikes(Convert.ToInt32(e.CommandArgument), "Comment");
                //  Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(this.TabId, "", DAL2.BlogContent.Util.PostKey + "=" + GetQueryStringValue(DAL2.BlogContent.Util.PostKey, "-1")));
            }
            else if (e.CommandName == "delete" || e.CommandName == "deleteReplyOne")
            {
                DeleteComment(Convert.ToInt32(e.CommandArgument));
                string url1 = BuildQuery(GetQueryStringValue(Components.Util.PostKey, -1));
                PostUrl(url1);
            }

            string url = BuildQuery(GetQueryStringValue(Components.Util.PostKey, -1), iCommentid);
            PostUrl(url);
        }
        #endregion

        #region "Paging"

        private void AddLikes(int iContentID, string ItemType)
        {
            var Ctrl = new Components.DBController();
            Ctrl.Post(iContentID, UserInfo.UserID, ItemType);
        }

        private void DeleteComment(int iContentID)
        {
            var Ctrl = new Components.DBController();
            //   Ctrl.DeleteLikes(iContentID, UserInfo.UserID, "Comment");
            //Delete comment as well like/dislike with that comment
            Ctrl.DeleteComment(iContentID);
        }
        public int CurrentPage
        {
            get
            {
                if (ViewState["PageNumber"] != null)
                {
                    return Convert.ToInt32(ViewState["PageNumber"]);
                }
                else
                {
                    ViewState["PageNumber"] = "1";
                    return 1;
                }
            }

            set
            {
                ViewState["PageNumber"] = value;
            }
        }


        private int TotalRecord
        {
            get
            {
                if (ViewState["TotalRecord"] != null)
                {
                    return Convert.ToInt32(ViewState["TotalRecord"]);
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                ViewState["TotalRecord"] = value;
            }
        }

        public int PageSize
        {
            get
            {
                int iValue = GetModuleSettings(Components.Util.PageSizeKey, 5);
                return iValue > 0 ? iValue : 5;
            }
        }

        protected void btnAllBlog_Click(object sender, EventArgs e)
        {
            PostUrl(BuildQuery(CurrentCatID, SortBy, -1, Server.UrlDecode(txtSearch.Text), IsMyVault, 0));

        }

        protected void btntodo_Click(object sender, EventArgs e)
        {
            PostUrl(BuildQuery(CurrentCatID, SortBy, -1, Server.UrlDecode(txtSearch.Text), IsMyVault, 1));
        }

        protected void btnDoneBlog_Click(object sender, EventArgs e)
        {
            PostUrl(BuildQuery(CurrentCatID, SortBy, -1, Server.UrlDecode(txtSearch.Text), IsMyVault, 2));
        }

        protected void lnkEventAll_Click(object sender, EventArgs e)
        {
            PostUrl(BuildQuery(CurrentCatID, SortBy, -1, Server.UrlDecode(txtSearch.Text), IsMyVault, GetQueryStringValue("Show", 0), -1));
        }

        protected void lnkEventCommunityCalls_Click(object sender, EventArgs e)
        {
            PostUrl(BuildQuery(CurrentCatID, SortBy, -1, Server.UrlDecode(txtSearch.Text), IsMyVault, GetQueryStringValue("Show", 0), 4));
        }

        protected void lnkWebCastCall_Click(object sender, EventArgs e)
        {
            PostUrl(BuildQuery(CurrentCatID, SortBy, -1, Server.UrlDecode(txtSearch.Text), IsMyVault, GetQueryStringValue("Show", 0), 5));

        }

        #endregion



    }
}