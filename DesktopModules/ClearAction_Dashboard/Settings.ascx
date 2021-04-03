<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="ClearAction.Modules.Dashboard.Components.Settings" %>

<div class="Form-item">
    Module Work For:
     <asp:RadioButtonList ID="rdobtn" runat="server">
         <asp:ListItem Text="Blog" Value="0"></asp:ListItem>
         <asp:ListItem Text="Forum" Value="1"></asp:ListItem>
         <asp:ListItem Text="Community Calls" Value="2"></asp:ListItem>
         <asp:ListItem Text="Webcast Conversations" Value="3"></asp:ListItem>
        <%-- <asp:ListItem Text="Zoom Meetings" Value="5"></asp:ListItem>--%>
     </asp:RadioButtonList>

</div>

<div class="Form-item">
    Feed Url:
 <asp:TextBox ID="txtFeedUrl" runat="server" Width="300" CssClass="NormalTextBox"></asp:TextBox>

</div>


<div class="Form-item">
    No. Of Item:
 <asp:TextBox ID="txtNumberofRecord" runat="server" Width="300" CssClass="NormalTextBox"></asp:TextBox>

</div>


