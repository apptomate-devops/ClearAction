//
// Active Forums - http://www.dnnsoftware.com
// Copyright (c) 2013
// by DNN Corp.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
//
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using System.Linq;
using System.Collections.Generic;
using DotNetNuke.Modules.ActiveForums.DAL2;
using DotNetNuke.Services.Social.Notifications;
using System.Data;
using DotNetNuke.Modules.ActiveForums.Controls;
using System.Web;

namespace DotNetNuke.Modules.ActiveForums
{
    public partial class SearchResult : ForumBase
    {

        #region Private Members

        private string _searchText;
        private string _tags;
        private int? _searchType;
        private int? _authorUserId;
        private string _authorUsername;
        private int? _searchColumns;
        private string _forums;
        private int? _searchDays;
        private int? _resultType;
        private int? _searchId;
        private int? _sort;

        private List<string> _parameters;

        private int _rowCount;

        private int _pageSize = 20;
        private int _rowIndex;

        private int? _searchAge;
        private int? _searchDuration;



        #endregion

        #region Properties

        private string SearchText
        {
            get
            {
                if (_searchText == null)
                {
                    _searchText = Request.Params["q"] + string.Empty;
                    _searchText = Utilities.XSSFilter(_searchText);
                    _searchText = Utilities.StripHTMLTag(_searchText);
                    _searchText = Utilities.CheckSqlString(_searchText);
                    _searchText = SearchText.Replace("\"", string.Empty);
                    _searchText = _searchText.Trim();
                }

                return _searchText;
            }
        }

        private string Tags
        {
            get
            {
                if (_tags == null)
                {
                    _tags = Request.Params["tg"] + string.Empty;

                    // The tag links are generated with "aftg"
                    if (_tags == string.Empty)
                        _tags = Request.Params["aftg"] + string.Empty;

                    _tags = Utilities.XSSFilter(_tags);
                    _tags = Utilities.StripHTMLTag(_tags);
                    _tags = Utilities.CheckSqlString(_tags);
                    _tags = _tags.Trim();
                }

                return _tags;
            }
        }

        private int SearchType
        {
            get
            {
                if (!_searchType.HasValue)
                {
                    int parsedSearchType;
                    _searchType = int.TryParse(Request.Params["k"], out parsedSearchType) ? parsedSearchType : 0;
                }

                return _searchType.Value;
            }
        }

        private string AuthorUsername
        {
            get
            {
                if (_authorUsername == null)
                {
                    _authorUsername = Request.Params["author"] + string.Empty;
                    _authorUsername = Utilities.XSSFilter(_authorUsername);
                    _authorUsername = Utilities.StripHTMLTag(_authorUsername);
                    _authorUsername = Utilities.CheckSqlString(_authorUsername);
                    _authorUsername = _authorUsername.Trim();
                }

                return _authorUsername;
            }
        }

        private int AuthorUserId
        {
            get
            {
                if (!_authorUserId.HasValue)
                {
                    int parsedValue;
                    _authorUserId = int.TryParse(Request.Params["uid"], out parsedValue) ? parsedValue : 0;
                }

                return _authorUserId.Value;
            }
        }

        private int SearchColumns
        {
            get
            {
                if (!_searchColumns.HasValue)
                {
                    int parsedSearchColumns;
                    _searchColumns = int.TryParse(Request.Params["c"], out parsedSearchColumns) ? parsedSearchColumns : 0;
                }

                return _searchColumns.Value;
            }
        }

        private string Forums
        {
            get
            {
                if (_forums == null)
                {
                    _forums = Request.Params["f"] + string.Empty;
                    _forums = Utilities.XSSFilter(_forums);
                    _forums = Utilities.StripHTMLTag(_forums);
                    _forums = Utilities.CheckSqlString(_forums);
                    _forums = _forums.Trim();
                }

                return _forums;
            }
        }

        private int SearchDays
        {
            get
            {
                if (!_searchDays.HasValue)
                {
                    int parsedValue;
                    _searchDays = int.TryParse(Request.Params["ts"], out parsedValue) ? parsedValue : 0;
                }

                return _searchDays.Value;
            }
        }

        private int ResultType
        {
            get
            {
                if (!_resultType.HasValue)
                {
                    int parsedValue;
                    _resultType = int.TryParse(Request.Params["rt"], out parsedValue) ? parsedValue : 0;
                }

                return _resultType.Value;
            }
        }

