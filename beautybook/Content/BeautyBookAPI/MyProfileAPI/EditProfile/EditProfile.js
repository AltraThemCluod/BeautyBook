//setCookie
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

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
var UserId = getCookie("UserId");
var SalonId = getCookie("SalonId");

//profile upload image name get
function profileUpload() {
    var fake_path = $('#profilePhoto').val();
    $('#uploadImagename').text(fake_path.split("\\").pop());
}

//Breadcrumbs and arrow html code bind
if (atob(SalonId) > 0) {
    $('#calcelButton').attr('href', '/Home/Index')
    $('#profileBreadcrumbs').html('<a href="/Home/Index">Dashboard</a>');
    $('#profileBackarrow').html('<a href="/Home/Index"><i class="bb-arrow-left mr-3"></i></a>')
} else {
    $('#calcelButton').attr('href', '/Salons/SalonsDetails')
    $('#profileBreadcrumbs').html('<a href="/Salons/SalonsDetails">Salons Details</a>');
    $('#profileBackarrow').html('<a href="/Salons/SalonsDetails"><i class="bb-arrow-left mr-3"></i></a>')
}

function editProfileDetails() {
    
    $('#loader').show();
    $('#employeeForm').hide();
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_ById?Id=' + atob(UserId) + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            debugger;
            $('#Id').val(result.Item.Id);
            $('#Firstname').val(result.Item.FirstName);
            $('#Lastname').val(result.Item.SecondName);
            $("input[name=gender][value=" + result.Item.Gender + "]").prop('checked', true);
            $('#birthDate').val(result.Item.Dob);           
            $('#Email').val(result.Item.Email);
            $('#Primaryphone').val(result.Item.PrimaryPhone == null || result.Item.PrimaryPhone == "" ? "" : result.Item.PrimaryPhone.replace("+966 ", ""));
            $('#Alternatephone').val(result.Item.AlternatePhone == null || result.Item.AlternatePhone == "" ? "" : result.Item.AlternatePhone.replace("+966 ", ""));
            $('#AddressLine1').val(result.Item.AddressLine1);
            $('#VisitorLatitude').val(result.Item.Latitude)
            $('#VisitorLongitude').val(result.Item.Longitude)
            $('#AddressLine2').val(result.Item.AddressLine2);
            $('#Country').selectpicker('val', result.Item.LookUpCountryId)
            $('#Country').val(result.Item.LookUpCountryId);
            $('#Country').trigger('change');
            SelectState();
            $('#State').selectpicker('val', result.Item.LookUpCountryId)
            $('#City').val(result.Item.City);
            $('#Zipcode').val(result.Item.ZipCode);
            $('#profilePhoto').prop('src', APIEndPoint + '/' + result.Item.ProfileUrl);
            if (result.Item.IsEmailVerified == true) {
                $('#verifySendcode').html('<i class="fa fa-check ml-2 text-success" aria-hidden="true"></i>');
            } else {
                $('#verifySendcode').html('<a href="javascript:void(0)" data-toggle="modal" data-target="#emailverifyModal" class="text-danger" id="verifySendcode"> Send verification code </a>');
            }
            $('#loader').hide();
            $('#employeeForm').show();

        }, error: function (error) {
            //Error function
        }
    });
    return false;
}
//setCookie
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function editProfile() {
    $('#editProfilebtn').hide();
    $('#editProfilebtnloading').show();
    debugger;
    
    var formData = new FormData();
    var ProfileUrl = document.getElementById("profilePhoto");
    formData.append("ProfileUrl", ProfileUrl.files[0]);
    formData.append("Id", $('#Id').val());
    formData.append("FirstName", $("#Firstname").val());
    formData.append("SecondName", $("#Lastname").val());
    formData.append("Gender", $('input[name="gender"]:checked').val());
    formData.append("Dob", $("#birthDate").val());
    formData.append("Email", $("#Email").val());
    formData.append("PrimaryPhone", $("#Primaryphone").val());
    formData.append("AlternatePhone", $("#Alternatephone").val());
    formData.append("AddressLine1", $("#AddressLine1").val());
    formData.append("Latitude", $('#VisitorLatitude').val());
    formData.append("Longitude", $('#VisitorLongitude').val());
    formData.append("AddressLine2", $("#AddressLine2").val());
    formData.append("LookUpCountryId", $("#Country").val() == "" || $("#Country").val() == null ? '0' : $("#Country").val());
    formData.append("LookUpStateId", $("#State").val() == "" || $("#State").val() == null ? '0' : $("#State").val());
    formData.append("City", $("#City").val());
    formData.append("ZipCode", $("#Zipcode").val());

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_Upsert',
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        processData: false,
        contentType: false,
        dataType: 'json',
        data: formData,
        crossDomain: true,
        success: function (result) {
            
            if (result.Code == 200) {
                setCookie("UserProfileUrl", result.Item.ProfileUrl, 30);
                setCookie("UserName", result.Item.FirstName + ' ' + result.Item.SecondName, 30);
                setCookie("UserEmail", btoa(result.Item.Email), 30);
                if (result.Item.IsEmailVerified == true) {
                    $('#verifySendcode').html('<i class="fa fa-check ml-2 text-success" aria-hidden="true"></i>');
                } else {
                    $('#verifySendcode').html('<a href="javascript:void(0)" data-toggle="modal" data-target="#emailverifyModal" class="text-danger" id="verifySendcode"> Send verification code </a>');
                }

                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    window.location = "/MyProfile/EditProfile";
                }, 3000);
            }
            $('#employeeForm')[0].reset();
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
            $('#editProfilebtn').show();
            $('#editProfilebtnloading').hide();
        }
    });
    return false;
}

