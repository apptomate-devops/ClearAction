<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="ClearAction.Modules.Connections.View" %>

<script src="https://static.filestackapi.com/v3/filestack.js"></script>

<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
 
<dnn:DnnJsInclude ID="DnnJsInclude1" runat="server" FilePath="~/Resources/Shared/Components/ComposeMessage/ComposeMessage.js" Priority="101" AddTag="false" />
<%--<dnn:DnnCssInclude ID="DnnCssInclude1" runat="server" FilePath="~/Resources/Shared/Components/ComposeMessage/ComposeMessage.css" AddTag="false" />--%>
<%--<dnn:DnnJsInclude ID="DnnJsInclude2" runat="server" FilePath="~/Resources/Shared/Components/UserFileManager/UserFileManager.js" Priority="102" AddTag="false" />--%>
<%--<dnn:DnnCssInclude ID="DnnCssInclude2" runat="server" FilePath="~/Resources/Shared/Components/UserFileManager/UserFileManager.css" AddTag="false" />--%>
<%--<dnn:DnnCssInclude ID="DnnCssInclude3" runat="server" FilePath="~/Resources/Shared/Components/UserFileManager/UserFileManager.css" AddTag="false" />--%>
<dnn:DnnJsInclude ID="DnnJsInclude3" runat="server" FilePath="~/Resources/Shared/Components/Tokeninput/jquery.tokeninput.js" Priority="103" AddTag="false" />
<dnn:DnnCssInclude ID="DnnCssInclude4" runat="server" FilePath="~/Resources/Shared/Components/Tokeninput/Themes/token-input-facebook.css" AddTag="false" />
<%--<dnn:DnnJsInclude ID="DnnJsInclude6" runat="server" FilePath="~/Resources/Shared/Components/UserFileManager/jquery.dnnUserFileUpload.js" Priority="104" AddTag="false" />--%>
 <asp:Panel ID="pnlBlogSearch" runat="server" Style="position: relative" Visible="false">
            </asp:Panel>


<style>
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
</style>
<script>
    jQuery(document).ajaxStart(function () {
        try {
            // showing a modal
            $("#loading").modal();

            var i = 0;
            var timeout = 750;

            (function progressbar() {
                i++;
                if (i < 1000) {
                    // some code to make the progress bar move in a loop with a timeout to 
                    // control the speed of the bar
                 
                    setTimeout(progressbar, timeout);
                }
            }
            )();
        }
        catch (err) {
            alert(err.message);
        }
    });
    jQuery(document).ajaxStop(function () {
        // hide the progress bar
        $("#loading").modal('hide');
    });
    function CallMe(index) {

        $('#<%=hyperMyConnection.ClientID%>').removeClass();
        $('#<%=hyperRoleSimilar.ClientID%>').removeClass();
        $('#<%=hyperPeopleinmycompany.ClientID%>').removeClass();

        $('#<%=hypernonconnection.ClientID%>').removeClass();
        $('#<%=hyperall.ClientID%>').removeClass();

        $('#' + index).removeClass().addClass('active');
        //alert(index);
    }

    $("#modalVisible_MessageBox").on('click', "a[data-window='external']", function () {
        debugger;
        window.open($(this).attr('href'));
        return false;
    });
</script>
<div id="loading" data-bind="visible: loadingData"   style="display: none; width: 100%; height: 100%; top: 100px; left: 0px; position: fixed; z-index: 10000; text-align: center;">
     <img src='<%= ResolveUrl("/images/progress.gif") %>'   alt="Loading..." style="position: fixed; top: 50%; left: 50%;" />
</div>
 
