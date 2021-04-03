<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchResult.ascx.cs" Inherits="DotNetNuke.Modules.ActiveForums.SearchResult" %>

<%@ Register TagPrefix="am" TagName="topheader" Src="~/DesktopModules/ActiveForums/controls/af_topheader.ascx" %>

<link type="text/css" href='<%=ModulePath %>module.css' />

  
<div class="solve_wrapper">
    <div class="cnt_wraper_lt col-sm-8">
       <am:topheader id="topheader" runat="server"></am:topheader>
           <div class="forum-post">
               <asp:Literal ID="litSearchTitle" runat="server"></asp:Literal>
               <asp:Literal ID="litRecordCount" runat="server"></asp:Literal>
               <asp:Panel ID="pnlMessage" runat="server"></asp:Panel>

               <asp:Literal ID="litMessage" runat="server"></asp:Literal>
           </div>
        <div class="clearfix"></div>
        <asp:Literal ID="ltrHolder" runat="server">
        </asp:Literal>

        <asp:Repeater ID="rptPosts" runat="server"></asp:Repeater>
          <asp:Repeater ID="rptTopics" runat="server">
            <ItemTemplate>
                <div class="forum-row-wrapper">
                    <div class="forum-left-col col-sm-8 forum-match-height">
                        <div class="forum-post">
                            <div class="forum-more-top">
                                <a href="#" class="forum-see-more-top active">Collapse</a>
                                <div class="forum-bio">
                                    <div class="forum-bio-left">
                                        <asp:HiddenField ID="hdnTopicId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "TopicId")%>' />
                                   
                                    </div>
                                    <div class="forum-bio-right">
                                     
                                      
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                               
                            </div>
                             
                            <div class="clearfix"></div>
                             
                            <a href="#" class="forum-see-more">See more</a>
                            <div class="clearfix"></div>
                            <div class="forum-sep"></div>
                        </div>
                    </div>
                    <div class="forum-right-col col-sm-4 forum-match-height">
                        <%--<div class="forum-right-status">
                            <div class="forum-right-toggle-content">
                                <span class="num">7</span>
                                <span class="title">active members</span>
                                <a href="#" class="toggle-content active right">Collapse</a>
                                <div class="green-circle-small"></div>
                                <div class="clearfix"></div>
                                <div class="members-expand active">
                                    <img src='<%=ModulePath%>images/forum-pic-a.png' alt="Forum" />
                                    <img src='<%=ModulePath%>images/forum-pic-b.png' alt="Forum" />
                                    <img src='<%=ModulePath%>images/forum-pic-a.png' alt="Forum" />
                                    <img src='<%=ModulePath%>images/forum-pic-b.png' alt="Forum" />
                                    <img src='<%=ModulePath%>images/forum-pic-a.png' alt="Forum" />
                                    <br>
                                    <img src='<%=ModulePath%>images/forum-pic-b.png' alt="Forum" />
                                    <img src='<%=ModulePath%>images/forum-pic-a.png" alt="Forum"' />
                                </div>
                            </div>
                            <div class="forum-right-toggle-content last">
                                <span class="num">3</span>
                                <span class="title">shared files</span>
                                <a href="#" class="toggle-content active right">Collapse</a>
                                <div class="clearfix"></div>
                                 <div class="members-expand active">
                                    <ul>
                                        <li>
                                            <a href="#">Marketing-Service-Powercouple.pdf   |  PDF</a>
                                        </li>
                                        <li>
                                            <a href="#">The-Big-Guide-to-Marketing-Performance-Management.pdf  |  PDF</a>
                                        </li>
                                        <li>
                                            <a href="#">Marketing Performance Management – A 3 Part E-Book How To!  |  DOC</a>
                                        </li>
                                    </ul>
                                </div> 
                            </div>
                        </div>--%>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
<%--   </contenttemplate>
</asp:UpdatePanel>--%>
