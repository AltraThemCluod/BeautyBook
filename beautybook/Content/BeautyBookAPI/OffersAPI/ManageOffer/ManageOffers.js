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


//Offer Category in POS

function OfferListPOS() {
    $('#Salonpackage').removeClass('active');
    $('#Salonservice').removeClass('active');
    $("#Salonoffer").addClass('active');
    $('#SalonCategory').hide();
    $('#PackagesCategory').hide();
    $('#salonServicesList').hide();
    $('#salonPackagesList').hide();
    $('#OfferCategory').show();
    $('#salonOffersList').show();
   
    SalonOffersListPOS(0);
    let offerListPOS = new Object();
    offerListPOS.IsPageProvided = true;
   
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/offer/Offer_All?search=&CreatedBy=0&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(offerListPOS),
        crossDomain: true,
        async: false,
        success: function (result) {
            $("#OfferCategory").html("");
            $('#OfferCategory').append(`<a href="javascript:void(0)" class="filter_link remact active cursor-pointer" id="OfferCatId_0" onclick="SalonOffersListPOS(0)" data-filter="all">${Langaugestore.All}</a>`);
            
            for (i = 0; i < result.Values.length; i++) {
                $('#OfferCategory').append(`
                    <a href="javascript:void(0)" class="filter_link remact cursor-pointer" id="OfferCatId_${result.Values[i].Id}" onclick="SalonOffersListPOS(${result.Values[i].Id})"  data-filter="offers">${result.Values[i].OfferName}</a>
                `);
            }
        }, error: function (error) {
            // Error function

        }
    });
    return false;
}

var getOfferid = new URLSearchParams(window.location.search);
getOfferid = parseInt(atob(getOfferid.get('Id')));
var getOfferidatob = ~~getOfferid;

function getOfferdataPOS() {
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/offer/Offer_ById?Id=' + getOfferidatob + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            if (result.Item != null && result.Item != "")
            {
                for (i = 0; i < result.Item.MasterOfferPrice.length; i++) {
                    $('#offerpricePOS' + result.Item.MasterOfferPrice[i].LookUpServicesId).val(result.Item.MasterOfferPrice[i].OfferPrice == 0 ? '' : result.Item.MasterOfferPrice[i].OfferPrice);
                }
            }
        }, error: function (error) {
        }
    });
}

