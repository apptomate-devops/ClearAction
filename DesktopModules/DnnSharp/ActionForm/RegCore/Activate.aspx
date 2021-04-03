<%@ Page Language="C#" AutoEventWireup="true" Inherits="avt.ActionForm.RegCore.Activate" CodeBehind="Activate.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" data-ng-app="Activate">
<head runat="server">

    <!-- the placeholder allows for both, the use of < %= and for head scripts to be injected through header controls -->
    <asp:PlaceHolder runat="server">
        <title>Activate <%= avt.ActionForm.Core.App.Info.Name %></title>
    </asp:PlaceHolder>
</head>
<body class="bstrap30">
    <form class="form-horizontal" role="form" runat="server">
        <div class="container" data-ng-controller="ActivateCtl" data-ng-init="
                app = <%= HttpUtility.HtmlEncode(GetAppJson()) %>;
                returnUrl = '<%= ReturnUrl %>';">

            <div data-ng-include="'<%= ResolveCommonInclude("/dnnsf/tpl/activate.html")  %>'"></div>

        </div>
    </form>

    <script type="text/javascript" src="<%=ResolveCommonInclude("/angular/angular.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveCommonInclude("/bootstrap3/js/bootstrap.min.js")%>"></script>
    <script type="text/javascript" src="<%=ResolveCommonInclude("/dnnsf/activate.js")%>"></script>

</body>
</html>
