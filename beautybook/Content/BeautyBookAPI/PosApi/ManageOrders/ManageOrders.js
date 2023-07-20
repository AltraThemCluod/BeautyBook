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
    

//posOrderList API call in ajax method
var PosOrderList = function () {
    $('#loader').show();
     
    let initemployeeDatalist = function () {
        var posOrderList = new Object();
        posOrderList.IsPageProvided = true;
        
        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/pOSDetails/POSDetails_All?search&SalonId=${atob(SalonId)}&POSSessionId=${SessionId}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(posOrderList),
            crossDomain: true,
            //async:false,
            success: function (Values) {
                $('#posOrderDatalist').DataTable({
                    "order": [[0, "desc"]],
                    data: Values.Values,
                    columns: [
                        {
                            "title": "Id", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["Id"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "0%"
                        },
                        {
                            "title": "Invoice Number", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `#${row["POSInvoiceNumber"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "10%"
                        },
                        {
                            "title": "Customer Name", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["CustomerName"] == "" || row["CustomerName"] == null ? "-" : row["CustomerName"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "10%"
                        },
                        {
                            "title": "Total Amount", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["TotalAmount"] == "" || row["TotalAmount"] == null ? "-" : "SAR" +" "+ row["TotalAmount"].toFixed(2)}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "10%"
                        },
                        {
                            "title": "Created Date", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["CreatedDateStr"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "10%"
                        },
                        {
                            "title": "Actions", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                if (PermissionData.find(record => record.ModuleKey === "ViewPOSOrderServices").Value == true) {
                                    htmlData += `<a href="#!" onclick="ViewOrderDetails('${btoa(row["Id"])}')" class="btn btn-light border btn-sm"><i class="bb-eye text-gray-600 fs-16 mr-2"></i>View</a>`;
                                }

                                if (PermissionData.find(record => record.ModuleKey === "ViewPOSInvoice").Value == true)
                                {
                                    htmlData += `<a href="/Pos/PosInvoice?ri=${btoa(row["POSInvoiceId"])}" class="btn btn-light border btn-sm ml-2">
                                                        <svg style="color:#6c757d;" class="fs-16 mr-1" xmlns="http://www.w3.org/2000/svg" height="18" width="18" fill="none" viewBox="0 0 23 23" stroke="currentColor">
                                                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z"></path>
                                                        </svg>
                                                Invoice</a>`;
                                }
                               
                                return htmlData;
                            }
                            , "orderable": false, "width": "10%"
                        },
                    ],
                    buttons: [
                        {
                            extend: 'pdf',
                            className: 'btn btn-light border font-weight-medium float-right mb-3',
                            text: '<i class="bb-printer fs-16 mr-2"></i>'+Langaugestore.Print+'',
                        },
                        {
                            extend: 'excel',
                            className: 'btn btn-light border font-weight-medium float-right mb-3 mr-2',
                            text: '<i class="bb-download fs-16 mr-2"></i>'+Langaugestore.ExporttoExcel+'',
                        },
                    ],
                    responsive: true,
                    "lengthMenu": [
                        [5, 15, 20, 40],
                        [5, 15, 20, 40] // change per page values here
                    ],
                    "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
                });

                $('#loader').hide();

            }, error: function (error) {
                $('#loader').hide();
            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#posOrderDatalist")) {
                $('#posOrderDatalist').dataTable().fnDestroy();
                $('#posOrderDatalistdiv').html('<table id="posOrderDatalist" class="table table-card" style="width:100%; display:inherit;"></table>');
            }
            initemployeeDatalist();
        }
    };
}();


////////////////////////View Order Details/////////////////////////////////////

function ViewOrderDetails(pdId)
{
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/pOSDetails/POSDetails_ById?Id=' + atob(pdId) + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        success: function (result) {
            console.log(result);
            debugger;

            $("#posOrderDetails").html("");

            if (result.Code == 200 && result.Item.PosOrderInvoice.length > 0) {
                for (i = 0; i < result.Item.PosOrderInvoice.length; i++) {
                    $('#posOrderDetails').append(`
                          <div class="card_box_profile_card d-flex mt-2 position-relative m-0 w-100 align-items-center" style="border-radius:6px !important;">
                            <div class="img_box">
                                <img onerror="this.src='/Content/assets/images/DefaultProduct.jpg'" src="${APIEndPoint}/${result.Item.PosOrderInvoice[i].ServPhotoUrl}" alt="Card Image" width="100" height="100" class="img-fluid object-cover-h-100 rounded">
                            </div>
                            <div class="content_box_card" style="width:60%;">
                                <div>
                                    <span class="profile_title text-truncate cursor-pointer" title="Hair Massage">
                                        ${result.Item.PosOrderInvoice[i].Name}
                                    </span>
                                    <div class="price text-left fs-14">
                                        Price: ${result.Item.PosOrderInvoice[i].Price == "" || result.Item.PosOrderInvoice[i].Price == null ? "0.00" : result.Item.PosOrderInvoice[i].Price.toFixed(2)} SAR
                                    </div>
                                    <div class="text-left fs-14 text-muted">
                                     <i class="bb-user text-gray-600 fs-16 mr-1"></i>
                                     ${result.Item.PosOrderInvoice[i].AssignUserName == "" || result.Item.PosOrderInvoice[i].AssignUserName == null ? "" : result.Item.PosOrderInvoice[i].AssignUserName}
                                    </div>
                                </div>
                            </div>
                        </div>
                    `);
                }

                $("#posOrderDetailsModal").modal("show");
            }
            else {
                $('#posOrderDetails').append(`
                    <div class="w-100 text-center">
                        <h5 class="cursor-pointer text-muted mb-1">Services not found</h5>
                    </div>
                `);
            }
        },
        error: function (error) {
            // Error function
        }
    });
}