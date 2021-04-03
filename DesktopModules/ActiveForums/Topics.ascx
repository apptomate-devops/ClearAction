<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="TopicsCtrl.ascx.cs" Inherits="DotNetNuke.Modules.ActiveForums.TopicsCtrl" %>
<%@ Register TagPrefix="am" Namespace="DotNetNuke.Modules.ActiveForums.Controls" Assembly="DotNetNuke.Modules.ActiveForums" %>
<%@ Register Src="~/controls/texteditor.ascx" TagPrefix="dnn" TagName="texteditor" %>
<%@ Register TagPrefix="am" TagName="topheader" Src="~/DesktopModules/ActiveForums/controls/af_topheader.ascx" %>
<script src="https://static.filestackapi.com/v3/filestack.js"></script>

<link type="text/css" href='<%=ModulePath %>module.css' />
<link rel="Stylesheet" href="//ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/themes/redmond/jquery-ui.css" />
<script type="text/javascript" src="<%=ModulePath%>scripts/tagmanager.js"></script>


<script>
    function SetTags(strTags) {
        var tagID = $(".tagsmanager").tagsManager({ tagsContainer: '#ForumTags', tagCloseIcon: 'X' });
        temp = strTags.split("_");
        for (a in temp) {
            $(tagID).tagsManager('pushTag', temp[a]);
        }

    }
    function ValidateCheckBoxList(sender, args) {
        args.IsValid = readListControl();
    }


    function readListControl() {


        var IsChecked = false;

        var chks = $("#<%= rptCategory.ClientID %> input:checkbox");

        var hasChecked = false;
        for (var i = 0; i < chks.length; i++) {
            if (chks[i].checked) {
                hasChecked = true;
                break;
            }
        }
        if (hasChecked == false) {

            hasChecked = false;

        }



        return hasChecked;
    }
    var webMethodUrl = '/DesktopModules/activeforums/api/ForumService/';


    $(function () {


        $('#txtTags').autocomplete({

            source: function (request, response) {

                $.ajax({
                    type: "GET",
                    url: webMethodUrl + "GetAutoCompleteText",

                    data: { SuggestedText: request.term },
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        var oData = JSON.parse(data);

                        response($.map(oData, function (item) {
                            return {
                                value: item
                            }
                        }))
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error");
                    }
                });
            },
            minLength: 2
        });


        $(".form-group .radio").click(function () {

            $('input:not(:checked)').parent().parent().removeClass("active");
            $('input:checked').parent().parent().addClass("active");
        });

        if ($.isFunction($.fn.tagsManager)) {
            var tagID = $(".tagsmanager").tagsManager({ tagsContainer: '#ForumTags', tagCloseIcon: 'X' });

            $('#AddForumTag').click(function () {

                var pTag = $(tagID).val();

                if ($(tagID).val().length <= 2)
                    return "";
                $(tagID).tagsManager('pushTag', $(tagID).val());

                AddRelatedTags(pTag);

            });
        }
    });
    //New Forum Tag Manager

    function AddRelatedTags(tag) {
        var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/GetRelatedTagList";
        var _data = "{'tag':'" + tag + "'}";
        CA_MakeRequest(_URL, _data, CA_OnAddRTag);
    }

    function CA_OnAddRTag(d) {
        var tagID = $(".tagsmanager").tagsManager({ tagsContainer: '#ForumTags', tagCloseIcon: 'X' });
        for (var i = 0; i < d.length; i++) {
            $(tagID).tagsManager('pushTag', d[i].Name);
        }
    }

