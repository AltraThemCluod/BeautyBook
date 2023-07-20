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

var getAppoinmentDetailsId = new URLSearchParams(window.location.search);
getAppoinmentDetailsId = parseInt(atob(getAppoinmentDetailsId.get('AppoinmentDetailsId')));
var getAppoinmentDetailsIdatob = ~~getAppoinmentDetailsId;


//profile upload image name get
function profileUpload() {

    var fake_path = $('#customerPhoto').val();
    $('#uploadImagename').text(fake_path.split("\\").pop());
}

// Appoinment add api call in ajax methos

var DataValid = false;

function appointmentsAdd() {
    
    AServiceCategory();
    if (DataValid == true) {
        $('#appointmentBtn').hide();
        $('#appointmentBtnloading').show();

        var AppointmentsAdd = new Object();
        AppointmentsAdd.Id = $('#appointmentId').val();
        AppointmentsAdd.UserId = $('#customerDatalist').val();
        AppointmentsAdd.SalonId = atob(SalonId);
        //AppointmentsAdd.AssignedToUserId = $('#userAssignto').val(); old code commented in 21/11/2022
        AppointmentsAdd.AssignedToUserId = ""; //new code pass balnk val 21/11/2022
        AppointmentsAdd.AppointmentDate = $('#appointmentsDate').val();
        AppointmentsAdd.AppointmentTime = $('#appointmentsTime').val();
        //AppointmentsAdd.Price = $('#appointmentsPrice').val(); old code commented in 21/11/2022
        AppointmentsAdd.Price = ""; //new code pass balnk val 21/11/2022
        AppointmentsAdd.TotalPrice = $('#appointmentsTotalPrice').val();
        //AppointmentsAdd.Duration = $('#appointmentsDuration').val(); old code commented in 21/11/2022
        AppointmentsAdd.Duration = ""; //new code pass balnk val 21/11/2022
        AppointmentsAdd.Comment = $('#appointmentsComment').val();
        //AppointmentsAdd.ServicesIds = $('#serviceName').val().toString(); old code commented in 21/11/2022
        AppointmentsAdd.ServicesIds = $("#aServiceCategoryDetails").val(); //new code pass balnk val 21/11/2022
        //AppointmentsAdd.CategoryId = $("#categoryName").val(); old code commented in 21/11/2022
        AppointmentsAdd.CategoryId = ""; //new code pass balnk val 21/11/2022

        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/userAppointments/UserAppointments_Upsert',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(AppointmentsAdd),
            crossDomain: true,
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
                        window.location = "/Appointments/ManageAppointments";
                    }, 3000);
                }

                $('#appointmentBtn').show();
                $('#appointmentBtnloading').hide();

            }, error: function (error) {

                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: error.responseJSON.Message,
                    showConfirmButton: false,
                    timer: 3000
                })

                $('#appointmentBtn').show();
                $('#appointmentBtnloading').hide();
            }
        });
    }
}

function removeCategoryservice(eventId, IsDbDeleted, sId) {
    
    if (IsDbDeleted == true) {
        Swal.fire({
            title: "Are you sure you want to delete this service",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: APIEndPoint + `/api/userAppointments/AppointmentServiceDetails_Delete?Id=${sId}`,
                    headers: { "Authorization": '' + DeviceTokenNumber + '' },
                    processData: false,
                    contentType: false,
                    crossDomain: true,
                    success: function (result) {
                        
                        if (result.Code == 200) {
                            $("#CategoryService_" + eventId).remove();
                            Swal.fire({
                                position: 'center',
                                icon: 'success',
                                title: result.Message,
                                showConfirmButton: false,
                                timer: 3000
                            })

                            setInterval(function () {
                                window.location.reload();
                            }, 3000);
                        }
                    }, error: function (error) {
                        window.location.reload();
                    }
                });
            }
        })
    } else {
        $("#CategoryService_" + eventId).remove();
        countDetails = countDetails - 1;
        appoinmentEmployeeWorkingHourse(eventId);
    }
}


//Blank appointment create
function CreateBlankAppointment() {
    $.ajax({
        type: 'POST',
        url: APIEndPoint + '/api/userAppointments/BlankAppointment_Create',
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: ({
            CreatedBy: atob(SalonId)
        }),
        crossDomain: true,
        success: function (result) {
            
            if (result.Code == 200) {
                window.location = `/Appointments/AppointmentDetails?AppoinmentDetailsId=${btoa(result.Item.Id)}`;
            }
        }, error: function (error) {
            window.location.reload();
        }
    });
}

