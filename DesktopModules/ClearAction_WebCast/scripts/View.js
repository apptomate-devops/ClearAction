$(function () {
   function GetLikes(id) {
        //   alert(GetLikesCount(id));
        // alert(GetLikesCount(id));
        return false;
    }
    // Define Context Menu
    $.contextMenu({
        selector: '.CA_UserMenu',
        build: function ($trigger, e) {
            var IsSelfAssigned = $trigger.attr('IsSelfAssigned');
            var ismyvault = $trigger.attr('IsMyValut');
            var IsDeletedshow = false;
            if (ismyvault == '1' && IsSelfAssigned == '1')
                IsDeletedshow = true;
            var iseditallow = false;
            if ($trigger.attr('isEditAllow') != undefined && $trigger.attr('isEditAllow') == 'True')
                iseditallow = true;
            var ComponentId = $trigger.attr('ComponentId');
            var oAddDisable = false;
            if (ismyvault == '1')
                oAddDisable = true;
            var cid = $trigger.attr('cid');
            if (IsSelfAssigned == '1') {
                return {
                    callback: function (key, options) {
                        if (key == "Remove4MyVault") {
                            var Selected_TID = $(this).attr("cid");
                            CA_RemoveFromMyVault(Selected_TID, ComponentId);
                        }

                        if (key == "ReportIssue") {
                            var btitle = $(this).parent().parent().find(".BRITitlehdn").val();

                            RI_ShowIssue('Digital Events', btitle);
                        }
                    },
                    items: {
                        "Add2MyVault": { name: "Add to My Vault", disabled: oAddDisable },
                        "Remove4MyVault": { name: "Remove from My Vault", disabled: !IsDeletedshow },
                        "sep1": "---------",
                        "ReportIssue": { name: "Report an Issue", disabled: false }
                    }
                };
            }
            else if (IsSelfAssigned == '0') {
                return {
                    callback: function (key, options) {
                        if (key == "Add2MyVault") {
                            var Selected_ID = $(this).attr("cid");
                            CA_Assign2Me(Selected_ID, ComponentId);
                        }
                        if (key == "ReportIssue") {
                            var btitle = $(this).parent().parent().find(".BRITitlehdn").val();
                            RI_ShowIssue('Digital Event', btitle);
                        }
                    },
                    items: {
                        "Add2MyVault": { name: "Add to My Vault", disabled: oAddDisable },
                        "Remove4MyVault": { name: "Remove from My Vault", disabled: !IsDeletedshow },
                        "sep1": "---------",
                        "ReportIssue": { name: "Report an Issue", disabled: false }
                    }
                };
            }
            else if (IsSelfAssigned == '2') {
                return {
                    callback: function (key, options) {
                        if (key == "AttachMedia") {
                            var commentID = $(this).attr("commentid");
                            var WccID = $(this).attr("cid");
                            //  alert('Comment:' + cid);
                            EditPostFile(this, commentID, WccID);

                        }
                        if (key == "EditReply") {

                            var commentID = $(this).attr("commentid");
                            var WccID = $(this).attr("cid");
                            EditPostReply(this, commentID, WccID);

                            ShowAttachList(commentID, 'topview', 'show');
                        }
                        if (key == "DeleteReply") {
                            var btitle = $(this).parent().parent().find(".BRITitlehdn").val();
                        }
                    },
                    items: {
                        "AttachMedia": { name: "Attach Media", disabled: oAddDisable },
                        "EditReply": { name: "Edit My Reply", disabled: !iseditallow },
                        "sep1": "---------"
                    }
                };
            }
            else if (IsSelfAssigned == '3') {
                return {
                    callback: function (key, options) {
                        if (key == "AttachMedia") {
                            

                            var commentID = $(this).attr("commentid");
                            var WccID = $(this).attr("cid");
                            //  alert('Comment:' + cid);
                            EditPostFile(this, commentID, WccID);
                        }
                        if (key == "EditReply") {
                            var btitle = $(this).parent().parent().find(".BRITitlehdn").val();
                            alert('Implementation require');
                        }

                        if (key == "DeleteReply") {
                            var btitle = $(this).parent().parent().find(".BRITitlehdn").val();
                            alert('Implementation require');
                        }
                    },
                    items: {
                        "AttachMedia": { name: "Attach Media", disabled: oAddDisable },
                    }
                };
            }
            else if (IsSelfAssigned == '-1') { // Added By Anuj For Attachment Menu : Top level
                return {
                    callback: function (key, options) {
                        if (key == "FileUpload") {
                            var Selected_CID = $(this).attr("cid");
                            AddFileToTempHolder(this, -1, Selected_CID);
                        }
                    },
                    items: {
                        "FileUpload": { name: "Attach a File", disabled: false },
                    }
                };
            }
            else if (IsSelfAssigned == '5') { // Added By Anuj For First Level Reply
                return {
                    callback: function (key, options) {
                        if (key == "FileUpload") {
                            var Selected_CID = $(this).attr("commentid");
                            var WCCID = $(this).attr("cid");
                            AddFileToTempHolder(this, Selected_CID, Selected_CID);
                        }
                    },
                    items: {
                        "FileUpload": { name: "Attach a File", disabled: false },
                    }
                };
            }


            else if (IsSelfAssigned == '6') {
                return {
                    callback: function (key, options) {
                        if (key == "AttachMedia") {
                            var commentID = $(this).attr("commentid");
                            var WccID = $(this).attr("cid");
                            //  alert('Comment:' + cid);

                            var flag = false;
                            addPostFile(this, commentID, WccID);

                        }
                        if (key == "EditReply") {
                            var commentID = $(this).attr("commentid");
                            var WccID = $(this).attr("cid");
                            //    EditPostReply(this, commentID, WccID);
                            $("#childaction_" + commentID).show();
                            ShowAttachList(commentID, 'UploadedAttachmentEdit', 'show');
                            //$("#UploadedAttachmentEdit_" + commentID).show();
                        }
                        if (key == "DeleteReply") {
                            var btitle = $(this).parent().parent().find(".BRITitlehdn").val();
                        }
                    },
                    items: {
                        "AttachMedia": { name: "Attach Media", disabled: oAddDisable },
                        "EditReply": { name: "Edit My Reply", disabled: !iseditallow },
                        "sep1": "---------"
                    }
                };
            }
            else if (IsSelfAssigned == '7') { // Added By Anuj For Attachment Menu : Top level
                return {
                    callback: function (key, options) {
                        if (key == "FileUpload") {
                            var Selected_CID = $(this).attr("commentid");
                            var WCCID = $(this).attr("cid");
                            AddFileToTempHolder(this, Selected_CID, Selected_CID);
                       }
                    },
                    items: {
                        "FileUpload": { name: "Attach a File", disabled: false },
                    }
                };
            }
            else if (IsSelfAssigned == '8') { // Added By Anuj For Attachment Menu : Top level
                return {
                    callback: function (key, options) {
                        if (key == "FileUpload") {
                            var Selected_CID = $(this).attr("cid");
                            AddFileToTempHolder(this, -1, Selected_CID);
                       }
                    },
                    items: {
                        "FileUpload": { name: "Attach a File", disabled: false },
                    }
                };
            }
            else if (IsSelfAssigned == '9') {
                return {
                    callback: function (key, options) {
                        if (key == "AttachMedia") {
                            var commentID = $(this).attr("commentid");
                            var WccID = $(this).attr("cid");
                            //  alert('Comment:' + cid);
                            EditPostFile(this, commentID, WccID);


                        }
                        if (key == "EditReply") {

                            var commentID = $(this).attr("commentid");
                            var WccID = $(this).attr("cid");
                            //    EditPostReply(this, commentID, WccID);
                            $("#childaction_" + commentID).show();
                            ShowAttachList(commentID, 'childLastLevel', 'show');
                        }
                        if (key == "DeleteReply") {
                            var btitle = $(this).parent().parent().find(".BRITitlehdn").val();
                        }
                    },
                    items: {
                        "AttachMedia": { name: "Attach Media", disabled: oAddDisable },
                        "EditReply": { name: "Edit My Reply", disabled: !iseditallow },
                        "sep1": "---------"
                    }
                };
            }
        },
        trigger: 'left'
    });
    setTimeout(ExpandTheTopicRepliedIfAny, 1000)
});

