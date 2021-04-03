<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdShowResults" Codebehind="PhdShowResults.ascx.vb" %>


<asp:Panel ID="panFileType" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" GroupingText="" CssClass="SubHead" Style="margin-top: 10px; margin-bottom: 10px;padding-left:30px;padding-right:20px;">
        <br />
       <div style=" text-align: left;" class="normal">
        <asp:Label ID="lblResults" runat="server" Visible="True"></asp:Label>
    </div>
    <br />
    <div style="text-align: center;">
        <asp:Button ID="butViewFullResults" runat="server" Text="View full results" CssClass="CommandButton dnnSecondaryAction" />
    </div>

</asp:Panel>
    <asp:Label ID="GUIDtext" runat="server" Visible="false"></asp:Label>

