$(document).ready(function () {
    try {
        CA_GetSolveSpaceAnalytics();
        CA_GetForumAnalytics();
        CA_GetBlogAnalytics();
        CA_GetCCAnalytics();
        CA_GetWCAnalytics();
        CA_UserComponentActivity();
        CA_ReEvaluateLiveGraph(); // now draw the line graph
    } catch (e) { }
    $(".CA_SingleGraph").parent().css('border-bottom', '4px solid #6992c0')
    $(".CA_SingleGraph").parent().css('height', '120px');
    $(".CA_SingleGraph").parent().css('weight', '120px')
    if ($(".CA_SingleGraph").is(":visible")) {
        $("canvas").attr("width", "120");
        $("canvas").attr("height", "120");
        $(".CA_SingleGraph").css("width", "100%");
        $("#dvAnalyticsContainer div:first-child").css("height", "217px");
        $("#dvAnalyticsContainer").removeClass("MainDiv").addClass("solve-space-graph");
        $("#dvInnerContainer").css("width", "100%");
        $("#dvInnerContainer").css("text-align", "center");
       $(".GraphContainer").css("margin-left", "-23px");
       $(".GraphFooter").css("top", "-35px");
       $(".GraphFooter").css("left", "0px");
    }
});
function CA_PlotSolveSpace(d) {
   // CA_PlotGraph("S", 'chartSolveSpace', d.Completed, d.Total, "#B3B3B3", "#AAC80C");
    CA_PlotGraph("S", 'chartSolveSpace', d.Completed, d.Total, "#B3B3B3", "#AAC80C");
}
function CA_PlotForum(d) {
    //CA_PlotGraph("F", 'chartForum', d.Completed, d.Total, "#B3B3B3", "#0095C6");
    CA_PlotGraph("F", 'chartForum', d.Completed, d.Total, "#B3B3B3", "#0095C6");
}
function CA_PlotInsight(d) {
    CA_PlotGraph("I", 'chartInsight', d.Completed, d.Total, "#B3B3B3", "#004A7E");
}
function CA_PlotCC(d) {
    CA_PlotGraph("CC", 'chartCC', d.Completed, d.Total, "#B3B3B3", "#A54F86");
}
function CA_PlotWC(d) {
    CA_PlotGraph("WC", 'chartWC', d.Completed, d.Total, "#B3B3B3", "#FFB12A");
}
function CA_PlotGraph(ComponentID, canvasID, Completed, Total, RemainingColor, CompletedColor) {
    var Remains = (Total - Completed);
    if (Total == 0) {
        $("#lblPercent" + ComponentID).text('0%');
        $("#lblTopValue" + ComponentID).text('0');
    }
    else {
        $("#lblPercent" + ComponentID).text(parseInt((Completed / (Total)) * 100) + "%");
        $("#lblTopValue" + ComponentID).text(Completed);
    }
    if (Total == 0) {
        Completed = 0; Remains = 1; };
    var options = {
        "colors": [CompletedColor, RemainingColor],
        "data": [Completed, Remains],
        "shade_factor": "0", //optional
        "shade_area_percent": "0.31",
        "inner_area_factor": "0.87"
    };
    $('#' + canvasID).drawDonut(options);
  //  CA_ReEvaluateLiveGraph();
    $("#lblRatio" + ComponentID).text(Completed + "/" + (Total));
}

function CA_PlotGraphEx_NIU(ComponentID, canvasID, Completed, Total, RemainingColor, CompletedColor) {
    var Remains = (Total - Completed);
    if (Total == Completed) Remains = 0;
    var ctx = document.getElementById(canvasID).getContext('2d');
    var options = {
        tooltips: { enabled: false },
        responsive: true,
        title: { display: false },
        animation: {
            animateScale: false,
            animateRotate: false
        }
    };

    var data = { datasets: [{ data: [Completed, Remains], backgroundColor: [CompletedColor, RemainingColor] }] };
    if (Total == 0) {
        $("#lblPercent" + ComponentID).text('0%');
        data = { datasets: [{ data: [0, 1], backgroundColor: [CompletedColor, RemainingColor] }] };
        $("#lblTopValue" + ComponentID).text('0');
        CA_ReEvaluateLiveGraph();
    }
    else {
        $("#lblPercent" + ComponentID).text(parseInt((Completed / (Total)) * 100) + "%");
        $("#lblTopValue" + ComponentID).text(Completed);
        CA_ReEvaluateLiveGraph();
    }
    var myChart = new Chart(ctx, {
        type: 'doughnut', data: data, options: options
    });
    $("#lblRatio" + ComponentID).text(Completed + "/" + (Total));
}

function CA_GetSolveSpaceAnalytics() {
    var _URL = "/DesktopModules/ClearAction_UserAnalytics/rh.asmx/GeSolveSpaceAnalytics";
    var _data = "{'UserID':'"+CA_UserID+"'}";
    CA_MakeRequest(_URL, _data, CA_PlotSolveSpace,null,true);
}

function CA_GetForumAnalytics() {
    var _URL = "/DesktopModules/ClearAction_UserAnalytics/rh.asmx/GeComponentAnalytics";
    var _data = "{'UserID':'" + CA_UserID + "','ComponentTypeID':'1'}";
    CA_MakeRequest(_URL, _data, CA_PlotForum, null, true);
}

