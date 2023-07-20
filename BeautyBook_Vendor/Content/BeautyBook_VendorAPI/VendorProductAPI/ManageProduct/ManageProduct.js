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
var VendorId = getCookie("UserId");

//fillter function
function seachProduct() {
    $('#searchProductbtn').hide();
    $('#searchProductbtnloading').show();
    productList.init();
}

//fllter reset function
function resetProduct() {
    $('#resetProductbtn').hide();
    $('#resetProductbtnloading').show();
    $('#productName').val('');
    $('#productType').selectpicker('val', null);
    $('#productBrand').selectpicker('val', null);
    $('#productStatus').selectpicker('val', null);
    productList.init();
}

var productList = function () {

    $('#productListloader').show();
     
    let initProductlist = function () {
        var productName = $('#productName').val();
        var productType = $('#productType').val() == "" || $('#productType').val() == null ? 0 : $('#productType').val();
        var productBrand = $('#productBrand').val() == "" || $('#productBrand').val() == null ? 0 : $('#productBrand').val();
        var productStatus = $('#productStatus').val() == "" || $('#productStatus').val() == null ? 0 : $('#productStatus').val();

        let ProductList = new Object();
        ProductList.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/product/Product_All?search=null&ProductName=${productName}&ProductTypeId=${productType}&ProductBrandId=${productBrand}&LookUpStatusId=${productStatus}&VendorId=${atob(VendorId)}&ProductBrandIds=&ProductCategoryIds=&MinPrice=&MaxPrice=&SortBy=4`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(ProductList),
            crossDomain: true,
            success: function (Values) {
                 
                console.log(Values);
                var i = 1;
                 
                $('#productList').DataTable({
                    data: Values.Values,
                    columns: [
                        {
                            "title": ""+Langaugestore.ID+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${i++}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "1%"
                        },
                        {
                            "title": ""+Langaugestore.ProductName+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["ProductName"] == "" || row["ProductName"] == null ? '-' : row["ProductName"]}
                                                <spna>${row["OfferPrice"] > 0 ? '<div class="badge bg-soft-success text-success border p-1">Offer</span>' : ''}</span>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.ProductType+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["ProductTypeName"] == "" || row["ProductTypeName"] == null ? '-' : row["ProductTypeName"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Brand+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["ProductBrandName"] == "" || row["ProductBrandName"] == null ? '-' : row["ProductBrandName"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Weight+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["Weight"] == "" || row["Weight"] == null ? '-' : row["Weight"]} 
                                            ${row["ProductWeightTypeName"] == "" || row["ProductWeightTypeName"] == null ? '' : row["ProductWeightTypeName"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.InStock+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<b>${row["ProductQty"] == "" || row["ProductQty"] == null ? '0' : row["ProductQty"]}</b>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.LowQtyAlert+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `
                                    ${row["ProductQty"] <= row["LowQtyAlert"] ?
                                    `<div class="text-danger font-weight-bold">${row["LowQtyAlert"] == "" || row["LowQtyAlert"] == null ? '-' : row["LowQtyAlert"]}</div>` :
                                    `<div class="font-weight-bold">${row["LowQtyAlert"] == "" || row["LowQtyAlert"] == null ? '-' : row["LowQtyAlert"]}</div>`}
                                `;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "Expiry Date", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `<b>${row["ExpiryDate"] == "" || row["ExpiryDate"] == null ? '-' : row["IsExpiryDateAfter30Day"] == true ? `<span class="text-danger">${row["ExpiryDate"]}</span>` : `<span>${row["ExpiryDate"]}</span>`}</b>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Status+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData += `${row["LookUpStatusId"] == 13 ? `<div class="badge bg-soft-success text-success border p-2" onclick="changeStatusmodal(${row["Id"]});">${row["LookUpStatusName"]}</div>` : ''}`;
                                htmlData += `${row["LookUpStatusId"] == 14 ? `<div class="badge bg-soft-danger text-danger border p-2" onclick="changeStatusmodal(${row["Id"]});">${row["LookUpStatusName"]}</div>` : ''}`;
                                htmlData += `${row["LookUpStatusId"] == 15 ? `<div class="badge bg-soft-secondary text-secondary border p-2" onclick="changeStatusmodal(${row["Id"]});">${row["LookUpStatusName"]}</div>` : ''}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "2%"
                        },
                        {
                            "title": ""+Langaugestore.Actions+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";      
                                htmlData = `<div class="dropdown">
                                                <button class="btn btn-icon" type="button" data-toggle="dropdown">
                                                    <i class="bb-more-horizontal fs-22"></i>
                                                </button>
                                                <div class="dropdown-menu dropdown-menu-right">
                                                    <a class="dropdown-item" href="javascript:void(0)" onclick="updateQty(${row["Id"]});"><i class="bb-repeat text-gray-600 fs-16 mr-3"></i>${Langaugestore.Update_QTY}</a>
                                                    <a class="dropdown-item" href="/VendorProduct/ProductDetails?productId=${btoa(row["Id"])}"><i class="bb-edit-3 text-gray-600 fs-16 mr-3"></i>${Langaugestore.EDIT}</a>
                                                    <hr>
                                                    <a class="dropdown-item text-danger" href="javascript:void(0)" onclick="productDelete(${row["Id"]})"><i class="bb-trash-2 text-danger fs-16 mr-3"></i>${Langaugestore.Delete}</a>
                                                </div>
                                            </div>`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "2%"
                        },
                    ],
                    buttons: [
                        {
                            className: 'btn btn-primary float-left mb-3',
                            text: ''+ Langaugestore.Add_New_Product+'',
                            action: function (e, dt, node, config) {
                                window.location = '/VendorProduct/ProductDetails';
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
               
                $('#searchProductbtn').show();
                $('#searchProductbtnloading').hide();

                $('#resetProductbtn').show();
                $('#resetProductbtnloading').hide();

                $('#productListloader').hide();
            }, error: function (error) {
                $('#searchProductbtn').show();
                $('#searchProductbtnloading').hide();

                $('#resetProductbtn').show();
                $('#resetProductbtnloading').hide();

                $('#productListloader').hide();

            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#productList")) {
                $('#productList').dataTable().fnDestroy();
                $('#manageProductdiv').html('<table id="productList" class="table table-card" style="width:100%; display:inherit;"</table >');
            }
            initProductlist();
        }
    };
}();