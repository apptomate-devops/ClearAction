<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="ClearAction.Modules.ProfileClearAction_ProfileSetup.View" EnableViewState="true" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke.Web" Namespace="DotNetNuke.Web.UI.WebControls" %>

<link rel="stylesheet" type="text/css" href="<%=ModulePath%>module.css">
<link href="<%=ModulePath%>CSS/bootstrap-theme.min.css" rel="stylesheet" />
<link rel="stylesheet" type="text/css" href="<%=ModulePath%>CSS/bootstrap-slider.min.css">

<script type="text/javascript" src="<%=ModulePath%>js/1.11.3.jquery.min.js"></script>
<script type="text/javascript" src="<%=ModulePath%>js/bootstrap-slider.min.js"></script>
<script type="text/javascript" src="<%=ModulePath%>js/bootstrap.min.js"></script>
<script type="text/javascript" src="<%=ModulePath%>js/autosize.min.js"></script>
<script src="//cdnjs.cloudflare.com/ajax/libs/html5shiv/r29/html5.min.js"></script>

<link rel="stylesheet" type="text/css" href="<%=ModulePath%>CSS/croppie.css">
<script type="text/javascript" src="<%=ModulePath%>js/croppie.js"></script>


<script type="text/javascript">
    //RG: set the tooltip for sliders
    function setSlideTooltip(elId) {
        setTooltip($(elId), 'top', 'hover');
        $(elId).tooltip('show');
        var tooltipId = $(elId).attr("aria-describedby");
        $('#' + tooltipId + ' .tooltip-inner').css('max-width', '400px');
        $(elId).tooltip('hide');
    }


    $(function () {

        var currTooltipEl = null;
        var desiredTooltipEl = null;

        $('#ex1').slider(

            {
                value: $("#<%=hdnSlider11.ClientID%>").val(),
                formatter: function (value) {

                    $("#<%=hdnSlider1.ClientID%>").val(value);

                    //RG: Adding tooltip support
                    ttId = 'tt-current-level-' + value;
                    if (currTooltipEl == null) {
                        currTooltipEl = $('#current-level');
                        $('#current-level').attr('tooltip-id', ttId);
                        setSlideTooltip("#current-level");
                    }
                    return 'Current value: ' + value;
                }
            });

        //RG: Update tooltip content according to the selected level
        $('#current-level').on('shown.bs.tooltip', function () {
            var ttId = '#tt-current-level-' + $("#<%=hdnSlider1.ClientID%>").val();
            var tooltipId = $('#current-level').attr("aria-describedby");
            $('#' + tooltipId + ' .tooltip-inner').html($(ttId).html());
        });
        $('#ex1').slider().on('change', function (event) {
            var value = event.value.newValue;
            //var b = event.value.oldValue;
            var tooltipId = $('#current-level').attr("aria-describedby");
            $('#' + tooltipId + ' .tooltip-inner').html($('#tt-current-level-' + value).html());
        });


        $('#ex2').slider({

            value: $("#<%=hdnSlider22.ClientID%>").val(),
            formatter: function (value) {

                $("#<%=hdnSlider2.ClientID%>").val(value);

                //RG: Adding tooltip support
                ttId = 'tt-current-level-' + value;
                if (desiredTooltipEl == null) {
                    desiredTooltipEl = $('#desired-level');
                    $('#desired-level').attr('tooltip-id', ttId);
                    setSlideTooltip('#desired-level');
                }

                return 'Current value: ' + value;
            }
        });

        //RG: Update tooltip content according to the selected level
        $('#desired-level').on('shown.bs.tooltip', function () {
            var ttId = '#tt-current-level-' + $("#<%=hdnSlider2.ClientID%>").val();
            var tooltipId = $('#desired-level').attr("aria-describedby");
            $('#' + tooltipId + ' .tooltip-inner').html($(ttId).html());
        });
        $('#ex2').slider().on('change', function (event) {
            var value = event.value.newValue;
            //var b = event.value.oldValue;
            var tooltipId = $('#desired-level').attr("aria-describedby");
            $('#' + tooltipId + ' .tooltip-inner').html($('#tt-current-level-' + value).html());
        });


        var iValue = $("#<%=hdnSlider1.ClientID%>").val();
      //  $("#<%=hdnSlider1.ClientID%>").val(iValue);
        $("#ex12").slider(
            {

                min: 0,
                max: 10,
                value: iValue,
                slide: function (event, ui) {

                    $("#<%=hdnSlider1.ClientID%>").val(ui.value);
                }
            });



        $('#<%=btnSaveInformation.ClientID%>').click(function () {
            if ($('#<%=chkTermandCondition.ClientID%>').is(':checked')) {

            }
            else {
                alert('please check terms & conditions'); return false;
            }
        })

    })

    $(function () {



        $('.btn_done').unbind().click(function () {


            $('#collapseOne, #collapseTwo, #collapseThree, #collapseFour, #collapseFive').not($(this).attr('href')).collapse('hide');


            $($(this).attr('href')).collapse('show');
        });


    });

    $(function () {
        "use strict";
        $('input').click(function () {

            $('input:not(:checked)').parent().parent().removeClass("active");
            $('input:checked').parent().parent().addClass("active");
        });

        $('.profile_setup .setup3 .checkbox_group a').unbind().click(function () {

            if ($(this).hasClass('select_all')) {
                $(this).html('(unselect all)');
                $(this).removeClass('select_all').addClass('unselect_all');
                $("[name='" + this.id + "']").prop('checked', true);
                $("[name='" + this.id + "']").parent().parent().addClass("active");
            }
            else {
                $(this).html('(select all)');
                $(this).removeClass('unselect_all').addClass('select_all');
                $("[name='" + this.id + "']").prop('checked', false);
                $("[name='" + this.id + "']").parent().parent().removeClass("active");
            }

            return false;
        });

    });

    jQuery(function ($) {


        var Step = GetParameterValues('Step');

        if (Step == null) {
            var otherway = getPathVariable('Step');
            if (otherway == '5' || otherway == '2')
                divToShow(otherway);
            else
                divToShow(1);
        }
        else
            divToShow(Step);

        $('#Start').click(function () {


            divToShow(2);

        });

        //Validate Company is selected or not.
        function CheckCompanySelectedorNot() {


            var gridView = document.getElementById("tbldropdowncontrol");
            var checkBoxes = gridView.getElementsByTagName("select");
            if (checkBoxes[0].value == -1)
                return false;

            //for (var i = 0; i < checkBoxes.length; i++) {
            //    if (checkBoxes[i].type == "select" && checkBoxes[i].checked) {
            //        alert(checkBoxes[i]);
            //    }
            //}

            return true;

        }



        $('#<%=Next2.ClientID%>').click(function () {

            divToShow(3);
            //if (CheckCompanySelectedorNot())
            //    divToShow(3);
            //else {
            //    alert('Please do select company from the list');
            //    $('[tooltip-id="require-company"]').click();
            //}

        });

        $('#<%=Next3.ClientID%>').click(function () {

            divToShow(4);

            //            $('#Step5').toggle('show');
        });

        $('#Save').click(function () {

            divToShow(5);


        });
        $('#Prev2').click(function () {

            divToShow(1);

        });
        $('#Prev3').click(function () {

            divToShow(2);

        });
        $('#Prev4').click(function () {

            divToShow(3);

        });
        $('#Prev5').click(function () {


            divToShow(4);

        });
    });
    function getPathVariable(variable) {
        var path = location.pathname;

        var parts = path.substr(1).split('/'), value;

        while (parts.length) {
            if (parts.shift() === variable)
                value = parts.shift();
            //      else parts.shift();
        }

        return value;
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





    function divToShow(step) {

        $('#Step4').hide();//('show');
        $('#Step1').hide();
        $('#Step2').hide();
        $('#Step3').hide();
        $('#Step5').hide();
        $('#Step6').hide();
        $('#Step' + step).toggle('show');
        // RG - Need to refresh the slides once the container becomes visible
        $("body").removeClass("profile_setup")
        if (step == 3) {
            $("input.ex1").slider("refresh");
            $("input.ex2").slider("refresh");
            $("body").addClass("profile_setup");
        }
    }

    function CA_ShowNextInGroup(ID) {
        //function added by Sachin to open next element after hidding the current div
        try {
            // Hide the current one
            $("#btnExCollapse_" + ID).click();
            // show the next in group
            $("#heading_" + ID).parent().next().find(".collapsed").click();
        } catch (e) { }
        return false;
    }



    function isokmaxlength(e, val, maxlengt) {
        var charCode = (typeof e.which == "number") ? e.which : e.keyCode;

        if (!(charCode == 44 || charCode == 46 || charCode == 0 || charCode == 8 || (val.value.length < maxlengt))) {
            AlertWindow('Warning', 'Max allowed input char:' + maxlengt);
            return false;
        }
    }

</script>
<script>

    function AlertWindow(head, content) {
        $.alert({
            title: head,
            type: 'red',
            content: content
        });
    }
    $(function () {
        $(".roles_column .radio").click(function () {
            $(this).addClass('active').siblings().removeClass('active');
        });
    });

    jQuery(function ($) {

        $("#<%=imageUpload.ClientID%>").change(function () {
            var input = this;

            var uploadFileExtension = $(this).val().split('.').pop().toLowerCase();


            var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
            if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {

                if (uploadFileExtension == '') {

                    return;
                }
                else {
                    AlertWindow("Invalid Format", "Only formats are allowed : " + fileExtension.join(', '));
                    $("#<%=imageUpload.ClientID%>").val('');
                    return;
                }
            }


            $("#<%=HiddenFieldCropedImageExtension.ClientID%>").val(uploadFileExtension)

            var file, img;
            if (input.files && input.files[0]) {
                img = new Image();
                var reader = new FileReader();

                reader.onload = function (e) {


                    var fileSize = Math.round(e.total / 1024);

                    if (fileSize < 2 * 2000) {

                        $("#<%=imgprvw.ClientID%>").attr('src', e.target.result);
                        $("#<%=HiddenFieldCropedImageString.ClientID%>").val(e.target.result)

                    }
                    else {
                      <%--  alert("Image size is greater than 1MB. Please choose an image of size less than 1MB.");
                        $("#<%=imgprvw.ClientID%>").attr('src', "");
                        $("#<%=imageUpload.ClientID%>").val("");--%>
                    }
                }
                reader.readAsDataURL(input.files[0]);


            }
        });
    });

    function CheckForOther(odata) {


        var selIndex = odata.selectedIndex;

        var selText = odata.options[selIndex].text;
        if (selText.toLowerCase() == 'other') {
            AlertWindow("Information", "Please select your company from the list. If you do not see your company here, select 'other', then send an email to <a href='mailto:success@clearaction.com' target='_blank'>success@clearaction.com</a> and we will add your company to the list.");
        }

    }

    function SetUniqueRadioButton(nameregex, current) {

        re = new RegExp(nameregex);
        for (i = 0; i < document.forms[0].elements.length; i++) {
            elm = document.forms[0].elements[i]
            if (elm.type == 'radio') {
                if (re.test(elm.name)) {

                    elm.checked = false;
                }
            }
        }


        $('#tblradioControl input[type="radio"]').each(function () {
            $(this).prop("checked", "");
        });

        $(this).prop("checked", "checked");
        current.checked = true;
    }


</script>

<style>
    #upload-cropping {
        margin-top: 10px;
    }

    .ActiveForCropping {
        display: block;
    }

    .de-ActiveForCropping {
        display: none;
    }

    #CropButtonSection {
        width: 100%;
        float: left;
        padding-left: 15px;
    }
