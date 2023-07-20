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

var getEmailMarketingid = new URLSearchParams(window.location.search);
getEmailMarketingid = parseInt(atob(getEmailMarketingid.get('Id')));
var getEmailMarketingidatob = ~~getEmailMarketingid;
 

if (getEmailMarketingidatob === 0) {
    $('#marketingform').show();
}

function getEmailMarketingdata() {
    debugger;
    if (getEmailMarketingidatob > 0) {
        $('#emailmarketingformloader').show();
        $('#marketingform').hide();
        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/emailMarketing/EmailMarketing_ById?Id=' + getEmailMarketingidatob + '',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                 
                $('#Id').val(result.Item.Id)
                $('#emailSubject').val(result.Item.EmailSubject);
                $("input[name=genderdetails][value=" + result.Item.Gender + "]").prop('checked', true);
                $(`input[name=genderdetails][value=${result.Item.Gender}]`).trigger('click');
                let CustomerSinceDate = $('#customerSince').val();
                const CustomerSinceArr = CustomerSinceDate.split("-");
                CustomerSinceStart = CustomerSinceArr[0];
                CustomerSinceEnd = CustomerSinceArr[1];
                $('#customerSince').val(result.Item.CustomerSinceStart == "" || result.Item.CustomerSinceStart == null ? '' : result.Item.CustomerSinceStart + '-' + result.Item.CustomerSinceEnd);

                let LastVisitDate = $('#lastVisit').val();
                const LastVisitArr = LastVisitDate.split("-");
                LastVisitStart = LastVisitArr[0];
                LastVisitEnd = LastVisitArr[1];
                $('#lastVisit').val(result.Item.LastVisitStart == "" || result.Item.LastVisitStart == null ? '' : result.Item.LastVisitStart + '-' + result.Item.LastVisitEnd);

                //$('#emailtemplate').summernote('code',(result.Item.EmailTemplate));

                $('#minappointment').val(result.Item.MinAppoinment);
                $('#maxappointment').val(result.Item.MaxAppoinment);
                $('#minageyear').val(result.Item.MinYear);
                $('#maxageyear').val(result.Item.MaxYear);
                var ServicesIdarr = result.Item.ServicesId.split(',')
                $('#lookupserviceslist').selectpicker('val', ServicesIdarr);
                
                //$('#emailtemplate').val(result.Item.EmailTemplate);
                $('#EditEmailTemplate').summernote('code', decodeURIComponent(result.Item.EmailTemplate));
                $('.selectpicker').selectpicker("refresh");

                $('#emailmarketingformloader').hide();
                $('#marketingform').show();
            }, error: function (error) {

                $('#emailmarketingformloader').hide();
                $('#marketingform').show();
            }
        });
        return false;
    } else {}

}