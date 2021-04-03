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
<dnn:meta id="mobileScale" runat="server" name="viewport" content="width=device-width,initial-scale=1" />

<!-- Sachin : Added file for Session Time feature -->
<script src="/Resources/Shared/scripts/timeout-dialog/js/timeout-dialog.js"></script>
<link href="/Resources/Shared/scripts/timeout-dialog/css/timeout-dialog.css" rel="stylesheet" />
<script src="/Resources/Shared/scripts/timeout-dialog/js/jquery.idle.min.js"></script>
<script src="/Resources/Shared/scripts/timeout-dialog/js/jquery.titlealert.js"></script>
<!-- SET: STYLESHEET -->
<link href="https://fonts.googleapis.com/css?family=Montserrat:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/bootstrap.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/font-awesome.min.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/style.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/responsive.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>ProfileSkin.css">
<!-- Style fixes or overrides to the efault style sheet -->
<link rel="stylesheet" href="<%=SkinPath%>css/stylefix.css">

<!--DEMO CSS FILE TO ALTER FUNCTIONALITY FOR DEMO
REMOV WHEN NECESSARY -->
 
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>Skin.css">

<!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<script type="text/javascript" src="js/IE9.js"></script>

<!-- Google Tag Manager -->
<script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);
})(window,document,'script','dataLayer','GTM-KLJM4HJ');</script>
<!-- End Google Tag Manager -->

<![endif]-->

<!-- Google Tag Manager (noscript) -->
<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-KLJM4HJ"
height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
<!-- End Google Tag Manager (noscript) -->

<!-- wrapper starts -->
<div class="wrapper">
    <!-- wrapper_cnt starts -->
    <div class="wrapper_cnt">
        <div class="col-sm-12 main_wrapper">
            <header>
                <div class="header_lt col-sm-2 nopadding">
                    <div class="exchange_lt">
						<div class="table">
							<div class="table-cell">
							<dnn:LOGO runat="server" ID="dnnLOGO"  alt="ClearAction" />
							</div>
						</div>
					</div>
                </div>
                <div class="menu-toggle col-sm-10 nopadding">


                    <div class="header_cnt col-sm-12 nopadding">
                        <div class="exchange_rt  nopadding">
                            <h1>Profile</h1>
                            <div class="clearfix"></div>
                        </div>
                        <div class="exchange_settings">
                            <ul>
                                <li class="demo">
                                    <a style="cursor:pointer" href="//<%#DotNetNuke.Common.Globals.GetPortalSettings().PortalAlias.HTTPAlias %>/profile?utm_source=menu">
                                        <span>Profile</span>
                                        <figure>
                                            <img src='<%=UserController.GetCurrentUserInfo().Profile.PhotoURL%>' alt="icon-profile" class="img-responsive">
                                        </figure>
                                    </a>
                                </li>
                                <li class="demo">
                                    <dnn:login id="dnnlogin" runat="server" cssclass="Header_Login_styleFixcss"  style="cursor:pointer"/>
                                </li>
                            </ul>
                        </div>
                    </div>


                </div>
                <div class="clearfix"></div>
            </header>
            <div class="menu">
                <a href="#"><i class="fa fa-bars" aria-hidden="true"></i></a>
            </div>
            <div class="clearfix"></div>
            <div class="my-exchange">
                <div id="ContentPane" runat="server" class="SkinFullPane clearfix"></div>
                <div class="cnt_wraper_lt col-sm-8">
                    <div id="Top_Title_Pane" runat="server" class="SkinFullPane">
					
                    </div>
                    <div class="clearfix"></div>
                    <div class="">
                        <div id="Analytics_Head_Pane" runat="server" class="SkinFullPane">
                        </div>
                        <div id="MyContributions_Pane" runat="server" class="SkinFullPane">
                        </div>
                        <div class="hidden-xs">
                            <div class="SkinFullPane" id="ExpandFullPane" runat="server"></div>

                        </div>
                    </div>
                    <div class="open_dtls" style="padding: 0px 0 0px 0;">
                        <div class="col-sm-6" id="Grid6_Spaces_LeftPane" runat="server">
                        </div>
                        <div class="col-sm-6" id="Grid6_forum_RightPane" runat="server">
                        </div>
                        <div class="clearfix"></div>
                    </div>
                   
                </div>
				 <div class="community">
                        <div class="col-sm-4" id="Grid4_calls_LeftPane" runat="server">
                        </div>
                        <div class="col-sm-4" id="Grid4_webcast_MiddlePane" runat="server">
                        </div>
                        <div class="col-sm-4" id="Grid4_blogs_RightPane" runat="server">
                        </div>
                        <div class="clearfix"></div>
                    </div>
                <div class="cnt_wrapper_rt col-sm-4">
                    <div class="" id="Right_Connections_PaneA" runat="server">
                    </div>
                    <div class="roles" id="Right_roles_PaneB" runat="server">
                    </div>
                    <div class="roles" id="Right_people_PaneC" runat="server">
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
           <footer>
        	    <div class="footer_dtls">
            	    <span>A Service of</span>
                    <a href="#">
                	    <figure>
                		    <img src="/portals/0/footer_logo.png" alt="footer_logo" class="img-responsive" width="129" height="26">
                        </figure>
                    </a>
                    <div class="clearfix"></div>
                </div>
                <div class="clearfix"></div>
            </footer>
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
<script>
    $(document).ready(function () {
        $('.tip').tipr();
    });
</script>




