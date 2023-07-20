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

function addEmailMarketingDetails() {
     
    //$('#addMarketingDetailsbtn').hide();
    $('#saveSalonsdetailsbtntext').show();

    var EmaiMarketing= new Object();

    EmaiMarketing.Id = $('#Id').val();
    EmaiMarketing.SalonId = atob(SalonId);
    EmaiMarketing.EmailSubject = $('#emailSubject').val();
    EmaiMarketing.Gender = $('input[name="genderdetails"]:checked').val() == undefined ? 0 : $('input[name="genderdetails"]:checked').val();

    let CustomerSinceDate = $('#customerSince').val();
    const CustomerSinceArr = CustomerSinceDate.split("-");
    EmaiMarketing.CustomerSinceStart = CustomerSinceArr[0] == undefined ? "" : CustomerSinceArr[0];
    EmaiMarketing.CustomerSinceEnd = CustomerSinceArr[1] == undefined ? "" : CustomerSinceArr[1];

    let LastVisitDate = $('#lastVisit').val();
    const LastVisitArr = LastVisitDate.split("-");
    EmaiMarketing.LastVisitStart = LastVisitArr[0] == undefined ? "" : LastVisitArr[0];
    EmaiMarketing.LastVisitEnd = LastVisitArr[1] == undefined ? "" : LastVisitArr[1];

    EmaiMarketing.MinAppoinment = $('#minappointment').val();
    EmaiMarketing.MaxAppoinment = $('#maxappointment').val();
    EmaiMarketing.MinYear = $('#minageyear').val();
    EmaiMarketing.MaxYear = $('#maxageyear').val();
    EmaiMarketing.ServicesId = $('#lookupserviceslist').val();
    //EmaiMarketing.EmailTemplate = $('#emailtemplate').summernote('code');
    EmaiMarketing.UpdatedBy = (atob(UserId));
    EmaiMarketing.UpdatedBy = (atob(UserId));

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/emailMarketing/EmailMarketing_Upsert',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(EmaiMarketing),
        success: function (result) {
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 30000
                })
               
               
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

function EmailTemplateSendUser() {

    var emailTemplateSendUser = new Object();
    emailTemplateSendUser.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/sendEmailMarketingTemplates/EmailUser_All?search=&CustomerSinceStartDate=2021-05-06&CustomerSinceEndDate=2021-05-30&MinAppoinment=0&MaxAppoinment=4&MinAge=23&MaxAge=23&LastVisitStartDate=&LastVisitEndDate=&ServicesIds=`,
        headers: { 'Content-Type': 'application/json'},
        dataType: 'json',
        data: JSON.stringify(emailTemplateSendUser),
        crossDomain: true,
        success: function (result) {
             
            setTimeout(function () {
                window.location = '/Marketing/ManageEmailMarketing';
            }, 3000);
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}

function lookUpServices() {

    var LookUpServices = new Object();
    LookUpServices.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url:   APIEndPoint + `/api/lookUpServices/LookUpServices_All?Search=&ParentId=0&SalonId=${atob(SalonId)}`,
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