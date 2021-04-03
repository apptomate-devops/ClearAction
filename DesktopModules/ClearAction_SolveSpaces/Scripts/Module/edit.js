var CA_CatID = -1; var eddialog;
$(document).ready(function () {
// Load Global Categories
    CA_BindCategories();
    $("#lstCategory").on('change', function () {
        CA_LoadSolveSpacesList(this.value);
    });

    eddialog = $("#dvEdit_NewSSForm").dialog({
        autoOpen: false,
        height: 500,
        width: '70%',
        modal: true,
        open: function (event, ui) {
            $("#dvEdit_NewSSForm .ddlCategories").html(_options);
        },
        buttons: [{
            text: "Create",
            "class": 'edPrimary',
            click: function() {
                CA_CreateNewSolveSpace();
            }
        },{
            text: "Close",
            "class": 'edButton',
            click: function() {
                eddialog.dialog("close");
            }
        }],
        close: function () {
           // form[0].reset();
           // allFields.removeClass("ui-state-error");
        }
    });
    
});
function ReloadList() {
    CA_LoadSolveSpacesList($("#lstCategory").val());
}
function OpenEditDialog() {
    eddialog.dialog("open");
    return false;
}

function CA_BindCategories() {
    var _URL = "/DesktopModules/ClearAction_SolveSpaces/requesthandler.asmx/ListCategories";
    CA_MakeRequest(_URL, "", CA_OnBindCategories);
}
var _options = "<option disabled>------------------------ List Of Categories ------------------------</option>";
function CA_OnBindCategories(d) {
    var MainOptions = "<option value='-1'>--- All ---</option>";
    for (var i = 0; i < d.length; i++) {
        _options += "<option value='" + d[i].CategoryId + "'>" + d[i].CategoryName + "</option>";
        MainOptions += "<option value='" + d[i].CategoryId + "'>" + d[i].CategoryName + "</option>";
    }
    $("#lstCategory").html(MainOptions);
    // Load list of Solve Space
    CA_LoadSolveSpacesList(-1);
}
/* Solve Spaces related methods */
function CA_LoadSolveSpacesList(CatID) {
    var _URL = "/DesktopModules/ClearAction_SolveSpaces/requesthandler.asmx/ListSolveSpacesEdit";
    var _data = "{'UserID':'-1','CatID':'" + CatID + "','pOption':'All','SearchTerm':'','LoggedInUser':'-1','IncludeDeleted':'1'}";
    CA_MakeRequest(_URL, _data, CA_OnLoadSolveSpacesList);
}

function CA_OnLoadSolveSpacesList(d) {
    if (d.length > 0) {
        $("#dvEdit_SolveSpaceList").setTemplateURL('/DesktopModules/ClearAction_SolveSpaces//templates/tmpEditSolveSpaces.htm?q='+$.now());
        $("#dvEdit_SolveSpaceList").show().processTemplate(d);
        $(".ddlCategories").html(_options);
        $.each(d, function (o, i) {
            $.each(i.MyCategories, function (a, b) {
                $("#ddlNewCategory_" + i.SolveSpaceID + " option[value='" + b.CategoryId + "']").prop("selected", true)
            });
        });
        //$("#dvEdit_SolveSpaceListContainer .ddlCategories").each(function (i, v) {
        //    //$(v +" option[value'"+).val($(v).attr('CatID'));
        //});
    }
    else {
        $("#dvEdit_SolveSpaceList").html("<div style='margin-left:40px;'><label style='color:red;font-weight:normal'>Sorry, No solve-space(s) found</label></div>");
    }
}

