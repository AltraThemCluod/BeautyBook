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

//SalonServicesSearch function
function TaxInvoiceSearch() {
    $('#Searchbtn').hide();
    $('#Searchbtnloading').show();
    TaxInvoiceList.init();
}

//ResetServiceData function 
function ResetTaxInvoice() {
    $('#ResetDatabtn').hide();
    $('#Resetbtnloading').show();

    $('#InvoiceDate').val("");
    $('#InvoiceNumber').val(null);
    $('#CustomerName').val(0);
    $('.selectpicker').selectpicker("refresh");
    TaxInvoiceList.init();
}

//SalonServicesList API call in ajax method
var TaxInvoiceList = function () {

    let initTaxInvoiceList = function () {
        $('#salonsServiceListingloader').show();

        let TaxInvoiceList = new Object();
        TaxInvoiceList.IsPageProvided = true;

        var ICustomerName = $("#CustomerName").val();
        var IInvoiceNumber = $("#InvoiceNumber").val();
        var IInvoiceDate = $("#InvoiceDate").val();

        debugger;   
        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `api/orders/OrderInvoice_All?UserId=${atob(VendorId)}&InvoiceDate=${IInvoiceDate}&InvoiceNumber=${IInvoiceNumber}&CustomerId=${ICustomerName == "" ? 0 : ICustomerName}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(TaxInvoiceList),
            crossDomain: true,
            success: function (Values) {
                var countNo = 0;
                $('#TaxInvoice').DataTable({
                    data: Values.Values,
                    columns: [
                        {
                            "title": "" + Langaugestore.Invoice_Counter + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";

                                var InvoiceCounter = countNo = countNo + 1;

                                htmlData = `<b>${InvoiceCounter}</b>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.ID + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<b>${row["Id"]}</b>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.Invoice_Number + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `#${row["InvoiceNo"] == "" || row["InvoiceNo"] == null ? '-' : row["InvoiceNo"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.Invoice_Date + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["OrderDate"] == "" && row["OrderDate"] == null ? '-' : row["OrderDate"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.CustomerName + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["CustomerName"] == "" && row["CustomerName"] == null ? '-' : row["CustomerName"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.Entered_By + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["Supplername"] == "" && row["Supplername"] == null ? '-' : row["Supplername"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.Action + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<div class="dropdown">
                                                <button class="btn btn-icon" type="button" data-toggle="dropdown">
                                                    <i class="bb-more-horizontal fs-22"></i>
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right">
                                                    ${row["ParentId"] > 0 ? 
                                                       '': `<a class="dropdown-item" href="javascript:void(0)" onclick="NoticeCreditor('${btoa(row["Id"])}');"><i class="bb-credit-card text-gray-600 fs-16 mr-3"></i>${Langaugestore.Notice_Creditor}</a>`
                                                    }
                                                    <a class="dropdown-item" href="/ManageVendorOrders/InvoiceDetails?OrderId=${btoa(row["Id"])}">
                                                        <svg style="color:#6c757d;" class="fs-16 mr-3" xmlns="http://www.w3.org/2000/svg" height="18" width="18" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z"></path>
                                                        </svg>
                                                        ${Langaugestore.View_Invoice}
                                                    </a>
                                                </div>
                                            </div>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
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

                $('#ResetDatabtn').show();
                $('#Resetbtnloading').hide();
                $('#Searchbtn').show();
                $('#Searchbtnloading').hide();
                $('#ManageListingloader').hide();

            }, error: function (error) {
                $('#ResetDatabtn').show();
                $('#Resetbtnloading').hide();
                $('#Searchbtn').show();
                $('#Searchbtnloading').hide();
                $('#ManageListingloader').hide();
            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#TaxInvoice")) {
                $('#TaxInvoice').dataTable().fnDestroy();
                $('#TaxInvoiceDiv').html('<table id="TaxInvoice" class="table table-card" style="width:100%; display:inherit;"></table >');
            }
            initTaxInvoiceList();
        }
    };
}();



//Customer list
function InvoiceCustomers() {
    $("#appoinmentAssignedUser").html(``);

    let invoiceCustomers = new Object();
    invoiceCustomers.IsPageProvided = true;
    debugger;
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `api/orders/OrderInvoiceSalon_All?UserId=${atob(VendorId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(invoiceCustomers),
        crossDomain: true,
        success: function (result) {
            console.log('result', result);
            if (result.Values.length > 0) {
                for (var i = 0; i < result.Values.length; i++) {
                    $("#CustomerName").append(`<option value="${result.Values[i].CustomerId}">${result.Values[i].CustomerName}</option>`)
                }
            }
            $('.selectpicker').selectpicker("refresh");
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}