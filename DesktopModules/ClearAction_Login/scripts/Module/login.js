//jQuery(document).ajaxStart(function () {
//    try {
//        // showing a modal
//        $("#loadingtop").modal();

//        var i = 0;
//        var timeout = 750;

//        (function progressbar() {
//            i++;
//            if (i < 1000) {
//                // some code to make the progress bar move in a loop with a timeout to 
//                // control the speed of the bar

//                setTimeout(progressbar, timeout);
//            }
//        }
//        )();
//    }
//    catch (err) {
//        alert(err.message);
//    }
//});
//jQuery(document).ajaxStop(function () {
//    // hide the progress bar
//    $("#loadingtop").modal('hide');
//});
//Perform user login
function CA_LoginUser() {

    var UserName = $.trim($("#" + currentID + "_txtUsername").val());
    var Pwd = $.trim($("#" + currentID + "_txtPassword").val());



    if (UserName == '') {
        $.dialog('Please enter username');
        $("#" + currentID + "_txtUsername").focus();
        return false;

    }

    if (Pwd == '') {
        alert('Please enter password');
        $("#" + currentID + "_txtPassword").focus();
        return false;
    }


    var _URL = "/DesktopModules/ClearAction_Login/RequestHandler.asmx/UserLogin";

    var _data = "{username:'" + unescape(UserName) + "',password:'" + unescape(Pwd) + "',portalID:'" + unescape(CA_PortalID) + "'}";



    $.ajax({
        type: "POST",
        url: _URL,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: _data,
        success: function (res) {

            if (res.d == 'failure') {
                LoginFailed();

            }
            else if (res.d == "success") {
                window.location.href = redURL;

            }


        },
        error:
            function (xhr) {
                LoginFailed();

            }

    });


}

function LoginFailed() {

    $.confirm({
        title: 'Login Failed',
        content: 'Please remember that passwords are case sensitive.',
        type: 'blue',
        typeAnimated: false,
        buttons: {
            tryAgain: {
                text: 'Try again',

                action: function () {

                    $("#" + currentID + "_txtPassword").focus();
                }
            },
            close: function () {
            }
        }
    });
}

function CA_OnUserLogin(d) {
    if (d == "success") {
        window.location.href = redURL;
    }
    else {
        jQuery("#loadingtop").hide();
        $("#errMsg").text('Login Failed. Please remember that passwords are case sensitive.');
        $("#errMsg").show();
        return false;
    }
}
function CA_OnUserLoginfailure(d) {

    jQuery("#loadingtop").hide();
    $("#errMsg").text('Login Failed. Please remember that passwords are case sensitive.');
    $("#errMsg").show();
    $("#txtUsername").focus();

    return false;
}


function CA_ShowPasswordDiv(show) {
   
    jQuery("#loadingtop").hide();
   
    var uname = $("#" + currentID + "_txtUsername").val();
    fnHideError();
   
    if (show) {
        $("#" + currentID + "_forgotPwd").val(uname);
        jQuery("#divLogin").hide();
        jQuery("#dvForgot").show();
    }
    else {
        
        jQuery("#divLogin").show();
        jQuery("#dvForgot").hide();
    }

}
//Reset password

function CA_ResetPassword(t) {


    debugger;
    //
    var UserName = $.trim($("#" + currentID + "_forgotPwd").val());

    if (UserName == '') {
        alert('Please enter username');
        $("#" + currentID + "_forgotPwd").focus();
        return false;
    }
    $("#loadingtop").show();
    t.innerHTML =  "Please wait...";

    var _URL = "/DesktopModules/ClearAction_Login/RequestHandler.asmx/ResetPassword";

    var _data = "{username:'" + unescape(UserName) + "',portalID:'" + unescape(CA_PortalID) + "'}";

   

    CA_MakeRequest(_URL, _data, CA_OnResetPassword, CA_OnResetPassword);

    t.innerHTML = "Reset Password";
   
    return false;
}

function CA_OnResetPassword(d) {

   
   
    
  console.log(d.responseText);
    if (d == "success") {
       
        $("#" + currentID + "_forgotPwd").val('');
       
        $.dialog("Email has been sent to you to reset your password.", "success");
        return false;
      //  $("#" + currentID + "_resetpassword").innerHTML = 'Reset Password';
      //  $("#" + currentID + "_resetpassword").innerHTML = "Please wait...";
    }
    else {
        //   $("#errMsg").text('');
        $.dialog("We are not able to identify your account. Please do check entered username and try again.");

        fnShowError(true);
        return false;

    }
    $("#" + currentID + "_forgotPwd").val('');
    $("#loadingtop").hide();

}


function PasswordReset() {


    $.dialog({
        title: 'Success',
        content: 'If the details entered were correct, you should receive an email message shortly with a link to reset your password.',
    });

}


function fnShowError(isError) {
    if (isError)
        $("#errMsg").css("color", "red");
    else
        $("#errMsg").css("color", "limegreen");

    $("#errMsg").show();
}

function fnHideError() {

    $("#errMsg").hide();
    $("#" + currentID + "_diverror").hide();
}
