<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="ClearAction.Modules.ComponentManager.View" %>
<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>



<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<style>
    .wid150 {
        width: 150px;
    }

    .wid175 {
        width: 175px;
    }

    .marg10 {
        margin: 10px;
    }

    .marg5 {
        margin: 5px;
    }

    .pad10 {
        padding: 10px;
    }

    .catbutton {
        background-color: #39618c;
        border-radius: 5px;
        color: white;
        padding: .5em;
        text-decoration: none;
    }

        .catbutton:hover {
            background-color: #39618c;
            color: white;
        }

        .catbutton:active, .catbutton:focus {
            background-color: #42f453;
            color: white;
        }

    .addbtn {
        background-color: #39618c;
        border-radius: 5px;
        color: white;
        padding: .5em;
        text-decoration: none;
    }

        .addbtn:hover {
            background-color: #39618c;
            color: white;
            text-decoration: none;
        }

    .cmwrapper {
        display: grid;
        grid-template-columns: 5% 30% 65%;
        grid-gap: 1px;
        background-color: #f7f8f9;
        color: #000;
        margin-bottom: 1px;
    }

    .catwrapper {
        display: grid;
        grid-template-columns: 8% 90% 2%;
        grid-gap: 1px;
        background-color: #f7f8f9;
        color: #000;
        margin-bottom: 1px;
    }

    .cmbox {
        background-color: #dee5ef;
        color: #000; /*border-radius: 5px; */
        padding-left: 5px;
    }
</style>

<div class="pad10">
    <a href="javascript:void(0);" id="lnkUser" onclick="CA_ShowSection('U');return false;" class="catbutton active">Manage User Components</a>
    <a href="javascript:void(0);" id="lnkGlobCat" onclick="CA_ShowSection('G');return false;" class="catbutton">Manage Global Category</a>
    <a href="javascript:void(0);" id="lnkTerm" onclick="CA_ShowSection('T');return false;" class="catbutton">Manage Taxonomy Terms</a>
    <a href="javascript:void(0);" id="lnkRTags" onclick="CA_ShowSection('R');return false;" class="catbutton">Manage Tags</a>
    <a href="javascript:void(0);" id="lnkGlobalCompany" onclick="CA_ShowSection('C');return false;" class="catbutton">Manage Company</a>
</div>

<div id="dvGlobalCompany" style="display: none;">

    <div style="margin-left: 20px;">
        <label style="margin-left: 20px; margin-top: 20px;">Manage Company</label>

        <div style="width: 100%; height: 50px; margin-top: 20px; margin-left: 20px; border-top: solid 1px #808080; padding-top: 15px;">
            <a href="javascript:void(0);" id="lnkAddCompany" onclick="CA_ShowAddCompany(1,0,0,'',0);return false;" class="btn btn-info">Add New</a>
        </div>
    </div>

    <div id="dvComPagination" style="margin: 5px; margin-left: 20px;"></div>
    <div id="dvAllCompany" class="marg10 pad10" style="margin-left: 20px;"></div>



    <div id="divAddComp" style="display: none;" class="pad10">

        <div class="dnnFormItem">

            <dnn:label ID="lblCompanyName" runat="server" Text="Company Name" />
            <input id="txtCompName" type="text" style="width: 400px;" class="form-control" maxlength="200" />
        </div>

        <div class="dnnFormItem">
            
            <dnn:label ID="lblIsCompanyActive" runat="server" Text="Active" />
            <input type="checkbox" id="chkComp" name="chkComp" style="-moz-appearance: checkbox; -webkit-appearance: checkbox;" />
        </div>

        <div class="dnnFormItem">
            <br />
        </div>
        <div class="clear">
        </div>


        <div>
            <input id="txtCompRecordID" style="display: none;" type="text" />
          
            &nbsp<input type="button" id="btnAddComp" style="display: none;" value="ADD" class="btn btn-primary" onclick="CA_AddCompany('0');" />
            &nbsp<input type="button" id="btnEditComp" style="display: none;" value="UPDATE" class="btn btn-primary" onclick="CA_AddCompany('1');" />
            <input type="button" id="btnCancelCompany" value="CANCEL" class="btn-danger" onclick="CA_ResetCompany();" />
        </div>
    </div>


</div>

