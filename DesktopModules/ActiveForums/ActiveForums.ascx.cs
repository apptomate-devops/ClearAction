
// Active Forums - http://www.dnnsoftware.com
// Copyright (c) 2013
// by DNN Corp.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
//
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using System.Linq;
using System.Collections.Generic;
using DotNetNuke.Modules.ActiveForums.DAL2;
using DotNetNuke.Services.Social.Notifications;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Common.Utilities;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Modules.ActiveForums.DAL2.Attachment;
using System.Net.Mail;

namespace DotNetNuke.Modules.ActiveForums
{

    public partial class ActiveForums : ForumBase, IActionable
    {
        #region DNN Actions

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                {
                    {
                        GetNextActionID(), Utilities.GetSharedResource("[RESX:ControlPanel]"),
                        ModuleActionType.AddContent, string.Empty, string.Empty, EditUrl(), false,
                        Security.SecurityAccessLevel.Edit, true, false
                    }
                };

                return actions;
            }
        }

        #endregion


        //private CA_UserSession CA_UserSelected {
        //    get
        //    {
        //        try
        //        {
        //            if ( (Session["CA_UserSelections"] != null) || (Request.QueryString["CategoryId"] != null))
        //            {
        //                // this session object is written in control AF_TOPHEADER.ASCX.CS
        //                    return (CA_UserSession)Session["CA_UserSelections"];
        //            }
        //            else
        //            {
        //                return new CA_UserSession();
        //            }
        //        }
        //        catch (Exception ex) { return new CA_UserSession(); }
        //       // return newSelections;
        //    }
        //}

        private bool IsShowAll = false;

        #region "Property"


        private int iCategoryID
        {
            get
            {
                return GetQueryStringValue("CategoryID", -1);

            }
        }

        private int iTopicID
        {
            get
            {
                return GetQueryStringValue("TopicID", -1);

            }
        }

        public int CurrentPage
        {
            get
            {
                if (ViewState["PageNumber"] != null)
                    return Convert.ToInt32(ViewState["PageNumber"]);
                else
                    return 0;
            }
            set
            {
                ViewState["PageNumber"] = value;
            }
        }

        private int iContentId
        {
            get
            {
                return GetQueryStringValue("ContentId", -1);

            }
        }

        private int _TotalRecord;

        private int TotalRecord
        {
            get
            {
                if (ViewState["TotalRecord"] != null)
                    return Convert.ToInt32(ViewState["TotalRecord"]);
                else
                    return 0;
            }
            set
            {
                ViewState["TotalRecord"] = value;
            }
        }

        private int PageSize
        {
            get
            {

                return 20;// Settings.GetInt(SettingKeys.PageSize, 10);
            }

        }



        #endregion

        #region Event Handlers

        protected void rptDissussionInner_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int TopicID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnInsideTopic")).Value);
                int ReplyId = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnReplyTo")).Value);

                Repeater rInnerDissuesion = (Repeater)e.Item.FindControl("rptTopicDisuccsionInner");

                if (rInnerDissuesion != null)
                {
                    var oData = new DAL2.Dal2Controller().GetDisucssionReply(IsUserAdmin, TopicID, ReplyId);
                    rInnerDissuesion.DataSource = oData;
                    rInnerDissuesion.DataBind();

                    Label lReplyCount = (Label)e.Item.FindControl("lblReplyCount");
                    if (lReplyCount != null)
                        lReplyCount.Text = oData.Count().ToString();
                }




            }
        }
        protected void rptTopicDissusssion_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int TopicID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnInsideTopic")).Value);
                int ReplyId = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnReplyTo")).Value);

                Repeater rInnerDissuesion = (Repeater)e.Item.FindControl("rptDissussionInner");

                if (rInnerDissuesion != null)
                {
                    var oData = new DAL2.Dal2Controller().GetDisucssionReply(IsUserAdmin, TopicID, ReplyId);
                    rInnerDissuesion.DataSource = oData;
                    rInnerDissuesion.DataBind();

                    Label lReplyCount = (Label)e.Item.FindControl("lblReplyCount");
                    if (lReplyCount != null)
                        lReplyCount.Text = oData.Count().ToString();
                }




            }

        }

        private string GetRequestUrl
        {
            get
            {
                string url = BuildQuery(GetQueryStringValue("CategoryID", -1), GetQueryStringValue("SortBy", -1), GetQueryStringValue("TopicID", -1), GetQueryStringValue("q", ""), GetQueryStringValue("ContentID", -1));
                return url;
            }
        }


        public string EditedReplyContent(string AuthorName, DateTime UpdatedDate)
        {
            string retunEditReplyContent = "";
            try
            {
                if (!string.IsNullOrEmpty(AuthorName))
                {

                    retunEditReplyContent = "<span>:: EDITED BY " + AuthorName + " " + UpdatedDate.ToString("MMM dd yyyy") + "</span>";
                }
            }
            catch
            { retunEditReplyContent = ""; }

            return retunEditReplyContent;
        }


        public string EditedReplyContentSecond(string AuthorName, DateTime UpdatedDate)
        {
            string retunEditReplyContent = "";
            try
            {
                if (!string.IsNullOrEmpty(AuthorName))
                {

                    retunEditReplyContent = "<span>:: EDITED BY " + AuthorName + " " + UpdatedDate.ToString("MMM dd yyyy") + "</span>";
                }
            }
            catch
            { retunEditReplyContent = ""; }

            return retunEditReplyContent;
        }


        public string EditedReplyContentThird(string AuthorName, DateTime UpdatedDate)
        {
            string retunEditReplyContent = "";
            try
            {
                if (!string.IsNullOrEmpty(AuthorName))
                {

                    retunEditReplyContent = "<span>:: EDITED BY " + AuthorName + " " + UpdatedDate.ToString("MMM dd yyyy") + "</span>";
                }
            }
            catch
            { retunEditReplyContent = ""; }

            return retunEditReplyContent;
        }

        protected void rptTopicDissusssion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "SaveDisscussion")
            {

                //string txtBody = ((System.Web.UI.HtmlControls.HtmlTextArea)e.Item.FindControl("txtInnerQuickReply")).Value;
                string txtBody = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtInnerQuickReply")).Text;

                string txtAttachmentBody = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("UploadedAttachmentSavePost")).Text;

                if (!string.IsNullOrEmpty(txtAttachmentBody))
                {
                    //txtBody = "" + txtAttachmentBody + "" + txtBody + "";
                }

                string txtTextBoxAttachmentData = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("TextBoxAttachmentData")).Text;


                JavaScriptSerializer js = new JavaScriptSerializer();

                List<AttachmentData> AttachmentDataDataList = js.Deserialize<List<AttachmentData>>(txtTextBoxAttachmentData);

                Data.AttachController acon = new Data.AttachController();






                if (string.IsNullOrEmpty(txtBody))
                    return;
                string hdnSubject = ((HiddenField)e.Item.FindControl("hdnInnerSubject")).Value;
                int iCategoryID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnCategoryID")).Value);
                int TopicID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnInsideTopic")).Value);
                int ReplyId = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnReplyTo")).Value);
                int NewReplyId = SaveQuickReply(TopicID, iCategoryID, hdnSubject, txtBody, ReplyId);



                foreach (AttachmentData item in AttachmentDataDataList)
                {

                    int contentId = 0;
                    int.TryParse(item.contentid, out contentId);


                    if (contentId != 0 && !string.IsNullOrEmpty(item.attachmentURL))
                    {
                        acon.AttachmentSaveAfterPost(contentId, item.attachmentURL, NewReplyId);
                    }

                }



                UpdateTopicSeenStatus(TopicID);
                int ContentId = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnContentId")).Value);

                //string url = MakeURL(TopicID);
                string url = MakeURLAfterReplyOperatin(TopicID, NewReplyId);
                Response.Redirect(url, true);
            }

            if (e.CommandName == "EditReply")
            {

                string txtBody = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtQuickReplyEditOne")).Text;

                string txtAttachmentBody = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("TextBoxAttachmentContentOne")).Text;

                if (!string.IsNullOrEmpty(txtAttachmentBody))
                {
                    //txtBody = "" + txtAttachmentBody + "" + txtBody + "";
                }


                if (string.IsNullOrEmpty(txtBody))
                    return;
                string hdnSubject = ((HiddenField)e.Item.FindControl("hdnInnerSubject")).Value;
                int iCategoryID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnCategoryID")).Value);
                int TopicID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnInsideTopic")).Value);
                int ReplyId = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnReplyTo")).Value);



                int HiddenFieldCurrentContentId = Convert.ToInt32(((HiddenField)e.Item.FindControl("HiddenFieldCurrentContentId")).Value);


                Data.AttachController acon = new Data.AttachController();
                string UpdateDate = DateTime.Now.ToString();
                int UpdatedByUserId = UserInfo.UserID;
                string UpdatedByUserName = UserInfo.DisplayName;
                acon.UpdateReplyPost(HiddenFieldCurrentContentId, txtBody, UpdateDate, UpdatedByUserId, UpdatedByUserName);



                UpdateTopicSeenStatus(TopicID);
                int ContentId = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnContentId")).Value);

                //string url = MakeURL(TopicID);
                string url = MakeURLAfterReplyOperatin(TopicID, ReplyId);
                Response.Redirect(url, true);
            }

            if (e.CommandName == "EditDisscussionReply")
            {

                string txtBody = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtQuickReplySecond")).Text;

                string txtAttachmentBody = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("TextBoxAttachmentSecond")).Text;

                if (!string.IsNullOrEmpty(txtAttachmentBody))
                {
                    //txtBody = "" + txtAttachmentBody + "" + txtBody + "";
                }


                if (string.IsNullOrEmpty(txtBody))
                    return;
                string hdnSubject = ((HiddenField)e.Item.FindControl("hdnInnerSubject")).Value;
                int iCategoryID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnCategoryID")).Value);
                int TopicID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnInsideTopic")).Value);
                int ReplyId = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnReplyTo")).Value);



                int HiddenFieldCurrentContentId = Convert.ToInt32(((HiddenField)e.Item.FindControl("HiddenFieldCurrentContentId")).Value);


                Data.AttachController acon = new Data.AttachController();
                string UpdateDate = DateTime.Now.ToString();
                int UpdatedByUserId = UserInfo.UserID;
                string UpdatedByUserName = UserInfo.DisplayName;
                acon.UpdateReplyPost(HiddenFieldCurrentContentId, txtBody, UpdateDate, UpdatedByUserId, UpdatedByUserName);



                UpdateTopicSeenStatus(TopicID);
                int ContentId = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnContentId")).Value);

                //string url = MakeURL(TopicID);
                string url = MakeURLAfterReplyOperatin(TopicID, ReplyId);
                Response.Redirect(url, true);

            }

            if (e.CommandName == "delete")
            {
                int TopicID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnInsideTopic")).Value);

                int ReplyTo = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnReplyTo")).Value);


                int intContentid = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnContentId")).Value);
                Data.AttachController acon = new Data.AttachController();
                acon.DeleteAttachmentWithPost(intContentid);

                new DAL2.Dal2Controller().ReplyDelete(ForumId, TopicID, ReplyTo, 1);



                //string URL = MakeURL(TopicID);
                string URL = MakeURLAfterReplyOperatin(TopicID, TopicID);
                Response.Redirect(URL, true);
            }
            if (e.CommandName == "like")
            {
                int ContentId = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnContentId")).Value);
                new LikesController().Like(ContentId, UserInfo.UserID);
                //  Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(this.TabId) + "", true);
                Response.Redirect(GetRequestUrl, true);
            }

        }

        protected void NavFirstBtn_Click(object sender, ImageClickEventArgs e)
        {
            CurrentPage = 1;
            BindTopics(1);
        }
        protected void NavImageNext_Click(object sender, ImageClickEventArgs e)
        {
            CurrentPage = CurrentPage + 1;
            BindTopics(1);
        }
        protected void NavImagePrev_Click(object sender, ImageClickEventArgs e)
        {
            CurrentPage = CurrentPage - 1;
            BindTopics(1);
        }
        protected void NavImageLast_Click(object sender, ImageClickEventArgs e)
        {
            CurrentPage = Convert.ToInt32(TotalRecord / PageSize);
            BindTopics(1);
        }

        public string MakeURL(int TopicID)
        {
            if (TopicID == -1)
                TopicID = GetQueryStringValue("TopicID", TopicID);
            string url = BuildQuery(GetQueryStringValue("CategoryID", -1), GetQueryStringValue("SortBy", -1), TopicID, GetQueryStringValue("q", ""), TopicID);
            int _Start = url.IndexOf("ContentID");
            return url.Insert(_Start, "PI/" + CurrentPage + "/");
        }

        public string MakeURLAfterReplyOperatin(int TopicID, int ReplyId)
        {
            if (TopicID == -1)
                TopicID = GetQueryStringValue("TopicID", TopicID);
            string url = BuildQuery(GetQueryStringValue("CategoryID", -1), GetQueryStringValue("SortBy", -1), TopicID, GetQueryStringValue("q", ""), ReplyId);
            int _Start = url.IndexOf("ContentID");
            return url.Insert(_Start, "PI/" + CurrentPage + "/");
        }

        public class AttachmentData
        {
            public string contentid { get; set; }
            public string currentContentId { get; set; }
            public string attachmentURL { get; set; }

        }

        protected void rptTopics_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ReplySave")
            {
                int iCategoryID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnCategoryID")).Value);
                int TopicID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnTopicId")).Value);
                int ContentID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnContentId")).Value);
                string hdnSubject = ((HiddenField)e.Item.FindControl("hdnSubject")).Value;

                string txtBody = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtQuickReply")).Text;

                string AttachmentHTML = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("UploadedAttachmentSavePost")).Text;


                string txtTextBoxAttachmentData = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("TextBoxAttachmentData")).Text;


                JavaScriptSerializer js = new JavaScriptSerializer();

                List<AttachmentData> AttachmentDataDataList = js.Deserialize<List<AttachmentData>>(txtTextBoxAttachmentData);

                Data.AttachController acon = new Data.AttachController();




                if (!string.IsNullOrEmpty(AttachmentHTML))
                {
                    //txtBody = "" + AttachmentHTML + "" + txtBody + "";
                }

                if (string.IsNullOrEmpty(txtBody))
                    return;

                int NewReplyId = SaveQuickReply(TopicID, iCategoryID, hdnSubject, txtBody, TopicID);



                foreach (AttachmentData item in AttachmentDataDataList)
                {

                    int contentId = 0;
                    int.TryParse(item.contentid, out contentId);


                    if (contentId != 0 && !string.IsNullOrEmpty(item.attachmentURL))
                    {
                        acon.AttachmentSaveAfterPost(contentId, item.attachmentURL, NewReplyId);
                    }

                }



                UpdateTopicSeenStatus(TopicID);


                try
                {
                    int CurrentUserid = DotNetNuke.Entities.Users.UserController.Instance.GetCurrentUserInfo().UserID;
                    DotNetNuke.Entities.Users.UserInfo user = DotNetNuke.Entities.Users.UserController.Instance.GetUserById(0, CurrentUserid);
                    Dal2Controller dal2 = new Dal2Controller();
                    string toEmail = dal2.GetEmail();
                    string ContentSubject = dal2.GetSubject(ContentID);
                    string ContentAuthor = dal2.GetAuthor(ContentID);
                    string Subject = ""; string BodyContent = "";

                
                  
                    Subject = user.DisplayName + ":Commented On Post in Forum";
                    BodyContent = user.DisplayName + ":Commented " + ContentAuthor + " Posts in Forum.      " + "The Subject heading of the post:" + ContentSubject;

                    DotNetNuke.Services.Mail.Mail.SendMail("members@clearaction.com",toEmail, "", "", toEmail, DotNetNuke.Services.Mail.MailPriority.High, Subject, DotNetNuke.Services.Mail.MailFormat.Text, System.Text.Encoding.UTF32, BodyContent, new string[0], "", "", "", "", true);
                
                   
                }
                catch (Exception ex)
                {
                  
                }




                //string url = MakeURL(TopicID);
                string url = MakeURLAfterReplyOperatin(TopicID, NewReplyId);
                Response.Redirect(url, true);
            }

            if (e.CommandName == "EditReply")
            {
                int iCategoryID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnCategoryID")).Value);

                int HiddenFieldCurrentContentId = Convert.ToInt32(((HiddenField)e.Item.FindControl("HiddenFieldCurrentContentId")).Value);

                int TopicID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnTopicId")).Value);
                int ContentID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnContentId")).Value);
                string hdnSubject = ((HiddenField)e.Item.FindControl("hdnSubject")).Value;


                string txtBody = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtQuickReplyEditOne")).Text;

                string AttachmentHTML = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("TextBoxAttachmentContentOne")).Text;


                //string txtTextBoxAttachmentData = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("TextBoxAttachmentData")).Text;






                Data.AttachController acon = new Data.AttachController();




                if (!string.IsNullOrEmpty(AttachmentHTML))
                {
                    //txtBody = "" + AttachmentHTML + "" + txtBody + "";
                }

                if (string.IsNullOrEmpty(txtBody))
                    return;

                int NewReplyId = SaveQuickReply(TopicID, iCategoryID, hdnSubject, txtBody, TopicID);






                UpdateTopicSeenStatus(TopicID);

                //string url = MakeURL(TopicID);
                string url = MakeURLAfterReplyOperatin(TopicID, NewReplyId);
                Response.Redirect(url, true);
            }


            if (e.CommandName == "like")
            {
                new LikesController().Like(Convert.ToInt32(e.CommandArgument), UserInfo.UserID);
                Response.Redirect(GetRequestUrl, true);
            }

            if (e.CommandName == "SaveAttachment")
            {
                int ContentID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnContentId")).Value);
                FileUpload file = ((FileUpload)e.Item.FindControl("flupload"));

                if (file.PostedFile.FileName != "")
                {
                    SaveAttachments(ContentID, file);
                    Response.Redirect(GetRequestUrl, true);
                }
            }
        }

        protected void rptTopics_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CA_TopicsEx rowData = ((DotNetNuke.Modules.ActiveForums.CA_TopicsEx)e.Item.DataItem);

                HiddenField hdnTopic = (HiddenField)e.Item.FindControl("hdnTopicId");
                if (hdnTopic != null)
                {
                    var oReplyController = new Dal2Controller();
                    Repeater rptinside = (Repeater)e.Item.FindControl("rptTopicDissusssion");
                    if (rptinside != null)
                    {
                        //  var oReplyController = new Dal2Controller();
                        var oData = oReplyController.GetTopicReply(IsUserAdmin, Convert.ToInt32(hdnTopic.Value)).ToList();
                        rptinside.DataSource = oData;
                        rptinside.DataBind();

                        Label lTotalcount = (Label)e.Item.FindControl("lblTotalCount");
                        if (lTotalcount != null)
                            lTotalcount.Text = oData.Count.ToString();
                    }

                    //Bind Images for like/dislike

                    HyperLink hypertopicdetail = (HyperLink)e.Item.FindControl("hypertopicdetail");
                    if (hypertopicdetail != null)
                    {
                        hypertopicdetail.NavigateUrl = BuildQuery(GetQueryStringValue("CategoryID", -1), GetQueryStringValue("SortBy", -1), Convert.ToInt32(hdnTopic.Value));// MakeURL(Convert.ToInt32(hdnTopic.Value));
                        hypertopicdetail.Text = HttpUtility.UrlDecode(Server.HtmlDecode(rowData.GetContent.Subject));
                    }



                    ///Bind Active Members


                    Repeater rptmember = (Repeater)e.Item.FindControl("rptActiveMember");
                    if (rptmember != null)
                    {


                        var oData = oReplyController.GetActiveMemberList(Convert.ToInt32(hdnTopic.Value));
                        rptmember.DataSource = oData;
                        rptmember.DataBind();

                        Label lbMemberCount = (Label)e.Item.FindControl("lblMemberCount");
                        if (lbMemberCount != null)
                            lbMemberCount.Text = oData.Count.ToString();
                        else
                            lbMemberCount.Text = "0";
                    }
                }

                HiddenField hdnAttachID = (HiddenField)e.Item.FindControl("hdnAttachID");
                if (hdnAttachID != null)
                {
                    int icount = 0;
                    Repeater rptShare = (Repeater)e.Item.FindControl("rptSharedFiles");
                    intAttachmentMemberId = 0;

                    if (rptShare != null)
                    {
                        var oController = new Dal2Controller();
                        var oData = oController.GetSharedFiles(IsUserAdmin, Convert.ToInt32(hdnAttachID.Value), out icount).ToList();

                        oData = oData.OrderBy(s => s.UserId).ToList();

                        rptShare.DataSource = oData;
                        rptShare.DataBind();

                        Label lTotalcount = (Label)e.Item.FindControl("lblNum");
                        if (lTotalcount != null)
                            lTotalcount.Text = oData.Count.ToString();
                    }
                }

                // User activity status 
                string strStatus = "";

                if (((DotNetNuke.Modules.ActiveForums.CA_TopicsEx)e.Item.DataItem).Status != null)
                {
                    strStatus = rowData.Status;
                }

                if (rowData.GetContent != null)
                {
                    var IsUserLike = new LikesController().CheckUserLikeOrDislikePost(rowData.GetContent.ContentId, UserId);
                    Image iLike = (Image)e.Item.FindControl("imgrecommanded");
                    //Image idiLike = (Image)e.Item.FindControl("imgunrecommanded");
                    string strImage = "/DesktopModules/ActiveForums/Images/{0}";

                    iLike.ImageUrl = string.Format(strImage, "forum-recommend.png");
                    //idiLike.ImageUrl = string.Format(strImage, "forum-unrecommend.png");

                    if (IsUserLike != null)
                    {

                        if (IsUserLike.Checked)
                        {
                            iLike.ImageUrl = string.Format(strImage, "forum-recommended.png");
                            //idiLike.ImageUrl = string.Format(strImage, "forum-unrecommend.png");


                        }
                        else
                        {
                            iLike.ImageUrl = string.Format(strImage, "forum-recommend.png");
                            //idiLike.ImageUrl = string.Format(strImage, "forum-unrecommended.png");
                        }
                    }

                }

                Label lblUserActivityStatus = (Label)e.Item.FindControl("lblUserActivityStatus");

                Image imgStatus = (Image)e.Item.FindControl("imgStatus");
                if ((lblUserActivityStatus != null) && (imgStatus != null))
                {
                    if (strStatus == "Completed")
                    {
                        lblUserActivityStatus.Text = "DONE";
                        imgStatus.ImageUrl = "/portals/0/img_17.png";
                    }
                    else if (strStatus == "To-Do")
                    {
                        lblUserActivityStatus.Text = "TO DO";
                        imgStatus.ImageUrl = "/portals/0/img_18.png";
                    }
                    else
                    {
                        imgStatus.Visible = false;
                        lblUserActivityStatus.Visible = false;
                    }
                }
                // Showing or Hiding the My Post Icon
                int iAuthorID = rowData.AuthorId;
                Panel pnlMyPost = (Panel)e.Item.FindControl("pnlMyPost");
                if (pnlMyPost != null)
                {
                    pnlMyPost.Visible = (iAuthorID != UserInfo.UserID || UserInfo.IsSuperUser) ? false : true;
                    HyperLink hyper = (HyperLink)e.Item.FindControl("hyperlink");
                    if (hyper != null)
                        hyper.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(this.TabId, "Topics", "mid=" + ModuleId.ToString(), "ForumId=4", "afv=Post", "action=crete", "TopicId=" + hdnTopic.Value);

                }

                // showing or hiding Forum Context Menu
                Panel pnlUserMenu = (Panel)e.Item.FindControl("pnlUserMenu");
                if (pnlUserMenu != null)
                {
                    pnlUserMenu.Visible = true;
                    if (pnlUserMenu.Visible)
                    {
                        Image imgUserMenu = (Image)e.Item.FindControl("imgUserMenu");
                        if (imgUserMenu != null)
                        {
                            imgUserMenu.Attributes.Add("TID", Convert.ToString(rowData.TopicId));
                            imgUserMenu.Attributes.Add("CID", Convert.ToString(rowData.ContentId));
                            imgUserMenu.Attributes.Add("Status", Convert.ToString(rowData.Status));
                            imgUserMenu.Attributes.Add("IsSelfAssigned", Convert.ToString(rowData.IsSelfAssigned));
                            imgUserMenu.Attributes.Add("class", "CA_UserMenu");

                        }

                        //Begin Added By Fali
                        Image ImagePostAcctachment = (Image)e.Item.FindControl("ImagePostAcctachment");
                        if (imgUserMenu != null)
                        {

                            ImagePostAcctachment.Attributes.Add("TID", Convert.ToString(rowData.TopicId));
                            ImagePostAcctachment.Attributes.Add("CID", Convert.ToString(rowData.ContentId));
                            ImagePostAcctachment.Attributes.Add("Status", Convert.ToString(rowData.Status));

                        }
                        //End Added By Fali

                    }
                }

                //Hide/Show Edit link for user


                HyperLink lnkEdit = (HyperLink)e.Item.FindControl("lnkEditTopic");
                if (lnkEdit != null)
                {
                    lnkEdit.Visible = IsUserAdmin;
                    lnkEdit.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(this.TabId, "Topics", "mid=" + ModuleId.ToString(), "ForumId=4", "afv=Post", "action=edit", "TopicId=" + hdnTopic.Value);
                }


                // Bind Body content
                Label lblBody = (Label)e.Item.FindControl("lblBody");
                if (lblBody != null)
                {
                    string strBody = rowData.GetContent.Body.Trim();
                    //if (strBody.Length > 200)
                    //    strBody = TruncateAtWord(strBody, 199, "");// strBody.Substring(0, 199);
                    while (strBody.EndsWith("<br>"))
                        strBody = strBody.Substring(0, strBody.Length - 4);
                    while (strBody.EndsWith("<br />"))
                        strBody = strBody.Substring(0, strBody.Length - 6);
                    lblBody.Text = HttpUtility.UrlDecode((Server.HtmlDecode(strBody)));
                }
            }
        }

        public string TruncateAtWord(string input, int length, string strUrl)
        {
            if (((input == null) || (input.Length < length)))
            {
                return input;
            }

            int iNextSpace = input.LastIndexOf(" ", length, StringComparison.Ordinal);
            return string.Format("{0}", input.Substring(0, length - 1).Trim());

        }

        protected void ddSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentPage = 1;
            BindTopics(1);
        }

        #region "Paging Buttons"
        private void SetPageButtons()
        {
            NavImageLast.Enabled = NavImageNext.Enabled = NavImagePrev.Enabled = NavFirstBtn.Enabled = false;
            NavFirstBtn2.Enabled = NavImagePre2.Enabled = NavImageNext2.Enabled = NavImageLast2.Enabled = false;


            if (CurrentPage == 1)
            {
                NavImageNext.Enabled = true;
                NavImageLast.Enabled = true;

                NavImageNext2.Enabled = true;
                NavImageLast2.Enabled = true;




            }
            if ((CurrentPage * PageSize) + 1 > TotalRecord)
            {
                NavImagePrev.Enabled = true;
                NavFirstBtn.Enabled = true;
                NavImagePre2.Enabled = true;
                NavFirstBtn2.Enabled = true;


            }

            if ((CurrentPage * PageSize) + 1 <= TotalRecord && CurrentPage > 1)
            {
                NavImageNext.Enabled = true;
                NavImageLast.Enabled = true;
                NavImagePrev.Enabled = true;
                NavFirstBtn.Enabled = true;

                NavImagePre2.Enabled = true;
                NavFirstBtn2.Enabled = true;
                NavImageNext2.Enabled = true;
                NavImageLast2.Enabled = true;
            }


        }
        #endregion

        private void BindTopics(int iPageIndex)
        {


            var Controller = new DAL2.Dal2Controller();
            int icount = 0;
            //var oquerydata = Controller.GetAllTopics(IsUserAdmin, GetQueryStringValue("CategoryID", -1), GetQueryStringValue("SortBy", 0), out icount);
            //  List<Topics> oquerydata = Controller.GetAllForumTopics(IsUserAdmin, GetQueryStringValue("CategoryID", -1), GetQueryStringValue("SortBy", 0)).ToList();
            string strSubOption = "All";
            if (Session["CA_Status"] != null)
            {
                int iStatus = int.Parse(Session["CA_Status"].ToString());
                strSubOption = ((iStatus == 0) ? "All" : ((iStatus == 1) ? "To-Do" : "Completed"));
            }
            int IsMyVault = 1;
            if (Session["CA_IsMyVault"] != null)
            {
                IsMyVault = int.Parse(Session["CA_IsMyVault"].ToString());
            }
            if (GetQueryStringValue("TopicID", -1) > 0)
                IsMyVault = 0;

            int CatID = -1;
            if (Session["CA_CategoryID"] != null)
            {
                CatID = int.Parse(Session["CA_CategoryID"].ToString());
            }
            else if (GetQueryStringValue("CategoryId", -1) != -1)
            {
                CatID = int.Parse(GetQueryStringValue("CategoryId", -1).ToString());
            }
            string SearchKey = Server.UrlDecode(GetQueryStringValue("q", ""));
            if (string.IsNullOrEmpty(SearchKey))
            {
                SearchKey = "";
                Session["CA_SearchKey"] = "";
            }

            List<CA_TopicsEx> oquerydata = Controller.GetAllForumTopics(((IsMyVault == 1) ? UserInfo.UserID : -1), CatID, strSubOption, ((IsMyVault == 0) ? UserInfo.UserID : -1), SearchKey, GetQueryStringValue("SortBy", 0)).ToList();

            if (GetQueryStringValue("TopicID", -1) > 0)
                oquerydata = oquerydata.Where(x => x.TopicId == GetQueryStringValue("TopicID", -1)).ToList();
            if (oquerydata != null)
            {
                int StarRow = ((CurrentPage - 1) * PageSize);
                rptTopics.DataSource = oquerydata.Skip(StarRow).Take(PageSize).ToList();
                rptTopics.DataBind();

                TotalRecord = oquerydata.Count();

                if (TotalRecord < PageSize)
                {
                    topPaging.Visible = false;
                    bottomPaging.Visible = false;
                    pnlNoRecords.Visible = TotalRecord == 0 ? true : false;
                }
                else
                {
                    SetPageButtons();
                    pnlNoRecords.Visible = false;
                    // Modified by @SP
                    //lblActiveTopicLastSevenDays.Text = string.Format("<h2>{0} Topic(s)<span>{1} </span></h2>", oquerydata.ToList().Count.ToString(), GetSortByStatusText());
                    //lblActiveTopicLastSevenDays.Text = string.Format("<h2>{0} Topic<span></span></h2>", oquerydata.ToList().Count.ToString());

                    var paging = TotalRecord / PageSize + ((TotalRecord % PageSize) > 0 ? 1 : 0);
                    lblPagerInfo1.Text = lblPagerInfo.Text = string.Format("{0} of {1}", CurrentPage.ToString(), paging);
                }
            }
            else
            {
                if (!IsUserAdmin)//Redirect to home. if non admin
                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.HomeTabId));
                else
                    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Something wrong, No record found!!", UI.Skins.Controls.ModuleMessage.ModuleMessageType.BlueInfo);
            }

        }
        private string GetSortByStatusText()
        {
            int iUserSelection = GetQueryStringValue("SortBy", 0);
            string rValue = "";
            switch (iUserSelection)
            {
                case 0:
                    rValue = "(Default)";
                    break;
                case 1:
                    rValue = "(Sorted A-Z)";
                    break;
                case 2:
                    rValue = "(Sorted Z-A)"; ;
                    break;
                case 3:
                    rValue = "(Topics in order of 'Most Recommendation' )";
                    break;
                case 4:
                    rValue = "(Topics in order of 'Most Replies')";
                    break;
                case 5:
                    rValue = "(Topics from Lastest to Older)";
                    break;
                case 6:
                    rValue = "(Topics, 'Active Today')";
                    break;
                case 7:
                    rValue = "(Topics, active in last 7 days)";
                    break;
                case 8:
                    rValue = "(Topics, active this month)";
                    break;

            }
            return rValue;
        }

        private void SetUI()
        {

            if (Request.QueryString["PI"] == null)
            {
                CurrentPage = 1;
            }
            else
            {
                CurrentPage = int.Parse(Request.QueryString["PI"].ToString());
            }
            UpdateTopicSeenStatus();
            BindTopics(1);
        }
        private void UpdateTopicSeenStatus(int iTopicid)
        {
            TopicsController oCtrl = new TopicsController();

            if (iTopicID != -1)
            {
                oCtrl.UpdateTopicSeenStatus(this.UserId, iTopicid);
            }

        }
        private void UpdateTopicSeenStatus()
        {
            UpdateTopicSeenStatus(GetQueryStringValue("TopicId", -1));
        }
        protected override void OnLoad(EventArgs e)
        {
            //            base.OnLoad(e);

            DotNetNuke.Framework.jQuery.RequestDnnPluginsRegistration();

            //hiddenPortalId.Value = this.PortalId.ToString();

            if (!IsPostBack)
            {
                NavImageNext.Click += NavImageNext_Click;
                SetUI();
                //if (Request.QueryString["msg"] != null)
                //{
                //    var iOperation = Request.QueryString["msg"];
                //    string message = "Your details have been saved successfully.";
                //    if (iOperation == "delete")
                //        message = "Your Record has been Deleted successfully.";


                //    string script = "window.onload = function(){ alert('";
                //    script += message;
                //    script += "')};";
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                //}
            }
        }

        #endregion

        #region "Methods

        public string GetImageName(int contentID)
        {
            LikesController oController = new LikesController();
            bool IsLike = oController.GetForPostCurrentUser(contentID, UserId);
            return Helper.RecommandImagename(IsLike);

        }


        int intAttachmentMemberId = 0;
        public string GetMemberName(int UserId)
        {
            string stringMemberName = "";
            try
            {
                if (UserId != 0)
                {

                    if (intAttachmentMemberId != UserId)
                    {

                        DotNetNuke.Entities.Users.UserInfo objUserInfo = new DotNetNuke.Entities.Users.UserInfo();

                        objUserInfo = DotNetNuke.Entities.Users.UserController.Instance.GetUserById(this.PortalId, UserId);


                        if (objUserInfo != null && !string.IsNullOrEmpty(objUserInfo.DisplayName))
                        {

                            intAttachmentMemberId = UserId;
                            string stringDisplayName = "" + objUserInfo.DisplayName + " attached media : ";
                            stringMemberName = "<span style='width:100%;padding-right:0;color:#5093c3;padding-bottom:0px;padding-top:20px;font-size:18px;font-weight:500;'>" + stringDisplayName + "</span>";
                        }
                    }
                }
            }
            catch
            {
                stringMemberName = "";
            }
            return stringMemberName;

        }




        public string GetBackgroundImage(int iAuthorid)
        {
            return "background:url('" + DotNetNuke.Modules.ActiveForums.UserProfiles.GetAvatarUrl(iAuthorid, 48, 48) + "') no-repeat 0 0;";
        }

        public string GetFilePath(string Filename)
        {
            if (string.IsNullOrEmpty(Filename))
                return "#";
            else

                return string.Format("{0}{1}/Portals/{2}/activeforums_Attach/{3}", Request.IsSecureConnection ? "https://" : "http://", PortalAlias.HTTPAlias, PortalId.ToString(), Filename);


        }


        public int SaveQuickReply(int iTopicID, int iCategoryID, string Subject, string sBody, int ReplyToOld)
        {
            SettingsInfo ms = DataCache.MainSettings(ForumModuleId);

            int retunReplyId = 0;

            if (!Request.IsAuthenticated)
            {
                return 0;
            }
            UserProfileInfo ui = new UserProfileInfo();
            if (UserId > 0)
            {
                ui = ForumUser.Profile;
            }
            else
            {
                ui.TopicCount = 0;
                ui.ReplyCount = 0;
                ui.RewardPoints = 0;
                ui.IsMod = false;
                ui.TrustLevel = -1;

            }
            bool UserIsTrusted = true;
            // UserIsTrusted = Utilities.IsTrusted((int)ForumInfo.DefaultTrustValue, ui.TrustLevel, Permissions.HasPerm(ForumInfo.Security.Trust, ForumUser.UserRoles), ForumInfo.AutoTrustLevel, ui.PostCount);
            bool isApproved = true;
            //   isApproved = Convert.ToBoolean(((ForumInfo.IsModerated == true) ? false : true));
            //if (UserIsTrusted || Permissions.HasPerm(ForumInfo.Security.ModApprove, ForumUser.UserRoles))
            //{
            //    isApproved = true;
            //}
            ReplyInfo ri = new ReplyInfo();
            ReplyController rc = new ReplyController();
            int ReplyId = -1;
            string sUsername = string.Empty;
            if (Request.IsAuthenticated)
            {
                switch (MainSettings.UserNameDisplay.ToUpperInvariant())
                {
                    case "USERNAME":
                        sUsername = UserInfo.Username.Trim(' ');
                        break;
                    case "FULLNAME":
                        sUsername = Convert.ToString(UserInfo.FirstName + " " + UserInfo.LastName).Trim(' ');
                        break;
                    case "FIRSTNAME":
                        sUsername = UserInfo.FirstName.Trim(' ');
                        break;
                    case "LASTNAME":
                        sUsername = UserInfo.LastName.Trim(' ');
                        break;
                    case "DISPLAYNAME":
                        sUsername = UserInfo.DisplayName.Trim(' ');
                        break;
                    default:
                        sUsername = UserInfo.DisplayName;
                        break;
                }

            }
            else
            {
                sUsername = Utilities.CleanString(PortalId, UserInfo.DisplayName, false, EditorTypes.TEXTBOX, true, false, ForumModuleId, ThemePath, false);
            }

            //Dim sSubject As String = Server.HtmlEncode(Request.Form("txtSubject"))
            //If (UseFilter) Then
            //    sSubject = Utilities.FilterWords(PortalId,  ForumModuleId, ThemePath, sSubject)
            //End If
            bool UseFilter = false;
            bool AllowScripts = true;
            bool AllowHTML = true;

            //if (AllowHTML)
            //{
            //    AllowHTML = IsHtmlPermitted(ForumInfo.EditorPermittedUsers, true, Permissions.HasPerm(ForumInfo.Security.ModEdit, ForumUser.UserRoles));
            //}
            sBody = Utilities.CleanString(PortalId, sBody, AllowHTML, EditorTypes.TEXTBOX, UseFilter, AllowScripts, ForumModuleId, ThemePath, true);
            DateTime createDate = DateTime.Now;
            ri.TopicId = iTopicID;// Convert.ToInt32(hdnTopicid.Value);
            ri.ReplyToId = ReplyToOld;// Convert.ToInt32(hdnTopicid.Value);
            ri.Content.AuthorId = UserId;
            ri.Content.AuthorName = sUsername;
            ri.Content.Body = sBody;
            ri.Content.DateCreated = createDate;
            ri.Content.DateUpdated = createDate;
            ri.Content.IsDeleted = false;
            ri.Content.Subject = Subject;
            ri.Content.Summary = string.Empty;
            ri.IsApproved = isApproved;
            ri.IsDeleted = false;
            ri.CategoryID = iCategoryID;
            ri.Content.IPAddress = Request.UserHostAddress;
            ReplyId = rc.Reply_Save(PortalId, ri);

            retunReplyId = ReplyId;

            //Check if is subscribed
            string cachekey = string.Format("AF-FV-{0}-{1}", PortalId, ModuleId);
            DataCache.CacheClearPrefix(cachekey);
            bool AllowSubscribe = false;

            // Subscribe or unsubscribe if needed
            if (AllowSubscribe && UserId > 0)
            {
                var subscribe = Request.Params["chkSubscribe"] == "1";
                var currentlySubscribed = Subscriptions.IsSubscribed(PortalId, ForumModuleId, ForumId, TopicId, SubscriptionTypes.Instant, UserId);

                if (subscribe != currentlySubscribed)
                {
                    // Will need to update this to support multiple subscrition types later
                    // Subscription_Update works as a toggle, so you only call it if you want to change the value.
                    var sc = new SubscriptionController();
                    sc.Subscription_Update(PortalId, ForumModuleId, ForumId, TopicId, 1, UserId, ForumUser.UserRoles);
                }
            }



            ControlUtils ctlUtils = new ControlUtils();
            TopicsController tc = new TopicsController();
            //    TopicInfo ti = tc.Topics_Get(PortalId, ForumModuleId, TopicId, ForumId, -1, false);
            //string fullURL = ctlUtils.BuildUrl(ForumTabId, ForumModuleId, ForumInfo.ForumGroup.PrefixURL, ForumInfo.PrefixURL, ForumInfo.ForumGroupId, ForumInfo.ForumID, TopicId, ti.TopicUrl, -1, -1, string.Empty, -1, ReplyId, SocialGroupId);

            //if (fullURL.Contains("~/") || Request.QueryString["asg"] != null)
            //{
            //    fullURL = Utilities.NavigateUrl(TabId, "", new string[] { ParamKeys.TopicId + "=" + TopicId, ParamKeys.ContentJumpId + "=" + ReplyId });
            //}
            //if (fullURL.EndsWith("/"))
            //{
            //    fullURL += "?" + ParamKeys.ContentJumpId + "=" + ReplyId;
            //}
            //if (isApproved)
            //{

            //    //Send Subscriptions

            //    try
            //    {
            //        //Dim sURL As String = Utilities.NavigateUrl(TabId, "", New String() {ParamKeys.ForumId & "=" & ForumId, ParamKeys.ViewType & "=" & Views.Topic, ParamKeys.TopicId & "=" & TopicId, ParamKeys.ContentJumpId & "=" & ReplyId})
            //        Subscriptions.SendSubscriptions(PortalId, ForumModuleId, TabId, ForumId, TopicId, ReplyId, UserId);
            //        try
            //        {
            //            Social amas = new Social();
            //            amas.AddReplyToJournal(PortalId, ForumModuleId, ForumId, TopicId, ReplyId, UserId, fullURL, Subject, string.Empty, sBody, ForumInfo.ActiveSocialSecurityOption, ForumInfo.Security.Read, SocialGroupId);
            //            //If Request.QueryString["asg"] Is Nothing And Not String.IsNullOrEmpty(MainSettings.ActiveSocialTopicsKey) And ForumInfo.ActiveSocialEnabled And Not ForumInfo.ActiveSocialTopicsOnly Then
            //            //    amas.AddReplyToJournal(PortalId, ForumModuleId, ForumId, TopicId, ReplyId, UserId, fullURL, Subject, String.Empty, sBody, ForumInfo.ActiveSocialSecurityOption, ForumInfo.Security.Read)
            //            //Else
            //            //    amas.AddForumItemToJournal(PortalId, ForumModuleId, UserId, "forumreply", fullURL, Subject, sBody)
            //            //End If

            //        }
            //        catch (Exception ex)
            //        {
            //            DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        DotNetNuke.Services.Exceptions.Exceptions.ProcessModuleLoadException(this, ex);
            //    }
            //    //Redirect to show post

            //    Response.Redirect(fullURL, false);
            //}
            //else if (isApproved == false)
            //{
            //    List<Entities.Users.UserInfo> mods = Utilities.GetListOfModerators(PortalId, ForumId);
            //    NotificationType notificationType = NotificationsController.Instance.GetNotificationType("AF-ForumModeration");
            //    string subject = Utilities.GetSharedResource("NotificationSubjectReply");
            //    subject = subject.Replace("[DisplayName]", UserInfo.DisplayName);
            //    subject = subject.Replace("[TopicSubject]", ti.Content.Subject);
            //    string body = Utilities.GetSharedResource("NotificationBodyReply");
            //    body = body.Replace("[Post]", sBody);
            //    string notificationKey = string.Format("{0}:{1}:{2}:{3}:{4}", TabId, ForumModuleId, ForumId, TopicId, ReplyId);

            //    Notification notification = new Notification();
            //    notification.NotificationTypeID = notificationType.NotificationTypeId;
            //    notification.Subject = subject;
            //    notification.Body = body;
            //    notification.IncludeDismissAction = false;
            //    notification.SenderUserID = UserInfo.UserID;
            //    notification.Context = notificationKey;

            //    NotificationsController.Instance.SendNotification(notification, PortalId, null, mods);

            //    var @params = new List<string> { ParamKeys.ForumId + "=" + ForumId, ParamKeys.ViewType + "=confirmaction", "afmsg=pendingmod", ParamKeys.TopicId + "=" + TopicId };
            //    if (SocialGroupId > 0)
            //    {
            //        @params.Add("GroupId=" + SocialGroupId);
            //    }
            //    Response.Redirect(Utilities.NavigateUrl(TabId, "", @params.ToArray()), false);
            //}
            //else
            //{
            //    //Dim fullURL As String = Utilities.NavigateUrl(TabId, "", New String() {ParamKeys.ForumId & "=" & ForumId, ParamKeys.ViewType & "=" & Views.Topic, ParamKeys.TopicId & "=" & TopicId, ParamKeys.ContentJumpId & "=" & ReplyId})
            //    //If MainSettings.UseShortUrls Then
            //    //    fullURL = Utilities.NavigateUrl(TabId, "", New String() {ParamKeys.TopicId & "=" & TopicId, ParamKeys.ContentJumpId & "=" & ReplyId})
            //    //End If

            //    try
            //    {
            //        Social amas = new Social();
            //        amas.AddReplyToJournal(PortalId, ForumModuleId, ForumId, TopicId, ReplyId, UserId, fullURL, Subject, string.Empty, sBody, ForumInfo.ActiveSocialSecurityOption, ForumInfo.Security.Read, SocialGroupId);
            //        //If Request.QueryString["asg"] Is Nothing And Not String.IsNullOrEmpty(MainSettings.ActiveSocialTopicsKey) And ForumInfo.ActiveSocialEnabled Then
            //        //    amas.AddReplyToJournal(PortalId, ForumModuleId, ForumId, TopicId, ReplyId, UserId, fullURL, Subject, String.Empty, sBody, ForumInfo.ActiveSocialSecurityOption, ForumInfo.Security.Read)
            //        //Else
            //        //    amas.AddForumItemToJournal(PortalId, ForumModuleId, UserId, "forumreply", fullURL, Subject, sBody)
            //        //End If

            //    }
            //    catch (Exception ex)
            //    {
            //        DotNetNuke.Services.Exceptions.Exceptions.LogException(ex);
            //    }
            //    Response.Redirect(fullURL, false);
            //}

            //End If

            return retunReplyId;
        }




        private void SaveAttachments(int contentId, FileUpload FileUploader)
        {
            var fileManager = FileManager.Instance;
            var folderManager = FolderManager.Instance;
            var adb = new Data.AttachController();

            var userFolder = folderManager.GetUserFolder(UserInfo);

            const string uploadFolderName = "activeforums_Upload";
            const string attachmentFolderName = "activeforums_Attach";
            const string fileNameTemplate = "__{0}__{1}__{2}";


            var uploadFilePath = PathUtils.Instance.GetPhysicalPath(PortalId, attachmentFolderName + "/");
            var attachmentFolder = folderManager.GetFolder(PortalId, attachmentFolderName) ?? folderManager.AddFolder(PortalId, attachmentFolderName);
            var attachmentsOld = adb.ListForContent(contentId).Where(o => !o.AllowDownload.HasValue || o.AllowDownload.Value);


            IFileInfo file = null;

            //var hidAttachments = ;
            //var attachmentsJson = hidAttachments.Value;
            //var serializer = new DataContractJsonSerializer(typeof(List<ClientAttachment>));
            //var ms = new MemoryStream(Encoding.UTF8.GetBytes((attachmentsJson)));
            //var attachmentsNew = (List<ClientAttachment>)serializer.ReadObject(ms);
            //ms.Close();

            foreach (var attachment in FileUploader.PostedFiles)
            {

                var filename = attachment.FileName;
                string ContentType = attachment.ContentType;
                var Size = attachment.ContentLength;
                var FileId = 0;

                attachment.SaveAs(uploadFilePath + "/" + filename);

                adb.Save(contentId, UserId, filename, ContentType, Size, FileId, null);


            }

        }


        #endregion

        protected void rptSharedFiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }


        public string GetContentAttachment(int ContentId)
        {
            string AttachmentList = "";

            try
            {
                Data.AttachController acon = new Data.AttachController();
                List<ContentAttach> ovjContentAttachList = new List<ContentAttach>();

                ovjContentAttachList = acon.ListContentAttachment(ContentId);

                foreach (ContentAttach item in ovjContentAttachList)
                {
                    string attachmentString = "";
                    if (item.UserId != 0)
                    {
                        attachmentString = "<div class='AfterAttachmentSestion'> <a contId='" + item.ContentId + "' class='AfterAttachment' UploadedByUserId='" + item.UserId + "' href='" + item.FileURL + "' target='_blank'>" + item.FileName + "</a> <a UploadedByUserId='" + item.UserId + "'  RemoveAttachmentURLAfterPost='" + item.FileURL + "' class='attachmentAfterRemoveButton' onClick='RemoveAttachmentURLAfterPost(this)' contId='" + item.ContentId + "'> &nbsp &nbsp &nbsp </a> </div>";
                    }

                    AttachmentList += attachmentString;
                }

            }
            catch
            {
                AttachmentList = "";
            }

            return AttachmentList;
        }

        protected void rptTopicDisuccsionInner_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                int TopicID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnInsideTopic")).Value);

                int ReplyTo = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnReplyTo")).Value);


                int intContentid = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnContentId")).Value);
                Data.AttachController acon = new Data.AttachController();
                acon.DeleteAttachmentWithPost(intContentid);

                new DAL2.Dal2Controller().ReplyDelete(ForumId, TopicID, ReplyTo, 1);



                //string URL = MakeURL(TopicID);
                string URL = MakeURLAfterReplyOperatin(TopicID, TopicID);
                Response.Redirect(URL, true);
            }


            if (e.CommandName == "EditDisscussionReply")
            {

                string txtBody = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txtQuickReplyThird")).Text;

                string txtAttachmentBody = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("TextBoxAttachmentDataThird")).Text;

                if (!string.IsNullOrEmpty(txtAttachmentBody))
                {
                    //txtBody = "" + txtAttachmentBody + "" + txtBody + "";
                }


                if (string.IsNullOrEmpty(txtBody))
                    return;
                string hdnSubject = ((HiddenField)e.Item.FindControl("hdnInnerSubject")).Value;
                int iCategoryID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnCategoryID")).Value);
                int TopicID = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnInsideTopic")).Value);
                int ReplyId = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnReplyTo")).Value);



                int HiddenFieldCurrentContentId = Convert.ToInt32(((HiddenField)e.Item.FindControl("HiddenFieldCurrentContentId")).Value);


                Data.AttachController acon = new Data.AttachController();
                string UpdateDate = DateTime.Now.ToString();
                int UpdatedByUserId = UserInfo.UserID;
                string UpdatedByUserName = UserInfo.DisplayName;
                acon.UpdateReplyPost(HiddenFieldCurrentContentId, txtBody, UpdateDate, UpdatedByUserId, UpdatedByUserName);



                UpdateTopicSeenStatus(TopicID);
                int ContentId = Convert.ToInt32(((HiddenField)e.Item.FindControl("hdnContentId")).Value);

                //string url = MakeURL(TopicID);
                string url = MakeURLAfterReplyOperatin(TopicID, ReplyId);
                Response.Redirect(url, true);
            }

        }
    }
}