        private int SearchId
        {
            get
            {
                if (!_searchId.HasValue)
                {
                    int parsedValue;
                    _searchId = int.TryParse(Request.Params["sid"], out parsedValue) ? parsedValue : 0;
                }

                return _searchId.Value;
            }
        }

        private int Sort
        {
            get
            {
                if (!_sort.HasValue)
                {
                    int parsedValue;
                    _sort = int.TryParse(Request.Params["srt"], out parsedValue) ? parsedValue : 0;
                }

                return _sort.Value;
            }
        }

        private List<string> Parameters
        {
            get
            {
                if (_parameters == null)
                {
                    _parameters = new List<string>();

                    if (!string.IsNullOrWhiteSpace(SearchText))
                        _parameters.Add("q=" + Server.UrlEncode(SearchText));

                    if (!string.IsNullOrWhiteSpace(Tags))
                        _parameters.Add("tg=" + Server.UrlEncode(Tags));

                    if (SearchId > 0)
                        _parameters.Add("sid=" + SearchId);

                    if (SearchType > 0)
                        _parameters.Add("k=" + SearchType);

                    if (ResultType > 0)
                        _parameters.Add("rt=" + ResultType);

                    if (SearchColumns > 0)
                        _parameters.Add("c=" + SearchColumns);

                    if (SearchDays > 0)
                        _parameters.Add("ts=" + SearchDays);

                    if (AuthorUserId > 0)
                        _parameters.Add("uid=" + AuthorUserId);

                    if (Sort > 0)
                        _parameters.Add("srt=" + Sort);

                    if (!string.IsNullOrWhiteSpace(AuthorUsername))
                        _parameters.Add("author=" + Server.UrlEncode(AuthorUsername));

                    if (!string.IsNullOrWhiteSpace(Forums))
                        _parameters.Add("f=" + Server.UrlEncode(Forums));
                }

                return _parameters;
            }
        }

        #endregion

        #region Event Handlers

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //  rptPosts.ItemCreated += RepeaterOnItemCreated;
            rptTopics.ItemCreated += RepeaterOnItemCreated;

            try
            {

                if (Request.QueryString["GroupId"] != null && SimulateIsNumeric.IsNumeric(Request.QueryString["GroupId"]))
                {
                    SocialGroupId = Convert.ToInt32(Request.QueryString["GroupId"]);
                }

                litSearchTitle.Text = GetSharedResource("[RESX:SearchTitle]");

                List<Keyword> keywords;

                // Note: Filter out any keywords that are not at least 3 characters in length

                if (SearchType == 2 && !string.IsNullOrWhiteSpace(SearchText) && SearchText.Trim().Length >= 3) //Exact Match
                    keywords = new List<Keyword> { new Keyword { Value = "\"" + SearchText.Trim() + "\"" } };
                else
                    keywords = SearchText.Split(' ').Where(kw => !string.IsNullOrWhiteSpace(kw) && kw.Trim().Length >= 3).Select(kw => new Keyword { Value = kw }).ToList();

                //if (keywords.Count > 0)
                //{
                //    phKeywords.Visible = true;
                //    rptKeywords.DataSource = keywords;
                //    rptKeywords.DataBind();
                //}

                //if (!string.IsNullOrWhiteSpace(AuthorUsername))
                //{
                //    phUsername.Visible = true;
                //    litUserName.Text = Server.HtmlEncode(AuthorUsername);
                //}

                //if (!string.IsNullOrWhiteSpace(Tags))
                //{
                //    phTag.Visible = true;
                //    litTag.Text = Server.HtmlEncode(Tags);
                //}

                BindPosts();

                // Update Meta Data
                var tempVar = BasePage;
                Environment.UpdateMeta(ref tempVar, "[VALUE] - " + GetSharedResource("[RESX:Search]") + " - " + SearchText, "[VALUE]", "[VALUE]");
            }
            catch (Exception ex)
            {
                Controls.Clear();
                RenderMessage("[RESX:ERROR]", "[RESX:ERROR:Search]", ex.Message, ex);
            }
        }

