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
<!-- -->

<dnn:META ID="mobileScale" runat="server" Name="viewport" Content="width=device-width,initial-scale=1" />


<!-- SET: STYLESHEET -->
<link href="https://fonts.googleapis.com/css?family=Montserrat:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/bootstrap.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/font-awesome.min.css">
<link rel="stylesheet" href="<%=SkinPath%>css/aria-tooltip.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/style.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/responsive.css">


<!--DEMO CSS FILE TO ALTER FUNCTIONALITY FOR DEMO
REMOV WHEN NECESSARY -->
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/demo.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>Skin.css">

<!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<script type="text/javascript" src="js/IE9.js"></script>
<![endif]-->

<!-- wrapper starts -->
<div class="wrapper">
   <!-- wrapper_cnt starts -->
  <div class="wrapper_cnt">
    <div class="col-sm-12 main_wrapper">
    	<header>
        	<div class="header_lt col-sm-2 nopadding">
            	<div class="exchange_lt">
					<dnn:LOGO runat="server" ID="dnnLOGO"  alt="ClearAction" />
                   
                </div>
			</div>
			<div class="menu-toggle col-sm-10 nopadding">
			
            <div class="header_cnt col-sm-12 nopadding">
<div class="exchange_navcontainer" id="HeaderMenuPane" runat="server">
					
                    </div>
					<div class="exchange_settings">
						<ul>
							<li>
								<a href="#" class="settings">More</a>
								<ul>
									<li>
										<a href="profile.aspx">
											<span>Profile</span>
											<figure>
												<img src='<%=UserController.GetCurrentUserInfo().Profile.PhotoURL%>' width="23" height="23" alt="icon-profile" class="img-responsive">
										   </figure>
										</a>
									</li>
									<%--<li class="demo">
										<a href="#">
											<span>Alerts</span>
											<figure>
												<img src="images/icon-alerts.png" width="20" height="21" alt="icon-alerts" class="img-responsive">
										   </figure>
										   <div class="pos_1">
												<div class="tbl">
													<div class="tbl_cell">
														<small>88</small>
													</div>
												</div>
										   </div>
										</a>
									</li>--%>
									<li class="demo">
										<a href="/Contact" class="Header_Contact">
											<span>Contact</span>
										</a>
									</li>
									<li class="demo">
										 <dnn:LOGIN ID="dnnlogin" runat="server" CssClass="Header_Login"/> 
										</li>
								</ul>
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
        <div class="cnt_wrapper my-exchange">
        <div id="ContentPane" runat="server" class="SkinFullPane clearfix"></div>
        	<div class="cnt_wraper_lt col-sm-8">
            <div id="Top_Title_Pane" runat="server" class="SkinFullPane">
				
                </div>
				<div class="clearfix"></div>
            	<div class="analytics">
                <div id="Analytics_Head_Pane" runat="server" class="SkinFullPane">
                	
                    </div>
                    <div id="MyContributions_Pane" runat="server" class="SkinFullPane">
                   
                    </div>
                     <div class="hidden-xs">
                     <div class="SkinFullPane" id="ExpandFullPane" runat="server"></div>
                     
                        </div>
                </div>
                <div class="open_dtls">
                	<div class="spaces col-sm-6" id="Grid6_Spaces_LeftPane" runat="server">
                	
                </div>
                	<div class="forum col-sm-6" id="Grid6_forum_RightPane" runat="server">

                </div>
                	<div class="clearfix"></div>
                </div>
                <div class="community">
                	<div class="calls col-sm-4" id="Grid4_calls_LeftPane" runat="server">
       
                    </div>
                    <div class="webcast col-sm-4" id="Grid4_webcast_MiddlePane" runat="server">
                 
                    </div>
                    <div class="blogs col-sm-4" id="Grid4_blogs_RightPane" runat="server">
                    	
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="cnt_wrapper_rt col-sm-4">
            	<div class="connections" id="Right_Connections_PaneA" runat="server">

                </div>
                <div class="connections roles" id="Right_roles_PaneB" runat="server">
                	
                </div>
                <div class="connections roles" id="Right_people_PaneC" runat="server">
                	
                </div>
            </div>
            <div class="clearfix"></div>
        </div>
        <footer>
            <div class="col-sm-4 footerpane_resizeLeft">
     <%--  <dnn:LOGIN ID="dnnlogin" runat="server" CssClass="FooterLogin" />--%>
            </div>        
            <div class="col-sm-8 footerpane_resizeRight" id="Footer_Pane" runat="server">
        
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
