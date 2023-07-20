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
var Languages = getCookie("Languages");


var getEmaployeeid = new URLSearchParams(window.location.search);
getEmaployeeid = parseInt(atob(getEmaployeeid.get('EmployeeId')));
var getEmaployeeidatob = ~~getEmaployeeid;

//EmployeePermissionModule API call in ajax method

var PermissionArr = [];

function EmployeePermissionModule() {
    var EPM = new Object();
    EPM.IsPageProvided = true;
    EPM.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/employeePermission/EmployeeModulePermission_All?search&EmployeeId=${getEmaployeeidatob}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(EPM),
        crossDomain: true,
        success: function (result) {
            console.log(result);

            if (result.TotalRecords > 0)
            {
                for (var i = 0; i < result.TotalRecords; i++) {

                    console.log("result.Values[i]", result.Values[i]);
                    var myObject = {
                        Id: result.Values[i].Id,
                        key: result.Values[i].ModuleKey,
                        Value: result.Values[i].Value
                    };

                    PermissionArr.push(myObject);

                    $("#employeePermission").append(`
                        <ul class="product-category-list p-0 permission-main-area">
                            <li parent-id="${result.Values[i].ParentId}" li-id="${result.Values[i].Id}">
                                <a href="#" class="product-category-link">
                                    <div class="custom-control custom-checkbox">
                                        <input value="${result.Values[i].Id}" onchange="selectPermission(${result.Values[i].Id} , this)" type="checkbox" class="custom-control-input EP_${result.Values[i].ParentId}" name="EmployeePermission" id="EP_${result.Values[i].Id}">

                                        ${Languages == "en" ?
                                            `<label class="custom-control-label fs-16" for="EP_${result.Values[i].Id}">${result.Values[i].En_ModuleName}</label>`
                                            :
                                            `<label class="custom-control-label fs-16" for="EP_${result.Values[i].Id}">${result.Values[i].Ar_ModuleName}</label>`}
                                    </div>
                                </a>
                            </li>
                        </ul>
                    `);

                    $('#EP_' + result.Values[i].Id).prop('checked', result.Values[i].Value);
                }

                TreeViewConvert();
            }

        }, error: function (error) {
            // Error function
        }
    });
}

function TreeViewConvert() {
    jQuery(function ($) {
        var $ul = $('ul');
        $ul.find('li[parent-id]').each(function () {
            $ul.find('li[parent-id=' + $(this).attr('li-id') + ']').wrapAll('<ul />').parent().appendTo(this)
        });
    })
}

function addEmployeePermission()
{
    debugger;
    $('#employeePermission .checkbox').each(function () {
        if ($(this).prop('checked')) {
            var checkboxValue = $(this).val();
            console.log(checkboxValue);
            // You can perform any desired operation with the checkbox value here
        }
    });
}



function selectPermission(eventId, checkbox) {
    debugger;
    if (checkbox.checked == true)
    {
        var objIndex = PermissionArr.findIndex((obj => obj.Id == eventId));
        PermissionArr[objIndex].Value = true;

        //$(".EP_" + eventId).prop("checked", true);

        //$('input[name="EmployeePermission"]:checked').each(function () {
        //    $(".EP_" + this.value).prop("checked", true);
        //});
    }
    else
    {
        for (var i = 0; i < PermissionArr.length; i++)
        {
            if (PermissionArr[i].Id == eventId)
            {
                var objIndex = PermissionArr.findIndex((obj => obj.Id == eventId));
                PermissionArr[objIndex].Value = false;
                //PermissionArr.splice(i);
            }
        }

        //$('input[name="EmployeePermission"]:unchecked').each(function () {
        //    $(".EP_" + this.value).prop("checked", false);
        //});
    }
}



//addEmployeePermission data add API Call In Ajax Method
function addEmployeesPermissionData() {

    $('#addEmployeePermissionbtn').hide();
    $('#addEmployeePermissionbtnloading').show();

    var EmployeesPermissionData = new Object();
    EmployeesPermissionData.EmployeeId = getEmaployeeidatob;
    EmployeesPermissionData.EmployeeModuleJsonData = JSON.stringify(PermissionArr);

    $.ajax({
        type: 'POST',
        url: APIEndPoint + '/api/employeePermission/EmployeePermissionData_Upsert',
        headers: { "Content-Type": "application/json","Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(EmployeesPermissionData),
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
                    window.location = '/Employee/ManageEmployee';
                }, 3000);
            }

            $('#addEmployeePermissionbtn').show();
            $('#addEmployeePermissionbtnloading').hide();

        }, error: function (error) {

            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })

            $('#addEmployeePermissionbtn').show();
            $('#addEmployeePermissionbtnloading').hide();
        }
    });
    return false;
}