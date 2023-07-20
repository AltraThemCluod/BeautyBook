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

//customerList API call in ajax method
var SmsMarketingList = function () {
    $('#loader').show();
    let initSmsMarketingList = function () {

        var smsMarketingList = new Object();
        smsMarketingList.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/smsMarketing/SmsMarketing_All?search=&CreatedBy=0`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(smsMarketingList),
            crossDomain: true,
            success: function (Values) {
                console.log(Values);
                 
                $('#smsMarketinglist').DataTable({
                    data: Values.Values,
                    columns: [
                        {
                            "title": "Date", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["CreatedDateStr"] == "" || row["CreatedDateStr"] == null ? '-' : `<b>${row["CreatedDateStr"]}</b>`}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "Subject", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["SmsSubject"] == "" || row["SmsSubject"] == null ? '-' : row["SmsSubject"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "Created By", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData =`
                                <a data-toggle="modal" href="javascript:void()"  data-target="#employeeModal"  class="link ml-2">${row["UserFullName"]}</a>
                                     `
                                return htmlData;
                            }
                            , "orderable": false, "width": "2%"
                        },
                        {
                            "title": "Edit", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<a class="btn btn-light border btn-sm" href="/Marketing/SmsMarketingDetails?Id=${btoa(row["Id"])}">
                                                    <i class="bb-edit-3 text-gray-600 fs-16 mr-2"></i>${Langaugestore.EDIT}
                                            </a>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "2%"
                        },
                    ],
                    buttons: [
                    ],
                    responsive: true,
                    "lengthMenu": [
                        [5, 15, 20, 40],
                        [5, 15, 20, 40] // change per page values here
                    ],
                    "dom": "<'row' <'col-md-12'B>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>", // horizobtal scrollable datatable
                });

                $('#customerReset').show();
                $('#customerResetloading').hide();
                $('#customerSearch').show();
                $('#customerSearchloading').hide();
                $('#loader').hide();
            }, error: function (error) {
                $('#customerReset').show();
                $('#customerResetloading').hide();
                $('#customerSearch').show();
                $('#customerSearchloading').hide();
                $('#loader').hide();
            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#smsMarketingList")) {
                $('#smsMarketingList').dataTable().fnDestroy();
                $('#smsMarketingListdiv').html('<table id="smsMarketingList" class="table table-card" style="width:100%; display:inherit;"></table >');
            }
            initSmsMarketingList();
        }
    };
}();

function mySmsPakageplandetails() {
    $('#SMSPackagesLoader').show();

    var SMSPackgesPlanlist = new Object();
    SMSPackgesPlanlist.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url:  APIEndPoint + `/api/selectPlan_SMSPackages/SelectPlan_SMSPackages_BySalonId?search=&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(SMSPackgesPlanlist),
        crossDomain: true,
        success: function (result) {
            if (result.Values == 0) {
                $("#showsmspackages").html(`
             <h4 class="text-white mb-">${Langaugestore.NoPackageSelected}</h4>
                    `);
            }
            for (var i = 0; i < result.Values.length; i++) {
                $('#showsmspackages').append(`
                   <h4 class="text-white mb-">${result.Values[i].SMSPackagesName}</h4>
                    <p class="mb-5 text-white "><span class="font-weight-medium">${result.Values[i].SendSMSCount}</span> / <span class="text-light">${result.Values[i].NoOfMessages}</span> ${Langaugestore.Messages}</p>
                    <a href="/SmsPackage/Index" class="btn btn-warning font-weight-semibold">${Langaugestore.Upgrade_Plan}</a>
                `);
            }
            $('#SMSPackagesLoader').hide();
        }, error: function (error) {
            $('#SMSPackagesLoader').hide();
        }
    });
    return false;
}