function EditReplyCancle(element) {
    var contId = $(element).attr("cid")
    $(".ReplyEditContentSection_" + contId + "").hide();
    $(".ReplyEditContentSection_" + contId + " .ReplyEditContent").text();
    $(".ReplyContentSection_" + contId + "").html($("#HiddenReplyEditContent_" + contId + "").val());
    $("#HiddenReplyEditContent_" + contId + "").val("")
    $(".ReplyContentSection_" + contId + "").show();
    $(".SeeAttachedMediaLinkContent[cid='" + contId + "']").show();
    $("#UploadedAttachment").hide();
    $("#childaction_" + contId).hide();
    $("#child_" + contId).hide();
}


function EditPostReply(ele, contId, replyToId) {


    if ($("#HiddenReplyEditContent_" + contId + "").val() == "") {
        var contentClass = "ReplyContentSection_" + contId + "";



        var EditPostContent = $("." + contentClass + "").clone().children().remove().end().text();
        EditPostContent = $.trim(EditPostContent);
        var EditPostContentAttachmentData = "";
        $("." + contentClass + "").find(".AfterAttachmentSestion").each(function () {

            EditPostContentAttachmentData = "" + EditPostContentAttachmentData + "" + $(this)[0].outerHTML + "";
        });



        $(".ReplyEditContentSection_" + contId + " .ReplyEditContentAttachmentData").text(EditPostContentAttachmentData);
        $(".ReplyEditContentSection_" + contId + " .ReplyEditContentAttachmentData").val(EditPostContentAttachmentData);


        $("#HiddenReplyEditContent_" + contId + "").val($("." + contentClass + "").html());


        $(".ReplyContentSection_" + contId + "").html(EditPostContentAttachmentData);

        $(".ReplyContentSection_" + contId + "").hide();

        $(".EditReplyAttachmentSection_" + contId + "").html(EditPostContentAttachmentData);


        $("." + contentClass + "").clone().children().remove().end().text('')
        $(".ReplyEditContentSection_" + contId + "").show();
        //alert(EditPostContent)
        $(".ReplyEditContentSection_" + contId + " .ReplyEditContent").text(EditPostContent);

        $(".ReplyEditContentSection_" + contId + " .ReplyEditContent").val(EditPostContent);

        //alert($(".ReplyEditContentSection_" + contId + " .ReplyEditContent").text())

        $(".EditReplyAttachmentSection_" + contId + " .AfterAttachmentSestion").prepend("<span class=AttachedFileText>Attached file : </span>")
        $(".AttchmentListSection_" + contId + "").html("");

        $(".SeeAttachedMediaLinkContent[cid='" + contId + "']").hide();
    }

}

var AttachmentArray = [];

