<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainView.ascx.cs" Inherits="ClearAction.Modules.WebCast.MainView" %>
<link href="/DesktopModules/ClearAction_SolveSpaces/Scripts/context/jquery.contextMenu.css" rel="stylesheet" />
<%--<%@ Register TagPrefix="blog" TagName="management" Src="controls/ManagementPanel.ascx" %>--%>

<script src="https://static.filestackapi.com/v3/filestack.js"></script>

<%@ Register TagPrefix="blog" TagName="topheader" Src="controls/TopHeader.ascx" %>

<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/module/common.js"></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/context/jquery.contextMenu.js"></script>
<link href="/DesktopModules/ClearAction_SolveSpaces/Scripts/context/jquery.contextMenu.css" rel="stylesheet" />
<script src="https://static.filestackapi.com/v3/filestack.js"></script>
<script src="/DesktopModules/ClearAction_WebCast/Scripts/View.js"></script>

<%--<link type="text/css" href='<%=ModulePath %>js/ClearActionapi.js' />--%>

<script type="text/javascript">
    var AF_CurrentCatID = -1;
    var oKey = '<%=GetAPIKey%>';// 'AUQ4R8zKvSKCs9gudWJlxz';
    var AF_IsMyVault = getPathVariable('IsMyVault');
    var AF_CurrentStatus = getPathVariable('Show');//1'; // Default is Zero / 1/2
    var AF_ComponentID = getPathVariable('ComponentID');
</script>

<style type="text/css">
    .gotoRegisterLink {
        cursor: pointer;
        color: #6992c0;
        font-size: 14px;
        line-height: 24px;
        font-weight: 300;
    }

    .oldgotoRegisterLink {
        cursor: pointer;
        color: #6992c0;
        font-size: 14px;
        line-height: 24px;
        font-weight: 300;
    }

    .gotoAfterRegisterLink {
        color: #6992c0;
        font-size: 14px;
        line-height: 24px;
        font-weight: 300;
    }
    .gotoJoinLInk {
        color:orange !important;
        font-size: 14px !important;
        line-height: 24px !important;
        font-weight: 300 !important;
    }
</style>

<%--<blog:management runat="server" id="ctlManagement" />--%>
<div class="clearfix"></div>

<asp:Panel ID="pnlTopHeader" runat="server">
    <%--<blog:topheader runat="server" ID="topheader" />--%>
    <div class="row">
        <div class="col-md-12">
            <div class="solve-spaces-top-nav">
                <div class="forum-title">
                    <h1>Events</h1>
                </div>
                <div class="forum-nav">
                    <div class="forum-topics">
                        <div class="forum-topics-title">
                            <asp:Label ID="lblTopicCount" runat="server"></asp:Label>
                        </div>
                        <div class="forum-topics-new" style="padding-left: 28px !important;">
                            <asp:HyperLink ID="hyperlinkNew" runat="server" Text="New"></asp:HyperLink>
                        </div>
                    </div>
                </div>
                <div class="solve-spaces-nav">
                    <asp:LinkButton ID="btnMyVault" runat="server" Text="<strong>My Vault</strong>Curated for me" OnClick="btnMyVault_Click"></asp:LinkButton>
                    <asp:LinkButton ID="btnShowAll" runat="server" Text="<strong>Show All</strong>Everything at all levels" OnClick="btnShowAll_Click"></asp:LinkButton>
                </div>
                <div class="forum-search">
                    <!-- Sachin : Please dont remove the div below, we need this -->
                    <asp:Panel ID="pnlBlogSearch" runat="server" DefaultButton="lnkSearch" Style="position: relative">
                        <asp:TextBox ID="txtSearch" runat="server" MaxLength="50" onkeypress="" />
                        <asp:LinkButton runat="server" ID="lnkSearch" CssClass="searchbutton" OnClick="lnkSearch_Click" />
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div id="CA_Blog_dvCatContainer" class="solve-spaces-middle-nav">
                <nav>
                    <ul>
                        <li class="active">
                            <asp:LinkButton ID="btnAllCat" CatId="-1" runat="server" OnClick="btnAllCat_Click"><div class="table"><div class="table-cell">All</div></div></asp:LinkButton>
                        </li>
                        <asp:Repeater ID="dataListGlobalCat" runat="server" OnItemDataBound="dataListGlobalCat_ItemDataBound">
                            <ItemTemplate>
                                <li id="categoryId">
                                    <asp:HyperLink ID="LinkButton1" runat="server"> </asp:HyperLink>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </nav>
            </div>
        </div>
    </div>
    <%--Commented by @SP--%>

   <%-- <div class="row">
        <div class="col-md-12">
            <div id="CA_Blog_dvCatContainer1" class="solve-spaces-middle-nav" style="background-color: #84A6CB !important">
                <nav>
                    <ul>
                        <li class="active" compid="-1">
                            <asp:LinkButton ID="lnkEventAll" EventType="-1" runat="server" OnClick="lnkEventAll_Click"><div class="table"><div class="table-cell">All</div></div></asp:LinkButton>
                        </li>
                        <li compid="4">
                            <asp:LinkButton ID="lnkEventCommunityCalls" EventType="4" runat="server" OnClick="lnkEventCommunityCalls_Click"><div class="table"><div class="table-cell">Community<br />Calls</div></div></asp:LinkButton>
                        </li>
                        <li compid="5">
                            <asp:LinkButton ID="lnkWebCastCall" EventType="5" runat="server" OnClick="lnkWebCastCall_Click"><div class="table"><div class="table-cell">Webcast<br />Conversations<sup>TM</sup></div></div></asp:LinkButton>
                        </li>
                    </ul>
                </nav>
            </div>
        </div>
    </div>--%>
