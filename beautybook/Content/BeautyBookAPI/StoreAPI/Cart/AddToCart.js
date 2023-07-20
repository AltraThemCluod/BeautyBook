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

function addTocart(productId) {
     

    var QtyCheck = ~~parseInt($('#productQty').val())

    var AddToCart = new Object();
    AddToCart.Id = 0;
    AddToCart.SalonId = parseInt(atob(salonId));
    AddToCart.ProductId = productId;
    AddToCart.Qty = parseInt(QtyCheck == 0 ? 1 : $('#productQty').val());
    AddToCart.CreatedBy = parseInt(atob(salonId));

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/addToCart/AddToCart_Upsert`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(AddToCart),
        crossDomain: true,
        success: function (result) {

            if (result.Code == 200) {
                CartProductCount();
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
            }
        }, error: function (error) {
             
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            });
        }
    });
}