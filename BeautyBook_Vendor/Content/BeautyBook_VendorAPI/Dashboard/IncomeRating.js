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
var vendorId = getCookie("VendorId");

function VendorSalesAndRating() {
    debugger;
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `api/salonVendorDashboard/VendorSalesAndRating_VendorId?VendorId=${atob(vendorId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            console.log('result', result);

            if (result.Item != null && result.Item != "") {
                $('#incomeAmmount').text(result.Item.TotalSales.toFixed(2));
                $('#vendorAveRatting').text(result.Item.AverageRating);
            }

            
        }, error: function (error) {
            //Error function
        }
    });
}



function VendorTopSalons() {
    debugger;
    let vendorTopSalons = new Object();
    vendorTopSalons.IsPageProvided = true;

    var demo = Langaugestore.SAR;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/salonVendorDashboard/VendorTopCustomer_All?VendorId=${atob(vendorId)}&FromDate=&ToDate=&Type=1`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(vendorTopSalons),
        crossDomain: true,
        async: false,
        success: function (result) {
            debugger;
            if (result.Values.length > 0) {
                for (i = 0; i < result.Values.length; i++) {
                    $('#VendorTopCustomerData').append(`
                         <tbody class="mb-3" style="vertical-align: top;">
                            <tr>
                                <th class="pb-3" scope="row">
                                    <div class="d-flex align-items-center flex-grow-0">
                                        <span class="avatar avatar-primary avatar-circle ">
                                            <span class="avatar-initials">${result.Values[i].SalonName.charAt(0)}</span>
                                        </span>
                                        <div class="ml-2 mr-2">
                                            <h6 class="mb-0">${result.Values[i].SalonName}</h6>
                                            <span class="font-weight-normal fs-13">Salon</span>
                                        </div>
                                    </div>
                                </th>
                                <th><span class="font-weight-normal fs-13 ml-3">${result.Values[i].SalonOrderCount} ${Langaugestore.Orders}</span></th>
                                <th><span class="font-weight-normal fs-13 ml-3">${Langaugestore.SAR} ${result.Values[i].TotalSales.toFixed(2)}</span></th>
                            </tr>
                        </tbody>
                    `);
                } 
            }
            else {
                $('#VendorTopCustomerData').append(`<p class="mb-0 w-100 text-center">Customer not found</p>`)
            }

            $('.selectpicker').selectpicker("refresh");

        }, error: function (error) {

        }
    });
    return false;
}



function VendorTopProduct() {
    debugger;
    let vendorTopProduct = new Object();
    vendorTopProduct.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/salonVendorDashboard/VendorTopProduct_All?VendorId=${atob(vendorId)}&FromDate=&ToDate=&Type=1`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(vendorTopProduct),
        crossDomain: true,
        async: false,
        success: function (result) {
            debugger;
            if (result.Values.length > 0) {
                for (i = 0; i < result.Values.length; i++) {
                    $('#VendorTopProduct').append(`
                          <div class="col-lg-6 p-2">
                            <div class="media top-product-card">
                                <div class="avatar avatar-xl border overflow-hidden mr-3">
                                    <img onerror="this.src='/Content/assets/images/DefaultProduct.jpg'" class="img-fluid" src="${APIEndPoint}/${result.Values[i].ProductThumbnailImage}" alt="Image Description">
                                </div>
                                <div class="media-body">
                                    <div class="row">
                                        <div class="col-md-12 mb-3 mb-md-0">
                                            <a class="fs-16 link font-weight-medium d-inline-block" href="javascript:void(0)">
                                                ${result.Values[i].ProductName}
                                            </a>
                                            <span class="badge float-right badge-primary d-inline-flex align-items-center align-middle mr-1">
                                                <span>${result.Values[i].ProductAverageRating}</span>
                                                <svg xmlns="http://www.w3.org/2000/svg" height="14" width="14" viewBox="0 0 20 20" fill="currentColor">
                                                    <path d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path>
                                                </svg>
                                            </span>
                                            <div class="product_price">
                                                <span class="price">SAR ${result.Values[i].OfferPrice != null && result.Values[i].OfferPrice > 0 ? `${result.Values[i].OfferPrice}` : `${result.Values[i].Price}`}</span>
                                                ${result.Values[i].OfferPrice != null && result.Values[i].OfferPrice > 0 ? `<del>SAR ${result.Values[i].Price}</del>` : ``}
                                                ${result.Values[i].OfferPrice != null && result.Values[i].OfferPrice > 0 ? `<div class="on_sale"><span>${result.Values[i].OfferPercentage}% off</span></div>` : ``}
                                            </div>
                                            <div class="d-flex align-items-center justify-content-between">
                                                <div class="rating_wrap">
                                                        <div class="Stars" style="--rating:${result.Values[i].ProductAverageRating == null || result.Values[i].ProductAverageRating == "" ? 0 : result.Values[i].ProductAverageRating};"></div>
                                                    <span class="rating_num">(${result.Values[i].ProductStarCount})</span>
                                                </div>
                                                <h5 class="font-weight-medium fs-14 mb-0">${result.Values[i].SalesCount} Sales</h5>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    `);
                } 
            }
            else {
                $('#VendorTopProduct').append(`<p class="mb-0 w-100 text-center">Product not found</p>`)
            }

            $('.selectpicker').selectpicker("refresh");

        }, error: function (error) {

        }
    });
    return false;
}
