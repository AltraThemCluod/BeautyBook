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
debugger;
var getRInvoiceId = new URLSearchParams(window.location.search);
getRInvoiceId = parseInt(atob(getRInvoiceId.get('RI')));

function NoticeCreditor(InvoiceId) {
    debugger;
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/orders/OrderReturnInvoice_ById?Id=${atob(InvoiceId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            console.log(result);
            window.location.href = `/Invoices/ReturnInvoice?RI=${btoa(result.Item.Id)}`
        }, error: function (error) {
            $('#orderinvoiceloader').hide();
        }
    });
}

//Return Invoice Service data push
var ReturnInvoiceService = [];

//Return Invoice Create
function returnInvoice() {
    $("#returnInvoiceLoader").show();
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/orders/OrderInvoice_ById?Id=${getRInvoiceId}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            console.log(result);


            if (result.Item != null) {
                var ReturnOrderList = "";

                for (var i = 0; i < result.Item.OrderItemData.length; i++) {

                    ReturnInvoiceService.push(result.Item.OrderItemData[i]);

                    ReturnOrderList += `<tr>
                                                <td class="col-lg-3 p-2">
                                                    <div class="custom-control custom-checkbox">
                                                        <input type="checkbox" value="1" data-id="myId" data-name="chck-${i}" onchange="SelectService(this,${result.Item.OrderItemData[i].Id});" class="custom-control-input checkbox invoice-item" id="InvoiceItem_${i}" data-group="${i}">
                                                        <label class="custom-control-label" for="${i}"></label>
                                                    </div>
                                                </td>
                                                <td>${result.Item.OrderItemData[i].ProductName}</td>
                                                <td>${result.Item.OrderItemData[i].InvoiceUnitePrice.toFixed(2)}</td>
                                                <td>15%</td>
                                                <td>${result.Item.OrderItemData[i].InvoiceTaxPrice.toFixed(2)}</td>
                                                <td>${result.Item.OrderItemData[i].InvoiceProductRate.toFixed(2)}</td>
                                                <td>${result.Item.OrderItemData[i].Qty}</td>
                                                <td class="text-right">${result.Item.OrderItemData[i].InvoiceProductAmount.toFixed(2)}</td>
                                            </tr>`
                }

                $("#returnInvoice").append(`
                    <div class="row mb-3" id="invoiceDateNo" dir="ltr">
                        <div class="col-sm-6 col-6 font-small">
                            <div><strong>${Langaugestore.Orginal_invoice_No} : </strong> <br>#${result.Item.OldInvoiceNumber}</div>
                        </div>
                        <div class="col-sm-6 col-6 text-right font-small">
                            <div><strong>${Langaugestore.Return_invoice_No} : </strong> <br>#${result.Item.InvoiceNo}</div>
                        </div>
                    </div>
                    <div class="invoice-service-details">
                        <span><b>${Langaugestore.Sevice_list_for_invoice_number} <span class="them-color"> #${result.Item.OldInvoiceNumber} </span></b> ${Langaugestore.Please_chose_the_services}</span>
                        <div class="table-card table-responsive mt-2">
                            <table class="table fs-14 mb-0">
                                 <thead>
                                    <tr>
                                        <th class="col-3"></th>
                                        <th class="col-3">${Langaugestore.ProductName}</th>
                                        <th class="col-2">${Langaugestore.UnitePrice}</th>
                                        <th class="col-2">Tax Rate</th>
                                        <th class="col-2">${Langaugestore.TaxAmount}</th>
                                        <th class="col-2">${Langaugestore.Rate}</th>
                                        <th class="col-1">${Langaugestore.QTY}</th>
                                        <th class="col-2 text-right">${Langaugestore.Amount}</th>
                                    </tr>
                                </thead>
                                <tbody id="ReturnServiceList">
                                    ${ReturnOrderList}
                                </tbody>
                            </table>
                        </div>
                    </div>
                `);
            }
            $("#returnInvoiceLoader").hide();
        }, error: function (error) {
            // Error function
            $('#returnInvoiceLoader').hide();
        }
    });
}

//Return invoice selected services
var selectedOrders = [];
var selectedOrdersId = [];
function SelectService(checkbox, OrderInvoiceId) {
    //$(".custom-control-input").attr("disabled", true);
    var invoiceSelectedlength = $('.invoice-item:checkbox:checked').length;
    if (invoiceSelectedlength > 0) {
        $("#saveButton").removeAttr('disabled', 'disabled');
        $("#saveButton").removeAttr('title', 'Please select a more then 1 item');
    } else {
        $("#saveButton").attr('disabled', 'disabled');
        $("#saveButton").attr('title', 'Please select a more then 1 item');
    }
    debugger;

    if (checkbox.checked == true) {
        selectedOrdersId.push(OrderInvoiceId);
    } else {
        for (i = 0; i < selectedOrdersId.length; i++) {
            if (selectedOrdersId[i] == OrderInvoiceId) {
                selectedOrdersId.splice(i, 1);
            }
        }
    }
}

function UpdateReturnInvoiceData() {
    debugger;
    $("#saveButton").hide();
    $("#saveLoadingButton").show();


    for (var i = 0; i < ReturnInvoiceService.length; i++) {
        for (var r = 0; r < selectedOrdersId.length; r++) {
            if (ReturnInvoiceService[i].Id == selectedOrdersId[r]) {
                selectedOrders.push(ReturnInvoiceService[i]);
            }
        }
    }


    console.log('selectedOrders', selectedOrders);
    if (selectedOrders != null) {
        var totalExcludingvat = 0;
        var Discount = 0;
        var totalTaxableamount = 0;
        var totalVat = 0;
        for (var SO = 0; SO < selectedOrders.length; SO++) {
            totalExcludingvat = totalExcludingvat + selectedOrders[SO].ProductAmount;
            Discount = Discount + selectedOrders[SO].ProductDiscount;
            totalTaxableamount = totalExcludingvat - Discount;
            totalVat = totalTaxableamount * 15 / 100;
        }

        for (var UP = 0; UP < selectedOrders.length; UP++) {
            selectedOrders[UP].InvoiceProductSubTotalWithDiscount = totalTaxableamount;
            selectedOrders[UP].InvoiceProductTaxAmount = totalTaxableamount * 15 / 100;
            selectedOrders[UP].InvoiceProductTotal = totalTaxableamount + totalVat;
            selectedOrders[UP].ProductSubTotal = totalExcludingvat;
            selectedOrders[UP].InvoiceProductDiscount = Discount;
        }
    }

    var UpdateReturnInvoiceData = new Object();
    UpdateReturnInvoiceData.Id = getRInvoiceId;
    UpdateReturnInvoiceData.OrderObjectStr = JSON.stringify(selectedOrders);
    
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/orders/OrderReturnInvoice_Update`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(UpdateReturnInvoiceData),
        crossDomain: true,
        success: function (result) {
            console.log('result', result);
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    window.location = "/Invoices/TaxInvoice";
                }, 2500);
            }
            $("#saveButton").show();
            $("#saveLoadingButton").hide();
        }, error: function (error) {
            console.log('error', error);
            $("#saveButton").show();
            $("#saveLoadingButton").hide();
        }
    });
}
