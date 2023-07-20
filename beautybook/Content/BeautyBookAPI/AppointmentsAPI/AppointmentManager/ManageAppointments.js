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

// appoinmentListfillter funcation
function appoinmentListfillter() {
    $('#appoinmentSearch').hide();
    $('#appoinmentSearchloading').show();
    appoinmentList.init();
}

//resetappoinmentList funcation
function resetappoinmentList() {

    $('#appoinmentReset').hide();
    $('#appoinmentloading').show();

    $('#appoinmentDate').val('');
    $('#appoinmentTime').val('');
    $('#appoinmentStatus').val(null);
    $('#customerDatalist').val(0);
    $('#appoinmentAssignedTo').val(0);
    $('.selectpicker').selectpicker("refresh");
    appoinmentList.init();
    appoinmentCalendar();
}



//Appoinment list API call in ajax methos
var appoinmentList = function () {

    $('#appoinmentGridloader').show();

    
    let initappoinmentList = function () {


        let AppoinmentList = new Object();
        AppoinmentList.IsPageProvided = true;

        var appointmentDates = $('#appoinmentDate').val();
        var appoinmentTimes = $('#appoinmentTime').val();
        var appoinmentStatus = ~~$('#appoinmentStatus').val();
        var appoinmentCustomer = ~~$('#customerDatalist').val();
        var appoinmentAssignedToUser = ~~$('#appoinmentAssignedTo').val();

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/userAppointments/UserAppointments_All?search&SalonId=${atob(SalonId)}&CustomerId=${appoinmentCustomer}&AssignedToUserId=${appoinmentAssignedToUser}&AppointmentDate=${appointmentDates}&AppointmentTime=${appoinmentTimes}&LookUpStatusId=${appoinmentStatus}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(AppoinmentList),
            crossDomain: true,
            success: function (Values) {
                console.log(Values);
                
                $('#appoinmentGrid').DataTable({
                    "order": [[0, "desc"]],
                    data: Values.Values,

                    columns: [
                        {
                            "title": "ID", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<a href="javascript:void(0)" onclick="appoinmentUserId(${row["Id"]})" data-toggle="modal" data-target="#appointmentModal" class="link">${row["Id"]}</a>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },
                        {
                            "title": ""+Langaugestore.CustomerName+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `
                                    <span class="avatar avatar-primary avatar-circle">
                                      <img onerror="this.src='/Content/assets/images/default-avatar.png'" src="${APIEndPoint}/${row["CustomerProfileUrl"]}" class="custome-profile-avatar" alt="User Profile"/>
                                    </span>
                                    <a href="javascript:void()" onclick="customerDetails(${row["UserId"]});"  class="link ml-2">${row["CustomerUsername"]}</a>
                                `;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Gender+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["CustomerGender"] == "" || row["CustomerGender"] == null ? '-' : row["CustomerGender"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "2%"
                        },
                        {
                            "title": ""+Langaugestore.Date+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["AppointmentDate"] == "" || row["AppointmentDate"] == null ? '-' : row["AppointmentDate"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "2%"
                        },
                        {
                            "title": "" + Langaugestore.Time +"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["AppointmentTime"] == "" || row["AppointmentTime"] == null ? '-' : onTimeChange(row["AppointmentTime"])}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Services+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["ServiceCount"] == "" || row["ServiceCount"] == null ? '0' : row["ServiceCount"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },
                        {
                            "title": "Total Price", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["TotalPrice"].toFixed(2)}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "2%"
                        },
                        //{
                        //    "title": ""+Langaugestore.Assigned_to+"", "data": "",
                        //    "render": function (data, type, row) {
                        //        let htmlData = "";
                        //        htmlData = `
                        //            <span class="avatar avatar-primary avatar-circle">
                        //                ${row["AssignedToProfile"] == "" || row["AssignedToProfile"] == null ?
                        //                `<span class="avatar-initials">${row["AssignedToFirstname"].charAt(0)}${row["AssignedToSecondName"].charAt(0)}</span>`
                        //                :
                        //                `<img src="${APIEndPoint}/${row["AssignedToProfile"]}" class="custome-profile-avatar" alt="User Profile"/>`
                        //            }
                        //            </span>
                        //            <a data-toggle="modal" href="javascript:void()" onclick="appoinmentModaldetails(${row["AssignedToUserId"]});" data-target="#employeeModal"  class="link ml-2">${row["AssignedToUsername"]}</a>
                        //        `;
                        //        return htmlData;
                        //    }
                        //    , "orderable": false, "width": "3%"
                        //},
                        {
                            "title": ""+Langaugestore.Status+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["LookUpStatusId"] == 6 ? `<div class="badge badge-warning p-2" style="cursor:pointer;">${row["LookUpStatusName"]}</div>` : ''}`;
                                htmlData += `${row["LookUpStatusId"] == 7 ? `<div class="badge badge-danger p-2" style="cursor:pointer;">${row["LookUpStatusName"]}</div>` : ''}`;
                                htmlData += `${row["LookUpStatusId"] == 8 ? `<div class="badge badge-warning p-2" style="cursor:pointer;">${row["LookUpStatusName"]}</div>` : ''}`;
                                htmlData += `${row["LookUpStatusId"] == 9 ? `<div class="badge badge-success p-2" style="cursor:pointer;">${row["LookUpStatusName"]}</div>` : ''}`;
                                htmlData += `${row["LookUpStatusId"] == 10 ? `<div class="badge badge-success p-2" style="cursor:pointer;">${row["LookUpStatusName"]}</div>` : ''}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },
                        {
                            "title": ""+Langaugestore.Actions+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `<div class="dropdown">
                                                <button class="btn btn-icon" type="button" data-toggle="dropdown" id="changeStatus">
                                                    <i class="bb-more-horizontal fs-22"></i>
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right">
                                                    ${PermissionData.find(record => record.ModuleKey === "ViewDetails").Value == true ? `<a class="dropdown-item" href="javascript:void(0)" onclick="appoinmentUserId(${row["Id"]})" data-toggle="modal" data-target="#appointmentModal"><i class="bb-eye text-gray-600 fs-16 mr-3"></i>${Langaugestore.View}</a>` : ""}
                                                    
                                                    ${row["LookUpStatusId "] == 7 || row["LookUpStatusId"] == 10 ? '' :
                                                        `${PermissionData.find(record => record.ModuleKey === "EditAppointment").Value == true ? `<a class="dropdown-item" href="/Appointments/AppointmentDetails?AppoinmentDetailsId=${btoa(row["Id"])}"><i class="bb-edit-3 text-gray-600 fs-16 mr-3"></i>${Langaugestore.EDIT}</a>` : ""}
                                                         ${PermissionData.find(record => record.ModuleKey === "ChangeStatus").Value == true ? `<a class="dropdown-item" href="javascript:void(0)" onclick="changeStatus(${row["Id"]})"><i class="bb-repeat text-gray-600 fs-16 mr-3"></i>Change Status</a>` : ""}`
                                                    }

                                                    ${row["LookUpStatusId"] == 10 && PermissionData.find(record => record.ModuleKey === "ViewInvoice").Value == true ?
                                                        `<a class="dropdown-item" href="javascript:void(0)" onclick="GeneratAppoinmentInvoice(${row["OrignalInvoiceNo"]});">
                                                            <svg style="color:#6c757d;" class="fs-16 mr-3" xmlns="http://www.w3.org/2000/svg" height="18" width="18" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
                                                            </svg>
                                                         Invoice</a>` : ""
                                                    }
                                                   

                                                    ${PermissionData.find(record => record.ModuleKey === "DeleteAppointment").Value == true ? ` <hr> <a class="dropdown-item text-danger" onclick="DeleteAppoinmentManager(${row["Id"]})" href="javascript:void(0)"><i class="bb-trash-2 text-danger fs-16 mr-3"></i>${Langaugestore.Delete}</a>` : ""}
                                                </div>
                                            </div>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },
                        
                    ],
                    buttons: [
                        {
                            extend: 'pdf',
                            className: 'btn btn-light border font-weight-medium float-right mb-3',
                            text: '<i class="bb-printer fs-16 mr-2"></i>' + Langaugestore.Print + '',
                        },
                        {
                            extend: 'excel',
                            className: 'btn btn-light border font-weight-medium float-right mb-3 mr-2',
                            text: '<i class="bb-download fs-16 mr-2"></i>' + Langaugestore.ExporttoExcel + '',
                        },
                    ],
                    responsive: true,
                    "lengthMenu": [
                        [5, 15, 20, 40],
                        [5, 15, 20, 40] // change per page values here
                    ],
                    "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
                });


                $('#appoinmentGridloader').hide();

                $('#appoinmentSearch').show();
                $('#appoinmentSearchloading').hide();

                $('#appoinmentReset').show();
                $('#appoinmentloading').hide();

            }, error: function (error) {
                $('#appoinmentGridloader').hide();

                $('#appoinmentSearch').show();
                $('#appoinmentSearchloading').hide();

                $('#appoinmentReset').show();
                $('#appoinmentloading').hide();
            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#appoinmentGrid")) {
                $('#appoinmentGrid').dataTable().fnDestroy();
                $('#appoinmentGriddiv').html('<table id="appoinmentGrid" class="table table-card" style="width:100%; display:inherit;"></table >');
            }
            initappoinmentList();
        }
    };
}();

//appoinmentAssignedToUser dropdown API call in ajax methos

function appoinmentAssignedUser() {
    

    $("#appoinmentAssignedUser").html(``);

    let AppoinmentAssignedUser = new Object();
    AppoinmentAssignedUser.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/userAppointments/UserAppointments_GetAssignTo?search&SalonId=' + atob(SalonId) + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(AppoinmentAssignedUser),
        crossDomain: true,
        success: function (result) {
            result.Values.reverse();
            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#appoinmentAssignedToUser').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].FirstName} ${result.Values[i].SecondName}</option>
                    `);
                    $('#userAssignto').removeAttr("disabled");
                    $('.selectpicker').selectpicker("refresh");
                }
            }
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}


//employeeList API call in ajax method
function appoinmentAssignedUser() {
    $('#appoinmentAssignedToUser').html('');
    var employeeList = new Object();
    employeeList.IsPageProvided = true;
    
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_All?search&LookUpStatusId=0&LookUpUserTypeId=3&Name&LookUpEmployeeTypeId=0&LookUpEmployeeRolesId=0&SalonId=' + atob(SalonId) + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(employeeList),
        success: function (result) {
            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#appoinmentAssignedTo').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].UserName} ${result.Values[i].SecondName} - ${result.Values[i].Email} + 11</option>
                    `);
                    $('#userAssignto').removeAttr("disabled");
                    $('.selectpicker').selectpicker("refresh");
                }
            }
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}



//appoinment api delete function

//swal Delete employee
function DeleteAppoinmentManager(employeeManagerId) {
    Swal.fire({
        title: '' + Langaugestore.Are_you_sure_you_want_to_delete_this_appoinment__+'',
        //text: "Are you sure Active this employee !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteAppoinmentswal(employeeManagerId);
        }
    })
}

// DeleteEmployeeManager API call in ajax method
function DeleteAppoinmentswal(employeeManagerId) {
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/userAppointments/UserAppointments_Delete?Id=' + employeeManagerId + '&DeletedBy=' + employeeManagerId+'',
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        processData: false,
        contentType: false,
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
                appoinmentList.init();
            }
        }, error: function (error) {
            // Error function
        }
    });
}



