var CA_WhichButton = -1;
//var CA_Margins = { top: 70, bottom: 40, left: 30, width: 550 };
$(document).ready(function () {
    $("#CA_btnShareSummary, #CA_btnShareLink").click(function () {
        CA_PopuplateUsers();
    })
    CA_ListConnections();
    $(".CA_SSPName").text($("h1").text().trim());
});
$(window).load(function () {
    //CA_GetCommentCount();
});

//function generatePdf() {
//    var doc = new jsPDF('p', 'pt', 'letter')
//    doc.setFontSize(18);
//    var specialElementHandlers = {
//        '#editor': function (element, renderer) {
//            return true;
//        }
//    };

//    doc.fromHTML(
//        $('.c-form').html(),
//        CA_Margins.left,
//        CA_Margins.top,
//        { width: CA_Margins.width },
//        function (dispose) {
//            //   headerFooterFormatting(doc, doc.internal.getNumberOfPages());
//        },
//        CA_Margins
//    );
//    var binary = doc.output();
//    return binary ? btoa(binary) : "";
//}
//function ParseHTML2Pdf() {
//    var reqData = generatePdf();
//    $.ajax({
//        url: "/DesktopModules/ClearAction_SummaryActions/rh.asmx/SendOverEmail",
//        data: JSON.stringify({ data: reqData }),
//        dataType: "json", type: "POST", contentType: "application/json; charset=utf-8",
//        success: function (d) {

//        }
//    });
//}
var CA_Subject = '';
var CA_Content = '';
var CA_uIDs = '';
CA_WhichButton = 0;
var selectedUsers = ''
function CA_ValidateNSend(o) {
    $(o).attr('disabled', true);
    $(o).text('Please wait...');
    selectedUsers = $("#CA_ulUsers input:checked");
    if (selectedUsers.length == 0) {
        CA_UpdateButtonText(CA_WhichButton);
        $(o).removeAttr('disabled');
        CA_WarnUser('Please select atleast one connection, to share the summary detail');
        return false;
    }
    else {
        $.each(selectedUsers, function (i, o) {
            CA_uIDs += $(o).attr('RelatedID') + ',';
        })
    }
    if (CA_WhichButton == 1) {
        CA_ShareSummary();
    }
    else { CA_ShareLink(); }
    return true;
}

