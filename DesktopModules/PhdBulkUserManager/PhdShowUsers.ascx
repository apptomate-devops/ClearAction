<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdShowUsers" Codebehind="PhdShowUsers.ascx.vb" %>
<div style="margin-bottom:15px;">
<span class="SubHead" style="text-align: center;">
    <asp:Label ID="LblImportedRecords" runat="server" CssClass="NormalBold"></asp:Label>
</span>
    <asp:PlaceHolder ID="phResults" runat="server">
        <div style="overflow: auto; height: 150px;">
            <asp:DataGrid ID="DGResults" runat="server" HorizontalAlign="Left" BorderColor="Blue"
                BorderWidth="1px" BackColor="White" BorderStyle="Solid" CellPadding="2" AllowPaging="True" Width="100%">
                <FooterStyle Wrap="False" ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                <SelectedItemStyle Font-Bold="True" Wrap="False" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                <EditItemStyle Wrap="False"></EditItemStyle>
                <AlternatingItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Top" BackColor="#FFFFC0"></AlternatingItemStyle>
                <ItemStyle Wrap="False" HorizontalAlign="Left" ForeColor="#330099" VerticalAlign="Top" BackColor="White" CssClass="Normal"></ItemStyle>
                <HeaderStyle Font-Bold="True" Wrap="False" ForeColor="#FFFFCC" BackColor="#8080FF" CssClass="NormalBold"></HeaderStyle>
                <PagerStyle Visible="False" HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC" Wrap="False"></PagerStyle>
            </asp:DataGrid>
        </div>
    </asp:PlaceHolder>
</div>

<asp:Label ID="lblRecordCount" runat="server" Text="0" Visible="False"></asp:Label>