function EditPostFile(ele, contId, wccid) {
    var currentContentId = "dvEachTopicContentId_" + contId + "";
    var parentContentId = $("." + currentContentId + "").closest('.mainContentSections').attr('contentid');
    var fsClient = filestack.init(oKey);
    fsClient.pick({}).then(function (response) {
        response.filesUploaded.forEach(function (file) {
            var ofileresponse = file;
            var uploadServiceUrl = webMethodUrl + 'SaveFileStackData';
            $.ajax({type: 'GET',dataType: "json", contentType: "application/json; charset=utf-8",
                url: uploadServiceUrl,
                data: {
                    ContentID: contId,CommentID: wccid,fileName: ofileresponse.filename,
                    contentType: ofileresponse.mimetype,size: ofileresponse.size,statckurl: ofileresponse.url
                },
                success: function () {
                    var CurrentUserId = $("#hiddenUserId").val();
                    var UploadStringHTMLSavePost = "<div class='AfterAttachmentSestion'> <a contId='" + contId + "' class='AfterAttachment' UploadedByUserId='" + CurrentUserId + "' href='" + ofileresponse.url + "' target='_blank'>" + ofileresponse.filename + "</a> <a UploadedByUserId='" + CurrentUserId + "'  RemoveAttachmentURLAfterPost='" + ofileresponse.url + "' class='attachmentAfterRemoveButton' onClick='RemoveAttachmentURLAfterPost(this)' contId='" + contId + "'> &nbsp &nbsp &nbsp </a> </div>";

                    SaveAttachmentToReplyPost(contId, ofileresponse.filename, UploadStringHTMLSavePost, ofileresponse.url);



                },
                xhrFields: {
                    onprogress: function (progress) {
                    }
                }
                // contentType: ofileresponse.mimetype
            }).done(function (response) {
            });
        });
    });
}

function AddFileToTempHolder(ele, contid, WccID) {
    var fsClient = filestack.init(oKey);
    fsClient.pick({}).then(function (response) {
        response.filesUploaded.forEach(function (file) {
            var ofileresponse = file;

            AttachmentArray.push({
                contentid: contid,
                currentContentId: WccID,
                attachmentURL: ofileresponse.url,
                contenttype: ofileresponse.mimetype,
                size: ofileresponse.size,
                filename: ofileresponse.filename
            });
            var CurrentUserId = $("#hiddenUserId").val();
            var UploadStringHTML = "<div class='BeforAttachmentSestion'> <a contId='" + contid + "' class='BeforeAttachment' UploadedByUserId='" + CurrentUserId + "' href='" + ofileresponse.url + "' target='_blank'>" + ofileresponse.filename + "</a> <a UploadedByUserId='" + CurrentUserId + "'  RemoveAttachmentURLBeforePost='" + ofileresponse.url + "' class='attachmentBeforRemoveButton' onClick='removeAttachmentURLBeforePost(this)' contId='" + contid + "' > &nbsp &nbsp &nbsp </a> </div>";
            if (contid == -1) {
                $("#UploadedAttachment").append(UploadStringHTML);
            }
            else if (contid > 0) {
                $("#UploadedAttachment_" + contid).append(UploadStringHTML);
            }
            alert('File is uploaded successfully');
        });
    });
}

function addPostFile(ele, contId, commentid) {
    var currentContentId = "dvEachTopic_" + contId + "";
    var parentContentId = commentid;//$("." + currentContentId + "").closest('.mainContentSections').attr('contentid');
    var fsClient = filestack.init(oKey);
    var blankContentId = 0;
    fsClient.pick({}).then(function (response) {
        response.filesUploaded.forEach(function (file) {
            var ofileresponse = file;
            var uploadServiceUrl = webMethodUrl + 'SaveFileStackData';
            $.ajax({
                type: 'GET',dataType: "json",contentType: "application/json; charset=utf-8",
                url: uploadServiceUrl,
                data: { contentId: contId, CommentID: commentid, FileName: ofileresponse.filename, contentType: ofileresponse.mimetype, size: ofileresponse.size, statckurl: ofileresponse.url },
                success: function () {
                    var CurrentUserId = $("#hiddenUserId").val();
                    var topicParentClass = ".dvEachTopicContentId_" + contId + "";
                    var UploadStringHTML = "<div class='BeforAttachmentSestion'> <a contId='" + contId + "' class='BeforeAttachment' UploadedByUserId='" + CurrentUserId + "' href='" + ofileresponse.url + "' target='_blank'>" + ofileresponse.filename + "</a> <a UploadedByUserId='" + CurrentUserId + "'  RemoveAttachmentURLBeforePost='" + ofileresponse.url + "' class='attachmentBeforRemoveButton' onClick='removeAttachmentURLBeforePost(this)' contId='" + contId + "' > &nbsp &nbsp &nbsp </a> </div>";
                    $("#UploadedAttachment").append(UploadStringHTML);
                    var UploadStringHTMLSavePost = "<div class='AfterAttachmentSestion'> <a contId='" + contId + "' class='AfterAttachment' UploadedByUserId='" + CurrentUserId + "' href='" + ofileresponse.url + "' target='_blank'>" + ofileresponse.filename + "</a> <a UploadedByUserId='" + CurrentUserId + "'  RemoveAttachmentURLAfterPost='" + ofileresponse.url + "' class='attachmentAfterRemoveButton' onClick='RemoveAttachmentURLAfterPost(this)' contId='" + contId + "'> &nbsp &nbsp &nbsp </a> </div>";
                    var uploadListHTML = $(" .UploadedAttachmentSavePost").val();
                    var finalUploadContentHTML = "" + uploadListHTML + "" + UploadStringHTMLSavePost + "";
                    $(" .UploadedAttachmentSavePost").val(UploadStringHTMLSavePost);
                    AttachmentArray.push({
                        contentid: parentContentId,
                        currentContentId: contId,
                        attachmentURL: ofileresponse.url,
                        contenttype: ofileresponse.mimetype,
                        size: ofileresponse.size,
                        filename: ofileresponse.filename
                    });
                    alert('File is uploaded successfully');
                },
                xhrFields: {
                    onprogress: function (progress) {
                    }
                }
            }).done(function (response) {

                GetFilesList(parentContentId);
            });
        });
    });
}

