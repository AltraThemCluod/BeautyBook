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
var userId = getCookie("UserId");
var salonId = getCookie("SalonId");

var getSalonId = atob(salonId);
var getSalonIdatob = ~~getSalonId;

//get product amount
$('#subTotal').text(atob(getCookie("SubTotal")));
$('#subTotalInp').val(atob(getCookie("SubTotal")));
$('#VATIncluded').text(atob(getCookie("VATincluded")));
$('#VATIncludedInp').val(atob(getCookie("VATincluded")));
$('#TotaltoPay').text(atob(getCookie("TotalToPay")));
$('#TotaltoPayInp').val(atob(getCookie("TotalToPay")));

function PaymentMethodList() {

    var paymentMethodList = new Object();
    paymentMethodList.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpPaymentMethod/LookUpPaymentMethod_All?search=`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(paymentMethodList),
        crossDomain: true,
        success: function (result) {
             
            $('#paymentCollapse').html(``);


            


            for (i = 0; i < result.Values.length; i++) {
                $('#paymentCollapse').append(`
                    <div class="card">
                        <div class="card-header py-3">
                            <div class="payment-title" role="button" data-toggle="collapse" data-target="#cardPayment${result.Values[i].Id}">
                                <div class="custom-control custom-radio w-100">
                                    <input type="radio" class="custom-control-input" id="paymentType${result.Values[i].Id}" value="${result.Values[i].Id}" name="paymentType" checked="">
                                    <label for="paymentType${result.Values[i].Id}" class="custom-control-label d-flex align-items-center">
                                        <strong class="d-block mr-3">${result.Values[i].Name}</strong>
                                    </label>
                                </div>
                            </div>
                        </div>

                        <div id="cardPayment${result.Values[i].Id}" class="collapse" data-parent="#paymentCollapse">
                            <div class="card-body">
                                <div class="row m-0" id="paymentTypeImages_${result.Values[i].Id}">
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                `);
            }

            $('#paymentTypeImages_3').append(`
                <p class="mb-0 text-muted">${Langaugestore.You_have_selected_to_pay_with_cash_upon_delivery_}.</p>
            `);

            $('#paymentTypeImages_1').append(`
                <img class="credit-card" src="../Content/assets/images/logo/cards.png" width="108" alt="Credit cards">
            `);

            $('#paymentTypeImages_2').append(`
                <div class="col-lg-6">
                    <img class="img-fluid" src="../Content/assets/images/logo/paypal.png" alt="PayPal">
                </div>
                <div class="col-lg-6">
                    <img class="img-fluid" src="../Content/assets/images/logo/paypal-credit.png" alt="PayPal">
                </div>
            `);

        }, error: function (error) {
             
        }
    });
    return false;
}

