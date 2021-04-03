using ClearAction.Modules.ComponentManager.Components;
using ClearAction.Modules.ProfileClearAction_ProfileSetup.Components;
using ClearAction.Modules.SolveSpaces.Components;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Entities.Profile;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security;
using DotNetNuke.Services.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.UI.WebControls;

namespace ClearAction.Modules.ProfileClearAction_ProfileSetup
{
    public partial class View : ProfileActionModuleBase, IActionable
    {
        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {

                         {
                            GetNextActionID(), "Admin Section", "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }



                    };


                return actions;
            }
        }

        #region "Private methods"
        private string GetQuestion(IQueryable<Question> oQuestions, string iQuestionid)
        {
            try
            {

                if (iQuestionid == "-1")
                    return "";
                var o = (from q in oQuestions
                         where q.QuestionID == Convert.ToInt32(iQuestionid)
                         select q).SingleOrDefault();

                if (o == null) return "";
                return o.QuestionText;
            }
            catch (Exception ex)
            {


            }
            return "";
        }
        private ProfileResponse GetResponse(int iQuestionID)
        {
            return new DbController().GetQuestionResponse(UserId, iQuestionID);
        }
        //private string StepSelect(string RadioStep1)
        //{

        //    Control divControl = ReadControlGeneric(RadioStep1);

        //    foreach (Control c in divControl.Controls)
        //    {
        //        if (c.GetType() == typeof(HtmlInputRadioButton))
        //        {
        //            if (((HtmlInputRadioButton)c).Checked)
        //                return c.ID;
        //        }

        //    }
        //    return "-1";

        //}

        //private void SetStepSelect(string RadioStep1, string SelectedID)
        //{
        //    if (SelectedID == "-1")
        //        return;
        //    Control divControl = ReadControlGeneric(RadioStep1);

        //    foreach (Control c in divControl.Controls)
        //    {
        //        if (c.GetType() == typeof(HtmlInputRadioButton))
        //        {
        //            if (((HtmlInputRadioButton)c).ID == SelectedID)
        //            { ((HtmlInputRadioButton)c).Checked = true; return; }
        //        }

        //    }

        //}
        public bool IsRoleUserIn(int iRoleid)
        {
            return new DbController().UserInRole(PortalId, iRoleid, UserInfo);
        }
        public bool GetQuestionResponseRadio(int iQuestionID, int QuestionoptionID)
        {
            try
            {


                var oData = GetResponse(iQuestionID);
                if (oData != null)
                {
                    var odatalist = oData.LstProfileResponseOption;
                    if (odatalist == null) return false;
                    return odatalist.Where(x => x.QuestionOptionID == QuestionoptionID).Where(x => x.IsActive == true).SingleOrDefault() != null ? true : false;
                }

            }
            catch (Exception rc)
            {


            }

            return false;

        }


        public string GetQuestionResponseText(int iQuestionID)
        {

            var odata = GetResponse(iQuestionID);
            if (odata != null)
                return odata.ResponseText;
            return "";
        }
        private List<ProfileResponseOption> ReadRepeatorControl(Repeater rpt, int QuestionID)
        {
            List<ProfileResponseOption> lstProfileResponseOption = new List<ProfileResponseOption>();

            if (rpt == null) return null;
            foreach (RepeaterItem i in rpt.Items)
            {
                if (i.ItemType == ListItemType.Item || i.ItemType == ListItemType.AlternatingItem)
                {
                    var htmlControl = i.FindControl("radioControl") as System.Web.UI.HtmlControls.HtmlInputCheckBox;
                    if (htmlControl != null)
                    {
                        var oProfileResponseOption = new ProfileResponseOption()
                        {
                            ProfileResponseOptionId = -1,
                            QuestionID = QuestionID,
                            QuestionOptionID = Convert.ToInt32(htmlControl.Value),
                            IsActive = htmlControl.Checked
                        };

                        lstProfileResponseOption.Add(oProfileResponseOption);
                    }
                }
            }
            return lstProfileResponseOption;
        }
        private List<ProfileResponseOption> ReadRepeatorControl(Repeater rpt, bool IsAspControl, int QuestionID)
        {
            List<ProfileResponseOption> lstProfileResponseOption = new List<ProfileResponseOption>();

            if (rpt == null) return null;
            foreach (RepeaterItem i in rpt.Items)
            {
                if (i.ItemType == ListItemType.Item || i.ItemType == ListItemType.AlternatingItem)
                {
                    var htmlControl = i.FindControl("radioControl") as RadioButton;
                    var htmvalue = i.FindControl("QuestionOptionID") as HiddenField;
                    if (htmlControl != null)
                    {
                        var oProfileResponseOption = new ProfileResponseOption()
                        {
                            ProfileResponseOptionId = -1,
                            QuestionID = QuestionID,
                            QuestionOptionID = Convert.ToInt32(htmvalue.Value),
                            IsActive = htmlControl.Checked
                        };

                        lstProfileResponseOption.Add(oProfileResponseOption);
                    }
                }
            }
            return lstProfileResponseOption;
        }
        private List<ProfileResponseOption> ReadRepeatorControl(DropDownList rpt, int QuestionID)
        {
            List<ProfileResponseOption> lstProfileResponseOption = new List<ProfileResponseOption>();

            if (rpt == null) return null;
            foreach (ListItem i in rpt.Items)
            {


                bool IsCheck = false;
                if (i.Selected)
                    IsCheck = true;
                var oProfileResponseOption = new ProfileResponseOption()
                {
                    ProfileResponseOptionId = -1,
                    QuestionID = QuestionID,
                    QuestionOptionID = Convert.ToInt32(i.Value),
                    IsActive = IsCheck
                };

                lstProfileResponseOption.Add(oProfileResponseOption);
            }
            return lstProfileResponseOption;
        }




