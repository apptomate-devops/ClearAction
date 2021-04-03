<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdMainDelete" Codebehind="PhdMainDelete.ascx.vb" %>
<%@ Register Src="PhdTimer.ascx" TagName="PhdTimer" TagPrefix="Timer" %>
<%@ Register Src="PhdTitle.ascx" TagName="PhdTitle" TagPrefix="Title" %>
<%@ Register Src="PhdSelectUsers.ascx" TagName="PhdSelectUsers" TagPrefix="SelectUsers" %>
<%@ Register Src="PhdShowUsers.ascx" TagName="PhdShowUsers" TagPrefix="ShowUsers" %>
<%@ Register Src="PhdImportFile.ascx" TagName="PhdImportFile" TagPrefix="ImportFile" %>
<%@ Register Src="PhdShowProgress.ascx" TagName="PhdShowProgress" TagPrefix="ShowProgress" %>
<%@ Register Src="PhdNavigation.ascx" TagName="PhdNavigation" TagPrefix="Navigation" %>
<%@ Register Src="PhdFooter.ascx" TagName="PhdFooter" TagPrefix="Footer" %>
<!-- Maindelete start -->
<Title:PhdTitle ID="PhdTitleControl" runat="server" Title="Delete users" />
<div style="margin: 5px 30px 0 30px;">
    <asp:Panel ID="panMain" runat="server" Style="width: 100%;">
        <div style="margin-left: 30px">
            <asp:RadioButtonList ID="rbMenu" runat="server" CssClass="Normal">
                <asp:ListItem Value="File" Selected="True">Upload a list of users to delete</asp:ListItem>
                <asp:ListItem Value="Search">Search for users to delete</asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfvMenu" runat="server" ControlToValidate="rbMenu" Display="Dynamic" ErrorMessage="Please select an option"></asp:RequiredFieldValidator>
        </div>
    </asp:Panel>
    <ImportFile:PhdImportFile ID="ImportFileControl" runat="server" Visible="true" />
    <SelectUsers:PhdSelectUsers ID="SelectUsersControl" runat="server" Visible="false" />
    <ShowUsers:PhdShowUsers ID="ShowUsersControl" runat="server" Visible="false" />
    <Footer:PhdFooter ID="ResultsFooterControl" runat="server" Visible="false" Footer="The user accounts displayed above will now be deleted." />
    <ShowProgress:PhdShowProgress ID="ShowProgressControl" runat="server" Visible="false" />
</div>
<Navigation:PhdNavigation ID="NavigationControl" runat="server" />
<Timer:PhdTimer ID="TimerControl" runat="server" />
<!-- Maindelete end -->


 