<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PhdVersion.ascx.vb" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdVersion" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>
<%@ Register TagPrefix="dnnLabel" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<div style="margin-left: 30px; margin-right: 30px; margin-bottom:15px">

    <asp:Panel ID="panHelpExpander" runat="server" CssClass="NormalBold" BorderWidth="0px">
        <dnn:SectionHead ID="dshShowHelp" runat="server" CssClass="SubHead" Text="Help"
            IsExpanded="False" Section="divHelpArea" ResourceKey="HelpArea"
            IncludeRule="false"  />
   </asp:Panel>
   <div id="divHelpArea" runat="server" style="padding:5px;margin-top:5px;border-top:1px solid silver;border-bottom:1px solid silver;" >
       <dnnLabel:Label ID="lblHelpArea" runat="server" CssClass="Normal"></dnnLabel:Label>
   </div>
</div>
    <div style="width: 100%; font-size: 60%; text-align: center;">
        <asp:Label ID="LblVersion" runat="server" CssClass="NormalDisabled"></asp:Label>
    </div>