</script>
<style>
    .icon {
        position: relative;
        display: block;
        margin-top: 10px;
        margin-bottom: 10px;
        background: #f0f4f9;
        border: 1px solid #d1ddec;
        margin: 2px;
        line-height: 15px;
        padding: 10px 0 10px 5px;
    }

    .forum_new_cnt .radio {
        border: 1px solid #d1ddec;
        border-bottom: none;
        padding: 14px 0 14px 10px;
        margin-bottom: 0px;
        background-color: #f0f4f9;
    }

    .forum_new_cnt .radio_group .active {
        background-color: #6992c0;
        border: 0px solid #6992c0;
    }

    .forum_new_cnt .radio label {
        font-weight: 300;
        color: #303030;
    }

    .forum_new_cnt input[type='radio'], input[type='checkbox'] {
        -webkit-appearance: none;
        width: 20px;
        height: 20px;
        border: 1px solid #c5d4e7;
        border-radius: 50%;
        outline: none;
        box-shadow: 0 0 5px 0px #c5d4e7 inset;
        margin-top: -1px;
    }

    .forum_new_cnt .tm-tag {
        color: #ffffff;
        background: #6992c0;
        border: 1px solid #c2d2e5;
        font-weight: 300;
        padding: 2% 4%;
        font-size: 14px;
        display: inline-block;
        margin: 1%;
    }

        .forum_new_cnt .tm-tag a {
            color: #ffffff;
            margin-left: 20px;
            font-size: 16px;
        }

    .forum_new_cnt span {
        font-size: 14px;
    }

    .forum_new_cnt input[type="text"] {
        border: 1px solid #d3dfed;
        width: 85%;
        height: 40px;
        padding: 5%;
        color: #666666;
        font-weight: 300;
    }

    .forum_new_cnt .form-group h6 {
        font-size: 14px !important;
        color: #303030;
        font-weight: normal;
        margin: 5% 0 1%;
    }

    /*Begin New Added ruby Ncode 12-January-2018*/

    .cnt_wraper_lt.col-sm-8 {
        display: none;
    }

    .cnt_wrapper_rt.col-sm-4 {
        display: none;
    }

    footer {
        display: none;
    }

    .cke_maximized {
        left: 75px !important;
        width: 1290px !important;
    }

    .buttonDisplayNone {
        display: none;
    }


    /* Renato: Commenting out the following styles. These break the Blog rich text editor*/
    /*
    #cke_104 .cke_toolbar_separator {
    }

    #cke_106, #cke_107, #cke_108, #cke_109, #cke_110, #cke_176 {
        display: none;
    }
    */
    /*End New Added ruby Ncode 12-January-2018*/

    .form-group {
    margin-bottom: -31px !important;
}
</style>


