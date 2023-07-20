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


//Package Category in POS
function PackagesListPOS() {
    $('#Salonservice').removeClass('active');
    $('#Salonoffer').removeClass('active');
    $("#Salonpackage").addClass('active');
    $('#SalonCategory').hide();
    $('#OfferCategory').hide();
    $('#salonServicesList').hide();
    $('#salonOffersList').hide();
    $('#PackagesCategory').show();
    $('#salonPackagesList').show();
   
    /*SalonPackageListPOS(0);*/
    let packagesListPOS = new Object();
    packagesListPOS.IsPageProvided = true;
    
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/servicePackage/ServicePackage_All?search&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(packagesListPOS),
        crossDomain: true,
        async: false,
        success: function (result) {

            console.log("result", result);

            $("#PackagesCategory").html(``);
            //$('#PackagesCategory').append(`<a type="button"  id="ServiePackageId_0"  class="filter_link active remact" id="CatId_0" onclick="SalonPackageListPOS(0)" data-filter="all">${Langaugestore.All}</a>`);
           
            for (i = 0; i < result.Values.length; i++) {
                $('#PackagesCategory').append(`
                    <a href="javascript:void(0)" id="ServiePackageId_${result.Values[i].Id}" class="filter_link remact cursor-pointer" id="CatId_${result.Values[i].Id}" onclick="SalonPackageListPOS(${result.Values[i].Id} , ${result.Values[i].CustomPrice})" data-filter="services">${result.Values[i].PackageName}</a>
                `);

                //$('#PackagesCategory').append(`
                //    <div class="package-price-parent">
                //        <a href="javascript:void(0)" id="ServiePackageId_${result.Values[i].Id}" class="filter_link remact cursor-pointer" id="CatId_${result.Values[i].Id}" onclick="SalonPackageListPOS(${result.Values[i].Id} , ${result.Values[i].CustomPrice})" data-filter="services">${result.Values[i].PackageName}</a>
                //        <p class="mb-0 package-price text-center" style="width: 93%;"><b class="text-muted">SAR </b> <span>${result.Values[i].CustomPrice}</span></p>
                //    </div>
                //`);
            }
        }, error: function (error) {
            // Error function

        }
    });
    return false;
}

//employeeList API call in ajax method
 
var PackagesList = function () {

    $('#loader').show();
    let initPackagesList = function () {

        var packagesList = new Object();
        packagesList.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint +`/api/servicePackage/ServicePackage_All?search&SalonId=${atob(SalonId)}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(packagesList),
            crossDomain: true,
            success: function (Values) {
                console.log(Values);
                 
                $('#packagesDatalist').DataTable({
                    data: Values.Values,
                    columns: [
                        {
                            "title": "" + Langaugestore.PACKAGENAME + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<b>${ row["PackageName"] == "" || row["PackageName"] == null ? '-' : row["PackageName"] }</b>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "4%"
                        },
                        {
                            "title": ""+Langaugestore.SERVICELIST+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<a href="javascript:void()" onclick="servicelistdetails(${row["Id"]});" style="cursor: pointer;"  class="link ml-2" > ${Langaugestore.SERVICELIST}</a >`
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.TOTALDURATION+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["TotalDuration"] == "" || row["TotalDuration"] == null ? '0' : row["TotalDuration"] + ' Min'}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Price+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["CustomPrice"] == "" || row["CustomPrice"] == null ? '0' : 'SAR ' + row["CustomPrice"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        //{
                        //    "title": "Daily Deal Offer Price", "data": "",
                        //    "render": function (data, type, row) {
                        //        let htmlData = "";
                        //        htmlData = `${row["DailyDealsOfferPrice"] == "" || row["DailyDealsOfferPrice"] == null ? '0' : '$' + row["DailyDealsOfferPrice"]}`;
                        //        return htmlData;
                        //    }
                        //    , "orderable": false, "width": "3%"
                        //},
                        {
                            "title": ""+Langaugestore.Status+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["IsActive"] == 1 ? `<div class="badge bg-soft-success text-success border p-2" style="cursor:pointer;">${Langaugestore.Active}</div>` : ''}`;
                                htmlData += `${row["IsActive"] == 0 ? `<div class="badge bg-soft-danger text-danger border p-2" style="cursor:pointer;">${Langaugestore.In_active}</div>` : ''}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Actions+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${PermissionData.find(record => record.ModuleKey === "EditPackage").Value == true ?
                                        `<a class="btn btn-light btn-sm border" role="button" href="/Packages/PackagesDetails?ServicePackageId=${btoa(row["Id"])}"><i class="bb-edit-3 text-gray-600 fs-16 mr-2"></i>${Langaugestore.EDIT}</a>` : "-"}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                    ],
                    buttons: [
                        {
                            className: `btn btn-primary float-left mb-3 ${PermissionData.find(record => record.ModuleKey === "CreateNewPackage").Value == true ? "d-block" : "d-none"}`,
                            text: '<i class="bb-plus fs-16 mr-1"></i> '+Langaugestore.Create_New_Package+'',
                            action: function (e, dt, node, config) {
                                window.location = '/Packages/PackagesDetails';
                            }
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
            if ($.fn.DataTable.isDataTable("#packagesDatalist")) {
                $('#packagesDatalist').dataTable().fnDestroy();
                $('#packagesDatalisdiv').html('<table id="packagesDatalist" class="table table-card" style="width:100%; display:inherit;"></table>');
            }
            initPackagesList();
        }
    };
}();