</asp:Panel>
<div class="clearfix"></div>
<asp:Panel ID="pnlListings" runat="server">
    <div class="cnt_wraper_lt col-sm-8">
        <div class="clearfix"></div>
        <div class="forum-active-topics col-sm-8 forum-intro-match-height">
            <div class="row">
                <div id="dvBlogStatusFilter">
                    <div class="filter-bar" id="filterbar" runat="server">
                        <div class="col-md-8">
                            <ul class="filter-spaces button-group filter-button-group" style="margin-left: 40px; margin-bottom: 40px;">
                                <li class="active" statusid="0">
                                    <div class="filter-button">
                                        <asp:Button runat="server" ID="btnAllBlog" CssClass="btnctrl" Text="All" CausesValidation="false" OnClick="btnAllBlog_Click" />
                                    </div>
                                </li>
                                <li statusid="1">
                                    <div class="filter-button">
                                        <img src="/images/img_18.png" alt="" width="9" height="9">
                                        <asp:Button runat="server" ID="btntodo" CssClass="btnctrl" Text="To Do" CausesValidation="false" OnClick="btntodo_Click" />
                                    </div>
                                </li>
                                <li statusid="2">
                                    <div class="filter-button">
                                        <img src="/images/img_17.png" alt="" width="9" height="9">
                                        <asp:Button runat="server" ID="btnDoneBlog" CssClass="btnctrl" Text="Done" CausesValidation="false" OnClick="btnDoneBlog_Click" />
                                    </div>
                                </li>
                            </ul>
                        </div>

                        <div class="col-md-4 text-right">
                            <asp:DropDownList ID="ddSortBy" runat="server" CssClass="forum-select" AutoPostBack="true" OnSelectedIndexChanged="ddSortBy_SelectedIndexChanged">
                                <asp:ListItem Text="Sort by" Value="-1" class="CA_SortOptions"></asp:ListItem>
                                <asp:ListItem Text="Alpha a-z" Value="0" class="CA_SortOptions"></asp:ListItem>
                                <asp:ListItem Text="Alpha z-a" Value="1" class="CA_SortOptions"></asp:ListItem>
                                <asp:ListItem Text="By presenter  a-z" Value="2" class="CA_SortOptions"></asp:ListItem>
                                <asp:ListItem Text="By presenter  z-a" Value="3" class="CA_SortOptions"></asp:ListItem>
                                <asp:ListItem Text="Most replies" Value="9" class="CA_SortOptions"></asp:ListItem>

                                <asp:ListItem Text="Occurring within (today - 7 days)" Value="7" class="CA_SortOptions"></asp:ListItem>
                                <asp:ListItem Text="Occurring within (today + 7 days)" Value="8" class="CA_SortOptions"></asp:ListItem>
                                <asp:ListItem Text="Occurring this month" Value="10" class="CA_SortOptions"></asp:ListItem>
                                <asp:ListItem Text="All upcoming" Value="11" class="CA_SortOptions"></asp:ListItem>
                                <asp:ListItem Text="All past" Value="12" class="CA_SortOptions"></asp:ListItem>
                                <asp:ListItem Text="All" Value="-2" class="CA_SortOptions"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

 <%--           <div class="clearfix"></div>--%>
            <%--<div class="events-page-title">--%>
                <%--RG: Was asked to remove bredcrumbs --%>
                <!--
                <asp:Literal ID="hyperlinkBreadcumb" runat="server"></asp:Literal>
                -->

               <%-- <asp:Label ID="lblHeader" runat="server"></asp:Label>--%>

                <%-- <div id="paging1" runat="server" style="float: right; display: none">
                            <asp:ImageButton ID="NavFirstBtn1" CssClass="forum-prev" Style="cursor: pointer;" runat="server" AlternateText="First" OnClick="NavFirstBtn_Click" ImageUrl="~/DesktopModules/ClearAction_WebCast/images/forum-page-prev-first.png" ToolTip="First" />
                            <asp:ImageButton ID="NavImagePrev1" CssClass="forum-prev" Style="cursor: pointer;" runat="server" AlternateText="Prev" OnClick="NavImagePrev_Click" ImageUrl="~/DesktopModules/ClearAction_WebCast/images/forum-page-prev.png" ToolTip="Previous" />
                            <asp:Label ID="lblPagerInfo1" runat="server" Text="1" CssClass="forum-page-count"></asp:Label>
                            <asp:ImageButton ID="NavImageNext1" CssClass="forum-prev" Style="cursor: pointer;" runat="server" AlternateText="Next" OnClick="NavImageNext_Click" ImageUrl="~/DesktopModules/ClearAction_WebCast/images/forum-page-next.png" ToolTip="Next" />
                            <asp:ImageButton ID="NavImageLast1" CssClass="forum-prev" Style="cursor: pointer;" runat="server" AlternateText="Last" OnClick="NavImageLast_Click" ImageUrl="~/DesktopModules/ClearAction_WebCast/images/forum-page-next-last.png" ToolTip="Last" />

                        </div>--%>
          <%--  </div>--%>
        </div>
        <div class="forum-right-col col-sm-4 forum-intro-match-height" style="height: 193px;"></div>
        <div class="clearfix"></div>
        <asp:Literal ID="ltrlCtrl" runat="server"></asp:Literal>
        <asp:Panel ID="pnlCC" runat="server">
            <div class="forum-active-topics col-sm-8 forum-intro-match-height">
                   <!--Chnages done by @SP-->
               <%-- <div class="container-fluid community cc">
                    <div>
                        <div>
                            <span>Community Calls</span>
                        </div>
                    </div>
                </div>--%>
                <div class="clear"></div>
                <div class="events-cc">
                    <asp:Literal ID="ltrCCctrl" runat="server"></asp:Literal>
                </div>
                <div class="clearfix"></div>
                <div class=" more-events">
                    <ul>
                        <li>
                            <asp:HyperLink ID="hyperlinkCommunity" runat="server" Visible="false">
                                <img src="<%=ModulePath %>images/dwon_arrow.png" width="15" height="20" alt="img_17"><span> More Community Calls</span>
                            </asp:HyperLink>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="forum-right-col col-sm-4 forum-intro-match-height" style="height: 193px;"></div>
            <div class="clearfix"></div>
        </asp:Panel>
        <asp:Panel ID="pnlWCC" runat="server">
            <div class="forum-active-topics col-sm-8 forum-intro-match-height">
                <%--<div class="container-fluid community wcc">
                    <div>
                        <div>
                          <span>Webcast Conversations<sup>TM</sup></span>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>--%>
                <div class="events-wcc">
                    <asp:Literal ID="ltrWcctrl" runat="server"></asp:Literal>
                </div>
                <div class=" more-events">
                    <ul>
                        <li>
                            <asp:HyperLink ID="hyperWebCastCalls" runat="server" Visible="false">
                                <img src="<%=ModulePath %>images/dwon_arrow.png" width="15" height="20" alt="img_17"><span> More WebCast Calls</span>
                            </asp:HyperLink>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="forum-right-col col-sm-4 forum-intro-match-height" style="height: 193px;"></div>
            <div class="clearfix"></div>
        </asp:Panel>
    </div>