</style>


<script type="text/javascript">

    $(document).ready(function () {


        $("#CropButtonSection").hide();

        $("#<%=imageUpload.ClientID%>").on('change', function () {

            readFile(this);
        });


        $uploadCrop = $('#upload-demo').croppie({
            viewport: { width: 200, height: 200, },
            enableResize: true,
            enableExif: true
        });

        $('.upload-result').on('click', function (ev) {

            $uploadCrop.croppie('result', {
                type: 'canvas',
                size: 'viewport'
            }).then(function (resp) {


                $("#<%=HiddenFieldCropedImageString.ClientID%>").val(resp)


                $("#<%=imgprvw.ClientID%>").attr("src", resp)

                $('#upload-cropping').removeClass('ActiveForCropping');
                $('#upload-cropping').addClass('de-ActiveForCropping');
                $("#CropButtonSection").show();
                });
        });

        $('.upload-result-cancel').on('click', function (ev) {

            $('#upload-cropping').removeClass('ActiveForCropping');
            $('#upload-cropping').addClass('de-ActiveForCropping');
            $("#CropButtonSection").show();

        });


        $('#buttonCrop').on('click', function (ev) {

            $('#upload-cropping').addClass('ActiveForCropping');
            $('#upload-cropping').removeClass('de-ActiveForCropping');
            $("#CropButtonSection").hide();

        });

        var $uploadCrop;
        function readFile(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {

                    $('#upload-cropping').addClass('ActiveForCropping');
                    $('#upload-cropping').removeClass('de-ActiveForCropping');

                    $("#CropButtonSection").hide();

                    $uploadCrop.croppie('bind', {
                        url: e.target.result
                    }).then(function () {
                        console.log('jQuery bind complete');
                    });

                }

                reader.readAsDataURL(input.files[0]);
            }
            else {
                swal("Your browser doesn't support the FileReader API; please try another browser.");
            }
        }





    });

</script>



<asp:HiddenField ID="hdnSlider1" runat="server" ClientIDMode="Static" Value="" />
<asp:HiddenField ID="hdnSlider2" runat="server" ClientIDMode="Static" Value="" />


<asp:HiddenField ID="hdnSlider11" runat="server" ClientIDMode="Static" Value="" />
<asp:HiddenField ID="hdnSlider22" runat="server" ClientIDMode="Static" Value="" />

<asp:HiddenField ID="HiddenFieldCropedImageString" runat="server" ClientIDMode="Static" Value="" />
<asp:HiddenField ID="HiddenFieldCropedImageExtension" runat="server" ClientIDMode="Static" Value="" />