        private void RepeaterOnItemCreated(object sender, RepeaterItemEventArgs repeaterItemEventArgs)
        {
            var dataRowView = repeaterItemEventArgs.Item.DataItem as DataRowView;

            if (dataRowView == null)
                return;


        }

        #endregion

        #region Private Methods
        public string ReadTemplate(string FileName)
        {
            string strFilePath = Server.MapPath("DesktopModules") + string.Format("/ActiveForums/config/{0}.html", FileName);

            try
            {
                if (System.IO.File.Exists(strFilePath))
                    return System.IO.File.ReadAllText(strFilePath);


            }
            catch (Exception exc)
            {


            }

            return "";
        }

        private void BindPosts()
        {
            _pageSize = (UserId > 0) ? UserDefaultPageSize : MainSettings.PageSize;

            if (_pageSize < 5)
                _pageSize = 10;

            _rowIndex = (PageId - 1) * _pageSize;

            // If we don't have a search string, tag or user id, there is nothing we can do so exit
            if (SearchText == string.Empty && Tags == string.Empty && AuthorUsername == String.Empty && AuthorUserId <= 0)
                return;

            // Build the list of forums to search
            // An intersection of the forums allows vs forums requested.

            var parseId = 0;

            var fc = new ForumController();

            var sForumsAllowed = fc.GetForumsForUser(ForumUser.UserRoles, PortalId, ModuleId, "CanRead", true); // Make sure and pass strict = true here
            var forumsAllowed = sForumsAllowed.Split(new[] { ':', ';' }).Where(f => int.TryParse(f, out parseId)).Select(f => parseId).ToList();
            var forumsRequested = Forums.Split(new[] { ':', ';' }).Where(f => int.TryParse(f, out parseId)).Select(f => parseId).ToList();

            var forumsToSearch = string.Empty;

            // If forums requested is empty or contains and entry less than or equal to zero, return all available forums
            if (!forumsRequested.Any() || forumsRequested.Any(o => o <= 0))
                forumsToSearch = forumsAllowed.Aggregate(forumsToSearch, (current, f) => current + ((current.Length == 0 ? string.Empty : ":") + f));
            else
                forumsToSearch = forumsRequested.Where(forumsAllowed.Contains).Aggregate(forumsToSearch, (current, f) => current + ((current.Length == 0 ? String.Empty : ":") + f));

            const int maxCacheHours = 1;

            var ds = DataProvider.Instance().Search(PortalId, ModuleId, UserId, SearchId, _rowIndex, _pageSize, SearchText, SearchType, SearchColumns, SearchDays, AuthorUserId, AuthorUsername, forumsToSearch, Tags, ResultType, Sort, maxCacheHours, MainSettings.FullText);

            var dtSummary = (ds != null) ? ds.Tables[0] : null;

            _searchId = (dtSummary != null) ? Convert.ToInt32(dtSummary.Rows[0][0]) : 0;
            _rowCount = (dtSummary != null) ? Convert.ToInt32(dtSummary.Rows[0][1]) : 0;
            _searchDuration = (dtSummary != null) ? Convert.ToInt32(dtSummary.Rows[0][2]) : 0;
            _searchAge = (dtSummary != null) ? Convert.ToInt32(dtSummary.Rows[0][3]) : 0;

            var totalSeconds = new TimeSpan(0, 0, 0, 0, _searchDuration.Value).TotalSeconds;
            var ageInMinutes = new TimeSpan(0, 0, 0, 0, _searchAge.Value).TotalMinutes;

            //litSearchDuration.Text = string.Format(GetSharedResource("[RESX:SearchDuration]"), totalSeconds);

            //if (ageInMinutes > 0.25)
            //    litSearchAge.Text = string.Format(GetSharedResource("[RESX:SearchAge]"), ageInMinutes);


            _parameters = null; // We reset this so we make sure to get an updated version

            var dtResults = (ds != null) ? ds.Tables[1] : null;
            if (dtResults != null && dtResults.Rows.Count > 0)
            {
                litRecordCount.Text = string.Format(GetSharedResource("[RESX:SearchRecords]"), _rowIndex + 1, _rowIndex + dtResults.Rows.Count, _rowCount);

                var rptResults = ResultType == 0 ? rptTopics : rptPosts;

                pnlMessage.Visible = false;

                try
                {
                    rptResults.Visible = true;
                    System.Text.StringBuilder strTemplate = new System.Text.StringBuilder();
                    string template = ReadTemplate("SearchTemplate");
                    foreach (DataRow dr in dtResults.Rows)
                    {
                        strTemplate.Append(ReplaceToken(dr, template));

                    }
                    //rptResults.DataSource = dtResults;
                    //rptResults.DataBind();
                    ltrHolder.Text = strTemplate.ToString();

                    //   BuildPager(PagerTop);
                    //   BuildPager(PagerBottom);
                }
                catch (Exception ex)
                {
                    litMessage.Text = ex.Message;
                    pnlMessage.Visible = true;
                    rptResults.Visible = false;
                }
            }
            else
            {
                litMessage.Text = ReadTemplate("NoRecord");// "Sorry!! No Search Result found!!";
                pnlMessage.Visible = true;
            }
        }

