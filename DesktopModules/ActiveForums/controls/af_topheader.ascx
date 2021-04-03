<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="af_topheader.ascx.cs" Inherits="DotNetNuke.Modules.ActiveForums.af_topheader" %>
<%@ Register TagPrefix="am" TagName="quickSearch" Src="~/DesktopModules/ActiveForums/controls/af_homesearch.ascx" %>
<script type="text/javascript">
    var AF_CurrentCatID = -1;
    var AF_IsMyVault = <%=this.MyVault%>;
    var AF_CurrentStatus =<%=this.CurrentStatus%>; // Default is Zero / 1/2
    var AF_IsDiscussion =<%=this.IsDiscussion%>;
    $(document).ready(function () {
        $(".solve-spaces-nav a").removeClass("active");
        if (AF_IsMyVault == 1) {
            $(".solve-spaces-nav a:last").addClass("active");
        } else {
            $(".solve-spaces-nav a:first").attr("class", "active");
        }
        var AF_CurrentCatID = getParameterByName('CategoryId');
        $("#CA_Forum_dvCatContainer li").removeClass("active");
        $("#CA_Forum_dvCatContainer li a[CatID='" + AF_CurrentCatID + "']").parent().addClass("active");

        $("#dvForumStatusFilter li").removeClass("active");
        $("#dvForumStatusFilter li[statusid='" + AF_CurrentStatus + "']").addClass("active");
        window.scrollTo(0, 0);
        if (AF_IsDiscussion == true) {
            $(".CA_SubHeader").hide();
        }
        Screensize();
    })
    function getParameterByName(name) {
        var url = window.location.pathname;
        if (url.lastIndexOf(name) == -1) return -1;
        var iStart = url.lastIndexOf(name) + name.length + 1;
        var iEnd = url.lastIndexOf("/SortBy");
        return url.substring(iStart, iEnd);;
    }

    function Screensize() {
        /*If browser resized, check width again */

        var viewportWidth = $(window).width();

        if (viewportWidth < 960) {
            $("#dvDropDown").removeClass("SearchFloat");
            $("#categoryId").css({ 'text-align': ' ', 'padding-top': ' ' });
        }
            //$("dvSSContainer").removeClass("GraphContainer").addClass("GraphContainerMobile");       
        else {
            $("#dvDropDown").addClass("SearchFloat");
            $("#categoryId").css({ 'text-align': 'center', 'padding-top':'35px'});
        }


    }

    $(window).resize(Screensize);

</script>
<div class="solve-spaces-top-nav CA_SubHeader" id="CA_Forum_dvUserVaultOption">
    <div class="forum-title">
        <h1>Open Forum</h1>
    </div>
    <div class="forum-nav">
        <div class="forum-topics">
            <div class="forum-topics-title">
                <label runat="server" id="lblCapTopicCount">Topics</label>&nbsp;<asp:Label ID="lblTopicCount" AssociatedControlID="lblCapTopicCount" runat="server" Text="" Style="min-width: 34px;"></asp:Label>
            </div>
            <div class="forum-topics-new">
                <asp:HyperLink ID="hyperlinkNew" runat="server" Text="New"></asp:HyperLink>
            </div>
        </div>
    </div>
    <div class="solve-spaces-nav">
        <asp:LinkButton ID="btnMyVault" runat="server" Text="<strong>My Vault</strong>Curated for me" OnClick="btnMyVault_Click"></asp:LinkButton>
        <asp:LinkButton ID="btnShowAll" runat="server" Text="<strong>Show All</strong>Everything at all levels" OnClick="btnShowAll_Click"></asp:LinkButton>
    </div>
    <div class="forum-search">
        <asp:Panel ID="pnlForumSearch" runat="server" DefaultButton="lnkSearch" Style="position: relative">
            <asp:TextBox ID="txtSearch" runat="server" MaxLength="50" />
            <asp:LinkButton runat="server" ID="lnkSearch" OnClick="lnkSearch_Click" CssClass="searchbutton" />
        </asp:Panel>
    </div>
</div>
<div id="CA_Forum_dvCatContainer" class="solve-spaces-middle-nav CA_SubHeader">
    <ul style="margin-left: 0px; margin-bottom: 0px;">
        <li class="active">
            <asp:LinkButton ID="btnAllCat" CatId="-1" runat="server" OnClick="btnAllCat_Click"><div class="table"><div class="table-cell">All</div></div></asp:LinkButton>
        </li>
        <asp:Repeater ID="dataListGlobalCat" runat="server" OnItemDataBound="dataListGlobalCat_ItemDataBound" OnItemCommand="dataListGlobalCat_ItemCommand">
            <ItemTemplate>
                <li id="categoryId">
                    <asp:LinkButton ID="LinkButton1" runat="server" > </asp:LinkButton>
                   

                  <%-- <div class="table" style="display:none">
                        <div class="table-cell"> <asp:HyperLink ID="hyperlink" runat="server"></asp:HyperLink>
                        </div>
                    </div> --%>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>

