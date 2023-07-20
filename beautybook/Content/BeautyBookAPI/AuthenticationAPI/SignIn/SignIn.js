//setCookie
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function CheckUserType(utype) {
    $("#signInUserType").val(utype);
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
    SignInvalue.LookUpUserTypeId = 2;

    var stringedObj = JSON.stringify(SignInvalue);

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

function EmployeeSignIn() {

    $('#userSignInbtn').hide();
    $('#userSignInbtnLoading').show();

    var SignInvalue = new Object();

    SignInvalue.UserName = $("#signInEmail").val();
    SignInvalue.Password = $("#signInPassword").val();
    SignInvalue.DeviceToken = "";
    SignInvalue.LoginType = 1,
    SignInvalue.grant_type = "password";
    SignInvalue.LookUpUserTypeId = 3;

    var stringedObj = JSON.stringify(SignInvalue);

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
            
            console.log(result);
            if (result.Item.LookUpUserTypeId != 2 && result.Item.LookUpUserTypeId != 3) {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: 'Email and password is incorrect !',
                    showConfirmButton: false,
                    timer: 3000
                })
            } else {
                setCookie("DeviceToken", DeviceTokenNumber, 30);
                setCookie("UserId", btoa(result.Item.Id), 30);
                setCookie("UserProfileUrl", result.Item.ProfileUrl, 30);
                setCookie("UserName", result.Item.FirstName + ' ' + result.Item.SecondName, 30);
                setCookie("UserEmail", btoa(result.Item.Email), 30);
                setCookie("UserTypeId", result.Item.LookUpUserTypeId, 30);
                //setCookie("EmployeePermissionData", result.Item.EmployeePermissionData, 30);

                if (result.Code == 200) {
                    setCookieData(
                        result,
                        result.Item.LookUpUserTypeId,
                        result.Item.SalonId,
                        result.Item.SalonName,
                        result.Item.SalonLogoUrl
                    );
                }
            }
           
        }, error: function (error) {
            //Error function
        }

    });
    return false;
}


function setCookieData(response, LookUpUserTypeId, SalonId, SalonName, SalonLogoUrl) {
    $.ajax({
        type: 'POST',
        url: '/SetAuthCookie/SetAuthTokenCookie',
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            if (result == true) {
                if (response.Item.IsEmailVerified == true) {
                    window.location = "/Salons/SalonsDetails";
                }
                else {

                    if (LookUpUserTypeId == 3) {
                        getSalons(SalonId, SalonName, SalonLogoUrl);
                    }
                    else {
                        window.location = "/Authentication/EmailVerificationCode";
                    }
                }
            }
        }
    });
}

function getSalons(salonId, salonName, salonsLogourl) {
    
    setCookie("SalonId", btoa(salonId), 30);
    setCookie("SalonName", salonName, 30);
    setCookie("SalonsLogoUrl", salonsLogourl, 30);

    window.location = "/Home/Index";
}