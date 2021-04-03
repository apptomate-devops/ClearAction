<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditWebCast.ascx.cs" Inherits="ClearAction.Modules.WebCast.EditWebCast" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<%@ Register TagPrefix="dnnweb" Assembly="DotNetNuke.Web.Deprecated" Namespace="DotNetNuke.Web.UI.WebControls" %>

<%@ Register TagPrefix="blog" Namespace="DotNetNuke.Modules.Blog.Controls" Assembly="DotNetNuke.Modules.Blog" %>
<script src="https://static.filestackapi.com/v3/filestack.js"></script>

<link href="/Resources/Shared/stylesheets/dnndefault/7.0.0/default.css?cdv=84" type="text/css" rel="stylesheet"/>
<link href="/Portals/_default/admin.css?cdv=84" type="text/css" rel="stylesheet"/>
<link href="/DesktopModules/Blog/css/tagit.css?cdv=84" type="text/css" rel="stylesheet"/>
<link href="/DesktopModules/Blog/module.css?cdv=84" type="text/css" rel="stylesheet"/>
<link href="/Portals/_default/Skins/_default/WebControlSkin/Default/DatePicker.Default.css?cdv=84" type="text/css" rel="stylesheet"/>
<link href="/Portals/0/skins/demoskin/skin.css?cdv=84" type="text/css" rel="stylesheet"/>
<link href="/Portals/0/portal.css?cdv=84" type="text/css" rel="stylesheet"/>
<link href="/DesktopModules/admin/Dnn.PersonaBar/css/personaBarContainer.css?cdv=84" type="text/css" rel="stylesheet"/>
<link href="/Resources/Shared/Components/Tokeninput/Themes/token-input-facebook.css?cdv=84" type="text/css" rel="stylesheet"/>
<link href="/Providers/HtmlEditorProviders/DNNConnect.CKE/css/CKEditorToolBars.css?cdv=84" type="text/css" rel="stylesheet"/>
<link href="/Providers/HtmlEditorProviders/DNNConnect.CKE/css/CKEditorOverride.css?cdv=84" type="text/css" rel="stylesheet"/>


<script type="text/javascript" src="/DesktopModules/ActiveForums/scripts/tagmanager.js"></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/Module/Common.js"></script>
<script type="text/javascript" src="/DesktopModules/ActiveForums/scripts/tagmanager.js"></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/Module/Common.js"></script>
<script src="/Providers/HtmlEditorProviders/DNNConnect.CKE/js/ckeditor/4.5.3/ckeditor.js" type="text/javascript"></script>
<script src="/Providers/HtmlEditorProviders/DNNConnect.CKE/js/jquery.ckeditor.adapter.js" type="text/javascript"></script>
<%--<link rel="Stylesheet" href="//ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/themes/redmond/jquery-ui.css" />--%>