<div class="content_wrapper">
    <div id="Step1">
        <div class="row">
            <div class="form_header col-sm-10">
                <h2>1</h2>
                <div class="circle circle_active"></div>
                <div class="circle"></div>
                <div class="circle"></div>
                <div class="circle"></div>
                <span>OF 4</span>
            </div>
        </div>
        <div class="row">

            <div class="form_body col-lg-6 col-md-10 col-sm-10">

                       <h3>Welcome to ClearAction Value Exchange</h3> 

              <strong>It's exciting that you and your company are investing in your future.</strong><br /><br />

              <ul><li><strong>Save time</strong>: Your answers on the next 2 pages will personalize your <em>My Exchange</em> home page and your <em>My Vault</em> curated list of resources.</li></ul>

              <ul><li><strong>Personalize</strong>: Your profile and photo add friendliness to <em>your</em> member-driven, member-led community.</li></ul>

              <ul><li><strong>Start a Routine</strong>: Set a time each day or week to log-in to the Exchange.</li></ul>

              <ul><li><strong>Stay Informed</strong>: White-list ClearAction.com in your email inbox.</li></ul>

              <br />

              <strong>The future is evolving rapidly. It's affecting your work TODAY.</strong><br /><br />

              The Exchange prepares you for it efficiently and sustainably.<br /><br />

              It's 24x7 real-time skill-building and problem-solving for your daily interactions.<br /><br />

              <strong>Key goals</strong>:<br /><br />

              <ul><li>Uplevel your company's collective capability.</li></ul>

              <ul><li>Increase your career satisfaction.</li></ul>

              <br />

              <strong>You can create on-the-spot action plans in 20-40 minutes:</strong><br /><br />

                                               <ul><li><strong>Solve-Spaces</strong><sup>TM</sup> &#8212; interactive templates for a new perspective to solve your challenges.</li></ul>

                <ul><li><strong>Open Forum</strong> &#8212; post questions, files and answers for all members.</li></ul>

                <ul><li><strong>WebCast Conversations</strong><sup>TM</sup> &#8212; 5-minute slide + 5-minutes to discuss x 4 slides.</li></ul>

                <ul><li><strong>Community Calls</strong> &#8212; free-form conversations facilitated by a subject matter expert.</li></ul>

                <ul><li><strong>Expert Presentations</strong> &#8212; 20-minute presentations by a subject matter expert with up to 10 minutes Q&amp;A.</li></ul>

              <ul><li><strong>Virtual Conferences</strong> &#8212; series of 20-minute sessions with 10-minute breaks spanning a day from Europe to North America to Australia.</li></ul>

              <ul><li><strong>Roundtables</strong> &#8212; quarterly afternoon workshops in metropolitan areas with 5+ member companies.</li></ul>

              <ul><li><strong>Insights Vault</strong> &#8212; actionable articles, training, videos, podcasts, slide decks, infographics, study highlights, how-to guides.</li></ul>

              <br />

          <p><strong>Let's get started!</strong></p>

            </div>

        </div>  

        <div class="row">
            <div class="form_footer col-sm-10"><a class="btn btn-large btn-primary" type="submit" id="Start"><span>START</span> </a></div>
        </div>
    </div>


    <div id="Step2" style="display: none;">
        <div class="row">
            <div class="form_header col-sm-10">
                <h2>2</h2>
                <div class="circle circle_completed"></div>
                <div class="circle circle_active"></div>
                <div class="circle"></div>
                <div class="circle"></div>
                <span>OF 4</span>
            </div>
        </div>
        <div class="row">
            <div class="form_body col-sm-6 col-xs-12 col-lg-5">
                 <h3>Personalize Your Experience</h3>
                <div class="form-group" style="display:none" id="toplinkedin" runat="server">
                     <label>Your profile and photo add friendliness to your member-driven, member-led community. </label>
                    <br>
                    <span class="upload-light">(You will be able to edit any information that LinkedIn inserts.)</span>
                    <br>
                    <asp:LinkButton runat="server" OnClick="linkedin_button_Click" ID="linkedin_button" CssClass="linkedin_connect_button">Import from LinkedIn</asp:LinkButton>
                </div>
                <div class="setup2">


                    <div class="row upload">



                        <div class="col-xs-3">



                            <div class="cloud">

                                <img src="#" id="imgprvw" runat="server" style="width: 85px; height: 85px" />
                            </div>

                            <div>
                                <%--<label for="imageUpload" class="btn btn-upload">Upload</label>--%>
                                <input type="file" id="imageUpload" class="btn btn-upload" runat="server">
                            </div>
                        </div>
                        <div class="col-xs-3 upload_text" style="width: 70%;">
                            Upload your profile photo
                        <br>
                            <span class="upload-light upload_text">Maximum size 1MB. Ideal size is 200 x 200 pixels.</span>
                        </div>

                        <div id="CropButtonSection">
                            <a id="buttonCrop" class="btn btn-large btn-primary">Crop Pictures</a>
                        </div>

                        <div id="upload-cropping" class="de-ActiveForCropping">
                            <div class="upload-demo-wrap">
                                <div id="upload-demo"></div>
                            </div>

                            <div style="padding-left: 15px;">
                                <a class="upload-result btn btn-large btn-primary">Set</a>
                                <a class="upload-result-cancel btn btn-large btn-secondary">Cancel</a>
                            </div>



                        </div>

                    </div>

                </div>

                <div class="setup3">
                    <table id="tbldropdowncontrol" style="width: 100%">
                        <tr>
                            <td>
                                <asp:Repeater ID="rptStep2" runat="server" OnItemDataBound="rptStep_ItemDataBound">
                                    <ItemTemplate>

                                        <asp:HiddenField ID="hdnQuestionID" runat="server" Value='<%#Eval("QuestionID") %>' />
                                        <asp:HiddenField ID="hdnQuestionType" runat="server" Value='<%#Eval("QuestionTypeID") %>' />
                                        <asp:HiddenField ID="hdnIsRequire" runat="server" Value='<%#Eval("RequireStyle") %>' />
                                        <asp:PlaceHolder ID="pholderInputText" runat="server">
                                            <div class="form-group">
                                                <label><%#Eval("QuestionText") %></label>
                                                <asp:TextBox ID="txtinputBox" placeholder='<%#Eval("Hint") %>' runat="server" MaxLength='<%#Eval("Size") %>' />
                                                <asp:RequiredFieldValidator ID="reqField" ControlToValidate="txtinputBox" ValidationGroup="step4" runat="server" ErrorMessage='<%#Eval("Hint") %>' SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>

                                        </asp:PlaceHolder>
                                        <asp:PlaceHolder ID="pholderInputChoice" runat="server">
                                            <div class="form-group">
                                                <div style="float: left">
                                                    <label><%#Eval("QuestionText") %></label>
                                                </div>

                                                <div class="community_calls" style="float: left">
                                                    <div class="calls_lt">
                                                        <h4 class="blg" tooltip-id="require-company">&nbsp;</h4>
                                                        <div id="require-company" class="popup">
                                                            <div class="content">
                                                                <div class="popup_con1">
                                                                  <p style="color: #FFFF !important; font-size: 12px">Please select your company from the list. If you do not see your company here, select "other", then send an email to <a href="mailto:success@clearaction.com" target="_blank">Success@clearaction.com</a> and we will add your company to the list. Then revisit "Profile" to specify your company.</p>


                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <asp:DropDownList ID="rptQuestionOptions" runat="server" CssClass="form-control" onchange="CheckForOther(this);"></asp:DropDownList>


                                                <br />
                                                <asp:RequiredFieldValidator ID="reqFieldDropdown" runat="server" Text="*Require" ValidationGroup="Step2" ControlToValidate="rptQuestionOptions" InitialValue="-1">
                                                </asp:RequiredFieldValidator>
                                            </div>
                                        </asp:PlaceHolder>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="setup3">


                    <div class="form-group">
                        <label>Social Media Links:</label>
                        <div class="form-group social">
                            <label class="light">LinkedIn profile address
                                <%--<span class="circle">?</span>--%>
                            </label>
                            <div class="form-inline">
                                <label>
                                    <img src="<%=ModulePath %>images/linkedin.png"></label>
                                <input id="txtlinkedin" type="text" class="form-control" placeholder="" runat="server">
                            </div>
                        </div>
                        <div class="form-group social">
                            <label class="light">Twitter profile address
                                <%--<span class="circle">?</span>--%>
                            </label>
                            <div class="form-inline">
                                <label>
                                    <img src="<%=ModulePath %>images/twitter.png"></label>
                                <input id="txttwitter" runat="server" type="text" class="form-control" placeholder="">
                            </div>
                        </div>
                        <div class="form-group social">
                            <label class="light">Facebook profile address
                                <%--<span class="circle">?</span>--%>
                            </label>
                            <div class="form-inline">
                                <label>
                                    <img src="<%=ModulePath %>images/facebook.png"></label>
                                <input id="txtfacebook" runat="server" type="text" class="form-control" placeholder="">
                            </div>
                        </div>

                    </div>

                    <asp:Repeater ID="rptStep3" runat="server" OnItemDataBound="rptStep_ItemDataBound">
                        <ItemTemplate>
                               <asp:PlaceHolder ID="pholderInputText" runat="server">
                            <div class="form-group">
                                <label><%#Eval("QuestionText") %></label>
                                <asp:HiddenField ID="hdnQuestionType" runat="server" Value='<%#Eval("QuestionTypeID") %>' />
                                <asp:HiddenField ID="hdnQuestionID" runat="server" Value='<%#Eval("QuestionID") %>' />
                                <asp:HiddenField ID="hdnIsRequire" runat="server" Value='<%#Eval("RequireStyle") %>' />
                                <asp:TextBox ID="txtinputBox" CssClass="form-control" placeholder='<%#Eval("Hint") %>' runat="server" MaxLength='<%#Eval("Size") %>' onkeypress='<%#String.Format("return isokmaxlength(event,this,{0});", DataBinder.Eval(Container,"Dataitem.Size"))%>' />
                                <asp:RequiredFieldValidator ID="reqField" CssClass="RequireField" ControlToValidate="txtinputBox" ValidationGroup="step3" runat="server" Text='<%#Eval("Hint") %>' SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                            </asp:PlaceHolder>  
                            <asp:PlaceHolder ID="pholderInputChoice" runat="server">
                            <div class="form-group radio_group radio_group1 clearfix">
                                  <label><%#Eval("QuestionText") %></label>
                                <table id="tblradioControl" style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Repeater ID="rptQuestionOptions" runat="server" OnItemDataBound="rptQuestionOptions_ItemDataBound">
                                                <ItemTemplate>
                                                    <div class="radio icon">
                                                        <label>
                                                            <asp:HiddenField ID="QuestionOptionID" runat="server" Value='<%#Eval("QuestionOptionID") %>' />
                                                            <asp:RadioButton GroupName="SingleSelection" value='<%#Eval("QuestionOptionID") %>' runat="server" ID="radioControl" Checked='<%#GetQuestionResponseRadio(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"QuestionID")), Convert.ToInt32(DataBinder.Eval(Container.DataItem,"QuestionOptionID"))) %>' />


                                                            <span><%#Eval("OptionText") %></span>
                                                        </label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </table>
                                <%--<div style="display: none">
                                    <asp:RadioButtonList ID="cmbRadioControl" runat="server" RepeatColumns="1" CssClass="radio">
                                    </asp:RadioButtonList>
                                </div>--%>
                            </div>
      </asp:PlaceHolder> 
                        </ItemTemplate>
                    </asp:Repeater>
                 
                    

                </div>
            </div>
        </div>



        <div class="row">
            <div class="form_footer col-sm-10">
                <a class="btn btn-large btn-secondary" type="submit" id="Prev2"><span>BACK</span> </a>
                <asp:HyperLink runat="server" href="javascript:doValidate()" ValidateRequestMode="Enabled" CssClass="btn btn-large btn-primary" Visible="false" ID="Next21" ValidationGroup="step2"><span>NEXT</span> </asp:HyperLink>
                <asp:LinkButton ID="Next2" runat="server" CausesValidation="true" ValidationGroup="step2" CssClass="btn btn-large btn-primary" OnClientClick="return false;"><span>NEXT</span></asp:LinkButton>
            </div>
        </div>
    </div>


    <div id="Step3">

        <div class="row">
            <div class="form_header col-sm-10">
                <h2>3</h2>
                <div class="circle circle_completed"></div>
                <div class="circle circle_completed"></div>
                <div class="circle circle_active"></div>
                <div class="circle"></div>
                <span>OF 4</span>
            </div>
        </div>

        <div class="row">
            <div class="form_body col-sm-10">
              <h3>Personalize Your Home Page</h3>
              <p><span>IMPORTANT!  Your home page curates resources tailored to you, based on your answers below:</span></p>
            </div>
        </div>
        <div class="row">
            <div class="form_body col-xs-12 col-md-5">
                <asp:Repeater ID="rptStep4" runat="server" OnItemDataBound="rptStep4_ItemDataBound">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnQuestionID" runat="server" Value='<%#Eval("QuestionID") %>' />
                        <asp:HiddenField ID="hdnQuestionType" runat="server" Value='<%#Eval("QuestionTypeID") %>' />
                        <asp:HiddenField ID="hdnIsRequire" runat="server" Value='<%#Eval("RequireStyle") %>' />
                        <asp:PlaceHolder ID="pholderInputText" runat="server">
                            <div class="form-group">
                                <label><%#Eval("QuestionText") %></label>
                                <asp:TextBox ID="txtinputBox" placeholder='<%#Eval("Hint") %>' runat="server" MaxLength='<%#Eval("Size") %>' />
                                <asp:RequiredFieldValidator ID="reqField" ControlToValidate="txtinputBox" ValidationGroup="step4" runat="server" ErrorMessage='<%#Eval("Hint") %>' SetFocusOnError="true"></asp:RequiredFieldValidator>
                            </div>
                            <br />
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="pholderInputChoice" runat="server">


                            <div class="form-group radio_group radio_group1 clearfix">
                                <label><%#Eval("QuestionText") %></label>
                                <table id="tblradioControl" style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Repeater ID="rptQuestionOptions" runat="server" OnItemDataBound="rptQuestionOptions_ItemDataBound">
                                                <ItemTemplate>
                                                    <div class="radio icon">
                                                        <label>
                                                            <asp:HiddenField ID="QuestionOptionID" runat="server" Value='<%#Eval("QuestionOptionID") %>' />
                                                            <asp:RadioButton GroupName="SingleSelection" value='<%#Eval("QuestionOptionID") %>' runat="server" ID="radioControl" Checked='<%#GetQuestionResponseRadio(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"QuestionID")), Convert.ToInt32(DataBinder.Eval(Container.DataItem,"QuestionOptionID"))) %>' />


                                                            <span><%#Eval("OptionText") %></span>
                                                        </label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </table>
                                <%--<div style="display: none">
                                    <asp:RadioButtonList ID="cmbRadioControl" runat="server" RepeatColumns="1" CssClass="radio">
                                    </asp:RadioButtonList>
                                </div>--%>
                            </div>


                        </asp:PlaceHolder>
                    </ItemTemplate>
                </asp:Repeater>

                <br />
                <div class="form-group">
                    <div class="form-group radio_group radio_group1 clearfix">
                        <label>Select the best match for your current role:</label>

                        <asp:DropDownList ID="cmbRoles" runat="server" CssClass="forum-select">
                        </asp:DropDownList>

                    </div>
                </div>


                <br />
                 <div class="radio_scale3 profile_setup setup3">
                    <label>Indicate your CURRENT level of strategic and collaborative skills:</label>
                    <br>
                    <br>
                    <br>
                    <div id="current-level" class="row slider_wrapper">
                        <div class="col-xs-4 left">Level I &raquo;</div>
                        <div class="col-xs-4 middle">Level II &raquo;</div>
                        <div class="col-xs-4 right">Level III &raquo;</div>
                        <div class="col-xs-12 slider_cnt">
                            <input id="ex1" style="width: 100%;" data-slider-tooltip="hide" type="text" data-slider-ticks="[1,2,3,4,5,6,7,8,9,10]" data-slider-ticks-snap-bounds="40" data-slider-ticks-labels="[1,2,3,4,5,6,7,8,9,10]" data-slider-value="4" />
                        </div>
                    </div>
                   <br>
                    <br>
                    <br>
                    <label>Indicate your DESIRED level in the next 3-4 months:</label>
                    <br>
                    <br>
                    <br>
                    <div id="desired-level" class="row slider_wrapper">
                        <div class="col-xs-4 left">Level I &raquo;</div>
                        <div class="col-xs-4 middle">Level II &raquo;</div>
                        <div class="col-xs-4 right">Level III &raquo;</div>
                        <div class="col-xs-12 slider_cnt">
                            <input id="ex2" style="width: 100%;" data-slider-tooltip="hide" type="text" data-slider-ticks="[1,2,3,4,5,6,7,8,9,10]" data-slider-ticks-snap-bounds="40" data-slider-ticks-labels="[1,2,3,4,5,6,7,8,9,10]" data-slider-value="7" />
                        </div>
                    </div>
                </div>
            </div>



        </div>
        <div class="row">
            <div class="form_body col-md-7">
                <br />
                <br />
                <br />
                <br />
                <div class="functional">
                    <asp:Repeater ID="rptStep5" runat="server" OnItemDataBound="rptStep5_ItemDataBound">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnQuestionID" runat="server" Value='<%#Eval("QuestionID") %>' />
                            <asp:HiddenField ID="hdnQuestionType" runat="server" Value='<%#Eval("QuestionTypeID") %>' />
                            <div class="form-group">
                                <label><%#Eval("QuestionText") %></label>
                            </div>
                            <br />
                            <div id="accordion" role="tablist" class="panel-group" aria-multiselectable="true">
                                <div class="clearfix"></div>

                                <asp:Repeater ID="rptMainCategory" runat="server" OnItemDataBound="rptMainCategory_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="panel card">
                                            <div class="card-header" role="tab" id="heading_<%#Eval("CategoryID") %>">
                                                <h5 class="mb-0"><a id="btnExCollapse_<%#Eval("CategoryID") %>" class="collapsed" data-toggle="collapse" href="#collapse_<%#Eval("CategoryID") %>" aria-expanded="false" aria-controls="collapsecollapse_<%#Eval("CategoryID") %>"><span><%#Eval("CategoryOrder") %></span> In <%#Eval("CategoryName") %>: <span>Select all that apply:</span> </a></h5>
                                            </div>
                                            <asp:HiddenField ID="hdnCategoryID" runat="server" Value='<%#Eval("CategoryID") %>' />
                                            <asp:Label ID="hdnQuestionID2" runat="server" Value='<%# DataBinder.Eval(Container.NamingContainer.NamingContainer, "DataItem.QuestionID")%>' />
                                            <div id="collapse_<%#Eval("CategoryID") %>" class="collapse" role="tabpanel" aria-labelledby="heading_<%#Eval("CategoryID") %>">
                                                <div class="row card-body">
                                                    <asp:Repeater ID="rptSubCategory" runat="server" OnItemDataBound="rptSubCategory_ItemDataBound">

                                                        <ItemTemplate>
                                                            <div class="col-lg-4">
                                                                <div style="padding-top: 10px">
                                                                    <div class="form-group radio_group checkbox_group radio_group4">
                                                                        <asp:HiddenField ID="hdnQuestionInnerID" runat="server" Value="17" />
                                                                        <label style="font-size: 14px; color: #004a7e;"><%#Eval("CategoryName") %></label>
                                                                        <asp:HiddenField ID="hdnSubCategoryID" runat="server" Value='<%#Eval("CategoryID") %>' />
                                                                        <asp:Repeater ID="rptQuestionOptions" runat="server">
                                                                            <ItemTemplate>
                                                                                <div class="radio icon">
                                                                                    <label style="color: #004a7e;">
                                                                                        <asp:HiddenField ID="hdnsubCategory" runat="server" Value='<%#Eval("QuestionOptionID") %>' />

                                                                                        <input type="checkbox" name="GoogleCalenderList" value='<%#Eval("QuestionOptionID") %>' runat="server" id="radioControl" checked='<%#GetQuestionResponseRadio(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"QuestionID")), Convert.ToInt32(DataBinder.Eval(Container.DataItem,"QuestionOptionID"))) %>' />
                                                                                        <span style="font-size: 14px"><%#Eval("OptionText") %></span></label>
                                                                                </div>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>

                                                    </asp:Repeater>

                                                </div>
                                                <div class="row card-body">
                                                    <div class="col-xs-12 text-center">
                                                        <div class="CA_btnDoneGrp" onclick='CA_ShowNextInGroup(<%#Eval("CategoryID")%>);'>Done with this group</div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>


                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </div>



        </div>
        <div class="row">
            <div class="form_body col-md-5">
                <div class="col-xs-12">

                    <asp:Repeater ID="rptStep6" runat="server" OnItemDataBound="rptStep6_ItemDataBound">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnQuestionID" runat="server" Value='<%#Eval("QuestionID") %>' />
                            <asp:HiddenField ID="hdnQuestionType" runat="server" Value='<%#Eval("QuestionTypeID") %>' />
                            <asp:HiddenField ID="hdnIsRequire" runat="server" Value='<%#Eval("RequireStyle") %>' />
                            <asp:PlaceHolder ID="pholderInputText" runat="server">
                                <div class="form-group">
                                    <label><%#Eval("QuestionText") %></label>

                                    <asp:TextBox ID="txtinputBox" value='<%#GetQuestionResponseText(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"QuestionID"))) %>' placeholder='<%#Eval("Hint") %>' runat="server" MaxLength='<%#Eval("Size") %>' />
                                </div>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="pholderInputChoice" runat="server">
                                <div class="form-group radio_group checkbox_group radio_group19">
                                    <label><%#Eval("QuestionText") %></label>

                                    <asp:Repeater ID="rptQuestionOptions" runat="server">
                                        <ItemTemplate>
                                            <div class="radio icon">
                                                <label>
                                                    <asp:HiddenField ID="QuestionOptionID" runat="server" Value='<%#Eval("QuestionOptionID") %>' />
                                                    <input type="checkbox" name="GoogleCalenderList" value='<%#Eval("QuestionOptionID") %>' runat="server" id="radioControl" checked='<%#GetQuestionResponseRadio(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"QuestionID")), Convert.ToInt32(DataBinder.Eval(Container.DataItem,"QuestionOptionID"))) %>' />
                                                    <span><%#Eval("OptionText") %></span>
                                                </label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div>
                            </asp:PlaceHolder>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form_footer col-sm-10">
                <a class="btn btn-large btn-secondary" id="Prev3" type="submit"><span>BACK</span> </a>
                <asp:HyperLink runat="server" CausesValidation="true" CssClass="btn btn-large btn-primary" ID="Next3" ValidationGroup="step4"><span>NEXT</span> </asp:HyperLink>
            </div>
        </div>
        <%--   <div class="row">
            <div class="form_footer col-sm-10">
                <a class="btn btn-large btn-secondary" id="Prev3" type="submit"><span>BACK</span> </a>
                <asp:HyperLink runat="server" CausesValidation="true" CssClass="btn btn-large btn-primary" ID="Next3" ValidationGroup="step3"><span>NEXT</span> </asp:HyperLink>
            </div>
        </div>--%>
    </div>


    <div id="Step4" style="display: none;">
        <div class="row">
            <div class="form_header col-sm-10">
                <h2>4</h2>
                <div class="circle circle_completed"></div>
                <div class="circle circle_completed"></div>
                <div class="circle circle_completed"></div>
                <div class="circle circle_active"></div>

                <span>OF 4</span>
            </div>
        </div>
        <div class="row">

            <div class="form_body col-sm-6 col-xs-12 col-lg-6">
                <h3>Terms and Conditions</h3>
              <strong>Respect in our community is emphasized in our membership agreement.</strong><br /><br />
              Here are 5 key points:<br />

              1) All content is for your internal company use only.<br />

              2) Show respect for everyone and their information.<br />

              3) Refrain from promoting any specific tool, brand, person or company.<br />

              4) Respect all intellectual property, copyright and other laws.<br />

              5) You are solely responsible for your interactions.<br />

              <br />
              Your privacy is respected:<br /><br />
              <ul><li>Your action plans in Solve-Spaces<sup>TM</sup> and templates are for your eyes only, unless you share them.</li></ul>
              <ul><li>Private conversations in Collab-Spaces™ for just your company are on our product roadmap.</li></ul>
              <ul><li>You can see the full policy anytime. You can also click these links at the bottom of the ClearAction.com website:</li></ul>
              &nbsp;&nbsp;&nbsp;&nbsp;<a href="https://clearaction.com/membership-agreement" target="_blank">clearaction.com/membership-agreement</a><br /><br />
              &nbsp;&nbsp;&nbsp;&nbsp;<a href="https://clearaction.com/privacy-policy" target="_blank">clearaction.com/privacy-policy</a><br /><br />
                <div class="setup4">
                    <div class="form-group">

                        <div style="word-wrap: break-word; resize: horizontal; height: 234px; overflow-y: scroll; word-wrap: break-word; resize: horizontal;" class="form-control">

              <p>PLEASE READ THESE TERMS OF USE CAREFULLY. <br /><br />
                 BY ACCESSING, USING AND PARTICIPATING IN THE CLEARACTION VALUE EXCHANGE ("Exchange"), YOU AGREE TO BE BOUND BY THE TERMS DESCRIBED HEREIN AND ALL TERMS INCORPORATED BY REFERENCE. <br /><br />
                IF YOU DO NOT AGREE TO ALL OF THESE TERMS, DO NOT PARTICIPATE IN THE CLEARACTION VALUE EXCHANGE.</p>
             <h3>ClearAction Value Exchange Membership Agreement</h3>
