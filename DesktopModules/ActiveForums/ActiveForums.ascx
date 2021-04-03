<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActiveForums.ascx.cs" Inherits="DotNetNuke.Modules.ActiveForums.ActiveForums" %>
<%@ Register TagPrefix="am" TagName="topheader" Src="~/DesktopModules/ActiveForums/controls/af_topheader.ascx" %>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/module/common.js"></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/context/jquery.contextMenu.js"></script>
<link href="/DesktopModules/ClearAction_SolveSpaces/Scripts/context/jquery.contextMenu.css" rel="stylesheet" />
<link type="text/css" href='<%=ModulePath %>module.css' />

<link href="/Portals/0/Skins/DemoSkin/css/stylefix.css" rel="stylesheet" />

<script src="<%=ModulePath %>scripts/activeforum.js?v1=2"></script>
<div class="activeForums" style="display: none">
    <!-- Provices CSS Scope -->
    <asp:PlaceHolder ID="plhAF" runat="server" />
</div>
<script src="https://static.filestackapi.com/v3/filestack.js"></script>
<script type="text/javascript">
    var webMethodUrl = '/DesktopModules/activeforums/api/ForumService/';
    var sWebServicePath = '<%=WebServicePath%>';
    var oKey = '<%=GetAPIKey%>';// 'AUQ4R8zKvSKCs9gudWJlxz';

    function CA_Assign2Me(ItemID) {
        // Expected ComponentID : 1-Forum, 2-Blog, 3-SolveSpace
        var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/Assign2Me";
        var _data = "{'CID':'" + 1 + "','ItemID':'" + ItemID + "','UID':'" + <%=this.UserInfo.UserID%>  + "'}";
        CA_MakeRequest(_URL, _data, CA_OnAssignItem2User);
    }
    function CA_OnAssignItem2User(d) {
        alert('Your vault is updated successfully');
        document.location.reload();
    }

    function CA_RemoveFromMyVault(ItemID) {
        var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/RemoveFromMyVault";
        var _data = "{'CID':'" + 1 + "','IID':'" + ItemID + "','UID':'" + <%=this.UserInfo.UserID%> + "'}";
        CA_MakeRequest(_URL, _data, CA_OnAssignItem2User);
    }


    function ExpandReplyBox(cntrl, contentid) {


        $('#' + cntrl).show();
        $('#replyShow_' + contentid).hide();
        $('#replyHide_' + contentid).show();
    }

    function ExpandReplyBoxClose(cntrl, contentid) {

        $('#' + cntrl).hide();

        $('#replyShow_' + contentid).show();
        $('#replyHide_' + contentid).hide();
    }
    function ExpandReplyBox1(cntrl, contentid) {


        $('#' + cntrl).show();
        $('#replyShow_1_' + contentid).hide();
        $('#replyHide_1_' + contentid).show();
    }
    function ExpandReplyBoxClose1(cntrl, contentid) {

        $('#' + cntrl).hide();

        $('#replyShow_1_' + contentid).show();
        $('#replyHide_1_' + contentid).hide();
    }

</script>