<script type="text/javascript">
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
        var webMethodUrl = '/DesktopModules/activeforums/api/ForumService/';

        $('#txtTags').autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "GET", dataType: "json",
                    url: webMethodUrl + "GetAutoCompleteText",
                    data: { SuggestedText: request.term },
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
    var oKey = '<%=GetAPIKey%>';   //'AUQ4R8zKvSKCs9gudWJlxz';
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
<div class="dnnForm dnnBlogEditPost dnnClear" id="dnnBlogEditPost" style="padding-left: 20px">
    <h2>
        <asp:Label ID="lblHeader" runat="server" Text="Create a New Digital Event"></asp:Label>
    </h2>
    <div class="dnnClear" id="dnnBlogEditContent">
        <fieldset>

            <div class="dnnFormItem" style="padding-top: 30px !important">

                <dnn:Label ID="lblEventTitle" runat="server" ControlName="lblEventTitle"></dnn:Label>
                <asp:TextBox ID="txtTitle" MaxLength="300" runat="server" CssPrefix="blog_rte" Width="50%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="valsave" ControlToValidate="txtTitle" ErrorMessage="*Required" CssClass="error"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group textarea_group dnnFormItem" >
                <dnn:Label ID="lblDescription" runat="server"></dnn:Label>
                 <blog:LongTextEdit id="txtDescription" runat="server" Width="100%" TextBoxWidth="100%" TextBoxHeight="300" CssPrefix="blog_rte_" Required="True" />
                <%--<blog:LongTextEdit id="txtShortDescription" runat="server" ShowRichTextBox="False" Width="520" TextBoxWidth="450" CssClass="blog_rte" />--%>
                <%--<asp:TextBox ID="txtShortDescription" runat="server" Height="200" Rows="50" Columns="100" CssClass="textbox" TextMode="MultiLine"></asp:TextBox>--%>
                <%--<asp:RequiredFieldValidator ID="reqShortDescription" runat="server" ValidationGroup="valsave" ControlToValidate="txtShortDescription" ErrorMessage="*Required" CssClass="error"></asp:RequiredFieldValidator>--%>
            </div>
            
   
            <div class="dnnFormItem">
                <dnn:Label ID="lblIsClassicArticle" runat="server"></dnn:Label>

                <asp:DropDownList ID="ddlComponent" runat="server">
                    <asp:ListItem Text="Community Calls" Value="4" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="Webcast Conversations" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Roundtable" Value="6"></asp:ListItem>
                    <asp:ListItem Text="Virtual Conference" Value="7"></asp:ListItem>
                    <asp:ListItem Text="Fireside Chat" Value="8"></asp:ListItem>
                    <asp:ListItem Text="Expert Presentation" Value="9"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="dnnFormItem">
                <asp:Panel ID="pnlAuthor" runat="server" class="dnnFormItem">
                    <div class="col-3">
                        <dnn:Label ID="lblPublished" runat="server" ControlName="chkPublished" Suffix=":" Text="Author" />
                        <input type="text" id="mdBasicSearch" class="SearchBox" data-bind="value: SearchTerm" placeholder="Search by Name" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="mdBasicSearch" ErrorMessage="*Required" CssClass="error" ValidationGroup="valsave"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-3">
                        <asp:Panel ID="pnlAuthorDetail" runat="server">
                            <div class="insights-row" style="padding-top: 40px; padding-left: 30%">
                                <div style="float: left">
                                    <img alt="images" id="profilepic" runat="server" height="78" width="78">
                                </div>

                                <div class="col-sm-4">
                                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                    <asp:HiddenField ID="hdnDisplayName" runat="server" />
                                    <asp:HiddenField ID="hdnCompanyName" runat="server" />
                                    <asp:HiddenField ID="hdnAuthorID" runat="server" />
                                    <asp:HiddenField ID="hdnProfileUrl" runat="server" />
                                    <asp:HiddenField ID="hdnRole" runat="server" />
                                    <br />
                                    <asp:Label ID="lblCompanyName" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblRole" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblAuthorID" runat="server" Style="display: none"></asp:Label><br />
                                    <a href="#" id="profilelink" target="_blank">Click to View Details</a>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </div>
            <div class="dnnFormItem">
                <dnn:Label ID="lblDate" runat="server" ControlName="lblDate" Suffix=":" />
                <dnnweb:DnnDatePicker ID="dpPostDate" runat="server" CssClass="dateFix" ShowPopupOnFocus="true" DateInput-InvalidStyle-CssClass="error" DateInput-ValidationGroup="valsave" />
                <asp:RequiredFieldValidator ID="reqField" runat="server" ControlToValidate="dpPostDate" ErrorMessage="*Required" CssClass="error" ValidationGroup="valsave"></asp:RequiredFieldValidator>
            </div>
            <div class="dnnformItem">
                <dnn:Label ID="lblTime" runat="server" ControlName="lblTime" Suffix=":" Text="Event Time" />
                <%--<dnnweb:DnnTimePicker ID="tpPostTime" runat="server" TimeView-Columns="4" ShowPopupOnFocus="true" DateInput-ShowButton="false" CssClass="dateFix" DateInput-ValidationGroup="valsave" DateInput-InvalidStyle-CssClass="error" />--%>
                <asp:TextBox ID="txtTime" runat="server" TextMode="Time"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTime" ErrorMessage="*Required" CssClass="error" ValidationGroup="valsave"></asp:RequiredFieldValidator>

            </div>
            <div class="dnnFormItem">
                <dnn:Label ID="lblRegistration" runat="server" ControlName="lblRegistration" Suffix=":" />
                <asp:TextBox ID="txtRegistration" runat="server" />
                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRegistration" ErrorMessage="*Required" CssClass="error" ValidationGroup="valsave"></asp:RequiredFieldValidator>--%>
            </div>
            <div class="dnnFormItem">
                <dnn:Label ID="lblListenViewLink" runat="server" ControlName="lblListenViewLink" Suffix=":" />
                <asp:TextBox ID="txtListenViewLink" runat="server" MaxLength="200"></asp:TextBox>

            </div>
            <div class="dnnFormItem">
                <dnn:Label ID="lblGoToMeetingAPi" runat="server" Suffix=":" Text="Goto Webinar Key" ControlName="lblGoToMeetingAPi" />
                <asp:TextBox ID="txtWebApiKey" runat="server" MaxLength="200"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtWebApiKey" ErrorMessage="*Required" CssClass="error" ValidationGroup="valsave"></asp:RequiredFieldValidator>--%>
            </div>
            <div class="dnnFormItem">
                <dnn:Label ID="lblIsActive" runat="server" ControlName="lblIsActive" Suffix=":" />
                <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
            </div>
            <div class="dnnFormItem">
                <h6>
                    <dnn:Label ID="lblCategory" runat="server" ControlName="lblCategory" Suffix=":" />
                    <h6>
                        <asp:CheckBoxList runat="server" ID="rdbCategory">
                        </asp:CheckBoxList>
            </div>
            <div class="dnnFormItem">
                <div class="form-group textarea_group tag_group">
                    <dnn:Label ID="lblTags" runat="server" ControlName="lblCategory" Suffix=":" />
                    <label>
                    </label>
                    <br />
                    <input style="display: none;" type="text" id="txtTags" class="tagsmanager" name="forum_tags">
                    <a style="display: none;" id="AddForumTag">
                        <img src="<%=ModulePath %>images/forum-add-new.png" /></a>
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
            </div>
            <div class="dnnFormItem" style="padding-left: 20%">
                <div id="ForumTags" class="tags_container">
                </div>
            </div>
        </fieldset>
    </div>
    <asp:HiddenField ID="hdAttachments" runat="server" />
    <%-- BEGINTEXT EDITOR--%>
