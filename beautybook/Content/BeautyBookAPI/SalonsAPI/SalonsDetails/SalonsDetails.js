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

//Add salons API call in ajax method

function saveSalonsdetails() {
    $('#saveSalonsdetailsbtn').hide();
    $('#saveSalonsdetailsbtnloading').show();
    debugger;
    var formData = new FormData();
    var ProfileUrl = document.getElementById("salonLogourl");
    formData.append("LogoUrl", ProfileUrl.files[0]);
    formData.append("Id", $('#salonsId').val());
    formData.append("SalonName", $('#salonName').val());
    formData.append("PrimaryPhone", $('#salonsPrimarynumber').val());
    formData.append("AlternatePhone", $('#salonsAlternatenumber').val());
    formData.append("AddressLine1", $('#addressLine1').val());
    formData.append("Latitude", $('#VisitorLatitude').val());
    formData.append("Longitude", $('#VisitorLongitude').val());
    formData.append("AddressLine2", $('#addressLine2').val());
    formData.append("LookUpCountryId", $('#salonsCountry').val() == "" || $('#salonsCountry').val() == null ? '0' : $('#salonsCountry').val());
    formData.append("LookUpStateId", $('#salonsState').val() == "" || $('#salonsState').val() == null ? '0' : $('#salonsState').val());
    formData.append("City", $('#salonsCity').val());
    formData.append("ZipCode", $('#salonsZipcode').val());
    formData.append("BankName", $('#salonsBankName').val());
    formData.append("IBANNumber", $('#salonsIBANNumber').val());
    formData.append("TaxNumber", $('#salonsTaxNumber').val());
    formData.append("VATNumber", $('#salonsVATNumber').val());

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/salons/Salons_Upsert',
        headers: {"Authorization": '' + DeviceTokenNumber + '' },
        processData: false,
        contentType: false,
        dataType: 'json',
        data: formData,
        crossDomain: true,
        success: function (result) {
            debugger;
            $('#addSalonInfoModal').modal('hide');
            if (result.Code == 200) {
                if (result.Item.Id > 0)
                {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: result.Message,
                        showConfirmButton: false,
                        timer: 5000
                    })
                }
                else
                {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: "The application will be reviewed and approved by the system management",
                        showConfirmButton: false,
                        timer: 5000
                    })
                }
               
                setCookie("SalonId", btoa(result.Item.Id), 30);
                setCookie("SalonName", result.Item.SalonName, 30);
                setCookie("SalonsLogoUrl", result.Item.LogoUrl, 30);
                
                setTimeout(function () {
                    window.location.reload();
                }, 5000);
            }
            if (parseInt($('#salonsId').val()) == 0) {
                mySalonslist();
            } else {}
            $('#saveSalonsdetailsbtn').show();
            $('#saveSalonsdetailsbtnloading').hide();
        }, error: function (error) {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
            $('#saveSalonsdetailsbtn').show();
            $('#saveSalonsdetailsbtnloading').hide();
        }
    });
    return false;
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
             
            console.log(result);
            $("#salonsCountry").html(``);

            for (i = 0; i < result.Values.length; i++) {
                $('#salonsCountry').append(`
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


$('#salonsCountry').change(function () {
    SalonsState();
})

//State dropdown API call in ajax methos

function SalonsState() {
    

    $('#salonsState').attr('disabled', true);


    var SalonsState = new Object();
    SalonsState.IsPageProvided = true;
    var countryId = ~~$('#salonsCountry').val();

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/lookUpState/LookUpState_All?search&CountryId='+countryId+'',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + ''  },
        dataType: 'json',
        data: JSON.stringify(SalonsState),
        crossDomain: true,
        async:false,
        success: function (result) {
            result.Values.reverse();

            $("#salonsState").html(``);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#salonsState').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].Name}</option> 
                    `);
                    $('#salonsState').removeAttr("disabled");
                }
            }

            $('.selectpicker').selectpicker("refresh");


        }, error: function (error) {
            // Error function
            
        }
    });
    return false;
}


