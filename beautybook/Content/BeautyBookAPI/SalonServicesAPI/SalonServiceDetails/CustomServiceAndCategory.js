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


//Service upload image name get
function servPhotoUpload() {
    var fake_path = $('#servPhoto').val();
    $('#uploadImagenames').text(fake_path.split("\\").pop());
}

//customCategoryadd ajax method

function customCategoryadd() {
    
    $('#AddCustomCategory').hide();
    $('#AddCustomCategoryLoading').show();

    $('#AddCustomService').hide();
    $('#AddCustomServiceLoading').show();
    debugger;

    var formData = new FormData();
    var parentCategoryId = $('#customCategoryName').val() == "" ? 0 : parseInt($('#customCategoryName').val());
    var ServPhotoUrl = document.getElementById("servPhoto");
    formData.append("ServPhotoUrl", ServPhotoUrl.files[0]);
    formData.append("Id", 0);
    formData.append("Name", $('#CustomCategory').val() == "" ? $('#CustomService').val() : $('#CustomCategory').val());
    formData.append("ParentId", parentCategoryId);
    formData.append("Price", $('#CustomServicePrice').val() == "" ? 0 : $('#CustomServicePrice').val());
    formData.append("Duration", $('#CustomServiceDuration').val() == "" ? 0 : $('#CustomServiceDuration').val());
    formData.append("Email", $("#Email").val());
    formData.append("UserId", parseInt(atob(UserId)));
    formData.append("SalonId", parseInt(atob(SalonId)));
    

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/lookUpServices/LookUpServices_Upsert',
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        processData: false,
        contentType: false,
        dataType: 'json',
        data: formData,
        crossDomain: true,

    //var parentCategoryId = $('#customCategoryName').val() == "" ? 0 : parseInt($('#customCategoryName').val());

    //var CustomCategoryAdd = new Object();
    //debugger;
    //var servphotourl = document.getelementbyid("servphoto");
    //customcategoryadd.servphotourl = ("servphotourl", servphotourl.files[0]);
    //CustomCategoryAdd.Id = 0;
    //CustomCategoryAdd.Name = $('#CustomCategory').val() == "" ? $('#CustomService').val() : $('#CustomCategory').val();
    //CustomCategoryAdd.ParentId = parentCategoryId;
    //CustomCategoryAdd.Price = $('#CustomServicePrice').val() == "" ? 0 : $('#CustomServicePrice').val();
    //CustomCategoryAdd.Duration = $('#CustomServiceDuration').val() == "" ? 0 : $('#CustomServiceDuration').val();
    //CustomCategoryAdd.UserId = parseInt(atob(UserId));
    //CustomCategoryAdd.SalonId = parseInt(atob(SalonId));

    //$.ajax({
    //    type: 'POST',
    //    url: APIEndPoint + `/api/lookUpServices/LookUpServices_Upsert`,
    //    headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
    //    dataType: 'json',
    //    data: JSON.stringify(CustomCategoryAdd),
    //    crossDomain: true,
        success: function (result) {
            ViewServicesName();
            ViewCategoryName();
            if (result.Code == 200) {
                /*setCookie("ServPhotosUrl", result.Item.ServPhotoUrl, 30);*/
                $('#CustomCategory').val("");
                $('#CustomService').val("");
                customCategoryGet();
                CategoryName();
                ServiceName();
                $('#newCategoryModal').modal('hide');
                $('#newServiceModal').modal('hide');
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
            }

            $('#AddCustomCategory').show();
            $('#AddCustomCategoryLoading').hide();

            $('#AddCustomService').show();
            $('#AddCustomServiceLoading').hide();

        }, error: function (error) {
            
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })

            $('#AddCustomCategory').show();
            $('#AddCustomCategoryLoading').hide();

            $('#AddCustomService').show();
            $('#AddCustomServiceLoading').hide();

        }
    });
    return false;
}



//customCategoryGet ajax method
function customCategoryGet() {

    $('#customCategoryName').html('');

    
    var CustomCategoryAdd = new Object();
    CustomCategoryAdd.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpServices/CustomCategory_All?search=&ParentId=0&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(CustomCategoryAdd),
        crossDomain: true,
        success: function (result) {
            
            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#customCategoryName').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                    `);
                }
            }

            $('.selectpicker').selectpicker("refresh");
           
        }, error: function (error) {
            //Error Function
        }
    });
    return false;
}