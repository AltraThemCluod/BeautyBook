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
function salonlistdropdown() {

    var SalonName = new Object();
    SalonName.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/salons/Salons_All?search&LookUpStatusId=0',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(SalonName),
        crossDomain: true,
        success: function (result) {
            result.Values.reverse();

            $("#salonName").html(``);

            for (i = 0; i < result.Values.length; i++) {
                $('#salonName').append(`
                 <option value="${result.Values[i].Id}">${result.Values[i].SalonName}</option>
                `);
                $('.selectpicker').selectpicker("refresh");
            }

        }, error: function (error) {
            // Error function
        }
    });
    return false;
}
var DeviceTokenNumber = getCookie("DeviceToken&Type"); 
 
var VendorId = getCookie("UserId");


//fillter function
function seachOrder() {
    $('#searchOrderbtn').hide();
    $('#searchOrderbtnloading').show();
    orderList.init();
}

//fllter reset function
function resetOrder() {
    $('#resetOrderbtn').hide();
    $('#resetOrderbtnloading').show();
    $('#orderDate').val('');
    $('#orderno').val('');
    $('#orderStatus').selectpicker('val', null);
    $('#salonName').selectpicker('val', null);
    orderList.init();
}

var orderList = function () {

    $('#orderListloader').show();

    let initOrderlist = function () {
        var orderDate = $('#orderDate').val() == "" || $('#orderDate').val() == null ? "" : $('#orderDate').val();
        var orderStatus = $('#orderStatus').val() == "" || $('#orderStatus').val() == null ? 0 : $('#orderStatus').val();
        var Search = null;
        var orderNo = $('#orderno').val() == "" || $('#orderno').val() == null ? "" : $('#orderno').val();
        var salonName = $('#salonName').val() == "" || $('#salonName').val() == null ? 0 : $('#salonName').val();
        var vendorId = atob(VendorId);

        let OrderList = new Object();
        OrderList.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/orders/Orders_All?search=${Search}&LookUpStatusId=${orderStatus}&OrderNo=${orderNo}&DateOfOrder=${orderDate}&SalonId=${salonName}&VendorId=${vendorId}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(OrderList),
            crossDomain: true,
            success: function (Values) {
                console.log(Values);
                var i = 1;
                 
                $('#orderList').DataTable({
                    "order": [[0, "desc"]],
                    data: Values.Values,
                    
                    columns: [
                        {
                            "title": ""+ Langaugestore.Order_Id+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `<a onclick="vieworderDetails(${row["Id"]});" style="cursor: pointer;color: #00acc1;">${row["Id"]}</a>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.Order_Date+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["OrderDate"] == "" || row["OrderDate"] == null ? '-' : row["OrderDate"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },
                        {
                            "title": "" + Langaugestore.Customer+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["CustomerName"] == "" || row["CustomerName"] == null ? '-' : `<a onclick="salonDetailsInOrderDetails(${row["SalonId"]});" style="cursor: pointer;color: #00acc1;font-weight: 600;">${row["CustomerName"]}</a>`}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.Total_Amount+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<b>${row["ProductTotal"] == "" || row["ProductTotal"] == 0 ? '-' : 'SAR ' + row["ProductTotal"].toFixed(2)}</b>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.Payment+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                if (row["Payment"] == 22) {
                                    htmlData += `${row["Payment"] == 22 ? `<div class="badge badge-success border p-2">${row["PaymentTypeName"]}</div>` : ''}`;
                                }
                                else {
                                    htmlData += `${row["Payment"] == 23 ? `<div class="badge badge-primary border p-2">${row["PaymentTypeName"]}</div>` : ''}`;
                                    }
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.Order_Status+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                if (row["LookUpStatusId"] == 16) {
                                    htmlData += `${row["LookUpStatusId"] == 16 ? `<div class="badge bg-soft-warning text-warning border p-2">${row["LookUpStatusName"]}</div>` : '-'}`;
                                }
                                else if (row["LookUpStatusId"] == 17) {
                                    htmlData += `${row["LookUpStatusId"] == 17 ? `<div class="badge bg-soft-danger text-danger border p-2">${row["LookUpStatusName"]}</div>` : '-'}`;
                                }
                                else if (row["LookUpStatusId"] == 18) {
                                    htmlData += `${row["LookUpStatusId"] == 18 ? `<div class="badge bg-soft-success text-success border p-2">${row["LookUpStatusName"]}</div>` : '-'}`;
                                }
                                else if (row["LookUpStatusId"] == 19) {
                                    htmlData += `${row["LookUpStatusId"] == 19 ? `<div class="badge bg-soft-primary text-primary border p-2">${row["LookUpStatusName"]}</div>` : '-'}`;
                                }
                                else if (row["LookUpStatusId"] == 20) {
                                    htmlData += `${row["LookUpStatusId"] == 20 ? `<div class="badge bg-soft-secondary text-secondary border p-2">${row["LookUpStatusName"]}</div>` : '-'}`;
                                }
                                else if (row["LookUpStatusId"] == 21) {
                                    htmlData += `${row["LookUpStatusId"] == 21 ? `<div class="badge bg-soft-success text-success border p-2">${row["LookUpStatusName"]}</div>` : '-'}`;
                                }
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.Last_Updated+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["UpdatedDate"] == "" || row["UpdatedDate"] == null ? '-' : row["UpdatedDateStr"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Actions+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<div class="dropdown">
                                                <button class="btn btn-icon" type="button" data-toggle="dropdown">
                                                    <i class="bb-more-horizontal fs-22"></i>
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right">
                                                    <a class="dropdown-item" href="javascript:void(0)" onclick="vieworderDetails(${row["Id"]});"><i class="bb-repeat text-gray-600 fs-16 mr-3"></i>${Langaugestore.View}</a>
                                                    <a class="dropdown-item" href="javascript:void(0)" onclick="GeneratInvoice('${btoa(row["OrignalInvoiceNo"])}');">
                                                        <svg style="color:#6c757d;" class="fs-16 mr-3" xmlns="http://www.w3.org/2000/svg" height="18" width="18" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
                                                        </svg>
                                                        Invoice
                                                    </a>
                                                    ${row["LookUpStatusId"] == 21 ? '' : 
                                                        `<a class="dropdown-item" href="javascript:void(0)" onclick="changeOrderStatusmodal(${row["Id"]});"><i class="bb-edit-3 text-gray-600 fs-16 mr-3"></i>${Langaugestore.Update_Status}</a>
                                                        <hr>
                                                        <a class="dropdown-item text-danger" href="javascript:void(0)" onclick="orderDelete(${row[" Id"]})"><i class="bb-x-circle text-danger fs-16 mr-3"></i>${Langaugestore.Reject}</a>`
                                                    }
                                                    
                                                </div>
                                            </div>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "2%"
                        },
                    ],
                    buttons: [
                        {
                            extend: 'pdf',
                            className: 'btn btn-light border font-weight-medium float-right mb-3',
                            text: '<i class="bb-printer fs-16 mr-2"></i>' + Langaugestore.Print + '',
                        },
                        {
                            extend: 'excel',
                            className: 'btn btn-light border font-weight-medium float-right mb-3 mr-2',
                            text: '<i class="bb-download fs-16 mr-2"></i>' + Langaugestore.ExporttoExcel + '',
                        },
                    ],
                    responsive: true,
                    "lengthMenu": [
                        [5, 15, 20, 40],
                        [5, 15, 20, 40] // change per page values here
                    ],
                    "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
                });
               
                $('#searchOrderbtn').show();
                $('#searchOrderbtnloading').hide();

                $('#resetOrderbtn').show();
                $('#resetOrderbtnloading').hide();

                $('#orderListloader').hide();

            }, error: function (error) {
                $('#searchOrderbtn').show();
                $('#searchOrderbtnloading').hide();

                $('#resetOrderbtn').show();
                $('#resetOrderbtnloading').hide();

                $('#orderListloader').hide();

            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#orderList")) {
                $('#orderList').dataTable().fnDestroy();
                $('#manageOrderdiv').html('<table id="orderList" class="table table-card" style="width:100%; display:inherit;"</table >');
            }
            initOrderlist();
        }
    };
}();