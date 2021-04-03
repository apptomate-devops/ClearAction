<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PhdRoleSelection.ascx.vb" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdRoleSelection" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<asp:Panel ID="panNoRoles" runat="server" CssClass="WorkPanel" Visible="False">
    <asp:Label ID="lblNoMoreRoles" CssClass="Normal" runat="server">No more roles are available to add.</asp:Label>
</asp:Panel>


<asp:Panel ID="panSelectRole" runat="server" CssClass="WorkPanel" Visible="True">
    <asp:GridView ID="grdRoles" runat="server" AutoGenerateColumns="False" GridLines="None"
        BorderWidth="0px" Width="100%" CellPadding="2" BorderStyle="None"
        DataKeyNames="RoleName" ShowFooter="false" ShowHeader="true"  >
        <HeaderStyle CssClass="NormalBold" HorizontalAlign="Left"  />
        <FooterStyle CssClass="Normal" HorizontalAlign="right"  />
       
        <RowStyle CssClass="Normal" />

        <Columns>
            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/delete.gif"
                ShowDeleteButton="True" CausesValidation="false"  ItemStyle-Width="20px" HeaderStyle-Width="20px" />
            <asp:TemplateField HeaderText ="Security Role" ItemStyle-Width="250px" HeaderStyle-Width="250px">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.RoleName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date"  ItemStyle-Width="250px"  HeaderStyle-Width="250px"/>
            <asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date"  ItemStyle-Width="250px" HeaderStyle-Width="250px"/>
        </Columns>

    </asp:GridView>
    <table id="Table7" style="background: inherit; border: none; border-spacing: 0; border-collapse: collapse; width:100%">
         <asp:PlaceHolder ID="phSelectedRoles" runat="server" />
        <tr>
            <td style="vertical-align: top; padding: 1px; padding-right: 5px; width:20px;"> 
            </td>
            <td style="vertical-align: middle; height: 21px; padding: 1px; padding-right:15px; width:250px;">
                <asp:DropDownList ID="ddlRoles" runat="server" CssClass="Normal"
                    ValidationGroup="AddRolesValidation">
                </asp:DropDownList>
            </td>
            <td style="vertical-align: middle; height: 21px; padding: 1px; padding-right:15px; white-space: nowrap; width:250px;">
                <asp:TextBox ID="tbEffectiveDate" runat="server" CssClass="Normal" Columns="12"
                    ValidationGroup="AddRolesValidation"></asp:TextBox>
                <asp:HyperLink ID="cmdEffectiveCalendar" runat="server" CssClass="CommandButton">Calendar</asp:HyperLink>
            </td>
            <td style="vertical-align: middle; height: 21px; padding: 1px; padding-right:15px; white-space: nowrap; width:250px;">
                <asp:TextBox ID="tbExpiryDate" runat="server" CssClass="Normal" Columns="12"
                    ValidationGroup="AddRolesValidation"></asp:TextBox>
                <asp:HyperLink ID="cmdExpiryCalendar" runat="server" CssClass="CommandButton">Calendar</asp:HyperLink>
            </td>
         </tr>
    </table>
      <asp:Button ID="CmdAdd" CssClass="CommandButton" runat="server" ValidationGroup="AddRolesValidation" Text="Add Role" ></asp:Button>
    <br />
    <asp:RequiredFieldValidator ID="rfvRole" runat="server"
        ControlToValidate="ddlRoles" ErrorMessage="Role must be selected"
        ValidationGroup="AddRolesValidation" ></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="cvDates" runat="server"
        ControlToCompare="tbExpiryDate" ControlToValidate="tbEffectiveDate"
        ErrorMessage="Effective Date must be earlier than Expiry Date"
        Operator="LessThanEqual" Type="Date" ValidationGroup="AddRolesValidation" ></asp:CompareValidator>
    &nbsp;

</asp:Panel>
<asp:Panel ID="pnlUserRoles" runat="server" CssClass="WorkPanel" Visible="True">

    <asp:CheckBox ID="cbNotification" runat="server" Text="Send Notification" CssClass="normalCheckBox" Visible="True" /><br />
</asp:Panel>
<asp:HiddenField ID="hidRoleList" runat="server" />
