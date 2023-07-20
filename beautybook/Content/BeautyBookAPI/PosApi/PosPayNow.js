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

function PosAddOrderDetails(sessid) {
    var customerDatalist = $("#customerDatalist").val();
    var ServiceDetailsCokkieArray = getCookie("ServiceDetailsCokkieArray");

    if (customerDatalist == null || customerDatalist == "")
    {
        alert("Please select a customer name")
    }
    else if (JSON.parse(ServiceDetailsCokkieArray).length < totalPackageService) {
        alert("Please select a selected package all services")
    }
    else
    {
        debugger;

        var POSDetails = new Object();
        POSDetails.POSSessionId = sessid;
        POSDetails.CustomerId = $("#customerDatalist").val();
        POSDetails.ServiceDetailsStr = ServiceDetailsCokkieArray;
        POSDetails.PosPaymentDetails = "";
        POSDetails.CreatedBy = (atob(SalonId));
        POSDetails.UpdatedBy = (atob(SalonId));

        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/pOSDetails/POSDetails_Upsert',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            data: JSON.stringify(POSDetails),
            success: function (result) {
                if (result.Code == 200) {


                    PosPayNow(result.Item.Id);

                    delete_cookie("ServiceDetailsCokkieArray");

                    //Swal.fire({
                    //    position: 'center',
                    //    icon: 'success',
                    //    title: result.Message,
                    //    showConfirmButton: false,
                    //    timer: 30000
                    //})
                    //setTimeout(function () {
                    //    window.location.reload();
                    //}, 3000);
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
}