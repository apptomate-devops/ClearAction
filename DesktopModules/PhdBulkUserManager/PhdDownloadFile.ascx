<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdDownloadFile" Codebehind="PhdDownloadFile.ascx.vb" %>
<%@ Register Src="PhdSelectFields.ascx" TagName="PhdSelectFields" TagPrefix="Fields" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<div style="margin-bottom:15px;">
<fields:PhdSelectFields ID="PhdSelectFields" runat="server" />
    <div class="phdform">
        <dnn:Label ID="lblFileFormatInfo" runat="server" CssClass="Normal" Text="" ControlName="lblFileFormat" />
        <asp:Label ID="lblFileFormat" runat="server" CssClass="Normal">File format</asp:Label>
    </div>    
    <asp:Panel ID="panFileType" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" GroupingText="" CssClass="SubHead">
        <div style="margin-top:10px;margin-left: 60px; text-align: left;" class="normal">
           <asp:RadioButtonList ID="RBExportType" runat="server" CssClass="Normal" Visible="True">
                <asp:ListItem Value=".xml" Selected="True">XML file (.xml)</asp:ListItem>
                <asp:ListItem Value=".csv">Comma delimited file (.csv)</asp:ListItem>
                <asp:ListItem Value=".txt">Tab delimited File (.txt)</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <asp:Label ID="lblDateFormat" runat="server" CssClass="Normal">Date format</asp:Label>
            <asp:DropDownList ID="ddlDateFormat" runat="server" CssClass="Normal" Enabled="true">
            </asp:DropDownList>
        </div>
        <div style="text-align: center;margin-bottom:10px;"><asp:Button ID="ButDownLoadFile" runat="server" CssClass="CommandButton dnnPrimaryAction" Text="  Download File  "></asp:Button></div>
    </asp:Panel>

    <asp:HiddenField ID="hfJobGUID" runat="server" Visible="False" />
    <asp:HiddenField ID="hfPortalID" runat="server" Value="0" Visible="False" />
  
</div>
