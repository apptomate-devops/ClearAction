<%@ Control Language="C#" AutoEventWireup="True" Inherits="avt.ActionForm.Reports" EnableViewState="true" CodeBehind="Reports.ascx.cs" %>
<%@ Import Namespace="avt.ActionForm" %>

<div class="avtBox sunny">
    <div class="formHeader" style="margin-bottom: 16px;">
        <%= ActionFormController.LocalizeReports("formHeader.download", "Download Report for form") %>
        <i>
            <asp:Label runat="server" ID="lbFormName"></asp:Label></i>
    </div>

    <b><%= ActionFormController.LocalizeReports("startDate.label", "Start Date") %></b><br />
    <asp:TextBox runat="server" ID="tbStartDate"></asp:TextBox>
    <span style="color: #828282; font-style: italic; font-size: 11px;"><%= ActionFormController.LocalizeReports("startDate.helpText", "leave blank to get all entries before start date") %></span>
    <br />

    <b><%= ActionFormController.LocalizeReports("endDate.label", "End Date") %></b><br />
    <asp:TextBox runat="server" ID="tbEndDate"></asp:TextBox>
    <span style="color: #828282; font-style: italic; font-size: 11px;"><%= ActionFormController.LocalizeReports("endDate.helpText", "leave blank to get all entries until today") %></span>
    <br />

    <b><%= ActionFormController.LocalizeReports("fieldSeparator.label", "Field Separator") %></b>
    <br />
    <asp:Label ID="lblSepDlt" runat="server" Text="Label"><%= ActionFormController.LocalizeReports("fieldSeparator.helpText", "use semicolon if you open CSV Files with Excel and your region Format is set to other than English (United States)") %></asp:Label>
    <br />
    <%--<asp:TextBox runat="server" ID="tbSepDl" Text=","></asp:TextBox>--%>
    <asp:DropDownList ID="tbSepDlt" runat="server"></asp:DropDownList>
    <span style="color: #828282; font-style: italic; font-size: 11px;"></span>
    <br />

    <b><%= ActionFormController.LocalizeReports("columnHeaders.label", "Column headers") %></b><br />
    <asp:DropDownList ID="ddlColumnHeaderDl" runat="server"></asp:DropDownList>
    <span style="color: #828282; font-style: italic; font-size: 11px;"></span>
    <br />

    <div style="text-align: center; margin: 30px 40px 10px 0;">
        <asp:LinkButton runat="server" OnClick="OnDownloadReport" Style="font-size: 14px; font-weight: bold; color: #4a8094; margin-right: 10px;" CausesValidation="false"><%= ActionFormController.LocalizeReports("downloadCSVButton.label", "Download as CSV") %></asp:LinkButton>
        <a href="<%= DotNetNuke.Common.Globals.NavigateURL(TabId) %>" style="font-size: 12px; font-weight: bold; color: #CC0000; font-style: normal;"><%= ActionFormController.LocalizeReports("backButton.label", "Back") %></a>
    </div>

</div>

<div class="avtBox sunny">

    <div class="formHeader" style="margin-bottom: 16px;"><%= ActionFormController.LocalizeReports("formHeader.upload", "Upload CSV Report") %></div>
    <p style="color: #525252;"><%= ActionFormController.LocalizeReports("formHeader.upload.helpText", "If you need to make modifications to the submitted data, download the CSV above then upload it back in the form below.") %></p>

    <b><%= ActionFormController.LocalizeReports("fieldSeparator.label", "Field Separator") %></b><br />
    <asp:Label ID="lblSepUplt" runat="server" Text="Label"><%= ActionFormController.LocalizeReports("fieldSeparator.upload.helpText", "use the delimitator from within the CSV file, usually preset by the software that you saved the file with.") %></asp:Label>
    <br />
    <%--<asp:TextBox runat="server" ID="tbSepUpl" Text=","></asp:TextBox>--%>
    <asp:DropDownList ID="tbSepUplt" runat="server"></asp:DropDownList>
    <span style="color: #828282; font-style: italic; font-size: 11px;"></span>
    <br />

    <b><%= ActionFormController.LocalizeReports("upload.label", "CSV File") %></b><br />
    <asp:FileUpload runat="server" ID="uplCsv"></asp:FileUpload>
    <br />

    <div style="text-align: center; margin: 30px 40px 10px 0;">
        <asp:LinkButton runat="server" OnClick="OnUploadReport" Style="font-size: 14px; font-weight: bold; color: #4a8094; margin-right: 10px;" CausesValidation="false"><%= ActionFormController.LocalizeReports("uploadButton.label", "Upload") %></asp:LinkButton>
        <a href="<%= DotNetNuke.Common.Globals.NavigateURL(TabId) %>" style="font-size: 12px; font-weight: bold; color: #CC0000; font-style: normal;"><%= ActionFormController.LocalizeReports("backButton.label", "Back") %></a>
    </div>

    <div runat="server" id="pnlImportStatus" style="color: #4a8094; text-align: center;"></div>

</div>


<script type="text/javascript">


    dnnsfjQuery(document).ready(function () {
        // initialize date pickers
        dnnsfjQuery("#<%= tbStartDate.ClientID %>").datepicker();
        dnnsfjQuery("#<%= tbEndDate.ClientID %>").datepicker();
    });


</script>

<div id="reclama">

    <br />
    <br />
    <br />
    <br />
    <b><%= ActionFormController.LocalizeReports("promoText.1", "For rich web based reports with search and filtering capabilities, please check our") %> <a href="http://www.dnnsharp.com/dnn/modules/action-grid-table-data" target="_blank"><b><b>Action Grid</b></b></a> <%= ActionFormController.LocalizeReports("promoText.2", "module") %>.
    <br />
    <%= ActionFormController.LocalizeReports("promoText.3", "Here's a live") %> </b>
    <a href="https://www.youtube.com/watch?v=_UJN7KGXyQY" target="_blank"><b><%= ActionFormController.LocalizeReports("promoText.4", "demo") %></b></a><b>.</b>
</div>
