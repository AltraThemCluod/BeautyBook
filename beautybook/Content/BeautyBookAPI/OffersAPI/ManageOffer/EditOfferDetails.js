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

var getOfferid = new URLSearchParams(window.location.search);
getOfferid = parseInt(atob(getOfferid.get('Id')));
var getOfferidatob = ~~getOfferid;
 
function getOfferdata()
{
    if (getOfferidatob > 0)
    {
        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/offer/Offer_ById?Id=' + getOfferidatob + '',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {

                $('#Id').val(result.Item.Id)
                $('#offerName').val(result.Item.OfferName);
                $("input[name=applyOn][value=" + result.Item.ApplyOn + "]").prop('checked', true);

                let OfferPeriod = $('#offerPeriod').val();
                const OfferPeriodArr = OfferPeriod.split("-");
                OfferPeriodStart = OfferPeriodArr[0];
                OfferPeriodEnd = OfferPeriodArr[1];
                $('#offerPeriod').val(result.Item.OfferPeriodStart + '-' + result.Item.OfferPeriodEnd);

                //for (i = 0; i < result.Item.MasterOfferPrice.length; i++) {
                //    $("#" + result.Item.MasterOfferPrice[i].LookUpServicesId).prop('checked', true)
                //}

                for (i = 0; i < result.Item.MasterOfferPrice.length; i++) {
                    $('#offerprice' + result.Item.MasterOfferPrice[i].LookUpServicesId).val(result.Item.MasterOfferPrice[i].OfferPrice == 0 ? '' : result.Item.MasterOfferPrice[i].OfferPrice);
                }

                for (p = 0; p < result.Item.PackageOfferPrice.length; p++) {
                    $('#PackageOffer' + result.Item.PackageOfferPrice[p].PackagesId).val(result.Item.PackageOfferPrice[p].PackagesOfferPrice == 0 ? '' : result.Item.PackageOfferPrice[p].PackagesOfferPrice);
                }

                $('#minageyear').val(result.Item.AgeBetweenMinYear);
                $('#maxageyear').val(result.Item.AgeBetweenMaxYear);
                $('#loader').hide();
            }, error: function (error) {
                $('#loader').hide();
            }
        });
    }
}