////changeServiceAmount
//var amountStore = 0;
//function changeServiceAmount(amount) {
//    
//    amountStore += parseInt(amount.value)
//    console.log("amountStore", amountStore);
//}

//Add appointment other category
var countDetails = 1;
var objData = {};
function addOtherservice(IsAddService) {

    //$(".addservice-button").hide();
    //$(".addservice-loader").show();

    
    //Add Other service data [Start] ==========================================  
    objData = {
        sd_serviceDetailsId: $("#ServiceDetailsId_" + countDetails).val(),
        sd_categoryName: $("#categoryName_" + countDetails).val(),
        sd_serviceName: $("#serviceName_" + countDetails).val(),
        sd_userAssignto: $("#userAssignto_" + countDetails).val(),
        sd_appointmentsPrice: $("#appointmentsPrice_" + countDetails).val(),
        sd_appointmentsDuration: $("#appointmentsDuration_" + countDetails).val(),
        IsValidObj: true,
    }

    if (objData.IsValidObj == true){
        $.ajax({
            type: "POST",
            url: APIEndPoint + "/api/userAppointments/AppointmentServiceDetails_Upsert",
            headers: { "Authorization": '' + DeviceTokenNumber + '' },
            dataType: "json",
            data: ({
                Id: objData.sd_serviceDetailsId,
                AppointmentId: getAppoinmentDetailsIdatob,
                CategoryId: objData.sd_categoryName,
                ServiceId: objData.sd_serviceName.toString(),
                AssignedToUserId: objData.sd_userAssignto,
                Price: objData.sd_appointmentsPrice,
                Duration: objData.sd_appointmentsDuration
            }),
            crossDomain: true,
            success: function (result) {
                
                //console.log("result", result)
                //var OldPrice = result.Item.OldPrice == null || result.Item.OldPrice == "" ? 0 : result.Item.OldPrice;
                //var amountStore = parseFloat($("#appointmentsTotalPrice").val()) - OldPrice;
                if (result.Code == 200 && result.Message != "Success.. your service details update in your appointment")
                {
                    $("#ServiceDetailsId_" + countDetails).val(result.Item.Id);
                }

                //$(".addservice-button").show();
                //$(".addservice-loader").hide();
            }, error: function (error) {
                //$(".addservice-button").show();
                //$(".addservice-loader").hide();
            }
        });
    }

    //Add Other service data [End] ========================================== 

    if (IsAddService == true) {

        countDetails = countDetails + 1;

        $("#ServiceDetailsId_" + countDetails).val("0");

        $("#otherCategory").append(`
            <div class="card p-3 mb-3" id="CategoryService_${countDetails}" style="box-shadow: 0px 0 12px rgb(173 181 189 / 11%); ">

                <div><a href="javascript:void(0)" onclick="removeCategoryservice(${countDetails} , false)" class="appo-pos-delete"><i class="bb-trash-2 text-danger fs-22 mr-3"></i></a></div>

                <input type="hidden" id="ServiceDetailsId_${countDetails}" value="0" />
                <div class="row">
                    <div class="col-lg-6">
                        <div class="form-group row">
                            <label for="productType" class="col-sm-4 col-form-label text-sm-right">Category</label>
                            <div class="col-sm-8">
                                <select class="selectpicker form-control categoryName" tabindex="2" id="categoryName_${countDetails}" onchange="ServiceName(${countDetails})" title="Select a category" data-live-search="true" data-size="5">
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group row">
                            <label for="productType" class="col-sm-4 col-form-label text-sm-right">Service Name</label>
                            <div class="col-sm-8">
                                <select class="selectpicker form-control" tabindex="3" id="serviceName_${countDetails}" onchange="parentUserAssignto(${countDetails}) , appoinmentEmployeeWorkingHourse(${countDetails})" title="Select a service" data-live-search="true" data-size="5" multiple disabled>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="form-group row">
                            <label for="assignTo" class="col-sm-2 col-form-label text-sm-right">Assigned to</label>
                            <div class="col-sm-10">
                                <select class="selectpicker form-control" id="userAssignto_${countDetails}" tabindex="6" name="userAssignto" onchange="getUserPriceDuration();" title="Select a Assign to" data-live-search="true" data-size="5">
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group row mb-0 align-items-center">
                            <label for="purchasePrice" class="col-sm-4 col-form-label text-sm-right mb-0">Price</label>
                            <div class="col-sm-8">
                                <div class="input-group">
                                    <h5 style="color: #363636; " class="mb-0" id="appointmentsPricetext_${countDetails}">0.00 SAR</h5>
                                    <input type="hidden" class="form-control w-100" tabindex="7" id="appointmentsPrice_${countDetails}" name="appointmentsPrice" placeholder="120">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group row mb-0 align-items-center">
                            <label for="productWeight" class="col-sm-4 col-form-label text-sm-right mb-0">Duration</label>
                            <div class="col-sm-8">
                                <div class="input-group">
                                    <h5 style="color: #363636;" class="mb-0" id="appointmentsDurationtext_${countDetails}">0.00 Min</h5>
                                    <input type="hidden" class="form-control w-100" tabindex="8" id="appointmentsDuration_${countDetails}" name="appointmentsDuration" placeholder="250">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        `)

        CategoryName(countDetails);
        userAssignto(countDetails);
    }
}

