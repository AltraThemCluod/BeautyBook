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
var userId = getCookie("UserId");
var salonId = getCookie("SalonId");

var getSalonId = atob(salonId);
var getSalonIdatob = ~~getSalonId;

//get product amount
$('#subTotal').text(atob(getCookie("SubTotal")));
$('#subTotalInp').val(atob(getCookie("SubTotal")));
$('#VATIncluded').text(atob(getCookie("VATincluded")));
$('#VATIncludedInp').val(atob(getCookie("VATincluded")));
$('#TotaltoPay').text(atob(getCookie("TotalToPay")));
$('#TotaltoPayInp').val(atob(getCookie("TotalToPay")));

function editAddress(AddressId) {
    $("#newAddressModal").modal("show");
        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/shippingAddress/ShippingAddress_ById?Id=' + AddressId + '',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                $('#addressId').val(result.Item.Id)
                $('#salonName').val(result.Item.SalonId);
                $('#checkoutaddress').val(result.Item.Address);
                if (result.Item.IsDefault == 1) {
                    $('#makeDefaultAddress').attr('checked', true);
                }
                else {
                    $('#makeDefaultAddress').attr('checked', false);
                }
                $('#countryName').val(result.Item.CountryId);
                $('#stateName').val(result.Item.StateId);
                $('#cityName').val(result.Item.City);
                $('#zipCode').val(result.Item.ZipCode);
                $('#primaryNumber').val(result.Item.PrimaryNumber);
                $('#alternateNumber').val(result.Item.AlternateNumber)
                $('.selectpicker').selectpicker("refresh");

            }, error: function (error) {
            }
        });
        return false;

}
function AddNewAddress() {
    $('#salonName').val('');
    $('#checkoutaddress').val('');
    $('#countryName').val(0);
    $('#stateName').val(0);
    $('#cityName').val('');
    $('#zipCode').val('');
    $('#primaryNumber').val('');
    $('#alternateNumber').val('');
    $('.selectpicker').selectpicker("refresh");
    $('#makeDefaultAddress').attr('checked', false);
    $("#newAddressModal").modal("show");

}
function saveAddressdetails() {

    var ShippingAddress = new Object();

    ShippingAddress.Id = $('#addressId').val();
    ShippingAddress.SalonId = $('#salonName').val();
    ShippingAddress.Address = $('#checkoutaddress').val();
    ShippingAddress.CountryId = $('#countryName').val();
    ShippingAddress.StateId = $('#stateName').val();
    ShippingAddress.City = $('#cityName').val();
    ShippingAddress.ZipCode = $('#zipCode').val();
    ShippingAddress.PrimaryNumber  = $('#primaryNumber').val();
    ShippingAddress.AlternateNumber = $('#alternateNumber').val();
    var checkedIsDefaultValue = $('#makeDefaultAddress').is(':checked');
    ShippingAddress.IsDefault = checkedIsDefaultValue;
    ShippingAddress.CreatedBy = getSalonId;
    ShippingAddress.UpdatedBy = $('#addressId').val();

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/shippingAddress/ShippingAddress_Upsert',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(ShippingAddress),
        success: function (result) {
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
            }
            $('#salonName').val('');
            $('#checkoutaddress').val('');
            $('#countryName').val(0);
            $('#stateName').val(0);
            $('#cityName').val('');
            $('#zipCode').val('');
            $('#primaryNumber').val('');
            $('#alternateNumber').val('');
            $("#makeDefaultAddress").removeAttr("disabled");
            $(checkedIsDefaultValue).removeAttr();
            $('.selectpicker').selectpicker("refresh");
            $('#makeDefaultAddress').prop('checked', false);
            $('#makeDefaultAddress').attr('checked', false);
            $("#newAddressModal").modal("hide");
            addressList();
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
    return false;
}


