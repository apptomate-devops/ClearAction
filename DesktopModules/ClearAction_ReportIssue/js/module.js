var modType = '';
var issueTitle = '';
function RI_ShowNext() {
    $("#dvStep2").show();
    $("#dvStep1").hide();
    $("#dvrithanks").hide();
    
    var titlerpt = $("label[for='rpttitle']").html();
    var selType = $('input[name=issueType]:checked').val();
    titlerpt = titlerpt.replace("#type#", selType);
    $("label[for='rpttitle']").html(titlerpt);
    
    return false;
}

function RI_ShowIssue(mt, title) {
    modType = mt;
    issueTitle = title;
    resetRIForm();
    $("#dvStep1").show();
    $("#dvStep2").hide();
    $("#dvrithanks").hide();
    $('body').append("<div class='ImgPopBG'></div>");
    $('body').append("<div class='ImgPopCont'></div>");
    var ImgPopupPos = $(window).width() / 2 - $(".ImgPopCont").width() / 2;
    $(".ImgPopCont").css("left", ImgPopupPos);
    $('.ImgPopCont').append($('#dvriCont')); // append -> object
    $(".ImgPopCont").append("<a href='javascript:void(0)' class='ImgPopClose' onclick='PopupClose();'>Close</a>");
    $(".DivRPCont").remove();
    return false;
}
function PopupClose()
{
    $('body').append("<div class='DivRPCont' style='display:none;'></div>");
    $(".DivRPCont").append($('#dvriCont')); // append -> object
    //$(".ImgPopClose").click(function () {
    $(".ImgPopBG").remove();
    $(".ImgPopCont").remove();
    $(".ImgPopCont div").remove();
    $(".ImgPopBigImage").remove();
    $(".ImgPopClose").remove();
}
function RI_ShowStep1() {
    $("#dvStep1").show();
    $("#dvStep2").hide();
    return false;
}

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    return pattern.test(emailAddress);
}

function resetRIForm() {
    $("#txtriDesc").val('');
    $("#txtriEmail").val('');
    $("#txtrilnkname").val('');
    $("input[name=issueType][value='Spam']").prop('checked', true);
}
//Perform user login
function RI_SubmitIssue() {
    var message = $.trim($("#txtriDesc").val());
    var email = $.trim($("#txtriEmail").val());
    var linkname = $.trim($("#txtrilnkname").val());
    if (linkname.length < 1 ) {
        alert('Please enter link.');
        $("#txtrilnkname").focus();
        return false;
    }

    if (message.length < 1 ) {
        alert('Please enter message.');
        $("#txtriDesc").focus();
        return false;
    }

    if (email.length < 1) {
        alert('Please enter email.');
        $("#txtriEmail").focus();
        return false;
    }

    if (!isValidEmailAddress(email)) {
        alert('Please enter valid email.');
        $("#txtriEmail").focus();
        return false;
    }

    var selType = $('input[name=issueType]:checked').val();
    var _URL = "/DesktopModules/ClearAction_ReportIssue/Mailer.ashx";

    var _data = "{type:'" + unescape(modType) + "',title:'" + unescape(issueTitle) + "',message:'" + unescape(message) + "',linkname:'" + unescape(linkname) + "',rpttype:'" + unescape(selType) + "',email:'" + unescape(email) + "'}";
    
    jQuery.ajax({
        type: 'POST',
        url: _URL,
        data: { type: modType, title: issueTitle, message: message, linkname: linkname, email: email, locURL: location.href},
        //data: { subject: options.subject, name: jQuery(this_id_prefix + '#name').val(), email: jQuery(this_id_prefix + '#email').val(), message: jQuery(this_id_prefix + '#message').val(), locURL: location.href },
        success: function (data) {
            if (data == 'success') {
                $("#dvStep2").hide();
                $("#dvStep1").hide();
                $("#dvrithanks").show();
            }
            else {

                $("#dvStep2").hide();
                $("#dvStep1").hide();
                $("#dvrithanks").show();
            }
        },
        error: function () {
            $("#dvStep2").hide();
            $("#dvStep1").hide();
            $("#dvrithanks").show();
        }
    });
}