function removeAttachmentURLBeforePost(e) {
    if (confirm("Are you sure want to remove attachment?")) {
        var attachmentURL = $(e).attr('removeattachmenturlbeforepost');
        var contId = $(e).attr('contId')
        var currentContentId = "dvEachTopicContentId_" + contId + "";
        var parentContentId = $("." + currentContentId + "").closest('.mainContentSections').attr('contentid');
        $('.BeforAttachmentSestion a[href="' + attachmentURL + '"]').hide();
        $('.BeforAttachmentSestion a[removeattachmenturlbeforepost="' + attachmentURL + '"]').hide();
        var UpdateAttachmentArray = [];
        $.each(AttachmentArray, function () {
            if (this.attachmentURL != attachmentURL) {
                UpdateAttachmentArray.push({
                    contentid: this.contentid,
                    currentContentId: this.currentContentId,
                    attachmentURL: this.attachmentURL,
                    contenttype: this.contenttype,
                    size: this.size,
                    filename: this.filename
                });
            }
        });

        AttachmentArray = UpdateAttachmentArray;
        if (attachmentURL != "") {
            var uploadServiceUrl = webMethodUrl + 'DeleteAttachmentsFromURL';
            $.ajax({
                type: 'GET', dataType: "json",contentType: "application/json; charset=utf-8",
                url: uploadServiceUrl,
                data: { fileurl: attachmentURL },
                success: function () {
                    alert('Attachment  is removed successfully');
                    $(this).hide();
                },
                xhrFields: {
                    onprogress: function (progress) { }
                }
                // contentType: ofileresponse.mimetype
            }).done(function (response) {

            });
        }
    }
}

function SaveAttachmentToReplyPost(contId, filename, UploadStringHTMLSavePost, Attachmenturl) {
    var SaveAttachmentServiceUrl = webMethodUrl + 'GetSaveEditReplyAttachments';
    $.ajax({
        type: 'GET',dataType: "json",contentType: "application/json; charset=utf-8",
        url: SaveAttachmentServiceUrl,
        data: { contentId: contId, fileName: filename, content: UploadStringHTMLSavePost, fileurl: Attachmenturl },
        success: function () {

            var flag = true;
            if ($("#HiddenReplyEditContent_" + contId + "").val() == "") {
                flag = false;
            }


            if (flag) {

                var EditPostContentAttachmentData = $(".ReplyEditContentSection_" + contId + " .ReplyEditContentAttachmentData").val();
                EditPostContentAttachmentData = "" + UploadStringHTMLSavePost + "" + EditPostContentAttachmentData + "";
                $(".ReplyEditContentSection_" + contId + " .ReplyEditContentAttachmentData").val(EditPostContentAttachmentData)
                $(".ReplyEditContentSection_" + contId + " .ReplyEditContentAttachmentData").text(EditPostContentAttachmentData)

                var hrefPostContent = $(".ReplyEditContentSection_" + contId + " .EditPostReplyButton").attr("href")
                window.location.href = hrefPostContent
            }
            else {

                location.reload();
            }





        },
        xhrFields: {
            onprogress: function (progress) {}
        }
        // contentType: ofileresponse.mimetype
    }).done(function (response) {

    });

}

function GetAttachListFormat(islogin) {
    if (islogin)
        return '<div class="AfterAttachmentSestion"><span class="AttachedFileText">{counter}.</span><small>{DateAdded}</small> :: <a contid="{commentid}" class="AfterAttachment" uploadedbyuserid="{userid}" href="{filestackurl}" target="_blank">{filename}</a><a uploadedbyuserid= "{userid}" removeattachmenturlafterpost= "{filestackurl}" class="attachmentAfterRemoveButton" onclick= "RemoveAttachmentURLAfterPost(this)" contid= "{commentid}" >&nbsp; &nbsp; &nbsp; </a > </div>';

    return '<div class="AfterAttachmentSestion"><span class="AttachedFileText">{counter}.</span>::<small>{DateAdded}</small> :: <a contid="{commentid}" class="AfterAttachment" uploadedbyuserid="{userid}" href="{filestackurl}" target="_blank">{filename}</a></div>';


}

