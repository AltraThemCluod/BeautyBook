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
var vendorId = getCookie("VendorId");

function GetVendorNewOrderCount() {
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `api/orderProducts/SalonOwnerOrdersCount_VendorId?VendorId=${atob(vendorId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            console.log('GetVendorNewOrderCount', result);

            if (result.Item != null && result.Item != "") {
                $("#VendorNewOrder").text(result.Item.TotalNewOrder);
            }
        }, error: function (error) {
            //Error function
        }
    });
}