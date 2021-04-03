<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdScheduleLog" Codebehind="PhdScheduleLog.ascx.vb" %>
 <%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<asp:Label ID="lblMessage" runat="server"></asp:Label>
<asp:Repeater ID="repLogs"  runat="server" >
    <HeaderTemplate>
       <table  style="width:100%;padding:0;border-collapse:collapse;"  class="dnnGrid dnnSecurityRolesGrid">
           <tbody>
                <tr class="dnnGridHeader">
                    <th style="text-align:left;">Start/End Time</th>
                    <th style="text-align:left;">Status</th>
                    <th style="text-align:left;">Details</th>
                </tr>

    </HeaderTemplate>
    <ItemTemplate>
            <tr class="dnnGridItem" style="padding-bottom:3px; border-bottom:1px solid black;">
                <td style="vertical-align:top;padding-bottom:5px; padding-right:10px; border-bottom:1px solid #d1d1d1; white-space:nowrap; "><%# DataBinder.Eval(Container.DataItem, "JobStartTime").ToString()%><br />
                                <%# DataBinder.Eval(Container.DataItem, "JobEndTime").ToString()%></td>
                <td style="vertical-align:top;padding-bottom:5px; padding-right:10px; border-bottom:1px solid #d1d1d1;  white-space:nowrap;"><%# DataBinder.Eval(Container.DataItem, "RunStatus").ToString()%></td>
                <td style="vertical-align:top;padding-bottom:5px; border-bottom:1px solid #d1d1d1;"><%# DataBinder.Eval(Container.DataItem, "Results").ToString().Replace(vbCrLf, "<br />")%></td>
            </tr>

    </ItemTemplate> 
    <FooterTemplate> 
           </tbody>
    </table>
 
    </FooterTemplate>
</asp:Repeater>
<asp:Label ID="lblJobId" runat="server" Visible="false" />

<br />
<p style="text-align:center;">
    <asp:LinkButton ID="clearLog" runat="server" Text="Clear log" CausesValidation="false" OnClick="clearLog_Click" ></asp:LinkButton>
</p>

<p style="text-align:center;">
    <asp:linkbutton cssclass="dnnPrimaryAction" id="cmdOK" resourcekey="cmdOK" runat="server" borderstyle="none" text=" OK " causesvalidation="False" OnClick="cmdOK_Click"></asp:linkbutton>&nbsp;
</p>
