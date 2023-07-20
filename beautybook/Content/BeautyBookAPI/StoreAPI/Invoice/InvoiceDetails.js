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

//Supplier company address old code
//13/03/2023
//<address>
//    ${result.Values[i].Supplername == "" || result.Values[i].Supplername == null ? "" : result.Values[i].Supplername}<br />
//    ${result.Values[i].SupplerAddress == "" || result.Values[i].SupplerAddress == null ? "" : result.Values[i].SupplerAddress}<br />
//    ${result.Values[i].SupplerCountryName == "" || result.Values[i].SupplerCountryName == null ? "" : result.Values[i].SupplerCountryName}
//</address>


function VendorTypeInvoiceDetails() {
     
    $('#orderinvoicelist').html(``);
    $('#orderinvoiceloader').show();

    let InvoiceDetails = new Object();
    InvoiceDetails.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/orders/OrderInvoiceMultipalVendor_All?OrderId=${OrderUrlatob == 0 ? atob(ordrId) : OrderUrlatob}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(InvoiceDetails),
        success: function (result) {
            console.log(result);    
            debugger;

            
            for (var i = 0; i < result.Values.length; i++) {
                var BuyerAddress = "";
                var OrderProductData = "";

                //Buyer address data show
                for (var s = 0; s < result.Values[i].SalonObject.length; s++) {
                    BuyerAddress += `<table class="w-100 border-none" >
                                        <tr>
                                            <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">Buyer Name</b></td>
                                            <td class="p-1 pl-2 pr-2 col-8 border-right-none"><span>${result.Values[i].SalonObject[s].CustomerName == "" || result.Values[i].SalonObject[s].CustomerName == null ? "-" : result.Values[i].SalonObject[s].CustomerName}</span></td>
                                        </tr>
                                        <tr>
                                            <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">Address</b></td>
                                            <td class="p-1 pl-2 pr-2 col-8 border-right-none">
                                                <span>
                                                    ${result.Values[i].SalonObject[s].SalonAddressName == "" || result.Values[i].SalonObject[s].SalonAddressName == null ? "-" : result.Values[i].SalonObject[s].SalonAddressName} <br/>
                                                    ${result.Values[i].SalonObject[s].SalonCountryName == "" || result.Values[i].SalonObject[s].SalonCountryName == null ? "-" : result.Values[i].SalonObject[s].SalonCountryName},
                                                    ${result.Values[i].SalonObject[s].SalonStateName == "" || result.Values[i].SalonObject[s].SalonStateName == null ? "-" : result.Values[i].SalonObject[s].SalonStateName},
                                                    ${result.Values[i].SalonObject[s].SalonCityName == "" || result.Values[i].SalonObject[s].SalonCityName == null ? "-" : result.Values[i].SalonObject[s].SalonCityName},
                                                    ${result.Values[i].SalonObject[s].SalonZipCode == "" || result.Values[i].SalonObject[s].SalonZipCode == null ? "" : result.Values[i].SalonObject[s].SalonZipCode}
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">Commercial Register No</b></td>
                                            <td class="p-1 pl-2 pr-2 col-8 border-right-none"><span>${result.Values[i].SalonObject[s].CommercialRegisterNo == "" || result.Values[i].SalonObject[s].CommercialRegisterNo == null ? "-" : result.Values[i].SalonObject[s].CommercialRegisterNo}</span></td>
                                        </tr>
                                        <tr>
                                            <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">VAT Number</b></td>
                                            <td class="p-1 pl-2 pr-2 col-8 border-right-none"><span>${result.Values[i].SalonObject[s].SalonVATNumber == "" || result.Values[i].SalonObject[s].SalonVATNumber == null ? "-" : result.Values[i].SalonObject[s].SalonVATNumber}</span></td>
                                        </tr>
                                    </table>`
                }

                //Order Product Data show
                for (var OrderProduct = 0; OrderProduct < result.Values[i].OrderObject.length; OrderProduct++) {
                        OrderProductData += `<tr>
                                                <td>${result.Values[i].OrderObject[OrderProduct].ProductName}</td>
                                                <td>${result.Values[i].OrderObject[OrderProduct].InvoiceUnitePrice.toFixed(2)}</td>
                                                <td>15%</td>
                                                <td>${result.Values[i].OrderObject[OrderProduct].InvoiceTaxPrice.toFixed(2)}</td>
                                                <td>${result.Values[i].OrderObject[OrderProduct].InvoiceProductRate.toFixed(2)}</td>
                                                <td>${result.Values[i].OrderObject[OrderProduct].Qty}</td>
                                                <td class="text-right">${result.Values[i].OrderObject[OrderProduct].InvoiceProductAmount.toFixed(2)}</td>
                                            </tr>`
                }

                $("#VendorTypeInvoiceDetails").append(`
                    <div class="card p-4 mb-3" id="Invoice_${i}">
                        <div class="row m-0 w-100">
                            <div class="col-lg-12 text-center">
                                <h4 class="text-7 mb-0">${Langaugestore.Tax_Invoice}</h4>
                            </div>
                        </div>

                        <div class="invoice-header mt-5">
                            <div class="row align-items-center">
                                <div class="col-sm-7 text-center text-sm-left mb-3 mb-sm-0">
                                    <img id="logo" src="../Content/assets/images/logo.png" title="BeautyBook" alt="BeautyBook" />
                                </div>
                                <div class="col-sm-5 text-center text-sm-right">
                                    <h4 class="text-7 mb-0">${Langaugestore.Invoice}</h4>
                                </div>
                            </div>
                            <hr>
                        </div>

                        <div class="invoice-main">
                            <div class="row" id="invoicedetails">
                                <div class="col-sm-6 text-ar-right">
                                    <div><strong>${Langaugestore.Order_Date} : </strong> <span class="fs-14">${result.Values[i].OrderDate}</span></div>
                                    <div class="mt-1"><strong>${Langaugestore.Printing_Date} : </strong> <span class="fs-14">${result.Values[i].InvoicePrintDate}</span></div>
                                </div>

                                <div class="col-sm-6 text-sm-right text-ar-left"> <strong>${Langaugestore.Invoice_No} : </strong> <span class="fs-14">${result.Values[i].InvoiceNo}</span></div>
                            </div>
                            <hr>
                            <div class="row fs-14 m-0" id="invoiceAddressDetails">
                                <div class="w-100 mb-2">
                                    <div class="card-body p-0">
                                        <div class="table-card table-responsive border-none">
                                            <table class="table fs-14 mb-0 border" id="address_formate">
                                                <thead>
                                                    <tr>
                                                        <th class="col-6 p-2">Supplier</th>
                                                        <th class="col-6 p-2">Buyer</th>
                                                    </tr>
                                                </thead>
                                                <tbody class="order-inovice-list">
                                                    <tr>
                                                        <td class="p-0" id="supplier_address">
                                                            <table class="w-100 border-none">
                                                                <tr>
                                                                    <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">Seller Name</b></td>
                                                                    <td class="p-1 pl-2 pr-2 col-8 border-right-none"><span>${result.Values[i].VendorCompanyName == "" || result.Values[i].VendorCompanyName == null ? "" : result.Values[i].VendorCompanyName}</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">Address</b></td>
                                                                    <td class="p-1 pl-2 pr-2 col-8 border-right-none">
                                                                        <span>
                                                                            ${result.Values[i].SupplerAddress == "" || result.Values[i].SupplerAddress == null ? "" : result.Values[i].SupplerAddress} <br/>
                                                                            ${result.Values[i].SupplerCountryName == "" || result.Values[i].SupplerCountryName == null ? "" : result.Values[i].SupplerCountryName},
                                                                            ${result.Values[i].SupplerStateName == "" || result.Values[i].SupplerStateName == null ? "" : result.Values[i].SupplerStateName},
                                                                            ${result.Values[i].SupplerCityName == "" || result.Values[i].SupplerCityName == null ? "" : result.Values[i].SupplerCityName},
                                                                            ${result.Values[i].SupplerZipCode == "" || result.Values[i].SupplerZipCode == null ? "" : result.Values[i].SupplerZipCode}
                                                                        </span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">Commercial Register No</b></td>
                                                                    <td class="p-1 pl-2 pr-2 col-8 border-right-none"><span>${result.Values[i].CommercialRegisterNumber == "" || result.Values[i].CommercialRegisterNumber == null ? "" : result.Values[i].CommercialRegisterNumber}</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">VAT Number</b></td>
                                                                    <td class="p-1 pl-2 pr-2 col-8 border-right-none"><span>${result.Values[i].VATNumber == "" || result.Values[i].VATNumber == null ? "" : result.Values[i].VATNumber}</span></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td class="p-0" id="buyer_address">
                                                            ${BuyerAddress}
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-body p-0">
                                    <div class="table-card table-responsive">
                                        <table class="table fs-14 mb-0">
                                            <thead>
                                                <tr>
                                                    <th class="col-3">${Langaugestore.ProductName}</th>
                                                    <th class="col-2">${Langaugestore.UnitePrice}</th>
                                                    <th class="col-2">Tax Rate</th>
                                                    <th class="col-2">${Langaugestore.TaxAmount}</th>
                                                    <th class="col-2">${Langaugestore.Rate}</th>
                                                    <th class="col-1">${Langaugestore.QTY}</th>
                                                    <th class="col-2 text-right">${Langaugestore.Amount}</th>
                                                </tr>
                                            </thead>
                                            <tbody class="order-inovice-list" id="orderinvoicelist">
                                                ${OrderProductData}
                                                <tr>
                                                    <td colspan="6" class="text-right text-right-ar"><strong>${Langaugestore.Total__excluding_VAT_} : </strong></td>
                                                    <td class="text-right"><b style="font-weight: 500;">SAR</b> ${result.Values[i].OrderObject[0].ProductSubTotal.toFixed(2)}</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" class="text-right text-right-ar"><strong>${Langaugestore.Discount} : </strong></td>
                                                    <td class="text-right"><b style="font-weight: 500;">SAR</b> ${result.Values[i].OrderObject[0].InvoiceProductDiscount.toFixed(2)}</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" class="text-right text-right-ar"><strong>${Langaugestore.Total_Taxable_amount__excluding_VAT_} : </strong></td>
                                                    <td class="text-right"><b style="font-weight: 500;">SAR</b> ${result.Values[i].OrderObject[0].InvoiceProductSubTotalWithDiscount.toFixed(2)}</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" class="text-right text-right-ar"><strong>${Langaugestore.Total_VAT} (15%) : </strong></td>
                                                    <td class="text-right"><b style="font-weight: 500;">SAR</b> ${result.Values[i].OrderObject[0].InvoiceProductTaxAmount.toFixed(2)}</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" class="text-right text-right-ar"><strong>${Langaugestore.TOTAL_AMOUNT} (Including VAT 15%) : </strong></td>
                                                    <td class="text-right"><b style="font-weight: 500;">SAR</b> ${result.Values[i].OrderObject[0].InvoiceProductTotal.toFixed(2)}</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="invoice-footer mt-3 text-center">
                            <p class="text-1">${Langaugestore.InvoiceNote}</p>
                            <div class="btn-group  d-print-none">
                                <a href="javascript:void(0)" onclick="PrintThisInovice(${i})" class="btn btn-light border">
                                    <i class="bb-printer fs-14 mr-2"></i> ${Langaugestore.Print}
                                </a>
                            </div>
                        </div>
                    </div>
                `);
            }

            dirAuto();
        }, error: function (error) {
            // Error function
            $('#orderinvoiceloader').hide();
        }
    });

    return false;
}


//Print Data
function PrintThisInovice(InvoiceKey) {
    debugger;
    printElement(document.getElementById(`Invoice_${InvoiceKey}`));
}

function printElement(elem) {
    var domClone = elem.cloneNode(true);

    var $printSection = document.getElementById("printSection");

    if (!$printSection) {
        var $printSection = document.createElement("div");
        $printSection.id = "printSection";
        document.body.appendChild($printSection);
    }

    $printSection.innerHTML = "";
    $printSection.appendChild(domClone);
    window.print();
}

//<div class="col-sm-6 order-sm-0 invoice-invoicedto">
//    <strong>${Langaugestore.Suppler}:</strong>
//    <address>
//        ${result.Item.ShippingCustomerName}<br />
//        ${result.Item.ShippingAddressName}<br />
//        ${result.Item.ShippingCountryName}
//    </address>
//</div>