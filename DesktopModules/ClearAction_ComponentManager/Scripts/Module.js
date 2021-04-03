var CA_UserID = -1;
var CA_CP = 1;
var CA_ActiveTabID = 0;
var CA_IsForumEdited = false;
var CA_IsBlogEdited = false;
var CA_IsSSEdited = false;
var CA_CompanyPageSize = 20;
$(document).ready(function () {
    $("#CA_ManageTabs").tabs({
        heightStyle: "content",
        active: 0,
        activate: function (event, ui) {
            $(".CA_ClearSearch").hide();
            CA_OnTabChange();
        },
        beforeActivate: function (event, ui) {
            if ((CA_IsSSEdited) || (CA_IsForumEdited) || (CA_IsBlogEdited)) {
                if (confirm('You will lose your selections, would you like to continue?') == false) {
                    return false;
                }
                else {
                    CA_IsSSEdited = CA_IsForumEdited = CA_IsBlogEdited = false;
                }
            }

        }
    });

    CA_LoadUsers();

    $("#lnkUser").focus();

    //on change of solve space categories 
    $("#ddlSSCats").on("change", function () {
        CA_OnTabChange();
    });

    $("#ddlUsers").on("change", function () {
        CA_CP = 1;
        CA_UserID = this.value;
        $("#lblCurrentUser").text('Assign component(s) for: ' + this.options[this.selectedIndex].innerHTML);
        $("#CA_ManageTabs").show();
        $("#dvCategories").show();
        CA_UpdateTabContent();
    });

    $("#btnUpdateSolveSpaces").on("click", function () {
        CA_UpdateSolveSpaces();
    })
    $("#btnUpdateBlogs").on("click", function () {
        CA_UpdateBlogs();
    });
    $("#btnUpdateForum").on("click", function () {
        CA_UpdateForum();
    });

    $(".CA_txtSearch").on("keypress", function (e) {
        if (e.keyCode === 13) {
            e.preventDefault(); // Ensure it is only this code that rusn
            var CKey = $(this).attr('id').substring(3, 4);
            CA_SearchComponent(CKey);
        }
    });
})

function CA_OnTabChange() {
    CA_ActiveTabID = $("#CA_ManageTabs").tabs("option", "active");
    CA_UpdateTabContent();
}
function CA_BindCategories() {
    var _URL = "/DesktopModules/ClearAction_SolveSpaces/requesthandler.asmx/ListCategories";
    CA_MakeRequest(_URL, "", CA_OnBindCategories);
}

function CA_OnBindCategories(d) {
    var _Options = '<option value="-1">-- All -- </option>';
    for (var i = 0; i < d.length; i++) {
        _Options += "<option value='" + d[i].CategoryId + "'>" + d[i].CategoryName + "</option>";
    }
    $("#ddlSSCats").html(_Options);
    // Load list of Solve Space
    CA_LoadSolveSpacesList(-1);
}

var CatID = -1;
var LastPI = -1;
/* Solve Spaces related methods */
function CA_LoadSolveSpacesList(PI, SearchKey) {
    if (PI <= 0) return;
    if (LastPI == PI) return;
    LastPI = PI;
    if (SearchKey == undefined) SearchKey = '';
    $("#dvAssignSolveSpaces").text('Please wait while we are retrieving data...');
    var UserID = $("#ddlUsers").val();
    if ($("#ddlSSCats").val() != undefined) { CatID = $("#ddlSSCats").val(); }
    //int UserId,int CatID
    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/ListSolveSpacesWithStatus";
    var _data = "{'UserId':'" + UserID + "','CatID':'" + CatID + "','PI':'" + PI + "','PS':'" + CA_PageSize + "','SearchKey':'" + SearchKey + "'}";
    CA_MakeRequest(_URL, _data, function (d) {
        if (d.length > 0) {
            $("#dvAssignSolveSpaces").setTemplateURL('/DesktopModules/ClearAction_ComponentManager/Templates/tmpSolveSpaces.htm?q=' + $.now());
            $("#dvAssignSolveSpaces").show().processTemplate(d);
            if (PI == 1) {
                CA_InitSSPagination(d);
            }
        }
        else {
            $("#dvAssignSolveSpaces").text('Nothing found');
        }
    });
}