/// Create object in category service

function AServiceCategory() {
    if (countDetails > 0) {
        var AServiceCategoryDetails = [];

        for (var i = 0; i < countDetails; i++) {
            var index = i + 1;
            if (
                (
                    $("#categoryName_" + index).val() != "" &&
                    $("#serviceName_" + index).val() != "" &&
                    $("#userAssignto_" + index).val() != "" &&
                    $("#appointmentsPrice_" + index).val() != "" &&
                    $("#appointmentsDuration_" + index).val() != ""
                )
                &&
                (
                    $("#categoryName_" + index).val() != null &&
                    $("#serviceName_" + index).val() != null &&
                    $("#userAssignto_" + index).val() != null &&
                    $("#appointmentsPrice_" + index).val() != null &&
                    $("#appointmentsDuration_" + index).val() != null
                )
            )
            {
                objData = {
                    sd_serviceDetailsId: $("#ServiceDetailsId_" + index).val(),
                    sd_categoryName: $("#categoryName_" + index).val(),
                    sd_serviceName: $("#serviceName_" + index).val().toString(),
                    sd_userAssignto: $("#userAssignto_" + index).val(),
                    sd_appointmentsPrice: $("#appointmentsPrice_" + index).val(),
                    sd_appointmentsDuration: $("#appointmentsDuration_" + index).val()
                }

                AServiceCategoryDetails.push(objData);

                DataValid = true;

            }
            else {
                DataValid = false;
                $("#CategoryService_" + index).addClass("service-error");
            }
        }

        $("#aServiceCategoryDetails").val(JSON.stringify(AServiceCategoryDetails));
    }
}

//appoinmentDetails edit API call in ajax methos

