<%@ Control Language="vb" AutoEventWireup="true" CodeBehind="Blog.ascx.vb" Inherits="DotNetNuke.Modules.Blog.Blog" %>
<%@ Register TagPrefix="blog" Assembly="DotNetNuke.Modules.Blog" Namespace="DotNetNuke.Modules.Blog.Templating" %>
<%@ Register TagPrefix="blog" TagName="comments" Src="controls/Comments.ascx" %>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/module/common.js"></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/context/jquery.contextMenu.js"></script>
<link href="/DesktopModules/ClearAction_SolveSpaces/Scripts/context/jquery.contextMenu.css" rel="stylesheet" />
<script src="/DesktopModules/Blog/scripts/view.js"></script>
<%--<%@ Register TagPrefix="blog" TagName="management" Src="controls/ManagementPanel.ascx" %>--%>

<%@ Register TagPrefix="blog" TagName="topheader" Src="controls/TopHeader.ascx" %>
<script src="https://static.filestackapi.com/v3/filestack.js"></script>

<style type="text/css">
    .AttachmentRemoveButton {
        cursor: pointer;
    }

    .AttachmentHidenData {
        display: none !important;
    }

    .forum-tooltips {
        margin-top: 10px;
    }

    .insights-recent-comments1 {
        overflow: hidden;
        padding: 20px 20px 15px;
        /* border-top: 1px solid #dddddd; */
        border-bottom: 1px solid #bbbbbb;
        margin-bottom: 20px;
    }

        .insights-recent-comments1 ul li {
            float: left;
            display: inline-block;
            padding-right: 10px;
        }

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
        float: left;
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
</style>


<link type="text/css" href='<%=ModulePath %>js/ClearActionapi.js' />
<div class="blog_addbtn" style="display: none">
    <asp:LinkButton ID="btnAddBlog" runat="server" CausesValidation="false" CssClass="dnnPrimaryAction" Text="Add Blog"></asp:LinkButton>
    <asp:Literal runat="server" ID="litTrackback" />
</div>

<%--<blog:management runat="server" id="ctlManagement" />--%>
<div class="clearfix"></div>

<asp:Panel ID="pnlTopHeader" runat="server">
    <div class="col-sm-12">
        <blog:topheader runat="server" ID="topheader" />
    </div>
</asp:Panel>
<div class="clearfix"></div>

<asp:Panel ID="pnlListings" runat="server">
    <div class="col-sm-12">
        <div class="insights_container">
            <div id="dvBlogStatusFilter" class="col-sm-8 insights-left-col insights-match-height">
                <div class="filter-bar"  id="divfilter" runat="server">
                    <div class="col-md-8">
                        <ul class="filter-spaces button-group filter-button-group" style="margin-bottom: 40px;">
                            <li class="active" statusid="0">
                                <div class="filter-button">
                                    <asp:Button OnClick="btnAllBlog_Click" runat="server" ID="btnAllBlog" CssClass="btnctrl" Text="All" CausesValidation="false" />
                                </div>
                            </li>
                            <li statusid="1">
                                <div class="filter-button">
                                    <img src="/images/img_18.png" alt="" width="9" height="9">
                                    <asp:Button runat="server" OnClick="btntodo_Click" ID="btntodo" CssClass="btnctrl" Text="To Do" CausesValidation="false" />
                                </div>
                            </li>
                            <li statusid="2">
                                <div class="filter-button">
                                    <img src="/images/img_17.png" alt="" width="9" height="9">
                                    <asp:Button runat="server" ID="btnDoneBlog" OnClick="btnDoneBlog_Click" CssClass="btnctrl" Text="Done" CausesValidation="false" />
                                </div>
                            </li>
                        </ul>
                    </div>
                    <div class="col-md-4 text-right">
                        <asp:DropDownList ID="ddSortBy" runat="server" CssClass="forum-select" AutoPostBack="true" OnSelectedIndexChanged="ddSortBy_SelectedIndexChanged">
                            <asp:ListItem Text="Sort by" Value="-1" class="CA_SortOptions"></asp:ListItem>
                            <asp:ListItem Text="Alpha a-z" Value="0" class="CA_SortOptions"></asp:ListItem>
                            <asp:ListItem Text="Alpha z-a" Value="1" class="CA_SortOptions"></asp:ListItem>
                            <asp:ListItem Text="Most likes" Value="2" class="CA_SortOptions"></asp:ListItem>
                            <asp:ListItem Text="Most replies" Value="3" class="CA_SortOptions"></asp:ListItem>
                            <asp:ListItem Text="Latest" Value="4" class="CA_SortOptions"></asp:ListItem>
                            <asp:ListItem Text="Active today" Value="5" class="CA_SortOptions"></asp:ListItem>
                            <asp:ListItem Text="Active last 7 days" Value="6" class="CA_SortOptions"></asp:ListItem>
                            <asp:ListItem Text="Active this month" Value="7" class="CA_SortOptions"></asp:ListItem>
                            <asp:ListItem Text="All" Value="-1" class="CA_SortOptions"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="forum-active-topics footer insights col-sm-12" id="Div1" runat="server">
                    <div class="forum-page-nav"  style="float:right">
                        <asp:ImageButton ID="NavFirstBtn1" CssClass="forum-prev" Style="cursor: pointer;" runat="server" AlternateText="First" OnClick="NavFirstBtn_Click" ImageUrl="~/DesktopModules/blog/images/forum-page-prev-first.png" ToolTip="First" />&nbsp;&nbsp;
                        <asp:ImageButton ID="NavImagePrev1" CssClass="forum-prev" Style="cursor: pointer;" runat="server" AlternateText="Prev" OnClick="NavImagePrev_Click" ImageUrl="~/DesktopModules/blog/images/forum-page-prev.png" ToolTip="Previous" />&nbsp;&nbsp;
                        <asp:Label ID="lblPaingTop" runat="server" Text="1" CssClass="forum-page-count"></asp:Label>&nbsp;&nbsp;
                        <asp:ImageButton ID="NavImageNext1" CssClass="forum-prev" Style="cursor: pointer;" runat="server" AlternateText="Next" OnClick="NavImageNext_Click" ImageUrl="~/DesktopModules/blog/images/forum-page-next.png" ToolTip="Next" />&nbsp;&nbsp;
                        <asp:ImageButton ID="NavImageLast1" CssClass="forum-prev" Style="cursor: pointer;" runat="server" AlternateText="Last" OnClick="NavImageLast_Click" ImageUrl="~/DesktopModules/blog/images/forum-page-next-last.png" ToolTip="Last" />
                    </div>
                </div>
                <div class="clearfix"></div>
                <asp:Literal ID="ltrlCtrl" runat="server"></asp:Literal>
            </div>
            <asp:Panel ID="pnlSearch" runat="server">
                <div class="col-sm-4 insights-right-col insights-match-height" style="height: 2020px;">
                    <div class="insights-right-container">
                        <h2>Recent Comments</h2>
                        <div class="insights-recent-comments1">
                            <ul>
                                <asp:Literal ID="ltrAuthorlist" runat="server"></asp:Literal>
                            </ul>
                        </div>
                        <div class="insights-previous-posts">
                            <div style="overflow: auto; height: 90%">
                                <h2>Previous Posts</h2>
                                <ul>
                                    <asp:Literal ID="ltrPreviousPost" runat="server"></asp:Literal>

                                </ul>
                                <ul id="ulCollection">
                                </ul>
                                <br />
                                <center><a onclick="LoadMore()" id="btnloadmore" class="insights-previous-posts-more">More</a></center>
                            </div>

                            <asp:HiddenField ID="hdnCurrentPage" runat="server" />
                            <asp:HiddenField ID="hdnTotalRecord" runat="server" />
                        </div>
                    </div>
                </div>
            </asp:Panel>

        </div>
    </div>
