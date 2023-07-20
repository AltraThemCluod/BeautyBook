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

var geteditServicePackageId = new URLSearchParams(window.location.search);
geteditServicePackageId = parseInt(atob(geteditServicePackageId.get('ServicePackageId')));
var geteditServicePackageIdatob = ~~geteditServicePackageId;
 
function editServicePackage() {
    if (geteditServicePackageIdatob > 0) {
        $('#salonserviceslist').html(``);

        $("#ActiveInActivediv").show();
        $('#editpackagediv').show();
        $('#headreditpackage').show();
        $('#breadcrumbeditpackage').show();
        $('#headeraddnewpackage').hide();
        $('#breadcrumbnewpackage').hide();
        $('#addpackagediv').hide();
        $('#savepackagebutton').hide();


        
        return false;
    }

}

function getservicedetails() {
    if (geteditServicePackageIdatob == 0) {
        return;
    }

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/servicePackage/ServicePackage_ById?Id=' + geteditServicePackageIdatob + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            $('#loader').show();

            $('#Id').val(result.Item.Id)
            $('#packageName').val(result.Item.PackageName)
            $('#PackageCustomPrice').val(result.Item.CustomPrice)
            $('#toggleActive').prop('checked', result.Item.IsActive);

            for (i = 0; i < result.Item.MasterServicePackage.length; i++) {
                $("#" + result.Item.MasterServicePackage[i].LookUpServicesId).prop('checked', true)
            }

            $('#loader').hide();
        }, error: function (error) {
            $('#loader').hide();
        }

    });
}
