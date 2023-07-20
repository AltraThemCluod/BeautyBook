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

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}


var getQueryParam = new URLSearchParams(window.location.search);
var PosId = getEmaployeeid = parseInt(atob(getQueryParam.get('ri')));
PosId = ~~PosId;

var DeviceTokenNumber = getCookie("DeviceToken&Type");
var SalonId = getCookie("SalonId");

var CoinsCashArray = [];
function CreatePosSession() {
    $('#sessionBtn').hide();
    $('#sessionLoadingBtn').show();

    var CoinsCashLength = $("#CoinsAmount > #CoinsCash").length

    if (CoinsCashLength > 0)
    {
        for (var i = 0; i < CoinsCashLength; i++) {

            var CoinsQty = $("#amount_" + i).val();
            var CoinsBillId = $("#amount_" + i).data("coinsbillid-" + i);

            var CoinsCashObj = {
                CoinsBilllsId: CoinsBillId,
                Qty: CoinsQty
            }

            CoinsCashArray.push(CoinsCashObj);
        }
    }

    debugger;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/pOSSession/POSSession_Create?Id=0&SalonId=${atob(SalonId)}&IsNewSessionCreate=true&CoinsAmountStr=${JSON.stringify(CoinsCashArray)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            debugger;
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    window.location = "/Pos/PosAuthentication?ri=" + btoa(result.Item.Id);
                }, 3000);

                $('#sessionBtn').show();
                $('#sessionLoadingBtn').hide();

            } else {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })

                $('#sessionBtn').show();
                $('#sessionLoadingBtn').hide();
            }
        },
        error: function (error) {
            
        }
    });
    return false;

}


//POS Authentication
function PosAuthentication() {

    var PosEmail = $("#PosEmail").val();

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/pOSSession/POSSession_Authentication?Email=${PosEmail}&SessionId=${PosId}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        crossDomain: true,
        success: function (result) {
            debugger;
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    setCookie("PosAuthEmail", result.Item.Email , 30);
                    window.location = "/Pos/Index?ri=" + btoa(PosId);
                }, 3000);

                $('#sessionBtn').show();
                $('#sessionLoadingBtn').hide();

            } else {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })

                $('#sessionBtn').show();
                $('#sessionLoadingBtn').hide();
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
}

///POS Session Close
function CloseSession(SessionId) {
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/pOSSession/POSSessionClosing_CachAndAt?Id=${SessionId}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        crossDomain: true,
        success: function (result) {
            if (result.Code == 200) {
                window.location.href = "/Pos/CreateSessions"
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
}



///Pos session card
function PosCard() {

    $("#loader").show();

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/pOSSession/POSSession_TopBySalonId?SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        crossDomain: true,
        success: function (result) {
            if (result.IsValid == true) {
                if (result.Code == 200) {
                    $("#posSessionCard").append(`<div class="col-lg-6">
                        <div class="card p-3">
                            <div class="mb-5 d-flex align-items-center justify-content-between">
                                <h5>${Langaugestore.Salon}</h5>
                            </div>
                            <div class="row m-0 align-items-center">
                                <div class="col-lg-6 p-0">
                                    ${PermissionData.find(record => record.ModuleKey === "NewSession").Value == true ? `<button type="button" onclick="NewSession()" class="btn btn-block btn-primary w-70">${Langaugestore.NEW_SESSION}</button>` : ""}
                                    ${PermissionData.find(record => record.ModuleKey === "ContinueSession").Value == true ? `<a href="/Pos/Index?ri=${btoa(result.Item.Id)}" class="btn btn-block btn-outline-primary w-70">${Langaugestore.CONTINUE_SELLING}</a>` : ""}
                                </div>
                                <div class="col-lg-6 p-0">
                                    <div>
                                        <p class="fs-14 text-muted mb-1 d-flex align-items-center justify-content-between">
                                            <span href="javascript:voi(0)" class="font-weight-medium">
                                                ${Langaugestore.Last_Closing_Date}
                                            </span>
                                            <span class="font-weight-medium">${result.Item.LastClosingAtStr == "" || result.Item.LastClosingAtStr == null ? "-" : result.Item.LastClosingAtStr}</span>
                                        </p>
                                        <p class="fs-14 text-muted mb-1 d-flex align-items-center justify-content-between">
                                            <span href="javascript:voi(0)" class="font-weight-medium">
                                                ${Langaugestore.Last_Closing_Cash}
                                            </span>
                                            <span class="font-weight-medium"><b>SAR</b> : ${result.Item.LastClosingCash == "" || result.Item.LastClosingCash == null ? "0.00" : result.Item.LastClosingCash.toFixed(2)}</span>
                                        </p>
                                        <p class="fs-14 text-muted mb-1 d-flex align-items-center justify-content-between">
                                            <span href="javascript:voi(0)" class="font-weight-medium">
                                                ${Langaugestore.Last_Closing_Online}
                                            </span>
                                            <span class="font-weight-medium"><b>SAR</b> : ${result.Item.LastClosingOnline == "" || result.Item.LastClosingOnline == null ? "0.00" : result.Item.LastClosingOnline.toFixed(2)}</span>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>`)
                }
            }
            else {
                $("#posSessionCard").append(`<div class="col-lg-6">
                    <div class="card p-3">
                        <div class="mb-5 d-flex align-items-center justify-content-between">
                            <h5>Salon</h5>
                        </div>
                        <div class="row m-0 align-items-end">
                            <div class="col-lg-6 p-0">
                                <button type="button" onclick="NewSession()" class="btn btn-block btn-primary w-70">NEW SESSION</button>
                            </div>
                            <div class="col-lg-6 p-0">
                                <div>
                                    <p class="fs-14 text-muted mb-1 d-flex align-items-center justify-content-between">
                                        <span href="javascript:voi(0)" class="font-weight-medium">
                                            Last Closing Date
                                        </span>
                                        <span class="font-weight-medium"></span>
                                    </p>
                                    <p class="fs-14 text-muted mb-1 d-flex align-items-center justify-content-between">
                                        <span href="javascript:voi(0)" class="font-weight-medium">
                                            Last Closing Cash
                                        </span>
                                        <span class="font-weight-medium"><b>SAR</b> : 0.00</span>
                                    </p>
                                    <p class="fs-14 text-muted mb-1 d-flex align-items-center justify-content-between">
                                        <span href="javascript:voi(0)" class="font-weight-medium">
                                            Last Closing Online
                                        </span>
                                        <span class="font-weight-medium"><b>SAR</b> : 0.00</span>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`)
            }

            $("#loader").hide();
        },
        error: function (error) {
            $("#loader").hide();
        }
    });
}