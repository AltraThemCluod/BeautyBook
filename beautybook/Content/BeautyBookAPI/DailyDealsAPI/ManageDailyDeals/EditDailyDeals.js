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

//Edit Data Daily Deals
var dailyDealsId = "";
function OpenModalDailyDealsEdit(DailyDealsId) {
    dailyDealsId = DailyDealsId;
    $('#EditDailyDealsModal').modal('show');

     
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + `/api/dailyDeals/DailyDeals_ById?Id=${atob(DailyDealsId)}`,
        //headers: { "Authorization": '' + DeviceTokenNumber + '' },
        crossDomain: true,
        dataType: 'json',
        async: false,
        success: function (result) {
             
            console.log('result', result)
            $('#DailyDealsId').val(result.Item.Id)
            $('#offerDate').val(result.Item.OfferDate)
            $('#StartTime').val(result.Item.StartTime)
            $('#EndTime').val(result.Item.EndTime)
            $('#OriginalPrice').val(result.Item.ServicePrice == 0 || result.Item.ServicePrice == null ? result.Item.PackagesPrice : result.Item.ServicePrice)
            $('#OfferPrice').val(result.Item.OfferPrice)

            $('#PackageAndServiceName').text(result.Item.ServiceName == "" || result.Item.ServiceName == null ? result.Item.PackagesName : result.Item.ServiceName);
            $('#PackageAndServiceType').text(result.Item.Type == 1 ? 'Service' : 'Package');

        }, error: function (error) {
            // Error function
        }
    });

}


//Update Daily Deald
function updateDataDailyDeals() {

    $('#DailyDealsUpdate').hide();
    $('#DailyDealsUpdateloading').show();

    var UpdateDataDailyDeals = new Object();
    UpdateDataDailyDeals.Id = $("#DailyDealsId").val();
    UpdateDataDailyDeals.SalonId = atob(SalonId);
    UpdateDataDailyDeals.OfferDate = $("#offerDate").val();
    UpdateDataDailyDeals.StartTime = $("#StartTime").val();
    UpdateDataDailyDeals.EndTime = $("#EndTime").val();
    UpdateDataDailyDeals.OfferPrice = $("#OfferPrice").val();

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/dailyDeals/DailyDeals_Upsert',
        headers: { 'Content-Type': 'application/json' },
        dataType: 'json',
        data: JSON.stringify(UpdateDataDailyDeals),
        crossDomain: true,
        success: function (result) {
            DailyDealsList.init();
            $('#EditDailyDealsModal').modal('hide');
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 2000
                })
            }

            $('#DailyDealsUpdate').show();
            $('#DailyDealsUpdateloading').hide();

        }, error: function (error) {
            DailyDealsList.init();
            $('#EditDailyDealsModal').modal('hide');
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 2000
            })

            $('#DailyDealsUpdate').show();
            $('#DailyDealsUpdateloading').hide();
        }
    });
}