</asp:Panel>
<asp:Panel ID="pnlDetail" runat="server" Visible="false" Style="padding-left: 20px;">
    <div class="cnt_wraper_lt col-sm-8">
        <div class="forum-active-topics  border col-sm-8 forum-intro-match-height">
            <div class="event-detail-container">
                <%-- RG: Was asked to remove breadcrumbs--%>
                <!--
                <div class="row">
                    <div class="events-page-title">
                        <asp:Literal ID="ltrlDetailBreadcumb" runat="server"></asp:Literal>
                    </div>
                </div>
                -->
                <div class="row">
                    <asp:Literal ID="ltrDetail" runat="server"></asp:Literal>
                </div>
                <div class="forum-more-bottom" style="display: block;">
                    <div>
                        <div class="button-top">Join the conversation</div>
                        <div style="float: right">
                            <asp:Panel ID="PanelCommentTop" runat="server" class="forum-tooltips InnerAttachButtons">
                                <asp:Image ID="imgUploadImagesTop" runat="server" class="CA_UserMenu" status="Completed" isselfassigned="-1" Style="cursor: pointer;" src="/images/more_icon.png" />
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="clearfix"></div>

                    <div>
                        <div style="display: none">
                            <asp:TextBox runat="server" ID="UploadedAttachmentSavePost" TextMode="MultiLine" CssClass="UploadedAttachmentSavePost multilineText UploadedAttachmentSavePostHide" Rows="3" Columns="20"></asp:TextBox>

                            <asp:TextBox runat="server" ID="TextBoxAttachmentData" TextMode="MultiLine" CssClass="TextBoxAttachmentData multilineText UploadedAttachmentSavePostHide" Rows="3" Columns="20"></asp:TextBox>
                        </div>
                        <asp:TextBox ID="txtComments" runat="server" placeholder="Reply: Your thoughts" MaxLength="5000" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="*Require" ForeColor="Red" ValidationGroup="valMainComments" ControlToValidate="txtComments" runat="server" />

                        <div class="clear"></div>
                    </div>
                    <input type="hidden" id="hiddenUserId" value='<%=this.UserInfo.UserID%>' />
                    <input type="hidden" id="hiddenIsAdminUser" value='<%=IsAuthorize(this.UserInfo.UserID)%>' />
                    <div id="UploadedAttachment">
                    </div>
                    <asp:Button ID="lnkSave" runat="server" Text="POST REPLY" CausesValidation="true" OnClick="lnkSave_Click" CssClass="button-bottom" ValidationGroup="valMainComments" OnClientClick='saveAttachmentDataWhenPost(this)'></asp:Button>

                    <div class="forum-posts-tab ui-tabs ui-corner-all ui-widget ui-widget-content">
                        <div class="clearfix"></div>
                        <div class="forum-tab-one active ui-tabs-panel ui-corner-bottom ui-widget-content" id="tabs-one" aria-labelledby="ui-id-4" role="tabpanel" aria-hidden="false">
                            <asp:Repeater ID="rptComments" runat="server" OnItemCommand="rptComments_ItemCommand" OnItemDataBound="rptComments_ItemDataBound">
                                <ItemTemplate>
                                    <div class="forum-tab-one active ui-tabs-panel ui-corner-bottom ui-widget-content" id="tabs-one" style="border-bottom: solid 1px #c2c3c5" aria-labelledby="ui-id-4" role="tabpanel" aria-hidden="false">
                                        <div class="forum-comments-row insights-discussion-left-col" id="dvEachTopic_<%#DataBinder.Eval(Container.DataItem, "CommentID")%>">

                                            <div class="forum-comments-content" style="<%#GetBackgroundImage(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>; border-bottom: none; background-size: 49px 49px;">
                                                <h4><%#Eval("AuthorLinkUrl") %><span>&nbsp;::&nbsp;<%#Eval("GetCreatedDateHumanFriendly") %></span>&nbsp;::&nbsp;<small><asp:Label ID="lblReplyCount" runat="server"></asp:Label></b>&nbsp;&nbsp;Replies</small></h4>

                                                <asp:Panel ID="PanelInnerComment" runat="server" class="forum-tooltips InnerAttachButtons title-tooltip" Style="float: right" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>'>
                                                    <asp:Image runat="server" ID="InnerImagePostAcctachment"
                                                        Style="cursor: pointer; margin-top: 15px; margin-right: 2px;" src="/images/more_icon.png"
                                                        alt="Tips" tid="0"
                                                        cid='<%#DataBinder.Eval(Container.DataItem, "WCCId")%>'
                                                        commentid='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>'
                                                        status="Completed" isselfassigned="2" class="CA_UserMenu"
                                                        IsDeletedshow='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>'
                                                        isEditAllow='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>' />
                                                </asp:Panel>


                                                <div class="ReplyContentSectionBody ReplyContentSection_<%#DataBinder.Eval(Container.DataItem, "CommentID")%>" cid="<%#DataBinder.Eval(Container.DataItem, "CommentID")%>">
                                                    <%#DataBinder.Eval(Container.DataItem, "Comment")%> &nbsp;&nbsp;&nbsp;&nbsp;
                                                </div>
                                                <asp:HiddenField ID="CommentID" runat="server" Value='<%#Eval("CommentID") %>' />

                                                <span id='hyper_<%#Eval("CommentID") %>'>&nbsp;<a class="forum-reply" style="cursor: pointer;" onclick='javascript:Show(<%#Eval("CommentID") %>)'>Reply</a></span>

                                                <a onclick='return GetLikesCount(<%#Eval("CommentID")%>,1,"Comment")' style="padding-left: 10px; padding-right: 10px; cursor: pointer;">
                                                    <span id="Main_count<%#Eval("CommentID") %>">
                                                        <img id='imgLikeStatusM_<%#Eval("CommentID")%>' class="like_imglink" src="<%# String.Format("{0}/DesktopModules/ClearAction_WebCast/Images/{1}", WebServicePath, GetImageName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CommentID")))) %>" />
                                                        &nbsp;LIKE&nbsp;<label id="lblLikeCountM_<%#Eval("CommentID")%>"><%#DataBinder.Eval(Container.DataItem, "GetLikesCount")%></label></span></a><span style='<%#String.Format("display:{0}",Convert.ToInt16(DataBinder.Eval(Container.DataItem,"TotalAttachment")) > 0 ? "inline": "none")%>; padding-left: 10px; padding-right: 10px; cursor: pointer' cid='<%#Eval("CommentID")%>' class="SeeAttachedMediaLinkContent" onclick="SeeAttachedMediaList(this,'top')" status="show"><img style="width: 20px; height: 20px;" src="<%# String.Format("{0}/DesktopModules/Activeforums/images/{1}", WebServicePath, "CAAttach.png") %>" />&nbsp;ATTACHED MEDIA&nbsp;
                                                        </span>
                                                <div id="top_<%#Eval("CommentID")%>" class="AttchmentListSection AttchmentListSection_<%#Eval("CommentID")%>"></div>


                                                <asp:ImageButton Style="position: relative; top: 2px;" ID="imgdelete" OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');" CommandName="delete" runat="server" AlternateText="Delete" ImageUrl="~/DesktopModules/ClearAction_WebCast/Images/small-delete.png" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>' />
                                                <div class="ReplyEditContentSection_<%#DataBinder.Eval(Container.DataItem, "CommentID")%>" style="display: none;">

                                                    <div class="UserReplyNameSection" style="width: 98%; float: left;">
                                                        <div>Edit my reply to <span><%#Eval("AuthorLinkUrl") %> ,</span></div>
                                                    </div>
                                                    <asp:TextBox Text='<%#Eval("Comment") %>' ID="txtQuickReplyEditOne" runat="server" TextMode="MultiLine" CssClass='multilineText ReplyEditContent' Rows="3" Columns="20" MaxLength="5000"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="reqFieldMainOne" runat="server" ControlToValidate="txtQuickReplyEditOne" CssClass="EditPostValidation" Display="Dynamic"
                                                        ValidationGroup='<%#String.Format("FirstLevel_{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    <div style="display: none">

                                                        <asp:TextBox PlaceHolder="Reply: Your thoughts" ID="TextBoxAttachmentContentOne" runat="server" TextMode="MultiLine" CssClass='multilineText ReplyEditContentAttachmentData' Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>

                                                        <asp:HiddenField ID="HiddenFieldCurrentContentId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>' />

                                                        <input type="hidden" value="" id="HiddenReplyEditContent_<%#DataBinder.Eval(Container.DataItem, "CommentID")%>" />
                                                    </div>
                                                     <div id="topview_<%#Eval("CommentID")%>" class="AttchmentListSection AttchmentListSection_<%#Eval("CommentID")%>"></div>
                                                    <asp:LinkButton
                                                        ID="EditLevelOneReply"
                                                        runat="server"
                                                        CommandName="EditReply" CausesValidation="true"
                                                        CssClass="EditPostReplyButton"
                                                        ValidationGroup='<%#string.Format("FirstLevel_{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>'>
                                                                 POST EDITED REPLY</asp:LinkButton>
                                                    <a onclick="EditReplyCancle(this)" class="CancelPostReplyButton" cid="<%#DataBinder.Eval(Container.DataItem, "CommentID")%>">CANCEL</a>
                                                    <asp:LinkButton
                                                        ID="LinkButtonDeleteReply"
                                                        OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');"
                                                        CommandName="delete"
                                                        CssClass="deleteReplyOne"
                                                        runat="server"
                                                        AlternateText="Delete"
                                                        CommandArgument="deleteInnerReply"
                                                        Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>'>
                                                           Delete My Reply
                                                    </asp:LinkButton>
                                                    <%--PROPER--%>
                                                </div>
                                                <div class="forum-comments-row">
                                                    <div id='<%#Eval("CommentID") %>' style="display: none; padding-top: 5px; position: relative;">
                                                        <div class="dvUserReply" style="width: 95%; float: left; margin-top: 15px; margin-bottom: 5px;">
                                                            <div>
                                                                Reply to <span><%#DataBinder.Eval(Container.DataItem, "AuthorLinkUrl")%> ,</span>

                                                            </div>
                                                        </div>
                                                        <%--Vertical dots when posting comment on First Level--%>
                                                        <asp:Panel ID="Panel11" runat="server" class="forum-tooltips">
                                                            <asp:Image runat="server" ID="ImagePostAcctachment"
                                                                Style="cursor: pointer; margin-top: 15px; margin-right: 2px;" src="/images/more_icon.png"
                                                                alt="Tips"
                                                                cid='<%#DataBinder.Eval(Container.DataItem, "WCCId")%>'
                                                                commentid='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>'
                                                                status="Completed" isselfassigned="5" class="CA_UserMenu" />
                                                        </asp:Panel>

                                                        <asp:TextBox ID="txtInnerQuickReply" runat="server" PlaceHolder="Reply to " TextMode="MultiLine" CssClass="multilineText" Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqField" runat="server" ControlToValidate="txtInnerQuickReply"
                                                            ValidationGroup='<%#String.Format("Child_{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <div id="UploadedAttachment_<%#DataBinder.Eval(Container.DataItem, "CommentID")%>"></div>

                                                        <div style="float: right; padding-bottom: 40px">
                                                            <asp:Button ID="lnkSve"
                                                                runat="server"
                                                                CssClass="btn btn-primary"
                                                                currentPostContenetId='<%#Eval("CommentID") %>'
                                                                CommandArgument="SaveReply"
                                                                ValidationGroup='<%#String.Format("Child_{0}", Convert.ToString(DataBinder.Eval(Container.DataItem, "CommentID"))) %>'
                                                                CommandName="SaveDisscussion"
                                                                Text="POST"
                                                                OnClientClick='saveAttachmentDataWhenPost(this)'></asp:Button>
                                                            <asp:ImageButton ID="ImageButton2" Visible="false" CommandArgument="SaveReply" Style="cursor: pointer; margin-top: 5px; font-size: 13px;" ValidationGroup='<%#String.Format("Child_{0}", Convert.ToString(DataBinder.Eval(Container.DataItem, "CommentID"))) %>' CommandName="SaveDisscussion" runat="server" AlternateText="Reply" ImageUrl="~/DesktopModules/ClearAction_WebCast/Images/small-reply.png" />
                                                            <span id='hyper1_<%#Eval("CommentID") %>' style="float: right; margin-top: 3px; margin-left: 20px;">
                                                                <a style="cursor: pointer" onclick='javascript:Hide(<%#Eval("CommentID") %>)'>Cancel</a>
                                                            </span>
                                                        </div>
                                                        <asp:HiddenField ID="hdnCommentId" runat="server" Value='<%#Eval("CommentID") %>' />



                                                    </div>
                                                    <div class="subcomment" style="padding-left: 80px; margin-bottom: 10%; position: relative">
                                                        <asp:Repeater ID="rptInnerComments" runat="server" OnItemCommand="rptInnerComments_ItemCommand" OnItemDataBound="rptInnerComments_ItemDataBound">
                                                            <ItemTemplate>
                                                                <div class="forum-comments-sub-content" style="<%#GetBackgroundImage(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>; padding-left: 60px; background-size: 49px 49px;" id="dvEachTopic_<%#DataBinder.Eval(Container.DataItem, "CommentID")%>">
                                                                    <h4><%#Eval("AuthorLinkUrl") %><span>&nbsp;::&nbsp;<%#Eval("GetCreatedDateHumanFriendly") %></span>&nbsp;::&nbsp;<small><asp:Label ID="lblReplyinnerCount" runat="server"></asp:Label></b>&nbsp;&nbsp;Replies</small></h4>
                                                                    <%-- Vertical Dots on Next Level For editing/update --%>
                                                                    <asp:Panel ID="Panel11" runat="server" class="forum-tooltips" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>'>
                                                                        <asp:Image runat="server" ID="ImagePostAcctachment"
                                                                            Style="cursor: pointer; margin-top: 15px; margin-right: 2px;" src="/images/more_icon.png"
                                                                            alt="Tips"
                                                                            cid='<%#DataBinder.Eval(Container.DataItem, "WCCId")%>'
                                                                            commentid='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>'
                                                                            status="Completed" isselfassigned="6" class="CA_UserMenu"
                                                                            IsDeletedshow='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>'
                                                                            isEditAllow='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>' />
                                                                    </asp:Panel>
                                                                    <p>
                                                                        <%#Eval("Comment") %>
                                                                    </p>
                                                                    <span id='hyper1_<%#Eval("CommentID") %>'><a class="forum-reply" style="cursor: pointer;" onclick='javascript:Show(<%#Eval("CommentID") %>)'>Reply</a></span>

                                                                    <a onclick='return GetLikesCount(<%#Eval("CommentID")%>,2,"Comment")' href="#" style="padding-left: 10px; padding-right: 10px; cursor: pointer;">
                                                                        <span id="Span_count<%#Eval("CommentID") %>">
                                                                            <img class="like_imglink" id="imgLikeStatus_<%#Eval("CommentID")%>" src="<%# String.Format("{0}/DesktopModules/ClearAction_WebCast/images/{1}", WebServicePath, GetImageName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CommentID")))) %>" />
                                                                            &nbsp;LIKE&nbsp;<label id="lblLikeCount_<%#Eval("CommentID")%>"><%#DataBinder.Eval(Container.DataItem, "GetLikesCount")%></label>
                                                                        </span>
                                                                    </a>


                                                                    

                                                                    <span style='<%#String.Format("display:{0}",Convert.ToInt16(DataBinder.Eval(Container.DataItem,"TotalAttachment")) > 0 ? "inline": "none")%>' cid="<%#Eval("CommentID")%>" class="SeeAttachedMediaLinkContent" onclick="SeeAttachedMediaList(this,'child')" status="show">
                                                                        <img style="width: 20px; height: 20px;" src="<%# String.Format("{0}/DesktopModules/Activeforums/images/{1}", WebServicePath, "CAAttach.png") %>" />
                                                                        &nbsp;ATTACHED MEDIA&nbsp;
                                                                    </span>
                                                                    <asp:ImageButton Style="top: 2px;" ID="imgdelete" OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');" CommandArgument='<%#Eval("CommentID") %>' CommandName="delete" runat="server" AlternateText="Delete" ImageUrl="~/DesktopModules/ClearAction_WebCast/images/small-delete.png" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>' />
                                                                  <div id="child_<%#Eval("CommentID")%>" class="AttchmentListSection AttchmentListSection_<%#Eval("CommentID")%>"></div>
                                                                   
                                                                    <%-- IInd level :Edit/Delete sub/menu start here--%>
                                                                    <div id="UploadedAttachment">
                                                                    </div>

                                                                    <div id='<%#Eval("CommentID") %>' style="display: none; padding-top: 5px; z-index: 999;">
                                                                            <div class="dvUserReply" style="width: 95%; float: left; margin-top: 15px; margin-bottom: 5px;">
                                                                                <div>
                                                                                    Reply to <span><%#DataBinder.Eval(Container.DataItem, "AuthorLinkUrl")%> ,</span>

                                                                                </div>
                                                                            </div>
                                                                            <%--Add New links for Submenu for attachements on third level--%>
                                                                            <asp:Panel ID="PanelInnerComment" runat="server" class="forum-tooltips InnerAttachButtons" Style="float: right">
                                                                                <asp:Image runat="server" ID="InnerImagePostAcctachment"
                                                                                    Style="cursor: pointer; margin-top: 15px; margin-right: 2px;" src="/images/more_icon.png"
                                                                                    alt="Tips"
                                                                                    cid='<%#DataBinder.Eval(Container.DataItem, "WCCId")%>'
                                                                                    commentid='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>'
                                                                                    status="Completed" isselfassigned="7" class="CA_UserMenu" />
                                                                            </asp:Panel>
                                                                            <asp:TextBox ID="txtInnerQuickReply" runat="server" PlaceHolder="Reply to " TextMode="MultiLine" CssClass="multilineText" Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="reqField" runat="server" ControlToValidate="txtInnerQuickReply"
                                                                                ValidationGroup='<%#String.Format("Child_{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                            <div id="UploadedAttachment_<%#Eval("CommentID") %>" ></div>
                                                                            <div style="float: right;">
                                                                                <asp:ImageButton ID="ImageButton2" Visible="false" CommandArgument="SaveReply" Style="cursor: pointer; margin-top: 5px; font-size: 13px;" ValidationGroup='<%#String.Format("Child_{0}", Convert.ToString(DataBinder.Eval(Container.DataItem, "CommentID"))) %>' CommandName="SaveDisscussion" runat="server" AlternateText="Reply" ImageUrl="~/DesktopModules/ClearAction_WebCast/Images/small-reply.png" />
                                                                                <asp:Button ID="lnkSve"
                                                                                    OnClientClick='saveAttachmentDataWhenPost(this)'
                                                                                    runat="server"
                                                                                    CssClass="btn btn-primary"
                                                                                    CommandArgument="SaveReply"
                                                                                    ValidationGroup='<%#String.Format("Child_{0}", Convert.ToString(DataBinder.Eval(Container.DataItem, "CommentID"))) %>'
                                                                                    CommandName="SaveDisscussion"
                                                                                    Text="POST"></asp:Button>
                                                                                <span id='hyper2_<%#Eval("CommentID") %>' style="float: right; margin-top: 3px; margin-left: 20px;">
                                                                                    <a style="cursor: pointer" onclick='javascript:Hide(<%#Eval("CommentID") %>)'>Cancel</a>
                                                                                </span>




                                                                            </div>
                                                                            <asp:HiddenField ID="hdnInnerCommentID" runat="server" Value='<%#Eval("CommentID") %>' />






                                                                        </div>
                                                                    <div style="display: none" id="childaction_<%#Eval("CommentID")%>">
                                                                        <div class="dvUserReply" style="width: 95%; float: left; margin-top: 10px; margin-bottom: 15px;">
                                                                            <div>
                                                                               Edit my Reply to <span><%#DataBinder.Eval(Container.DataItem, "AuthorLinkUrl")%> ,</span>

                                                                            </div>
                                                                        </div>
                                                                        <asp:TextBox Text='<%#Eval("Comment") %>' ID="txtQuickReplyEditOne" runat="server" TextMode="MultiLine" Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuickReplyEditOne"
                                                                            ValidationGroup='<%#String.Format("ChildEdit_{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                                        <asp:HiddenField ID="HiddenFieldCurrentContentId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>' />
                                                                         <div id="UploadedAttachmentEdit_<%#Eval("CommentID") %>" class="AttchmentListSection AttchmentListSection_<%#Eval("CommentID")%>"></div>
                                                                        <input type="hidden" value="" id="HiddenReplyEditContent_<%#DataBinder.Eval(Container.DataItem, "CommentID")%>" />
                                                                        <asp:LinkButton
                                                                            ID="EditLevelOneReply"
                                                                            runat="server"
                                                                            CommandName="EditReply"
                                                                            CssClass="EditPostReplyButton"
                                                                            ValidationGroup='<%#string.Format("ChildEdit_{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>'>
                                                                 POST EDITED REPLY</asp:LinkButton>

                                                                        <a onclick="EditReplyCancle(this)" class="CancelPostReplyButton" cid="<%#DataBinder.Eval(Container.DataItem, "CommentID")%>">CANCEL</a>


                                                                        <asp:LinkButton
                                                                            ID="LinkButtonDeleteReply"
                                                                            OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');"
                                                                            CommandName="delete"
                                                                            CssClass="deleteReplyOne"
                                                                            runat="server"
                                                                            AlternateText="Delete"
                                                                            CommandArgument="deleteInnerReply"
                                                                            Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>'>
                                                           Delete My Reply
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                     <div class="forum-comments-row" style="padding-bottom: 40px">
                                                                        <asp:Repeater ID="rptInnerCommentlevel2" runat="server" OnItemCommand="rptInnerCommentlevel2_ItemCommand">
                                                                            <ItemTemplate>
                                                                                <div class="forum-comments-sub-content" style="<%#GetBackgroundImage(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>; padding-left: 60px; background-size: 49px 49px;" id='dvEachTopic_<%#DataBinder.Eval(Container.DataItem, "CommentID")%>'>
                                                                                    <h4><%#Eval("AuthorLinkUrl") %><span>&nbsp;::&nbsp;<%#Eval("GetCreatedDateHumanFriendly") %></span></h4>
                                                                                    <!- Vertical Dots on Last Level For Edit-!>
                                                                         <asp:Panel ID="PanelInnerComment" runat="server" class="forum-tooltips InnerAttachButtons" Style="float: right" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>'>
                                                                             <asp:Image runat="server" ID="InnerImagePostAcctachment"
                                                                                 Style="cursor: pointer; margin-top: 15px; margin-right: 2px;" src="/images/more_icon.png"
                                                                                 alt="Tips" tid="0"
                                                                                 cid='<%#DataBinder.Eval(Container.DataItem, "WCCId")%>'
                                                                                 commentid='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>'
                                                                                 status="Completed" isselfassigned="9" class="CA_UserMenu"
                                                                                 IsDeletedshow='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>'
                                                                                 isEditAllow='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>' />
                                                                         </asp:Panel>
                                                                                    <p>
                                                                                        <%#Eval("Comment") %>
                                                                                    </p>
                                                                                    <%--<span id='hyper1_<%#Eval("CommentID") %>'><a class="forum-reply" style="cursor: pointer;" onclick='javascript:Show(<%#Eval("CommentID") %>)'>Reply</a></span>--%>

                                                                                    <a onclick='return GetLikesCount(<%#Eval("CommentID")%>,3,"Comment")' href="#" style="padding-left: 10px; padding-right: 10px; cursor: pointer;">
                                                                                        <span id="Span_count<%#Eval("CommentID") %>">
                                                                                            <img class="like_imglink" id="imgLikeStatus_<%#Eval("CommentID")%>" src="<%# String.Format("{0}/DesktopModules/ClearAction_WebCast/images/{1}", WebServicePath, GetImageName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CommentID")))) %>" />
                                                                                            &nbsp;LIKE&nbsp;<label id="lblLikeCount_<%#Eval("CommentID")%>"><%#DataBinder.Eval(Container.DataItem, "GetLikesCount")%></label>
                                                                                        </span>
                                                                                    </a>
                                                                                    <asp:ImageButton Style="top: 2px;" ID="imgdelete" OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');" CommandArgument='<%#Eval("CommentID") %>' CommandName="delete" runat="server" AlternateText="Delete" ImageUrl="~/DesktopModules/ClearAction_WebCast/images/small-delete.png" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>' />

                                                                                    <span style='<%#String.Format("display:{0}",Convert.ToInt16(DataBinder.Eval(Container.DataItem,"TotalAttachment")) > 0 ? "inline": "none")%>' cid="<%#Eval("CommentID")%>" class="SeeAttachedMediaLinkContent" onclick="SeeAttachedMediaList(this,'child')" status="show">
                                                                                        <img style="width: 20px; height: 20px;" src="<%# String.Format("{0}/DesktopModules/Activeforums/images/{1}", WebServicePath, "CAAttach.png") %>" />
                                                                                        &nbsp;ATTACHED MEDIA&nbsp;
                                                                                    </span>

                                                                                    <div id="child_<%#Eval("CommentID")%>" class="AttchmentListSection AttchmentListSection_<%#Eval("CommentID")%>"></div>

                                                                                    <%--<asp:ImageButton ID="imgReply" CommandName="like" runat="server" AlternateText="Like" ImageUrl='<%#String.Format("~/DesktopModules/ClearAction_WebCast/images/{0}", GetImageName(DataBinder.Eval(Container.DataItem, "CommentID"))) %>' CommandArgument='<%#Eval("CommentID") %>' />--%>

                                                                                    <div style="display: none" id="childaction_<%#Eval("CommentID")%>">
                                                                                        <div class="dvUserReply" style="width: 95%; float: left; margin-top: 15px; margin-bottom: 5px;">
                                                                                            <div>
                                                                                              Edit my  Reply to <span><%#DataBinder.Eval(Container.DataItem, "AuthorLinkUrl")%> ,</span>

                                                                                            </div>
                                                                                        </div>
                                                                                        <asp:TextBox Text='<%#Eval("Comment") %>' ID="txtQuickReplyEditOne" runat="server" TextMode="MultiLine" Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuickReplyEditOne"
                                                                                            ValidationGroup='<%#String.Format("ChildEdit_{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                                                        <asp:HiddenField ID="HiddenFieldCurrentContentId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>' />
                                                                                        <div id="childLastLevel_<%#Eval("CommentID")%>" class="AttchmentListSection AttchmentListSection_<%#Eval("CommentID")%>"></div>
                                                                                        <input type="hidden" value="" id="HiddenReplyEditContent_<%#DataBinder.Eval(Container.DataItem, "CommentID")%>" />
                                                                                        <asp:LinkButton
                                                                                            ID="EditLevelOneReply"
                                                                                            runat="server"
                                                                                            CommandName="EditReply"
                                                                                            CssClass="EditPostReplyButton"
                                                                                            CommandArgument='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>'
                                                                                            ValidationGroup='<%#string.Format("ChildEdit_{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>'>
                                                                 POST EDITED REPLY</asp:LinkButton>

                                                                                        <a onclick="EditReplyCancle(this)" class="CancelPostReplyButton" cid="<%#DataBinder.Eval(Container.DataItem, "CommentID")%>">CANCEL</a>
                                                                                        <asp:HiddenField ID="hdnInnerCommentID" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>' />

                                                                                        <asp:LinkButton
                                                                                            ID="LinkButtonDeleteReply"
                                                                                            OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');"
                                                                                            CommandName="delete"
                                                                                            CssClass="deleteReplyOne"
                                                                                            runat="server"
                                                                                            AlternateText="Delete"
                                                                                            CommandArgument='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>'
                                                                                            Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>'>
                                                           Delete My Reply
                                                                                        </asp:LinkButton>
                                                                                    </div>


                                                                                </div>
                                                                                <!-- New Level of Comment !-->

                                                                            </ItemTemplate>
                                                                        </asp:Repeater>

                                                                    </div>
                                                                    <!-- New Level of Comment  : Third Level of work started!-->

                                                                    <%--Proper--%>
                                                                </div>

                                                                <%-- IInd level : Edit/delete sub menu end here--%>



                                                                <%--<asp:ImageButton ID="imgReply" CommandName="like" runat="server" AlternateText="Like" ImageUrl='<%#String.Format("~/DesktopModules/ClearAction_WebCast/images/{0}", GetImageName(DataBinder.Eval(Container.DataItem, "CommentID"))) %>' CommandArgument='<%#Eval("CommentID") %>' />--%>
                                                            </ItemTemplate>
                                                        </asp:Repeater>

                                                    </div>
                                                    <%--II nd Comments level start --%>
                                                    <div class="clearfix"></div>
                                                </div>


                                                <!-- First level edit !-->

                                                

                                                <!-- First Level Edit end here !-->

                                                <%--Vertical level First Level work Block --%>
                                            </div>


                                        </div>
                                    </div>

                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
         <!--Changes done by @SP-->
      <%--  <div class="forum-right-col col-sm-4 forum-intro-match-height" style="display:none;">
 
            <div style="padding-left: 10px; padding-top: 20px;" class="forum-right-toggle-content">
                <a href="#" class="toggle-content right active">Collapse</a>
                <span>
                    <asp:Label ID="lblMemberCount" CssClass="num" runat="server"></asp:Label>&nbsp;most active members</span>

                <div class="members-expand active">
                    <!--Anuj work Merged : dated 21st Feb -->

                    <asp:Repeater runat="server" ID="rptActiveMember">
                        <ItemTemplate>
                            <div style="float: left">
                                <a href='/MyProfile/UserName/<%#DataBinder.Eval(Container.DataItem, "Username") %>'>
                                    <img src='<%#GetAvatarUrl(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")), 49, 49)%>' class="tip" style="width: 49px; height: 49px" alt="user profile" data-tip='<%#Eval("DisplayName") %>' />
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="forum-right-toggle-content ">
                <span class="title">
                    <asp:Label ID="lblNum" runat="server"></asp:Label>&nbsp;shared files</span>
                <a href="#" class="toggle-content active right">Collapse</a>
                <div class="clearfix"></div>
                <div class="members-expand active">
                    <asp:Literal ID="ltrlFiles" runat="server"></asp:Literal>
                    <asp:Repeater runat="server" ID="rptSharedFiles">
                        <ItemTemplate>
                            <div>
                                <span style='width: 100%; padding-right: 0; color: #5093c3; padding-bottom: 0px; padding-top: 20px; font-size: 18px; font-weight: 500;'>
                                    <%# String.Format("{0}", GetDisplayName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "UserId")))) %> &nbsp; attached media :
                                </span>
                                <a href="<%#String.Format("{0}",DataBinder.Eval(Container.DataItem, "FileUrl")) %>" target="_blank" attachid="<%#String.Format("{0}",DataBinder.Eval(Container.DataItem, "AttachId")) %>">
                                    <p style="float: left; width: 100%; margin-bottom: 0px; color: #000 !important"><%#DataBinder.Eval(Container.DataItem, "FileName")%></p>
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>--%>

            <!--Sachin work on Sharing Digital event with it Connection(s)-->
            <div style="padding-left: 10px; font-size: 14px; padding-top: 20px;" class="forum-right-toggle-content">
                <a href="#" class="toggle-content right active">Collapse</a>
                
               <!--Changes done by @SP-->
                <%--<span>
                    <asp:Label ID="Label1" CssClass="num" runat="server"></asp:Label>Interesting? Choose connections to
                     share this digital event with:</span>--%>
                <span>
                    <asp:Label ID="Label1" CssClass="num" runat="server"></asp:Label>Share with others!
                </span>

                               <!--Changes done by @SP  Share, With This Message to  Check boxes above & click here-->
                <div class="members-expand active">
                    <ul id="CA_ulUsers" style="width: 90%; min-height: 50px;"></ul>
                    <input data-toggle="modal" type="button" id="CA_btnShareWithConnections" onclick="return CA_ShowSharePopup();" value="Check boxes above & click here..." style="display: block; background-color: #84A6CB; color: #FFF; text-transform: uppercase; padding: 3px;" />
                </div>
            </div>
   
        <div class="clearfix"></div>
    </div>
