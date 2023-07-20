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
var VendorId = getCookie("UserId");

//updateQty modal open function
var ProductId = 0;
function updateQty(productId) {
     
    ProductId = productId;
    $('#updateQtyModal').modal('show');
    productQtyget();
}

//changeStatus modal open function call
function productQtyget() {
     
    if (ProductId > 0) {
        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/product/Product_ById?Id=' + ProductId + '',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                 
                $('#productQty').val(result.Item.ProductQty);
            }, error: function (error) {
                 
                //Error function
            }
        });
    }
    return false;
}

//updateQty api function call
function updateQtyfun() {

    $('#saveQtybtn').hide();
    $('#saveQtybtnloading').show();

    var productQtynumber = $('#productQty').val();
     
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + `/api/product/Product_UpdateQty?Id=${ProductId}&ProductQty=${productQtynumber}&UpdatedBy=${atob(VendorId)}`,
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
             
            $('#updateQtyModal').modal('hide');
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    productList.init();
                }, 3000);
            }

            $('#saveQtybtn').show();
            $('#saveQtybtnloading').hide();

        }, error: function (error) {
             
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
            $('#saveQtybtn').show();
            $('#saveQtybtnloading').hide();
        }
    });
    return false;
}