function appoinmentDetailsedit() {


    if (getAppoinmentDetailsIdatob > 0) {

        $('#newCustomeradd').hide();

        $('#appointmentFormloader').show();
        $('#appointmentForm').hide();

        //$("#userAssignto").html(``);
        //$('#userAssignto').attr('disabled', true);


        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/userAppointments/UserAppointments_ById?Id=' + getAppoinmentDetailsIdatob + '',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                

                console.log("result", result);


                //var ServicesIdsarr = result.Item.ServicesIds;
                //var ServicesIdsarrsplit = "";
                //if (ServicesIdsarr != null && ServicesIdsarr != "") {
                //    ServicesIdsarrsplit = ServicesIdsarr.split(',');
                //}

                $('#appointmentId').val(result.Item.Id)
                $('#appointmentsComment').val(result.Item.Comment);
                //$('#appointmentsPrice').val(result.Item.Price);
                $('#appointmentsTotalPrice').val(result.Item.TotalPrice);
                $('#appointmentsTotalPricetext').text(result.Item.TotalPrice.toFixed(2) + " " + "SAR");
                //$('#appointmentsDuration').val(result.Item.Duration);
                $('#appointmentsDate').val(result.Item.AppointmentDate);
                $('#appointmentsTime').val(result.Item.AppointmentTime);
                $('#customerDatalist').selectpicker('val', result.Item.UserId);
                //$('#categoryName').selectpicker('val', result.Item.CategoryId);
                //$('#categoryName').val(result.Item.CategoryId);
                //$('#categoryName').trigger('change');

                //ServiceName();
                //userAssignto();

                //$('#userAssignto').selectpicker('val', result.Item.AssignedToUserId);
                //$('#serviceName').selectpicker('val', ServicesIdsarrsplit);

                if (result.Item.ServiceDetailsObj != null) {
                    for (var i = 0; i < result.Item.ServiceDetailsObj.length; i++) {
                        countDetails = i + 1;
                        if (i == 0) {
                            CategoryName(countDetails);
                            var ServicesIdsarr = result.Item.ServiceDetailsObj[i].ServiceId;
                            var ServicesIdsarrsplit = "";
                            if (ServicesIdsarr != null && ServicesIdsarr != "") {
                                ServicesIdsarrsplit = ServicesIdsarr.split(',');
                            }
                            $("#appointmentsPricetext_" + countDetails).text(result.Item.ServiceDetailsObj[i].Price.toFixed(2) + " " + "SAR");
                            $("#appointmentsDurationtext_" + countDetails).text(result.Item.ServiceDetailsObj[i].Duration.toFixed(2) + " " + "Min");
                            $('#ServiceDetailsId_' + countDetails).val(result.Item.ServiceDetailsObj[i].Id);
                            $('#appointmentsPrice_' + countDetails).val(result.Item.ServiceDetailsObj[i].Price);
                            $('#appointmentsDuration_' + countDetails).val(result.Item.ServiceDetailsObj[i].Duration);
                            $("#categoryName_" + countDetails).selectpicker("val", result.Item.ServiceDetailsObj[i].CategoryId);
                            $("#categoryName_" + countDetails).trigger('change');
                            $("#serviceName_" + countDetails).selectpicker('val', ServicesIdsarrsplit);
                            userAssignto(countDetails);
                            $("#userAssignto_" + countDetails).selectpicker("val", result.Item.ServiceDetailsObj[i].AssignedToUserId);
                        } else {
                            $("#otherCategory").append(`
                                <div class="card p-3 mb-3" id="CategoryService_${countDetails}" style="box-shadow: 0px 0 12px rgb(173 181 189 / 11%); ">
                                    <div><a href="javascript:void(0)" onclick="removeCategoryservice(${countDetails} , true , ${result.Item.ServiceDetailsObj[i].Id})" class="appo-pos-delete"><i class="bb-trash-2 text-danger fs-22 mr-3"></i></a></div>

                                    <input type="hidden" id="ServiceDetailsId_${countDetails}" value="0" />
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="form-group row">
                                                <label for="productType" class="col-sm-4 col-form-label text-sm-right">Category</label>
                                                <div class="col-sm-8">
                                                    <select class="selectpicker form-control categoryName" tabindex="2" id="categoryName_${countDetails}" onchange="ServiceName(${countDetails})" title="Select a category" data-live-search="true" data-size="5">
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group row">
                                                <label for="productType" class="col-sm-4 col-form-label text-sm-right">Service Name</label>
                                                <div class="col-sm-8">
                                                    <select class="selectpicker form-control" tabindex="3" id="serviceName_${countDetails}" onchange="parentUserAssignto(${countDetails}) , appoinmentEmployeeWorkingHourse(${countDetails})" title="Select a service" data-live-search="true" data-size="5" multiple disabled>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-12">
                                            <div class="form-group row">
                                                <label for="assignTo" class="col-sm-2 col-form-label text-sm-right">Assigned to</label>
                                                <div class="col-sm-10">
                                                    <select class="selectpicker form-control" id="userAssignto_${countDetails}" tabindex="6" name="userAssignto" onchange="getUserPriceDuration();" title="Select a Assign to" data-live-search="true" data-size="5">
                                                    </select>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-6">
                                            <div class="form-group row mb-0 align-items-center">
                                                <label for="purchasePrice" class="col-sm-4 col-form-label text-sm-right mb-0">Price</label>
                                                <div class="col-sm-8">
                                                    <div class="input-group">
                                                        <h5 style="color: #363636; " class="mb-0" id="appointmentsPricetext_${countDetails}">0.00 SAR</h5>
                                                        <input type="hidden" class="form-control w-100" tabindex="7" id="appointmentsPrice_${countDetails}" value="${result.Item.ServiceDetailsObj[i].Price}" name="appointmentsPrice" placeholder="120">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-6">
                                            <div class="form-group row mb-0 align-items-center">
                                                <label for="productWeight" class="col-sm-4 col-form-label text-sm-right mb-0">Duration</label>
                                                <div class="col-sm-8">
                                                    <div class="input-group">
                                                        <h5 style="color: #363636;" class="mb-0" id="appointmentsDurationtext_${countDetails}">0.00 Min</h5>
                                                        <input type="hidden" class="form-control w-100" tabindex="8" id="appointmentsDuration_${countDetails}" value="${result.Item.ServiceDetailsObj[i].Duration}" name="appointmentsDuration" placeholder="250">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            `)

                            
                            CategoryName(countDetails);
                            var ServicesIdsarr = result.Item.ServiceDetailsObj[i].ServiceId;
                            var ServicesIdsarrsplit = "";
                            if (ServicesIdsarr != null && ServicesIdsarr != "") {
                                ServicesIdsarrsplit = ServicesIdsarr.split(',');
                            }
                            $("#appointmentsPricetext_" + countDetails).text(result.Item.ServiceDetailsObj[i].Price.toFixed(2) + " " + "SAR");
                            $("#appointmentsDurationtext_" + countDetails).text(result.Item.ServiceDetailsObj[i].Duration.toFixed(2) + " " + "Min");
                            $('#ServiceDetailsId_' + countDetails).val(result.Item.ServiceDetailsObj[i].Id);
                            $('#appointmentsPrice_' + countDetails).val(result.Item.ServiceDetailsObj[i].Price);
                            $('#appointmentsDuration_' + countDetails).val(result.Item.ServiceDetailsObj[i].Duration);
                            $("#categoryName_" + countDetails).selectpicker("val", result.Item.ServiceDetailsObj[i].CategoryId);
                            $("#categoryName_" + countDetails).trigger('change');
                            $("#serviceName_" + countDetails).selectpicker('val', ServicesIdsarrsplit);
                            //$("#serviceName_" + countDetails).trigger('change');
                            userAssignto(countDetails);
                            $("#userAssignto_" + countDetails).selectpicker("val", result.Item.ServiceDetailsObj[i].AssignedToUserId);

                        }
                    }
                }

                $('#appointmentFormloader').hide();
                $('#appointmentForm').show();

            }, error: function (error) {
                // Error function
                $('#appointmentFormloader').hide();
                $('#appointmentForm').show();
            }
        });
    }

    return false;
}

