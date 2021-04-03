<%@ Control Language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" TagName="STYLES" Src="~/Admin/Skins/Styles.ascx" %>
<%@ Register TagPrefix="dnn" TagName="CURRENTDATE" Src="~/Admin/Skins/CurrentDate.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LANGUAGE" Src="~/Admin/Skins/Language.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGO" Src="~/Admin/Skins/Logo.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SEARCH" Src="~/Admin/Skins/Search.ascx" %>
<%@ Register TagPrefix="dnn" TagName="USER" Src="~/Admin/Skins/User.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>
<%@ Register TagPrefix="dnn" TagName="PRIVACY" Src="~/Admin/Skins/Privacy.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TERMS" Src="~/Admin/Skins/Terms.ascx" %>
<%@ Register TagPrefix="dnn" TagName="BREADCRUMB" Src="~/Admin/Skins/BreadCrumb.ascx" %>
<%@ Register TagPrefix="dnn" TagName="COPYRIGHT" Src="~/Admin/Skins/Copyright.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LINKTOMOBILE" Src="~/Admin/Skins/LinkToMobileSite.ascx" %>
<%@ Register TagPrefix="dnn" TagName="META" Src="~/Admin/Skins/Meta.ascx" %>
<%@ Register TagPrefix="dnn" TagName="MENU" Src="~/DesktopModules/DDRMenu/Menu.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="jQuery" Src="~/Admin/Skins/jQuery.ascx" %>


<dnn:META ID="META1" runat="server" Name="viewport" Content="width=device-width,initial-scale=1" />

<!--[if lt IE 9]>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html5shiv/3.7.2/html5shiv.min.js"></script>
<![endif]-->
 
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/bootstrap.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/font-awesome.min.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/aria-tooltip.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/style.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/responsive.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/demo.css">
<%--<script src="<%=SkinPath%>js/bootstrap.min.js"></script>--%>
<div class="container-fluid">

    <!--BEGIN NEW CHRIS HEADER -->
    <div class="row">
        <header>
            <div class="header_lt col-sm-2 nopadding">
                <div class="exchange_lt">
                    <dnn:LOGO runat="server" ID="dnnLOGO" />
                </div>
            </div>
            <div class="menu-toggle col-sm-10 nopadding">
                <div class="header_rt col-sm-4 nopadding">
                    <ul>
                        <li><a href='<%=DotNetNuke.Common.Globals.NavigateURL(PortalSettings.UserTabId) %>'><span>Profile</span>
                            <figure>
                                <img src='<%=SkinPath %>images/icon-profile-photo.png' width="23" height="23" alt="icon-profile" class="img-responsive">
                            </figure>
                        </a></li>
                        <li class="demo"><a href='<%=DotNetNuke.Common.Globals.NavigateURL(PortalSettings.UserTabId) %>?show=message'><span>Alerts</span>
                            <figure>
                                <img src='<%=SkinPath %>images/icon-alerts.png' width="20" height="21" alt="icon-alerts" class="img-responsive">
                            </figure>
                            <div class="pos_1">
                                <div class="tbl">
                                    <div class="tbl_cell"><small>88</small> </div>
                                </div>
                            </div>
                        </a></li>
                        <li class="demo"><a href="#"><span>Contact</span>
                            <figure>
                                <img src='<%=SkinPath %>images/icon-contact.png' width="36" height="21" alt="icon-contact" class="img-responsive">
                            </figure>
                        </a></li>
                        <li class="demo">
                            <% if (Request.IsAuthenticated) %>
                            <%{ %>
                            <a href="/logout"><span>Log Out</span>
                                <figure>
                                    <img src='<%=SkinPath %>images/icon-logout.png' width="22" height="22" alt="icon-logout" class="img-responsive">
                                </figure>
                                <% } %>
                                <% else
                                    { %>
                                <a href='<%=DotNetNuke.Common.Globals.NavigateURL(PortalSettings.LoginTabId) %>'><span>Log In</span>
                                    <figure>
                                        <img src='<%=SkinPath %>images/icon-logout.png' width="22" height="22" alt="icon-logout" class="img-responsive">
                                    </figure>
                                    <%} %>
                                </a></li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
                <div class="header_cnt col-sm-8 nopadding">
                    <div class="exchange_rt">
                        <h1>Profile Setup</h1>
                        <div class="clearfix"></div>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="clearfix"></div>
        </header>
    </div>
    <!--END NEW CHRIS HEADER -->

    <div class="content_wrapper row" id="ContentPane" runat="server">
    </div>
    <!-- BEGIN CHRIS FOOTER -->
    <footer class="row">
        <div class="footer_dtls">
            <span>A Service of</span> <a href="#">
                <figure>
                    <img src="<%=SkinPath%>images/footer_logo.png" width="129" height="26" alt="footer_logo" class="img-responsive">
                </figure>
            </a>
            <div class="clearfix"></div>
        </div>
        <div class="clearfix"></div>
    </footer>
    <!-- END CHRIS FOOTER -->
</div>