// Create New Solve Space
function CA_CreateNewSolveSpace() {
    var Title = $.trim($("#txtSolveSpaceTitle").val());
    var ShortDesc = $.trim($("#txtSSDescription").val());
    var Duration = $.trim($("#txtSSDuration").val());
    var Steps = $.trim($("#txtSSSteps").val());
    if (Title == '') {
        alert('Please provide a title for the solve space');
        $("#txtSolveSpaceTitle").focus();
        return false;
    }
    if (ShortDesc == ''){
        alert('Please provide some short description for the solve space');
        $("#txtSSDescription").focus();
        return false;
    }
    if (Duration == '') {
        alert('Please provide duration for the solve space');
        $("#txtSSDuration").focus();
        return false;
    }
    if (Steps == '') {
        alert('Please provide steps for the solve space');
        $("#txtSSSteps").focus();
        return false;
    }

    var CatIDs = $("#ddlNewCategory").val().toString();
    CatIDs = CatIDs.replace(/,/g, '|');
    var TabLink = $("#txtSolveSpaceTabLink").val();
    var _URL = "/DesktopModules/ClearAction_SolveSpaces/requesthandler.asmx/CreateNewSolveSpace";
    var _data = "{'Title':'" + escape(Title) + "','ShortDescription':'" + escape(ShortDesc) + "','CatIDs':'" + CatIDs + "','DurationInMin':'" + Duration + "','TotalSteps':'" + Steps + "','TabLink':'" + escape(TabLink) + "'}";
    CA_MakeRequest(_URL, _data, CA_OnCreateNewSolveSpace);
}

function CA_OnCreateNewSolveSpace(d) {
    if (d > 0) {
        ReloadList();
        alert('Created Successfully');
    } else {
        alert('Unable to create solve-space cause of some')
    }
}

function CA_RemoveSolveSpace(ID) {
    if (confirm("Are you sure to remove this Solve-Space detail???")==true) {
        var _URL = "/DesktopModules/ClearAction_SolveSpaces/requesthandler.asmx/RemoveSolveSpace";
        var _data = "{'ID':'" + ID + "'}";
        CA_MakeRequest(_URL, _data, CA_OnRemoveSolveSpace);
    }
}

function CA_OnRemoveSolveSpace(d) {
    ReloadList();
    alert('Removed successfully');
}

// Update Solve Space
function CA_UpdateSolveSpace(ID) {
    var Title = $.trim($("#txtSolveSpaceTitle_"+ID).val());
    var ShortDesc = $.trim($("#txtSSDescription_"+ID).val());
    var Duration = $.trim($("#txtSSDuration_"+ID).val());
    var Steps = $.trim($("#txtSSSteps_"+ID).val());
    if (Title == '') {
        alert('Please provide a title for the solve space');
        $("#txtSolveSpaceTitle_"+ID).focus();
        return false;
    }
    if (ShortDesc == '') {
        alert('Please provide some short description for the solve space');
        $("#txtSSDescription_"+ID).focus();
        return false;
    }
    if (Duration == '') {
        alert('Please provide duration for the solve space');
        $("#txtSSDuration_"+ID).focus();
        return false;
    }
    if (Steps == '') {
        alert('Please provide steps for the solve space');
        $("#txtSSSteps").focus();
        return false;
    }

    var CatIDs = $("#ddlNewCategory_" + ID).val().toString();
    CatIDs = CatIDs.replace(/,/g, '|');

    var TabLink = $("#txtSolveSpaceTabLink_"+ID).val();
    if (TabLink == undefined) TabLink = '';
    var _URL = "/DesktopModules/ClearAction_SolveSpaces/requesthandler.asmx/UpdateSolveSpace";
    var _data = "{'ID':'" + ID + "','Title':'" + escape(Title) + "','ShortDescription':'" + escape(ShortDesc) + "','CatIDs':'" + CatIDs + "','Duration':'" + Duration + "','Steps':'" + Steps + "','TabLink':'" + escape(TabLink) + "'}";
    CA_MakeRequest(_URL, _data, CA_OnUpdateSolveSpace);
}
function CA_OnUpdateSolveSpace() {
    ReloadList();
    alert('Updated successfully');
}

function CA_UnDeleteSolveSpace(ID) {
    if (confirm("Are you sure to Un-Delete this Solve-Space???") == true) {
        var _URL = "/DesktopModules/ClearAction_SolveSpaces/requesthandler.asmx/UnDeleteSolveSpace";
        var _data = "{'ID':'" + ID + "'}";
        CA_MakeRequest(_URL, _data, CA_OnUpdateSolveSpace);
    }
}