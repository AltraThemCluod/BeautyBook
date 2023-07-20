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
var OwnerId = getCookie("UserId");
var SalonId = getCookie("SalonId");


var getEmaployeeid = new URLSearchParams(window.location.search);
getEmaployeeid = parseInt(atob(getEmaployeeid.get('EmployeeId')));
var getEmaployeeidatob = ~~getEmaployeeid;



function searchEmployeeLeavedata() {
    $('#EmployeeLeaveSearch').hide();
    $('#EmployeeLeaveSearchloading').show();
    EmployeeLeaveList.init();
}


//resetEmployeeLeavedata function call
function resetEmployeeLeavedata() {

    $('#EmployeeLeaveReset').hide();
    $('#EmployeeLeaveResetloading').show();

    $('#employeeDatalistSearch').val(0);
    $('#LookUpLeaveTypeSearch').val(0);
    $('#employeeRole').val(0);
    $('#FromDateSearch').val('');
    $('#ToDateSearch').val('');
    $('#SearchStatus').val(null);
    $('.selectpicker').selectpicker("refresh");
    EmployeeLeaveList.init();
}


//ADD function

var fromDate = "";
var ToDate = "";
$(function () {
    $('input[name="FromDate"]').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        minDate: new Date(),
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear'
        }
    }, function (start, end, label) {
        fromDate = start.format('MM/DD/YYYY');
        toDate();
        noOfdate();
    });
    $('input[name="FromDate"]').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('MM/DD/YYYY'));
        $('input[name="FromDate"]').trigger('keyup');
        fromDate = (picker.startDate.format('MM/DD/YYYY'));
        toDate();
        noOfdate();
    });
    $('input[name="FromDate"]').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });
});

function toDate() {
    $('#ToDate').removeAttr("disabled")
    $('input[name="ToDate"]').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        minDate: new Date(fromDate),
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear'
        }
    }, function (start, end, label) {
        ToDate = start.format('MM/DD/YYYY');
        noOfdate();
    });
    $('input[name="ToDate"]').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('MM/DD/YYYY'));
        $('input[name="ToDate"]').trigger('keyup');
        noOfdate();
    });
    $('input[name="ToDate"]').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });
}

function noOfdate() {
    
    var millisecondsPerDay = 1000 * 60 * 60 * 24;

    var startDay = new Date(fromDate);
    var endDay = new Date(ToDate);

    var millisBetween = endDay.getTime() - startDay.getTime();
    var days = millisBetween / millisecondsPerDay;
    $('#NoOfDays').val(~~Math.floor(days));
}

//EDIT function

var fromDateedit = "";
var ToDateedit = "";
$(function () {
    $('input[name="FromDateEdit"]').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        minDate: new Date(),
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear'
        }
    }, function (start, end, label) {
        $('#FromDateEdit').val(start.format('MM/DD/YYYY'));
        fromDateedit = start.format('MM/DD/YYYY');
        toDateedit();
        noOfdateedit();
    });
    $('input[name="FromDateEdit"]').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('MM/DD/YYYY'));
        $('input[name="FromDateEdit"]').trigger('keyup');
    });
    $('input[name="FromDateEdit"]').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });
});


function toDateedit() {

    var dateedit = new Date(fromDateedit);
    dateedit.setDate(dateedit.getDate() + 1);

    $('input[name="ToDateEdit"]').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        minDate: dateedit,
        autoUpdateInput: false,
        locale: {
            cancelLabel: 'Clear'
        }
    }, function (start, end, label) {
        $('#ToDateEdit').val(start.format('MM/DD/YYYY'));
        ToDateedit = start.format('MM/DD/YYYY');
        noOfdateedit();
    });
    $('input[name="ToDateEdit"]').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('MM/DD/YYYY'));
        $('input[name="ToDateEdit"]').trigger('keyup');
        ToDateedit = picker.startDate.format('MM/DD/YYYY');
        noOfdateedit();
    });
    $('input[name="ToDateEdit"]').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });
}

function noOfdateedit() {
     
    var millisecondsPerDay = 1000 * 60 * 60 * 24;

    var startDay = new Date(fromDateedit);
    var endDay = new Date(ToDateedit);

    var millisBetween = endDay.getTime() - startDay.getTime();
    var days = millisBetween / millisecondsPerDay;
    $('#NoOfDaysedit').val(~~Math.floor(days + 1));
}


