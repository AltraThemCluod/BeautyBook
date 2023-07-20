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

//customerDetails api function in ajax methos
function customerProfile(customerDetailsId) {
    $('#customerModalbtn').html(``);
    $('#customerDetails').html(``);
    $('#userAppointmentslist').html(``);
    $('#customerDetailsloader').show();
    $('#userAppointmentslistloader').show();

    $('#customerModal').modal('show');

    if (customerDetailsId > 0) {
        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/users/Users_ById?Id=' + customerDetailsId + '',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                $('#customerDetails').append(`
                     <div class="col-lg-9">
                        <div class="row">
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Full_Name}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.FirstName == "" || result.Item.FirstName == null ? '-' : result.Item.FirstName}</span>
                                    <span class="font-weight-medium">${result.Item.SecondName == "" || result.Item.SecondName == null ? '-' : result.Item.SecondName}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Gender}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.Gender == "" || result.Item.Gender == null ? '-' : result.Item.Gender}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Email}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.Email == "" || result.Item.Email == null ? '-' : result.Item.Email}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.PHONE}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.PrimaryPhone == "" || result.Item.PrimaryPhone == null ? '-' : result.Item.PrimaryPhone}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Alternate_Phone}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.AlternatePhone == "" || result.Item.AlternatePhone == null ? '-' : result.Item.AlternatePhone}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Customer_Since}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.CreatedVisitDate == "" || result.Item.CreatedVisitDate == null ? '-' : result.Item.CreatedVisitDate}</span>
                                </div>
                            </div>


                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Referred_By}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.ReferedByEmail == "" || result.Item.ReferedByEmail == null ? '-' : result.Item.ReferedByEmail}</span>
                                </div>
                            </div>

                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.Appointments}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.AppointmentsCount}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.LastVisit}</div>
                                <div>
                                    <span class="font-weight-medium">${result.Item.UserAppoinmentLastVisitStr}</span>
                                </div>
                            </div>
                            <div class="col-lg-6 mb-3">
                                <div class="text-gray-500 fs-12 mb-1">${Langaugestore.SALES}</div>
                                <div>
                                    <span class="font-weight-medium">SAR ${result.Item.TotalSales == "" || result.Item.TotalSales == null ? '0' : result.Item.TotalSales}</span>
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

                // Appoinment list data append
                if (result.Item.UserAppointments.length <= 0) {
                    $('#userAppointmentslist').html(`
                    <tr>
                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
                    </tr>
                `)
                } else {
                    $("#userAppointmentslist").html(``);
                }

                for (i = 0; i < result.Item.UserAppointments.length; i++) {
                    $('#userAppointmentslist').append(`
                        <tr>
                            <th>${result.Item.UserAppointments[i].Id}</th>
                            <td>
                                ${result.Item.UserAppointments[i].AppointmentDate == "" || result.Item.UserAppointments[i].AppointmentDate == null ? '-' : result.Item.UserAppointments[i].AppointmentDate} 
                                ${result.Item.UserAppointments[i].AppointmentTime == "" || result.Item.UserAppointments[i].AppointmentTime == null ? '-' : onTimeChange(result.Item.UserAppointments[i].AppointmentTime)}
                            </td>
                            <td>${result.Item.UserAppointments[i].ServicesIdsName == "" || result.Item.UserAppointments[i].ServicesIdsName == null ? '-' : result.Item.UserAppointments[i].ServicesIdsName}</td>
                            <td>${result.Item.UserAppointments[i].AssignedToUsername == "" || result.Item.UserAppointments[i].AssignedToUsername == null ? '-' : result.Item.UserAppointments[i].AssignedToUsername}</td>
                            <td>SAR ${result.Item.UserAppointments[i].Price == "" || result.Item.UserAppointments[i].Price == null ? '-' : result.Item.UserAppointments[i].Price}</td>
                            <td>
                                ${result.Item.UserAppointments[i].LookUpStatusId == 6 ? `<div class="badge bg-soft-warning text-warning border p-2" style="cursor:pointer;">${result.Item.UserAppointments[i].LookUpStatusName}</div>` : ''}
                                ${result.Item.UserAppointments[i].LookUpStatusId == 7 ? `<div class="badge bg-soft-danger text-danger border p-2" style="cursor:pointer;">${result.Item.UserAppointments[i].LookUpStatusName}</div>` : ''}
                                ${result.Item.UserAppointments[i].LookUpStatusId == 8 ? `<div class="badge bg-soft-warning text-warning border p-2" style="cursor:pointer;">${result.Item.UserAppointments[i].LookUpStatusName}</div>` : ''}
                                ${result.Item.UserAppointments[i].LookUpStatusId == 9 ? `<div class="badge bg-soft-success text-success border p-2" style="cursor:pointer;">${result.Item.UserAppointments[i].LookUpStatusName}</div>` : ''}
                                ${result.Item.UserAppointments[i].LookUpStatusId == 10 ? `<div class="badge bg-soft-success text-success border p-2" style="cursor:pointer;">${result.Item.UserAppointments[i].LookUpStatusName}</div>` : ''}
                            </td>
                        </tr>
                    `);
                }



                $('#customerModalbtn').append(`
                    <button type="button" class="btn btn-link" data-dismiss="modal">${Langaugestore.Close}</button>
                    <a href="/Customers/CustomerDetails?Id=${btoa(result.Item.Id)}" role="button" class="btn btn-primary">${Langaugestore.Edit_Profile}</a>
                `);

                $('#customerDetailsloader').hide();
                $('#userAppointmentslistloader').hide();

            }, error: function (error) {
                $('#customerDetailsloader').hide();
                $('#userAppointmentslistloader').hide();
            }
        });
        return false;
    } else { }
}
