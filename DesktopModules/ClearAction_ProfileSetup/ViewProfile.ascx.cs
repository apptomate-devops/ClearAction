
using DotNetNuke.Common;
using DotNetNuke.Entities.Users;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using ClearAction.Modules.ProfileClearAction_ProfileSetup.Components;
using DotNetNuke.Entities.Portals;
using ClearAction.Modules.Dashboard;
namespace ClearAction.Modules.ProfileClearAction_ProfileSetup
{
    public partial class ViewProfile : ProfileActionModuleBase
    {

        public int iUserID
        {

            get
            {
                int iCurrentuserid = UserId;
                try
                {
                    if (Request.QueryString["UserID"] != null)
                    {
                        Int32.TryParse(Request.QueryString["UserID"], out iCurrentuserid);
                    }

                }
                catch (Exception e)
                {

                }

                return iCurrentuserid;
            }
        }


        public string GetAPIKey
        {
            get
            {
                return ConfigurationManager.AppSettings["FileStackApiKey"];
            }
        }

        public string sUserName
        {

            get
            {
                string iCurrentuserid = UserInfo.Username;
                try
                {
                    if (Request.QueryString["UserName"] != null)
                    {
                        iCurrentuserid = Convert.ToString(Request.QueryString["UserName"]);
                    }

                }
                catch (Exception e)
                {

                }

                return iCurrentuserid;
            }
        }

        public int ToUserID
        {


            get; set;
        }



        private bool RedirectToHome()
        {
            if (UserInfo.IsInRole("Administrator") || UserInfo.IsSuperUser)
                return false;
            if (Request.QueryString["SocialSiteName"] != null)
                return false;

            if (Request.QueryString["utm_source"] != null)
                return false;
            if (Request.QueryString["Step"] != null)
                return false;
            var oprofilevalue = UserInfo.Profile.GetProperty("ProfileFinished");
            if (oprofilevalue != null)
                return oprofilevalue.PropertyValue == "1" ? true : false;
            return false;

        }

        private string GetProfileResponse(IQueryable<ProfileResponse> pResponse, int iQuestiond)
        {
            if (pResponse == null)
                return "";
            var single = pResponse.Where(x => x.QuestionId == iQuestiond).SingleOrDefault();
            if (single == null) return "";
            return single.ResponseText;
        }
        private string GetProfileResponse(IQueryable<ProfileResponse> pResponse, int iQuestiond, bool IsCompany)
        {
            var oCompanyID = GetProfileResponse(pResponse, iQuestiond);
            try
            {
                if (!string.IsNullOrEmpty(oCompanyID))
                    return new DbController().GetCompanyName(Convert.ToInt32(oCompanyID));

            }
            catch (Exception ex)
            {


            }
            return "";
        }

