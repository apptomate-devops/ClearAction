/*
' Copyright (c) 2017 Ajit jha
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/
using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;

using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Profile;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Tokens;
using System.Web;
using System.Runtime.Serialization;

namespace ClearAction.Modules.Connections
{


    public class Member
    {
        private UserInfo _user;
        private UserInfo _viewer;
        private PortalSettings _settings;

        public Member(UserInfo user, PortalSettings settings)
        {
            _user = user;
            _settings = settings;
            _viewer = settings.UserInfo;
        }

        public int MemberId
        {
            get { return _user.UserID; }
        }

        public bool IsFavorite
        {
            get
            {
                return
                  new DbController().GetUserfavoriteRecord(_viewer.UserID, _user.UserID);
            }
        }
        //public bool HasPendingResponse
        //{
        //    get
        //    {

        //        if (FriendStatus == 1 && _viewer.UserID == FriendId)
        //            return true;
        //        return false;
        //    }
        //}

        public int OrderValue
        {
            get
            {

                if (IsFavorite)
                    return 4;
                if (HasPendingRequest)
                    return 3;
                if (FriendStatus == 2 && !IsFavorite)
                    return 2;
                if (FriendStatus == 1 && !HasPendingRequest)
                    return 1;


                return 0;
            }
        }
       
        public bool RoleSimilarToMine
        {
            get
            {
                var rName = HelperController.GetcurrentUserRoleName(_viewer.PortalID, _viewer.UserID);
                if (!string.IsNullOrEmpty(rName))
                    if (rName == RoleNameFilter)
                        return true;

                return false;
            }
        }

        public bool CompanySimilarToMine
        {
            get
            {
                var rName = HelperController.GetProfileResponse(Convert.ToInt32(PortalController.GetPortalSetting("CompanyName", 0, "4")), _viewer.UserID, true);
                if (!string.IsNullOrEmpty(rName))
                    if (rName == CompanyNameFilter)
                        return true;

                return false;
            }
        }
        public bool HasPendingRequest
        {
            get
            {
                return (_user.Social.Friend == null) ? false : (_viewer.UserID == FriendId);
            }
        }


        public string City
        {
            get { return GetProfileProperty("City"); }
        }

        public string Country
        {
            get { return GetProfileProperty("Country"); }
        }

        public DateTime CreatedonDate
        {
            get { return _user.Membership.CreatedDate; }
        }
        public string DisplayName
        {
            get { return _user.DisplayName; }
        }
        public string RoleName
        {


            get
            {
                var Compa = RoleNameFilter;
                if (Compa.Length > 50)
                    Compa = Compa.Substring(0, 9) + " ...";
                return Compa;
            }
            
        }

        private string RoleNameFilter
        {
            get
            {
                var rName = HelperController.GetcurrentUserRoleName(_user.PortalID, _user.UserID);
                return string.IsNullOrEmpty(rName) == true ? "" : rName;
            }
        }
        public string JobTitle
        {
            get
            {


                var rName = HelperController.GetProfileResponse(Convert.ToInt32(PortalController.GetPortalSetting("Title", 0, "15")), _user.UserID);
                return string.IsNullOrEmpty(rName) == true ? "" : rName;
            }
        }
        public string CompanyName
        {
            get
            {
                var Compa = CompanyNameFilter;
                if (Compa.Length > 50)
                    Compa = Compa.Substring(0, 49) + " ...";
                return Compa;
            }
        }

        private string CompanyNameFilter
        {
            get
            {
                var rName = HelperController.GetProfileResponse(Convert.ToInt32(PortalController.GetPortalSetting("CompanyName", 0, "4")), _user.UserID, true);
                return string.IsNullOrEmpty(rName) == true ? "" : rName;
            }
        }

     
        public string CompanyId
        {
            get
            {
                var rName = HelperController.GetProfileResponse(Convert.ToInt32(PortalController.GetPortalSetting("CompanyName", 0, "4")), _user.UserID);
                return string.IsNullOrEmpty(rName) == true ? "-1" : rName;
            }
        }


        public string Location
        {
            get
            {
                var rName = HelperController.GetProfileResponse(Convert.ToInt32(PortalController.GetPortalSetting("Location", 0, "5")), _user.UserID);
                return string.IsNullOrEmpty(rName) == true ? "" : rName;
            }
        }

        public string Phone
        {
            get
            {
                var rName = HelperController.GetProfileResponse(Convert.ToInt32(PortalController.GetPortalSetting("Phone", 0, "25")), _user.UserID);
                return string.IsNullOrEmpty(rName) == true ? "" : rName;
            }
        }
        public string Email
        {
            get { return _user.Email; }
        }

        public string FirstName
        {
            get { return GetProfileProperty("FirstName"); }
        }

        public int FollowerStatus
        {
            get { return (_user.Social.Follower == null) ? 0 : (int)_user.Social.Follower.Status; }
        }

        public int FollowingStatus
        {
            get { return (_user.Social.Following == null) ? 0 : (int)_user.Social.Following.Status; }
        }

        public int FriendId
        {
            get { return (_user.Social.Friend == null) ? -1 : (int)_user.Social.Friend.RelatedUserId; }
        }

        public int FriendStatus
        {
            get { return (_user.Social.Friend == null) ? 0 : (int)_user.Social.Friend.Status; }
        }


        public string LastName
        {
            get { return _user.LastName; }
        }



        public string PhotoURL
        {
            get { return _user.Profile.PhotoURL; }
        }

        public Dictionary<string, string> ProfileProperties
        {
            get
            {
                var properties = new Dictionary<string, string>();
                bool propertyNotFound = false;
                var propertyAccess = new ProfilePropertyAccess(_user);
                try
                {
                    foreach (ProfilePropertyDefinition property in _user.Profile.ProfileProperties)
                    {
                        string value = propertyAccess.GetProperty(property.PropertyName,
                                                                 String.Empty,
                                                                 Thread.CurrentThread.CurrentUICulture,
                                                                 _viewer,
                                                                 Scope.DefaultSettings,
                                                                 ref propertyNotFound);

                        properties[property.PropertyName] = string.IsNullOrEmpty(value) ? "" : DotNetNuke.Common.Utilities.HtmlUtils.Clean(HttpUtility.HtmlDecode(value), false);
                    }
                }
                catch (Exception ec)
                {

                }
                return properties;
            }
        }

        public string Title
        {
            get { return _user.Profile.Title; }
        }

        public string UserName
        {
            get { return _user.Username; }
        }

        public string Website
        {
            get { return GetProfileProperty("Website"); }
        }

        public string ProfileUrl
        {
            get { return Globals.UserProfileURL(MemberId); }
        }

        /// <summary>
        /// This method returns the value of the ProfileProperty if is defined, otherwise it returns an Empty string
        /// </summary>
        /// <param name="propertyName">property name</param>
        /// <returns>property value</returns>
        private string GetProfileProperty(string propertyName)
        {
            try
            {
                var profileProperties = ProfileProperties;
                string value;

                return profileProperties.TryGetValue(propertyName, out value) ? value : string.Empty;
            }
            catch (Exception ec)
            {


            }
            return "";
        }
    }
}
