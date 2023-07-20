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
var SalonId = getCookie("SalonId");

//changeStatus modal open function call
var ProductNames = "";
var ProductQtys = 0;
function SubtractInventory(Pname, Pqty) {
     
    $('#subtractInventoryModal').modal('show');
    ProductNames = Pname;
    ProductQtys = atob(Pqty);
    var productQty = parseInt(atob(Pqty));
    
    $('#productSubtractQty').val(productQty);
}

//updateQty api function call
function SavePrdInventoryQty() {

     

    $('#SaveInventoryQty').hide();
    $('#SaveInventoryQtyloading').show();

    var ProductNamesSplit = ProductNames.split(',');

    var SaveInventoryQtyObj = new Object();
    SaveInventoryQtyObj.ProductName = $.trim(atob(ProductNamesSplit[0]));
    SaveInventoryQtyObj.ProductQty = parseInt($('#productSubtractQty').val());
    SaveInventoryQtyObj.ProductTypeId = parseInt(atob(ProductNamesSplit[1]));
    SaveInventoryQtyObj.ProductBrandId = parseInt(atob(ProductNamesSplit[2]));
    SaveInventoryQtyObj.Weight = parseInt(atob(ProductNamesSplit[3]));
    SaveInventoryQtyObj.WeightTypeId = parseInt(atob(ProductNamesSplit[4]));
    SaveInventoryQtyObj.SalonId = parseInt(atob(SalonId));
    SaveInventoryQtyObj.UpdatedBy = parseInt(atob(SalonId));
    
    $.ajax({
        type: 'POST',
        url:  APIEndPoint + `/api/inventoryProduct/InventoryProduct_UpdateQty`,
        headers: { "Authorization": '' + DeviceTokenNumber + '', "Content-Type": "application/json" },
        dataType: 'json',
        data: JSON.stringify(SaveInventoryQtyObj),
        crossDomain: true,
        success: function (result) {
             
            inventoryList.init();
            $('#subtractInventoryModal').modal('hide');
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
            }

            $('#SaveInventoryQty').show();
            $('#SaveInventoryQtyloading').hide();

        }, error: function (error) {
             
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
            $('#SaveInventoryQty').show();
            $('#SaveInventoryQtyloading').hide();
        }
    });
}



var ProductDetailsvar = '';

function lowQtyAlert(ProductDetails) {
    ProductDetailsvar = ProductDetails;
    $('#productLowQtyAlert').val('');
    $('#LowQtyAlertModal').modal('show');
}

//Low Qty alert api function call
function lowQtyAlertconfirm() {

     

    $('#LowQtyAlertconfirm').hide();
    $('#LowQtyAlertconfirmloading').show();

    var ProductDetailsSplit = ProductDetailsvar.split(',')
     
    var SaveInventoryQtyObj = new Object();
    SaveInventoryQtyObj.ProductName = $.trim(atob(ProductDetailsSplit[0]));
    SaveInventoryQtyObj.LowQtyAlert = parseInt($('#productLowQtyAlert').val());
    SaveInventoryQtyObj.ProductTypeId = atob(ProductDetailsSplit[1]);
    SaveInventoryQtyObj.ProductBrandId = atob(ProductDetailsSplit[2]);
    SaveInventoryQtyObj.Weight = atob(ProductDetailsSplit[3]);
    SaveInventoryQtyObj.WeightTypeId = atob(ProductDetailsSplit[4]);
    SaveInventoryQtyObj.SalonId = parseInt(atob(SalonId));
     
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/inventoryProduct/InventoryProduct_LowQtyAlert`,
        headers: { "Authorization": '' + DeviceTokenNumber + '', "Content-Type": "application/json" },
        dataType: 'json',
        data: JSON.stringify(SaveInventoryQtyObj),
        crossDomain: true,
        success: function (result) {
             
            $('#LowQtyAlertModal').modal('hide');
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    inventoryList.init();
                }, 3000);
            }

            $('#LowQtyAlertconfirm').show();
            $('#LowQtyAlertconfirmloading').hide();

        }, error: function (error) {
            $('#LowQtyAlertModal').modal('hide');
             
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
            $('#LowQtyAlertconfirm').show();
            $('#LowQtyAlertconfirmloading').hide();
        }
    });
}

