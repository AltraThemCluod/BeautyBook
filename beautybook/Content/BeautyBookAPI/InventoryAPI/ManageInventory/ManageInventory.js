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

//fillter function
function seachInventory() {
    $('#searchInventorybtn').hide();
    $('#searchInventorybtnloading').show();
    inventoryList.init();
}

//fllter reset function
function resetInventory() {
    $('#resetInventorybtn').hide();
    $('#resetInventorybtnloading').show();
    $('#productName').val('');
    $('#productBrandsearch').selectpicker('val', null);
    $('#productTypesearch').selectpicker('val', null);
    inventoryList.init();
}

var inventoryList = function () {

    $('#inventoryListloader').show();

    let initInventorylist = function () {
        var ProductBrand = $('#productBrandsearch').val() == "" || $('#productBrandsearch').val() == null ? 0 : $('#productBrandsearch').val();
        var ProductType = $('#productTypesearch').val() == "" || $('#productTypesearch').val() == null ? 0 : $('#productTypesearch').val();
        var ProductName = $('#productName').val();
        let InventoryList = new Object();
        InventoryList.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/inventoryProduct/InventoryProduct_TopOne?search=&ProductName=${ProductName}&ProductTypeId=${parseInt(ProductType)}&ProductBrandId=${parseInt(ProductBrand)}&SalonId=${atob(SalonId)}`,
            headers: { 'Content-Type': 'application/json' },
            //"Authorization": '' + DeviceTokenNumber + ''
            dataType: 'json',
            data: JSON.stringify(InventoryList),
            crossDomain: true,
            success: function (Values) {
                debugger;
                $('#inventoryList').DataTable({
                    //"order": [[0, "desc"]],
                    data: Values.Values,
                    columns: [
                        {
                            "title": "" + Langaugestore.ProductName + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `<a href="/InventoryProduct/InventoryProduct?InventoryPrd=${btoa(row["InventoryOnlineId"] == null || row["InventoryOnlineId"] == "" ? row["InventoryOfflineId"] : row["InventoryOnlineId"])}&Type=${btoa(row["ProductTypeId"])}&Brand=${btoa(row["ProductBrandId"])}&We=${btoa(row["Weight"])}&WTI=${btoa(row["WeightTypeId"])}" class="link">${row["ProductName"]}</a>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.ProductType + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["ProductTypeName"] == "" || row["ProductTypeName"] == null ? '-' : row["ProductTypeName"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.Brand + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["ProductBrandName"] == "" || row["ProductBrandName"] == null ? '-' : row["ProductBrandName"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.Weight + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["Weight"] == "" || row["Weight"] == null ? '-' : row["Weight"]} 
                                            ${row["ProductWeightName"] == "" || row["ProductWeightName"] == null ? '' : row["ProductWeightName"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.InStock + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["ProductQty"] <= row["LowQtyAlert"] ?
                                    `<td>
                                        <div class="text-danger font-weight-bold">${row["ProductQty"]}
                                            ${row["InventoryOnlineId"] > 0 ?
                                        `<a href="/Store/ViewProduct?productId=${btoa(row["InventoryOnlineId"])}&typeId=${btoa(row["ProductTypeId"])}" class="badge text-danger p-2 bg-soft-danger"><i class="bb-shopping-cart fs-14 mr-1"></i>${Langaugestore.Buy_Now}</a>`
                                        : '<a href="javascript:void(0)" class="badge text-danger p-2 bg-soft-danger"><i class="bb-shopping-cart fs-14 mr-1"></i>' + Langaugestore.Not_Available_Online_Store + '</a>'}
                                        </div>
                                    </td>` :

                                    `<div class="font-weight-bold">${row["ProductQty"] == "" || row["ProductQty"] == null ? '-' : row["ProductQty"]}
                                        ${row["InventoryOnlineId"] > 0 ?
                                        `<a href="/Store/ViewProduct?productId=${btoa(row["InventoryOnlineId"])}&typeId=${btoa(row["ProductTypeId"])}" class="badge text-success p-2 bg-soft-success"><i class="bb-shopping-cart fs-14 mr-1"></i>${Langaugestore.Buy_Now}</a>`
                                        : '<a href="javascript:void(0)" class="badge text-success p-2 bg-soft-success"><i class="bb-shopping-cart fs-14 mr-1"></i>' + Langaugestore.Not_Available_Online_Store + '</a>'}
                                    </div>`}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.LowQtyAlert + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["LowQtyAlert"] == "" || row["LowQtyAlert"] == null ? '0' : row["LowQtyAlert"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
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
                                                    ${PermissionData.find(record => record.ModuleKey === "ViewProductDetails").Value == true ? `<a class="dropdown-item" href="/InventoryProduct/InventoryProduct?InventoryPrd=${btoa(row["Id"])}&Type=${btoa(row["ProductTypeId"])}&Brand=${btoa(row["ProductBrandId"])}&We=${btoa(row["Weight"])}&WTI=${btoa(row["WeightTypeId"])}"><i class="bb-eye text-gray-600 fs-16 mr-3"></i>${Langaugestore.View}</a>` : ""}
                                                    ${PermissionData.find(record => record.ModuleKey === "LowQtyAlert").Value == true ? `<a class="dropdown-item" href="javascript:void(0)" onclick="lowQtyAlert('${btoa(row["Id"])},${btoa(row["ProductTypeId"])},${btoa(row["ProductBrandId"])},${btoa(row["Weight"])},${btoa(row["WeightTypeId"])}');"><i class="bb-edit-3 text-gray-600 fs-16 mr-3"></i>${Langaugestore.Low_Qty_Alert}</a>` : ""}
                                                    ${PermissionData.find(record => record.ModuleKey === "UpdateQuantity").Value == true ? `<a class="dropdown-item" href="#" onclick="ShowOfflineInventoryProductDetails('${row["InventoryProductIds"]}')" data-toggle="modal" data-target="#addInventoryModal"><i class="bb-plus text-gray-600 fs-16 mr-3"></i>${Langaugestore.Update_Quantity}</a>` : ""}
                                                    ${PermissionData.find(record => record.ModuleKey === "SubtractQuantity").Value == true ? `<a class="dropdown-item" onclick="SubtractInventory('${btoa(row["Id"])},${btoa(row["ProductTypeId"])},${btoa(row["ProductBrandId"])},${btoa(row["Weight"])},${btoa(row["WeightTypeId"])}','${btoa(row["ProductQty"])}')" href="javascript:void(0)"><i class="bb-minus text-gray-600 fs-16 mr-3"></i>${Langaugestore.Subtract_Quantity}</a>` : ""}
                                                    <hr>
                                                    ${PermissionData.find(record => record.ModuleKey === "Delete").Value == true ? `<a class="dropdown-item text-danger" href="javascript:void(0)" onclick="inventoryDelete('${btoa(row["InventoryProductIds"])}')"><i class="bb-trash-2 text-danger fs-16 mr-3"></i>${Langaugestore.Delete}</a>` : ""}
                                                </div>
                                            </div>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "2%"
                        },
                    ],
                    buttons: [
                        {
                            className: `btn btn-primary float-left ${PermissionData.find(record => record.ModuleKey === "AddNewProduct").Value == true ? "d-block" : "d-none"}`,
                            text: '' + Langaugestore.Add_New_Prouduct + '',
                            action: function (e, dt, node, config) {
                                window.location = '/InventoryProduct/ProductDetails';
                            }
                        },
                        {
                            className: `btn btn-link float-left mb-3 ml-2 import-inventory-data ${PermissionData.find(record => record.ModuleKey === "ImportInventoryData").Value == true ? "d-block" : "d-none"}`,
                            text: "Import Inventory Data",
                            action: function (e, dt, node, config) {
                                window.location = "/InventoryProduct/UploadExcel";
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

                $('#searchInventorybtn').show();
                $('#searchInventorybtnloading').hide();

                $('#resetInventorybtn').show();
                $('#resetInventorybtnloading').hide();

                $('#inventoryListloader').hide();

            }, error: function (error) {
                $('#searchInventorybtn').show();
                $('#searchInventorybtnloading').hide();

                $('#resetInventorybtn').show();
                $('#resetInventorybtnloading').hide();

                $('#inventoryListloader').hide();

            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#inventoryList")) {
                $('#inventoryList').dataTable().fnDestroy();
                $('#manageInventorydiv').html('<table id="inventoryList" class="table table-card" style="width:100%; display:inherit;"></table >');
            }
            initInventorylist();
        }
    };
}();

//masterProductType dropdown API call in ajax methos
function masterProductTypeSearch() {

    let MasterProductType = new Object();
    MasterProductType.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/masterProductType/MasterProductType_All?search',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(MasterProductType),
        crossDomain: true,
        //async: false,
        success: function (result) {

            $("#productTypesearch").html(``);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#productTypesearch').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                    `);
                }
            }

            $('.selectpicker').selectpicker("refresh");


        }, error: function (error) {
            // Error function

        }
    });
    return false;
}

