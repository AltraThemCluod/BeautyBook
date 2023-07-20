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

//profile upload image name get
function profileUpload() {
    var fake_path = $('#customerPhoto').val();
    $('#uploadImagename').text(fake_path.split("\\").pop());
}

//addCustomer API Call In Ajax Method
function addCustomer() {
    $('#addCustomerbtn').hide();
    $('#addCustomerbtnloading').show();

    var AppoinmentCustomeraddflag = $('#AppoinmentCustomeradd').val();

    var formData = new FormData();
    var ProfileUrl = document.getElementById("customerPhoto");
     formData.append("ProfileUrl", ProfileUrl.files[0]);
     formData.append("Id", $('#Id').val());
     formData.append("SalonId", atob(SalonId));
     formData.append("LookUpUserTypeId",4);
     formData.append("FirstName" ,$("#firstName").val());
     formData.append("SecondName" ,$("#lastName").val());
     formData.append("Gender" ,$('input[name="gender"]:checked').val());
     formData.append("Dob" ,$("#birthDate").val());
     formData.append("Email" ,$("#emailAddress").val());
     formData.append("PrimaryPhone" ,$("#phoneNumber").val());
     formData.append("AlternatePhone" ,$("#alternateNumber").val());
     formData.append("ReferedByEmail" ,$("#referredBy").val());
     formData.append("Tags" ,$("#customerTags").val());
   
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_Upsert',
        headers: { "Authorization": '' + DeviceTokenNumber + ''},
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
                    if (AppoinmentCustomeraddflag != "true") {
                        window.location = "/Customers/ManageCustomers";
                    } else {
                        location.reload();
                    }

                }, 3000);
            }
            $('#addCustomerbtn').show();
            $('#addCustomerbtnloading').hide();

        }, error: function (error) {
            
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
            $('#addCustomerbtn').show();
            $('#addCustomerbtnloading').hide();
        }
    });
    return false;
}

var getUserId = new URLSearchParams(window.location.search);
getUserId = getUserId.get('Id');
var atobId = atob(getUserId);

if (atobId > 0) {
    $('.customernew').text('edit');
} else {
    $('#signinform').show();
}

function getCustomerdata() {
    
    $('#loader').show();
    
    if (atobId > 0) {

        $('#signinformloader').show();
        $('#signinform').hide();

        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/users/Users_ById?Id=' +atobId+'',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                debugger;

                $('#Id').val(result.Item.Id)
                $('#firstName').val(result.Item.FirstName);
                $('#lastName').val(result.Item.SecondName);
                $("input[name=gender][value=" + result.Item.Gender + "]").prop('checked', true);
                $('#birthDate').val(result.Item.Dob);
                $('#emailAddress').val(result.Item.Email);
                $('#phoneNumber').val(result.Item.PrimaryPhone.replace("+966 ", ""));
                $('#alternateNumber').val(result.Item.AlternatePhone.replace("+966 ", ""));
                $('#referredBy').val(result.Item.ReferedByEmail);
                $('#customerTags').val(result.Item.Tags);
                $('#ProfileUrl').val(result.Item.ProfileUrl);
                $('#loader').hide();

                $('#signinformloader').hide();
                $('#signinform').show();

            }, error: function (error) {
                $('#loader').hide();

                $('#signinformloader').hide();
                $('#signinform').show();
            }
        });
        return false;
    } else { }

}