function ShowAttachList(contId, divid, status) {
    var PortalId = 0;
    $.ajax({
        type: "GET",contentType: "application/json; charset=utf-8", dataType: "json",
        url: webMethodUrl + "GetAttachmentByCommentID",
        data: { PortalId: PortalId, ContentPostID: contId },
    }).done(function (response) {
        var oData = JSON.parse(response);
        var strTemp = "";
        var strMainData = "";
        var icount = 0;

        var AttachmentUserId = 0;
        var currentPortalId = 0;
        var oTemplate = GetAttachListFormat();

        for (var i = 0; i < oData.length; i++) {
            strTemp = GetAttachListFormat(oData[i].UserID == $("#hiddenUserId").val());
            strTemp = strTemp.replace("{commentid}", oData[i].ContentId);
            strTemp = strTemp.replace("{userid}", oData[i].UserID);
            strTemp = strTemp.replace("{filestackurl}", oData[i].FileUrl);
            strTemp = strTemp.replace("{filename}", oData[i].FileName);
            strTemp = strTemp.replace("{DateAdded}", oData[i].DateAdded);
            //Second element repelacement
            strTemp = strTemp.replace("{commentid}", oData[i].ContentId);
            strTemp = strTemp.replace("{userid}", oData[i].UserID);
            strTemp = strTemp.replace("{filestackurl}", oData[i].FileUrl);
            strTemp = strTemp.replace("{filename}", oData[i].FileName);
            strTemp = strTemp.replace("{counter}", (i + 1).toString());
            strTemp = strTemp.replace("{DateAdded}", oData[i].DateAdded);
            icount++;
            strMainData = strMainData + strTemp;
        }
        $("#" + divid + "_" + contId).html(strMainData);
        $("#" + divid + "_" + contId).show();
    });
}

function SeeAttachedMediaList(element, divid) {
    var contId = $(element).attr("cid");
    var status = $(element).attr("status");
    if (status != 'show') {
        $("#" + divid + "_" + contId).hide();
        $(element).attr("status", 'show');
        return;
    }
    ShowAttachList(contId, divid, status);
    $(element).attr("status", 'hide');
}

function saveAttachmentDataWhenPost(buttonElement) {
    var ccId = $(buttonElement).attr("currentPostContenetId");
    var finalStringAttachment = "";
    for (index = 0; index < AttachmentArray.length; ++index) {
        if (ccId == AttachmentArray[index].currentContentId) {
            var CurStringAttachment = "{contentid:'" + AttachmentArray[index].contentid + "',currentContentId:'" + AttachmentArray[index].currentContentId + "',attachmentURL:'" + AttachmentArray[index].attachmentURL + "',contenttype:'" + AttachmentArray[index].contenttype + "',size:'" + AttachmentArray[index].size + "',filename:'" + AttachmentArray[index].filename + "'}";
            if (finalStringAttachment == "") {
                finalStringAttachment = "" + finalStringAttachment + "" + CurStringAttachment + "";
            }
            else {
                finalStringAttachment = "" + finalStringAttachment + "," + CurStringAttachment + "";
            }
        }
    }
    finalStringAttachment = "[" + finalStringAttachment + "]";
    $(".TextBoxAttachmentData").val(finalStringAttachment);
}

function RemoveAttachmentURLAfterPost(e) {
    if (confirm("Are you sure want to remove attachment?")) {
        var attachmentURL = $(e).attr('RemoveAttachmentURLAfterPost');
        var contId = $(e).attr('contId')
        var currentContentId = "dvEachTopicContentId_" + contId + "";
        var parentContentId = $("." + currentContentId + "").closest('.mainContentSections').attr('contentid');
        if (attachmentURL != "") {
            var uploadServiceUrl = webMethodUrl + 'DeleteAttachmentsFromURL';
            $.ajax({
                type: 'GET',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: uploadServiceUrl,
                data: { fileurl: attachmentURL },
                success: function () {
                    //$("#lnkSeeMore_" + contId).click();
                    alert('Attachment  is removed successfully');
                },
                xhrFields: {
                    onprogress: function (progress) {
                    }
                }
                // contentType: ofileresponse.mimetype
            }).done(function (response) {
                //hideprogress();
                GetFilesList(parentContentId);
                HideShowAttachmentLinkForUserJS();
            });
        }
    }
}

function GetFilesList(contId) {
    // var webMethodUrl = '<%=WebServicePath%>/DesktopModules/activeforums/api/ForumService/';
    var PortalId = 0;
    $.ajax({
        type: "GET",dataType: "json",contentType: "application/json; charset=utf-8",
        url: webMethodUrl + "GetAttachmentByCommentID",
        data: { PortalId: PortalId, ContentPostID: contId },
    }).done(function (response) {
        var oData = JSON.parse(response);
        var strTemp = "";
        var icount = 0;

        var AttachmentUserId = 0;
        var currentPortalId = 0;

        for (var i = 0; i < oData.length; i++) {
            var CurrentUserId = oData[i].UserId;
            if (AttachmentUserId != CurrentUserId) {
                AttachmentUserId = CurrentUserId;
                strTemp += "<li> " + oData[i].UserDisplayName + "" + '<a href="{0}" target="_blank" ><p style="float:left;width:100%;margin-bottom:0px;">{1}</p></a>'.format(oData[i].filestackurl, oData[i].FileName) + "</li>"
            }
            else {
                strTemp += "<li>" + '<a href="{0}" target="_blank" ><p style="float:left;width:100%;margin-bottom:0px;">{1}</p></a>'.format(oData[i].filestackurl, oData[i].FileName) + "</li>"
            }
            icount++;
        }

        $('#file_' + contId).html("<ul>" + strTemp + "</ul>");
        $('#filecount_' + contId).html(icount);
        HideShowAttachmentLinkForUserJS();
    });
}


