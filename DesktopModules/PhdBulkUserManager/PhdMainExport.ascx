<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdMainExport" Codebehind="PhdMainExport.ascx.vb" %>
<%@ Register Src="PhdTimer.ascx" TagName="PhdTimer" TagPrefix="Timer" %>
<%@ Register Src="PhdTitle.ascx" TagName="PhdTitle" TagPrefix="Title" %>
<%@ Register Src="PhdSelectUsers.ascx" TagName="PhdSelectUsers" TagPrefix="Select" %>
<%@ Register Src="PhdDownloadFile.ascx" TagName="PhdDownloadFile" TagPrefix="Download" %>
<%@ Register Src="PhdShowUsers.ascx" TagName="PhdShowUsers" TagPrefix="ShowUsers" %>
<%@ Register Src="PhdShowProgress.ascx" TagName="PhdShowProgress" TagPrefix="ShowProgress" %>
<%@ Register Src="PhdNavigation.ascx" TagName="PhdNavigation" TagPrefix="Nav" %>
<%@ Register src="PhdCheckConfiguration.ascx" tagname="PhdCheckConfiguration" tagprefix="CheckConfig" %>
<!-- Mainexport start -->
<Title:PhdTitle ID="PhdTitle" runat="server" Title="Select users to export" />
<div style="margin: 5px 30px 0 30px;">
    <Select:PhdSelectUsers ID="SelectUsersControl" runat="server" />
    <Download:PhdDownloadFile ID="DownloadFileControl" runat="server" />
    <ShowUsers:PhdShowUsers ID="ShowUsersControl" runat="server" />
    <CheckConfig:PhdCheckConfiguration ID="CheckConfigurationControl" runat="server" />
    <ShowProgress:PhdShowProgress ID="ShowProgressControl" runat="server" Visible="false" />
</div>
<Nav:PhdNavigation ID="NavigationControl" runat="server" />

<Timer:PhdTimer ID="TimerControl" runat="server" />

<!-- Mainexport end -->
