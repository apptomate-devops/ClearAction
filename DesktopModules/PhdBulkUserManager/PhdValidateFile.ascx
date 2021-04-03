<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdValidateFile" Codebehind="PhdValidateFile.ascx.vb" %>
<asp:Panel ID="panValidationResults" runat="server" BorderColor="Silver" BorderStyle="None"
    BorderWidth="1px" GroupingText="Issues found with imported file" CssClass="SubHead" >
    <div style="margin-left: 30px; text-align: left;" class="normal" id="ErrorMessage">
        <div style="text-align: left;" class="normal" id="InvalidUsernames">
            <asp:PlaceHolder ID="phUserNameMsg" runat="server"></asp:PlaceHolder>
            <br />
        </div>
        <div style="text-align: left;" class="normal" id="InvalidPasswords">
            <asp:PlaceHolder ID="phPasswordMsg" runat="server"></asp:PlaceHolder>
            <br />
            <asp:RadioButtonList ID="rbPasswordFix" runat="server" CssClass="Normal">
                <asp:ListItem Selected="True" Value="Random">Assign a random password to these accounts</asp:ListItem>
                <asp:ListItem Value="Remove">Remove these accounts from the import</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <br />
    </div>
    <div style="text-align: center;">
        <asp:Button ID="butViewErrors" runat="server" Text="View full error details" CssClass="CommandButton dnnSecondaryAction" />
    </div>
</asp:Panel>
<asp:Panel ID="panCleanseResults" runat="server" BorderColor="Silver" BorderStyle="None"
    BorderWidth="1px" GroupingText="Results of cleaning imported file" CssClass="SubHead" >
    <div style="margin-left: 60px; text-align: left;" class="normal" id="CleanseMessage">
        <asp:PlaceHolder ID="phResultsMsg" runat="server"></asp:PlaceHolder>
        <br />
    </div>
    <br />
</asp:Panel>
    <asp:Label ID="GUIDtext" runat="server" Visible="false"></asp:Label>


