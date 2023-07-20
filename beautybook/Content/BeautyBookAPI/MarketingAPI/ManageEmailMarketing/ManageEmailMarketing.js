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

//setCookie
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

var DeviceTokenNumber = getCookie("DeviceToken&Type");
var SalonId = getCookie("SalonId");

//EmailMarketingList API call in ajax method
var EmailMarketingList = function () {
    $('#loader').show();
    let initEmailMarketingList = function () {

        var emailMarketingList = new Object();
        emailMarketingList.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/emailMarketing/EmailMarketing_All?search=&CreatedBy=0&SalonId=${atob(SalonId)}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(emailMarketingList),
            crossDomain: true,
            success: function (Values) {
                console.log(Values);
                 
                $('#emailMarketinglist').DataTable({
                    data: Values.Values,
                    columns: [
                        {
                            "title": ""+Langaugestore.Date+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["CreatedDateStr"] == "" || row["CreatedDateStr"] == null ? '-' : `<b>${row["CreatedDateStr"]}</b>`}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.SUBJECT+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["EmailSubject"] == "" || row["EmailSubject"] == null ? '-' : row["EmailSubject"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.CREATEDBY+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `
                                <a data-toggle="modal" href="javascript:void()" onclick="" class="link">${row["UserFullName"]}</a>
                                    `
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Status+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                if (row["IsSendEmail"] == true) {
                                    htmlData = `<div class="badge bg-soft-success text-success border p-2">Send</div>`
                                } else {
                                    htmlData = `<div class="badge bg-soft-light text-dark border p-2">Draf</div>`
                                }

                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.EDIT+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${PermissionData.find(record => record.ModuleKey === "EditEmail").Value == true ?
                                            `<a class="btn btn-light border btn-sm" href="/Marketing/EmailTemplateEditor?Id=${btoa(row["Id"])}">
                                                    <i class="bb-edit-3 text-gray-600 fs-16 mr-2"></i>${Langaugestore.EDIT}
                                            </a>` : ""}`;
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
            if ($.fn.DataTable.isDataTable("#emailMarketingList")) {
                $('#emailMarketingList').dataTable().fnDestroy();
                $('#emailMarketingListdiv').html('<table id="emailMarketingList" class="table table-card" style="width:100%; display:inherit;"></table >');
            }
            initEmailMarketingList();
        }
    };
}();
function myEmailPakagesPlanlist() {
    $('#EmailPackagesLoader').show();

    var EmailPackgesPlanlist = new Object();
    EmailPackgesPlanlist.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + `/api/selectPlan_EmailPackages/SelectPlan_EmailPackages_BySalonId?search=&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(EmailPackgesPlanlist),
        crossDomain: true,
        success: function (result) {
            debugger;
            //
            if (result.Values.length <= 0) {
                $("#CreateEmailMarketing_Btn").hide();
            }
            setCookie("IsPackages", result.Values.length, 30);
            if (result.Values == 0) {
                $("#showpackages").html(`
                    <h4 class="text-white mb-0">${Langaugestore.NoPackageSelected}</h4>
                    <a href = "/Email_Sms_Package/Index" class= "btn btn-warning font-weight-semibold mt-5">${Langaugestore.SelectPlan}</a>
                `);
            }
            for (var i = 0; i < result.Values.length; i++) {
                $('#showpackages').append(`
                    <h4 class="text-white mb-">${result.Values[i].EmailMsgPackagesName}</h4>
                    <p class="text-white mb-0"><span class="font-weight-medium">${result.Values[i].CompleteNoOfMsg}</span> / <span class="text-light">${result.Values[i].NoOfMessages == 0 ? 'Unlimited' : result.Values[i].NoOfMessages}</span> ${Langaugestore.Messages}</p>
                    <p class="text-white mb-0 mt-2">Your plan will expire in <b>${result.Values[i].ExpiredPlanDay}</b> days</p>
                `);
            }
            $('#EmailPackagesLoader').hide();
        }, error: function (error) {    
            $('#EmailPackagesLoader').hide();
        }
    });
    return false;
}

//
