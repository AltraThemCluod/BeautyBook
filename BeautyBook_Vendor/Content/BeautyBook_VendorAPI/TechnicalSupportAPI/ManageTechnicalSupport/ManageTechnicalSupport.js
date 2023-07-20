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
var VendorId = getCookie("VendorId");

//TechnicalSupportList API call in ajax method
var TechnicalSupportList = function () {

    $('#loader').show();
    
    let initTechnicalSupportList = function () {
        var TechnicalSupportList = new Object();
        TechnicalSupportList.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/technicalSupport/TechnicalSupport_All?UserId=${atob(VendorId)}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(TechnicalSupportList),
            crossDomain: true,
            success: function (Values) {
                console.log(Values);
                
                $('#TechnicalSupportList').DataTable({
                    data: Values.Values,
                    columns: [
                        {
                            "title": "" + Langaugestore.Description +"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["Discription"] == "" || row["Discription"] == null ? '-' : decodeURIComponent(escape(window.atob(row["Discription"])))}`;                                return htmlData;
                            }
                            , "orderable": false, "width": "40%"
                        },
                        {
                            "title": "" + Langaugestore.CreatedDate+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["CreatedDateStr"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "5%"
                        },
                        {
                            "title": "" + Langaugestore.Actions + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<td>
                                                <a href="/TechnicalSupport/TechnicalSupportDetails?Id=${btoa(row["Id"])}" style="cursor:pointer;"class="btn btn-light border btn-sm"><i class="bb-edit-3 text-gray-600 fs-16 mr-2"></i>Edit</a>
                                                <a href="javascript:void(0)" onclick="confirmTicketDeleted(${row["Id"]})" class="btn btn-danger btn-sm"><i class="bb-trash-2 text-white fs-16 mr-2"></i>Delete</a>
                                            </td>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "10%"
                        },
                    ],
                
                    buttons: [
                        {
                            className: 'btn btn-primary float-left mb-3',
                            text: '' + Langaugestore.Create_Support_Ticket+'',
                            action: function (e, dt, node, config) {
                                window.location = '/TechnicalSupport/TechnicalSupportDetails';
                            }
                        },
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
            if ($.fn.DataTable.isDataTable("#TechnicalSupportList")) {
                $('#TechnicalSupportList').dataTable().fnDestroy();
                $('#TechnicalSupportListdiv').html('<table id="TechnicalSupportList" class="table table-card" style="width:100%; display:inherit;"></table>');
            }
            initTechnicalSupportList();
        }
    };
}();



//swal Delete Ticket
function confirmTicketDeleted(ticketId) {
    Swal.fire({
        title: 'Are you sure you want to delete this ticket',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteTicket(ticketId);
        }
    })
}


// Ticket Delete API call in ajax method
function DeleteTicket(ticketId) {

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/technicalSupport/TechnicalSupport_Delete?Id=${(ticketId)}&DeletedBy=${(atob(VendorId))}`,
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        crossDomain: true,
        dataType: 'json',
        async: false,
        success: function (result) {
            if (result.Code == 200) {
                TechnicalSupportList.init();
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
            }
        }, error: function (error) {
            // Error function
        }
    });
}