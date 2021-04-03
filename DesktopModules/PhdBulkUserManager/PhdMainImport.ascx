<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdMainImport" Codebehind="PhdMainImport.ascx.vb" %>
<%@ Register Src="PhdTitle.ascx" TagName="PhdTitle" TagPrefix="Title" %>
<%@ Register Src="PhdTimer.ascx" TagName="PhdTimer" TagPrefix="Timer" %>
<%@ Register Src="PhdImportFile.ascx" TagName="PhdImportFile" TagPrefix="ImportFile" %>
<%@ Register Src="PhdShowUsers.ascx" TagName="PhdShowUsers" TagPrefix="ShowUsers" %>
<%@ Register Src="PhdImportOptions.ascx" TagName="PhdImportOptions" TagPrefix="ImportOptions" %>
<%@ Register Src="PhdShowProgress.ascx" TagName="PhdShowProgress" TagPrefix="ShowProgress" %>
<%@ Register Src="PhdNavigation.ascx" TagName="PhdNavigation" TagPrefix="Navigation" %>
<%@ Register Src="PhdValidateFile.ascx" TagName="PhdValidateFile" TagPrefix="ValidateFile" %>
<!-- Mainimport start -->
<Title:PhdTitle ID="PhdTitle" runat="server" Title="Export users" />
<div style="margin: 5px 30px 0 30px;">
    <asp:Panel ID="panPWmsg" runat="server"  BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" GroupingText="" CssClass="SubHead" style="margin-bottom:15px;padding:10px;" Visible="false">
        <asp:Label ID="lblPWmsg" runat="server" CssClass="Normal"></asp:Label>
    </asp:Panel>
    <ImportFile:PhdImportFile ID="ImportFileControl" runat="server" Visible="true" />
    <ValidateFile:PhdValidateFile ID="ValidateFileControl" runat="server" Visible="false" />
    <ImportOptions:PhdImportOptions ID="ImportOptionsControl" runat="server" Visible="false" />
    <ShowUsers:PhdShowUsers ID="ShowUsersControl" runat="server" Visible="false" />
    <ShowProgress:PhdShowProgress ID="ShowProgressControl" runat="server" Visible="false" />
    <Navigation:PhdNavigation ID="NavigationControl" runat="server" />
    <Timer:PhdTimer id="PhdTimerControl" runat="server" />
</div>
<!-- Mainimport stop -->

 