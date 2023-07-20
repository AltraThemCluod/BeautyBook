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

if (getEmaployeeidatob == 0) {

    $('#employeeForm').show();
}

function editEmployeemanagerdata() {
    if (getEmaployeeidatob > 0) {

        $("#employeeForm").hide();
        $('#employeeFormloader').show();

        $.ajax({
            type: 'POST',
            url: APIEndPoint + `/api/users/Users_ById?Id=${getEmaployeeidatob}&IsLanguage=${Languages}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                $('#employeedetailsId').val(result.Item.Id)
                $('#employeeFirstname').val(result.Item.FirstName);
                $('#employeeLastname').val(result.Item.SecondName);
                $("input[name=employeeGender][value=" + result.Item.Gender + "]").prop('checked', true);
                $('#employeeDob').val(result.Item.Dob);
                $('#employeeEmail').val(result.Item.Email);
                $('#employeePrimaryphone').val(result.Item.PrimaryPhone?.replace("+966 ", ""));
                $('#employeeAlternatephone').val(result.Item.AlternatePhone?.replace("+966 ", ""));
                $('#employeeJoiningdate').val(result.Item.JoiningDate);
                $('#VisitorLatitude').val(result.Item.Latitude)
                $('#VisitorLongitude').val(result.Item.Longitude)
                $('#employeeRole').selectpicker('val', result.Item.LookUpEmployeeRolesId);
                $('#employeeType').selectpicker('val', result.Item.LookUpEmployeeTypeId);
                $('#employeeCountry').selectpicker('val', result.Item.LookUpCountryId);
                SalonsState();
                $('#employeeState').selectpicker('val', result.Item.LookUpStateId);
                $('#employeeCity').val(result.Item.City);
                $('#employeeAddressLine1').val(result.Item.AddressLine1);
                $('#employeeAddressLine2').val(result.Item.AddressLine2);
                $('#employeeZipcode').val(result.Item.ZipCode);
                $('.selectpicker').selectpicker("refresh");

                for (i = 0; i < result.Item.UserWorkingHours.length; i++) {

                    //var weekAr = DayNameArabic(result.Item.UserWorkingHours[i].Day);

                    //<label class="custom-control-label " for="isAvailableswitch${result.Item.UserWorkingHours[i].Id}">
                    //    ${result.Item.UserWorkingHours[i].Day}
                    //</label>

                    $('#userWorkinghourslist').append(`
                        <tr>
                            <th class="font-weight-normal" scope="row">
                                <div class="custom-control custom-switch">
                                    <input type="checkbox" class="custom-control-input" onchange="workingHours(this, ${result.Item.UserWorkingHours[i].Id}, ${i})" value="${result.Item.UserWorkingHours[i].Id}" id="isAvailableswitch${result.Item.UserWorkingHours[i].Id}" ${result.Item.UserWorkingHours[i].IsAvailable == true ? 'checked' : ''}>
                                   
                                    <label class="custom-control-label " for="isAvailableswitch${result.Item.UserWorkingHours[i].Id}">
                                        ${result.Item.UserWorkingHours[i].Day}
                                    </label>
                                </div>
                            </th>
                            <td>
                                <div class="form-group mb-0">
                                    <input type="time" class="form-control form-control-sm border-0" id="workingStartTime" name="workingStartTime[]"  tabindex="1" value="${result.Item.UserWorkingHours[i].StartTime}" />
                                </div>
                            </td>
                            <td>
                                <div class="form-group mb-0">
                                    <input type="time" class="form-control form-control-sm border-0" id="workingEndTime" name="workingEndTime[]" tabindex="1" value="${result.Item.UserWorkingHours[i].EndTime}" />
                                </div>
                            </td>
                        </tr>
                    `);
                    workingHoursArr.push(result.Item.UserWorkingHours[i].Id);
                    IsAvailableflagArr.push(result.Item.UserWorkingHours[i].IsAvailable == true ? 1 : 0)
                }

                $("#employeeForm").show();
                $('#employeeFormloader').hide();

                $('#loader').hide();
            }, error: function (error) {
                $('#loader').hide();

                $("#employeeForm").show();
                $('#employeeFormloader').hide();
            }
        });
        return false;
    } else {}

}


function DayNameArabic(dayName) {

    var dirType = $("html").attr("dir");

    if (dirType == "rtl") {
        if (dayName == "Sunday") {
            return "الأحد";
        }
        else if (dayName == "Monday") {
            return "الأثنين";
        }
        else if (dayName == "Tuesday") {
            return "الثلاثاء";
        }
        else if (dayName == "Wednesday") {
            return "الأربعاء";
        }
        else if (dayName == "Thursday") {
            return "الخميس";
        }
        else if (dayName == "Friday") {
            return "الجمعة";
        }
        else if (dayName == "Saturday") {
            return "السبت";
        }
    } else {
        if (dayName == "Sunday") {
            return "Sunday";
        }
        else if (dayName == "Monday") {
            return "Monday";
        }
        else if (dayName == "Tuesday") {
            return "Tuesday";
        }
        else if (dayName == "Wednesday") {
            return "Wednesday";
        }
        else if (dayName == "Thursday") {
            return "Thursday";
        }
        else if (dayName == "Friday") {
            return "Friday";
        }
        else if (dayName == "Saturday") {
            return "Saturday";
        }
    }
}