<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="ClearAction.Modules.WebCast.Settings" %>


<!-- uncomment the code below to start using the DNN Form pattern to create and update settings -->


<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a></h2>
<fieldset>
    <div class="dnnFormItem">
        <dnn:label id="lblSetting1" runat="server" text="Community Calls" helptext="Select which component associated with Community Calls" />
        <asp:DropDownList ID="ddCommunityCalls" runat="server"></asp:DropDownList>

    </div>
    <div class="dnnFormItem" style="display:none">
        <dnn:label id="Label1" runat="server" text="Community Calls" helptext="Select which component associated with Community Calls" />
        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>

    </div>
    <div class="dnnFormItem" style="display:none">
        <dnn:label id="lblWebCast" runat="server" text="Web Cast Conversion" helptext="Select which component associated with WebCastConversion" />
        <asp:DropDownList ID="ddWebCastConversion" runat="server"></asp:DropDownList>
    </div>


    <div class="dnnFormItem">
        <dnn:label id="lblMaxAllowedToDo" runat="server" text="Max Event Count" helptext="Maximum Event setup for each component for Self Assignement" />
        <asp:TextBox ID="txtMaxCount" runat="server"></asp:TextBox>
    </div>




    
    <div class="dnnFormItem" style="display:none">
        <dnn:label id="lblShowListingFor" runat="server" text="Show Listing for" helptext="Module Show Listing For?" />
        <asp:RadioButtonList ID="rdoListingApplicableFor" runat="server">
            <asp:ListItem Text="Community Call Only" Value="1" Selected="True"></asp:ListItem>
            <asp:ListItem Text="WebCast Conversion Only" Value="0"></asp:ListItem>
            <asp:ListItem Text="All" Value="-1"></asp:ListItem>
        </asp:RadioButtonList>

    </div>

    <div class="dnnFormItem">
        <dnn:label id="lblSendCongrtsEmail" runat="server" text="Send Congrts Email" helptext="Send Congrts email when to-do has been done" />
        <asp:CheckBox runat="server" ID="chkSendCongrtsEmail" />
    </div>

</fieldset>


