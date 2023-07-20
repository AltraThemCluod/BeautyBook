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

//Invoice Print 
var getInvoiceId = new URLSearchParams(window.location.search);
getInvoiceId = parseInt(atob(getInvoiceId.get('InvoiceId')));
var getInvoiceIdatob = ~~getInvoiceId;

function AppoinmentInvoice() {

    $('#orderinvoicelist').html(``);
    $('#orderinvoiceloader').show();
    debugger;
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/userAppointments/GlobalAppoinmentInvoice_ById?Id=${getInvoiceIdatob}`,
        headers: { 'Content-Type': 'application/json'},
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            console.log(result);
            debugger;

            //InvoiceHeader
            $('#InvoiceHeader').append(`

                <div class="row align-items-center justify-content-center">
                    <div class="col-sm-12 text-center">
                        <h4 class="text-7 mb-0" style="font-size: 20px;">${Langaugestore.Simplifed_tax_invoice}</h4>
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
                ${result.Item.ParentId > 0 ? `<div>
                                                  <p style="text-align:center;font-size: 20px;font-weight: 500;margin-top: 5px;">Return Invoice</p>
                                              </div>` : ''}
                
                <hr>
            `);

            //${Langaugestore.Invoice}
            if (result.Item.ParentId > 0) {
                $('#invoiceDateNo').append(`
                    <div class="col-sm-6 col-6 font-small">
                       <div><strong>${Langaugestore.Invoice_No} : </strong> <br/> #${result.Item.OldInvoiceNumber}</div>
                       <div class="mt-2"><strong>${Langaugestore.Invoice_Date} : </strong> <br/> ${result.Item.AppointmentDate}</div>
                       <div class="mt-2"><strong>${Langaugestore.Printing_Date} : </strong> <br/> ${result.Item.InvoicePrintDate}</div>
                    </div>
                    <div class="col-sm-6 col-6 text-right font-small">
                        <div><strong>Return Invoice No : </strong> <br/> #${result.Item.InvoiceNo}</div>
                        <div class="mt-2"><strong>${Langaugestore.Tax_No} : </strong> <br/> ${result.Item.SalonTaxNumber == "" || result.Item.SalonTaxNumber == null ? '' : result.Item.SalonTaxNumber}</div>
                    </div>
                `);
            } else {
                $('#invoiceDateNo').append(`
                    <div class="col-sm-6 col-6 font-small">
                       <div><strong>${Langaugestore.Invoice_Date} : </strong> <br/> ${result.Item.AppointmentDate}</div>
                       <div class="mt-2"><strong>${Langaugestore.Printing_Date} : </strong> <br/> ${result.Item.InvoicePrintDate}</div>
                    </div>
                    <div class="col-sm-6 col-6 text-right font-small">
                        <div><strong>${Langaugestore.Invoice_No} : </strong> <br/> #${result.Item.InvoiceNo}</div>
                        <div class="mt-2"><strong>${Langaugestore.Tax_No} : </strong> <br/> ${result.Item.SalonTaxNumber == "" || result.Item.SalonTaxNumber == null ? '' : result.Item.SalonTaxNumber}</div>
                    </div>
                `);
            }

            //invoiceAddressDetails
            $('#invoiceAddressDetails').append(`
                <div class="col-sm-12">
                    <strong>${Langaugestore.Salon_Details} : </strong>
                    <address>
                            <i class="bb-user text-gray-600 fs-16 mr-2"></i>${result.Item.SalonName == "" || result.Item.SalonName == null ? '' : result.Item.SalonName}<br />
                            <i class="bb-phone text-gray-600 fs-16 mr-2"></i>${result.Item.SalonPrimaryPhone == "" || result.Item.SalonPrimaryPhone == null ? '' : result.Item.SalonPrimaryPhone}<br />
                            <i class="bb-map-pin text-gray-600 fs-16 mr-2"></i>${result.Item.SalonAddressLine1 == "" || result.Item.SalonAddressLine1 == null ? '' : result.Item.SalonAddressLine1}
                    </address>
                </div>
                <div class="col-sm-12 text-right">
                    <strong>${Langaugestore.CustomerDetails} : </strong>
                    <address>
                        ${result.Item.CustomerFirstname == "" || result.Item.CustomerFirstname == null ? '' : result.Item.CustomerFirstname}
                        ${result.Item.CustomerSecondName == "" || result.Item.CustomerSecondName == null ? '' : result.Item.CustomerSecondName}<i class="bb-user text-gray-600 fs-16 ml-2"></i><br />
                        ${result.Item.CustomerPrimaryPhone == "" || result.Item.CustomerPrimaryPhone == null ? '' : result.Item.CustomerPrimaryPhone}<i class="bb-phone text-gray-600 fs-16 ml-2"></i><br />
                        ${result.Item.CustomerEmail == "" || result.Item.CustomerEmail == null ? '' : result.Item.CustomerEmail}<i class="bb-mail text-gray-600 fs-16 ml-2"></i><br />
                    </address>
                </div>
              
            `);

            //var demo = result.Item.ServicesIdsName.split(',').length;
            for (S = 0; S < result.Item.AppoinmentServices.length; S++) {
                $('#serviceListRow').append(`
                    <tr>
                        <td class="col-3 p-2">${result.Item.AppoinmentServices[S].Name}</td>
                        <td class="col-2 text-right p-2"><b style="font-weight: 500;">SAR</b> ${result.Item.AppoinmentServices[S].Price}</td>
                    </tr>
                `);
            }

            //Price
            $('#serviceListRow').append(`
                <tr>
                    <td class="text-right p-2"><strong>Total :</strong></td>
                    <td class="col-2 text-right p-2"><b style="font-weight: 500;">SAR</b> ${result.Item.Price}</td>
                </tr>
            `);

            //serviceListRow
            $('#serviceListRow').append(`
                <tr>
                    <td class="text-right p-2"><strong>${Langaugestore.VAT_15__} :</strong></td>
                    <td class="col-2 text-right p-2"><b style="font-weight: 500;">SAR</b> ${result.Item.Tax}</td>
                </tr>
            `);

            //serviceListRow
            $('#serviceListRow').append(`
                <tr>
                    <td class="text-right p-2"><strong>${Langaugestore.Total_Including_tax} :</strong></td>
                    <td class="col-2 text-right p-2"><b style="font-weight: 500;">SAR</b> ${result.Item.ServicesTotalPrice}</td>
                </tr>
            `);

            //QRCode_Area
            let website = UrlEndPoint + "AppointmentInvoice/Index?InvoiceId=" + btoa(result.Item.Id);
            console.log(website);
            if (website) {
                let qrcodeContainer = document.getElementById("qrcode");
                qrcodeContainer.innerHTML = "";
                new QRCode(qrcodeContainer, website);
            } else {
                alert("Please enter a valid URL");
            }


            $('#orderinvoiceloader').hide();

        }, error: function (error) {
            // Error function
            $('#orderinvoiceloader').hide();
        }
    });

    return false;
}