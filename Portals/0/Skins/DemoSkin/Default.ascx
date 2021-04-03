<%@ Control Language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" TagName="LANGUAGE" Src="~/Admin/Skins/Language.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGO" Src="~/Admin/Skins/Logo.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SEARCH" Src="~/Admin/Skins/Search.ascx" %>
<%@ Register TagPrefix="dnn" TagName="USER" Src="~/Admin/Skins/User.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>
<%@ Register TagPrefix="dnn" TagName="PRIVACY" Src="~/Admin/Skins/Privacy.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TERMS" Src="~/Admin/Skins/Terms.ascx" %>
<%@ Register TagPrefix="dnn" TagName="COPYRIGHT" Src="~/Admin/Skins/Copyright.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LINKS" Src="~/Admin/Skins/Links.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LINKTOMOBILE" Src="~/Admin/Skins/LinkToMobileSite.ascx" %>
<%@ Register TagPrefix="dnn" TagName="META" Src="~/Admin/Skins/Meta.ascx" %>
<%@ Register TagPrefix="dnn" TagName="MENU" Src="~/DesktopModules/DDRMenu/Menu.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<!-- Sachin : Added file for Session Time feature -->
<script src="/Resources/Shared/scripts/timeout-dialog/js/timeout-dialog.js"></script>
<link href="/Resources/Shared/scripts/timeout-dialog/css/timeout-dialog.css" rel="stylesheet" />
<script src="/Resources/Shared/scripts/timeout-dialog/js/jquery.idle.min.js"></script>
<script src="/Resources/Shared/scripts/timeout-dialog/js/jquery.titlealert.js"></script>
<dnn:META ID="mobileScale" runat="server" Name="viewport" Content="width=device-width,initial-scale=1" />


<!-- SET: STYLESHEET -->
<link href="https://fonts.googleapis.com/css?family=Montserrat:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/bootstrap.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/font-awesome.min.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/style.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/responsive.css">
<!-- Style fixes or overrides to the efault style sheet -->
<link rel="stylesheet" href="<%=SkinPath%>css/stylefix.css">

<!--DEMO CSS FILE TO ALTER FUNCTIONALITY FOR DEMO
REMOV WHEN NECESSARY -->
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/demo.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>Skin.css">

<!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<script type="text/javascript" src="js/IE9.js"></script>
<!-- Global site tag (gtag.js) - Google Analytics -->
<script async src="https://www.googletagmanager.com/gtag/js?id=UA-149164895-1"></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/jquery-jtemplates/jquery-jtemplates.js"></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/Module/Common.js"></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/context/jquery.contextMenu.js"></script>
<link href="/DesktopModules/ClearAction_SolveSpaces/Scripts/context/jquery.contextMenu.css" rel="stylesheet" />


<!-- Google Tag Manager -->
<script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);
})(window,document,'script','dataLayer','GTM-KLJM4HJ');</script>
<!-- End Google Tag Manager -->


<![endif]-->

<!-- wrapper starts -->

<!-- Google Tag Manager (noscript) -->
<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-KLJM4HJ"
height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
<!-- End Google Tag Manager (noscript) -->


<div class="wrapper">
    <!-- wrapper_cnt starts -->
    <div class="wrapper_cnt">
        <div class="col-sm-12 main_wrapper">
            <header>
                <div class="header_lt col-sm-2 nopadding">
                    <div class="exchange_lt">
                        <div class="table">
                            <div class="table-cell">
                                <dnn:LOGO runat="server" ID="dnnLOGO" alt="ClearAction" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="menu-toggle col-sm-10 nopadding">

                    <div class="header_cnt col-sm-12 nopadding">
                        <%-- Renato: Removing header which is edited from the CMS. Adding the header to the actual template -->
                        <%--
                        <div id="HeaderMenuPane" runat="server"></div>
                        --%>
                        <!-- #include file="parts\ClearAction_Header.ascx" --> 
                    </div>

                </div>
                <div class="clearfix"></div>
            </header>
            <div class="menu">
                <a href="#"><i class="fa fa-bars" aria-hidden="true"></i></a>
            </div>
            <div class="clearfix"></div>
            <div class="cnt_wrapper my-exchange">
                <div id="ContentPane" runat="server" class="SkinFullPane clearfix"></div>
                <div class="cnt_wraper_lt col-sm-12">
                    <div class="intro-titles">
					     <h1 tooltip-id="tt-myexchange-video" class="my-exchange-title info-icon">My Exchange</h1>
				    </div>
 
                    </div>
                    <div class="clearfix"></div>
                   
                    <div class="open_dtls">
					<div id="Analytics_Search_Pane" runat="server">
						<div class="SkinFullPane">
                	<div class="solve-spaces-top-nav">
    <div class="solve-spaces-title">
        <h1 style="font-size:25px">My Exchange<sup style="font-size:10px">TM</sup></h1>
    </div>
      <div class="solve-spaces-search">
        <div style="position:relative;width:100%">
            <input required="" id="CA_SS_txtSearch" value="" type="text">           
			<div onclick="SearchInSolveSpaceForm();" class="CA_SearchGlass"  ></div>
        </div>
        <div class="clearfix"></div>
        <label id="lblClearSearch" onclick="return ClearSearch();" style="display:none;" class="ClearSearch">Clear Search Result</label>
    </div>
	<div class="col-md-4 text-right" style="display:none">
			<select id="CA_SS_ddlSortBy" class="forum-select">
				<option value="1"  class="CA_SortOptions">Alpha a-z</option>
				<option value="2"  class="CA_SortOptions">Alpha z-a</option>
				<option selected="selected" value="3" class="CA_SortOptions">Recent to former</option>
				<option value="4"  class="CA_SortOptions">Former to recent</option>
				<option value="5"  class="CA_SortOptions">Active today</option>
				<option value="6"  class="CA_SortOptions">Active last 7 days</option>
				<option value="7"  class="CA_SortOptions">Active this month</option>
			</select>
		</div>
		</div>
				<div id="CA_SS_dvSSContainer" style="width:100%;clear:both;display:none;" class="bstrap3-material">
