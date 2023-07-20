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

function mySmsPakageslist() {
    $('#SMSPackagesLoader').show();
     
    var SmsPackgeslist = new Object();
    SmsPackgeslist.IsPageProvided = true;
     
    $.ajax({
        type: 'POST',
        url:  APIEndPoint + `/api/sms/SmsPakages_All?search=&LookUpDurationId=0&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(SmsPackgeslist),
        crossDomain: true,
        success: function (result) {
             
            $('#plansSmsdetails').html(``);
            for (var i = 0; i < result.Values.length; i++) {
                $('#plansSmsdetails').append(`
                    <div class="col mb-3">
                        <div class="card">
                            <h6 class="card-header">${result.Values[i].SMSPackagesName}</h6>
                            <div class="card-body">
                                <h2 class="text-primary mb-0">${result.Values[i].SMSPackagesPrice == 0 ? 'Free' : result.Values[i].SMSPackagesPrice + '﷼‎'}</h2>
                                <p class="text-muted">${result.Values[i].LookUpDurationId} ${Langaugestore.Day}</p>
                                <p class="my-5">${result.Values[i].NoOfTextMessages == 0 ? 'Unlimited' : result.Values[i].NoOfTextMessages} ${Langaugestore.TextMessages}</p>
                                ${result.Values[i].SMSPackagesPrice == 0 ?
                                    `${result.Values[i].SelectedPackages == 1 ? `<button type="button" onclick=AddSMSPackage(${result.Values[i].Id}); class="btn btn-primary font-weight-semibold" disabled>Try for free</button>`
                                        : `<button type="button" onclick=AddSMSPackage(${result.Values[i].Id}); class="btn btn-outline-primary font-weight-semibold">Try for free</button>`}`
                    : `${result.Values[i].SelectedPackages == 1 ? `<button type="button" onclick=AddSMSPackage(${result.Values[i].Id}); class="btn btn-primary font-weight-semibold" disabled>${Langaugestore.SelectPlan}</button>`
                                        : `<button type="button" onclick=AddSMSPackage(${result.Values[i].Id}); class="btn btn-outline-primary font-weight-semibold">${Langaugestore.SelectPlan}</button>`}`
                                }
                            </div>
                        </div>
                    </div>
                `);
            }
            $('#SMSPackagesLoader').hide();
        }, error: function (error) {
            $('#SMSPackagesLoader').hide();
        }
    });
    return false;
}
 
function AddSMSPackage(SMSPackagesId) {
    debugger;
    $('.loader-text').show();
    var SMSPackage = new Object();

    SMSPackage.SalonId = atob(SalonId);
    SMSPackage.SMSPackagesId = SMSPackagesId;
    SMSPackage.CompleteNoOfMsg = 0;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/selectPlan_SMSPackages/SelectPlan_SMSPackages_Upsert',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(SMSPackage),
        success: function (result) {
            //mySmsPakageslist();
            if (result.Code == 200) {
                //Swal.fire({
                //    position: 'center',
                //    icon: 'success',
                //    title: result.Message,
                //    showConfirmButton: false,
                //    timer: 3000
                //})
                //setTimeout(function () {
                //    window.location.href = "/Marketing/ManageSmsMarketing"
                //}, 3000);
                PayTabsSMSPackagesPayment(result.Item.SMSPackagesPrice, `cart ${result.Item.Id}`, `cart desc: ${result.Item.Id}`, btoa(result.Item.Id));

            }

        },

        error: function (error) {
            $('.loader-text').hide();
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

function PayTabsSMSPackagesPayment(TotalAmount, cart_id, cart_description, PackagesId) {
    debugger;
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `api/selectPlan_SMSPackages/Payment_SMSPackages?TotalAmount=${TotalAmount}&cart_id=${cart_id}&cart_description=${cart_description}&PackageId=${PackagesId}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            debugger;
            if (result != null && result != undefined && result.redirect_url != null && result.redirect_url != '') {

                window.location.href = result.redirect_url;
            }
            $('.loader-text').hide();
        },
        error: function (error) {
            debugger;
            console.log('error', error);
        }
    });
}