function addressList() {
    $('#shippingaddresslist').html(``);
    $('#Billingaddresslist').html(``);
    $('#addressloader').show();

    let AddressList = new Object();
    AddressList.IsPageProvided = true;
     
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/shippingAddress/ShippingAddress_All?search=&SalonId=${getSalonIdatob}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(AddressList),
        crossDomain: true,
        success: function (result) {
             
            if (result.Values.length <= 0) {
                $("#shippingaddresslist").html(`
                <div class="col-lg-6 mb-3">
                    <div class="border p-3 rounded">
                        ${Langaugestore.NoRecordsFound}
                    </div>
                </div>
                `);
            } else {
                for (i = 0; i < result.Values.length; i++) {
                    $('#shippingaddresslist').append(`
                         <div class="col-lg-6 mb-3">
                            <div class="border p-3 rounded">
                                <div class="custom-control custom-radio mb-3">
                                    <input type="radio" class="custom-control-input" id="ShippingAddress${result.Values[i].Id}" name="ShippingAddress" value="${result.Values[i].Id}" checked="">
                                    <label for="ShippingAddress${result.Values[i].Id}" class="custom-control-label d-flex align-items-center">
                                        <span>
                                            <strong class="d-block mb-2">${result.Values[i].SalonName}<div class="badge badge-primary p-2 ml-3">${result.Values[i].IsDefault == 0 ? `` : `${Langaugestore.Default}`}</div></strong>
                                            <span class="text-muted fs-14">${result.Values[i].Address}</span>
                                        </span>
                                    </label>
                                </div>

                                <div class="d-flex flex-wrap">
                                    <a onclick="editAddress(${result.Values[i].Id})" class="btn btn-outline-secondary btn-sm m-1" >${Langaugestore.EDIT}</a>
                                    <a onclick="deleteAddress(${result.Values[i].Id})" class="btn btn-outline-danger btn-sm m-1">${Langaugestore.Delete}</a>
                                </div>
                            </div>
                        </div>
                    `);


                    $('#Billingaddresslist').append(`
                         <div class="col-lg-6 mb-3">
                            <div class="border p-3 rounded">
                                <div class="custom-control custom-radio mb-3">
                                    <input type="radio" class="custom-control-input" id="BillingAddress${result.Values[i].Id}" value="${result.Values[i].Id}" name="BillingAddress" checked="">
                                    <label for="BillingAddress${result.Values[i].Id}" class="custom-control-label d-flex align-items-center">
                                        <span>
                                            <strong class="d-block mb-2">${result.Values[i].SalonName}<div class="badge badge-primary p-2 ml-3">${result.Values[i].IsDefault == 0 ? `` : `Default`}</div></strong>
                                            <span class="text-muted fs-14">${result.Values[i].Address}</span>
                                        </span>
                                    </label>
                                </div>

                                <div class="d-flex flex-wrap">
                                    <a onclick="editAddress(${result.Values[i].Id})" class="btn btn-outline-secondary btn-sm m-1" >${Langaugestore.EDIT}</a>
                                    <a onclick="deleteAddress(${result.Values[i].Id})" class="btn btn-outline-danger btn-sm m-1">${Langaugestore.Delete}</a>
                                </div>
                            </div>
                        </div>
                    `);
                    

                }
            }
            $('#addressloader').hide();
           
        }, error: function (error) {
             
            // Error function
            $('#addressloader').hide();
        }
    });
    return false;
}

//swal Delete employee
function deleteAddress(AddressId) {

    Swal.fire({
        title: '' + Langaugestore.Are_you_sure_you_want_to_delete_this_address_details_+'',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteAddressswal(AddressId);
        }
    })
}

// DeleteEmployeeManager API call in ajax method

function DeleteAddressswal(AddressId) {

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/shippingAddress/ShippingAddress_Delete?Id=' + AddressId + '&DeletedBy=' + atob(userId) + '',
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        processData: false,
        contentType: false,
        crossDomain: true,
        success: function (result) {
            if (result.Code == 200) {
                if (result.Code == 200) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: result.Message,
                        showConfirmButton: false,
                        timer: 3000
                    })
                    addressList();
                }
            }
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

//MasterOrderStatus dropdown API call in ajax methos
function salonName() {

    let SalonName = new Object();
    SalonName.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/salons/Salons_ByUserId?search=&LookUpStatusId=0&UserId=${atob(userId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(SalonName),
        crossDomain: true,
        async: false,
        success: function (result) {
            
            $("#salonName").html(``);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#salonName').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].SalonName}</option>
                    `);
                }
            }

            $('.selectpicker').selectpicker("refresh");


        }, error: function (error) {
            // Error function

        }
    });
    return false;
}

function SelectCountry() {

    var SelectCountry = new Object();
    SelectCountry.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/lookUpCountry/LookUpCountry_All?search',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(SelectCountry),
        crossDomain: true,
        success: function (result) {

            result.Values.reverse();

            $("#countryName").html(``);

            for (i = 0; i < result.Values.length; i++) {
                $('#countryName').append(`
                     <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                `);
            }
            $('.selectpicker').selectpicker("refresh");
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}

$('#countryName').change(function () {
    SelectState();
})

//State dropdown API call in ajax methos
function SelectState() {

    $('#stateName').attr('disabled', true);

    var SelectState = new Object();
    SelectState.IsPageProvided = true;
    var countryId = ~~$('#countryName').val();

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/lookUpState/LookUpState_All?search&CountryId=' + countryId + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(SelectState),
        crossDomain: true,
        async: false,
        success: function (result) {
            result.Values.reverse();

            $("#stateName").html(``);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#stateName').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                    `);
                    $('#stateName').removeAttr("disabled");
                }
            }

            $('.selectpicker').selectpicker("refresh");
        }, error: function (error) {
            // Error function

        }
    });
    return false;
}