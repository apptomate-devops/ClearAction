<%@ Control Language="C#" ClassName="ClearAction_Chat" Inherits="ClearAction_Chat.View" CodeBehind="View.ascx.cs" %>
<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>



<script src="/DesktopModules/ClearAction_Chat/Scripts/jquery.signalR-1.0.1.min.js" type="text/javascript"></script>
<script type="text/javascript" src='<%= ResolveClientUrl("~/signalr/hubs") %>'></script>


<link href="/DesktopModules/ClearAction_Chat/CSS/font-awesome.min.css" rel="stylesheet">
<link href="/DesktopModules/ClearAction_Chat/CSS/clear-action-chat.css" rel="stylesheet">
<script src="/DesktopModules/ClearAction_Chat/Scripts/jquery.nicescroll.min.js" type="text/javascript"></script>

<script type="text/javascript">

    $(document).ready(function () {
        $(".chat").niceScroll();
        $(".user-unready-message-count").hide();
        var selectedUserId = GetSelectedUserId();


        var chatHub = $.connection.chatHub;
        //var chatHub = $.connection.createHubProxy("ClearAction_Chat");
        // Declare a function on the chat hub so the server can invoke it



        // Start the connection, and when it completes successfully wire up the click handler for the link
        $.connection.hub.start().done(function () {
            var selectedUserId = GetSelectedUserId();
            ChangeSelectedUser(selectedUserId);
            var messageSentByUserId = GetLoginUserId();
            var messageReceiveByUserId = GetSelectedUserId();
            var oldConnectionHubId = $("#<%= HiddenFieldConnectionId.ClientID %>").val();



            $("#SentMessage").click(function () {

                messageSentByUserId = GetLoginUserId();
                messageReceiveByUserId = GetSelectedUserId();
                var message = $('#msg').val();
                if (message != "" && messageReceiveByUserId != "") {

                    chatHub.server.send(messageSentByUserId, messageReceiveByUserId, message);
                }
            });

            $("#SentAttachment").click(function () {

                //OpenFileUpload();
            });

            if (messageReceiveByUserId == "") {
                $("#SentMessage").attr("disabled", "disabled");
                $("#SentAttachment").attr("disabled", "disabled");
                $("#msg").attr("disabled", "disabled");

            }
            else {

                $("#SentMessage").removeAttr("disabled");
                $("#SentAttachment").removeAttr("disabled");
                $("#msg").removeAttr("disabled");
            }

            chatHub.server.notify(messageSentByUserId, oldConnectionHubId, $.connection.hub.id);
            GetSetUserUnreadCount();

        });






        chatHub.client.broadcastMessage = function (FromUserProfileId, FromUserMobile, ToUserProfileId, ToUserMobile, communicationMessage, FormUserMessageTime, sendToConversionId, storeFileName) {


            var hiddenLoginUserProfileId = GetLoginUserId();
            var hiddenToUserProfileId = GetSelectedUserId();
            var hiddenPersonalCommunicationSelectedProfileId = GetSelectedUserId();

            if (FromUserProfileId == hiddenLoginUserProfileId) {
                var LoginUserName = GetLoginUserFullName();
                var LoginUserImagePath = GetLoginUserImageURL();
                if (storeFileName != "") {

                    $("#UserPersonalChatArea").append("<div class='answer left'> <div class='avatar' >\
                            <img src='"+ LoginUserImagePath + "' alt='" + LoginUserName + "'>\
                                <div status '></div>\
                        </div>\
                                <div class='name'>"+ LoginUserName + "</div>\
                            <div class='text'> <a target='_blank' class='FileDocumentClassQuickChat' href='/images/quickchat/" + storeFileName + "'> " + communicationMessage + "</a> </div>\
                            <div class='time'>"+ FormUserMessageTime + "</div>\
                            </div > ");

                }
                else {

                    $("#UserPersonalChatArea").append("<div class='answer left'> <div class='avatar' >\
                            <img src='"+ LoginUserImagePath + "' alt='" + LoginUserName + "'>\
                                <div status '></div>\
                        </div>\
                                <div class='name'>"+ LoginUserName + "</div>\
                            <div class='text'> "+ communicationMessage + " </div>\
                            <div class='time'>"+ FormUserMessageTime + "</div>\
                            </div > ");

                }
                CommunicationScrolToLastMessage();
                $('#msg').val("");
                $('#msg').focus();
            }
            else {


                if (hiddenLoginUserProfileId == ToUserProfileId && hiddenPersonalCommunicationSelectedProfileId == FromUserProfileId) {

                    var SelectedUserName = GetSelectedUserFullName();
                    var SelectedUserImagePath = GetSelectedUserImageURL();
                    if (storeFileName != "") {

                        $("#UserPersonalChatArea").append("\
                            <div class='answer right receiveMessageIremClass unreadByUser-false' ConversionSendToId=" + sendToConversionId + ">\
                            <div class='avatar' >\
                            <img src='"+ SelectedUserImagePath + "' alt='" + SelectedUserName + "'>\
                                <div class='status ChatAreaUserOnlineOffLine-"+ FromUserProfileId + "'></div>\
                        </div>\
                                <div class='name'>"+ SelectedUserName + "</div>\
                            <div class='text'> <a target='_blank' class='FileDocumentClassQuickChat' href='/images/quickchat/" + storeFileName + "'> " + communicationMessage + "</a> </div>\
                            <div class='time'>"+ FormUserMessageTime + "</div>\
                            </div > ");

                    }
                    else {

                        $("#userPersonalChatCommunicationArea .personalChatItem").append(" \
                                <div  class='direct-chat-msg receiveMessageIremClass unreadByUser-false' ConversionSendToId=" + sendToConversionId + "> \
                                <div class='direct-chat-info clearfix'> \
                                <span class='direct-chat-name pull-left'>" + SelectedUserName + "</span> \
                                <span class='direct-chat-timestamp pull-right'>" + FormUserMessageTime + "</span> \
                                </div> \
                                <img class='direct-chat-img' src='" + SelectedUserImagePath + "' alt='" + SelectedUserName + "'>\
                                <div class='direct-chat-text'>" + communicationMessage + "</div> \
                                </div> ");



                        $("#UserPersonalChatArea").append("\
                            <div class='answer right receiveMessageIremClass unreadByUser-false' ConversionSendToId=" + sendToConversionId + ">\
                            <div class='avatar' >\
                            <img src='"+ SelectedUserImagePath + "' alt='" + SelectedUserName + "'>\
                                <div class='status ChatAreaUserOnlineOffLine-"+ FromUserProfileId + "'></div>\
                        </div>\
                                <div class='name'>"+ SelectedUserName + "</div>\
                            <div class='text'> " + communicationMessage + "</div>\
                            <div class='time'>"+ FormUserMessageTime + "</div>\
                            </div > ");
                    }
                    //receiverScrolWhenRecceiveMessage();
                    //OnWindowScroll();

                }

            }

            SetOnlineUser(FromUserProfileId)
            GetSetUserUnreadCount();
        };











        chatHub.client.UserOnline = function (UserId) {
            SetOnlineUser(UserId);
            $("#hiddenConnectionId").val($.connection.hub.id);
        };

        chatHub.client.UpdateOnlineOfflineSet = function (UserId) {
            SetOnlineUser(UserId);
            GetSetUserUnreadCount();
        };

        chatHub.client.SetUserUnreadCount = function (objUserUnreadMessageCountModelList) {

            SetUserCountLabel(objUserUnreadMessageCountModelList);
        };



        chatHub.client.AppandUserPersonalConversion = function (data, afterAppendLastConSendToId, remainingCount) {


            var hiddenLoginUserProfileId = GetLoginUserId();
            var hiddenToUserProfileId = GetSelectedUserId();
            var totalRecordCount = data.length
            var currentRecord = 0;

            if (remainingCount != "0") {
                if ($("#UserPersonalChatArea .loadMoreCommunication").length == 0) {

                    $("#UserPersonalChatArea").append(" \
                                    <h3 id='loadMorePerCommunication' LastConversionId='"+ afterAppendLastConSendToId + "' class='loadMoreCommunication-" + afterAppendLastConSendToId + "'>LOAD MORE </h3> \
                                    ");

                    $("#UserPersonalChatArea .loadMoreCommunication-" + afterAppendLastConSendToId + "").click(function () {
                        var lastConversionId = $(this).attr("LastConversionId");
                        loadMorePersonalConversion(lastConversionId)
                    });
                }
            }



            $.each(data, function (key, value) {
                currentRecord = currentRecord + 1;

                var ConversionId = value.ConversionId;
                var ConversionSendToId = value.ConversionSendToId;

                var SentByProfileId = value.SentByUserId;
                var SentByMobileNumber = value.SentByMobileNumber;

                var ReceiveByProfileId = value.ReceiveByUserId;
                var ReceiveByMobileNumber = value.ReceiveByMobileNumber;

                var ConversionText = value.ConversionText;
                var IsReadByReceiveByProfileId = value.IsReadByReceiveByUserId;
                var ConversionType = value.ConversionType;
                var ProfileGroupId = value.ProfileGroupId;
                var SentDateTime = value.SentDateTime;

                var IsAttachment = value.IsAttachment;
                if (IsAttachment == null) {
                    IsAttachment = "";
                }
                var OriginalFileName = value.OriginalFileName;
                if (OriginalFileName == null) {
                    OriginalFileName = "";
                }
                var StoreFileName = value.StoreFileName;
                if (StoreFileName == null) {
                    StoreFileName = "";
                }

                if (SentByProfileId == hiddenLoginUserProfileId) {
                    var LoginUserName = GetLoginUserFullName();
                    var LoginUserImagePath = GetLoginUserImageURL();

                    if (IsAttachment != "false" && StoreFileName != "" && OriginalFileName != "") {


                        $("#UserPersonalChatArea").append("<div class='answer left'> <div class='avatar' >\
                            <img src='"+ LoginUserImagePath + "' alt='" + LoginUserName + "'>\
                                <div status '></div>\
                        </div>\
                                <div class='name'>"+ LoginUserName + "</div>\
                            <div class='text'> <a target='_blank' class='FileDocumentClassQuickChat' href='/images/quickchat/" + StoreFileName + "'> " + OriginalFileName + "</a> </div>\
                            <div class='time'>"+ SentDateTime + "</div>\
                            </div > ");


                    }
                    else {

                        $("#UserPersonalChatArea").append("<div class='answer left'> <div class='avatar' >\
                            <img src='"+ LoginUserImagePath + "' alt='" + LoginUserName + "'>\
                                <div status '></div>\
                        </div>\
                                <div class='name'>"+ LoginUserName + "</div>\
                            <div class='text'> "+ ConversionText + " </div>\
                            <div class='time'>"+ SentDateTime + "</div>\
                            </div > ");


                    }

                }
                else {

                    if (IsReadByReceiveByProfileId == false) {
                        if ($("#UserPersonalChatArea .unreadMessageLabelInCommunicationArea").length == 0) {

                            $("#UserPersonalChatArea").append(" \
                                    <h3 class='unreadMessageLabelInCommunicationArea' >UNREAD </h3> \
                                    ");
                        }
                    }
                    var SelectedUserName = GetSelectedUserFullName();
                    var SelectedUserImagePath = GetSelectedUserImageURL();

                    if (IsAttachment != "false" && StoreFileName != "" && OriginalFileName != "") {

                        $("#UserPersonalChatArea").append("\
                            <div class='answer right receiveMessageIremClass unreadByUser-" + IsReadByReceiveByProfileId + "' ConversionSendToId=" + ConversionSendToId + ">\
                            <div class='avatar' >\
                            <img src='"+ SelectedUserImagePath + "' alt='" + SelectedUserName + "'>\
                                <div class='status ChatAreaUserOnlineOffLine-"+ SentByProfileId + "'></div>\
                        </div>\
                                <div class='name'>"+ SelectedUserName + "</div>\
                            <div class='text'> <a target='_blank' class='FileDocumentClassQuickChat' href='/images/quickchat/" + StoreFileName + "'> " + OriginalFileName + "</a> </div>\
                            <div class='time'>"+ SentDateTime + "</div>\
                            </div > ");



                    } else {


                        $("#UserPersonalChatArea").append("\
                            <div class='answer right receiveMessageIremClass unreadByUser-" + IsReadByReceiveByProfileId + "' ConversionSendToId=" + ConversionSendToId + ">\
                            <div class='avatar' >\
                            <img src='"+ SelectedUserImagePath + "' alt='" + SelectedUserName + "'>\
                                <div class='status ChatAreaUserOnlineOffLine-"+ SentByProfileId + "'></div>\
                        </div>\
                                <div class='name'>"+ SelectedUserName + "</div>\
                            <div class='text'> " + ConversionText + "</div>\
                            <div class='time'>"+ SentDateTime + "</div>\
                            </div > ");


                    }

                }
                if (totalRecordCount == currentRecord) {
                    if ($("#UserPersonalChatArea .unreadMessageLabelInCommunicationArea").length > 0) {

                        $('#UserPersonalChatArea').animate({
                            scrollTop: $("#UserPersonalChatArea .unreadMessageLabelInCommunicationArea").offset().top - 400
                        }, 10);
                        OnWindowScroll();
                    }
                    else {
                        CommunicationScrolToLastMessage()
                        OnWindowScroll();
                    }
                }
            });

            $("#msg").removeAttr("disabled")
            $("#SentMessage").removeAttr("disabled")
            $("#SentAttachment").removeAttr("disabled")

            if (totalRecordCount == currentRecord) {
                if ($("#UserPersonalChatArea .unreadMessageLabelInCommunicationArea").length > 0) {


                    $('#UserPersonalChatArea').animate({
                        scrollTop: $("#UserPersonalChatArea .unreadMessageLabelInCommunicationArea").offset().top - 400
                    }, 10);

                    //OnWindowScroll();
                }
                else {

                    CommunicationScrolToLastMessage()
                    //OnWindowScroll();
                }
            }

            GetSetUserUnreadCount();
        }




        chatHub.client.AppandLoadedUserPersonalConversion = function (data, afterAppendLastConSendToId, remainingCount) {


            var hiddenLoginUserProfileId = GetLoginUserId()
            var hiddenToUserProfileId = GetSelectedUserId();
            var totalRecordCount = data.length
            var currentRecord = 0;

            var old_height = $("#UserPersonalChatArea").prop('scrollHeight'); //store document height before modifications
            var old_scroll = $("#UserPersonalChatArea").scrollTop(); //remember the scroll position

            $("#UserPersonalChatArea").prepend(" \
                                    <h6 class='lodedBorderDiv' ></h6> \
                                    ");

            $.each(data, function (key, value) {
                currentRecord = currentRecord + 1;

                var ConversionId = value.ConversionId;
                var ConversionSendToId = value.ConversionSendToId;

                var SentByProfileId = value.SentByUserId;
                var SentByMobileNumber = value.SentByMobileNumber;

                var ReceiveByProfileId = value.ReceiveByUserId;
                var ReceiveByMobileNumber = value.ReceiveByMobileNumber;

                var ConversionText = value.ConversionText;
                var IsReadByReceiveByProfileId = value.IsReadByReceiveByUserId;
                var ConversionType = value.ConversionType;
                var ProfileGroupId = value.ProfileGroupId;
                var SentDateTime = value.SentDateTime;

                var IsAttachment = value.IsAttachment;
                if (IsAttachment == null) {
                    IsAttachment = "";
                }
                var OriginalFileName = value.OriginalFileName;
                if (OriginalFileName == null) {
                    OriginalFileName = "";
                }
                var StoreFileName = value.StoreFileName;
                if (StoreFileName == null) {
                    StoreFileName = "";
                }

                if (SentByProfileId == hiddenLoginUserProfileId) {
                    var LoginUserName = GetLoginUserFullName();
                    var LoginUserImagePath = GetLoginUserImageURL();

                    if (IsAttachment != "false" && StoreFileName != "" && OriginalFileName != "") {

                        $("#UserPersonalChatArea").prepend("<div class='answer left'> <div class='avatar' >\
                            <img src='"+ LoginUserImagePath + "' alt='" + LoginUserName + "'>\
                                <div status '></div>\
                        </div>\
                                <div class='name'>"+ LoginUserName + "</div>\
                            <div class='text'> <a target='_blank' class='FileDocumentClassQuickChat' href='/images/quickchat/" + StoreFileName + "'> " + OriginalFileName + "</a> </div>\
                            <div class='time'>"+ SentDateTime + "</div>\
                            </div > ");

                    }
                    else {
                        $("#UserPersonalChatArea").prepend("<div class='answer left'> <div class='avatar' >\
                            <img src='"+ LoginUserImagePath + "' alt='" + LoginUserName + "'>\
                                <div status '></div>\
                        </div>\
                                <div class='name'>"+ LoginUserName + "</div>\
                            <div class='text'> "+ ConversionText + " </div>\
                            <div class='time'>"+ SentDateTime + "</div>\
                            </div > ");

                    }

                    var dadadaa123 = $("#UserPersonalChatArea").prop('scrollHeight');
                    $("#UserPersonalChatArea").scrollTop(old_scroll + dadadaa123 - old_height);
                }
                else {

                    if (IsReadByReceiveByProfileId == false) {

                        if ($("#UserPersonalChatArea .unreadMessageLabelInCommunicationArea").length == 0) {

                            $("#UserPersonalChatArea").prepend(" \
                                    <h3 class='unreadMessageLabelInCommunicationArea' >UNREAD </h3> \
                                    ");
                            var dadadaa123 = $("#UserPersonalChatArea").prop('scrollHeight');
                            $("#UserPersonalChatArea").scrollTop(old_scroll + dadadaa123 - old_height);
                        }
                    }
                    var SelectedUserName = GetSelectedUserFullName();
                    var SelectedUserImagePath = GetSelectedUserImageURL();
                    if (IsAttachment != "false" && StoreFileName != "" && OriginalFileName != "") {
                        $("#UserPersonalChatArea").prepend("\
                            <div class='answer right receiveMessageIremClass unreadByUser-" + IsReadByReceiveByProfileId + "' ConversionSendToId=" + ConversionSendToId + ">\
                            <div class='avatar' >\
                            <img src='"+ SelectedUserImagePath + "' alt='" + SelectedUserName + "'>\
                                <div class='status ChatAreaUserOnlineOffLine-"+ SentByProfileId + "'></div>\
                        </div>\
                                <div class='name'>"+ SelectedUserName + "</div>\
                            <div class='text'> <a target='_blank' class='FileDocumentClassQuickChat' href='/images/quickchat/" + StoreFileName + "'> " + OriginalFileName + "</a> </div>\
                            <div class='time'>"+ SentDateTime + "</div>\
                            </div > ");
                    }
                    else {
                        $("#UserPersonalChatArea").prepend("\
                            <div class='answer right receiveMessageIremClass unreadByUser-" + IsReadByReceiveByProfileId + "' ConversionSendToId=" + ConversionSendToId + ">\
                            <div class='avatar' >\
                            <img src='"+ SelectedUserImagePath + "' alt='" + SelectedUserName + "'>\
                                <div class='status ChatAreaUserOnlineOffLine-"+ SentByProfileId + "'></div>\
                        </div>\
                                <div class='name'>"+ SelectedUserName + "</div>\
                            <div class='text'> " + ConversionText + "</div>\
                            <div class='time'>"+ SentDateTime + "</div>\
                            </div > ");
                    }
                    var dadadaa123 = $("#UserPersonalChatArea").prop('scrollHeight');
                    $("#UserPersonalChatArea").scrollTop(old_scroll + dadadaa123 - old_height);

                }
                if (totalRecordCount == currentRecord) {

                    if (remainingCount != "0") {

                        if ($("#UserPersonalChatArea .loadMoreCommunication").length == 0) {

                            $("#UserPersonalChatArea").prepend(" \
                                    <h3 id='loadMorePerCommunication' LastConversionId='"+ afterAppendLastConSendToId + "' class='loadMoreCommunication-" + afterAppendLastConSendToId + "' >LOAD MORE </h3> \
                                    ");

                            $("#UserPersonalChatArea .loadMoreCommunication-" + afterAppendLastConSendToId + "").click(function () {
                                var lastConversionId = $(this).attr("LastConversionId");
                                loadMorePersonalConversion(lastConversionId)
                            });

                            var dadadaa123 = $("#UserPersonalChatArea").prop('scrollHeight');
                            $("#UserPersonalChatArea").scrollTop(old_scroll + dadadaa123 - old_height);
                        }
                        else {

                        }
                    }
                }
            });

        }










        $("#UserPersonalChatArea").scroll(function () {

            OnWindowScroll();
        });


        chatHub.client.responceConversionToRead = function (status, ConversionSendToId, SelectedUserid) {


            if (status) {
                GetSetUserUnreadCount();
            }

        };




        function loadMorePersonalConversion(lastConverSendTo) {

            var hiddenLoginUserProfileId = GetLoginUserId();
            var hiddenToUserProfileId = GetSelectedUserId();

            $("#UserPersonalChatArea #loadMorePerCommunication").remove();

            $.connection.chatHub.server.checkAndGetPersonalConversion(hiddenLoginUserProfileId, hiddenToUserProfileId, lastConverSendTo)

        }


        function receiverScrolWhenRecceiveMessage() {
            var windowHeight = $(window).height() + $(window).scrollTop() + 10;
            var windowTopScrol = $(window).scrollTop()
            var conversionItemTopPosition = $("#UserPersonalChatArea .receiveMessageIremClass:last").position().top
            var unreadConversionPosition = $("#UserPersonalChatArea .unreadByUser-false:first").position().top

            if (conversionItemTopPosition <= unreadConversionPosition) {
                if (conversionItemTopPosition < windowHeight) {
                    CommunicationScrolToLastMessage()
                }
            }
        }

        function OnWindowScroll() {

            var hiddenPersonalCommunicationSelectedProfileId = GetSelectedUserId();
            var elem = document.getElementById('UserPersonalChatArea');

            var windowHeight = $("#UserPersonalChatArea").height() - 0;
            var windowTopScrol = elem.scrollTop;

            $("#UserPersonalChatArea .unreadByUser-false").each(function () {

                var conversionItemTopPosition = $(this).position().top + 0;

                var elementTop = document.getElementById('UserPersonalChatArea').offsetTop
                var divTop = $(this).position().top

                var elementRelativeTop = elementTop + divTop;

                // $("#selectedUserUnreadCountNEW").text("conTopIN---" + parseInt(conversionItemTopPosition) + ",,winHeiIN" + parseInt(windowHeight) + "")

                if (parseInt(conversionItemTopPosition) < parseInt(windowHeight)) {

                    var ConversionSendToId = $(this).attr("ConversionSendToId")


                    $(this).removeClass("unreadByUser-false")
                    SelectedUserUnreadMessage(ConversionSendToId)

                }
            });

        }


        function SelectedUserUnreadMessage(ConversionSendToId) {

            if (ConversionSendToId != "" && ConversionSendToId != null && ConversionSendToId != "0") {


                chatHub.server.setConversionToRead(ConversionSendToId, GetSelectedUserId());
            }

        }

        function GetOldPersonalConversion() {

            var hiddenLoginUserProfileId = GetLoginUserId();
            var hiddenToUserProfileId = GetSelectedUserId();
            $.connection.chatHub.server.checkAndGetPersonalConversion(hiddenLoginUserProfileId, hiddenToUserProfileId, "0")
        }

        function SetUserCountLabel(objUserUnreadMessageCountModelList) {


            $.each(objUserUnreadMessageCountModelList, function (key, value) {


                var selectedUserIdForUnReadCount = value.SelectedUserId;
                var UnreadyMessageCount = value.UnreadMessageCount;

                if (UnreadyMessageCount == "0") {
                    $("#UserUnreadMessageCount-" + selectedUserIdForUnReadCount + "").hide();
                    $("#UserUnreadMessageCount-" + selectedUserIdForUnReadCount + "").html(UnreadyMessageCount)

                }
                else {
                    $("#UserUnreadMessageCount-" + selectedUserIdForUnReadCount + "").show();
                    $("#UserUnreadMessageCount-" + selectedUserIdForUnReadCount + "").html(UnreadyMessageCount)
                }

            });


        }


        function GetSetUserUnreadCount() {
            var LoginUserId = GetLoginUserId();
            var UserPortalId = GetPortalId();
            $.connection.chatHub.server.getAndSetUserUnreadyCount(UserPortalId, LoginUserId);

        }


        function ChangeSelectedUser(selectedUserId) {

            $("#<%= HiddenSelectedUserId.ClientID %>").val(selectedUserId);
            $("#UserPersonalChatArea").html("");
            GetOldPersonalConversion();
        }



        $(".user-left-section").click(function () {

            var selectedUserId = $(this).attr("ClearActionChatUserId")

            $("#<%= HiddenSelectedUserId.ClientID %>").val(selectedUserId);
            $("#UserPersonalChatArea").html("");
            GetOldPersonalConversion();

        });


        $('#<%= FileUploadAttachment.ClientID %>').on('change', function (e) {

            var files = e.target.files;

            if (files.length > 0) {
                if (window.FormData !== undefined) {
                    var data = new FormData();

                    for (var x = 0; x < files.length; x++) {
                        data.append("file" + x, files[x]);
                    }

                    alert(data)



                    var month = 2;
                    var year = 2018;
                    alert("CALL WEB METHOD");

                    $.ajax({
                        url: '../DesktopModules/ClearAction_Chat/ClearActionChatFileUpload.aspx/UploadedImages',
                        type: 'POST',
                        data: {},
                        processData: false,
                        contentType: 'json',

                        success: function (dataR) {
                            debugger
                            alert(dataR.d)
                            alert("success")
                        },

                        error: function (xhr, ajaxOptions, thrownError) {

                            debugger
                            alert("Error")
                        },

                    });


                }
            }

        });






        $(document).submit(function () {
            $("#SentMessage").trigger("click");
            return false;
        });



        function SetOnlineUser(UserId) {

            $("#UserOnlineOfflineMode-" + UserId + "").removeClass("offline");
            $("#UserOnlineOfflineMode-" + UserId + "").addClass("online");

            $(".ChatAreaUserOnlineOffLine-" + UserId + "").removeClass("offline");
            $(".ChatAreaUserOnlineOffLine-" + UserId + "").addClass("online");

            $("#chatSelectedUserName").html(GetSelectedUserFullName());
        }






        function CommunicationScrolToLastMessage() {
            var elem = document.getElementById('UserPersonalChatArea');
            elem.scrollTop = elem.scrollHeight;
        }


        function GetPortalId() {
            return $("#<%= HiddenUserPOrtalId.ClientID %>").val();
        }

        function GetLoginUserId() {
            return $("#<%= HiddenUserId.ClientID %>").val();
        }

        function GetLoginUserFullName() {
            return $("#<%= HiddenUserFullName.ClientID %>").val();
    }

    function GetLoginUserImageURL() {
        return $("#<%= HiddenFieldImageURL.ClientID %>").val();
    }

    function GetSelectedUserId() {
        return $("#<%= HiddenSelectedUserId.ClientID %>").val();
        }

        function GetSelectedUserFullName() {
            return $("#UserFullName-" + GetSelectedUserId() + "").html();
        }

        function GetSelectedUserImageURL() {

            return $("#imgUser-" + GetSelectedUserId() + "").attr("src");
        }

        function OpenFileUpload() {
            alert("OPFUL")
            $("#<%= FileUploadAttachment.ClientID %>").trigger("click");

        }

    });