function CA_LoadUsers() {
    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/ListUsers";
    var _data = "";
    CA_MakeRequest(_URL, _data, CA_OnLoadUsers);
}
function CA_OnLoadUsers(d) {
    var _options = "<option id='-1' disabled selected>-- Select User --</option>";
    for (var i = 0; i < d.length; i++) {
        _options += "<option value='" + d[i].UserID + "'>" + d[i].FirstName + ' ' + d[i].LastName + ' (' + d[i].UserName + ")</option>";
    }
    $("#ddlUsers").html(_options);
    CA_BindCategories();
}

function CA_UpdateSolveSpaces() {
    var IDs = "";
    var selCheckBoxes = $(".SSCheckBoxes:checked");
    for (var i = 0; i < selCheckBoxes.length; i++) {
        var id = $(selCheckBoxes[i]).val();
        IDs += id + "|";
    }

    var unSelCheckBoxes = $(".SSCheckBoxes:not(:checked)");
    var unCheckedIDs = "";
    for (var i = 0; i < unSelCheckBoxes.length; i++) {
        var id = $(unSelCheckBoxes[i]).val();
        unCheckedIDs += id + "|";
    }
    CA_AssignItem2User(3, IDs, unCheckedIDs);
}

function CA_UpdateBlogs() {
    var IDs = "";
    var selCheckBoxes = $(".BlogsCheckBoxes:checked");
    for (var i = 0; i < selCheckBoxes.length; i++) {
        var id = $(selCheckBoxes[i]).val();
        IDs += id + "|";
    }

    var unSelCheckBoxes = $(".BlogsCheckBoxes:not(:checked)");
    var unCheckedIDs = "";
    for (var i = 0; i < unSelCheckBoxes.length; i++) {
        var id = $(unSelCheckBoxes[i]).val();
        unCheckedIDs += id + "|";
    }
    CA_AssignItem2User(2, IDs, unCheckedIDs);
}

function CA_UpdateForum() {
    var IDs = "";
    var selCheckBoxes = $(".ForumCheckBoxes:checked");
    for (var i = 0; i < selCheckBoxes.length; i++) {
        var id = $(selCheckBoxes[i]).val();
        IDs += id + "|";
    }
    var unSelCheckBoxes = $(".ForumCheckBoxes:not(:checked)");
    var unCheckedIDs = "";
    for (var i = 0; i < unSelCheckBoxes.length; i++) {
        var id = $(unSelCheckBoxes[i]).val();
        unCheckedIDs += id + "|";
    }
    CA_AssignItem2User(1, IDs, unCheckedIDs);
}

function CA_OnComponentUpdated() {
    CA_IsSSEdited = CA_IsForumEdited = CA_IsBlogEdited = false;
    alert('Updated Successfully');
}

function CA_AssignItem2User(ComponentID, csvItemID, unCheckedIDs) {
    // Expected ComponentID : 1-Forum, 2-Blog, 3-SolveSpace
    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/AssignItemToUser";
    var _data = "{'CID':'" + ComponentID + "','csvItemID':'" + csvItemID + "','UID':'" + CA_UserID + "','csvUnchecked':'" + unCheckedIDs + "'}";
    CA_MakeRequest(_URL, _data, CA_OnComponentUpdated);
}

function CA_UpdateTabContent() {
    CA_CP = 1;
    if (CA_ActiveTabID == 0) {// Forum Tab is active
        // do something when forum tab is activated
        CA_LoadForumList(1);
    }
    if (CA_ActiveTabID == 1) {// Blog Tab is active
        // do something when Blog tab is activated
        CA_LoadBlogList(1);
    }
    if (CA_ActiveTabID == 2) {// Solve Space Tab is active
        CA_LoadSolveSpacesList(1);
    }
}

