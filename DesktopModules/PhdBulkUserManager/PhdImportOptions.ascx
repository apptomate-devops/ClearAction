<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdImportOptions" Codebehind="PhdImportOptions.ascx.vb" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>


<div class="SubHead">Select the update action required:</div>
<asp:Panel ID="panActions" runat="server" BorderColor="DarkGray" BorderWidth="1px" Enabled="True"  BorderStyle="Solid">
    <div style="margin-left: 30px; margin-top: 5px; margin-bottom: 5px;">
        <asp:RadioButtonList ID="rbAction" runat="server" CssClass="Normal">
            <asp:ListItem Value="Add" Selected="True">Add new users only</asp:ListItem>
            <asp:ListItem Value="Update">Update existing users only</asp:ListItem>
            <asp:ListItem Value="AddUpdate">Add new users and update existing users</asp:ListItem>
            <asp:ListItem Value="RecoverAdd">Add new users and recover deleted users</asp:ListItem>
            <asp:ListItem Value="RecoverAddUpdate">Add new users, recover deleted users and update users</asp:ListItem>
        </asp:RadioButtonList>
        <asp:RequiredFieldValidator ID="rfvAction" runat="server" ControlToValidate="rbAction"
            Display="Dynamic" ErrorMessage="Please select an action" CssClass="Normal"></asp:RequiredFieldValidator>
        <asp:Label ID="lblFileName" runat="server" Visible="False"></asp:Label>
        <br />
        <div class="phdform">
            <dnn:Label ID="PhdImportOptions_lblNotify" runat="server" CssClass="Normal" Text="" ControlName="cbNotify"> </dnn:Label>
            <asp:CheckBox ID="cbNotify" runat="server" CssClass="normalCheckBox" Text="Notify new users via email" />
        </div>
    </div>
</asp:Panel>
<asp:HiddenField ID="hdEmailAddress" runat="server"></asp:HiddenField>

<br style="clear: both;" />
<asp:PlaceHolder ID="phJavaScript" runat="server"></asp:PlaceHolder>
    <asp:Label ID="GUIDtext" runat="server" Visible="false"></asp:Label>
