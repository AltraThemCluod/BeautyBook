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

//product image name get
function productImageUpload() {

    var fake_path = $('#productImage').val();
    $('#uploadImagename').text(fake_path.split("\\").pop());
}
 
//saveProduct product function call
function saveInventory() {
    $('#saveBtn').hide();
    $('#saveBtnloading').show();

    debugger;

    if ($('#AddOfflineInventory').val() == "true") {
        var formData = new FormData();
        formData.append("Id", parseInt($('#productInventoryId').val()));
        formData.append("SalonId", parseInt(atob(SalonId)));
        formData.append("ProductName", $('#productNameOffline').val());
        formData.append("ProductTypeId", parseInt($('#productTypeOffline').val()));
        formData.append("ProductBrandId", parseInt($('#productBrandOffline').val()));
        formData.append("PurchaseDate", $('#purchaseDateOffline').val());
        formData.append("ProductQty", parseInt($('#productQtyOffline').val()));
        formData.append("Price", parseInt($('#purchasePriceOffline').val()));
        formData.append("Weight", parseInt($('#productWeightOffline').val()));
        formData.append("WeightTypeId", parseInt($('#productWeightTypeoffline').val()));
        formData.append("PurchaseVia", 25);
        formData.append("BillNumber", $('#BillNumberOffline').val());
        formData.append("OfflineVendorId", $('#OfflineVendor').val());
    } else {
        var formData = new FormData();
        var ProductImage = document.getElementById("productImage");

        formData.append("ProductImage", ProductImage.files[0]);
        formData.append("Id", parseInt($('#productInventoryId').val()));
        formData.append("SalonId", parseInt(atob(SalonId)));
        formData.append("ProductName", $('#productName').val());
        formData.append("ProductTypeId", $('#productType').val() == "" || $('#productType').val() == null ? 0 : parseInt($('#productType').val()));
        formData.append("ProductBrandId", $('#productBrand').val() == "" || $('#productBrand').val() == null ? 0 : parseInt($('#productBrand').val()));
        formData.append("PurchaseDate", $('#purchaseDate').val());
        formData.append("ProductQty", parseInt($('#productQty').val()));
        formData.append("Price", parseInt($('#purchasePrice').val()));
        formData.append("Weight", parseInt($('#productWeight').val()));
        formData.append("WeightTypeId", $('#productWeightType').val() == "" || $('#productWeightType').val() == null ? 0 : parseInt($('#productWeightType').val()));
        formData.append("LowQtyAlert", 0);
        formData.append("PurchaseVia", 25);
        formData.append("BillNumber", $('#BillNumber').val());
        formData.append("OfflineVendorId", $('#OfflineVendor').val());
        formData.append("ExpiryDate", $('#ExpiryDate').val());
        formData.append("IsExpiryDate", $("#IsExpiryDate").prop("checked"));
    }

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/inventoryProduct/InventoryProduct_Upsert`,
        headers: { "Authorization": '' + DeviceTokenNumber + ''},
        processData: false,
        contentType: false,
        dataType: 'json',
        data: formData,
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
            }
            setTimeout(function () {
                window.location = "/InventoryProduct/ManageInventory";
            }, 3000);

            $('#saveBtn').show();
            $('#saveBtnloading').hide();

        }, error: function (error) {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
            $('#saveBtn').show();
            $('#saveBtnloading').hide();
        }
    });
    return false;
}

function saveOfflineInventory() {
    $('#saveBtn').hide();
    $('#saveBtnloading').show();
     
    var formData = new FormData();
    //var ProductImageOffline = document.getElementById("productImageOffline");

    //formData.append("ProductImage", ProductImageOffline.files[0]);
    formData.append("Id", parseInt($('#productInventoryId').val()));
    formData.append("SalonId", parseInt(atob(SalonId)));
    formData.append("ProductName", $('#productNameOffline').val());
    formData.append("ProductTypeId", $('#productTypeOffline').val() == "" || $('#productTypeOffline').val() == null ? 0 : parseInt($('#productTypeOffline').val()));
    formData.append("ProductBrandId", $('#productBrandOffline').val() == "" || $('#productBrandOffline').val() == null ? 0 : parseInt($('#productBrandOffline').val()));
    formData.append("PurchaseDate", $('#purchaseDateOffline').val());
    formData.append("ProductQty", parseInt($('#productQtyOffline').val()));
    formData.append("Price", parseInt($('#purchasePriceOffline').val()));
    formData.append("Weight", parseInt($('#productWeightoffline').val()));
    formData.append("WeightTypeId", $('#productWeightTypeoffline').val() == "" || $('#productWeightTypeoffline').val() == null ? 0 : parseInt($('#productWeightTypeoffline').val()));
    formData.append("PurchaseVia", 25);
    formData.append("BillNumber", $('#BillNumber').val());
    formData.append("LowQtyAlert", parseInt($('#qtyAlertOffline').val()));
    formData.append("OfflineVendorId", $('#OfflineVendor').val());
    formData.append("ExpiryDate", $('#ExpiryDate').val());

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/inventoryProduct/InventoryProduct_Upsert`,
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        processData: false,
        contentType: false,
        dataType: 'json',
        data: formData,
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
            }

            $('#purchaseDateoffline').val('');
            $('#productQtyOffline').val('');
            $('#purchasePriceOffline').val('');
            $('#productWeightoffline').val('');
            $('#productWeightTypeoffline').selectpicker('val', null);
            $('#BillNumber').val('');
            $('#OfflineVendor').selectpicker('val', null);
            setTimeout(function () {
                window.location = "/InventoryProduct/ManageInventory";
            }, 3000);

            $('#saveBtn').show();
            $('#saveBtnloading').hide();

        }, error: function (error) {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
            $('#saveBtn').show();
            $('#saveBtnloading').hide();
        }
    });
    return false;
}




//Create Offline vendor
function CreateOfflineVendor() {
     
    $('#CreateVendor').hide();
    $('#CreateVendorLoading').show();

    var createOfflineVendor = new Object();
    createOfflineVendor.Id = 0;
    createOfflineVendor.SalonId = atob(SalonId);
    createOfflineVendor.Name = $("#VendorName").val();
    createOfflineVendor.PhoneNumber = $("#PhoneNumber").val();
    createOfflineVendor.Email = $("#Email").val();
    createOfflineVendor.CreatedBy = atob(SalonId);

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/offlineVendor/OfflineVendor_Upsert',
        headers: { "Authorization": '' + DeviceTokenNumber + '','Content-Type': 'application/json' },
        dataType: 'json',
        data: JSON.stringify(createOfflineVendor),
        crossDomain: true,
        success: function (result) {
             
            OfflineVendor();
            $('#CreateNewOfflineVendor').modal('hide');
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
            }

            $('#CreateVendor').show();
            $('#VendorName').val('');
            $('#PhoneNumber').val('');
            $('#Email').val('');
            $('#CreateVendorLoading').hide();

        }, error: function (error) {

            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })

            $('#CreateVendor').show();
            $('#CreateVendorLoading').hide();
        }
    });
    return false;
}