        private List<ProfileResponse> ReadRepeator(Repeater id)
        {
            List<ProfileResponse> pResponse = new List<ProfileResponse>();
            foreach (RepeaterItem item in id.Items)
            {


                HiddenField hdnQuestionTypeID = item.FindControl("hdnQuestionType") as HiddenField;
                HiddenField hdnQuestionID = item.FindControl("hdnQuestionID") as HiddenField;

                if (hdnQuestionTypeID != null && hdnQuestionID != null)
                {
                    var oProfileResponse = new ProfileResponse()
                    {
                        UserID = UserId,
                        CreatedOnDate = System.DateTime.Now,
                        UpdateOnDate = System.DateTime.Now,
                        QuestionId = Convert.ToInt32(hdnQuestionID.Value),
                        QuestionOptionId = (int)Utiity.ControlType.TextBox,
                        ResponseText = ""
                    };

                    if (hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.TextBox) || (hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.MultiTextBox)))
                    {

                        oProfileResponse.ResponseText = (item.FindControl("txtinputBox") as TextBox).Text;
                    }
                    else if (hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.RadioButton) || hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.GlobalCategory))
                    {

                        oProfileResponse.QuestionOptionId = Convert.ToInt32(hdnQuestionTypeID.Value);


                        Repeater rpt = item.FindControl("rptQuestionOptions") as Repeater;

                        oProfileResponse.LstProfileResponseOption = ReadRepeatorControl(rpt,true, oProfileResponse.QuestionId);
                    }
                    else if (hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.DropDown))
                    {
                        oProfileResponse.QuestionOptionId = Convert.ToInt32(hdnQuestionTypeID.Value);
                        Repeater rpt = item.FindControl("rptQuestionOptions") as Repeater;

                        oProfileResponse.LstProfileResponseOption = ReadRepeatorControl(rpt, true, oProfileResponse.QuestionId);

                    }


                    else if (hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.GlobalCompany))
                    {

                        oProfileResponse.QuestionOptionId = Convert.ToInt32(hdnQuestionTypeID.Value);


                        DropDownList rpt = item.FindControl("rptQuestionOptions") as DropDownList;
                        foreach (ListItem l in rpt.Items)
                        {
                            if (l.Selected)
                            {
                                oProfileResponse.ResponseText = Convert.ToString(l.Value);
                            }

                        }
                        // oProfileResponse.LstProfileResponseOption = ReadRepeatorControl(rpt, oProfileResponse.QuestionId);
                    }

                    pResponse.Add(oProfileResponse);
                }

            }
            return pResponse;

        }

        private List<ProfileResponse> ReadRepeator(Repeater id, bool Threelevel)
        {
            List<ProfileResponse> pResponse = new List<ProfileResponse>();
            var LstProfileResponseOption = new List<ProfileResponseOption>();
            foreach (RepeaterItem item in id.Items)
            {


                HiddenField hdnQuestionTypeID = item.FindControl("hdnQuestionType") as HiddenField;
                HiddenField hdnQuestionID = item.FindControl("hdnQuestionID") as HiddenField;

                if (hdnQuestionTypeID != null && hdnQuestionID != null)
                {
                    var oProfileResponse = new ProfileResponse()
                    {
                        UserID = UserId,
                        CreatedOnDate = System.DateTime.Now,
                        UpdateOnDate = System.DateTime.Now,
                        QuestionId = Convert.ToInt32(hdnQuestionID.Value),
                        QuestionOptionId = (int)Utiity.ControlType.TextBox,
                        ResponseText = ""
                    };

                    if (hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.TextBox) || (hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.MultiTextBox)))
                    {

                        oProfileResponse.ResponseText = (item.FindControl("txtinputBox") as TextBox).Text;
                    }
                    else if (hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.MultiSelect))
                    {

                        oProfileResponse.QuestionOptionId = Convert.ToInt32(hdnQuestionTypeID.Value);
                        Repeater rpt = item.FindControl("rptMainCategory") as Repeater;
                        List<ProfileResponseOption> lstProfileResponseOption = new List<ProfileResponseOption>();
                        if (rpt != null)
                            foreach (RepeaterItem i in rpt.Items)
                            {

                                if (i.ItemType == ListItemType.Item || i.ItemType == ListItemType.AlternatingItem)
                                {

                                    Repeater rLast = i.FindControl("rptSubCategory") as Repeater;
                                    if (rLast != null)
                                    {
                                        foreach (RepeaterItem iinner in rLast.Items)
                                        {
                                            if (iinner.ItemType == ListItemType.Item || iinner.ItemType == ListItemType.AlternatingItem)
                                            {

                                                Repeater rQuestionOptions = iinner.FindControl("rptQuestionOptions") as Repeater;
                                                var oRepatorData = ReadRepeatorControl(rQuestionOptions, Convert.ToInt32(hdnQuestionID.Value));

                                                if (oRepatorData.Count > 0)
                                                    LstProfileResponseOption.AddRange(oRepatorData);

                                            }
                                        }

                                    }

                                }
                            }

                    }
                    oProfileResponse.LstProfileResponseOption = LstProfileResponseOption;
                    pResponse.Add(oProfileResponse);
                }

            }
            return pResponse;

        }

        private void LoadQuestion()
        {


            //Load default profile


            var oController = new DbController();

            var oQuestions = oController.GetAllQuestion(false);

            //GetResponse by QuestionID


            rptStep2.DataSource = oQuestions.Where(x => x.StepID == 2);
            rptStep2.DataBind();
            rptStep3.DataSource = oQuestions.Where(x => x.StepID == 3);
            rptStep3.DataBind();

            rptStep4.DataSource = oQuestions.Where(x => x.StepID == 4);
            rptStep4.DataBind();



            rptStep5.DataSource = oQuestions.Where(x => x.StepID == 5);
            rptStep5.DataBind();

            rptStep6.DataSource = oQuestions.Where(x => x.StepID == 6);
            rptStep6.DataBind();

            if (Session[Utiity.Linkedin_SessionKey] == null)
            {
                txtlinkedin.Value = GetQuestionResponseText(ModuleSettingKey("Linkedin"));
            }
            txttwitter.Value = GetQuestionResponseText(ModuleSettingKey("Twitter"));
            txtfacebook.Value = GetQuestionResponseText(ModuleSettingKey("Facebook"));



            //Bin Roles to Step#4
            cmbRoles.DataSource = oController.GetPortalRoles(this.PortalId);
            cmbRoles.DataTextField = "RoleName";
            cmbRoles.DataValueField = "RoleID";
            cmbRoles.DataBind();
            string strUserRole = oController.GetcurrentUserRoleName(this.PortalId, UserId);
            if (!string.IsNullOrEmpty(strUserRole))
                cmbRoles.Items.FindByText(strUserRole).Selected = true;


            hdnSlider1.Value = GetQuestionResponseText(ModuleSettingKey("Rank1"));
            if (string.IsNullOrEmpty(hdnSlider1.Value))
                hdnSlider1.Value = "4";
            hdnSlider2.Value = GetQuestionResponseText(ModuleSettingKey("Rank2"));
            if (string.IsNullOrEmpty(hdnSlider2.Value))
                hdnSlider2.Value = "4";
            hdnSlider11.Value = hdnSlider1.Value;
            hdnSlider22.Value = hdnSlider2.Value;

        }

        private string ReturnUrl
        {
            get
            {
                return DotNetNuke.Common.Globals.NavigateURL(this.TabId, "", "Mid=" + ModuleId.ToString(), "SocialSiteName=LinkedIn", "Step=2");
            }
        }

        private void UploadAvatar()
        {

            //Generate User Folder
            UserInfo myDnnUser = this.UserInfo;
            DotNetNuke.Services.FileSystem.IFolderManager folderManager = DotNetNuke.Services.FileSystem.FolderManager.Instance;

            DotNetNuke.Services.FileSystem.IFolderInfo folderInfo = folderManager.GetUserFolder(myDnnUser);

            if (!System.IO.Directory.Exists(folderInfo.PhysicalPath))
                System.IO.Directory.CreateDirectory(folderInfo.PhysicalPath);


            if (Session[Utiity.Linkedin_SessionKey] != null && imageUpload.PostedFile.ContentLength == 0 && !string.IsNullOrEmpty(imgprvw.Src))
            {

                var linkedin = (Linkedin.LinkEdin)Session[Utiity.Linkedin_SessionKey];

                string TempFile = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                if (!string.IsNullOrEmpty(linkedin.pictureUrl))
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(linkedin.pictureUrl, folderInfo.PhysicalPath + TempFile);


                    //   FileStream fs = new FileStream(folderInfo.PhysicalPath + TempFile, FileMode.Create);

                    try
                    {

                        //var objFiles = new FileController();
                        var objFile = (DotNetNuke.Services.FileSystem.FileInfo)FileManager.Instance.AddFile(folderInfo, TempFile, null);
                        UserInfo.Profile.Photo = objFile != null ? objFile.FileId.ToString() : UserInfo.Profile.Photo;

                        ProfileController.UpdateUserProfile(UserInfo);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        //      fs.Close();
                        //       bw.Close();
                    }
                    //Save to DNN profile

                }

            }

            string CropedFileName = "";
            if (imageUpload.PostedFile != null && imageUpload.PostedFile.ContentLength > 0)
            {
                string FileName = System.IO.Path.GetFileName(imageUpload.PostedFile.FileName);
                string FolderPath = folderInfo.PhysicalPath + FileName;
                CropedFileName = FileName;

                DotNetNuke.Services.FileSystem.FileInfo objFile = (DotNetNuke.Services.FileSystem.FileInfo)FileManager.Instance.AddFile(folderInfo, FileName, imageUpload.PostedFile.InputStream);
                UserInfo.Profile.Photo = objFile != null ? objFile.FileId.ToString() : UserInfo.Profile.Photo;



                ProfileController.UpdateUserProfile(UserInfo);




            }


            if (!string.IsNullOrEmpty(HiddenFieldCropedImageString.Value))
            {
                try
                {
                    string FileName = "";
                    if (!string.IsNullOrEmpty(CropedFileName))
                    {
                        FileName = CropedFileName;
                    }
                    else
                    {
                        FileName = "" + DateTime.Now.Year.ToString() + "" + DateTime.Now.Month.ToString() + "" + DateTime.Now.Day.ToString() + "" + DateTime.Now.Hour.ToString() + "" + DateTime.Now.Minute.ToString() + "" + DateTime.Now.Second.ToString() + "";

                        if (!string.IsNullOrEmpty(HiddenFieldCropedImageExtension.Value))
                        {
                            FileName = "" + FileName + "." + HiddenFieldCropedImageExtension.Value + "";
                        }
                        else
                        {
                            FileName = "" + FileName + ".jpg";
                        }

                    }

                    string imgprvwBase64String = HiddenFieldCropedImageString.Value;
                    string FolderPath = folderInfo.PhysicalPath + FileName;

                    string x = imgprvwBase64String.Replace("data:image/png;base64,", "");
                    x = x.Replace("data:image/jpeg;base64,", "");



                    // Convert Base64 String to byte[]
                    byte[] imageBytes = Convert.FromBase64String(x);
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

                    // Convert byte[] to Image
                    ms.Write(imageBytes, 0, imageBytes.Length);

                    DotNetNuke.Services.FileSystem.FileInfo objFile = (DotNetNuke.Services.FileSystem.FileInfo)FileManager.Instance.AddFile(folderInfo, FileName, ms);
                    UserInfo.Profile.Photo = objFile != null ? objFile.FileId.ToString() : UserInfo.Profile.Photo;



                    ProfileController.UpdateUserProfile(UserInfo);
                }
                catch
                {

                }
            }


        }




        private void UpdateUserRoles()
        {



            // new DbController.UpdateUserRoleInformation()
            int iRoleId = cmbRoles.SelectedValue != null ? Convert.ToInt32(cmbRoles.SelectedValue) : -1;
            if (iRoleId > 0)
                new DbController().UpdateUserRoleInformation(PortalSettings, PortalId, iRoleId, UserInfo, true);
        }
        #endregion
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
        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DotNetNuke.Framework.jQuery.RequestDnnPluginsRegistration();
                DotNetNuke.UI.Utilities.ClientAPI.AddButtonConfirm(linkedin_button, "Are You sure want to navigate to other page. All un-saved information will be lost?");
                if (UserInfo.UserID == -1)
                {
                    Globals.Redirect(Globals.AccessDeniedURL(string.Format("You are not authorize to access profile page. Please do login with your credentials. To Login <a href='{0}'>Click here</a>", Globals.NavigateURL(PortalSettings.LoginTabId))), true);

                }
                if (RedirectToHome())
                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.HomeTabId));

                imgprvw.Src = UserInfo.Profile.PhotoURL;




                ///Check for linkedin setting is setup or not
                ///
                if (string.IsNullOrEmpty(ModuleSettingKeyString(Utiity.LinkedinAPIKey)) || string.IsNullOrEmpty(ModuleSettingKeyString(Utiity.LinkedinToken)) || string.IsNullOrEmpty(ModuleSettingKeyString(Utiity.LinkedinScope)))
                    toplinkedin.Visible = false;

                Session[Utiity.Linkedin_SessionKey] = null;
                if (Request.QueryString["SocialSiteName"] != null)
                {
                    if (Request.QueryString["Code"] != null)
                    {
                        ClearAction.Modules.ProfileClearAction_ProfileSetup.Linkedin.LinkEdin oLinkedinProfileInfo = new Components.WebController().GetAuthorizeCode(Convert.ToString(Request.QueryString["Code"]), ReturnUrl, ModuleSettingKeyString("LinkedinAPIKey"), ModuleSettingKeyString("LinkedinToken"), new OAuthContext());
                        if (oLinkedinProfileInfo != null)
                        {
                            txtlinkedin.Value = oLinkedinProfileInfo.publicProfileUrl;
                            Session[Utiity.Linkedin_SessionKey] = oLinkedinProfileInfo;
                            imgprvw.Src = oLinkedinProfileInfo.pictureUrl;
                        }


                    }
                }
                LoadQuestion();
            }
        }
        #endregion



        private ProfileResponse GetDefault(int iQuestionid, string responsetext)
        {
            var o = new ProfileResponse()
            {
                CreatedOnDate = System.DateTime.Now,
                UserID = UserId,
                UpdateOnDate = System.DateTime.Now,
                QuestionId = iQuestionid,
                LstProfileResponseOption = null,
                ProfileResponseId = -1,
                QuestionOptionId = -1,
                ResponseText = responsetext

            };
            return o;
        }


        protected void btnSaveInformation_Click(object sender, EventArgs e)
        {

            //Save Basic Information


            var oController = new DbController();


            List<ProfileResponse> pResponse = new List<ProfileResponse>();
            pResponse.Add(GetDefault(ModuleSettingKey("Facebook"), txtfacebook.Value));
            pResponse.Add(GetDefault(ModuleSettingKey("Linkedin"), txtlinkedin.Value));
            pResponse.Add(GetDefault(ModuleSettingKey("Twitter"), txttwitter.Value));
            pResponse.Add(GetDefault(ModuleSettingKey("Rank1"), hdnSlider1.Value));
            pResponse.Add(GetDefault(ModuleSettingKey("Rank2"), hdnSlider2.Value));


            //Save Step#2
            pResponse.AddRange(ReadRepeator(rptStep2));
            oController.AddUpdateProfile(pResponse);


            //Save Step#3

            pResponse = null;
            pResponse = ReadRepeator(rptStep3);
            oController.AddUpdateProfile(pResponse);
            pResponse = null;

            //Save Step#4

            pResponse = ReadRepeator(rptStep4);
            oController.AddUpdateProfile(pResponse);


            //Save Step#4_b
            pResponse = null;
            pResponse = ReadRepeator(rptStep5, true);
            oController.AddUpdateProfile(pResponse);



            //Save Step#4_C

            pResponse = null;
            pResponse = ReadRepeator(rptStep6);
            oController.AddUpdateProfile(pResponse);
            UserInfo.Profile.SetProfileProperty("ProfileFinished", "1");
            UserController.UpdateUser(this.PortalId, UserInfo);

            try
            {
                oController.UpdateTagsInformation(UserId);


                // Update the Category Assignment to the user on the basis of new selections
                UpdateUserSSolveSpaceAssignment(UserId);

                UploadAvatar();
                UpdateUserRoles();
            }
            catch (Exception exc)
            {

                //throw;
            }

            //Update Profile Perference for Blogs
            Globals.Redirect(Globals.NavigateURL(this.TabId) + "?Step=5", true);
            //  Page.ClientScript.RegisterStartupScript(GetType(), "MyKey", "divToShow(6);", true);
        }

        private void UpdateUserSSolveSpaceAssignment(int UserId)
        {

            try
            {
                /* Added by Sachin on 11th Jan 2018 */
                DbController oCtrl = new DbController();
                SolveSpaceController oSSCtrl = new SolveSpaceController();
                ComponentController oCmCtrl = new ComponentController();




                List<QuestionOption> lstSelectedCategories = oCtrl.GetUserProfileCategories(UserId);
                foreach (QuestionOption QO in lstSelectedCategories)
                {
                    // get solvesapcess in the selected category'
                    if (QO.QuestionOptionID != -1)
                    {
                        // get all the solvespace in the given category
                        List<SolveSpaceInfo> lstSolveSpace = oSSCtrl.ListSolveSpacesWithStatus(UserId, QO.QuestionOptionID);
                        foreach (SolveSpaceInfo oSS in lstSolveSpace)
                        {
                            // if not assigned then assign
                            if (!oSS.IsAssigned)
                            {
                                oCmCtrl.AssignToUser(3, QO.QuestionOptionID, UserId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }

        }
        protected void rptStep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var oQuestion = (Components.Question)e.Item.DataItem;
                HiddenField hdnQuestionTypeID = e.Item.FindControl("hdnQuestionType") as HiddenField;
                HiddenField hdnQuestionID = e.Item.FindControl("hdnQuestionID") as HiddenField;
                PlaceHolder pText = e.Item.FindControl("pholderInputText") as PlaceHolder;
                PlaceHolder pholderInputChoice = e.Item.FindControl("pholderInputChoice") as PlaceHolder;
                if (pholderInputChoice != null)
                    pholderInputChoice.Visible = false;
                if (pText != null)
                    pText.Visible = false;

                if ((hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.TextBox)) || (hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.MultiTextBox)))
                {
                    if (pText != null)
                        pText.Visible = true;





                    var itxtinputBox = e.Item.FindControl("txtinputBox") as TextBox;
                    bool IsSingleBox = (hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.TextBox)) ? true : false;
                    itxtinputBox.CssClass = "form-control";
                    if (!IsSingleBox)
                    {
                        itxtinputBox.Rows = 3;
                        itxtinputBox.TextMode = TextBoxMode.MultiLine;
                        itxtinputBox.MaxLength = oQuestion.Size;
                        // itxtinputBox.MaxLength=(txttwitter.ValidateRequestMode)

                    }
                    var Isrequire = e.Item.FindControl("hdnIsRequire") as HiddenField;
                    if (Isrequire != null)
                    {

                        RequiredFieldValidator req = e.Item.FindControl("reqField") as RequiredFieldValidator;
                        if (req != null)
                        {
                            bool IsValidateNeed = Isrequire.Value == "1" ? true : false;
                            req.ValidationGroup = IsValidateNeed ? req.ValidationGroup : "val_" + Guid.NewGuid().ToString().Replace("-", "");
                            req.Enabled = IsValidateNeed ? true : false;
                            req.ControlToValidate = itxtinputBox.ID;
                            req.Visible = IsValidateNeed;

                        }
                    }

                    //Load default user information for first name and last name
                    itxtinputBox.Text = GetQuestionResponseText(Convert.ToInt32(hdnQuestionID.Value));
                    if (string.IsNullOrEmpty(itxtinputBox.Text))
                    {
                        if (ModuleSettingKeyString(Utiity.FirstName) == hdnQuestionID.Value)
                            itxtinputBox.Text = UserInfo.FirstName;
                        if (ModuleSettingKeyString(Utiity.LastName) == hdnQuestionID.Value)
                            itxtinputBox.Text = UserInfo.LastName;
                    }

                    //Linkedin Session value
                    try
                    {
                        if (Session[Utiity.Linkedin_SessionKey] != null)
                        {
                            var oLinkedin = (Linkedin.LinkEdin)Session[Utiity.Linkedin_SessionKey];
                            if (hdnQuestionID.Value == ModuleSettingKeyString(Utiity.Education))
                                itxtinputBox.Text = oLinkedin.Education;


                            if (hdnQuestionID.Value == ModuleSettingKeyString(Utiity.Overview))
                                itxtinputBox.Text = oLinkedin.summary;

                            if (hdnQuestionID.Value == ModuleSettingKeyString(Utiity.WorkHistory))
                                itxtinputBox.Text = oLinkedin.Postion;

                            if (hdnQuestionID.Value == ModuleSettingKeyString(Utiity.Location))
                                itxtinputBox.Text = oLinkedin.location == null ? itxtinputBox.Text : oLinkedin.location.name;

                            if (hdnQuestionID.Value == ModuleSettingKeyString(Utiity.Title))
                                itxtinputBox.Text = string.IsNullOrEmpty(oLinkedin.headline) == true ? itxtinputBox.Text : oLinkedin.headline;




                        }
                    }
                    catch (Exception ex)
                    {


                    }



                }

                else if (hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.DropDown))
                {

                    if (pholderInputChoice != null)
                        pholderInputChoice.Visible = true;

                    HiddenField hdfld = (HiddenField)e.Item.FindControl("QuestionOptionID");

                    ////if(hdfld.Value)

                    Repeater rpt = e.Item.FindControl("rptQuestionOptions") as Repeater;
                    rpt.DataSource = new DbController().GetQuestionOption(Convert.ToInt32(hdnQuestionID.Value));
                    rpt.DataBind();


                    //var Questionoptions = new DbController().GetQuestionOption(Convert.ToInt32(hdnQuestionID.Value)); ;
                    //RadioButtonList cmbRadioControl = (RadioButtonList)e.Item.FindControl("cmbRadioControl");
                    //cmbRadioControl.DataSource = Questionoptions;
                    //cmbRadioControl.DataTextField = "OptionText";
                    //cmbRadioControl.DataValueField = "QuestionOptionID";
                    //cmbRadioControl.DataBind();
                    ////Do check which option is selected.
                    //bool IsOptionSelected = false;
                    //foreach (QuestionOption oP in Questionoptions)
                    //{
                    //    ///Mark them as selected
                    //    if (!IsOptionSelected)
                    //    {
                    //        IsOptionSelected = GetQuestionResponseRadio(Convert.ToInt32(hdnQuestionID.Value), oP.QuestionOptionID);
                    //        if (IsOptionSelected)
                    //        {
                    //            cmbRadioControl.Items.FindByValue(oP.QuestionOptionID.ToString()).Selected = true;

                    //        }
                    //    }
                    //}



                }

                else if (hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.RadioButton))
                {

                    if (pholderInputChoice != null)
                        pholderInputChoice.Visible = true;
                    Repeater rpt = e.Item.FindControl("rptQuestionOptions") as Repeater;
                    rpt.DataSource = new DbController().GetQuestionOption(Convert.ToInt32(hdnQuestionID.Value));
                    rpt.DataBind();

                }

                else if (hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.GlobalCategory))
                {

                    if (pholderInputChoice != null)
                        pholderInputChoice.Visible = true;
                    Repeater rpt = e.Item.FindControl("rptQuestionOptions") as Repeater;
                    rpt.DataSource = new DbController().GetQuestionGlobcalCategory(Convert.ToInt32(hdnQuestionID.Value));
                    rpt.DataBind();

                }
                else if (hdnQuestionTypeID.Value == Convert.ToString((int)Utiity.ControlType.GlobalCompany))
                {

                    if (pholderInputChoice != null)
                        pholderInputChoice.Visible = true;
                    DropDownList rpt = e.Item.FindControl("rptQuestionOptions") as DropDownList;
                    rpt.DataSource = new DbController().GetCompanyList(Convert.ToInt32(hdnQuestionID.Value));
                    rpt.DataValueField = "QuestionOptionID";
                    rpt.DataTextField = "OptionText";
                    rpt.DataBind();

                    try
                    {
                        rpt.Items.Insert(0, new ListItem("Select Company", "-1"));


                        if (Session[Utiity.Linkedin_SessionKey] != null)
                        {
                            var oLinkedin = (Linkedin.LinkEdin)Session[Utiity.Linkedin_SessionKey];

                            if (hdnQuestionID.Value == ModuleSettingKeyString(Utiity.CompanyName))
                                rpt.Items.FindByText(oLinkedin.CompanyName).Selected = true;
                            else
                            {
                                rpt.Items.FindByText("Other".ToLower()).Selected = true;
                            }


                        }

                        var oDataText = GetQuestionResponseText(Convert.ToInt32(hdnQuestionID.Value));
                        if (!string.IsNullOrEmpty(oDataText))
                            rpt.Items.FindByValue(Convert.ToString(oDataText)).Selected = true;

                        //Linkedin Session value


                    }
                    catch (Exception ex)
                    {


                    }




                }



            }
        }
        protected void rptStep4_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            rptStep_ItemDataBound(sender, e);
        }
        protected void rptStep6_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            rptStep_ItemDataBound(sender, e);
        }

        protected void rptStep5_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnQuestionID = e.Item.FindControl("hdnQuestionID") as HiddenField;

                Repeater rpt = e.Item.FindControl("rptMainCategory") as Repeater;
                rpt.DataSource = new DbController().GetMainCategoryByQuestionID(Convert.ToInt32(hdnQuestionID.Value));
                rpt.DataBind();

            }
        }

        protected void rptMainCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnCategoryID = e.Item.FindControl("hdnCategoryID") as HiddenField;
                Repeater rpt = e.Item.FindControl("rptSubCategory") as Repeater;
                rpt.DataSource = new DbController().GetChildCategory(Convert.ToInt32(hdnCategoryID.Value)).ToList();
                rpt.DataBind();
            }
        }





        protected void rptSubCategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnCategoryID = e.Item.FindControl("hdnSubCategoryID") as HiddenField;
                HiddenField hdnQuestionInnerID = e.Item.FindControl("hdnQuestionInnerID") as HiddenField;

                Repeater rpt = e.Item.FindControl("rptQuestionOptions") as Repeater;
                var oQuestionRecord = new DbController().GetQuestionOption(Convert.ToInt32(hdnQuestionInnerID.Value), Convert.ToInt32(hdnCategoryID.Value)).ToList();
                rpt.DataSource = oQuestionRecord;
                rpt.DataBind();
            }

        }

        protected void linkedin_button_Click(object sender, EventArgs e)
        {


            //            string returnUrl = DotNetNuke.Common.Globals.NavigateURL(this.TabId, "Step1", "Mid=" + ModuleId.ToString(), "SocialSiteName=LinkedIn&LoginType=linkedin", "Jobid=" + Request.QueryString["Jobid"]);
            //   OAuthContext ocallback = new OAuthContext(returnUrl);
            //  string oauth_token = Request.QueryString["oauth_token"];//oauth_verifier
            //    string oauth_verifier = Request.QueryString["oauth_verifier"];
            //     OAuthLinkedInClient oAutLinkedin = new OAuthLinkedInClient();
            var oRequesturl = WebController.GetAuthorization(ModuleSettingKeyString(Utiity.LinkedinAPIKey), ReturnUrl, ModuleSettingKeyString(Utiity.LinkedinScope));// oAutLinkedin.BeginAuthentication(returnUrl, ModuleSettingKeyString("LinkedinAPIKey"), ModuleSettingKeyString("LinkedinToken"));
            Response.Redirect(oRequesturl, true);


            //OAuthContext ocallback = new OAuthContext(ReturnUrl);

            //OAuthLinkedInClient oAutLinkedin = new OAuthLinkedInClient();
            //oAutLinkedin.BeginAuthentication(ReturnUrl, ModuleSettingKeyString("LinkedinAPIKey"), ModuleSettingKeyString("LinkedinToken"));

        }

        protected void rptQuestionOptions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;
            var data = (QuestionOption)e.Item.DataItem;
            RadioButton rdo = (RadioButton)e.Item.FindControl("radioControl");
            rdo.Checked = GetQuestionResponseRadio(data.QuestionID, data.QuestionOptionID);
            string script =
               "SetUniqueRadioButton('rptQuestionOptions.*radioControl',this)";
            rdo.Attributes.Add("onclick", script);
        }
        protected void rptQuestionOptions1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
                return;
            var data = (QuestionOption)e.Item.DataItem;
            RadioButton rdo = (RadioButton)e.Item.FindControl("radioControl1");
            rdo.Checked = GetQuestionResponseRadio(data.QuestionID, data.QuestionOptionID);
            string script =
               "SetUniqueRadioButton('rptQuestionOptions.*radioControl1',this)";
            rdo.Attributes.Add("onclick", script);
        }

    }
}