function CA_SetButton(Button) {
    CA_WhichButton = Button;
    CA_UpdateButtonText(Button);
}
function CA_UpdateButtonText(Button) {
    if (Button == 1) {
        $("#CA_H4Header").html('Share this Solve-Space <br>WITH my answers:');
        $("#CA_btnSharePDF").text("Send via email");
    }
    else {
        $("#CA_H4Header").html('Share this Solve-Space <br>WITHOUT my answers:');
        $("#CA_btnSharePDF").text("Send Message");
    }
}
function CA_WarnUser(msg) {
    $.alert({ title: '', type: 'modern', content: msg });
}
var CA_Connections = '';
function CA_ListConnections() {
    $.ajax({
        url: "/DesktopModules/ClearAction_SummaryActions/rh.asmx/GetUserConnections",
        data: JSON.stringify({ UserID: CA_UserID }),
        dataType: "json", type: "POST", contentType: "application/json; charset=utf-8",
        success: function (d) {
            CA_Connections = d.d;
        }
    });
}
function CA_ShareSummary() {
    reqData = CA_GetFormattedHTML();
    $.ajax({
        dataType: "json", type: "POST", contentType: "application/json; charset=utf-8",
        url: "/DesktopModules/ClearAction_SummaryActions/rh.asmx/SendOverEmail",
        data: JSON.stringify({ data: reqData, csvIDs: CA_uIDs, senderID: CA_UserID }),
        success: function (d) {
            $("#CA_btnSharePDF").removeAttr('disabled');
            CA_UpdateButtonText(1);
            $.alert({ title: '', type: 'modern', content: 'Shared WITH answers successfully with the selected connections.' });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $("#CA_btnSharePDF").removeAttr('disabled');
            CA_UpdateButtonText(1);
        }
    });
}
function CA_ShareLink() {
    var _Link = document.location.href.slice(0, document.location.href.indexOf('summary') - 1);
    $.each(selectedUsers, function (i, o) {
        var toUserID = $(o).attr('RelatedID');
        var RelatedName = $(o).attr("RelatedName");
        var FromUserId = CA_UserID;
        var sBody = 'Hey ' + RelatedName + ' I just took this solvespace. GO <a href=' + _Link + ' target="_blank" onclick="window.open(\'' + _Link + '\');">here</a> to add it to your vault. I think you would be interested in taking it.';
        $.get('/DesktopModules/ClearAction_Connections/API/Connections/SendMessage?FromUserID=' + CA_UserID + '&ToUserID=' + toUserID + '&Subject=""&Body="' + sBody + '"&Filename=""&AttachLink=""'
            , function (d) {
                $("#CA_btnSharePDF").removeAttr('disabled');
                CA_UpdateButtonText(2);
            });
    });
    $.alert({ title: '', type: 'modern', content: 'Shared WITHOUT answers successfully with the selected connections.' });
    CA_UpdateButtonText(2);
}
function CA_PopuplateUsers() {
    $("#CA_ulUsers").empty();
    try {
        for (var i = 0; i <= CA_Connections.length; i++) {
            var oCurr = CA_Connections[i];
            $("#CA_ulUsers").append('<li ><div><input type="checkbox" uEmail="' + oCurr.RelatedEmail + '" RelatedID="' + oCurr.RelatedUserID +
                '" RelatedName="' + oCurr.RelatedFullName + '" /><img onerror="this.src=\'/images/no_avatar.gif\';" src="' + oCurr.ProfilePic +
                '"/><div class="user_info">' + oCurr.RelatedFullName + '<br/>' + oCurr.Title + '<br> Influencer Role: ' +
                oCurr.Roles + '</div></div><div style="clear:both;"></div></li>');
        }
    } catch (e) { }
}
function CA_GotoJoinDiscussionLink(o) {
    $(o).attr('disabled', true);
    $("#CA_lnkJoinTheDiscussion").text('Please wait...')
    $.ajax({
        url: "/DesktopModules/ClearAction_SummaryActions/rh.asmx/GetJoinDiscussionLink",
        data: JSON.stringify({ SSID: CA_SS_SSID }),
        dataType: "json", type: "POST", contentType: "application/json; charset=utf-8",
        success: function (d) {
            $("#CA_lnkJoinTheDiscussion").text('Join the discussion...')
            window.open(d.d, '_blank');
        }
    });
}

//function CA_DownloadPDF(o) {
//    var _HTML = "<strong>" + $("h1").html() + "</strong><br>";
//    var _Responses = $(".c-form > .form-group").slice(1, 4);
//    var _Count = _Responses.length;
//    $.each(_Responses, function (i, o) {
//        _HTML += $(o).html();
//    });
//    _HTML = _HTML.replace(/<script>[\s\S]*?<\/script>/, '');
//    $('[id*="hdHTMLString"]').val(_HTML);
//    return true;
//}

function CA_DownloadPDFNew(o) {
    var _HTMLFinal = CA_GetFormattedHTML();
    $('[id*="hdHTMLString"]').val(_HTMLFinal);
    return true;
}