        private void BuildPager(PagerNav pager)
        {
            var intPages = Convert.ToInt32(Math.Ceiling(_rowCount / (double)_pageSize));

            //pager.PageCount = intPages;
            //pager.CurrentPage = PageId;
            //pager.TabID = TabId;
            //pager.ForumID = ForumId;
            //pager.PageText = Utilities.GetSharedResource("[RESX:Page]");
            //pager.OfText = Utilities.GetSharedResource("[RESX:PageOf]");
            //pager.View = "search";
            //pager.PageMode = PagerNav.Mode.Links;

            //pager.Params = Parameters.ToArray();
        }

        #endregion

        #region Public Methods
        private string ReplaceToken(DataRow dr, string Template)
        {
            Template = Template.Replace("{GetThreadUrl}", GetThreadUrl(dr));
            Template = Template.Replace("{GetForumUrl}", GetForumUrl(dr));

            Template = Template.Replace("{GetPostUrl}", GetPostUrl(dr));
            Template = Template.Replace("{GetLastPostAuthor}", GetLastPostAuthor(dr));
            Template = Template.Replace("{AUTHORNAME}", GetAuthor(dr));
            Template = Template.Replace("{GetPostTime}", GetPostTime(dr));
            Template = Template.Replace("{SUBJECT}", GetTokenReplaced(dr, "Subject"));
            Template = Template.Replace("{BODY}", GetTokenReplaced(dr, "Body"));
            Template = Template.Replace("{TOPICURL}", GetTopicUrl(dr));
            Template = Template.Replace("{VIEWCOUNT}", GetTokenReplaced(dr, "viewcount"));
            var oCount = GetTokenReplaced(dr, "ContentId");
            Template = Template.Replace("{RECOMMAND}", string.IsNullOrEmpty(oCount) == false ? new LikesController().GetForPost(Convert.ToInt32(oCount)).Count().ToString() : "0");
            return Template;
        }
        public string GetSearchUrl()
        {
            var @params = new List<string> { ParamKeys.ViewType + "=searchadvanced" };
            @params.AddRange(Parameters);

            if (SocialGroupId > 0)
                @params.Add("GroupId=" + SocialGroupId.ToString());

            return NavigateUrl(TabId, string.Empty, @params.ToArray());
        }

        public string GetTokenReplaced(DataRow _currentRow, string Column)
        {
            if (_currentRow == null)
                return null;

            return _currentRow[Column] == null ? "" : Convert.ToString(_currentRow[Column]);



        }
        public string GetTopicUrl(DataRow _currentRow)
        {
            if (_currentRow == null)
                return null;

            var topicid = _currentRow["TopicID"].ToString();

            var @params = new List<string> { ParamKeys.TopicId + "=" + topicid };




            return NavigateUrl(TabId, string.Empty, @params.ToArray());
        }

        public string GetForumUrl(DataRow _currentRow)
        {
            if (_currentRow == null)
                return null;

            var forumId = _currentRow["ForumID"].ToString();

            var @params = new List<string> { ParamKeys.ForumId + "=" + forumId, ParamKeys.ViewType + "=" + Views.Topics };


            if (SocialGroupId > 0)
                @params.Add("GroupId=" + SocialGroupId.ToString());

            return NavigateUrl(TabId, string.Empty, @params.ToArray());
        }

