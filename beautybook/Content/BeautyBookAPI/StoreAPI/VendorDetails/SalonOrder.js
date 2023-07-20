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

var getVendorId = new URLSearchParams(window.location.search);
getVendorId = parseInt(atob(getVendorId.get('VendorId')));
var getVendorIdatob = ~~getVendorId;



//product list api call
function SalonOrderData() {

    $('#salonOrderdataloader').show();
     
    let OrderData = new Object();
    OrderData.IsPageProvided = true;

     
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/orders/Orders_All?search=&LookUpStatusId=0&OrderNo=&DateOfOrder=&SalonId=${atob(SalonId)}&VendorId=${getVendorIdatob}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(OrderData),
        crossDomain: true,
        async: false,
        success: function (result) {
             
            if (result.Values.length <= 0) {
                $('#salonOrderdata').html(`
                    <tr>
                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
                    </tr>
                `)
            } else {
                $("#salonOrderdata").html(``);
            }

            for (i = 0; i < result.Values.length; i++) {
                $('#salonOrderdata').append(`
                    <tr>
                        <th scope="row"><a href="javascript:;" onclick="OrderItemData(${result.Values[i].Id});">#${result.Values[i].Id}</a></th>
                        <td>${result.Values[i].OrderDate}</td>
                        <td>${result.Values[i].TotalItems}</td>
                        <td>SAR ${result.Values[i].ProductTotal}</td>
                    </tr>
                `);
            }

            $('#salonOrderdataloader').hide();

        }, error: function (error) {
            $('#salonOrderdataloader').hide();
            // Error function
        }
    });
    return false;
}


//Order Item Data api function call
function OrderItemData(orderId) {
     
    $('#orderDetailsModal').modal('show');
    let orderItemData = new Object();
    orderItemData.IsPageProvided = true;
     
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/orderProducts/OrderProducts_All?search=&OrderId=${orderId}&VendorId=0`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(orderItemData),
        crossDomain: true,
        async: false,
        success: function (result) {

            $('#OrderId').text('#'+orderId);

             
            if (result.Values.length <= 0) {
                $('#orderItem').html(`
                    <tr>
                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
                    </tr>
                `)
            } else {
                $("#orderItem").html(``);
                $("#subTotalarea").html(``);
            }

            for (i = 0; i < result.Values.length; i++) {
                $('#orderItem').append(`
                    <tr>
                        <th scope="row">${result.Values[i].Id}</th>
                        <td>${result.Values[i].ProductName == "" || result.Values[i].ProductName == null ? '-' : result.Values[i].ProductName}</td>
                        <td>
                            ${result.Values[i].ProductWeight == "" || result.Values[i].ProductWeight == null ? '-' : result.Values[i].ProductWeight}
                            ${result.Values[i].WeightTypeName == "" || result.Values[i].WeightTypeName == null ? '' : result.Values[i].WeightTypeName}
                        </td>
                        <td>SAR ${result.Values[i].ProductRate == "" || result.Values[i].ProductRate == null ? '-' : result.Values[i].ProductRate.toFixed(2)}</td>
                        <td>x${result.Values[i].Qty == "" || result.Values[i].Qty == null ? '-' : result.Values[i].Qty }</td>
                        <td>SAR ${result.Values[i].ProductAmount == "" || result.Values[i].ProductAmount == null ? '-' : result.Values[i].ProductAmount.toFixed(2)}</td>
                    </tr>
                `);
            }

            $('#subTotalarea').append(`
                <dl class="row text-sm-right vendor-order-subtotal">
                    <dt class="col-sm-6">${Langaugestore.Subtotal}:</dt>
                    <dd class="col-sm-6">SAR ${result.Values[0].ProductSubTotal == "" || result.Values[0].ProductSubTotal == null ? '-' : result.Values[0].ProductSubTotal}.00</dd>
                    <dt class="col-sm-6">${Langaugestore.Shipping_fee}:</dt>
                    <dd class="col-sm-6">SAR 0.00</dd>
                    <dt class="col-sm-6">${Langaugestore.Tax}:</dt>
                    <dd class="col-sm-6">SAR ${result.Values[0].ProductTaxAmount == "" || result.Values[0].ProductTaxAmount == null ? '-' : result.Values[0].ProductTaxAmount}.00</dd>
                    <dt class="col-sm-6">${Langaugestore.Total}:</dt>
                    <dd class="col-sm-6">SAR ${result.Values[0].ProductTotal == "" || result.Values[0].ProductTotal == null ? '-' : result.Values[0].ProductTotal}.00</dd>
                </dl>
            `);

        }, error: function (error) {
            // Error function
        }
    });
    return false;

}