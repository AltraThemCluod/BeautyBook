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
var UserId = getCookie("UserId");

var getRInvoiceId = new URLSearchParams(window.location.search);
getRInvoiceId = parseInt(atob(getRInvoiceId.get('RI')));

function NoticeCreditor(InvoiceId) {
    debugger;
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/pOSPayment/PosReturnInvoice_ById?Id=${atob(InvoiceId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            window.location.href = `/Pos/ReturnInvoice?RI=${btoa(result.Item.Id)}`
        }, error: function (error) {
            $('#orderinvoiceloader').hide();
        }
    });
}

//Return Invoice Service data push
var ReturnInvoiceService = [];
var DiscountSalePercentage = 0;

//Return Invoice Create
function returnInvoice() {
    $("#returnInvoiceLoader").show();
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/pOSPayment/PosInvoice_ById?Id=${getRInvoiceId}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            console.log(result);
            debugger;

            if (result.Item != null) {
                var ReturnServiceList = "";

                DiscountSalePercentage = result.Item.DiscountSales;

                for (var i = 0; i < result.Item.PosInvoiceOrder.length; i++) {

                    ReturnInvoiceService.push(result.Item.PosInvoiceOrder[i]);

                    ReturnServiceList += `<tr>
                                                <td class="col-lg-3 p-2">
                                                    <div class="custom-control custom-checkbox">
                                                        <input type="checkbox" value="1" data-id="myId" data-name="chck-${i}" onchange="SelectService(this,${result.Item.PosInvoiceOrder[i].Id});" class="custom-control-input checkbox invoice-item" id="InvoiceItem_${i}" data-group="${i}">
                                                        <label class="custom-control-label" for="${i}"></label>
                                                    </div>
                                                </td>
                                                <td class="col-lg-3 p-2">${result.Item.PosInvoiceOrder[i].Name}</td>
                                                <td class="col-lg-3 text-right p-2"><b style="font-weight: 500;">SAR </b>${result.Item.PosInvoiceOrder[i].Price == "" || result.Item.PosInvoiceOrder[i].Price == null ? "" : result.Item.PosInvoiceOrder[i].Price.toFixed(2)}</td>
                                                <td class="col-lg-3 text-right p-2">${result.Item.PosInvoiceOrder[i].ServiceProvider}</td>
                                            </tr>`
                }

                $("#returnInvoice").append(`
                    <div class="row mb-3" id="invoiceDateNo" dir="ltr">
                        <div class="col-sm-6 col-6 font-small">
                            <div><strong>${Langaugestore.Orginal_invoice_No} : </strong> <br>#${result.Item.OldInvoiceNumber}</div>
                        </div>
                        <div class="col-sm-6 col-6 text-right font-small">
                            <div><strong>${Langaugestore.Return_invoice_No} : </strong> <br>#${result.Item.PosInvoiceNumber}</div>
                        </div>
                    </div>
                    <div class="invoice-service-details">
                        <span><b>${Langaugestore.Sevice_list_for_invoice_number} <span class="them-color"> #${result.Item.OldInvoiceNumber} </span></b> ${Langaugestore.Please_chose_the_services}</span>
                        <div class="table-card table-responsive mt-2">
                            <table class="table fs-14 mb-0">
                                <thead>
                                    <tr>
                                        <th class="col-lg-3 pl-2 pr-2"></th>
                                        <th class="col-lg-3 pl-2 pr-2">${Langaugestore.ServiceName}</th>
                                        <th class="col-lg-3 text-right pl-2 pr-2">${Langaugestore.Amount}</th>
                                        <th class="col-lg-3 text-right pl-2 pr-2">${Langaugestore.Service_Provider}</th>
                                    </tr>
                                </thead>
                                <tbody id="ReturnServiceList">
                                    ${ReturnServiceList}
                                </tbody>
                            </table>
                        </div>
                    </div>
                `);
            }
            $("#returnInvoiceLoader").hide();
        }, error: function (error) {
            debugger;
            // Error function
            $('#returnInvoiceLoader').hide();
        }
    });
}


//Return invoice selected services
var selectedServices = [];
var selectedServicesId = [];
function SelectService(checkbox, AppointmentInvoiceId) {
    debugger;
    var invoiceSelectedlength = $('.invoice-item:checkbox:checked').length;
    if (invoiceSelectedlength > 0) {
        $("#saveButton").removeAttr('disabled', 'disabled');
        $("#saveButton").removeAttr('title', 'Please select a more then 1 item');
    } else {
        $("#saveButton").attr('disabled', 'disabled');
        $("#saveButton").attr('title', 'Please select a more then 1 item');
    }

    if (checkbox.checked == true) {
        selectedServicesId.push(AppointmentInvoiceId);
    } else {
        for (i = 0; i < selectedServicesId.length; i++) {
            if (selectedServicesId[i] == AppointmentInvoiceId) {
                selectedServicesId.splice(i, 1);
            }
        }
    }
}

function UpdateReturnInvoiceData() {
    debugger;
    $("#saveButton").hide();
    $("#saveLoadingButton").show();

    for (var i = 0; i < ReturnInvoiceService.length; i++) {
        for (var r = 0; r < selectedServicesId.length; r++) {
            if (ReturnInvoiceService[i].Id == selectedServicesId[r]) {
                selectedServices.push(ReturnInvoiceService[i]);
            }
        }
    }

    console.log('selectedServices', selectedServices);

    var SubTotalAmount = 0;
    var TotalSalesTax = 0;
    var DiscountSalesAmount = 0;

    if (selectedServices != null && selectedServices != "") {
        for (var i = 0; i < selectedServices.length; i++) {
            SubTotalAmount = SubTotalAmount + selectedServices[i].Price;
        }
    }

    if (SubTotalAmount > 0 && SubTotalAmount != null && SubTotalAmount != "")
    {
        DiscountSalesAmount = SubTotalAmount - (SubTotalAmount / 100 * DiscountSalePercentage)
        TotalSalesTax = DiscountSalesAmount / 100 * 15;
    }

    var PosReturnInvoiceData = new Object();
    PosReturnInvoiceData.Id = getRInvoiceId;
    PosReturnInvoiceData.PosOrderInvoiceStr = JSON.stringify(selectedServices);
    PosReturnInvoiceData.TotalAmount = (DiscountSalesAmount + TotalSalesTax).toFixed(2);
    PosReturnInvoiceData.SubTotal = SubTotalAmount.toFixed(2);
    PosReturnInvoiceData.TotalSalesTax = TotalSalesTax.toFixed(2);
    PosReturnInvoiceData.SalonId = atob(SalonId);

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/pOSPayment/PosReturnInvoice_Update`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(PosReturnInvoiceData),
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
                setTimeout(function () {
                    window.location = "/Pos/SiplifedInvoice";
                }, 2500);
            }
            $("#saveButton").show();
            $("#saveLoadingButton").hide();
        }, error: function (error) {
            $("#saveButton").show();
            $("#saveLoadingButton").hide();
        }
    });
}
