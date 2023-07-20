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

//setCookie
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

var DeviceTokenNumber = getCookie("DeviceToken&Type");

//Add salons API call in ajax method

function mySalonslist() {
    $('#loader').show();

    var Salonslist = new Object();
    Salonslist.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/salons/Salons_ByUserId?search',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(Salonslist),
        crossDomain: true,
        success: function (result) {
             
            if (result.Values.length <= 0) {
                $("#salonsList").html(`
                   
                `);
                $('#salonsList').append(`
                    <div class="col mb-3 mb-xl-0">
                        <a class="card add-salon-card h-100" href="#" data-toggle="modal" data-target="#addSalonInfoModal" role="button">
                            <div class="card-body">
                                <div class="details-add-icon">
                                    <i class="bb-plus fs-24"></i>
                                </div>

                                <h6 class="mb-0">Add New Branch</h6>
                            </div>
                        </a>
                    </div>
                `)
                $("#loader").hide();
            } else {
                $("#salonsList").html(``);
            }

            if (result.Values.length > 0) {

                for (i = 0; i < result.Values.length; i++) {
                    $('#salonsList').append(`
                        <div class="col mb-3" style="cursor:pointer;">
                            <a class="card add-salon-card h-100" ${result.Values[i].LookUpStatusId == 3 ? `onclick="getSalons(${result.Values[i].Id},'${result.Values[i].SalonName}','${result.Values[i].LogoUrl}','${result.Values[i].IsEmployee}')"` : `title="This salon not Approved"`}>
                                <div class="card-body">
                                    ${result.Values[i].LogoUrl == "" || result.Values[i].LogoUrl == null ?  
                                        `<img class="mb-4" src="../Content/assets/images/svg-icons/salon-shop.svg" alt="salon-shop">`
                                        :
                                        `<img class="mb-4" onerror="this.src='/Content/assets/images/svg-icons/salon-shop.svg'"  src="${APIEndPoint}/${result.Values[i].LogoUrl}" alt="salon-shop">`
                                    }
                                    <h6 class="mb-0 textline-truncate" title="${result.Values[i].SalonName}">${result.Values[i].SalonName}</h6>
                                </div>
                                ${result.Values[i].LookUpStatusId == 5 ? 
                                    `<div class="badge badge-soft-warning custome-position-badge px-3 py-2 m-1" title="This Salon Approval Pending"><i class="fa fa-clock-o" aria-hidden="true"></i></div>` : ''
                                }
                                ${result.Values[i].LookUpStatusId == 4 ?
                                    `<div class="badge badge-soft-danger custome-position-badge px-3 py-2 m-1"  title="This Salon Rejected"><i class="fa fa-ban" aria-hidden="true"></i></div>` : ''
                                }
                                ${result.Values[i].LookUpStatusId == 3 ?
                                    `<div class="badge badge-soft-success custome-position-badge px-3 py-2 m-1"  title="This Salon Approved"><i class="fa fa-check" aria-hidden="true"></i></div>` : ''
                                }
                            </a>
                        </div>
                `);
                }
                
                $('#salonsList').append(`
                    <div class="col mb-3">
                        <a class="card add-salon-card h-100" onclick="salonsModal();" href="javascript:void();">
                            <div class="card-body">
                                <div class="details-add-icon">
                                    <i class="bb-plus fs-24"></i>
                                </div>

                                <h6 class="mb-0">${Langaugestore.addNewSalon}</h6>
                            </div>
                        </a>
                    </div>
                `)
                
            }
            $('#loader').hide();
        }, error: function (error) {
            $('#loader').hide();
        }
    });
    return false;
}

function salonsModal() {
    $('#salonsForm')[0].reset();
    $('#salonsLogobox').hide();
    $('#addSalonInfoModal').modal('show');
}

function getSalons(salonId, salonName, salonsLogourl, IsEmployee) {
    
    setCookie("SalonId", btoa(salonId), 30);
    setCookie("SalonName", salonName, 30);
    setCookie("SalonsLogoUrl", salonsLogourl, 30);

    if (IsEmployee > 0) {
        window.location = "/Home/Index";
    } else {
        window.location = "/Employee/ManageEmployee";
    }
}