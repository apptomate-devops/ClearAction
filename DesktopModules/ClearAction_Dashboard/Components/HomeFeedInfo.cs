using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace ClearAction.Modules.Dashboard.Components
{

    public class HomeFeed
    {
        public int WCCId { get; set; }

        public bool HasSeen { get; set; }

        public bool IsAssigned { get; set; }

        public string title { get; set; }

        public string link { get; set; }

        public string description { get; set; }

        public string pubDate { get; set; }


        public string lastBuildDate { get; set; }

        public string generator { get; set; }
        public string ttl { get; set; }

        public string atom { get; set; }

        public string shortdescription
        {
            get
            {
                if (description.Length > 100)
                    return description.Substring(0, 100);
                return description;
            }
        }
        public string creator { get; set; }
        public int author { get; set; }

        DotNetNuke.Entities.Users.UserInfo _u;
        public DotNetNuke.Entities.Users.UserInfo GetUSer
        {
            get
            {
                if (_u == null)
                    _u = DotNetNuke.Entities.Users.UserController.GetUserById(Utilies.CurrentPortalid, author);
                return _u;
            }
        }
        public string creatorpic
        {
            get
            {
                if (author > 0)
                {
                    var oUser = GetUSer;
                    if (oUser != null)
                        return oUser.Profile.PhotoURL;


                }
                return "";
            }

        }

        public string CreatorDisplayName
        {
            get
            {
                if (author > 0)
                {
                    var oUser = GetUSer;
                    if (oUser != null)
                        return oUser.DisplayName;


                }
                return "";
            }

        }

        public string CreaterDisplayNameHref
        {
            get
            {
                var oUser = GetUSer;
                if (oUser != null)
                    return string.Format("<a href='/MyProfile/UserName/{0}'>{1}</a>", oUser.Username, oUser.DisplayName);
                return "";
            }

        }

        public string CreaterHref
        {
            get
            {
                var oUser = GetUSer;
                if (oUser != null)
                    return string.Format("/MyProfile/UserName/{0}", oUser.Username);
                return "";
            }

        }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }

        public string AuthorName { get; set; }
        public string StatusImage { get; set; }
        public string UserImage { get; set; }
        public string StatusText { get; set; }

        public string EventDateTime { get; set; }

        public DateTime EventDate { get; set; }
        public DateTime EventTime { get; set; }
        public string WebApiKey { get; set; }
        public bool ISVISIBLEREGiSTER
        {
            get
            {
                if (EventDate.Ticks > System.DateTime.Now.Ticks)
                    return true;
                return false;
            }
        }
        public string EventDateTimeCST { get; set; }

        public string RegistrationLink { get; set; }
        public string ExpiredviewLink { get; set; }



    }
}