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
 
function addSmsMarketingDetails() {
    //('#addMarketingDetailsbtn').hide();
    $('#saveSalonsdetailsbtntext').show();

    var SmsMarketing= new Object();

    SmsMarketing.Id = $('#Id').val();
    SmsMarketing.SalonId = atob(SalonId);
    SmsMarketing.SmsSubject = $('#smsSubject').val();
    SmsMarketing.Gender = $('input[name="genderdetails"]:checked').val();

    let CustomerSinceDate = $('#customerSince').val();
    const CustomerSinceArr = CustomerSinceDate.split("-");
    SmsMarketing.CustomerSinceStart = CustomerSinceArr[0];
    SmsMarketing.CustomerSinceEnd = CustomerSinceArr[1];

    let LastVisitDate = $('#lastVisit').val();
    const LastVisitArr = LastVisitDate.split("-");
    SmsMarketing.LastVisitStart = LastVisitArr[0];
    SmsMarketing.LastVisitEnd = LastVisitArr[1];

    SmsMarketing.MinAppoinment = $('#minappointment').val();
    SmsMarketing.MaxAppoinment = $('#maxappointment').val();
    SmsMarketing.MinYear = $('#minageyear').val();
    SmsMarketing.MaxYear = $('#maxageyear').val();
    SmsMarketing.ServicesId = $('#lookupserviceslist').val();
    //SmsMarketing.SmsTemplate = $('#emailtemplate').summernote('code');
    SmsMarketing.UpdatedBy = (atob(UserId));
    SmsMarketing.UpdatedBy = (atob(UserId));

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/smsMarketing/SmsMarketing_Upsert',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(SmsMarketing),
        success: function (result) {
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 30000
                })
                setTimeout(function () {
                    window.location = '/Marketing/ManageSmsMarketing';
                }, 3000);
            }
        },

        error: function (error) {
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

//customerTypedrp dropdown API call in ajax methos
function lookUpServices() {

    var LookUpServices = new Object();
    LookUpServices.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/lookUpServices/LookUpServices_All?Search=&ParentId=0',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(LookUpServices),
        crossDomain: true,
        success: function (result) {
            result.Values.reverse();

            $("#lookupserviceslist").html(``);

            for (i = 0; i < result.Values.length; i++) {
                $('#lookupserviceslist').append(`
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


