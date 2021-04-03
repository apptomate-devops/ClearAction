<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="ClearAction.Modules.Login.Login" EnableViewState="true" %>
<link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.0/jquery-confirm.min.css">
<script src="//cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.0/jquery-confirm.min.js"></script>
<style type="text/css">
    .loginbutton {
        color: #ffffff;
        width: 100%;
        text-align: center;
        border-radius: 0;
        background: #4270b1;
        font-size: 16px;
        border: none;
        padding: 9.5% 0;
        font-weight: 700;
        margin: 5% 0 0;
        background: #4270b1 !important;
    }
</style>
<asp:HiddenField ID="hdnCurrentDateTime" runat="server" />
<script type="text/javascript">

    window.onbeforeunload = DisableButton;
    function DisableButton() {
        document.getElementById("<%=btnSigninSecond.ClientID %>").disabled = true;
    }
    var currentID =  <%= "'" + this.ClientID+ "'" %>;
    $(function () {

        $("#" + currentID + "_txtUsername").keypress(function (event) {

            var clckbtn = ("#" + currentID + "_btnSigninSecond");
            if (event.which == 13) {
                event.preventDefault();
                document.getElementById(clckbtn).click(); // Click on the Button
            }
        });

        $("#" + currentID + "_txtPassword").keypress(function (event) {

            var clckbtn = ("#" + currentID + "_resetpassword");
            if (event.which == 13) {
                //     alert('enter pressed: '+clckbtn );
                event.preventDefault();
                // document.getElementById(clckbtn).click(); // Click on the Button
            }
        });

    });


    function ValidateInputs(t) {
        var UserName = $.trim($("#" + currentID + "_txtUsername").val());
        var Pwd = $.trim($("#" + currentID + "_txtPassword").val());



        if (UserName == '') {
            alert('Please enter username');
            $("#" + currentID + "_txtUsername").focus();
            return false;

        }

        if (Pwd == '') {
            $.alert('Please enter password');
            $("#" + currentID + "_txtPassword").focus();
            return false;
        }
        //t.disabled = 'true';
        t.value = 'Please wait.. Sign in...';

        return true;

    }
</script>
<div id="loadingtop" data-bind="visible: loadingData" style="display: none; width: 100%; height: 100%; top: 100px; left: 0px; position: fixed; z-index: 10000; text-align: center;">
    <img src="../images/progress.gif" alt="Loading..." style="position: fixed; top: 50%; left: 50%;" />
</div>
<asp:UpdatePanel ID="uppanel" runat="server">
    <Triggers>
        <asp:PostBackTrigger ControlID="btnSigninSecond" />
    </Triggers>
    <ContentTemplate>
        <div class="login_wrapper">
            <p class="text-center">
                <img src="/Portals/0/Skins/CustomLogin/images/login/logo-CVE.png" alt="ClearAction Logo" />
                <br>
                <br>
                <br />
                Customer Value Growth Strategy<br />
                Ease of Work & Ease of Doing Business
                <br>
                <br />
            </p>
            <div class="form-group">
                <label id="errMsg" for="error" style="color: red;" runat="server" visible="false"></label>
            </div>
            <div id="divLogin">
                <asp:Panel ID="pnldefault" runat="server" DefaultButton="btnSigninSecond">
                    <div class="dnnFormMessage dnnFormValidationSummary" id="diverror" runat="server">
                        <center> <span class="dnnModMessageHeading"><strong>Login Failed</strong></span></center>
                        <br />
                        <span>Please remember that passwords are case sensitive.</span>
                    </div>
                    <div class="form-group has-feedback">
                        <label for="username">Username</label>
                        <input type="text" class="form-control" id="txtUsername" runat="server">
                        <span class="username form-control-feedback" aria-hidden="true"></span>
                    </div>
                    <div class="form-group has-feedback">
                        <label for="password">Password</label>
                        <input type="password" class="form-control" id="txtPassword" runat="server">
                        <span class="password form-control-feedback" aria-hidden="true"></span>
                    </div>
                    <asp:Button ID="btnSigninSecond" runat="server" Text="SIGN IN" CssClass="loginbutton" OnClick="btnSigninSecond_Click" OnClientClick="return ValidateInputs(this)" CausesValidation="false" />
                    <p class="text-center font-normal">
                        <br>
                        <a href="javascript:void(0);" onclick="return CA_ShowPasswordDiv(true);">Forgot Password?</a>
                    </p>
                    <p class="text-center" style="font-size: 14px;">
                        <br>
                        <strong>New to ClearAction Value Exchange? <a href="https://clearaction.com/downloads/1exchange/" target="_blank">Learn more!</a></strong>
                    </p>
                </asp:Panel>
            </div>

            <div id="dvForgot" style="display: none;">

                <div class="form-group has-feedback">
                    <label for="username">Username</label>
                    <input type="text" class="form-control" id="forgotPwd" runat="server">
                    <span class="username form-control-feedback" aria-hidden="true"></span>

                    <button class="btn btn-primary" onclick="return CA_ResetPassword(this);" id="resetpassword">Reset Password</button>
                    <p class="text-center">
                        <br>
                        <a href="javascript:void(0);" onclick="return CA_ShowPasswordDiv(false);">Back to Login</a>
                    </p>

                </div>

            </div>

        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    var CA_PortalID =<%=this.PortalId%>;
    var redURL =  <%= "'" + this.RedirectURL  + "'" %>;
    var redTabid =  <%= "'" + this.iTabId  + "'" %>;

</script>

<script src="/DesktopModules/ClearAction_SolveSpaces/Scripts/Module/Common.js"></script>
<script src="/DesktopModules/ClearAction_Login/Scripts/Module/login.js?v=1"></script>
