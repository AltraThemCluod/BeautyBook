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


function PosInvoice(invoiceId) {

    debugger;
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/pOSPayment/PosInvoice_ById?Id=${atob(invoiceId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            console.log(result);
            debugger;


            //POS invoice ZATCA Qr code image set
            if (result.Item.QrCodeURL != null || result.Item.QrCodeURL != "") {
                $("#qrcodeUrl").attr("src", APIEndPoint + "/SimplifiedPOSInvoice/" + result.Item.QrCodeURL);
            } else {
                $("#qrcodeUrl").hide();
            }

            //InvoiceHeader
            $('#InvoiceHeader').append(`

                <div class="row align-items-center justify-content-center">
                    <div class="col-sm-12 text-center">
                        <h4 class="text-7 mb-0" style="font-size: 20px;">Pos Invoice</h4>
                    </div>
                </div>

                 <div class="row align-items-center justify-content-center mt-3">
                    <div class="col-sm-6 col-7 text-center text-sm-left mb-3 mb-sm-0 d-flex justify-content-center">
                        ${result.Item.SalonLogoUrl == "" || result.Item.SalonLogoUrl == null ?
                    `<img id="logo" src="../Content/assets/images/svg-icons/salon-shop.svg" title="Koice" alt="Salon Shop">` :
                    `<img id="logo" class="w-100" src="${APIEndPoint}/${result.Item.SalonLogoUrl}" title="${result.Item.SalonName}" alt="${result.Item.SalonName}">`
                }
                    </div>
                </div>
                <hr>
            `);

            //${Langaugestore.Invoice}
            $('#invoiceDateNo').append(`
                <div class="col-sm-12 col-12 font-small">
                    <div><strong>${Langaugestore.Invoice_Date} : </strong> <br/> ${result.Item.CreatedDateStr}</div>
                </div>
                <div class="col-sm-6 col-6 font-small">
                    <div class="mt-2"><strong>${Langaugestore.Invoice_No} : </strong> <br/> #${result.Item.PosInvoiceNumber}</div>
                </div>
                <div class="col-sm-6 col-6 text-right font-small">
                    <div class="mt-2"><strong>${Langaugestore.Tax_No} : </strong> <br/> ${result.Item.TaxNumber == "" || result.Item.TaxNumber == null ? '' : result.Item.TaxNumber}</div>
                </div>
            `);

            //invoiceAddressDetails
            $('#invoiceAddressDetails').append(`
                <div class="col-sm-12">
                    <strong>${Langaugestore.Salon_Details} : </strong>
                    <address>
                            <i class="bb-user text-gray-600 fs-16 mr-2"></i>${result.Item.SalonName == "" || result.Item.SalonName == null ? '' : result.Item.SalonName}<br />
                            <i class="bb-phone text-gray-600 fs-16 mr-2"></i>${result.Item.SalonPrimaryPhone == "" || result.Item.SalonPrimaryPhone == null ? '' : result.Item.SalonPrimaryPhone}<br />
                            <i class="bb-map-pin text-gray-600 fs-16 mr-2"></i>${result.Item.AddressLine1 == "" || result.Item.AddressLine1 == null ? '' : result.Item.AddressLine1}
                    </address>
                </div>
                <div class="col-sm-12 text-right">
                    <strong>${Langaugestore.CustomerDetails} : </strong>
                    <address>
                        ${result.Item.CustomerName == "" || result.Item.CustomerName == null ? '' : result.Item.CustomerName}<i class="bb-user text-gray-600 fs-16 ml-2"></i><br />
                        ${result.Item.CustomerPrimaryPhone == "" || result.Item.CustomerPrimaryPhone == null ? '' : result.Item.CustomerPrimaryPhone}<i class="bb-phone text-gray-600 fs-16 ml-2"></i><br />
                        ${result.Item.CustomerEmail == "" || result.Item.CustomerEmail == null ? '' : result.Item.CustomerEmail}<i class="bb-mail text-gray-600 fs-16 ml-2"></i><br />
                    </address>
                </div>
            `);

            for (S = 0; S < result.Item.PosInvoiceOrder.length; S++) {
                $('#serviceListRow').append(`
                    <tr>
                        <td class="col-3 p-2">${result.Item.PosInvoiceOrder[S].Name}</td>
                        <td class="col-2 text-right p-2"><b style="font-weight: 500;">SAR</b> ${result.Item.PosInvoiceOrder[S].Price == "" || result.Item.PosInvoiceOrder[S].Price == null ? "0.00" : result.Item.PosInvoiceOrder[S].Price.toFixed(2)}</td>
                    </tr>
                `);
            }

            $('#serviceListRow').append(`
                <tr>
                    <td class="text-right p-2"><strong>Total :</strong></td>
                    <td class="col-2 text-right p-2"><b style="font-weight: 500;">SAR</b> ${result.Item.SubTotal.toFixed(2)}</td>
                </tr>
            `);

            $('#serviceListRow').append(`
                <tr>
                    <td class="text-right p-2"><strong>Discount Sales(${result.Item.DiscountSales}%) :</strong></td>
                    <td class="col-2 text-right p-2"><b style="font-weight: 500;">SAR</b> ${result.Item.DiscountSalesAmount.toFixed(2)}</td>
                </tr>
            `);

            $('#serviceListRow').append(`
                <tr>
                    <td class="text-right p-2"><strong>${Langaugestore.VAT_15__} :</strong></td>
                    <td class="col-2 text-right p-2"><b style="font-weight: 500;">SAR</b> ${result.Item.TotalSalesTax.toFixed(2)}</td>
                </tr>
            `);

            $('#serviceListRow').append(`
                <tr>
                    <td class="text-right p-2"><strong>${Langaugestore.Total_Including_tax} :</strong></td>
                    <td class="col-2 text-right p-2"><b style="font-weight: 500;">SAR</b> ${result.Item.TotalAmount.toFixed(2)}</td>
                </tr>
            `);

        }, error: function (error) {
           
        }
    });

    return false;
}