function CA_GetBlogAnalytics() {
    var _URL = "/DesktopModules/ClearAction_UserAnalytics/rh.asmx/GeComponentAnalytics";
    var _data = "{'UserID':'" + CA_UserID + "','ComponentTypeID':'2'}";
    CA_MakeRequest(_URL, _data, CA_PlotInsight, null, true);
}
function CA_GetCCAnalytics() {
    var _URL = "/DesktopModules/ClearAction_UserAnalytics/rh.asmx/GeComponentAnalytics";
    var _data = "{'UserID':'" + CA_UserID + "','ComponentTypeID':'4'}";
    CA_MakeRequest(_URL, _data, CA_PlotCC, null, true);
}
function CA_GetWCAnalytics() {
    var _URL = "/DesktopModules/ClearAction_UserAnalytics/rh.asmx/GeComponentAnalytics";
    var _data = "{'UserID':'" + CA_UserID + "','ComponentTypeID':'5'}";
    CA_MakeRequest(_URL, _data, CA_PlotWC, null, true);
}
function CA_UserComponentActivity() {
    var _URL = "/DesktopModules/ClearAction_UserAnalytics/rh.asmx/GetUserActvityData";
    var _data = "{'UserID':'" + CA_UserID + "'}";
    CA_MakeRequest(_URL, _data, function (d) {
            $("#lblTopValueF").text(d.Interactions);
            $("#lblTopValueI").text(d.BlogComments);
            $("#lblTopValueCC").text(d.CCJoined);
            $("#lblTopValueWC").text(d.WCJoined);
            CA_ReEvaluateLiveGraph();
    },null, true);
}

var counter = 0;
function CA_ReEvaluateLiveGraph() {
    var ItemWithNoItems = 0;
    var done = $("#lblTopValueS").text();
    var interactions = $("#lblTopValueF").text();
    var comments = $("#lblTopValueI").text();
    var CCJoined = $("#lblTopValueCC").text();
    var WCJoined = $("#lblTopValueWC").text();
    var denom = 90;
     if (done == 0 || interactions == 0 || comments == 0 || CCJoined == 0 || WCJoined == 0) denom = 60;
    if (isNaN(done) || done == 0) { done = 0; ItemWithNoItems++; }
    if (isNaN(interactions) || interactions == 0) { done = 0; ItemWithNoItems++; }
    if (isNaN(comments) || comments == 0) { comments = 0; ItemWithNoItems++; }
    if (isNaN(CCJoined) || CCJoined == 0) { CCJoined = 0; ItemWithNoItems++; }
    if (isNaN(WCJoined) || WCJoined == 0) { WCJoined = 0; ItemWithNoItems++; }

    var Total = parseInt(done) + parseInt(interactions) + parseInt(comments) + parseInt(CCJoined) + parseInt(WCJoined);
    Total = Total + ItemWithNoItems;
    // Individual Percent
    var doneP = ((done / Total) * denom).toFixed(0);
    var interactionP = ((interactions / Total) * denom).toFixed(0);
    var commentP = ((comments / Total) * denom).toFixed(0);
    var CCJoinsP = ((CCJoined / Total) * denom).toFixed(0);
    var WCJoinsP = ((WCJoined / Total) * denom).toFixed(0);

    // How Many are stretching to minimum percent
    var minimumP = 13; // minimum percent to any item (even if it is zero)
    ItemWithNoItems = 0;
    if (doneP <= minimumP) { doneP = minimumP; ItemWithNoItems++; }
    if (interactionP <= minimumP) { interactionP = minimumP; ItemWithNoItems++; }
    if (commentP <= minimumP) { commentP = minimumP; ItemWithNoItems++; }
    if (CCJoinsP <= minimumP) { CCJoinsP = minimumP; ItemWithNoItems++; }
    if (WCJoinsP <= minimumP) { WCJoinsP = minimumP; ItemWithNoItems++; }
    // Total participations
    var TotalP = parseInt(doneP) + parseInt(interactionP) + parseInt(commentP) + parseInt(CCJoinsP) + parseInt(WCJoinsP);

    var extraP = 13;
    // calculate the extra percent and share it among the items with no activity 
    if (ItemWithNoItems > 0) {
        extraP = (TotalP - denom);
        extraP = (extraP / ItemWithNoItems).toFixed(0);
        extraP = minimumP - extraP;
    }
    // assign the final values of percent calculated after sharing to each component
    if (isNaN(doneP) || doneP <= minimumP) {
        doneP = extraP;
    }
    if (isNaN(interactionP) || interactionP <= minimumP) {
        interactionP = extraP;
    }
    if (isNaN(commentP) || commentP <= minimumP) {
        commentP = extraP;
    }
    if (isNaN(CCJoinsP) || CCJoinsP <= minimumP) {
        CCJoinsP = extraP;
    }
    if (isNaN(WCJoinsP) || WCJoinsP <= minimumP) {
        WCJoinsP = extraP;
    }
    $(".completed").css("width", doneP + "%"); // SSP line
    $(".interactions").css("width", interactionP + "%"); // Forum line
    $(".comments").css("width", commentP + "%"); // Insights Line
    $(".interactions1").css("width", CCJoinsP + "%"); // CC Line
    $(".joined").css("width", WCJoinsP + "%"); //WC Line
}