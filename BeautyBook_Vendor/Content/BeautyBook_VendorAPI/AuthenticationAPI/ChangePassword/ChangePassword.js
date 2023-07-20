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
var UserId = getCookie("UserId");
var SalonId = getCookie("SalonId");

//ChangePassword API call in ajax method
function ChangePassword() {
    
    $('#saveChangespassword').hide();
    $('#saveChangepasswordloading').show();

    var OldPassword = $("#userOldpassword").val();
    var NewPassword = $("#userNewpassword").val();
    var ConfirmPassword = $("#userConfirmpassword").val();

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_ResetPassword?Id=' + atob(UserId) + '&OldPassword=' + OldPassword + '&NewPassword=' + NewPassword + '&ConfirmPassword=' + ConfirmPassword + '&Type=' + 1 +'',
        headers: { 'Content-Type': 'application/json'},
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
                    window.location = "/Authentication/SignIn";
                }, 3000);
            }
           
            $('#saveChangespassword').show();
            $('#saveChangepasswordloading').hide();

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

            $('#saveChangespassword').show();
            $('#saveChangepasswordloading').hide();

        }
    });
    return false;
}