var BLastPI = -1;
function CA_LoadBlogList(PI, SearchKey) {
    if (PI <= 0) return;
    if (BLastPI == PI) return;
    BLastPI = PI;
    if (SearchKey == undefined) SearchKey = '';
    var UserID = $("#ddlUsers").val();
    if ($("#ddlSSCats").val() != undefined) { CatID = $("#ddlSSCats").val(); }
    //int UserId,int CatID
    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/ListBlogsWithStatus";
    var _data = "{'UserID':'" + UserID + "','CatID':'" + CatID + "','PI':'" + PI + "','PS':'" + CA_BlogPageSize + "','SearchKey':'" + SearchKey + "'}";
    CA_MakeRequest(_URL, _data, function (d) {
        if (d.length > 0) {
            $("#dvAssignBlogs").setTemplateURL('/DesktopModules/ClearAction_ComponentManager/Templates/tmpBlogs.htm?q=' + $.now());
            $("#dvAssignBlogs").show().processTemplate(d);
            if (PI == 1) CA_InitBPagination(d);
        }
        else {
            $("#dvAssignBlogs").html("<div style='margin-left:40px;'><label style='color:red;font-weight:normal'>No blog(s) found</label></div>");
        }
    });
}
var FLastPI = -1;
function CA_LoadForumList(PI, SearchKey) {
    if (PI <= 0) return;
    if (FLastPI == PI) return;
    FLastPI = PI;
    if (SearchKey == undefined) SearchKey = '';
    var UserID = $("#ddlUsers").val();
    if ($("#ddlSSCats").val() != undefined) { CatID = $("#ddlSSCats").val(); }
    //int UserId,int CatID
    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/ListForumsWithStatus";
    var _data = "{'UserID':'" + UserID + "','CatID':'" + CatID + "','PI':'" + PI + "','PS':'" + CA_ForumPageSize + "','SearchKey':'" + SearchKey + "'}";
    CA_MakeRequest(_URL, _data, function (d) {
        if (d.length > 0) {
            $("#dvAssignForums").setTemplateURL('/DesktopModules/ClearAction_ComponentManager/Templates/tmpForums.htm?q=' + $.now());
            $("#dvAssignForums").show().processTemplate(d);
            if (PI == 1) CA_InitFPagination(d);
        }
        else {
              // Removed (s) from topic(s) by @SP
            //$("#dvAssignForums").html("<div style='margin-left:40px;'><label style='color:red;font-weight:normal'>No forum topic(s) found</label></div>");
            $("#dvAssignForums").html("<div style='margin-left:40px;'><label style='color:red;font-weight:normal'>No forum topic found</label></div>");
        }
    });
}

function CA_InitSSPagination(d) {
    $("#dvSPagination").pagination(d[0].TotalRecords, {
        callback: function (NewPageIndex) { CA_LoadSolveSpacesList(NewPageIndex + 1); },
        next_text: '&nbsp;>>',
        prev_text: "<<&nbsp;",
        next_show_always: false,
        prev_show_always: false,
        num_edge_entries: 1,
        num_display_entries: 5,
        items_per_page: CA_PageSize
    });
}

function CA_InitFPagination(d) {// Forum Pagination
    $("#dvFPagination").pagination(d[0].TotalRecords, {
        callback: function (NewPageIndex) { CA_LoadForumList(NewPageIndex + 1); },
        next_text: '&nbsp;>>',
        prev_text: "<<&nbsp;",
        next_show_always: false,
        prev_show_always: false,
        num_edge_entries: 1,
        num_display_entries: 5,
        items_per_page: CA_ForumPageSize
    });
}

function CA_InitBPagination(d) {// Blog Pagination
    $("#dvIPagination").pagination(d[0].TotalRecords, {
        callback: function (NewPageIndex) {
            CA_LoadBlogList(NewPageIndex + 1);
        },
        next_text: '&nbsp;>>',
        prev_text: "<<&nbsp;",
        next_show_always: false,
        prev_show_always: false,
        num_edge_entries: 1,
        num_display_entries: 5,
        items_per_page: CA_BlogPageSize
    });
}
function CA_OnItemClicked(For) {
    if (For == 'SS') CA_IsSSEdited = true;
    if (For == 'B') CA_IsBlogEdited = true;
    if (For == 'F') CA_IsForumEdited = true;
}