//MasterOrderStatus dropdown API call in ajax methos
function masterProductBrandSearch() {

    let MasterProductBrand = new Object();
    MasterProductBrand.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/masterProductBrand/MasterProductBrand_All?search&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(MasterProductBrand),
        crossDomain: true,
        //async: false,
        success: function (result) {

            $("#productBrandsearch").html(``);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#productBrandsearch').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                    `);
                }
            }

            $('.selectpicker').selectpicker("refresh");


        }, error: function (error) {
            // Error function

        }
    });
    return false;
}

//Offline vendor dropdown API call in ajax methos
function OfflineVendor() {

    let offlineVendor = new Object();
    offlineVendor.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/offlineVendor/OfflineVendor_All?search=&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(offlineVendor),
        crossDomain: true,
        //async: false,
        success: function (result) {

            $("#OfflineVendor").html(``);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#OfflineVendor').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                    `);
                }
            }
            $('.selectpicker').selectpicker("refresh");
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}


function ShowOfflineInventoryProductDetails(InvntoryId) {


    var InvntoryIdSplit = InvntoryId.split(',');
    var removeItem = 0;
    InvntoryIdSplit = jQuery.grep(InvntoryIdSplit, function (value) {
        return value != removeItem;
    });



    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + `/api/inventoryProduct/InventoryProduct_ById?Id=${parseInt(InvntoryIdSplit)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {
            $('#productWeightOffline').val(result.Item.Weight);
            $('#productNameOffline').val(result.Item.ProductName);
            $('#productTypeOffline').val(result.Item.ProductTypeId);
            $('#productBrandOffline').val(result.Item.ProductBrandId);
            $('#productWeightTypeoffline').selectpicker('val', result.Item.WeightTypeId);
        }, error: function (error) {
            // Error function
        }
    });
}

//masterProductType dropdown API call in ajax methos
function masterProductWeightOffline() {

    let MasterProductWeight = new Object();
    MasterProductWeight.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/masterProductWeight/MasterProductWeight_All?search',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(MasterProductWeight),
        crossDomain: true,
        //async: false,
        success: function (result) {

            $("#productWeightTypeoffline").html(``);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#productWeightTypeoffline').append(`
                        <option value="${result.Values[i].Id}">${result.Values[i].Name}</option>
                    `);
                }
            }

            $('.selectpicker').selectpicker("refresh");


        }, error: function (error) {
            // Error function
        }
    });
    return false;
}

function closeofflineinventorymodel() {
    $('#purchaseDateoffline').val('');
    $('#productQtyOffline').val('');
    $('#purchasePriceOffline').val('');
    $('#productWeightoffline').val('');
    $('#productWeightTypeoffline').selectpicker('val', null);
    $('#BillNumber').val('');
    $('#OfflineVendor').selectpicker('val', null);
}