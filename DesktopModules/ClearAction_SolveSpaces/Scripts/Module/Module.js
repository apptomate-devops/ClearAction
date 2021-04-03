var CA_CatID = -1;
var CA_CurrOption = 'All';
var CA_SearchTerm = '';
var CA_IsAllContent = false;
$(document).ready(function () {
// Load Global Categories
    CA_BindCategories();
// Load list of Solve Space
    CA_LoadSolveSpacesList();
// Attach event to OnChange of SortBy Dropdown
    $("#CA_SS_ddlSortBy").on("change", function () {
        CA_LoadSolveSpacesList();
    });
    $("#CA_SS_txtSearch").on("keydown", function (event) {
        if (event.which == 13) {
            event.preventDefault();
            SearchInSolveSpace();
        }
    });
});

function Set2AllContent(o) {
    $("#lnkAllContent").addClass("active");
    $("#lnkMyContent").removeClass("active");
    CA_IsAllContent = true;
    ReInitializeVar();
    var lnkAll = $("#aAllCategory");
    MakeLinkActive(lnkAll);
    CA_LoadSolveSpacesList();
}
function Set2MyContent(o) {
    $("#lnkAllContent").removeClass("active");
    $("#lnkMyContent").addClass("active");
    CA_IsAllContent = false;
    ReInitializeVar();
    var lnkAll = $("#aAllCategory");
    MakeLinkActive(lnkAll);
    CA_LoadSolveSpacesList();
}
function ReInitializeVar() {
    CA_CatID = -1;
    CA_CurrOption = 'All';
    CA_SearchTerm = '';
}
/* Categories Method */
function ShowForCat(CatID,oCurr) {
    CA_CatID = CatID;
// Update the Category Name.
    $("#hCatName").text($(oCurr).text());
    MakeLinkActive(oCurr);
// Load Solve Space
    CA_LoadSolveSpacesList();
}
function MakeLinkActive(o) {
    // Assign the yellow bottom  bar to currently selected category
    //$(o).parent().parent().parent().parent().children().removeClass('active');
    //$(o).parent().parent().parent().addClass('active');
    $(o).parent().siblings().removeClass("active");
    $(o).parent().addClass("active");
}

function SearchInSolveSpace() {
// No need to do anything when user has not entered
    CA_SearchTerm = $("#CA_SS_txtSearch").val();
    if (CA_SearchTerm == '') return false;
// Search in all the list of solve-spaces
    CA_CurrOption = 'All';
    var lnkAll = $("#aAllCategory");
// Make All link as active
    MakeLinkActive(lnkAll);
    $("#lblClearSearch").show();
// Start Seacrching
    CA_LoadSolveSpacesList();
}
function ClearSearch() {
    ReInitializeVar();
    $("#lblClearSearch").hide();
    CA_LoadSolveSpacesList();
}
function CA_BindCategories() {
    var _URL = "/DesktopModules/ClearAction_SolveSpaces/requesthandler.asmx/ListCategories";
    CA_MakeRequest(_URL, "", CA_OnBindCategories);
}
function CA_OnBindCategories(d) {
    $("#CA_SS_dvCatContainer").setTemplateURL('/DesktopModules/ClearAction_SolveSpaces//templates/tmpCategories.htm?q='+$.now());
    $("#CA_SS_dvCatContainer").show().processTemplate(d);
}
/* Solve Spaces related methods */
function CA_LoadSolveSpacesList(pOption) {
    if (pOption != undefined) { CA_CurrOption = pOption; }
    var _SortOrder = $("#CA_SS_ddlSortBy").val();
    var _URL = "/DesktopModules/ClearAction_SolveSpaces/requesthandler.asmx/ListSolveSpaces";
    var _data = "{'LoggedInUser':'" + (CA_IsAllContent ? CA_UserID : -1) + "','UserID':'" + (CA_IsAllContent ? -1 : CA_UserID) + "','CatID':'" + CA_CatID + "','pOption':'" + CA_CurrOption + "','SearchTerm':'" + CA_SearchTerm + "','SortingOrder':'" + _SortOrder + "'}";
    CA_MakeRequest(_URL, _data, CA_OnLoadSolveSpacesList);
}

function CA_OnLoadSolveSpacesList(d) {
    if (d.length > 0) {
        $("#CA_SS_dvSSContainer").setTemplateURL('/DesktopModules/ClearAction_SolveSpaces//templates/tmpSolveSpaces.htm?q='+$.now());
        $("#CA_SS_dvSSContainer").show().processTemplate(d);
        $.contextMenu({
            selector: '.CA_UserMenu',
            build: function ($trigger,e) {
                var IsSelfAssigned = $trigger.attr('IsSelfAssigned');
                if (IsSelfAssigned=='true') {
                    return {
                        callback: function (key, options) {
                            if (key == "Remove4MyVault") {
                                
                                var Selected_SSID = $(this).attr("id").split("_")[2];
                                if (confirm("Are you sure to remove this item???") == true) {
                                    CA_RemoveFromMyVault(Selected_SSID);
                                }
                            }
                        },
                        items: {
                            "Add2MyVault": { name: "Add to My Vault", disabled: true },
                            "Remove4MyVault": { name: "Remove from My Vault", disabled: false }
                        }
                    };
                }
                else {
                    return {
                        callback: function (key, options) {
                            if (key == "Add2MyVault") {
                                var Selected_SSID = $(this).attr("id").split("_")[2];
                                CA_Assign2Me(Selected_SSID);
                            }
                        },
                        items: {
                            "Add2MyVault": { name: "Add to My Vault",disabled: false },
                             "Remove4MyVault": { name: "Remove from My Vault", disabled: true }
                        }
                    };
                }
                
            },
            trigger: 'left'
        });
    }
    else {
        $("#CA_SS_dvSSContainer").html("<div style='margin-left:40px;'><label style='color:#6992c0;font-weight:normal'>No solve-space(s) found</label></div>");
    }
}

function CA_IsRecommended(Id){
    var _URL = "/DesktopModules/ClearAction_SolveSpaces/requesthandler.asmx/IsRecommended";
    var _data = "{'UserID':'" + CA_UserID + "','SolveSpaceID':'" + Id + "'}";
    CA_MakeRequest(_URL, _data, function (d){
        if (d == '1'){
            $("#lblSSRecommeded_" + Id).text("Recommended");
        }
    });
}

function CA_Assign2Me(ItemID) {
    // Expected ComponentID : 1-Forum, 2-Blog, 3-SolveSpace
    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/Assign2Me";
    var _data = "{'CID':'" + 3 + "','ItemID':'" + ItemID + "','UID':'" + CA_UserID + "'}";
    CA_MakeRequest(_URL, _data, CA_OnAssignItem2User);
}
function CA_OnAssignItem2User(d) {
    alert('Your vault is updated successfully');
    CA_LoadSolveSpacesList();
}

function CA_RemoveFromMyVault(ItemID) {
    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/RemoveFromMyVault";
    var _data = "{'CID':'" + 3 + "','IID':'" + ItemID + "','UID':'" + CA_UserID + "'}";
    CA_MakeRequest(_URL, _data, CA_OnAssignItem2User);
}