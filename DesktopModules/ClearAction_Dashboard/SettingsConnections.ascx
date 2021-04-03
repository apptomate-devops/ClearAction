<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SettingsConnections.ascx.cs" Inherits="ClearAction.Modules.Dashboard.Components.SettingsConnections" %>
<%--<%@ Register Src="http://localhost:65402/SettingsConnections.ascx" TagPrefix="uc1" TagName="SettingsConnections" %>--%>
<%@ Register Src="~/controls/texteditor.ascx" TagPrefix="dnn" TagName="texteditor" %>
<%@ Register Src="~/controls/labelcontrol.ascx" TagPrefix="dnn" TagName="Label" %>
<div class="dnnFormItem">
    
    <dnn:Label id="lblModuleTitle"  runat="server"></dnn:Label>
     <div  style="padding-left:35%">
         <asp:RadioButtonList ID="rdobtn" runat="server" CssClass="form-control">
         <asp:ListItem Text="Connections" Value="0"></asp:ListItem>
         <asp:ListItem Text="Roles Similar to Mine" Value="1"></asp:ListItem>
         <asp:ListItem Text="People in My Company" Value="2"></asp:ListItem>
     
     </asp:RadioButtonList>
     </div>
</div>
<div class="dnnFormItem">
    
     <dnn:Label ID="lblPaging" runat="server" />
     
     <asp:TextBox ID="txtNumberofRecords" runat="server" MaxLength="2" pattern="[0-9]" onkeypress="return isNumberKey(event)" CssClass="form-control"></asp:TextBox>

</div>

<div class="dnnFormItem">
    
       <dnn:Label id="lblJSReference"  runat="server"></dnn:Label>
     <asp:CheckBox ID="chkJSRefernce" runat="server" CssClass="form-control"/>

</div>


<%--<div class="dnnFormItem">
   
      <dnn:Label id="lblEmailTemplate"  runat="server"></dnn:Label>
   <div style="float:left">
        <dnn:texteditor runat="server" ID="txteditor" DefaultMode="Html" Width="90%" />
   </div>

</div>--%>

<script language="Javascript">

    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }

</script>
