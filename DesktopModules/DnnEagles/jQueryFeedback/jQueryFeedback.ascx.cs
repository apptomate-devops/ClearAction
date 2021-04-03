using DotNetNuke.Entities.Modules;
using System;
using System.Web.UI.WebControls;

using DotNetNuke.Entities.Tabs;

namespace DnnEagles.jQueryFeedback.DesktopModules.DnnEagles.jQueryFeedback
{
	public class jQueryFeedback : PortalModuleBase
	{
		protected Literal ltrlScript;
        

		protected void Page_Load(object sender, EventArgs e)
		{

            try {


            
			if (base.Settings["MAIL_RECEIVER"] != null)
			{
				if (base.Settings["MAIL_RECEIVER"].ToString() != "")
				{
					base.Session["SENDER"] = base.Settings["MAIL_RECEIVER"].ToString();
					base.Session["NOTIFY"] = base.Settings["NOTIFY_SENDER"].ToString();
					base.Session["MAIL_SENDER"] = base.Settings["MAIL_TO_SENDER"].ToString();
					base.Session["SUBJECT_NOTIFY"] = base.Settings["SUBJECT_NOTIFY"].ToString();
					this.loadHeader();
					string text = ", subject: 'feedback URL:' + location.href";
					if (base.Settings["MAIL_SUBJECT"].ToString() != "")
					{
						text = ", subject: '" + base.Settings["MAIL_SUBJECT"].ToString() + "'";
					}
					string text2 = "";
					if (base.Settings["MESSAGE_SENT"].ToString() != "")
					{
						text2 = ",recievedMsg: '" + base.Settings["MESSAGE_SENT"].ToString().Replace("'", "\\'") + "'";
					}
					string text3 = "";
					if (base.Settings["MESSAGE_NOT_SENT"].ToString() != "")
					{
						text3 = ",notRecievedMsg: '" + base.Settings["MESSAGE_NOT_SENT"].ToString().Replace("'", "\\'") + "'";
					}
					string text4 = "";
					if (base.Settings["DISCLAIMER"].ToString() != "")
					{
						text4 = ",disclaimer: '" + base.Settings["DISCLAIMER"].ToString().Replace("'", "\\'") + "'";
					}
					string text5 = "";
					if (base.Settings["HIDE_ON_SUBMIT"].ToString() != "")
					{
						text5 = ",hideOnSubmit: " + base.Settings["HIDE_ON_SUBMIT"].ToString();
					}
					this.ltrlScript.Text = string.Concat(new string[]
					{
						"<script type=\"text/javascript\">jQuery(function () {jQuery('#contactable').contactable({url: '",
						base.ControlPath,
						"Mailer.ashx' ",
						text,
						"  ",
						text2,
						"  ",
						text3,
						"  ",
						text4,
						" ",
						text5,
						" });});</script>"
					});
				}
			}
            }
            catch (Exception ex)
            {

            }
        }

		public void loadHeader()
		{
			if (this.Page.Header.FindControl("ComponentiScriptFeedback") == null)
			{
				Literal literal = new Literal();
				literal.ID = "ComponentiScriptFeedback";
				string text = "<script type=\"text/javascript\" src=\"" + base.ControlPath + "js/jquery.validate.pack.js\"></script>";
				if (base.PortalId > 0)
				{
					text = text + "<script type=\"text/javascript\" src=\"" + base.ControlPath + "js/jquery.contactable2.js\"></script>";
				}
				else
				{
					text = text + "<script type=\"text/javascript\" src=\"" + base.ControlPath + "js/jquery.contactable.js\"></script>";
				}
				text = text + "<link rel=\"stylesheet\" href=\"" + base.ControlPath + "css/contactable.css\" type=\"text/css\" />";
				literal.Text = text;
				this.Page.Header.Controls.Add(literal);
			}

                        
		}

    }
}
