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

//employeeTypedrp dropdown API call in ajax method
function customerData() {

    var CustomerData = new Object();
    CustomerData.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_All?search&LookUpUserTypeId=4&SalonId=' + atob(SalonId) + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(CustomerData),
        crossDomain: true,
        async: false,
        success: function (result) {
            $("#customerDatalist").html(``);
            for (i = 0; i < result.Values.length; i++) {
                $('#customerDatalist').append(`
                 <option value="${result.Values[i].Id}">${result.Values[i].UserName}  -  ${result.Values[i].PrimaryPhone}</option>
                `);
                $('.selectpicker').selectpicker("refresh");
            }
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}


//Payment Way Type
var PayType = 0;
function PaymentWayType(typeId) {

    debugger;

    PayType = typeId;

    var PosTotalInp = $("#posTotalinp").val();

    if (typeId == 1)
    {
        $("#cardAmountArea").text(0);
        $("#poscardAmount").val("");

        $("#cashAmountArea").text(PosTotalInp);
        $("#poscashAmount").val(PosTotalInp);

        $("#cashAmount").val("");
        $("#cashAmountDiv").hide();
    }
    else if (typeId == 2)
    {
        $("#cashAmountArea").text(0);
        $("#poscashAmount").val("");

        $("#cardAmountArea").text(PosTotalInp);
        $("#poscardAmount").val(PosTotalInp);

        $("#cashAmountDiv").hide();
    }
    else if (typeId == 3)
    {
        $("#cashAmountArea").text(0);
        $("#poscashAmount").val("");

        $("#cardAmountArea").text(0);
        $("#poscardAmount").val("");

        $("#cashAmount").val("");
        $("#cashAmountDiv").show();
    }
    else
    {
        $("#cashAmount").val(0);
        $("#cashAmountDiv").hide();
    }
}

//Add Cash Amount
function addCashAmount(event) {
    debugger;
    var PosTotalInp = $("#posTotalinp").val();

    if (parseFloat(event.value) <= parseFloat(PosTotalInp)) {
        $("#amountError").text("");

        var cashSar = event.value == "" || event.value == 0 ? 0 : event.value;

        $("#cashAmountArea").text(cashSar);
        $("#poscashAmount").val(cashSar);

        var casdSar = PosTotalInp - cashSar;

        $("#cardAmountArea").text(casdSar.toFixed(2));
        $("#poscardAmount").val(casdSar);
    }
    else {
        $("#amountError").text("Please enter a valid amount");
        $("#cashAmountArea").text(0);
        $("#cardAmountArea").text(0);
    }
}


function PosPayNow(posdetailsid) {
    debugger;

    $("#payment").hide();
    $("#paymentLoader").show();

    var PosPaymentDetails = new Object();
    PosPaymentDetails.POSDetailsId = posdetailsid;
    PosPaymentDetails.SubTotal = $("#posSubTotalinp").val();
    PosPaymentDetails.DiscountSales = $("#DiscountSales").val();
    PosPaymentDetails.TotalSalesTax = $("#posSubTotalSalesTaxinp").val();
    PosPaymentDetails.TotalAmount = $("#posTotalinp").val();
    PosPaymentDetails.PaymentTypeId = PayType;
    PosPaymentDetails.CashAmount = $("#poscashAmount").val();
    PosPaymentDetails.CareditAmount = $("#poscardAmount").val();
    PosPaymentDetails.SalonId = atob(SalonId);

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/pOSPayment/POSPayment_Upsert',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(PosPaymentDetails),
        success: function (result) {
            if (result.Code == 200) {

                delete_cookie("ServiceDetailsCokkieArray");

                PosOrderComplete(result.Item.POSDetailsId, result.Item.Id);

                //Swal.fire({
                //    position: 'center',
                //    icon: 'success',
                //    title: result.Message,
                //    showConfirmButton: false,
                //    timer: 30000
                //})
                //setTimeout(function () {
                //    window.location.reload();
                //}, 3000);
            }
        },
        error: function (error) {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })

            $("#payment").show();
            $("#paymentLoader").hide();
        }
    });
}

//PosOrderComplete

function PosOrderComplete(poid,paymentId)
{
    debugger;
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/pOSDetails/POSDetails_ById?Id=' + poid + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        async: false,
        success: function (result) {
            debugger;
            console.log(result);

            if (result.Code == 200)
            {
                $("#posOrderPaymentDetails").html(``);
                var PosOrderList = "";

                if (result.Item.PosOrderInvoice.length > 0) {
                    for (var i = 0; i < result.Item.PosOrderInvoice.length; i++) {
                        PosOrderList += `<div class="card_box_profile_card d-flex align-items-center p-0 mt-1" style="border-radius:5px;">
                            <div class="content_box_card" style="width:60%;">
                                <div>
                                    <span class="profile_title text-truncate cursor-pointer" title="Hair Wash">
                                        ${result.Item.PosOrderInvoice[i].Name == "" || result.Item.PosOrderInvoice[i].Name == null ? "" + Langaugestore.Service_not_found + "" : result.Item.PosOrderInvoice[i].Name}
                                    </span>
                                    <div class="price text-left fs-14 mt-0" style="line-height: 1.3;">
                                        SAR  ${result.Item.PosOrderInvoice[i].Price == "" || result.Item.PosOrderInvoice[i].Price == null ? "0.00" : result.Item.PosOrderInvoice[i].Price.toFixed(2)}
                                    </div>
                                </div>
                            </div>
                        </div>`
                    }
                }
                else {
                    PosOrderList += `<div class="card_box_profile_card d-flex align-items-center text-center p-0" style="border-radius:5px;"><h5>${Langaugestore.Services_not_found}</h5></div>`
                }

                $("#posOrderPaymentDetails").append(
                    `<div class="row m-0 align-items-center">
                        <div class="col-lg-6">
                            <div class="pos-order-complete pr-1">
                                ${PosOrderList}
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <span>${Langaugestore.Total}</span>
                            <h3>${result.Item.TotalAmount == null || result.Item.TotalAmount == "" ? "0.00" : result.Item.TotalAmount.toFixed(2)} <sub class="fs-14">SAR</sub></h3>
                        </div>
                    </div>
                    <div class="col-lg-12 mt-3 d-flex justify-content-center">
                        <a href="/Pos/PosInvoice?ri=${btoa(result.Item.POSInvoiceId)}" class="btn btn-lg btn-primary w-50 d-flex align-items-center justify-content-center"
                            style="text-transform: uppercase;"
                        >
                            ${Langaugestore.Print_Invoice}
                        </a>
                        <a href="javascript:void(0)" onclick="window.location.reload();" class="btn btn-lg btn-link w-50"
                            style="text-transform: uppercase;
                            font-size: 16px;
                            letter-spacing: 1px;"
                        >
                            ${Langaugestore.CONTINUE_SELLING}
                        </a>
                    </div>`)

                $("#PosOrderComplete").modal("show");
            }
            else
            {
                window.location.reload();
            }

            $("#payment").show();
            $("#paymentLoader").hide();
            
        }, error: function (error) {
            // Error function
        }
    });
}