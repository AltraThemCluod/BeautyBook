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

//editSalonsdetails API call in ajax method
function editSalonsdetails()
{
    $('#salonsEditformloader').show();
    $('#salonsEditform').hide();

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/salons/Salons_ById?Id='+atob(SalonId)+'',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            $('#salonsId').val(result.Item.Id);
            $('#salonName').val(result.Item.SalonName);
            $('#addressLine1').val(result.Item.AddressLine1);
            $('#addressLine2').val(result.Item.AddressLine2);
            $('#salonsCountry').selectpicker('val', result.Item.LookUpCountryId);
            $('#Country').trigger('change');
            SalonsState();
            $('#salonsState').selectpicker('val', result.Item.LookUpStateId);
            $('#salonsCity').val(result.Item.City);
            $('#VisitorLatitude').val(result.Item.Latitude)
            $('#VisitorLongitude').val(result.Item.Longitude)
            $('#salonsZipcode').val(result.Item.ZipCode);
            $('#salonsPrimarynumber').val(result.Item.PrimaryPhone.replace("+966 ", ""));
            $('#salonsBankName').val(result.Item.BankName);
            $('#salonsIBANNumber').val(result.Item.IBANNumber);
            $('#salonsTaxNumber').val(result.Item.TaxNumber);
            $('#salonsVATNumber').val(result.Item.VATNumber);
            $('#salonsAlternatenumber').val(result.Item.AlternatePhone.replace("+966 ", ""));
            $('#salonsLogoUrl').prop('src', APIEndPoint + '/' + result.Item.LogoUrl);

            $('#salonsEditformloader').hide();
            $('#salonsEditform').show();
        }, error: function (error) {
            $('#salonsEditformloader').hide();
            $('#salonsEditform').show();
        }
    });
    return false;
}