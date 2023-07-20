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

var orderList = function () {

    $('#orderListloader').show();

    let initOrderlist = function () {
        let OrderList = new Object();
        OrderList.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/orders/SalonOrder_All?search=&SalonId=${atob(SalonId)}`,
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
                            "title": "" + Langaugestore.Order_ID+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                if (PermissionData.find(record => record.ModuleKey === "ViewOrderDetails").Value == true) {
                                    htmlData += `<a onclick="vieworderDetails(${row["Id"]});" style="cursor: pointer;color: #00acc1;"><b>#${row["OrderNo"]}</b></a>`;
                                }
                                else {
                                    htmlData += `<a href="javascript:void(0)" style="cursor: pointer;color: #00acc1;"><b>#${row["OrderNo"]}</b></a>`;
                                }
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.ORDER_DATE+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["OrderDate"] == "" || row["OrderDate"] == null ? '-' : row["OrderDate"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.TOTAL_AMOUNT+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<b>${row["ProductTotal"] == "" || row["ProductTotal"] == 0 ? '-' : 'SAR ' + row["ProductTotal"].toFixed(2)}</b>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.PAYMENT+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                if (row["Payment"] == 22) {
                                    htmlData += `${row["Payment"] == 22 ? `<div class="badge badge-success border p-2">${row["PaymentTypeName"]}</div>` : '-'}`;
                                }
                                else {
                                    htmlData += `${row["Payment"] == 23 ? `<div class="badge badge-primary border p-2">${row["PaymentTypeName"]}</div>` : ''}`;
                                }
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.ORDER_STATUS+"", "data": "",
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
                            "title": "" + Langaugestore.LAST_UPDATED+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["UpdatedDate"] == "" || row["UpdatedDate"] == null ? '-' : row["UpdatedDateStr"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "Invoice", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                if (PermissionData.find(record => record.ModuleKey === "OrderViewInvoice").Value == true) {
                                    htmlData = `<a target="_blank" class="btn btn-primary font-weight-semibold" href="/Store/Invoice?OrderId=${btoa(row["Id"])}">
                                                <svg xmlns="http://www.w3.org/2000/svg" height="18" width="18" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z"></path>
                                                </svg>
                                                Invoice
                                            </a>`;
                                }
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
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

                $('#orderListloader').hide();
            }, error: function (error) {
                $('#orderListloader').hide();
            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#orderList")) {
                $('#orderList').dataTable().fnDestroy();
                $('#orderListdiv').html('<table id="orderList" class="table table-card" style="width:100%; display:inherit;"></table >');
            }
            initOrderlist();
        }
    };
}();