<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="ClearAction.Modules.SolveSpaces.Edit" %>
<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<style>
    .edContainers {
        width: 100%;
        border-bottom: double 1px #808080;
        border-top: solid 1px #808080;
        margin-bottom: 20px;
        padding:10px;
    }
  .edButton {
        padding:5px;
        min-width:75px;
    }
  .edListContainer{
      padding:10px;
  }
  .edTextBox{
      border:solid 1px #C0C0C0;
      padding:5px;
      width:90%;
  }
  .edPrimary{
      padding:5px;
      background-color:#3399FF;
      color:#FFF;
      min-width:75px;
  }

</style>
<div class="edContainers">
    <div style="margin:20px;text-align:right;"><input class="edPrimary" type="button" id="btnCreateNew" onclick=" return OpenEditDialog();" value="Create New Solve-Space" /></div>
</div>
<div id="dvEdit_NewSSForm" style="width: 100%;color:#4d4d4d" title="Solve-Space Information">
    <div style="width: 100%;padding-top:25px;">
        <div style="width:100%;">
            <input class="SolveSpaceHeader edTextBox" style="width:100%;" id="txtSolveSpaceTitle" placeholder="Solve-Space Title" type="text"  maxlength="200" />
        </div>
        <div style="width: 50%; float: left; margin-top: 25px;">
            <textarea id="txtSSDescription" placeholder="Short Description" class="edTextBox" rows="4" cols="80" style="width: 100%;" maxlength="500"></textarea>
             <div style="margin-top: 10px;">
            <table style="width:100%">
                <tr>
                    <td style="text-align:left;width:50%;">
                        <div style="margin-top: 25px;">
                            <input id="txtSSDuration" type="text" maxlength="3" placeholder="Duration" value="30" style="width: 50px;" class="edTextBox" />&nbsp;min
                        </div>
                    </td>
                    <td style="text-align:right;width:50%;">
                        <div style="margin-top: 25px;">
                            <input id="txtSSSteps" type="text" maxlength="1" placeholder="Steps" value="5" style="width: 50px;" class="edTextBox" />&nbsp;step
                        </div>
                    </td>
                </tr>
                 <tr>
                    <td style="text-align:left;">
                        <div style="margin-top: 25px;">
                            <label style="font-weight:normal;">Tab Link:</label>&nbsp;
                           <input class="edTextBox" id="txtSolveSpaceTabLink" placeholder="Page URL" type="text" style="width:350px;"  maxlength="200" />
                        </div>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        </div>
        <div style="width: 47%; float: left;margin-left:20px;margin-top:25px;">
            <select id="ddlNewCategory" class="ddlCategories edTextBox" multiple="multiple" style="width:100%;height:200px;color:#4d4d4d !important"></select>
        </div>
    </div>
</div>
<div id="dvEdit_SolveSpaceListContainer" class="edListContainer">
    <div style="margin-left:20px;">
        <label style="font-weight:normal;">Solve-spaces under category:&nbsp;</label>
        <select id="lstCategory"></select>
    </div>
    <div id="dvEdit_SolveSpaceList" style="margin-top:20px;width:100%;" class="edButton">

    </div>
</div>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/jquery-jtemplates/jquery-jtemplates.js"></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/Module/Common.js"></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/Module/edit.js"></script>