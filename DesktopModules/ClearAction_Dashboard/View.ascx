<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="ClearAction.Modules.Dashboard.View" %>
<asp:Literal ID="pHolder" runat="server"></asp:Literal>
<asp:Button ID="Button1" runat="server" OnClick="btnGeneratePDF_Click" Text="Generate"  style="display:none;"/>
<asp:TextBox ID="txtSearch" CssClass="txtsearch" runat="server" Text="" style="display:none;"></asp:TextBox>
<asp:Button ID="btn_search" runat="server" CssClass="btnsearch" OnClick="btn_search_Click" Text="Search" style="display:none;"  />