//county dropdown API call in ajax methos

function SelectCountry() {
    
    var SelectCountry = new Object();
    SelectCountry.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/lookUpCountry/LookUpCountry_All?search',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(SelectCountry),
        crossDomain: true,
        success: function (result) {
            
            result.Values.reverse();

            $("#Country").html(``);

            for (i = 0; i < result.Values.length; i++) {
                $('#Country').append(`
                     <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                `);
            }
            $('.selectpicker').selectpicker("refresh");
            editProfileDetails();
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}


$('#Country').change(function () {
    SelectState();
})

//State dropdown API call in ajax methos
function SelectState() {

    $('#State').attr('disabled', true);

    var SelectState = new Object();
    SelectState.IsPageProvided = true;
    var countryId = ~~$('#Country').val();

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/lookUpState/LookUpState_All?search&CountryId=' + countryId + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(SelectState),
        crossDomain: true,
        async:false,
        success: function (result) {
            result.Values.reverse();

            $("#State").html(``);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#State').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                    `);
                    $('#State').removeAttr("disabled");
                }
            }

            $('.selectpicker').selectpicker("refresh");
        }, error: function (error) {
            // Error function

        }
    });
    return false;
}

var UserEmail = getCookie("UserEmail");

//getemail in token and store input field
$('#signInEmail').val(atob(UserEmail));

//resendverifyCode verifyCode
var ResendFlag = false;
function resendverifyCode(resendFlag) {
    ResendFlag = resendFlag;
    $('#resendCode').hide();
    $('#resendCodeloading').show();
    sendverifyCodeemail();
}


//sendverifyCode verifyCode
function sendverifyCode() {
    $('#EmailVerifycode').val('');
    $('#sendVerifycode').hide();
    $('#sendVerifycodeloading').show();
    sendverifyCodeemail();
}

function sendverifyCodeemail() {
    
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_VerifyCode',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            if (result.Code == 200) {
                
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: '' + Langaugestore.Verification_code_sent_successfully__+'',
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    $('#emailVerifycode').show();
                    $('#sendVerifycode').hide();
                    $('#Cancelbtn').show();
                    $('#Skipbtn').hide();
                    $('#Verifybtn').show();
                }, 3000);
            }

            if (ResendFlag == true) {
                $('#sendVerifycode').hide();
            } else {
                $('#sendVerifycode').show();
            }

            $('#sendVerifycodeloading').hide();

            $('#resendCode').show();
            $('#resendCodeloading').hide();

        }, error: function (error) {
            
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })

            $('#sendVerifycode').hide();
            $('#sendVerifycodeloading').show();

            $('#resendCode').show();
            $('#resendCodeloading').hide();

        }

    });
    return false;
}

function Skip() {
    setCookie("IsUserSkip", 'Yes', 30);
    //$('#emailverifyModal').hide();
    window.location = "/MyProfile/EditProfile";
}

function verifyCode() {

    $('#Verifybtn').hide();
    $('#Verifybtnloading').show();

    
    var EmailVerificationCode = $("#EmailVerifycode").val();
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_VerifyEmail?EmailVerificationCode=' + EmailVerificationCode,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            if (result.Code == 200) {
                
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'User email verified successfully !',
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    window.location = "/MyProfile/EditProfile";
                }, 3000);
            }

            $('#Verifybtn').show();
            $('#Verifybtnloading').hide();

        }, error: function (error) {
            
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })

            $('#Verifybtn').show();
            $('#Verifybtnloading').hide();

        }

    });
    return false;
}