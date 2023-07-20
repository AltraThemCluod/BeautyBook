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

var getUserId = new URLSearchParams(window.location.search);
getUserId = parseInt(atob(getUserId.get('Id')));

function resetpasswordEmail() {
    
    $('#saveresetpasswordbtn').hide();
    $('#saveresetpasswordloading').show();
    var NewPassword = $("#userPassWord").val();
    var ConfirmPassword = $("#repeatPassword").val();

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + "/api/users/Users_ResetPassword?Id=" + getUserId + "&OldPassword=" + null + "&NewPassword=" + NewPassword + "&ConfirmPassword=" + ConfirmPassword + "&Type=" + 0,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
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
                    $('#saveresetpasswordbtn').show();
                    $('#saveresetpasswordloading').hide();
                    window.location = "/Authentication/SignIn";
                }, 3000);
            }
        }, error: function (error) {
            
            if (error.responseJSON.Code == 403) {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: error.responseJSON.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
            }
            $('#saveresetpasswordbtn').show();
            $('#saveresetpasswordloading').hide();

        }
    });
    return false;
}