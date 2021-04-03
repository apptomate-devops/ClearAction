using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.UI.UserControls;
using System;
using System.Web.UI.WebControls;

namespace DnnEagles.jQueryFeedback.DesktopModules.DnnEagles.jQueryFeedback
{
	public class Settings : ModuleSettingsBase
	{
		protected LabelControl lblMailReceiver;

		protected TextBox txtMailReceiver;

		protected LabelControl lblMailSubject;

		protected TextBox txtMailSubject;

		protected LabelControl lblMessageSent;

		protected TextBox txtMessageSent;

		protected LabelControl lblMessageNotSent;

		protected TextBox txtMessageNotSent;

		protected LabelControl lblDisclaimer;

		protected TextBox txtDisclaimer;

		protected LabelControl lblHideOnSubmit;

		protected DropDownList ddlHide;

		protected LabelControl lblNotifySender;

		protected CheckBox chkNotify;

		protected LabelControl lblSubject;

		protected TextBox txtSubject;

		protected LabelControl lblMailToSender;

		protected TextBox txtMailToSender;

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		public override void LoadSettings()
		{
			try
			{
				if (!this.Page.IsPostBack)
				{
					if ((string)base.TabModuleSettings["MAIL_RECEIVER"] != null)
					{
						this.txtMailReceiver.Text = (string)base.TabModuleSettings["MAIL_RECEIVER"];
					}
					if ((string)base.TabModuleSettings["MAIL_SUBJECT"] != null)
					{
						this.txtMailSubject.Text = (string)base.TabModuleSettings["MAIL_SUBJECT"];
					}
					if ((string)base.TabModuleSettings["MESSAGE_SENT"] != null)
					{
						this.txtMessageSent.Text = (string)base.TabModuleSettings["MESSAGE_SENT"];
					}
					if ((string)base.TabModuleSettings["MESSAGE_NOT_SENT"] != null)
					{
						this.txtMessageNotSent.Text = (string)base.TabModuleSettings["MESSAGE_NOT_SENT"];
					}
					if ((string)base.TabModuleSettings["DISCLAIMER"] != null)
					{
						this.txtDisclaimer.Text = (string)base.TabModuleSettings["DISCLAIMER"];
					}
					if ((string)base.TabModuleSettings["HIDE_ON_SUBMIT"] != null)
					{
						this.ddlHide.SelectedValue = (string)base.TabModuleSettings["HIDE_ON_SUBMIT"];
					}
					if ((string)base.TabModuleSettings["NOTIFY_SENDER"] != null)
					{
						this.chkNotify.Checked = Convert.ToBoolean(base.TabModuleSettings["NOTIFY_SENDER"]);
					}
					if ((string)base.TabModuleSettings["MAIL_TO_SENDER"] != null)
					{
						this.txtMailToSender.Text = (string)base.TabModuleSettings["MAIL_TO_SENDER"];
					}
					if ((string)base.TabModuleSettings["SUBJECT_NOTIFY"] != null)
					{
						this.txtSubject.Text = (string)base.TabModuleSettings["SUBJECT_NOTIFY"];
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
				moduleController.UpdateTabModuleSetting(base.TabModuleId, "MAIL_RECEIVER", this.txtMailReceiver.Text);
				moduleController.UpdateTabModuleSetting(base.TabModuleId, "MAIL_SUBJECT", this.txtMailSubject.Text);
				moduleController.UpdateTabModuleSetting(base.TabModuleId, "MESSAGE_SENT", this.txtMessageSent.Text);
				moduleController.UpdateTabModuleSetting(base.TabModuleId, "MESSAGE_NOT_SENT", this.txtMessageNotSent.Text);
				moduleController.UpdateTabModuleSetting(base.TabModuleId, "DISCLAIMER", this.txtDisclaimer.Text);
				moduleController.UpdateTabModuleSetting(base.TabModuleId, "HIDE_ON_SUBMIT", this.ddlHide.SelectedValue);
				moduleController.UpdateTabModuleSetting(base.TabModuleId, "NOTIFY_SENDER", this.chkNotify.Checked.ToString());
				moduleController.UpdateTabModuleSetting(base.TabModuleId, "MAIL_TO_SENDER", this.txtMailToSender.Text);
				moduleController.UpdateTabModuleSetting(base.TabModuleId, "SUBJECT_NOTIFY", this.txtSubject.Text);
				ModuleController.SynchronizeModule(base.ModuleId);
			}
			catch (Exception exc)
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}
	}
}