function HideShowAttachmentLinkForUserJS() {



    $(".BeforAttachmentSestion").each(function (index) {
        var attachmentURL = $(this).find('.BeforeAttachment').attr('href');

        //if (attachmentURL != "") {
        //    if ($(".attachmentListRightSections a[href$='" + attachmentURL + "']").length <= 0) {
        //        $(this).hide();
        //    }
        //}

    });




    $(".AfterAttachmentSestion").each(function (index) {
        var attachmentURL = $(this).find('.AfterAttachment').attr('href');
        if (attachmentURL != "") {
            if ($(".attachmentListRightSections a[href$='" + attachmentURL + "']").length <= 0) {
                $(this).hide();
            }
        }
    });

    $(".AfterAttachmentSestion .attachmentAfterRemoveButton").each(function (index) {

        var attachmentURL = $(this).attr('UploadedByUserId');
        var CurrentUserId = $("#hiddenUserId").val();

        if (attachmentURL != CurrentUserId) {
            $(this).hide();
        }
    });

}
function getPathVariable(variable) {
    var path = location.pathname;

    var parts = path.substr(1).split('/'), value;

    while (parts.length) {
        if (parts.shift() === variable)
            value = parts.shift();
        //      else parts.shift();
    }

    return value;
}


String.prototype.format = function () {
    var str = this;
    for (var i = 0; i < arguments.length; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        str = str.replace(reg, arguments[i]);
    }
    return str;
}

$(document).ready(function () {
    $(".solve-spaces-nav a").removeClass("active");

    if (AF_IsMyVault == 1 || AF_IsMyVault == undefined) {
        $(".solve-spaces-nav a:first").addClass("active");
    } else {
        $(".solve-spaces-nav a:last").attr("class", "active");
    }

    var AF_CurrentCatID = getParameterByName('CategoryId');
    $("#CA_Blog_dvCatContainer li").removeClass("active");
    $("#CA_Blog_dvCatContainer li a[CatID='" + AF_CurrentCatID + "']").parent().addClass("active");

    //$("#CA_Blog_dvCatContainer1 li").removeClass("active");  //Commented by @SP

    if (AF_ComponentID == 1 || AF_ComponentID == undefined)
        AF_ComponentID = "-1";
    //$("#CA_Blog_dvCatContainer1 li a[EventType='" + AF_ComponentID + "']").parent().addClass("active");  //Commented by @SP
    $("#dvBlogStatusFilter li").removeClass("active");

    if (AF_CurrentStatus == undefined)
        AF_CurrentStatus = 0;
    $("#dvBlogStatusFilter li[statusid='" + AF_CurrentStatus + "']").addClass("active");
    // trap enter key of search textbox 
    $('[id*="txtSearch"]').on("keypress", function (event) {
        if (event.which == 13) {
            $('[id*="lnkSearch"]').click();
        }
    });
    CA_ListConnections();
    //A_PopuplateUsers();
})
function getParameterByName(name) {
    var url = window.location.pathname;
    if (url.lastIndexOf(name) == -1) return -1;
    var iStart = url.lastIndexOf(name) + name.length + 1;
    var iEnd = url.lastIndexOf("/SortBy");
    return url.substring(iStart, iEnd);
}


function SetScrollerPosition() {
    if (document.getElementById("XPos").value != "") {
        var x = document.getElementById("XPos").value;
        var y = document.getElementById("YPos").value;
        window.scrollTo(x, y);
    }

    document.getElementById("XPos").value = "0";
    document.getElementById("YPos").value = "0";
}

function GetScollerPosition() {
    var scrollX, scrollY;

    if (document.all) {
        if (!document.documentElement.scrollLeft)
            scrollX = document.body.scrollLeft;
        else
            scrollX = document.documentElement.scrollLeft;

        if (!document.documentElement.scrollTop)
            scrollY = document.body.scrollTop;
        else
            scrollY = document.documentElement.scrollTop;
    }
    else {
        scrollX = window.pageXOffset;
        scrollY = window.pageYOffset;
    }

    document.getElementById("XPos").value = scrollX;
    document.getElementById("YPos").value = scrollY;
}

function RecommendThisPost(ItemID) {
    $.ajax({
        type: "GET", dataType: "json", contentType: "application/json; charset=utf-8",
        url: webMethodUrl + "GetLikeContentIDCount",
        data: { ContentPostID: ItemID, ItemType: 'Post' },
    }).done(function (response) {
        var oData = JSON.parse(response);
        $(".RecomCount").text(oData.LikesCount);
        $(".UnRecomCount").text(oData.UnLikeCount);
    });
    return false;
}
function UnRecommendThisPost(ItemID) {

    $.ajax({
        type: "GET", dataType: "json", contentType: "application/json; charset=utf-8",
        url: webMethodUrl + "UnLikeThePost",
        data: { ContentPostID: ItemID, ItemType: 'Post' },
    }).done(function (response) {
        var oData = JSON.parse(response);
        $(".RecomCount").text(oData.LikesCount);
        $(".UnRecomCount").text(oData.UnLikeCount);
    });
    return false;
}

function Show(id) {
    if (document.getElementById(id).style.display == 'none') {
        try {
            //Added try captch if element id changed accidently
            document.getElementById(id).style.display = 'block';

            document.getElementById('hyper_' + id).style.display = 'none';
            document.getElementById('hyper1_' + id).style.display = 'inline';

            document.getElementById('hyper2_' + id).style.display = 'inline';
        } catch (e) {

        }
    }

    return false;
}
function Hide(id) {
    if (document.getElementById(id).style.display == 'block') {
        try {//Added try captch if element id changed accidently
            document.getElementById(id).style.display = 'none';
            document.getElementById('hyper_' + id).style.display = 'inline';
            document.getElementById('hyper1_' + id).style.display = 'none';
            document.getElementById('hyper2_' + id).style.display = 'none';
        } catch (e) {

        }

    }
    return false;
}