</script>

<asp:HiddenField ID="HiddenUserPOrtalId" runat="server" />
<asp:HiddenField ID="HiddenUserId" runat="server" />
<asp:HiddenField ID="HiddenUserFullName" runat="server" />
<asp:HiddenField ID="HiddenFieldImageURL" runat="server" />

<asp:HiddenField ID="HiddenSelectedUserId" runat="server" />
<asp:HiddenField ID="HiddenFieldConnectionId" runat="server" />

<br />





<div id="ChatSection" class="content container-fluid bootstrap snippets">
    <div class="row row-broken">
        <div class="col-sm-3 col-xs-12">
            <div class="col-inside-lg decor-default border-top-solid chat chat-left" style="overflow: hidden; outline: none;" tabindex="5000">
                <div class="chat-users" id="chat-users">
                    <h6>User List</h6>

                    <asp:Repeater ID="RepterUserList" runat="server">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="user user-left-section" clearactionchatuserid='<%#Eval("UserId") %>'>
                                <div class="avatar">
                                    <img id="imgUser-<%#Eval("UserId") %>" src='<%#Eval("UserPhotoURL") %>' alt='<%#Eval("FullName") %>'>
                                    <div class="status offline" id="UserOnlineOfflineMode-<%#Eval("UserId") %>"></div>
                                </div>
                                <div id="UserFullName-<%#Eval("UserId") %>" class="name"><%#Eval("FullName") %></div>
                                <div class="mood"><%#Eval("UserName") %> <span class="user-unready-message-count" id="UserUnreadMessageCount-<%#Eval("UserId") %>">0</span> </div>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="col-sm-9 col-xs-12">

            <div class="col-inside-lg col-inside-lg-right decor-default border-top-solid">
                <h3 id="chatSelectedUserName" class="chatSelectedUserName">No User Selected</h3>
                <div id="UserPersonalChatArea" class="chat-body chat" style="overflow: hidden; outline: none;" tabindex="5001">
                </div>
                <div class="chat-body">
                    <div class="answer-add">
                        <form action="#" method="post">
                            <input placeholder="Write a message" id="msg">
                            <span class="answer-btn answer-btn-1" id="SentMessage"></span>
                            <span class="answer-btn answer-btn-2" id="SentAttachment"></span>

                        </form>


                        <asp:FileUpload ID="FileUpload1" runat="server" OnDataBinding="FileUpload1_DataBinding" Visible="false" />

                        <asp:UpdatePanel runat="server" Visible="false">
                            <ContentTemplate>
                                <asp:FileUpload ID="FileUploadAttachment" runat="server" OnDataBinding="FileUploadAttachment_DataBinding" />

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="UcmdSave" EventName="click" />
                                <%--  <asp:PostBackTrigger ControlID="UcmdSave"  />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:LinkButton ID="UcmdSave" Visible="false" ValidationGroup="Update" Text="Save" runat="server" CssClass="Formbtn btnsave savebtn" OnClick="UcmdSave_Click" />
                    </div>
                </div>
            </div>

        </div>

    </div>
</div>
</div>



