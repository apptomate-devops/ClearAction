<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcControl.ascx.cs" Inherits="ClearAction.Modules.Dashboard.UcControl" %>


<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/context/jquery.contextMenu.js"></script>
<link href="/DesktopModules/ClearAction_SolveSpaces/Scripts/context/jquery.contextMenu.css" rel="stylesheet" />
<script src="https://static.filestackapi.com/v3/filestack.js"></script>

<%--<script src="/DesktopModules/ClearAction_Dashboard/Tipr/tipr.js"></script>
<link href="/DesktopModules/ClearAction_Dashboard/Tipr/tipr.css" rel="stylesheet" />
<script>
    $(document).ready(function () {
        $('.tip').tipr();
    });
</script>--%>


<script>
    function AlertWindow(head, content) {
        $.alert({
            title: head,
            type: 'blue',
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

    var toUserId;
    var IsEmail;
    var viewerId = '<%=UserController.Instance.GetCurrentUserInfo().UserID%>';
    var baseServicepath = '/DesktopModules/ClearAction_Connections/API/Connections/';
    var oKey = '<%=GetAPIKey%>';// 'AUQ4R8zKvSKCs9gudWJlxz';
    function addFile() {
        var fsClient = filestack.init(oKey);
        fsClient.pick({}).then(function (response) {

            response.filesUploaded.forEach(function (file) {
                var ofileresponse = file;
                var url = ofileresponse.url;



                $("#HdnAttachUrl").text(url);

                $("#FileName").html(ofileresponse.filename);

                $("#DivAttach").hide();


            });

        });

    }

    function RemoveFriend() {

        $.ajax({
            type: "POST",
            cache: false,
            url: baseServicepath + 'RemoveFriend',
            beforeSend: null,
            data: { friendId: toUserId }
        }).done(function (data) {
            if (data.Result === "success") {

                SuccessWindow('Confirmation', 'User has been removed from your friend list');
                window.location.href = '/Home';
            }
            else {
                AlertWindow('Warning', 'Person either not in your friend list or it already removed.');

            }
        }).fail(function (xhr, status) {

        });
    };
    function addFriend() {

        $.ajax({
            type: "POST",
            cache: false,
            url: baseServicepath + 'AddFriend',
            beforeSend: null,
            data: { friendId: toUserId }
        }).done(function (data) {
            if (data.Result === "success") {

                SuccessWindow('Confirmation', 'Request has been sent to user.');

            } else {

                AlertWindow('Warning', 'Person already in your friend list or pending approval.');
            }
        }).fail(function (xhr, status) {


            AlertWindow('Warning', 'Something went wrong please do try later.');
        });
    };

    function ResetControl() {
        $("#FileName").html('');
        $("#txtSubject").val('');
        $("#HdnAttachUrl").val('');
        $("#txtMessage").val('');
       
        $("#loading").hide();
        $("#DivAttach").show();
        $("#divShowMessage").hide();
        $("#msgShowList").html('');

        $('#contactemail').modal('hide'); 

    }

    function GetMessageList() {
        $("#msgShowList").html('Loading your message...');
        var strMsg = "";
        $.ajax({
            type: "GET",
            cache: false,
            url: baseServicepath + 'GetMessage',
            beforeSend: null,
            data: { friendId: toUserId }
        }).done(function (member) {
            debugger;
            var omsgLst = member.GetPrivateMessage;
            for (var i = 0; i < omsgLst.length; i++) {
                var message = omsgLst[i];
                if (message != null) {
                    if (message.SenderUserID == viewerId)
                        strMsg += "You (<small>" + message.DisplayDateTime + "</small>) &nbsp;:&nbsp;&nbsp;";
                    else
                        strMsg += "<b>" + message.From + "</b> (<small>" + message.DisplayDateTime + "</small>) &nbsp;:&nbsp;&nbsp;";

                    strMsg += (message.Body != null ? message.Body.replace("<Attach>", "...") : "");

                    strMsg += "<br/>"


                }

            }
            $("#msgShowList").html('');
            $("#msgShowList").html(strMsg);
            $("#divShowMessage").show();





        }).fail(function (xhr, status) {
            $("#divShowMessage").html('');

            //  response({});
        });

    };

    function SendEmailToUser() {
        var sSubject = $("#txtSubject").val();
        var sBody = $("#txtMessage").val();

        var sAttachFileName = $("#FileName").html();
        sAttachFileName = sAttachFileName.trim();
        var sFileStackLink = $("#HdnAttachUrl")[0].innerText;
        sFileStackLink = sFileStackLink.trim();

        $('#loading').show();
        if ((sSubject == '' || sBody.trim() == '') && IsEmail == '1') {
            AlertWindow('Warning', 'Sorry!! Subject/Body is required field.');
            return;
        }

        if (sSubject == '' && IsEmail == '0') {
            AlertWindow('Warning', 'Sorry!! message is required field.');
            return;
        }

        var mmethod = "SendMessage";
        if (IsEmail == '1')
        { mmethod = "SendEmail"; }
        else {
            sBody = sSubject;
            sSubject = "Private Message";
        }
        $.ajax({
            type: "GET",
            cache: false,
            url: baseServicepath + mmethod,
            beforeSend: null,
            data: {
                FromUserID: viewerId,
                ToUserID: toUserId,
                Subject: sSubject,
                Body: sBody,
                Filename: sAttachFileName,
                AttachLink: sFileStackLink
            }
        }).done(function (data) {
            if (data.Result === "success") {

                ResetControl();
                if (IsEmail == '0')
                    SuccessWindow('Confirmation', 'Your Message has been sent succesfully');
                else
                    SuccessWindow('Confirmation', 'Your Email has been sent succesfully');

                $('#contactemail').modal('hide'); 
                $('#loading').hide();
            } else {
                AlertWindow('Warning', 'Something went wrong please do try later');
                $('#loading').hide();
            }
        }).fail(function (xhr, status) {
            $('#loading').hide();
        });
    };

    $(window).load(function () {

        $.contextMenu({
            selector: '.CA_UserMenu',
            build: function ($trigger, e) {

                var sectionid = $trigger.attr('sectionid');
                var status = false;
                var isconnection = false;
                if ($trigger.attr('isconnection') == 'true')
                    isconnection = true;
                $("#loading").hide();
                if (sectionid == '1') {
                    return {

                        callback: function (key, options) {
                            ResetControl();

                            if (key == "email") {
                                var curreleID = this.attr("uid");

                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");

                                IsEmail = '1';
                                toUserId = curreleID;

                                $("#txtMessage").show();
                                $("#header").html("<h2>Email</h2>");
                                $("#txtSubject").attr("placeholder", "Subject");

                             //   var modal = document.getElementById('myModal');
                                // modal.style.display = "block";
                                var span = document.getElementById("close");
                                 $('#contactemail').modal('show');
                                span.onclick = function () {
                                    $("#txtSubject").val('');
                                   
                                };

                                //   window.open("/MyProfile/UserId/" + curreleID + "/DoPrint/yes");
                            }
                            if (key == "sendamessage") {
                                var curreleID = this.attr("uid");
                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");

                                IsEmail = '0';
                                toUserId = curreleID;

                                $("#txtMessage").hide();
                                $("#header").html("<h2>Message </h2>");
                                $("#txtSubject").attr("placeholder", "Send message");




                                toUserId = curreleID;

                                GetMessageList();

                            //    var modal = document.getElementById('myModal');
                             //   modal.style.display = "block";
                            //    var span = document.getElementById("close");
                            //    span.onclick = function () {
                             //       $("#txtSubject").val('');
                            //        modal.style.display = "none";
                            //    };

                                var span = document.getElementById("close");
                                $('#contactemail').modal('show');
                                span.onclick = function () {
                                    $("#txtSubject").val('');

                                };

                            }
                            if (key == "reportblock") {

                                RI_ShowIssue('Profile', $('#' + curreleID + '').val());
                            }
                            if (key == "call") {
                                var Phone_CID = $(this).attr("uphone");
                                var Selected_CID = $(this).attr("uid");
                                window.open("tel:" + Phone_CID);

                            }
                            if (key == "Print") {
                                var curreleID = this.attr("uid");
                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");

                                window.open("/MyProfile/UserId/" + curreleID + "/DoPrint/yes");
                            }



                            if (key == "addconnection") {

                                var curreleID = this.attr("uid");
                                toUserId = curreleID;
                                addFriend();
                            }
                            if (key == "removeconnection") {

                                var curreleID = this.attr("uid");
                                toUserId = curreleID;
                                RemoveFriend();
                            }

                            if (key == "converttoPDF" || key == "Downloadprofile") {
                                var curreleID = this.attr("uid");
                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");
                                window.open("/MyProfile/UserId/" + curreleID + "/DownloadProfile/yes");
                            }
                        },
                        items:
                        {
                            "addconnection": { name: "Add Connection", disabled: isconnection },
                            "removeconnection": { name: "Remove Connection", disabled: !isconnection },
                            "sendamessage": { name: "Send Message", disabled: false },
                            //  "call": { name: "call", disabled: false },
                            "email": { name: "Email", disabled: false },
                            //"share": { name: "share", disabled: false },
                            //"reportblock": { name: "Report / block", disabled: false },
                            // "converttoPDF": { name: "Convert to PDF", disabled: false },
                            "Print": { name: "Print", disabled: false },
                            "Downloadprofile": { name: "Download profile", disabled: false }
                        }
                    };
                }

                if (sectionid == '2') {
                    return {

                        callback: function (key, options) {

                            ResetControl();
                            if (key == "email") {
                                var curreleID = this.attr("uid");
                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");

                                IsEmail = '1';
                                toUserId = curreleID;

                                $("#txtMessage").show();
                                $("#header").html("<h2>Email</h2>");
                                $("#txtSubject").attr("placeholder", "Subject");

                                //var modal = document.getElementById('myModal');
                                //modal.style.display = "block";
                                //var span = document.getElementById("close");
                                //span.onclick = function () {
                                //    $("#txtSubject").val('');
                                //    modal.style.display = "none";
                                //};


                                var span = document.getElementById("close");
                                $('#contactemail').modal('show');
                                span.onclick = function () {
                                    $("#txtSubject").val('');

                                };
                                //   window.open("/MyProfile/UserId/" + curreleID + "/DoPrint/yes");
                            }
                            if (key == "sendamessage") {
                                var curreleID = this.attr("uid");
                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");
                              //  var modal = document.getElementById('myModal');

                                IsEmail = '0';
                                toUserId = curreleID;

                                $("#txtMessage").hide();
                                $("#header").html("<h2>Message</h2>");
                                $("#txtSubject").attr("placeholder", "Send message");


                                toUserId = curreleID;

                                GetMessageList();

                                //var modal = document.getElementById('myModal');
                                //modal.style.display = "block";
                                //var span = document.getElementById("close");
                                //span.onclick = function () {
                                //    $("#txtSubject").val('');
                                //    modal.style.display = "none";
                                //};
                                var span = document.getElementById("close");
                                $('#contactemail').modal('show');
                                span.onclick = function () {
                                    $("#txtSubject").val('');

                                };

                                //   window.open("/MyProfile/UserId/" + curreleID + "/DoPrint/yes");
                            }

                            if (key == "call") {
                                var Phone_CID = $(this).attr("uphone");
                                var Selected_CID = $(this).attr("uid");
                                window.open("tel:" + Phone_CID);

                            }
                            if (key == "Print") {
                                var curreleID = this.attr("uid");
                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");

                                window.open("/MyProfile/UserId/" + curreleID + "/DoPrint/yes");
                            }



                            if (key == "reportblock") {

                                RI_ShowIssue('Profile', $('#' + curreleID + '').val());
                            }


                            if (key == "removeconnection") {

                                var curreleID = this.attr("uid");
                                toUserId = curreleID;
                                RemoveFriend();
                            }
                            if (key == "converttoPDF" || key == "Downloadprofile") {
                                var curreleID = this.attr("uid");
                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");
                                window.open("/MyProfile/UserId/" + curreleID + "/DownloadProfile/yes");
                            }
                        },
                        items: {

                            "sendamessage": { name: "Send Message", disabled: false },
                            //  "call": { name: "call", disabled: false },
                            "email": { name: "Email", disabled: false },
                            "removeconnection": { name: "Remove Connection", disabled: isconnection },
                            //  "share": { name: "share", disabled: false },
                            //  "reportblock": { name: "Report / block", disabled: false },
                            //        "converttoPDF": { name: "Convert to PDF", disabled: false },
                            "Print": { name: "Print", disabled: false },
                            "Downloadprofile": { name: "Download Profile", disabled: false }
                        }
                    };
                }


                if (sectionid == '3') {
                    return {

                        callback: function (key, options) {
                            ResetControl();



                            if (key == "email") {
                                var curreleID = this.attr("uid");

                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");

                                IsEmail = '1';
                                toUserId = curreleID;

                                $("#txtMessage").show();
                                $("#header").html("<h2>Email</h2>");
                                $("#txtSubject").attr("placeholder", "Subject");
                                //var modal = document.getElementById('myModal');
                                //modal.style.display = "block";
                                //var span = document.getElementById("close");
                                //span.onclick = function () {
                                //    $("#txtSubject").val('');
                                //    modal.style.display = "none";
                                //};

                                var span = document.getElementById("close");
                                $('#contactemail').modal('show');
                                span.onclick = function () {
                                    $("#txtSubject").val('');

                                };

                            }
                            if (key == "sendamessage") {
                                var curreleID = this.attr("uid");
                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");

                                IsEmail = '0';
                                toUserId = curreleID;

                                $("#txtMessage").hide();
                                $("#header").html("<h2>Message</h2>");
                                $("#txtSubject").attr("placeholder", "Send message");
                                toUserId = curreleID;

                                GetMessageList();

                                //var modal = document.getElementById('myModal');
                                //modal.style.display = "block";
                                //var span = document.getElementById("close");
                                //span.onclick = function () {
                                //    $("#txtSubject").val('');
                                //    modal.style.display = "none";
                                //};
                                var span = document.getElementById("close");
                                $('#contactemail').modal('show');
                                span.onclick = function () {
                                    $("#txtSubject").val('');

                                };
                                //   window.open("/MyProfile/UserId/" + curreleID + "/DoPrint/yes");
                            }

                            if (key == "call") {
                                var Phone_CID = $(this).attr("uphone");
                                var Selected_CID = $(this).attr("uid");
                                window.open("tel:" + Phone_CID);

                            }

                            if (key == "reportblock") {

                                RI_ShowIssue('Profile', $('#' + curreleID + '').val());
                            }
                            if (key == "Print") {
                                var curreleID = this.attr("uid");
                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");

                                window.open("/MyProfile/UserId/" + curreleID + "/DoPrint/yes");
                            }



                            if (key == "addconnection") {

                                var curreleID = this.attr("uid");
                                toUserId = curreleID;
                                addFriend();
                            }
                            if (key == "removeconnection") {

                                var curreleID = this.attr("uid");
                                toUserId = curreleID;
                                RemoveFriend();
                            }

                            if (key == "converttoPDF" || key == "Downloadprofile") {
                                var curreleID = this.attr("uid");

                                window.open("/MyProfile/UserId/" + curreleID + "/DownloadProfile/yes");
                            }
                        },
                        items: {
                            "addconnection": { name: "Add Connection", disabled: isconnection },
                            "removeconnection": { name: "Remove Connection", disabled: !isconnection },
                            "sendamessage": { name: "Send Message", disabled: false },
                            //   "call": { name: "call", disabled: false },
                            "email": { name: "Email", disabled: false },
                            //    "share": { name: "share", disabled: false },
                            //   "reportblock": { name: "Report / block", disabled: false },
                            //   "converttoPDF": { name: "Convert to PDF", disabled: false },
                            "Print": { name: "Print", disabled: false },
                            "Downloadprofile": { name: "Download profile", disabled: false }
                        }
                    };
                }

            },
            trigger: 'left'
        });
    });

    jQuery(document).ajaxStart(function () {
        try {
            // showing a modal
            $("#loadingtop").modal();

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
        $("#loadingtop").modal('hide');
    });
</script>

<div id="loadingtop" data-bind="visible: loadingData"   style="display: none; width: 100%; height: 100%; top: 100px; left: 0px; position: fixed; z-index: 10000; text-align: center;">
     <img src="../images/progress.gif"  alt="Loading..." style="position: fixed; top: 50%; left: 50%;" />
</div>


<style type="text/css">
    /* The Modal (background) */
    .modalpopup {
        display: none; /* Hidden by default */
        position: fixed; /* Stay in place */
        z-index: 1; /* Sit on top */
        left: 30%;
        top: 20%;
        padding-left: 20px;
        width: 30%; /* Full width */
        height: 50%; /* Full height */
        overflow: auto; /* Enable scroll if needed */
        background-color: rgb(0,0,0); /* Fallback color */
        background-color: #e6e6e6; /* Black w/ opacity */
    }

    /* Modal Content */
    .modalpopup-content {
        margin: auto;
        padding: 20px;
        border: 1px solid #888;
        width: 100%;
    }

    /* The Close Button */
    .close {
        color: #aaaaaa;
        float: right;
        font-size: 28px;
        font-weight: bold;
    }

        .close:hover,
        .close:focus {
            color: #000;
            text-decoration: none;
            cursor: pointer;
        }
</style>

<div class="modal fade" role="dialog" id="contactemail" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 id="AlertHeader" class="Share"> <span id="header"></span>
                    <asp:Label ID="lblUserName" runat="server"></asp:Label></h4>
            </div>
            <div class="modal-body">
                
                <div id="divShowMessage" style="overflow-y: auto; max-height: 200px; color: #000">
                    <span id="msgShowList" style="width: 500px !important; padding: 10px !important; max-height: 600px;"></span>
                </div>
                <div>
                    <input type="text" placeholder="Subject" maxlength="150" class="form-control" id="txtSubject"><br />
                    <textarea placeholder="Message" id="txtMessage" style="word-wrap: break-word; resize: horizontal;" maxlength="500" class="form-control">
                </textarea><br />
                    <input type="hidden" id="HdnAttachUrl" />

                    <div id="DivAttach"><a class="btn btn-default" onclick="addFile()">Attach</a></div>
                    <br />
                    <p>
                        <a class="btn btn-primary" onclick="SendEmailToUser()">Send</a>&nbsp;&nbsp;&nbsp;&nbsp;
                 
        <span id="close" class="label label-danger" style="cursor: pointer" data-dismiss="modal">Close</span>
                    </p>
                    <span id="FileName" />

                    <div id="loading">
                        <center>  <img src='<%= ResolveUrl("/images/loading.gif") %>'  /></center>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<%--<div data-toggle="buttons" class="modalpopup" id="myModal">
    <div style="width: 96%">
    </div>


</div>--%>
