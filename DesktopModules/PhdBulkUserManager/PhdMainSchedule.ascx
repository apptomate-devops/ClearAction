<%@ Control Language="VB" AutoEventWireup="false" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdMainSchedule" Codebehind="PhdMainSchedule.ascx.vb" %>
<%@ Register Src="PhdTitle.ascx" TagName="PhdTitle" TagPrefix="Title" %>
<%@ Register Src="PhdScheduleList.ascx" TagName="PhdScheduleList" TagPrefix="List" %>
<%@ Register Src="PhdScheduleLog.ascx" TagName="PhdScheduleLog" TagPrefix="Log" %>
<%@ Register Src="PhdScheduleEdit.ascx" TagName="PhdScheduleEdit" TagPrefix="Edit" %>
<%@ Register Src="PhdNavigation.ascx" TagName="PhdNavigation" TagPrefix="PNav" %>
<!-- Mainschedule start -->
<Title:PhdTitle ID="PhdTitleControl" runat="server" Title="Current scheduled jobs" />
<div style="margin: 5px 30px 0 30px;">
    <List:PhdScheduleList ID="PhdScheduleListControl" runat="server" visible="true" />
    <Edit:PhdScheduleEdit ID="PhdScheduleEditControl" runat="server" visible="false" />
    <Log:PhdScheduleLog ID="PhdScheduleLogControl" runat="server" visible="false" />
</div>
<!-- Mainschedule end -->
