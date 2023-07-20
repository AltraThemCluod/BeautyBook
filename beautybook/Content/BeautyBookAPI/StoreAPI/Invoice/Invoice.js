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
 
var ordrId = getCookie("OrderId");

var OrderUrl = new URLSearchParams(window.location.search);
OrderUrl = parseInt(atob(OrderUrl.get('OrderId')));
var OrderUrlatob = ~~OrderUrl;


function OrderInvoice() {
     
    $('#orderinvoicelist').html(``);
    $('#orderinvoiceloader').show();


    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/orders/Orders_ById?Id=${OrderUrlatob == 0 ? atob(ordrId) : OrderUrlatob}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            console.log(result);    
             debugger;
            $('#invoicedetails').append(`
                <div class="col-sm-6 text-ar-right">
                    <div><strong>${Langaugestore.Order_Date} : </strong> <span class="fs-14">${result.Item.OrderDate}</span></div>
                    <div class="mt-1"><strong>${Langaugestore.Printing_Date} : </strong> <span class="fs-14">${result.Item.InvoicePrintDate}</span></div>
                </div>

                <div class="col-sm-6 text-sm-right text-ar-left"> <strong>${Langaugestore.Invoice_No} : </strong> <span class="fs-14">${result.Item.InvoiceNo}</span></div>
            `);
            $('#invoiceAddressDetails').append(`
                <div class="w-100 mb-2">
                    <div class="card-body p-0">
                        <div class="table-card table-responsive border-none">
                            <table class="table fs-14 mb-0 border" id="address_formate">
                                <thead>
                                    <tr>
                                        <th class="col-6 p-2">Buyer</th>
                                    </tr>
                                </thead>
                                <tbody class="order-inovice-list">
                                    <tr>
                                        <td class="p-0" id="supplier_address">
                                            <table class="w-100 border-none">
                                                <tr>
                                                    <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">Seller Name</b></td>
                                                    <td class="p-1 pl-2 pr-2 col-8 border-right-none"><span>${result.Item.CustomerName == "" || result.Item.CustomerName == null ? "-" : result.Item.CustomerName} </span></td>
                                                </tr>
                                                <tr>
                                                    <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">Address</b></td>
                                                    <td class="p-1 pl-2 pr-2 col-8 border-right-none">
                                                        <span>
                                                            ${result.Item.SalonAddressName == "" || result.Item.SalonAddressName == null ? "-" : result.Item.SalonAddressName} <br/>
                                                            ${result.Item.SalonCountryName == "" || result.Item.SalonCountryName == null ? "-" : result.Item.SalonCountryName},
                                                            ${result.Item.SalonStateName == "" || result.Item.SalonStateName == null ? "-" : result.Item.SalonStateName},
                                                            ${result.Item.SalonCityName == "" || result.Item.SalonCityName == null ? "-" : result.Item.SalonCityName},
                                                            ${result.Item.SalonZipCode == "" || result.Item.SalonZipCode == null ? "" : result.Item.SalonZipCode}
                                                        </span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">Commercial Register No</b></td>
                                                    <td class="p-1 pl-2 pr-2 col-8 border-right-none"><span>${result.Item.CommercialRegisterNo == "" || result.Item.CommercialRegisterNo == null ? "-" : result.Item.CommercialRegisterNo}</span></td>
                                                </tr>
                                                <tr>
                                                    <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">VAT Number</b></td>
                                                    <td class="p-1 pl-2 pr-2 col-8 border-right-none"><span>${result.Item.SalonVATNumber == "" || result.Item.SalonVATNumber == null ? "-" : result.Item.SalonVATNumber}</span></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            `);
            if (result.Item.OrderProducts.length <= 0) {
                $('#orderinvoicelist').html(`
                    <tr>
                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
                    </tr>
                `)
            } else {
                $("#orderinvoicelist").html(``);
            }
            for (i = 0; i < result.Item.OrderProducts.length; i++) {
                $('#orderinvoicelist').append(`
                    <tr>
                        <td class="col-3" style="white-space: inherit !important;">${result.Item.OrderProducts[i].ProductName}</td>
                        <td class="col-2 text-left"><b style="font-weight: 500;">SAR</b> ${result.Item.OrderProducts[i].InvoiceUnitePrice.toFixed(2)}</td>
                        <td class="col-2 text-left">15%</td>
                        <td class="col-2 text-left"><b style="font-weight: 500;">SAR</b> ${result.Item.OrderProducts[i].InvoiceTaxPrice.toFixed(2)}</td>
                        <td class="col-2 text-left"><b style="font-weight: 500;">SAR</b> ${result.Item.OrderProducts[i].InvoiceProductRate.toFixed(2)}</td>
                        <td class="col-1 text-left">${result.Item.OrderProducts[i].Qty}</td>
                        <td class="col-2 text-right"><b style="font-weight: 500;">SAR</b> ${result.Item.OrderProducts[i].InvoiceProductAmount.toFixed(2)}</td>
                    </tr>
                `);
            }
            $('#orderinvoicelist').append(`
                <tr>
                    <td colspan="6" class="text-right text-right-ar"><strong>${Langaugestore.Total__excluding_VAT_} : </strong></td>
                    <td class="text-right"><b style="font-weight: 500;">SAR</b> ${result.Item.OrderProducts[0].ProductSubTotal.toFixed(2)}</td>
                </tr>
                <tr>
                    <td colspan="6" class="text-right text-right-ar"><strong>${Langaugestore.Discount} : </strong></td>
                    <td class="text-right"><b style="font-weight: 500;">SAR</b> ${result.Item.OrderProducts[0].InvoiceProductDiscount.toFixed(2)}</td>
                </tr>
                <tr>
                    <td colspan="6" class="text-right text-right-ar"><strong>${Langaugestore.Total_Taxable_amount__excluding_VAT_} : </strong></td>
                    <td class="text-right"><b style="font-weight: 500;">SAR</b> ${result.Item.OrderProducts[0].InvoiceProductSubTotalWithDiscount.toFixed(2)}</td>
                </tr>
                <tr>
                    <td colspan="6" class="text-right text-right-ar"><strong>${Langaugestore.Total_VAT} (15%) : </strong></td>
                    <td class="text-right"><b style="font-weight: 500;">SAR</b> ${result.Item.OrderProducts[0].InvoiceProductTaxAmount.toFixed(2)}</td>
                </tr>
                <tr>
                    <td colspan="6" class="text-right text-right-ar"><strong>${Langaugestore.TOTAL_AMOUNT} (Including VAT 15%) : </strong></td>
                    <td class="text-right"><b style="font-weight: 500;">SAR</b> ${result.Item.OrderProducts[0].InvoiceProductTotal.toFixed(2)}</td>
                </tr>
                                            
            `);

            $('#orderinvoiceloader').hide();
            dirAuto();
        }, error: function (error) {
            // Error function
            $('#orderinvoiceloader').hide();
        }
    });

    return false;
}

//<div class="col-sm-6 order-sm-0 invoice-invoicedto">
//    <strong>${Langaugestore.Suppler}:</strong>
//    <address>
//        ${result.Item.ShippingCustomerName}<br />
//        ${result.Item.ShippingAddressName}<br />
//        ${result.Item.ShippingCountryName}
//    </address>
//</div>