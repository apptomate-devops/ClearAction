<%@ control language="VB" autoeventwireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdUpdateUserOptions" Codebehind="PhdUpdateUserOptions.ascx.vb" %>
<%@ Register Src="phdroleSelection.ascx" TagName="PhdRoleSelection" TagPrefix="RoleSelection" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<!-- UpdateUserOptions Start -->
<div class="phdform">
    <dnn:Label ID="lblPasswordReset" runat="server" CssClass="Normal" Text="" ControlName="lblPWReset" />
    <asp:Label ID="lblPWReset" runat="server" CssClass="Normal">Password reset</asp:Label>
</div>
    <asp:Label ID="lblPWmode" runat="server" Visible="false"></asp:Label>
<asp:Panel ID="panPWmsg" runat="server"  BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" GroupingText="" CssClass="SubHead" style="margin-bottom:15px;padding:10px;">
    <asp:Label ID="lblPWmsg" runat="server" CssClass="Normal"></asp:Label>
</asp:Panel>
<asp:Panel ID="panPassword" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" GroupingText="" CssClass="SubHead" style="margin-bottom:15px;">
    <div style="margin-left:30px;margin-top:5px;margin-bottom:0px;" class="normal">
        <table style="background: inherit;">
            <tr class="SubHead">
                <td><dnn:LABEL id="lblResetPassword" runat="server" text="Reset user password" controlname="cbResetPassword" CssClass="SubHead"></dnn:LABEL></td>
                <td style="vertical-align:top;"><asp:CheckBox ID="cbResetPassword" runat="server"  CssClass="normalCheckBox" /></td>
            </tr>
            <tr  >
                <td><dnn:LABEL id="lblPasswordNotify" runat="server" text="Notify user of password change" controlname="cbPasswordNotify" CssClass="SubHead"></dnn:LABEL></td>
                <td style="vertical-align:top;"><asp:CheckBox ID="cbPasswordNotify" runat="server"  CssClass="normalCheckBox" /></td>
            </tr>
            <tr id="trCustomPW" runat="server">
                <td><dnn:LABEL id="lblCustomPassword" runat="server" text="Custom password generation" controlname="cbCustomPassword" CssClass="SubHead"></dnn:LABEL></td>
                <td style="vertical-align:top;"><asp:CheckBox ID="cbCustomPassword" runat="server"  CssClass="normalCheckBox" /></td>
            </tr>
        </table>
    </div>
    <div style="margin-left:90px;margin-top:0px;margin-bottom:5px;" class="normal" id="divCustomPW" runat="server">
        <table style="background: inherit;">
            <tr>
                <td><dnn:LABEL id="lblMinPWLength" runat="server" text="Minimum password length" controlname="ddlMinPWLength" CssClass="SubHead"></dnn:LABEL></td>
                <td style="vertical-align:top;"><asp:DropDownList ID="ddlMinPWLength" runat="server" CssClass="Normal phdnobottommargin" /></td>
            </tr>
            <tr>
                <td><dnn:LABEL id="lblMaxPWLength" runat="server" text="Maximum password length" controlname="ddlMaxPWLength" CssClass="SubHead"></dnn:LABEL></td>
                <td style="vertical-align:top;"><asp:DropDownList ID="ddlMaxPWLength" runat="server" CssClass="Normal phdnobottommargin" /></td>
            </tr>
            <tr>
                <td><dnn:LABEL id="lblLowerCaseOnly" runat="server" text="Use lower-case only" controlname="cbLowerOnly" CssClass="SubHead"></dnn:LABEL></td>
                <td style="vertical-align:top;"><asp:CheckBox ID="cbLowerOnly" runat="server" CssClass="normalCheckBox" /></td>
            </tr>
            <tr>
                <td><dnn:LABEL id="lblSpecialChars" runat="server" text="Use special characters" controlname="cbSpecialChars" CssClass="SubHead"></dnn:LABEL></td>
                <td style="vertical-align:top;"><asp:CheckBox ID="cbSpecialChars" runat="server" CssClass="normalCheckBox" /></td>
            </tr>
            <tr>
                <td><dnn:LABEL id="lblDigits" runat="server" text="Use digits" controlname="cbDigits" CssClass="SubHead"></dnn:LABEL></td>
                <td style="vertical-align:top;"><asp:CheckBox ID="cbDigits" runat="server" CssClass="normalCheckBox" /></td>
            </tr>
        </table>
    </div>
</asp:Panel>

<div class="phdform">
    <dnn:Label ID="lblAccountSettings" runat="server" CssClass="Normal" Text="" ControlName="lblAccSettings" />
    <asp:Label ID="lblAccSettings" runat="server" CssClass="Normal">Account settings</asp:Label>
