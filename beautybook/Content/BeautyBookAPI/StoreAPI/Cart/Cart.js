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
var gettypeId = new URLSearchParams(window.location.search);
gettypeId = parseInt(atob(gettypeId.get('typeId')));
var gettypeIdatob = ~~gettypeId;
//setCookie
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

var DeviceTokenNumber = getCookie("DeviceToken&Type");
var salonId = getCookie("SalonId");

var IsChangeQty = false;
$(document).on('click', '.bootstrap-touchspin-up', function () {
    IsChangeProductQty(true);
});
$(document).on('click', '.bootstrap-touchspin-down', function () {
    IsChangeProductQty(true);
});

function CartProduct() {
     
    $('#productCartloader').show();

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
            console.log(result);
             
            if (result.Values.length <= 0) {
                $('.cart-area .container .row .cart-div-area').addClass('col-lg-12');
                $('#priceTotalSidebar').html(``);
                $('#productCart').html(`
                     <div class="text-center">
                        <h2 class="mb-0" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</h2>
                     </div>
                `)
            } else {
                $("#productCart").html(``);
                $('#priceTotalSidebar').html(``);
                $('.cart-area .container .row .cart-div-area').addClass('col-lg-8');
            }

            var SumTotalAmo = 0;
            var SumProductTax = 0;

            //cart product count 
            for (i = 0; i < result.Values.length; i++) {

                $('#productCart').append(`
                    <div class="media">
                        <div class="avatar avatar-xl border overflow-hidden mr-3">
                            <img class="img-fluid" onerror="this.src='/Content/assets/images/DefaultProduct.jpg'" src="${APIEndPoint}/${result.Values[i].ProductThumbnailImage}" alt="Image Description">
                            <input type="hidden" id="productCartId" value="${result.Values[i].Id}"/>
                            <input type="hidden" id="productVendorId" value="${result.Values[i].VendorId}"/>
                        </div>

                        <div class="media-body">
                            <div class="row">
                                <div class="col-md-6 mb-3 mb-md-0 product-basic-details">
                                    <a class="fs-16 link font-weight-medium d-block" href="/Store/ViewProduct?productId=${btoa(result.Values[i].ProductId)}">
                                        ${result.Values[i].ProductName}
                                    </a>
                                    <h5 class="font-weight-medium fs-14 mt-3">SAR ${result.Values[i].ProductPrice.toFixed(2)}</h5>
                                </div>

                                <div class="col col-md-3 col-12 mb-1 product-qty-count align-self-center">
                                    <div class="store-touchspin">
                                        <input id="productQty" class="subtract-quantity text-center form-control-sm" type="text" value="${result.Values[i].Qty}"
                                                name="subtract-quantity">
                                    </div>
                                </div>

                                <div class="col col-md-3 col-12 mb-1 product-delete align-self-center text-right">
                                    <h5 class="font-weight-medium fs-14">SAR ${result.Values[i].ProductTotalAmount.toFixed(2)}</h5>

                                    <span class="text-danger" role="button" onclick="deleteProductConfirm(${result.Values[i].Id});">
                                        <svg xmlns="http://www.w3.org/2000/svg" height="24" width="24" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
                                        </svg>
                                    </span>
                                </div>

                            </div>
                        </div>
                    </div>
                    <hr class="hr-line">
                `);
            }

            $('#priceTotalSidebar').append(`
                    <div class="sticky-panel-top">
                        <div class="card">
                            <div class="card-body">
                                <div class="row align-items-center mb-2">
                                    <span class="col-6 fs-14">${Langaugestore.Subtotal}:</span>
                                    <h4 class="col-6 font-weight-medium fs-16 text-right text-dark mb-0" id="subTotal">SAR ${result.Values[0].ProductSubTotal.toFixed(2)}</h4>
                                    <input type="hidden" name="name" id="subTotalInp" value="SAR ${result.Values[0].ProductSubTotal.toFixed(2)}" />
                                </div>
                                <div class="row align-items-center">
                                    <span class="col-6 fs-14">${Langaugestore.Delivery}:</span>
                                    <h4 class="col-6 font-weight-medium fs-16 text-right text-dark mb-0">Free</h4>
                                </div>
                                <hr class="my-3">
                                <div class="row align-items-center">
                                    <span class="col-6 fs-14">${Langaugestore.VAT_included}:</span>
                                    <h4 class="col-6 font-weight-medium fs-16 text-right text-dark mb-0" id="VATIncluded">SAR ${result.Values[0].ProductTaxAmount.toFixed(2)}</h4>
                                    <input type="hidden" id="VATIncludedInp" name="name" value="SAR ${result.Values[0].ProductTaxAmount.toFixed(2)}" />
                                </div>
                                <hr class="my-3">
                                <div class="row align-items-center">
                                    <span class="col-6 fs-14 text-dark font-weight-bold">${Langaugestore.Total_to_pay}:</span>
                                    <h3 class="col-6 font-weight-medium fs-16 text-right text-dark mb-0" id="TotaltoPay">SAR ${result.Values[0].ProductTotal.toFixed(2)}</h3>
                                    <input type="hidden" id="TotaltoPayInp" name="name" value="SAR ${result.Values[0].ProductTotal.toFixed(2)}" />
                                </div>
                                <a href="javascript:;" onclick="ProceedToCheckout();" class="btn btn-primary btn-wide btn-lg font-weight-medium btn-block mt-5"> <span id="p_button_text">${Langaugestore.Proceed_to_Chechouts}</span> <img id="p_button_loader" style="width:50px;display:none;" src="../Content/assets/images/loading.gif"></a>
                                <a href="javascript:;" onclick="updateCartItems();"  id="UpdateQtyButton" class="btn btn-outline-primary btn-wide btn-lg font-weight-medium btn-block mt-2">${Langaugestore.Update_Cart}</a>
                            </div>
                        </div>
                    </div>
                `);

            //subtract-quantity function
            touchSpin();
            $('#productCartloader').hide();
            IsChangeProductQty(false);
        }, error: function (error) {
            // Error function
            $('#productCartloader').hide();
        }
    });
    
    return false;
}

//<img src="../Content/assets/images/circles-menu-1.gif">


function IsChangeProductQty(ChangeTrueFlag) {
     
    IsChangeQty = ChangeTrueFlag;
    if (IsChangeQty == true) {
        $('#UpdateQtyButton').show();
    } else {
        $('#UpdateQtyButton').hide();
    }
}


//deleteProductconfirm function
function deleteProductConfirm(cartProductId) {

    Swal.fire({
        title: '' + Langaugestore.Are_you_sure_you_want_to_remove_this_product__+'',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteProductSwal(cartProductId);
        }
    })
}

//deleteProduct function

function DeleteProductSwal(cartProductId) {
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/addToCart/AddToCart_Delete?Id=' + cartProductId + '&DeletedBy=' + atob(UserId) + '',
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        success: function (result) {
            if (result.Code == 200) {
                CartProduct();
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
            })
        }
    });
}

