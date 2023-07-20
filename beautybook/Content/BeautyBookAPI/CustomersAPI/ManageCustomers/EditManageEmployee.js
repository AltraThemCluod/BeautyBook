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

var getEmaployeeid = new URLSearchParams(window.location.search);
getEmaployeeid = parseInt(atob(getEmaployeeid.get('EmployeeId')));
var getEmaployeeidatob = ~~getEmaployeeid;

function editEmployeemanagerdata() {
    if (getEmaployeeidatob > 0) {
        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/users/Users_ById?Id=' + getEmaployeeidatob + '',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                $('#employeeId').val(result.Item.Id)
                $('#employeeFirstname').val(result.Item.FirstName);
                $('#employeeLastname').val(result.Item.SecondName);
                $("input[name=employeeGender][value=" + result.Item.Gender + "]").prop('checked', true);
                $('#employeeDob').val(result.Item.Dob);
                $('#employeeEmail').val(result.Item.Email);
                $('#employeePrimaryphone').val(result.Item.PrimaryPhone);
                $('#employeeAlternatephone').val(result.Item.AlternatePhone);
                $('#employeeJoiningdate').val(result.Item.JoiningDate);
                $('#employeeRole').val(result.Item.LookUpEmployeeRolesId);
                $('#employeeType').val(result.Item.LookUpEmployeeTypeId);
                $('#employeeCountry').val(result.Item.LookUpCountryId);
                $('#employeeState').val(result.Item.LookUpStateId);
                $('#employeeCity').val(result.Item.City);
                $('#employeeAddressLine1').val(result.Item.AddressLine1);
                $('#employeeAddressLine2').val(result.Item.AddressLine2);
                $('#employeeZipcode').val(result.Item.ZipCode);
                $('.selectpicker').selectpicker("refresh");
                $('#loader').hide();
            }, error: function (error) {
                $('#loader').hide();
            }
        });
        return false;
    } else {}

}