</div>
<asp:Panel ID="panAccount" runat="server" BorderColor="Silver" BorderStyle="solid" BorderWidth="1px" GroupingText="" CssClass="SubHead" style="margin-bottom:15px;">
    <div style="margin-left: 20px; margin-top:5px; margin-bottom:5px; text-align: left;" class="normal">
        <table style="background: inherit;">
            <tr>
                <td><asp:CheckBox ID="cbAuthorize" runat="server" CssClass="normalCheckBox" /></td>
                <td><dnn:LABEL id="lblAuthorizeUsers" runat="server" text="Authorize user account:" controlname="cbAuthorize" CssClass="SubHead"></dnn:LABEL></td>
                <td>
                    <asp:DropDownList ID="ddlAuthorize" runat="server" Style="margin-left: 5px;" CssClass="Normal phdnobottommargin">
                        <asp:ListItem Selected="True" Value="TRUE">Authorize users</asp:ListItem>
                        <asp:ListItem Value="FALSE">Un-Authorize users</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                </td>
            </tr>
            <tr>
                <td><asp:CheckBox ID="cbForcePW" runat="server" CssClass="normalCheckBox" /></td>
                <td><dnn:LABEL id="lblForcePasswordChange" runat="server" text="Force Password Change:" controlname="cbForcePW" CssClass="SubHead"></dnn:LABEL></td>
                <td><asp:DropDownList ID="ddlForcePW" runat="server" Style="margin-left: 5px" CssClass="Normal phdnobottommargin">
                        <asp:ListItem Selected="True" Value="TRUE">Force users to change password</asp:ListItem>
                        <asp:ListItem Value="FALSE">Remove force password change</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
        </table>

    </div>
</asp:Panel>

<div class="phdform">
    <dnn:Label ID="lblRegionalSettings" runat="server" CssClass="Normal" Text="" ControlName="lblRegSettings" />
    <asp:Label ID="lblRegSettings" runat="server" CssClass="Normal">Regional settings</asp:Label>
</div>
<asp:Panel ID="panRegion" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" GroupingText="" CssClass="SubHead" style="margin-bottom:15px;">
    <div style="margin-left: 20px; margin-top:5px;margin-bottom:5px; text-align: left;" class="normal">
        <table style="background: inherit;">
            <tr>
                <td><asp:CheckBox ID="cbLanguage" runat="server" CssClass="normalCheckBox" /></td>
                <td><dnn:LABEL id="lblPreferredLanguage" runat="server" text="Preferred Language:" controlname="cbLanguage" CssClass="SubHead"></dnn:LABEL></td>
                <td>
                    <asp:DropDownList ID="ddlLanguage" runat="server" Style="margin-left: 5px" CssClass="Normal phdnobottommargin"></asp:DropDownList>
                    <br />
                </td>
            </tr>
            <tr>
                <td><asp:CheckBox ID="cbTimeZone" runat="server" CssClass="normalCheckBox" /></td>
                <td><dnn:LABEL id="lblTimezone" runat="server" text="Time Zone:" controlname="cbTimeZone" CssClass="SubHead"></dnn:LABEL></td>
                <td><asp:DropDownList ID="ddlTimeZone" runat="server" Style="margin-left: 5px" CssClass="Normal phdnobottommargin"></asp:DropDownList></td>
            </tr>
        </table>
    </div>
</asp:Panel>

<div class="phdform">
    <dnn:Label ID="lblAddToRoles" runat="server" CssClass="Normal" Text="" ControlName="lblAddRoles" />
    <asp:Label ID="lblAddRoles" runat="server" CssClass="Normal">Add to roles</asp:Label>
</div>
<asp:Panel ID="panRoles" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" GroupingText="" CssClass="SubHead" Style="margin-bottom:15px;padding:10px;">
    <RoleSelection:PhdRoleSelection ID="PHDAddRoles" runat="server"  />
</asp:Panel>

<div class="phdform">
    <dnn:Label ID="lblRemoveFromRoles" runat="server" CssClass="Normal" Text="" ControlName="lblRemoveRoles" />
    <asp:Label ID="lblRemoveRoles" runat="server" CssClass="Normal">Remove from roles</asp:Label>
</div>
<asp:Panel ID="panRemoveRoles" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" GroupingText="" CssClass="SubHead" Style="margin-bottom:15px;padding: 10px;">
    <RoleSelection:PhdRoleSelection ID="PhdRemoveRoles" runat="server" IsRemove="true" />
</asp:Panel>

<asp:PlaceHolder ID="phJavaScript" runat="server" Visible="true"></asp:PlaceHolder>
<!-- UpdateUserOptions End -->
