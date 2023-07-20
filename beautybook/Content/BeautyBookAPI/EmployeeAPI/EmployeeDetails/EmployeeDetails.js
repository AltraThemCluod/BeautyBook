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


var getEmaployeeid = new URLSearchParams(window.location.search);
getEmaployeeid = parseInt(atob(getEmaployeeid.get('EmployeeId')));
var getEmaployeeidatob = ~~getEmaployeeid;

//profile upload image name get
function profileUpload() {
    
    var fake_path = $('#employeePhoto').val();
    $('#uploadImagename').text(fake_path.split("\\").pop());
}

//Query parameter [ EmployeeId > 0 ] workingHourse section show and employeetextStatus change
if (getEmaployeeidatob > 0) {
    $('#workingHourse').show();
    $('.employeeStatus').text('Edit');
    $('#employeeBtntext').text('Save');
    $('#employeeinfoEdit').text('edit')
}

var workingHoursArr = [];
var IsAvailableflagArr = [];

function workingHours(checkbox, workingHoursId, index) {
    
    if (checkbox.checked == true) {
        //workingHoursArr.push(workingHoursId);
        IsAvailableflagArr[index] = 1;
    } else {
        for (i = 0; i < workingHoursArr.length; i++) {
            if (workingHoursArr[i] == workingHoursId) {
                //workingHoursArr.splice(i, 1);
                IsAvailableflagArr[i] = 0;
            }
        }
    }
}


//addEmployee API Call In Ajax Method
function addEmployees(IsCreatenewemployee) {
     
    $('#addEmployeebtn').hide();
    $('#addEmployeebtnloading').show();

    var workingStartTimearr = $("input[id='workingStartTime']").map(function () { return $(this).val(); }).get();
    var workingEndTimearr = $("input[id='workingEndTime']").map(function () { return $(this).val(); }).get();

    var workingStartTimes = workingStartTimearr;
    var workingEndTimes = workingEndTimearr;

    for (var i = 0; i < workingStartTimes.length; i++) {
        if (workingStartTimes[i] == "") {
            workingStartTimes[i] = null;
        }
    }

    for (var i = 0; i < workingEndTimes.length; i++) {
        if (workingEndTimes[i] == "") {
            workingEndTimes[i] = null;
        }
    }

     

    var workingStartTimesStr = workingStartTimes.toString();
    var workingEndTimesStr = workingEndTimes.toString();

    var workingHoursStr = workingHoursArr.toString();
    var IsAvailableflagStr = IsAvailableflagArr.toString();
    
    var formData = new FormData();
    var ProfileUrl = document.getElementById("employeePhoto");
    formData.append("ProfileUrl", ProfileUrl.files[0]);
    formData.append("Id", $('#employeedetailsId').val());
    formData.append("LookUpUserTypeId", 3);
    formData.append("FirstName", $("#employeeFirstname").val());
    formData.append("SalonId", atob(SalonId));
    formData.append("SecondName", $("#employeeLastname").val());
    formData.append("Gender", $('input[name="employeeGender"]:checked').val());
    formData.append("Dob", $("#employeeDob").val());
    formData.append("Email", $("#employeeEmail").val());
    formData.append("PrimaryPhone" , $("#employeePrimaryphone").val());
    formData.append("AlternatePhone" , $("#employeeAlternatephone").val());
    formData.append("JoiningDate" , $("#employeeJoiningdate").val());
    formData.append("LookUpEmployeeRolesId", $("#employeeRole").val() == "" || $("#employeeRole").val() == null ? 0 : $("#employeeRole").val());
    formData.append("LookUpEmployeeTypeId", $('#employeeType').val() == "" || $('#employeeType').val() == null ? 0 : $('#employeeType').val());
    formData.append("LookUpCountryId", $("#employeeCountry").val() == "" || $("#employeeCountry").val() == null ? 0 : $("#employeeCountry").val());
    formData.append("LookUpStateId", $("#employeeState").val() == "" || $("#employeeState").val() == null ? 0 : $("#employeeState").val());
    formData.append("City" , $("#employeeCity").val());
    formData.append("AddressLine1", $("#employeeAddressLine1").val());
    formData.append("Latitude", $('#VisitorLatitude').val());
    formData.append("Longitude", $('#VisitorLongitude').val());
    formData.append("AddressLine2" , $("#employeeAddressLine2").val());
    formData.append("ZipCode", $("#employeeZipcode").val());
    formData.append("WorkingStartTime", workingStartTimesStr);
    formData.append("WorkingEndTime", workingEndTimesStr);
    formData.append("WorkingHoursId", workingHoursStr);
    formData.append("IsAvailable", IsAvailableflagStr);
    
    $.ajax({
        type: 'POST',
        url: APIEndPoint+'/api/users/Users_Upsert',
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        processData: false,
        contentType: false,
        dataType: 'json',
        data: formData,
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
                setTimeout(function () {
                    if (IsCreatenewemployee == true) {
                        window.location = '/Employee/EmployeeDetails';
                    } else if (IsCreatenewemployee == false) {
                        window.location = '/Employee/ManageEmployee';
                    }
                }, 3000);
            }
            $('.selectpicker').selectpicker("refresh");
            $('#addEmployeebtn').show();
            $('#addEmployeebtnloading').hide();

        }, error: function (error) {
            
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })

            $('#addEmployeebtn').show();
            $('#addEmployeebtnloading').hide();
        }
    });
    return false;
}


