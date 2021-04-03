<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdMainUpdate" Codebehind="PhdMainUpdate.ascx.vb" %>
<%@ Register Src="PhdTimer.ascx" TagName="PhdTimer" TagPrefix="Timer" %>
<%@ Register Src="PhdTitle.ascx" TagName="PhdTitle" TagPrefix="Title" %>
<%@ Register Src="PhdSelectUsers.ascx" TagName="PhdSelectUsers" TagPrefix="SelectUsers" %>
<%@ Register Src="PhdShowUsers.ascx" TagName="PhdShowUsers" TagPrefix="ShowUsers" %>
<%@ Register Src="PhdNavigation.ascx" TagName="PhdNavigation" TagPrefix="Navigation" %>
<%@ Register Src="PhdUpdateUserOptions.ascx" TagName="PhdUpdateUserOptions" TagPrefix="UpdateUser" %>
<%@ Register Src="PhdShowProgress.ascx" TagName="PhdShowProgress" TagPrefix="ShowProgress" %>
<%@ Register Src="PhdFooter.ascx" TagName="PhdFooter" TagPrefix="Footer" %>
<%@ Register src="PhdTitle.ascx" tagname="PhdTitle" tagprefix="phdtle" %>
<!-- Mainupdate start -->
<Title:PhdTitle ID="PhdTitleControl" runat="server" Title="Update users" />
<div style="margin: 5px 30px 0 30px;">
    <SelectUsers:PhdSelectUsers ID="SelectUsersControl" runat="server" visible="true" />
    <ShowUsers:PhdShowUsers ID="ShowUsersControl" runat="server" visible="false" />
    <UpdateUser:PhdUpdateUserOptions ID="UpdateUserOptionsControl" runat="server" visible="false" />
    <Footer:PhdFooter ID="PhdResultsFooter" runat="server" Footer="The user accounts selected will now be updated." />
    <ShowProgress:PhdShowProgress ID="ShowProgressControl" runat="server" />
    <Navigation:PhdNavigation ID="NavigationControl" runat="server" />
    <Timer:PhdTimer ID="TimerControl" runat="server" />
</div>
<!-- Mainupdate end -->

 