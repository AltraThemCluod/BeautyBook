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

//savePromotion function call
function savePromotion() {
    $('#saveBtn').hide();
    $('#saveBtnloading').show();

    var Promotion = new Object();
    Promotion.Id = $('#promotionId').val();
    Promotion.VendorId = atob(VendorId);
    Promotion.ProductId = $('#productName').val();
    Promotion.ProductTypeId= 1;
    Promotion.ProductBrandId = 1;
    let str = $('#offerperiod').val();
    const myArr = str.split("-");
    Promotion.StartDate = myArr[0];
    Promotion.EndDate = myArr[1];
    Promotion.OriginalPrice = $('#originalprice').val();
    Promotion.OfferPrice = $('#offerprice').val();
    Promotion.CreatedBy = atob(VendorId);
    Promotion.UpdatedBy = atob(VendorId);

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/promotion/Promotion_Upsert',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(Promotion),
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
                window.location = "/Promotion/ManagePromotion";
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


//MasterOrderStatus dropdown API call in ajax methos
function masterProductName() {

    let MasterProductName = new Object();
    MasterProductName.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/product/Product_All?search=&ProductName=&ProductTypeId=0&ProductBrandId=0&LookUpStatusId=0&VendorId=${atob(VendorId)}&ProductBrandIds=&ProductCategoryIds=&MinPrice=&MaxPrice=`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(MasterProductName),
        crossDomain: true,
        async: false,
        success: function (result) {
            
            $("#productName").html(``);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#productName').append(`
                        <option data-value="${result.Values[i].Price}" value="${result.Values[i].Id}">${result.Values[i].ProductName}</option>
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


function getProductPrice() {
    var productPrice = $('select#productName').find(':selected').data('value');
    $('#originalprice').val(productPrice);
}


