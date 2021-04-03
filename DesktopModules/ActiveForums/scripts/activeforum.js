$(window).load(function () {

    String.prototype.format = function () {
        var str = this;
        for (var i = 0; i < arguments.length; i++) {
            var reg = new RegExp("\\{" + i + "\\}", "gm");
            str = str.replace(reg, arguments[i]);
        }
        return str;
    }


    $('#txtInnerQuickReply').focus(function () {
        $(this).animate({ width: '350px' }, 500);
    });

    $('#txtInnerQuickReply').blur(function () {
        $(this).animate({ width: '100px' }, 500);
    });








    //forum slide toggles
    $('.forum-see-more').click(function (e) {

        if (e.currentTarget.text == "See more") {
            var controlid = e.currentTarget.id;
            var contentid = controlid.match(/\d+/);
            UpdateTopicStatus(contentid);
        }
        // UpdateTopicStatus(e.act)
        //$(this).closest('div.forum-row-wrapper').find('.forum-left-col').height('auto');
        //$(this).closest('div.forum-row-wrapper').find('.forum-right-col').height('auto');
        //$(this).parent('div').find('.forum-more-top').slideToggle();
        //$(this).parent('div').find('.forum-more-bottom').slideToggle('', function () {
        //    $.fn.matchHeight._maintainScroll = true;
        //    $('.forum-match-height').matchHeight();
        //});
        //$(this).parent('div').find('.forum-excerpt').show();
        //$(this).closest('div.forum-row-wrapper').find('.forum-right-status').show();
        //$(this).parent('div').toggleClass('expanded');
        //$(this).closest('div.forum-row-wrapper').find('.forum-right-col').toggleClass('expanded');
        //if ($(this).hasClass('active')) {
        //    $(this).removeClass('active');
        //    $(this).text('See more');
        //} else {
        //    $(this).addClass('active');
        //    $(this).text('Collapse');
        //}
    });
    $('.forum-see-more-reply').click(function (e) {


        $(this).closest('div.forum-row-wrapper').find('.forum-left-col').height('auto');
        $(this).closest('div.forum-row-wrapper').find('.forum-right-col').height('auto');
        $(this).closest('div.forum-row-wrapper').find('.forum-more-top').slideToggle();
        $(this).closest('div.forum-row-wrapper').find('.forum-more-bottom').slideToggle('', function () {
            $.fn.matchHeight._maintainScroll = true;
            $('.forum-match-height').matchHeight();
        });
        $(this).closest('div.forum-row-wrapper').find('.forum-excerpt').slideToggle();
        $(this).closest('div.forum-row-wrapper').find('.forum-right-status').slideToggle();
        $(this).closest('div.forum-row-wrapper').find('.forum-post').toggleClass('expanded');
        $(this).closest('div.forum-row-wrapper').find('.forum-right-col').toggleClass('expanded');

        $(this).closest('div.forum-row-wrapper').find('.forum-see-more').toggleClass('active');

        $(this).closest('div.forum-row-wrapper').find('.forum-see-more').text('See more');
        if ($(this).parent('div').find('.members-expand').hasClass('active')) {
            $(this).parent('div').find('.members-expand').removeClass('active');
            $(this).text('Expand');
            $(this).removeClass('active');


        }
        else {
            $(this).find('.members-expand').addClass('active');
            $(this).closest('div.forum-row-wrapper').find('.forum-see-more').text('Collapse');
            $(this).addClass('active');

        }


    });
    $('.forum-see-more-top').click(function (e) {

        e.preventDefault();
        //$(this).closest('div.forum-row-wrapper').find('.forum-left-col').height('auto');
        //$(this).closest('div.forum-row-wrapper').find('.forum-right-col').height('auto');
        //$(this).closest('div.forum-row-wrapper').find('.forum-more-top').slideToggle();
        //$(this).closest('div.forum-row-wrapper').find('.forum-more-bottom').slideToggle('', function () {
        //    $.fn.matchHeight._maintainScroll = true;
        //    $('.forum-match-height').matchHeight();
        //});
        //$(this).closest('div.forum-row-wrapper').find('.forum-excerpt').slideToggle();
        //$(this).closest('div.forum-row-wrapper').find('.forum-right-status').slideToggle();
        //$(this).closest('div.forum-row-wrapper').find('.forum-post').toggleClass('expanded');
        //$(this).closest('div.forum-row-wrapper').find('.forum-right-col').toggleClass('expanded');
        //$(this).closest('div.forum-row-wrapper').find('.forum-see-more').toggleClass('active');
        //$(this).closest('div.forum-row-wrapper').find('.forum-see-more').text('See more');

    });

    $('.forum-see-more-topInner').click(function (e) {
        e.preventDefault();
        $(this).closest('div.forum-row-wrapper1').find('.forum-left-col').height('auto');
        $(this).closest('div.forum-row-wrapper1').find('.forum-right-col').height('auto');
        $(this).closest('div.forum-row-wrapper1').find('.forum-more-top').slideToggle();
        $(this).closest('div.forum-row-wrapper1').find('.forum-more-bottom').slideToggle('', function () {
            $.fn.matchHeight._maintainScroll = true;
            $('.forum-match-height').matchHeight();
        });
        $(this).closest('div.forum-row-wrapper1').find('.forum-excerpt').slideToggle();
        $(this).closest('div.forum-row-wrapper1').find('.forum-right-status').slideToggle();
        $(this).closest('div.forum-row-wrapper1').find('.forum-post').toggleClass('expanded');
        $(this).closest('div.forum-row-wrapper1').find('.forum-right-col').toggleClass('expanded');
        $(this).closest('div.forum-row-wrapper1').find('.forum-see-more').toggleClass('active');


    });





    //forum tabs
    $(".forum-posts-tab").tabs();
    $(".forum-posts-tab").on("tabsactivate", function (event, ui) {
        $.fn.matchHeight._maintainScroll = true;
        $('.forum-match-height').matchHeight();
    });
    //forum sidebar collapse
    $('.forum-right-toggle-content a.toggle-content').click(function (e) {
        e.preventDefault();

        //$(this).parent('div').find('.members-expand').slideToggle();

        //if ($(this).parent('div').find('.members-expand').hasClass('active')) {
        //    $(this).parent('div').find('.members-expand').removeClass('active');
        //    $(this).text('Expand');
        //    $(this).removeClass('active');
        //} else {
        //    $(this).parent('div').find('.members-expand').addClass('active');
        //    $(this).text('Collapse');
        //    $(this).addClass('active');

        //}

    });

    // initialize context menu for user forum
    $.contextMenu({
        selector: '.CA_UserMenu',
        build: function ($trigger, e) {

            var IsSelfAssigned = $trigger.attr('IsSelfAssigned');
            var status = false;


            if (IsSelfAssigned == '8') { // Added By Fali For Edit Attachment Link Buttons.
                return {

                    callback: function (key, options) {

                        if (key == "FileUpload") {
                            var Selected_CID = $(this).attr("cid");
                            var Selected_rToId = $(this).attr("rtoid");

                            var flag = false;

                            if ($("#HiddenReplyEditContent_" + Selected_CID + "").val() == "") {
                                flag = true;
                            }
                            else {
                                if ($(".ReplyEditContentSection_" + Selected_CID + " .ReplyEditContent").val() != "") {
                                    flag = true;
                                }
                            }


                            if (flag) {
                                EditPostFile(this, Selected_CID, Selected_rToId);
                            }
                            else {
                                alert("Please enter reply text");
                            }




                        }

                        if (key == "EditMyReply") {
                            var Selected_CID = $(this).attr("cid");
                            var Selected_rToId = $(this).attr("rtoid");
                            EditPostReply(this, Selected_CID, Selected_rToId);

                        }


                    },
                    items: {

                        "FileUpload": { name: "Attach a File", disabled: false },
                        "EditMyReply": { name: "Edit my reply ", disabled: false },

                    }
                };
            }
            else if (IsSelfAssigned == '7') { // Added By Fali For Attachment Menu
                return {

                    callback: function (key, options) {


                        if (key == "FileUpload") {
                            var Selected_CID = $(this).attr("cid");

                            addPostFile(this, Selected_CID);
                        }

                    },
                    items: {


                        "FileUpload": { name: "Attach a File", disabled: false },


                    }
                };
            }
            else if (IsSelfAssigned == '1') {
                return {

                    callback: function (key, options) {

                        if (key == "Remove4MyVault") {
                            var Selected_TID = $(this).attr("cid");
                            if (confirm("Are you sure to remove this item???") == true) {
                                CA_RemoveFromMyVault(Selected_TID);
                            }
                        }
                        if (key == "FileUpload") {
                            var Selected_CID = $(this).attr("cid");
                            addFile(this, Selected_CID);
                        }
                        if (key == "ReportIssue") {
                            var curreleID = this.attr("id");
                            curreleID = curreleID.replace("imgUserMenu", "hdnSub");
                            RI_ShowIssue('Forum', $('#' + curreleID + '').val());
                        }
                    },
                    items: {
                        "Add2MyVault": { name: "Add to My Vault", disabled: true },
                        "Remove4MyVault": { name: "Remove from My Vault", disabled: false },
                        "sep1": "---------",
                        "FileUpload": { name: "Attach Media", disabled: false },
                        "sep2": "---------",
                        "ReportIssue": { name: "Report an Issue", disabled: false }
                    }
                };
            }
            else {
                return {
                    callback: function (key, options) {

                        if (key == "Add2MyVault") {
                            var Selected_ID = $(this).attr("cid");
                            CA_Assign2Me(Selected_ID);
                        }
                        if (key == "FileUpload") {
                            var Selected_CID = $(this).attr("cid");
                            addFile(this, Selected_CID);
                        }
                        if (key == "ReportIssue") {
                            var curreleID = this.attr("id");
                            curreleID = curreleID.replace("imgUserMenu", "hdnSub");
                            RI_ShowIssue('Forum', $('#' + curreleID + '').val());
                        }
                    },
                    items: {
                        "Add2MyVault": { name: "Add to My Vault", disabled: false },

                        "Remove4MyVault": { name: "Remove from My Vault", disabled: true },
                        "sep1": "---------",
                        "FileUpload": { name: "Attach Media", disabled: false },
                        "sep2": "---------",
                        "ReportIssue": { name: "Report an Issue", disabled: false }
                    }
                };
            }
        },
        trigger: 'left'
    });





    setTimeout(ExpandTheTopicRepliedIfAny, 1000)
});

