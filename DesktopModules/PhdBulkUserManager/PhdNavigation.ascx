<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdNavigation" Codebehind="PhdNavigation.ascx.vb" %>
<div style="text-align: center;padding-top:5px;padding-bottom:10px;">
    <asp:Button ID="butPrev" runat="server" CssClass="CommandButton dnnSecondaryAction" Text="  < Previous  " CausesValidation="False" />
    &nbsp; &nbsp;&nbsp;
    <asp:Button ID="butNext" runat="server" CssClass="CommandButton dnnPrimaryAction" Text="    Next >    " />
    <br />
    <asp:Label ID="lblCurrentStatus" runat="server" Visible="False"></asp:Label>
</div>
 