<div id="dvRTags" style="display: none;" class="marg10">

    <label style="margin-left: 20px; margin-top: 20px;">Manage Tags</label>

    <div style="width: 100%; height: 50px; margin-top: 20px; margin-left: 20px; border-top: solid 1px #808080; padding-top: 15px;"></div>

    <%--<div id="dvRTagPagination" style="margin:5px;"></div>
        <div id="dvRTagList" class="marg10 pad10"></div>--%>

    <div id="divAddRTag" style="margin-left: 20px;">

        <div class="marg5">
            <label class="wid150">Parent Tag</label>
            <br />
            <select id="ddlTags" style="width: 400px;" onchange="CA_RTagChange();"></select>
            <br />
        </div>
        <div class="marg5">
            <br />
            <div>
                <label>Related Tags ( ; separated)</label>
            </div>
            <input id="txtRTag" type="text" style="width: 500px;" class="edTextBox" />
            <br />

            <input style="margin-top: 20px;" type="button" id="btnAddRTag" value="SAVE" class="CA_btnSearch" onclick="CA_AddRTag('0');" />
        </div>
    </div>

</div>

<div id="dvManageCategory" style="display: none;">

    <label style="margin-left: 20px; margin-top: 20px;">Manage Global Category</label>

    <div style="width: 100%; height: 50px; margin-top: 20px; margin-left: 20px; border-top: solid 1px #808080; padding-top: 15px;"></div>

    <div id="dvCatPagination" style="margin: 5px; margin-left: 20px;"></div>
    <div id="dvAllCat" class="marg10 pad10" style="margin-left: 20px;"></div>

    <div style="margin-left: 20px;"><a href="javascript:void(0);" id="lnkAdd" onclick="CA_ShowAdd(1,0,0,'',0);return false;" class="addbtn">Add New</a></div>

    <div id="divAddCat" style="display: none;" class="pad10">

        <div class="marg5">
            <label class="wid150">Category Name</label>
            <input id="txtCatName" type="text" style="width: 400px;" class="edTextBox" />
        </div>
        <div class="marg5">
            <label class="wid150">Active</label>
            <input type="checkbox" id="chkCat" name="chkCat" style="zoom: 1.5;" />
        </div>

        <div class="marg5" style="display: none;">
            <label class="wid150">Component</label><select id="ddlComp" style="width: 400px;"></select>
        </div>

        <div>
            <input id="txtCatID" style="display: none;" type="text" />
            <input id="txtCompID" style="display: none;" type="text" />
            &nbsp<input type="button" id="btnAddCat" style="display: none;" value="ADD" class="CA_btnSearch" onclick="CA_AddCat('0');" />
            &nbsp<input type="button" id="btnEditCat" style="display: none;" value="UPDATE" class="CA_btnSearch" onclick="CA_AddCat('1');" />

        </div>
    </div>


</div>

<div id="dvTaxonomy" style="display: none;">

    <label style="margin-left: 20px; margin-top: 20px;">Manage Taxonomy Terms</label>
    <div style="width: 100%; height: 50px; margin-top: 20px; margin-left: 20px; border-top: solid 1px #808080; padding-top: 15px;"></div>

    <div id="dvTermPagination" style="margin: 5px; margin-left: 20px;"></div>
    <div id="divTermList" style="margin-left: 20px;"></div>
    <br />
    <div style="margin-left: 20px;"><a href="javascript:void(0);" id="lnkAddTerm" onclick="CA_ShowTermAdd(true);return false;" class="addbtn">Add Term</a></div>

    <%--<a href="javascript:void(0);" id="lnkAddRTag" onclick="CA_ShowAddRTag();return false;"  class="addbtn"  >Add Related tags</a>--%>


    <div id="divAddTerm" style="display: none;" class="pad10">
        <div class="marg5">
            <label class="wid175">Term Name</label>
            <input id="txtTerm" type="text" style="width: 450px;" class="edTextBox" />
        </div>
        <div class="marg5">
            <label class="wid175">Parent Term Name</label>
            <input id="txtPTerm" type="text" style="width: 450px;" class="edTextBox" />
        </div>
        <div>
            &nbsp<input type="button" id="btnAddTerm" value="ADD" class="CA_btnSearch" onclick="CA_AddTerm();" />
        </div>
    </div>




</div>



