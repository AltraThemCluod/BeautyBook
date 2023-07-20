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


//posSessionList API call in ajax method
var PosSessionList = function () {

    $('#loader').show();

    let initemployeeDatalist = function () {
        var posSessionList = new Object();
        posSessionList.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/pOSSession/POSSession_BySalonId?search&SalonId=${atob(SalonId)}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(posSessionList),
            crossDomain: true,
            //async: false,
            success: function (Values) {
                $('#posSessionDatalist').DataTable({
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
                            "title": "Session Handler", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["SessionHandlerUserName"] == "" || row["SessionHandlerUserName"] == null ? "-" : row["SessionHandlerUserName"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "10%"
                        },
                        {
                            "title": "Closing Cash Amount", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["LastClosingCash"] == "" || row["LastClosingCash"] == null ? "-" : "SAR" + " " + row["LastClosingCash"].toFixed(2)}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "10%"
                        },
                        {
                            "title": "Closing Online Amount", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["LastClosingOnline"] == "" || row["LastClosingOnline"] == null ? "-" : "SAR" + " " + row["LastClosingOnline"].toFixed(2)}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "10%"
                        },
                        {
                            "title": "Last Closing Date", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["LastClosingAtStr"] == "" || row["LastClosingAtStr"] == null ? "-" : row["LastClosingAtStr"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "10%"
                        },
                        {
                            "title": "Created Session", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["CreatedSessionAtStr"] == "" || row["CreatedSessionAtStr"] == null ? "-" : row["CreatedSessionAtStr"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "10%"
                        },
                        {
                            "title": "Total OrderSales", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";

                                if (PermissionData.find(record => record.ModuleKey === "POSViewSales").Value == true) {
                                    htmlData += `<a href="/Pos/Orders?ri=${btoa(row["Id"])}" class="btn btn-light border btn-sm"><b class="mr-1">${(row["TotalOrderSales"])}</b> View Sales</a>`;
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

                $('#loader').hide();

            }, error: function (error) {
                $('#loader').hide();
            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#posSessionDatalist")) {
                $('#posSessionDatalist').dataTable().fnDestroy();
                $('#posSessionDatalistdiv').html('<table id="posSessionDatalist" class="table table-card" style="width:100%; display:inherit;"></table>');
            }
            initemployeeDatalist();
        }
    };
}();