function CA_SearchComponent(CKey) {
    var txt = $("#txt" + CKey + "Search").val();

    if ($.trim(txt) == '') return false;
    FLastPI = -1;
    LastPI = -1;
    BLastPI = -1;
    if (CA_ActiveTabID == 0) {// Forum Tab is active
        CA_LoadForumList(1, txt);
    }
    if (CA_ActiveTabID == 1) {// Blog Tab is active
        CA_LoadBlogList(1, txt);
    }
    if (CA_ActiveTabID == 2) {// Solve Space Tab is active
        CA_LoadSolveSpacesList(1, txt);
    }
    $("#btn" + CKey + "ClearSearch").show();
}
function CA_ClearSearchComponent(CKey) {
    $("#txt" + CKey + "Search").val('');
    FLastPI = -1;
    LastPI = -1;
    BLastPI = -1;
    if (CA_ActiveTabID == 0) {// Forum Tab is active
        CA_LoadForumList(1, '');
    }
    if (CA_ActiveTabID == 1) {// Blog Tab is active
        CA_LoadBlogList(1, '');
    }
    if (CA_ActiveTabID == 2) {// Solve Space Tab is active
        CA_LoadSolveSpacesList(1, '');
    }
    $("#btn" + CKey + "ClearSearch").hide();
}


/* Added by Kusum            */



function CA_ShowSection(sname) {
    $("#dvGlobalCompany").hide();
    $("#dvManageCategory").hide();
    $("#dvTaxonomy").hide();
    $("#dvRTags").hide();
    $("#dvMainUser").hide();

    if (sname == "U") {
        $("#dvMainUser").show();


    }
    else if (sname == "G") {

        $("#dvManageCategory").show();


        CLastPI = -1;
        CA_LoadGlobalCategory(1);
        fnResetAdd();
        //CA_BindComponenMaster();
    }
    else if (sname == "T") {

        $("#dvTaxonomy").show();

        TLastPI = -1;
        CA_LoadTerms(1);
    }
    else if (sname == "R") {
        $("#dvRTags").show();

        CA_BindParentTags();
    }

    else if (sname == "C") {

        $("#dvGlobalCompany").show();

        CA_LoadCompany(1);
    }

}

function CA_ShowUserComp() {
    $("#dvManageCategory").hide();
    $("#dvMainUser").show();

}

function fnResetAdd() {
    $("#txtCatID").val('0');
    $("#txtCatName").val('');
    $("#divAddCat").hide();

}


function CA_InitCatPagination(d) {
    $("#dvCatPagination").pagination(d[0].TotalRecords, {
        callback: function (NewPageIndex) { CA_LoadGlobalCategory(NewPageIndex + 1); },
        next_text: '&nbsp;>>',
        prev_text: "<<&nbsp;",
        next_show_always: false,
        prev_show_always: false,
        num_edge_entries: 1,
        num_display_entries: 5,
        items_per_page: CA_CPageSize
    });
}

function CA_DeleteGlobalCategory(catid) {
    if (confirm('Are you sure, you want to delete this?')) {
        var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/DeleteCategory";
        var _data = "{'CID':'" + catid + "'}";
        CA_MakeRequest(_URL, _data, CA_OnDeleteCategory, CA_OnDeleteCategory);
    }

}

var CLastPI = -1;
function CA_LoadGlobalCategory(PI) {

    $("#lnkGlobCat").focus();
    if (PI <= 0) return;
    if (CLastPI == PI) return;
    CLastPI = PI;
    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/ListAllCategories";
    var _data = "{'PI':'" + PI + "','PS':'" + CA_CPageSize + "'}";
    CA_MakeRequest(_URL, _data, function (d) {
        if (d.length > 0) {
            $("#dvAllCat").setTemplateURL('/DesktopModules/ClearAction_ComponentManager/Templates/tmpGlobCat.htm?q=' + $.now());
            $("#dvAllCat").show().processTemplate(d);
            if (PI == 1) {
                CA_InitCatPagination(d);
            }
        }
        else {
            $("#dvAllCat").text('No Global Category found.');
        }
    });
}





/*
- Company Related Work : Anuj
Dated : 12th Jan
*/

