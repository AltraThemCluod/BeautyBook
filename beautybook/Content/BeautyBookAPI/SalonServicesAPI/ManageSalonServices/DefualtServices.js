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



function defualtServiceSelect() {
    
    var DefualtServiceSelect = new Object();
    DefualtServiceSelect.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpServices/LookUpServices_AllServices?SalonId=0&PackageId=0`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(DefualtServiceSelect),
        crossDomain: true,
        success: function (result) {
            

            var ServiceId = [];
            for (i = 0; i < result.Values.length; i++) {

                var DefualtServiceParentId = result.Values[i].ParentId;
               
                if (DefualtServiceParentId == 0) {
                    ServiceId.push(result.Values[i].Id);
                    $('#defualtServiceSelect').append(`
                        <div class="col-12 mb-3">
                            <div class="card rounded-sm shadow-1">
                                <div class="card-header">
                                    <h6 class="card-title py-2 mb-0 mr-auto">${result.Values[i].Name}</h6>
                                </div>
                                <div class="card-body">
                                    <div class="row row-cols-xxl-6 row-cols-xl-4 row-cols-md-3 row-cols-sm-2 row-cols-1 text-center" id="ServiceList${result.Values[i].Id}">
                                    </div>
                                </div>
                            </div>
                        </div>
                    `);
                }
                for (j = 0; j < ServiceId.length; j++) {
                    if (ServiceId[j] == DefualtServiceParentId) {
                        $(`#ServiceList${DefualtServiceParentId}`).append(`
                            <div class="col my-2">
                                <div class="card shadow-1 hover-border-primary rounded-sm h-100" data-toggle="modal" data-target="#serviceModal" role="button">
                                    <div class="card-body">
                                        <img class="img-fluid mb-3" src="../Content/assets/images/svg-icons/salon.svg" alt="">
                                        <h6 class="font-weight-medium ls-normal fs-14 mb-0">${result.Values[i].Name}</h6>
                                    </div>
                                </div>
                            </div>
                        `);
                    }
                }
            }

        }, error: function (error) {
            //Error Function
        }
    });
    return false;
}