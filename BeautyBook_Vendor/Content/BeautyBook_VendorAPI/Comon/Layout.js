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
var UserId = getCookie("UserId");

//checkAuthenticationApi function call
function checkAuthenticationApi() {
    debugger;
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_ById?Id=' + atob(UserId) + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            
        }, error: function (error) {
            if (error.status == 401) {
                userLogout();
            }
        }
    });
    return false;
}