function GoForDeActivateRecord(companyid) {
    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/DeleteCompany";
    var _data = "{'CID':'" + companyid + "'}";
    CA_MakeRequest(_URL, _data, CA_OnDeleteCompany, CA_OnDeleteCompany);
}
function CA_DeleteCompany(companyid, compname) {


    $.confirm({
        title: 'WARNING!',
        content: 'Are you sure, you want to delete <b>' + compname + '</b> ?',
        theme: 'modern',
        buttons: {


            confirm:
            {
                text: 'Yes',
                btnClass: 'btn-red',

                action: function () {
                    GoForDeActivateRecord(companyid);
                }

            }
            ,

            cancel: {
                text: 'No'



            }

        }
    });

}

function CA_InitCompanyPagination(d) {
    debugger;
    $("#dvComPagination").pagination(d[0].TotalRecords, {
        callback: function (NewPageIndex) { CA_LoadCompany(NewPageIndex + 1); },
        next_text: '&nbsp;>>',
        prev_text: "<<&nbsp;",
        next_show_always: false,
        prev_show_always: false,
        num_edge_entries: 1,
        num_display_entries: 5,
        items_per_page: CA_CompanyPageSize
    });
}
function CA_OnDeleteCompany(d) {
    CoLastPI = -1;
    CA_LoadCompany(1);
}


var CoLastPI = -1;
function CA_LoadCompany(PI) {
    debugger;
    $("#lnkGlobalCompany").focus();
    if (PI <= 0) return;
    if (CoLastPI == PI) return;
    CoLastPI = PI;
    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/ListCompany";
    var _data = "{'PI':'" + PI + "','PS':'" + CA_CompanyPageSize + "'}";
    CA_MakeRequest(_URL, _data, function (d) {
        if (d.length > 0) {
            $("#dvAllCompany").setTemplateURL('/DesktopModules/ClearAction_ComponentManager/Templates/tmpGlobComp.htm?q=' + $.now());
            $("#dvAllCompany").show().processTemplate(d);
            if (PI == 1) {
                CA_InitCompanyPagination(d);
            }
        }
        else {
            $("#dvAllCompany").text('No Global Company found.');
        }
    });
}


function CA_ResetCompany() {

    $("#btnAddCat").show();
    $("#btnEditCat").hide();
    $("#txtCatName").val('');
    $("#chkComp").prop('checked', false);
    $("#txtCompName").val('');
    $('#divAddComp').hide();
    $("#dvAllCompany").show();

}
function CA_ShowAddCompany(isAdd, catID, compID, catname, isactive) {

    $("#divAddComp").show();


    if (isAdd) {

        $("#btnAddComp").show();
        $("#btnEditComp").hide();
        $("#txtCompRecordID").val('');
        $("#chkCompCat").prop('checked', false);
        $("#txtCompRecordID").val('0');
        $("#txtCompName").focus();
    }
    else {
        $("#btnAddComp").hide();
        $("#btnEditComp").show();
        $("#txtCompRecordID").val(catID);
        $("#txtCompName").val(catname);
        // $("#ddlComp").val(compID);

        if (isactive == 'true') {

            $("#chkComp").prop('checked', true);
        }
        else {
            $("#chkComp").prop('checked', false);
        }

    }

}

function CA_OnAddCompany(d) {

    if (d == 'fail') {
        AlertWindow('Warning', 'Company already exits in database. <br/><center><b>Save discarded</b></center>');
        $('#txtCompName').focus();
        return;
    }
    SuccessWindow("Information", "Company information has been Saved successfully!!!");

    CA_ResetCompany();
    CoLastPI = -1;
    CA_LoadCompany(1);



}

function CA_OnAddFailureCompany(d) {
    SuccessWindow("Information", "Company not added successuflly");

    fnResetCompany();
    CLastPI = -1;
    CA_LoadCompany(1);



}