<p>By accessing the ClearAction Value Exchange ("Exchange") you are agreeing to be bound by the following terms and conditions ("Terms of Service"). ClearAction Continuum reserves the right to update and change the Terms of Service from time to time without notice. <br /><br />Any new features that augment or enhance the current Exchange, including the release of new tools and resources, will be subject to the Terms of Service. Continued use of the Exchange after any such changes will constitute your consent to such changes. <br /><br />Violation of any of the terms below will result in the termination of your account. You agree to use the Exchange at your own risk. <br /><br />The ClearAction Continuum Privacy Policy available at <a href="https://clearaction.com/privacy-policy/" target="_blank">https://clearaction.com/privacy-policy</a> is hereby incorporated by reference into these Terms of Service.</p>

                          <p><strong>Account Terms</strong></p>
<p>&#8226; You must be 18 years or older to be part of this Program.</p>
<p>&#8226; You must provide your legal full name, a valid email address, and any other information requested in order to complete the signup process.</p>
<p>&#8226; Your login may only be used by one person – a single login shared by multiple people is not permitted.</p>
<p>&#8226; One person or legal entity may not maintain more than one account.</p>
<p>&#8226; You are responsible for maintaining the security of your account and password. ClearAction Continuum cannot and will not be liable for any loss or damage from your failure to comply with this security obligation.</p>
<p>&#8226; You are responsible for all Content posted and activity that occurs under your account.</p>
<p>&#8226; You may not use the Exchange for any illegal or unauthorized purpose. You must not, in the use of the Service, violate any laws in your jurisdiction (including but not limited to copyright laws).</p>
<p>&#8226; You may not use the Exchange for commercial purposes, which means you will not copy or redistribute Exchange content for any reason other than your internal company use.</p>

                          <p><strong>Copyright and Limited License</strong></p>
