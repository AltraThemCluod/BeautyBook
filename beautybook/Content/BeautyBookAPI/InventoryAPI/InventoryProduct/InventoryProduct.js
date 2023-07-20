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

 

var getInventoryPrd = new URLSearchParams(window.location.search);
var InventoryProduct = atob(getInventoryPrd.get('InventoryPrd'));
var Type = atob(getInventoryPrd.get('Type'));
var Brand = atob(getInventoryPrd.get('Brand'));
var We = atob(getInventoryPrd.get('We'));
var WTI = atob(getInventoryPrd.get('WTI'));

var inventoryProductList = function () {

    $('#inventoryProductListloader').show();
    
    let initInventorylist = function () {
        debugger;
        let InventoryProductList = new Object();
        InventoryProductList.IsPageProvided = true;

        $.ajax({
            type: 'POST',
            url: APIEndPoint + `/api/inventoryProduct/InventoryProduct_All?search=&ProductName=${InventoryProduct}&ProductTypeId=${Type}&ProductBrandId=${Brand}&Weight=${We}&WeightTypeId=${WTI}&SalonId=${atob(SalonId)}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(InventoryProductList),
            crossDomain: true,
            success: function (Values) {    
                debugger;
                console.log(Values);
                let abc = {};
                for (let i = 0; i < Values.length; i++) {
                    if (Values[i].PurchaseVia == 24) {
                        abc = Values[i];
                        break;
                    }
                }
                console.log(abc);
                //Product Basic details show other div
                if (Values.Values.length == 1) {
                    $('#productBasicDetails').append(`
                        ${Values.Values[0].ProductImage == "" || Values.Values[0].ProductImage == null ?
                            `<img src="../Content/assets/images/DefaultProduct.jpg" class="img-fluid rounded border mr-3" alt="" style="width: 5rem;">` :
                        `<img src="${APIEndPoint}/${Values.Values[0].ProductImage}" class="img-fluid rounded border mr-3" alt="" style="width: 5rem;">`
                        }
                        <div class="">
                            <h5 class="mb-1">${Values.Values[0].ProductName}</h5>
                            <p class="text-muted fs-14 mb-2">${Values.Values[0].ProductTypeName} - ${Values.Values[0].ProductBrandName}</p>
                            <div class="badge badge-success">${Values.Values[0].SumProductQty} ${Langaugestore.InStock}</div>
                        </div>
                    `);
                }
                else if (Values.Values.length > 1) {
                    for (i = 0; i < Values.Values.length; i++) {
                        if (Values.Values[i].PurchaseVia == 24) {
                            var onlineimage = (Values.Values[i].ProductThumbnailImage)
                        }
                    }
                    $('#productBasicDetails').append(`
                        ${Values.Values[0].ProductThumbnailImage == "" || Values.Values[0].ProductThumbnailImage == null ?
                                `<img src="../Content/assets/images/DefaultProduct.jpg" class="img-fluid rounded border mr-3" alt="" style="width: 5rem;">` :
                                `<img src="${APIEndPoint}/${onlineimage}" class="img-fluid rounded border mr-3" alt="" style="width: 5rem;">`
                            }
                        <div class="">
                            <h5 class="mb-1">${Values.Values[0].ProductName}</h5>
                            <p class="text-muted fs-14 mb-2">${Values.Values[0].ProductTypeName} - ${Values.Values[0].ProductBrandName}</p>
                            <div class="badge badge-success">${Values.Values[0].SumProductQty} ${Langaugestore.InStock}</div>
                        </div>
                    `);
                }
                else if (Values.TotalRecords = 0 ) {
                    $('#productBasicDetails').append(`<h5 class="mb-1 text-muted text-center w-100">Product not found</h5>`);
                }
                

                //Product Data table
                $('#inventoryProductList').DataTable({
                    data: Values.Values,
                    columns: [
                        {
                            "title": "" + Langaugestore.Order_ID+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["VendorId"] > 0 ?
                                                `<b>#${row["OrderNo"] == "" || row["OrderNo"] == null ? '-' : row["OrderNo"]}</b>`
                                                :
                                                `<b>#${row["BillNumber"] == "" || row["BillNumber"] == null ? '-' : row["BillNumber"]}</b>`
                                             }`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.Date+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["PurchaseDate"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "Purchse", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["PurchaseVia"] == 25 ?
                                                `<div class="badge bg-soft-secondary text-secondary p-2">Offline</div>` :
                                                `${row["PurchaseVia"] == 24 ? `<div class="badge bg-soft-danger text-danger p-2">Online</div>`:''}`
                                            }`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "Vendor", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["VendorId"] > 0 ? `<a href="/Store/VendorDetails?VendorId=${btoa(row["VendorId"])}" class="link">${row["VendorName"]}</a>`
                                                :
                                    `<a href="javascript:void(0)" onclick="offlineVendor('${btoa(row["OfflineVendorId"])}')" class="link">${row["VendorOfflineName"]}</a>`}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.Weight+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["Weight"]} ${row["ProductWeightTypeName"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "Quatity", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["ProductQty"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": "" + Langaugestore.Price+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `SAR ${row["Price"].toFixed(2)}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.Total+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `SAR ${row["Total"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "Expiry Date", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["ExpiryDate"] == "" || row["ExpiryDate"] == null ? "-" : `${row["IsExpiryDateAfter30Day"] == true ? '<b class="cursor-pointer text-danger"><b><i class="fa fa-exclamation-triangle mr-1 ml-1"></i></b><b title="This product will expire in ' + row["NoOfDay"] + ' days">' + row["ExpiryDate"] + '</b></b>' : '<b><span>' + row["ExpiryDate"] +'</span></b>'}`}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.Actions +"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["PurchaseVia"] == 25 && PermissionData.find(record => record.ModuleKey === "EditProduct").Value == true ?
                                            `<a href="/InventoryProduct/ProductDetails?InventoryPrdIds=${btoa(row["Id"])}" >
                                                <div role="button"><i class="bb-edit-3 text-gray"></i></div>
                                            </a>` : ''}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
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
               
                $('#inventoryProductListloader').hide();

            }, error: function (error) {
                $('#inventoryProductListloader').hide();
            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#inventoryProductList")) {
                $('#inventoryProductList').dataTable().fnDestroy();
                $('#inventoryProductListdiv').html('<table id="inventoryProductList" class="table table-card" style="width:100%; display:inherit;"</table >');
            }
            initInventorylist();
        }
    };
}();



