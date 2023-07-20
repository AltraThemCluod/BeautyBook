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

function searchEmployeeWorkSheetdata() {
    $('#EmployeeWorkSheetSearch').hide();
    $('#EmployeeWorkSheetSearchloading').show();
    EmployeeWorkSheetList.init();
}

//resetEmployeeWorkSheetdata function call
function resetEmployeeWorkSheetdata() {
    $('#EmployeeWorkSheetReset').hide();
    $('#EmployeeWorkSheetResetloading').show();

    $('#employeeDatalistSearch').val(0);
    $('#employeeRole').val(0);
    $('#AttendanceDateSearch').val('');
    $('#SearchStatus').val(null);
    $('.selectpicker').selectpicker("refresh");
    TodayEmployeeWorkSheetList.init();
}

// employee/User Leave add api call in ajax methos
function employeeWorkSheetAdd() {
     
    $('#employeeWorkSheetEditBtn').hide();
    $('#employeeWorkSheetEditBtnloading').show();
    var UserWorkSheet = new Object();
    UserWorkSheet.Id = $('#Id').val();
    UserWorkSheet.UserId = $('#employeeDatalistEdit').val();
    UserWorkSheet.SalonId = atob(SalonId);
    UserWorkSheet.LookUpStatusId = $('#employeeStatus').val();
    if ($('#employeeStatus').val() == 12) {
        $('#inTime').val('');
        $('#outTime').val('');
        $('#breaktime').val('');
        $('#shortLeave').val('');
        UserWorkSheet.InTime = null;
        UserWorkSheet.OutTime = null;
        UserWorkSheet.Break = null;
        UserWorkSheet.ShortLeave = null;
    }
    else {
        UserWorkSheet.InTime = $('#inTime').val();
        UserWorkSheet.OutTime = $('#outTime').val();
        UserWorkSheet.Break = $('#breaktime').val();
        UserWorkSheet.ShortLeave = $('#shortLeave').val();
    }
    UserWorkSheet.CreatedBy = atob(OwnerId);
    UserWorkSheet.UpdatedBy = atob(OwnerId);

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/userWorkSheet/UserWorkSheet_Upsert',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(UserWorkSheet),
        crossDomain: true,
        success: function (result) {

            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 2000
                })
                setTimeout(function () {
                    window.location.reload();
                }, 2000);
            }

            $('#employeeWorkSheetEditBtn').show();
            $('#employeeWorkSheetEditBtnloading').hide();

        }, error: function (error) {

            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 2000
            })

            $('#employeeWorkSheetEditBtn').show();
            $('#employeeWorkSheetEditBtnloading').hide();
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