<p>Unless otherwise indicated, the Exchange and all content and other materials in the Exchange, including, without limitation, the logo, text, graphics, photos. video, audio, data, software, and other files and the selection and arrangement thereof (collectively, the "Exchange Content") are the proprietary property of ClearAction Continuum or its sponsors or licensors and are protected by U.S. and international copyright and/or other intellectual property laws. <br /><br />Subject to these Terms, ClearAction Continuum grants you a limited, non-exclusive, non-sublicensble and fully revocable license to access and use the Exchange and Exchange Content during the time that you are in compliance with these Terms of Service.</p>

                          <p><strong>Member Content</strong></p>
<p>Members are responsible for any content or information they post or transmit in the Exchange, and ClearAction Continuum assumes no responsibility for the conduct of any Member submitting any content or for the information transmitted by any Member. <br /><br />You will not make available in the Exchange any material or information that infringes any copyright, trademark, patent, trade secret, right of privacy, right of publicity, or other right of any person or entity, or impersonates any other person.</p>

<p>ClearAction Continuum assumes no responsibility for monitoring the Exchange for inappropriate or illegal content or conduct and has no obligation to monitor Member content. <br /><br />To the extent ClearAction Continuum becomes aware of any non-compliant Member content, it will have the right, in its sole discretion, to edit, refuse to post, or remove any Member content.</p> 