//Offline vendor details modal
function offlineVendor(OfflineVendorId) {
    $('#viewOfflineVendorDetails').modal('show');
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/offlineVendor/OfflineVendor_ById?Id=${atob(OfflineVendorId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        async: false,
        success: function (result) {
             
            $("#OfflineVendorDetails").html(``);

            $('#OfflineVendorDetails').append(`
                <div class="form-group mb-0 row align-items-center">
                    <label for="newProductName" class="col-sm-4 col-form-label text-gray-500 fs-12 mb-1">${Langaugestore.Name}: </label>
                    <div class="col-sm-8">
                        <p class="font-weight-medium mb-0" style="font-size:14px;">${result.Item.Name}</p>
                    </div>
                </div>
                <div class="form-group mb-0 row align-items-center">
                    <label for="newProductName" class="col-sm-4 col-form-label text-gray-500 fs-12 mb-1">${Langaugestore.Email} : </label>
                    <div class="col-sm-8">
                        <p class="font-weight-medium mb-0" style="font-size:14px;">${result.Item.Email}</p>
                    </div>
                </div>
                <div class="form-group mb-0 row align-items-center">
                    <label for="newProductName" class="col-sm-4 col-form-label text-gray-500 fs-12 mb-1">Phone Number : </label>
                    <div class="col-sm-8">
                        <p class="font-weight-medium mb-0" style="font-size:14px;">${result.Item.PhoneNumber}</p>
                    </div>
                </div>
            `);

        }, error: function (error) {
            // Error function
        }
    });
    return false;
}; 