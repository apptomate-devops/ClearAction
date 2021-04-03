using DotNetNuke.Services.Mail;
using System;
using System.Web;
using System.Web.SessionState;

namespace DnnEagles.jQueryFeedback.DesktopModules.DnnEagles.jQueryFeedback
{
    public class Mailer : IHttpHandler, IRequiresSessionState
    {
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {

            //By Kusum  29-11-17
            System.Web.HttpBrowserCapabilities browser = context.Request.Browser;

            string browserName = browser.Browser;
            string url = context.Request.Url.ToString();



            string text = (string)context.Session["SENDER"];
            string newValue = "";
            if (context.Request.Form["name"] != null)
            {
                newValue = context.Request.Form["name"];
            }
            string text2 = "";
            if (context.Request.Form["email"] != null)
            {
                text2 = context.Request.Form["email"];
            }
            string text3 = "";
            if (context.Request.Form["message"] != null)
            {
                text3 = context.Request.Form["message"];
            }
            string subject = "";
            if (context.Request.Form["subject"] != null)
            {
                subject = context.Request.Form["subject"];
            }

            string locURL = "";
            if (context.Request.Form["locURL"] != null)
            {
                locURL = context.Request.Form["locURL"];
            }


            bool flag = Convert.ToBoolean(context.Session["NOTIFY"]);
            string text4 = (string)context.Session["MAIL_SENDER"];
            string subject2 = (string)context.Session["SUBJECT_NOTIFY"];
            try
            {
                Mail.SendEmail(text2, text, subject, text3 + Environment.NewLine + Environment.NewLine + " Browser : " + browserName + Environment.NewLine + Environment.NewLine + " Page: " + locURL);
                if (flag)
                {
                    text4 = text4.Replace("{#MESSAGE_SENDER#}", text3).Replace("{#NAME_SENDER#}", newValue);
                    Mail.SendEmail(text, text2, subject2, text4);
                }
                context.Response.ContentType = "text/plain";
                context.Response.Write("success");
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(ex.Message);
            }
        }
    }
}
