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

var gettypeId = new URLSearchParams(window.location.search);
gettypeId = parseInt(atob(gettypeId.get('typeId')));
var gettypeIdatob = ~~gettypeId;

//product list api call
function ViewProductList(SortById) {
     
    $('#viewProductlistloader').show();


    let viewProductList = new Object();
    viewProductList.IsPageProvided = true;

    var sortById = SortById;


    var MasterProductCategory = $("input[name=ProductCategory]:checkbox:checked").map(function () {
        return this.value;
    }).toArray();
    var masterProductCategory = array = MasterProductCategory + ""

    if (MasterProductCategory != "") {
        gettypeIdatob = 0
    }

    var MasterProductBrand = $("input[name=ProductBrand]:checkbox:checked").map(function () {
        return this.value;
    }).toArray();
    var masterProductBrand = array = MasterProductBrand + ""

    var minPrice = $('#MinPrice').val();
    var maxPrice = $('#MaxPrice').val();

     
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/product/Product_All?search=&ProductName=&ProductTypeId=${gettypeIdatob}&ProductBrandId=0&LookUpStatusId=0&VendorId=0&ProductBrandIds=${masterProductBrand}&ProductCategoryIds=${masterProductCategory}&MinPrice=${minPrice}&MaxPrice=${maxPrice}&SortBy=${sortById}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(viewProductList),
        crossDomain: true,
        async: false,
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
                $("#viewProductlist").html(``);
                $('#viewProductlist').addClass('row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-xxl-5');
            }

             
            for (i = 0; i < result.Values.length; i++) {
                $('#viewProductlist').append(`
                <div class="col mb-3">
                    <a href="/Store/ViewProduct?productId=${btoa(result.Values[i].Id)}&typeId=${btoa(gettypeIdatob)}">
                        <div class="product">
                            <div class="product_img">
                                <img class="img-fluid product-defualt-img" onerror="this.src='/Content/assets/images/DefaultProduct.jpg'" href="/Store/ViewProduct?productId=${btoa(result.Values[i].Id)}&typeId=${btoa(gettypeIdatob)}" src="${APIEndPoint}/${result.Values[i].ProductThumbnailImage}" alt="">
                            </div>

                            <div class="product_info">
                                <h6 class="product_title" style="text-transform:uppercase;">${result.Values[i].ProductName}</h6>

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
                    </a>
                </div>
            `);
            }
            $('#viewProductlistloader').hide();
        }, error: function (error) {
            // Error function
            $('#viewProductlistloader').hide();
        }
    });
    return false;
}




//product category list api call
function productCategory() {
    let MasterProductCategory = new Object();
    MasterProductCategory.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/masterProductType/MasterProductType_All?search&IsAllow=false`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(MasterProductCategory),
        crossDomain: true,
        async: false,
        success: function (result) {
            debugger;
            $("#categoriesFilterlist").html(``);
            console.log('MasterProductCategoryObj', result)
            var ProductCategoryVar = "";
            for (i = 0; i < result.Values.length; i++) {
                ProductCategoryVar = "";
                if (result.Values[i].ProductCategoryJson != null) {
                    for (j = 0; j < result.Values[i].ProductCategoryJson.length; j++) {
                        ProductCategoryVar += `<li style="list-style: none;">
                                    <a href="#" class="product-category-link">
                                        <div class="custom-control custom-checkbox">
                                            <input value="${result.Values[i].ProductCategoryJson[j].Id}" onchange="SelectProductCategory(this,${result.Values[i].Id})" type="checkbox" class="custom-control-input ProductType_${result.Values[i].Id}" name="ProductCategory" id="ProductCategory${result.Values[i].ProductCategoryJson[j].Id}">
                                            <label class="custom-control-label" for="ProductCategory${result.Values[i].ProductCategoryJson[j].Id}">${result.Values[i].ProductCategoryJson[j].ProductCategoryName}</label>
                                        </div>
                                    </a>
                                </li>`
                    }
                } else {
                    ProductCategoryVar += `<p style="list-style: none;" class="text-muted mb-0 card p-1 text-center">Not found</p>`
                }

                
                if (result.Values.length > 0) {
                    $('#categoriesFilterlist').append(`
                        <li>
                            <a href="#" class="product-category-link">
                                <div class="custom-control custom-checkbox">
                                    <input value="${result.Values[i].Id}" onchange="SelectProductType(this,${result.Values[i].Id})" type="checkbox" class="custom-control-input" name="ProductType" id="ProductType${result.Values[i].Id}">
                                    <label class="custom-control-label" for="ProductType${result.Values[i].Id}">${result.Values[i].Name}</label>
                                </div>
                            </a>
                            <ul id="SubCategory_${result.Values[i].Id}">
                                ${ProductCategoryVar}
                            </ul>
                        </li>
                    `);
                }

                $("#SubCategory_" + result.Values[i].Id).hide();

            }
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}

//SelectProductType Onchange Slide
function SelectProductType(Checkbox, ProductTypeId) {
    debugger;
    if (Checkbox.checked == true) {
        $("#SubCategory_" + ProductTypeId).show();
        $('.ProductType_' + ProductTypeId).prop('checked', true)
    } else if (Checkbox.checked == false) {
        $("#SubCategory_" + ProductTypeId).hide();
        $('.ProductType_' + ProductTypeId).prop('checked', false)
    }
}

function SelectProductCategory(Checkbox, CategoryId) {

    var CategoryCheckboxCount = $('.ProductType_' + CategoryId+':checkbox').length;
    var CategoryCheckboxcheckedCount = $('.ProductType_' + CategoryId +':checkbox:checked').length;

    if (CategoryCheckboxcheckedCount == 0) {
        $("#SubCategory_" + CategoryId).hide();
        $('.ProductType_' + CategoryId).prop('checked', false)
    }
    if (Checkbox.checked == true) {
        if (CategoryCheckboxCount == CategoryCheckboxcheckedCount) {
            $('#ProductType' + CategoryId).prop('checked', true)
        }
    } else if (Checkbox.checked == false) {
        $('#ProductType' + CategoryId).prop('checked', false)
    }
}


//product brand list api call
function productBrand() {
    debugger;
    let MasterProductBrand = new Object();
    MasterProductBrand.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/masterProductBrand/MasterProductBrand_All?search&SalonId=0`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(MasterProductBrand),
        crossDomain: true,
        async: false,
        success: function (result) {
            debugger;
            $("#brandFilterslist").html(``);

            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#brandFilterslist').append(`
                        <div class="custom-control custom-checkbox">
                            <input type="checkbox" value="${result.Values[i].Id}" class="custom-control-input" name="ProductBrand" id="ProductBrand${result.Values[i].Id}">
                            <label class="custom-control-label product-category-link" for="ProductBrand${result.Values[i].Id}">${result.Values[i].Name}</label>
                        </div>
                            
                    `);
                }
            }
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}