//employeeTypedrp dropdown API call in ajax method
function customerData() {

    var CustomerData = new Object();
    CustomerData.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_All?search&LookUpUserTypeId=4&SalonId=' + atob(SalonId) + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(CustomerData),
        crossDomain: true,
        async: false,
        success: function (result) {
            result.Values.reverse();
            $("#customerDatalist").html(``);

            for (i = 0; i < result.Values.length; i++) {
                $('#customerDatalist').append(`
                 <option value="${result.Values[i].Id}">${result.Values[i].UserName}  -  ${result.Values[i].Email}</option>
                `);
                $('.selectpicker').selectpicker("refresh");
            }

        }, error: function (error) {
            // Error function
        }
    });
    return false;
}


//CategoryName dropdown API call in ajax methos

function CategoryName() {
    
    let CategoryName = new Object();
    CategoryName.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpServices/LookUpServices_All?search&ParentId=0&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(CategoryName),
        crossDomain: true,
        async: false,
        success: function (result) {
            $("#categoryName_" + countDetails).html(``);

            for (i = 0; i < result.Values.length; i++) {
                $("#categoryName_" + countDetails).append(`
                    <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                `);
            }
            $('.selectpicker').selectpicker("refresh");

        }, error: function (error) {
            // Error function
        }
    });
    return false;
}


//$('#categoryName').change(function () {
//    ServiceName();
//});


//ServiceName dropdown API call in ajax methos

function ServiceName(categoryId) {
    
    $("#serviceName_" + categoryId).html(``);
    $("#serviceName_" + categoryId).attr('disabled', true);

    let ServiceName = new Object();
    ServiceName.IsPageProvided = true;

    let serviceParentid = $("#categoryName_" + categoryId).val();

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpServices/LookUpServices_All?search&ParentId=${serviceParentid}&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(ServiceName),
        crossDomain: true,
        async: false,
        success: function (result) {

            $("#serviceName_" + categoryId).html(``);

            console.log("Service result", result);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $("#serviceName_" + categoryId).append(`
                        <option priceduration_${categoryId}="${result.Values[i].Price},${result.Values[i].Duration}"  value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                    `);
                    $("#serviceName_" + categoryId).removeAttr("disabled");
                }
            }

            $('.selectpicker').selectpicker("refresh");

        }, error: function (error) {
            // Error function
        }
    });
    return false;
}

