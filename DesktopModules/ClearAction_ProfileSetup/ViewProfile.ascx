<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewProfile.ascx.cs" Inherits="ClearAction.Modules.ProfileClearAction_ProfileSetup.ViewProfile" EnableViewState="true" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>

<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/context/jquery.contextMenu.js"></script>
<link href="/DesktopModules/ClearAction_SolveSpaces/Scripts/context/jquery.contextMenu.css" rel="stylesheet" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.0/jquery-confirm.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.0/jquery-confirm.min.js"></script>

<script src="/DesktopModules/ClearAction_ProfileSetup/js/croppie.js"></script>
<link href="/DesktopModules/ClearAction_ProfileSetup/CSS/croppie.css" rel="stylesheet" />

<link rel="stylesheet" type="text/css" href="<%=ModulePath%>module.css">

<script src="https://static.filestackapi.com/v3/filestack.js"></script>

<%--<script type="text/javascript" src="jquery.print.js"></script>--%>

<script type="text/javascript">

    function AlertWindow(head, content) {
        $.alert({
            title: head,
            content: content
        });
    }

    function SuccessWindow(head, content) {
        $.alert(
            {
                title: head,
                content: content
            });
    }

    function printPage() {

        var divElements = "<html><head><title></title></head><body>" + $('#printme').html() + "</body></html>";// document.getElementById('<%=ltrlVisible.ClientID%>').innerHTML;

        var WinPrint = window.open('', '', 'left=0,top=0,width=900,height=600,toolbar= 0, scrollbars = 0, status = 0');
        WinPrint.document.write(divElements);
        WinPrint.document.close();
        WinPrint.focus();
        WinPrint.print();
        WinPrint.close();
        prtContent.innerHTML = oldPage;
        //Restore orignal HTML
        document.body.innerHTML = oldPage;
        return false;
    }
</script>
<div id="printme" style="display: none">
    <asp:Literal ID="ltrlVisible" runat="server" />

</div>
<script type="text/javascript">
    function Side_Show() {
        $("#dvMore").toggle();//.show();
    }

    function RI_ShowIssueInner(a, b) {
        //Call Report Div
        RI_ShowIssue(a, b);

    }
    function GetMessageList() {
        var strMsg = "";
        $.ajax({
            type: "GET",
            cache: false,
            url: baseServicepath + 'GetMessage',
            beforeSend: null,
            data: { friendId: toUserId }
        }).done(function (member) {
 

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
            $("#msgShowList").html(strMsg);
            $("#divShowMessage").show();
 
        }).fail(function (xhr, status) {


            //  response({});
        });

    };
</script>