        public void SetUI()
        {

            UserInfo u = null;
            bool IsQuerystring = false;
            if (Request.QueryString["UserName"] != null)
            {
                u = UserController.GetUserByName(this.PortalId, sUserName);
                IsQuerystring = true;
            }

            if (Request.QueryString["UserID"] != null && !IsQuerystring)
            {
                u = UserController.GetUserById(this.PortalId, iUserID);
                IsQuerystring = true;
            }

            if (!IsQuerystring || u == null)
                u = UserInfo;
            ToUserID = u.UserID;
            var oData = new DbController().GetQuestionResponse(u.UserID).OrderBy(x => x.QuestionId);

            lblDisplayName.Text = UserController.GetUserById(this.PortalId, u.UserID).DisplayName;

            if (oData != null)
            {
                lblEducation.Text = GetProfileResponse(oData, ModuleSettingKey(Utiity.Education));
                lblHonorsRewards.Text = GetProfileResponse(oData, ModuleSettingKey(Utiity.HonorsReward));
                lblLocation.Text = GetProfileResponse(oData, ModuleSettingKey(Utiity.Location));
                lblUserName.Text = UserInfo.DisplayName;
                lblOverview.Text = GetProfileResponse(oData, ModuleSettingKey(Utiity.Overview));
                lblPhone.Text = GetProfileResponse(oData, ModuleSettingKey(Utiity.Phone));
                lblProfessionalAssociates.Text = GetProfileResponse(oData, ModuleSettingKey(Utiity.ProfessionalAssociates));
                lblCompanyName.Text = GetProfileResponse(oData, ModuleSettingKey(Utiity.CompanyName), true);
                lblTitle.Text = GetProfileResponse(oData, ModuleSettingKey(Utiity.Title));
                lblWorkHistory.Text = GetProfileResponse(oData, ModuleSettingKey(Utiity.WorkHistory));
                var oLinkedin = GetProfileResponse(oData, ModuleSettingKey(Utiity.Linkedin));
                if (!string.IsNullOrEmpty(oLinkedin))
                    lnkEdin.HRef = oLinkedin.Contains("linkedin") == true ? oLinkedin : "https://linkedin.com/in/" + oLinkedin;
                else
                    lnkEdin.Attributes.Add("style", "display:none");


                var oFacebook = GetProfileResponse(oData, ModuleSettingKey(Utiity.Facebook));
                if (!string.IsNullOrEmpty(oFacebook))
                    lnkFacebook.HRef = oFacebook.Contains("facebook") == true ? oFacebook : "https://facebook.com/" + oFacebook;
                else
                    lnkFacebook.Attributes.Add("style", "display:none");


                var otwitter = GetProfileResponse(oData, ModuleSettingKey(Utiity.Twitter));
                if (!string.IsNullOrEmpty(otwitter))
                    lnkTwitter.HRef = otwitter.Contains("twitter") == true ? otwitter : "https://twitter.com/" + otwitter;
                else
                    lnkTwitter.Attributes.Add("style", "display:none");


                //lnkEdin.HRef = "https://linkedin.com/" + GetProfileResponse(oData, ModuleSettingKey(Utiity.Linkedin));
                //lnkFacebook.HRef = "https://facebook.com/" + GetProfileResponse(oData, ModuleSettingKey(Utiity.Facebook));
                //lnkTwitter.HRef = "https://twitter.com/" + GetProfileResponse(oData, ModuleSettingKey(Utiity.Twitter));

                // UserInfo uCreated = UserController.GetUserById(this.PortalId, iUserID);
                if (u != null)
                {
                    lblMemberSince.Text = u.Membership.CreatedDate.ToShortDateString();
                    profilepic.Src = u.Profile.PhotoURL;
                    lblRole.Text = GetcurrentUserRoleName(this.PortalId, u.UserID);
                    mailto.Attributes.Add("href", string.Format("mailto:{0}?subject=ClearAction&Body=About your profile on clearaction", u.Email));
                    imgCall.Attributes.Add("href", string.Format("tel:{0}", GetProfileResponse(oData, ModuleSettingKey(Utiity.Phone))));
                }

                if (u.UserID == UserId)
                {

                    //imgRemove.Visible = false;
                    hrefAddConnection.Attributes.Add("onclick", string.Format("javascript:alert('Oh!! you cant add your self to your connection.');return false;"));
                    hrefRemoveConnection.Attributes.Add("onclick", string.Format("javascript:alert('Oh!! you cant remove your self to your connection.');return false;"));
                    mailto.Attributes.Add("onclick", string.Format("javascript:alert('Oh!! you cant send to your connection.');return false;"));
                    imgUserMenu.Attributes.Add("isconnection", "false");
                }
                else
                    imgUserMenu.Attributes.Add("isconnection", new ClearAction.Modules.Dashboard.Components.DashboardController().CheckUserIsFriendofCurrentuser(u.UserID));





                if (!u.IsDeleted)
                {
                    imgUserMenu.Attributes.Add("uid", Convert.ToString(u.UserID));
                    imgUserMenu.Attributes.Add("uphone", lblPhone.Text);
                }
                else
                {
                    imgUserMenu.Visible = false;
                    DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "User account has been deleted or not active on Portal. Messaging/Email will not be activate for that account", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.YellowWarning);
                }



                //Replace print tokens

                ltrlVisible.Text = ReadTemplate("ViewPdf");
                ltrlVisible.Text = ltrlVisible.Text.Replace("[PORTALALiAS]", (Request.IsSecureConnection == true ? "https://" : "http://") + PortalAlias.HTTPAlias);
                ltrlVisible.Text = ltrlVisible.Text.Replace("[profile_picture]", profilepic.Src);
                ltrlVisible.Text = ltrlVisible.Text.Replace("[title]", lblTitle.Text);
                ltrlVisible.Text = ltrlVisible.Text.Replace("[full_name]", lblDisplayName.Text);
                ltrlVisible.Text = ltrlVisible.Text.Replace("[company]", lblCompanyName.Text);
                ltrlVisible.Text = ltrlVisible.Text.Replace("[role]", lblRole.Text);
                ltrlVisible.Text = ltrlVisible.Text.Replace("[join_date]", lblMemberSince.Text);
                ltrlVisible.Text = ltrlVisible.Text.Replace("[Location]", lblLocation.Text);
                ltrlVisible.Text = ltrlVisible.Text.Replace("[phone]", lblPhone.Text);
                ltrlVisible.Text = ltrlVisible.Text.Replace("[overview_bio]", lblOverview.Text);
                ltrlVisible.Text = ltrlVisible.Text.Replace("[education]", lblEducation.Text);
                ltrlVisible.Text = ltrlVisible.Text.Replace("[work_history]", lblWorkHistory.Text);
                ltrlVisible.Text = ltrlVisible.Text.Replace("[professional_associations]", lblProfessionalAssociates.Text);
                ltrlVisible.Text = ltrlVisible.Text.Replace("[honors_awards]", lblHonorsRewards.Text);


            }




        }






        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //    DotNetNuke.Framework.jQuery.RequestDnnPluginsRegistration();
                if (UserInfo.UserID == -1)
                {
                    Globals.Redirect(Globals.AccessDeniedURL(string.Format("You are not authorize to access profile page. Please do login with your credentials. To Login <a href='{0}'>Click here</a>", Globals.NavigateURL(PortalSettings.LoginTabId))), true);
                    return;
                }
                lblDisplayName.Text = UserInfo.DisplayName;

