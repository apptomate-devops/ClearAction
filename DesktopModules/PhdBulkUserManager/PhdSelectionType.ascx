<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdSelectionType" Codebehind="PhdSelectionType.ascx.vb" %>
<%@ Register Src="PhdSelectUsers.ascx" TagName="PhdSelectUsers" TagPrefix="uc1" %>
<%@ Register Src="PhdImportFile.ascx" TagName="PhdImportFile" TagPrefix="uc2" %>
<asp:Panel ID="panSelectType" runat="server" BorderColor="Silver" BorderStyle="None" BorderWidth="1px" GroupingText="Select users by"  CssClass="SubHead" Style="margin-left: 30px;
    margin-right: 30px;">
     <div style="margin-left: 60px;text-align:left;" class="normal">
<asp:RadioButtonList ID="RBSelectType" runat="server" CssClass="Normal" 
    Visible="True" AutoPostBack="True" >
    <asp:ListItem Value="SELECT" Selected="True">searching</asp:ListItem>
    <asp:ListItem Value="IMPORT">importing from file</asp:ListItem>
</asp:RadioButtonList>
</div>
<br />
</asp:Panel>
<br />
<uc1:PhdSelectUsers ID="SelectUsersControl" runat="server" />
<uc2:PhdImportFile ID="ImportFileControl" runat="server" />
 