function GetLikesCount(getcontid, isParent, sItemType) {
    //alert(webMethodUrl)
    $.ajax({
        type: "GET",
        url: webMethodUrl + "GetLikeContentIDCount",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { ContentPostID: getcontid, ItemType: sItemType },
    }).done(function (response) {
        var oData = JSON.parse(response);
        
        if (isParent == 1) {

            $('#Main_count' + getcontid).html('<img src="{0}/DesktopModules/ActiveForums/Images/{1}" style="width: 14px; height: 16px;"></img>&nbsp;LIKE&nbsp;'.format(sWebServicePath, oData.ImageName) + oData.LikesCount);
            //    $("#imgLikeStatusM_" + getcontid).attr('src', '/DesktopModules/ActiveForums/Images/' + oData.ImageName);
        }

        //if (isParent == 2) {
        //    $('#Span_count' + getcontid).html(oData.LikesCount + ' <img src="{0}/DesktopModules/ActiveForums/Images/{1}"></img>'.format(sWebServicePath, oData.ImageName));
        //}

        else {
            $('#Span_count' + getcontid).html('<img src="{0}/DesktopModules/ActiveForums/Images/{1}" style="width: 14px; height: 16px;"></img>&nbsp;LIKE&nbsp;'.format(sWebServicePath, oData.ImageName) + oData.LikesCount);
            //    $("#imgLikeStatus_" + getcontid).attr('src', '/DesktopModules/ActiveForums/Images/' + oData.ImageName);
        }
        return false;

    });
    return false;
}

