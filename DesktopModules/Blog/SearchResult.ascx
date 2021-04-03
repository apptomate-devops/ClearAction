<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="SearchResult.ascx.vb" Inherits="DotNetNuke.Modules.Blog.SearchResult" %>
<%@ Register TagPrefix="blog" TagName="topheader" Src="controls/TopHeader.ascx" %>

<div class="insights_wrapper">
    <div class="col-sm-12">
        <blog:topheader runat="server" id="topheader" />
        <div class="clearfix"></div>
        <div class="Insight_container">
            <heading >
                <asp:Label ID="lblSearchheading" runat="server"></asp:Label>
                
            </heading><br /><br />
            <asp:Literal ID="ltrlSearch" runat="server">
            </asp:Literal>
        </div>
    </div>
</div>
