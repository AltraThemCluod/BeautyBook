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

var getTechnicalSupport = new URLSearchParams(window.location.search);
getTechnicalSupport = parseInt(atob(getTechnicalSupport.get('Id')));
var TechnicalSupportatob = ~~getTechnicalSupport;

var DeviceTokenNumber = getCookie("DeviceToken&Type");
var SalonId = getCookie("SalonId");
var UserId = getCookie("SalonId");

//CreateTechnicalTicket API Call In Ajax Method
function CreateTechnicalTicket() {

    debugger;

    $('#addTicketbtn').hide();
    $('#addTicketbtnloading').show();

    //var CheckVal = $('#TechnicalSupportDiscription').summernote('code');

    var TechnicalSupport = new Object();
    TechnicalSupport.Id = $("#TicketId").val(),
    TechnicalSupport.SalonId = atob(SalonId);
    TechnicalSupport.UserId = atob(UserId);
    TechnicalSupport.Discription = btoa(unescape(encodeURIComponent($('#TechnicalSupportDiscription').summernote('code'))));
    TechnicalSupport.UserTypeId = 2;
    TechnicalSupport.CreatedBy = atob(UserId);
    TechnicalSupport.UpdatedBy = atob(UserId);

    $.ajax({
        type: 'POST',
        url: APIEndPoint + '/api/technicalSupport/TechnicalSupport_Upsert',
        headers: { "Content-Type": "application/json","Authorization": '' + DeviceTokenNumber + ''},
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(TechnicalSupport),
        success: function (result) {
            debugger;
            $('#addTicketbtn').show();
            $('#addTicketbtnloading').hide();

            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    window.location = "/TechnicalSupport/ManageTechnicalSupport";
                }, 2000);
            }
        }, error: function (error) {
            debugger;
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })

            $('#addTicketbtn').show();
            $('#addTicketbtnloading').hide();
        }
    });
}

function getTechnicalTicketdata() {
    if (TechnicalSupportatob > 0) {
        $.ajax({
            type: 'POST',
            url: APIEndPoint + `api/technicalSupport/TechnicalSupport_ById?Id=${TechnicalSupportatob}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                $('#TicketId').val(result.Item.Id);
                $('#TechnicalSupportDiscription').summernote('code', decodeURIComponent(escape(window.atob(result.Item.Discription))));
            }, error: function (error) {

            }
        });
    }
}