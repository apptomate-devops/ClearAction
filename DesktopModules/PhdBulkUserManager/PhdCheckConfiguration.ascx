<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdCheckConfiguration" Codebehind="PhdCheckConfiguration.ascx.vb" %>
<div class="normal" id="InvalidUsernames" style="text-align: center;">
    <asp:PlaceHolder ID="phUserCount" runat="server"></asp:PlaceHolder>
    <br />
</div>
<asp:Panel ID="panPasswordMsg" runat="server" BorderColor="Silver" BorderStyle="None"
    BorderWidth="1px" GroupingText="Passwords cannot be retrieved with the current portal configuration"
     Font-Bold="True">
    <div style="margin-left: 40px; margin-top: 20px; text-align: left;" class="normal"
        id="ErrorMessage">
        <div style="text-align: left;" class="normal" id="InvalidPasswords">
            <asp:Label ID="lblMessage" runat="server" CssClass="Normal" Font-Bold="False"
                Text=""></asp:Label>
            <br />
        </div>
    </div>
</asp:Panel>

 

