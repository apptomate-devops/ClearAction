<%@ Control Language="VB" AutoEventWireup="false"
    Inherits="Phd.Dnn.BulkUserManager.Screens.PhdSelectUsers" Codebehind="PhdSelectUsers.ascx.vb" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>


<!-- Simple search start -->
<asp:panel ID="panSimpleSearch" runat="server" Visible="True">
    <div style="margin-bottom:5px;" class="NormalBold">
        Simple Search - &nbsp;<asp:LinkButton ID="lnkAdvancedSearch" runat="server" CausesValidation="false">switch to Advanced Search</asp:LinkButton>
    </div>
    <div class="phdform">
        <dnn:Label ID="lblBasicSearchOption" runat="server" CssClass="Normal" Text="" ControlName="lblBasicSearch" />
        <asp:Label ID="lblBasicSearch" runat="server" CssClass="Normal">Search criteria</asp:Label>
    </div>
    <asp:Panel ID="panSimpleCriteria" runat="server" BorderWidth="1px" BorderColor="DarkGray"
        Enabled="True" BorderStyle="Solid" Wrap="False">
        <table id="tblSimple" style="background: inherit; border: none; border-spacing: 7px;">
            <tr>
                <td style="padding: 1px;">
                    <asp:Label ID="lblSimpleField" runat="server" CssClass="Normal">Field name</asp:Label><br>
                    <asp:DropDownList ID="ddlFilterFieldSimple" runat="server" CssClass="Normal" Enabled="true">
                    </asp:DropDownList>
                </td>
                <td style="padding: 1px;">
                    <asp:Label ID="lblSimpleText" runat="server" CssClass="Normal">Text to match</asp:Label><br>
                    <asp:TextBox ID="tbFilterTextSimple" runat="server" CssClass="Normal" Enabled="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
<!-- Simple search end -->


