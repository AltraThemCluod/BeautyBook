
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

var getTemplateId = new URLSearchParams(window.location.search);
getTemplateId = parseInt(atob(getTemplateId.get('TemplateId')));
var getTemplateIdatob = ~~getTemplateId;

//EmailMarketingList API call in ajax method

function EditEmailTemplate() {
    $('#ChooseEmailloader').show();
    if (getTemplateIdatob > 0) {
     
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/emailMarketingTemplates/EmailMarketingTemplates_ById?Id=${getTemplateIdatob}`,
        headers: { 'Content-Type': 'application/json' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
             
            $('#templateName').text(result.Item.Name)
            $('#EditEmailTemplate').summernote('code', decodeURIComponent(result.Item.HtmlTemplatesText));
        }, error: function (error) {
             

        }
    });
    return false;
    }
}


//Edit Email template insert api function
//function EditEmailTemplateInsert() {
//     

//    $('#SendEmailNextStep').hide();
//    $('#SendEmailNextStepLoading').show();

//    var editEmailTemplateInsert = new Object();
//    editEmailTemplateInsert.Id = 0;
//    editEmailTemplateInsert.SalonId = atob(SalonId);
//    editEmailTemplateInsert.UserId = 1;
//    editEmailTemplateInsert.EmailSubject = "Subject";
//    editEmailTemplateInsert.EmailTemplateName = $('#templateName').text();
//    editEmailTemplateInsert.UpdateHtmlTemplatesText = encodeURIComponent($('#EditEmailTemplate').summernote('code'));

//    $.ajax({
//        type: 'POST',
//        url: '' + APIEndPoint + '/api/sendEmailMarketingTemplates/SendEmailMarketingTemplates_Upsert',
//        headers: { 'Content-Type': 'application/json' },
//        dataType: 'json',
//        data: JSON.stringify(editEmailTemplateInsert),
//        crossDomain: true,
//        success: function (result) {

//            if (result.Code == 200) {
//                Swal.fire({
//                    position: 'center',
//                    icon: 'success',
//                    title: result.Message,
//                    showConfirmButton: false,
//                    timer: 3000
//                })
//            }

//            $('#SendEmailNextStep').show();
//            $('#SendEmailNextStepLoading').hide();

//        }, error: function (error) {

//            Swal.fire({
//                position: 'center',
//                icon: 'error',
//                title: error.responseJSON.Message,
//                showConfirmButton: false,
//                timer: 3000
//            })

//            $('#SendEmailNextStep').show();
//            $('#SendEmailNextStepLoading').hide();
//        }
//    });
//    return false;
//}



function addEmailMarketingDetails(SaveStatusFlag) {
     

    if (SaveStatusFlag == false) {
        $('#addEmailMarketingDetailsFal').hide();
        $('#addEmailMarketingDetailsFalloading').show();
    } else if (SaveStatusFlag == true) {
        $('#addEmailMarketingDetailsTru').hide();
        $('#addEmailMarketingDetailsTruloading').show();
    }

    var EmailMarketing = new Object();

    EmailMarketing.Id = $('#Id').val();
    EmailMarketing.SalonId = atob(SalonId);
    EmailMarketing.EmailSubject = $('#emailSubject').val();
    EmailMarketing.Gender = $('input[name="genderdetails"]:checked').val() == undefined ? 0 : $('input[name="genderdetails"]:checked').val();

    let CustomerSinceDate = $('#customerSince').val();
    const CustomerSinceArr = CustomerSinceDate.split("-");
    EmailMarketing.CustomerSinceStart = CustomerSinceArr[0] == undefined ? "" : CustomerSinceArr[0];
    EmailMarketing.CustomerSinceEnd = CustomerSinceArr[1] == undefined ? "" : CustomerSinceArr[1];

    let LastVisitDate = $('#lastVisit').val();
    const LastVisitArr = LastVisitDate.split("-");
    EmailMarketing.LastVisitStart = LastVisitArr[0] == undefined ? "" : LastVisitArr[0];
    EmailMarketing.LastVisitEnd = LastVisitArr[1] == undefined ? "" : LastVisitArr[1];

    EmailMarketing.MinAppoinment = $('#minappointment').val();
    EmailMarketing.MaxAppoinment = $('#maxappointment').val();
    EmailMarketing.MinYear = $('#minageyear').val();
    EmailMarketing.MaxYear = $('#maxageyear').val();
    EmailMarketing.ServicesId = $('#lookupserviceslist').val().toString();
    EmailMarketing.IsSendEmail = false;
    EmailMarketing.SaveStatus = SaveStatusFlag;
    EmailMarketing.EmailTemplate = encodeURIComponent($('#EditEmailTemplate').summernote('code'));
    EmailMarketing.CreatedBy = (atob(UserId));
    EmailMarketing.UpdatedBy = (atob(UserId));

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/emailMarketing/EmailMarketing_Upsert',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(EmailMarketing),
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
                    window.location = '/Marketing/ManageEmailMarketing';
                }, 3000);
            }

            if (SaveStatusFlag == false) {
                $('#addEmailMarketingDetailsFal').show();
                $('#addEmailMarketingDetailsFalloading').hide();
            } else if (SaveStatusFlag == true) {
                $('#addEmailMarketingDetailsTru').show();
                $('#addEmailMarketingDetailsTruloading').hide();
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
            if (SaveStatusFlag == false) {
                $('#addEmailMarketingDetailsFal').show();
                $('#addEmailMarketingDetailsFalloading').hide();
            } else if (SaveStatusFlag == true) {
                $('#addEmailMarketingDetailsTru').show();
                $('#addEmailMarketingDetailsTruloading').hide();
            }
        }
    });
    return false;
}

//function EmailTemplateSendUser() {

//    var emailTemplateSendUser = new Object();
//    emailTemplateSendUser.IsPageProvided = true;

//    $.ajax({
//        type: 'POST',
//        url: APIEndPoint + `/api/sendEmailMarketingTemplates/EmailUser_All?search=&CustomerSinceStartDate=2021-05-06&CustomerSinceEndDate=2021-05-30&MinAppoinment=0&MaxAppoinment=4&MinAge=23&MaxAge=23&LastVisitStartDate=&LastVisitEndDate=&ServicesIds=`,
//        headers: { 'Content-Type': 'application/json' },
//        dataType: 'json',
//        data: JSON.stringify(emailTemplateSendUser),
//        crossDomain: true,
//        success: function (result) {
//             
//            setTimeout(function () {
//                window.location = '/Marketing/ManageEmailMarketing';
//            }, 3000);
//        }, error: function (error) {
//            // Error function
//        }
//    });
//    return false;
//}

function lookUpServices() {

    var LookUpServices = new Object();
    LookUpServices.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpServices/LookUpServices_All?Search=&ParentId=0&SalonId=${atob(SalonId)}`,
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