// employee/User Leave add api call in ajax methos
function employeeLeaveAdd() {
    

    var UserLeave = new Object();
    UserLeave.Id = $('#Id').val();
    UserLeave.SalonId = atob(SalonId);
    if ($('#Id').val() == 0) {
        $('#employeeLeaveBtn').hide();
        $('#employeeLeaveBtnloading').show();
        UserLeave.UserId = $('#employeeDatalist').val();
        UserLeave.LookUpLeaveTypeId = $('#LookUpLeaveType').val();
        UserLeave.FromDate = $('#FromDate').val();
        UserLeave.ToDate = $('#ToDate').val();
        UserLeave.NoOfDays = $('#NoOfDays').val();
        UserLeave.Reason = $('#Reason').val();
    }
    else {
        $('#employeeLeaveEditBtn').hide();
        $('#employeeLeaveEditBtnloading').show();

        UserLeave.UserId = $('#employeeDatalistEdit').val();
        UserLeave.LookUpLeaveTypeId = $('#LookUpLeaveTypeEdit').val();
        UserLeave.FromDate = $('#FromDateEdit').val();
        UserLeave.ToDate = $('#ToDateEdit').val();
        UserLeave.NoOfDays = $('#NoOfDaysedit').val();
        UserLeave.Reason = $('#ReasonEdit').val();
    }

    UserLeave.CreatedBy = atob(OwnerId);
    UserLeave.UpdatedBy = atob(OwnerId);

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/userLeave/UserLeave_Upsert',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(UserLeave),
        crossDomain: true,
        success: function (result) {
            $('#addLeaveModal').modal('hide');
            $('#leaveEditModal').modal('hide');
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 2000
                })
                EmployeeLeaveList.init();
            }

            $('#employeeLeaveBtn').show();
            $('#employeeLeaveBtnloading').hide();
            $('#employeeLeaveEditBtn').show();
            $('#employeeLeaveEditBtnloading').hide();

        }, error: function (error) {
            
            if (error.status == 400) {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: error.responseJSON.Message,
                    showConfirmButton: false,
                    timer: 2000
                })
            }

            if (error.status == 403) {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: error.responseJSON.Message,
                }).then((result) => {
                    if (result.isConfirmed) {
                        $('#addLeaveModal').modal('hide');
                        $('#employee-tab').removeClass('active');
                        $('#employee').removeClass('active');
                        $('#AssignToAppointments-tab').addClass('active');
                        $('#AssignToAppointments').addClass('active');
                        $('#employeeModal').modal('show');
                        employeeProfile($('#employeeDatalist').val() == '' || $('#employeeDatalist').val() == null ? $('#employeeDatalistEdit').val() : $('#employeeDatalist').val());
                    }
                })
            }

            $('#employeeLeaveBtn').show();
            $('#employeeLeaveBtnloading').hide();
            $('#employeeLeaveEditBtn').show();
            $('#employeeLeaveEditBtnloading').hide();
        }
    });
    return false;
}

//employeeTypedrp dropdown API call in ajax methos

