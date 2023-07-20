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

//changeStatusmodal open function
var ProductId = 0;
function changeStatusmodal(productId) {
    ProductId = productId;
    $('#changeProductStatusModal').modal('show');
    changeStatus();
}

//changeStatus modal open function call
function changeStatus() {
     

    $('#updateStatus').selectpicker('val', 0);

    $('#updateStatusgrp').hide();
    $('#updateStatusgrploader').show();

    if (ProductId > 0) {
        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/product/Product_ById?Id=' + ProductId + '',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                 
                $('#updateStatus').selectpicker('val', result.Item.LookUpStatusId);
                $('#updateStatusgrp').show();
                $('#updateStatusgrploader').hide();
                $('#Changestatus').attr('disabled', false);
            }, error: function (error) {
                //Error function
                $('#updateStatusgrp').show();
                $('#updateStatusgrploader').hide();
                $('#Changestatus').attr('disabled', false);
            }
        });
    }

    return false;
}

//productChangestatus validation function
function productChangestatus() {
    
    var changeStatusno = $('#updateStatus').val();

    if (changeStatusno === "") {
        Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Please select status',
            showConfirmButton: false,
            timer: 3000
        })
    } else {
        productChangestatusvalid();
    }
}


//productTypedrp dropdown API call in ajax methos

function productChangestatusvalid() {
    
    $('#Changestatus').hide();
    $('#Changestatusloading').show();

    var updateStatusId = $('#updateStatus').val();
     
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + `/api/product/Product_ChangeStatus?Id=${ProductId}&LookUpStatusId=${updateStatusId}`,
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
             
            $('#changeProductStatusModal').modal('hide');
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    productList.init();
                }, 3000);
            }

            $('#Changestatus').show();
            $('#Changestatusloading').hide();

        }, error: function (error) {
            
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
            $('#Changestatus').show();
            $('#Changestatusloading').hide();
        }
    });
    return false;
}