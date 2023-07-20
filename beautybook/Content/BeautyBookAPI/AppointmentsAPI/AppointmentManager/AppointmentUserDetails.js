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

function appoinmentUserId(AppoinmentUserdetails) {
    
    $('#profileModalbtn').html(``);
    $('#appointmentUserdetails').html(``);
    $('#appointmentUserdetailsloader').show();
    if (AppoinmentUserdetails > 0) {
        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/userAppointments/UserAppointments_ById?Id='+AppoinmentUserdetails+'',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {

                console.log("Appointment Details", result);

                if (result.Item.LookUpStatusId == 7 || result.Item.LookUpStatusId == 10) {
                    $('#profileModalbtn').hide();
                } else {
                    $('#profileModalbtn').show();
                }

                var AssignedToUsersData = "";

                if (result.Item.ServiceDetailsObj != null || result.Item.ServiceDetailsObj != "") {
                    debugger;
                    for (var i = 0; i < result.Item.ServiceDetailsObj.length; i++) {
                        AssignedToUsersData +=
                        `<div class="col-lg-6 p-2">
                            <div class="card p-2 shadow-1 hover-border-primary rounded-sm h-100" data-toggle="modal" data-target="#serviceModal" role="button">
                                <div class="d-flex align-items-center flex-grow-0">
                                   <span class="avatar avatar-primary avatar-circle">
                                        ${result.Item.ServiceDetailsObj[i].AssignedUserProfile == "" || result.Item.ServiceDetailsObj[i].AssignedUserProfile == null ?
                                            `<span class="avatar-initials">${result.Item.ServiceDetailsObj[i].AssignedUserName.charAt(0)}</span>`
                                            :
                                            `<img src="${APIEndPoint}/${result.Item.ServiceDetailsObj[i].AssignedUserProfile}" class="custome-profile-avatar" alt="User Profile"/>`
                                        }
                                    </span>
                                    <div class="ml-2 chart-users-name">
                                        <h6 class="mb-0">${result.Item.ServiceDetailsObj[i].AssignedUserName}</h6>
                                        <span class="font-weight-normal fs-13">Employee</span>
                                    </div>
                                </div>
                            </div>
                        </div>`
                    }
                }

                $('#appointmentUserdetails').append(`
                     <div class="col-lg-12">

                        <div class="row">
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Appointment_ID}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.Id}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-13 mb-1">${Langaugestore.CustomerName}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.CustomerUsername == "" || result.Item.CustomerUsername == null ? '-' : result.Item.CustomerUsername}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Gender}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.CustomerGender == "" || result.Item.CustomerGender == null ? '-' : result.Item.CustomerGender}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Email}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.CustomerEmail == "" || result.Item.CustomerEmail == null ? '-' : result.Item.CustomerEmail}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Primary_Phone}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.CustomerPrimaryPhone == "" || result.Item.CustomerPrimaryPhone == null ? '-' : result.Item.CustomerPrimaryPhone}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Alternate_Phone}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.CustomerAlternatePhone == "" || result.Item.CustomerAlternatePhone == null ? '-' : result.Item.CustomerAlternatePhone}</span>
                                </div>
                            </div>
                        </div>

                        <hr>

                        <div class="row">
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Appointment_Date}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.AppointmentDate == "" || result.Item.AppointmentDate == null ? '-' : onTimeChange(result.Item.AppointmentDate)}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Appointment_Time}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.AppointmentTime == "" || result.Item.AppointmentTime == null ? '-' : result.Item.AppointmentTime}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Duration}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.ATotalDuration == "" || result.Item.ATotalDuration == null ? '-' : result.Item.ATotalDuration} Min</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Price}</div>
                                <div>
                                    <span class="font-weight-medium">SAR ${result.Item.TotalPrice == "" || result.Item.TotalPrice == null ? '-' : result.Item.TotalPrice}</span>
                                </div>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-12">
                                <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Comment}</div>
                                <p>${result.Item.Comment == "" || result.Item.Comment == null ? '-' : result.Item.Comment}</p>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-12">
                                <div class="text-gray-500 fs-13 mb-1">Assigned To Users</div>
                                <div class="row m-0 mb-3" id="TopNewCustomers">${AssignedToUsersData}</div>
                            </div>
                        </div>
                    </div>
                `);

                 $('#profileModalbtn').append(`
                    <button type="button" class="btn btn-link" data-dismiss="modal">${Langaugestore.Close}</button>
                    ${result.Item.LookUpStatusId == 10 ? `` : `<a href="/Appointments/AppointmentDetails?AppoinmentDetailsId=${btoa(result.Item.Id)}" role="button" class="btn btn-primary">${Langaugestore.EDIT}</a>`}
                `);
                $('#appointmentUserdetailsloader').hide();
             
            }, error: function (error) {
                $('#appointmentUserdetailsloader').hide();
            }
        });
        return false;
    } else { }
}


//<div class="col-lg-6 mb-3">
//    <div class="text-gray-500 fs-13 mb-1">Services Taken</div>
//    <div>
//        <span class="font-weight-medium">${result.Item.ServicesIdsName == "" || result.Item.ServicesIdsName == null ? '-' : result.Item.ServicesIdsName}</span>,
//    </div>
//</div>

//<div class="col-lg-6 mb-3">
//    <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Assign_To}</div>
//    <div>
//        <span class="font-weight-medium">${result.Item.AssignedToUsername == "" || result.Item.AssignedToUsername == null ? '-' : result.Item.AssignedToUsername}</span>
//    </div>
//</div>