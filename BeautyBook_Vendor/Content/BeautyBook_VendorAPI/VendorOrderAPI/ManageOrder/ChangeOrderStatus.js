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
var OrderId = 0;
function changeOrderStatusmodal(orderId) {
    OrderId = orderId;
    $('#updateOrderStatusComment').val('');
    $('#changeOrderStatusModal').modal('show');
    changeOrderStatus();
}

//changeStatus modal open function call
function changeOrderStatus() {
    $('#updateOrderStatus').selectpicker('val', 0);

    $('#updateStatusgrp').hide();
    $('#updateStatusgrploader').show();

    if (OrderId > 0) {
        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/orders/Orders_ById?Id=' + OrderId + '',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                 
                $('#updateOrderStatus').selectpicker('val', result.Item.LookUpStatusId);
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
function orderChangestatus() {
    
    var changeOrderStatusno = $('#updateOrderStatus').val();

    if (changeOrderStatusno === "") {
        Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Please select status',
            showConfirmButton: false,
            timer: 3000
        })
    } else {
        orderChangestatusvalid();
    }
}


//productTypedrp dropdown API call in ajax methos

function orderChangestatusvalid() {
    
    $('#Changestatus').hide();
    $('#Changestatusloading').show();

    var orderstatusid = $('#updateOrderStatus').val();
    var comment = $('#updateOrderStatusComment').val();
     
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + `/api/orders/Orders_ChangeStatus?Id=${OrderId}&LookUpStatusId=${orderstatusid}&Comment=${comment}`,
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
             
            $('#changeOrderStatusModal').modal('hide');
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    orderList.init();
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