function showprogress() {
    $('#wait').show();
}
function hideprogress() {
    $('#wait').hide();
}

function Show(id) {
    if (document.getElementById(id).style.display == 'none') {
        document.getElementById(id).style.display = 'block';
        //  $(id).show(1000);
        document.getElementById('hyper_' + id).style.display = 'none';
        document.getElementById('hyper1_' + id).style.display = 'block';

        document.getElementById('hyper2_' + id).style.display = 'block';
        document.getElementById('hyper3_' + id).style.display = 'block';
        document.getElementById('hyper4_' + id).style.display = 'block';
    }
    return false;
}
function Hide(id) {
    if (document.getElementById(id).style.display == 'block') {
        document.getElementById(id).style.display = 'none';
        document.getElementById('hyper_' + id).style.display = 'block';
        document.getElementById('hyper1_' + id).style.display = 'none';

        document.getElementById('hyper2_' + id).style.display = 'none';
        document.getElementById('hyper3_' + id).style.display = 'none';
        document.getElementById('hyper4_' + id).style.display = 'none';

    }
    return false;
}

function GetLikesCount(getcontid, isParent) {
    $.ajax({
        type: "GET",
        url: webMethodUrl + "GetLikeContentIDCount",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { ContentPostID: getcontid },
    }).done(function (response) {
        var oData = JSON.parse(response);
        if (isParent == 1) {
            $('#Main_count' + getcontid).html(oData.LikesCount);
            $("#dvRecommend_" + getcontid).css('background', 'url("/DesktopModules/Activeforums/Images/{0}")'.format(oData.ImageName));
        }

        if (isParent == 2) {
            $('#Span_count' + getcontid).html(oData.LikesCount + ' <img src="{0}/DesktopModules/ActiveForums/Images/{1}"></img>'.format(sWebServicePath, oData.ImageName));
        }

        if (isParent == 4) {
            $('#Main4_count' + getcontid).html(oData.LikesCount + ' <img src="{0}/DesktopModules/ActiveForums/Images/{1}"></img>'.format(sWebServicePath, oData.ImageName));
            $("#dvRecommend4_" + getcontid).css('background', 'url("/DesktopModules/Activeforums/Images/{0}")'.format(oData.ImageName));
        }
        else {
            $("#lblLikeCount_" + getcontid).html(oData.LikesCount);
            debugger;
            $("#imgLikeCount_" + getcontid).attr('src', '/DesktopModules/ActiveForums/Images/' + oData.ImageName);
        }
    });


    return false;
}