        public string GetThreadUrl(DataRow _currentRow)
        {
            if (_currentRow == null)
                return null;

            var forumId = _currentRow["ForumID"].ToString();
            var topicId = _currentRow["TopicId"].ToString();

            var @params = new List<string> { ParamKeys.ForumId + "=" + forumId, ParamKeys.ViewType + "=" + Views.Topic, ParamKeys.TopicId + "=" + topicId };


            if (SocialGroupId > 0)
                @params = new List<string> { ParamKeys.TopicId + "=" + topicId };
            @params.Add("GroupId=" + SocialGroupId.ToString());

            return NavigateUrl(TabId, string.Empty, @params.ToArray());
        }

        // Jumps to post for post view, or last reply for topics view
        public string GetPostUrl(DataRow _currentRow)
        {
            if (_currentRow == null)
                return null;

            var forumId = _currentRow["ForumID"].ToString();
            var topicId = _currentRow["TopicId"].ToString();
            var contentId = _currentRow["ContentId"].ToString();

            var @params = new List<string> { ParamKeys.ForumId + "=" + forumId, ParamKeys.ViewType + "=" + Views.Topic, ParamKeys.TopicId + "=" + topicId, ParamKeys.ContentJumpId + "=" + contentId };


            if (SocialGroupId > 0)
                @params.Add("GroupId=" + SocialGroupId.ToString());

            return NavigateUrl(TabId, string.Empty, @params.ToArray());
        }

        // Todo: Localize today and yesterday
        public string GetPostTime(DataRow _currentRow)
        {
            if (_currentRow == null)
                return null;

            var date = GetUserDate(Convert.ToDateTime(_currentRow["DateCreated"]));
            var currentDate = GetUserDate(DateTime.Now);

            var datePart = date.ToString(MainSettings.DateFormatString);

            if (currentDate.Date == date.Date)
                datePart = GetSharedResource("Today");
            else if (currentDate.AddDays(-1).Date == date.Date)
                datePart = GetSharedResource("Yesterday");

            return string.Format(GetSharedResource("SearchPostTime"), datePart, date.ToString(MainSettings.TimeFormatString));
        }

        public string GetAuthor(DataRow _currentRow)
        {
            if (_currentRow == null)
                return null;

            var userId = Convert.ToInt32(_currentRow["AuthorId"]);
            var userName = _currentRow["AuthorUserName"].ToString();
            var firstName = _currentRow["AuthorFirstName"].ToString();
            var lastName = _currentRow["AuthorLastName"].ToString();
            var displayName = _currentRow["AuthorDisplayName"].ToString();

            return UserProfiles.GetDisplayName(ModuleId, true, false, ForumUser.IsAdmin, userId, userName, firstName, lastName, displayName);
        }

        public string GetLastPostAuthor(DataRow _currentRow)
        {
            if (_currentRow == null)
                return null;

            var userId = Convert.ToInt32(_currentRow["LastReplyAuthorId"]);
            var userName = _currentRow["LastReplyUserName"].ToString();
            var firstName = _currentRow["LastReplyFirstName"].ToString();
            var lastName = _currentRow["LastReplyLastName"].ToString();
            var displayName = _currentRow["LastReplyDisplayName"].ToString();

            return UserProfiles.GetDisplayName(ModuleId, true, false, ForumUser.IsAdmin, userId, userName, firstName, lastName, displayName);
        }

        // Todo: Localize today and yesterday
        public string GetLastPostTime(DataRow _currentRow)
        {
            if (_currentRow == null)
                return null;

            var date = GetUserDate(Convert.ToDateTime(_currentRow["LastReplyDate"]));
            var currentDate = GetUserDate(DateTime.Now);

            var datePart = date.ToString(MainSettings.DateFormatString);

            if (currentDate.Date == date.Date)
                datePart = GetSharedResource("Today");
            else if (currentDate.AddDays(-1).Date == date.Date)
                datePart = GetSharedResource("Yesterday");

            return datePart + " @ " + date.ToString(MainSettings.TimeFormatString);
        }

        public string GetPostSnippet(DataRow _currentRow)
        {
            var post = _currentRow["Body"].ToString();
            post = Utilities.StripHTMLTag(post);
            post = post.Replace(System.Environment.NewLine, " ");

            if (post.Length > 255)
                post = post.Substring(0, 255).Trim() + "...";

            return post;
        }

