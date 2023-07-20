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
var VendorId = getCookie("UserId");

var getpromotionId = new URLSearchParams(window.location.search);
getpromotionId = parseInt(atob(getpromotionId.get('promotionId')));
var getpromotionIdatob = ~~getpromotionId;


if (getpromotionIdatob== 0) {
    $('#promotionform').show();
}

//appoinmentDetails edit API call in ajax methos
function promotionDetailsedit() {

    if (getpromotionIdatob > 0) {
         
        $('#promotionformloader').show();
        $('#promotionform').hide();

        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + `/api/promotion/Promotion_ById?Id=${getpromotionIdatob}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                $('#promotionId').val(result.Item.Id)
                $('#productName').selectpicker('val', result.Item.ProductId);
                $('#originalprice').val(result.Item.OriginalPrice);
                $('#offerprice').val(result.Item.OfferPrice);
                let str = $('#offerperiod').val();
                const myArr = str.split("-");
                StartDate = myArr[0];
                EndDate = myArr[1];
                $('#offerperiod').val(result.Item.StartDate + '-' + result.Item.EndDate);
                $('#promotionformloader').hide();
                $('#promotionform').show();
            }, error: function (error) {
                // Error function
                 
                $('#promotionloader').hide();
                $('#promotionform').show();
            }
        });
    }

    return false;
}