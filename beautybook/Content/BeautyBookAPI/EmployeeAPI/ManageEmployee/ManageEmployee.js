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

//searchEmployeedata function call

function searchEmployeedata() {
    $('#employeeSearch').hide();
    $('#employeeSearchloading').show();
    EmployeeList.init();
}

//resetEmployeedata function call
function Employeedatareset() {
    
    $('#employeeReset').hide();
    $('#employeeResetloading').show();

    $('#employeeStatus').val(null);
    $('#employeeType').val(0);
    $('#employeeRole').val(0);
    $('#employeeName').val('');
    $('.selectpicker').selectpicker("refresh");
    EmployeeList.init();
}



//employeeList API call in ajax method
var EmployeeList = function () {

    $('#loader').show();
     
    let initemployeeDatalist = function () {

        var LookUpStatus = parseInt($('#employeeStatus').val());
        var employeeTypeId = parseInt($('#employeeType').val());
        var employeeRoleId = parseInt($('#employeeRole').val());
        var EmployeeName = $('#employeeName').val();

        var employeeList = new Object();
        employeeList.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/users/Users_All?search&LookUpStatusId=${LookUpStatus}&LookUpUserTypeId=3&Name=${EmployeeName}&LookUpEmployeeTypeId=${employeeTypeId}&LookUpEmployeeRolesId=${employeeRoleId}&SalonId=${atob(SalonId)}&IsLanguage=${Languages}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(employeeList),
            crossDomain: true,
            //async:false,
            success: function (Values) {
                console.log(Values);
                 
                $('#employeeDatalist').DataTable({
                    //"order": [[0, "desc"]],
                    data: Values.Values,

                    columns: [
                        {
                            "title": ""+Langaugestore.EmployeeName+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `
                                    <span class="avatar avatar-primary avatar-circle ">
                                        <img onerror="this.src='/Content/assets/images/default-avatar.png'" src="${APIEndPoint}/${row["ProfileUrl"]}" class="custome-profile-avatar" alt="User Profile"/>
                                    </span>
                                    <a href="javascript:void()" onclick="employeeProfile(${row["Id"]});"  class="link ml-2 mr-2">${row["UserName"]}</a>
                                `;
                                return htmlData;
                            }
                            , "orderable": false, "width": "8%"
                        },
                        {
                            "title": ""+Langaugestore.Gender+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["Gender"] == "" || row["Gender"] == null ? '-' : row["Gender"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },
                        {
                            "title": ""+Langaugestore.Type+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["LookUpEmployeeTypeName"] == "" || row["LookUpEmployeeTypeName"] == null ? '-' : row["LookUpEmployeeTypeName"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Role+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["LookUpEmployeeRolesName"] == "" || row["LookUpEmployeeRolesName"] == null ? '-' : row["LookUpEmployeeRolesName"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.PHONE+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["PrimaryPhone"] == "" || row["PrimaryPhone"] == null ? '-' : `<a href="tel:${row["PrimaryPhone"]}">${row["PrimaryPhone"]}</a>`}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.Email + " & " + Langaugestore.Password +"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `<p class="mb-1 d-flex align-items-center">${row["Email"] == "" || row["Email"] == null ? '-' : `<i class="bb-voicemail mr-2 ml-2"></i><a href="mailto:${row["Email"]}">${row["Email"]}</a>`}</p>`;
                                htmlData += `<p class="mb-0 d-flex align-items-center">${row["PasswordHash"] == "" || row["PasswordHash"] == null ? '-' : `<i class="bb-key mr-2 ml-2"></i>` + row["PasswordHash"]}</p>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Status+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["LookUpStatusId"] == 1 ? `<div class="badge bg-soft-success text-success border p-2" onclick="employeeInactive(${row["Id"]})" style="cursor:pointer;">${Langaugestore.Active}</div>` : ''}`;
                                htmlData += `${row["LookUpStatusId"] == 2 ? `<div class="badge bg-soft-danger text-danger border p-2" onclick="employeeActive(${row["Id"]})" style="cursor:pointer;">${Langaugestore.In_active}</div>` : ''}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },
                        {
                            "title": ""+Langaugestore.Actions+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<div class="dropdown">
                                                <button class="btn btn-icon" type="button" data-toggle="dropdown">
                                                    <i class="bb-more-horizontal fs-22"></i>
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right">
                                                    ${PermissionData.find(record => record.ModuleKey === "EditEmployee").Value == true ? `<a class="dropdown-item" href="/Employee/EmployeeDetails?EmployeeId=${btoa(row["Id"])}"><i class="bb-edit-3 text-gray-600 fs-16 mr-3"></i>${Langaugestore.EDIT}</a>` : ""}
                                                    ${PermissionData.find(record => record.ModuleKey === "EmployeePermission").Value == true ? `<a class="dropdown-item" href="/Employee/EmployeePermission?EmployeeId=${btoa(row["Id"])}"><i class="bb-user-check text-gray-600 fs-16 mr-3"></i>${Langaugestore.EmployeePermission}</a>` : ""}
                                                    ${PermissionData.find(record => record.ModuleKey === "DeleteEmployee").Value == true ? `<hr><a class="dropdown-item text-danger" href="javascript:void(0)" onclick="DeleteEmployeeManager(${row["Id"]})"><i class="bb-slash text-danger fs-16 mr-3"></i>${Langaugestore.Terminate}</a>` : ""}
                                                </div>
                                            </div>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },
                    ],
                    buttons: [
                        {
                            className: `btn btn-primary float-left mb-3 ${PermissionData.find(record => record.ModuleKey === "AddNewEmployee").Value == true ? "d-block" : "d-none"}`,
                            text: '<i class="bb-plus fs-16 mr-1"></i> ' + Langaugestore.Add_New_Employee+'',
                            action: function (e, dt, node, config) {
                                //window.location = '/Employee/EmployeeDetails';
                                CreateBlankEmployee();
                            }
                        },
                        {
                            className: 'btn btn-primary float-left ml-2 mb-3 addnewservices-btn',
                            text: '<i class="bb-plus fs-16 mr-1"></i> ' + Langaugestore.AddNewService+'',
                            action: function (e, dt, node, config) {
                                window.location = '/SalonServices/SalonServiceDetails';
                            }
                        },
                        {
                            extend: 'pdf',
                            className: 'btn btn-light border font-weight-medium float-right mb-3',
                            text: '<i class="bb-printer fs-16 mr-2"></i>'+Langaugestore.Print+'',
                        },
                        {
                            extend: 'excel',
                            className: 'btn btn-light border font-weight-medium float-right mb-3 mr-2',
                            text: '<i class="bb-download fs-16 mr-2"></i>'+Langaugestore.ExporttoExcel+'',
                        },
                    ],
                    responsive: true,
                    "lengthMenu": [
                        [5, 15, 20, 40],
                        [5, 15, 20, 40] // change per page values here
                    ],
                    "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
                });

                $('#employeeReset').show();
                $('#employeeResetloading').hide();
                $('#employeeSearch').show();
                $('#employeeSearchloading').hide();
                $('#loader').hide();

            }, error: function (error) {
                $('#employeeReset').show();
                $('#employeeResetloading').hide();
                $('#employeeSearch').show();
                $('#employeeSearchloading').hide();
                $('#loader').hide();
            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#employeeDatalist")) {
                $('#employeeDatalist').dataTable().fnDestroy();
                $('#employeeDatalistdiv').html('<table id="employeeDatalist" class="table table-card" style="width:100%; display:inherit;"></table>');
            }
            initemployeeDatalist();
        }
    };
}();


//swal Delete employee
function DeleteEmployeeManager(employeeManagerId) {
   
    Swal.fire({
        title: '' + Langaugestore.Are_you_sure_you_want_to_delete_this_employee_details_+'',
        //text: "Are you sure Active this employee !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteEmployeeswal(employeeManagerId);
        }
    })
}

// DeleteEmployeeManager API call in ajax method

function DeleteEmployeeswal(employeeManagerId) {
    
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_Delete?Id=' + employeeManagerId + '&DeletedBy=' + employeeManagerId + '',
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        processData: false,
        contentType: false,
        crossDomain: true,
        success: function (result) {
            if (result.Code == 200) {
                if (result.Code == 200) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: result.Message,
                        showConfirmButton: false,
                        timer: 3000
                    })
                    setTimeout(function () {
                        EmployeeList.init();
                    }, 3000);
                }
            }
        }, error: function (error) {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
        }
    });
}


function employeeIscreate() {
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/users/Employee_IsCreate?SalonId=${atob(SalonId)}`,
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        async:false,
        success: function (result) {
             
            if (result.Item.NoOfEmployee > 0) {
                if (result.Item.IsService <= 0) {
                    $('.addnewservices-btn').show();
                } else {
                    $('.addnewservices-btn').hide();
                }
            } else {
                $('.addnewservices-btn').hide();
            }
        }, error: function (error) {

        }
    });
}
