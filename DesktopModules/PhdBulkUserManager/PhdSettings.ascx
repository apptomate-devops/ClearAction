<%@ Control Language="VB" AutoEventWireup="false" Inherits="DesktopModules_PhdBulkUserManager_PhdSettings" Codebehind="PhdSettings.ascx.vb" %>
 <%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register Src="PhdVersion.ascx" TagName="PhdVersion" TagPrefix="phdversion" %>


<h2 id="dnnPanel-ModuleGeneralDetails" class="dnnFormSectionHead">Diagnostics</h2>
    <table style="background: inherit;">
        <tr>
            <td>
                <dnn:label id="traceLabel" runat="server" controlname="traceLanguageLabel" />
            </td>
            <td>
                <asp:CheckBox ID="cbTrace" runat="server" CssClass="normalCheckBox" /></td>
        </tr>
    <tr id="SendMsg" runat="server">
        <td>
            <dnn:label id="sendmsgLabel" runat="server" controlname="sendmsgLanguageLabel" />
        </td>
        <td>
            <asp:CheckBox ID="cbSendMsg" runat="server" onclick="SetValidators(this.checked);" CssClass="normalCheckBox" /></td>
    </tr>
    <tr id="EmailTo" runat="server">
        <td>
            <dnn:label id="emailtoLabel" runat="server" controlname="emailtoLanguageLabel" />
        </td>
        <td>
            <asp:TextBox Width="80" ID="tbEmailTo" runat="server" Enabled="false" Style="width: 250px;" />
            <br />
            <asp:RequiredFieldValidator ID="rfvEmailTo" ControlToValidate="tbEmailTo" runat="server" ErrorMessage=" Email to must be entered" Enabled="false" Display="Dynamic" /></td>
    </tr>
    <tr id="EmailFrom" runat="server">
        <td>
            <dnn:label id="emailfromLabel" runat="server" controlname="emailfromLanguageLabel" />
        </td>
        <td>
            <asp:TextBox Width="80" ID="tbEmailFrom" runat="server" Enabled="false" Style="width: 250px;" />
            <br />
            <asp:RequiredFieldValidator ID="rfvEmailFrom" ControlToValidate="tbEmailFrom" runat="server" ErrorMessage=" Email from must be entered" Enabled="false" Display="Dynamic" /></td>
    </tr>
    <tr id="TraceMsg" runat="server">
        <td style="vertical-align:top;">
            <dnn:Label id="tracemsgLabel" runat="server" ControlName="tracemsgLanguageLabel" />
        </td>
        <td>
            <asp:TextBox TextMode="MultiLine" Width="50" Rows="10" ID="tbTraceMsg" runat="server" Enabled="false" Style="width: 250px;" />
            <br />
            <asp:RequiredFieldValidator ID="rfvMsg" ControlToValidate="tbTraceMsg" runat="server" ErrorMessage=" Description must be entered" Enabled="false" Display="Dynamic" /></td>
    </tr>
</table>
 
 <script  type="text/javascript">
     function SetValidators(state) {
         ValidatorEnable(document.getElementById('<%=rfvEmailTo.ClientID %>'), state);
         ValidatorEnable(document.getElementById('<%=rfvEmailFrom.ClientID %>'), state);
         ValidatorEnable(document.getElementById('<%=rfvMsg.ClientID %>'), state);
         document.getElementById('<%=tbEmailTo.ClientID %>').disabled = !state;
         document.getElementById('<%=tbEmailFrom.ClientID %>').disabled = !state;
         document.getElementById('<%=tbTraceMsg.ClientID %>').disabled = !state;
     }
     SetValidators(false);
 </script>

    <phdversion:PhdVersion ID="PhdVersion" runat="server" Visible="false" />
