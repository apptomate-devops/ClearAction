using DotNetNuke.Services.Mail;
using System;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace ClearAction.Modules.ReportIssue
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

            string toaddress = "";
            string lnkName = "";
            string modtype = "";

            if (context.Request.Form["type"] != null)
            {
                modtype = context.Request.Form["type"];
            }

            if (context.Request.Form["linkname"] != null)
            {
                lnkName = context.Request.Form["linkname"];
            }
            string reportMailer = "";
            if (context.Request.Form["email"] != null)
            {
                reportMailer = context.Request.Form["email"];
            }
            string message = "";
            if (context.Request.Form["message"] != null)
            {
                message = context.Request.Form["message"];
            }
            string subject = "Reporting an issue for ##module## ##title##";
            string fromAddress = "";
            if (context.Session["RI_SENDER"] != null)
            {
                fromAddress = (string)context.Session["RI_SENDER"];
            }
            if (context.Session["RI_RECEIVER"] != null)
            {
                toaddress = (string)context.Session["RI_RECEIVER"];
            }

            if (context.Session["RI_SUBJECT"] != null)
            {
                subject = (string)context.Session["RI_SUBJECT"];
            }

            string mailBody = "";
            if (context.Session["RI_MESSAGE"] != null)
            {
                mailBody = (string)context.Session["RI_MESSAGE"];
            }
            
            string locURL = "";
            if (context.Request.Form["locURL"] != null)
            {
                locURL = context.Request.Form["locURL"];
            }

            string title = "";
            if (context.Request.Form["title"] != null)
            {
                title = context.Request.Form["title"];
            }

            string issueType = "";
            if (context.Request.Form["rpttype"] != null)
            {
                issueType = context.Request.Form["rpttype"];
            }


            try
            {
                StringBuilder sbBody = new StringBuilder();

                sbBody.Append(Environment.NewLine + Environment.NewLine + " Sender E-mail : " + reportMailer);
                sbBody.Append(Environment.NewLine + Environment.NewLine + " Link : " + lnkName);
                sbBody.Append(Environment.NewLine + Environment.NewLine + " Message : " + message);

                sbBody.Append(Environment.NewLine + Environment.NewLine + " Module : " + modtype);
                sbBody.Append(Environment.NewLine + Environment.NewLine + " Title : " + title);
                sbBody.Append(Environment.NewLine + Environment.NewLine + " Browser : " + browserName);
                sbBody.Append(Environment.NewLine + Environment.NewLine + " Page : " + locURL);

                //##data##

                mailBody = mailBody.Replace("##data##", sbBody.ToString());

                subject = subject.Replace("##module##", modtype);
                subject = subject.Replace("##issuetype##", issueType);

                Mail.SendEmail(fromAddress,toaddress, subject, mailBody);
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
