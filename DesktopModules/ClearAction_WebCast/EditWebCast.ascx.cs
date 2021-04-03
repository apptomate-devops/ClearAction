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
using System.Linq;
using DotNetNuke.Services.Exceptions;
using ClearAction.Modules.WebCast.Components;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Threading.Tasks;

namespace ClearAction.Modules.WebCast
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditClearAction_WebCast class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from ClearAction_WebCastModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class EditWebCast : ModuleBase
    {

        //add new line
        private void BindDropDown()
        {
            var controller = new Components.DBController();
            var oGlobalCategory = controller.GetGlobalCategory(-1).ToList();
            rdbCategory.DataSource = oGlobalCategory;
            rdbCategory.DataTextField = "CategoryName";
            rdbCategory.DataValueField = "CategoryId";
            rdbCategory.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtDescription.ShowRichTextBox = true;
                    txtDescription.InitialBind();

                    if (!Request.IsAuthenticated)
                        Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(PortalSettings.LoginTabId));
                    BindDropDown();
                    pnlAuthorDetail.Attributes.Add("style", "display:none");
                    hlCancel.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(this.TabId);
                    BindTags();
                    int iPostKey = GetQueryStringValue(Components.Util.PostKey, -1);
                    if (iPostKey > 0)
                    {
                        cmdDelete.Visible = true;
                        BindRecord(iPostKey);

                    }

                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void ddAuthor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BindTags()
        {
            DBController oCtrl = new DBController();
            IQueryable<CA_TaxonomyTerms> lstTags = oCtrl.GetTagList(-1);
            dlTags.DataSource = lstTags.ToList();
            dlTags.DataBind();
        }
        private void BindRecord(int iPostKey)
        {
            var oWccInfo = new Components.DBController().Get_Post(iPostKey);
            if (oWccInfo != null)
            {
                txtDescription.DefaultText = oWccInfo.ShortDescription;
                txtListenViewLink.Text = oWccInfo.ExpiredviewLink;
                txtRegistration.Text = oWccInfo.RegistrationLink;
                txtTitle.Text = oWccInfo.Title;
                profilepic.Src = oWccInfo.PresenterProfilePic;
                lblAuthorID.Text = Convert.ToString(oWccInfo.PresenterID);
                lblCompanyName.Text = oWccInfo.PresenterCompany;
                chkIsActive.Checked = oWccInfo.IsPublish;
                hdnAuthorID.Value = oWccInfo.PresenterID.ToString();
                hdnCompanyName.Value = oWccInfo.PresenterCompany;
                hdnContentItemID.Value = oWccInfo.WCCId.ToString();
                hdnDisplayName.Value = oWccInfo.PresenterName;
                hdnProfileUrl.Value = oWccInfo.PresenterProfilePic;
                hdnRole.Value = oWccInfo.PresenterTitle;
                mdBasicSearch.Attributes.Add("value", oWccInfo.PresenterName);
                pnlAuthorDetail.Attributes.Add("style", "display:block");
                txtWebApiKey.Text = oWccInfo.WebApiKey;
                dpPostDate.DbSelectedDate = oWccInfo.EventDate;

                //tpPostTime.DbSelectedDate = oWccInfo.EventTime;
                txtTime.Text = oWccInfo.EventTime.ToString("hh:mm tt");

                lblHeader.Text = "Update a Digital Event";



                if (oWccInfo.GetCategory != null)
                {
                    //foreach (PostCategoryRelationInfo oCategory in oWccInfo.GetCategory)
                    //{
                    //    //foreach (System.Web.UI.WebControls.ListItem l in ddlComponent.Items)
                    //    //{
                    //    //    if (l.Value == oCategory.CategoryId.ToString())
                    //    //    {
                    //    //        l.Selected = true;

                    //    //    }

                    //    //}
                    //}

                }

                ddlComponent.Items.FindByValue(oWccInfo.ComponentID.ToString()).Selected = true;
                Page.ClientScript.RegisterStartupScript(GetType(), "MySetTagsFunction", string.Format("ApplySelectedTags('{0}');", oWccInfo.tags.Replace(",", "_")), true);


            }
            else
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(), true);
        }
        protected void cmdSave_Click(object sender, EventArgs e)
        {

            int iWCCID = GetQueryStringValue(Util.PostKey, -1);

            var oController = new Components.DBController();
            WCC_PostInfo odata = null;
            if (iWCCID > 0)
            {

                odata = oController.Get_Post(iWCCID);
                odata.UpdatedOnDate = System.DateTime.Now;
                odata.UpdatedByUserID = UserId;
            }
            else
            {
                odata = new WCC_PostInfo();
                odata.CreatedOnDate = System.DateTime.Now;
                odata.UpdatedOnDate = System.DateTime.Now;
                odata.ViewCount = 0;
                odata.UpdatedByUserID = UserId;
                odata.CreatedByUserID = UserId;

            }

            if (txtRegistration.Text == "" && txtWebApiKey.Text == "")
            {
                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, "Either Registration link or WebAPi Key is required", DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.RedError);
                return;
            }
            if (chkIsActive.Checked)
                odata.IsPublish = true;
            odata.ComponentID = ddlComponent.SelectedItem == null ? 4 : Convert.ToInt32(ddlComponent.SelectedValue);
            odata.Title = txtTitle.Text;
            odata.PresenterID = string.IsNullOrEmpty(hdnAuthorID.Value) ? -1 : Convert.ToInt32(hdnAuthorID.Value);
            odata.LongDescription = "";
            odata.ShortDescription = txtDescription.DefaultText;
            odata.PresenterCompany = hdnCompanyName.Value;
            odata.PresenterName = hdnDisplayName.Value;
            odata.PresenterProfilePic = hdnProfileUrl.Value;
            odata.PresenterTitle = hdnRole.Value;
            odata.WebApiKey = txtWebApiKey.Text;

            odata.EventDate = dpPostDate.SelectedDate != null ? dpPostDate.SelectedDate.Value : System.DateTime.Now;
            odata.RegistrationLink = txtRegistration.Text;
            odata.ExpiredviewLink = txtListenViewLink.Text;

            //odata.EventTime = tpPostTime.SelectedDate != null ? tpPostTime.SelectedDate.Value : System.DateTime.Now;
            odata.EventTime = !string.IsNullOrEmpty(txtTime.Text) ? Convert.ToDateTime(txtTime.Text) : DateTime.Now;

            odata.filestackurl = "";

            if (iWCCID > 0)
                oController.UpdateWCC_Post(odata);
            else
                oController.AddWCC_Post(odata);
                

                




            //Add global Category


            foreach (System.Web.UI.WebControls.ListItem items in rdbCategory.Items)
            {

                var oGlobalCategory = new Components.PostCategoryRelationInfo()
                {
                    CategoryId = Convert.ToInt32(items.Value),
                    IsActive = items.Selected,
                    WCCId = odata.WCCId,
                    WPostCategoryId = -1


                };

                oController.WCCCategoryRelation_AddUpdate(oGlobalCategory);

            }

            var tagForm = string.Empty;
            //if (Request.Form["hidden-forum_tags"] != null)
            //    tagForm = Request.Form["hidden-forum_tags"];

            if (hdTagsSelected.Value != string.Empty)
            {
                tagForm = hdTagsSelected.Value;
                var tags = tagForm.Split(',');
                new DBController().ClearPostTags(odata.WCCId);
                foreach (var tag in tags)
                {
                    if (tag.Trim() != "")
                    {
                        var otags = new Components.Tags()
                        {
                            Clicks = 0,
                            Items = 1,
                            ModuleId = this.ModuleId,
                            Portalid = this.PortalId,
                            Priority = 0,
                            TagId = -1,
                            TagName = tag,
                            WCCID = odata.WCCId
                        };
                        new DBController().SavePostTag(otags);
                    }
                }
            }
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(this.TabId, "", Components.Util.PostKey + "=" + odata.WCCId.ToString()));
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {

                new DBController().DeleteWCC_Post(GetQueryStringValue(Components.Util.PostKey, -1));
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL(this.TabId));
            }
            catch (Exception ex)
            {


            }
        }

        protected void dlTags_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {
            if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Item || e.Item.ItemType == System.Web.UI.WebControls.ListItemType.AlternatingItem)
            {
                // Read TagID/TermID
                int TagID = ((ClearAction.Modules.WebCast.Components.CA_TaxonomyTerms)e.Item.DataItem).TermID;
                Label lblName = (Label)e.Item.FindControl("lblName");
                lblName.Text = ((ClearAction.Modules.WebCast.Components.CA_TaxonomyTerms)e.Item.DataItem).Name;
                // Get the children of current TermID
                DataList dlChild = (DataList)e.Item.FindControl("dlTagChildren");
                DBController oCtrl = new DBController();
                dlChild.DataSource = oCtrl.GetTagList(TagID);
                dlChild.DataBind();
            }
        }

        protected void dlTagChildren_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Read TagID/TermID
                int TagID = ((CA_TaxonomyTerms)e.Item.DataItem).TermID;
                CheckBox lblName = (CheckBox)e.Item.FindControl("lblChildName");
                if (((CA_TaxonomyTerms)e.Item.DataItem).VocabularyName.Trim() == "Single")
                {
                    lblName.InputAttributes.Add("class", "CA_Single");
                    // lblName.CssClass = "CA_Single";// = "CA_singleCheckbox" + ((CA_TaxonomyTerms)e.Item.DataItem).ParentTermID.ToString();
                }

                lblName.Text = ((CA_TaxonomyTerms)e.Item.DataItem).Name;
            }
        }
    }
}