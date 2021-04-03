<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PhdShowProgress.ascx.vb" Inherits="Phd.Dnn.BulkUserManager.Screens.PhdShowProgress" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register Src="PhdShowResults.ascx" TagName="PhdShowResults" TagPrefix="ShowResults" %>


<div id="ProgressBlock" runat="server">
    <div style="background-color:black; border-radius:10px; padding: 0px;height:20px;">
        <asp:PlaceHolder ID="phProgressBar" runat="server"></asp:PlaceHolder>
    </div>
    <br />
    <div style="text-align:center;">
        <asp:Label ID="ProcessedCounter" runat="server" Visible="true" ></asp:Label> records processed. <asp:Label ID="ProcessedPercent" runat="server" Visible="true"></asp:Label>% completed.
    </div>
</div>

<ShowResults:PhdShowResults ID="ShowResultsControl" runat="server" Visible="false" />
<div id="TimeTakenBlock" runat="server" style="text-align:center;" visible="false">
    Time taken : <asp:Label ID="TimeTaken" runat="server" Visible="true"></asp:Label>
</div>
<div id="TimeRemainingBlock" runat="server" style="text-align:center;" visible="false">
    Time remaining : <asp:Label ID="TimeRemaining" runat="server" Visible="true"></asp:Label>
</div>

<asp:Label ID="StartTime" runat="server" Visible="false"></asp:Label>

<asp:PlaceHolder ID="phJavaScript" runat="server"></asp:PlaceHolder>
        



