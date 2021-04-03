<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="ClearAction.Modules.ReportIssue.View" %>
<link type="text/css" href='<%=ModulePath %>module.css' />
<script type="text/javascript" src='<%=ModulePath%>js/module.js'></script>
<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/Module/Common.js"></script>
<style type="text/css">
    input[type="radio"] {
        -webkit-appearance: radio;
    }

    .acceptbutton {
        padding: 35px 30px;
        border: none;
        border-radius: 0px;
        background: #FFB12A;
        margin: 11px 7px;
        color: #000;
        font-weight: bold;
    }

    .declinebutton {
        padding: 35px 30px;
        border: none;
        border-radius: 0px;
        background: #bbbbbb;
        font-size: 16px;
        font-weight: 600;
        margin: 11px 7px;
        color: #000;
    }
</style>
<%--<a href="javascript:void(0);"  onclick="return RI_ShowIssue('forum','New Test account in dev');"  >Report an issue</a>
<a href="javascript:void(0);"  onclick="return RI_ShowIssue('blog','my new blog');"  >Report blog issue</a>--%>

<div id="dvriCont">
    <div id="dvStep1" class="ridiv" style="display: none;">
        <h2>Report an Issue</h2>
        <br />
        <div style="font-size: 15px;">
            See something inappropriate here? Thank you for letting us
know. We'll take a look.
        </div>
        <br />
        <div>
            <label style="font-size: 16px;">What kind of issue is it?</label>

            <div>
                <input type="radio" name="issueType" id="issueType_1" value="Spam" checked="checked">
                <label for="issueType_1" class="rispn">Spam</label><br>
                <input type="radio" name="issueType" id="issueType_2" value="Malware or virus"><label for="issueType_2" class="rispn">Malware or virus</label><br>
                <input type="radio" name="issueType" id="issueType_3" value="Copyright violation"><label for="issueType_3" class="rispn">Copyright violation</label><br>
                <input type="radio" name="issueType" id="issueType_4" value="Illegal content"><label for="issueType_4" class="rispn">Illegal content</label><br>
                <input type="radio" name="issueType" id="issueType_5" value="Drop-down box is not working properly"><label for="issueType_5" class="rispn">Drop-down box is not working properly</label><br>
                <input type="radio" name="issueType" id="issueType_6" value="Other inappropriate content"><label for="issueType_6" class="rispn">Other inappropriate content</label><br>
            </div>
            <br />
            <br />
            <button class="acceptbutton" onclick="return RI_ShowNext();">NEXT</button>
        </div>
    </div>

    <div id="dvStep2" style="display: none;">

        <h2>
            <label id="lblriTitle" for="rpttitle">Report an issue: #type#</label></h2>

        <br />
        <br />
        Use this form to report issues to ClearAction Value
Exchange.<br />
        <br />

        <label>Link</label><br />
        <input type="text" class="form-control" id="txtrilnkname" style="width: 80%;">


        <label>Please describe the issue you see</label><br />
        <%--<input type="text" class="form-control" id="txtriDescold" >--%>
        <textarea name="txtriDesc" id="txtriDesc" cols="80" rows="5" style="width: 80%;"></textarea>

        <br />
        <br />
        <span>If you appear in the content or need more help, please provide your email address and we will reach out if necessary.</span>
        <br />
        <br />
        <label>Your email address</label><br />
        <input type="text" class="form-control" id="txtriEmail" style="width: 80%;">

        <br />
        <br />
        <button class="declinebutton" onclick="return RI_ShowStep1();">BACK</button>
        <button class="acceptbutton" onclick="return RI_SubmitIssue();">SEND</button>
    </div>
    <div id="dvrithanks" style="padding: 15px; display: none;">
        <span style="font-size: 20px; color: green;">Thank you for reporting the issue. We will review it and get back to you soon.</span>

    </div>
</div>