</asp:Panel>
<div class="clearfix"></div>
<div class="forum-active-topics footer insights col-sm-12" id="Paging" runat="server">
    <div class="forum-page-nav">
        <asp:ImageButton ID="NavFirstBtn" CssClass="forum-prev" Style="cursor: pointer;" runat="server" AlternateText="First" OnClick="NavFirstBtn_Click" ImageUrl="~/DesktopModules/ClearAction_WebCast/images/forum-page-prev-first.png" ToolTip="First" />
        <asp:ImageButton ID="NavImagePrev" CssClass="forum-prev" Style="cursor: pointer;" runat="server" AlternateText="Prev" OnClick="NavImagePrev_Click" ImageUrl="~/DesktopModules/ClearAction_WebCast/images/forum-page-prev.png" ToolTip="Previous" />
        <asp:Label ID="lblPagerInfo" runat="server" Text="1" CssClass="forum-page-count"></asp:Label>
        <asp:ImageButton ID="NavImageNext" CssClass="forum-prev" Style="cursor: pointer;" runat="server" AlternateText="Next" OnClick="NavImageNext_Click" ImageUrl="~/DesktopModules/ClearAction_WebCast/images/forum-page-next.png" ToolTip="Next" />
        <asp:ImageButton ID="NavImageLast" CssClass="forum-prev" Style="curs
    or: pointer;"
            runat="server" AlternateText="Last" OnClick="NavImageLast_Click" ImageUrl="~/DesktopModules/ClearAction_WebCast/images/forum-page-next-last.png" ToolTip="Last" />
    </div>
