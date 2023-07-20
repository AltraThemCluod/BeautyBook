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



function myEmailPakageslist() {
    $('#EmailPackagesLoader').show();

    var EmailPackgeslist = new Object();
    EmailPackgeslist.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/email/EmailPakages_All?search=&LookUpDurationId=0&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(EmailPackgeslist),
        crossDomain: true,
        success: function (result) {
           
            $("#plansdetails").html(``);
            for (var i = 0; i < result.Values.length; i++) {
                $('#plansdetails').append(`
                    <div class="col mb-3">
                        <div class="card">
                            <h6 class="card-header">${result.Values[i].EmailMsgPackagesName}</h6>
                            <div class="card-body">
                                <h2 class="text-primary mb-0">${result.Values[i].EmailMsgPackagesPrice == 0 ? 'Free' : result.Values[i].EmailMsgPackagesPrice + '﷼'}</h2>
                                <p class="text-muted">${result.Values[i].LookUpDurationId} ${Langaugestore.Day}</p>
                                <p class="my-5">${result.Values[i].NoOfMessages == 0 ? 'Unlimited' : result.Values[i].NoOfMessages} ${Langaugestore.Messages}</p>
                                ${result.Values[i].EmailMsgPackagesPrice == 0 ?
                        `${result.Values[i].SelectedPackages == 1 ? `<button type="button" onclick=AddEmailPackage(${result.Values[i].Id}); class="btn btn-primary font-weight-semibold" disabled>Try for free</button>`
                            : `<button type="button" onclick=AddEmailPackage(${result.Values[i].Id}); class="btn btn-outline-primary font-weight-semibold">Try for free</button>`}`

                        : `${result.Values[i].SelectedPackages == 1 ? `<button type="button" onclick=AddEmailPackage(${result.Values[i].Id}); class="btn btn-primary font-weight-semibold" disabled>${Langaugestore.SelectPlan}</button>`
                            : `<button type="button" onclick=AddEmailPackage(${result.Values[i].Id}); class="btn btn-outline-primary font-weight-semibold">${Langaugestore.SelectPlan}</button>`}`
                    }
                            </div>
                        </div>
                    </div>
                `);
            }
            $('#EmailPackagesLoader').hide();
        }, error: function (error) {
            $('#EmailPackagesLoader').hide();
        }
    });
    return false;
}
function AddEmailPackage(EmailPackagesId) {
    debugger;
    $('.loader-text').show();
    var EmailPackage = new Object();

    EmailPackage.SalonId = atob(SalonId);
    EmailPackage.EmailPackagesId = EmailPackagesId;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/selectPlan_EmailPackages/SelectPlan_EmailPackages_Upsert',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(EmailPackage),
        success: function (result) {
            debugger;
            console.log('result', result);
            if (result.Code == 200) {
                //Swal.fire({
                //    position: 'center',
                //    icon: 'success',
                //    title: result.Message,
                //    showConfirmButton: false,
                //    timer: 3000
                //})
                PayTabsEmailPackagesPayment(result.Item.EmailMsgPackagesPrice, `cart ${result.Item.Id}`, `cart desc: ${result.Item.Id}`, btoa(result.Item.Id));
                //setTimeout(function () {
                //    window.location.href = "/Marketing/ManageEmailMarketing"
                //}, 3000);
            }

        },

        error: function (error) {
            debugger;
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


function PayTabsEmailPackagesPayment(TotalAmount, cart_id, cart_description, PackagesId) {
    debugger;
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `api/selectPlan_EmailPackages/Payment_EmailPackages?TotalAmount=${TotalAmount}&cart_id=${cart_id}&cart_description=${cart_description}&PackageId=${PackagesId}`,
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



