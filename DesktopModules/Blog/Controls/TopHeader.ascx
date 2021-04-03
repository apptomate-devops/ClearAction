<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TopHeader.ascx.vb" Inherits="DotNetNuke.Modules.Blog.Controls.TopHeader" %>

<script type="text/javascript">
    var AF_CurrentCatID = -1;
    var AF_IsMyVault = '<%=Me.IsMyVault()%>';
    var AF_CurrentStatus = <%=Me.CurrentStatus()%>; // Default is Zero / 1/2


    $(document).ready(function () {
        $(".solve-spaces-nav a").removeClass("active");
        if (AF_IsMyVault != 1) {
            $(".solve-spaces-nav a:first").addClass("active");
        } else {
            $(".solve-spaces-nav a:last").attr("class", "active");
        }
        debugger;
        var AF_CurrentCatID = getParameterByName('CategoryId');
        $("#CA_Blog_dvCatContainer li").removeClass("active");
        $("#CA_Blog_dvCatContainer li a[CatID='" + AF_CurrentCatID + "']").parent().addClass("active");

        $("#dvBlogStatusFilter li").removeClass("active");
        $("#dvBlogStatusFilter li[statusid='" + AF_CurrentStatus + "']").addClass("active");

        // trap enter key of search textbox 
        $('[id*="txtSearch"]').on("keypress", function (event) {
            if (event.which == 13) {
                $('[id*="lnkSearch"]').click();
            }
        });
    })
    function getParameterByName(name) {
        var url = window.location.pathname;
        if (url.lastIndexOf(name) == -1) return -1;
        var iStart = url.lastIndexOf(name) + name.length + 1;
        var iEnd = url.lastIndexOf("/SortBy");
        return url.substring(iStart, iEnd);
    }
</script>

<div class="row">
	<div class="col-md-12">
		<div class="solve-spaces-top-nav">
			<div class="forum-title">
				<h1>Insights</h1>
			</div>
			<div class="forum-nav">
				<div class="forum-topics">
					<div class="forum-topics-title">
						<asp:Label ID="lblTopicCount"   runat="server"></asp:Label>
					</div>
					<div class="forum-topics-new">
						<asp:HyperLink ID="hyperlinkNew" runat="server" Text="New"></asp:HyperLink>
					</div>
				</div>
			</div>
			<div class="solve-spaces-nav">
				<asp:LinkButton ID="btnMyVault" runat="server" Text="<strong>My Vault</strong>Curated for me" OnClick="btnMyVault_Click"></asp:LinkButton>
				<asp:LinkButton ID="btnShowAll" runat="server" Text="<strong>Show All</strong>Everything at all levels" OnClick="btnShowAll_Click"></asp:LinkButton>
			</div>
			<div class="forum-search">
                <!-- Sachin : Please dont remove the div below, we need this -->
                <asp:Panel ID="pnlBlogSearch" runat="server" DefaultButton="lnkSearch" Style="position: relative">
                    <asp:TextBox ID="txtSearch" runat="server" MaxLength="50" onkeypress="" />
                    <asp:LinkButton runat="server" ID="lnkSearch" CssClass="searchbutton" />
                </asp:Panel>
			</div>
		</div>
	</div>
</div>
<div class="row">
	<div class="col-md-12">
		<div id="CA_Blog_dvCatContainer" class="solve-spaces-middle-nav">
			<nav>
				<ul>
					<li class="active">
						<asp:LinkButton ID="btnAllCat" CatId="-1" runat="server" OnClick="btnAllCat_Click"><div class="table"><div class="table-cell">All</div></div></asp:LinkButton>
					</li>
					<asp:DataList ID="dataListGlobalCat" runat="server"  RepeatColumns="10" RepeatDirection="Horizontal" RepeatLayout="Flow" OnItemDataBound="dataListGlobalCat_ItemDataBound" ShowFooter="False" ShowHeader="False" OnItemCommand="dataListGlobalCat_ItemCommand">
						<ItemTemplate>
							<li>
								<asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
							</li>
						</ItemTemplate>
					</asp:DataList>
				</ul>
			</nav>
		</div>
	</div>
</div>