</asp:Panel>

<asp:Panel ID="pnlDetails" runat="server">
    <!-- Single post view -->
    <div class="clearfix"></div>
    <div class="insights_container">
        <div class="cnt_wraper_lt col-sm-8">


            <div class="forum-active-topics  border col-sm-12 forum-intro-match-height">
                <div class="event-detail-container" style="padding-top: 40px">
                    <div class="row">
                        <asp:Literal ID="ltrlCtrlDetail" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>


            <div class="forum-post col-sm-12 expanded insights-single-discussion" style="padding-left: 20px">
                <div class="col-sm-12 insights-discussion-left-col">
                    <asp:Literal ID="ltrlBlogHeader" runat="server"></asp:Literal>
                    <div class="forum-more-bottom" style="display: block;">

                        <div>
                            <asp:TextBox ID="txtComments" runat="server" placeholder="Reply: Your thoughts" MaxLength="5000" TextMode="MultiLine" Height="110px"></asp:TextBox>
                            <%--	<asp:RequiredFieldValidator ID="reqComments" runat="server" ControlToValidate="txtComments" ValidationGroup="valMainComments" ErrorMessage="*Require" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                            <asp:RequiredFieldValidator ErrorMessage="*Require" ForeColor="Red" ValidationGroup="valMainComments" ControlToValidate="txtComments" runat="server" />
                            <div style="float: right">
                            </div>
                            <div class="clear"></div>
                        </div>
                        <p>

                            <%--                    <div id="divattach_0" style="display: none">
                        <span id="lnkdetail_0"></span><a onclick="DeleteUpload('0')">
                            <img src="/images/delete.gif" alt="delete" />
                        </a>
                    </div>--%>
                            <div id="UploadedAttachment"></div>

                            <asp:Button ID="lnkSave" runat="server" Text="POST REPLY" CausesValidation="false" OnClick="lnkSave_Click" CssClass="btn btn-primary" ValidationGroup="valMainComments" OnClientClick='saveAttachmentDataWhenPost(this)'></asp:Button>
                        </p>
                        <div style="display: none">
                            <asp:TextBox runat="server" ID="UploadedAttachmentSavePost" TextMode="MultiLine" CssClass="UploadedAttachmentSavePost multilineText UploadedAttachmentSavePostHide" Rows="3" Columns="20"></asp:TextBox>

                            <asp:TextBox runat="server" ID="TextBoxAttachmentData" TextMode="MultiLine" CssClass="TextBoxAttachmentData multilineText UploadedAttachmentSavePostHide" Rows="3" Columns="20"></asp:TextBox>
                        </div>
                        <div class="forum-posts-tab ui-tabs ui-corner-all ui-widget ui-widget-content">
                            <div class="clearfix"></div>
                            <div class="forum-tab-one active ui-tabs-panel ui-corner-bottom ui-widget-content" id="tabs-one" aria-labelledby="ui-id-4" role="tabpanel" aria-hidden="false">
                                <asp:Repeater ID="rptComments" runat="server" OnItemCommand="rptComments_ItemCommand" OnItemDataBound="rptComments_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="forum-tab-one active ui-tabs-panel ui-corner-bottom ui-widget-content" id="tabs-one" style="border-bottom: solid 1px #c2c3c5" aria-labelledby="ui-id-4" role="tabpanel" aria-hidden="false">
                                            <div class="forum-comments-row insights-discussion-left-col" id="dvEachTopic_<%#DataBinder.Eval(Container.DataItem, "CommentID")%>">
                                                <div class="forum-comments-content" style="<%#GetBackgroundImage(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Author")))%>; border-bottom: none; background-size: 49px 49px;">
                                                    <h4><%#Eval("AuthorLinkUrl") %>&nbsp;&nbsp;<%--<span>&nbsp;::&nbsp;<%#Eval("GetCreatedDateHumanFriendly") %></span>&nbsp;::&nbsp;--%><small><asp:Label ID="lblReplyCount" Text='<%#Eval("ReplyCount") %>' runat="server"></asp:Label></b>&nbsp;&nbsp;Replies</small></h4>
                                                    <p><%#Eval("Comment") %></p>



                                                    <asp:HiddenField ID="CommentID" runat="server" Value='<%#Eval("CommentID") %>' />

                                                    <asp:Panel ID="PanelTopContextMenu" runat="server" class="forum-tooltips InnerAttachButtons title-tooltip" Style="float: right" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>'>
                                                        <asp:Image runat="server" ID="InnerImagePostAcctachment"
                                                            Style="cursor: pointer; margin-top: 15px; margin-right: 2px;" src="/images/more_icon.png"
                                                            alt="Tips" tid="0"
                                                            ContentItemId='<%#DataBinder.Eval(Container.DataItem, "ContentItemId")%>'
                                                            commentid='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>'
                                                            status="Completed" isselfassigned="5" class="CA_UserMenu"
                                                            IsDeletedshow='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>'
                                                            isEditAllow='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>' />
                                                    </asp:Panel>
                                                    <%-- <div style="display: none" class="ReplyContentSectionBody ReplyContentSection_<%#DataBinder.Eval(Container.DataItem, "CommentID")%>" cid="<%#DataBinder.Eval(Container.DataItem, "CommentID")%>">
                                                <%#DataBinder.Eval(Container.DataItem, "Comment")%>
                                            </div>
                                                    --%>


                                                    <p>
                                                        <span id='hyper_<%#Eval("CommentID") %>'><a class="forum-reply" style="cursor: pointer; position: relative; top: 3px;" onclick='javascript:Show(<%#Eval("CommentID") %>)'>Reply</a></span>

                                                        <a onclick='return GetLikesCount(<%#Eval("CommentID")%>,1,"Comment")' style="padding-left: 10px; padding-right: 10px; cursor: pointer;">
                                                            <span id="Main_count<%#Eval("CommentID") %>">
                                                                <img id='imgLikeStatusM_<%#Eval("CommentID")%>' class="like_imglink" src="<%# String.Format("{0}/DesktopModules/ClearAction_WebCast/Images/{1}", WebServicePath, GetImageName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CommentID")))) %>" />
                                                                &nbsp;LIKE&nbsp;<label id="lblLikeCountM_<%#Eval("CommentID")%>"><%#DataBinder.Eval(Container.DataItem, "GetLikesCount")%></label>
                                                            </span>
                                                        </a>
                                                        <span style='<%#GetAttachmentDisplayCSS(Convert.ToInt16(DataBinder.Eval(Container.DataItem,"TotalAttachment")))%>; padding-left: 10px; padding-right: 10px; cursor: pointer' cid='<%#Eval("CommentID")%>' class="SeeAttachedMediaLinkContent" onclick="SeeAttachedMediaList(this,'top')" status="show">
                                                            <img style="width: 20px; height: 20px;" src="<%# String.Format("{0}/DesktopModules/Activeforums/images/{1}", WebServicePath, "CAAttach.png") %>" />&nbsp;ATTACHED MEDIA&nbsp;
                                                        </span>
                                                        <asp:ImageButton Style="position: relative; top: 2px;" ID="imgdelete" OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');" CommandName="delete" runat="server" AlternateText="Delete" ImageUrl="~/DesktopModules/Blog/Images/small-delete.png" />
                                                    </p>

                                                    <div id="divattachReplies_<%#Eval("CommentID") %>" style="display: none">
                                                        <div class="dvUserReply" style="width: 97%; float: left;">
                                                            <div>Editing Replied to <span><%#Eval("AuthorLinkUrl") %> ,</span></div>
                                                        </div>

                                                        <asp:TextBox ID="txtReplyEdit" Text='<%#Eval("Comment") %>' runat="server" TextMode="MultiLine" CssClass="multilineText" Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                        <div id="topedit_<%#Eval("CommentID")%>" class="AttchmentListSection AttchmentListSection_<%#Eval("CommentID")%>"></div>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReplyEdit"
                                                            ValidationGroup='<%#String.Format("top_Comment_Edit{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                        <div style="float: right;">
                                                            <asp:LinkButton
                                                                ID="EditLevelOneReply"
                                                                runat="server"
                                                                CommandName="EditReply"
                                                                CssClass="EditPostReplyButton"
                                                                CommandArgument='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>'
                                                                ValidationGroup='<%#String.Format("top_Comment_Edit{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>'>
                                                                 POST EDITED REPLY</asp:LinkButton>

                                                            <a onclick="EditReplyCancle(this)" class="CancelPostReplyButton" cid="<%#DataBinder.Eval(Container.DataItem, "CommentID")%>">CANCEL</a>

                                                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" CommandArgument="SaveReply" Visible="false" CommandName="SaveDisscussion" Text="Save"></asp:Button>

                                                        </div>
                                                    </div>
                                                    <div id="top_<%#Eval("CommentID")%>" class="AttchmentListSection AttchmentListSection_<%#Eval("CommentID")%>"></div>
                                                    <div id='<%#Eval("CommentID") %>' style="display: none; padding-top: 5px">
                                                        <div class="dvUserReply" style="width: 97%; float: left;">
                                                            <div>Replying to <span><%#Eval("AuthorLinkUrl") %> ,</span></div>
                                                        </div>

                                                        <div>
                                                            <a href="#" class="forum-tooltips">
                                                                <img src="/DesktopModules/Blog//images/more_icon.png" alt="Attach Media" class="CA_UserMenu" label="2" cid='<%#Eval("CommentID") %>'
                                                                    isselfassigned="6" contenitemid='<%#Eval("ContentItemID") %>'>
                                                            </a>
                                                        </div>

                                                        <asp:TextBox ID="txtInnerQuickReply" runat="server" PlaceHolder="Reply to " TextMode="MultiLine" CssClass="multilineText" Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqField" runat="server" ControlToValidate="txtInnerQuickReply"
                                                            ValidationGroup='<%#String.Format("Child_{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <div id="UploadedAttachment_<%#Eval("CommentID") %>"></div>

                                                        <div style="float: right;">
                                                            <asp:Button ID="ImageButton2" CommandArgument="SaveReply" Style="cursor: pointer;" currentPostContenetId='<%#Eval("CommentID") %>' ValidationGroup='<%#String.Format("Child_{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>' CommandName="SaveDisscussion" runat="server" AlternateText="Reply" CssClass="btn btn-primary" Text="POST REPLY" OnClientClick='saveAttachmentDataWhenPost(this)' />
                                                            <span id='hyper1_<%#Eval("CommentID") %>' style="float: right; margin-top: 10px; margin-left: 20px;"><a class="btn-see-more" style="cursor: pointer" onclick='javascript:Hide(<%#Eval("CommentID") %>)'>Cancel</a></span>

                                                        </div>




                                                    </div>
                                                </div>
                                                <div class="subcomment">
                                                    <div class="forum-comments-row" style="padding-left: 80px">
                                                        <asp:Repeater ID="rptInnerComments" runat="server" OnItemCommand="rptInnerComments_ItemCommand" OnItemDataBound="rptInnerComments_ItemDataBound">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="CommentID" runat="server" Value='<%#Eval("CommentID") %>' />

                                                                <div class="forum-comments-sub-content" style="<%#GetBackgroundImage(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Author")))%>; padding-left: 60px; background-size: 49px 49px;" id="dvEachTopic_<%#DataBinder.Eval(Container.DataItem, "CommentID")%>">
                                                                    <h4><%#Eval("AuthorLinkUrl") %>&nbsp;&nbsp;&nbsp;&nbsp;<%--<span>&nbsp;::&nbsp;<%#Eval("GetCreatedDateHumanFriendly") %></span>&nbsp;::&nbsp;--%><small><asp:Label ID="lblReplyCount" Text='<%#Eval("ReplyCount") %>' runat="server"></asp:Label></b>&nbsp;&nbsp;Replies</small></h4>
                                                                    <p>


                                                                        <%#Eval("Comment") %>

                                                                        <asp:Panel ID="PanelInnerComment" runat="server" class="forum-tooltips InnerAttachButtons title-tooltip" Style="float: right" Visible='<%#IsCommentAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>'>
                                                                            <asp:Image runat="server" ID="InnerImagePostAcctachment"
                                                                                Style="cursor: pointer; margin-top: 15px; margin-right: 2px;" src="/images/more_icon.png"
                                                                                alt="Tips" tid="0"
                                                                                ContentItemId='<%#DataBinder.Eval(Container.DataItem, "ContentItemId")%>'
                                                                                commentid='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>'
                                                                                status="Completed" isselfassigned="7" class="CA_UserMenu"
                                                                                IsDeletedshow='<%#IsCommentAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>'
                                                                                isEditAllow='<%#IsCommentAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>' />
                                                                        </asp:Panel>
                                                                    </p>
                                                                    <p>
                                                                        <span id='hyper_<%#Eval("CommentID") %>'>
                                                                            <a class="forum-reply" style="cursor: pointer; position: relative; top: 3px;" onclick='javascript:Show(<%#Eval("CommentID") %>)'>Reply</a>
                                                                        </span>
                                                                        <a onclick='return GetLikesCount(<%#Eval("CommentID")%>,0,"Comment")' style="padding-left: 10px; padding-right: 10px; cursor: pointer;">
                                                                            <span id="Span_count<%#Eval("CommentID") %>">
                                                                                <img id='imgLikeStatus_<%#Eval("CommentID")%>' class="like_imglink" src="<%# String.Format("{0}/DesktopModules/ClearAction_WebCast/Images/{1}", WebServicePath, GetImageName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CommentID")))) %>" />
                                                                                &nbsp;LIKE&nbsp;<label id="lblLikeCount_<%#Eval("CommentID")%>"><%#DataBinder.Eval(Container.DataItem, "GetLikesCount")%></label>
                                                                            </span>
                                                                        </a>
                                                                        <span style='<%#GetAttachmentDisplayCSS(Convert.ToInt16(DataBinder.Eval(Container.DataItem,"TotalAttachment")))%>; padding-left: 10px; padding-right: 10px; cursor: pointer' cid='<%#Eval("CommentID")%>' class="SeeAttachedMediaLinkContent" onclick="SeeAttachedMediaList(this,'top')" status="show">
                                                                            <img style="width: 20px; height: 20px;" src="<%# String.Format("{0}/DesktopModules/Activeforums/images/{1}", WebServicePath, "CAAttach.png") %>" />&nbsp;ATTACHED MEDIA&nbsp;
                                                                        </span>
                                                                        <asp:ImageButton Style="position: relative; top: 2px;" ID="imgdelete" OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');" CommandArgument='<%#Eval("CommentID") %>' CommandName="delete" runat="server" AlternateText="Delete" ImageUrl="~/DesktopModules/Blog/images/small-delete.png" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>' />

                                                                    </p>



                                                                    <div id="top_<%#Eval("CommentID")%>" class="AttchmentListSection AttchmentListSection_<%#Eval("CommentID")%>"></div>
                                                                    <div id="divattachReplies_<%#Eval("CommentID") %>" style="display: none">
                                                                        <div class="dvUserReply" style="width: 97%; float: left;">
                                                                            <div>Editing Replied to <span><%#Eval("AuthorLinkUrl") %> ,</span></div>
                                                                        </div>

                                                                        <asp:TextBox ID="txtReplyChildEdit" Text='<%#Eval("Comment") %>' runat="server" TextMode="MultiLine" CssClass="multilineText" Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                                        <div id="topedit_<%#Eval("CommentID")%>" class="AttchmentListSection AttchmentListSection_<%#Eval("CommentID")%>"></div>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtReplyChildEdit"
                                                                            ValidationGroup='<%#String.Format("child1_Comment_Edit{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                                        <div style="float: right;">
                                                                            <asp:LinkButton
                                                                                ID="LinkButton1"
                                                                                runat="server"
                                                                                CommandName="EditReply"
                                                                                CssClass="EditPostReplyButton"
                                                                                CommandArgument='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>'
                                                                                ValidationGroup='<%#String.Format("child1_Comment_Edit{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>'>
                                                                 POST EDITED REPLY</asp:LinkButton>

                                                                            <a onclick="EditReplyCancle(this)" class="CancelPostReplyButton" cid="<%#DataBinder.Eval(Container.DataItem, "CommentID")%>">CANCEL</a>

                                                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" CommandArgument="SaveReply" Visible="false" CommandName="SaveDisscussion" Text="Save"></asp:Button>

                                                                        </div>
                                                                    </div>

                                                                    <div id='<%#Eval("CommentID") %>' style="display: none; padding-top: 5px">
                                                                        <div class="dvUserReply" style="width: 97%; float: left;">
                                                                            <div>Replying to <span><%#Eval("AuthorLinkUrl") %> ,</span></div>
                                                                        </div>

                                                                        <div>
                                                                            <a href="#" class="forum-tooltips">
                                                                                <img src="/DesktopModules/Blog//images/more_icon.png" contenitemid='<%#Eval("ContentItemID") %>' alt="Attach Media" class="CA_UserMenu" label="2" cid='<%#Eval("CommentID") %>' isselfassigned="4">
                                                                            </a>
                                                                        </div>

                                                                        <asp:TextBox ID="txtInnerQuickReply" runat="server" PlaceHolder="Reply to " TextMode="MultiLine" CssClass="multilineText" Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="reqField" runat="server" ControlToValidate="txtInnerQuickReply"
                                                                            ValidationGroup='<%#String.Format("Child_{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>



                                                                        <div id="UploadedAttachment_<%#DataBinder.Eval(Container.DataItem, "CommentID")%>"></div>
                                                                        <div style="float: right;">
                                                                            <asp:Button ID="ImageButton2"
                                                                                CommandArgument="SaveReply"
                                                                                currentPostContenetId='<%#Eval("CommentID") %>'
                                                                                Style="cursor: pointer;"
                                                                                ValidationGroup='<%#String.Format("Child_{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>'
                                                                                CommandName="SaveDisscussion"
                                                                                runat="server"
                                                                                AlternateText="Reply"
                                                                                CssClass="btn btn-primary"
                                                                                Text="POST REPLY"
                                                                                OnClientClick='saveAttachmentDataWhenPost(this)' />
                                                                            <span id='hyper1_<%#Eval("CommentID") %>' style="float: right; margin-top: 10px; margin-left: 20px;"><a class="btn-see-more" style="cursor: pointer" onclick='javascript:Hide(<%#Eval("CommentID") %>)'>Cancel</a></span>
                                                                            <asp:Button ID="lnkSve" runat="server" CssClass="btn btn-primary" CommandArgument="SaveReply" Visible="false" CommandName="SaveDisscussion" Text="Save"></asp:Button>

                                                                        </div>
                                                                    </div>
                                                                    <%--<asp:ImageButton ID="imgReply" CommandName="like" runat="server" AlternateText="Like" ImageUrl='<%#String.Format("~/DesktopModules/Blog/images/{0}", GetImageName(DataBinder.Eval(Container.DataItem, "CommentID"))) %>' CommandArgument='<%#Eval("CommentID") %>' />--%>
                                                                    <div class="subcomment">
                                                                        <div class="forum-comments-row" style="padding-left: 80px">
                                                                            <asp:Repeater ID="rptInnerComments2" runat="server" OnItemCommand="rptInnerComments2_ItemCommand" OnItemDataBound="rptInnerComments2_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <div class="forum-comments-sub-content" style="<%#GetBackgroundImage(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Author")))%>; padding-left: 60px; background-size: 49px 49px;" id="dvEachTopic_<%#DataBinder.Eval(Container.DataItem, "CommentID")%>">
                                                                                        <h4><%#Eval("AuthorLinkUrl") %>&nbsp;&nbsp;<%--<span>&nbsp;::&nbsp;<%#Eval("GetCreatedDateHumanFriendly") %></span>--%></h4>
                                                                                        <p>
                                                                                            <%#Eval("Comment") %>
                                                                                            <asp:ImageButton
                                                                                                Style="position: relative; top: 2px;"
                                                                                                ID="ImageButtonDeleteRepliesAttachment"
                                                                                                OnClientClick="javascript: return confirm('Are you sure want to delete your attachment?');"
                                                                                                CommandName="deleteAttachment"
                                                                                                runat="server"
                                                                                                AlternateText="Delete Attachment"
                                                                                                ImageUrl="/images/delete.gif"
                                                                                                CssClass="AttachmentRemoveButton"
                                                                                                CommandArgument='<%#Eval("CommentID") %>'
                                                                                                Visible='<%#IsAuthorizeToDeleteAttachment(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")), Convert.ToString(DataBinder.Eval(Container.DataItem, "AttachFileLink")))%>' />


                                                                                            <%#IsAuthorizeToDeleteAttachmentDevider(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")), Convert.ToString(DataBinder.Eval(Container.DataItem, "AttachFileLink")))%>
                                                                                        </p>
                                                                                        <asp:Panel ID="PanelInnerComment" runat="server" class="forum-tooltips InnerAttachButtons title-tooltip" Style="float: right" Visible='<%#IsCommentAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>'>
                                                                                            <asp:Image runat="server" ID="InnerImagePostAcctachment"
                                                                                                Style="cursor: pointer; margin-top: 15px; margin-right: 2px;" src="/images/more_icon.png"
                                                                                                alt="Tips" tid="0"
                                                                                                ContentItemId='<%#DataBinder.Eval(Container.DataItem, "ContentItemId")%>'
                                                                                                commentid='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>'
                                                                                                status="Completed" isselfassigned="9" class="CA_UserMenu"
                                                                                                IsDeletedshow='<%#IsCommentAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>'
                                                                                                isEditAllow='<%#IsCommentAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>' />
                                                                                        </asp:Panel>
                                                                                        <div id="top_<%#Eval("CommentID")%>" class="AttchmentListSection AttchmentListSection_<%#Eval("CommentID")%>"></div>

                                                                                        <a onclick='return GetLikesCount(<%#Eval("CommentID")%>,0,"Comment")' style="padding-left: 10px; padding-right: 10px; cursor: pointer;">
                                                                                            <span id="Span_count<%#Eval("CommentID") %>">
                                                                                                <img id='imgLikeStatus_<%#Eval("CommentID")%>' class="like_imglink" src="<%# String.Format("{0}/DesktopModules/ClearAction_WebCast/Images/{1}", WebServicePath, GetImageName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CommentID")))) %>" />
                                                                                                &nbsp;LIKE&nbsp;<label id="lblLikeCount_<%#Eval("CommentID")%>"><%#DataBinder.Eval(Container.DataItem, "GetLikesCount")%></label>
                                                                                            </span>
                                                                                        </a>
                                                                                        <span style='<%#GetAttachmentDisplayCSS(Convert.ToInt16(DataBinder.Eval(Container.DataItem,"TotalAttachment")))%>; padding-left: 10px; padding-right: 10px; cursor: pointer' cid='<%#Eval("CommentID")%>' class="SeeAttachedMediaLinkContent" onclick="SeeAttachedMediaList(this,'top')" status="show">
                                                                                            <img style="width: 20px; height: 20px;" src="<%# String.Format("{0}/DesktopModules/Activeforums/images/{1}", WebServicePath, "CAAttach.png") %>" />&nbsp;ATTACHED MEDIA&nbsp;
                                                                                        </span>
                                                                                        <asp:ImageButton Style="position: relative; top: 2px;" ID="imgdelete" OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');" CommandArgument='<%#Eval("CommentID") %>' CommandName="delete" runat="server" AlternateText="Delete" ImageUrl="~/DesktopModules/Blog/images/small-delete.png" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "CreatedByUserID")))%>' />

                                                                                        <div id="divattachReplies_<%#Eval("CommentID") %>" style="display: none">
                                                                                            <div class="dvUserReply" style="width: 97%; float: left;">
                                                                                                <div>Editing Replied to <span><%#Eval("AuthorLinkUrl") %> ,</span></div>
                                                                                            </div>

                                                                                            <asp:TextBox ID="txtReplyChildEdit2" Text='<%#Eval("Comment") %>' runat="server" TextMode="MultiLine" CssClass="multilineText" Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                                                            <div id="topedit_<%#Eval("CommentID")%>" class="AttchmentListSection AttchmentListSection_<%#Eval("CommentID")%>"></div>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtReplyChildEdit2"
                                                                                                ValidationGroup='<%#String.Format("child2_Comment_Edit{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                                                            <div style="float: right;">
                                                                                                <asp:LinkButton
                                                                                                    ID="LinkButton1"
                                                                                                    runat="server"
                                                                                                    CommandName="EditReply"
                                                                                                    CssClass="EditPostReplyButton"
                                                                                                    CommandArgument='<%#DataBinder.Eval(Container.DataItem, "CommentID")%>'
                                                                                                    ValidationGroup='<%#String.Format("child2_Comment_Edit{0}", DataBinder.Eval(Container.DataItem, "CommentID")) %>'>
                                                                 POST EDITED REPLY</asp:LinkButton>

                                                                                                <a onclick="EditReplyCancle(this)" class="CancelPostReplyButton" cid="<%#DataBinder.Eval(Container.DataItem, "CommentID")%>">CANCEL</a>

                                                                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" CommandArgument="SaveReply" Visible="false" CommandName="SaveDisscussion" Text="Save"></asp:Button>

                                                                                            </div>
                                                                                        </div>

                                                                                    </div>
                                                                                    <asp:HiddenField ID="hdnCommentId2" runat="server" Value='<%#Eval("CommentID") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>

                                                                            <asp:HiddenField ID="hdnCommentId" runat="server" Value='<%#Eval("CommentID") %>' />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>

                                                        <asp:HiddenField ID="hdnCommentId" runat="server" Value='<%#Eval("CommentID") %>' />
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>





        </div>
    </div>
    <div style="background-color: #FFF !important; overflow: hidden; height: 100%; padding: 0px 0 50px;">
        <div class="col-sm-8 insights-right-col insights-match-height" style="height: 2020px; background-color: #fafafa !important; padding-top: 20px !important">

            <div style="padding-left: 10px; padding-top: 20px;" class="forum-right-toggle-content">
                <a href="#" class="toggle-content right active">Collapse</a>
                <span>
                    <asp:Label ID="lblMemberCount" CssClass="num" runat="server"></asp:Label>&nbsp;most active members</span>

                <div class="members-expand active">
                    <!--Anuj work Merged : dated 23rd Mar -->

                    <asp:Repeater runat="server" ID="rptActiveMember">
                        <ItemTemplate>
                            <div style="float: left">
                                <a href='/MyProfile/UserName/<%#GetUser(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId"))).Username %>'>
                                    <img src='<%#GetAvatarUrl(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")), 49, 49)%>' class="tip" style="width: 49px; height: 49px" alt="user profile" data-tip='<%#GetUser(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId"))).DisplayName %>' />
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
                                <a href="<%#String.Format("{0}", DataBinder.Eval(Container.DataItem, "FileUrl")) %>" target="_blank" attachid="<%#String.Format("{0}", DataBinder.Eval(Container.DataItem, "AttachId")) %>">
                                    <p style="float: left; width: 100%; margin-bottom: 0px; color: #000 !important"><%#DataBinder.Eval(Container.DataItem, "FileName")%></p>
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

        </div>
    </div>