//employeeList API call in ajax method
var EmployeeWorkSheetList = function () {

    $('#loader').show();
     
    let initEmployeeWorkSheetList = function () {

        var UserId = parseInt(~~$('#employeeDatalistSearch').val());
        var atobSalonId = atob(SalonId);
        var AttendanceDates = $('#AttendanceDateSearch').val();
        var LookUpEmployeeRolesId = parseInt(~~$('#employeeRole').val());

        var employeeWorkSheetLists = new Object();
        employeeWorkSheetLists.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/userWorkSheet/UserWorkSheet_All?search&UserId=${UserId}&SalonId=${atobSalonId}&AttendanceDate=${AttendanceDates}&LookUpEmployeeRolesId=${LookUpEmployeeRolesId}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(employeeWorkSheetLists),
            crossDomain: true,
            success: function (Values) {
                console.log(Values);
                debugger;
                $('#employeeWorkSheetList').DataTable({
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
                                    <a href="javascript:void()" onclick="employeeProfile(${row["UserId"]});"  class="link ml-2">${row["FullName"] == '' || row["FullName"] == null ? '-' : row["FullName"]}</a>
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
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Status+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["LookUpStatusId"] == 11 ? `<div class="badge bg-soft-success text-success border p-2">${row["LookUpStatus"]}</div>` : ''}`;
                                htmlData += `${row["LookUpStatusId"] == 12 ? `<div class="badge bg-soft-danger text-danger border p-2">${row["LookUpStatus"]}</div>` : ''}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },
                        {
                            "title": "" + Langaugestore.IN_TIME +"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<b>${row["InTime"] == "" || row["InTime"] == null ? '-' : onTimeChange(row["InTime"])}</b>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.OUT_TIME +"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<b>${row["OutTime"] == "" || row["OutTime"] == null ? '-' : onTimeChange(row["OutTime"])}</b>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },

                        {
                            "title": "" + Langaugestore.BREAK +"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["Break"] == "" || row["Break"] == null ? '-' : row["Break"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },

                        {
                            "title": "" + Langaugestore.SHORT_LEAVE +"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["ShortLeave"] == "" || row["ShortLeave"] == null ? '-' : row["ShortLeave"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.PRODUCTIVE +"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["Productive"] == "" || row["Productive"] == null ? '-' : row["Productive"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                            , "orderable": false, "width": "0%"
                        },
                        {
                            "title": "" + Langaugestore.Actions +"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${PermissionData.find(record => record.ModuleKey === "EditWorkingHours").Value == true ?
                                            `<button class="btn btn-light btn-sm font-weight-semibold fs-12 border" type="button" onclick="getempWorkSheetdetails(${row["UserId"]})">
                                                <i class="bb-edit-3 text-gray-600 fs-14 mr-2"></i>Edit
                                            </button>` : ""}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },
                    ],
                    buttons: [
                    ],
                    responsive: true,
                    "lengthMenu": [
                        [5, 15, 20, 40],
                        [5, 15, 20, 40] // change per page values here
                    ],
                    "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
                });

                $('#loader').hide();
                $('#EmployeeWorkSheetReset').show();
                $('#EmployeeWorkSheetResetloading').hide();
                $('#EmployeeWorkSheetSearch').show();
                $('#EmployeeWorkSheetSearchloading').hide();
            }, error: function (error) {
                $('#EmployeeWorkSheetReset').show();
                $('#EmployeeWorkSheetResetloading').hide();
                $('#EmployeeWorkSheetSearch').show();
                $('#EmployeeWorkSheetSearchloading').hide();
                $('#loader').hide();
            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#employeeWorkSheetList")) {
                $('#employeeWorkSheetList').dataTable().fnDestroy();
                $('#employeeWorkSheetListdiv').html('<table id="employeeWorkSheetList" class="table table-card" style="width:100%; display:inherit;"></table >');
            }
            initEmployeeWorkSheetList();
        }
    };
}();