<div id="CADirectory1" runat="server" class="col-sm-12 main_wrapper">
     <div class="solve-spaces-top-nav col-sm-8" id="CA_Forum_dvUserVaultOption1">
        <div class="forum-title">
            <h1>Connections</h1>
        </div>

        <div class="solve-spaces-nav">
            <a data-bind="click: function (data, event) { DoFilter('1', event) }" style="cursor: pointer" id="hyperMyConnection" runat="server">My Connections</a>
            <a data-bind="click: function (data, event) { DoFilter('2', event) }" style="cursor: pointer" id="hyperRoleSimilar" runat="server">Roles Similar
                <br />
                to mine</a>
            <a data-bind="click: function (data, event) { DoFilter('3', event) }" style="cursor: pointer" id="hyperPeopleinmycompany" runat="server">People In My
                <br />
                Company</a>
            <a data-bind="click: function (data, event) { DoFilter('4', event) }" style="cursor: pointer" id="hypernonconnection" runat="server">All Members</a>
            <a data-bind="click: function (data, event) { DoFilter('5', event) }" style="cursor: pointer" id="hyperall" runat="server">All</a>
        </div>

    </div>

    <div style="padding-top: 8px;" class="solve-spaces-middle-nav col-sm-4" id="CA_Forum_dvUserVaultOption">

        <div class="forum-title">
            <h1>&nbsp;</h1>
        </div>
        <div class="forum-nav filter-bar">
            <select id="sortby" class="forum-select" style="color: #000" data-bind="event: { change: DoSort }">
                <option value="1" class="CA_SortOptions">Sort by: Newest Members</option>
                <option value="1" class="CA_SortOptions">Sort by: Newest Members</option>
                <option value="2" class="CA_SortOptions">Alpha a-z</option>
                <option value="3" class="CA_SortOptions">Alpha z-a</option>
            </select>
        </div>
        <div class="forum-nav">
            <div class="forum-topics">
                <div class="forum-topics-title">
                    <label id="dnn_ctr421_ActiveForums_topheader_lblCapTopicCount"></label>
                    &nbsp;&nbsp;<label id="spnCounter" runat="server"></label>
                </div>

            </div>
        </div>
        

        <div class="forum-search" id="mdBasicSearchBar">
           <div style="position:relative;">
             <%--<a href="#" id="refreshResults" data-bind="visible: ResetEnabled, click: resetSearch"><span>Refresh</span></a>--%>
            <input type="text" id="mdBasicSearch" data-bind="value: SearchTerm" />
            <a href="" title="search" data-bind="click: basicSearch" class="searchbutton"></a>
           </div>
        </div>
    </div>

    </div>
<div id="CADirectory" runat="server" class="dnnForm dnnMemberDirectory col-sm-8">
   
    <div id="loading1"  style="display: none; width: 100%; height: 100%; top:400px; left: 0px; position: fixed; z-index: 10000; text-align: center;">

        <p style="padding-top: 20px">
            <center><img src='<%= ResolveUrl("/images/progress.gif") %>' alt='Loading...' /><br />Loading....</center>
        </p>
    </div>
    <div class="dnnFormMessage dnnFormInfo" style="display: none" data-bind="visible: !HasMembers()">No member has been found</div>
    <div class="row connection_wrapper" id="mdMemberList" style="display: none" data-bind="foreach: { data: Members, afterRender: handleAfterRender }, css: { mdMemberListVisible: Visible }, visible: true">

        <%=DefaultItemTemplate %>
    </div>
    
    <center>  <div id="loadMore" style="display:none" runat="server" viewstatemode="Disabled"  class="mdLoadMore"  data-bind="visible: CanLoadMore()"><a id="hyperloading" href="" class="btn btn-primary" title="" style="display:none" data-bind="click: loadMore">&darr; Load More..</a></div>
  </center>
</div>
<br />
<br />
<br />
<br />

