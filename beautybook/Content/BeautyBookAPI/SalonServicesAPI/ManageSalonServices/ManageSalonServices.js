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

//SalonServicesSearch function
function SalonServicesSearch() {
    $('#Searchbtn').hide();
    $('#Searchbtnloading').show();
    SalonServicesList.init();
}

//ResetServiceData function 
function ResetServiceData() {
    $('#ResetServiceDatabtn').hide();
    $('#ResetServiceDatabtnloading').show();

    $('#serviceName').val("");
    $('#serviceStatus').val(null);
    $('#categoryName').val("");
    $('.selectpicker').selectpicker("refresh");
    SalonServicesList.init();
}

//SalonServicesListPOS

function SalonServicesListPOS(CatId) {
    $('.remact').removeClass('active');
    $("#CatId_" + CatId + "").addClass('active');
   
    let salonServicesListPOS = new Object();
    salonServicesListPOS.IsPageProvided = true;
    $.ajax({
        processing: true,
        serverSide: true,
        type: 'POST',
        url: '' + APIEndPoint + '/api/salonServices/SalonServices_All?search&SalonsId=' + atob(SalonId) + '&LookUpServicesId=0&LookUpStatusId=0&LookUpCategoryId=' + CatId + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(salonServicesListPOS),
        crossDomain: true,
        success: function (result) {
            $("#salonServicesList").html(``);
            if (result.Values.length <= 0) {
                $('#salonServicesList').append(`
                    <a class="filter_link remact" href="#" data-filter="services">No Record Found</a>
                `);
            } 
            for (i = 0; i < result.Values.length; i++) {
                $('#salonServicesList').append(`
                  <li class="cd-item services">
                      <label for="salonService_1_${result.Values[i].LookUpServicesId}"  class="image-checkbox" id="salonServiceCheckbox_1_${result.Values[i].LookUpServicesId}">
                          <img onerror="this.src='/Content/assets/images/DefaultProduct.jpg'" src='${APIEndPoint}/${result.Values[i].ServicePhoto}' style="width:180px; height:180px;object-fit:cover;" alt="" class="img-fluid">
                          <div class="cd-trigger ">
                              <small class="d-block">Price: SAR ${result.Values[i].Price}</small>${result.Values[i].LookUpServicesName}
                          </div>
                          <input  Id="salonService_1_${result.Values[i].LookUpServicesId}" type="checkbox" onclick="serviceCheck('${APIEndPoint+"/"+result.Values[i].ServicePhoto}', 1,this, ${result.Values[i].LookUpServicesId} , '${result.Values[i].LookUpServicesName}' , ${result.Values[i].Price})" />
                          <img src="/Content/assets/images/checked.png" alt="check" class="checked_">
                          <img src="/Content/assets/images/unchecked.png" alt="check" class="unchecked">
                      </label>
                  </li>
                `);
            }


            //pos allready added service checked checkbox
            checkedAllreadyService(1);
        }
    });
}
//<img src="${APIEndPoint}/${result.Values[i].ServicePhoto}" class="img-fluid" alt="Service Photo" />