function employeeData() {

    var CustomerData = new Object();
    CustomerData.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_All?search&LookUpUserTypeId=3&SalonId=' + atob(SalonId) + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(CustomerData),
        crossDomain: true,
        success: function (result) {
            result.Values.reverse();
            $("#employeeDatalist").html(``);

            for (i = 0; i < result.Values.length; i++) {
                $('#employeeDatalist').append(`
                 <option value="${result.Values[i].Id}">${result.Values[i].UserName}  -  ${result.Values[i].Email}</option>
                `);
                $('#employeeDatalistEdit').append(`
                 <option value="${result.Values[i].Id}">${result.Values[i].UserName}  -  ${result.Values[i].Email}</option>
                `);
                $('#employeeDatalistSearch').append(`
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

function SelectLeaveType() {
    
    var SelectLeave = new Object();
    SelectLeave.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/lookUpLeaveType/LookUpLeaveType_All?search',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(SelectLeave),
        crossDomain: true,
        success: function (result) {
            
            result.Values.reverse();

            $("#LookUpLeaveType").html(``);

            for (i = 0; i < result.Values.length; i++) {
                $('#LookUpLeaveType').append(`
                 <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                `);
                $('#LookUpLeaveTypeEdit').append(`
                 <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                `);
                $('#LookUpLeaveTypeSearch').append(`
                 <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                `);
                $('.selectpicker').selectpicker("refresh");
            }
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}

function EmployeeLeaveDataSearch() {
    $('#Searchbtn').hide();
    $('#Searchbtnloading').show();
    EmployeeLeaveList.init();
}

//ResetServiceData function 
function ResetEmployeeLeaveData() {
    $('#ResetServiceDatabtn').hide();
    $('#ResetServiceDatabtnloading').show();

    $('#LookUpLeaveTypeSearch').val("");
    $('#SearchStatus').val(null);
    $('#FromDateSearch').val("");
    $('#ToDateSearch').val("");
    $('#LookUpStatusId').val(0);
    $('#employeeDatalistSearch').val(0);
    $('.selectpicker').selectpicker("refresh");
    EmployeeLeaveList.init();
}

//employeeList API call in ajax method
var EmployeeLeaveList = function (){

    let initEmployeeLeaveManageMentList = function () {
        let LookUpLeaveTypeId = 0, LookUpStatusId = 0, LookUpEmployeeRolesId = 0; UserId = 0;
        if (parseInt($('#LookUpLeaveTypeSearch').val()) > 0) {
            LookUpLeaveTypeId = parseInt($('#LookUpLeaveTypeSearch').val());
        }
        if (parseInt($('#SearchStatus').val()) > 0) {
            LookUpStatusId = parseInt($('#SearchStatus').val());
        }
        if (parseInt($('#employeeRole').val()) > 0) {
            LookUpEmployeeRolesId = parseInt($('#employeeRole').val());
        }
        if (parseInt($('#employeeDatalistSearch').val()) > 0) {
            UserId = parseInt($('#employeeDatalistSearch').val());
        }
        $('#employeeLeaveManagementloader').show();

        let employeeLeaveList= new Object();
        employeeLeaveList.IsPageProvided = true;

        var atobSalonId = atob(SalonId);
        var FromDates = $('#FromDateSearch').val();
        var ToDates = $('#ToDateSearch').val();

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: '' + APIEndPoint + '/api/userLeave/UserLeave_All?search&UserId=' + UserId + '&SalonId=' + atobSalonId + '&LookUpLeaveTypeId=' + LookUpLeaveTypeId + '&FromDate=' + FromDates + '&ToDate=' + ToDates + '&LookUpStatusId=' + LookUpStatusId + '&LookUpEmployeeRolesId=' + LookUpEmployeeRolesId + '',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(employeeLeaveList),
            crossDomain: true,
            success: function (Values) {
                console.log(Values);
                $('#employeeLeaveManagementListing').DataTable({
                    "order": [[0, "desc"]],
                    data: Values.Values,

                    columns: [
                        {
                            "title": ""+Langaugestore.EmployeeName+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `
                                    <span class="avatar avatar-primary avatar-circle ">
                                       <img onerror="this.src='/Content/assets/images/default-avatar.png'" src="${APIEndPoint}/${row["ProfileImage"]}" class="custome-profile-avatar" alt="User Profile"/>
                                    </span>
                                    <a href="javascript:void()" onclick="employeeProfile(${row["UserId"]});"  class="link ml-2">${row["FullName"]}</a>
                                `;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Role+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["LookUpEmployeeRoles"] == "" || row["LookUpEmployeeRoles"] == null ? '-' : row["LookUpEmployeeRoles"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.LeaveType+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["LeaveType"] == "" || row["LeaveType"] == null ? '-' : row["LeaveType"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": ""+Langaugestore.From+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["FromDate"] == "" || row["FromDate"] == null ? '-' : row["FromDate"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": ""+Langaugestore.To+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["ToDate"] == "" || row["ToDate"] == null ? '-' : row["ToDate"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": ""+Langaugestore.NoofDays+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["NoOfDays"] == 1 ? row["NoOfDays"] +' Day' : row["NoOfDays"] +' Days'}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": ""+Langaugestore.Reason+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["Reason"] == "" || row["Reason"] == null ? '-' : row["Reason"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": ""+Langaugestore.Status+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["LookUpStatusId"] == 3 ? `<div class="badge bg-soft-success text-success border p-2">${Langaugestore.Approved}</div>` : ''}`;
                                htmlData += `${row["LookUpStatusId"] == 4 ? `<div class="badge bg-soft-danger text-danger border p-2">${Langaugestore.Rejected}</div>` : ''}`;
                                htmlData += `${row["LookUpStatusId"] == 5 ? `<div class="badge bg-soft-info text-info border p-2">${Langaugestore.Approval_Pending}</div>` : ''}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": ""+Langaugestore.Actions+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<td>
                                    <div class="d-flex leave-table-btn">
                                        ${PermissionData.find(record => record.ModuleKey === "EditLeave").Value == true ?
                                        `<button class="btn btn-light btn-sm font-weight-semibold fs-12 mr-2 border"  id="editbtn" type="button" onclick="getempleavedetails(${row["Id"]});">
                                            <i class="bb-edit-3 text-gray-600 fs-14 mr-2"></i>${Langaugestore.EDIT}
                                        </button>` : ""}

                                        ${PermissionData.find(record => record.ModuleKey === "DeleteLeave").Value == true ?
                                        `<button class="btn btn-light-danger  text-danger btn-sm font-weight-semibold fs-12 border" type="button" onclick="DeleteEmployeeLeaveswal(${row["Id"]});">
                                            <i class="bb-trash-2 text-danger fs-14"></i>
                                        </button>` : ""}
                                
                                        ${PermissionData.find(record => record.ModuleKey === "LeaveChangeStatus").Value == true ?
                                        `<div class="dropdown">
                                            <button class="btn btn-icon" type="button" data-toggle="dropdown">
                                                <i class="bb-more-horizontal fs-22"></i>
                                            </button>
                                            <div class="dropdown-menu dropdown-menu-right">
                                                <a class="dropdown-item" href="#" onclick="getempleavedetails(${row["Id"]}, true)" data-toggle="modal" data-target="#changeAppointmentStatusModal"><i class="bb-repeat text-gray-600 fs-16 mr-3"></i>${Langaugestore.Change_Status}</a>
                                            </div>
                                         </div>` : ""}
                                      </div>
                                </td>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                    ],
                    buttons: [
                        {
                            className: `btn btn-primary float-left mb-3 ${PermissionData.find(record => record.ModuleKey === "AddLeave").Value == true ? "d-block" : "d-none"}`,
                            text: '<i class="bb-plus fs-16 mr-1"></i> '+Langaugestore.Add_Leave+'',
                            action: function (e, dt, node, config) {
                                window.open = OpenAddLeavePopup();
                            }
                        },
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
                $('#ResetServiceDatabtn').show();
                $('#ResetServiceDatabtnloading').hide();
                $('#Searchbtn').show();
                $('#Searchbtnloading').hide();
                $('#employeeLeaveManagementloader').hide();

            }, error: function (error) {
                $('#ResetServiceDatabtn').show();
                $('#ResetServiceDatabtnloading').hide();
                $('#Searchbtn').show();
                $('#Searchbtnloading').hide();
                $('#employeeLeaveManagementloader').hide();
            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#employeeLeaveManagementListing")) {
                $('#employeeLeaveManagementListing').dataTable().fnDestroy();
                $('#employeeLeaveManagementListingdiv').html('<table id="employeeLeaveManagementListing" class="table table-card" style="width:100%; display:inherit;"></table >');
            }
            initEmployeeLeaveManageMentList();
        }
    };
}();


function chageLeaveStatus() {
    $('#employeeLeaveStatusBtn').show();
    $('#employeeLeaveStatusloading').hide();
    var Id = $('#LeaveId').val();
    var LookUpStatusId = $('#updateStatus').val();
    var LookUpStatusChangedBy = atob(OwnerId);
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/userLeave/UserLeave_ChangeStatus?Id=' + Id + '&LookUpStatusId=' + LookUpStatusId + '&LookUpStatusChangedBy=' + LookUpStatusChangedBy + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            $('#changeLeaveStatusModal').modal('hide');
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 2000
                })
                EmployeeLeaveList.init();
            }

            $('#employeeLeaveStatusBtn').show();
            $('#employeeLeaveStatusBtnloading').hide();

        }, error: function (error) {
            
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 2000
            })

            $('#employeeLeaveStatusBtn').show();
            $('#employeeLeaveStatusBtnloading').hide();
        }
    });
    return false;
}

//function employeeLeaveStatus(Id) {
//    $('#LeaveId').val(Id);
//    $('.selectpicker').selectpicker("refresh");
//    $('#changeLeaveStatusModal').modal('show');
//}

var ChangeStatusFlag = false;
function getempleavedetails(Id, changeStatusflag) {
    ChangeStatusFlag = changeStatusflag;
    
    if (ChangeStatusFlag == true) {
        $('#employeeLeaveStatusBtn').attr('disabled', true);
        $('#changeLeaveStatusModal').modal('show');
        $('#LeaveId').val(Id);
        $('#updateStatusgrp').hide();
        $('#updateStatusgrploader').show();
    } else {
        $('#employeeLeaveEditBtn').attr('disabled', true);
        $('#leaveEditModal').modal('show');
        $('#updateLeavegrp').hide();
        $('#updateLeavegrploader').show();
    }

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/userLeave/UserLeave_ById?Id=' + Id + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            $('#Id').val(result.Item.Id);
            $('#employeeDatalistEdit').val(result.Item.UserId);
            $('#LookUpLeaveTypeEdit').selectpicker('val', result.Item.LookUpLeaveTypeId);
            $('#FromDateEdit').val(result.Item.FromDate);
            $('#ToDateEdit').val(result.Item.ToDate);
            ToDateedit = result.Item.ToDate;
            $('#NoOfDaysedit').val(result.Item.NoOfDays);
            $('#ReasonEdit').val(result.Item.Reason);
            $('.selectpicker').selectpicker("refresh");
            fromDateedit = result.Item.FromDate;
            $('#FromDateEdit').keyup();
            toDateedit();

            if (ChangeStatusFlag == true) {
                $('#updateStatus').selectpicker('val', result.Item.LookUpStatusId);
                $('#updateStatusgrp').show();
                $('#updateStatusgrploader').hide();
                $('#employeeLeaveStatusBtn').attr('disabled', false);
            } else {
                $('#updateLeavegrp').show();
                $('#updateLeavegrploader').hide();
                $('#employeeLeaveEditBtn').attr('disabled', false);
            }

        }, error: function (error) {
            if (ChangeStatusFlag == true) {
                $('#updateStatusgrp').show();
                $('#updateStatusgrploader').hide();
                $('#employeeLeaveStatusBtn').attr('disabled', false);
            } else {
                $('#updateLeavegrp').show();
                $('#updateLeavegrploader').hide();
                $('#employeeLeaveEditBtn').attr('disabled', false);
            }
        }
    });
    return false;
}

// DeleteEmployeeLeave API call in ajax method
function DeleteEmployeeLeaveswal(EmployeeLeaveId) {
    Swal.fire({
        title: '' + Langaugestore.Are_you_sure_delete_this_employee_leave_details__+'',
        //text: "Are you sure Active this employee !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteEmployeeLeave(EmployeeLeaveId);
        }
    })

}

function DeleteEmployeeLeave(EmployeeLeaveId) {
    
    var DeletedBy = atob(OwnerId);
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/userLeave/UserLeave_Delete?Id=' + EmployeeLeaveId + '&DeletedBy=' + DeletedBy + '',
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
            }
            EmployeeLeaveList.init();
        }, error: function (error) {
            //error function 
        }
    });
}

function cancel() {
    
    $('#employeeDatalist').val(0);
    $('#LookUpLeaveType').val(0);
    $('#FromDate').val('');
    $('#ToDate').val('');
    $('#NoOfDays').val(0);
    $('#Reason').val('');
    $('.selectpicker').selectpicker("refresh");
}

//employeeTypedrp dropdown API call in ajax methos

function employeeRolesdrp() {

    var EmployeeRolesdrp = new Object();
    EmployeeRolesdrp.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/lookUpEmployeeRoles/LookUpEmployeeRoles_All?search',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(EmployeeRolesdrp),
        crossDomain: true,
        success: function (result) {
            result.Values.reverse();

            $("#employeeRole").html(``);

            for (i = 0; i < result.Values.length; i++) {
                $('#employeeRole').append(`
                 <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                `);

                $('.selectpicker').selectpicker("refresh");
            }

        }, error: function (error) {
            // Error function
        }
    });
    return false;
}

function statuscancel() {
    $('#updateStatus').val(0);
}