function CA_OnAssignItem2User(d) {
    $.confirm({
        title: 'Success',
        content: 'Your vault has been updated successfully',
        type: 'blue',
        typeAnimated: true,
        buttons: {
            tryAgain: {
                text: 'OK',
                btnClass: 'btn-info',
                action: function () {
                    document.location.reload();
                }
            }
        }
    });



}
function CA_ShowDiscussion() {
    $('html, body').animate({
        scrollTop: $("#CA_Blog_dvDiscussion").offset().top
    }, 2000);
    return false;
}
function ExpandTheTopicRepliedIfAny() {
    // 
    var ExpandReplyString = window.location.href;
    if (ExpandReplyString.toLowerCase().indexOf('commentid') != -1) {
        var aryTemp = ExpandReplyString.split('/');
        var LinkID = aryTemp[aryTemp.length - 1];

        //  var linkcount = $("#dvEachTopic_" + LinkID + " .forum-see-more").length - 1;
        //   if (linkcount == 'nothing') return;
        //    $("#dvEachTopic_" + LinkID + " .forum-see-more")[linkcount].click();
        // $("#lnkSeeMore_" + LinkID).click();



        LinkID = LinkID.replace(/#/g, "");
        $('html, body').animate({
            scrollTop: $("#dvEachTopic_" + LinkID).offset().top - 20
        }, 500);
        return true;
    }
}


function CA_PopuplateUsers(lstUsers) {
    $("#CA_ulUsers").empty();
    try {
        for (var i = 0; i <= lstUsers.length; i++) {
            var oCurr = lstUsers[i];
            $("#CA_ulUsers").append('<li style="width:auto;margin-right:8px;margin-top:8px;"><div><div style="float:left;width:60px;height;60px;"><img onerror="this.src=\'/images/no_avatar.gif\'" src="' + oCurr.ProfilePic + '"/></div><div style="float:left;margin-left:10px;width:70%;min-height:30px;"><label style="float:left;width:92%;font-weight:normal;">' + oCurr.RelatedFullName + '<br/>' + oCurr.Roles + '&nbsp;<input id="chkUserToShare_' + oCurr.RelatedUserID + '" type="checkbox" uEmail="' + oCurr.RelatedEmail + '" RelatedID="' + oCurr.RelatedUserID + '" RelatedName="' + oCurr.RelatedFullName + '" /></label></div></div><div style="clear:both;"></div></li>');
        }
    } catch (e) { }
}

function CA_ListConnections() {
    $.ajax({
        url: "/DesktopModules/ClearAction_SummaryActions/rh.asmx/GetUserConnections",
        data: JSON.stringify({ UserID: CA_UserID }),
        dataType: "json", type: "POST", contentType: "application/json; charset=utf-8",
        success: function (d) {
            CA_PopuplateUsers(d.d);
        }
    });
}
var CA_uIDs = '';
function CA_ShowSharePopup(o) {
    
    selectedUsers = $("#CA_ulUsers input:checked");
    if (selectedUsers.length == 0) {
        $(o).removeAttr('disabled');
        CA_WarnUser('Please select atleast one connection, to share the summary detail');
        return false;
    }
    else {
        $('#modalShareDigitalEvent').modal('show');
           //Changes done by @SP
        //var _MessageTemplate = 'You might be interested in this digital event "<a href="{2}">{0}</a>" by {1}.';
        var _MessageTemplate = 'This topic may be useful to you: "<a href="{2}">{0}</a>" facilitated by  {1}.';

        var _LITemplate = "<li id='CA_liUserSelected_{0}' class='CA_ShareLI'><div><label>To</label></div><div style='width: 80%;float: left;'><label style='font-weight:normal'>{1}</label></div><div class='CA_RemoveConnection' onclick='CA_UpdateUserSelection({0})'></div></li>";
        var _LIs = '';
        CA_uIDs = '';
        $("#CA_ulSelectedUsers").empty();
        $.each(selectedUsers, function (i, o) {
            CA_uIDs += $(o).attr('RelatedID') + ',';
            _LIs += _LITemplate.format($(o).attr('RelatedID'), $(o).attr('RelatedName'));
        });
        $("#CA_ulSelectedUsers").append(_LIs);
        $("#CA_txtMessage").html(_MessageTemplate.format($(".title>a:first-child").text(), $(".event-bio-detail>h3:first-child").text(), window.location.href));
    }
}
function CA_WarnUser(msg) {
    $.alert({ title: '', type: 'modern', content: msg });
}

var CA_FinalUserSelections = "";
function CA_UpdateUserSelection(o) {
    $("#chkUserToShare_" + o).click();
    $("#CA_btnShareWithConnections").click();
}

    var CA_AttachmentURL = "";
    var CA_AttachmentFileName = "";
    function CA_UploadOnFileStack() {
        var fsClient = filestack.init(oKey);
        fsClient.pick({}).then(function (response) {
            response.filesUploaded.forEach(function (file) {
                CA_AttachmentURL = file.url;
                CA_AttachmentFileName = file.filename;
                $("#HdnAttachUrl").text(CA_AttachmentURL);
                $("#FileName").html(CA_AttachmentFileName);
                $("#DivAttach").hide();
                CA_WarnUser('File is attached successfully.');
            });
        });
    }

function CA_ShareDigitalEvent(btn) {
    try {
        $(btn).text('Please wait...').attr('disabled', 'true');
        $(".CA_ShareLI").each(function (i, o) {
            var toUserID = $(o).attr('id').split('_')[2];
            var digitalEventLink = $("#CA_txtMessage a:first").attr("href");
            $("#CA_txtMessage a:first").attr("onclick", "window.open('" + digitalEventLink + "')")
            var sBody = $("#CA_txtMessage").html();
            var _APIPath = '/DesktopModules/ClearAction_Connections/API/Connections/SendMessageFromDigitalEvent';
            $.get(_APIPath + '?FromUserID=' + CA_UserID + '&ToUserID=' + toUserID + '&Subject=""&Body="' + sBody + '"&Filename="' + CA_AttachmentFileName.trim() + '"&AttachLink="' + CA_AttachmentURL.trim() + '"'
                , function (d) { });
        });
        CA_ResetButton(btn);
        $('#modalShareDigitalEvent').modal('hide');
        CA_WarnUser('Shared successfully to the selected connections');
    }
    catch (e) {
        CA_WarnUser('Unable to share the digital event with connection due to some technical issue. Please try after sometime.');
        CA_ResetButton(btn);
    }
}
function CA_ResetButton(o) {
    $(o).removeAttr('disabled');
    $(o).text('Send');
}
// Fali's GoToWebinar work Starts
    var GlobalWCCId;
    var GlobalLoginUserId;
    var GlobalComponentId;

    function GotoWebinarRegister(e) {

        var WebInarKeyvalue = $(e).attr("webinarkey");
        var WCCIdvalue = $(e).attr("wccid");
        var LoginUserIdValue = $(e).attr("luserid");
        var componentidValue = $(e).attr("componentid");

        $(".gotoRegisterLink").attr("onclick", "");

        $(e).append("<span style='color:orange;padding-left:22px;'>Please wait... </span>")

        $.ajax({
            type: "GET",
            url: webMethodUrl + "GotoWebinarRegisterAPI",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { WebInarKey: WebInarKeyvalue, WCCId: WCCIdvalue, LoginUserId: LoginUserIdValue, ComponentId: componentidValue },
        }).done(function (response) {

            if (response == 1) {


                GlobalWCCId = WCCIdvalue;
                GlobalLoginUserId = LoginUserIdValue;
                GlobalComponentId = componentidValue;

                ChnageStatusAssignToMe(WCCIdvalue, LoginUserIdValue, componentidValue);



        }
        else if (response == 2) {
            alert("You have already registerd for this webinar.")
            location.reload();
        }
        else {
            alert("Registration for Webinar was not successfully. Please try again")
            location.reload();
        }

        });



    }

    function ChnageStatusAfterGotoWebInarRegister(WCCId, LoginUserId, ComponentId) {

        var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/SaveUserVisit";
        var _data = "{'UserID':'" + LoginUserId + "', 'ItemID':'" + WCCId + "' , 'ComponentTypeID':'" + ComponentId + "' }";
        CA_MakeRequest(_URL, _data, AfterRegisterStatusChnageOne, AfterRegisterStatusChnageTwo);

    }


    function ChnageStatusAssignToMe(WCCId, LoginUserId, ComponentId) {

        var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/Assign2Me";
        var _data = "{'CID':'" + ComponentId + "', 'ItemID':'" + WCCId + "' , 'UID':'" + LoginUserId + "' }";
        CA_MakeRequest(_URL, _data, AfterChnageStatusAssignToMeOne, AfterChnageStatusAssignToMeTwo);

    }


    function AfterRegisterStatusChnageOne(d) {

        alert("Successfully registered for the webinar.");
        location.reload();
    }

    function AfterRegisterStatusChnageTwo(d) {

        alert("Successfully registered for the webinar.")
        location.reload();
    }
    function AfterChnageStatusAssignToMeOne(d) {

        alert("Successfully registered for the webinar.")
        location.reload();
        //ChnageStatusAfterGotoWebInarRegister(GlobalWCCId, GlobalLoginUserId, GlobalComponentId);
    }

    function AfterChnageStatusAssignToMeTwo(d) {
        alert("Successfully registered for the webinar.")
        location.reload();
        //ChnageStatusAfterGotoWebInarRegister(GlobalWCCId, GlobalLoginUserId, GlobalComponentId);
    }