        public string GetIcon(DataRow _currentRow)
        {
            if (_currentRow == null)
                return null;

            var theme = MainSettings.Theme;

            // If we have a post icon, use it
            var icon = _currentRow["TopicIcon"].ToString();
            if (!string.IsNullOrWhiteSpace(icon))
                return "~/DesktopModules/ActiveForums/themes/" + theme + "/emoticons/" + icon;

            // Otherwise, chose the icons based on the post stats

            var pinned = Convert.ToBoolean(_currentRow["IsPinned"]);
            var locked = Convert.ToBoolean(_currentRow["IsLocked"]);

            if (pinned && locked)
                return "~/DesktopModules/ActiveForums/themes/" + theme + "/images/topic_pinlocked.png";

            if (pinned)
                return "~/DesktopModules/ActiveForums/themes/" + theme + "/images/topic_pin.png";

            if (locked)
                return "~/DesktopModules/ActiveForums/themes/" + theme + "/images/topic_lock.png";

            // Unread has to be calculated based on a few fields
            var topicId = Convert.ToInt32(_currentRow["TopicId"]);
            var replyCount = Convert.ToInt32(_currentRow["replyCount"]);
            var lastReplyId = Convert.ToInt32(_currentRow["LastReplyId"]);
            var userLastTopicRead = Convert.ToInt32(_currentRow["UserLastTopicRead"]);
            var userLastReplyRead = Convert.ToInt32(_currentRow["UserLastReplyRead"]);
            var unread = (replyCount <= 0 && topicId > userLastTopicRead) || (lastReplyId > userLastReplyRead);

            if (unread)
                return "~/DesktopModules/ActiveForums/themes/" + theme + "/images/topic.png";

            return "~/DesktopModules/ActiveForums/themes/" + theme + "/images/topic_new.png";
        }

        public string GetMiniPager(DataRow _currentRow)
        {
            if (_currentRow == null)
                return null;

            var replyCount = Convert.ToInt32(_currentRow["ReplyCount"]);
            var pageCount = Convert.ToInt32(Math.Ceiling((double)(replyCount + 1) / _pageSize));
            var forumId = _currentRow["ForumId"].ToString();
            var topicId = _currentRow["TopicId"].ToString();

            // No pager if there is only one page.
            if (pageCount <= 1)
                return null;

            List<string> @params;

            var result = string.Empty;

            if (pageCount <= 5)
            {
                for (var i = 1; i <= pageCount; i++)
                {
                    @params = new List<string> { ParamKeys.ForumId + "=" + forumId, ParamKeys.TopicId + "=" + topicId, ParamKeys.ViewType + "=" + Views.Topic, ParamKeys.PageId + "=" + i };

                    if (SocialGroupId > 0)
                        @params.Add("GroupId=" + SocialGroupId.ToString());

                    result += "<a href=\"" + NavigateUrl(TabId, string.Empty, @params.ToArray()) + "\">" + i + "</a>";
                }

                return result;
            }

            // 1 2 3 ... N

            for (var i = 1; i <= 3; i++)
            {
                @params = new List<string> { ParamKeys.ForumId + "=" + forumId, ParamKeys.TopicId + "=" + topicId, ParamKeys.ViewType + "=" + Views.Topic, ParamKeys.PageId + "=" + i };
                if (SocialGroupId > 0)
                    @params.Add("GroupId=" + SocialGroupId.ToString());

                result += "<a href=\"" + NavigateUrl(TabId, string.Empty, @params.ToArray()) + "\">" + i + "</a>";
            }

            result += "<span>...</span>";

            @params = new List<string> { ParamKeys.ForumId + "=" + forumId, ParamKeys.TopicId + "=" + topicId, ParamKeys.ViewType + "=" + Views.Topic, ParamKeys.PageId + "=" + pageCount };
            if (SocialGroupId > 0)
                @params.Add("GroupId=" + SocialGroupId.ToString());

            result += "<a href=\"" + NavigateUrl(TabId, string.Empty, @params.ToArray()) + "\">" + pageCount + "</a>";

            return result;
        }

        #endregion

        public class Keyword
        {
            public string Value { get; set; }

            public string HtmlEncodedValue
            {
                get { return string.IsNullOrWhiteSpace(Value) ? Value : HttpUtility.HtmlEncode(Value); }
            }
        }











    }
}