function GetTopicLikeToggle(getcontid, isParent) {
    $.ajax({
        type: "GET",
        url: webMethodUrl + "GetDisLikeContentIDCount",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { ContentPostID: getcontid },
    }).done(function (response) {

        var oData = JSON.parse(response);

        $('#Main4_count' + getcontid).html('<img src="{0}/DesktopModules/ActiveForums/Images/{1}"></img>&nbsp;&nbsp;'.format(sWebServicePath, oData.ImageName) + oData.LikesCount);
        //   $("#dvRecommend4_" + getcontid).css('background', 'url("/DesktopModules/Activeforums/Images/{0}")'.format(oData.ImageName));

        $('#Main5_count' + getcontid).html('<img src="{0}/DesktopModules/ActiveForums/Images/{1}"></img>&nbsp;&nbsp;'.format(sWebServicePath, oData.DisLikeImageName) + oData.DisLikeCount);
        //  $("#dvRecommend4_" + getcontid).css('background', 'url("/DesktopModules/Activeforums/Images/{0}")'.format(oData.dvRecommend4_));
    });


    return false;
}



function GetLikesCount1(getcontid, isParent) {


    $.ajax({
        type: "GET",
        url: webMethodUrl + "GetLikeContentIDCount",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { ContentPostID: getcontid },
    }).done(function (response) {


        var oData = JSON.parse(response);

        if (isParent == 1) {
            $('#Main_count' + getcontid).html(oData.LikesCount);
            $("#dvRecommend_" + getcontid).css('background', 'url("/DesktopModules/Activeforums/Images/{0}")'.format(oData.ImageName));
        }

        if (isParent == 2) { $('#Span_count' + getcontid).html(oData.LikesCount + ' <img src="{0}/DesktopModules/ActiveForums/Images/{1}"></img>'.format(sWebServicePath, oData.ImageName)); }

        else
            $('#bottom_count' + getcontid).html(oData.LikesCount + ' <img src="{0}/DesktopModules/ActiveForums/Images/{1}"></img>'.format(sWebServicePath, oData.ImageName));

    });


    return false;
}


