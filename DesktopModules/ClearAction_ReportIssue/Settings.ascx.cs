using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.UI.UserControls;
using System;
using System.Web.UI.WebControls;

namespace ClearAction.Modules.ReportIssue
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Settings class manages Module Settings
    /// 
    /// Typically your settings control would be used to manage settings for your module.
    /// There are two types of settings, ModuleSettings, and TabModuleSettings.
    /// 
    /// ModuleSettings apply to all "copies" of a module on a site, no matter which page the module is on. 
    /// 
    /// TabModuleSettings apply only to the current module on the current page, if you copy that module to
    /// another page the settings are not transferred.
    /// 
    /// If you happen to save both TabModuleSettings and ModuleSettings, TabModuleSettings overrides ModuleSettings.
    /// 
    /// Below we have some examples of how to access these settings but you will need to uncomment to use.
    /// 
    /// Because the control inherits from ClearAction_ReportIssueSettingsBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class Settings : ClearAction_ReportIssueModuleSettingsBase
    {


        protected Label lblMailReceiver;

        protected TextBox txtMailReceiver;

        protected Label lblMailSubject;

        protected TextBox txtMailSubject;

        protected Label lblMessageSent;

        protected TextBox txtMessageSent;

        protected Label lblMessageNotSent;

        protected TextBox txtMessageNotSent;

        protected Label lblSubject;

        protected TextBox txtSubject;

        protected Label lblMailToSender;

        protected TextBox txtSender;


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public override void LoadSettings()
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    if ((string)base.TabModuleSettings["RI_MAIL_RECEIVER"] != null)
                    {
                        this.txtMailReceiver.Text = (string)base.TabModuleSettings["RI_MAIL_RECEIVER"];
                    }
                    else
                    {
                        this.txtMailReceiver.Text = this.PortalSettings.Email;
                    }
                    if ((string)base.TabModuleSettings["RI_MAIL_SUBJECT"] != null)
                    {
                        this.txtMailSubject.Text = (string)base.TabModuleSettings["RI_MAIL_SUBJECT"];
                    }
                    else
                    {
                        this.txtMailSubject.Text = "An issue reported : ##module## :  ##issuetype## ";
                    }
                    if ((string)base.TabModuleSettings["RI_MESSAGE_SENT"] != null)
                    {
                        this.txtMessageSent.Text = (string)base.TabModuleSettings["RI_MESSAGE_SENT"];
                    }
                    else
                    {
                        this.txtMessageSent.Text = "Dear Admin, " + System.Environment.NewLine + System.Environment.NewLine + " An issue has been reported, please find below details about the issue : ##data## " + System.Environment.NewLine + System.Environment.NewLine + "Thanks, " + System.Environment.NewLine + System.Environment.NewLine + "System Admin";
                    }

                    if ((string)base.TabModuleSettings["RI_MAIL_SENDER"] != null)
                    {
                        this.txtSender.Text = (string)base.TabModuleSettings["RI_MAIL_SENDER"];
                    }
                    else
                    {
                        this.txtSender.Text = this.PortalSettings.Email;
                    }
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        public override void UpdateSettings()
        {
            try
            {
                ModuleController moduleController = new ModuleController();
                moduleController.UpdateTabModuleSetting(base.TabModuleId, "RI_MAIL_RECEIVER", this.txtMailReceiver.Text);
                moduleController.UpdateTabModuleSetting(base.TabModuleId, "RI_MAIL_SUBJECT", this.txtMailSubject.Text);
                moduleController.UpdateTabModuleSetting(base.TabModuleId, "RI_MESSAGE_SENT", this.txtMessageSent.Text);
                moduleController.UpdateTabModuleSetting(base.TabModuleId, "RI_MAIL_SENDER", this.txtSender.Text);
                ModuleController.SynchronizeModule(base.ModuleId);
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
    }
}