//ServiceName dropdown API call in ajax methos

//function AppointmentsTime() {
//    userAssignto();
//    appoinmentEmployeeWorkingHourse();
//}


function parentUserAssignto(countDetailsID) {
    userAssignto(countDetailsID)
}
function userAssignto(countDetailsID) {
    let UserAssignto = new Object();
    UserAssignto.IsPageProvided = true;

    if ($("#categoryName_" + countDetailsID).val() == '') {
        var categoryId = 0;
    }
    else {
        var categoryId = $("#categoryName_" + countDetailsID).val();
    }
    
    var SumTime = 0;
    if (countDetails > 0) {
        for (var i = 1; i < countDetails; i++) {
            SumTime = parseFloat(SumTime) + parseFloat($("#appointmentsDuration_" + i).val());
        }
    }

    var AppointmentsTime = moment.utc($('#appointmentsTime').val(), 'hh:mm').add(SumTime, 'minutes').format('hh:mm');

    var AppointmentsDate = $('#appointmentsDate').val();
    var ServicesId = $("#serviceName_" + countDetailsID).val().toString();

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/userAppointments/UserAppointments_GetAssignTo?search&SalonId=' + atob(SalonId) + '&CategoryId=' + categoryId + '&ServicesIds=' + ServicesId + '&AppointmentDate=' + AppointmentsDate + '&AppointmentTime=' + AppointmentsTime + '&UserAppointmentId=' + getAppoinmentDetailsIdatob + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(UserAssignto),
        crossDomain: true,
        async: false,
        success: function (result) {
            $("#userAssignto_" + countDetails).html(``);
            
            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $("#userAssignto_" + countDetails).append(`
                        <option value="${result.Values[i].Id}">
                            ${result.Values[i].FirstName} ${result.Values[i].SecondName} -  ${result.Values[i].Email} - ${result.Values[i].LastAppointmentTime == '' || result.Values[i].LastAppointmentTime == null ? 'Available' : 'Next Appointment' + ' : ' + result.Values[i].LastAppointmentDate + '  ' + onTimeChange(result.Values[i].LastAppointmentTime)}
                        </option>
                    `);

                    //$('#userAssignto').append(`
                    //    <option data-value="${result.Values[i].TotalPrice},${result.Values[i].TotalDuration}" value="${result.Values[i].Id}">
                    //        ${result.Values[i].FirstName} ${result.Values[i].SecondName} -  ${result.Values[i].Email} - ${result.Values[i].LastAppointmentTime == '' || result.Values[i].LastAppointmentTime == null ? 'Available' : 'Next Appointment' + ' : ' + result.Values[i].LastAppointmentDate + '  ' + onTimeChange(result.Values[i].LastAppointmentTime)}
                    //    </option>
                    //`);

                }
            }
            $('.selectpicker').selectpicker("refresh");

        }, error: function (error) {
            // Error function
        }
    });
    return false;
}


function NunConvert(eventVal) {
    return ~~eventVal;
}

//Select category and service assign employee working houese show



