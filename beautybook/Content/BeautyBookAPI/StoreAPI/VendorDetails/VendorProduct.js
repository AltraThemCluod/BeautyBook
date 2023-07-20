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

var getVendorId = new URLSearchParams(window.location.search);
getVendorId = parseInt(atob(getVendorId.get('VendorId')));
var getVendorIdatob = ~~getVendorId;

//Data load more function
var productOffsetdata = 0;
var productLimitdata = 5;
var productListlength = 0;
function loadMore() {
     
    $('#loadIcon').show();
    
    if (productListlength > productLimitdata) {
        productOffsetdata += productLimitdata;
        ViewProductList();
    } else {
        $('#loadMorebtn').hide();
    }
}

//product list api call
function ViewProductList() {

    $('#viewProductlistloader').show();

    let viewProductList = new Object();
    viewProductList.IsPageProvided = true;
    viewProductList.Offset = productOffsetdata;
    viewProductList.Limit = productLimitdata;

     
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/product/Product_All?search=&ProductName=&ProductTypeId=0&ProductBrandId=0&LookUpStatusId=0&VendorId=${getVendorIdatob}&ProductBrandIds=&ProductCategoryIds=&MinPrice=&MaxPrice=&SortBy=1`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(viewProductList),
        crossDomain: true,
        //async: false,
        success: function (result) {
             

            if (result.Values.length <= 0) {
                $('#viewProductlist').removeClass('row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-xxl-5');
                $('#viewProductlist').html(`
                    <div class="col-lg-12">
                        <div class="card p-3 text-center">
                            ${Langaugestore.NoProductFound}
                        </div>
                    </div>
                `)
            } else {
                //$("#viewProductlist").html(``);
                $('#viewProductlist').addClass('row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-xxl-5');

                for (i = 0; i < result.Values.length; i++) {
                    productListlength = result.TotalRecords;
                    $('#viewProductlist').append(`
                        <div class="col mb-3">
                            <div class="product">
                                <div class="product_img">
                                    <img class="img-fluid" src="${APIEndPoint}/${result.Values[i].ProductThumbnailImage}" alt="">
                                </div>

                                <div class="product_info">
                                    <h6 class="product_title" style="text-transform:uppercase;"><a href="/Store/ViewProduct?productId=${btoa(result.Values[i].Id)}">${result.Values[i].ProductName}</a></h6>

                                    <div class="product_price">
                                        <span class="price">SAR ${result.Values[i].OfferPrice != null && result.Values[i].OfferPrice > 0 ? `${result.Values[i].OfferPrice}` : `${result.Values[i].Price}`}</span>
                                        ${result.Values[i].OfferPrice != null && result.Values[i].OfferPrice > 0 ? `<del>SAR ${result.Values[i].Price}</del>` : ``}
                                        ${result.Values[i].OfferPrice != null && result.Values[i].OfferPrice > 0 ? `<div class="on_sale"><span>${result.Values[i].OfferPercentage}% off</span></div>` : ``}
                                    </div>

                                     <div class="rating_wrap">
                                        <div class="Stars" style="--rating:${result.Values[i].ProductAverageRating == null || result.Values[i].ProductAverageRating == "" ? 0 : result.Values[i].ProductAverageRating};"></div>
                                        <span class="rating_num">(${result.Values[i].ProductStarCount})</span>
                                    </div>
                                </div>

                                 <div class="product_action_box">
                                    <ul class="list-unstyled pr_action_btn">
                                        <li class="add-to-cart">
                                            <a href="javascript:void(0)" onclick="addTocart(${result.Values[i].Id});" class="btn btn-white"><i class="bb-shopping-cart"></i></a>
                                        </li>

                                        <li>
                                            <a href="javascript:void(0)" onclick="addTowishlist(${result.Values[i].Id});" class="btn btn-white"><i class="bb-heart"></i></a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                      `);
                    }
                    $('#loadMorebtn').show();
                    $('#loadIcon').hide();
            }
            if (productListlength <= productLimitdata + productOffsetdata) {
                $('#loadMorebtn').hide();
            }
            $('#viewProductlistloader').hide();

        }, error: function (error) {
            // Error function
            $('#viewProductlistloader').hide();
        }
    });
    return false;
}

