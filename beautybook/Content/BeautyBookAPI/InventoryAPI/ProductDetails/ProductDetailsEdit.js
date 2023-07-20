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


var getproductId = new URLSearchParams(window.location.search);
getproductId = parseInt(atob(getproductId.get('InventoryPrdIds')));
var getproductIdatob = ~~getproductId;


//appoinmentDetails edit API call in ajax methos

if (getproductIdatob == 0) {
    $('#inventoryform').show();
}

function editInventory() {
    
    if (getproductIdatob > 0) {

        $('#inventoryformloader').show();
        $('#inventoryform').hide();

        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + `/api/inventoryProduct/InventoryProduct_ById?Id=${getproductIdatob}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                debugger;
                $('#productInventoryId').val(result.Item.Id);
                $('#BillNumber').val(result.Item.BillNumber);
                $('#OfflineVendor').selectpicker('val',result.Item.OfflineVendorId);
                $('#productName').val(result.Item.ProductName);
                $('#productType').selectpicker('val', result.Item.ProductTypeId);
                $('#productBrand').selectpicker('val', result.Item.ProductBrandId);
                $('#purchaseDate').val(result.Item.PurchaseDate);
                $('#productQty').val(result.Item.ProductQty);
                $('#purchasePrice').val(result.Item.Price);
                $('#productWeight').val(result.Item.Weight);
                $('#productWeightType').selectpicker('val', result.Item.WeightTypeId);
                $('#qtyAlert').val(result.Item.LowQtyAlert);
                $('#ExpiryDate').val(result.Item.ExpiryDate);
                $("#IsExpiryDate").prop("checked", result.Item.IsExpiryDate)

                if (result.Item.IsExpiryDate == true)
                {
                    $("#IsExpiryDate_Div").show();
                }
                else {
                    $("#IsExpiryDate_Div").hide();
                }

                $('#inventoryformloader').hide();
                $('#inventoryform').show();
            }, error: function (error) {
                // Error function
                $('#inventoryformloader').hide();
                $('#inventoryform').show();
            }
        });
    }

    return false;
}

//masterProductType dropdown API call in ajax methos
function masterProductType() {

    let MasterProductType = new Object();
    MasterProductType.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/masterProductType/MasterProductType_All?search',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(MasterProductType),
        crossDomain: true,
        async: false,
        success: function (result) {

            $("#productTypeedit").html(``);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#productType').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                    `);
                }
            }

            $('.selectpicker').selectpicker("refresh");


        }, error: function (error) {
            // Error function

        }
    });
    return false;
}

//MasterProductBrand dropdown API call in ajax methos
function masterProductBrand() {
    
    let MasterProductBrand = new Object();
    MasterProductBrand.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url:  APIEndPoint + `/api/masterProductBrand/MasterProductBrand_All?search&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(MasterProductBrand),
        crossDomain: true,
        async: false,
        success: function (result) {

            $("#productBrand").html(``);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#productBrand').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                    `);
                }
            }

            $('.selectpicker').selectpicker("refresh");


        }, error: function (error) {
            // Error function

        }
    });
    return false;
}


//ViewProductBrand API call in ajax methos
function viewProductBrand() {
    
    let ViewProductBrand = new Object();
    ViewProductBrand.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/masterProductBrand/MasterProductBrand_All?search&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(ViewProductBrand),
        crossDomain: true,
        async: false,
        success: function (result) {
             
            $("#viewProductBrandArea").html(``);
            var ValidLength = 0;
            for (i = 0; i < result.Values.length; i++) {
                if (result.Values[i].SalonId > 0) {
                    ValidLength = +1;
                    $('#viewProductBrandArea').append(`
                        <div class="form-group mb-0 row align-items-center">
                            <label for="newProductName" class="col-sm-10 col-form-label">${result.Values[i].Name}</label>
                            <div class="col-sm-2">
                                <button class="btn btn-light-danger  text-danger btn-sm font-weight-semibold fs-12 border" onclick="DeleteProductDeleteswal(${result.Values[i].Id})" title="Brand Delete" type="button">
                                    <i class="bb-trash-2 text-danger fs-14"></i>
                                </button>
                            </div>
                        </div>
                    `);
                } else if (ValidLength <= 0) {
                    $('#viewProductBrandArea').append(`
                        <div class="form-group mb-0 row justify-content-center align-items-center">
                            <label class="col-form-label text-muted" style="font-size:20px;">${Langaugestore.NoRecordsFound}</label>
                        </div>
                    `);
                    break;
                }
            }

            $('.selectpicker').selectpicker("refresh");


        }, error: function (error) {
            // Error function

        }
    });
    return false;
}

// DeleteProductBrand API call in ajax method
function DeleteProductDeleteswal(ProductBrandId) {
    Swal.fire({
        title: 'Are you sure Delete this Brand ?',
        //text: "Are you sure Active this employee !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteProductBrand(ProductBrandId);
        }
    })
}

//Delete this salon custome brand
function DeleteProductBrand(ProductBrandIds) {
     
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/masterProductBrand/MasterProductBrand_Delete?Id=${ProductBrandIds}&DeletedBy=${atob(SalonId)}`,
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        processData: false,
        contentType: false,
        crossDomain: true,
        success: function (result) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: result.Message,
                showConfirmButton: false,
                timer: 3000
            })
            viewProductBrand();
            masterProductBrand();
        }, error: function (error) {
             
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
        }
    });
}

//masterProductType dropdown API call in ajax methos
function masterProductWeight() {
    
    let MasterProductWeight = new Object();
    MasterProductWeight.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/masterProductWeight/MasterProductWeight_All?search',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(MasterProductWeight),
        crossDomain: true,
        async: false,
        success: function (result) {

            $("#productWeightType").html(``);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#productWeightType').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                    `);
                }
            }

            $('.selectpicker').selectpicker("refresh");


        }, error: function (error) {
            // Error function
        }
    });
    return false;
}


//Offline vendor dropdown API call in ajax methos
function OfflineVendor() {
    
    let offlineVendor = new Object();
    offlineVendor.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/offlineVendor/OfflineVendor_All?search=&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(offlineVendor),
        crossDomain: true,
        async: false,
        success: function (result) {
            
            $("#OfflineVendor").html(``);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#OfflineVendor').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                    `);
                }
            }
            $('.selectpicker').selectpicker("refresh");
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}


//customBrandadd ajax method
function customBrandadd() {
    
    $('#AddCustomBrand').hide();
    $('#AddCustomBrandLoading').show();


    var customerBrandAdd = new Object();
    customerBrandAdd.Id = 0;
    customerBrandAdd.Name = $('#CustomBrand').val();
    customerBrandAdd.SalonId = parseInt(atob(SalonId));
    customerBrandAdd.CreatedBy = 0;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/masterProductBrand/MasterProductBrand_Upsert`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(customerBrandAdd),
        crossDomain: true,
        success: function (result) {
            
            masterProductBrand();
            viewProductBrand();
            $("#newBrandModal").modal("hide");
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                });

            }
            $('#CustomBrand').val("");
            $('#AddCustomBrand').show();
            $('#AddCustomBrandLoading').hide();
            masterProductBrand();

        }, error: function (error) {
            
            $("#newBrandModal").modal("hide");
            $('#CustomBrand').val("");
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })

            $('#AddCustomBrand').show();
            $('#AddCustomBrandLoading').hide();

        }
    });
    return false;
}