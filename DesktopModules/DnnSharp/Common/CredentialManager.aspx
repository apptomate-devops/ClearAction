<%@ Page Language="C#" 
    AutoEventWireup="true" 
    Inherits="DnnSharp.Common.CredentialStore.UI.CredentialManager" 
    CodeBehind="CredentialManager.aspx.cs" 
    EnableTheming="false" StylesheetTheme="" Theme="" %>

    <!DOCTYPE html>
    <html data-ng-app="app" class="bstrap30 bstrap3-material">

    <head>

        <title>Credential Manager</title>

<%--        <link type="text/css" rel="stylesheet" href="static/ui-themes/redmond/jquery-ui-1.8.22.css?v=<%= App.AppInfo.CommonVersion %>" />--%>

        <script type="text/javascript" src="static/jquery/jquery.min.js?<%= App.AppInfo.CommonVersion %>"></script>

        <script type="text/javascript" src="static/jquery/jquery-ui.min.js?<%= App.AppInfo.CommonVersion %>"></script>

        <link type="text/css" rel="stylesheet" href="<%= TemplateSourceDirectory%>/static/bootstrap/css/bootstrap.min.css?v=<%= App.AppInfo.CommonVersion %>" />
        <link type="text/css" rel="stylesheet" href="<%= TemplateSourceDirectory%>/static/angular15/textAngular.css?v=<%= App.AppInfo.CommonVersion %>" />
        <link type="text/css" rel="stylesheet" href="<%= TemplateSourceDirectory%>/static/bootstrap-material/css/bootstrap.min.css?v=<%= App.AppInfo.CommonVersion %>" />
        <link type="text/css" rel="stylesheet" href="<%= TemplateSourceDirectory%>/static/credential-store/admin.css?v=<%= App.AppInfo.CommonVersion %>" />

<%--        <link rel="stylesheet" type="text/css" href="static/css/screen_layout_large.css" />--%>
<%--        <link rel="stylesheet" type="text/css" media="only screen and (min-width:581px) and (max-width:877px)" href="static/css/screen_layout_medium.css" />
        <link rel="stylesheet" type="text/css" media="only screen and (min-width:471px) and (max-width:580px)" href="static/css/screen_layout_medium2.css" />
        <link rel="stylesheet" type="text/css" media="only screen and (min-width:50px) and (max-width:470px)" href="static/css/screen_layout_small.css" />--%>
        <!--[if lt IE 9]>
		<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
		<![endif]-->

        <script type="text/javascript">
            var dnn = {};
            var $ = dnnsfjQuery;
            var g_dnnsfState = {};
            var g_resourceVersion = '<%= App.AppInfo.Build %>';
          
        </script>
    </head>

    <body data-ng-cloak="">

        <form runat="server">

            <div ng-view></div>

            <script type="text/javascript" src="static/angular15/angular.min.js?<%= App.AppInfo.CommonVersion %>"></script>
            <script type="text/javascript" src="static/angular15/angular-route.min.js?<%= App.AppInfo.CommonVersion %>"></script>
            <script type="text/javascript" src="static/angular15/angular-sanitize.min.js?<%= App.AppInfo.CommonVersion %>"></script>
            <script type="text/javascript" src="static/angular15/textAngular-sanitize.min.js?<%= App.AppInfo.CommonVersion %>"></script>
            <script type="text/javascript" src="static/angular15/textAngularSetup.js?<%= App.AppInfo.CommonVersion %>"></script>
            <script type="text/javascript" src="static/angular15/textAngular.min.js?<%= App.AppInfo.CommonVersion %>"></script>

            <%--<script type="text/javascript" src="static/angular.js?
                <%= App.AppInfo.CommonVersion %>"></script>--%>
                    <script type="text/javascript" src="static/bootstrap/js/bootstrap.min.js?<%= App.AppInfo.CommonVersion %>"></script>
                    <script type="text/javascript" src="static/angular15/ui-bootstrap-tpls.js?<%= App.AppInfo.CommonVersion %>"></script>
                    <%--<script type="text/javascript" src="static/angular-dragdrop.min.js?
                        <%= App.AppInfo.CommonVersion %>"></script>--%>

                            <!-- Adding Angular - Bootstrap ui -->
<%--                            <script type="text/javascript" src="static/angular/ui-bootstrap-tpls-0.13.0.min.js?<%= App.AppInfo.CommonVersion %>"></script>--%>

<%--                            <script type="text/javascript" src="<%= TemplateSourceDirectory%>/static/tinymce/tiny_mce.js"></script>
                            <script type="text/javascript" src="<%= TemplateSourceDirectory%>/static/angular/angular-tinymce.js"></script>
                            <script type="text/javascript" src="<%= TemplateSourceDirectory%>/static/angular/angular-dragdrop.js"></script>--%>

                            <script type="text/javascript" src="<%= TemplateSourceDirectory%>/static/js/lodash.min.js"></script>
                            <script type="text/javascript" src="static/dnnsf/dnnsf.js?<%= App.AppInfo.CommonVersion %>"></script>
                            <script type="text/javascript" src="static/dnnsf/dnnsf.admin.js?<%= App.AppInfo.CommonVersion %>"></script>
                            <script type="text/javascript" src="static/dnnsf/dnnsf.components.js?<%= App.AppInfo.CommonVersion %>"></script>
                            <script type="text/javascript" src="static/credential-store/admin.js?<%= App.AppInfo.CommonVersion %>"></script>

        </form>

    </body>

    </html>