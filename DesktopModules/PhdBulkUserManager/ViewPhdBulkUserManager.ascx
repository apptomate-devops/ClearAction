<%@ Control language="vb" Inherits="Phd.Dnn.BulkUserManager.Screens.ViewPhdBulkUserManager" AutoEventWireup="false" Explicit="True" Codebehind="ViewPhdBulkUserManager.ascx.vb" %>
<%@ Register Src="PhdMainExport.ascx" TagName="PhdMainExport" TagPrefix="phdexport" %>
<%@ Register Src="PhdMainImport.ascx" TagName="PhdMainImport" TagPrefix="phdimport" %>
<%@ Register Src="PhdTitle.ascx" TagName="PhdTitle" TagPrefix="phdt" %>
<%@ Register Src="PhdNavigation.ascx" TagName="PhdNavigation" TagPrefix="phdn" %>
<%@ Register Src="PhdMainUpdate.ascx" TagName="PhdMainUpdate" TagPrefix="phdupdate" %>
<%@ Register Src="PhdMainDelete.ascx" TagName="PhdSelectDeleteUsers" TagPrefix="phddelete" %>
<%@ Register Src="PhdMainRecover.ascx" TagName="PhdMainRecover" TagPrefix="phdrecover" %>
<%@ Register Src="PhdMainRemove.ascx" TagName="PhdMainRemove" TagPrefix="phdremove" %>
<%@ Register Src="PhdMainSchedule.ascx" TagName="PhdMainSchedule" TagPrefix="phdschedule" %>
<%@ Register Src="PhdVersion.ascx" TagName="PhdVersion" TagPrefix="phdversion" %>
<%@ Register Src="PhdNotifications.ascx" TagName="PhdNotification" TagPrefix="phdnotification" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<!-- Main module start -->
<asp:DataList ID="lstContent" DataKeyField="ItemID" runat="server" CellPadding="4">
    <ItemTemplate>
        <tablestyle="background:inherit; width:100%;">
            <tr>
                <td style="vertical-align:top;width:100%;text-align:left;padding:4px;">
                    <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# EditURL("ItemID",DataBinder.Eval(Container.DataItem,"ItemID")) %>'
                        Visible="<%# IsEditable %>" runat="server">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit"
                            Visible="<%# IsEditable%>" resourcekey="Edit" /></asp:HyperLink>
                    <asp:Label ID="lblContent" runat="server" CssClass="Normal" />
                </td>
            </tr>
        </table>
    </ItemTemplate>
</asp:DataList>
<div style="margin-left: 30px; margin-right: 30px;">
    <asp:Panel ID="panMain" runat="server" Width="100%">
        <phdt:PhdTitle ID="PhdTitle" runat="server" Title="Select the action required" />
        <div style="margin-left: 30px; padding-top: 5px;">
            <asp:RadioButtonList ID="rbMenu" runat="server" CssClass="Normal">
                <asp:ListItem Value="Import" Selected="True">Import user details (Allows add and update)</asp:ListItem>
                <asp:ListItem Value="Export">Export user details</asp:ListItem>
                <asp:ListItem Value="Update">Update group of users</asp:ListItem>
                <asp:ListItem Value="Delete">Delete users</asp:ListItem>
                <asp:ListItem Value="DeleteRemove">Permanently remove users</asp:ListItem>
                <asp:ListItem Value="Recover">Recover deleted users</asp:ListItem>
                <asp:ListItem Value="Remove">Permanently remove deleted users</asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="rfvMenu" runat="server" ControlToValidate="rbMenu"
                Display="Dynamic" ErrorMessage="Please select an option"></asp:RequiredFieldValidator>
        </div>
        <phdnotification:PhdNotification ID="NotificationControl" runat="server"  />
        <phdn:PhdNavigation ID="NavigationControl" runat="server" PreviousEnabled="false" />
    </asp:Panel>
    <phdexport:PhdMainExport ID="ExportUsersControl" runat="server" Visible="false" />
    <phdimport:PhdMainImport ID="ImportUsersControl" runat="server" Visible="false" />
    <phdupdate:PhdMainUpdate ID="SelectUsersControl" runat="server" Visible="false" />
    <phddelete:PhdSelectDeleteUsers ID="SelectDeleteUsersControl" runat="server" Visible="false" />

    <phdrecover:PhdMainRecover ID="RecoverUsersControl" runat="server" Visible="false" />
    <phdremove:PhdMainRemove ID="RemoveUsersControl" runat="server" Visible="false" />

    <phdschedule:PhdMainSchedule ID="ScheduleJobsControl" runat="server" Visible="false" />

    <phdversion:PhdVersion ID="PhdVersion" runat="server" Visible="true" />
    <asp:Label ID="GUIDtext" runat="server" Visible="false"></asp:Label>

</div>
<!-- Main module end -->