</asp:Panel>

<div class="clearfix"></div>

<div class="forum-active-topics footer insights col-sm-12" id="Paging" runat="server">
    <div class="forum-page-nav">
        <asp:ImageButton ID="NavFirstBtn" CssClass="forum-prev" Style="cursor: pointer;" runat="server" AlternateText="First" OnClick="NavFirstBtn_Click" ImageUrl="~/DesktopModules/blog/images/forum-page-prev-first.png" ToolTip="First" />&nbsp;&nbsp;
        <asp:ImageButton ID="NavImagePrev" CssClass="forum-prev" Style="cursor: pointer;" runat="server" AlternateText="Prev" OnClick="NavImagePrev_Click" ImageUrl="~/DesktopModules/blog/images/forum-page-prev.png" ToolTip="Previous" />&nbsp;&nbsp;
        <asp:Label ID="lblPagerInfo" runat="server" Text="1" CssClass="forum-page-count"></asp:Label>&nbsp;&nbsp;
        <asp:ImageButton ID="NavImageNext" CssClass="forum-prev" Style="cursor: pointer;" runat="server" AlternateText="Next" OnClick="NavImageNext_Click" ImageUrl="~/DesktopModules/blog/images/forum-page-next.png" ToolTip="Next" />&nbsp;&nbsp;
        <asp:ImageButton ID="NavImageLast" CssClass="forum-prev" Style="cursor: pointer;" runat="server" AlternateText="Last" OnClick="NavImageLast_Click" ImageUrl="~/DesktopModules/blog/images/forum-page-next-last.png" ToolTip="Last" />&nbsp;&nbsp;
    </div>
