
<!-- Google Tag Manager -->
<script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);
})(window,document,'script','dataLayer','GTM-KLJM4HJ');</script>
<!-- End Google Tag Manager -->


<!-- Google Tag Manager (noscript) -->
<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-KLJM4HJ"
height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
<!-- End Google Tag Manager (noscript) -->


<div class="exchange_rt">
    <nav>
        <ul>
            <li <% If (PortalSettings.ActiveTab.TabName = "Home") Then Response.Write("class=""active""")%>><a class="exe1" href="/Home">My Exchange</a></li>
            <li <% If (PortalSettings.ActiveTab.TabName = "SolveSpaces") Then Response.Write("class=""active""")%>><a class="exe2" href="/SolveSpaces">Solve-Spaces</a></li>
            <li <% If (PortalSettings.ActiveTab.TabName = "Forum") Then Response.Write("class=""active""")%>><a class="exe3" href="/Forum">Open Forum</a></li>
            <li <% If (PortalSettings.ActiveTab.TabName = "Blog" Or PortalSettings.ActiveTab.TabName = "Insights") Then Response.Write("class=""active""")%>><a class="exe12" href="/Blog.aspx">Insights</a></li>
            
            <%--Modifed by @SP--%>
       <%--     <li <% If (PortalSettings.ActiveTab.TabName = "Digital") Then Response.Write("class=""active""")%>><a class="exe4" href="/Digital">Digital Events1</a></li>--%>
                <li <% If (PortalSettings.ActiveTab.TabName = "Digital") Then Response.Write("class=""active""")%>><a class="exe4" href="/Digital/CategoryId/-1/SortBy/-1/ComponentID/-1/Show/0/IsMyVault/0">Events</a></li>
            
            <!--li><a class="exe4" href="/Digital">Digital Events</a></li>
            <li><a class="exe5" href="/Value-Vault">Value Vault</a></li>-->
            <li <% If (PortalSettings.ActiveTab.TabName = "Connections") Then Response.Write("class=""active""")%>><a class="exe7" href="/Connections">Connections</a></li>
        </ul>
    </nav>
</div>
<div class="exchange_settings">
    <ul>
        <li class="demo">
            <a style="cursor:pointer" href="//<%#DotNetNuke.Common.Globals.GetPortalSettings().PortalAlias.HTTPAlias %>/profile?utm_source=menu">
                <span>Profile</span>
                <figure>
                    <img src='<%=UserController.GetCurrentUserInfo().Profile.PhotoURL%>' alt="icon-profile" class="img-responsive">
                </figure>
            </a>
        </li>
        <%--<li class="demo">
            <a href="#" data-toggle="modal" data-target="#contact" id="toggle"  style="cursor:pointer">
                <span>Alerts</span>
                <figure>
                    <img src="//<%#DotNetNuke.Common.Globals.GetPortalSettings().PortalAlias.HTTPAlias %>/images/icon-alerts.png" alt="icon-alerts" class="img-responsive">
                </figure>
                <div class="pos_1">
                    <div class="tbl">
                        <div class="tbl_cell">
                            <span id="spanCounter"></span>
                        </div>
                    </div>
                </div>
            </a>
        </li>--%>
        <li class="demo">
            <dnn:login id="dnnlogin" runat="server" cssclass="Header_Login_styleFixcss"  style="cursor:pointer"/>
        </li>
    </ul>
</div>