<script type="text/javascript">
    var oKey = '<%=GetAPIKey%>';// 'AUQ4R8zKvSKCs9gudWJlxz';
    $(window).load(function () {
         
    });
   
    function ToolTipInfo() {

        $('[data-toggle="popover"]').popover({
            html: true,

            //blank title needed for bootstrap bug #12563. hide title using css
            title: function () {
                if ($(this).data('title'))

                    return $(this).data('title');
                else return " ";
            },
            placement: 'top',
            content: function () {

                var id = $(this).data('contentwrapper');
                return $(id).html();
            }
        }).on('shown.bs.popover', function (e) {
            $('[data-toggle="popover"]').not(e.target).popover('hide');
        }).on('hidden.bs.popover', function (e) {
            $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };

        });
    }

    function closePopovers() {
        "use strict";
        $('[rel="popover"], [data-toggle="popover"]').popover('hide');
    }

    var baseServicepath = '/DesktopModules/ClearAction_Connections/API/Connections/';

    jQuery(document).ready(function ($) {

        var md = new CADirectory($, ko, {
            userId: <% = (ProfileUserId == -1) ? ModuleContext.PortalSettings.UserId: ProfileUserId %>,
            viewerId: <% = ModuleContext.PortalSettings.UserId %>,
            groupId:<% = GroupId %>,
            pageSize: <% = PageSize %>,
            profileUrl: "<% = ViewProfileUrl %>",
            profileUrlUserToken: "<% = ProfileUrlUserToken %>",
            profilePicHandler: '<% = DotNetNuke.Common.Globals.UserProfilePicRelativeUrl() %>',

            addFriendText: 'Mark as a Favorite',
            OrderBy: '<% = OrderBy %>',
            ViewType: '<% = ViewType %>',
            FilterBy: '<% = ViewType %>',

            acceptFriendText: 'Accept as a Favorite',
            friendPendingText: 'Friend Pending',
            removeFriendText: 'Remove Friend',
            followText: 'Follow up',
            unFollowText: 'UnFollow',
            sendMessageText: 'Send Message',
            userNameText: 'User Name',
            emailText: 'Email',
            cityText: 'City',
            searchErrorText: 'Search Error',
            serverErrorText: 'Server Error',
            serverErrorWithDescriptionText: 'Server Error',
            servicesFramework: $.ServicesFramework(<%=ModuleContext.ModuleId %>),
            disablePrivateMessage: <%= DisablePrivateMessage.ToString().ToLowerInvariant() %>
        },
            {
                title: 'Title',
                toText: 'To',
                subjectText: 'Subject',
                messageText: 'Your Message',
                sendText: 'Send',
                cancelText: 'Cancel',
                attachmentsText: 'Image Attach',
                browseText: 'Browse',
                uploadText: 'Upload',
                maxFileSize: <%=Config.GetMaxUploadSize()%>,
                removeText: 'Remove',
                messageSentTitle: 'Confirmation',
                messageSentText: 'Your message has been sent successfully',
                dismissThisText: 'Dismiss Me',
                throttlingText: 'Throttling',
                noResultsText: 'No Result Found',
                searchingText: 'Searching....',
                createMessageErrorText: 'Something went wrong',
                createMessageErrorWithDescriptionText: 'error on the request. Please try again later.',
                autoSuggestErrorText: 'Auto Suggestion Error'
            });
        md.init('#<%= CADirectory.ClientID %>');

      

        //$("#mdBasicSearchBar input[type=text]").keydown(function (e) {
        //    if (e.which == 13) {
        //        $("#mdBasicSearchBar a[class*=searchbutton]").focus().click();
        //        e.preventDefault();
        //    }
        //});


        ToolTipInfo();

    });


    function AlertWindow(head, content) {
        $.alert({
            title: head,
            type: 'modern',
            content: content
        });
    }

    function SuccessWindow(head, content) {
        $.alert({
            title: head,
            type: 'blue',
            content: content
        });
    }

    function Confirm() {
        $.confirm({
            icon: 'fa fa-spinner fa-spin',
            title: 'Working!',
            content: 'We are processing your request!'
        });
    }

    //RG: chat content must scroll to the bottom
    function scrollToBottom(el) {
        var elId = $(el).attr("id");
        var parts = elId.split("_");
        elId = "chatContent_" + parts[parts.length - 1];
        var chatArea = $('#' + elId);
        $('#' + elId).stop().animate({ scrollTop: $('#' + elId)[0].scrollHeight }, 1000);
        
    }

    

</script>

<%--<div >
    <asp:Panel ID="pnldonothing" DefaultButton="btnDonothing" runat="server">
        <asp:Button ID="btnDonothing" runat="server" />
    </asp:Panel>
</div>--%>