<div id="dvMainUser">
    <label style="margin-left: 20px; margin-top: 20px;">Manage User Components</label>
    <div style="width: 100%; height: 50px; margin-top: 20px; margin-left: 20px; border-top: solid 1px #808080; padding-top: 15px;">
        <label>Users:</label>&nbsp;<select id="ddlUsers"> </select>
    </div>
    <div style="width: 100%; margin-top: 10px; margin-bottom: 10px; margin-left: 20px;">
        <label id="lblCurrentUser" style="font-weight: normal;">Please select user from the above list to assign component(s)</label>
    </div>
    <div id="dvCategories" style="width: 100%; display: none; margin: 20px;">
        <label>Category:</label>&nbsp;<select id="ddlSSCats" style="width: 450px;"></select>
    </div>
    <div id="CA_ManageTabs" style="display: none;">
        <ul>
            <li><a href="#dvManageForum">Forum</a></li>
            <li><a href="#dvManageBlogs">Blog</a></li>
            <li><a href="#dvManageSolveSpaces">Solve Spaces</a></li>
        </ul>
        <div id="dvManageForum" class="CA_UserTabs">
            <div id="dvFSearch" style="width: 100%; margin-bottom: 20px;">
                <input type="text" id="txtFSearch" class="CA_txtSearch" placeholder="Search forums by Title & Description" />
                &nbsp<input type="button" id="btnFSearch" value="Search" class="CA_btnSearch" onclick="CA_SearchComponent('F')" />
                <br />
                <a href="#" id="btnFClearSearch" onclick="CA_ClearSearchComponent('F');return false;" class="CA_ClearSearch">Clear Search</a>
            </div>
            <div id="dvFPagination" style="margin: 20px;"></div>
            <div id="dvAssignForums" style="width: 100%; margin-top: 20px; min-height: 200px; height: auto; border-bottom: dashed 1px #808080; margin-bottom: 10px;">
            </div>
            <div id="dvForumButtons" style="width: 100%; height: 30px">
                <input type="button" id="btnUpdateForum" value="Update Forums Assignment" class="CA_Buttons" />
            </div>
        </div>
        <div id="dvManageBlogs" class="CA_UserTabs">
            <div id="dvBSearch" style="width: 100%; margin-bottom: 20px;">
                <input type="text" id="txtBSearch" class="CA_txtSearch" placeholder="Search blogs by Title & Summary" />
                &nbsp<input type="button" id="btnBSearch" value="Search" class="CA_btnSearch" onclick="CA_SearchComponent('B')" />
                <br />
                <a href="#" id="btnBClearSearch" onclick="CA_ClearSearchComponent('B');return false;" class="CA_ClearSearch">Clear Search</a>
            </div>
            <div id="dvIPagination" style="margin: 20px;"></div>
            <div id="dvAssignBlogs" style="width: 100%; margin-top: 20px; min-height: 200px; height: auto; border-bottom: dashed 1px #808080; margin-bottom: 10px;">
            </div>
            <div id="dvBlogsButtons" style="width: 100%; height: 30px">
                <input type="button" id="btnUpdateBlogs" value="Update Blogs Assignment" class="CA_Buttons" />
            </div>
        </div>
        <div id="dvManageSolveSpaces" class="CA_UserTabs" style="">
            <div id="dvSSearch" style="width: 100%; margin-bottom: 20px; padding: 4px;">
                <input type="text" id="txtSSearch" class="CA_txtSearch" placeholder="Search solve-spaces by Title & Description" />
                &nbsp<input type="button" id="btnSSearch" value="Search" class="CA_btnSearch" onclick="CA_SearchComponent('S')" />
                <br />
                <a href="#" id="btnSClearSearch" onclick="CA_ClearSearchComponent('S');return false;" class="CA_ClearSearch">Clear Search</a>
            </div>
            <div id="dvSPagination" style="margin: 20px;"></div>
            <div id="dvAssignSolveSpaces" style="width: 100%; margin-top: 20px; min-height: 200px; height: auto; border-bottom: dashed 1px #808080; margin-bottom: 10px;">
            </div>
            <div id="dvSSButtons" style="width: 100%; height: 30px">
                <input type="button" id="btnUpdateSolveSpaces" value="Update Solve Spaces Assignment" class="CA_Buttons" />
            </div>
        </div>
        <div id="hiddenresult" style="display: none;"></div>
    </div>


</div>
<script type="text/javascript">
    CA_PageSize = <%=this.PageSize%>;
    CA_ForumPageSize =<%=this.FPageSize%>;
    CA_BlogPageSize =<%=this.BPageSize%>;
    CA_TPageSize =<%=this.TPageSize%>;
    CA_CPageSize =<%=this.CatPageSize%>;
    CA_logUserID =<%=this.UserId%>;

</script>
<script src="/DesktopModules/ClearAction_ComponentManager/Scripts/pagination/jquery.pagination.js"></script>
<link href="/DesktopModules/ClearAction_ComponentManager/Scripts/pagination/pagination.css" rel="stylesheet" />
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/jquery-jtemplates/jquery-jtemplates.js"></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/Module/Common.js"></script>
<script src="/DesktopModules/ClearAction_ComponentManager/Scripts/Module.js"></script>

