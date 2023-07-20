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
var Languages = getCookie("Languages");

function employeeProfile(employeeId) {
    debugger;
    $('#employeeInfodetails').html(``);
    $('#employeeWorksheet').html(``);
    $('#profileModalbtn').html(``);
    $('#userServicestable').html(``);

    $('#employeeModal').modal('show');
    $("#employeeProfileloader").show();
    $('#employeeWorksheetloader').show();
    $('#userServicestableloader').show();
    $('#AssignToAppointmentslistloader').show();
    leaveCount(employeeId);
    if (employeeId > 0) {
        $.ajax({
            type: 'POST',
            url: APIEndPoint + `/api/users/Users_ById?Id=${employeeId}&IsLanguage=${Languages}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                debugger;
                $('#employeeInfodetails').append(`
                    <div class="col-lg-9">
                        <div class="row">
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Full_Name}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.FirstName}</span>
                                    <span class="font-weight-medium">${result.Item.SecondName}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Gender}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.Gender == '' || result.Item.Gender == null ? '-' : result.Item.Gender}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Email}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.Email == '' || result.Item.Email == null ? '-' : result.Item.Email}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.PHONE}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.PrimaryPhone == '' || result.Item.PrimaryPhone == null ? '-' : result.Item.PrimaryPhone}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Alternate_Phone}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.AlternatePhone == '' || result.Item.AlternatePhone == null ? '-' : result.Item.AlternatePhone}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Joining_Date}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.JoiningDate == '' || result.Item.JoiningDate == null ? '-' : result.Item.JoiningDate}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Role}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.LookUpEmployeeRolesName == '' || result.Item.LookUpEmployeeRolesName == null ? '-' : result.Item.LookUpEmployeeRolesName}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Type}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.LookUpEmployeeTypeName == '' || result.Item.LookUpEmployeeTypeName == null ? '-' : result.Item.LookUpEmployeeTypeName}</span>
                                </div>
                            </div>
                            <div class="col-lg-12 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Adress}</div>
                                <div>
                                    <span class="font-weight-medium">
                                        ${result.Item.AddressLine1 == '' || result.Item.AddressLine1 == null ? '-' : result.Item.AddressLine1} <br/> 
                                        ${result.Item.AddressLine2 == '' || result.Item.AddressLine2 == null ? '' : result.Item.AddressLine2} <br/>
                                    </span>
                                    <span class="font-weight-medium">${result.Item.LookUpCountry == '' || result.Item.LookUpCountry == null ? '-' : result.Item.LookUpCountry}</span>
                                    <span class="font-weight-medium">${result.Item.LookUpState == '' || result.Item.LookUpState == null ? '-' : result.Item.LookUpState}</span>
                                    <span class="font-weight-medium">${result.Item.City == '' || result.Item.City == null ? '-' : result.Item.City}</span>
                                    <span class="font-weight-medium">${result.Item.ZipCode == '' || result.Item.ZipCode == null ? '-' : result.Item.ZipCode}</span>
                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="col-lg-3">
                        <div class="employee-photo-view-mode">
                            ${result.Item.ProfileUrl == null || result.Item.ProfileUrl == "" ?
                        `<span class="avatar-initials avatar avatar-primary avatar-circle custome-defual-profile">${result.Item.FirstName.charAt(0)}${result.Item.SecondName.charAt(0)}</span>`
                        :
                        `<img src="${APIEndPoint}/${result.Item.ProfileUrl}" class="img-fluid rounded shadow-sm" alt="">`
                    }
                        </div>
                    </div>
                `);

                if (result.Item.UserWorkingHours.length <= 0) {
                    $('#employeeWorksheet').html(`
                    <tr>
                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
                    </tr>
                `)
                } else {
                    $("#employeeWorksheet").html(``);
                }

                for (i = 0; i < result.Item.UserWorkingHours.length; i++) {
                    $('#employeeWorksheet').append(`
                        <tr>
                            <th>${result.Item.UserWorkingHours[i].Day}</th>
                            <td>
                                ${result.Item.UserWorkingHours[i].IsAvailable == false ? '-' :
                            `${result.Item.UserWorkingHours[i].StartTime == "" || result.Item.UserWorkingHours[i].StartTime == 0 || result.Item.UserWorkingHours[i].StartTime == null ?
                                '-'
                                :
                                onTimeChange(result.Item.UserWorkingHours[i].StartTime)
                            }`
                        }
                            </td>
                            <td>
                                ${result.Item.UserWorkingHours[i].IsAvailable == false ? '-' :
                            `${result.Item.UserWorkingHours[i].EndTime == "" || result.Item.UserWorkingHours[i].EndTime == 0 || result.Item.UserWorkingHours[i].EndTime == null ?
                                '-'
                                :
                                onTimeChange(result.Item.UserWorkingHours[i].EndTime)
                            }`
                        }
                            </td>
                        </tr>
                    `);
                }


                var categoryIds = [];

                //ServiceIds
                var serviceIds = [];

                if (result.Item.UserServices.length > 0) {
                    for (i = 0; i < result.Item.UserServices.length; i++) {
                        var categoryId = result.Item.UserServices[i].LookUpCategoryId;
                        var serviceName = '';
                        if (!categoryIds.includes(categoryId)) {

                            for (j = 0; j < result.Item.UserServices.length; j++) {
                                if (categoryId == result.Item.UserServices[j].LookUpCategoryId) {

                                    var serviceId = result.Item.UserServices[j].LookUpServicesId;

                                    if (!serviceIds.includes(serviceId) && serviceId > 0) {
                                        serviceIds.push(serviceId);

                                        serviceName += `
                                                <tr>
                                                    <th>
                                                        ${result.Item.UserServices[j].LookUpServicesName}   
                                                        ${result.Item.UserServices[j].LookUpStatusSalonServiceId == 2 ?
                                                `<span class="badge bg-soft-danger text-danger border p-1" style="cursor:pointer;">${Langaugestore.In_active}</span>` : ''
                                            }
                                                    </th>
                                                    <td>${result.Item.UserServices[j].Duration} ${Langaugestore.mins}</td>
                                                    <td>SAR ${result.Item.UserServices[j].Price}</td>
                                                </tr>
                                            `;
                                    }

                                }
                            }

                            categoryIds.push(categoryId);
                            $('#userServicestable').append(`
                                    <div class="table-responsive table-card mb-3">
                                        <table class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th class="bg-soft-primary text-primary w-lg-50">
                                                        ${result.Item.UserServices[i].LookUpCategoryName}
                                                        ${result.Item.UserServices[i].LookUpServicesId == 0 ?
                                    `${result.Item.UserServices[i].LookUpStatusSalonServiceId == 2 ?
                                        `<span class="badge bg-soft-danger text-danger border p-1" style="cursor:pointer;">${Langaugestore.In_active}</span>` : ''
                                    }` : ''
                                }
                                                    </th>
                                                    <th class="bg-soft-primary text-primary">
                                                        ${result.Item.UserServices[i].LookUpServicesId == 0 ?
                                `${result.Item.UserServices[i].Duration} ${Langaugestore.mins}` : `${Langaugestore.Duration}`
                                }
                                                    </th>
                                                    <th class="bg-soft-primary text-primary">
                                                        ${result.Item.UserServices[i].LookUpServicesId == 0 ?
                                `SAR ${result.Item.UserServices[i].Price}` : `${Langaugestore.Price}`
                                }
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                ${serviceName}
                                            </tbody>
                                        </table>
                                    </div>
                                `);
                        }

                    }
                }
                else {
                    $('#userServicestable').append(`
                        <div class="card">
                            <p class="text-center mb-3 mt-3" style="font-size:18px;">${Langaugestore.NoRecordsFound}</p>
                        </div>
                    `);
                }

                // Assign To Appoinment list data append
                if (result.Item.AssignToUserAppointments.length <= 0) {
                    $('#AssignToAppointmentslist').html(`
                    <tr>
                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
                    </tr>
                `)
                } else {
                    $("#AssignToAppointmentslist").html(``);
                }

                for (i = 0; i < result.Item.AssignToUserAppointments.length; i++) {
                    $('#AssignToAppointmentslist').append(`
                        <tr>
                            <th>${result.Item.AssignToUserAppointments[i].Id}</th>
                            <td>
                                ${result.Item.AssignToUserAppointments[i].AppointmentDate == "" || result.Item.AssignToUserAppointments[i].AppointmentDate == null ? '-' : result.Item.AssignToUserAppointments[i].AppointmentDate} 
                                ${result.Item.AssignToUserAppointments[i].AppointmentTime == "" || result.Item.AssignToUserAppointments[i].AppointmentTime == null ? '-' : onTimeChange(result.Item.AssignToUserAppointments[i].AppointmentTime)}
                            </td>
                            <td>${result.Item.AssignToUserAppointments[i].ServicesIdsName == "" || result.Item.AssignToUserAppointments[i].ServicesIdsName == null ? '-' : result.Item.AssignToUserAppointments[i].ServicesIdsName}</td>
                            <td>${result.Item.AssignToUserAppointments[i].CustomerUsername == "" || result.Item.AssignToUserAppointments[i].CustomerUsername == null ? '-' : result.Item.AssignToUserAppointments[i].CustomerUsername}</td>
                            <td>SAR ${result.Item.AssignToUserAppointments[i].Price == "" || result.Item.AssignToUserAppointments[i].Price == null ? '-' : result.Item.AssignToUserAppointments[i].Price}</td>
                            <td>
                                ${result.Item.AssignToUserAppointments[i].LookUpStatusId == 6 ? `<div class="badge bg-soft-warning text-warning border p-2" style="cursor:pointer;">${result.Item.AssignToUserAppointments[i].LookUpStatusName}</div>` : ''}
                                ${result.Item.AssignToUserAppointments[i].LookUpStatusId == 7 ? `<div class="badge bg-soft-danger text-danger border p-2" style="cursor:pointer;">${result.Item.AssignToUserAppointments[i].LookUpStatusName}</div>` : ''}
                                ${result.Item.AssignToUserAppointments[i].LookUpStatusId == 8 ? `<div class="badge bg-soft-warning text-warning border p-2" style="cursor:pointer;">${result.Item.AssignToUserAppointments[i].LookUpStatusName}</div>` : ''}
                                ${result.Item.AssignToUserAppointments[i].LookUpStatusId == 9 ? `<div class="badge bg-soft-success text-success border p-2" style="cursor:pointer;">${result.Item.AssignToUserAppointments[i].LookUpStatusName}</div>` : ''}
                                ${result.Item.AssignToUserAppointments[i].LookUpStatusId == 10 ? `<div class="badge bg-soft-success text-success border p-2" style="cursor:pointer;">${result.Item.AssignToUserAppointments[i].LookUpStatusName}</div>` : ''}
                            </td>
                        </tr>
                    `);
                }


                $('#profileModalbtn').append(`
                    <button type="button" class="btn btn-link" data-dismiss="modal">${Langaugestore.Close}</button>
                    <a href="/Employee/EmployeeDetails?EmployeeId=${btoa(result.Item.Id)}" role="button" class="btn btn-primary">${Langaugestore.Edit_Profile}</a>
                `);

                $('#userServicestableloader').hide();
                $("#employeeProfileloader").hide();
                $('#employeeWorksheetloader').hide();
                $('#AssignToAppointmentslistloader').hide();

            }, error: function (error) {
                $("#employeeProfileloader").hide();
                $('#employeeWorksheetloader').hide();
                $('#userServicestableloader').hide();
                $('#AssignToAppointmentslistloader').hide();
            }
        });
        return false;
    } else { }

}

function leaveCount(employeeId) {

    $('#leaveCountListloader').show();
    var atobSalonId = atob(SalonId);
    var employeeleaveCount = new Object();
    employeeleaveCount.IsPageProvided = true;
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/userLeave/UserLeave_LeaveTypeCount?UserId=' + employeeId + '&SalonId=' + atobSalonId + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(employeeleaveCount),
        success: function (result) {

            if (result.TotalRecords <= 0) {
                $('#leaveCountList').removeClass('row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-xl-4');
                $('#leaveCountList').html(`
                    <div class="card w-100">
                        <p class="text-center mb-3 mt-3" style="font-size:18px;">${Langaugestore.NoRecordsFound}</p>
                    </div>
                `)
            } else {
                $("#leaveCountList").html(``);
            }

            for (i = 0; i < result.Values.length; i++) {
                $('#leaveCountList').addClass('row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-xl-4');
                $('#leaveCountList').append(`
                    <div class="col mb-3">
                        <div class="card shadow-1 py-2">
                            <div class="card-body">
                                <h6>${result.Values[i].LeaveTypeCount}</h6>
                                <p class="text-muted fs-13 mb-0">${result.Values[i].LookUpLeaveType}</p>
                            </div>
                        </div>
                    </div>
                `);
            }

            $('#leaveCountListloader').hide();
        }, error: function (error) {
            $('#leaveCountListloader').hide();
        }
    });
    return false;
}