<div class="solve_wrapper">
    <div class="cnt_wraper_lt forum_new_wraper col-sm-12">
        <am:topheader ID="topheader" runat="server" SubHeader="false"></am:topheader>
        <div class="clearfix"></div>
        <div class="forum_new_cnt col-sm-8">
            <div class="forum_newarea col-sm-8">
                <%--<h1>Create a New Topic</h1>--%>
                <asp:Literal ID="ltrlHeader" runat="server"></asp:Literal>
                <div class="forum-bio clearfix">
                    <div class="forum-bio-left">
                        <asp:Image runat="server" ID="imgAuthor" />
                    </div>
                    <div class="forum-bio-right">
                        <strong>
                            <asp:Label runat="server" ID="lblUserName" /></strong>
                        <br />
                        <br />

                    </div>
                </div>

                <div class="form-group textarea_group">

                    <label for="exampleInputEmail1">Your question:</label>


                    <asp:TextBox ID="txtTitle" runat="server" TextMode="MultiLine" class="form-control" placeholder="Ex: What is a good way to adjust an agency's role?" Style="overflow: hidden; word-wrap: break-word; resize: horizontal;" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitle" ErrorMessage=" Title Required" CssClass="titleVaidationSection" ValidationGroup="T" ForeColor="Red" />
                </div>

                <%-- <div class="row">
                    <div class="col-md-12">
                        <div class="form-group textarea_group">
                            <asp:Label ID="lblTopic" runat="server">Topic text</asp:Label>

                            <dnn:texteditor runat="server" ID="txteditor" DefaultMode="Html" Width="99%" />

                        </div>
                    </div>
                </div>--%>


                <div class="row after_editor" id="div1" runat="server">
                    <div class="col-md-6">
                        <div class="form-group radio_group radio_group1 clearfix">
                            <h6>Choose the category or caegories that best represent this topic (you may choose more than one)</h6>
                            <asp:Repeater ID="rptCategory" runat="server" OnItemDataBound="rptCategory_OnItemDataBound">
                                <ItemTemplate>
                                    <div class='radio icon <%#GetRadioSelectedCSS(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"CategoryId")),true) %>'>
                                        <label>
                                            <asp:HiddenField ID="hdnCategoryID" runat="server" Value='<%#Eval("CategoryId") %>' />
                                            <input type="checkbox" value='<%#Eval("CategoryId") %>' runat="server" id="radioControl" name="GoogleCalenderList" checked='<%#GetQuestionResponseRadio(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"CategoryId")),true) %>' />
                                            <span><%#Eval("CategoryName") %></span>
                                        </label>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <asp:CustomValidator runat="server" ID="CustomValidator1" ErrorMessage="Please select at least one Category."
                                ForeColor="Red" EnableClientScript="true" ClientValidationFunction="ValidateCheckBoxList"></asp:CustomValidator>

                            <br />
                        </div>
                    </div>
                </div>

                <div class="row"  id="div2" runat="server">
                     

                    <div class="col-sm-6">

                        <div class="form-group textarea_group tag_group">
                            <label>
                                Tags – tap the (+) to add a tag</label><br />
                            <input type="text" id="txtTags" class="tagsmanager" name="forum_tags"><a id="AddForumTag"><img src="<%=ModulePath %>images/forum-add-new.png" alt="add tag icon"></a>
                        </div>
                        <div id="ForumTags" class="tags_container">
                        </div>


                    </div>
                </div>




                <div class="row after_editor"  id="div3" runat="server">
                    <div class="col-md-8">
                        <div class="form-group radio_group radio_group1 clearfix">
                            <h6>Who will most interested by this topic? You may choose more than one</h6>
                            <asp:Repeater ID="rptSubCategory" runat="server">
                                <ItemTemplate>
                                    <div class='radio icon <%#GetRadioSelectedCSS(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"CategoryId")),false) %>'>
                                        <label>
                                            <asp:HiddenField ID="hdnCategoryID" runat="server" Value='<%#Eval("CategoryId") %>' />
                                            <input type="checkbox" value='<%#Eval("CategoryId") %>' runat="server" id="radioControl" name="GoogleCalenderList" checked='<%#GetQuestionResponseRadio(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"CategoryId")),false) %>' />
                                            <span><%#Eval("CategoryName") %></span>
                                        </label>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <asp:CustomValidator runat="server" ID="CustomValidator2" ErrorMessage="Please select at least one Category."
                                ForeColor="Red" EnableClientScript="true" ClientValidationFunction="ValidateCheckBoxList"></asp:CustomValidator>

                            <br />
                        </div>
                    </div>
                </div>


                <div class="row">
                    <div style="width: 65%; float: left">
                        <div class="col-md-12">
                            <div class="form-group textarea_group">
                                <asp:Label ID="lblTopic" runat="server">Topic text</asp:Label>

                                <%--<asp:TextBox runat="server" ID="txteditor" DefaultMode="Html" Width="99%" />--%>
                                <asp:Button runat="server" ID="Set" Text="" CssClass="buttonDisplayNone" />
                                <asp:TextBox runat="server" ID="txteditor"  TextMode="MultiLine" Rows="10" Columns="50" Width="99%" />

                            </div>
                        </div>

                    </div>
                    <div style="width: 35%; float: right; padding: 10px; border-radius: 20px">
                        <strong>Attachments</strong>
                        <div id="divFileCollection" style="padding: 10px;">
                        </div>
                        <br />
                        <center>  <button type="button" id="btnAddFile1" class="btn btn-success btn-block" onclick="BindFileCollection(this);">Upload New File</button></center>
                        <asp:HiddenField ID="hdAttachments" runat="server" />
                    </div>
                </div>




                <div>
                    <h1>That’s it, ready to post this topic?</h1>
                   Best-practices to increase replies:   <ul style="list-style-type:circle">
                      
                        <li >
                            Make your subject line a <strong>concise question</strong>.

                        </li>
                        <li>Break complex topics into separate simple questions.<br />
                            <small>(make it easy and attractive for members on-the-go to participate)</small>
                        </li>
                        <li>Give a bit of background about why you're asking the question.</li>
                        <li>Provide an example as an attachment if applicable</li>
                    </ul>
                    <asp:Button ID="btnPOST" runat="server" OnClientClick="return CheckValidation()" CssClass="btn btn-primary" Text="POST" OnClick="btnPOST_Click" ValidationGroup="T" />
                    <asp:Button ID="btnCANCEL" runat="server" CausesValidation="false" CssClass="btn btn-secondary" Text="CANCEL" OnClick="btnCANCEL_Click" />
                    <asp:Button ID="btnDelete" runat="server" CausesValidation="false" CssClass="btn btn-secondary" Text="DELETE" OnClick="btnDelete_Click" />
                </div>
            </div>

        </div>
        <div class="clearfix"></div>
    </div>

