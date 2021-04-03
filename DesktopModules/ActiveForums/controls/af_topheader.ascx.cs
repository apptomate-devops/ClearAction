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
using System.Collections;
using System.Collections.Generic;
using System.Data;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke;
using System.Linq;
using System.Text.RegularExpressions;
//using DotNetNuke.Services.ClientCapability;
using DotNetNuke.Services.Social.Notifications;
using DotNetNuke.Services.Localization;
using DotNetNuke.Entities.Tabs;

namespace DotNetNuke.Modules.ActiveForums
{

    public partial class af_topheader : ForumBase
    {

        private int IsMyVault = 1;
        private int SubOption = 0;  // 0 - All , 1 - To Do , 2 - Done
        private int _CurrentCatID = -1;

        public int MyVault
        {
            get
            {
                int iReturn = 1;
                try
                {
                    if (((GetQueryStringValue("CategoryId", -99) != -99)) || (Session["CA_IsMyVault"] != null))
                    {
                        iReturn = int.Parse(Session["CA_IsMyVault"].ToString());
                    }
                    else { iReturn = 1; }
                }
                catch (Exception ex) { }
                return iReturn;
            }
        }
        public int CurrentStatus
        {
            get
            {
                int iReturn = 0;
                try
                {
                    if (Session["CA_Status"] != null)
                    {
                        iReturn = int.Parse(Session["CA_Status"].ToString());
                    }
                    else
                    {
                        iReturn = 0;
                    }
                }
                catch (Exception ex) { }
                return iReturn;
            }
        }
        private int _CatID = -1;
        public int CurrentCatID
        {
            get { return _CatID; }
            set { _CatID = value; }
        }

        public int MID;
        public int iModuleId
        {
            get
            {
                return MID;
            }
            set
            {
                MID = value;
            }

        }

        bool _topHeader2;
        public bool SubHeader
        {
            get
            {
                if (_topHeader2 == null)
                    return true;
                return _topHeader2;
            }
            set
            {
                _topHeader2 = value;
            }

        }

        public string IsDiscussion
        {
            get
            {
                // Hide the SubHeader if current page name is "Discussions"
                TabController oTabCtrl = new TabController();
                string _IsDiscussion = "false";
                TabInfo oCurrTab = oTabCtrl.GetTab(PortalSettings.ActiveTab.TabID, 0);
                if (oCurrTab != null)
                {
                    if (oCurrTab.TabName.Trim().ToLower() == "discussion")
                    {
                        _IsDiscussion = "true";
                    }
                }
                return _IsDiscussion;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                if (GetQueryStringValue("CategoryId", -99) == -99)
                {
                    //reset the session values on fresh page call
                    Session["CA_Status"] = null;
                    Session["CA_IsMyVault"] = null;
                    Session["CA_CategoryID"] = null;
                    Session["CA_IsMyVault"] = null;
                }
                if (!IsPostBack)
                {
                    topHeader2.Visible = SubHeader;



                    hyperlinkNew.NavigateUrl = DotNetNuke.Common.Globals.NavigateURL(this.TabId, "Topics", "mid=" + GetModuleId().ToString(), ParamKeys.ForumId + string.Format("={0}&afv=Post&action=crete&CategoryID=", ForumId) + CategoryFilter.ToString());

                    var Controller = new DAL2.Dal2Controller();

                    var oGlobalCategory = Controller.GetGlobalCategory(-1, true).ToList();
                    ddGlobalCategory.DataSource = oGlobalCategory;
                    ddGlobalCategory.DataTextField = "CategoryName";
                    ddGlobalCategory.DataValueField = "CategoryId";
                    ddGlobalCategory.DataBind();

                    ddGlobalCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Category", "-1"));
                    ddGlobalCategory.Items.Insert(oGlobalCategory.Count + 1, new System.Web.UI.WebControls.ListItem("All", "-1"));

                    // By Sachin : Binding Global Categories in formatted HTML using DataList control
                    dataListGlobalCat.DataSource = oGlobalCategory;
                    dataListGlobalCat.DataBind();

                    if (GetQueryStringValue("CategoryID", -1) > 0)
                        ddGlobalCategory.Items.FindByValue(GetQueryStringValue("CategoryID", -1).ToString()).Selected = ddGlobalCategory.Items.FindByValue(GetQueryStringValue("CategoryID", -1).ToString()) != null ? true : false;
                    //Bind this for later phase when myvalue will be clear
                    //ddGlobalCategory.Items.Insert(ddGlobalCategory.Items.Count + 1, new System.Web.UI.WebControls.ListItem("Those in My Vault", "0"));
                    txtSearch.Text = GetQueryStringValue("q", "");
                    //Bind Stats 
                    //To do : Stats stored procedure and respective class will be updated according.
                    if (GetQueryStringValue("SortBy", -1) > 0)
                        ddSortBy.Items.FindByValue(GetQueryStringValue("SortBy", -1).ToString()).Selected = ddSortBy.Items.FindByValue(GetQueryStringValue("SortBy", -1).ToString()) != null ? true : false;

                    var oStats = Controller.GetSats();
                    if (oStats != null)
                    {
                        lblTotalFileToday.Text = Convert.ToString(oStats.TotalFileToday);
                        lblTopicCount.Text = Convert.ToString(oStats.TotalTopics);
                        lblActiveMember.Text = Convert.ToString(oStats.ActiveMember);
                    }
                }
            }
            catch (Exception exc)
            {
                Services.Exceptions.Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
        public int SortBy
        {
            get

            {
                if (ddSortBy.SelectedItem != null)
                    return Convert.ToInt32(ddSortBy.SelectedValue);
                return 0;
            }
        }
        //public int GetNumberofDays
        //{
        //    get
        //    {
        //        if (SortBy == 3)
        //            return 3;
        //        if (SortBy == 2)
        //            return 2;
        //        if (SortBy == 4)
        //            return "4";
        //        if (SortBy == 5)
        //            return "1";
        //        if (SortBy == 6)
        //            return "7";
        //        if (SortBy == 7)
        //            return "30";
        //        return "7";

        //    }
        //}

        public int CategoryFilter
        {
            get

            {
                if (ddGlobalCategory.SelectedItem != null)
                    return Convert.ToInt32(ddGlobalCategory.SelectedValue);
                return -1;
            }
        }

        protected void ddSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(BuildQuery(GetQueryStringValue("CategoryId", CategoryFilter), SortBy, -1));
        }
        protected void ddGlobalCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  Response.Redirect(BuildQuery(CategoryFilter, SortBy, -1));
        }

