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
var UserId = getCookie("UserId");

//DailyDealsList API call in ajax method
var DailyDealsList = function () {

     
    let initDailyDealsList = function () {
     
        $('#DailyDealsListingloader').show();

        let dailyDealsList = new Object();
        dailyDealsList.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/dailyDeals/DailyDeals_All?search=&SalonId=${atob(SalonId)}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(dailyDealsList),
            crossDomain: true,
            success: function (Values) {
                console.log(Values);
                 
                $('#DailyDealsListing').DataTable({
                    "order": [[0, "desc"]],
                    data: Values.Values,

                    columns: [
                        {
                            "title": ""+Langaugestore.SERVICEPACKAGENAME+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["ServiceName"] == "" || row["ServiceName"] == null ? 
                                    row["PackagesName"]
                                    : row["ServiceName"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": ""+Langaugestore.Type+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["Type"] == 1 ? 'Service' : 'Package'}`;
                                return htmlData; 
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": ""+Langaugestore.OFFERDATE+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["OfferDate"] == "" || row["OfferDate"] == null ? '-' : row["OfferDate"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": ""+Langaugestore.START_TIME+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["StartTime"] == "" || row["StartTime"] == null ? '-' : onTimeChange(row["StartTime"])}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": ""+Langaugestore.END_TIME+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["EndTime"] == "" || row["EndTime"] == null ? '-' : onTimeChange(row["EndTime"])}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": ""+Langaugestore.Original_Price+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["ServicePrice"] == 0.0 || row["ServicePrice"] == null ? 'SAR ' + row["PackagesPrice"] : 'SAR '+row["ServicePrice"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": ""+Langaugestore.Offer_Price+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["OfferPrice"] == 0.0 || row["OfferPrice"] == null ? '-' : 'SAR '+row["OfferPrice"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": ""+Langaugestore.Actions+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${PermissionData.find(record => record.ModuleKey === "EditDailyDeals").Value == true ?
                                            `<a href="#!" onclick="OpenModalDailyDealsEdit('${btoa(row["Id"])}')" class="btn btn-light border btn-sm"><i class="bb-edit-3 text-gray-600 fs-16 mr-2"></i>Edit</a>` : ""}

                                            ${PermissionData.find(record => record.ModuleKey === "DeleteDailyDeals").Value == true ?
                                            `<a href="#!" onclick="ConfirmDailyDealsDelete('${btoa(row["Id"])}')" class="btn btn-danger btn-sm"><i class="bb-trash-2 text-white fs-16 mr-2"></i>Delete</a>` : ""}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                    ],
                    buttons: [
                        //{
                        //    className: 'btn btn-primary float-left mb-3',
                        //    text: '<i class="bb-plus fs-16 mr-1"></i>Add New Service',
                        //    action: function (e, dt, node, config) {
                        //        window.location = '/SalonServices/SalonServiceDetails';
                        //    }
                        //},
                    ],
                    responsive: true,
                    "lengthMenu": [
                        [5, 15, 20, 40],
                        [5, 15, 20, 40] // change per page values here
                    ],
                    "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
                });

                $('#DailyDealsListingloader').hide();

            }, error: function (error) {
                $('#DailyDealsListingloader').hide();
            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#DailyDealsListing")) {
                $('#DailyDealsListing').dataTable().fnDestroy();
                $('#DailyDealsListingdiv').html('<table id="DailyDealsListing" class="table table-card" style="width:100%; display:inherit;"></table >');
            }
            initDailyDealsList();
        }
    };
}();


//Daily Deals data delete

function ConfirmDailyDealsDelete(DailyDealsId) {
    Swal.fire({
        title: '' + Langaugestore.Are_you_sure_you_want_to_delete_this_Daily_Deals__+'',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            DailyDealsDelete(DailyDealsId);
        }
    })
}


function DailyDealsDelete(DailyDealsId) {
     
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + `/api/dailyDeals/DailyDeals_Delete?Id=${atob(DailyDealsId)}&DeletedBy=${atob(UserId)}`,
        //headers: { "Authorization": '' + DeviceTokenNumber + '' },
        crossDomain: true,
        dataType: 'json',
        async: false,
        success: function (result) {
            DailyDealsList.init();
            if (result.Code == 200) {
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