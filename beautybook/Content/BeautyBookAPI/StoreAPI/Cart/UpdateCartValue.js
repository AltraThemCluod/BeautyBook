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
var salonId = getCookie("SalonId");



function updateCartItems() {
    var productCartStr = $("input[id='productCartId']").map(function () { return $(this).val(); }).get().toString();
    var productQtyStr = $("input[id='productQty']").map(function () { return $(this).val(); }).get().toString();

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/addToCart/AddToCart_UpdateQty?ProductIds=${productCartStr}&Qtys=${productQtyStr}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        success: function (result) {
            CartProduct();

            Swal.fire({
                position: 'center',
                icon: 'success',
                title: result.Message,
                showConfirmButton: false,
                timer: 3000
            })
        }, error: function (error) {
            //Error function
        }

    })
}
