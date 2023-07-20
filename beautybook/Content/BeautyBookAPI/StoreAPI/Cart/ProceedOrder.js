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

//setCookie
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

var DeviceTokenNumber = getCookie("DeviceToken&Type");
var salonId = getCookie("SalonId");

//ProceedToCheckout amount cookies store
function ProceedToCheckout() {
    setCookie("SubTotal", btoa($('#subTotalInp').val()), 30);
    setCookie("VATincluded", btoa($('#VATIncludedInp').val()), 30);
    setCookie("TotalToPay", btoa($('#TotaltoPayInp').val()), 30);

    $('#p_button_text').hide();
    $('#p_button_loader').show();

    var vendorIdStr = $("input[id='productVendorId']").map(function () { return $(this).val(); }).get().toString();
    var productIdStr = $("input[id='productCartId']").map(function () { return $(this).val(); }).get().toString();

    var proceedToCheckout = new Object();
    proceedToCheckout.Id = 0;
    proceedToCheckout.SalonId = atob(salonId);
    proceedToCheckout.CreatedBy = 1;
    proceedToCheckout.VendorIdStr = vendorIdStr;
    proceedToCheckout.AddToCartIdStr = productIdStr;
    debugger;
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/orders/Orders_Upsert`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(proceedToCheckout),
        crossDomain: true,
        success: function (result) {

            setCookie("OrderId", btoa(result.Item.Id), 30);
            setCookie("OrderNo", btoa(result.Item.OrderNo), 30);
            if (result.Code == 200) {
                //Swal.fire({
                //    position: 'center',
                //    icon: 'success',
                //    title: result.Message,
                //    showConfirmButton: false,
                //    timer: 1000
                //})
                window.location.href = "/Store/CheckoutDetails"
                $('#p_button_text').show();
                $('#p_button_loader').hide();
            }
        }, error: function (error) {
            $('#p_button_text').show();
            $('#p_button_loader').hide();
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 1000
            });
        }
    });
}



//ProceedToPayment order update data api call
function ProceedToPayment() {
    $('#p_button_text').hide();
    $('#p_button_loader').show();
    var proceedToPayment = new Object();
    proceedToPayment.Id = atob(getCookie("OrderId"));
    proceedToPayment.ShippingAddressId = parseInt($("input[type='radio'][name='ShippingAddress']:checked").val());
    proceedToPayment.IsBillingAddress = $("#shippingAddress").prop("checked") == true ? 1 : 0;
    proceedToPayment.BillingAddressId = parseInt($("input[type='radio'][name='BillingAddress']:checked").val());

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/orders/Orders_UpdateCheckoutDetails`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(proceedToPayment),
        crossDomain: true,
        success: function (result) {

            if (result.Code == 200) {
                //Swal.fire({
                //    position: 'center',
                //    icon: 'success',
                //    title: result.Message,
                //    showConfirmButton: false,
                //    timer: 1000
                //})
                window.location.href = "/Store/Payment"
                $('#p_button_text').show();
                $('#p_button_loader').hide();
            }
        }, error: function (error) {
            $('#p_button_text').show();
            $('#p_button_loader').hide();
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 1000
            });
        }
    });
}


//ProceedToPayment order update data api call
function CompleteOrder() {
    $('#p_button_text').hide();
    $('#p_button_loader').show();

    var TotalToPayAmount = getCookie("TotalToPay");
    var PayAmount = atob(TotalToPayAmount);
    var completeOrder = new Object();
    completeOrder.Id = atob(getCookie("OrderId"));
    completeOrder.MasterPaymentMethodId = parseInt($("input[type='radio'][name='paymentType']:checked").val());
    debugger;
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/orders/Orders_UpdatePaymentMethod`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(completeOrder),
        crossDomain: true,
        success: function (result) {
            debugger;
            if (result.Code == 200) {
                //Swal.fire({
                //    position: 'center',
                //    icon: 'success',
                //    title: result.Message,
                //    showConfirmButton: false,
                //    timer: 1000
                //});

                if (parseInt($("input[type='radio'][name='paymentType']:checked").val()) == 3) {
                    window.location.href = `/Store/CheckoutComplete?OI=${btoa(result.Item.Id)}`
                }
                else {
                    PayTabsPayment(PayAmount.replace("SAR", ""), `cart ${result.Item.Id}`, `cart desc: ${result.Item.Id}`, result.Item.Id);
                }
                
                //setTimeout(function () {
                //    window.location.href = "/Store/CheckoutComplete"
                //}, 1000);
            }
        }, error: function (error) {
            $('#p_button_text').show();
            $('#p_button_loader').hide();
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 1000
            });
        }
    });
}

function PayTabsPayment(TotalAmount, cart_id, cart_description , OrderId)
{
    debugger;
    $.ajax({
        type: 'POST',
        url: APIEndPoint + 'api/orders/Orders_PayTabsPayment?TotalAmount=' + TotalAmount + '&cart_id=' + cart_id + '&cart_description=' + cart_description + '&OrderId=' + btoa(OrderId),
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            debugger;
            if (result != null && result != undefined && result.redirect_url != null && result.redirect_url != '') {
                window.location.href = result.redirect_url;
            }
        },
        error: function (error) {
            debugger;
            console.log('error', error);
        }
    });
}