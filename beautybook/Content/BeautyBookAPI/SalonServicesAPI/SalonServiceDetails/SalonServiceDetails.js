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
var UserId = getCookie("UserId");

var getSalonServicesId = new URLSearchParams(window.location.search);
getSalonServicesId = parseInt(atob(getSalonServicesId.get('SalonServices')));
var getSalonServicesIdatob = ~~getSalonServicesId;

//CategoryName dropdown API call in ajax methos

function CategoryName() {
    let CategoryName = new Object();
    CategoryName.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpServices/LookUpServices_All?search&ParentId=0&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(CategoryName),
        crossDomain: true,
        async: false,
        success: function (result) {
            $("#categoryName").html(``);
            for (i = 0; i < result.Values.length; i++) {
                $('#categoryName').append(`
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

//View Category
function ViewCategoryName() {
    
    let viewCategoryName = new Object();
    viewCategoryName.IsPageProvided = true;
     
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpServices/LookUpServices_All?search&ParentId=0&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(viewCategoryName),
        crossDomain: true,
        async: false,
        success: function (result) {
             
            $("#SalonCategory").html(``);
            debugger;
            if (result.Values.length <= 0) {
                $('#SalonCategory').append(`
                    <div class="form-group mb-0 row justify-content-center align-items-center">
                        <label class="col-form-label text-muted" style="font-size:20px;">${Langaugestore.NoRecordsFound}</label>
                    </div>
                `);
            } 


            for (i = 0; i < result.Values.length; i++) {
                $('#SalonCategory').append(`
                    <div class="form-group mb-0 row align-items-center">
                        <label for="newProductName" class="col-sm-10 col-form-label">${result.Values[i].Name}</label>
                        <div class="col-sm-2">
                            <button class="btn btn-light-danger  text-danger btn-sm font-weight-semibold fs-12 border" onclick="DeleteCategorySwal(${result.Values[i].Id})" title="Brand Delete" type="button">
                                <i class="bb-trash-2 text-danger fs-14"></i>
                            </button>
                        </div>
                    </div>
                `);
            }
        }, error: function (error) {
            // Error function

        }
    });
    return false;
}

//View Category in POS
function ViewCategoryNamePOS() {
    $('#Salonpackage').removeClass('active');
    $('#Salonoffer').removeClass('active');
    $("#Salonservice").addClass('active');
    $('#PackagesCategory').hide();
    $('#OfferCategory').hide();
    $('#salonPackagesList').hide();
    $('#salonOffersList').hide();
    $('#SalonCategory').show();
    $('#salonServicesList').show();
    let viewCategoryNamePOS = new Object();
    viewCategoryNamePOS.IsPageProvided = true;
  SalonServicesListPOS(0);
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpServices/LookUpServices_All?search&ParentId=0&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(viewCategoryNamePOS),
        crossDomain: true,
        async: false,
        success: function (result) {
            $("#SalonCategory").html(``);
            
            $('#SalonCategory').append(`<a class="filter_link active remact cursor-pointer" id="CatId_0" onclick="SalonServicesListPOS(0)" data-filter="all">${Langaugestore.All}</a>`);
           
            for (i = 0; i < result.Values.length; i++) {
                $('#SalonCategory').append(`
                    <a javascript:void(0) class="filter_link remact cursor-pointer" id="CatId_${result.Values[i].Id}" onclick="SalonServicesListPOS(${result.Values[i].Id})" data-filter="services">${result.Values[i].Name}</a>
                `);
            }
        }, error: function (error) {
            // Error function

        }
    });
    return false;
}

function DeleteCategorySwal(CategoryId) {
    Swal.fire({
        title: 'Are you sure Delete this Category ?',
        //text: "Are you sure Active this employee !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteSalonCategory(CategoryId);
        }
    })
}

//Delete this salon custome brand
function DeleteSalonCategory(CategoryId) {
    
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpServices/LookUpServices_Delete?Id=${CategoryId}&DeletedBy=${atob(SalonId)}`,
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        processData: false,
        contentType: false,
        crossDomain: true,
        success: function (result) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: result.Message,
                showConfirmButton: false,
                timer: 3000
            })
            setTimeout(function () { window.location.reload(); }, 3000);

        }, error: function (error) {
            
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
        }
    });
}

$('#categoryName').change(function () {
    ServiceName();    
});

//ServiceName dropdown API call in ajax methos