function UpdateTopicStatus(contId) {
    // var webMethodUrl = '<%=WebServicePath%>/DesktopModules/activeforums/api/ForumService/';
    $.ajax({
        type: "GET",
        url: webMethodUrl + "UpdateTopicSeenStatus",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { TopicId: contId },
    }).done(function (response) {

    });
}
function GetFilesList(contId) {


    // var webMethodUrl = '<%=WebServicePath%>/DesktopModules/activeforums/api/ForumService/';
    var PortalId = $("#hiddenPortalId").val();
    $.ajax({
        type: "GET",
        url: webMethodUrl + "GetAttachments",

        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { PortalId: PortalId, contentId: contId },
    }).done(function (response) {
        var oData = JSON.parse(response);
        var strTemp = "";
        var icount = 0;

        var AttachmentUserId = 0;
        var currentPortalId = $("#hiddenPortalId").val();

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

function addFile(ele, contId) {
    var fsClient = filestack.init(oKey);
    fsClient.pick({}).then(function (response) {

        response.filesUploaded.forEach(function (file) {
            var ofileresponse = file;
            var uploadServiceUrl = webMethodUrl + 'GetSaveAttachments';
            $.ajax({
                type: 'GET',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: uploadServiceUrl,
                data: { contentId: contId, fileName: ofileresponse.filename, contentType: ofileresponse.mimetype, size: ofileresponse.size, fileurl: ofileresponse.url },
                success: function () {
                    $("#lnkSeeMore_" + contId).click();
                    alert('File is uploaded successfully');
                },
                xhrFields: {
                    onprogress: function (progress) {
                    }
                }
                // contentType: ofileresponse.mimetype
            }).done(function (response) {
                hideprogress();
                GetFilesList(contId);
            });

        });

    });

}



var AttachmentArray = [];

function addPostFile(ele, contId) {

    var currentContentId = "dvEachTopicContentId_" + contId + "";

    var parentContentId = $("." + currentContentId + "").closest('.mainContentSections').attr('contentid');


    var fsClient = filestack.init(oKey);

    var blankContentId = 0;
    fsClient.pick({}).then(function (response) {

        response.filesUploaded.forEach(function (file) {
            var ofileresponse = file;
            var uploadServiceUrl = webMethodUrl + 'GetSaveAttachments';
            $.ajax({
                type: 'GET',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: uploadServiceUrl,
                data: { contentId: blankContentId, fileName: ofileresponse.filename, contentType: ofileresponse.mimetype, size: ofileresponse.size, fileurl: ofileresponse.url },
                success: function () {
                    //$("#lnkSeeMore_" + contId).click();

                    var CurrentUserId = $("#hiddenUserId").val();

                    var topicParentClass = ".dvEachTopicContentId_" + contId + "";

                    var UploadStringHTML = "<div class='BeforAttachmentSestion'> <a contId='" + contId + "' class='BeforeAttachment' UploadedByUserId='" + CurrentUserId + "' href='" + ofileresponse.url + "' target='_blank'>" + ofileresponse.filename + "</a> <a UploadedByUserId='" + CurrentUserId + "'  RemoveAttachmentURLBeforePost='" + ofileresponse.url + "' class='attachmentBeforRemoveButton' onClick='removeAttachmentURLBeforePost(this)' contId='" + contId + "' > &nbsp &nbsp &nbsp </a> </div>";

                    $("" + topicParentClass + " #UploadedAttachment").first().append(UploadStringHTML)

                    var UploadStringHTMLSavePost = "<div class='AfterAttachmentSestion'> <a contId='" + contId + "' class='AfterAttachment' UploadedByUserId='" + CurrentUserId + "' href='" + ofileresponse.url + "' target='_blank'>" + ofileresponse.filename + "</a> <a UploadedByUserId='" + CurrentUserId + "'  RemoveAttachmentURLAfterPost='" + ofileresponse.url + "' class='attachmentAfterRemoveButton' onClick='RemoveAttachmentURLAfterPost(this)' contId='" + contId + "'> &nbsp &nbsp &nbsp </a> </div>";

                    var uploadListHTML = $("" + topicParentClass + " .UploadedAttachmentSavePost").first().val();

                    var finalUploadContentHTML = "" + uploadListHTML + "" + UploadStringHTMLSavePost + "";

                    $("" + topicParentClass + " .UploadedAttachmentSavePost").first().val(finalUploadContentHTML);


                    AttachmentArray.push({
                        contentid: parentContentId,
                        currentContentId: contId,
                        attachmentURL: ofileresponse.url,
                    });


                    alert('File is uploaded successfully');
                },
                xhrFields: {
                    onprogress: function (progress) {
                    }
                }
                // contentType: ofileresponse.mimetype
            }).done(function (response) {
                hideprogress();
                GetFilesList(parentContentId);
            });

        });

    });

}


function EditPostReply(ele, contId, replyToId) {


    if ($("#HiddenReplyEditContent_" + contId + "").val() == "") {
        var contentClass = "ReplyContentSection_" + contId + "";



        var EditPostContent = $("." + contentClass + " .BodyContent").html();
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

function SeeAttachedMediaList(element) {

    var contId = $(element).attr("cid");

    if ($(".AttchmentListSection_" + contId + "").html() == "") {


        var EditPostContentAttachmentData = "";
        $(".ReplyContentSection_" + contId + "").find(".AfterAttachmentSestion").each(function () {

            EditPostContentAttachmentData = "" + EditPostContentAttachmentData + "" + $(this)[0].outerHTML + "";
        });


        $(".AttchmentListSection_" + contId + "").html(EditPostContentAttachmentData);
        $(".AttchmentListSection_" + contId + " .AfterAttachmentSestion").prepend("<span class=AttachedFileText>Attached file : </span>")

    }
    else {

        $(".AttchmentListSection_" + contId + "").html("");
    }


}


function EditReplyCancle(element) {


    var contId = $(element).attr("cid")

    $(".ReplyEditContentSection_" + contId + "").hide();
    $(".ReplyEditContentSection_" + contId + " .ReplyEditContent").text();
    $(".ReplyContentSection_" + contId + "").html($("#HiddenReplyEditContent_" + contId + "").val());
    $("#HiddenReplyEditContent_" + contId + "").val("")
    $(".ReplyContentSection_" + contId + "").show();
    $(".SeeAttachedMediaLinkContent[cid='" + contId + "']").show();
}


function EditPostFile(ele, contId, replyToId) {


    var currentContentId = "dvEachTopicContentId_" + contId + "";
    var parentContentId = $("." + currentContentId + "").closest('.mainContentSections').attr('contentid');

    var fsClient = filestack.init(oKey);


    fsClient.pick({}).then(function (response) {

        response.filesUploaded.forEach(function (file) {
            var ofileresponse = file;
            var uploadServiceUrl = webMethodUrl + 'GetSaveAttachments';
            $.ajax({
                type: 'GET',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: uploadServiceUrl,
                data: { contentId: parentContentId, fileName: ofileresponse.filename, contentType: ofileresponse.mimetype, size: ofileresponse.size, fileurl: ofileresponse.url },
                success: function () {

                    var CurrentUserId = $("#hiddenUserId").val();
                    var UploadStringHTMLSavePost = "<div class='AfterAttachmentSestion'> <a contId='" + contId + "' class='AfterAttachment' UploadedByUserId='" + CurrentUserId + "' href='" + ofileresponse.url + "' target='_blank'>" + ofileresponse.filename + "</a> <a UploadedByUserId='" + CurrentUserId + "'  RemoveAttachmentURLAfterPost='" + ofileresponse.url + "' class='attachmentAfterRemoveButton' onClick='RemoveAttachmentURLAfterPost(this)' contId='" + contId + "'> &nbsp &nbsp &nbsp </a> </div>";
                    UploadStringHTMLSavePost = "";
                    SaveAttachmentToReplyPost(contId, ofileresponse.filename, UploadStringHTMLSavePost, ofileresponse.url)


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


function SaveAttachmentToReplyPost(contId, filename, UploadStringHTMLSavePost, Attachmenturl) {


    var SaveAttachmentServiceUrl = webMethodUrl + 'GetSaveEditReplyAttachments';
    $.ajax({
        type: 'GET',
        dataType: "json",
        contentType: "application/json; charset=utf-8",
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
            onprogress: function (progress) {
            }
        }
        // contentType: ofileresponse.mimetype
    }).done(function (response) {

    });

}


function EditPostReplyButtonCall() {

    return true;
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
                });
            }

        });

        AttachmentArray = UpdateAttachmentArray;


        if (attachmentURL != "") {

            var uploadServiceUrl = webMethodUrl + 'DeleteAttachmentsFromURL';
            $.ajax({
                type: 'GET',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: uploadServiceUrl,
                data: { fileurl: attachmentURL },
                success: function () {
                    alert('Attachment  is removed successfully');
                    $(this).hide();
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



function saveAttachmentDataWhenPost(buttonElement) {


    var ccId = $(buttonElement).attr("currentPostContenetId");



    var finalStringAttachment = "";

    for (index = 0; index < AttachmentArray.length; ++index) {



        if (ccId == AttachmentArray[index].currentContentId) {



            var CurStringAttachment = "{contentid:'" + AttachmentArray[index].contentid + "',currentContentId:'" + AttachmentArray[index].currentContentId + "',attachmentURL:'" + AttachmentArray[index].attachmentURL + "'}";

            if (finalStringAttachment == "") {
                finalStringAttachment = "" + finalStringAttachment + "" + CurStringAttachment + "";
            }
            else {
                finalStringAttachment = "" + finalStringAttachment + "," + CurStringAttachment + "";
            }


        }


    }
    finalStringAttachment = "[" + finalStringAttachment + "]";

    // finalStringAttachment = "{ AttachmentData:"+finalStringAttachment+" }"



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

function RemoveAttachemnt(ele, contId, id) {



}

function addFileSubTopic(ele, contId, id) {
    var fsClient = filestack.init(oKey);
    fsClient.pick({}).then(function (response) {

        response.filesUploaded.forEach(function (file) {
            var ofileresponse = file;
            var uploadServiceUrl = webMethodUrl + 'GetSaveAttachments';
            $.ajax({
                type: 'GET',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: uploadServiceUrl,
                data: { contentId: contId, fileName: ofileresponse.filename, contentType: ofileresponse.mimetype, size: ofileresponse.size, fileurl: ofileresponse.url },
                success: function () {
                    $("#attach_" + id).hide();
                    $("#delete_" + id).show();
                    debugger;
                    $("#filename_" + id).html(ofileresponse.filename);
                    $("#hdnAttach_" + id).val(ofileresponse.url);
                    //   alert('File is uploaded successfully');
                },
                xhrFields: {
                    onprogress: function (progress) {
                    }
                }
                // contentType: ofileresponse.mimetype
            }).done(function (response) {
                $("#hdnAttachID_" + id).val(response);
                // hideprogress();
                //   GetFilesList(contId);
            });

        });

    });

}

function AjaxCall(MethodName, postdata, methodtype, datatype, contenttype) {

    $.ajax({
        type: methodtype,
        url: webMethodUrl + MethodName,
        contentType: contenttype,
        dataType: datatype,
        data: postdata,
    }).done(function (response) {


        var oData = JSON.parse(response);
        return odat;

    });

}

function ExpandTheTopicRepliedIfAny() {
    var ExpandReplyString = window.location.href;
    //if (ExpandReplyString.toLowerCase().indexOf('contentid') != -1) {   

    if (ExpandReplyString.toLowerCase().indexOf('/pi/') != -1) {
        var aryTemp = ExpandReplyString.split('/');
        var LinkID = aryTemp[aryTemp.length - 1];
        var ContentId = aryTemp[aryTemp.length - 1];
        var topicid = aryTemp[aryTemp.length - 5];

        if (topicid > 0 && ContentId <= -1) {
            var linkcount = $("#dvEachTopic_" + topicid + " .forum-see-more").length - 1;
            if (linkcount == 'nothing') {
                return;
            }

            $("#dvEachTopic_" + topicid + " .forum-see-more")[linkcount].click();
            $('html, body').animate({
                scrollTop: $(".dvEachTopicReplyId_" + LinkID).offset().top
            }, 500);
            return true;
        }
        else {
            var linkcount = $("#dvEachTopic_" + topicid + " .forum-see-more").length - 1;
            if (linkcount == 'nothing') {
                return;
            }

            $("#dvEachTopic_" + topicid + " .forum-see-more")[linkcount].click();
            $('html, body').animate({
                scrollTop: $(".dvEachTopicReplyId_" + LinkID).offset().top
            }, 500);

            return true;
        }

    }
    else {

        var aryTemp = ExpandReplyString.split('/');
        var LinkID = aryTemp[aryTemp.length - 1];
        var ContentId = aryTemp[aryTemp.length - 1];
        var topicid = aryTemp[aryTemp.length - 3];

        if (topicid > 0 && ContentId <= -1) {

            var linkcount = $("#dvEachTopic_" + topicid + " .forum-see-more").length - 1;
            if (linkcount == 'nothing') return;

            $("#dvEachTopic_" + topicid + " .forum-see-more")[linkcount].click();
            $('html, body').animate({
                scrollTop: $(".dvEachTopicReplyId_" + LinkID).offset().top
            }, 500);
            return true;
        }
        else {

            var linkcount = $("#dvEachTopic_" + topicid + " .forum-see-more").length - 1;
            if (linkcount == 'nothing') return;

            $("#dvEachTopic_" + topicid + " .forum-see-more")[linkcount].click();
            $('html, body').animate({
                scrollTop: $(".dvEachTopicReplyId_" + LinkID).offset().top
            }, 500);

            return true;
        }

    }
}