//<img src='~/Content/assets/images/unsplash_e4KElxlRF7w.png' alt="" class="img-fluid">
//SalonServicesList API call in ajax method
var SalonServicesList = function () {


    let initSalonServicesList = function () {
        let LookUpServices = 0, LookUpStatus = 0, LookUpCategory = 0;
        if (parseInt($('#serviceName').val()) > 0) {
            LookUpServices = parseInt($('#serviceName').val());
        }
        if (parseInt($('#serviceStatus').val()) > 0) {
            LookUpStatus = parseInt($('# ').val());
        }
        if (parseInt($('#categoryName').val()) > 0) {
            LookUpCategory = parseInt($('#categoryName').val());
        }

        $('#salonsServiceListingloader').show();

        let salonServicesList = new Object();
        salonServicesList.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: '' + APIEndPoint + '/api/salonServices/SalonServices_All?search&SalonsId=' + atob(SalonId) + '&LookUpServicesId=' + ~~LookUpServices + '&LookUpStatusId=' + ~~LookUpStatus + '&LookUpCategoryId=' + ~~LookUpCategory + '',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(salonServicesList),
            crossDomain: true,
            success: function (Values) {
                console.log(Values);

                $('#salonsServiceListing').DataTable({
                    "order": [[0, "desc"]],
                    data: Values.Values,
                    columns: [
                        {
                            "title": "" + Langaugestore.ServiceName + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<b><a href="javascript:void(0)">${row["LookUpServicesName"] == "" || row["LookUpServicesName"] == null ? '-' : row["LookUpServicesName"]}</a></b>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.ServiceCategory + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["LookUpCategoryName"] == "" || row["LookUpCategoryName"] == null ? '-' : row["LookUpCategoryName"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.Duration + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["MinDuration"] == "" && row["MinDuration"] == null ? '-' : row["MinDuration"] + ' Min'}  - ${row["MaxDuration "] == "" && row["MaxDuration"] == null ? '' : row["MaxDuration"] + 'Min'}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.Price + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `SAR ${row["MinPrice"]} - SAR ${row["MaxPrice"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.Status + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["LookUpStatusId"] == 1 ? `<div class="badge bg-soft-success text-success border p-2" onclick="InActiveSalonService(${row["Id"]});" style="cursor:pointer;">${Langaugestore.Active}</div>` : ''}`;
                                htmlData += `${row["LookUpStatusId"] == 2 ? `<div class="badge bg-soft-danger text-danger border p-2" onclick="ActiveSalonService(${row["Id"]});" style="cursor:pointer;">${Langaugestore.In_active}</div>` : ''}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.Actions + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<div class="dropdown">
                                                <button class="btn btn-icon" type="button" data-toggle="dropdown">
                                                    <i class="bb-more-horizontal fs-22"></i>
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right">
                                                    ${PermissionData.find(record => record.ModuleKey === "EditService").Value == true ?
                                                        `<a class="dropdown-item" href="/SalonServices/SalonServiceDetails?SalonServices=${btoa(row["Id"])}"><i class="bb-edit-3 text-gray-600 fs-16 mr-3"></i>${Langaugestore.EDIT}</a>` : ""}
                                                </div>
                                            </div>`;
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

                $('#ResetServiceDatabtn').show();
                $('#ResetServiceDatabtnloading').hide();
                $('#Searchbtn').show();
                $('#Searchbtnloading').hide();
                $('#salonsServiceListingloader').hide();

            }, error: function (error) {
                $('#ResetServiceDatabtn').show();
                $('#ResetServiceDatabtnloading').hide();
                $('#Searchbtn').show();
                $('#Searchbtnloading').hide();
                $('#salonsServiceListingloader').hide();
            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#salonsServiceListing")) {
                $('#salonsServiceListing').dataTable().fnDestroy();
                $('#salonsServiceListingdiv').html('<table id="salonsServiceListing" class="table table-card" style="width:100%; display:inherit;"></table >');
            }
            initSalonServicesList();
        }
    };
}();

function ActiveSalonService(id) {

    Swal.fire({
        title: 'Are you sure you want to Active this service ?',
        //text: "Are you sure Active this employee !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            ActInActSalonService(id, "Activate !", 1);
        }
    })
}
function InActiveSalonService(id) {
    Swal.fire({
        title: '' + Langaugestore.Are_you_sure_you_want_to_In_Active_this_service__ + '',
        //text: "Are you sure Active this employee !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            ActInActSalonService(id, "In-Active", 2);
        }
    })
}
function ActInActSalonService(id, msg, statusId) {

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/SalonServices/SalonServices_ActInact?Id=' + id + '&LookUpStatusId=' + statusId + '&SalonId=' + atob(SalonId) + '&LookUpStatusChangedBy=' + atob(UserId),
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        processData: false,
        contentType: false,
        crossDomain: true,
        success: function (result) {

            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 2000
                });
                SalonServicesList.init();
            }
        }, error: function (error) {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 2000
            });
        }
    });
}