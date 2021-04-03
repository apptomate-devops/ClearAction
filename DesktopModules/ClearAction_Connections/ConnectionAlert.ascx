<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConnectionAlert.ascx.cs" Inherits="ClearAction.Modules.Connections.ConnectionAlert" %>

<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnJsInclude ID="DnnJsInclude1" runat="server" FilePath="~/Resources/Shared/Components/ComposeMessage/ComposeMessage.js" Priority="101" AddTag="false" />
<dnn:DnnCssInclude ID="DnnCssInclude1" runat="server" FilePath="~/Resources/Shared/Components/ComposeMessage/ComposeMessage.css" AddTag="false" />
<%--<dnn:DnnJsInclude ID="DnnJsInclude2" runat="server" FilePath="~/Resources/Shared/Components/UserFileManager/UserFileManager.js" Priority="102" AddTag="false" />--%>
<%--<dnn:DnnCssInclude ID="DnnCssInclude2" runat="server" FilePath="~/Resources/Shared/Components/UserFileManager/UserFileManager.css" AddTag="false" />--%>
<%--<dnn:DnnCssInclude ID="DnnCssInclude3" runat="server" FilePath="~/Resources/Shared/Components/UserFileManager/UserFileManager.css" AddTag="false" />--%>
<dnn:DnnJsInclude ID="DnnJsInclude3" runat="server" FilePath="~/Resources/Shared/Components/Tokeninput/jquery.tokeninput.js" Priority="103" AddTag="false" />

<style type="text/css">
    .conn_type_active {border-bottom: 10px solid #fbb03b; }
    #divConnectionAlert { border:1px solid #888; border-radius: 5%; }
    button.close { opacity: 1; }

    .modal-header { padding: 15px !important; background: #eeeeee; border-bottom: #6992c0 2px dotted; opacity:1; }

    .modal-body { padding: 0; }

    .notification_type { background-color: #6992c0; height:60px; color:#ffffff; }
    .notification_type .notif_connections { display: inline-block; float:left; margin-left: 15px; height: 60px; padding-top:15px;}
    .notification_type .notif_connections:hover {border-bottom: 10px solid #fbb03b; }
    .notification_type .notif_requests { display: inline-block; float:right; margin-right: 15px; height: 60px; padding-top:15px;}
    .notification_type .notif_requests:hover {border-bottom: 10px solid #fbb03b;}

    .connecton_header { background-color: #eeeeee; height: 35px; padding-top: 6px; }
    .connecton_header select { float: left; margin-left: 15px; width: 155px; background-color: #eeeeee;}
    .connecton_header .conn_count {float: left; margin-left: 30px; }
    .connecton_header .link { float: right; padding-right: 15px; text-align: right;}
    .connection-pop { min-height: 300px; padding: 15px;}

    .modal-footer { background-color: #eeeeee; height: 32px; border-top: dotted 1px #000000; }
</style>

<script type="text/javascript">


    $(function () {

        $("#btnclose").click(function () {
            //  $(".overlay").fadeOut(1000);//.remove();
            $("#contact").hide();

        });
    });

<iframe width="854" height="480" src="https://www.youtube.com/embed/0MzRiC8ClxU" frameborder="0" allow="autoplay; encrypted-media" allowfullscreen></iframe>

    var baseServicepath = '/DesktopModules/ClearAction_Connections/API/Connections/';
    jQuery(document).ready(function ($) {

        var md1 = new koConnectionAlert($, ko, {
            userId: <% = (ProfileUserId == -1) ? ModuleContext.PortalSettings.UserId: ProfileUserId %>,
            viewerId: <% = ModuleContext.PortalSettings.UserId %>,
            groupId:<% = GroupId %>,
            pageSize: <% = PageSize %>,
            profileUrl: "<% = ViewProfileUrl %>",
            profileUrlUserToken: "<% = ProfileUrlUserToken %>",
            profilePicHandler: '<% = DotNetNuke.Common.Globals.UserProfilePicRelativeUrl() %>',

            addFriendText: 'Mark as a Favorite',
            OrderBy: '1',
            ViewType: '1',
            FilterBy: '1',

            acceptFriendText: 'Accept as a Favorite',
            friendPendingText: 'Friend Pending',
            removeFriendText: 'Remove Friend',
            followText: 'Follow up',
            unFollowText: 'UnFollow',
            sendMessageText: 'Send Message',
            userNameText: 'User Name',
            emailText: 'Email',
            cityText: 'City',
            searchErrorTe: '',
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
                messageSentText: 'Your Message has been sent successfully',
                dismissThisText: 'Dismmiss Me',
                throttlingText: 'Throttling',
                noResultsText: 'No Result Found',
                searchingText: 'Searching....',
                createMessageErrorText: 'Something went wrong',
                createMessageErrorWithDescriptionText: 'Error on the request. Please try again later.',
                autoSuggestErrorText: 'Auto Suggestion Error'
            });
        md1.init('#<%= divConnectionAlert.ClientID %>');



    });

</script>
<div id="divConnectionAlert" runat="server">
    <div class="modal fade" role="dialog" id="contact" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 id="AlertHeader" class="modal-title">NOTIFICATIONS</h4>
                </div>
                <div class="modal-body">
                    <div class="notification_type">
                        <span class="notif_connections conn_type_active">CONNECTIONS</span>
                        <span class="notif_requests">REQUESTS</span>
                    </div>
                    <div class="connections_list">
                        <div class="connecton_header">
                            <select id="sortbyAlert" data-bind="event: { change: DoSortAlert }">
                                <option value="1">Newest Members</option>
                                <option value="2">Alpha a-z</option>
                                <option value="3">Alpha z-a</option>
                            </select>
                            <div class="conn_count">Connections' Activity <span id="requestcount"></span></div>
                            <!-- ! Hide See More on connection tab!-->
                            <div class="link" style="display: <%=ConnectionTab%>">
                                <div>
                                    <a href="/Connections">See More</a>
                                </div>
                            </div>
                        </div>
                    <div class="connection-pop" style="display: none;" data-bind="visible: !HasMembers()">
                        <br />
                        <h4>No new request there</h4>
                    </div>
                </div>
                <div id="mdMemberList" style="display: none;" data-bind="foreach: { data: Members, afterRender: handleAfterRender }, css: { mdMemberListVisible: Visible }, visible: true">
                    <table>
                        <%=DefaultItemTemplate %>
                    </table>
                </div>
            </div>
            <div class="modal-footer">
                &nbsp;
            </div>
            </div>
        </div>
    </div>
</div>


