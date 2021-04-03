$(document).ready(function() {
// Load list of completed Solve-Space
    CA_LoadTopSolveSpaces();
});

/* Solve Spaces related methods */
function CA_LoadTopSolveSpaces() {
    var _URL = "/DesktopModules/ClearAction_SolveSpaces/requesthandler.asmx/ListCompletedSolveSpaces";
    var _data = "{'UserID':'" + CA_UserID + "'}";
    CA_MakeRequest(_URL, _data, CA_OnLoadTopSolveSpaces);
}

function CA_OnLoadTopSolveSpaces(d) {
    if (d.length > 0) {
        $("#CS_SS_dvTopSSContainer").setTemplateURL('/DesktopModules/ClearAction_SolveSpaces//templates/tmpTopSolveSpaces.htm');
        $("#CS_SS_dvTopSSContainer").show().processTemplate(d);
    }
    else {
        $("#CS_SS_dvTopSSContainer").html("<div style='margin-left:40px;'><label style='color:red;font-weight:normal'>Sorry, No solve-space(s) found</label></div>");
    }
}