</div>
<input type="hidden" id="hiddenSelectedAttachment" value="" />

<%--<footer>
    <div class="footer_dtls">
        <span>A Service of</span> <a href="#">
            <figure>
                <img src="images/footer_logo.png" width="129" height="26" alt="footer_logo" class="img-responsive">
            </figure>
        </a>
        <div class="clearfix"></div>
    </div>
    <div class="clearfix"></div>
</footer>--%>


<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/Module/Common.js"></script>

<script type="text/javascript">


    function CheckValidation() {
        var titleText = $('#<%=txtTitle.ClientID%>').val();
        var flag = false;

        if ($.trim(titleText) != '') {

            $('.titleVaidationSection').attr('style', 'color:red;visibility:hidden;');
            flag = true;
        }
        else {

            $('.titleVaidationSection').attr('style', 'color:red;visibility:visible;');
            $('#<%=txtTitle.ClientID%>').focus();
        }



        return flag;

    }


    $(document).ready(function () {

        // Renato: The following function call could be breaking the BLog rich text editor

        //setInterval(function () {
        //    SetCKEditor();
        //}, 1000);

    });

    var count = 0;
    function SetCKEditor() {

        var IdTextArea = $(".editor").attr("Id");

        if (IdTextArea != "") {
            if (count == 0) {
                if (CKEDITOR.instances["" + IdTextArea + ""]) {
                    /* destroying instance */
                    CKEDITOR.instances["" + IdTextArea + ""].destroy();
                    count++;
                } else {
                    /* re-creating instance */
                    CKEDITOR.replace('' + IdTextArea + '');
                }
            }
            else {

                if ($('#cke_' + IdTextArea + '').length > 0) {
                    //$('#' + IdTextArea + '').ckeditor();
                }
                else {
                    if (CKEDITOR.instances["" + IdTextArea + ""]) {
                        /* destroying instance */
                        CKEDITOR.instances["" + IdTextArea + ""].destroy();
                        count++;

                    } else {
                        /* re-creating instance */
                        CKEDITOR.replace('' + IdTextArea + '');
                    }
                }
            }

        }
    }
