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

//profile upload image name get
function profileUpload() {
    var fake_path = $('#productImage').val();
    $('#uploadImagename').text(fake_path.split("\\").pop());
}

//saveProduct product function call
function saveProduct() {
    debugger;
    $('#saveBtn').hide();
    $('#saveBtnloading').show();

    var HighlightLabelStr = $("input[id='HighlightLabel']").map(function () { return $(this).val(); }).get().toString();
    var HighlightDiscriptionStr = $("input[id='HighlightDiscription']").map(function () { return $(this).val(); }).get().toString();
    var SpecificationsHighlightLabelStr = $("input[id='SpecificationsHighlightLabel']").map(function () { return $(this).val(); }).get().toString();
    var SpecificationsHighlightDiscriptionStr = $("input[id='SpecificationsHighlightDiscription']").map(function () { return $(this).val(); }).get().toString();
    var ServiceHighlightDiscriptionStr = $("input[id='ServiceHighlightDiscription']").map(function () { return $(this).val(); }).get().toString();

    //var productInformationContent = btoa($('#productInformation').summernote('code'));
    //var returnPolicyContent = btoa($('#returnPolicy').summernote('code'));

    var productInformationContent = btoa(unescape(encodeURIComponent($('#productInformation').summernote('code'))));
    var returnPolicyContent = btoa(unescape(encodeURIComponent($('#returnPolicy').summernote('code'))));
   
    var formData = new FormData();

    formData.append("Id", $('#productId').val());
    formData.append("VendorId", atob(VendorId));
    formData.append("ProductName", $('#productName').val());
    formData.append("ProductTypeId", $('#productType').val() == "" || $('#productType').val() == null ? 0 : $('#productType').val());
    formData.append("ProductCategoryId", $('#productCategory').val() == "" || $('#productCategory').val() == null ? 0 : $('#productCategory').val());
    formData.append("ProductBrandId", $('#productBrand').val() == "" || $('#productBrand').val() == null ? 0 : $('#productBrand').val());
    formData.append("ProductQty", $('#productQty').val());
    formData.append("Price", $('#purchasePrice').val());
    formData.append("Weight", $('#productWeight').val());
    formData.append("ExpiryDate", $('#ExpiryDate').val());
    formData.append("IsExpiryDate", $("#IsExpiryDate").prop("checked"));
    formData.append("WeightTypeId", $('#productWeightType').val() == "" || $('#productWeightType').val() == null ? 0 : $('#productWeightType').val());
    formData.append("LowQtyAlert", $('#qtyAlert').val());
    formData.append("HighlightsLabel", HighlightLabelStr);
    formData.append("HighlightsDiscription", HighlightDiscriptionStr);
    formData.append("SpecificationsHighlightsLabel", SpecificationsHighlightLabelStr);
    formData.append("SpecificationsHighlightsDiscription", SpecificationsHighlightDiscriptionStr);
    formData.append("ServiceHighlightsDiscription", ServiceHighlightDiscriptionStr);
    formData.append("ProductInformation", productInformationContent);
    formData.append("ReturnPolicy", returnPolicyContent);
     
    formData.append("ProductTax", $('#producttax').val());

    let ProductImageIds = [];
    let ProductImageIdArr = [];
     
    if ($('#productImageId').val() != "") {
        ProductImageIds = $('#productImageId').val().split(',')
    }
    if ($("#productImagesUrl1")[0].files[0] != null && $("#productImagesUrl1")[0].files[0] != undefined) {
        if (ProductImageIds[0] != undefined && ProductImageIds[0] != null) {
            ProductImageIdArr.push(ProductImageIds[0]);
        }
        formData.append("productImagesUrl1", $("#productImagesUrl1")[0].files[0]);

    }
    if ($("#productImagesUrl2")[0].files[0] != null && $("#productImagesUrl2")[0].files[0] != undefined) {
        if (ProductImageIds[1] != undefined && ProductImageIds[1] != null) {
            ProductImageIdArr.push(ProductImageIds[1]);
        }
        formData.append("productImagesUrl2", $("#productImagesUrl2")[0].files[0]);
    }
    if ($("#productImagesUrl3")[0].files[0] != null && $("#productImagesUrl3")[0].files[0] != undefined) {
        if (ProductImageIds[2] != undefined && ProductImageIds[2] != null) {
            ProductImageIdArr.push(ProductImageIds[2]);
        }
        ProductImageIdArr.push(ProductImageIds[2]);
        formData.append("productImagesUrl3", $("#productImagesUrl3")[0].files[0]);
    }
    if ($("#productImagesUrl4")[0].files[0] != null && $("#productImagesUrl4")[0].files[0] != undefined) {
        if (ProductImageIds[3] != undefined && ProductImageIds[3] != null) {
            ProductImageIdArr.push(ProductImageIds[3]);
        }
        formData.append("productImagesUrl4", $("#productImagesUrl4")[0].files[0]);
    }
    if (ProductImageIdArr.length > 0) {
        formData.append("ProductImageIds", ProductImageIdArr.join(","));
    } else {
        formData.append("ProductImageIds", "");
    }

    formData.append("CreatedBy", atob(VendorId));
    formData.append("UpdatedBy", atob(VendorId));

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/product/Product_Upsert',
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
            setTimeout(function () {
                window.location = "/VendorProduct/ManageProduct";
            }, 3000);

            $('#saveBtn').show();
            $('#saveBtnloading').hide();

        }, error: function (error) {
            debugger;
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


//MasterOrderStatus dropdown API call in ajax methos
function masterProductBrand() {

    let MasterProductBrand = new Object();
    MasterProductBrand.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/masterProductBrand/MasterProductBrand_All?search&SalonId=0',
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

            $("#productType").html(``);

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

$('#productType').change(function () {
   
    masterProductCategory();
});


//masterProductType dropdown API call in ajax methos
function masterProductCategory() {

    let MasterProductCategory = new Object();
    MasterProductCategory.IsPageProvided = true;

    var productTypeId = $('#productType').val();

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/masterProductCategory/MasterProductCategory_All?search&ProductTypeId=${productTypeId}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(MasterProductCategory),
        crossDomain: true,
        async: false,
        success: function (result) {
            
            $("#productCategory").html(``);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#productCategory').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].ProductCategoryName}</option>
                    `);
                    $('#productCategory').removeAttr("disabled");
                }
            }

            $('.selectpicker').selectpicker("refresh");


        }, error: function (error) {
            // Error function

        }
    });
    return false;
}


//masterProductType dropdown API call in ajax methos
function masterProductWeight() {

    let MasterProductType = new Object();
    MasterProductType.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/masterProductWeight/MasterProductWeight_All?search',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(MasterProductType),
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