function CA_AddCompany(CompanyId) {

    var nCompanyID = '0';
    var compname = '';
    var isactive = 'false';

    if (CompanyId == '1') {
        nCompanyID = $("#txtCompRecordID").val();
    }

    compname = $("#txtCompName").val();
    var chkVal = $("#chkComp").is(':checked');


    if (jQuery.trim(compname).length > 1) {
        var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/AddCompany";
        var _data = "{'CompanyID':'" + nCompanyID + "','CompanyName':'" + compname + "','IsChecked':'" + chkVal + "'}";
        CA_MakeRequest(_URL, _data, CA_OnAddCompany, CA_OnAddCompany);
    }
    else {
        AlertWindow('Alert', 'Please enter Company name.');
        $("#txtCompName").focus();
        return false;
    }

}



function AlertWindow(head, content) {
    $.alert({
        title: head,
        type: 'red',
        content: content
    });
}
function AlertWindowNoTitle(content) {

    AlertWindow('Alert', content);
}
function SuccessWindow(head, content) {
    $.alert({
        title: head,
        type: 'blue',
        content: content
    });
}



/*

Company Related Work End Here
//Auhtor : Anuj(Ajit)
 */


function CA_ShowAdd(isAdd, catID, compID, catname, isactive) {

    $("#divAddCat").show();


    if (isAdd) {
        $("#btnAddCat").show();
        $("#btnEditCat").hide();
        $("#txtCatName").val('');
        $("#chkCat").prop('checked', false);
        $("#txtCatID").val('0');
    }
    else {
        $("#btnAddCat").hide();
        $("#btnEditCat").show();
        $("#txtCatID").val(catID);
        $("#txtCatName").val(catname);
        // $("#ddlComp").val(compID);

        if (isactive == 'true') {

            $("#chkCat").prop('checked', true);
        }
        else {
            $("#chkCat").prop('checked', false);
        }

    }

}


function CA_AddCat(catid) {

    var nCatID = '0';
    var catname = '';
    var isactive = 'false';

    if (catid == '1') {
        nCatID = $("#txtCatID").val();
    }

    catname = $("#txtCatName").val();
    var chkVal = $("#chkCat").is(':checked');
    //var compID = $("#ddlComp").val();
    //Setting -1 to always add/update global category only
    var compID = '-1';

    if (jQuery.trim(catname).length > 1) {
        var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/UpdateCategory";
        var _data = "{'CID':'" + nCatID + "','name':'" + catname + "','isActive':'" + chkVal + "','CompID':'-1'}";
        CA_MakeRequest(_URL, _data, CA_OnAddCategory, CA_OnAddCategory);
    }
    else {
        AlertWindow('Alert', 'Please enter category name.');
        $("#txtCatName").focus();
        return false;
    }

}

function CA_OnAddCategory(d) {
    SuccessWindow("Information", "Saved successfully!!!");

    fnResetAdd();
    CLastPI = -1;
    CA_LoadGlobalCategory(1);



}

/*
function CA_BindComponenMaster() {
    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/ListComponentMaster";
    CA_MakeRequest(_URL, "", CA_OnBindComponenMaster, CA_OnBindComponenMaster);
}

function CA_OnBindComponenMaster(d) {

    var _Options = '<option value="-1">-- Global Category -- </option>';
    for (var i = 0; i < d.length; i++) {
        _Options += "<option value='" + d[i].ComponentID + "'>" + d[i].ComponentName + "</option>";
    }
    $("#ddlComp").html(_Options);
}
*/

function CA_InitTermPagination(d) {
    $("#dvTermPagination").pagination(d[0].TotalRecords, {
        callback: function (NewPageIndex) { CA_LoadTerms(NewPageIndex + 1); },
        next_text: '&nbsp;>>',
        prev_text: "<<&nbsp;",
        next_show_always: false,
        prev_show_always: false,
        num_edge_entries: 1,
        num_display_entries: 5,
        items_per_page: CA_TPageSize
    });
}

var TLastPI = -1;
function CA_LoadTerms(PI) {

    $("#lnkTerm").focus();
    if (PI <= 0) return;
    if (TLastPI == PI) return;
    TLastPI = PI;
    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/ListTerms";
    var _data = "{'PI':'" + PI + "','PS':'" + CA_TPageSize + "'}";
    CA_MakeRequest(_URL, _data, function (d) {
        if (d.length > 0) {
            $("#divTermList").setTemplateURL('/DesktopModules/ClearAction_ComponentManager/Templates/tmpTerm.htm?q=' + $.now());
            $("#divTermList").show().processTemplate(d);
            if (PI == 1) {
                CA_InitTermPagination(d);
            }
        }
        else {
            $("#divTermList").text('No terms found.');
        }
    });



}