                SetUI();


                if (Request.QueryString["DownloadProfile"] != null)
                {
                    CreatePdfDoDownload();
                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.HomeTabId), true);

                }

                if (Request.QueryString["DoPrint"] != null)
                {


                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "printPage()", true);
                    //   Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.HomeTabId), true);

                }



            }
        }

        #region "Click Event"               


        private void CreatePdfDoDownload()
        {
            string strfilename = CreatePDF();
            strfilename = Server.MapPath("") + @"\Portals\0\" + strfilename;
            DownloadFile(Response, strfilename);
            try
            {

                if (System.IO.File.Exists(strfilename))
                    System.IO.File.Delete(strfilename);
            }
            catch (Exception ex)
            {


            }
        }




        protected void imgShare_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgReport_Click(object sender, ImageClickEventArgs e)
        {

            //Need to change page url
            Response.Redirect("/Contact?ReportUserName=" + lblDisplayName.Text);

        }

        protected void imgPdf_Click(object sender, ImageClickEventArgs e)
        {
            CreatePdfDoDownload();


        }

        //protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        //{

        //}

        protected void imgDownload_Click(object sender, ImageClickEventArgs e)
        {
            string strfilename = CreatePDF();
            strfilename = Server.MapPath("") + @"\Portals\0\" + strfilename;
            DownloadFile(Response, strfilename);
        }

        #endregion
        #region "ItextSharp "
        public Font CreateFont(string Name, int Size, int iStyle, string strFont = "#5194c3")
        {

            //  var color=  System.Drawing.ColorTranslator.FromHtml(strFont);
            if (string.IsNullOrEmpty(strFont))
                strFont = "#5194c3";
            BaseColor baseColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml(strFont));


            var font = FontFactory.GetFont(Name, Size, baseColor);

            if (iStyle > 0)
                return FontFactory.GetFont(Name, Size, iStyle);
            return font;
        }
        public PdfPCell GetPDFCell(string Text, Font font8)
        {
            PdfPCell PdfPCell2 = new PdfPCell(new Phrase(new Chunk(Text, font8)));
            PdfPCell2.Border = Rectangle.NO_BORDER;
            PdfPCell2.HorizontalAlignment = Element.ALIGN_LEFT;
            return PdfPCell2;

        }
        public PdfPCell GetPDFCell(string Text, Font font8, int alignment)
        {
            PdfPCell PdfPCell2 = new PdfPCell(new Phrase(new Chunk(Text, font8)));
            //  PdfPCell2.Border = Rectangle.NO_BORDER;
            PdfPCell2.HorizontalAlignment = alignment;
            return PdfPCell2;

        }
        public PdfPCell GetPDFCell(string Text, Font font8, int alignment, bool noborder)
        {
            PdfPCell PdfPCell2 = new PdfPCell(new Phrase(new Chunk(Text, font8)));
            PdfPCell2.Border = Rectangle.RIGHT_BORDER;
            PdfPCell2.Border = Rectangle.LEFT_BORDER;
            PdfPCell2.Border = Rectangle.BOTTOM_BORDER;
            PdfPCell2.HorizontalAlignment = alignment;
            return PdfPCell2;

        }

        private string CreatePDF()
        {

            //    string strTemplate = ReadTemplate("ViewPdf");
            string strFile = Guid.NewGuid().ToString().Replace("-", "") + ".pdf";
            string strFileName = Server.MapPath("") + @"\Portals\0\" + strFile;
            FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write, FileShare.None);

            Document pdfdoc = new Document(PageSize.A4, 10, 10, 20, 20);

            PdfWriter writer = PdfWriter.GetInstance(pdfdoc, fs);
            // Phrase phrase = null;
            PdfPCell cell = null;
            PdfPTable table = null;
            Font font14 = CreateFont("Montserrat", 14, 0);
            //fonth.Color =  ;
            Font font12 = CreateFont("Montserrat", 12, 0);
            Font font10 = CreateFont("Montserrat", 10, 0);
            Font font10black = CreateFont("Montserrat", 10, 0, "#000000");
            pdfdoc.Open();

            table = new PdfPTable(1);
            //  table.SetWidths(new float[] { 1.5f, 5f });
            table.WidthPercentage = 95f;

            table.SpacingBefore = 20f;
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            table.HorizontalAlignment = Element.ALIGN_RIGHT;

            //Portal logo files
            string strLogo = "/images/email_profile_logo_top.gif";// + PortalSettings.LogoFile;

            var jpgimage = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + strLogo);
            //Resize image depend upon your need

            jpgimage.ScaleToFit(850f, 150f);
            jpgimage.SpacingAfter = 1f;

            jpgimage.Alignment = Element.ALIGN_CENTER;

            table.AddCell(jpgimage);


            var table1 = new PdfPTable(2);
            table1.WidthPercentage = 95f;
            table1.SpacingBefore = 20f;
            table1.DefaultCell.Border = Rectangle.NO_BORDER;
            table1.SetWidths(new float[] { 1.5f, 5f });
            table1.HorizontalAlignment = Element.ALIGN_RIGHT;


            //Add Profile header

            cell = PhraseCell("Member Profile", font14, PdfPCell.NO_BORDER);
            table1.AddCell(cell);

            cell = PhraseCell("", font10, PdfPCell.NO_BORDER);
            table1.AddCell(cell);

            //Now Add Table with profile pic and other data
            string strProfilepic = profilepic.Src;
            if (strProfilepic.Contains("?"))
                strProfilepic = strProfilepic.Split(Convert.ToChar("?"))[0];


            if (!string.IsNullOrEmpty(strProfilepic) && System.IO.File.Exists(Server.MapPath(".") + strProfilepic))
            {
                var image = iTextSharp.text.Image.GetInstance(Server.MapPath(".") + strProfilepic);
                //Resize image depend upon your need

                image.ScaleToFit(100f, 100f);
                image.SpacingAfter = 1f;

                image.Alignment = Element.ALIGN_LEFT;



                table1.AddCell(image);
            }

            var innerTable = new PdfPTable(1);
            innerTable.WidthPercentage = 95f;
            innerTable.SpacingBefore = 20f;
            innerTable.DefaultCell.Border = Rectangle.NO_BORDER;
            innerTable.DefaultCell.BorderWidth = 0;
            innerTable.DefaultCell.BorderColor = BaseColor.WHITE;

            innerTable.AddCell(PhraseCell(lblDisplayName.Text, CreateFont("Montserrat", 14, 0, "#000000"), PdfPCell.NO_BORDER));


            if (!string.IsNullOrEmpty(lblTitle.Text))
                innerTable.AddCell(PhraseCell(lblTitle.Text, CreateFont("Montserrat", 10, 0, "#000000"), PdfPCell.NO_BORDER));
            if (!string.IsNullOrEmpty(lblCompanyName.Text))
                innerTable.AddCell(PhraseCell(lblCompanyName.Text, CreateFont("Montserrat", 10, 0, "#000000"), PdfPCell.NO_BORDER));
            if (!string.IsNullOrEmpty(lblRole.Text))
                innerTable.AddCell(PhraseCell("Role:" + lblRole.Text, CreateFont("Montserrat", 10, 0, "#000000"), PdfPCell.NO_BORDER));
            if (!string.IsNullOrEmpty(lblMemberSince.Text))
                innerTable.AddCell(PhraseCell("Member Since:" + lblMemberSince.Text, CreateFont("Montserrat", 10, 0, "#000000"), PdfPCell.NO_BORDER));
            if (!string.IsNullOrEmpty(lblLocation.Text))
                innerTable.AddCell(PhraseCell(lblLocation.Text, CreateFont("Montserrat", 10, 0, "#000000"), PdfPCell.NO_BORDER));
            if (!string.IsNullOrEmpty(lblPhone.Text))
                innerTable.AddCell(PhraseCell(lblPhone.Text, CreateFont("Montserrat", 10, 0, "#000000"), PdfPCell.NO_BORDER));

            //   cell = PhraseCell("test", font10black, PdfPCell.NO_BORDER);

            var outerCell = new PdfPCell();
            outerCell.Border = 0;
            outerCell.BorderWidth = 0;
            outerCell.AddElement(innerTable);
            table1.AddCell(outerCell);


            //  table1.AddCell(PhraseCell(new Phrase("Member Profile on ClearAction", fonth), PdfPCell.ALIGN_LEFT));


            //blank cell
            var line = new iTextSharp.text.pdf.draw.LineSeparator(1f, 95f, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#5194c3")), Element.ALIGN_LEFT, 10);
            cell = PhraseCell("", font14, PdfPCell.NO_BORDER);
            table1.AddCell(cell);

            cell = PhraseCell("", font10, PdfPCell.NO_BORDER);
            table1.AddCell(cell);

            //Add New Tables

            var bio = new PdfPTable(1);
            bio.WidthPercentage = 95f;
            bio.SpacingBefore = 20f;
            // bio.SetWidths(new float[] { 5f, 1.5f });
            bio.DefaultCell.Border = Rectangle.NO_BORDER;
            bio.DefaultCell.BorderWidth = 0;
            bio.DefaultCell.BorderColor = BaseColor.WHITE;

            cell = PhraseCell("", font14, PdfPCell.NO_BORDER);
            bio.AddCell(cell);

            cell = PhraseCell("", font10, PdfPCell.NO_BORDER);
            bio.AddCell(cell);
            cell = PhraseCell("", CreateFont("Montserrat", 10, 0, "#00000"), PdfPCell.ALIGN_LEFT);
            cell.AddElement(line);
            bio.AddCell(cell);
            //OverView
            bio.AddCell(PhraseCell("Overview | Bio ", CreateFont("Montserrat", 10, 1, "#000000"), PdfPCell.NO_BORDER));
            bio.AddCell(PhraseCell(lblOverview.Text, CreateFont("Montserrat", 10, 0, "#000000"), PdfPCell.NO_BORDER));
            cell = PhraseCell("\n", font10, PdfPCell.NO_BORDER);
            bio.AddCell(cell);
            //Education
            bio.AddCell(PhraseCell("Education", CreateFont("Montserrat", 10, 1, "#000000"), PdfPCell.NO_BORDER));
            bio.AddCell(PhraseCell(lblEducation.Text, CreateFont("Montserrat", 10, 0, "#000000"), PdfPCell.NO_BORDER));
            cell = PhraseCell("\n", font10, PdfPCell.NO_BORDER);
            bio.AddCell(cell);
            //Work History
            bio.AddCell(PhraseCell("Work History", CreateFont("Montserrat", 10, 1, "#000000"), PdfPCell.NO_BORDER));
            bio.AddCell(PhraseCell(lblWorkHistory.Text, CreateFont("Montserrat", 10, 0, "#000000"), PdfPCell.NO_BORDER));
            cell = PhraseCell("\n", font10, PdfPCell.NO_BORDER);
            bio.AddCell(cell);

            //Professional Associates
            bio.AddCell(PhraseCell("Professional Associations", CreateFont("Montserrat", 10, 1, "#000000"), PdfPCell.NO_BORDER));
            bio.AddCell(PhraseCell(lblProfessionalAssociates.Text, CreateFont("Montserrat", 10, 0, "#000000"), PdfPCell.NO_BORDER));
            cell = PhraseCell("\n", font10, PdfPCell.NO_BORDER);
            bio.AddCell(cell);

            //Honors & Awards
            bio.AddCell(PhraseCell("Honors & Awards", CreateFont("Montserrat", 10, 1, "#000000"), PdfPCell.NO_BORDER));
            bio.AddCell(PhraseCell(lblHonorsRewards.Text, CreateFont("Montserrat", 10, 0, "#000000"), PdfPCell.NO_BORDER));
            cell = PhraseCell("\n", font10, PdfPCell.NO_BORDER);
            bio.AddCell(cell);

            pdfdoc.Add(table);
            pdfdoc.Add(table1);
            pdfdoc.Add(bio);



            // pdfdoc.Add(new Chunk(line));
            pdfdoc.Close();


            return strFile;

        }
        private Chunk GetChunk()
        {
            var oChunk = new Chunk();
            return oChunk;

        }
        private static PdfPCell PhraseCell(string strText, Font font, int align)
        {
            var phrase = new Phrase(strText, font);
            PdfPCell cell = new PdfPCell(phrase);
            //cell.BorderColor = Color.WHITE;
            //  cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 2f;
            cell.BorderWidth = 0;
            cell.PaddingTop = 2f;
            cell.BorderWidth = 0;
            cell.Border = 0;

            return cell;
        }
        private void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, BaseColor color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }

        #endregion

        public void DownloadFile(HttpResponse response, string fileRelativePath)
        {
            try
            {
                string contentType = "";
                //Get the physical path to the file.
                //string FilePath = HttpContext.Current.Server.MapPath(fileRelativePath);

                string fileExt = Path.GetExtension(fileRelativePath).Split('.')[1].ToLower();

                if (fileExt == "pdf")
                {
                    //Set the appropriate ContentType.
                    contentType = "Application/pdf";
                }

                //Set the appropriate ContentType.
                response.ContentType = contentType;
                response.AppendHeader("content-disposition", "attachment; filename=" + (new System.IO.FileInfo(fileRelativePath)).Name);

                //Write the file directly to the HTTP content output stream.
                response.WriteFile(fileRelativePath);
                response.End();
            }
            catch
            {
                //To Do
            }
        }





    }
}