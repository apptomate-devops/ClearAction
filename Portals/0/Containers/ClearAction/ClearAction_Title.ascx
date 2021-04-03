<%@ Control AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Containers.Container" %>
 <%@ Register TagPrefix="dnn" TagName="TITLE" Src="~/Admin/Containers/Title.ascx" %>
 
<link rel="stylesheet"  href="<%=ContainerPath%>Container.css"  type="text/css"/>
<div class="clearfix">

<div class="ClearHeadContainer clearfix">
        <h2><dnn:TITLE runat="server" id="dnnTITLE" CssClass="ClearHead"/></h2>
</div>
 <div id="ContentPane" runat="server"></div>
 <div class="clear"></div>
</div>
