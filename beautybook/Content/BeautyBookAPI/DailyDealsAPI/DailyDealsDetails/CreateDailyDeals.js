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

let date = new Date();

let year = date.getFullYear();
let month = (1 + date.getMonth()).toString().padStart(2, '0');
let day = date.getDate().toString().padStart(2, '0');

$('#offerDate').val(day + '/' + month + '/' + year)

function addDailyDealsDetails() {
    
    $('#CreateDailyDeals').hide();
    $('#CreateDailyDealsloading').show();

    var DailyDeals = new Object();
    DailyDeals.Id = 0;
    DailyDeals.SalonId = atob(SalonId);
    DailyDeals.OfferDate = $('#offerDate').val();
    DailyDeals.StartTime = $('#startTime').val();
    DailyDeals.EndTime = $('#endTime').val()
   

    //Service Offer Price
    var SlectedListPrice = new Array();
    var SlectedListServiceId = new Array();
    $("input.selectedvalue").each(function () {
        if ($(this).val() != "") {
            SlectedListPrice.push($(this).val());
            SlectedListServiceId.push($(this).data('service'));
        }
    });
    DailyDeals.ServicesIds = SlectedListServiceId.toString();
    DailyDeals.ServiceOfferPrice = SlectedListPrice.toString();

    //ServicePackages Offer Price
    var SlectedPackageOfferPrice = new Array();
    var SlectedPackageId = new Array();
    $("input.PackageOffersPrice").each(function () {
        if ($(this).val() != "") {
            SlectedPackageOfferPrice.push($(this).val());
            SlectedPackageId.push($(this).data('service'));
        }
    });
    DailyDeals.PackagesIds = SlectedPackageId.toString();
    DailyDeals.PackagesOfferPrice = SlectedPackageOfferPrice.toString();

    DailyDeals.CreatedBy = (atob(UserId));

    
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/dailyDeals/DailyDeals_Upsert',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(DailyDeals),
        success: function (result) {
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 2000
                })
                setTimeout(function () {
                    window.location = '/DailyDeals/ManageDailyDeals';
                }, 2000);
            }
            $('#CreateDailyDeals').show();
            $('#CreateDailyDealsloading').hide();
        },

        error: function (error) {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 2000
            })
            $('#CreateDailyDeals').show();
            $('#CreateDailyDealsloading').hide();
        }
    });
    return false;
}

