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


<dnn:META ID="mobileScale" runat="server" Name="viewport" Content="width=device-width,initial-scale=1" />



<link href="https://fonts.googleapis.com/css?family=Montserrat:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">

<!-- Bootstrap -->
<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous">

<!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->


<!-- Latest compiled and minified JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
<script src="<%=SkinPath%>js/jquery.lightbox_me.js"></script>
<script src="<%=SkinPath%>js/jquery.matchHeight.js"></script>
<script src="<%=SkinPath%>js/isotope.pkgd.min.js"></script>
<script type="text/javascript" src="<%=SkinPath%>js/main.js"></script>
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/font-awesome.min.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/style.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/responsive.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/ProfileStyle.css">

<!--DEMO CSS FILE TO ALTER FUNCTIONALITY FOR DEMO
REMOV WHEN NECESSARY -->
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>css/demo.css">
<link rel="stylesheet" type="text/css" href="<%=SkinPath%>Skin.css">


<!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->



<div class="container-fluid"> 
  
  <!--BEGIN NEW CHRIS HEADER -->
  <div class="row">
    <header>
      <div class="header_lt col-sm-2 nopadding">
        <div class="exchange_lt">  
        <dnn:LOGO runat="server" ID="dnnLOGO"  alt="ClearAction" />
        </div>
      </div>
      <div class="menu-toggle col-sm-10 nopadding">
        <div class="header_rt col-sm-4 nopadding">
          <ul>
            <li> <a href="/Profile"> <span>Profile</span>
              <figure> <img src="<%=SkinPath%>images/icon-profile-photo.png" width="23" height="23" alt="icon-profile" class="img-responsive"> </figure>
              </a> </li>
            <li class="demo"> <a href="#"> <span>Alerts</span>
              <figure> <img src="<%=SkinPath%>images/icon-alerts.png" width="20" height="21" alt="icon-alerts" class="img-responsive"> </figure>
              <div class="pos_1">
                <div class="tbl">
                  <div class="tbl_cell"> <small>88</small> </div>
                </div>
              </div>
              </a> </li>
            <li class="demo"> <a href="#"> <span>Contact</span>
              <figure> <img src="<%=SkinPath%>images/icon-contact.png" width="36" height="21" alt="icon-contact" class="img-responsive"> </figure>
              </a> </li>
            <li class="demo"> <a href="#"> <span>Log Out</span>
              <figure> <img src="<%=SkinPath%>images/icon-logout.png" width="22" height="22" alt="icon-logout" class="img-responsive"> </figure>
              </a> </li>
          </ul>
          <div class="clearfix"></div>
        </div>
        <div class="header_cnt col-sm-8 nopadding">
          <div class="exchange_rt">
            <div id="PageTitle_Pane" runat="server"></div>
            <div class="clearfix"></div>
          </div>
          <div class="clearfix"></div>
        </div>
      </div>
      <div class="clearfix"></div>
    </header>
  </div>
  <!--END NEW CHRIS HEADER -->
  
  <div class="content_wrapper row">
    <div class="row">
    <div id="ContentPane" runat="server" class="SkinFullPane clearfix"></div>
    <div id="FullPaneA" runat="server" class="SkinFullPane">
      
        </div>
    </div>
    <div class="row" id="FullPaneB" runat="server">
      
    </div>
    <div class="row" id="FullPaneC" runat="server">
      
    </div>
  </div>
  <!-- BEGIN CHRIS FOOTER -->
  <footer class="row">
  <div class="SkinFullPane" id="Footer_Pane" runat="server">
    
    </div>
    <div class="clearfix"></div>
  </footer>
  <!-- END CHRIS FOOTER --> 
</div>
<dnn:LOGIN ID="dnnlogin" runat="server" />