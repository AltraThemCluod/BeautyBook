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


//Product api delete function

//swal Delete Offer
function offerDelete(offerId) {
    Swal.fire({
        title: '' + Langaugestore.Are_you_sure_you_want_to_delete_this_offer__+'',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteOfferswal(offerId);
        }
    })
}

// Offer Delete API call in ajax method
function DeleteOfferswal(offerId) {
     
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + `/api/offer/Offer_Delete?Id=${(offerId)}&DeletedBy=${(atob(UserId))}`,
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        crossDomain: true,
        dataType: 'json',
        async: false,
        success: function (result) {

            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    OfferList.init();
                }, 3000);
            }
        }, error: function (error) {
            // Error function
        }
    });
}