<div class="insights_wrapper" id="printDiv">
    <div class="col-sm-12">

        <div class="row">
            <div class="col-md-12">
                <div class="solve-spaces-top-nav">
                    <h1>
                        <asp:Label ID="lblDisplayName" runat="server"></asp:Label>
                    </h1>

                </div>
            </div>
        </div>

        <div class="row ">

            <div class="clearfix"></div>
            <div class="insights_Header col-sm-12">

                <div class="insights-row" style="padding-top: 40px">

                    <div style="float: left">
                        <img alt="images" id="profilepic" runat="server" height="157" width="157">
                        <br />
                        <div class="social-icon">
                            <a href="#" runat="server" id="lnkEdin" target="_blank"><i class="fa fa-linkedin" aria-hidden="true"></i></a>&nbsp;&nbsp;&nbsp;
                              <a href="#" runat="server" id="lnkFacebook" target="_blank"><i class="fa fa-facebook" aria-hidden="true"></i></a>&nbsp;&nbsp;&nbsp;
                             <a href="#" runat="server" id="lnkTwitter" target="_blank"><i class="fa fa-twitter" aria-hidden="true"></i></a>

                        </div>


                    </div>

                    <div class="col-sm-4">
                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                        <br />
                        <span>
                            <asp:Label ID="lblCompanyName" runat="server"></asp:Label>
                        </span>
                        <br />
                        <span>Role: 
                            <asp:Label ID="lblRole" runat="server"></asp:Label></span><br />
                        <span>Member Since
                            <asp:Label ID="lblMemberSince" runat="server"></asp:Label></span><br />
                        <span>
                            <asp:Label ID="lblLocation" runat="server"></asp:Label></span><br />
                        <span>
                            <asp:Label ID="lblPhone" runat="server" Text="N/A"></asp:Label></span><br />
                    </div>

                    <div id="dvMore" class="col-sm-6 more" style="display: none">

                        <div class="col-sm-6">
                            <a id="hrefAddConnection" runat="server" onclick="addFriend()">
                                <img src="/DesktopModules/ClearAction_ProfileSetup/images/add-1.png" alt="favorite">
                            </a>
                            <br />
                            <a id="hrefRemoveConnection" runat="server" onclick="RemoveFriend()">
                                <img src="/DesktopModules/ClearAction_ProfileSetup/images/remove.png" alt="favorite">
                            </a>

                            <%--<asp:ImageButton ID="imgMessage" runat="server" OnClick="imgMessage_Click" ImageUrl="~/DesktopModules/ClearAction_ProfileSetup/images/message.png" />--%><br />
                            <a id="imgMessage">
                                <img src="/DesktopModules/ClearAction_ProfileSetup/images/message.png" alt="favorite">
                            </a>
                            <br />
                            <a id="imgCall" runat="server">
                                <img src="/DesktopModules/ClearAction_ProfileSetup/images/call.png" alt="favorite">
                            </a>
                            <br />

                            <a id="mailto" runat="server" target="_blank">
                                <img src="/DesktopModules/ClearAction_ProfileSetup/images/email.png" alt="favorite">
                            </a>
                        </div>
                        <div class="col-sm-6">

                            <asp:ImageButton ID="imgShare" runat="server" OnClick="imgShare_Click" ImageUrl="~/DesktopModules/ClearAction_ProfileSetup/images/share.png" Enabled="false" /><br />
                            <asp:ImageButton ID="imgReport" runat="server" OnClientClick="return RI_ShowIssueInner('Profile', 'Report Profile issue');" ImageUrl="~/DesktopModules/ClearAction_ProfileSetup/images/report.png" /><br />
                            <asp:ImageButton ID="imgPdf" runat="server" OnClick="imgPdf_Click" ImageUrl="~/DesktopModules/ClearAction_ProfileSetup/images/pdf.png" Visible="false" />
                            <asp:ImageButton ID="imgPrint" runat="server" OnClientClick="printPage(); return false;" ImageUrl="~/DesktopModules/ClearAction_ProfileSetup/images/print.png" /><br />
                            <asp:ImageButton ID="imgDownload" runat="server" OnClick="imgDownload_Click" ImageUrl="~/DesktopModules/ClearAction_ProfileSetup/images/download.png" /><br />


                        </div>
                    </div>


                    <div class="tooltips">

                        <asp:ImageButton ID="imgbtnShare" runat="server" Visible="false" OnClientClick="Side_Show(); return false;" CssClass="imgshare" ImageUrl="~/DesktopModules/ClearAction_ProfileSetup/images/more_icon.png" />
                        <asp:Image runat="server" ID="imgUserMenu" CssClass="CA_UserMenu" data-toggle="modal" data-target="#"  Style="cursor: pointer;" src="/DesktopModules/ClearAction_ProfileSetup/images/more_icon.png" alt="Tips" />

                    </div>

                </div>


            </div>
        </div>




        <div class="row">
            <div class="clearfix"></div>
            <div class="insights_container col-sm-8">

                <h3>Overview | Bio</h3>
                <br />
                <div class="myProfile">
                    <asp:Label ID="lblOverview" runat="server" />
                </div>


                <br />
                <br />


            </div>



        </div>


        <div class="row" id="dvEducation">
            <div class="clearfix"></div>
            <div class="insights_container col-sm-8">

                <h3>Education</h3>

                <div class="myProfile">
                    <asp:Label ID="lblEducation" runat="server">MBA, Marketing, Finance, International Business