</div>
<%--END TEXT EDITOR--%>
<asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select at least one Category."
    ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" />
<ul class="dnnActions dnnAction-button-section">
    <li>
        <asp:LinkButton ID="cmdSave" runat="server" CssClass="btn btn-info" Text="Save" ValidationGroup="valsave" OnClick="cmdSave_Click" OnClientClick="return Validate()" /></li>
    <li>
        <asp:HyperLink ID="hlCancel" ResourceKey="cmdCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" /></li>
    <li>
        <asp:LinkButton ID="cmdDelete" ResourceKey="cmdDelete" runat="server" CssClass="btn btn-danger" CausesValidation="False" Visible="False" Text="Delete" OnClick="cmdDelete_Click" /></li>
</ul>
<div class="dnnFormItem">
    <asp:ValidationSummary ID="valSummary" CssClass="dnnFormMessage dnnFormValidationSummary" EnableClientScript="true" runat="server" DisplayMode="BulletList" />
</div>

<asp:CustomValidator ID="valPost" EnableClientScript="False" runat="server" ResourceKey="valPost.ErrorMessage" Display="None" />
<asp:CustomValidator ID="valUpload" EnableClientScript="False" runat="server" Display="None" />
<script lang="javascript" type="text/javascript">
    var CA_selectedTags = '';
    function Validate() {
        var rb = document.getElementById("<%=rdbCategory.ClientID%>");
        var radio = rb.getElementsByTagName("input");
        var isChecked = false;
        for (var i = 0; i < radio.length; i++) {
            if (radio[i].checked) {
                isChecked = true;
                break;
            }
        }
        if (!isChecked) {
            alert("Please select aleast one Category from list!");
            return false;
        }
        var apikey = $('#<%=txtWebApiKey.ClientID%>').val();//document.getElementById("<%=txtWebApiKey.ClientID%>").val();
        var registrationlink = $('#<%=txtRegistration.ClientID%>').val();//document.getElementById("#<%=txtRegistration.ClientID%>").val();

        if (apikey == '' && registrationlink == '') {
            alert("Atleast Web API key or Registration link need to be shared");
            return false;
        }
        CA_selectedTags = '';
        // get the list of selected tags
        $('[id*="dlTags"] input:checked').each(function (i, o) {
            //selected.push($(this).attr('name'));
            CA_selectedTags += $('label[for="' + $(o)["0"].id + '"]').html() + ',';
        });

        if (CA_selectedTags == '') {
            alert('Please assign atleast one Tag');
            return false;
        }
        $('[id*="hdTagsSelected"]').val(CA_selectedTags);
        return isChecked;
    }

    function ValidateCheckBoxList(sender, args) {
        args.IsValid = readListControl()
    }

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
                    $('#' + IdTextArea + '').ckeditor();
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
                    // $('#' + IdTextArea+'').ckeditor();
                }
            }
        }
    }

    var baseServicepath = '/DesktopModules/ClearAction_Connections/API/Connections/';

    function getSuggestions(term, response) {
        $.ajax({
            type: "GET",
            cache: false,
            url: baseServicepath + "GetSuggestions",
            beforeSend: serviceFramework.setModuleHeaders,
            data: {
                groupId: -1,
                displayName: term
            }
        }).done(function (data) {

            response(data);
        }).fail(function () {
            //   displayMessage(settings.searchErrorText, "dnnFormWarning");
            response({}); // From jQuery UI docs: You must always call the response callback even if you encounter an error
        });
    };

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
            $('#<%=mdBasicSearch.ClientID%>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "GET",
                        url: baseServicepath + "GetSuggestions",
                        data: { groupId: -1, displayName: request.term },
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response(data);
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("Error");
                        }
                    });
                },
                minLength: 2,
                select: function (event, ui) {
                    getMember(ui.item);
                }
            });
            var profilePicHandler ='<% =DotNetNuke.Common.Globals.UserProfilePicRelativeUrl() %>';

            function getProfilePicture(u, w, h) {
                return profilePicHandler.replace("{0}", u).replace("{1}", h).replace("{2}", w);
            };

            function getMember(item) {
                $.ajax({
                    type: "GET",
                    cache: false,
                    url: baseServicepath + "GetMember",
                    //      beforeSend: serviceFramework.setModuleHeaders,
                    data: {
                        userId: item.userId,
                        SortBy: '1'
                    }
                }).done(function (members) {
                    if (typeof members !== "undefined" && members != null) {
                        var m = members[0];
                        $('#<%=lblTitle.ClientID%>').html(m.DisplayName);
                        $('#<%=lblCompanyName.ClientID%>').html(m.CompanyName);
                        $('#<%=lblAuthorID.ClientID%>').html(m.MemberId);
                        $('#<%=lblRole.ClientID%>').html(m.RoleName);
                        $('#profilelink').attr('href', m.ProfileUrl);

                        $('#<%=hdnAuthorID.ClientID%>').val(m.MemberId);
                        $('#<%=hdnCompanyName.ClientID%>').val(m.CompanyName);
                        $('#<%=hdnDisplayName.ClientID%>').val(m.DisplayName);
                        $('#<%=hdnRole.ClientID%>').val(m.RoleName);
                        $('#<%=hdnProfileUrl.ClientID%>').val(getProfilePicture(m.MemberId, 78, 78));
                        $('#profilelink').attr('href', m.ProfileUrl);

                        $('#<%=pnlAuthorDetail.ClientID%>').show();
                        $('#<%=profilepic.ClientID%>').attr('src', getProfilePicture(m.MemberId, 78, 78));
                    } else {
                        $('#<%=pnlAuthorDetail.ClientID%>').hide();
                    }
                }).fail(function (xhr, status) {
                    $('#<%=pnlAuthorDetail.ClientID%>').hide();
                });
            };

            $('#dnnBlogEditPost').dnnTabs({ selected: 0 });

            $('#<%=cmdDelete.ClientID%>').dnnConfirm({
                text: 'Are you sure want to delete this Event?',
                yesText: 'Yes',
                noText: 'No',
                title: 'Confirm'
            });

            $('#<%= dpPostDate.ClientID %>_wrapper').show();

            //$('#<= tpPostTime.ClientID %>_wrapper').show();
            $('#<%= txtTime.ClientID %>_wrapper').show();

            $("#mdBasicSearchBar input[type=text]").keydown(function (e) {
                if (e.which == 13) {
                    $("#mdBasicSearchBar a[class*=dnnPrimaryAction]").focus().click();
                    e.preventDefault();
                }
            });
            $(".CA_Single").click(function () {
                $(this).closest("table").find(".CA_Single").removeAttr('checked');
                $(this).attr('checked', 'checked');
            });
        });

        function SetTags(strTags) {
            var tagID = $(".tagsmanager").tagsManager({ tagsContainer: '#ForumTags', tagCloseIcon: 'X' });
            temp = strTags.split("_");
            for (a in temp) {
                $('[id*="dlTags"] input[type=checkbox]').each(function (i, o) {
                    if ($('label[for="' + $(o)["0"].id + '"]').html() == temp[a]) {
                        $("#" + $(o)["0"].id).attr("checked", 'true');
                    }
                });
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