<style type="text/css">
    .BeforAttachmentSestion {
        float: left;
        width: 100%;
    }

    .forum-right-col.expanded {
        background-color: #FFF !important;
    }

    .forum-post.expanded {
        background: #FFF !important;
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


    .ReplyContentSectionBody .BodyContent {
        float: left;
        width:100%;
    }

    .SeeAttachedMediaLinkContent {
        cursor: pointer;
        color: #6992c0;
    }
</style>


<div id="wait" style="display: none; width: 100%; height: 100%; top: 100px; left: 0px; position: fixed; z-index: 10000; text-align: center;">
    <img src="/images/loading_blue2.gif" width="45" height="45" alt="Loading..." style="position: fixed; top: 50%; left: 50%;" />
</div>
<%--<asp:UpdatePanel id="UpdatePanel1" runat="server">
    <contenttemplate> --%>

<%--<asp:HiddenField id="hiddenPortalId" runat="server" value='<%=this.PortalId %>'/>--%>

<%--<input type="hidden" id="hiddenPortalId"  value='<%this.PortalId%>' />--%>

<input type="hidden" id="hiddenPortalId" value='<%=this.PortalId %>' />


<div class="solve_wrapper" style="border: 0;">

    <am:topheader ID="topheader" runat="server" SubHeader="true"></am:topheader>
    <div class="clearfix"></div>
    <div class="cnt_wraper_lt col-sm-8">
        <div class="forum-active-topics col-sm-8 forum-intro-match-height" style="padding-top: 0;">
            <div class="border">
                <%--  <asp:Label ID="lblActiveTopicLastSevenDays" runat="server"></asp:Label>--%>
                <div class="forum-page-nav" id="topPaging" runat="server">
                    <asp:ImageButton ID="NavFirstBtn" CssClass="forum-prev pointer" runat="server" OnClick="NavFirstBtn_Click" ImageUrl="~/DesktopModules/ActiveForums/images/forum-page-prev-first.png" />
                    <asp:ImageButton ID="NavImagePrev" CssClass="forum-prev pointer" runat="server" OnClick="NavImagePrev_Click" ImageUrl="~/DesktopModules/ActiveForums/images/forum-page-prev.png" />
                    <asp:Label ID="lblPagerInfo" runat="server" Text="1" CssClass="forum-page-count"></asp:Label>
                    <asp:ImageButton ID="NavImageNext" CssClass="forum-prev pointer" runat="server" OnClick="NavImageNext_Click" ImageUrl="~/DesktopModules/ActiveForums/images/forum-page-next.png" />
                    <asp:ImageButton ID="NavImageLast" CssClass="forum-prev pointer" runat="server" OnClick="NavImageLast_Click" ImageUrl="~/DesktopModules/ActiveForums/images/forum-page-next-last.png" />
                </div>
            </div>
        </div>
        <div class="forum-right-col col-sm-4 forum-intro-match-height"></div>
        <div class="clearfix"></div>

        <asp:Panel runat="server" ID="pnlNoRecords" Style="width: 100%; margin: 40px;" Visible="false">
            <asp:Label ID="lblNoRecords" runat="server" Text="Sorry, no topic(s) found."></asp:Label>
        </asp:Panel>
        <asp:Repeater ID="rptTopics" runat="server" OnItemDataBound="rptTopics_ItemDataBound" OnItemCommand="rptTopics_ItemCommand">
            <ItemTemplate>
                <div class="forum-row-wrapper mainContentSections  dvEachTopicContentId_<%#DataBinder.Eval(Container.DataItem, "ContentId")%> dvEachTopicReplyId_<%#DataBinder.Eval(Container.DataItem, "TopicId")%>" contentid="<%#DataBinder.Eval(Container.DataItem, "ContentId")%> " id="dvEachTopic_<%#DataBinder.Eval(Container.DataItem, "TopicId")%>">
                    <div class="forum-left-col col-sm-8 forum-match-height">
                        <div class="forum-post">
                            <div class="forum-more-top">
                                <a href="#" class="forum-see-more-top active">Collapse</a>
                                <div class="forum-bio">
                                    <div class="forum-bio-left">


                                        <asp:HyperLink ID="lnkEditTopic" runat="server" Text="Edit"></asp:HyperLink>

                                        <%#DotNetNuke.Modules.ActiveForums.UserProfiles.GetAvatarUrl(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "GetContent.AuthorId")), 49, 49,true)%>
                                        <asp:HiddenField ID="hdnTopicId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "TopicId")%>' />
                                        <asp:HiddenField ID="hdnCategoryID" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "CategoryID")%>' />
                                        <asp:HiddenField ID="hdnSubject" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "GetContent.Subject")%>' />




                                    </div>
                                    <div class="forum-bio-right">
                                        <strong><%#DataBinder.Eval(Container.DataItem, "GetContent.AuthorSignatureWithLink")%></strong><br>
                                        <%#DataBinder.Eval(Container.DataItem, "GetContent.AuthorSubTitle")%><%--, <%#DataBinder.Eval(Container.DataItem, "GetContent.AuthorProfiler.UserCaption")%>--%><br>
                                    </div>
                                    </a>  
                                </div>
                                <div class="clearfix"></div>
                                <%-- <div class="forum-post-date">
                                    Posted this Discussion <%#DataBinder.Eval(Container.DataItem, "GetContent.DateCreated","{0:MMM dd-yyyy hh:mm tt}")%>
                                    <%#DataBinder.Eval(Container.DataItem, "GetContent.EditDisplayStyle")%>
                                </div>--%>
                            </div>
                            <div class="forum-content">
                                <asp:Panel ID="pnlMyPost" runat="server" CssClass="forum-my-post" stye="width:100%;">
                                    <asp:HyperLink ID="hyperlink" runat="server">My post</asp:HyperLink>
                                </asp:Panel>
                                <h3>
                                    <asp:HyperLink ID="hypertopicdetail" runat="server"></asp:HyperLink>
                                </h3>
                                <asp:HiddenField ID="hdnSub" Value='<%#DataBinder.Eval(Container.DataItem, "GetContent.Subject")%>' runat="server" />
                                <asp:Panel ID="pnlUserMenu" runat="server" class="forum-tooltips">
                                    <asp:Image runat="server" ID="imgUserMenu" Style="cursor: pointer;" src="/images/more_icon.png" alt="Tips" />

                                </asp:Panel>
                                <div class="clearfix"></div>
                                <%--<p style="font-size: 12px; text-transform: uppercase;"><%#DataBinder.Eval(Container.DataItem, "GetTopicCategoriesName")%>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Image ID="imgStatus" runat="server" Width="9" Height="9" />&nbsp;&nbsp;<asp:Label ID="lblUserActivityStatus" runat="server"></asp:Label></p>
                                --%>
                                <br />
                                <%--<asp:Label ID="lblBody" runat="server"></asp:Label>--%>
                            </div>


                            <div class="forum-interact">
                                <div style="float: left;">
                                    <a onclick='GetTopicLikeToggle(<%#Eval("GetContent.ContentId")%>,4)' id="hyper4_<%#Eval("GetContent.ContentId")%>">
                                        <span id="Main4_count<%#Eval("GetContent.ContentId") %>" style="color: #2d4d79;">
                                            <asp:Image ID="imgrecommanded" runat="server" />
                                            &nbsp;&nbsp;<%#DataBinder.Eval(Container.DataItem, "GetContent.ContentLikes")%>&nbsp;&nbsp;
                                        </span>
                                    </a>
                                </div>
                                <%--<div style="float: left; padding-left: 30px">
                                    <a onclick='GetTopicLikeToggle(<%#Eval("GetContent.ContentId")%>,5)' id="hyper5_<%#Eval("GetContent.ContentId")%>">



                                        <span id="Main5_count<%#Eval("GetContent.ContentId") %>" style="color: #2d4d79;">
                                            <asp:Image ID="imgunrecommanded" runat="server" />&nbsp;&nbsp;<%#DataBinder.Eval(Container.DataItem, "GetContent.ContentDisLikes")%>&nbsp;&nbsp;
                                        </span>

                                    </a>
                                </div>--%>
                                <div style="float: left; padding-left: 30px" class="forum-see-more-reply">
                                    <asp:Image ID="imgbtnReplies" ImageUrl="/DesktopModules/ActiveForums/images/forum-comments.png" runat="server" />&nbsp;<%#DataBinder.Eval(Container.DataItem, "ReplyCount")%><span style="font-size: 14px; font-weight: 300;">&nbsp;&nbsp;REPLIES</span>
                                </div>

                                <table width="40%" style="display: none">
                                    <tr>
                                        <td style="width: 25%"></td>

                                        <td style="width: 25%; display: none">
                                            <asp:ImageButton ID="imgbtnUnRecommend" ImageUrl="~/DesktopModules/ActiveForums/images/forum-unrecommend.png" runat="server" />
                                        </td>



                                    </tr>
                                </table>
                            </div>


                            <div class="clearfix"></div>
                            <div class="forum-more-bottom">
                                <asp:Label ID="lblBody" runat="server"></asp:Label>
                                <div>
                                    <div class="dvUserReply" style="width: 95%; float: left; margin-top: 15px; margin-bottom: 5px;">
                                        <div>Reply to <span><%#DataBinder.Eval(Container.DataItem, "GetContent.AuthorSignatureWithLink")%> ,</span></div>
                                    </div>

                                    <%--Begin Add By Fali--%>
                                    <asp:Panel ID="Panel11" runat="server" class="forum-tooltips">
                                        <asp:Image runat="server" ID="ImagePostAcctachment"
                                            Style="cursor: pointer; margin-top: 15px; margin-right: 2px;" src="/images/more_icon.png"
                                            alt="Tips" tid="0" cid="0"
                                            status="Completed" isselfassigned="7" class="CA_UserMenu" />
                                    </asp:Panel>


                                    <input type="hidden" id="hiddenUserId" value='<%=this.UserInfo.UserID%>' />
                                    <input type="hidden" id="hiddenIsAdminUser" value='<%=IsAuthorizeAdminUser(this.UserInfo.UserID)%>' />
                                    <div id="UploadedAttachment">
                                    </div>


                                    <asp:TextBox runat="server" ID="UploadedAttachmentSavePost" TextMode="MultiLine" CssClass="UploadedAttachmentSavePost multilineText UploadedAttachmentSavePostHide" Rows="3" Columns="20"></asp:TextBox>

                                    <asp:TextBox runat="server" ID="TextBoxAttachmentData" TextMode="MultiLine" CssClass="TextBoxAttachmentData multilineText UploadedAttachmentSavePostHide" Rows="3" Columns="20"></asp:TextBox>



                                    <%--End Add By Fali--%>
                                    <asp:TextBox PlaceHolder="Reply: Your thoughts" ID="txtQuickReply" runat="server" TextMode="MultiLine" CssClass="multilineText  txtQuickReplyBox" Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqFieldMain" runat="server" ControlToValidate="txtQuickReply"
                                        ValidationGroup='<%#String.Format("main_{0}", DataBinder.Eval(Container.DataItem, "TopicId")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                    <div style="float: right; margin-top: 8px;">
                                        <a onclick="addFile(this,<%#DataBinder.Eval(Container.DataItem, "GetContent.ContentID")%>)" style="display: none;">
                                            <img src="/DesktopModules/Activeforums/images/add_attach.png" alt="attach" />
                                        </a>



                                        <asp:Button ID="lnkSve" runat="server" CssClass="btn btn-primary" OnClientClick='saveAttachmentDataWhenPost(this)' currentPostContenetId='<%#DataBinder.Eval(Container.DataItem, "ContentId")%>' ValidationGroup='<%#String.Format("main_{0}", DataBinder.Eval(Container.DataItem, "TopicId")) %>' CommandArgument="SaveComment" CommandName="ReplySave" Text="POST REPLY"></asp:Button>
                                    </div>
                                    <span style="display: none">
                                        <input type="text" id="txtSubject" class="aftextbox" readonly="readonly" value='<%#DataBinder.Eval(Container.DataItem, "GetContent.Subject")%>' /></span>
                                </div>
                                <div class="forum-posts-tab">

                                    <div class="clearfix"></div>
                                    <asp:Repeater ID="rptTopicDissusssion" runat="server" OnItemCommand="rptTopicDissusssion_ItemCommand" OnItemDataBound="rptTopicDissusssion_ItemDataBound">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnInsideTopic" runat="server" Value='<%#DataBinder.Eval(Container.NamingContainer.NamingContainer, "DataItem.TopicId")%>' />

                                            <asp:HiddenField ID="hdnReplyTo" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ReplyId")%>' />
                                            <asp:HiddenField ID="hdnContentId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ContentId")%>' />
                                            <asp:HiddenField runat="server" ID="hdnInnerSubject" Value='<%#DataBinder.Eval(Container.DataItem, "GetContent.Subject")%>' />
                                            <asp:HiddenField runat="server" ID="hdnCategoryID" Value='<%#DataBinder.Eval(Container.NamingContainer.NamingContainer, "DataItem.CategoryID")%>' />
                                            <div class="forum-tab-one active" id="tabs-one">
                                                <div class="forum-comments-row">
                                                    <div id="dvForum_Comment_Content_<%#Eval("GetContent.ContentId")%>" class="forum-comments-content dvEachTopicContentId_<%#Eval("GetContent.ContentId")%> dvEachTopicReplyId_<%#DataBinder.Eval(Container.DataItem, "ReplyId")%>" style="<%#GetBackgroundImage(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")))%>">
                                                        <h4 style="width: 97%; float: left;">
                                                            <%#DataBinder.Eval(Container.DataItem, "GetContent.AuthorSignatureWithLink")%>
                                                            <%--<span>&nbsp;::  
                                                          &nbsp;<%#DataBinder.Eval(Container.DataItem, "ForatedCreateDate")%></span>--%>&nbsp;::&nbsp;<small><b><asp:Label ID="lblReplyCount" runat="server"></asp:Label></b>&nbsp;&nbsp;REPLIES</small>

                                                            <%#EditedReplyContent(Convert.ToString(DataBinder.Eval(Container.DataItem, "EditedAuthorName")), Convert.ToDateTime( DataBinder.Eval(Container.DataItem, "DateUpdated")))%>

                                                        </h4>

                                                        <%--First Leavel Replies Edit Button --%>
                                                        <asp:Panel ID="PanelUpdateReplySection" runat="server" class="forum-tooltips PanelUpdateReplySection" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")))%>'>
                                                            <asp:Image runat="server" ID="ImagePostAcctachment"
                                                                Style="cursor: pointer; margin-top: 0px; margin-right: 2px;" src="/images/more_icon.png"
                                                                alt="Tips" tid="0" cid='<%#DataBinder.Eval(Container.DataItem, "ContentId")%>'
                                                                rToId='<%#DataBinder.Eval(Container.DataItem, "ReplyId")%>'
                                                                status="Completed" isselfassigned="8" class="CA_UserMenu" />
                                                        </asp:Panel>

                                                        <p>

                                                            <div class="ReplyContentSectionBody ReplyContentSection_<%#DataBinder.Eval(Container.DataItem, "ContentId")%>" cid="<%#DataBinder.Eval(Container.DataItem, "ContentId")%>">

                                                                <%#GetContentAttachment(Convert.ToInt16(DataBinder.Eval(Container.DataItem, "ContentId")))%>

                                                                <div class="BodyContent"><%#DataBinder.Eval(Container.DataItem, "Body")%></div>
                                                            </div>

                                                            <div class="ReplyEditContentSection_<%#DataBinder.Eval(Container.DataItem, "ContentId")%>" style="display: none;">

                                                                <div class="UserReplyNameSection" style="width: 98%; float: left;">
                                                                    <div>Edit my reply to <span><%#DataBinder.Eval(Container.DataItem, "GetContent.AuthorSignatureWithLink")%> ,</span></div>
                                                                </div>

                                                                <asp:TextBox PlaceHolder="Reply: Your thoughts" ID="txtQuickReplyEditOne" runat="server" TextMode="MultiLine" CssClass='multilineText ReplyEditContent' Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqFieldMainOne" runat="server" ControlToValidate="txtQuickReplyEditOne" CssClass="EditPostValidation" Display="Dynamic"
                                                                    ValidationGroup='<%#String.Format("FirstLevel_{0}", DataBinder.Eval(Container.DataItem, "ContentId")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>


                                                                <div class="EditAttachmentContent EditReplyAttachmentSection_<%#DataBinder.Eval(Container.DataItem, "ContentId")%>">
                                                                </div>




                                                                <asp:TextBox PlaceHolder="Reply: Your thoughts" ID="TextBoxAttachmentContentOne" runat="server" TextMode="MultiLine" CssClass='multilineText ReplyEditContentAttachmentData' Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>

                                                                <asp:HiddenField ID="HiddenFieldCurrentContentId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ContentId")%>' />

                                                                <input type="hidden" value="" id="HiddenReplyEditContent_<%#DataBinder.Eval(Container.DataItem, "ContentId")%>" />



                                                                <asp:LinkButton
                                                                    ID="EditLevelOneReply"
                                                                    runat="server"
                                                                    CommandName="EditReply"
                                                                    CssClass="EditPostReplyButton"
                                                                    ValidationGroup='<%#string.Format("FirstLevel_{0}", DataBinder.Eval(Container.DataItem, "ContentId")) %>'>
                                                                 POST EDITED REPLY</asp:LinkButton>

                                                                <a onclick="EditReplyCancle(this)" class="CancelPostReplyButton" cid="<%#DataBinder.Eval(Container.DataItem, "ContentId")%>">CANCEL</a>


                                                                <asp:LinkButton
                                                                    ID="LinkButtonDeleteReply"
                                                                    OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');"
                                                                    CommandName="delete"
                                                                    CssClass="deleteReplyOne"
                                                                    runat="server"
                                                                    AlternateText="Delete"
                                                                    CommandArgument="deleteInnerReply"
                                                                    Visible='<%#IsAuthorizeForDelete(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")),Convert.ToString(DataBinder.Eval(Container.DataItem, "ReplyCount")))%>'>
                                                                    Delete My Reply
                                                                </asp:LinkButton>
                                                                <%--pROPER--%>
                                                            </div>


                                                            <div class="AttchmentListSection AttchmentListSection_<%#Eval("GetContent.ContentId")%>"></div>

                                                            <div style="display: inline-block; cursor: pointer">
                                                                &nbsp;<a class="forum-reply" id='replyShow_1_<%#Eval("GetContent.ContentId")%>' onclick="javascript:ExpandReplyBox1('ReplyInner2_1_<%#Eval("GetContent.ContentId")%>','<%#Eval("GetContent.ContentId")%>')" style="cursor: pointer">Reply</a>

                                                                <a onclick='GetLikesCount(<%#Eval("GetContent.ContentId")%>,3)' id="hyper3_" class="forum-like">
                                                                    <span id="bottom_count<%#Eval("GetContent.ContentId") %>">
                                                                        <img id="imgLikeCount_<%#Eval("GetContent.ContentId")%>" style="width: 14px; height: 16px;" src="<%# String.Format("{0}/DesktopModules/Activeforums/images/{1}", WebServicePath, GetImageName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "GetContent.ContentId")))) %>" />
                                                                        &nbsp;LIKE&nbsp;<label id="lblLikeCount_<%#Eval("GetContent.ContentId")%>"><%#DataBinder.Eval(Container.DataItem, "GetContent.ContentLikes")%></label>
                                                                    </span>
                                                                </a>


                                                                <span cid="<%#Eval("GetContent.ContentId")%>" class="SeeAttachedMediaLinkContent" onclick="SeeAttachedMediaList(this)">
                                                                    <img style="width: 20px; height: 20px;" src="<%# String.Format("{0}/DesktopModules/Activeforums/images/{1}", WebServicePath, "CAAttach.png") %>" />
                                                                    &nbsp;SEE ATTACHED MEDIA&nbsp;
                                                                </span>


                                                                <asp:ImageButton ID="imgdelete" OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');"
                                                                    CommandName="delete" runat="server" AlternateText="Delete" ImageUrl="~/DesktopModules/ActiveForums/images/small_close_btn1.png" CommandArgument="deleteInnerReply" Visible='<%#IsAuthorizeForDelete(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")),Convert.ToString(DataBinder.Eval(Container.DataItem, "ReplyCount")))%>' />
                                                            </div>



                                                            <div class="forum-recommend1" style="display: none;">

                                                                <a onclick='GetLikesCount(<%#Eval("GetContent.ContentId")%>,2)' id="hyper2_<%#Eval("GetContent.ContentId") %>" style="cursor: pointer">
                                                                    <span id="Span_count<%#Eval("GetContent.ContentId") %>"><%#DataBinder.Eval(Container.DataItem, "GetContent.ContentLikes")%>
                                                                        <img src="<%# String.Format("{0}/DesktopModules/Activeforums/images/{1}", WebServicePath, GetImageName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "GetContent.ContentId")))) %>" />
                                                                    </span>
                                                                </a>

                                                            </div>
                                                            <div class="clearfix"></div>

                                                        </p>
                                                        <div class="clearfix" style="float: right; font-size: 12px; cursor: pointer">
                                                        </div>
                                                        <div class="forum-row-wrapper1" style="margin-top: 10px; display: none" id="ReplyInner2_1_<%#Eval("GetContent.ContentId")%>">
                                                            <div style="display: none">
                                                                <asp:ImageButton ID="ImageButton1" Style="width: 14px; height: 14px;" OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');" CommandName="RemoveComment1" runat="server" AlternateText="Delete" ImageUrl="~/DesktopModules/ActiveForums/images/small_close_btn1.png" CommandArgument="deleteInnerReply" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")))%>' />
                                                            </div>
                                                            <div class="dvUserReply" style="width: 98%; float: left;">
                                                                <div>Reply to <span><%#DataBinder.Eval(Container.DataItem, "GetContent.AuthorSignatureWithLink")%> ,</span></div>
                                                            </div>


                                                            <%--Begin add --%>
                                                            <asp:Panel ID="PanelInnerComment" runat="server" class="forum-tooltips InnerAttachButtons">
                                                                <asp:Image runat="server" ID="InnerImagePostAcctachment"
                                                                    Style="cursor: pointer; margin-top: 15px; margin-right: 2px;" src="/images/more_icon.png"
                                                                    alt="Tips" tid="0"
                                                                    cid='<%#DataBinder.Eval(Container.DataItem, "ContentId")%>'
                                                                    status="Completed" isselfassigned="7" class="CA_UserMenu" />
                                                            </asp:Panel>


                                                            <div id="UploadedAttachment">
                                                            </div>


                                                            <asp:TextBox runat="server" ID="UploadedAttachmentSavePost" TextMode="MultiLine" CssClass="UploadedAttachmentSavePost multilineText UploadedAttachmentSavePostHide" Rows="3" Columns="20"></asp:TextBox>
                                                            <asp:TextBox runat="server" ID="TextBoxAttachmentData" TextMode="MultiLine" CssClass="TextBoxAttachmentData multilineText UploadedAttachmentSavePostHide" Rows="3" Columns="20"></asp:TextBox>
                                                            <%--End add --%>


                                                            <asp:TextBox ID="txtInnerQuickReply" runat="server" PlaceHolder='<%#string.Format("Reply to {0} ::",DataBinder.Eval(Container.DataItem, "GetContent.AuthorName")) %>' TextMode="MultiLine" CssClass="multilineText" Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="reqField" runat="server" ControlToValidate="txtInnerQuickReply"
                                                                ValidationGroup='<%#string.Format("Child_{0}", DataBinder.Eval(Container.DataItem, "ReplyId")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <div style="float: right; cursor: pointer; margin-top: 5px;">
                                                                <a onclick="addFileSubTopic(this,<%#DataBinder.Eval(Container.NamingContainer.NamingContainer, "DataItem.GetContent.ContentId")%>,<%#Eval("GetContent.ContentId")%>)" id="attach_<%#Eval("GetContent.ContentId")%>" style="display: none;">
                                                                    <img src="/DesktopModules/Activeforums/images/add_attach.png" alt="attach" />


                                                                </a>
                                                                <span id="filename_<%#Eval("GetContent.ContentId")%>"></span><a onclick="addFileSubTopic(this,<%#DataBinder.Eval(Container.NamingContainer.NamingContainer, "DataItem.GetContent.ContentId")%>)" id="delete_<%#Eval("GetContent.ContentId")%>" style="display: none">
                                                                    <img src="/DesktopModules/Activeforums/images/delete12.png" alt="attach" />
                                                                </a>
                                                                <input type="hidden" id="hdnAttach_<%#Eval("GetContent.ContentId")%>" />
                                                                <input type="hidden" id="hdnAttachID_<%#Eval("GetContent.ContentId")%>" />
                                                                <asp:ImageButton ID="ImageButton2" Visible="false" Style="cursor: pointer" ValidationGroup='<%#string.Format("Child_{0}", DataBinder.Eval(Container.DataItem, "ReplyId")) %>' CommandArgument="SaveReply" CommandName="SaveDisscussion" runat="server" AlternateText="Reply" ImageUrl="~/DesktopModules/ActiveForums/images/small-reply.png" />
                                                                <asp:Button ID="lnkSve" runat="server" OnClientClick='saveAttachmentDataWhenPost(this)' currentPostContenetId='<%#Eval("GetContent.ContentId")%>' CssClass="btn btn-primary btn-xs" ValidationGroup='<%#string.Format("Child_{0}", DataBinder.Eval(Container.DataItem, "ReplyId")) %>' CommandArgument="SaveReply" Visible="true" CommandName="SaveDisscussion" Text="POST"></asp:Button>
                                                                <a href="#" class="forum-see-more" message="DONT Remove This element(Sachin)" style="display: none" id="lnkSeeMore1_<%#Eval("GetContent.ContentId") %>">See more</a>
                                                                <a id='replyHide_1_<%#Eval("GetContent.ContentId")%>' onclick="javascript:ExpandReplyBoxClose1('ReplyInner2_1_<%#Eval("GetContent.ContentId")%>','<%#Eval("GetContent.ContentId")%>')" style="display: none">Close</a>
                                                            </div>
                                                        </div>
                                                        <asp:Repeater ID="rptDissussionInner" runat="server" OnItemCommand="rptTopicDissusssion_ItemCommand" OnItemDataBound="rptDissussionInner_ItemDataBound">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnInsideTopic" runat="server" Value='<%#DataBinder.Eval(Container.NamingContainer.NamingContainer, "DataItem.TopicId")%>' />
                                                                <asp:HiddenField ID="hdnInnerSubject" runat="server" Value='<%#DataBinder.Eval(Container.NamingContainer.NamingContainer, "DataItem.Subject")%>' />
                                                                <asp:HiddenField ID="hdnReplyTo" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ReplyId")%>' />
                                                                <asp:HiddenField runat="server" ID="hdnCategoryID" Value="-1" />
                                                                <asp:HiddenField ID="hdnContentId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ContentId")%>' />
                                                                <div id="dvForum_Comment_Content_<%#Eval("GetContent.ContentId")%>" class="forum-comments-sub-content dvEachTopicContentId_<%#Eval("GetContent.ContentId")%>  dvEachTopicReplyId_<%#DataBinder.Eval(Container.DataItem, "ReplyId")%>" style="<%#GetBackgroundImage(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")))%>">

                                                                    <h4 style="width: 97%; float: left;"><%#DataBinder.Eval(Container.DataItem, "GetContent.AuthorSignatureWithLink")%><%--<span>&nbsp;::&nbsp;<%#DataBinder.Eval(Container.DataItem, "ForatedCreateDate")%></span>--%> &nbsp;

                                                                         <%#EditedReplyContentSecond(Convert.ToString(DataBinder.Eval(Container.DataItem, "EditedAuthorName")), Convert.ToDateTime( DataBinder.Eval(Container.DataItem, "DateUpdated")))%>

                                                                        <asp:ImageButton ID="imgdelete" OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');" CommandName="delete" runat="server" ImageUrl="~/DesktopModules/ActiveForums/images/small_close_btn1.png" CommandArgument="deleteInnerReply"
                                                                            Visible='<%#IsAuthorizeForDelete(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")),Convert.ToString(DataBinder.Eval(Container.DataItem, "ReplyCount")))%>' CssClass="tip" />

                                                                    </h4>

                                                                    <%--Second Level Replies Edit Button --%>
                                                                    <asp:Panel ID="PanelUpdateReplySecondLevelSection" runat="server" class="forum-tooltips PanelUpdateReplySection" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")))%>'>
                                                                        <asp:Image runat="server" ID="ImagePostAcctachment"
                                                                            Style="cursor: pointer; margin-top: 0px; margin-right: 2px;" src="/images/more_icon.png"
                                                                            alt="Tips" tid="0" cid='<%#DataBinder.Eval(Container.DataItem, "ContentId")%>'
                                                                            rToId='<%#DataBinder.Eval(Container.DataItem, "ReplyId")%>'
                                                                            status="Completed" isselfassigned="8" class="CA_UserMenu" />
                                                                    </asp:Panel>

                                                                    <p>
                                                                        <div class="ReplyContentSectionBody ReplyContentSection_<%#DataBinder.Eval(Container.DataItem, "ContentId")%>" cid="<%#DataBinder.Eval(Container.DataItem, "ContentId")%>">
                                                                            <%#GetContentAttachment(Convert.ToInt16(DataBinder.Eval(Container.DataItem, "ContentId")))%>
                                                                            <div class="BodyContent"><%#DataBinder.Eval(Container.DataItem, "Body")%></div>
                                                                        </div>

                                                                        <div class="ReplyEditContentSection_<%#DataBinder.Eval(Container.DataItem, "ContentId")%>" style="display: none;">

                                                                            <div class="UserReplyNameSection" style="width: 98%; float: left;">
                                                                                <div>Edit my reply to <span><%#DataBinder.Eval(Container.DataItem, "GetContent.AuthorSignatureWithLink")%> ,</span></div>
                                                                            </div>

                                                                            <asp:TextBox PlaceHolder="Reply: Your thoughts" ID="txtQuickReplySecond" runat="server" TextMode="MultiLine" CssClass='multilineText ReplyEditContent' Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="reqFieldMainSecond" runat="server" ControlToValidate="txtQuickReplySecond" CssClass="EditPostValidation" Display="Dynamic"
                                                                                ValidationGroup='<%#String.Format("SecondLevel_{0}", DataBinder.Eval(Container.DataItem, "ContentId")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                                            <div class="EditAttachmentContent EditReplyAttachmentSection_<%#DataBinder.Eval(Container.DataItem, "ContentId")%>">
                                                                            </div>


                                                                            <asp:TextBox PlaceHolder="Reply: Your thoughts" ID="TextBoxAttachmentSecond" runat="server" TextMode="MultiLine" CssClass='multilineText ReplyEditContentAttachmentData' Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                                            <asp:HiddenField ID="HiddenFieldCurrentContentId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ContentId")%>' />
                                                                            <input type="hidden" value="" id="HiddenReplyEditContent_<%#DataBinder.Eval(Container.DataItem, "ContentId")%>" />


                                                                            <asp:LinkButton
                                                                                ID="EditLevelOneReply"
                                                                                runat="server"
                                                                                CommandName="EditDisscussionReply"
                                                                                CssClass="EditPostReplyButton"
                                                                                ValidationGroup='<%#string.Format("SecondLevel_{0}", DataBinder.Eval(Container.DataItem, "ContentId")) %>'>
                                                                               POST EDITED REPLY</asp:LinkButton>


                                                                            <a onclick="EditReplyCancle(this)" class="CancelPostReplyButton" cid="<%#DataBinder.Eval(Container.DataItem, "ContentId")%>">CANCEL</a>



                                                                            <asp:LinkButton ID="ImageButtonDeleteReplySecond"
                                                                                OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');"
                                                                                CommandName="delete"
                                                                                runat="server"
                                                                                CommandArgument="deleteInnerReply"
                                                                                Visible='<%#IsAuthorizeForDelete(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")),Convert.ToString(DataBinder.Eval(Container.DataItem, "ReplyCount")))%>'
                                                                                CssClass="tip deleteReplySecond"> 
                                                                                Delete My Reply
                                                                            </asp:LinkButton>

                                                                        </div>

                                                                        <div class="AttchmentListSection AttchmentListSection_<%#Eval("GetContent.ContentId")%>">
                                                                        </div>

                                                                        <div class="forum-recommend1" style="display: block;">

                                                                            <a class="forum-reply" id='replyShow_<%#Eval("GetContent.ContentId")%>' onclick="javascript:ExpandReplyBox('ReplyInner2_<%#Eval("GetContent.ContentId")%>','<%#Eval("GetContent.ContentId")%>')" style="cursor: pointer">Reply</a>
                                                                            &nbsp;<a onclick='GetLikesCount(<%#Eval("GetContent.ContentId")%>,3)' id="hyper3_" style="cursor: pointer">
                                                                                <span id="bottom_count<%#Eval("GetContent.ContentId") %>">
                                                                                    <img id="imgLikeCount_<%#Eval("GetContent.ContentId")%>" style="width: 14px; height: 16px; cursor: pointer" src="<%# String.Format("{0}/DesktopModules/Activeforums/images/{1}", WebServicePath, GetImageName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "GetContent.ContentId")))) %>" />
                                                                                    &nbsp;LIKE&nbsp;<label id="lblLikeCount_<%#Eval("GetContent.ContentId")%>"><%#DataBinder.Eval(Container.DataItem, "GetContent.ContentLikes")%></label>

                                                                                </span>
                                                                            </a>

                                                                            <span cid="<%#Eval("GetContent.ContentId")%>" class="SeeAttachedMediaLinkContent" onclick="SeeAttachedMediaList(this)">
                                                                                <img style="width: 20px; height: 20px;" src="<%# String.Format("{0}/DesktopModules/Activeforums/images/{1}", WebServicePath, "CAAttach.png") %>" />
                                                                                &nbsp;SEE ATTACHED MEDIA&nbsp;
                                                                            </span>


                                                                        </div>



                                                                    </p>
                                                                    <div class="forum-row-wrapper1" style="margin-top: 10px; display: none" id="ReplyInner2_<%#Eval("GetContent.ContentId")%>">
                                                                        <div style="display: none">
                                                                            <asp:ImageButton ID="ImageButton1" Style="width: 14px; height: 14px;" OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');" CommandName="RemoveComment1" runat="server" AlternateText="Delete" ImageUrl="~/DesktopModules/ActiveForums/images/small_close_btn1.png" CommandArgument="deleteInnerReply" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")))%>' />
                                                                        </div>
                                                                        <div class="dvUserReply" style="width: 97%; float: left;">
                                                                            <div>Reply to <span><%#DataBinder.Eval(Container.DataItem, "GetContent.AuthorSignatureWithLink")%> ,</span></div>
                                                                        </div>


                                                                        <%--Begin add 3 --%>
                                                                        <asp:Panel ID="PanelInnerComment" runat="server" class="forum-tooltips InnerAttachButtons">
                                                                            <asp:Image runat="server" ID="InnerImagePostAcctachment"
                                                                                Style="cursor: pointer; margin-top: 15px; margin-right: 2px;" src="/images/more_icon.png"
                                                                                alt="Tips" tid="0"
                                                                                cid='<%#DataBinder.Eval(Container.DataItem, "ContentId")%>'
                                                                                status="Completed" isselfassigned="7" class="CA_UserMenu" />
                                                                        </asp:Panel>


                                                                        <div id="UploadedAttachment">
                                                                        </div>


                                                                        <asp:TextBox runat="server" ID="UploadedAttachmentSavePost" TextMode="MultiLine" CssClass="UploadedAttachmentSavePost multilineText UploadedAttachmentSavePostHide" Rows="3" Columns="20"></asp:TextBox>
                                                                        <asp:TextBox runat="server" ID="TextBoxAttachmentData" TextMode="MultiLine" CssClass="TextBoxAttachmentData multilineText UploadedAttachmentSavePostHide" Rows="3" Columns="20"></asp:TextBox>
                                                                        <%--End add 3--%>



                                                                        <asp:TextBox ID="txtInnerQuickReply" runat="server" PlaceHolder='<%#string.Format("Reply to {0} ::",DataBinder.Eval(Container.DataItem, "GetContent.AuthorName")) %>' TextMode="MultiLine" CssClass="multilineText" Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="reqField" runat="server" ControlToValidate="txtInnerQuickReply"
                                                                            ValidationGroup='<%#string.Format("Child_{0}", DataBinder.Eval(Container.DataItem, "ReplyId")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                        <div style="float: right; cursor: pointer; margin-top: 5px;">
                                                                            <asp:ImageButton ID="ImageButton2" Visible="false" Style="cursor: pointer" ValidationGroup='<%#string.Format("Child_{0}", DataBinder.Eval(Container.DataItem, "ReplyId")) %>' CommandArgument="SaveReply" CommandName="SaveDisscussion" runat="server" AlternateText="Reply" ImageUrl="~/DesktopModules/ActiveForums/images/small-reply.png" />
                                                                            <a onclick="addFile(this,<%#DataBinder.Eval(Container.NamingContainer.NamingContainer.Parent.NamingContainer, "DataItem.GetContent.ContentId")%>)" style="display: none;">
                                                                                <img src="/DesktopModules/Activeforums/images/add_attach.png" alt="attach" />
                                                                            </a>
                                                                            <asp:Button ID="lnkSve" runat="server" OnClientClick='saveAttachmentDataWhenPost(this)' currentPostContenetId='<%#Eval("GetContent.ContentId")%>' CssClass="btn btn-primary btn-xs" ValidationGroup='<%#string.Format("Child_{0}", DataBinder.Eval(Container.DataItem, "ReplyId")) %>' CommandArgument="SaveReply" Visible="true" CommandName="SaveDisscussion" Text="POST"></asp:Button>
                                                                            &nbsp;<a id='replyHide_<%#Eval("GetContent.ContentId")%>' onclick="javascript:ExpandReplyBoxClose('ReplyInner2_<%#Eval("GetContent.ContentId")%>','<%#Eval("GetContent.ContentId")%>')" style="display: none">Close</a> <a href="#" class="forum-see-more" message="DONT Remove This element(Sachin)" style="display: none" id="lnkSeeMore1_<%#Eval("GetContent.ContentId") %>">See more</a>
                                                                        </div>
                                                                    </div>
                                                                    <asp:Repeater ID="rptTopicDisuccsionInner" runat="server" OnItemCommand="rptTopicDisuccsionInner_ItemCommand">
                                                                        <ItemTemplate>
                                                                            <asp:HiddenField ID="hdnInsideTopic" runat="server" Value='<%#DataBinder.Eval(Container.NamingContainer.NamingContainer, "DataItem.TopicId")%>' />
                                                                            <asp:HiddenField ID="hdnInnerSubject" runat="server" Value='<%#DataBinder.Eval(Container.NamingContainer.NamingContainer, "DataItem.Subject")%>' />
                                                                            <asp:HiddenField ID="hdnReplyTo" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ReplyId")%>' />
                                                                            <asp:HiddenField runat="server" ID="hdnCategoryID" Value="-1" />
                                                                            <asp:HiddenField ID="hdnContentId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ContentId")%>' />
                                                                            <div id="dvForum_Comment_Content_<%#Eval("GetContent.ContentId") %>" class="forum-comments-sub-content dvEachTopicContentId_<%#Eval("GetContent.ContentId")%> dvEachTopicReplyId_<%#DataBinder.Eval(Container.DataItem, "ReplyId")%>" style="<%#GetBackgroundImage(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")))%>">
                                                                                <a href="#"></a>
                                                                                <h4 style="width: 97%; float: left;"><%#DataBinder.Eval(Container.DataItem, "GetContent.AuthorSignatureWithLink")%><%--<span>&nbsp;::&nbsp;<%#DataBinder.Eval(Container.DataItem, "ForatedCreateDate")%></span>--%> &nbsp;

                                                                                 <%#EditedReplyContentThird(Convert.ToString(DataBinder.Eval(Container.DataItem, "EditedAuthorName")), Convert.ToDateTime( DataBinder.Eval(Container.DataItem, "DateUpdated")))%>

                                                                                    <%--Third Level Replies Edit Button --%>
                                                                                    <asp:Panel ID="PanelUpdateReplyThirdLevelSection" runat="server" class="forum-tooltips PanelUpdateReplySection" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")))%>'>
                                                                                        <asp:Image runat="server" ID="ImagePostAcctachment"
                                                                                            Style="cursor: pointer; margin-top: 0px; margin-right: 2px;" src="/images/more_icon.png"
                                                                                            alt="Tips" tid="0" cid='<%#DataBinder.Eval(Container.DataItem, "ContentId")%>'
                                                                                            rToId='<%#DataBinder.Eval(Container.DataItem, "ReplyId")%>'
                                                                                            status="Completed" isselfassigned="8" class="CA_UserMenu" />
                                                                                    </asp:Panel>


                                                                                    <asp:ImageButton ID="imgdelete" OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');" CommandName="delete" runat="server" ImageUrl="~/DesktopModules/ActiveForums/images/small_close_btn1.png" CommandArgument="deleteInnerReply" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")))%>' CssClass="tip" />


                                                                                    <%--<asp:ImageButton ID="imgdelete" OnClientClick="javascript: return confirmCommentDelete(return confirm('Are you sure want to delete your comments?'));" CommandName="delete" runat="server" ImageUrl="~/DesktopModules/ActiveForums/images/small_close_btn1.png" CommandArgument="deleteInnerReply" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")))%>' CssClass="tip" />--%>

                                                                                </h4>
                                                                                <p>
                                                                                    <div class="ReplyContentSectionBody ReplyContentSection_<%#DataBinder.Eval(Container.DataItem, "ContentId")%>" cid="<%#DataBinder.Eval(Container.DataItem, "ContentId")%>">
                                                                                        <%#GetContentAttachment(Convert.ToInt16(DataBinder.Eval(Container.DataItem, "ContentId")))%>
                                                                                        <div class="BodyContent"><%#DataBinder.Eval(Container.DataItem, "Body")%></div>
                                                                                    </div>

                                                                                    <div class="ReplyEditContentSection_<%#DataBinder.Eval(Container.DataItem, "ContentId")%>" style="display: none;">

                                                                                        <div class="UserReplyNameSection" style="width: 98%; float: left;">
                                                                                            <div>Edit my reply to <span><%#DataBinder.Eval(Container.DataItem, "GetContent.AuthorSignatureWithLink")%> ,</span></div>
                                                                                        </div>

                                                                                        <asp:TextBox PlaceHolder="Reply: Your thoughts" ID="txtQuickReplyThird" runat="server" TextMode="MultiLine" CssClass='multilineText ReplyEditContent' Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="reqFieldMainThird" runat="server" ControlToValidate="txtQuickReplyThird" CssClass="EditPostValidation" Display="Dynamic"
                                                                                            ValidationGroup='<%#String.Format("ThirdLevel_{0}", DataBinder.Eval(Container.DataItem, "ContentId")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                                                        <div class="EditAttachmentContent EditReplyAttachmentSection_<%#DataBinder.Eval(Container.DataItem, "ContentId")%>">
                                                                                        </div>

                                                                                        <asp:TextBox PlaceHolder="Reply: Your thoughts" ID="TextBoxAttachmentDataThird" runat="server" TextMode="MultiLine" CssClass='multilineText ReplyEditContentAttachmentData' Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>

                                                                                        <asp:HiddenField ID="HiddenFieldCurrentContentId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ContentId")%>' />
                                                                                        <input type="hidden" value="" id="HiddenReplyEditContent_<%#DataBinder.Eval(Container.DataItem, "ContentId")%>" />

                                                                                        <asp:LinkButton
                                                                                            ID="EditLevelOneReply"
                                                                                            runat="server"
                                                                                            CommandName="EditDisscussionReply"
                                                                                            CssClass="EditPostReplyButton"
                                                                                            ValidationGroup='<%#string.Format("ThirdLevel_{0}", DataBinder.Eval(Container.DataItem, "ContentId")) %>'>
                                                                               POST EDITED REPLY</asp:LinkButton>


                                                                                        <a onclick="EditReplyCancle(this)" class="CancelPostReplyButton" cid="<%#DataBinder.Eval(Container.DataItem, "ContentId")%>">CANCEL</a>

                                                                                        <asp:LinkButton
                                                                                            ID="ImageButtonDeleteReplyThird"
                                                                                            OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');"
                                                                                            CommandName="delete"
                                                                                            runat="server"
                                                                                            ImageUrl="~/DesktopModules/ActiveForums/images/small_close_btn1.png"
                                                                                            CommandArgument="deleteInnerReply"
                                                                                            Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")))%>'
                                                                                            CssClass="tip deleteMyReplyThird">
                                                                                            Delete My Reply
                                                                                        </asp:LinkButton>

                                                                                    </div>

                                                                                    <div class="AttchmentListSection AttchmentListSection_<%#Eval("GetContent.ContentId")%>">
                                                                                    </div>


                                                                                    <div class="forum-recommend1" style="display: block;">
                                                                                        <a onclick='GetLikesCount(<%#Eval("GetContent.ContentId")%>,3)' id="hyper3_">
                                                                                            <span id="bottom_count<%#Eval("GetContent.ContentId") %>">
                                                                                                <img id="imgLikeCount_<%#Eval("GetContent.ContentId")%>" style="width: 14px; height: 16px;" src="<%# String.Format("{0}/DesktopModules/Activeforums/images/{1}", WebServicePath, GetImageName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "GetContent.ContentId")))) %>" />
                                                                                                &nbsp;LIKE&nbsp;<label id="lblLikeCount_<%#Eval("GetContent.ContentId")%>"><%#DataBinder.Eval(Container.DataItem, "GetContent.ContentLikes")%></label>

                                                                                            </span>
                                                                                        </a>

                                                                                        <span cid="<%#Eval("GetContent.ContentId")%>" class="SeeAttachedMediaLinkContent" onclick="SeeAttachedMediaList(this)">
                                                                                            <img style="width: 20px; height: 20px;" src="<%# String.Format("{0}/DesktopModules/Activeforums/images/{1}", WebServicePath, "CAAttach.png") %>" />
                                                                                            &nbsp;SEE ATTACHED MEDIA&nbsp;
                                                                                        </span>


                                                                                    </div>



                                                                                </p>
                                                                                <div class="forum-row-wrapper1" style="margin-top: 10px; min-height: 100%; display: none" id="ReplyInner2_<%#Eval("GetContent.ContentId")%>">
                                                                                    <div style="display: none">
                                                                                        <asp:ImageButton ID="ImageButton1" Style="width: 14px; height: 14px;" OnClientClick="javascript: return confirm('Are you sure want to delete your comments?');" CommandName="RemoveComment1" runat="server" AlternateText="Delete" ImageUrl="~/DesktopModules/ActiveForums/images/small_close_btn1.png" CommandArgument="deleteInnerReply" Visible='<%#IsAuthorize(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")))%>' />
                                                                                    </div>
                                                                                    <asp:TextBox ID="txtInnerQuickReply" runat="server" PlaceHolder='<%#string.Format("Reply to {0} ::",DataBinder.Eval(Container.DataItem, "GetContent.AuthorName")) %>' TextMode="MultiLine" CssClass="multilineText" Rows="3" Columns="20" MaxLength="4000"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="reqField" runat="server" ControlToValidate="txtInnerQuickReply"
                                                                                        ValidationGroup='<%#string.Format("Child2_{0}", DataBinder.Eval(Container.DataItem, "ReplyId")) %>' ForeColor="Red" ErrorMessage="*Require" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                    <div style="float: right; cursor: pointer; margin-top: 5px;">
                                                                                        <asp:ImageButton ID="ImageButton2" Visible="false" Style="cursor: pointer" ValidationGroup='<%#string.Format("Child2_{0}", DataBinder.Eval(Container.DataItem, "ReplyId")) %>' CommandArgument="SaveReply" CommandName="SaveDisscussion" runat="server" AlternateText="Reply" ImageUrl="~/DesktopModules/ActiveForums/images/small-reply.png" />
                                                                                        <asp:Button ID="lnkSve" runat="server" CssClass="btn btn-primary btn-xs" ValidationGroup='<%#string.Format("Child2_{0}", DataBinder.Eval(Container.DataItem, "ReplyId")) %>' CommandArgument="SaveReply" Visible="true" CommandName="SaveDisscussion" Text="POST"></asp:Button>
                                                                                        &nbsp;<a id='replyHide_<%#Eval("GetContent.ContentId")%>' onclick="javascript:ExpandReplyBoxClose('ReplyInner2_<%#Eval("GetContent.ContentId")%>','<%#Eval("GetContent.ContentId")%>')" style="display: none">Close</a> <a href="#" class="forum-see-more" message="DONT Remove This element(Sachin)" style="display: none" id="lnkSeeMore1_<%#Eval("GetContent.ContentId") %>">See more</a>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <br />
                                                        <p>&nbsp;</p>

                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <a href="#" class="forum-see-more" id="lnkSeeMore_<%#Eval("GetContent.ContentId") %>">See more</a>
                            <div class="clearfix"></div>
                            <div class="forum-sep"></div>
                        </div>
                    </div>
                    <div class="forum-right-col col-sm-4 forum-match-height">

                        <div class="forum-right-status" style="display: none;">
                            <div class="forum-right-toggle-content">

                                <span id="membercount_<%#Eval("TopicID") %>">
                                    <asp:Label ID="lblMemberCount" CssClass="num" runat="server"></asp:Label>
                                </span>
                                <span class="title">most active members</span>
                                <a href="#" class="toggle-content right active">Collapse</a>
                                <div class="green-circle-small"></div>
                                <div class="clearfix"></div>
                                <div class="members-expand active" style="display: block;">
                                    <!--Anuj work Merged : dated 21st Feb -->
                                    <asp:Repeater runat="server" ID="rptActiveMember">
                                        <ItemTemplate>
                                            <div style="float: left">
                                                <a href="/MyProfile?Userid=">
                                                    <%#DotNetNuke.Modules.ActiveForums.UserProfiles.GetAvatarUrl(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AuthorId")), 49, 49,true)%>

                                                </a>
                                            </div>

                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div>
                            </div>
                            <div class="forum-right-toggle-content" style="display: none;">
                                <button type="button" id="btnAddFile" class="dnnPrimaryAction" onclick="addFile(this,<%#DataBinder.Eval(Container.DataItem, "ContentId")%>);">Add File</button>
                                <asp:HiddenField ID="hdnContentId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ContentId")%>' />
                            </div>
                            <div class="forum-right-toggle-content last">
                                <span id="filecount_<%#Eval("GetContent.ContentId") %>">
                                    <asp:Label ID="lblNum" runat="server"></asp:Label>
                                </span>
                                <span class="title">shared files</span>

                                <a href="#" class="toggle-content active right">Collapse</a>
                                <div class="clearfix"></div>
                                <div class="members-expand active">
                                    <div id="file_<%#Eval("GetContent.ContentId") %>" class="attachmentListRightSections">
                                        <asp:HiddenField ID="hdnAttachID" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ContentId")%>' />
                                        <asp:Repeater runat="server" ID="rptSharedFiles" OnItemDataBound="rptSharedFiles_ItemDataBound">
                                            <ItemTemplate>

                                                <%# String.Format("{0}", GetMemberName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "UserId")))) %>

                                                <a href="<%#String.Format("{0}",DataBinder.Eval(Container.DataItem, "filestackurl")) %>" target="_blank" attachid="<%#String.Format("{0}",DataBinder.Eval(Container.DataItem, "AttachId")) %>">

                                                    <p style="float: left; width: 100%; margin-bottom: 0px;"><%#DataBinder.Eval(Container.DataItem, "Filename")%></p>

                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>

                                </div>
                            </div>



                        </div>
                    </div>

                </div>
            </ItemTemplate>
        </asp:Repeater>

        <div class="forum-active-topics footer col-sm-6">
            <div class="forum-page-nav" id="bottomPaging" runat="server">

                <asp:ImageButton ID="NavFirstBtn2" CssClass="forum-prev pointer" runat="server" OnClick="NavFirstBtn_Click" ImageUrl="~/DesktopModules/ActiveForums/images/forum-page-prev-first.png" />
                <asp:ImageButton ID="NavImagePre2" CssClass="forum-prev pointer" runat="server" OnClick="NavImagePrev_Click" ImageUrl="~/DesktopModules/ActiveForums/images/forum-page-prev.png" />
                <asp:Label ID="lblPagerInfo1" runat="server" Text="1" CssClass="forum-page-count"></asp:Label>
                <asp:ImageButton ID="NavImageNext2" CssClass="forum-prev pointer" runat="server" OnClick="NavImageNext_Click" ImageUrl="~/DesktopModules/ActiveForums/images/forum-page-next.png" />
                <asp:ImageButton ID="NavImageLast2" CssClass="forum-prev pointer " runat="server" OnClick="NavImageLast_Click" ImageUrl="~/DesktopModules/ActiveForums/images/forum-page-next-last.png" />

            </div>
        </div>
        <div class="clearfix"></div>

        <div class="cnt_wrapper_rt col-sm-4">
        </div>
        <div class="clearfix"></div>
    </div>