function CA_GetFormattedHTML() {
    // add ssp name as a document header text
    var _HTML = "<h2><u><strong>" + $("h1").html() + "</strong></u></h2><br>";
    $("#finalPDFHTMLContent").html('');// reset the div to empty 

    $(".ss_summary").each(function (index) {

        var stepHtml = $(this).html();
        var finalStepHTML = '<div class="ss_summary">' + stepHtml + '</div>';

        _HTML += finalStepHTML;

    });


    _HTML = _HTML.replace(/<script>[\s\S]*?<\/script>/, '');

    $("#finalPDFHTMLContent").html(_HTML)

    var currentSiteHTTPProtocol = location.protocol;
    var currentHostName = window.location.host;

    $("#finalPDFHTMLContent :hidden").each(function () {
        if ($(this).css("display") == "none") {
            $(this).remove();
        }
    });


    $("<br>").insertBefore("#finalPDFHTMLContent table");

    $("#finalPDFHTMLContent table th").each(function (index) {
        $(this).attr("bgcolor", "silver");

    });

    $("#finalPDFHTMLContent table").each(function (index) {
        $(this).attr("border", "1");
        $(this).attr("cellpadding", "5");
    });


    $("#finalPDFHTMLContent img").each(function (index) {

        var currentImgSRC = $(this).attr("src");
        var finaleimgUrl = "" + currentSiteHTTPProtocol + "//" + currentHostName + "" + currentImgSRC + "";

        $(this).attr("width", '100');
        $(this).attr("height", '15');
        $(this).attr("src", finaleimgUrl);
    });

    $("#finalPDFHTMLContent .bold").each(function (index) {

        var currentBoldElementHtml = $(this).html();

        $(this).html("<b>" + currentBoldElementHtml + "</b>")

    });


    $("#finalPDFHTMLContent h4").each(function (index) {


        var stepNumaber = $(this).find("span:first").text()
        stepNumaber = stepNumaber.trim()



        if (stepNumaber == '1') {
            $(this).find("span:first").remove();
            $(this).prepend("<img  width='20' height='20'  src='" + currentSiteHTTPProtocol + "//" + currentHostName + "/Portals/0/Skins/DemoSkin/images/solvespaces/summary-1.png' > &nbsp ");
        }
        else if (stepNumaber == '2') {
            $(this).find("span:first").remove();
            $(this).prepend("<img  width='20' height='20'  src='" + currentSiteHTTPProtocol + "//" + currentHostName + "/Portals/0/Skins/DemoSkin/images/solvespaces/summary-2.png' > &nbsp");

        }
        else if (stepNumaber == '3') {
            $(this).find("span:first").remove();
            $(this).prepend("<img  width='20' height='20'  src='" + currentSiteHTTPProtocol + "//" + currentHostName + "/Portals/0/Skins/DemoSkin/images/solvespaces/summary-3.png' > &nbsp");
        }
        else if (stepNumaber == '4') {
            $(this).find("span:first").remove();
            $(this).prepend("<img  width='18' height='18'  src='" + currentSiteHTTPProtocol + "//" + currentHostName + "/Portals/0/Skins/DemoSkin/images/solvespaces/summary-4.png' > &nbsp");
        }
        else {
            //do Nothing
        }

        var currentTagHtml = $(this).html();
        //var finaleTagHtml = "<br><h2 color='#6992c0'><strong>" + currentTagHtml + "</strong></h2>";
        var finaleTagHtml = "<br><br><h3 color='#6992c0'><font size='5'>" + currentTagHtml + "</font></h3>";

        $(this).html(finaleTagHtml);

    });

    return $("#finalPDFHTMLContent").html();


}
function CA_GetCommentCount() {
    $.ajax({
        url: "/DesktopModules/ClearAction_SummaryActions/rh.asmx/GetForumCommentCount",
        data: JSON.stringify({ SSID: CA_SS_SSID }),
        dataType: "json", type: "POST", contentType: "application/json; charset=utf-8",
        success: function (d) {
            $("#CA_dvCommentCount").text(d.d);
        }
    });
}

function CA_PrintSSP() {
    var _HTML = CA_GetFormattedHTML();
    var newWin = window.open('', 'Print-Window');
    newWin.document.open();
    newWin.document.write('<html><body onload="window.print()">' + _HTML + '</body></html>');
    newWin.document.close();
    setTimeout(function () { newWin.close(); }, 10);
    return false;
}