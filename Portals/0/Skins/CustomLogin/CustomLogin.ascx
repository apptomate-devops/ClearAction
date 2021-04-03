<%@ Control Language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%--<%@ Register TagPrefix="dnn" TagName="LANGUAGE" Src="~/Admin/Skins/Language.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGO" Src="~/Admin/Skins/Logo.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SEARCH" Src="~/Admin/Skins/Search.ascx" %>
<%@ Register TagPrefix="dnn" TagName="USER" Src="~/Admin/Skins/User.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>
<%@ Register TagPrefix="dnn" TagName="PRIVACY" Src="~/Admin/Skins/Privacy.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TERMS" Src="~/Admin/Skins/Terms.ascx" %>
<%@ Register TagPrefix="dnn" TagName="COPYRIGHT" Src="~/Admin/Skins/Copyright.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LINKS" Src="~/Admin/Skins/Links.ascx" %>

<%@ Register TagPrefix="dnn" TagName="MENU" Src="~/DesktopModules/DDRMenu/Menu.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>--%>
<!-- -->
<%@ Register TagPrefix="dnn" TagName="META" Src="~/Admin/Skins/Meta.ascx" %>
<dnn:META ID="mobileScale" runat="server" Name="viewport" Content="width=device-width,initial-scale=1" />


<!-- SET: STYLESHEET -->
<link href="https://fonts.googleapis.com/css?family=Montserrat:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">
<link href="https://fonts.googleapis.com/css?family=Zilla+Slab:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">




<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/bootstrap.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/bootstrap-theme.min.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/bootstrap-slider.min.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/font-awesome.min.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/aria-tooltip.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/customLoginStyle.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/responsive.css">



<!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<script type="text/javascript" src="js/IE9.js"></script>
<![endif]-->

<div class="login">
    <div class="container">
        <div class="row" style="margin-top: 3.5% !important;">
            <%--<div class="col-sm-6 left">
                <div id="leftPane" runat="server">
                </div>
            </div>--%>
            <div class="right">
                <div id="ContentPane" runat="server">
                </div>

                <div class="copyright">
                    <p class="text-center">&copy;Copyright 2017, ClearAction Continuum, Inc. All Rights Reserved. ClearAction Value Exchange is a trademark of ClearAction Continuum.</p>
                </div>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript" src="<%=SkinPath%>js/bootstrap.min.js"></script>
<%--<script src="<%=SkinPath%>js/jquery.lightbox_me.js"></script>--%>
<%--<script src="<%=SkinPath%>js/jquery.matchHeight.js"></script>--%>
<%--<script src="<%=SkinPath%>js/isotope.pkgd.min.js"></script>--%>
<%--<script type="text/javascript" src="<%=SkinPath%>js/main.js"></script>--%>
<script type="text/javascript" src="<%=SkinPath%>js/autosize.min.js"></script>
