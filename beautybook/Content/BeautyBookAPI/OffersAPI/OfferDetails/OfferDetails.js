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

 
function addOfferDetails() {
     
    $('#addMarketingDetailsbtn').hide();
    $('#saveSalonsdetailsbtntext').show();

    $('#emptyofferpricevalidation').hide();
    $('#offerpricevalidation').hide();

    var Offer= new Object();
    Offer.Id = $('#Id').val();
    Offer.SalonId = atob(SalonId);
    Offer.OfferName = $('#offerName').val();
    Offer.ApplyOn = $('input[name="applyOn"]:checked').val();

    let OfferPeriod = $('#offerPeriod').val();
    const OfferPeriodArr = OfferPeriod.split("-");
    Offer.OfferPeriodStart = OfferPeriodArr[0];
    Offer.OfferPeriodEnd = OfferPeriodArr[1];

    Offer.AgeBetweenMinYear = $('#minageyear').val();
    Offer.AgeBetweenMaxYear = $('#maxageyear').val();

    //Service Offer Price

    var SlectedListPrice = new Array();
    var SlectedListServiceId = new Array();
    $("input.selectedvalue").each(function () {
        SlectedListPrice.push($(this).val());
        SlectedListServiceId.push($(this).data('service'));
    });
    Offer.LookUpServicesIdStr = SlectedListServiceId.toString();
    Offer.OfferPriceStr = SlectedListPrice.toString().replace(/,/g, ',0');

    //ServicePackages Offer Price
    var SlectedPackageOfferPrice = new Array();
    var SlectedPackageId = new Array();
    $("input.PackageOffersPrice").each(function () {
        SlectedPackageOfferPrice.push($(this).val());
        SlectedPackageId.push($(this).data('service'));
    });
    Offer.PackagesOfferPrices = SlectedPackageOfferPrice.toString().replace(/,/g, ',0');
    Offer.PackagesIds = SlectedPackageId.toString();
    Offer.IsType = 0;

    Offer.CreatedBy = (atob(UserId));
    Offer.UpdatedBy = (atob(UserId));

    var servicechecked = $('.selectall:checked');

     
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/offer/Offer_Upsert',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(Offer),
        success: function (result) {
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 30000
                })
                setTimeout(function () {
                    window.location = '/Offers/ManageOffers';
                }, 3000);
            }
        },

        error: function (error) {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
        }
    });
    return false;
}