function appoinmentEmployeeWorkingHourse(sId) {
    var TimeAndDuration = $(`#serviceName_${sId} :selected`).map((i, el) => $(el).attr("priceduration_" + sId)).toArray();

    if (TimeAndDuration != null || TimeAndDuration != "") {
        $("#appointmentsPrice_" + sId).val("");
        $("#appointmentsDuration_" + sId).val("");

        $("#appointmentsPricetext_" + sId).text("0.00 SAR");
        $("#appointmentsDurationtext_" + sId).text("0.00 Min");

        for (var t = 0; t < TimeAndDuration.length; t++) {
            var TValue = TimeAndDuration[t].split(',');
           
            var alreadyValP = $("#appointmentsPrice_" + sId).val();
            var alreadyValD = $("#appointmentsDuration_" + sId).val();

            $("#appointmentsPrice_" + sId).val(parseInt(NunConvert(alreadyValP)) + parseInt(TValue[0]));
            $("#appointmentsDuration_" + sId).val(parseInt(NunConvert(alreadyValD)) + parseInt(TValue[1]));

            $("#appointmentsPricetext_" + sId).text((parseInt(NunConvert(alreadyValP)) + parseInt(TValue[0])).toFixed(2) + " " + "SAR");
            $("#appointmentsDurationtext_" + sId).text((parseInt(NunConvert(alreadyValD)) + parseInt(TValue[1])).toFixed(2) + " " + "Min");
        }
        
        var TPrice = document.getElementsByName('appointmentsPrice');
        var STotalAmount = 0;
        for (var tp = 0; tp < TPrice.length; tp++) {
            STotalAmount += NunConvert(parseFloat(TPrice.item(tp).value));
        }

        $("#appointmentsTotalPricetext").text(parseFloat(STotalAmount).toFixed(2) + " " + "SAR");
        $("#appointmentsTotalPrice").val(STotalAmount)
    }

    $('#employeeWorkingHourse').html(``);
    $('#employeeWorkingHourse').append(`
        <h6 class="text-center text-muted mb-0" style="font-size:20px;">${Langaugestore.NoRecordsFound}</h6>
    `)
    
    let AppoinmentEmployeeWorkingHourse = new Object();
    AppoinmentEmployeeWorkingHourse.IsPageProvided = true;

    var ServicesIds = $("#serviceName_" + sId).val().toString();
    var CategoryId = $("#categoryName_" + sId ).val();

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/userWorkingHours/EmployeeWorkingHour_ServicesId?search&SalonId=${atob(SalonId)}&CategoryId=${CategoryId}&ServicesIds=${ServicesIds}`,
        headers: { 'Content-Type': 'application/json' },
        dataType: 'json',
        data: JSON.stringify(AppoinmentEmployeeWorkingHourse),
        crossDomain: true,
        success: function (result) {
            
           
            if (result.Values.length <= 0) {
                $('#employeeWorkingHourse').html(``);
                $('#employeeWorkingHourse').append(`
                     <h6 class="text-center text-muted mb-0" style="font-size:20px;">${Langaugestore.NoRecordsFound}</h6>
                `)
            } else {
                $('#employeeWorkingHourse').html(``);
            }

            var UserIdArr = [];
            for (i = 0; i < result.Values.length; i++) {
                var workingHourse = "";

                var UserId = result.Values[i].UserId;
                debugger;
                if (!UserIdArr.includes(UserId)) {
                    for (j = 0; j < result.Values.length; j++) {
                        if (UserId == result.Values[j].UserId) {
                            workingHourse +=
                             `<tr>
                                <th>${result.Values[j].Day}</th>
                                <td>${result.Values[j].StartTime == null || result.Values[j].StartTime == "" ? "0.00" : onTimeChange(result.Values[j].StartTime)}</td>
                                <td>${result.Values[j].EndTime == null || result.Values[j].EndTime == "" ? "0.00" : onTimeChange(result.Values[j].EndTime)}</td>
                            </tr>`
                        }
                    }

                    $('#employeeWorkingHourse').append(`
                        <div class="table-responsive table-card mb-3">
                            <h6 class="mb-0 pb-3 pt-3 pl-2 collapseHeading${i}" data-toggle="collapse" href="#multiCollapseExample${i}" role="button" aria-expanded="false" aria-controls="multiCollapseExample1" style="cursor:pointer;">
                                <i class="bb-user"></i>
                                ${result.Values[i].UserName}
                                <i class="bb-chevron-down float-right mr-2"></i>
                            </h6>
                            <table class="table table-hover collapse" data-parent="#employeeWorkingHourse" id="multiCollapseExample${i}">
                                <thead>
                                    <tr>
                                        <th class="bg-soft-primary text-primary w-lg-50">${Langaugestore.Day}</th>
                                        <th class="bg-soft-primary text-primary">${Langaugestore.Start}</th>
                                        <th class="bg-soft-primary text-primary">${Langaugestore.End}</th>
                                    </tr>
                                </thead>
                                <tbody>
                                   ${workingHourse}
                                </tbody>
                            </table>
                        </div>
                    `);
                }
                $('.collapseHeading0').click();
                UserIdArr.push(UserId);
                console.log('UserIdArr', UserIdArr);

            }



            console.log('workingHourse', workingHourse)
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}



