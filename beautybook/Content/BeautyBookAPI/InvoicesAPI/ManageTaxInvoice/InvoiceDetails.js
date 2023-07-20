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
var VendorId = getCookie("VendorId");
var salonId = getCookie("SalonId");

var getSalonId = atob(salonId);
var getSalonIdatob = ~~getSalonId;
 
var ordrId = getCookie("OrderId");

var OrderUrl = new URLSearchParams(window.location.search);
OrderUrl = parseInt(atob(OrderUrl.get('OrderId')));
var OrderUrlatob = ~~OrderUrl;



function VendorTypeInvoiceDetails() {
    $('#orderinvoicelist').html(``);
    $('#orderinvoiceloader').show();
    debugger;
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/orders/OrderInvoice_ById?Id=${OrderUrlatob}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            console.log(result);    
            debugger;

            
            if (result.Item != null) {
                var BuyerAddress = "";
                var OrderProductData = "";

                //Buyer address data show
                for (var s = 0; s < result.Item.OrderSalonData.length; s++) {
                    BuyerAddress += `<table class="w-100 border-none" >
                                        <tr>
                                            <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">Buyer Name</b></td>
                                            <td class="p-1 pl-2 pr-2 col-8 border-right-none"><span>${result.Item.OrderSalonData[s].CustomerName == "" || result.Item.OrderSalonData[s].CustomerName == null ? "-" : result.Item.OrderSalonData[s].CustomerName}</span></td>
                                        </tr>
                                        <tr>
                                            <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">Address</b></td>
                                            <td class="p-1 pl-2 pr-2 col-8 border-right-none">
                                                <span>
                                                    ${result.Item.OrderSalonData[s].SalonAddressName == "" || result.Item.OrderSalonData[s].SalonAddressName == null ? "-" : result.Item.OrderSalonData[s].SalonAddressName} <br/>
                                                    ${result.Item.OrderSalonData[s].SalonCountryName == "" || result.Item.OrderSalonData[s].SalonCountryName == null ? "-" : result.Item.OrderSalonData[s].SalonCountryName},
                                                    ${result.Item.OrderSalonData[s].SalonStateName == "" || result.Item.OrderSalonData[s].SalonStateName == null ? "-" : result.Item.OrderSalonData[s].SalonStateName},
                                                    ${result.Item.OrderSalonData[s].SalonCityName == "" || result.Item.OrderSalonData[s].SalonCityName == null ? "-" : result.Item.OrderSalonData[s].SalonCityName},
                                                    ${result.Item.OrderSalonData[s].SalonZipCode == "" || result.Item.OrderSalonData[s].SalonZipCode == null ? "" : result.Item.OrderSalonData[s].SalonZipCode}
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">Commercial Register No</b></td>
                                            <td class="p-1 pl-2 pr-2 col-8 border-right-none"><span>${result.Item.OrderSalonData[s].CommercialRegisterNo == "" || result.Item.OrderSalonData[s].CommercialRegisterNo == null ? "-" : result.Item.OrderSalonData[s].CommercialRegisterNo}</span></td>
                                        </tr>
                                        <tr>
                                            <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">VAT Number</b></td>
                                            <td class="p-1 pl-2 pr-2 col-8 border-right-none"><span>${result.Item.OrderSalonData[s].SalonVATNumber == "" || result.Item.OrderSalonData[s].SalonVATNumber == null ? "-" : result.Item.OrderSalonData[s].SalonVATNumber}</span></td>
                                        </tr>
                                    </table>`
                }

                //Order Product Data show
                for (var OrderProduct = 0; OrderProduct < result.Item.OrderItemData.length; OrderProduct++) {
                    OrderProductData += `<tr>
                                            <td>${result.Item.OrderItemData[OrderProduct].ProductName}</td>
                                            <td>${result.Item.OrderItemData[OrderProduct].InvoiceUnitePrice.toFixed(2)}</td>
                                            <td>15 %</td>
                                            <td>${result.Item.OrderItemData[OrderProduct].InvoiceTaxPrice.toFixed(2)}</td>
                                            <td>${result.Item.OrderItemData[OrderProduct].InvoiceProductRate.toFixed(2)}</td>
                                            <td>${result.Item.OrderItemData[OrderProduct].Qty}</td>
                                            <td class="text-right">${result.Item.OrderItemData[OrderProduct].InvoiceProductAmount.toFixed(2)}</td>
                                        </tr>`
                }

                //Parent id condition invoice no and return invoice number
                var InvoiceNumber = "";

                if (result.Item.ParentId > 0) {
                    InvoiceNumber += `<div><strong>${Langaugestore.Invoice_No} : </strong> <span class="fs-14">#${result.Item.OldInvoiceNumber}</span></div>
                                    <div class="mt-1"><strong>Return Invoice No : </strong> <span class="fs-14">#${result.Item.InvoiceNo}</span></div>`
                } else {
                    InvoiceNumber += `<div><strong>${Langaugestore.Invoice_No} : </strong> <span class="fs-14">#${result.Item.InvoiceNo}</span></div>`
                }

                

                $("#VendorTypeInvoiceDetails").append(`
                    <div class="card p-4 mb-3">
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
                                    ${result.Item.ParentId > 0 ? '<h4>Return Invoice</h4>' : `<h4 class="text-7 mb-0">${Langaugestore.Invoice}</h4>`}
                                    
                                </div>
                            </div>
                            <hr>
                        </div>

                        <div class="invoice-main">
                            <div class="row" id="invoicedetails">
                                <div class="col-sm-6 text-ar-right">
                                    <div><strong>${Langaugestore.Order_Date} : </strong> <span class="fs-14">${result.Item.OrderDate}</span></div>
                                    <div class="mt-1"><strong>${Langaugestore.Printing_Date} : </strong> <span class="fs-14">${result.Item.InvoicePrintDate}</span></div>
                                </div>

                                <div class="col-sm-6 text-sm-right text-ar-left">
                                    ${InvoiceNumber}
                                </div>
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
                                                                    <td class="p-1 pl-2 pr-2 col-8 border-right-none"><span>${result.Item.VendorCompanyName == "" || result.Item.VendorCompanyName == null ? "" : result.Item.VendorCompanyName}</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">Address</b></td>
                                                                    <td class="p-1 pl-2 pr-2 col-8 border-right-none">
                                                                        <span>
                                                                            ${result.Item.SupplerAddress == "" || result.Item.SupplerAddress == null ? "" : result.Item.SupplerAddress} <br/>
                                                                            ${result.Item.SupplerCountryName == "" || result.Item.SupplerCountryName == null ? "" : result.Item.SupplerCountryName},
                                                                            ${result.Item.SupplerStateName == "" || result.Item.SupplerStateName == null ? "" : result.Item.SupplerStateName},
                                                                            ${result.Item.SupplerCityName == "" || result.Item.SupplerCityName == null ? "" : result.Item.SupplerCityName},
                                                                            ${result.Item.SupplerZipCode == "" || result.Item.SupplerZipCode == null ? "" : result.Item.SupplerZipCode}
                                                                        </span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">Commercial Register No</b></td>
                                                                    <td class="p-1 pl-2 pr-2 col-8 border-right-none"><span>${result.Item.CommercialRegisterNumber == "" || result.Item.CommercialRegisterNumber == null ? "" : result.Item.CommercialRegisterNumber}</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="p-1 pl-2 pr-2 col-4 border-left-none"><b class="text-muted">VAT Number</b></td>
                                                                    <td class="p-1 pl-2 pr-2 col-8 border-right-none"><span>${result.Item.VATNumber == "" || result.Item.VATNumber == null ? "" : result.Item.VATNumber}</span></td>
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
                                                    <td class="text-right"><b style="font-weight: 500;">SAR</b> ${result.Item.OrderItemData[0].ProductSubTotal.toFixed(2)}</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" class="text-right text-right-ar"><strong>${Langaugestore.Discount} : </strong></td>
                                                    <td class="text-right"><b style="font-weight: 500;">SAR</b> ${result.Item.OrderItemData[0].InvoiceProductDiscount.toFixed(2)}</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" class="text-right text-right-ar"><strong>${Langaugestore.Total_Taxable_amount__excluding_VAT_} : </strong></td>
                                                    <td class="text-right"><b style="font-weight: 500;">SAR</b> ${result.Item.OrderItemData[0].InvoiceProductSubTotalWithDiscount.toFixed(2)}</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" class="text-right text-right-ar"><strong>${Langaugestore.Total_VAT} (15%): </strong></td>
                                                    <td class="text-right"><b style="font-weight: 500;">SAR</b> ${result.Item.OrderItemData[0].InvoiceProductTaxAmount.toFixed(2)}</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" class="text-right text-right-ar"><strong>${Langaugestore.TOTAL_AMOUNT} (Including VAT 15%): </strong></td>
                                                    <td class="text-right"><b style="font-weight: 500;">SAR</b> ${result.Item.OrderItemData[0].InvoiceProductTotal.toFixed(2)}</td>
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
                                <a href="javascript:window.print()" class="btn btn-light border">
                                    <i class="bb-printer fs-14 mr-2"></i>  ${Langaugestore.Print}
                                </a>
                            </div>
                        </div>
                    </div>
                `);
            }
            $('#orderinvoiceloader').hide();
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