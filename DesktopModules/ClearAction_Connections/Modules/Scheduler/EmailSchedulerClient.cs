using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Services.Scheduling;
using DotNetNuke.Services.Exceptions;
using ClearAction.Modules.Scheduler.Components;
using ClearAction.Modules.Scheduler.Entity;
namespace ClearAction.Modules.Scheduler.Components
{
    public class EmailSchedulerClient : SchedulerClient
    {
        public EmailSchedulerClient(ScheduleHistoryItem objScheduleHistoryItem)
            : base()
        {
            ScheduleHistoryItem = objScheduleHistoryItem;
        }

        public override void DoWork()
        {
            try
            {

                ScheduleHistoryItem.AddLogNote(string.Format("<br/>Sending Email Batch Start at :{0}", System.DateTime.Now.ToShortTimeString()));

                var Controller = new DbController();
                var lEmailSchedular = Controller.GetUnSendEmail();
                int iCounter = 0;
                int Adminid = new DotNetNuke.Entities.Portals.PortalController().GetPortal(0).AdministratorId;
                var uEmail = new DotNetNuke.Entities.Users.UserController().GetUser(0, Adminid);
                string toEmailAddress = PortalEmailAddress;
                if (uEmail == null)
                {
                    toEmailAddress = uEmail.Email;
                }
                if (lEmailSchedular != null)
                {
                    foreach (EmailScheduler oemailschedualar in lEmailSchedular)
                    {

                        try
                        {

                            DotNetNuke.Services.Mail.Mail.SendMail(toEmailAddress, oemailschedualar.ReceiverEmail, "", "", oemailschedualar.SenderEmail, DotNetNuke.Services.Mail.MailPriority.High, oemailschedualar.Subject, DotNetNuke.Services.Mail.MailFormat.Html, System.Text.Encoding.UTF32, oemailschedualar.Body, new string[0], "", "", "", "", true);
                            oemailschedualar.IsSent = true;
                            oemailschedualar.SentOnDate = System.DateTime.Now;
                            Controller.UpdateEmailSchedular(oemailschedualar);
                            iCounter++;
                        }
                        catch (Exception ex)
                        {


                        }

                    }
                    ScheduleHistoryItem.AddLogNote(string.Format("<br/>{0} Email has been sent in this batch. job finished at {1}", iCounter++, System.DateTime.Now.ToShortDateString()));

                }
                else
                    ScheduleHistoryItem.AddLogNote(string.Format("No Email found in this batch"));
                ScheduleHistoryItem.Succeeded = true;

            }

            catch (Exception exception1)
            {
                Exception exception = exception1;
                ScheduleHistoryItem.Succeeded = false;
                ScheduleHistoryItem.AddLogNote(string.Concat("EXCEPTION: ", exception.ToString()));
                base.Errored(ref exception);
                Exceptions.LogException(exception);
            }
        }



        public string PortalEmailAddress
        {
            get
            {
                var oEmail = System.Configuration.ConfigurationManager.AppSettings["NotificationEmail"].ToString();
                if (oEmail != null)
                    return oEmail;
                return "members@clearaction.com";
            }
        }
    }
}