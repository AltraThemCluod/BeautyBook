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
var userId = getCookie("UserId");
var salonId = getCookie("SalonId");

//checkAuthenticationApi function call
function checkAuthenticationApi() {
    debugger;
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_ById?Id=' + atob(userId) + '',
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



function CartProductCount() {

    var cartProduct = new Object();
    cartProduct.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/addToCart/AddToCart_All?search&SalonId=${atob(salonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(cartProduct),
        crossDomain: true,
        success: function (result) {
            if (result.Values != "" && result.Values != null) {
                $("#cartProductcount").text(result.Values.length);
            } else {
                $("#cartProductcount").text("0");
            }
           
        }, error: function (error) {
            
        }
    });
}

function WishlistProductcount() {

    var wishlistProduct = new Object();
    wishlistProduct.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/productWishlist/ProductWishlist_All?search&SalonId=${atob(salonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(wishlistProduct),
        crossDomain: true,
        success: function (result) {
            if (result.Values != "" && result.Values != null) {
                $("#wishlistProductcount").text(result.Values.length);
            } else {
                $("#wishlistProductcount").text("0");
            }

        }, error: function (error) {

        }
    });
}