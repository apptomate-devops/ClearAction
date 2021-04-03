
using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Portals;
using ClearAction.Modules.ProfileClearAction_ProfileSetup.Components;
namespace ClearAction.Modules.ProfileClearAction_ProfileSetup
{
    public partial class Settings : ProfileModuleSettingsBase
    {


        private void BindQuestion()
        {

            var oRecord = new Components.DbController().GetQuestion(false);
            ddquestions1.DataSource = oRecord;
            ddquestions1.DataBind();
            ddquestions2.DataSource = oRecord;
            ddquestions2.DataBind();

            ddquestions3.DataSource = oRecord;
            ddquestions3.DataBind();

            ddquestions4.DataSource = oRecord;
            ddquestions4.DataBind();

            ddquestions5.DataSource = oRecord;
            ddquestions5.DataBind();

            ddquestions6.DataSource = oRecord;
            ddquestions6.DataBind();

            ddquestions7.DataSource = oRecord;
            ddquestions7.DataBind();

            ddquestions8.DataSource = oRecord;
            ddquestions8.DataBind();


            ddquestions9.DataSource = oRecord;
            ddquestions9.DataBind();


            ddquestions10.DataSource = oRecord;
            ddquestions10.DataBind();

            ddquestions11.DataSource = oRecord;
            ddquestions11.DataBind();


            ddquestions12.DataSource = oRecord;
            ddquestions12.DataBind();

            ddquestions13.DataSource = oRecord;
            ddquestions13.DataBind();
            ddquestions14.DataSource = oRecord;
            ddquestions14.DataBind();
            ddFirstName.DataSource = oRecord;
            ddFirstName.DataBind();

            ddLastName.DataSource = oRecord;
            ddLastName.DataBind();

        }
        private string ReturnUrl
        {
            get
            {
                return DotNetNuke.Common.Globals.NavigateURL(this.TabId, "", "Mid=" + ModuleId.ToString(), "SocialSiteName=LinkedIn", "Step=2");
            }
        }
        public override void LoadSettings()
        {
            try
            {
                if (Page.IsPostBack == false)
                {
                    BindQuestion();
                    lblReturnUrl.Text = ReturnUrl;

                    txtLinkedinAPIKey.Text = PortalController.GetPortalSetting(Utiity.LinkedinAPIKey, PortalId, "LinkedinAPIKey");
                    txtScope.Text = PortalController.GetPortalSetting(Utiity.LinkedinScope, PortalId, "r_basicprofile r_emailaddress");


                    txtLinkedinToken.Text = PortalController.GetPortalSetting(Utiity.LinkedinToken, PortalId, "LinkedinToken");// Convert.ToString(Settings["LinkedinToken"]);


                    ddquestions1.Items.FindByValue(PortalController.GetPortalSetting(Utiity.Linkedin, PortalId, ddquestions1.Items[0].Value)).Selected = true;

                    ddquestions2.Items.FindByValue(PortalController.GetPortalSetting(Utiity.Facebook, PortalId, ddquestions2.Items[0].Value)).Selected = true;


                    ddquestions3.Items.FindByValue(PortalController.GetPortalSetting(Utiity.Twitter, PortalId, ddquestions3.Items[0].Value)).Selected = true;



                    ddquestions4.Items.FindByValue(PortalController.GetPortalSetting(Utiity.Rank1, PortalId, ddquestions4.Items[0].Value)).Selected = true;

                    ddquestions5.Items.FindByValue(PortalController.GetPortalSetting(Utiity.Rank2, PortalId, ddquestions5.Items[0].Value)).Selected = true;


                    ddFirstName.Items.FindByValue(PortalController.GetPortalSetting(Utiity.FirstName, PortalId, ddFirstName.Items[0].Value)).Selected = true;


                    ddLastName.Items.FindByValue(PortalController.GetPortalSetting(Utiity.LastName, PortalId, ddLastName.Items[0].Value)).Selected = true;

                    ddquestions6.Items.FindByValue(PortalController.GetPortalSetting(Utiity.Phone, PortalId, ddquestions6.Items[0].Value)).Selected = true;
                    ddquestions7.Items.FindByValue(PortalController.GetPortalSetting(Utiity.Location, PortalId, ddquestions7.Items[0].Value)).Selected = true;


                    ddquestions8.Items.FindByValue(PortalController.GetPortalSetting(Utiity.Education, PortalId, ddquestions8.Items[0].Value)).Selected = true;
                    ddquestions9.Items.FindByValue(PortalController.GetPortalSetting(Utiity.HonorsReward, PortalId, ddquestions9.Items[0].Value)).Selected = true;
                    ddquestions10.Items.FindByValue(PortalController.GetPortalSetting(Utiity.Overview, PortalId, ddquestions10.Items[0].Value)).Selected = true;
                    ddquestions11.Items.FindByValue(PortalController.GetPortalSetting(Utiity.ProfessionalAssociates, PortalId, ddquestions11.Items[0].Value)).Selected = true;
                    ddquestions12.Items.FindByValue(PortalController.GetPortalSetting(Utiity.Title, PortalId, ddquestions12.Items[0].Value)).Selected = true;
                    ddquestions13.Items.FindByValue(PortalController.GetPortalSetting(Utiity.WorkHistory, PortalId, ddquestions13.Items[0].Value)).Selected = true;

                    ddquestions14.Items.FindByValue(PortalController.GetPortalSetting(Utiity.CompanyName, PortalId, ddquestions14.Items[0].Value)).Selected = true;
                }
            }
            catch (Exception exc) //Module failed to load
            {
                //  Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        public override void UpdateSettings()
        {
            try
            {
                var modules = new ModuleController();
                PortalController.UpdatePortalSetting(this.PortalId, Utiity.LinkedinAPIKey, txtLinkedinAPIKey.Text);

                PortalController.UpdatePortalSetting(this.PortalId, Utiity.LinkedinScope, txtScope.Text);

                PortalController.UpdatePortalSetting(this.PortalId, Utiity.LinkedinToken, txtLinkedinToken.Text);
                PortalController.UpdatePortalSetting(this.PortalId, Utiity.Linkedin, ddquestions1.SelectedItem == null ? ddquestions1.Items[0].Value : ddquestions1.SelectedValue);
                PortalController.UpdatePortalSetting(this.PortalId, Utiity.Facebook, ddquestions2.SelectedItem == null ? ddquestions2.Items[0].Value : ddquestions2.SelectedValue);
                PortalController.UpdatePortalSetting(this.PortalId, Utiity.Twitter, ddquestions3.SelectedItem == null ? ddquestions3.Items[0].Value : ddquestions3.SelectedValue);
                PortalController.UpdatePortalSetting(this.PortalId, Utiity.Rank1, ddquestions4.SelectedItem == null ? ddquestions4.Items[0].Value : ddquestions4.SelectedValue);
                PortalController.UpdatePortalSetting(this.PortalId, Utiity.Rank2, ddquestions5.SelectedItem == null ? ddquestions5.Items[0].Value : ddquestions5.SelectedValue);

                PortalController.UpdatePortalSetting(this.PortalId, Utiity.FirstName, ddFirstName.SelectedItem == null ? ddFirstName.Items[0].Value : ddFirstName.SelectedValue);
                PortalController.UpdatePortalSetting(this.PortalId, Utiity.LastName, ddLastName.SelectedItem == null ? ddLastName.Items[0].Value : ddLastName.SelectedValue);

                PortalController.UpdatePortalSetting(this.PortalId, Utiity.Phone, ddquestions6.SelectedItem == null ? ddquestions6.Items[0].Value : ddquestions6.SelectedValue);
                PortalController.UpdatePortalSetting(this.PortalId, Utiity.Location, ddquestions7.SelectedItem == null ? ddquestions7.Items[0].Value : ddquestions7.SelectedValue);




                PortalController.UpdatePortalSetting(this.PortalId, Utiity.Education, ddquestions8.SelectedItem == null ? ddquestions8.Items[0].Value : ddquestions8.SelectedValue);
                PortalController.UpdatePortalSetting(this.PortalId, Utiity.HonorsReward, ddquestions9.SelectedItem == null ? ddquestions9.Items[0].Value : ddquestions9.SelectedValue);
                PortalController.UpdatePortalSetting(this.PortalId, Utiity.Overview, ddquestions10.SelectedItem == null ? ddquestions10.Items[0].Value : ddquestions10.SelectedValue);

                PortalController.UpdatePortalSetting(this.PortalId, Utiity.ProfessionalAssociates, ddquestions11.SelectedItem == null ? ddquestions11.Items[0].Value : ddquestions11.SelectedValue);

                PortalController.UpdatePortalSetting(this.PortalId, Utiity.Title, ddquestions12.SelectedItem == null ? ddquestions12.Items[0].Value : ddquestions12.SelectedValue);

                PortalController.UpdatePortalSetting(this.PortalId, Utiity.WorkHistory, ddquestions13.SelectedItem == null ? ddquestions13.Items[0].Value : ddquestions13.SelectedValue);
                PortalController.UpdatePortalSetting(this.PortalId, Utiity.CompanyName, ddquestions14.SelectedItem == null ? ddquestions14.Items[0].Value : ddquestions14.SelectedValue);




            }



            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

    }
}