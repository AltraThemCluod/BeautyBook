//confirm password check
function userCheckconfirmpwd() {

    $('#userSignUpbtn').hide();
    $('#userSignUpbtnLoading').show();

    if ($("#userPassword").val() === $('#userConfirmpassword').val()) {
        userSignUp();
    } else {
        Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Password and confirmpassword does not match',
            showConfirmButton: false,
            timer: 3000
        })
        $('#userSignUpbtn').show();
        $('#userSignUpbtnLoading').hide();
    }
}


//SignUp API Call In Ajax Method

function userSignUp() {
    
    var SignUpvalue = new Object();
    SignUpvalue.LookUpUserTypeId = 2;
    SignUpvalue.FirstName = $("#userFirstname").val();
    SignUpvalue.SecondName = $("#userLastname").val();
    SignUpvalue.Email = $("#userEmail").val();
    SignUpvalue.PasswordHash = $("#userPassword").val();

    var stringedObj = JSON.stringify(SignUpvalue);

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_SignUp',
        headers: { 'Content-Type': 'application/json' },
        dataType: 'json',
        data: JSON.stringify(SignUpvalue),
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
                    window.location = "/Authentication/SignIn";
                }, 3000);
            }

            $('#userSignUpbtn').show();
            $('#userSignUpbtnLoading').hide();

        }, error: function (error) {

                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: error.responseJSON.Message,
                    showConfirmButton: false,
                    timer: 3000
                })

            $('#userSignUpbtn').show();
            $('#userSignUpbtnLoading').hide();
        }
    });
    return false;
}


function TermsAndConditions() {
    
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/termsAndConditions/TermsAndConditions_ById?Id=1',
        headers: { 'Content-Type': 'application/json' },
        dataType: 'json',
        crossDomain: true,
        async : false,
        success: function (result) {
            $('#TnCBodyContent').html(decodeURIComponent(result.Item.TermsAndConditionsText));
        }, error: function (error) {}
    });
    return false;
}


function PrivacyPolicy()
{
    debugger;
    $.ajax({
        type: 'POST',
        url: ''+ APIEndPoint + '/api/privacyPolicy/PrivacyPolicy_ById?Id=1&Type=1',
        headers: { 'Content-Type': 'application/json' },
        dataType: 'json',
        crossDomain: true,
        async: false,
        success: function (result) {
            debugger;
            $('#PrivacyPolicyBodyContent').html(decodeURIComponent(result.Item.PrivacyPolicyText));
        }, error: function (error) { }
    });
    return false;
}