function CA_ShowTerms(bshow) {
    CA_LoadTerms(1);
    fnResetTerm();
}

function CA_DeleteTerm(id) {

    if (confirm('Are you sure, you want to delete this Term?')) {
        var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/DeleteTerm";
        var _data = "{'TermID':'" + id + "'}";
        CA_MakeRequest(_URL, _data, CA_OnDeleteTerm, CA_OnDeleteTerm);
    }
}

function CA_OnDeleteTerm(d) {
    TLastPI = -1;
    CA_LoadTerms(1);
}

function CA_AddTerm() {
    var term = $("#txtTerm").val();
    var pterm = $("#txtPTerm").val();

    if (jQuery.trim(term).length < 1) {

        alert('Please enter term  name.');
        $("#txtTerm").focus();
        return false;
    }
    if (jQuery.trim(pterm).length < 1) {
        alert('Please enter parent term name.');
        $("#txtPTerm").focus();
        return false;
    }

    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/AddTerm";
    var _data = "{'PTerm':'" + pterm + "','Term':'" + term + "'}";
    CA_MakeRequest(_URL, _data, CA_OnAddTerm, CA_OnAddTerm);

}

function CA_OnAddTerm(d) {
    alert("Saved successfully!!!");
    fnResetTerm();
    TLastPI = -1;
    CA_LoadTerms(1);
}

function fnResetTerm() {
    $("#txtTerm").val('');
    $("#txtPTerm").val('');
}

function CA_ShowTermAdd(isShow) {
    if (isShow) {
        $("#divAddTerm").show();
        fnResetTerm();
    }
    else {
        $("#divAddTerm").hide();
    }
    $("#lnkTerm").focus();
}


function CA_ShowAddRTag() {
    $("#divAddRTag").show();
    CA_BindParentTags();
}


function CA_BindParentTags() {
    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/ListAllTerms";
    CA_MakeRequest(_URL, "", CA_OnBindParentTags);
}

function CA_OnBindParentTags(d) {
    var _Options = '<option value="-1">-- Select --</option>';
    for (var i = 0; i < d.length; i++) {
        _Options += "<option value='" + d[i].TermID + "'>" + d[i].Name + "</option>";
    }
    $("#ddlTags").html(_Options);
    // Load list of Solve Space
}


function CA_AddRTag() {

    var rTag = $("#txtRTag").val();
    var ptermID = $("#ddlTags").val();
    //CA_logUserID



    //var UserID = $("#ddlUsers").val();

    if (jQuery.trim(rTag).length < 1) {

        alert('Please enter related tags.');
        $("#txtRTag").focus();
        return false;
    }
    if (jQuery.trim(ptermID) == "-1") {
        alert('Please select parent tag.');
        $("#ddlTags").focus();
        return false;
    }

    var rtagid = 0;
    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/AddRelatedTags";
    var _data = "{'RelatedTagID':'" + rtagid + "','TermID':'" + ptermID + "','UserID':'" + CA_logUserID + "','Tags':'" + rTag + "'}";
    CA_MakeRequest(_URL, _data, CA_OnAddRelatedTag, CA_OnAddRelatedTag);

}

function CA_OnAddRelatedTag(d) {
    alert("Saved successfully!!!");
    $("#txtRTag").val('');
    $("#ddlTags").val('-1');
}


function CA_RTagChange() {
    var nTermID = $("#ddlTags").val();
    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/GetRelatedTags";
    var _data = "{'TermID':'" + nTermID + "'}";
    CA_MakeRequest(_URL, _data, CA_OnRTagChange, CA_OnRTagChange);
}

function CA_OnRTagChange(d) {

    $("#txtRTag").val('');
    if (d != null) {
        for (var i = 0; i < d.length; i++) {
            $("#txtRTag").val(d[i].Tags);
        }
    }
}

