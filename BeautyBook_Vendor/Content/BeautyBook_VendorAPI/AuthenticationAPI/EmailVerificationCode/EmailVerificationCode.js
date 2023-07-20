//setCookie
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

//getCookie
function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";

}

var DeviceTokenNumber = getCookie("DeviceToken&Type");
var UserEmail = getCookie("UserEmail");

//getemail in token and store input field
$('#signInEmail').val(atob(UserEmail));

//resendverifyCode verifyCode
var ResendFlag = false;
function resendverifyCode(resendFlag) {
    ResendFlag = resendFlag;
    $('#resendCode').hide();
    $('#resendCodeloading').show();
    sendverifyCodeemail();
}

//sendverifyCode verifyCode
function sendverifyCode() {
    
    $('#EmailVerifycode').val('');
    $('#sendVerifycode').hide();
    $('#sendVerifycodeloading').show();
    sendverifyCodeemail();
}

function sendverifyCodeemail() {
    
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_VerifyCode',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            if (result.Code == 200) {
                
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Verification code sent successfully !',
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    $('#emailVerifycode').show();
                    $('#sendVerifycode').hide();
                    $('#Cancelbtn').show();
                    $('#Skipbtn').hide();
                    $('#Verifybtn').show();
                }, 3000);
            }

            if (ResendFlag == true) {
                $('#sendVerifycode').hide();
            } else {
                $('#sendVerifycode').show();
            }
            
            $('#sendVerifycodeloading').hide();

            $('#resendCode').show();
            $('#resendCodeloading').hide();

        }, error: function (error) {
            
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })

            $('#sendVerifycode').hide();
            $('#sendVerifycodeloading').show();

            $('#resendCode').show();
            $('#resendCodeloading').hide();

        }

    });
    return false;
}

function Skip() {
     
    setCookie("IsUserSkip", 'Yes', 30);
    window.location = "/Home/Index";
}

function verifyCode() {

    $('#Verifybtn').hide();
    $('#Verifybtnloading').show();
    
    var EmailVerificationCode = $("#EmailVerifycode").val();
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_VerifyEmail?EmailVerificationCode='+EmailVerificationCode,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            if (result.Code == 200) {
                
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'User email verified successfully !',
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    window.location = "/Home/Index";
                }, 3000);
            }

            $('#Verifybtn').show();
            $('#Verifybtnloading').hide();

        }, error: function (error) {
            
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })

            $('#Verifybtn').show();
            $('#Verifybtnloading').hide();

        }

    });
    return false;
}