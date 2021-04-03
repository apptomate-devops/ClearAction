<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="ClearAction.Modules.UserAnalytics.Settings" %>
<table>
    <tr>
        <td>
            <label>Display Mode:</label>
        </td>
        <td>
            <asp:DropDownList runat="server" ID="ddlDisplayMode">
                <asp:ListItem Text="All" Value="-1">All</asp:ListItem>
                <asp:ListItem Text="Solve-Space" Value="1"></asp:ListItem>
                <asp:ListItem Text="Forum" Value="2"></asp:ListItem>
                <asp:ListItem Text="Insight" Value="3"></asp:ListItem>
            </asp:DropDownList></td>
    </tr>
</table>

<!-- uncomment the code below to start using the DNN Form pattern to create and update settings -->
<%--  

<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

	<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a></h2>
	<fieldset>
        <div class="dnnFormItem">
            <dnn:Label ID="lblSetting1" runat="server" /> 
 
            <asp:TextBox ID="txtSetting1" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:label ID="lblSetting2" runat="server" />
            <asp:TextBox ID="txtSetting2" runat="server" />
        </div>
    </fieldset>


--%>