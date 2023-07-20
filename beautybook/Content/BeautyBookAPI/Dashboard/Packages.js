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

var salonId = getCookie("SalonId");
var DeviceTokenNumber = getCookie("DeviceToken&Type");
var userId = getCookie("UserId");
var userName = getCookie("UserName");

function CompletedUnCompletedPackages() {
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/salonOwnerDashboard/SalonOwnerSMSandEmailPackages_All?SalonId=${atob(salonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            $('#SmscompleteMessage').text(result.Item.SMSCompletedMessage == "" || result.Item.SMSCompletedMessage == null ? 0 : result.Item.SMSCompletedMessage);
            $('#SmsUncompleteMessage').text(result.Item.SMSUnCompleteMessage == "" || result.Item.SMSUnCompleteMessage == null ? 0 : result.Item.SMSUnCompleteMessage);
            $('#EmailcompleteMessage').text(result.Item.EmailCompletedMessage == "" || result.Item.EmailCompletedMessage == null ? 0 : result.Item.EmailCompletedMessage);
            $('#EmailUncompleteMessage').text(result.Item.EmailUnCompleteMessage == "" || result.Item.EmailUnCompleteMessage == null ? 0 : result.Item.EmailUnCompleteMessage);
        }, error: function (error) {
            //Error function
        }

    });
}



function ServiceName() {
    debugger;
    let ServiceName = new Object();
    ServiceName.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpServices/LookUpServices_AllServices?SalonId=${atob(salonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(ServiceName),
        crossDomain: true,
        async: false,
        success: function (result) {
            debugger;
            for (i = 0; i < result.Values.length; i++) {
                if (result.Values[i].ParentId > 0) {
                    $('#serviceName').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                    `);
                }
            }

            $('.selectpicker').selectpicker("refresh");

        }, error: function (error) {

        }
    });
    return false;
}
function tecnicalSupport() {
    $('#tecnicalSupportCard').html('');
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/salonOwnerDashboard/TecnicalSupport_SalonId?SalonId=${atob(salonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            debugger;
            $('#tecnicalSupportCard').append(`
                <div class="card card-lg">
                    <div class="card-header text-center w-100">
                        <h6 class="mb-0 pt-2 pb-2">${Langaugestore.Hello}, ${userName}</h6>
                    </div>
                    <div class="card-body text-center">
                        ${result.Item.LogoUrl == "" || result.Item.LogoUrl == null ?
                            `<img class="mb-4" src="../Content/assets/images/svg-icons/salon-shop.svg" alt="salon-shop" style="width:30% !important;">`
                            :
                            `<img class="mb-4 w-50"  onerror="this.src='/Content/assets/images/svg-icons/salon-shop.svg'" src="${APIEndPoint}/${result.Item.LogoUrl}" alt="${result.Item.SalonName}">`
                        }
                        <h6 class="mb-0">${result.Item.SalonName}</h6>
                        <hr>
                        <div class="row m-0 align-items-center">
                            <div class="col-lg-6 text-left pl-0" style="border-right: 1px solid #e5e5e5;">
                                <p class="mb-1">${result.Item.CurrentTime}</p>
                                <p class="mb-1">${result.Item.CurrentDay}</p>
                                <p class="mb-1">${result.Item.CurrentDate}</p>
                                <p class="mb-1">${result.Item.CountryName} ,${result.Item.StateName}</p>
                            </div>
                            <div class="col-lg-6">
                                <p class="mb-0 pt-2 pb-2">${Langaugestore.Today_s_Appointments} :</p>
                                <div class="btn btn-primary w-50"><b>${result.Item.ToDaysAppointments}</b></div>
                            </div>
                        </div>
                        <hr>
                        <h6 class="mb-0 pt-2 pb-2">${Langaugestore.Technical_Support} : </h6>
                        <a href="/TechnicalSupport/ManageTechnicalSupport" class="btn btn-primary btn-wide">${Langaugestore.Contact_Us}</a>
                    </div>
                </div>
            `);
        }, error: function (error) {
            //Error function
        }

    });
}