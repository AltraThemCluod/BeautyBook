//setCookie
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}


//SignIn API Call In Ajax Method
function forgotpasswordEmail() {
    //$('#resendemailLoader').show();
    $('#userSignUpbtn').hide();
    $('#userSignUpbtnLoading').show();
    getUserEmail = $("#resetPasswordEmail").val();

    $.ajax({
        type: 'POST',
        //url: + APIEndPoint + `/api/users/Users_Login?Email=${getUserEmail}&PasswordHash=10&DeviceToken=10&LoginType=3&LookUpUserTypeId=2`,
        url: `${APIEndPoint}/api/users/Users_Login?Email=${getUserEmail}&PasswordHash=10&DeviceToken=10&LoginType=3&LookUpUserTypeId=2`,
        headers: { 'Content-Type': 'application/json' },
        dataType: 'json',
        // data: JSON.stringify(objgetUserId.Id),
        crossDomain: true,
        success: function (result) {
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    window.location.reload();
                }, 3000);
            }
       
            $("#userEmailAddress").val('');
            $('#userSignUpbtn').show();
            $('#userSignUpbtnLoading').hide();
            //$('#resendemailLoader').hide();

        }, error: function (error) {
            debugger;
            console.log("dasdasd", error);
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'You entered wrong email address please enter registerd email address',
                showConfirmButton: false,
                timer: 3000
            })
            $('#userSignUpbtn').show();
            $('#userSignUpbtnLoading').hide();
        }
    });
    return false;
}

