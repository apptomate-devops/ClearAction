using LogMeIn.GoToCoreLib.Api;
using LogMeIn.GoToWebinar.Api;
using LogMeIn.GoToWebinar.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using DotNetNuke.Web.Api;
using ClearAction.Modules.WebCast.Components;
using ClearAction.Modules.WebCast.Components.Entity;
using System.Configuration;

namespace GotoMeetingAPISchedular
{


    public class GotoMeetingAPISchedular : DotNetNuke.Services.Scheduling.SchedulerClient
    {

        public GotoMeetingAPISchedular(DotNetNuke.Services.Scheduling.ScheduleHistoryItem objScheduleHistoryItem)
            : base()
        {
            this.ScheduleHistoryItem = objScheduleHistoryItem;
        }
        public override void DoWork()
        {
            try
            {
                this.Progressing();
                string strMessage = Processing();
                this.ScheduleHistoryItem.Succeeded = true;
                this.ScheduleHistoryItem.AddLogNote("GotoMeetingAPISchedular_ Succeeded");

            }
            catch (Exception exc)
            {
                this.ScheduleHistoryItem.Succeeded = false;
                this.ScheduleHistoryItem.AddLogNote("GotoMeetingAPISchedular_ Failed");
                this.Errored(ref exc);
            }
        }
        public string Processing()
        {
            try
            {
                GoToWebInnerAPICall();

            }
            catch
            {

            }
            string Message = "";
            return Message;
        }





        public void GoToWebInnerAPICall()
        {
            try
            {
                
                string userName = ConfigurationManager.AppSettings["GotoWebinarLoginEmailAddress"];
                string userPassword = ConfigurationManager.AppSettings["GotoWebinarLoginPassword"];
                string consumerKey = ConfigurationManager.AppSettings["GotoWebinarConsumerKey"];
                
                var authApi = new AuthenticationApi();
                var response = authApi.directLogin(userName, userPassword, consumerKey, "password");

                var accessToken = response.access_token;
                long OrganizerKey;

                long.TryParse(response.organizer_key, out OrganizerKey);

                WebinarsApi objWebinarsApi = new WebinarsApi();
                RegistrantsApi objRegistrantsApi = new RegistrantsApi();

                List<Webinar> objWebinarList = new List<Webinar>();
                try
                {
                    objWebinarList = objWebinarsApi.getAllWebinars(accessToken, OrganizerKey);
                }
                catch (Exception ex)
                {
                    objWebinarList = new List<Webinar>();
                }


                var dal2 = new ClearAction.Modules.WebCast.Components.DBController();
                IQueryable<Webinar_User_RegistrationDetail> objWebinar_User_RegistrationDetailList = dal2.GetWebinarUserRegistrationDetails();

                List<long> WebinarKeyListForCheck = new List<long>();

                foreach (Webinar_User_RegistrationDetail item in objWebinar_User_RegistrationDetailList)
                {
                    long longCheckWebInarKey = 0;
                    long.TryParse(item.WebinarKey, out longCheckWebInarKey);
                    if (longCheckWebInarKey != 0)
                    {
                        if (objWebinarList.Where(s => s.webinarKey == longCheckWebInarKey).Any())
                        {
                            if (!WebinarKeyListForCheck.Where(s => s == longCheckWebInarKey).Any())
                            {
                                WebinarKeyListForCheck.Add(longCheckWebInarKey);
                            }
                        }
                    }
                }


                foreach (long webiKeyItem in WebinarKeyListForCheck)
                {
                    List<Attendee> objAttendeeList = new List<Attendee>();
                    try
                    {
                        objAttendeeList = objWebinarsApi.getAttendeesForAllWebinarSessions(accessToken, OrganizerKey, webiKeyItem);

                        foreach (Webinar_User_RegistrationDetail itemDetail in objWebinar_User_RegistrationDetailList.Where(s => s.WebinarKey == webiKeyItem.ToString()))
                        {
                            long longRegistrantKey = 0;
                            long.TryParse(itemDetail.UserWebinarRegistrationKey, out longRegistrantKey);

                            if (objAttendeeList.Where(s => s.registrantKey == longRegistrantKey).Any())
                            {
                                //Update Status in Clear Action Database.
                                bool status = dal2.UpdateWebinarUserAttendanceStatus(itemDetail.UserId, itemDetail.WCCId, itemDetail.ComponentId, itemDetail.WebinarKey, itemDetail.UserWebinarRegistrationKey);
                            }

                        }

                    }
                    catch (Exception ex) { }
                }



            }
            catch (Exception ex)
            { }
        }

    }
}