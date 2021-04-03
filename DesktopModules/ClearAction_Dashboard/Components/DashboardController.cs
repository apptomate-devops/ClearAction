/*
' Copyright (c) 2017 ClearAction
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using DotNetNuke.Entities.Users;
using DotNetNuke.Modules.ActiveForums;
using System;

namespace ClearAction.Modules.Dashboard.Components
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for ClearAction_Dashboard
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class DashboardController//ModuleSearchBase, IPortable, IUpgradeable
    {





        public List<HomeFeed> GetBlogFeed(string feedUrl, int TopN, int UserID)
        {
            List<HomeFeed> feeds = new List<HomeFeed>();
            var oData = new DBController().GetAllBlog(UserID, TopN);
            if (oData != null)
            {
                string imgStats = "";
                foreach (CA_BlogPosts o in oData)
                {
                    if (o.Status == "Completed")
                    {
                        imgStats = "/portals/0/img_17.png";
                    }
                    else
                    {
                        imgStats = "/portals/0/img_18.png";
                    }

                    var ohomefeed = new HomeFeed()
                    {
                        HasSeen = o.HasSeen,
                        IsAssigned = o.IsAssigned,
                        atom = "",
                        author = -1,
                        creator = "",
                        description = ((o.ShortDescription == null) ? "" : System.Web.HttpUtility.HtmlDecode(o.ShortDescription)),
                        generator = "",
                        lastBuildDate = Utilies.DisplayCulutureDateTime(o.PublishedOnDate),
                        link = string.Format("/Insights/ContentItemId/{0}/{1}", o.ContentItemId.ToString(), System.Text.RegularExpressions.Regex.Replace(o.Title, "[^a-zA-Z0-9]", " ").Replace(" ", "-") + "?UpdateStatus=True"),
                        pubDate = Utilies.DisplayCulutureDateTime(o.PublishedOnDate),
                        title = o.Title,
                        ttl = "",
                        StatusImage = imgStats
                    };
                    feeds.Add(ohomefeed);
                }
            }
            return feeds;
        }
        public List<HomeFeed> GetBlogFeedSearch(string feedUrl, int TopN, int UserID, string search)
        {
            List<HomeFeed> feeds = new List<HomeFeed>();
            var oData = new DBController().GetAllBlogSearch(UserID, TopN, search);
            if (oData != null)
            {
                string imgStats = "";
                foreach (CA_BlogPosts o in oData)
                {
                    if (o.Status == "Completed")
                    {
                        imgStats = "/portals/0/img_17.png";
                    }
                    else
                    {
                        imgStats = "/portals/0/img_18.png";
                    }

                    var ohomefeed = new HomeFeed()
                    {
                        HasSeen = o.HasSeen,
                        IsAssigned = o.IsAssigned,
                        atom = "",
                        author = -1,
                        creator = "",
                        description = ((o.ShortDescription == null) ? "" : System.Web.HttpUtility.HtmlDecode(o.ShortDescription)),
                        generator = "",
                        lastBuildDate = Utilies.DisplayCulutureDateTime(o.PublishedOnDate),
                        link = string.Format("/Insights/ContentItemId/{0}/{1}", o.ContentItemId.ToString(), System.Text.RegularExpressions.Regex.Replace(o.Title, "[^a-zA-Z0-9]", " ").Replace(" ", "-") + "?UpdateStatus=True"),
                        pubDate = Utilies.DisplayCulutureDateTime(o.PublishedOnDate),
                        title = o.Title,
                        ttl = "",
                        StatusImage = imgStats
                    };
                    feeds.Add(ohomefeed);
                }
            }
            return feeds;
        }

        public List<HomeFeed> GetDigitalEvent(int iComponenetId, int TopN, int UserID)
        {
            List<HomeFeed> feeds = new List<HomeFeed>();
            var oData = new DBController().GetDigitalEvent(iComponenetId, UserID, TopN);
            DotNetNuke.Entities.Users.UserController oCtrl = new DotNetNuke.Entities.Users.UserController();
            if (oData != null)
            {
                string imgStats = "";
                string _UserName = "";
                string _UserImage = "/images/no_avatar.gif";
                string strStatusText = "";
                foreach (CA_DigitalEvents o in oData)
                {
                    if (o.Status == "Completed")
                    {
                        imgStats = "/portals/0/img_17.png";
                        strStatusText = "Completed :: ";
                    }
                    else
                    {
                        imgStats = "/portals/0/img_18.png";
                        strStatusText = "To Do :: ";
                    }
                    UserInfo oUserInfo = oCtrl.GetUser(0, o.PresenterID);
                    if (oUserInfo != null)
                    {
                        _UserImage = oUserInfo.Profile.PhotoURL;
                        _UserName = oUserInfo.FirstName + " " + oUserInfo.LastName;

                    }

                    var ohomefeed = new HomeFeed()
                    {
                        WCCId = o.WCCId,
                        HasSeen = o.HasSeen,
                        IsAssigned = o.IsAssigned,
                        atom = "",
                        author = o.PresenterID,
                        creator = "",
                        description = ((o.ShortDescription == null) ? "" : System.Web.HttpUtility.HtmlDecode(o.ShortDescription)),
                        generator = "",
                        lastBuildDate = Utilies.DisplayCulutureDateTime(o.EventDate),
                        link = string.Format("/Digital/WccPostKey/{0}/{1}", o.WCCId.ToString(), System.Text.RegularExpressions.Regex.Replace(o.Title, "[^a-zA-Z0-9]", " ").Replace(" ", "-") + "?UpdateStatus=True"),
                        pubDate = Utilies.DisplayCulutureDateTime(o.CreatedOnDate),
                        title = o.Title,
                        ttl = "",
                        StatusImage = imgStats,
                        EventDateTime = Utilies.DisplayCulutureDateTime(o.EventDate) + ", " + Utilies.DisplayCulutureTime(o.EventTime),

                        //Chnages done by @SP
                        //EventDateTimeCST = Utilies.DisplayCulutureDateTime(o.EventDate, true) + ", " + Utilies.DisplayCulutureTime(o.EventTime) + " CST",
                        EventDateTimeCST = o.EventDate > DateTime.Now ? Utilies.DisplayCulutureDateTime(o.EventDate, true) + ", " + Utilies.DisplayCulutureTime(o.EventTime) + " ET (i.e. NYC)" : "",

                        AuthorName = o.PresenterName,
                        EventDate = o.EventDate,
                        EventTime = o.EventTime,
                        ExpiredviewLink = o.ExpiredviewLink,
                        RegistrationLink = o.RegistrationLink,
                        WebApiKey = o.WebApiKey,
                        StatusText = strStatusText



                    };






                    feeds.Add(ohomefeed);
                }
            }
            return feeds;
        }
        public List<HomeFeed> GetDigitalEventSearch(int iComponenetId, int TopN, int UserID, string search)
        {
            List<HomeFeed> feeds = new List<HomeFeed>();
            var oData = new DBController().GetDigitalEventSearch(iComponenetId, UserID, TopN, search);
            DotNetNuke.Entities.Users.UserController oCtrl = new DotNetNuke.Entities.Users.UserController();
            if (oData != null)
            {
                string imgStats = "";
                string _UserName = "";
                string _UserImage = "/images/no_avatar.gif";
                string strStatusText = "";
                foreach (CA_DigitalEvents o in oData)
                {
                    if (o.Status == "Completed")
                    {
                        imgStats = "/portals/0/img_17.png";
                        strStatusText = "Completed :: ";
                    }
                    else
                    {
                        imgStats = "/portals/0/img_18.png";
                        strStatusText = "To Do :: ";
                    }
                    UserInfo oUserInfo = oCtrl.GetUser(0, o.PresenterID);
                    if (oUserInfo != null)
                    {
                        _UserImage = oUserInfo.Profile.PhotoURL;
                        _UserName = oUserInfo.FirstName + " " + oUserInfo.LastName;

                    }

                    var ohomefeed = new HomeFeed()
                    {
                        WCCId = o.WCCId,
                        HasSeen = o.HasSeen,
                        IsAssigned = o.IsAssigned,
                        atom = "",
                        author = o.PresenterID,
                        creator = "",
                        description = ((o.ShortDescription == null) ? "" : System.Web.HttpUtility.HtmlDecode(o.ShortDescription)),
                        generator = "",
                        lastBuildDate = Utilies.DisplayCulutureDateTime(o.EventDate),
                        link = string.Format("/Digital/WccPostKey/{0}/{1}", o.WCCId.ToString(), System.Text.RegularExpressions.Regex.Replace(o.Title, "[^a-zA-Z0-9]", " ").Replace(" ", "-") + "?UpdateStatus=True"),
                        pubDate = Utilies.DisplayCulutureDateTime(o.CreatedOnDate),
                        title = o.Title,
                        ttl = "",
                        StatusImage = imgStats,
                        EventDateTime = Utilies.DisplayCulutureDateTime(o.EventDate) + ", " + Utilies.DisplayCulutureTime(o.EventTime),

                        //Chnages done by @SP
                        //EventDateTimeCST = Utilies.DisplayCulutureDateTime(o.EventDate, true) + ", " + Utilies.DisplayCulutureTime(o.EventTime) + " CST",
                        EventDateTimeCST = o.EventDate > DateTime.Now ? Utilies.DisplayCulutureDateTime(o.EventDate, true) + ", " + Utilies.DisplayCulutureTime(o.EventTime) + " ET (i.e. NYC)" : "",

                        AuthorName = o.PresenterName,
                        EventDate = o.EventDate,
                        EventTime = o.EventTime,
                        ExpiredviewLink = o.ExpiredviewLink,
                        RegistrationLink = o.RegistrationLink,
                        WebApiKey = o.WebApiKey,
                        StatusText = strStatusText



                    };






                    feeds.Add(ohomefeed);
                }
            }
            return feeds;
        }
        public List<HomeFeed> GetForumFeed(string feedUrl, int TopN, int UserID)
        {
            List<HomeFeed> feeds = new List<HomeFeed>();
            List<CA_Topics> oData = new DBController().GetForums(UserID, TopN);
            if (oData != null)
            {
                DotNetNuke.Entities.Users.UserController oCtrl = new DotNetNuke.Entities.Users.UserController();
                foreach (CA_Topics o in oData)
                {
                    string _UserImage = "/images/no_avatar.gif";
                    string imgStats = "";
                    string _UserName = "";
                    string strStatusText = "";
                    if (o.Status == "Completed")
                    {
                        imgStats = "/portals/0/img_17.png";
                        strStatusText = "Completed :: ";
                    }
                    else
                    {
                        imgStats = "/portals/0/img_18.png";
                        strStatusText = "To Do :: ";
                    }
                    UserInfo oUserInfo = oCtrl.GetUser(0, o.AuthorId);
                    if (oUserInfo != null)
                    {
                        _UserImage = oUserInfo.Profile.PhotoURL;
                        _UserName = oUserInfo.FirstName + " " + oUserInfo.LastName;

                    }

                    var ohomefeed = new HomeFeed()
                    {
                        atom = "",
                        author = o.AuthorId,
                        creator = "",
                        description = System.Web.HttpUtility.HtmlDecode(o.GetContent.Body),
                        generator = "",
                        lastBuildDate = Utilies.DisplayCulutureDateTime(o.GetContent.DateCreated),
                        link = string.Format("/forum/TopicId/{0}/{1}", o.TopicId.ToString(), System.Text.RegularExpressions.Regex.Replace(o.GetContent.Subject, "[^a-zA-Z0-9]", " ").Replace(" ", "-")),
                        pubDate = Utilies.DisplayCulutureDateTime(o.GetContent.DateCreated),
                        title = o.GetContent.Subject,
                        ttl = "",
                        StatusImage = imgStats,
                        StatusText = strStatusText,
                        UserImage = _UserImage,
                        AuthorName = _UserName,

                    };

                    feeds.Add(ohomefeed);

                }
            }

            return feeds;
        }
        public List<HomeFeed> GetForumFeedSearch(string feedUrl, int TopN, int UserID, string search)
        {
            List<HomeFeed> feeds = new List<HomeFeed>();
            DotNetNuke.Entities.Users.UserController oCtrl = new DotNetNuke.Entities.Users.UserController();
            
            UserInfo oUserInfo = oCtrl.GetUserByDisplayname(0,search);
              
                       
            
            List<CA_Topics> oData = null;
            if (oUserInfo != null)
            {
                oData = new DBController().GetForumsSearch(UserID, TopN, oUserInfo.UserID);
            }
            
            

                if (oData != null)
                {

                    foreach (CA_Topics o in oData)
                    {
                        string _UserImage = "/images/no_avatar.gif";
                        string imgStats = "";
                        string _UserName = "";
                        string strStatusText = "";
                        if (o.Status == "Completed")
                        {
                            imgStats = "/portals/0/img_17.png";
                            strStatusText = "Completed :: ";
                        }
                        else
                        {
                            imgStats = "/portals/0/img_18.png";
                            strStatusText = "To Do :: ";
                        }
                        oUserInfo = oCtrl.GetUser(0, o.AuthorId);
                        if (oUserInfo != null)
                        {
                            _UserImage = oUserInfo.Profile.PhotoURL;
                            _UserName = oUserInfo.FirstName + " " + oUserInfo.LastName;

                        }

                        var ohomefeed = new HomeFeed()
                        {
                            atom = "",
                            author = o.AuthorId,
                            creator = "",
                            description = System.Web.HttpUtility.HtmlDecode(o.GetContent.Body),
                            generator = "",
                            lastBuildDate = Utilies.DisplayCulutureDateTime(o.GetContent.DateCreated),
                            link = string.Format("/forum/TopicId/{0}/{1}", o.TopicId.ToString(), System.Text.RegularExpressions.Regex.Replace(o.GetContent.Subject, "[^a-zA-Z0-9]", " ").Replace(" ", "-")),
                            pubDate = Utilies.DisplayCulutureDateTime(o.GetContent.DateCreated),
                            title = o.GetContent.Subject,
                            ttl = "",
                            StatusImage = imgStats,
                            StatusText = strStatusText,
                            UserImage = _UserImage,
                            AuthorName = _UserName,

                        };

                        feeds.Add(ohomefeed);

                    }
                }

                return feeds;
            }
    }
}