//OfferList API call in ajax method
var OfferList = function () {
    $('#loader').show();
    let initOfferList = function () {

        var offerList = new Object();
        offerList.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/offer/Offer_All?search=&CreatedBy=0&SalonId=${atob(SalonId)}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(offerList),
            crossDomain: true,
            success: function (Values) {
                console.log(Values);
                
                $('#offerList').DataTable({
                    data: Values.Values,
                    columns: [
                        {
                            "title": ""+Langaugestore.OfferName+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["OfferName"] == "" || row["OfferName"] == null ? '-' : `<b>${row["OfferName"]}</b>`}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Start_Date+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["OfferPeriodStart"] == "" || row["OfferPeriodStart"] == null ? '-' : row["OfferPeriodStart"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.End_Date+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["OfferPeriodEnd"] == "" || row["OfferPeriodEnd"] == null ? '-' : row["OfferPeriodEnd"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.CREATEDBY+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData=`
                                <a href="javascript:void()" style="cursor:pointer;" data-toggle="modal" data-target="#employeeModal"  class="link ml-2" > ${row["FullName"]}</a>`
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Actions+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `
                                           <td>
                                                ${PermissionData.find(record => record.ModuleKey === "EditOffer").Value == true ?
                                                `<a href="/Offers/OfferDetails?Id=${btoa(row["Id"])}" style="cursor:pointer;"class="btn btn-light border btn-sm"><i class="bb-edit-3 text-gray-600 fs-16 mr-2"></i>Edit</a>` : ""}
                                                ${PermissionData.find(record => record.ModuleKey === "DeleteOffer").Value == true ?
                                                `<a href="javascript:void(0)" onclick="offerDelete(${row["Id"]})" class="btn btn-danger btn-sm"><i class="bb-trash-2 text-white fs-16 mr-2"></i>Delete</a>` : ""}
                                          </td> 
                                          `;
                                return htmlData;
                            }
                            , "orderable": false, "width": "4%"
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
            if ($.fn.DataTable.isDataTable("#offerList")) {
                $('#offerList').dataTable().fnDestroy();
                $('#offerListdiv').html('<table id="offerList" class="table table-card" style="width:100%; display:inherit;"></table >');
            }
            initOfferList();
        }
    };
}();

//SalonOffersList POS
function SalonOffersListPOS(CatId) {
    debugger;
    $('.remact').removeClass('active');
    $("#OfferCatId_" + CatId + "").addClass('active');

    let salonOffersListPOS = new Object();
    salonOffersListPOS.IsPageProvided = true;
    $.ajax({
        processing: true,
        serverSide: true,
        type: 'POST',
        url: APIEndPoint + `/api/pOSDetails/PosOffer_All?SalonId=${atob(SalonId)}&OfferId=${CatId}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(salonOffersListPOS),
        crossDomain: true,
        success: function (result) {
            console.log(result);
            $("#salonOffersList").html(``);

            if (result.Values.length <= 0) {
                $('#salonOffersList').append(`
                    <a class="filter_link remact" href="#" data-filter="services">No Record Found</a>
                `);
            }

            for (i = 0; i < result.Values.length; i++) {
                $("#salonOffersList").append(`
                    <li class="cd-item services">
                        <label for="offerService_3_${result.Values[i].LookUpServicesId}" class="image-checkbox" id="offerServiceCheckbox_3_${result.Values[i].LookUpServicesId}">
                          <img onerror="this.src='/Content/assets/images/DefaultProduct.jpg'" src='${APIEndPoint}/${result.Values[i].ServicePhoto}' style="width:180px; height:180px;object-fit:cover;" alt="" class="img-fluid">
                            <div class="cd-trigger ">
                                <small class="d-block">Price: SAR ${result.Values[i].OfferPrice} </small>
                                ${result.Values[i].ServiceName}
                            </div>
                            <input type="checkbox" id="offerService_3_${result.Values[i].LookUpServicesId}" onclick="serviceCheck('${APIEndPoint + "/" + result.Values[i].ServicePhoto}', 3,this, ${result.Values[i].LookUpServicesId},'${result.Values[i].ServiceName}' , ${result.Values[i].OfferPrice})" />
                            <img src="/Content/assets/images/checked.png" alt="check" class="checked_">
                            <img src="/Content/assets/images/unchecked.png" alt="check" class="unchecked">
                        </label>
                    </li>
                `)
            }
            getOfferdataPOS();


            //pos allready added service checked checkbox
            checkedAllreadyService(3);
        }
    });
}

// datatable SalonOffersList POS
//function OfferslistdetailsPOS(callbackfn) {
    

//    $('#salonOffersList').html(``);
//    var SalonServicesData = new Object();
//    SalonServicesData.IsPageProvided = true;

//    $.ajax({
//        type: 'POST',
//        url: APIEndPoint + `/api/lookUpServices/LookUpServices_AllServices?SalonId=${atob(SalonId)}&PackageId=0`,
//        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
//        dataType: 'json',
//        data: JSON.stringify(SalonServicesData),
//        crossDomain: true,
//        async: false,
//        success: function (result) {

            

//            $("#salonOffersList").html(`
//                <thead>
//                    <tr>
//                        <th scope="col" class="bg-primary text-white w-lg-60">${Langaugestore.Service_Name}</th>
//                        <th scope="col" class="bg-primary text-white">${Langaugestore.Original_Price}</th>
//                        <th scope="col" class="bg-primary text-white">${Langaugestore.Price}</th>
//                    </tr>
//                </thead>
//            `);

//            if (result.Values.length <= 0) {
//                $('#salonOffersList').html(`
//                    <tr>
//                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
//                    </tr>
//                `)
//            } else {
//                $("#salonOffersList").html(``);
//            }

//            for (i = 0; i < result.Values.length; i++) {
//                var abc = (result.Values[i].ParentId)
//                if (abc == 0) {
//                    $('#salonOffersList').append(`
//                        <tbody>
//                            <tr>
//                                <th scope="row" class="bg-gray-100" colspan="3">
//                                    <label class="m-0" for="${result.Values[i].Id}">${result.Values[i].Name}</label>   
//                                </th>
//                            </tr>
//                        </tbody>
//                    `);
//                }
//                if (abc = result.Values[i].ParentId) {
//                    $('#salonserviceslist').append(`
//                        <tbody>
//                            <tr>
//                                <th scope="row" class="font-weight-normal">
//                                    <label class="mb-0 ml-5" for="${result.Values[i].Id}">${result.Values[i].Name}</label>
//                                </th>
//                                <td data-price-id="${result.Values[i].Id}">SAR ${result.Values[i].Price}</td>
//                                <td><input data-service="${result.Values[i].Id}" type="text" id="offerprice${result.Values[i].Id}" class="form-control selectedvalue" placeholder="00"></td>
//                            </tr>
//                        </tbody>
//                    `);
//                }
//            }

//            callbackfn();

//        }, error: function (error) {
//            // Error function
//        }
//    });
//    return false;
//}

function getsalonservicedata(callbackfn) {
    $('#loader').show();
    $("#ActiveInActivediv").hide();
    $("#breadcrumbeditpackage").hide();
    $("#headreditpackage").hide();
    $("#editpackagediv").hide();
    $("#editpackagediv").hide();


    var SalonServicesData = new Object();
    SalonServicesData.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpServices/LookUpServices_AllServices?SalonId=${atob(SalonId)}&PackageId=0`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(SalonServicesData),
        crossDomain: true,
        async: false,
        success: function (result) {

            $('#loader').hide();

            $("#salonserviceslist").html(`
                <thead>
                    <tr>
                        <th scope="col" class="bg-primary text-white w-lg-60">${Langaugestore.Service_Name}</th>
                        <th scope="col" class="bg-primary text-white">${Langaugestore.Original_Price}</th>
                        <th scope="col" class="bg-primary text-white">${Langaugestore.Price}</th>
                    </tr>
                </thead>
            `);

            if (result.Values.length <= 0) {
                $('#PackagesGrid').html(`
                    <tr>
                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
                    </tr>
                `)
            } else {
                $("#PackagesGrid").html(``);
            }

            for (i = 0; i < result.Values.length; i++) {
                var abc = (result.Values[i].ParentId)
                if (abc == 0) {
                    $('#salonserviceslist').append(`
                        <tbody>
                            <tr>
                                <th scope="row" class="bg-gray-100" colspan="3">
                                    <label class="m-0" for="${result.Values[i].Id}">${result.Values[i].Name}</label>   
                                </th>
                            </tr>
                        </tbody>
                    `);
                }
                if (abc = result.Values[i].ParentId) {
                    $('#salonserviceslist').append(`
                        <tbody>
                            <tr>
                                <th scope="row" class="font-weight-normal">
                                    <label class="mb-0 ml-5" for="${result.Values[i].Id}">${result.Values[i].Name}</label>
                                </th>
                                <td data-price-id="${result.Values[i].Id}">SAR ${result.Values[i].Price}</td>
                                <td><input data-service="${result.Values[i].Id}" type="text" id="offerprice${result.Values[i].Id}" class="form-control selectedvalue" placeholder="00"></td>
                            </tr>
                        </tbody>
                    `);
                }
            }

            callbackfn();

        }, error: function (error) {
            // Error function
        }
    });
    return false;
}


function copy(e) {

    var OfferPrice = $('#offerprice').val();
        if (OfferPrice == "") {
            alert('Please enter offer price first!');
            e.preventDefault(); // Stops checkbox from becoming checked.
        }
}

//Package offer price creat 
function packageOfferPrice() {
     
    var PackageOfferPrice = new Object();
    PackageOfferPrice.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/servicePackage/ServicePackage_All?search&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(PackageOfferPrice),
        crossDomain: true,
        async:false,
        success: function (result) {
             
            console.log('result', result);

            if (result.Values.length <= 0) {
                $('#PackagesGrid').html(`
                    <tr>
                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
                    </tr>
                `)
            } else {
                $("#PackagesGrid").html(``);
            }

            for (i = 0; i < result.Values.length; i++) {
                $('#PackagesGrid').append(`
                    <tr>
                        <th scope="row" class="font-weight-normal">${result.Values[i].PackageName}</th>
                        <td>SAR ${result.Values[i].TotalPrice}</td>
                        <td><input data-service="${result.Values[i].Id}" type="text" id="PackageOffer${result.Values[i].Id}" class="form-control PackageOffersPrice" placeholder="00"></td>
                    </tr>
                `);
            }

        }, error: function (error) {
            // Error function
        }
    });
    return false;
}