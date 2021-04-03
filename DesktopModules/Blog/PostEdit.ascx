<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="PostEdit.ascx.vb" Inherits="DotNetNuke.Modules.Blog.PostEdit" %>
<%@ Register TagPrefix="dnnweb" Assembly="DotNetNuke.Web.Deprecated" Namespace="DotNetNuke.Web.UI.WebControls" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="blog" Namespace="DotNetNuke.Modules.Blog.Controls" Assembly="DotNetNuke.Modules.Blog" %>
<script src="https://static.filestackapi.com/v3/filestack.js"></script>

<script type="text/javascript" src="/DesktopModules/ActiveForums/scripts/tagmanager.js"></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/Module/Common.js"></script>
<style type="text/css">
.tm-tag {
    color: #ffffff;
    background: #6992c0;
    border: 1px solid #c2d2e5;
    font-weight: 300;
    padding: 2px 4px;
    font-size: 14px;
    display: inline-block;
    margin: 1%;
}

.tm-tag a {color: #ffffff;margin-left: 20px;font-size: 16px;}

.text-editor-fileupload-section {
    /*min-height:430px;*/
}

.dnnAction-button-section {float: left;padding-top: 0px;}
.cke_maximized {left: 75px !important;width: 1290px !important;}

    /* Renato: I am undoing the following changes. These are breaking the rich text editor in the Blog editor*/
    /*
     #cke_104 .cke_toolbar_separator {
        display: none;
    }

    #cke_106, #cke_107, #cke_108, #cke_109, #cke_110, #cke_176 {
        display: none;
    }
    */
</style>

<script type="text/javascript">
    function Validate() {
        CA_selectedTags = '';
        // get the list of selected tags
        $('[id*="dlTags"] input:checked').each(function (i, o) {
            CA_selectedTags += $('label[for="' + $(o)["0"].id + '"]').html() + ',';
        });

        if (CA_selectedTags == '') {
            alert('Please assign atleast one Tag');
            return false;
        }
        $('[id*="hdTagsSelected"]').val(CA_selectedTags);
        return true;
    }

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

    $(document).ready(function () {
        GetFilesList(getParameterDetails("ContentItemId"));
        $(".CA_Single").click(function () {
            $(this).closest("table").find(".CA_Single").removeAttr('checked');
            $(this).attr('checked', 'checked');
        });
    });

    function AddRelatedTags(event, ui, clientid) {
        AddRelatedTagChild(ui.tagLabel);
    }

    function AddRelatedTagChild(tag) {
        var _URL = "/DesktopModules/ClearAction_ComponentManager/rh.asmx/GetRelatedTagList";
        var _data = "{'tag':'" + tag + "'}";
        CA_MakeRequest(_URL, _data, CA_OnAddRTag, CA_OnAddRTag);
    }

    function CA_OnAddRTag(d) {
        debugger;
        var tagID = $(".tagsmanager").tagsManager({ tagsContainer: '#ForumTags', tagCloseIcon: 'X' });
        var sep = "";
        var Ids = "";
        for (var i = 0; i < d.length; i++) {
            $(tagID).tagsManager('pushTag', d[i].Name);
            Ids += sep + d[i].NTerm;
            sep = ";";
        }
        $("#rtagIDs").val(Ids);
    }

    function getParameterDetails(name) {
        var url = window.location.pathname;
        if (url.lastIndexOf(name) == -1) return -1;
        //var iStart = url.lastIndexOf(name) + name.length + 1;
        //var iEnd = url.lastIndexOf("/ContentItemId");

        var contentid = -1;
        var data = url.split("/");
        for (index = 0; index < data.length; ++index) {
            contentid = data[index];
        }
        return contentid;
        // return url.substring(iStart, iEnd);
    }
    var webMethodUrl = '/DesktopModules/Blog/API/ClearAction/';
    var CA_Blog_RecentFileUploads = '';
    var oKey = '<%=GetFileStack%>';   //'AUQ4R8zKvSKCs9gudWJlxz';
    function addFile(ele) {
        var fsClient = filestack.init(oKey);
        fsClient.pick({}).then(function (response) {
            response.filesUploaded.forEach(function (file) {
                var ofileresponse = file;
                $('#btnAddFile').hide();
                $('#cmdImageRemove').show();
                $('#<%=imgPostImage.ClientID%>').attr('src', ofileresponse.url);
                $('#<%=hdnImageUrl.ClientID%>').val(ofileresponse.url);
                $('#<%=imgPostImage.ClientID%>').show();
                $('#<%=hdnImage.ClientID%>').val(ofileresponse.filename);
                var contId = $('#<%=hdnContentItemID.ClientID%>').val();

                var uploadServiceUrl = webMethodUrl + 'SaveFileStackData';
                $.ajax({type: 'GET',dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    url: uploadServiceUrl,
                    data: { ContentPostID: contId, FileName: ofileresponse.filename, contentType: ofileresponse.mimetype, size: ofileresponse.size, statckurl: ofileresponse.url },
                    success: function (d) {}
                }).done(function (response) {
                    GetFilesList(contId);
                });
            });
        });
    }

    function GetParameterValues(param) {
        var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < url.length; i++) {
            var urlparam = url[i].split('=');
            if (urlparam[0] == param) {
                return urlparam[1];
            }
        }
    }
    function copyToClipboard(element) {
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val(element).select();
        document.execCommand("copy");
        $temp.remove();
        SuccessWindow('Success', 'Copied to Clipboard');
        return false;
    }

    function DeleteFileStackReference(element, contId) {
        $.confirm({title: 'Delete Confirm!',
            content: 'Are you sure want to delete file reference?',
            buttons: {
                confirm: function () {
                    var uploadServiceUrl = webMethodUrl + 'DeleteFileStack';
                    $.ajax({type: 'GET',dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        url: uploadServiceUrl,
                        data: { FileName: element },
                        success: function (d) {}
                    }).done(function (response) {
                        GetFilesList(contId);
                    });
                },
                cancel: function () {}
            }
        });
        return false;
    }
    function GetFilesList(contId) {
        $.ajax({
            type: "GET",dataType: "json",contentType: "application/json; charset=utf-8",
            url: webMethodUrl + "GetFileStackData",
            data: { ContentPostID: contId },
        }).done(function (response) {
            var oData = JSON.parse(response);
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
                strTemp += '<tr><td style="width:250px;text-align:left;padding:10px">{1}.<button class="btn btn-delete btn-sm" onclick="return DeleteFileStackReference({4},{5})"><img src="/images/delete.gif"></img></button> <button class="btn btn-primary btn-sm" onclick="return copyToClipboard({4})">Copy Link</button> &nbsp;&nbsp; <a target="_new"  href="{0}" title="{3}">{2}</a></td></tr/>'.format(oData[i].filestackurl, icount + 1, strFileName, oData[i].FileName, "'" + oData[i].filestackurl + "'", contId);
                icount++;
            }
            $('#divFileCollection').html("<table cellpadding='15' cellspacing='15' style='width:100%'>" + strTemp + "</table>");
            //    $('#filecount_' + contId).html(icount);
        });
    }

    function BindFileCollection() {
        var fsClient = filestack.init(oKey);
        fsClient.pick({}).then(function (response) {
            response.filesUploaded.forEach(function (file) {
                var ofileresponse = file;
                var contId = $('#<%=hdnContentItemID.ClientID%>').val();
                var uploadServiceUrl = webMethodUrl + 'SaveFileStackData';
                $.ajax({
                    type: 'GET',dataType: "json",contentType: "application/json; charset=utf-8",
                    url: uploadServiceUrl,
                    data: { ContentPostID: contId, FileName: ofileresponse.filename, contentType: ofileresponse.mimetype, size: ofileresponse.size, statckurl: ofileresponse.url },
                    success: function (d) {
                        var strFiles = $('[id*="hdAttachments"]').val();
                        strFiles += d;
                        $('[id*="hdAttachments"]').val(strFiles);
                        SuccessWindow('Success', 'File has been uploaded successfully');
                    }
                }).done(function (response) {
                    GetFilesList(contId);
                });
            });
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

    function RemoveFile(ele) {
        $('#btnAddFile').show();
        $('#cmdImageRemove').hide();
        $('#<%=imgPostImage.ClientID%>').hide();
        $('#<%=hdnImage.ClientID%>').val('');
        $('#<%=imgPostImage.ClientID%>').hide();
        //If Edit will stay old
        if ($('#<%=hdnImageEdit.ClientID%>').val() != null) {
            $('#<%=imgPostImage.ClientID%>').attr('src', $('#<%=hdnImageEdit.ClientID%>').val());
        }
    }
    function ApplySelectedTags(strTags) {
        temp = strTags.split("_");
        for (a in temp) {
            $('[id*="dlTags"] input[type=checkbox]').each(function (i, o) {
                if ($('label[for="' + $(o)["0"].id + '"]').html() == temp[a]) {
                    $("#" + $(o)["0"].id).attr("checked", 'true');
                }
            });
        }
    }
</script>
<asp:HiddenField ID="hdTagsSelected" runat="server" />
<asp:HiddenField ID="hdnContentItemID" runat="server" />
<div class="dnnForm dnnBlogEditPost dnnClear" id="dnnBlogEditPost">
    <ul class="dnnAdminTabNav">
        <li><a href="#dnnBlogEditContent"><%= LocalizeString("Content")%></a></li>
        <li><a href="#dnnBlogEditPublishing"><%= LocalizeString("Publishing")%></a></li>
    </ul>
    <div class="clear"></div>
    <div class="dnnClear" id="dnnBlogEditContent">
        <fieldset>
            <div class="form-group textarea_group" style="padding-top: 30px !important">
                <b>Title:</b><br />
                <blog:ShortTextEdit id="txtTitle" runat="server" Required="True" CssClass="blog_rte_full" RequiredResourceKey="Title.Required" CssPrefix="blog_rte_" />
            </div>
            <div class="form-group textarea_group">
                <b>Description:</b><br />

                <asp:TextBox ID="txtShortDescription" runat="server" Height="300" Width="100%" CssClass="blog_rte" TextMode="MultiLine"></asp:TextBox>
            </div>
            <%--<div class="form-group textarea_group">
                <div style="width: 70%; float: left">
                    <b>Body:</b><br />
                    <blog:LongTextEdit id="txtDescription" runat="server" Width="100%" TextBoxWidth="100%" TextBoxHeight="300" CssPrefix="blog_rte_" Required="True" />
                </div>
                <div style="width: 30%; float: right;padding-top:10px">
                   <strong>Attachment(s):</strong>
                    <div id="divFileCollection" style="padding:10px;">
                    </div>
                    <br />
                  <center>  <button type="button" id="btnAddFile1" class="dnnPrimaryAction" onclick="BindFileCollection(this);">Upload</button></center>
                    <asp:HiddenField ID="hdAttachments" runat="server" />
                </div>
            </div>--%>
            <div class="form-group textarea_group" style="clear: both;">
                <b>
                    <asp:Label ID="lblIsClassicArticle" runat="server" AssociatedControlID="chkIsClassicArticle"> </asp:Label>Is Classic Article ?:</b>
                <asp:CheckBox ID="chkIsClassicArticle" runat="server" />
            </div>
        </fieldset>
        <fieldset>
            <%--   <dnn:Label ID="lblSummary" runat="server" controlname="txtDescription" suffix=":" CssClass="dnnLeft" />--%>

            <div class="dnnFormItem" id="rowLocale" runat="server">
                <dnn:Label ID="lblLocale" runat="server" controlname="ddLocale" suffix=":" />
                <asp:DropDownList ID="ddLocale" runat="server" DataTextField="NativeName" />
            </div>
            <div class="form-group textarea_group">
                <b>Images:</b><br />
                <asp:Image runat="server" ID="imgPostImage" />
                <asp:HiddenField ID="hdnImageUrl" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnImage" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnImageEdit" runat="server" />
                <button type="button" id="btnAddFile" class="btn btn-primary" onclick="addFile(this);">Add File</button>
                <button id="cmdImageRemove" class="btn btn-danger" onclick="RemoveFile(this,1);">Remove</button>
            </div>
            <div class="dnnClear"></div>
            <div class="form-group textarea_group">
                <h6>Choose one or Many category to best represent this Blog</h6>
                <asp:CheckBoxList runat="server" ID="rdbCategory">
                </asp:CheckBoxList>
                <br />
            </div>
            <div class="dnnclear"></div>
            <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select at least one Category."
                ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" />
            <asp:Panel ID="pnlCategories" runat="server" class="dnnFormItem">
            </asp:Panel>
            <div class="dnnClear"></div>
            <div class="form-group textarea_group">
                <b>Tags:</b><br />
                <blog:TagEdit ID="ctlTags" runat="server" width="500px" visible="false" AllowSpaces="True" />
                <div class="form-group tag_group">
                    <asp:DataList ID="dlTags" runat="server" OnItemDataBound="dlTags_ItemDataBound">
                        <ItemTemplate>
                            <div style="width: auto; margin-bottom: 15px; float: left;">
                                <asp:Label ID="lblName" runat="server" Font-Bold="True"></asp:Label>
                                <asp:HiddenField ID="hdTagID" runat="server" />
                                <asp:DataList ID="dlTagChildren" runat="server" OnItemDataBound="dlTagChildren_ItemDataBound">
                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:CheckBox ID="lblChildName" runat="server" Font-Bold="False"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
            <span style="display: none;">
                <input type="text" id="txtTags" class="tagsmanager" name="forum_tags" style="">
                <input type="hidden" id="rtagIDs" name="rtagIDs" />
            </span>
            <div id="ForumTags" class="tags_container"></div>
              <div class="form-group textarea_group text-editor-fileupload-section">
        <div style="width: 65%; float: left">
            <b>Body:</b><br />
            <%--<blog:LongTextEdit id="txtDescription" runat="server" Width="100%" TextBoxWidth="100%" TextBoxHeight="300" CssPrefix="blog_rte_" Required="True" />--%>
            <blog:LongTextEdit id="txtDescription" runat="server" Width="100%" TextBoxWidth="100%" TextBoxHeight="300" CssPrefix="blog_rte_" Required="True" />
        </div>
        <div style="width: 35%; float: right; padding: 10px;  border-radius: 20px">
            <strong>Attachments</strong>
            <div id="divFileCollection" style="padding: 10px;">
            </div>
            <br />
            <center>  <button type="button" id="btnAddFile1" class="btn btn-success btn-block" onclick="BindFileCollection(this);">Upload New File</button></center>
            <asp:HiddenField ID="hdAttachments" runat="server" />
        </div>
    </div>
        </fieldset>
    </div>

    <div class="dnnClear" id="dnnBlogEditPublishing">
        <fieldset>
            <asp:Panel ID="pnlPublished" runat="server" class="dnnFormItem">
                <dnn:Label ID="lblPublished" runat="server" controlname="chkPublished" suffix=":" />
                <asp:CheckBox ID="chkPublished" runat="server" Checked="true" />
            </asp:Panel>
            <asp:Panel ID="pnlComments" runat="server" Visible="false" class="dnnFormItem">
                <dnn:Label ID="lblAllowComments" runat="server" controlname="chkAllowComments" suffix=":" />
                <asp:CheckBox ID="chkAllowComments" runat="server" Checked="true" />
            </asp:Panel>
            <div class="dnnFormItem" style="display: none">
                <dnn:Label ID="lblDisplayCopyright" runat="server" controlname="chkDisplayCopyright" suffix=":" />
                <asp:CheckBox ID="chkDisplayCopyright" runat="server" AutoPostBack="True" />
            </div>
            <asp:Panel ID="pnlCopyright" runat="server" Visible="False" class="dnnFormItem">
                <dnn:Label ID="lblCopyright" runat="server" controlname="txtCopyright" suffix=":" />
                <asp:TextBox ID="txtCopyright" runat="server" />
            </asp:Panel>
            <div class="dnnFormItem" style="display: none">
                <dnn:Label ID="lblPublishDate" runat="server" ControlName="dpPostDate" Suffix=":" />
                <div class="dnnLeft">
                    <asp:CheckBox runat="server" ID="chkPublishNow" ResourceKey="Now" Checked="true" />
                    <dnnweb:DnnDatePicker ID="dpPostDate" runat="server" CssClass="dateFix" />
                    <dnnweb:DnnTimePicker ID="tpPostTime" runat="server" TimeView-Columns="4" ShowPopupOnFocus="true" CssClass="dateFix" />
                </div>
            </div>
            <div class="dnnClear" style="padding: 10px 0px;">
                <em><%= LocalizeString("PublishTimeZoneNote")%>&nbsp;<asp:Literal runat="server" ID="litTimezone" /></em>
            </div>
        </fieldset>
    </div>
</div>
<ul class="dnnActions dnnAction-button-section">
    <li>
        <asp:Button ID="cmdSave" runat="server" CssClass="btn btn-info" resourcekey="cmdSave" ValidationGroup="valBlogPostEdit"  OnClientClick="return Validate()"  /></li>
    <li>
        <asp:HyperLink ID="hlCancel" ResourceKey="cmdCancel" runat="server" CssClass="btn btn-warning" /></li>
    <li>
        <asp:Button ID="cmdDelete" ResourceKey="cmdDelete" runat="server" CssClass="btn btn-danger" CausesValidation="False" Visible="False" /></li>
</ul>
<div class="dnnFormItem">
    <asp:ValidationSummary ID="valSummary" CssClass="dnnFormMessage dnnFormValidationSummary" EnableClientScript="true" runat="server" DisplayMode="BulletList" />
</div>
</div>
<asp:CustomValidator ID="valPost" EnableClientScript="False" runat="server" ResourceKey="valPost.ErrorMessage" Display="None" />
<asp:CustomValidator ID="valUpload" EnableClientScript="False" runat="server" Display="None" />
<script language="javascript" type="text/javascript">

    function ValidateCheckBoxList(sender, args) {
        args.IsValid = readListControl()
    }

    $(document).ready(function () {
        // Renato: The following function call could be breaking the BLog rich text editor
        /*
        setInterval(function () {
            SetCKEditor();
        }, 1000);
        */
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

    function readListControl() {
        var IsChecked = false;
        var chks = $("#<%= rdbCategory.ClientID %> input:checkbox");
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

    (function ($, Sys) {
        $(document).ready(function () {
            $('#dnnBlogEditPost').dnnTabs({ selected: 0 });

            $('.dnnPostDelete').dnnConfirm({
                text: '<%= LocalizeJSString("DeleteItem") %>',
                yesText: '<%= LocalizeJSString("Yes.Text", Localization.SharedResourceFile) %>',
                noText: '<%= LocalizeJSString("No.Text", Localization.SharedResourceFile) %>',
                title: '<%= LocalizeJSString("Confirm.Text", Localization.SharedResourceFile) %>'
            });

            $('#<%= chkPublishNow.ClientID %>').click(function () {
                setDateSelector();
            });

            setDateSelector();
        });

        function setDateSelector() {
            if ($('#<%= chkPublishNow.ClientID %>').is(':checked')) {
                $('#<%= dpPostDate.ClientID %>_wrapper').hide();
                $('#<%= tpPostTime.ClientID %>_wrapper').hide();
            } else {
                $('#<%= dpPostDate.ClientID %>_wrapper').show();
                $('#<%= tpPostTime.ClientID %>_wrapper').show();
            }
        }

    }(jQuery, window.Sys));

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