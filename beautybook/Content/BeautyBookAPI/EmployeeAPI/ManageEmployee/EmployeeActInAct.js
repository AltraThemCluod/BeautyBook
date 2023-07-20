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


//swal actinact
function employeeActive(employeeId) {
    Swal.fire({
        title: '' + Langaugestore.Are_you_sure_Active_this_employee__+'',
        //text: "Are you sure Active this employee !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            employeeActInAct(1, employeeId);
        }
    })
    
}

//swal actinact
function employeeInactive(employeeId) {
    Swal.fire({
        title: '' + Langaugestore.Are_you_sure_In_Active_this_employee__+'',
        //text: "Are you sure In-Active this employee !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: ''+Langaugestore.YES+''
    }).then((result) => {
        if (result.isConfirmed) {
            employeeActInAct(2, employeeId);
        }
    })
}

//employeeTypedrp dropdown API call in ajax methos

function employeeActInAct(employeeActInAct, employeeId) {
    
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/users/Users_ActInact?Id=' + employeeId + '&LookUpStatusId=' + employeeActInAct + '&LookUpStatusChangedBy=' + employeeId+'',
        headers: {"Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        async:false,
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
                    EmployeeList.init();
                }, 3000);
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
    return false;
}