function CreateBlankEmployee() {
    debugger;
    var formData = new FormData();

    formData.append("Id", 0);
    formData.append("SalonId", atob(SalonId));
    formData.append("LookUpUserTypeId", 3);

    $.ajax({
        type: 'POST',
        url: APIEndPoint + '/api/users/BlankUsers_Create',
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        processData: false,
        contentType: false,
        dataType: 'json',
        data: formData,
        crossDomain: true,
        success: function (result) {
            console.log('resultresultresult', result);
            debugger;
            if (result.Code == 200) {
                window.location = `/Employee/EmployeeDetails?EmployeeId=${btoa(result.Item.Id)}`;
            }
        }, error: function (error) {
            window.location.reload();
        }
    });
}



//county dropdown API call in ajax methos

function SalonsCountry() {

    var SalonsCountry = new Object();
    SalonsCountry.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/lookUpCountry/LookUpCountry_All?search',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(SalonsCountry),
        crossDomain: true,
        success: function (result) {
            result.Values.reverse();

            $("#employeeCountry").html(``);

            for (i = 0; i < result.Values.length; i++) {
                $('#employeeCountry').append(`
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


$('#employeeCountry').change(function () {
    
    SalonsState();
})

//State dropdown API call in ajax methos
function SalonsState() {
    

    $('#employeeState').attr('disabled', true);

    var SalonsState = new Object();
    SalonsState.IsPageProvided = true;
    var countryId = ~~$('#employeeCountry').val();

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/lookUpState/LookUpState_All?search&CountryId=' + countryId + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(SalonsState),
        crossDomain: true,
        async:false,
        success: function (result) {
            result.Values.reverse();

            $("#employeeState").html(``);
            
            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#employeeState').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>                    
                    `);
                    $('#employeeState').removeAttr("disabled");
                }
            }

            $('.selectpicker').selectpicker("refresh");

        }, error: function (error) {
            // Error function
            
        }
    });
    return false;
}


//employeeTypedrp dropdown API call in ajax methos

function employeeTypedrp() {

    var EmployeeType = new Object();
    EmployeeType.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/lookUpEmployeeType/LookUpEmployeeType_All?search',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(EmployeeType),
        crossDomain: true,
        success: function (result) {
            result.Values.reverse();

            $("#employeeType").html(``);

            for (i = 0; i < result.Values.length; i++) {
                $('#employeeType').append(`
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

//employeeTypedrp dropdown API call in ajax methos

function employeeRolesdrp() {

    var EmployeeRolesdrp = new Object();
    EmployeeRolesdrp.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpEmployeeRoles/LookUpEmployeeRoles_All?search&IsLanguage=${Languages}`,
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