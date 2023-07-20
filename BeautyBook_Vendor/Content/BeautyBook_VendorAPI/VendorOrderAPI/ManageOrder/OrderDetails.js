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

function vieworderDetails(orderId) {
    
    $('#ordersummaryetails').html(``);
    $('#orderitemdetails').html(``);
    $('#orderDetails').modal('show');


    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + `/api/orders/Orders_ById?Id=${orderId}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
                $('#ordersummaryetails').append(`
                    <div class="col-lg-4 col-sm-6 mb-3">
                        <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Order_Id}</div>
                        <div>
                            <span class="font-weight-medium">${result.Item.OrderNo}</span>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-6 mb-3">
                        <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Order_Date}</div>
                        <div>
                            <span class="font-weight-medium">${result.Item.OrderDate}</span>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-6 mb-3">
                        <div class="text-gray-500 fs-13 mb-1">Total Payment</div>
                        <div>
                            <span class="font-weight-medium">SAR ${result.Item.OrderProducts[0].ProductTotal}</span>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-6 mb-3">
                        <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Payment}</div>
                        <div>
                            ${result.Item.Payment == 22 ? `<div class="badge badge-success border p-2">${result.Item.PaymentTypeName}</div>` : ``}
                            ${result.Item.Payment == 23 ? `<div class="badge badge-primary border p-2">${result.Item.PaymentTypeName}</div>` : ``}
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-6 mb-3">
                        <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Order_Status}</div>
                        <div>
                            ${result.Item.LookUpStatusId == 16 ? `<div class="badge bg-soft-warning text-warning border p-2">${result.Item.LookUpStatusName}</div>` : ``}
                            ${result.Item.LookUpStatusId == 17 ? `<div class="badge bg-soft-danger text-danger border p-2">${result.Item.LookUpStatusName}</div>` : ``}
                            ${result.Item.LookUpStatusId == 18 ? `<div class="badge bg-soft-success text-success border p-2">${result.Item.LookUpStatusName}</div>` : ``}
                            ${result.Item.LookUpStatusId == 19 ? `<div class="badge bg-soft-primary text-primary border p-2">${result.Item.LookUpStatusName}</div>` : ``}
                            ${result.Item.LookUpStatusId == 20 ? `<div class="badge bg-soft-secondary text-secondary border p-2">${result.Item.LookUpStatusName}</div>` : ``}
                            ${result.Item.LookUpStatusId == 21 ? `<div class="badge bg-soft-success text-success border p-2">${result.Item.LookUpStatusName}</div>` : ``}
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-6 mb-3">
                        <div class="text-gray-500 fs-13 mb-1">${Langaugestore.Last_Updated}</div>
                        <div>
                            <span class="font-weight-medium">${result.Item.UpdatedDateStr}</span>
                        </div>
                    </div>

                    <div class="col-lg-4 col-sm-6 mb-3">
                        <div class="text-gray-500 fs-13 mb-1">${Langaugestore.CustomerName}</div>
                        <div>
                            <span class="font-weight-medium">${result.Item.CustomerName}</span>
                        </div>
                    </div>
                    <div class="col-lg-6 mb-3">
                        <div class="text-gray-500 fs-13 mb-1">${Langaugestore.ShipmentTo}</div>
                        <div>
                            <span class="font-weight-medium">${result.Item.ShippingCustomerName == "" || result.Item.ShippingCustomerName == null ? '' : result.Item.ShippingCustomerName}</span>
                        </div>
                    </div>
                `);
            // orderitemdetails data append

            if (result.Item.OrderProducts.length <= 0)
            {
                $('#orderitemdetails').html(`
                    <tr>
                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
                    </tr>
                `)
            } else {
                $("#orderitemdetails").html(``);
            }
            
            var orderdetailstable =
                `
                    <table class="table mb-0">
                    <thead>
                        <tr>
                            <th class="col-3">${Langaugestore.ProductName}</th>
                            <th class="col-2">${Langaugestore.RATE}</th>
                            <th class="col-1">${Langaugestore.QTY}</th>
                            <th class="col-2 text-right">${Langaugestore.Amount}</th>
                        </tr>
                    </thead>
                `;
            
                for (i = 0; i < result.Item.OrderProducts.length; i++) {
                    orderdetailstable += `
                        <tbody>
                            <tr>
                                <td class="col-3">${result.Item.OrderProducts[i].ProductName}</td>
                                <td class="col-2 text-center">SAR ${result.Item.OrderProducts[i].ProductRate.toFixed(2)}</td>
                                <td class="col-1 text-center">${result.Item.OrderProducts[i].Qty}</td>
                                <td class="col-2 text-right">SAR ${result.Item.OrderProducts[i].ProductAmount.toFixed(2)}</td>
                            </tr>
                    `;
                }

                orderdetailstable += `
                    <tr>
                        <td colspan="3" class="text-right"><strong>${Langaugestore.SubTotal}:</strong></td>
                        <td class="text-right">SAR ${result.Item.OrderProducts[0].ProductSubTotal.toFixed(2)}</td>
                    </tr>
                    <tr>
                        <td colspan="3" class="text-right"><strong>${Langaugestore.Tax}:</strong></td>
                        <td class="text-right">SAR ${result.Item.OrderProducts[0].ProductTaxAmount.toFixed(2)}</td>
                    </tr>
                    <tr>
                        <td colspan="3" class="text-right"><strong>${Langaugestore.Total}:</strong></td>
                        <td class="text-right">SAR ${result.Item.OrderProducts[0].ProductTotal.toFixed(2)}</td>
                    </tr>
                `;
                orderdetailstable += `
                    </tbody>
                    </table>
                `;
            $("#orderitemdetails").append(orderdetailstable);
            // orderitemdetails data append
            if (result.Item.OrderStatusTracking.length <= 0) {
                $('#orderstatustracking').html(`
                    <tr>
                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
                    </tr>
                `)
            } else {
                $("#orderstatustracking").html(``);
            }
            for (i = 0; i < result.Item.OrderStatusTracking.length; i++) {
                $('#orderstatustracking').append(`
                   <div class="timeline-item done">
                    <h6 class="fs-14">${result.Item.OrderStatusTracking[i].LookUpStatus}</h6>
                    <div>
                        <p class="text-muted fs-14 mb-0 lh-1">${result.Item.OrderStatusTracking[i].Comment == null || result.Item.OrderStatusTracking[i].Comment == "" ? `-` : result.Item.OrderStatusTracking[i].Comment}</p>
                        <span class="text-muted fs-12">${result.Item.OrderStatusTracking[i].CreatedDateStr}</span>
                    </div>
                </div>
                `);
            }

            OrderUpdateStatus(orderId);
        }
    });
    return false;
}



function OrderUpdateStatus(orderId) {
    debugger;
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `api/orderProducts/SalonOwnerOrdersCount_UpdateStatus?OrderId=${orderId}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result)
        {
            debugger;
            if (result.Code == 200)
            {
                GetVendorNewOrderCount();
            }
        }, error: function (error) {
            //Error function
        }
    });
}