function ServiceName() {

    $("#serviceName").html(``);
    $('#serviceName').attr('disabled', true);

    let ServiceName = new Object();
    ServiceName.IsPageProvided = true;
    let serviceParentid = $('#categoryName').val();
    
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpServices/LookUpServices_All?search&ParentId=${serviceParentid}&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(ServiceName),
        crossDomain: true,
        async:false,
        success: function (result) {
            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#serviceName').append(`
                        <option priceduration="${result.Values[i].Price},${result.Values[i].Duration}" value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                    `);
                    $('#serviceName').removeAttr("disabled");
                }
            }

            $('.selectpicker').selectpicker("refresh");

        }, error: function (error) {
            
        }
    });
    return false;
}

//Salon service onchange get service price and duration and set assigned user 
function SetServicePriceDuration() {

    var TimeAndDuration = $(`#serviceName :selected`).attr("priceduration")

    if (TimeAndDuration != "" && TimeAndDuration != null) {

        var TimeAndDurationSplit = TimeAndDuration.split(",");

        $("#serverAssignEmployee #Price").val(TimeAndDurationSplit[0]);
        $("#serverAssignEmployee #Duration").val(TimeAndDurationSplit[1]);
    } else {
        $("#serverAssignEmployee #Price").val("0");
        $("#serverAssignEmployee #Duration").val("0");
    }
};

//View Category
function ViewServicesName() {
    let viewServicesName = new Object();
    viewServicesName.IsPageProvided = true;
    
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpServices/CustomServices_All?search&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(viewServicesName),
        crossDomain: true,
        async: false,
        success: function (result) {
            $("#SalonServices").html(``);
            
            if (result.Values.length <= 0) {
                $('#SalonServices').append(`
                    <div class="form-group mb-0 row justify-content-center align-items-center">
                        <label class="col-form-label text-muted" style="font-size:20px;">${Langaugestore.NoRecordsFound}</label>
                    </div>
                `);
            }
            for (i = 0; i < result.Values.length; i++) {
                if (result.Values[i].ParentId > 0) {
                    $('#SalonServices').append(`
                        <div class="form-group mb-0 row align-items-center">
                            <label for="newProductName" class="col-sm-10 col-form-label">${result.Values[i].Name}</label>
                            <div class="col-sm-2">
                                <button class="btn btn-light-danger  text-danger btn-sm font-weight-semibold fs-12 border" onclick="DeleteCategorySwal(${result.Values[i].Id})" title="Brand Delete" type="button">
                                    <i class="bb-trash-2 text-danger fs-14"></i>
                                </button>
                            </div>
                        </div>
                    `);
                }
            }
        }, error: function (error) {
            // Error function

        }
    });
    return false;
}


//employeeCharges section show condition

if (getSalonServicesIdatob > 0) {
    $('#employeeCharges').show();
}
else {
    $('#employeeCharges').html("");
}

//addSalonservices API Call In Ajax Method

function addSalonservices() {
    
    $('#addSalonservicesbtn').hide();
    $('#addSalonservicesbtnFalse').hide();
    $('#addSalonservicesbtnloading').show();
    
    let isValid = true;
    let errorMsg = "";
    let DurationStr = [];
    let PriceStr = [];
    let UserIdStr = [];
    if (EmployeeLength > 0) {
        for (i = 0; i < EmployeeLength; i++) {
            if (parseInt($(`#EmployeeData #emp${i} #empPrice`).val()) > 0 && parseInt($(`#EmployeeData #emp${i} #empDuration`).val()) > 0) {
                DurationStr.push($(`#EmployeeData #emp${i} #empDuration`).val());
                PriceStr.push($(`#EmployeeData #emp${i} #empPrice`).val());
                UserIdStr.push($(`#EmployeeData #emp${i} #UsId`).val());
                isValid = true;
            }
            else {
                isValid = false;
                errorMsg = "Please Enter Valid Price and Duration"
                break;
            }
        }
    }
    let AddSalonServices = new Object();
    if (isValid == true) {
        AddSalonServices.Id = $('#salonsServiceId').val();
        AddSalonServices.LookUpServicesId = $('#serviceName').val();
        if ($('#categoryName').val() != "") {
            AddSalonServices.LookUpCategoryId = $('#categoryName').val();
            
        } else {
            errorMsg = "Please Select Category";
            isValid = false;
        }
        
        AddSalonServices.SalonId = atob(SalonId);
        AddSalonServices.Description = $('#DescriptionComment').val();
        AddSalonServices.UserServiceId = UserIdStr.join(",");
        AddSalonServices.Duration = DurationStr.join(",");
        AddSalonServices.Price = PriceStr.join(",");
        AddSalonServices.CreatedBy = atob(UserId);
        AddSalonServices.UpdatedBy = atob(UserId);
        AddSalonServices.DeletedBy = atob(UserId);
    }
    
    if (isValid) {
        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/salonServices/SalonServices_Upsert',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(AddSalonServices),
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
                        window.location.href = '/SalonServices/ManageSalonServices';
                    }, 3000);
                }
                //$('#employeeForm')[0].reset();
                //$('.selectpicker').selectpicker("refresh");
                $('#addSalonservicesbtn').show();
                $('#addSalonservicesbtnFalse').hide();
                $('#addSalonservicesbtnloading').hide();

            }, error: function (error) {
                
                let msg = error.responseJSON.Message != null ? error.responseJSON.Message : error.responseJSON.Error.Message;
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: msg,
                    showConfirmButton: false,
                    timer: 3000
                });
                $('#addSalonservicesbtn').show();
                $('#addSalonservicesbtnFalse').hide();
                $('#addSalonservicesbtnloading').hide();
            }
        });
    } else {
        $('#addSalonservicesbtn').show();
        $('#addSalonservicesbtnloading').hide();
        Swal.fire({
            position: 'center',
            icon: 'error',
            title: errorMsg,
            showConfirmButton: false,
            timer: 3000
        });
    }
    return false;
}


function BlankSalonService() {
    debugger;
    let CreateBlankServices = new Object();
    CreateBlankServices.SalonId = atob(SalonId);
    CreateBlankServices.CreatedBy = atob(UserId);
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/salonServices/BlankSalonServices_Upsert',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(CreateBlankServices),
        crossDomain: true,
        success: function (result) {
            debugger;
            console.log("resultresult", result);
            if (result.Code == 200) {
                window.location.href = `/SalonServices/SalonServiceDetails?SalonServices=${btoa(result.Item.Id)}`;
            }
        }, error: function (error) {
            window.location.reload();
        }
    });
}