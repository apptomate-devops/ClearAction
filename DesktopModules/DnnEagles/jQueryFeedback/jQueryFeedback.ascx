<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="jQueryFeedback.ascx.cs" Inherits="DnnEagles.jQueryFeedback.DesktopModules.DnnEagles.jQueryFeedback.jQueryFeedback" %>

<script src="\DesktopModules\DnnEagles\jQueryFeedback\js\jquery.browser.js"></script>
<script src="//code.jquery.com/jquery-migrate-1.2.0.js"></script>


<script type="text/javascript">
    var moduleID = <% =this.ModuleId %>;
    var portalID = <% =this.PortalId %>;

    var fbName = "<% =this.UserInfo.DisplayName %>";
    var fbEmail = "<% =this.UserInfo.Email %>";
   
</script>
<asp:Literal ID="ltrlScript" runat="server"></asp:Literal>
<div id="contactable"><!-- contactable html placeholder --></div>















