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

var promotionList = function () {

    $('#promotionListloader').show();
     
    let initPromotionlist = function () {

        let PromotionList = new Object();
        PromotionList.IsPageProvided = true;

        $.ajax({
            processing: true,
            serverSide: true,
            type: 'POST',
            url: APIEndPoint + `/api/promotion/Promotion_All?search&VendorId=${atob(VendorId)}&ProductId=0&ProductTypeId=0&ProductBrandId=0`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(PromotionList),
            crossDomain: true,
            success: function (Values) {
                console.log(Values);
                var i = 1;
                 
                $('#promotionList').DataTable({
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
                                htmlData = `${row["ProductName"] == "" || row["ProductName"] == null ? '-' : row["ProductName"]}`;
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
                            "title": "" + Langaugestore.Start_Date + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["StartDate"] == "" || row["StartDate"] == null ? '-' : row["StartDate"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": "" + Langaugestore.End_Date + "", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["EndDate"] == "" || row["EndDate"] == null ? '-' : row["EndDate"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Price+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["OrignalPrice"] == "" || row["OriginalPrice"] == 0 ? '-' : 'SAR ' + row["OriginalPrice"]}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Offer_Price+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `${row["OfferPrice"] == "" || row["OfferPrice"] == 0 ? '-' : `<div class="text-success fw-medium">SAR ${row["OfferPrice"]}</div>`}`;
                                return htmlData;
                            }
                            , "orderable": false, "width": "3%"
                        },
                        {
                            "title": ""+Langaugestore.Actions+"", "data": "",
                            "render": function (data, type, row) {
                                let htmlData = "";
                                htmlData = `
                                     <a class="btn btn-sm btn-light border" href="/Promotion/PromotionDetails?promotionId=${btoa(row["Id"])}"><i class="bb-edit-3 text-gray-600 fs-16 mr-2"></i>${Langaugestore.EDIT}</a>
                                           `;
                                return htmlData;
                            }
                            , "orderable": false, "width": "2%"
                        },
                    ],
                    buttons: [
                        {
                            className: 'btn btn-primary float-left mb-3',
                            text: '' + Langaugestore.Add_New_Promotion +'',
                            action: function (e, dt, node, config) {
                                window.location = '/Promotion/PromotionDetails';
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
               
                $('#promotionListloader').hide();

            }, error: function (error) {

                $('#promotionListloader').hide();

            }
        });
    }
    return {
        //main function to initiate the module
        init: function () {
            if ($.fn.DataTable.isDataTable("#promotionList")) {
                $('#promotionList').dataTable().fnDestroy();
                $('#managePromotiondiv').html('<table id="promotionList" class="table table-card" style="width:100%; display:inherit;"</table >');
            }
            initPromotionlist();
        }
    };
}();