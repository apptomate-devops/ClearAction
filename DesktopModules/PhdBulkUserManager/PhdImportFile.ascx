<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdImportFile" Codebehind="PhdImportFile.ascx.vb" %>
<div style="margin-bottom: 10px;margin-top:10px;text-align:center;min-height:15px;">
    <input id="ImportFile" type="file"  name="ImportFile" runat="server" class="NormalTextBox dnnPrimaryAction">
</div>
<div style="width: 100%;text-align:center;clear:both;">
    <asp:RequiredFieldValidator ID="RFVImportFile" runat="server" CssClass="NormalRed" Style="width: 100%; text-align: center"
        Display="Dynamic" ErrorMessage="Please select a file. Valid file types are .XML, .CSV (comma delimited) and .TXT (tab delimited)."
        ControlToValidate="ImportFile"></asp:RequiredFieldValidator>
    <asp:CustomValidator ID="cvErrorMessage" runat="server" CssClass="NormalRed" Style="width: 100%; text-align: center" Display="Dynamic" ErrorMessage="Invalid file type detected"></asp:CustomValidator>
</div>
<asp:Label ID="GUIDtext" runat="server" Visible="false"></asp:Label>