</div>
<div class="clearfix"></div>

<asp:HiddenField ID="hdnFileUrl" runat="server" />
<asp:HiddenField ID="hdnFileName" runat="server" />

<asp:HiddenField ID="hdnCommentID" runat="server" />
<asp:HiddenField ID="hdnContentID" runat="server" />


<asp:HiddenField ID="hdnFileUrlReplies" runat="server" />
<asp:HiddenField ID="hdnFileNameReplies" runat="server" />

<asp:HiddenField ID="hdnCommentIDReplies" runat="server" />
<asp:HiddenField ID="hdnContentIDReplies" runat="server" />

<input type="hidden" id="hiddenUserId" value='<%=Me.UserInfo.UserID%>' />

<script type="text/javascript">


    var webMethodUrl = '/DesktopModules/Blog/API/ClearAction/';





    //   var AF_CurrentCatID = -1;

    // var AF_IsMyVault = getPathVariable('IsMyVault');
    //var AF_CurrentStatus = getPathVariable('Show');//1'; // Default is Zero / 1/2
    var AF_ComponentID = getPathVariable('ComponentID');
    var oKey = '<%=GetFileStack%>';

    function addFile(ele, contId, label) {
        var fsClient = filestack.init(oKey);
        fsClient.pick({}).then(function (response) {

            response.filesUploaded.forEach(function (file) {
                var ofileresponse = file;
                debugger;
                //fileName: ofileresponse.filename, contentType: ofileresponse.mimetype, size: ofileresponse.size, fileurl: ofileresponse.url 
                $("#<%=hdnFileUrl.ClientID%>").val(ofileresponse.url);
                $("#<%=hdnFileName.ClientID%>").val(ofileresponse.filename);
                $("#<%=hdnCommentID.ClientID%>").val(contId);

                ShowPanel(label);
            });

        });

    }



    var sWebServicePath = '<%=WebServicePath%>';

   <%-- function GetLikesCount(getcontid, isParent, itemType) {
        if (itemType == undefined) itemType = 'Comment';
        var webMethodUrl = '/DesktopModules/Blog/API/ClearAction/';
        $.ajax({
            type: "GET",
            url: webMethodUrl + "GetLikeContentIDCount",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { ContentPostID: getcontid, ItemType: itemType },
        }).done(function (response) {
            var oData = JSON.parse(response);


            if (isParent == 1) {

                $('#Main_count' + getcontid).html('<img src="{0}/DesktopModules/ActiveForums/Images/{1}" style="width: 14px; height: 16px;"></img>&nbsp;LIKE&nbsp;'.format(sWebServicePath, oData.ImageName) + oData.LikesCount);
                //    $("#imgLikeStatusM_" + getcontid).attr('src', '/DesktopModules/ActiveForums/Images/' + oData.ImageName);
            }

            //if (isParent == 2) {
            //    $('#Span_count' + getcontid).html(oData.LikesCount + ' <img src="{0}/DesktopModules/ActiveForums/Images/{1}"></img>'.format(sWebServicePath, oData.ImageName));
            //}

            else {
                $('#Span_count' + getcontid).html('<img src="{0}/DesktopModules/ActiveForums/Images/{1}" style="width: 14px; height: 16px;"></img>&nbsp;LIKE&nbsp;'.format(sWebServicePath, oData.ImageName) + oData.LikesCount);
                //    $("#imgLikeStatus_" + getcontid).attr('src', '/DesktopModules/ActiveForums/Images/' + oData.ImageName);
            }
               // $('#Main_count' + getcontid).html(' <img style="padding-right:10px;padding-left:10px;cursor:pointer;width:14px;height:14px" src="{0}/DesktopModules/Blog/Images/{1}"/> {2}'.format('<%=WebServicePath%>', oData.ImageName, oData.LikesCount));
            });
        //
        return false;
    }--%>
    //function RecommendThisPost(ItemID) {
    //    var webMethodUrl = '/DesktopModules/Blog/API/ClearAction/';
    //    $.ajax({
    //        type: "GET", dataType: "json", contentType: "application/json; charset=utf-8",
    //        url: webMethodUrl + "GetLikeContentIDCount",
    //        data: { ContentPostID: ItemID, ItemType: 'Post' },
    //    }).done(function (response) {
    //        var oData = JSON.parse(response);
    //        $(".RecomCount").text(oData.LikesCount);
    //        $(".UnRecomCount").text(oData.UnLikeCount);
    //    });
    //    return false;
    //}


    //function UnRecommendThisPost(ItemID) {
    //    var webMethodUrl = '/DesktopModules/Blog/API/ClearAction/';
    //    $.ajax({
    //        type: "GET", dataType: "json", contentType: "application/json; charset=utf-8",
    //        url: webMethodUrl + "UnLikeThePost",
    //        data: { ContentPostID: ItemID, ItemType: 'Post' },
    //    }).done(function (response) {
    //        var oData = JSON.parse(response);
    //        $(".RecomCount").text(oData.LikesCount);
    //        $(".UnRecomCount").text(oData.UnLikeCount);
    //    });
    //    return false;
    //}

    //function Show(id) {
    //    if (document.getElementById(id).style.display == 'none') {
    //        document.getElementById(id).style.display = 'block';
    //        //  $(id).show(1000);
    //        document.getElementById('hyper_' + id).style.display = 'none';
    //        document.getElementById('hyper1_' + id).style.display = 'block';
    //    }

    //    return false;
    //}
    //function Hide(id) {
    //    if (document.getElementById(id).style.display == 'block') {
    //        document.getElementById(id).style.display = 'none';
    //        document.getElementById('hyper_' + id).style.display = 'block';
    //        document.getElementById('hyper1_' + id).style.display = 'none';
    //    }
    //    return false;
    //}
    function CA_Assign2Me(ItemID) {
        // Expected ComponentID : 1-Forum, 2-Blog, 3-SolveSpace
        var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/Assign2Me";
        var _data = "{'CID':'" + 2 + "','ItemID':'" + ItemID + "','UID':'" + <%=Me.UserInfo.UserID%>  + "'}";
        CA_MakeRequest(_URL, _data, CA_OnAssignItem2User);
    }
    function CA_OnAssignItem2User(d) {
        // $.confirm('Sucess', 'Your vault has been updated successfully');

        $.confirm({
            title: 'Success',
            content: 'Your vault has been updated successfully',
            type: 'blue',
            typeAnimated: true,
            buttons: {
                tryAgain: {
                    text: 'OK',
                    btnClass: 'btn-red',
                    action: function () {
                        document.location.reload();
                    }
                }
            }
        });



    }


    function CA_RemoveFromMyVault(ItemID) {
        $.confirm({
            title: 'Confirm!',
            content: 'Are you sure want to remove???',
            buttons: {
                confirm: function () {
                    debugger;
                    var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/RemoveFromMyVault";
                    var _data = "{'CID':'" + 2 + "','IID':'" + ItemID + "','UID':'" + <%=Me.UserInfo.UserID%> + "'}";
                    CA_MakeRequest(_URL, _data, CA_OnAssignItem2User);
                },
                cancel: function () {

                }
            }
        });




    }


    //$('.solve-spaces-middle-nav ul li').matchHeight();
</script>

