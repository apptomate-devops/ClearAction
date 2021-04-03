<%@ Page Language="C#" AutoEventWireup="true" Inherits="avt.ActionForm.Initialize" CodeBehind="Initialize.aspx.cs" EnableTheming="false" StylesheetTheme="" Theme="" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" data-ng-app="ActionForm" class="bstrap30 bstrap3-material">
<head runat="server">
    <title>Initialize Action Form</title>

    <link type="text/css" rel="stylesheet" href="<%# AppInfo.CommonUrl + "/static/bootstrap-material/css/bootstrap.min.css?v=" + avt.ActionForm.Core.App.Info.Build %>" />
    <link type="text/css" rel="stylesheet" href="<%# TemplateSourceDirectory + "/static/admin.css?v=" + avt.ActionForm.Core.App.Info.Build %>" />

    <script type="text/javascript" src="<%# TemplateSourceDirectory + "/static/jquery.min.js?v=" + avt.ActionForm.Core.App.Info.Build %>"></script>

</head>
<body data-spy="scroll" data-target=".navbar" data-offset="0" class="ng-cloak">
    <form id="form1" runat="server" class="bstrap30">

        <div style="margin-left: 20px; margin-right: 20px;">
            <nav class="navbar navbar-inverse navbar-fixed-top" id="navbar" role="navigation">
                <div class="navbar-inner">

                    <ul class="nav navbar-nav navbar-right" style="margin-left: 20px; margin-right: 20px">
                        <li><a href="<%= ViewUrl %>" target="_top" style="font-weight: bold; color: #fff;" title="Return to the form. Make sure to save your settings first.">Back</a></li>
                    </ul>

                </div>
            </nav>

            <p class="alert alert-warning" style="margin-top: 70px; font-weight: bold;">
                This Action Form is not initialized.
                Select a template below or start from scratch.
            </p>
            <h1></h1>

            <div class="container">
                <ul class="list-group">
                    <asp:Repeater runat="server" ID="rpTemplates">
                        <ItemTemplate>
                            <li class="list-group-item">
                                <asp:LinkButton runat="server" CommandName="fromTemplate" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Name") %>' CssClass='<%# Container.ItemIndex == 0 ? "btn-primary btn pull-right" : "btn-default btn pull-right" %>'>Start&#160;<i class="glyphicon glyphicon-chevron-right"></i></asp:LinkButton>
                                <h4 class="list-group-item-heading"><%# DataBinder.Eval(Container.DataItem, "Name") %></h4>
                                <p class="list-group-item-text text-muted"><%# DataBinder.Eval(Container.DataItem, "Description") %></p>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </form>

    <script type="text/javascript" src="<%=AppInfo.CommonUrl%>/static/bootstrap-material/js/bootstrap.min.js?<%= avt.ActionForm.Core.App.Info.Build %>"></script>
</body>
</html>