</div>
<%--   </contenttemplate>
</asp:UpdatePanel>--%>



<script type="text/javascript">
    function DisableButtons() {
        var inputs = document.getElementsByTagName("INPUT");
        for (var i in inputs) {
            if (inputs[i].type == "button" || inputs[i].type == "submit") {
                inputs[i].disabled = true;
            }
        }
    }
    window.onbeforeunload = DisableButtons;
</script>



<script type="text/javascript">

    HideShowAttachmentLinkForUser();

    function HideShowAttachmentLinkForUser() {

        $(".AfterAttachmentSestion").each(function (index) {

            var attachmentURL = $(this).find('.AfterAttachment').attr('href');

            if (attachmentURL != "") {
                if ($(".attachmentListRightSections a[href$='" + attachmentURL + "']").length <= 0) {
                    $(this).remove();
                }
            }
        });

        $(".AfterAttachmentSestion .attachmentAfterRemoveButton").each(function (index) {

            var attachmentURL = $(this).attr('UploadedByUserId');
            var CurrentUserId = $("#hiddenUserId").val();
            var hiddenIsAdminUser = $("#hiddenIsAdminUser").val();

            if (attachmentURL != CurrentUserId && hiddenIsAdminUser == "False") {

                $(this).remove();
            }
        });


        $(".SeeAttachedMediaLinkContent").each(function (index) {

            var contId = $(this).attr('cid');

            var TotalAttachmentCount = $(".ReplyContentSection_" + contId + " .AfterAttachmentSestion").length;

            if (TotalAttachmentCount == 0) {

                $(this).remove();
            }
        });


        $(".AttchmentListSection").each(function (index) {
            $(this).html("");
        });



    }




</script>


