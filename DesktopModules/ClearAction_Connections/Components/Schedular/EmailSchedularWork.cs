using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Services.Scheduling;
using DotNetNuke.Services.Exceptions;

namespace ClearAction.Modules.Connections
{
    public class EmailSchedularWork : SchedulerClient
    {
        public EmailSchedularWork(ScheduleHistoryItem objScheduleHistoryItem)
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
                if (lEmailSchedular != null)
                {
                    foreach (EmailScheduler oemailschedualar in lEmailSchedular)
                    {
                        DotNetNuke.Services.Mail.Mail.SendMail(oemailschedualar.SenderEmail, oemailschedualar.ReceiverEmail, "", oemailschedualar.Subject, oemailschedualar.Body, "", "HTML", "", "", "", "");
                        oemailschedualar.IsSent = true;
                        oemailschedualar.SentOnDate = System.DateTime.Now;
                        Controller.UpdateEmailSchedular(oemailschedualar);
                        iCounter++;
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
    }
}