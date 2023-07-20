//setCookie
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}


//SignIn API Call In Ajax Method

function userSignIn() {
    
    $('#userSignInbtn').hide();
    $('#userSignInbtnLoading').show();

    var SignInvalue = new Object();

    SignInvalue.UserName = $("#signInEmail").val();
    SignInvalue.Password = $("#signInPassword").val();
    SignInvalue.DeviceToken = "";
    SignInvalue.LoginType = 1,
    SignInvalue.grant_type = "password";
    SignInvalue.LookUpUserTypeId = 5;

    var stringedObj = JSON.stringify(SignInvalue);
    debugger;
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/token',
        headers: { 'Content-Type': 'application/json' },
        data: SignInvalue,
        crossDomain: true,
        success: function (result) {
             
            setCookie("DeviceToken&Type", result.token_type + ' ' + result.access_token, 30);
            getUsername();
            $('#userSignInbtn').show();
            $('#userSignInbtnLoading').hide();

        }, error: function (error) {
            
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.error_description,
                showConfirmButton: false,
                timer: 3000
            })
            $('#userSignInbtn').show();
            $('#userSignInbtnLoading').hide();
        }
    });
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

function getUsername() {
    var DeviceTokenNumber = getCookie("DeviceToken&Type");
     
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/GetUser_ById',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
             

            if (result.Item.LookUpUserTypeId != 5) {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: 'Email and password is incorrect !',
                    showConfirmButton: false,
                    timer: 3000
                })
            } else {
                setCookie("VendorId", btoa(result.Item.Id), 30);
                setCookie("UserProfileUrl", result.Item.ProfileUrl, 30);
                setCookie("UserName", result.Item.FirstName + ' ' + result.Item.SecondName, 30);
                setCookie("UserEmail", btoa(result.Item.Email), 30);
                setCookie("UserTypeId", result.Item.LookUpUserTypeId, 30);
                setCookie("IsAdminApproved", result.Item.IsAdminApproved, 30);
                 
                if (result.Code == 200) {
                    
                    if (result.Item.IsEmailVerified == true) {
                        window.location = "/Home/Index";
                    }
                    else {
                        if (result.Item.IsAdminApproved == true) {
                            window.location = "/Authentication/EmailVerificationCode";

                        } else {
                            if (result.Item.Dob != null) {
                                window.location = "/ApplicationForm/ApprovelProcess";
                            } else {
                                window.location = "/ApplicationForm/Index";
                            }
                        }
                    }
                }
            }

            
        }, error: function (error) {
            //Error function
        }

    });
    return false;
}