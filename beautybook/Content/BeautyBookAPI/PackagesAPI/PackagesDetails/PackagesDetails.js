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


var getEmaployeeid = new URLSearchParams(window.location.search);
getEmaployeeid = parseInt(atob(getEmaployeeid.get('EmployeeId')));
var getEmaployeeidatob = ~~getEmaployeeid;

var geteditServicePackageId = new URLSearchParams(window.location.search);
geteditServicePackageId = parseInt(atob(geteditServicePackageId.get('ServicePackageId')));
var geteditServicePackageIdatob = ~~geteditServicePackageId;


if (geteditServicePackageIdatob > 0) {
    $('#packageServicesList').show();
} else {
    $('#packageServicesList').hide();
}

//employeeTypedrp dropdown API call in ajax methos

function getsalonservicedata(callbackfn) {
     
    $('#loader').show();
    $("#ActiveInActivediv").hide();
    $("#breadcrumbeditpackage").hide();
    $("#headreditpackage").hide();
    $("#editpackagediv").hide();
    $("#editpackagediv").hide();


    var SalonServicesData = new Object();
    SalonServicesData.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpServices/LookUpServices_AllServices?SalonId=${atob(SalonId)}&PackageId=${geteditServicePackageIdatob}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(SalonServicesData),
        crossDomain: true,
        success: function (result) {
             
            $('#loader').hide();

            $("#salonserviceslist").html(`
                <thead>
                    <tr>
                        <th scope="col" class="bg-primary text-white w-lg-60">${Langaugestore.ServiceName}</th>
                        <th scope="col" class="bg-primary text-white">${Langaugestore.ServiceCategory}</th>
                        <th scope="col" class="bg-primary text-white">${Langaugestore.Price}</th>
                        <th scope="col" class="bg-primary text-white"></th>
                    </tr>
                </thead>
            `);
            for (i = 0; i < result.Values.length; i++) {
                var abc = (result.Values[i].ParentId)
                if (abc == 0) {
                    $('#salonserviceslist').append(`
                        <tbody>
                            <tr>
                                <th scope="row" class="bg-gray-100" colspan="4">
                                    <div class="custom-control custom-checkbox">
                                        <input type="checkbox" value="${result.Values[i].Id}"  data-id="myId" data-name="chck-${result.Values[i].Id}" class="custom-control-input checkbox selectall serviceclass"  id="${result.Values[i].Id}" data-group="1">
                                        <label class="custom-control-label" for="${result.Values[i].Id}">${result.Values[i].Name}</label>
                                    </div>
                                </th>
                            </tr>
                        </tbody>
                    `);
                }
                 else if (abc == result.Values[i].ParentId) {
                    $('#salonserviceslist').append(`
                        <tbody>
                            <tr>
                                <th scope="row" class="font-weight-normal">
                                    <div class="custom-control custom-checkbox ml-5">
                                        <input type="checkbox" value="${result.Values[i].Id}" data-id="myId"  data-parent="chck-${result.Values[i].ParentId}"  data-name="chck-${result.Values[i].Id}" class="custom-control-input child serviceclass" name="${result.Values[i].Id}" data-parent-id="${result.Values[i].ParentId}" data-child-id="${result.Values[i].Id}" id="${result.Values[i].Id}" data-group="1">
                                        <label class="custom-control-label"  for="${result.Values[i].Id}">${result.Values[i].Name}</label>
                                    </div>
                                </th>
                                <td>SAR     ${result.Values[i].PackageCustomPrices == 0 ? result.Values[i].Price : result.Values[i].PackageCustomPrices}</td>
                                <td>${result.Values[i].Duration}Min</td>
                                <td style="cursor:pointer;" onclick="OpenModalCustomPrice('${btoa(result.Values[i].Id)}','${result.Values[i].PackageCustomPrices == 0 ? btoa(result.Values[i].Price) : btoa(result.Values[i].PackageCustomPrices)}');"><i class="bb-edit-3 text-gray-600 fs-16 mr-3"  title="Custom price create"></i></td>
                            </tr>
                        </tbody>
                    `);
                 }
            }
            
            $(".selectall").change(function (e) {
                $("input[type=checkbox][data-parent-id="+e.target.id+"]").prop('checked', this.checked);
            });
            $(".child").change(function (e) {
                if ($("input[type=checkbox][data-parent-id=" + e.target.getAttribute("data-parent-id") + "]").filter(':checked').length > 0)
                {
                    $("input[type=checkbox][id=" + e.target.getAttribute("data-parent-id") + "]").prop('checked', true);
                }
                else
                {
                 $("input[type=checkbox][id=" + e.target.getAttribute("data-parent-id") + "]").prop('checked', false);

               }
            });

            callbackfn();
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}


function AddServicePackage() {
     
    $('#addPackagesDetails').hide();
    $('#addPackagesDetailsLoading').show();

    $("#toggleActive").hide();
    $("#breadcrumbeditpackage").hide();
    $("#headreditpackage").hide();
    $("#editpackagediv").hide();
    $("#toggleActive").hide();
    $("#toggleActive").hide();


    var ServicePackage = new Object();
     
    ServicePackage.Id = $('#Id').val();
    ServicePackage.SalonId = atob(SalonId);
    ServicePackage.PackageName = $('#packageName').val();
    ServicePackage.CustomPrice = $('#PackageCustomPrice').val();

    var SlectedList = new Array();
    $("input.serviceclass:checked").each(function () {
        SlectedList.push($(this).val());
    });
    var SlectedListValue = SlectedList.toString();
    ServicePackage.ServicePackageIdStr = SlectedListValue;
    ServicePackage.CreatedBy = (atob(UserId));
    ServicePackage.UpdatedBy = (atob(UserId));
     
    ServicePackage.IsActive = $('#toggleActive').prop('checked');

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/servicePackage/ServicePackage_Upsert',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(ServicePackage),
        success: function (result) {
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 30000
                })
                setTimeout(function () {
                    window.location = `/Packages/PackagesDetails?ServicePackageId=${btoa(result.Item.Id)}`;
                }, 3000);
            }

            $('#addPackagesDetails').show();
            $('#addPackagesDetailsLoading').hide();

        },

        error: function (error) {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
            $('#addPackagesDetails').show();
            $('#addPackagesDetailsLoading').hide();
        }
    });
    return false;
}



// Package create custom price
var ServiceId = "";
function OpenModalCustomPrice(serviceId,PackagePrice) {
    ServiceId = serviceId;
    $('#CustomPrice').val(`${atob(PackagePrice)}`);
    $('#CreateCustomPrice').modal('show');
}


function creatCustomPrice() {
    
    $('#addCustomPrice').hide();
    $('#addCustomPriceloading').show();


    var CreatCustomPrice = new Object();
    CreatCustomPrice.Id = 0;
    CreatCustomPrice.SalonId = (atob(SalonId));
    CreatCustomPrice.PackageId = (geteditServicePackageIdatob);
    CreatCustomPrice.LookUpServiceId = (atob(ServiceId));
    CreatCustomPrice.CustomPrice = $('#CustomPrice').val();

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/packageCustomPrice/PackageCustomPrice_Create`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(CreatCustomPrice),
        success: function (result) {
             
            $('#CreateCustomPrice').modal('hide');
            $('#CustomPrice').val('');
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    window.location.reload();
                }, 3000);
            }

            $('#addCustomPrice').show();
            $('#addCustomPriceloading').hide();

        },
        error: function (error) {
             
            console.log(error);
            $('#CreateCustomPrice').modal('hide');
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })

            $('#addCustomPrice').show();
            $('#addCustomPriceloading').hide();

        }
    });
    return false;
}