//employeeList API call in ajax method
var TodayEmployeeWorkSheetList = function () {

    $('#loader').show();
     
    let initTodayEmployeeWorkSheetList = function () {

        var UserId = parseInt(~~$('#employeeDatalistSearch').val());
        var atobSalonId = atob(SalonId);
        var AttendanceDates = $('#AttendanceDateSearch').val();
        var LookUpEmployeeRolesId = parseInt(~~$('#employeeRole').val());

        var todayemployeeWorkSheetLists = new Object();
        todayemployeeWorkSheetLists.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/userSalons/UserSalons_gettodayworksheet?search&UserId=${UserId}&SalonId=${atobSalonId}&AttendanceDate=${AttendanceDates}&LookUpEmployeeRolesId=${LookUpEmployeeRolesId}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(todayemployeeWorkSheetLists),
            crossDomain: true,
            success: function (Values) {
                console.log(Values);
                 
                $('#employeeWorkSheetList').DataTable({
                    "order": [[0, "desc"]],
                    data: Values.Values,
                    columns: [
                        {
                            "title": "" + Langaugestore.EmployeeName + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `
                                    <span class="avatar avatar-primary avatar-circle ">
                                        <img onerror="this.src='/Content/assets/images/default-avatar.png'" src="${APIEndPoint}/${row["ProfileImage"]}" class="custome-profile-avatar" alt="User Profile"/>
                                    </span>
                                    <a href="javascript:void()" onclick="employeeProfile(${row["UserId"]});"  class="link ml-2">${row["FullName"] == '' || row["FullName"] == null ? '-' : row["FullName"]}</a>
                                `;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.Role + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["LookUpEmployeeRoles"] == "" || row["LookUpEmployeeRoles"] == null ? '-' : row["LookUpEmployeeRoles"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Status+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                if (row["LookUpStatusId"] == 11)
                                htmlData += `<div class="badge bg-soft-success text-success border p-2">${row["LookUpStatus"]}</div>`;
                                else if (row["LookUpStatusId"] == 12)
                                htmlData += `<div class="badge bg-soft-danger text-danger border p-2">${row["LookUpStatus"]}</div>`;
                                else if (row["LookUpStatusId"] == 0)
                                htmlData += `-`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },
                        {
                            "title": "" + Langaugestore.IN_TIME + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<b>${row["InTime"] == "" || row["InTime"] == null ? '-' : onTimeChange(row["InTime"])}</b>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.OUT_TIME + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<b>${row["OutTime"] == "" || row["OutTime"] == null ? '-' : onTimeChange(row["OutTime"])}</b>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },

                        {
                            "title": "" + Langaugestore.BREAK + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["Break"] == "" || row["Break"] == null ? '-' : row["Break"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },

                        {
                            "title": "" + Langaugestore.SHORT_LEAVE + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["ShortLeave"] == "" || row["ShortLeave"] == null ? '-' : row["ShortLeave"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.PRODUCTIVE + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["Productive"] == "" || row["Productive"] == null ? '-' : row["Productive"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                            , "orderable": false, "width": "0%"
                        },
                        {
                            "title": "" + Langaugestore.Actions + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${PermissionData.find(record => record.ModuleKey === "EditWorkingHours").Value == true ?
                                            `<button class="btn btn-light btn-sm font-weight-semibold fs-12 border" type="button" onclick="getempWorkSheetdetails(${row["Id"]},${row["UserId"]})">
                                                <i class="bb-edit-3 text-gray-600 fs-14 mr-2"></i>${Langaugestore.EDIT}
                                            </button>` : ""}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },
                    ],
                    buttons: [
                    ],
                    responsive: true,
                    "lengthMenu": [
                        [5, 15, 20, 40],
                        [5, 15, 20, 40] // change per page values here
                    ],
                    "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
                });

                $('#loader').hide();
                $('#EmployeeWorkSheetReset').show();
                $('#EmployeeWorkSheetResetloading').hide();
                $('#EmployeeWorkSheetSearch').show();
                $('#EmployeeWorkSheetSearchloading').hide();
            }, error: function (error) {
                $('#EmployeeWorkSheetReset').show();
                $('#EmployeeWorkSheetResetloading').hide();
                $('#EmployeeWorkSheetSearch').show();
                $('#EmployeeWorkSheetSearchloading').hide();
                $('#loader').hide();
            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#employeeWorkSheetList")) {
                $('#employeeWorkSheetList').dataTable().fnDestroy();
                $('#employeeWorkSheetListdiv').html('<table id="employeeWorkSheetList" class="table table-card" style="width:100%; display:inherit;"></table >');
            }
            initTodayEmployeeWorkSheetList();
        }
    };
}();

function getempWorkSheetdetails(Id,UserId) {
    if (Id == 0) {
        $('#employeeDatalistEdit').val(UserId);
        $('#workEditModal').modal('show');
    }
    else if (Id > 0) {
        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/userWorkSheet/UserWorkSheet_ById?Id=' + Id + '',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {

                $('#Id').val(result.Item.Id);
                $('#employeeDatalistEdit').val(result.Item.UserId);
                $('#AttendanceDateEdit').val(result.Item.AttendanceDate);
                $('#employeeStatus').selectpicker('val', result.Item.LookUpStatusId);
                if ($('#employeeStatus').val() == "11") {
                    $('#presentValid').show();
                }
                else {
                    $('#presentValid').hide();
                }
                $('#inTime').val(result.Item.InTime);
                $('#outTime').val(result.Item.OutTime);
                $('#breaktime').val(result.Item.Break);
                $('#shortLeave').val(result.Item.ShortLeave);
                $('.selectpicker').selectpicker("refresh");
                $('#workEditModal').modal('show');

            }, error: function (error) {
                //Error function
            }
        });
    }
    return false;
}

function cancel() {

    $('#employeeDatalist').val(0);
    $('#LookUpLeaveType').val(0);
    $('#AttendanceDate').val('');
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

