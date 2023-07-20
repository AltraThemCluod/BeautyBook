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

var getSmsMarketingid = new URLSearchParams(window.location.search);
getSmsMarketingid = parseInt(atob(getSmsMarketingid.get('Id')));
var getSmsMarketingidatob = ~~getSmsMarketingid;
 
function getSmsMarketingdata() {
    if (getSmsMarketingidatob > 0) {
        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/smsMarketing/SmsMarketing_ById?Id=' + getSmsMarketingidatob + '',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                $('#Id').val(result.Item.Id)
                $('#SalonId').val(result.Item.SalonId)
                $('#smsSubject').val(result.Item.SmsSubject);
                $("input[name=genderdetails][value=" + result.Item.Gender + "]").prop('checked', true);

                let CustomerSinceDate = $('#customerSince').val();
                const CustomerSinceArr = CustomerSinceDate.split("-");
                CustomerSinceStart = CustomerSinceArr[0];
                CustomerSinceEnd = CustomerSinceArr[1];
                $('#customerSince').val(result.Item.CustomerSinceStart + '-' + result.Item.CustomerSinceEnd);

                let LastVisitDate = $('#lastVisit').val();
                const LastVisitArr = LastVisitDate.split("-");
                LastVisitStart = LastVisitArr[0];
                LastVisitEnd = LastVisitArr[1];
                $('#lastVisit').val(result.Item.LastVisitStart + '-' + result.Item.LastVisitEnd);

                $('#emailtemplate').summernote('code',(result.Item.SmsTemplate));

                $('#minappointment').val(result.Item.MinAppoinment);
                $('#maxappointment').val(result.Item.MaxAppoinment);
                $('#minageyear').val(result.Item.MinYear);
                $('#maxageyear').val(result.Item.MaxYear);
                $('#lookupserviceslist').val(result.Item.ServicesIds);
                $('#smstemplate').val(result.Item.SmsTemplate);
                $('.selectpicker').selectpicker("refresh");
                $('#loader').hide();
            }, error: function (error) {
                $('#loader').hide();
            }
        });
        return false;
    } else {}

}