<p>The Exchange experience may provide you and other Members with opportunity to submit questions, comments, suggestions, ideas, plans, notes, drawings, creative materials or other information related to the Exchange ("Submissions"). Submissions, however they are transmitted, are non-confidential as between you and ClearAction Continuum and shall become the sole property of ClearAction Continuum upon receipt. <br /><br />ClearAction Continuum shall own, and you hereby assign to ClearAction Continuum, all rights, title and interest, including all intellectual property rights, in and to such Submissions. ClearAction Continuum shall be entitled to the unrestricted use and dissemination of these Submissions for non-commercial purposes, without compensation to you. You agree to execute any documentation required by ClearAction Continuum (in its sole discretion) to confirm such assignment to, and unrestricted use and dissemination by, ClearAction Continuum of such Submissions. <br /><br />Exceptions apply to authors of Solve-Spaces<sup>TM</sup> and Insights Vault posts, where authors' attribution shall be published and authors' copyright shall be respected.</p>

                          <p><strong>Member Disputes</strong></p>
<p>You are solely responsible for your interactions in the Exchange with other Members and any other Parties with whom you interact through the Exchange and/or ClearAction Continuum services. ClearAction Continuum reserves the right, but has no obligation, to become involved in any way in these disputes. <br /><br />As a condition of using the Exchange, you release ClearAction Continuum (and ClearAction Continuum's shareholders, partners, affiliates, directors, officers, subsidiaries, employees, agents, suppliers and  licensees) from claims, demands and damages (actual and consequential) of every kind and nature, known and unknown, suspected and unsuspected, disclosed and undisclosed, arising out of or in any way connected with any dispute you have, or claim to have, with one or more Members of the Exchange.</p>

                          <p><strong>Third-Party Content or Advertising</strong></p>
<p>ClearAction Continuum may, from time to time, provide third-party content in the Exchange and may provide, as a service, links to webpages and content of third parties ("Third-Party Content"). ClearAction Continuum does not monitor or have control over Third-Party Content or third-party websites. Unless otherwise expressly stated, ClearAction Continuum does not endorse or adopt, and is not responsible or liable for any Third-Party Content. <br /><br />ClearAction Continuum does not make any representations or warranties of any kind regarding the Third-Party Content, including, without limitation, any representations or warranties regarding its accuracy, completeness or non-infringement. ClearAction Continuum undertakes no responsibility to update or review any Third-Party Content, and Members use such Third-Party Content contained therein at their own risk.</p>

                          <p><strong>DMCA</strong></p>
<p>ClearAction Continuum’s policy is to respond to notices of alleged infringement that comply with the Digital Millennium Copyright Act ("DMCA"). Copyright infringing materials found within the Exchange can be identified and removed via the ClearAction Continuum DMCA compliance process posted at <a href="https://clearaction.com/dmca" target="_blank">https://clearaction.com/dmca</a>. You agree to comply with such process in the event you are involved in any claim of copyright infringement to which the DMCA may be applicable.</p>

                          <p><strong>Conduct Within the Exchange</strong></p>
<p>As a condition to your participation in the Exchange, you agree that while you are an Exchange participant you will comply with all laws, ordinances, rules, regulations, orders, licenses, permits, judgments, decisions or other requirements of any governmental authority that has jurisdiction over you, whether those laws, etc. are now in effect or later come into effect during the time you are an Exchange participant. <br /><br />Without limiting the foregoing obligation, you agree that as a condition of your participation in the Exchange you will comply with our policies to ensure a safe and inclusive community. Accordingly, you will not:</p>
<p>&#8226; Attempt to use the Exchange to harass, abuse or harm, or to advocate or incite harassment, abuse or harm of another person or group.</p>
<p>&#8226; Post any abusive, threatening, obscene, defamatory, libelous, or racially, sexually, religiously, or otherwise objectionable or offensive content or information or any content or information that contains nudity, excessive violence, or offensive subject matter or that contains a link to such content.</p>
<p>&#8226; Solicit or attempt to solicit personal information from other Members of the Exchange, or harvest or post anyone's private information, including personally identifiable or financial information in or through the use of the Exchange.</p>
<p>&#8226; Advertise, spam or distribute any malware, spyware or other malicious content in or through the Exchange.</p>
<p>&#8226; Disrupt the Exchange in any manner (including but not limited to using automation software, bots, hacks, mods or any software designed to modify or interfere with the Exchange or Exchange experience).</p>

                          <p><strong>Limitations of Liability</strong></p>
<p>ClearAction Continuum will not be liable for indirect, special, or consequential damages (or any loss of revenue, profits, or data) arising in connection with this Agreement or the Exchange, even if we have been advised of the possibility of such damages. Further, our aggregate liability arising with respect to this Agreement and the Exchange will not exceed the total membership fees paid or payable to you under this Agreement.</p>

                          <p><strong>Disclaimers</strong></p>
<p>We make no express or implied warranties or representations with respect to the Exchange or any products sold through the Exchange (including, without limitation, warranties of fitness, merchantability, noninfringement, or any implied warranties arising out of a course of performance, dealing, or trade usage). In addition, we make no representation that the operation of the Exchange will be uninterrupted or error-free, and we will not be liable for the consequences of any interruptions or errors.</p>

                         <p><strong>Arbitration</strong></p>
<p>Any dispute relating in any way to this Agreement (including any actual or alleged breach hereof), any transactions or activities under this Agreement or your relationship with ClearAction Continuum or any of our affiliates will be submitted to confidential arbitration, except that, to the extent you have in any manner violated or threatened to violate our intellectual property rights, we may seek injunctive or other appropriate relief in any state or federal court (and you consent to non-exclusive jurisdiction and venue in such courts) or any other court of competent jurisdiction. <br /><br />Arbitration under this agreement will be conducted under the rules then prevailing of the American Arbitration Association. The arbitrator's award will be binding and may be entered as a judgment in any court of competent jurisdiction. To the fullest extent permitted by applicable law, no arbitration under this Agreement will be joined to an arbitration involving any other party subject to this Agreement, whether through class arbitration proceedings or otherwise.</p>

                          <p><strong>Miscellaneous</strong></p>
<p>This Agreement will be governed by the laws of The United States, without reference to rules governing choice of laws. You may not assign this Agreement, by operation of law or otherwise, without our prior written consent. Subject to that restriction, this Agreement will be binding on, inure to the benefit of, and be enforceable against the parties and their respective successors and assigns. Our failure to enforce your strict performance of any provision of this Agreement will not constitute a waiver of our right to subsequently enforce such provision or any other provision of this Agreement.</p>

<p>The failure of ClearAction Continuum to exercise or enforce any right or provision of the Terms of Service will not constitute a waiver of such right or provision. The Terms of Service constitutes the entire agreement between you and ClearAction Continuum and govern your use of the Service, superseding any prior agreements between you and ClearAction Continuum (including, but not limited to, any prior versions of the Terms of Service).</p>

<p><em>Last Revised: 29 April 2019</em></p>
                            <p><strong>Questions</strong></p>
                            <p>If you have any questions about these Terms and how they relate to your participation in ClearAction Value Exchange, please email ClearAction Continuum at <a href="mailto:Success@clearaction.com">Success@clearaction.com</a> with the subject line "Membership Terms Question".</p>
                        </div>
                    </div>
                    <br />
                    <div class="form-group agree_btn text-center">
                        <label class="radio-inline">
                            <asp:CheckBox ID="chkTermandCondition" runat="server" />
                            &nbsp;&nbsp;&nbsp;I agree to these Terms and Conditions</label>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <a class="btn btn-large btn-secondary" type="submit" id="Prev4"><span>BACK</span> </a>
            <asp:LinkButton CssClass="btn btn-large btn-primary btn_save" runat="server" ID="btnSaveInformation" type="submit" OnClick="btnSaveInformation_Click"><span>SAVE</span> </asp:LinkButton>
        </div>
    </div>

<div id="Step5" style="display: none;">
        <div class="row">
            <div class="form_header col-sm-10">

                <h2>Ready</h2>

                <div class="circle circle_completed"></div>

                <div class="circle circle_completed"></div>

                <div class="circle circle_completed"></div>

                <div class="circle circle_completed"></div>
            </div>
        </div>
        <div class="row">
            <div class="form_body thankyou col-sm-6">
                <h3>Get Ready to Take Action</h3>
                <div>
                    <strong>Your personalized home page will save you lots of time every day!</strong><br /><br />

                    <ul><li>The dials on My Exchange are your quarterly learning path.</li></ul>

                    <ul><li>My Vault contains your recommended learning path resources.</li></ul>

                    <ul><li>Change your answers anytime by clicking "Profile" at the top of the page.</li></ul>

                    <ul><li>Bookmark this site on your computer and mobile devices for ready access.</li></ul>

                    <ul><li>Explore a Solve-Space<sup>TM</sup>, RSVP for an event and answer an Open Forum question.</li></ul>

                  <strong>See your personalized <em>My Exchange</em> home page and <em>My Vault</em> resources:</strong><br />
                </div>
            </div>
        </div>
        <div class="row thankyou_menu_row">
            <div class="col-sm-2">
                <div class="thankyou_menu_item text-center"><strong><a href="/Home" class="thankyou_myexchange">START</a></strong></div>
            </div>
        </div>
    </div>

<div style="display: none">

    <div id="tt-current-level-1">

        <div class="slide-tooltip tt-one">

            <p style="color: #fff !important; margin-bottom: 10px;">

                You can execute the basic requirements in your job. Tasks include individual assignments that do not require frequent team interactions.  

            </p>

        </div>

    </div>

 

    <div id="tt-current-level-2">

        <div class="slide-tooltip tt-two">

            <p style="color: #fff !important; margin-bottom: 10px;">

                You've mastered the basic requirements of the job  &#8212;  the individual assignments that don’t require frequent team interactions. And, you can say that team interactions are starting to happen more frequently.   

            </p>

        </div>

    </div>

 

    <div id="tt-current-level-3">

        <div class="slide-tooltip tt-three">

            <p style="color: #fff !important; margin-bottom: 10px;">

                You have the basic requirements of the job mastered  &#8212;  the individual assignments that don't require frequent team interactions. But you're also occasionally showing your strategic ability, and can say you've inﬂuenced others in strategic, team-oriented interactions.  

            </p>

        </div>

    </div>

 

    <div id="tt-current-level-4">

        <div class="slide-tooltip tt-four">

            <p style="color: #fff !important; margin-bottom: 10px;">

                You're executing some advanced tasks, strategically thinking and collaborating, and at times inﬂuencing others to achieve team-oriented objectives.

            </p>

            <p style="color: #fff !important; margin-bottom: 10px;">

                You’re improving and proving all of these skills.

            </p>

        </div>

    </div>

 

    <div id="tt-current-level-5">

        <div class="slide-tooltip tt-five">

            <p style="color: #fff !important; margin-bottom: 10px;">

                You're executing advanced tasks, strategically thinking and collaborating, and sometimes, even regularly inﬂuencing others to achieve team-oriented objectives, but you would like to make that happen more often.

            </p>

        </div>

    </div>

 

    <div id="tt-current-level-6">

        <div class="slide-tooltip tt-six">

            <p style="color: #fff !important; margin-bottom: 10px;">

                You’re executing advanced tasks, strategically thinking and collaborating, and regularly inﬂuencing others to achieve team-oriented objectives. You're involved in some complex assignments and gaining recognition for them.

            </p>

        </div>

    </div>

 

    <div id="tt-current-level-7">

        <div class="slide-tooltip tt-seven">

            <p style="color: #fff !important; margin-bottom: 10px;">

                You're executing advanced tasks, strategically thinking and collaborating, and often inﬂuencing others to achieve team-oriented objectives. You may have dipped your toes in expert territory &#8212; completing more complex assignments, and inﬂuencing stakeholders at your level to consensus strategies and approaches that impact the company's overall business objectives.

            </p>

        </div>

    </div>

 

    <div id="tt-current-level-8">

        <div class="slide-tooltip tt-eight">

            <p style="color: #fff !important; margin-bottom: 10px;">

                Your peers are starting to consider you an expert. You're executing complex assignments. From diverse points of view you're inﬂuencing stakeholders at your level (and sometimes above) to consensus strategies and approaches that impact the company's overall business objectives.

            </p>

        </div>

    </div>

 

    <div id="tt-current-level-9">

        <div class="slide-tooltip tt-nine">

            <p style="color: #fff !important; margin-bottom: 10px;">

                Your peers consider you an expert. You are executing complex assignments. From diverse points of view, you're inﬂuencing stakeholders at your level, and often above, to consensus strategies and approaches that impact the company's overall business objectives.

            </p>

        </div>

    </div>

 

    <div id="tt-current-level-10">

        <div class="slide-tooltip tt-ten">

            <p style="color: #fff !important; margin-bottom: 10px;">

               Peers consider you the expert. You execute complex assignments, inﬂuence and direct stakeholders at your level, and are regularly &#8212; and often &#8212; inﬂuencing those above to consensus strategies and approaches that impact the company’s overall business objectives.

            </p>

        </div>

    </div>

</div>


