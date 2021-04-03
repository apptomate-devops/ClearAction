<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PhdScheduleExportOptions.ascx.vb" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdScheduleExportOptions" %>
<%@ Register Src="PhdSelectUsers.ascx" TagName="PhdSelectUsers" TagPrefix="phdsu" %>
<%@ Register Src="PhdSelectFields.ascx" TagName="PhdSelectFields" TagPrefix="phdsf" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<div class="phdform">
	<tr style="vertical-align:top;" id="tr4" runat="server">
		<td class="dnnFormItem"  style="width:130px;padding:5px;"><dnn:Label id="PhdExportOptions_lblEmailResultsTo" runat="server" controlname="txtEmailResultsTo" suffix=":" ></dnn:Label></td>
		<td class="dnnFormItem" style="padding:5px;">
			<asp:TextBox id="txtEmailResultsTo" runat="server" MaxLength="300" resourcekey="txtEmailResultsTo" />
		</td>
	</tr>
</div>
    
	<tr style="vertical-align:top;" id="tr1" runat="server">
		<td class=""  style="padding:5px;" colspan="2">
            <div class="phdform">
                <dnn:Label ID="PhdExportOptions_lblFileFormat" runat="server" CssClass=" Normal" Text="" ControlName="PhdExportOptions_lblFileFormatTitle" suffix="" ></dnn:Label>
                <asp:Label ID="PhdExportOptions_lblFileFormatTitle" runat="server" CssClass="  Normal" Text="Export file format"  suffix="" ></asp:Label>
            </div>
       
    <asp:Panel ID="panFileType" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" GroupingText=""  CssClass="SubHead" Style="margin-bottom:15px;">
        <div style="margin-left: 30px;margin-top:5px;margin-bottom:5px;text-align:left;" class="normal">
            <asp:RadioButtonList ID="rbFileFormat" runat="server" CssClass="Normal" Visible="True" >
                <asp:ListItem Value=".xml" Selected="True">XML file (.xml)</asp:ListItem>
                <asp:ListItem Value=".csv">Comma delimited file (.csv)</asp:ListItem>
                <asp:ListItem Value=".txt">Tab delimited File (.txt)</asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </asp:Panel>
 <phdsf:PhdSelectFields ID="SelectFieldsControl" runat="server" Visible="true" />

<phdsu:PhdSelectUsers ID="SelectUsersControl" runat="server" Visible="true" />
   
   </td>
 </tr>

