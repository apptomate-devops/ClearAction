using System;
using System.Web.UI.WebControls;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
//using HiQPdf;
//using iTextSharp.text;
//using iTextSharp.text.html.simpleparser;
//using iTextSharp.text.pdf;


using ClearAction.Modules.Dashboard.Components;
using System.Web.Configuration;
using System.Web;
using System.IO;
using System.Linq;
using ClearAction.Modules.WebCast.Components.Entity;
using System.Collections.Generic;
using System.Web.UI;

namespace ClearAction.Modules.Dashboard
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from ClearAction_DashboardModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : ClearAction_DashboardModuleBase//, IActionable
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                int iValue = Settings.Contains("SettingFor") == false ? 0 : Convert.ToInt32(Settings["SettingFor"]);

                if (iValue == 1 || iValue == 0)
                {
                    RenderUI(IsBlog);
                    return;
                }
                try
                {
                    int iSettingValue = Convert.ToInt32(Settings["SettingFor"]);
                    RenderWebCast(iSettingValue);
                }
                catch (Exception ex)
                {
                }
                // Commented Zoom Integration part.
                //if (iValue == 5)
                //{
                //    ZoomMeetingAPISchedular.ZoomMeetingAPISchedular zoom = new ZoomMeetingAPISchedular.ZoomMeetingAPISchedular();
                //    //zoom.ZoomAPICall();
                //    List<WebCast.ZoomMeeting> zList = new List<WebCast.ZoomMeeting>();
                //    zList = zoom.getZoomMeetingList(UserInfo.Email);                    
                //    if (zList != null && zList.Count > 0)
                //    {
                //        System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
                //        string ItemTemplate = ReadTemplate("ZoomMeetingItem");
                //        string shortdesc = "";
                //        string otempdata = "";
                //        foreach (var item in zList)
                //        {
                //            otempdata = ItemTemplate;
                //            shortdesc = item.topic + " start at " + item.start_time + " status " + item.status + " TimeZone " + item.timezone;
                //            otempdata = otempdata.Replace("{link}", item.join_url);
                //            otempdata = otempdata.Replace("{title}", item.topic);
                //            otempdata = otempdata.Replace("{shortdescription}", shortdesc);
                //            strBuilder.Append(otempdata);
                //        }

                //        pHolder.Text = ReadTemplate("ZoomMeeting").Replace("[Items]", strBuilder.ToString());
                //    }
                //}
             
            }
            _search = txtSearch.Text;
            if (_search != "")
            {
                try
                {
                    int iValue = Settings.Contains("SettingFor") == false ? 0 : Convert.ToInt32(Settings["SettingFor"]);

                    if (iValue == 1 || iValue == 0)
                    {
                        RenderUISearch(IsBlog);
                        return;
                    }

                    int iSettingValue = Convert.ToInt32(Settings["SettingFor"]);
                    RenderWebCastSearch(iSettingValue);
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void RenderWebCast(int settingID)
        {
            int iComponentID = 4; // Assuming Community Call to be default
            if (settingID == 3) iComponentID = 5; // change to WebCast Conversations as per Admin Settings

            // Load the Template according to the Admin Setting
            string ItemTemplate = ReadTemplate(iComponentID == 5 ? "WebCastItem" : "CommunityItem");
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();

            // Load the User's digital Data for MyExchange
            //  var feeddata = iComponentID == 4 ? new DashboardController().GetDigitalEvent(iComponentID, TopN_Blog, this.UserId) : new DashboardController().GetDigitalEvent(iComponentID, TopN_Forum, this.UserId);
            var feeddata = new DashboardController().GetDigitalEvent(iComponentID, TopN_Blog, this.UserId);

            // Iterate through the data and replace the tokens with actual data
            if (feeddata.Count > 0)
            {
                foreach (HomeFeed oFeed in feeddata)
                {

                    string otempdata = ItemTemplate;

                    try
                    {

                        ClearAction.Modules.WebCast.Components.DBController objDBController = new ClearAction.Modules.WebCast.Components.DBController();

                        IQueryable<Webinar_User_RegistrationDetail> objWebinar_User_RegistrationDetail = objDBController.GetWebinarUserRegistrationForEvent(this.UserInfo.UserID, oFeed.WCCId, iComponentID);


                        if (oFeed.ISVISIBLEREGiSTER)
                        {
                            otempdata = otempdata.Replace("{ISVISIBLEREGiSTER}", "display:block");
                            otempdata = otempdata.Replace("{ISVISIBLEVIEW}", "display:none");
                            //      otempdata = !string.IsNullOrEmpty(oFeed.WebApiKey) ? otempdata.Replace("{REGISTERLINK}", string.Format("https://register.gotowebinar.com/register/{0}?firstName={1}&lastName={2}&email={3}", oFeed.WebApiKey, UserInfo.FirstName, UserInfo.LastName, UserInfo.Email)) : otempdata.Replace("{REGISTERLINK}", string.Format("{0}?firstName={1}&lastName={2}&email={3}", oFeed.RegistrationLink, UserInfo.FirstName, UserInfo.LastName, UserInfo.Email));

                            if (!string.IsNullOrEmpty(oFeed.WebApiKey))
                            {
                                //otempdata = otempdata.Replace("{REGISTERLINK}", string.Format("https://register.gotowebinar.com/register/{0}?firstName={1}&lastName={2}&email={3}", oFeed.WebApiKey, UserInfo.FirstName, UserInfo.LastName, UserInfo.Email));

                                if (objWebinar_User_RegistrationDetail.Count() <= 0)
                                {
                                    otempdata = otempdata.Replace("{WEBINARKEY}", oFeed.WebApiKey);
                                    otempdata = otempdata.Replace("{REGISTERCOMPONENTID}", iComponentID.ToString());
                                    otempdata = otempdata.Replace("{REGISTERLOGINUSERID}", this.UserInfo.UserID.ToString());
                                    otempdata = otempdata.Replace("{WCCID}", oFeed.WCCId.ToString());

                                    otempdata = otempdata.Replace("{LINKCLASSNAME}", "gotoRegisterLink");

                                    otempdata = otempdata.Replace("{ONCLICKEVENTMETHOD}", "onclick='GotoWebinarRegister(this)'");

                                    otempdata = otempdata.Replace("{LINKTEXTCONTENT}", "Register for live event");

                                }
                                else
                                {

                                    otempdata = otempdata.Replace("{WEBINARKEY}", oFeed.WebApiKey);
                                    otempdata = otempdata.Replace("{REGISTERCOMPONENTID}", iComponentID.ToString());
                                    otempdata = otempdata.Replace("{REGISTERLOGINUSERID}", this.UserInfo.UserID.ToString());
                                    otempdata = otempdata.Replace("{WCCID}", oFeed.WCCId.ToString());

                                    otempdata = otempdata.Replace("{LINKCLASSNAME}", "gotoAfterRegisterLink");

                                    otempdata = otempdata.Replace("{ONCLICKEVENTMETHOD}", "");

                                    otempdata = otempdata.Replace("{LINKTEXTCONTENT}", "Already Registered   for join <a target='_blank' class='gotoJoinLInk' href='" + objWebinar_User_RegistrationDetail.FirstOrDefault().UserWebinarJoinURL + "'>Click here<a>");

                                }


                            }
                            else if (!string.IsNullOrEmpty(oFeed.RegistrationLink))
                                otempdata = otempdata.Replace("{REGISTERLINK}", "");
                            else
                                otempdata = otempdata.Replace("{REGISTERLINK}", "");

                        }
                        else
                        {

                            otempdata = otempdata.Replace("{ISVISIBLEREGiSTER}", "display:none");
                            otempdata = otempdata.Replace("{ISVISIBLEVIEW}", "display:block");
                            otempdata = otempdata.Replace("{EXPIREDVIEWLINK}", oFeed.ExpiredviewLink);

                        }
                        strBuilder.Append(Utilies.ReplaceFeedToken(otempdata, oFeed));
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            else
            {// No records found
                strBuilder.Append("<label style='color:#6992c0;font-weight:normal;width:100%;text-align:center;'>No " + ((iComponentID == 5) ? "Webcast Conversation(s)" : "Community Call(s)") + " found</label>");
            }
            pHolder.Text = ReadTemplate(iComponentID == 5 ? "WebCast" : "Community").Replace("[Items]", strBuilder.ToString());
        }
        private void RenderWebCastSearch(int settingID)
        {
            int iComponentID = 4; // Assuming Community Call to be default
            if (settingID == 3) iComponentID = 5; // change to WebCast Conversations as per Admin Settings

            // Load the Template according to the Admin Setting
            string ItemTemplate = ReadTemplate(iComponentID == 5 ? "WebCastItem" : "CommunityItem");
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();

            // Load the User's digital Data for MyExchange
            //  var feeddata = iComponentID == 4 ? new DashboardController().GetDigitalEvent(iComponentID, TopN_Blog, this.UserId) : new DashboardController().GetDigitalEvent(iComponentID, TopN_Forum, this.UserId);
            var feeddata = new DashboardController().GetDigitalEventSearch(iComponentID, TopN_BlogSearch, this.UserId,_search);

            // Iterate through the data and replace the tokens with actual data
            if (feeddata.Count > 0)
            {
                foreach (HomeFeed oFeed in feeddata)
                {

                    string otempdata = ItemTemplate;

                    try
                    {

                        ClearAction.Modules.WebCast.Components.DBController objDBController = new ClearAction.Modules.WebCast.Components.DBController();

                        IQueryable<Webinar_User_RegistrationDetail> objWebinar_User_RegistrationDetail = objDBController.GetWebinarUserRegistrationForEvent(this.UserInfo.UserID, oFeed.WCCId, iComponentID);


                        if (oFeed.ISVISIBLEREGiSTER)
                        {
                            otempdata = otempdata.Replace("{ISVISIBLEREGiSTER}", "display:block");
                            otempdata = otempdata.Replace("{ISVISIBLEVIEW}", "display:none");
                            //      otempdata = !string.IsNullOrEmpty(oFeed.WebApiKey) ? otempdata.Replace("{REGISTERLINK}", string.Format("https://register.gotowebinar.com/register/{0}?firstName={1}&lastName={2}&email={3}", oFeed.WebApiKey, UserInfo.FirstName, UserInfo.LastName, UserInfo.Email)) : otempdata.Replace("{REGISTERLINK}", string.Format("{0}?firstName={1}&lastName={2}&email={3}", oFeed.RegistrationLink, UserInfo.FirstName, UserInfo.LastName, UserInfo.Email));

                            if (!string.IsNullOrEmpty(oFeed.WebApiKey))
                            {
                                //otempdata = otempdata.Replace("{REGISTERLINK}", string.Format("https://register.gotowebinar.com/register/{0}?firstName={1}&lastName={2}&email={3}", oFeed.WebApiKey, UserInfo.FirstName, UserInfo.LastName, UserInfo.Email));

                                if (objWebinar_User_RegistrationDetail.Count() <= 0)
                                {
                                    otempdata = otempdata.Replace("{WEBINARKEY}", oFeed.WebApiKey);
                                    otempdata = otempdata.Replace("{REGISTERCOMPONENTID}", iComponentID.ToString());
                                    otempdata = otempdata.Replace("{REGISTERLOGINUSERID}", this.UserInfo.UserID.ToString());
                                    otempdata = otempdata.Replace("{WCCID}", oFeed.WCCId.ToString());

                                    otempdata = otempdata.Replace("{LINKCLASSNAME}", "gotoRegisterLink");

                                    otempdata = otempdata.Replace("{ONCLICKEVENTMETHOD}", "onclick='GotoWebinarRegister(this)'");

                                    otempdata = otempdata.Replace("{LINKTEXTCONTENT}", "Register for live event");

                                }
                                else
                                {

                                    otempdata = otempdata.Replace("{WEBINARKEY}", oFeed.WebApiKey);
                                    otempdata = otempdata.Replace("{REGISTERCOMPONENTID}", iComponentID.ToString());
                                    otempdata = otempdata.Replace("{REGISTERLOGINUSERID}", this.UserInfo.UserID.ToString());
                                    otempdata = otempdata.Replace("{WCCID}", oFeed.WCCId.ToString());

                                    otempdata = otempdata.Replace("{LINKCLASSNAME}", "gotoAfterRegisterLink");

                                    otempdata = otempdata.Replace("{ONCLICKEVENTMETHOD}", "");

                                    otempdata = otempdata.Replace("{LINKTEXTCONTENT}", "Already Registered   for join <a target='_blank' class='gotoJoinLInk' href='" + objWebinar_User_RegistrationDetail.FirstOrDefault().UserWebinarJoinURL + "'>Click here<a>");

                                }


                            }
                            else if (!string.IsNullOrEmpty(oFeed.RegistrationLink))
                                otempdata = otempdata.Replace("{REGISTERLINK}", "");
                            else
                                otempdata = otempdata.Replace("{REGISTERLINK}", "");

                        }
                        else
                        {

                            otempdata = otempdata.Replace("{ISVISIBLEREGiSTER}", "display:none");
                            otempdata = otempdata.Replace("{ISVISIBLEVIEW}", "display:block");
                            otempdata = otempdata.Replace("{EXPIREDVIEWLINK}", oFeed.ExpiredviewLink);

                        }
                        strBuilder.Append(Utilies.ReplaceFeedToken(otempdata, oFeed));
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            else
            {// No records found
                strBuilder.Append("<label style='color:#6992c0;font-weight:normal;width:100%;text-align:center;'>No " + ((iComponentID == 5) ? "Webcast Conversation(s)" : "Community Call(s)") + " found</label>");
            }
            pHolder.Text = ReadTemplate(iComponentID == 5 ? "WebCast" : "Community").Replace("[Items]", strBuilder.ToString());
        }
        private bool IsBlog
        {
            get
            {

                try
                {
                    if (Settings.Contains("SettingFor"))
                        return Convert.ToString(Settings["SettingFor"]) == "0" ? true : false;
                    return true;
                }
                catch (Exception ex)
                {
                }
                return true;
            }
        }

        private string FeedUrl
        {
            get
            {
                if (Settings.Contains("FeedUrl"))
                    return Convert.ToString(Settings["FeedUrl"]);
                return "";
            }
        }

        private int TopN_Blog
        {
            get
            {
                int TopN = 5;
                try
                {
                    string strTopN = WebConfigurationManager.AppSettings["MyExchange_TopN_Blog"];
                    TopN = int.Parse(strTopN);
                }
                catch (Exception ex) { }
                return TopN;
            }
        }

        private int TopN_Forum
        {
            get
            {
                int TopN = 5;
                try
                {
                    string strTopN = WebConfigurationManager.AppSettings["MyExchange_TopN_Forum"];
                    TopN = int.Parse(strTopN);
                }
                catch (Exception ex) { }
                return TopN;
            }
        }

        private int TopN_BlogSearch
        {
            get
            {
                int TopN = 1000;
              
                return TopN;
            }
        }

        private int TopN_ForumSearch
        {
            get
            {
                int TopN = 1000;
               
                return TopN;
            }
        }

        private void RenderUI(bool bBlog)
        {
            //if (string.IsNullOrEmpty(FeedUrl))
            //{
            //    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Module Configuration Require!!", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);
            //    return;
            //}
            var _TopN = (bBlog == true) ? TopN_Blog : TopN_Forum;

            var feeddata = bBlog == true ? new DashboardController().GetBlogFeed(FeedUrl, _TopN, this.UserId) : new DashboardController().GetForumFeed(FeedUrl, _TopN, this.UserId);

            string ItemTemplate = ReadTemplate(bBlog ? "BlogItem" : "ForumItem");

            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();

            if (feeddata.Count > 0)
            {
                foreach (HomeFeed oFeed in feeddata)
                {
                    strBuilder.Append(Utilies.ReplaceFeedToken(ItemTemplate, oFeed));
                }
            }
            else
            {
                strBuilder.Append("<label style='color:#6992c0;font-weight:normal;width:100%;text-align:center;'>No " + ((bBlog) ? "Insight(s)" : "Forum(s)") + " found</label>");
            }

            pHolder.Text = ReadTemplate(bBlog ? "BlogHeader" : "Forum").Replace("[Items]", strBuilder.ToString());
        }
        private void RenderUISearch(bool bBlog)
        {
            //if (string.IsNullOrEmpty(FeedUrl))
            //{
            //    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Module Configuration Require!!", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);
            //    return;
            //}
            var _TopN = (bBlog == true) ? TopN_BlogSearch : TopN_ForumSearch;

            var feeddata = bBlog == true ? new DashboardController().GetBlogFeedSearch(FeedUrl, _TopN, this.UserId,_search) : new DashboardController().GetForumFeedSearch(FeedUrl, _TopN, this.UserId,_search);

            string ItemTemplate = ReadTemplate(bBlog ? "BlogItem" : "ForumItem");

            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();

            if (feeddata.Count > 0)
            {
                foreach (HomeFeed oFeed in feeddata)
                {
                    strBuilder.Append(Utilies.ReplaceFeedToken(ItemTemplate, oFeed));
                }
            }
            else
            {
                strBuilder.Append("<label style='color:#6992c0;font-weight:normal;width:100%;text-align:center;'>No " + ((bBlog) ? "Insight(s)" : "Forum(s)") + " found</label>");
            }

            pHolder.Text = ReadTemplate(bBlog ? "BlogHeader" : "Forum").Replace("[Items]", strBuilder.ToString());
        }
        string _search = "";
        protected void btn_search_Click(object sender, EventArgs e)
        {
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "AKey", "getlastcall();", true);
        }
            protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            #region SelectPDF implementation
            //{
            //// instantiate a html to pdf converter object
            //HtmlToPdf converter = new HtmlToPdf();

            //// set startup mode
            //converter.Options.StartupMode = (HtmlToPdfStartupMode)Enum.Parse(typeof(HtmlToPdfStartupMode), "Automatic", true);
            //// set timeout
            //int timeout = 10;
            //converter.Options.MaxPageLoadTime = timeout;

            //// create a new pdf document to convert
            //PdfDocument doc = converter.ConvertHtmlString(hdHTMLString.Value.ToString());
            //// save pdf document test
            //doc.Save(Response, false, "SolveSpace_Summary.pdf");
            //// close pdf document
            //doc.Close();
            #endregion

            #region HiQPDF Implementation
            /* 
            // create the HTML to PDF converter
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

            //  set a demo serial number
            htmlToPdfConverter.SerialNumber = "YCgJMTAE-BiwJAhIB-EhlWTlBA-UEBRQFBA-U1FOUVJO-WVlZWQ==";

            //   convert HTML to PDF
            byte[] pdfBuffer = null;

            string hdHTMLString1 = "<div>this is a sample test from dashboard</div>";

            //  convert HTML code to a PDF memory buffer
            pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(hdHTMLString1, string.Empty);

            //    inform the browser about the binary data format
            HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

            //  let the browser know how to open the PDF document, attachment or inline, and the file name
            HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("{0}; filename=HtmlToPdf.pdf; size={1}",
                 "attachment", pdfBuffer.Length.ToString()));

            //  write the PDF buffer to HTTP response
            HttpContext.Current.Response.BinaryWrite(pdfBuffer);

            //   call End() method of HTTP response to stop ASP.NET page processing
            HttpContext.Current.Response.End();
            // }
            */
            #endregion

            #region iTextSharp
            //string hdHTMLString1 = "<div>this is a sample test from dashboard</div>";
            //StringReader sr = new StringReader(hdHTMLString1);

            //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            //    pdfDoc.Open();

            //    htmlparser.Parse(sr);
            //    pdfDoc.Close();

            //    byte[] bytes = memoryStream.ToArray();
            //    memoryStream.Close();
            //    // Clears all content output from the buffer stream                 
            //    Response.Clear();
            //    // Gets or sets the HTTP MIME type of the output stream.
            //    Response.ContentType = "application/pdf";
            //    // Adds an HTTP header to the output stream
            //    Response.AddHeader("Content-Disposition", "attachment; filename=Summary.pdf");

            //    //Gets or sets a value indicating whether to buffer output and send it after
            //    // the complete response is finished processing.
            //    Response.Buffer = true;
            //    // Sets the Cache-Control header to one of the values of System.Web.HttpCacheability.
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    // Writes a string of binary characters to the HTTP output stream. it write the generated bytes .
            //    Response.BinaryWrite(bytes);
            //    // Sends all currently buffered output to the client, stops execution of the
            //    // page, and raises the System.Web.HttpApplication.EndRequest event.

            //    Response.End();
            //    // Closes the socket connection to a client. it is a necessary step as you must close the response after doing work.its best approach.
            //    Response.Close();
            //}
            #endregion


        }
    }
}