</div>
<div class="clearfix"></div>
<div id="modalShareDigitalEvent" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 id="CA_H4Header" class="modal-title">Share</h4>
            </div>
            <div class="modal-body">
                <div style="width: 100%;">
                    <div style="width: 100%; height: auto;">
                        <div style="width: 100%">
                            <ul id="CA_ulSelectedUsers" style="width: 100%; min-height: 25px; max-height: 250px; margin-left: 0px !important;"></ul>
                        </div>
                    </div>
                    <div style="width: 100%; margin-bottom: 10px;">
                        <div style="width: 100%;">Message </div>
                        <div style="width: 100%">
                            <label id="CA_txtMessage" style="width: 100%; padding: 8px; font-weight: normal;"></label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <div style="width: 100%;">
                    <div style="width: 50%; float: left;">
                     <label></label>
                        <%--Changes done by @SP--%>
                       <%-- <img src="/Images/icon-from-url.png" style="cursor: pointer; float: left;" title="Insert Attachment" onclick="CA_UploadOnFileStack();" />--%>
                    </div>
                    <div style="width: 50%; float: left;">
                        <button type="button" class="btn btn-info" onclick="return CA_ShareDigitalEvent(this);">Send</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="XPos" runat="server" />
<input type="hidden" id="YPos" runat="server" />
<script>
    var CA_UserID =<%=this.UserInfo.UserID%> ;
    var webMethodUrl = '/DesktopModules/ClearAction_WebCast/API/WebCast/';
    var sWebServicePath = '<%=WebServicePath%>';
    function CA_Assign2Me(ItemID, componentid) {
        // Expected ComponentID : 1-Forum, 2-Blog, 3-SolveSpace , 4-CC, 5:WCC
        var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/Assign2Me";
        var _data = "{'CID':'" + componentid + "','ItemID':'" + ItemID + "','UID':'" + <%=this.UserInfo.UserID%>  + "'}";
        CA_MakeRequest(_URL, _data, CA_OnAssignItem2User);
    }

    function CA_RemoveFromMyVault(ItemID, ComponentID) {
        $.confirm({
            title: 'Confirm!',
            content: 'Are you sure to remove???',
            buttons: {
                confirm: function () {

                    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/RemoveFromMyVault";
                    var _data = "{'CID':'" + ComponentID + "','IID':'" + ItemID + "','UID':'" + <%=this.UserInfo.UserID%> + "'}";
                    CA_MakeRequest(_URL, _data, CA_OnAssignItem2User);
                },
                cancel: function () {
                }
            }
        });
    }
