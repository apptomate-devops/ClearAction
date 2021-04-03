<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdScheduleEdit" Codebehind="PhdScheduleEdit.ascx.vb" %>
<%@ Register Src="PhdScheduleExportOptions.ascx" TagName="PhdScheduleExportOptions" TagPrefix="phdeo" %>
<%@ Register Src="PhdScheduleImportOptions.ascx" TagName="PhdScheduleImportOptions" TagPrefix="phdso" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<br />
<table summary="Edit Table" style="margin-left: 20px; width:100%; border:none; ">
	<tr style="vertical-align:top;"  id="trJobTitle" runat="server">
		<td class="dnnFormItem" style="width:130px;padding:5px;"><dnn:Label id="lblJobTitle" runat="server" controlname="txtJobTitle" suffix=":"></dnn:Label></td>
		<td class="dnnFormItem" style="padding:5px;">
			<asp:TextBox id="txtJobTitle" runat="server" MaxLength="50" style="margin-bottom:0;"/>
			<asp:RequiredFieldValidator ID="valJobTitle" resourcekey="valJobTitle.ErrorMessage" ControlToValidate="txtJobTitle"
				CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br>Job Name is required" Runat="server" />
		</td>
	</tr>
	<tr id="trJobFile" runat="server" style="vertical-align:top;">
		<td class="dnnFormItem"  style="width:130px;padding:5px;"><dnn:Label id="lblJobFileName" runat="server" controlname="txtJobFileName" suffix=":" ></dnn:Label><dnn:Label id="lblJobFolderName" runat="server" controlname="txtJobFileName" suffix=":" ></dnn:Label></td>
		<td class="dnnFormItem" style="padding:5px;">
			<asp:TextBox id="txtJobFileName" runat="server" MaxLength="300" resourcekey="txtJobFileName" style="margin-bottom:0;"/>
			<asp:RequiredFieldValidator ID="valJobFileName" resourcekey="valJobFileName.ErrorMessage" ControlToValidate="txtJobFileName"
				CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br>File name is required" Runat="server" />
		</td>
	</tr>
	<tr style="vertical-align:top;" id="trSchedule" runat="server">
		<td class="dnnFormItem"  style="width:130px;padding:5px;"><dnn:Label id="lblSchedule" runat="server" controlname="ddlScheduleSpan" suffix=":"  ></dnn:Label></td>
		<td class="dnnFormItem" style="padding:5px;">
            <asp:DropDownList ID="ddlScheduleSpan" runat="server" style="margin-bottom:0;width:80px;"></asp:DropDownList>
            <asp:DropDownList ID="ddlScheduleType" runat="server" style="margin-bottom:0;width:120px;"></asp:DropDownList>
		</td>
	</tr>
	<tr style="vertical-align:top;" id="trStartingAt" runat="server">
		<td class="dnnFormItem"  style="width:130px;padding:5px;"><dnn:Label id="lblStartingAt" runat="server" controlname="ddlStartingDate" suffix=":"  ></dnn:Label></td>
		<td class="dnnFormItem" style="padding:5px;">
            <asp:DropDownList ID="ddlHour" runat="server" style="margin-bottom:0;width:80px;"></asp:DropDownList>
            <asp:DropDownList ID="ddlMinute" runat="server" style="margin-bottom:0;width:80px;"></asp:DropDownList>
		</td>
	</tr>
	<tr style="vertical-align:top;" id="trActive" runat="server">
		<td class="dnnFormItem"  style="width:130px;padding:5px;"><dnn:Label id="lblActive" runat="server" controlname="cbActive" suffix=":" ></dnn:Label></td>
		<td class="dnnFormItem verticalBottom" style="padding:5px;">
			<asp:CheckBox id="cbActive" runat="server"  style="margin-bottom:0;" CssClass="normalCheckBox" />
		</td>
	</tr>
    <phdso:PhdScheduleImportOptions ID="ImportOptionsControl" runat="server" visible="false" />
    <phdeo:PhdScheduleExportOptions ID="ExportOptionsControl" runat="server" Visible="false" />
	<tr style="vertical-align:top;" id="trCreatedBy" runat="server">
		<td class="dnnFormItem"  style="width:130px;padding:5px;"><dnn:Label id="lblCreatedBy" runat="server" controlname="txtCreatedBy" suffix=":" ></dnn:Label></td>
		<td class="dnnFormItem verticalBottom" style="padding:5px;">
			<asp:label ID="lblCreatedByText" ReadOnly="true" runat="server"  />
		</td>
	</tr>
	<tr style="vertical-align:top;" id="trCreated" runat="server">
		<td class="dnnFormItem" style="width:130px;padding:5px;"><dnn:Label id="lblCreated" runat="server" controlname="txtCreated" suffix=":" ></dnn:Label></td>
		<td class="dnnFormItem verticalBottom" style="padding:5px;">
			<asp:label id="lblCreatedText" ReadOnly="true" runat="server"  />
		</td>
	</tr>
</table>
<asp:HiddenField ID="hidPortalID" runat="server" />
<asp:HiddenField ID="hidJobID" runat="server" />
<asp:HiddenField ID="hidUserID" runat="server" />
<asp:HiddenField ID="hidJobType" runat="server" />
<br />
<br />
<p style="text-align:center;">
    <asp:linkbutton cssclass="dnnPrimaryAction CommandButton" id="cmdUpdate" resourcekey="cmdUpdate" runat="server" borderstyle="none" text="Update" OnClick="cmdUpdate_Click"   ></asp:linkbutton>&nbsp;
    <asp:linkbutton cssclass="dnnSecondaryAction CommandButton" id="cmdCancel" resourcekey="cmdCancel" runat="server" borderstyle="none" text="Cancel" causesvalidation="False" OnClick="cmdCancel_Click"  ></asp:linkbutton>&nbsp;
    <asp:linkbutton cssclass="dnnSecondaryAction CommandButton" id="cmdDelete" resourcekey="cmdDelete" runat="server" borderstyle="none" text="Delete" causesvalidation="False" OnClick="cmdDelete_Click" OnClientClick="javascript:return confirm('Are You Sure You Wish To Delete This Item?');"  ></asp:linkbutton>&nbsp;

</p>