</script>
<asp:HiddenField ID="hdnContentItemID" runat="server" />
<!-- ANUJ : Filestack on API-->
<script>
    $(document).ready(function () {
        var contId = '<%=hdnContentItemID.Value%>';
        if (contId == -1) return;
        debugger;
        GetFilesList(contId);
    });

    function AlertWindow(head, content) {
        $.alert({
            title: head,
            content: content
        });
    }

    function SuccessWindow(head, content) {
        $.alert({
            title: head,
            content: content
        });
    }

    var oKey = '<%=GetAPIKey%>';
    function copyToClipboard(element) {

        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val(element).select();
        document.execCommand("copy");
        $temp.remove();

        $("#divFileCollection .SelectedAttachment").remove();

        $('<span class="SelectedAttachment" style="padding-left: 5px;"><img src="/DesktopModules/ActiveForums/images/admin_check.png"></img></span>').insertAfter('#divFileCollection a[href="' + element + '"]');

        $("#hiddenSelectedAttachment").val(element);

        //SuccessWindow('Success', 'Copied to Clipboard');
        return false;
    }

    function GetFilesList(contId) {

        $.ajax({
            type: "GET",
            url: webMethodUrl + "GetAttachments",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { contentId: contId },
        }).done(function (response) {
            var oData = JSON.parse(response);
            debugger;
            var strTemp = "";
            var icount = 0;
            //strTemp = "<tr><td><strong><center>File Name</center></strong></td><td><strong><center>File links</center></strong></td></tr/>";
            strTemp = "";//"<tr><td><strong><center>File Name</center></strong></td></tr/>";
            for (var i = 0; i < oData.length; i++) {
                var strFileName = oData[i].FileName;
                if (strFileName.length > 40) {
                    strFileName = strFileName.slice(0, 40);
                }
                //strTemp += '<tr><td style="width:150px;"><center>{2}</center></td><td><input type="text" value="{0}" id="input_{1}" style="width:100% !important;" class="form-control"></input></td></tr/>'.format(oData[i].filestackurl, icount, strFileName);
                strTemp += '<tr><td style="width:250px;text-align:left;padding:10px">{1}. <button class="btn btn-primary btn-sm" onclick="return copyToClipboard({4})">Copy Link</button> &nbsp;&nbsp; <a target="_new"  href="{0}" title="{3}">{2}</a></td></tr/>'.format(oData[i].filestackurl, icount + 1, strFileName, oData[i].FileName, "'" + oData[i].filestackurl + "'");
                icount++;
            }
            $('#divFileCollection').html("<table cellpadding='15' cellspacing='15' style='width:100%'>" + strTemp + "</table>");

            var hiddenSelectedAttachment = $("#hiddenSelectedAttachment").val();
            if (hiddenSelectedAttachment != "") {
                copyToClipboard(hiddenSelectedAttachment);
            }

            //    $('#filecount_' + contId).html(icount);
        });
    }


    function GetFilesListForCreateForumsPost(contId) {

        $.ajax({
            type: "GET",
            url: webMethodUrl + "GetAttachmentsForCreateForumsPost",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { contentId: contId },
        }).done(function (response) {
            var oData = JSON.parse(response);
            debugger;
            var strTemp = "";
            var icount = 0;
            //strTemp = "<tr><td><strong><center>File Name</center></strong></td><td><strong><center>File links</center></strong></td></tr/>";
            strTemp = "";//"<tr><td><strong><center>File Name</center></strong></td></tr/>";
            for (var i = 0; i < oData.length; i++) {
                var strFileName = oData[i].FileName;
                if (strFileName.length > 40) {
                    strFileName = strFileName.slice(0, 40);
                }
                //strTemp += '<tr><td style="width:150px;"><center>{2}</center></td><td><input type="text" value="{0}" id="input_{1}" style="width:100% !important;" class="form-control"></input></td></tr/>'.format(oData[i].filestackurl, icount, strFileName);
                strTemp += '<tr><td style="width:250px;text-align:left;padding:10px">{1}. <button class="btn btn-primary btn-sm" onclick="return copyToClipboard({4})">Copy Link</button> &nbsp;&nbsp; <a target="_new"  href="{0}" title="{3}">{2}</a></td></tr/>'.format(oData[i].filestackurl, icount + 1, strFileName, oData[i].FileName, "'" + oData[i].filestackurl + "'");
                icount++;
            }
            $('#divFileCollection').html("<table cellpadding='15' cellspacing='15' style='width:100%'>" + strTemp + "</table>");
            var hiddenSelectedAttachment = $("#hiddenSelectedAttachment").val();
            if (hiddenSelectedAttachment != "") {
                copyToClipboard(hiddenSelectedAttachment);
            }
            //    $('#filecount_' + contId).html(icount);
        });
    }


    String.prototype.format = function () {
        var str = this;
        for (var i = 0; i < arguments.length; i++) {
            var reg = new RegExp("\\{" + i + "\\}", "gm");
            str = str.replace(reg, arguments[i]);
        }
        return str;
    }

    function BindFileCollection() {
        var fsClient = filestack.init(oKey);
        fsClient.pick({}).then(function (response) {
            response.filesUploaded.forEach(function (file) {
                var ofileresponse = file;
                var contId = $('#<%=hdnContentItemID.ClientID%>').val();
                var uploadServiceUrl = webMethodUrl + 'GetSaveAttachments';
                debugger;
                $.ajax({
                    type: 'GET',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    url: uploadServiceUrl,
                    data: { contentId: contId, fileName: ofileresponse.filename, contentType: ofileresponse.mimetype, size: ofileresponse.size, fileurl: ofileresponse.url },
                    success: function (d) {
                        //var strFiles = $('[id*="hdAttachments"]').val();
                        //strFiles += d;
                        //$('[id*="hdAttachments"]').val(strFiles);

                        SuccessWindow('Success', 'File has been uploaded successfully');
                    }
                }).done(function (response) {

                    GetFilesListForCreateForumsPost(contId);
                });

            });

        });

    }
</script>
