<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="ClearAction.Modules.SolveSpaces.Settings" %>
<div style="width: 100%; border: dashed 1px #ff0000;padding:40px;">
    <table style="width: 100%;">
        <tr>
            <td style="width:25%;"><label>Display Mode:</label></td>
            <td style="width:75%;"><asp:DropDownList ID="ddlDisplayMode" runat="server">
                <asp:ListItem Text="Dashboard" Value="Dashboard" Selected="True"></asp:ListItem>
                <asp:ListItem Text="MyExchange" Value="MyExchange"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width:25%;">
                <label>My Vault Tab:</label>
            </td>
            <td style="width:75%;">
                <asp:DropDownList ID="ddlTabs" runat="server"></asp:DropDownList>
            </t>
        </tr>
    </table>
</div>