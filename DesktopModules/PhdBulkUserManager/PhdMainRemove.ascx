<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdMainRemove" Codebehind="PhdMainRemove.ascx.vb" %>
<%@ Register Src="PhdTimer.ascx" TagName="PhdTimer" TagPrefix="Timer" %>
<%@ Register Src="PhdTitle.ascx" TagName="PhdTitle" TagPrefix="Title" %>
<%@ Register Src="PhdSelectDeletedUsers.ascx" TagName="PhdSelectUsers" TagPrefix="SelectUsers" %>
<%@ Register Src="PhdShowUsers.ascx" TagName="PhdShowUsers" TagPrefix="ShowUsers" %>
<%@ Register Src="PhdShowProgress.ascx" TagName="PhdShowProgress" TagPrefix="ShowProgress" %>
<%@ Register Src="PhdNavigation.ascx" TagName="PhdNavigation" TagPrefix="Navigation" %>
<%@ Register Src="PhdFooter.ascx" TagName="PhdFooter" TagPrefix="phdftr" %>
<!-- Mainremove start -->
<Title:PhdTitle ID="PhdTitle" runat="server" Title="Export users" />
<div style="margin: 5px 30px 0 30px;">
    <SelectUsers:PhdSelectUsers ID="SelectUsersControl" runat="server" visible="true" />
    <ShowUsers:PhdShowUsers ID="ShowUsersControl" runat="server" visible="false" />
    <ShowProgress:PhdShowProgress ID="ShowProgressControl" runat="server" Visible="false" />
    <Navigation:PhdNavigation ID="NavigationControl" runat="server" />
    <Timer:PhdTimer ID="TimerControl" runat="server" />
</div>
<!-- Mainremove end -->