        protected void dataListGlobalCat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                LinkButton lnkButton = (LinkButton)e.Item.FindControl("LinkButton1");
                lnkButton.Text = "<div class='table'><div class='table-cell'>" + ((DotNetNuke.Modules.ActiveForums.DAL2.GlobalCategory)e.Item.DataItem).DisplayName + "</div></div>";
                lnkButton.Attributes.Add("CatID", ((DotNetNuke.Modules.ActiveForums.DAL2.GlobalCategory)e.Item.DataItem).CategoryId.ToString());
                lnkButton.CommandArgument = ((DotNetNuke.Modules.ActiveForums.DAL2.GlobalCategory)e.Item.DataItem).CategoryId.ToString();
                lnkButton.CommandName = "LoadTopic";
                //lnkButton.CssClass = "";
                //HyperLink hyperlink = (HyperLink)e.Item.FindControl("hyperlink");
                //hyperlink.Text = ((DotNetNuke.Modules.ActiveForums.DAL2.GlobalCategory)e.Item.DataItem).DisplayName;
                //hyperlink.NavigateUrl = BuildQuery(((DotNetNuke.Modules.ActiveForums.DAL2.GlobalCategory)e.Item.DataItem).CategoryId, GetQueryStringValue("SortBy", 0), GetQueryStringValue("TopicId", 0)); // DotNetNuke.Common.Globals.NavigateURL(this.TabId, "", "CategoryId=" + GetQueryStringValue("CategoryId", ((DotNetNuke.Modules.ActiveForums.DAL2.GlobalCategory)e.Item.DataItem).CategoryId.ToString()), "SortBy=" + GetQueryStringValue("SortBy", "0"), "TopicId=" + GetQueryStringValue("TopicId", "-1"));

            }
        }

        protected void dataListGlobalCat_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.ToLower() == "loadtopic")
            {
                int iCatID = int.Parse(e.CommandArgument.ToString());
                CurrentCatID = iCatID;
                Session["CA_CategoryID"] = iCatID;
                GetData();
            }
        }

        protected void btnAllCat_Click(object sender, EventArgs e)
        {
            CurrentCatID = -1;
            Session["CA_CategoryID"] = -1;
            GetData();
        }

        protected void btnMyVault_Click(object sender, EventArgs e)
        {
            IsMyVault = 1;
            Session["CA_IsMyVault"] = 1;
            Session["CA_CategoryID"] = -1;
            GetData();
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            Session["CA_IsMyVault"] = 0;
            Session["CA_CategoryID"] = -1;
            GetData();
        }

        protected void btnSubOptAll_Click(object sender, EventArgs e)
        {
            Session["CA_Status"] = 0;
            GetData();

        }
        //private CA_UserSession _userselections = new CA_UserSession();
        //private CA_UserSession CA_UserSelected
        //{
        //    get
        //    {
        //       Session["CA_UserSelections"] = _userselections;
        //       return _userselections;
        //    }
        //    set
        //    {
        //        _userselections = value;
        //    }
        //}

        private void GetData()
        {
            if (Session["CA_CategoryID"] != null)
                CurrentCatID = int.Parse(Session["CA_CategoryID"].ToString());

            string strSearch = "";
            if (string.IsNullOrEmpty(txtSearch.Text))
                Session["CA_SearchKey"] = null;
            if (Session["CA_SearchKey"] != null)
                strSearch = Convert.ToString(Session["CA_SearchKey"]);
            else
                strSearch = txtSearch.Text;
            Response.Redirect(BuildQuery(CurrentCatID, SortBy, -1, Server.UrlEncode(strSearch), -1));
        }

        protected void btnSubOptToDo_Click(object sender, EventArgs e)
        {
            Session["CA_Status"] = 1;
            GetData();
        }

        protected void btnSubOptDone_Click(object sender, EventArgs e)
        {
            Session["CA_Status"] = 2;
            GetData();
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            Session["CA_SearchKey"] = Server.UrlDecode(txtSearch.Text.Trim());
            //  txtSearch.Text = "";
            GetData();
        }
    }
}
