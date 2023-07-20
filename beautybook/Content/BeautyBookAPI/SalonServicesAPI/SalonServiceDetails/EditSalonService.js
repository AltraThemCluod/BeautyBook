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

function EmployeeName() {

    let EmployeeName = new Object();
    EmployeeName.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/Users/Users_All?search&LookUpStatusId=1&LookUpUserTypeId=3&EmployeeName=""&UserTypeId=0&UserRoleId=0&SalonId=' + atob(SalonId),
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(EmployeeName),
        crossDomain: true,
        success: function (result) {
            
            result.Values.reverse();

            $("#EmployeeName").html(``);
            if (result.Values.length > 0) {
                $('#EmployeeName').append(`
                 <option value="" selected>${Langaugestore.Select_Employee}</option>
                `);
                for (i = 0; i < result.Values.length; i++) {
                    $('#EmployeeName').append(`
                 <option value="${result.Values[i].Id}">${result.Values[i].UserName}</option>
                `);

                }
                $('.selectpicker').selectpicker("refresh");
            }
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}

var DeviceTokenNumber = getCookie("DeviceToken&Type");
var SalonId = getCookie("SalonId");
var UserId = getCookie("UserId");


var getSalonServicesId = new URLSearchParams(window.location.search);
getSalonServicesId = parseInt(atob(getSalonServicesId.get('SalonServices')));
var getSalonServicesIdatob = ~~getSalonServicesId;

if (getSalonServicesIdatob == 0) {
    $('#addServiceform').show();
}

//editSalonServices API call in ajax method
function editSalonServices() {
    
    if (getSalonServicesIdatob > 0) {
        $('#addServiceformloader').show();
        $('#addServiceform').hide();
        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/salonServices/SalonServices_ById?Id=' + getSalonServicesIdatob + '',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                
                if (result.Item != null) {
                    $("#salonsServiceId").val(result.Item.Id);
                    $('#DescriptionComment').val(result.Item.Description);
                    $('#categoryName').selectpicker('val', result.Item.LookUpCategoryId);
                    $('#categoryName').val(result.Item.LookUpCategoryId);
                    $('#categoryName').trigger('change');
                    ServiceName();
                    $('#serviceName').selectpicker('val', result.Item.LookUpServicesId);
                    EmployeeName();
                    DisplayEmployeeData();

                    SetServicePriceDuration();
                }

                $('#addServiceformloader').hide();
                $('#addServiceform').show();

            }, error: function (error) {
                $('#addServiceformloader').hide();
                $('#addServiceform').show();
            }
        });
        return false;
    } else { }

}
function AddEmpoyee() {

    $('#addBtn').hide();
    $('#addBtnloader').show();

    let EmpId = 0;
    let SalonServiceId = 0;
    let Duration = "";
    let Price = 0;
    let IsValid = false;
    let msg = "";
    try {
        

        if (!(parseInt($("#EmployeeName").val()) > 0)) {
            
            IsValid = false;
            msg = "Please select employee";
            $('#addBtn').show();
            $('#addBtnloader').hide();
        }
        else if (!(parseInt($("#Price").val()) > 0 && $.isNumeric($("#Price").val()))) {
            IsValid = false;
            msg = "Only digits are allowed for price";
            $('#addBtn').show();
            $('#addBtnloader').hide();
        }
        else if (!(parseInt($("#Duration").val()) > 0 && $("#Duration").val().length <= 3 && $.isNumeric($("#Duration").val()))) {
            IsValid = false;
            msg = "Maximum 3 digits allowed for duration";
            $('#addBtn').show();
            $('#addBtnloader').hide();
        }
        else if (!(parseInt($("#salonsServiceId").val()) > 0)) {
            IsValid = false;
            msg = "Please try again";
            $('#addBtn').show();
            $('#addBtnloader').hide();
        }
        else {
            IsValid = true;
            EmpId = parseInt($("#EmployeeName").val());
            Price = parseInt($("#Price").val());
            Duration = parseInt($("#Duration").val());
            SalonServiceId = parseInt($("#salonsServiceId").val());
        }
    } catch
    {
        IsValid = false;
    }

    if (IsValid == true) {
        var formData = new FormData();
        formData.append("SalonServiceId", SalonServiceId);
        formData.append("Duration", Duration);
        formData.append("Price", Price);
        formData.append("UserId", EmpId);
        formData.append("CreatedBy", atob(UserId));
        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/UserServices/UserServices_Upsert',
            headers: { "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            processData: false,
            contentType: false,
            data: formData,
            crossDomain: true,
            success: function (result) {
                
                if (result.Item != null && result.Code == 200) {
                    DisplayEmployeeData();
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: result.Message,
                        showConfirmButton: false,
                        timer: 3000
                    });
                } else {
                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: result.Message,
                        showConfirmButton: false,
                        timer: 3000
                    });
                }
                $("#Price").val("");
                $("#Duration").val("");
                $("#EmployeeName").val("");
                $('.selectpicker').selectpicker("refresh");

                $('#addBtn').show();
                $('#addBtnloader').hide();

            }, error: function (error) {

                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: error.responseJSON.Message,
                    showConfirmButton: false,
                    timer: 3000
                });
                $("#Price").val("");
                $("#Duration").val("");
                $("#EmployeeName").val("");
                $('.selectpicker').selectpicker("refresh");

                $('#addBtn').show();
                $('#addBtnloader').hide();

            }
        });
    } else {
        Swal.fire({
            position: 'center',
            icon: 'error',
            title: msg,
            showConfirmButton: false,
            timer: 3000
        });
    }
}
function DeleteEmployeeManager(employeeManagerId) {

    Swal.fire({
        title: '' + Langaugestore.Are_you_sure_you_want_to_delete_this_employee_details_+'',//'Are you sure you want to delete this employee ?',
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
        url: '' + APIEndPoint + '/api/UserServices/UserServices_Delete?Id=' + employeeManagerId + '&DeletedBy=' + atob(UserId) + '',
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        success: function (result) {
            if (result.Code == 200) {
                DisplayEmployeeData();
                if (result.Code == 200) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: result.Message,
                        showConfirmButton: false,
                        timer: 3000
                    })
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
function DisplayEmployeeData() {
    
    $('#loader').show();
    let SalonServiceId = 0;
    if (parseInt($("#salonsServiceId").val()) > 0) {
        SalonServiceId = parseInt($("#salonsServiceId").val());
      
    }
    else {
       
    }
    let salonServicesList = new Object();
    salonServicesList.IsPageProvided = true;
    
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/UserServices/UserServices_SalonServicesById?search&SalonServicesById=' + SalonServiceId,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(salonServicesList),
        success: function (result) {

            if (result.TotalRecords <= 0) {
                $('#EmployeeData').html(`
                    <tr>
                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
                    </tr>
                `)
            } else {
                $("#EmployeeData").html(``);
            }

            EmployeeLength = result.Values.length;
            for (i = 0; i < result.Values.length; i++) {
                $('#EmployeeData').append(`
                    <tr id="emp${i}">
                                    
                        <th scope="row">
                        <input type="hidden"  id="UsId" value="${result.Values[i].Id}" /> 
                            <span class="avatar avatar-primary avatar-circle ">
                               ${result.Values[i].EmployeeProfileUrl == "" || result.Values[i].EmployeeProfileUrl == null || result.Values[i].EmployeeProfileUrl == "null" ?
                                `<span class="avatar-initials">${result.Values[i].EmployeeFirstname.charAt(0)}</span>`
                                    :
                                `<img src="${APIEndPoint}/${result.Values[i].EmployeeProfileUrl}" class="custome-profile-avatar" alt="User Profile"ActiUrln />`
                                }
                            </span>
                            <a href="javascript:void()" onclick="employeeProfile(${result.Values[i].UserId});" class="link ml-2"> ${result.Values[i].EmployeeFirstname} ${result.Values[i].EmployeeSecondname}</a>
                        </th>
                        <td>
                           <div class="input-group input-group-merge">
                                <input id="empPrice" type="number" value="${result.Values[i].Price}" class="form-control" placeholder="60" disabled>
                                <div class="input-group-append">
                                    <span class="input-group-text">SAR</span>
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="input-group input-group-merge">
                                <input id="empDuration" type="number" value="${result.Values[i].Duration}" class="form-control" placeholder="60" min="1" max="000" disabled>
                                <div class="input-group-append">
                                    <span class="input-group-text">mins</span>
                                </div>
                            </div>  
                        </td>
                        
                        <td><div onclick="DeleteEmployeeManager(${result.Values[i].Id});" role="button"><i class="bb-trash-2 text-danger"></i></div></td>
                    </tr>
                `);
            }
            
            $('#loader').hide();
        }, error: function (error) {

            
            $('#loader').hide();
        }
    });
    return false;
}

