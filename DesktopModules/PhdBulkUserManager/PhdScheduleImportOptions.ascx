<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PhdScheduleImportOptions.ascx.vb" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdScheduleImportOptions" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<asp:Panel ID="panImportOptions" runat="server" BorderColor="Silver" BorderStyle="None"
    BorderWidth="1px" GroupingText="" CssClass="SubHead" Style="margin-left: 30px;
    margin-right: 30px;" Visible="true">

	<tr style="vertical-align:top;" id="trActive" runat="server">
		<td class="dnnFormItem"  style="width:130px;padding:5px;"><dnn:Label id="PhdImportOptions_lblNotify" runat="server" controlname="cbNotify" suffix=":" text="Notify new users" ></dnn:Label></td>
		<td class="dnnFormItem verticalBottom" style="padding:5px;">
			<asp:CheckBox ID="cbNotify" runat="server"  CssClass="normalCheckBox paddingTop10"  />
		</td>
	</tr>
	<tr style="vertical-align:top;" id="tr1" runat="server">
		<td class="dnnFormItem"  style="width:130px;padding:5px;"><dnn:Label id="PhdImportOptions_lblRemoveImportFile" runat="server" controlname="cbRemoveImportFile" suffix=":" ></dnn:Label></td>
		<td class="dnnFormItem verticalBottom" style="padding:5px;">
			<asp:CheckBox ID="cbRemoveImportFile" runat="server"  CssClass="normalCheckBox paddingTop10"  />
		</td>
	</tr>
	<tr style="vertical-align:top;" id="tr4" runat="server">
		<td class="dnnFormItem"  style="width:130px;padding:5px;"><dnn:Label id="PhdImportOptions_lblEmailResultsTo" runat="server" controlname="txtEmailResultsTo" suffix=":" ></dnn:Label></td>
		<td class="dnnFormItem" style="padding:5px;">
			<asp:TextBox id="txtEmailResultsTo" runat="server" MaxLength="300" resourcekey="txtEmailResultsTo" />
		</td>
	</tr>
	<tr style="vertical-align:top;" id="tr2" runat="server">
		<td class="dnnFormItem"  style="padding:5px;padding-bottom:0;" colspan="2">
            <dnn:Label id="PhdImportOptions_ImportAction" runat="server" controlname="tbAction" suffix=":" ></dnn:Label>
 		</td>
	</tr>
 	<tr style="vertical-align:top;" id="tr5" runat="server">
		<td class=""  style="padding:5px;" colspan="2">
          <asp:RadioButtonList ID="rbAction" runat="server" CssClass="Normal" style="margin-left: 60px;">
                <asp:ListItem Value="Add" Selected="True">Add new users only</asp:ListItem>
                <asp:ListItem Value="Update">Update existing users only</asp:ListItem>
                <asp:ListItem Value="AddUpdate">Add new users and update existing users</asp:ListItem>
                <asp:ListItem Value="RecoverAdd">Add new users and recover deleted users</asp:ListItem>
                <asp:ListItem Value="RecoverAddUpdate">Add new users, recover deleted users and update users</asp:ListItem>
                <asp:ListItem Value="Recover">Recover deleted users</asp:ListItem>
                <asp:ListItem Value="Remove">Permanently remove deleted users</asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfvAction" runat="server" ControlToValidate="rbAction"
                Display="Dynamic" ErrorMessage="Please select an action" CssClass="Normal"></asp:RequiredFieldValidator>
            <asp:Label ID="lblFileName" runat="server" Visible="False"></asp:Label><br />
		</td>
	</tr>

	<tr style="vertical-align:top;" id="tr3" runat="server">
		<td class="dnnFormItem"  style="padding:5px; padding-bottom:0;" colspan="2">
            <dnn:Label id="PhdImportOptions_PasswordIssues" runat="server" controlname="rbPasswordFix" suffix=":" ></dnn:Label>
  		</td>
	</tr>
 	<tr style="vertical-align:top;" id="tr6" runat="server">
		<td class=""  style="padding:5px;" colspan="2">
            <asp:RadioButtonList ID="rbPasswordFix"  runat="server"  CssClass="Normal"  style="margin-left: 60px;" >
                <asp:ListItem Selected="True" Value="Random">Assign a random password to these accounts</asp:ListItem>
                <asp:ListItem Value="Remove">Remove these accounts from the import</asp:ListItem>
            </asp:RadioButtonList>
            <br />
		</td>
	</tr>

</asp:Panel>
<asp:PlaceHolder ID="phJavaScript" runat="server"></asp:PlaceHolder>
