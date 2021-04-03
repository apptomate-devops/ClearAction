$(function () {
    $('#linkreplies').click(function () {
        $('input#<%=txtComments.ClientID%>').removeAttr('value');
        $('input#<%=txtComments.ClientID%>').focusin();
    });

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



            if (IsSelfAssigned == '1') {
                return {
                    callback: function (key, options) {
                        if (key == "Remove4MyVault") {
                            var Selected_TID = $(this).attr("cid");
                            if (confirm("Are you sure to remove this item???") == true) {
                                CA_RemoveFromMyVault(Selected_TID);
                            }
                        }

                        if (key == "ReportIssue") {
                            var btitle = $(this).parent().parent().find(".BRITitlehdn").val();

                            RI_ShowIssue('Blog', btitle);
                        }
                    },
                    items: {
                        "Add2MyVault": { name: "Add to My Vault", disabled: true },
                        "Remove4MyVault": { name: "Remove from My Vault", disabled: false },
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
                            CA_Assign2Me(Selected_ID);
                        }
                        if (key == "ReportIssue") {
                            var btitle = $(this).parent().parent().find(".BRITitlehdn").val();
                            RI_ShowIssue('Blog', btitle);
                        }
                    },
                    items: {
                        "Add2MyVault": { name: "Add to My Vault", disabled: false },
                        "Remove4MyVault": { name: "Remove from My Vault", disabled: true },
                        "sep1": "---------",
                        "ReportIssue": { name: "Report an Issue", disabled: false }
                    }
                };
            }
            else if (IsSelfAssigned == '2') {
                return {
                    callback: function (key, options) {
                        if (key == "addattachement") {

                            var Selected_ID = $(this).attr("cid");
                            var label = $(this).attr("label");
                            //   CA_Assign2Me(Selected_ID);
                            //   addFileRepies(this, Selected_ID, label);
                            var Selected_CID = $(this).attr("cid");
                            AddFileToTempHolder(this, -1, Selected_CID);
                        }

                    },
                    items: {
                        "addattachement": { name: "Attach Media", disabled: false }
                    }
                };
            }

            else if (IsSelfAssigned == '3' || IsSelfAssigned == '4' || IsSelfAssigned == '6') {
                return {
                    callback: function (key, options) {
                        if (key == "addattachement") {

                            var Selected_ID = $(this).attr("cid");
                            var label = $(this).attr("label");

                            var Selected_CID = $(this).attr("cid");
                            AddFileToTempHolder(this, Selected_CID, Selected_CID);
                        }

                    },
                    items: {
                        "addattachement": { name: "Attach Media", disabled: false }
                    }
                };
            }

            else if (IsSelfAssigned == '5') {
                return {
                    callback: function (key, options) {
                        if (key == "AttachMedia") {
                            var commentID = $(this).attr("commentid");
                            var ContentItemId = $(this).attr("ContentItemId");
                            //  alert('Comment:' + cid);



                            AddFileToExitingComment(this, ContentItemId, commentID);
                        }
                        if (key == "EditReply") {

                            var commentID = $(this).attr("commentid");
                            var WccID = $(this).attr("cid");
                            ShowAttachList(commentID, 'topedit', 'show');
                            debugger;
                            $('#edit_' + commentID).show();
                            $('#divattachReplies_' + commentID).show();
                            $('#' + commentID).hide();
                            $('#top_' + commentID).hide();
                            //  EditPostReply(this, commentID, WccID);

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


            else if (IsSelfAssigned == '7' || IsSelfAssigned == '9') {
                return {
                    callback: function (key, options) {
                        if (key == "AttachMedia") {
                            var commentID = $(this).attr("commentid");
                            var ContentItemId = $(this).attr("ContentItemId");
                            //  alert('Comment:' + cid);



                            AddFileToExitingComment(this, ContentItemId, commentID);
                        }
                        if (key == "EditReply") {

                            var commentID = $(this).attr("commentid");
                            var WccID = $(this).attr("cid");
                            ShowAttachList(commentID, 'topedit', 'show');
                            debugger;
                            $('#edit_' + commentID).show();
                            $('#divattachReplies_' + commentID).show();
                            $('#' + commentID).hide();
                            $('#top_' + commentID).hide();
                            //  EditPostReply(this, commentID, WccID);

                        }

                    },
                    items: {
                        "AttachMedia": { name: "Attach Media", disabled: oAddDisable },
                        "EditReply": { name: "Edit My Reply", disabled: !iseditallow },
                        "sep1": "---------"
                    }
                };
            }
            else {
                return {
                    callback: function (key, options) {
                        if (key == "addattachement") {

                            var Selected_ID = $(this).attr("cid");
                            var label = $(this).attr("label");
                            //   CA_Assign2Me(Selected_ID);
                            AddFileToTempHolder(this, -1, Selected_ID);
                        }

                    },
                    items: {
                        "addattachement": { name: "Attach Media", disabled: false }
                    }
                };
            }
        },
        trigger: 'left'
    });
    //  window.scrollTo(0, 0);

    setTimeout(ExpandTheTopicRepliedIfAny, 1000);

});