Vanderbilt University</asp:Label>
                </div>


            </div>
        </div>


        <div class="row">
            <div class="clearfix"></div>
            <div class="insights_container col-sm-8 ">

                <h3>Work History</h3>

                <div class="myProfile">
                    <asp:Label ID="lblWorkHistory" runat="server"></asp:Label>
                </div>



            </div>
        </div>


        <div class="row">
            <div class="insights_container col-sm-8">
                <h3>Professional Associations</h3>


                <div class="myProfile">
                    <asp:Label ID="lblProfessionalAssociates" runat="server"></asp:Label>
                </div>


            </div>
        </div>


        <div class="row">
            <div class="insights_container col-sm-8">
                <h3>Honors & Awards</h3>
                <div class="myProfile">

                    <asp:Label runat="server" ID="lblHonorsRewards" />

                </div>


            </div>
        </div>


    </div>

</div>
<div id="loadingtop"   style="display: none; width: 100%; height: 100%; top: 100px; left: 0px; position: fixed; z-index: 10000; text-align: center;">
     <img src='<%= ResolveUrl("/images/progress.gif") %>'   alt="Loading..." style="position: fixed; top: 50%; left: 50%;" />
</div>
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
<script>
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
            } else {
                AlertWindow('Warning', 'Person either not in your friend list or it already removed.');
            }
        }).fail(function (xhr, status) {
            AlertWindow('Server Error', 'Something went wrong on server');
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
            AlertWindow('Warning', 'Something went wrong please do try later');
        });
    };

    function cropProfilePicture() {
        var cropImage = document.getElementById('imageUpload');
        var img = new Croppie(cropImage, {
            showZoomer: false, zoom: '25%', viewport: { width: 100, height: 100 }
        });
        img.bind();

    }

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
    var toUserId = '<%=ToUserID%>';
    var viewerId = '<%=UserId%>';
    var baseServicepath = '/DesktopModules/ClearAction_Connections/API/Connections/';
    function SendEmailToUser() {
        var sSubject = $("#txtSubject").val();
        var sBody = $("#txtMessage").val();

        var sAttachFileName = $("#FileName").html();
        sAttachFileName = sAttachFileName.trim();
        var sFileStackLink = $("#HdnAttachUrl")[0].innerText;
        sFileStackLink = sFileStackLink.trim();


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

                
                $("#FileName").html('');
                $("#txtSubject").val('');
                $("#HdnAttachUrl").val('');
                $("#txtMessage").val('');
                $("#DivAttach").show();
                 
                $('#contactemail').modal('hide');
                
                if (IsEmail == '0')
                    SuccessWindow('Confirmation', 'Your Message has been sent succesfully');
                else
                    SuccessWindow('Confirmation', 'Your Email has been sent succesfully');

                //$('#myModal').hide();

            } else {
                AlertWindow('Warning', 'Something went wrong please do try later');
            }
        }).fail(function (xhr, status) {

        });
    };
    // Get the modal


    // Get the button that opens the modal
    var btn = document.getElementById("imgMessage");

    // Get the <span> element that closes the modal
    var span = document.getElementById("close");

    // When the user clicks the button, open the modal 
    btn.onclick = function () {
        modal.style.display = "block";
    }

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {

        var sSubject = $("#txtSubject").val();
        var sBody = $("#txtMessage").val();
        if (sSubject != '' || sBody.trim() != '') {
            $.confirm(
                {
                    title: 'Cancel Send?',
                    content: 'Are you sure want to cancel?!',
                    buttons: {
                        confirm: function () {
                            modal.style.display = "none";
                        },
                        cancel: function () {

                        }

                    }
                });

        }
        else
            modal.style.display = "none";





    }

    

    $(window).load(function () {
        $.contextMenu({
            selector: '.CA_UserMenu',
            build: function ($trigger, e) {

                var sectionid = 2;
                var status = false;
                var isconnection = false;
                var IsSelf = (toUserId == viewerId);
                if ($trigger.attr('isconnection') == 'true')
                    isconnection = true;
                if (toUserId == viewerId)
                    sectionid = '3';
                if (toUserId != viewerId)
                    sectionid == '1';

                $("#loading").hide();
                if (sectionid == '1') {
                    return {

                        callback: function (key, options) {

                            $("#txtMessage").val('');
                            $("#txtSubject").val('');

                            $("#divShowMessage").hide();
                            $("#msgShowList").html('');
                            if (key == "sendamessage") {
                                var curreleID = this.attr("uid");
                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");
                                //var modal = $("#myModal");
                                //modal.show();
                                //var span = document.getElementById("close");


                                IsEmail = '0';
                             //   span.onclick = function () {
                              //      modal.hide();
                             //   }
                                toUserId = curreleID;
                                $("#txtMessage").hide();
                                $("#header").html("<h2>Message</h2>");
                                $("#txtSubject").attr("placeholder", "Send message");
                                toUserId = curreleID;



                                GetMessageList();
                                $("#txtSubject").focus();

                                var span = document.getElementById("close");
                                $('#contactemail').modal('show');
                                span.onclick = function () {
                                    $("#txtSubject").val('');


                                };

                                //   window.open("/MyProfile/UserId/" + curreleID + "/DoPrint/yes");
                            }
                            if (key == "email") {
                                var curreleID = this.attr("uid");

                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");
                              //  var modal = $("#myModal");
                              //  modal.show();
                             ////   var span = document.getElementById("close");
                             //   span.onclick = function () {
                             //      modal.hide();
                             //    }
                                IsEmail = '1';
                                toUserId = curreleID;
                           
                                $("#txtMessage").show();
                                $("#header").html("<h2>Email to:</h2>");
                                $("#txtSubject").attr("placeholder", "Subject");

                                $("#msgShowList").html('');
                                $("#divShowMessage").hide();
                                $("#txtSubject").focus();

                                var span = document.getElementById("close");
                                $('#contactemail').modal('show');
                                span.onclick = function () {
                                    $("#txtSubject").val('');

                                };

                                //   window.open("/MyProfile/UserId/" + curreleID + "/DoPrint/yes");
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

                            if (key == "reportuser") {

                                RI_ShowIssue('Profile', $('#' + curreleID + '').val());
                            }

                            if (key == "blockuser") {

                                //    var curreleID = this.attr("uid");
                                // toUserId = curreleID;
                                AlertWindow('Not implmeneted', 'Not yet done');
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
                            //     "share": { name: "share", disabled: false },
                            //    "reportuser": { name: "Report User", disabled: false },
                            //    "blockuser": { name: "block User", disabled: false },
                            //     "converttoPDF": { name: "Convert to PDF", disabled: false },
                            "Print": { name: "Print", disabled: false },
                            "Downloadprofile": { name: "Download profile", disabled: false }
                        }
                    };
                }
                if (sectionid == '2') {
                    return {

                        callback: function (key, options) {

                            $("#txtMessage").val('');
                            $("#txtSubject").val('');
                            if (key == "sendamessage") {
                                var curreleID = this.attr("uid");
                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");
                                //var modal = $("#myModal");
                                //modal.show();
                                var span = document.getElementById("close");
                                IsEmail = '0';
                                span.onclick = function () {
                                    modal.hide();
                                }
                                $("#txtMessage").hide();
                                $("#header").html("<h2>Message</h2>");
                                $("#txtSubject").attr("placeholder", "Send message");
                                toUserId = curreleID;


                                GetMessageList();
                                $("#txtSubject").focus();

                                var span = document.getElementById("close");
                                $('#contactemail').modal('show');
                                span.onclick = function () {
                                    $("#txtSubject").val('');

                                };


                                //   window.open("/MyProfile/UserId/" + curreleID + "/DoPrint/yes");
                            }
                            if (key == "email") {
                                var curreleID = this.attr("uid");

                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");
                               // var modal = $('#myModal');
                              //  modal.show();
                                var span = document.getElementById("close");
                                
                                IsEmail = '1';
                                toUserId = curreleID;

                                $("#txtMessage").show();
                                $("#header").html("<h2>Email</h2>");
                                $("#txtSubject").attr("placeholder", "Subject");

                                $("#msgShowList").html('');
                                $("#divShowMessage").hide();
                                $("#txtSubject").focus();

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


                            if (key == "reportuser") {

                                RI_ShowIssue('Profile', $('#' + curreleID + '').val());
                            }

                            if (key == "blockuser") {

                                //    var curreleID = this.attr("uid");
                                // toUserId = curreleID;
                                AlertWindow('Not implmeneted', 'Not yet done');
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
                        items: {
                            "addconnection": { name: "Add Connection", disabled: isconnection },
                            "removeconnection": { name: "Remove Connection", disabled: !isconnection },
                            "sendamessage": { name: "Send Message", disabled: false },
                            //    "call": { name: "call", disabled: false },
                            "email": { name: "Email", disabled: false },

                            //   "share": { name: "share", disabled: false },
                            //     "reportuser": { name: "Report User", disabled: false },
                            //     "blockuser": { name: "Block User", disabled: false },
                            //   "converttoPDF": { name: "Conver to PDF", disabled: false },
                            "Print": { name: "Print", disabled: false },
                            "Downloadprofile": { name: "Download profile", disabled: false }
                        }
                    };
                }


                if (sectionid == '3') {
                    return {

                        callback: function (key, options) {

                            $("#txtMessage").val('');
                            $("#txtSubject").val('');
                            if (key == "sendamessage") {
                                var curreleID = this.attr("uid");
                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");
                               // var modal = $('#myModal');
                             //   modal.show();
                               // var span = document.getElementById("close");
                                IsEmail = '0';
                                
                                $("#txtMessage").hide();
                                $("#header").html("<h2>Message</h2>");
                                $("#txtSubject").attr("placeholder", "Send message");
                                toUserId = curreleID;
                                GetMessageList();
                                $("#txtSubject").focus();

                                var span = document.getElementById("close");
                                $('#contactemail').modal('show');
                                span.onclick = function () {
                                    $("#txtSubject").val('');

                                };

                            }
                            if (key == "email") {
                                var curreleID = this.attr("uid");

                                // curreleID = curreleID.replace("imgUserMenu", "hdnSub");
                             //   var modal = $('#myModal');
                              //  modal.show();
                               // var span = document.getElementById("close");
                              //  span.onclick = function () {
                             //       modal.hide();
                              //  }
                                IsEmail = '1';
                                toUserId = curreleID;

                                $("#txtMessage").show();
                                $("#header").html("<h2>Email</h2>");
                                $("#txtSubject").attr("placeholder", "Subject");

                                $("#msgShowList").html('');
                                $("#divShowMessage").hide();
                                $("#txtSubject").focus();

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
                            "addconnection": { name: "Add Connection", disabled: true },
                            "removeconnection": { name: "Remove Connection", disabled: true },
                            "sendamessage": { name: "Send Message", disabled: true },
                            "call": { name: "Call", disabled: true },
                            "email": { name: "Email", disabled: true },
                            //   "share": { name: "share", disabled: false },
                            // "reportuser": { name: "Report User", disabled: false },
                            //   "blockuser": { name: "Block User", disabled: false },
                            //  "converttoPDF": { name: "Conver to PDF", disabled: false },
                            "Print": { name: "Print", disabled: false },
                            "Downloadprofile": { name: "Download profile", disabled: false }
                        }
                    };
                }

            },
            trigger: 'left'
        });

    });

</script>