<!-- Complex search start -->
<asp:Panel ID="panComplexSearch" runat="server" Visible="false">
    <div style="margin-bottom:5px;" class="NormalBold">
        Advanced Search - <asp:LinkButton ID="lbSimpleSearch" runat="server" CausesValidation="false">switch to Simple Search</asp:LinkButton>
    </div>
    <div class="phdform">
        <dnn:Label ID="lblAndOr" runat="server" CssClass="Normal" Text="" ControlName="lblJoinText"></dnn:Label>
        <asp:Label ID="lblJoinText" runat="server" CssClass="Normal">Choose the approach for user account selection</asp:Label>
    </div>
    <asp:Panel ID="panAndOr" runat="server" BorderWidth="1px" BorderColor="DarkGray"
        Enabled="True" BorderStyle="Solid" Wrap="False">
        <div style="margin-left: 30px; margin-top: 5px; margin-bottom: 5px; text-align: left;" class="normal">
            <asp:RadioButtonList ID="rbAndOr" runat="server" Width="512px" CssClass="Normal">
                <asp:ListItem Value="AND" Selected="True">I want all selected filters to be matched (AND)</asp:ListItem>
                <asp:ListItem Value="OR">I want any one of the selected filters to be matched (OR)</asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </asp:Panel>
    <br />
    <div class="phdform">
        <dnn:Label ID="lblFilterOption" runat="server" CssClass="Normal" Text="" ControlName="cbFilterSelect">
        </dnn:Label>
        <asp:CheckBox ID="cbFilterSelect" runat="server" CssClass="normalCheckBox" Text="Filter by details"></asp:CheckBox>
    </div>
    <asp:Panel ID="panFilter" runat="server" BorderColor="DarkGray" BorderWidth="1px" Wrap="False" BorderStyle="Solid">
        <table id="Table3" style="background: inherit; border: none; border-spacing: 7px;">
            <tr>
                <td style="padding: 1px; white-space: nowrap;">

                    <asp:Label ID="Label4" runat="server" CssClass="Normal">Field name</asp:Label><br>
                    <asp:DropDownList ID="ddlFilterField1" runat="server" CssClass="Normal" Enabled="true" style="margin-bottom:0;">
                    </asp:DropDownList>&nbsp;
                </td>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:Label ID="Label5" runat="server" CssClass="Normal">Match type</asp:Label><br>
                    <asp:DropDownList ID="ddlFilterCondition1" runat="server" CssClass="Normal" Enabled="true" style="margin-bottom:0;">
                        <asp:ListItem Value="Equals" Selected="True">Equals</asp:ListItem>
                        <asp:ListItem Value="Starts with">Starts with</asp:ListItem>
                        <asp:ListItem Value="Contains">Contains</asp:ListItem>
                        <asp:ListItem Value="NotEquals">NOT Equals</asp:ListItem>
                        <asp:ListItem Value="Does Not Start with">Does NOT start with</asp:ListItem>
                        <asp:ListItem Value="Does Not Contain">Does NOT contain</asp:ListItem>
                        <asp:ListItem Value="Is Blank">Is Blank</asp:ListItem>
                        <asp:ListItem Value="Is NOT Blank">Is NOT Blank</asp:ListItem>
                    </asp:DropDownList>&nbsp;</td>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:Label ID="Label9" runat="server" CssClass="Normal">Text to match</asp:Label><br>
                    <asp:TextBox ID="tbFilterText1" runat="server" CssClass="Normal" Enabled="true" style="margin-bottom:0;"></asp:TextBox></td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <div class="phdform">
        <dnn:Label ID="lblFilterDateOption" runat="server" CssClass="Normal" Text="" ControlName="cbDateFilterNotification">
        </dnn:Label>
        <asp:CheckBox ID="cbFilterDate" runat="server" CssClass="normalCheckBox" Text="Filter by date"></asp:CheckBox>
    </div>
    <asp:Panel ID="panFilterDate" runat="server" BorderColor="DarkGray" BorderWidth="1px"
        Enabled="True" Wrap="False" BorderStyle="Solid">
        <table id="TableDate3" style="background: inherit; border: none; border-spacing: 7px;">
            <tr>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:Label ID="LabelDate4" runat="server" CssClass="Normal">Date field</asp:Label><br>
                    <asp:DropDownList ID="ddlFilterDate1" runat="server" CssClass="Normal" Enabled="True" style="margin-bottom:0;">
                    </asp:DropDownList>&nbsp;
                </td>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:Label ID="LabelDate5" runat="server" CssClass="Normal">Match type</asp:Label><br>
                    <asp:DropDownList ID="ddlFilterDateCondition1" runat="server" CssClass="Normal" Enabled="True" style="margin-bottom:0;">
                        <asp:ListItem Value="DateEquals" Selected="True">Equals</asp:ListItem>
                        <asp:ListItem Value="DateBefore">Before</asp:ListItem>
                        <asp:ListItem Value="DateAfter">After</asp:ListItem>
                    </asp:DropDownList>&nbsp;</td>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:Label ID="LabelDate9" runat="server" CssClass="Normal">Date to match</asp:Label><br>
                    <asp:TextBox ID="tbFilterDate1" runat="server" CssClass="Normal" Enabled="True" style="margin-bottom:0;"></asp:TextBox>
                    <asp:HyperLink ID="cmdEffectiveCalendar1" runat="server" CssClass="CommandButton" style="display:inline;"></asp:HyperLink></td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <div class="phdform">
        <dnn:Label ID="lblFilterActivityOption" runat="server" CssClass="Normal" Text="" ControlName="cbActivityFilterNotification">
        </dnn:Label>
        <asp:CheckBox ID="cbFilterActivity" runat="server" CssClass="normalCheckBox" Text="Filter by account activity type"></asp:CheckBox>
    </div>
    <asp:Panel ID="panFilterActivity" runat="server" BorderColor="DarkGray" BorderWidth="1px"
        Enabled="True" Wrap="False" BorderStyle="Solid">
        <table id="tblFilterActivity" style="background: inherit;  border: none; border-spacing: 7px;">
            <tr>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:DropDownList ID="ddlActivityType" runat="server" CssClass="Normal" Enabled="True" style="margin-bottom:0;">
                        <asp:ListItem Value="accountcreated" Selected="True" >Account created</asp:ListItem>
                        <asp:ListItem Value="accountupdated">Account updated</asp:ListItem>
                     </asp:DropDownList>&nbsp;
                </td>
                <td style="padding: 1px; white-space: nowrap;">in the last</td>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:DropDownList ID="ddlActivityAmount" runat="server" CssClass="Normal" Enabled="True" style="margin-bottom:0;">
                    </asp:DropDownList>&nbsp;</td>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:DropDownList ID="ddlActivityPeriod" runat="server" CssClass="Normal" Enabled="True" style="margin-bottom:0;">
                        <asp:ListItem Value="minute">Minutes</asp:ListItem>
                        <asp:ListItem Value="hour" Selected="True">Hours</asp:ListItem>
                        <asp:ListItem Value="day">Days</asp:ListItem>
                        <asp:ListItem Value="month">Months</asp:ListItem>
                    </asp:DropDownList>&nbsp;</td>
             </tr>
        </table>
    </asp:Panel>
    <br />
    <div class="phdform">
        <dnn:Label ID="lblFilterApprovalOption" runat="server" CssClass="Normal" Text=""
            ControlName="cbFilterApproval"></dnn:Label>
            <asp:CheckBox ID="cbFilterApproval" runat="server" CssClass="normalCheckBox" Text="Filter by approval status"></asp:CheckBox>
    </div>
    <asp:Panel ID="panFilterApproval" runat="server" BorderColor="DarkGray" BorderWidth="1px" Wrap="False" BorderStyle="Solid">
        <table id="TableApproval" style="background: inherit; border: none; border-spacing: 7px;">
            <tr>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:Label ID="Label7" runat="server" CssClass="Normal">User account status</asp:Label><br>
                    <asp:DropDownList ID="ddlFilterApproval" runat="server" CssClass="Normal" Enabled="True" style="margin-bottom:0;">
                        <asp:ListItem Value="All" Selected="True">All accounts</asp:ListItem>
                        <asp:ListItem Value="Approved">Approved accounts only</asp:ListItem>
                        <asp:ListItem Value="NotApproved">Non-approved accounts only</asp:ListItem>
                    </asp:DropDownList>&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <div class="phdform">
        <dnn:LABEL id="lblFilterRoleOption" runat="server" cssclass="Normal" text="" controlname="cbFilterRole"></dnn:LABEL>
        <asp:CheckBox ID="cbFilterRole" runat="server" CssClass="normalCheckBox" Text="Filter by roles"></asp:CheckBox>
    </div>
    <asp:Panel ID="panFilterRole" runat="server" BorderWidth="1px" BorderColor="DarkGray" BorderStyle="Solid" Wrap="False">
        <table id="TableRole" style="background: inherit; border: none; border-spacing: 7px;">
            <tr>
                <td style="padding: 1px; white-space: nowrap; vertical-align: top;">
                    <asp:DropDownList ID="ddlRoleSelection" runat="server" CssClass="Normal" Enabled="True" style="margin-bottom:0;">
                        <asp:ListItem Value="inall" Selected="True">Is a member of all selected</asp:ListItem>
                        <asp:ListItem Value="inany">Is a member of one of more selected</asp:ListItem>
                        <asp:ListItem Value="notinall">Is NOT a member of all selected</asp:ListItem>
                    </asp:DropDownList>&nbsp;
                </td>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:ListBox ID="lbFilterRole" runat="server" CssClass="Normal" SelectionMode="Multiple" style="margin-bottom:0;"></asp:ListBox></td>
            </tr>
        </table>
    </asp:Panel>
    <asp:PlaceHolder ID="phExportJavaScript" runat="server"></asp:PlaceHolder>
</asp:Panel>
<!-- Complex search end -->

<asp:Label ID="GUIDtext" runat="server" Visible="false"></asp:Label>