</div>
</div>
                        </div>
                        <div class="col-sm-12 disp Grid6_Spaces_LeftPane" id="Grid6_Spaces_LeftPane" runat="server"  >
                        </div>
                        <div class="col-sm-12 disp Grid6_forum_RightPane" id="Grid6_forum_RightPane" runat="server"  >
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="community">
					<!-- calls webcast blogs spaces forum  --> 
                        <div class="col-sm-12 disp Grid4_calls_LeftPane" id="Grid4_calls_LeftPane" runat="server" >
                        </div>
                        <div class="col-sm-12 disp Grid4_webcast_MiddlePane" id="Grid4_webcast_MiddlePane" runat="server">
                        </div>
                        <div class="col-sm-12 disp Grid4_blogs_RightPane" id="Grid4_blogs_RightPane" runat="server" >
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
                <div class="cnt_wrapper_rt col-sm-4">
                    <div id="Right_Connections_PaneA" runat="server">
                    </div>
                    <div id="Right_roles_PaneB" runat="server">
                    </div>
                    <div id="Right_people_PaneC" runat="server">
                    </div>
                </div>
				 <div class="analytics">
					
                        <div id="Analytics_Head_Pane" runat="server" class="SkinFullPane">
						
                        <div id="MyContributions_Pane" runat="server" class="SkinFullPane">
                        </div>
                        <div class="hidden-xs">
                            <div class="SkinFullPane" id="ExpandFullPane" runat="server"></div>

                        </div>
                    </div>
                <div class="clearfix"></div>
            </div>
            <!-- #include file="parts\ClearAction_Footer.ascx" --> 
        </div>
        <div class="clearfix"></div>
    </div>
    <!-- wrapper_cnt ends -->
</div>
<!-- wrapper ends -->
<!-- SET: SCRIPTS -->

<script type="text/javascript" src="<%=SkinPath%>js/bootstrap.min.js"></script>
<script src="<%=SkinPath%>js/jquery.lightbox_me.js"></script>
<script src="<%=SkinPath%>js/jquery.matchHeight.js"></script>
<script src="<%=SkinPath%>js/isotope.pkgd.min.js"></script>
<script type="text/javascript" src="<%=SkinPath%>js/main.js"></script>

<!-- 
    - Script/CSS related to Alert/Tooltip.
    -Ajit : 15-Jan
    -->


<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.0/jquery-confirm.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.0/jquery-confirm.min.js"></script>

<script type="text/javascript" src="<%=SkinPath%>Tipr/tipr.js"></script>
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>Tipr/tipr.css">
<div id="tt-myexchange-video" style="display:none">
    <div class="content">
        <div class="popup_con">
            <h5>My Exchange</h5>
            <p>recommended resources and events tailored to you, based on your answers to Step 3 of "Profile" - update your questionnaire anytime. See My Vault links below.</p>
        </div>
    </div>
    <%--<div class="vidyard-container">
        <script type="text/javascript" id="vidyard_embed_code_sLKAUkuFnazXdysw43Ca5q" src="https://play.vidyard.com/sLKAUkuFnazXdysw43Ca5q.js?v=3.1.1&type=inline"></script>
    </div>--%>
</div>
<style>
.community_calls {
}
</style>
<script>

function SearchInSolveSpaceForm(){

document.getElementById("CA_SS_dvSSContainer").style.display="none";
if(document.getElementById("CA_SS_txtSearch").value!=""){
$(".txtsearch").val(document.getElementById("CA_SS_txtSearch").value);
 $(".btnsearch").click();
//$("#dnn_ctr451_View_btn_search").click();$("#dnn_ctr450_View_btn_search").click();$("#dnn_ctr1494_View_btn_search").click();$("#dnn_ctr1499_View_btn_search").click();


	

}
else{
 var x = document.getElementsByClassName("disp");
  var i;
  for (i = 0; i < x.length; i++) {
    x[i].style.display="none";
  }
}
return false;
}

function getlastcall()
{

CA_LoadSolveSpacesRSearch();

}




    $(document).ready(function () {
	
	document.getElementById("CA_SS_dvSSContainer").style.display="none";
        //$('.tip').tipr();
        ttId = "tt-myexchange-video";
        ttEl = $('[tooltip-id="' + ttId + '"]');
        setTooltip(ttEl, 'bottom', 'click');
        //RG: for the video tooltip, we need different size. Needs to be set when shown (not hidden)
        ttEl.on('shown.bs.tooltip', function () {
            var tooltipId = ttEl.attr("aria-describedby");
            $('#' + tooltipId + ' .tooltip-inner').css('max-width', '450px');
        });
    });
</script>