function AddFileToExitingComment(ele, Contentitemid, commentid) {

    var fsClient = filestack.init(oKey);

    fsClient.pick({}).then(function (response) {
        response.filesUploaded.forEach(function (file) {
            var ofileresponse = file;
            debugger;
            var uploadServiceUrl = webMethodUrl + 'AddAttachmentByComment';
            $.ajax({
                type: 'GET', dataType: "json", contentType: "application/json; charset=utf-8",
                url: uploadServiceUrl,
                data: { ContentID: Contentitemid, CommentID: commentid, FileName: ofileresponse.filename, contentType: ofileresponse.mimetype, size: ofileresponse.size, statckurl: ofileresponse.url },
                success: function () {

                    alert('File has been uploaded successfully');

                    ShowAttachList(commentid, 'top', 'show');
                },
                xhrFields: {
                    onprogress: function (progress) {
                    }
                }
            }).done(function (response) {


            });
        });
    });
}
function LoadMore() {

    var cPage = Number($('#<%=hdnCurrentPage.ClientID%>').val()) + 1;
    var cPageSize = 15; var TabID = '<%#TabId%>';
    $.ajax({
        type: "GET",
        url: webMethodUrl + "GetBlogList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { CurrentPage: cPage, PageSize: cPageSize },
    }).done(function (response) {
        var oData = JSON.parse(response);
        debugger;
        var strTemp = "";
        var icount = 0;
        //strTemp = "<tr><td><strong><center>File Name</center></strong></td><td><strong><center>File links</center></strong></td></tr/>";
        strTemp = "";//"<tr><td><strong><center>File Name</center></strong></td></tr/>";
        for (var i = 0; i < oData.length; i++) {

            strTemp += '<li><a href="{1}">{0}</a></li>'.format(oData[i].Title, oData[i].GetBlogDetailUrl);
            icount++;
        }
        if (cPage == 1)
            $('#ulCollection').html(strTemp);
        else
            $("#ulCollection").append(strTemp);
        $('#<%=hdnCurrentPage.ClientID%>').val(Number(cPage));
        var totalPage = $('#<%=hdnTotalRecord.ClientID%>').val();
        if (totalPage < cPage * cPageSize)
            $('#btnloadmore').hide();
    });
}

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
    $("#divattachReplies_" + contId).hide();
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
            debugger;
            var CurrentUserId = $("#hiddenUserId").val();
            var UploadStringHTML = "<div class='BeforAttachmentSestion'><a contId='" + contid + "' class='BeforeAttachment' UploadedByUserId='" + CurrentUserId + "' href='" + ofileresponse.url + "' target='_blank'>" + ofileresponse.filename + "</a> <a UploadedByUserId='" + CurrentUserId + "'  RemoveAttachmentURLBeforePost='" + ofileresponse.url + "' class='attachmentBeforRemoveButton' onClick='removeAttachmentURLBeforePost(this)' contId='" + contid + "' > &nbsp &nbsp &nbsp </a> </div>";
            if (contid == -1) {
                $("#UploadedAttachment").append(UploadStringHTML);
            }
            else if (contid > 0) {
                $("#UploadedAttachment_" + contid).append(UploadStringHTML);
            }
            alert('File has been uploaded successfully');
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
                type: 'GET', dataType: "json", contentType: "application/json; charset=utf-8",
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
        type: 'GET', dataType: "json", contentType: "application/json; charset=utf-8",
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
            onprogress: function (progress) { }
        }
        // contentType: ofileresponse.mimetype
    }).done(function (response) {

    });

}

function GetAttachListFormat(islogin) {
    if (islogin)
        return '<div class="AfterAttachmentSestion"><span class="AttachedFileText">&nbsp;&nbsp;&nbsp;</span><small>{DateAdded}</small> :: <a contid="{commentid}" class="AfterAttachment" uploadedbyuserid="{userid}" href="{filestackurl}" target="_blank">{filename}</a><a uploadedbyuserid= "{userid}" removeattachmenturlafterpost= "{filestackurl}" class="attachmentAfterRemoveButton" onclick= "RemoveAttachmentURLAfterPost(this)" contid= "{commentid}" >&nbsp; &nbsp; &nbsp; </a > </div>';

    return '<div class="AfterAttachmentSestion"><span class="AttachedFileText">&nbsp;&nbsp;&nbsp;</span><small>{DateAdded}</small> :: <a contid="{commentid}" class="AfterAttachment" uploadedbyuserid="{userid}" href="{filestackurl}" target="_blank">{filename}</a></div>';


}

function ShowAttachList(contId, divid, status) {
    var PortalId = 0;
    $.ajax({
        type: "GET", contentType: "application/json; charset=utf-8", dataType: "json",
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
    debugger;
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
                    alert('Attachment  has been removed successfully');
                    debugger;
                    ShowAttachList(contId, 'top', 'show');
                },
                xhrFields: {
                    onprogress: function (progress) {
                    }
                }
                // contentType: ofileresponse.mimetype
            }).done(function (response) {

            });
        }
    }
}