</script>
 
<style type="text/css">
    .BeforAttachmentSestion {
        float: left;
        width: 100%;
    }

    .UploadedAttachmentSavePostHide {
        display: none !important;
    }

    .BeforeAttachment, .BeforeAttachment:hover, .BeforeAttachment:visited {
        /*color: orange;*/
        font-size: 12px;
        font-weight: 400;
    }

    .AfterAttachment, .AfterAttachment:hover, .AfterAttachment:visited {
        /*color: orange;*/
        font-size: 12px;
        font-weight: 400;
    }

    .attachmentBeforRemoveButton {
        /*background-image: url(/DesktopModules/ActiveForums/images/small_close_btn1.png);*/
        background-image: url(/DesktopModules/Activeforums/images/delete12.png);
        background-repeat: no-repeat;
        cursor: pointer;
    }

    .attachmentAfterRemoveButton {
        /*background-image: url(/DesktopModules/ActiveForums/images/small_close_btn1.png);*/
        background-image: url(/DesktopModules/Activeforums/images/delete12.png);
        background-repeat: no-repeat;
        cursor: pointer;
    }

    .InnerAttachButtons {
        float: left;
        padding-top: 0;
    }

    .PanelUpdateReplySection {
        padding-top: 5px !important;
    }

    .ReplyEditContentAttachmentData {
        display: none !important;
    }

    .UserReplyNameSection div {
        display: inline-block;
        background: #c5d5e7;
        color: #333333;
        font-size: 12px;
        padding: 10px 15px;
        font-weight: normal;
    }

    .EditPostReplyButton, .EditPostReplyButton:hover, .EditPostReplyButton:focus {
        color: #fff;
        background-color: #286090;
        border-color: #286090;
        padding: 7px;
        line-height: 3;
        font-size: 12px;
        cursor: pointer;
    }

    .CancelPostReplyButton, .CancelPostReplyButton:hover, .CancelPostReplyButton:focus {
        color: #fff;
        background-color: #BDBDBD;
        border-color: #BDBDBD;
        padding: 7px;
        line-height: 3;
        font-size: 12px;
        cursor: pointer;
    }

    .deleteReplyOne, .deleteReplyOne:hover, .deleteReplyOne:focus {
        color: #fff;
        background-color: #9E9E9E;
        border-color: #9E9E9E;
        padding: 7px;
        line-height: 3;
        cursor: pointer;
        font-size: 12px;
        text-transform: uppercase;
    }

    .deleteReplySecond, .deleteReplySecond:hover, .deleteReplySecond:focus {
        color: #fff;
        background-color: #9E9E9E;
        border-color: #9E9E9E;
        padding: 7px;
        line-height: 3;
        cursor: pointer;
        font-size: 12px;
        text-transform: uppercase;
    }

    .deleteMyReplyThird, .deleteMyReplyThird:hover, .deleteMyReplyThird:focus {
        color: #fff;
        background-color: #9E9E9E;
        border-color: #9E9E9E;
        padding: 7px;
        line-height: 3;
        cursor: pointer;
        font-size: 12px;
        text-transform: uppercase;
    }

    .EditPostValidation {
        float: left;
        width: 100%;
    }

    .EditAttachmentContent {
        float: left;
        padding-top: 0px;
        width: 100%;
    }

        .EditAttachmentContent .AfterAttachmentSestion {
            border-top: 1px solid #c5d5e7;
            border-left: 1px solid #c5d5e7;
            border-right: 1px solid #c5d5e7;
            padding-top: 7px;
            padding-bottom: 7px;
            padding-left: 2px;
            padding-right: 2px;
            width: 100%;
            background-color: aliceblue;
        }

            .EditAttachmentContent .AfterAttachmentSestion:last-child {
                border-bottom: 1px solid #c5d5e7;
            }

            .EditAttachmentContent .AfterAttachmentSestion .attachmentAfterRemoveButton {
                float: right;
                background-image: url(/DesktopModules/Activeforums/images/CADelete.png);
            }

            .EditAttachmentContent .AfterAttachmentSestion .AttachedFileText {
                font-weight: bold;
            }

    .AttchmentListSection {
        /*float: left;*/
        padding-top: 0px;
        width: 100%;
    }

        .AttchmentListSection .AfterAttachmentSestion {
            border-top: 1px solid #c5d5e7;
            border-left: 1px solid #c5d5e7;
            border-right: 1px solid #c5d5e7;
            padding-top: 7px;
            padding-bottom: 7px;
            padding-left: 2px;
            padding-right: 2px;
            width: 100%;
            background-color: aliceblue;
        }

            .AttchmentListSection .AfterAttachmentSestion:last-child {
                border-bottom: 1px solid #c5d5e7;
            }

            .AttchmentListSection .AfterAttachmentSestion .attachmentAfterRemoveButton {
                float: right;
                background-image: url(/DesktopModules/Activeforums/images/CADelete.png);
            }

            .AttchmentListSection .AfterAttachmentSestion .AttachedFileText {
                font-weight: bold;
            }

    .ReplyContentSectionBody .AfterAttachmentSestion {
        display: none;
    }

    .SeeAttachedMediaLinkContent {
        cursor: pointer;
        color: #6992c0;
    }

    .modal.in .modal-dialog {
        position: fixed;
        bottom: 15px;
        right: 30px;
        margin: 0px;
        width: 30%;
    }

    .CA_RemoveConnection {
        margin-top: -42px;
        margin-right: 14px;
        cursor: pointer;
        color: #fff;
        border: 1px solid #AEAEAE;
        border-radius: 30px;
        background: #605F61;
        font-size: 25px;
        font-weight: normal;
        display: inline-block;
        line-height: 0px;
        padding: 11px 3px;
        float: right;
        width: auto;
    }

        .CA_RemoveConnection:before {
            content: "×";
        }

    .CA_ShareLI {
        width: 100%;
        height: 50px;
        line-height: 50px;
        border-bottom: solid 1px #369;
        clear: both;
    }

        .CA_ShareLI > div:first-child {
            width: 30px;
            float: left;
            margin-left: 0px;
        }

    .modal-body {
        position: relative;
        padding: 0 px !important;
    }
</style>






