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

function salonDetailsInOrderDetails(SalonId) {
    $('#salonformloader').show();
    $('#salondetails').html(``);
    $('#salonprofile').html(``);
    $('#customerModal').modal('show');
    
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + `/api/salons/Salons_ById?Id=${SalonId}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {

                $('#salondetails').append(`
                    <div class="col-lg-12 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Name}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.SalonName}</span>
                                </div>
                            </div>
                            <div class="col-lg-12 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Address}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.AddressLine1}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Primary_Phone}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.UserEmail}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Email}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.UserPrimaryPhone}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Alternate_Phone}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.UserAlternatePhone}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Customer_Since}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.CustomerSince}</span>
                                </div>
                            </div>

                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.TotalOrders}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.TotalOrders}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.LastOrder}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.LastOrder}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.SALES}</div>
                                <div>
                                    <span class="font-weight-medium">SAR ${result.Item.TotalSales}</span>
                                </div>
                            </div>
                `);
            $('#salonprofile').append(`
                   <div class="employee-photo-view-mode">
                  ${result.Item.LogoUrl == "" || result.Item.LogoUrl == null ?
                    `<img class="img-fluid rounded shadow-sm" src="~/Content/assets/images/svg-icons/salon-shop.svg" alt="salon-shop">`
                    :
                `<img src="${APIEndPoint}/${result.Item.LogoUrl}" class="img-fluid rounded shadow-sm" alt="salon-shop">
`
                                    }
                        </div>
                `);
            $('#salonformloader').hide();

        }
    });
    return false;
}