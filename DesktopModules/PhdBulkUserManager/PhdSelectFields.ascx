<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdSelectFields" Codebehind="PhdSelectFields.ascx.vb" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>
<div style="margin-bottom:15px">
    <asp:Panel ID="panExpander" runat="server" CssClass="NormalBold" BorderWidth="0px">
        <dnn:SectionHead ID="dshSelectFields" runat="server" CssClass="SubHead" Text="Select Fields"
            IsExpanded="False" Section="tblSelectFields" ResourceKey="SelectFields"
            IncludeRule="false" />
   </asp:Panel>
   <table id="tblSelectFields" style="border: solid 1px silver; width: 100%; background: inherit;" summary="Select Fields" runat="server">
        <tr>
            <td style="text-align: left; padding: 3px;">
                <asp:Panel ID="panSelectFields" runat="server" CssClass="Normal" Style="margin-left: 30px; margin-right: 30px;"
                    BorderWidth="1px" BorderColor="Silver" BorderStyle="None">
                    <asp:CheckBoxList ID="CBLFields" runat="server" CssClass="normalCheckBox" RepeatColumns="3"
                        Width="100%">
                    </asp:CheckBoxList>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                <span class="Normal" style="text-align: center; width: 100%"><strong>Select</strong>
                    <a id="A1" href="#" onclick="javascript: CheckBoxListSelect ('<%= CBLFields.ClientID %>',true);false;">All</a> | <a id="A2" href="#" onclick="javascript: CheckBoxListSelect ('<%= CBLFields.ClientID %>',false);false;">None</a> </span>
            </td>
        </tr>
    </table>
 
 </div>