<div class="cnt_wraper_lt col-sm-8" style="padding-bottom:0;">
    <div id="dvForumStatusFilter" class="forum-active-topics col-sm-8 forum-intro-match-height CA_SubHeader">
        <div class="row">
            <div class="col-md-8">
                <ul class="filter-spaces button-group filter-button-group" style="margin-left:40px;margin-bottom:40px;">
                    <li class="active" statusid="0">
                        <asp:LinkButton ID="btnSubOptAll" runat="server" OnClick="btnSubOptAll_Click">All</asp:LinkButton>
                    </li>
                    <li statusid="1">
                        <asp:LinkButton ID="btnSubOptToDo" runat="server" OnClick="btnSubOptToDo_Click">
                            <img src="/images/img_18.png" alt="img_18" width="9" height="9">To Do</asp:LinkButton>
                    </li>
                    <li statusid="2">
                        <asp:LinkButton ID="btnSubOptDone" runat="server" OnClick="btnSubOptDone_Click">
                                <img src="/images/img_17.png" alt="img_17" width="9" height="9">Done
                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
            <div id="dvStatusFilter" style="display: none;">
                <div id="dvStatusContainer">
                    <div style="float: left; cursor: pointer;">
                        <asp:LinkButton ID="btntSubOptAll" runat="server">All</asp:LinkButton>
                    </div>
                    <div style="float: left; cursor: pointer;">
                        <asp:LinkButton ID="LinkButton2" runat="server">To Do</asp:LinkButton>
                    </div>
                    <div style="float: left; cursor: pointer;">
                        <asp:LinkButton ID="LinkButton3" runat="server">Done</asp:LinkButton>
                    </div>
                </div>
            </div>
            <div id="dvDropDown" class="col-md-4 text-right" >
                <asp:DropDownList ID="ddSortBy" runat="server" CssClass="forum-select" OnSelectedIndexChanged="ddSortBy_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="Sort by" Value="0" class="CA_SortOptions"></asp:ListItem>
                    <asp:ListItem Text="Alpha a-z" Value="1" class="CA_SortOptions"></asp:ListItem>
                    <asp:ListItem Text="Alpha z-a" Value="2" class="CA_SortOptions"></asp:ListItem>
                    <asp:ListItem Text="Most likes" Value="3" class="CA_SortOptions"></asp:ListItem>
                    <asp:ListItem Text="Most replies" Value="4" class="CA_SortOptions"></asp:ListItem>
                    <asp:ListItem Text="Latest" Value="5" class="CA_SortOptions"></asp:ListItem>
                    <asp:ListItem Text="Active today" Value="6" class="CA_SortOptions"></asp:ListItem>
                    <asp:ListItem Text="Active last 7 days" Value="7" class="CA_SortOptions"></asp:ListItem>
                    <asp:ListItem Text="Active this month" Value="8" class="CA_SortOptions"></asp:ListItem>
                     <asp:ListItem Text="All" Value="9" class="CA_SortOptions"></asp:ListItem>
                </asp:DropDownList>

            </div>
        </div>
    </div>
    <div class="forum-right-col col-sm-4 forum-intro-match-height"></div>
    <div class="clearfix"></div>
</div>

<div class="solve-spaces-top-nav CA_SubHeader" style="display: none;">
    <div class="forum-title">
        <h1>Open Forum-old</h1>
    </div>
    <div class="forum-nav">
        <div class="forum-topics">
            <div class="forum-topics-title">
                Topics
                        
            </div>
            <div class="forum-topics-new">
            </div>
        </div>
    </div>
    <div class="forum-search">
        <div class="forum-select-container">
            <asp:DropDownList ID="ddGlobalCategory" runat="server" CssClass="forum-select" AutoPostBack="true" OnSelectedIndexChanged="ddGlobalCategory_SelectedIndexChanged">
            </asp:DropDownList>

        </div>
        <div class="forum-select-container second">
        </div>
        <div class="Search_form">
            <%-- <input type="text" id="txtSearch" runat="server" />
                    <input type="submit" runat="server" />--%>
        </div>

    </div>

</div>
<div class="forum-happening-now" runat="server" id="topHeader2" style="display: none;">
    <h2>Happening now...</h2>
    <div class="forum-happening-files">
        <div class="left">
            <span>
                <asp:Label ID="lblTotalFileToday" runat="server"></asp:Label>
            </span>
            <a href="#">List</a>
        </div>
        <div class="right">
            Files have been
                    <br>
            shared today
        </div>
    </div>
    <div class="forum-happening-members">
        <div class="left">
            <span>
                <asp:Label ID="lblActiveMember" runat="server"></asp:Label>
            </span>
            <a href="#">All</a>
        </div>
        <div class="right">
            Members are active in the
                    <br>
            Open Forum at this moment
        </div>
    </div>
    <div class="forum-status-text">
        <%-- <div class="status-text">
                    Show my status as: 
                </div>
                <div class="status-select">
                    <select class="forum-select">
                        <option value="">Available</option>
                        <option value="0">Away</option>
                        <option value="1">Do not disturb</option>
                    </select>
                </div>
                <div class="green-circle-large"></div>--%>
    </div>
</div>
<style>

    .HeaderPadding{
        height: 93px;width:250px;
        text-align: center;padding-top:35px
    }
     .HeaderPaddingMobile{
        height: 93px;width:150px;       
    }
    /*dnn_ctr421_ActiveForums_topheader_lnkSearch
    {
         position: absolute;
    right: 28% !important;
    
    background: url(images/solve-space-search-icon.png) no-repeat 50% 50%;
    padding: 22px;
    width: 29px;
    height: 24px;
    border: 0 none;
    right: 32% !important;
    top: 44px;
    }*/
/*

    .forum-search .searchbutton {
        position: absolute;
        right: 5px;
        top: 0;
        background: url(/images/solve-space-search-icon.png) no-repeat 50% 50%;
        padding: 22px;
        width: 29px;
        height: 24px;
        border: 0 none;
    }
*/
    

</style>
