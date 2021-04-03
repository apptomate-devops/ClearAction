<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdScheduleList" Codebehind="PhdScheduleList.ascx.vb" %>
<asp:Label ID="lblMessage" runat="server"></asp:Label>
<asp:Repeater ID="repJobs"  runat="server" >
    <HeaderTemplate>
       <table  style="width:100%;"  class="dnnGrid dnnSecurityRolesGrid">
           <tbody>
                <tr class="dnnGridHeader">
                    <th></th>
                    <th style="text-align:left;">Job Name</th>
                    <th style="text-align:left;">Job Type</th>
                    <th style="text-align:left;">Last/Next run date</th>
                    <th style="text-align:left;">Last run status</th>
                    <th style="text-align:left;">Schedule</th>
                    <th style="text-align:left;">Active</th>
                </tr>
    </HeaderTemplate>
    <ItemTemplate>
            <tr class="dnnGridItem">
                <td style="vertical-align:top;">
                    <asp:LinkButton ID="EditLink" CommandName="EditLinkCmd" runat="server"><asp:Image ID="Image1" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit"  resourcekey="Edit" /></asp:LinkButton>
                    <asp:HiddenField ID="HiddenJobID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "JobID").ToString() %>' />
                    <asp:HiddenField ID="HiddenJobType" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "JobType").ToString() %>' />
                </td>
                <td style="vertical-align:top;"><%# DataBinder.Eval(Container.DataItem,"JobTitle").ToString() %></td>
                <td style="vertical-align:top;"><%# DataBinder.Eval(Container.DataItem, "JobType").ToString()%></td>
                <td style="vertical-align:top;"><%# DataBinder.Eval(Container.DataItem,"LastRunTime").ToString() %><br /><%# DataBinder.Eval(Container.DataItem,"NextScheduleTime").ToString() %></td>
                <td style="vertical-align:top;"><asp:LinkButton ID="LogLink" runat="server" CommandName="LogLinkCmd" ToolTip="View log details"  ><%# DataBinder.Eval(Container.DataItem,"LastRunStatus").ToString() %></asp:LinkButton></td>
                <td style="vertical-align:top;">Repeat every <%# DataBinder.Eval(Container.DataItem,"RepeatEverySpan").ToString() %> <%# DataBinder.Eval(Container.DataItem,"RepeatEveryType").ToString() %></td>
                <td style="vertical-align:top;"><%# DataBinder.Eval(Container.DataItem,"Active").ToString() %></td>
            </tr>
    </ItemTemplate> 
    <FooterTemplate> 
           </tbody>
    </table>
    </FooterTemplate>
</asp:Repeater>
<div style="text-align:center;margin-top:10px;margin-bottom:10px;width:100%;">
    <asp:Button ID="butAddImport" runat="server" CssClass="CommandButton dnnSecondaryAction" Text="  Add Import Job  " CausesValidation="False" />
    &nbsp;&nbsp;
    <asp:Button ID="butExit" runat="server" CssClass="CommandButton dnnSecondaryAction" Text="     Cancel     " CausesValidation="False" />
    &nbsp;&nbsp;
    <asp:Button ID="butAddExport" runat="server" CssClass="CommandButton dnnSecondaryAction" Text="  Add Export Job  " CausesValidation="False" />
    
</div>


