<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="BlogImport.ascx.vb" Inherits="DotNetNuke.Modules.Blog.BlogImport" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<div class="dnnForm dnnBlogImport dnnClear" id="dnnBlogImport">
    <asp:Wizard ID="wizBlogImport" runat="server" DisplaySideBar="false" ActiveStepIndex="0"
        CellPadding="5" CellSpacing="5"
        DisplayCancelButton="True"
        CancelButtonType="Link"
        StartNextButtonType="Link"
        StepNextButtonType="Link"
        StepPreviousButtonType="Link"
        FinishCompleteButtonType="Link">
        <CancelButtonStyle CssClass="dnnSecondaryAction" />
        <StartNextButtonStyle CssClass="dnnPrimaryAction" />
        <StepNextButtonStyle CssClass="dnnPrimaryAction" />
        <FinishCompleteButtonStyle CssClass="dnnPrimaryAction" />
        <StepStyle VerticalAlign="Top" />
        <NavigationButtonStyle BorderStyle="None" BackColor="Transparent" />
        <HeaderTemplate>
            <asp:Label ID="lblTitle" CssClass="Head" runat="server"><%=GetText("Title")%></asp:Label><br />
            <br />
            <asp:Label ID="lblHelp" CssClass="WizardText" runat="server"><%=GetText("Help") %></asp:Label>
        </HeaderTemplate>
        <StartNavigationTemplate>
            <ul class="dnnActions dnnClear">
                <li>
                    <asp:LinkButton ID="nextButtonStart" runat="server" CssClass="dnnPrimaryAction" CommandName="MoveNext" resourcekey="Next" /></li>
                <li>
                    <asp:LinkButton ID="cancelButtonStart" runat="server" CssClass="dnnSecondaryAction" CommandName="Cancel" resourcekey="Cancel" CausesValidation="False" /></li>
            </ul>
        </StartNavigationTemplate>
        <StepNavigationTemplate>
            <ul class="dnnActions dnnClear">
                <li>
                    <asp:LinkButton ID="nextButtonStep" runat="server" CssClass="dnnPrimaryAction" CommandName="MoveNext" resourcekey="Next" /></li>
                <li>
                    <asp:LinkButton ID="cancelButtonStep" runat="server" CssClass="dnnSecondaryAction" CommandName="Cancel" resourcekey="Cancel" CausesValidation="False" /></li>
            </ul>
        </StepNavigationTemplate>
        <FinishNavigationTemplate>
            <ul class="dnnActions dnnClear">
                <li>
                    <asp:LinkButton ID="finishButtonStep" runat="server" CssClass="dnnPrimaryAction" CommandName="Cancel" resourcekey="Return" /></li>
            </ul>
        </FinishNavigationTemplate>
        <WizardSteps>
            <asp:WizardStep ID="Step0" runat="Server" Title="Upload" StepType="Start" AllowReturn="false">
                <div class="dnnForm" >
                    <div class="dnnFormItem">
                        <dnn:label id="lblTarget" runat="server" controlname="lblTarget" suffix=":" />
                        <asp:Label ID="lblTargetName" runat="server" />
                    </div>
                    <div class="dnnFormItem dnnClear">
                        <input id="cmdBrowse" type="file" size="50" name="cmdBrowse" runat="server" />
                        <asp:Label ID="lblLoadMessage" runat="server" CssClass="dnnFormMessage dnnFormValidationSummary" Visible="false" />
                    </div>
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="Step1" runat="Server" Title="Analysis" StepType="Step" AllowReturn="false">
                <asp:TextBox runat="server" ID="txtAnalysis" TextMode="MultiLine" Width="300" Height="200" /><br />
                <div class="dnnForm" style="display:none">
                    <div class="dnnFormItem">
                        <dnn:label id="lblImportCategories" runat="server" controlname="chkImportCategories" suffix=":" />
                        <asp:CheckBox runat="server" ID="chkImportCategories"  Checked="false"/>
                    </div>
                    <div class="dnnFormItem">
                        <dnn:label id="lblImportMissingCategoriesAsKeywords" runat="server" controlname="chkImportMissingCategoriesAsKeywords" suffix=":" />
                        <asp:CheckBox runat="server" ID="chkImportMissingCategoriesAsKeywords"  Checked="false"/>
                    </div>
                    <div class="dnnFormItem">
                        <dnn:label id="lblImportComments" runat="server" controlname="chkImportComments" suffix=":" />
                        <asp:CheckBox runat="server" ID="chkImportComments"  Checked="false"/>
                    </div>
                </div>
            </asp:WizardStep>
            <asp:WizardStep ID="Step2" runat="server" Title="Report" StepType="Step" AllowReturn="false">
                <asp:TextBox runat="server" ID="txtReport" TextMode="MultiLine" Width="300" Height="200" />
            </asp:WizardStep>
        </WizardSteps>
    </asp:Wizard>
</div>
<asp:HiddenField ID="hdnfile" runat="server" />
<script language="javascript" type="text/javascript">
    /*globals jQuery, window, Sys */
    (function ($, Sys) {
        function setUpDnnExtensions() {
            $('#dnnBlogImport').dnnPanels();
        }
        $(document).ready(function () {
            setUpDnnExtensions();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                setUpDnnExtensions();
            });
        });
    }(jQuery, window.Sys));
</script>
