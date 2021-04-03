<%@ Control Language="VB" AutoEventWireup="false"
    Inherits="Phd.Dnn.BulkUserManager.Screens.PhdSelectDeletedUsers" Codebehind="PhdSelectDeletedUsers.ascx.vb" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<asp:HiddenField ID="hidMode" runat="server" Visible="false" Value="SIMPLE" />

<asp:Panel ID="panSimpleSearch" runat="server" Visible="True">
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


<asp:Panel ID="panComplexSearch" runat="server" Visible="false">
    <div style="margin-bottom:5px;" class="NormalBold">
        Advanced Search - <asp:LinkButton ID="lbSimpleSearch" runat="server" CausesValidation="false">switch to Simple Search</asp:LinkButton>
    </div>
    <div class="phdform">
        <dnn:Label ID="lblAndOr" runat="server" CssClass="Normal" Text="" ControlName="lblJoinText"> </dnn:Label>
        <asp:Label ID="lblJoinText" runat="server" CssClass="Normal">Choose the approach for user account selection</asp:Label>
    </div>
    <asp:Panel ID="panAndOr" runat="server" BorderWidth="1px" BorderColor="DarkGray"
        Enabled="True" BorderStyle="Solid" Wrap="False">
        <asp:RadioButtonList ID="rbAndOr" runat="server" Width="512px" CssClass="Normal">
            <asp:ListItem Value="AND" Selected="True">I want all selected filters to be matched (AND)</asp:ListItem>
            <asp:ListItem Value="OR">I want any one of the selected filters to be matched (OR)</asp:ListItem>
        </asp:RadioButtonList>
    </asp:Panel>
    <br>
    <div class="phdform">
        <dnn:Label ID="lblFilterOption" runat="server" CssClass="Normal" Text="" ControlName="cbNotification">
        </dnn:Label>
        <asp:CheckBox ID="cbFilterSelect" runat="server" CssClass="normalCheckBox" Text="Filter by details"></asp:CheckBox>
    </div>
    <asp:Panel ID="panFilter" runat="server" BorderColor="DarkGray" BorderWidth="1px" Wrap="False" BorderStyle="Solid">
        <table id="Table3" style="background: inherit; border: none;">
            <tr>
                <td style="padding: 1px; white-space: nowrap;">

                    <asp:Label ID="Label4" runat="server" CssClass="Normal">Field name</asp:Label><br>
                    <asp:DropDownList ID="ddlFilterField1" runat="server" CssClass="Normal" Enabled="true">
                    </asp:DropDownList>&nbsp;
                </td>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:Label ID="Label5" runat="server" CssClass="Normal">Match type</asp:Label><br>
                    <asp:DropDownList ID="ddlFilterCondition1" runat="server" CssClass="Normal" Enabled="true">
                        <asp:ListItem Value="Equals" Selected="True">Equals</asp:ListItem>
                        <asp:ListItem Value="Starts with">Starts with</asp:ListItem>
                        <asp:ListItem Value="Contains">Contains</asp:ListItem>
                    </asp:DropDownList>&nbsp;</td>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:Label ID="Label9" runat="server" CssClass="Normal">Text to match</asp:Label><br>
                    <asp:TextBox ID="tbFilterText1" runat="server" CssClass="Normal" Enabled="true"></asp:TextBox></td>
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
        <table id="TableDate3" style="background: inherit; border: none;">
            <tr>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:Label ID="LabelDate4" runat="server" CssClass="Normal">Date field</asp:Label><br>
                    <asp:DropDownList ID="ddlFilterDate1" runat="server" CssClass="Normal" Enabled="true">
                    </asp:DropDownList>&nbsp;
                </td>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:Label ID="LabelDate5" runat="server" CssClass="Normal">Match type</asp:Label><br>
                    <asp:DropDownList ID="ddlFilterDateCondition1" runat="server" CssClass="Normal" Enabled="true">
                        <asp:ListItem Value="DateEquals" Selected="True">Equals</asp:ListItem>
                        <asp:ListItem Value="DateBefore">Before</asp:ListItem>
                        <asp:ListItem Value="DateAfter">After</asp:ListItem>
                    </asp:DropDownList>&nbsp;</td>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:Label ID="LabelDate9" runat="server" CssClass="Normal">Date to match</asp:Label><br>
                    <asp:TextBox ID="tbFilterDate1" runat="server" CssClass="Normal" Enabled="true"></asp:TextBox>
                    <asp:HyperLink ID="cmdEffectiveCalendar1" runat="server" CssClass="CommandButton"></asp:HyperLink></td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <div class="phdform">
        <dnn:Label ID="lblFilterApprovalOption" runat="server" CssClass="Normal" Text=""
            ControlName="cbFilterApproval">
        </dnn:Label>
        <asp:CheckBox ID="cbFilterApproval" runat="server" CssClass="normalCheckBox" Text="Filter by approval status"></asp:CheckBox>
    </div>
    <asp:Panel ID="panFilterApproval" runat="server" BorderColor="DarkGray" BorderWidth="1px" Wrap="False" BorderStyle="Solid">
        <table id="TableApproval" style="background: inherit; border: none;">
            <tr>
                <td style="padding: 1px; white-space: nowrap;">
                    <asp:Label ID="Label7" runat="server" CssClass="Normal">User account status</asp:Label><br>
                    <asp:DropDownList ID="ddlFilterApproval" runat="server" CssClass="Normal" Enabled="true">
                        <asp:ListItem Value="All" Selected="True">All accounts</asp:ListItem>
                        <asp:ListItem Value="Approved">Approved accounts only</asp:ListItem>
                        <asp:ListItem Value="NotApproved">Non-approved accounts only</asp:ListItem>
                    </asp:DropDownList>&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:PlaceHolder ID="phExportJavaScript" runat="server"></asp:PlaceHolder>
</asp:Panel>
<asp:Label ID="GUIDtext" runat="server" Visible="false"></asp:Label>

