using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using DotNetNuke.Services.Scheduling;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Users;
namespace ClearAction_AutoSchdularTags
{
    public class ScheduledTask : SchedulerClient
    {

        public ScheduledTask(ScheduleHistoryItem objScheduleHistoryItem)
            : base()
        {
            ScheduleHistoryItem = objScheduleHistoryItem;
        }

        public override void DoWork()
        {
            try
            {

                ScheduleHistoryItem.AddLogNote(string.Format("Auto Assign Task has been started : <br/>"));

                int iCurrentPortal = DotNetNuke.Entities.Portals.PortalController.Instance.GetCurrentPortalSettings() != null ? DotNetNuke.Entities.Portals.PortalController.Instance.GetCurrentPortalSettings().PortalId : 0;

                var oUserList = DotNetNuke.Entities.Users.UserController.GetUsers(iCurrentPortal);
                foreach(UserInfo oUser in oUserList)
                {
                    ScheduleHistoryItem.AddLogNote(string.Format("Assigned to user - {0}[{1}] <br/>", oUser.DisplayName, new ClearAction_AutoSchdularTags.Controller().UpdatePerference(oUser.UserID)));

                    
                }
                ScheduleHistoryItem.AddLogNote(string.Format("Auto Tag Finshed"));

                ScheduleHistoryItem.Succeeded = true;
                ScheduleHistoryItem.NextStart = System.DateTime.Now.AddDays(1);
              

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


