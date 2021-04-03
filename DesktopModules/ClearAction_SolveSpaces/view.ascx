<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="ClearAction.Modules.SolveSpaces.View" %>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/jquery-jtemplates/jquery-jtemplates.js"></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/Module/Common.js"></script>
<asp:Literal ID="pnlSolveSpaceDashboard" runat="server">
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/Module/Module.js"></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/context/jquery.contextMenu.js"></script>
<link href="/DesktopModules/ClearAction_SolveSpaces/Scripts/context/jquery.contextMenu.css" rel="stylesheet" />
<div class="solve-spaces-top-nav">
    <div class="solve-spaces-title">
        <h1 style="font-size:25px">Solve-Spaces<sup style="font-size:10px">TM</sup></h1>
    </div>
    <div class="solve-spaces-nav">
        <a  href="#" id="lnkMyContent" onclick="return Set2MyContent(this);">
           <strong>My Vault</strong>Curated for me</a> 
        <a class="active" href="#" id="lnkAllContent" onclick="return Set2AllContent(this);">
            <strong>Show All</strong>Everything at all levels</a>
    </div>
    <div class="solve-spaces-search">
        <div style="position:relative;width:100%">
            <input required="" id="CA_SS_txtSearch" value="" type="text">
            <div onclick="return SearchInSolveSpace();" class="CA_SearchGlass" ></div>
        </div>
        <div class="clearfix"></div>
        <label id="lblClearSearch" onclick="return ClearSearch();" style="display:none;" class="ClearSearch">Clear Search Result</label>
    </div>
</div>
<div id="CA_SS_dvCatContainer" style="width:100%;clear:both;">
</div>
<div class="solve-spaces-vault">
	<div class="filter-bar">
		<div class="col-md-8">
			<ul class="filter-spaces button-group filter-button-group" style="margin-bottom:20px;">
				<li class="active" data-filter="*"><a href="#" onclick="return CA_LoadSolveSpacesList('All')">All</a></li>
				<li><a data-filter=".to-do" href="#" onclick="return CA_LoadSolveSpacesList('To-Do')"><img alt="img_18" src="/portals/0/images/img_18.png" height="9" width="9">To Do</a></li>
				<li><a data-filter=".in-progress" href="#" onclick="return CA_LoadSolveSpacesList('In-Progress')"><img alt="img_16" src="/portals/0/images/img_16.png" height="9" width="9">In Progress</a></li>
				<li><a data-filter=".done" href="#" onclick="return CA_LoadSolveSpacesList('Completed')"><img alt="img_17" src="/portals/0/images/img_17.png" height="9" width="9">Done</a></li>
				<li><a data-filter=".new" href="#" onclick="return CA_LoadSolveSpacesList('Soon')">Coming Soon</a></li>
			</ul>
		</div>
		<div class="col-md-4 text-right">
			<select id="CA_SS_ddlSortBy" class="forum-select">
				<option value="1"  class="CA_SortOptions">Alpha a-z</option>
				<option value="2"  class="CA_SortOptions">Alpha z-a</option>
				<option selected="selected" value="3" class="CA_SortOptions">Recent to former</option>
				<option value="4"  class="CA_SortOptions">Former to recent</option>
				<option value="5"  class="CA_SortOptions">Active today</option>
				<option value="6"  class="CA_SortOptions">Active last 7 days</option>
				<option value="7"  class="CA_SortOptions">Active this month</option>
			</select>
		</div>
	</div>
    <div class="clearfix"></div>
	<div class="solve_dtls">
		<div id="CA_SS_dvSSContainer" style="width:100%;clear:both;"></div>
	</div>
</div>
<div id="CA_SS_dvSSContainer" style="width:100%;clear:both;" class="bstrap3-material">
</div>
</asp:Literal>
<asp:Literal ID="ltTopSolveSpaces" runat="server" >
     <script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/Module/Recommended.js"></script>
    <div id="CS_SS_dvTopSSContainer" style="width:100%;clear:both;"></div>
</asp:Literal>
<script type="text/javascript">
    var CA_UserID =<%=this.UserId%>;
</script>