//SalonPackagesList POS
var packageService = [];
var totalPackageService = 0;
function SalonPackageListPOS(ServiePackageId , PackagePrice) {
    debugger;
    $('.remact').removeClass('active');
    $("#ServiePackageId_" + ServiePackageId + "").addClass('active');

    let salonPackageListPOS = new Object();
    salonPackageListPOS.IsPageProvided = true;
    $.ajax({
        processing: true,
        serverSide: true,
        type: 'POST',
        url: APIEndPoint + `/api/masterServicePackage/MasterServicePackage_All?search=&ServicePackageId=${ServiePackageId}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(salonPackageListPOS),
        crossDomain: true,
        success: function (result) {
            $("#salonPackagesList").html(``);
            if (result.Values.length <= 0) {
                $('#salonPackagesList').append(`
                    <a class="filter_link remact" href="#" data-filter="services">No Record Found</a>
                `);
            }
            //LookUpServicesId to id replace
            totalPackageService = result.Values.length;
            for (i = 0; i < result.Values.length; i++) {
                $("#salonPackagesList").append(`
                    <li class="cd-item services">
                        <label for="packageService_2_${result.Values[i].Id}" class="image-checkbox" id="packageServiceCheckbox_2_${result.Values[i].Id}">
                          <img onerror="this.src='/Content/assets/images/DefaultProduct.jpg'" src='${APIEndPoint}/${result.Values[i].ServicePhoto}' style="width:180px; height:180px;object-fit:cover;" alt="" class="img-fluid">
                            <div class="cd-trigger ">
                                <small class="d-block">Price: SAR ${result.Values[i].PackageCustomPrices == 0 ? result.Values[i].LookUpServicesPrice : result.Values[i].PackageCustomPrices}</small>
                                            
                                ${result.Values[i].ServiceName}
                            </div>
                            <input Id="packageService_2_${result.Values[i].Id}" type="checkbox" class="package_service_${result.Values[i].ServicePackageId}" onclick="serviceCheck('${APIEndPoint + "/" + result.Values[i].ServicePhoto}',2,this, ${result.Values[i].Id} , '${result.Values[i].ServiceName}' , ${result.Values[i].PackageCustomPrices == 0 ? result.Values[i].LookUpServicesPrice : result.Values[i].PackageCustomPrices})" />
                            <img src="/Content/assets/images/checked.png" alt="check" class="checked_">
                            <img src="/Content/assets/images/unchecked.png" alt="check" class="unchecked">
                        </label>
                    </li>
                `);
            }

            //pos allready added service checked checkbox
            CalculatePaymentAmount(PackagePrice,true,0 , 2);
            checkedAllreadyService(2);
        }
    });
}


//function addOrder(packageService) {
//    console.log("packageService", packageService);
//    debugger;
//    for (var i = 0; i < packageService.length; i++) {
//        PosPackageOrderSession (
//            packageService[i].ServiceImageUrl,
//            packageService[i].PosType,
//            packageService[i].ServiceId,
//            packageService[i].LookUpServicesName,
//            packageService[i].Price
//        );
//    }

//}

//datatable for SalonPackagesList POS
//function PakageslistdetailsPOS(ServiePackageId) {
//    $('#CatId_0').removeClass('active');
//    $("#CatId").addClass('active');


//    $('#salonPackagesList').html(``);
//    let packagesserviceList = new Object();
//    packagesserviceList.IsPageProvided = true;

//    var ServiePackageId = ServiePackageId

//        $.ajax({
//            processing: true,
//            serverSide: true,
//            type: 'POST',
//            url: APIEndPoint + `/api/masterServicePackage/MasterServicePackage_All?search=&ServicePackageId=${ServiePackageId}`,
//            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
//            dataType: 'json',
//            data: JSON.stringify(packagesserviceList),
//            crossDomain: true,
//            success: function (result) {

//                // Assign To Appoinment list data append
//                if (result.Values.length<= 0) {
//                    $('#salonPackagesList').html(`
//                    <tr>
//                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
//                    </tr>
//                `)
//                } else {
//                    $("#salonPackagesList").html(`
//                        <thead>
//                            <tr>
//                                <th scope="col">#</th>
//                                <th scope="col">${Langaugestore.ServiceName}</th>
//                                <th scope="col">${Langaugestore.ServiceCategory}</th>
//                                <th scope="col">${Langaugestore.Price}</th>
//                                <th scope="col">${Langaugestore.Duration}</th>
//                            </tr>
//                        </thead>
//                    `);
//                }

//                for (i = 0; i < result.Values.length; i++) {
//                    $('#salonPackagesList').append(`
//                        <tbody>
//                            <tr>
//                                <th scope="row">${result.Values[i].Id}</th>
//                                <td>${result.Values[i].ServiceName}</td>
//                                <td>${result.Values[i].CategoryName}</td>
//                                <td>SAR ${result.Values[i].PackageCustomPrices == 0 ? result.Values[i].LookUpServicesPrice : result.Values[i].PackageCustomPrices}</td >
//                                <td>${result.Values[i].LookUpServicesDuration} Min</td>
//                            </tr>
//                        </tbody>
//                    `);
//                }

//            }, error: function (error) {

//            }
//        });
//        return false;

//}


function servicelistdetails(ServiePackageId) {
    $('#serviceModal').modal('show');

    $('#servicepackagedetails').html(``);
    let packagesserviceList = new Object();
    packagesserviceList.IsPageProvided = true;

    var ServiePackageId = ServiePackageId

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/masterServicePackage/MasterServicePackage_All?search=&ServicePackageId=${ServiePackageId}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(packagesserviceList),
            crossDomain: true,
            success: function (result) {

                // Assign To Appoinment list data append
                if (result.Values.length<= 0) {
                    $('#servicepackagedetails').html(`
                    <tr>
                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
                    </tr>
                `)
                } else {
                    $("#servicepackagedetails").html(`
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">${Langaugestore.ServiceName}</th>
                                <th scope="col">${Langaugestore.ServiceCategory}</th>
                                <th scope="col">${Langaugestore.Price}</th>
                                <th scope="col">${Langaugestore.Duration}</th>
                            </tr>
                        </thead>
                    `);
                }

                for (i = 0; i < result.Values.length; i++) {
                    $('#servicepackagedetails').append(`
                        <tbody>
                            <tr>
                                <th scope="row">${result.Values[i].Id}</th>
                                <td>${result.Values[i].ServiceName}</td>
                                <td>${result.Values[i].CategoryName}</td>
                                <td>SAR ${result.Values[i].PackageCustomPrices == 0 ? result.Values[i].LookUpServicesPrice : result.Values[i].PackageCustomPrices}</td >
                                <td>${result.Values[i].LookUpServicesDuration} Min</td>
                            </tr>
                        </tbody>
                    `);
                }

                $('#userServicestableloader').hide();
                $("#employeeProfileloader").hide();
                $('#employeeWorksheetloader').hide();
                $('#AssignToAppointmentslistloader').hide();

            }, error: function (error) {
                $("#employeeProfileloader").hide();
                $('#employeeWorksheetloader').hide();
                $('#userServicestableloader').hide();
                $('#AssignToAppointmentslistloader').hide();
            }
        });
        return false;

}
