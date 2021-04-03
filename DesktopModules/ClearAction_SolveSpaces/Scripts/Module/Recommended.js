var count=0;
var dispcount=0;

$(document).ready(function() {
// Load list of Recommended Solve-Space

if(count==0){
    CA_LoadSolveSpacesR();
}
 var x = document.getElementsByClassName("disp");
	if(dispcount==0)
	{
	
  var i;
  for (i = 0; i < x.length; i++) {
    x[i].style.display="none";
  }
	}else{
			 for (i = 0; i < x.length; i++) {
    x[i].style.display="block";
  }
if($(".community_calls").children(".calls_dtls").children("ul").children("label")[0]!=undefined){
if($(".community_calls").children(".calls_dtls").children("ul").children("label")[0].innerHTML=="No Insight(s) found")
{
document.getElementsByClassName("disp")[2].style.display="none";
}
}

if($(".community_calls").children(".calls_dtls").children("ul").children("label")[1]!=undefined){
if($(".community_calls").children(".calls_dtls").children("ul").children("label")[1].innerHTML=="No Webcast Conversation(s) found")
{
document.getElementsByClassName("disp")[3].style.display="none";
}
}

if($(".community_calls").children(".connection_dtls").children(".connection_cnt").children("ul").children("label")[0]!=undefined){
if($(".community_calls").children(".connection_dtls").children(".connection_cnt").children("ul").children("label")[0].innerHTML=="No Community Call(s) found")
{
document.getElementsByClassName("disp")[4].style.display="none";
}
}

if($(".connections").children(".connection_dtls").children(".connection_cnt").children("ul").children("label")[0]!=undefined){
if($(".connections").children(".connection_dtls").children(".connection_cnt").children("ul").children("label")[0].innerHTML=="No Forum(s) found")
{
document.getElementsByClassName("disp")[1].style.display="none";
}
}

	}
});

/* Solve Spaces related methods */
function CA_LoadSolveSpacesR() {
    var _URL = "/DesktopModules/ClearAction_SolveSpaces/requesthandler.asmx/ListRSolveSpaces";
    var _data = "{'UserID':'" + CA_UserID + "'}";
    CA_MakeRequest(_URL, _data, CA_OnLoadTopSolveSpaces);
}

function CA_OnLoadTopSolveSpaces(d) {
    if (d.length > 0) {
        $("#CS_SS_dvTopSSContainer").setTemplateURL('/DesktopModules/ClearAction_SolveSpaces/templates/tmpRecommended.htm?q='+$.now());
        $("#CS_SS_dvTopSSContainer").show().processTemplate(d);
    }
    else {
        $("#CS_SS_dvTopSSContainer").html("<div class='spaces_dtls' style='margin-left:0px;text-align:center;'><div class='space_lt' style='float:left'><h4 tooltip-id=\"tt-solvespaces\">Solve - Spaces</h4> <div id='tt-solvespaces' class='popup' style='margin-top:50px;'><div class='content'><div class='popup_con'><h5>Solve-Spaces</h5><p>Solve marketing challenges in an interesting and engaging way.</p></div></div ></div ></div ><label style='color:#6992c0;font-weight:normal;padding-top:30px;padding-bottom:20px'>No SolveSpace(s) found</label> <div class='space_rt'><a href='/SolveSpaces'>My Vault</a></div></div >");
        //Ajit Change
        //RG: set the tooltip
			if($(".spaces_dtls").children("label")[0]!=undefined){
if($(".spaces_dtls").children("label")[0].innerHTML=="No SolveSpace(s) found")
{
document.getElementsByClassName("disp")[0].style.display="none";
}
}
        setTooltip($('[tooltip-id="tt-solvespaces"]'), 'top', 'click');
    }
}

function CA_LoadSolveSpacesRSearch() {
	count=1;
	dispcount=1;
    var _search = $(".txtsearch")[0].value;
    var _URL = "/DesktopModules/ClearAction_SolveSpaces/requesthandler.asmx/ListRSolveSpacesSearch";
    var _data = "{'UserID':'" + CA_UserID + "','search':'"+ _search + "'}";
    CA_MakeRequest(_URL, _data, CA_OnLoadTopSolveSpaces);

}

