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
var VendorId = getCookie("UserId");


//Product api delete function

//swal Delete employee
function inventoryDelete(inventoryId) {
    Swal.fire({
        title: 'Are you sure you want to delete this inventory ?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteInventoryswal(inventoryId);
        }
    })
}

// product Delete API call in ajax method

function DeleteInventoryswal(inventoryId) {
     
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + `/api/inventoryProduct/InventoryProduct_Delete?Ids=${atob(inventoryId)}&DeletedBy=${atob(VendorId)}`,
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        processData: false,
        contentType: false,
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
                    inventoryList.init();
                }, 3000);
            }
        }, error: function (error) {
            // Error function
        }
    });
}