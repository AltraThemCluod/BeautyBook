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

if (getAppoinmentDetailsIdatob > 0) {
    
    $('.EditStatustext').text('edit');
    $('.EditStatus').text('edit');
}

if (getAppoinmentDetailsIdatob == 0) {
    $('#appointmentForm').show();
}

////appoinmentDetails edit API call in ajax methos

//var countDetailsE = 1;
//function appoinmentDetailsedit() {
    

//    if (getAppoinmentDetailsIdatob > 0) {

//        $('#newCustomeradd').hide();

//        $('#appointmentFormloader').show();
//        $('#appointmentForm').hide();

//        $("#userAssignto").html(``);
//        //$('#userAssignto').attr('disabled', true);

           
//        $.ajax({
//            type: 'POST',
//            url: '' + APIEndPoint + '/api/userAppointments/UserAppointments_ById?Id=' + getAppoinmentDetailsIdatob + '',
//            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
//            dataType: 'json',
//            crossDomain: true,
//            success: function (result) {
//                debugger;

//                console.log("result", result);

//                if (result.Item.ServiceDetailsObj.length > 0) {
//                    for (var i = 0; i < result.Item.ServiceDetailsObj.length; i++) {
//                        countDetailsE = i + 1;
//                        if (i == 0) {
//                            $('#appointmentsPrice_' + countDetailsE).val(result.Item.ServiceDetailsObj[i].Price);
//                            $('#appointmentsDuration_' + countDetailsE).val(result.Item.ServiceDetailsObj[i].Duration);
//                            $("#userAssignto_" + countDetailsE).selectpicker('val', result.Item.ServiceDetailsObj[i].AssignedToUserId);

//                        } else {
//                            $("#otherCategory").append(`
//                                <div class="card p-3 mb-3" id="CategoryService_${countDetailsE}" style="box-shadow: 0px 0 12px rgb(173 181 189 / 11%) !important; ">
//                                    <div><a href="javascript:void(0)" onclick="removeCategoryservice(${countDetailsE})" class="appo-pos-delete"><i class="bb-trash-2 text-danger fs-22 mr-3"></i></a></div>

//                                    <input type="hidden" id="ServiceDetailsId_${countDetailsE}" value="0" />
//                                    <div class="row">
//                                        <div class="col-lg-6">
//                                            <div class="form-group row">
//                                                <label for="productType" class="col-sm-4 col-form-label text-sm-right">Category</label>
//                                                <div class="col-sm-8">
//                                                    <select class="selectpicker form-control categoryName" tabindex="2" id="categoryName_${countDetailsE}" onchange="ServiceName(${countDetailsE})" title="Select a category" data-live-search="true" data-size="5">
//                                                    </select>
//                                                </div>
//                                            </div>
//                                        </div>
//                                        <div class="col-lg-6">
//                                            <div class="form-group row">
//                                                <label for="productType" class="col-sm-4 col-form-label text-sm-right">Service Name</label>
//                                                <div class="col-sm-8">
//                                                    <select class="selectpicker form-control" tabindex="3" id="serviceName_${countDetailsE}" onchange="appoinmentEmployeeWorkingHourse();" title="Select a service" data-live-search="true" data-size="5" multiple disabled>
//                                                    </select>
//                                                </div>
//                                            </div>
//                                        </div>
//                                        <div class="col-lg-12">
//                                            <div class="form-group row">
//                                                <label for="assignTo" class="col-sm-2 col-form-label text-sm-right">Assigned to</label>
//                                                <div class="col-sm-10">
//                                                    <select class="selectpicker form-control" id="userAssignto_${countDetailsE}" tabindex="6" name="userAssignto" onchange="getUserPriceDuration();" title="Select a Assign to" data-live-search="true" data-size="5">
//                                                    </select>
//                                                </div>
//                                            </div>
//                                        </div>

//                                        <div class="col-lg-6">
//                                            <div class="form-group row mb-0">
//                                                <label for="purchasePrice" class="col-sm-4 col-form-label text-sm-right">Price</label>
//                                                <div class="col-sm-8">
//                                                    <div class="input-group">
//                                                        <input type="text" class="form-control w-100" tabindex="7" id="appointmentsPrice_${countDetailsE}" value="${result.Item.ServiceDetailsObj[i].Price}" name="appointmentsPrice" placeholder="120">
//                                                        <div class="input-group-append input-box-label">
//                                                            <span class="input-group-text text-muted">SAR</span>
//                                                        </div>
//                                                    </div>
//                                                </div>
//                                            </div>
//                                        </div>

//                                        <div class="col-lg-6">
//                                            <div class="form-group row mb-0">
//                                                <label for="productWeight" class="col-sm-4 col-form-label text-sm-right">Duration</label>
//                                                <div class="col-sm-8">
//                                                    <div class="input-group">
//                                                        <input type="text" class="form-control w-100" tabindex="8" id="appointmentsDuration_${countDetailsE}" value="${result.Item.ServiceDetailsObj[i].Duration}" name="appointmentsDuration" placeholder="250">
//                                                        <div class="input-group-append input-box-label">
//                                                            <span class="input-group-text text-muted">mins</span>
//                                                        </div>
//                                                    </div>
//                                                </div>
//                                            </div>
//                                        </div>
//                                    </div>
//                                </div>
//                            `)
//                        }

//                        debugger;
//                        CategoryName();
//                        userAssignto();


//                    }
//                }


//                var ServicesIdsarr = result.Item.ServicesIds;
//                var ServicesIdsarrsplit = "";
//                if (ServicesIdsarr != null && ServicesIdsarr != "") {
//                    ServicesIdsarrsplit = ServicesIdsarr.split(',');
//                }

//                $('#appointmentId').val(result.Item.Id)
//                $('#appointmentsComment').val(result.Item.Comment);
//                $('#appointmentsPrice').val(result.Item.Price);
//                $('#appointmentsTotalPrice').val(result.Item.TotalPrice);
//                $('#appointmentsDuration').val(result.Item.Duration);
//                $('#appointmentsDate').val(result.Item.AppointmentDate);
//                $('#appointmentsTime').val(result.Item.AppointmentTime);
//                $('#customerDatalist').selectpicker('val', result.Item.UserId);
//                $('#categoryName').selectpicker('val', result.Item.CategoryId);
//                $('#categoryName').val(result.Item.CategoryId);
//                $('#categoryName').trigger('change');

//                ServiceName();
//                userAssignto();

//                $('#userAssignto').selectpicker('val', result.Item.AssignedToUserId);
//                $('#serviceName').selectpicker('val', ServicesIdsarrsplit);

                
//                $('#appointmentFormloader').hide();
//                $('#appointmentForm').show();

//            }, error: function (error) {
//                // Error function
//                $('#appointmentFormloader').hide();
//                $('#appointmentForm').show();
//            }
//        });
//    }
    
//    return false;
//}