function GetFilesList(contId) {
    // var webMethodUrl = '<%=WebServicePath%>/DesktopModules/activeforums/api/ForumService/';
    var PortalId = 0;
    $.ajax({
        type: "GET", dataType: "json", contentType: "application/json; charset=utf-8",
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

//$(document).ready(function () {
//    $(".solve-spaces-nav a").removeClass("active");

//    if (AF_IsMyVault == 1 || AF_IsMyVault == undefined) {
//        $(".solve-spaces-nav a:first").addClass("active");
//    } else {
//        $(".solve-spaces-nav a:last").attr("class", "active");
//    }

//    var AF_CurrentCatID = getParameterByName('CategoryId');
//    $("#CA_Blog_dvCatContainer li").removeClass("active");
//    $("#CA_Blog_dvCatContainer li a[CatID='" + AF_CurrentCatID + "']").parent().addClass("active");

//    $("#CA_Blog_dvCatContainer1 li").removeClass("active");

//    if (AF_ComponentID == 1 || AF_ComponentID == undefined)
//        AF_ComponentID = "-1";
//    $("#CA_Blog_dvCatContainer1 li a[EventType='" + AF_ComponentID + "']").parent().addClass("active");
//    $("#dvBlogStatusFilter li").removeClass("active");

//    if (AF_CurrentStatus == undefined)
//        AF_CurrentStatus = 0;
//    $("#dvBlogStatusFilter li[statusid='" + AF_CurrentStatus + "']").addClass("active");
//    debugger;
//    // trap enter key of search textbox 
//    $('[id*="txtSearch"]').on("keypress", function (event) {
//        if (event.which == 13) {
//            $('[id*="lnkSearch"]').click();
//        }
//    });
//    //CA_ListConnections();
//    //A_PopuplateUsers();
//})
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
function DeleteUpload(label) {
    $("#divattach_" + label).hide();
    $("#lnkdetail_" + label).html("");
    $("#<%=hdnFileUrl.ClientID%>").val('');
    $("#<%=hdnFileName.ClientID%>").val('');
    $("#<%=hdnCommentID.ClientID%>").val('');

}



function DeleteUploadReplies(contId) {
    $("#divattachReplies_" + contId + " .RepliesAttachmentURL").text("")
    $("#divattachReplies_" + contId + " .RepliesAttachmentName").text("")

    $("#lnkdetailReplies_" + contId + "").text("");
    $("#divattachReplies_" + contId + "").hide();
}



function ShowPanel(label) {

    $("#divattach_" + label).show();
    $("#lnkdetail_" + label).html("<a target='_blank' href='" + $("#<%=hdnFileUrl.ClientID%>").val() + "'>" + $("#<%=hdnFileName.ClientID%>").val() + "</a>");

}

function ShowPanelReplies(label) {

    $("#divattachReplies_" + label).show();
    $("#lnkdetailReplies_" + label).html("<a target='_blank' href='" + $("#<%=hdnFileUrlReplies.ClientID%>").val() + "'>" + $("#<%=hdnFileNameReplies.ClientID%>").val() + "</a>");

}

function addFileRepies(ele, contId, label) {


    var fsClient = filestack.init(oKey);
    fsClient.pick({}).then(function (response) {

        response.filesUploaded.forEach(function (file) {
            var ofileresponse = file;
            debugger;
            //fileName: ofileresponse.filename, contentType: ofileresponse.mimetype, size: ofileresponse.size, fileurl: ofileresponse.url 
            //   <%--$("#<%=hdnFileUrl.ClientID%>").val(ofileresponse.url);
            //$("#<%=hdnFileName.ClientID%>").val(ofileresponse.filename);
            //$("#<%=hdnCommentID.ClientID%>").val(contId); --%>



            $("#divattachReplies_" + contId + " .RepliesAttachmentURL").text(ofileresponse.url)
            $("#divattachReplies_" + contId + " .RepliesAttachmentName").text(ofileresponse.filename)

            $("#lnkdetailReplies_" + contId + "").text(ofileresponse.filename);
            $("#divattachReplies_" + contId + "").show();

        });

    });
}
function Show(id) {
    if (document.getElementById(id).style.display == 'none') {
        try {
            //Added try captch if element id changed accidently
            $("#divattachReplies_" + id).hide();
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
            $("#divattachReplies_" + id).hide();
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
        debugger;
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
    $('html, body').animate({ scrollTop: $("#CA_Blog_dvDiscussion").offset().top }, 2000);
    return false;
}

function ExpandTheTopicRepliedIfAny() {

    var ExpandReplyString = window.location.href;
    if (ExpandReplyString.toLowerCase().indexOf('commentid') != -1) {
        var aryTemp = ExpandReplyString.split('/');
        var LinkID = aryTemp[aryTemp.length - 1];

        //  var linkcount = $("#dvEachTopic_" + LinkID + " .forum-see-more").length - 1;
        //   if (linkcount == 'nothing') return;
        //    $("#dvEachTopic_" + LinkID + " .forum-see-more")[linkcount].click();
        // $("#lnkSeeMore_" + LinkID).click();
        $('html, body').animate({
            scrollTop: $("#dvEachTopic_" + LinkID).offset().top
        }, 500);
        return true;
    }
}

