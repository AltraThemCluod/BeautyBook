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
var UserId = getCookie("UserId");

//changeStatus modal open function call
var AppoinmentId = 0;
function changeStatus(appoinmentId) {

    $('#Changestatus').attr('disabled', true);

    $('#updateStatus').selectpicker('val', 0);
    AppoinmentId = appoinmentId;
    $('#changeAppointmentStatusModal').modal('show');

    $('#updateStatusgrp').hide();
    $('#updateStatusgrploader').show();

    if (AppoinmentId > 0) {
        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/userAppointments/UserAppointments_ById?Id=' + appoinmentId + '',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                
                $('#updateStatus').selectpicker('val', result.Item.LookUpStatusId);
                $('#updateStatusgrp').show();
                $('#updateStatusgrploader').hide();
                $('#Changestatus').attr('disabled', false);
            }, error: function (error) {
                //Error function
                $('#updateStatusgrp').show();
                $('#updateStatusgrploader').hide();
                $('#Changestatus').attr('disabled', false);
            }
        });
    }

    return false;
}

//appoinmentChangestatus validation function
function appoinmentChangestatus() {
    
    var changeStatusno = $('#updateStatus').val();

    if (changeStatusno === "") {
        Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Please select status',
            showConfirmButton: false,
            timer: 3000
        })
    } else {
        appoinmentChangestatusvalid();
    }
}


//employeeTypedrp dropdown API call in ajax methos

function appoinmentChangestatusvalid() {
    
    $('#Changestatus').hide();
    $('#Changestatusloading').show();

    var updateStatusId = $('#updateStatus').val();

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/userAppointments/UserAppointments_ChangeStatus?Id='+AppoinmentId+'&LookUpStatusId='+updateStatusId+'&LookUpStatusChangedBy=' + atob(UserId)+'',
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            $('#changeAppointmentStatusModal').modal('hide');
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
                appoinmentList.init();
            }

            $('#Changestatus').show();
            $('#Changestatusloading').hide();

        }, error: function (error) {
            
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
            $('#Changestatus').show();
            $('#Changestatusloading').hide();
        }
    });
    return false;
}