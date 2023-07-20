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
//searchCustomerdata function call

function searchCustomerdata() {
    $('#customerSearch').hide();
    $('#customerSearchloading').show();
    CustomerList.init();
}

//resetCustomerdata function call
function resetCustomerdata() {

    $('#customerReset').hide();
    $('#customerResetloading').show();

    $('#customerName').val('');
    $('#customerPhone').val('');
    $('#customerGender').selectpicker('val', '');
    CustomerList.init();
}

//customerList API call in ajax method
var CustomerList = function () {

    $('#loader').show();
    
    let initCustomerList = function () {

        var Name = $('#customerName').val();
        var PrimaryPhone = $('#customerPhone').val();

        var Gender = $('#customerGender').val();

        var customerList = new Object();
        customerList.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/users/Users_All?search&LookUpStatusId=0&LookUpUserTypeId=4&Name=${Name}&LookUpEmployeeTypeId=0&LookUpEmployeeRolesId=0&PrimaryPhone=${PrimaryPhone}&Gender=${Gender}&SalonId=${atob(SalonId)}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(customerList),
            crossDomain: true,
            success: function (Values) {
                console.log(Values);
                
                $('#customerList').DataTable({
                    "order": [[1, "desc"]],
                    data: Values.Values,
                    columns: [
                        {
                            "title": ""+Langaugestore.CustomerName+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `
                                    <span class="avatar avatar-primary avatar-circle">
                                        <img onerror="this.src='/Content/assets/images/default-avatar.png'" src="${APIEndPoint}/${row["ProfileUrl"]}" class="custome-profile-avatar" alt="User Profile"/>
                                    </span>
                                    <a href="javascript:void()" onclick="customerProfile(${row["Id"]});"  class="link ml-2">${row["UserName"]}</a>
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
                            "title": ""+Langaugestore.Email+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["Email"] == "" || row["Email"] == null ? '-' : `<a href="mailto:${row["Email"]}">${row["Email"]}</a>`}`;
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
                            "title": ""+Langaugestore.Appointments+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["AppointmentsCount"] == "" || row["AppointmentsCount"] == null ? '-' : row["AppointmentsCount"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },
                        {
                            "title": ""+Langaugestore.SALES+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["TotalSales"] == "" || row["TotalSales"] == null ? 'SAR 0' : 'SAR ' + row["TotalSales"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "2%"
                        },
                        {
                            "title": ""+Langaugestore.LastVisit+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["UserAppoinmentLastVisitStr"] == "" || row["UserAppoinmentLastVisitStr"] == null ? '-' : row["UserAppoinmentLastVisitStr"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "5%"
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
                                                    ${PermissionData.find(record => record.ModuleKey === "EditCustomer").Value == true ?
                                                        `<a class="dropdown-item" href="/Customers/CustomerDetails?Id=${btoa(row["Id"])}"><i class="bb-edit-3 text-gray-600 fs-16 mr-3"></i>${Langaugestore.EDIT}</a><hr>` : ""}

                                                    ${PermissionData.find(record => record.ModuleKey === "DeleteCustomer").Value == true ?
                                                        `<a class="dropdown-item text-danger" href="javascript:void(0)" onClick="DeleteCustomerManagerswal(${row["Id"]})"><i class="bb-trash-2 text-danger fs-16 mr-3"></i>${Langaugestore.Delete}</a>` : ""}
                                                </div>
                                            </div>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },
                    ],
                
                    buttons: [
                        {
                            className: `btn btn-primary float-left mb-3 ${PermissionData.find(record => record.ModuleKey === "AddNewCustomer").Value == true ? "d-block" : "d-none"}`,
                            text: "" + Langaugestore.Add_New_Customer + "",
                            action: function (e, dt, node, config) {
                                window.location = "/Customers/CustomerDetails";
                            },
                        },
                        {
                            className: `btn btn-link float-left mb-3 ml-2 import-customer-data ${PermissionData.find(record => record.ModuleKey === "ImportCustomerData").Value == true ? "d-block" : "d-none"}`,
                            text: "Import Customer Data",
                            action: function (e, dt, node, config) {
                                window.location = "/Customers/UploadExcel";
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

                $('#customerReset').show();
                $('#customerResetloading').hide();
                $('#customerSearch').show();
                $('#customerSearchloading').hide();
                $('#loader').hide();
            }, error: function (error) {
                $('#customerReset').show();
                $('#customerResetloading').hide();
                $('#customerSearch').show();
                $('#customerSearchloading').hide();
                $('#loader').hide();
            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#customerList")) {
                $('#customerList').dataTable().fnDestroy();
                $('#customerListdiv').html('<table id="customerList" class="table table-card" style="width:100%; display:inherit;"></table >');
            }
            initCustomerList();
        }
    };
}();


//customerTypedrp dropdown API call in ajax methos

function lookUpstatus() {

    var LookUpstatus = new Object();
    LookUpstatus.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/lookUpStatus/LookUpStatus_All?search',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(LookUpstatus),
        crossDomain: true,
        success: function (result) {
            result.Values.reverse();

            $("#customerStatus").html(``);

            for (i = 0; i < result.Values.length; i++) {
                $('#customerStatus').append(`
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

// DeleteCustomerManager API call in ajax method
function DeleteCustomerManagerswal(customerManagerId) {
    Swal.fire({
        title: '' + Langaugestore.Are_you_sure_Delete_this_customer_details__+'',
        //text: "Are you sure Active this employee !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteCustomerManager(customerManagerId);
        }
    })
}

function DeleteCustomerManager(customerManagerId) {
    
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_Delete?Id=' + customerManagerId + '&DeletedBy=' + customerManagerId + '',
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        processData: false,
        contentType: false,
        crossDomain: true,
        success: function (result) {
            
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: result.Message,
                showConfirmButton: false,
                timer: 3000
            })
            CustomerList.init();
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
