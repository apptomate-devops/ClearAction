<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdMainRecover" Codebehind="PhdMainRecover.ascx.vb" %>
<%@ Register Src="PhdTimer.ascx" TagName="PhdTimer" TagPrefix="Timer" %>
<%@ Register Src="PhdTitle.ascx" TagName="PhdTitle" TagPrefix="Title" %>
<%@ Register Src="PhdSelectDeletedUsers.ascx" TagName="PhdSelectUsers" TagPrefix="SelectUsers" %>
<%@ Register Src="PhdShowUsers.ascx" TagName="PhdShowUsers" TagPrefix="ShowUsers" %>
<%@ Register Src="PhdShowProgress.ascx" TagName="PhdShowProgress" TagPrefix="ShowProgress" %>
<%@ Register Src="PhdNavigation.ascx" TagName="PhdNavigation" TagPrefix="Navigation" %>
<!-- Mainrecover start -->
<Title:PhdTitle ID="PhdTitleControl" runat="server" Title="Update users" />
<div style="margin: 5px 30px 0 30px;">
    <SelectUsers:PhdSelectUsers ID="SelectUsersControl" runat="server" visible="true" />
    <ShowUsers:PhdShowUsers ID="ShowUsersControl" runat="server" visible="false" />
    <ShowProgress:PhdShowProgress ID="ShowProgressControl" runat="server" Visible="false" />
    <Navigation:PhdNavigation ID="NavigationControl" runat="server" visible="true" />
    <Timer:PhdTimer ID="TimerControl" runat="server" />
</div>
<!-- Mainrecover end -->





