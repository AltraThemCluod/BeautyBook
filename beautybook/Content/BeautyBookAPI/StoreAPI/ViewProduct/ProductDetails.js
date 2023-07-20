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

var getProductId = new URLSearchParams(window.location.search);
getProductId = parseInt(atob(getProductId.get('productId')));
var getProductIdatob = ~~getProductId;
 
var gettypeId = new URLSearchParams(window.location.search);
gettypeId = gettypeId.get('typeId');

//backarrow add url parameter
$('#productBackarrow').attr('href', `/Store/StoreInside?typeId=${gettypeId}`);

function ProductDetails() {

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/product/Product_ById?Id=${getProductIdatob}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        success: function (result) {

            if (result.Item.ProductImage.length > 0) {
                $("#MainImage").html("");
                $("#SideImage").html("");
                let MainImage = '';
                let SideImage = '';

                for (let i = 0; i < result.Item.ProductImage.length; i++) {
                    MainImage = `<div>
                                    <a data-fancybox="gallery" href="${APIEndPoint + "/" + result.Item.ProductImage[i].ImageUrl}">
                                        <img onerror="this.src='/Content/assets/images/DefaultProduct.jpg'" src="${APIEndPoint + "/" + result.Item.ProductImage[i].ImageUrl}" class="img-fluid mx-auto">
                                    </a>
                                </div>`;
                    SideImage = ` <div class="border mx-1 rounded-sm">
                                    <img onerror="this.src='/Content/assets/images/DefaultProduct.jpg'" src="${APIEndPoint + "/" + result.Item.ProductImage[i].ImageUrl}" class="img-fluid">
                                </div>`;
                    $("#MainImage").append(MainImage);
                    $("#SideImage").append(SideImage);

                }
                $('.slider').slick('refresh');

            } else {
            }
            //Product Highlights Bind
            if (result.Item.ProductHighlights.length <= 0) {
                $('#productHighlights').append(`
                    <span class="text-muted fs-14">${Langaugestore.Highlights_not_fount}</span>
                `);
            }

            for (i = 0; i < result.Item.ProductHighlights.length; i++) {
                $('#productHighlights').append(`
                    <li class="my-2"><span class="font-weight-medium">${result.Item.ProductHighlights[i].HighlightsLabel}:</span> ${result.Item.ProductHighlights[i].HighlightsDiscription}</li>
                `);
            }

            //Product Service Bind
            if (result.Item.ProductServiceHighlights.length <= 0) {
                $('#productService').append(`
                    <span class="text-muted fs-14">${Langaugestore.Services_not_found}</span>
                `);
            }

            for (i = 0; i < result.Item.ProductServiceHighlights.length; i++) {
                $('#productService').append(`
                        <li class="my-2">${result.Item.ProductServiceHighlights[i].ServiceHighlightsDiscription}</li>
                `);
            }

            //Product Specifications Bind
            if (result.Item.ProductSpecifications.length <= 0) {
                $('#productSpecifications').append(`
                    <span class="text-muted fs-14">${Langaugestore.Specifications_not_fount}</span>
                `);
            }

            for (i = 0; i < result.Item.ProductSpecifications.length; i++) {
                $('#productSpecifications').append(`
                     <tr class="row">
                        <td class="col-4 col-sm-2"><span class="text-muted">${result.Item.ProductSpecifications[i].HighlightsLabel}</span></td>
                        <td class="col-8 col-sm-9">${result.Item.ProductSpecifications[i].HighlightsDiscription}</td>
                    </tr>   
                `);
            }

            //$('#productInformation').html(atob(result.Item.ProductInformation))
            //$('#productReturnPolicy').html(atob(result.Item.ReturnPolicy))
            $('#productInformation').html(decodeURIComponent(escape(window.atob(result.Item.ProductInformation))))
            $('#productReturnPolicy').html(decodeURIComponent(escape(window.atob(result.Item.ReturnPolicy))))

            $('#productDetailsrighttop').append(`
                ${result.Item.ProductQty == 0 ?
                    `<div class="badge badge-danger p-2">${Langaugestore.Stock_Out}</div>`
                    :
                    `<div class="badge badge-success p-2">${Langaugestore.In_Stock}</div>`
                }
                <h4 class="my-2 font-weight-medium">${result.Item.ProductName} ${result.Item.Weight} ${result.Item.ProductWeightTypeName}</h4>
                <a href="" class="link font-weight-normal d-flex align-items-center product-review">
                    <span class="badge badge-primary d-inline-flex align-items-center align-middle mr-1">
                        <span>${result.Item.ProductAverageRating == null || result.Item.ProductAverageRating == "" ? 0 : result.Item.ProductAverageRating}</span>
                        <svg xmlns="http://www.w3.org/2000/svg" height="14" width="14" viewBox="0 0 20 20" fill="currentColor">
                            <path d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path>
                        </svg>
                    </span>
                    <span class="text-muted fs-14">${result.Item.ReviewCount} ${Langaugestore.Reviews} & ${result.Item.StarCount} ${Langaugestore.Ratings}</span>
                </a>

                <div class="my-4">
                    <h4 class="price-detail font-weight-normal">
                        <b class="text-primary mr-1">SAR ${result.Item.OfferPrice != null && result.Item.OfferPrice > 0 ? `${result.Item.OfferPrice}` : `${result.Item.Price}`}</b>
                        ${result.Item.OfferPrice != null && result.Item.OfferPrice > 0 ? `<del>SAR ${result.Item.Price}</del>` : ``}
                        ${result.Item.OfferPrice != null && result.Item.OfferPrice > 0 ? `<span>${result.Item.OfferPercentage}% off</span>` : ``}
                    </h4>

                    <p class="font-weight-semibold fs-14 text-success mt-1">${Langaugestore.inclusive_of_all_taxes}</p>
                </div>
                ${result.Item.ProductQty == 0 ? '<p style="font-size:18px;color:#878787;font-weight:400;">' + Langaugestore.This_item_is_currently_out_of_stock+'</p>' : ''}
                 
                <div class="d-flex flex-wrap align-items-center product-dif-btn">
                        <div class="store-touchspin mr-3 mb-3">
                            <input id="productQty" class="subtract-quantity text-center" type="text" value="1"
                                    name="subtract-quantity">
                        </div>
                    <a href="javascript:void(0)" onclick="addTocart(${result.Item.Id});" class="btn btn-primary btn-wide font-weight-medium mr-3 mb-3" type="button"><i class="bb-shopping-cart fs-16 mr-2"></i>${Langaugestore.Add_to_Cart}</a>
                    <a href="javascript:void(0)" onclick="addTowishlist(${result.Item.Id});" class="btn btn-wide btn-light border add-to-wishlist mb-3" type="button"><i class="bb-heart mr-2 fs-16"></i>${Langaugestore.Add_to_wishlist}</a></div>
                </div>
            `);



            if (result.Item.ProductSeller.length <= 0) {
                $('#sellerListmodal').append(`
                    <div class="card hover-border-primary shadow-2 mb-3">
                        <div class="card-body">
                            <h5 class="mb-1">${Langaugestore.NoRecordsFound}</h5>
                        </div>
                    </div>
                `);
            }

            for (i = 0; i < result.Item.ProductSeller.length; i++) {
                 
                $('#sellerListmodal').append(`
                    <div class="card hover-border-primary shadow-2 mb-3">
                        <div class="card-body">
                            <h5 class="mb-1">
                                <span>${result.Item.ProductSeller[i].FirstName} ${result.Item.ProductSeller[i].SecondName}</span>
                                <span class="badge badge-info ml-2 align-items-center">
                                    <span class="align-middle font-weight-medium fs-12">${result.Item.ProductSeller[i].VendorAverageRating}</span>
                                    <svg xmlns="http://www.w3.org/2000/svg" height="14" width="14" viewBox="0 0 20 20" fill="currentColor">
                                        <path d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path>
                                    </svg>
                                </span>
                                </h4>
                                <p class="text-muted fs-14 mb-2">${Langaugestore.Seller_since} <p class="font-weight-medium text-dark mb-2">${result.Item.ProductSeller[i].CreatedDateStr}</p> and <span class="font-weight-medium text-dark">${result.Item.ProductSeller[i].CustomerRecommend}%</span> of customers recommend</p>
                                <hr>
                                <h4 class="price-detail font-weight-normal mb-2">
                                    <b class="text-primary mr-1">SAR ${result.Item.ProductSeller[i].OfferPrice != null && result.Item.ProductSeller[i].OfferPrice > 0 ? `${result.Item.ProductSeller[i].OfferPrice}` : `${result.Item.ProductSeller[i].ProductPrice}`}</b>
                                    ${result.Item.ProductSeller[i].OfferPrice != null && result.Item.ProductSeller[i].OfferPrice > 0 ? `<del>SAR ${result.Item.ProductSeller[i].ProductPrice}</del>` : ``}
                                    ${result.Item.ProductSeller[i].OfferPrice != null && result.Item.ProductSeller[i].OfferPrice > 0 ? `<span>${result.Item.ProductSeller[i].OfferPercentage}% off</span>` : ``}
                                </h4>

                                <a href="javascript:;"onclick="addTocart(${result.Item.ProductSeller[i].ProductId});" class="btn btn-primary btn-wide font-weight-medium mt-3" role="button"><i class="bb-shopping-cart fs-16 mr-2"></i>${Langaugestore.Add_to_Cart}</a>
                        </div>
                    </div>
                `);
            }

            $('#feedbackCountbox').append(`
                <div class="card bg-primary text-white p-4 mb-3 sticky-panel-top">
                    <div class="d-block mb-1 display-4">${result.Item.CustomersRecommend}%</div>
                    <p class="text-gray-100 mb-0">${Langaugestore.of_customers_recommend_this_product}</p>
                    <hr>
                    <div class="text-gray-100">${result.Item.StarCount} ${Langaugestore.Ratings} & ${result.Item.ReviewCount} ${Langaugestore.Reviews}</div>
                </div>
            `);

            //vendorWholesaler
            $('#vendorWholesaler').append(`
                <p class="fs-14 text-muted mb-2">
                    ${Langaugestore.Seller}:
                    <a href="/Store/VendorDetails?VendorId=${btoa(result.Item.VendorId)}" class="font-weight-medium">
                        ${result.Item.VendorFirstname} ${result.Item.VendorSecondname}
                        <span class="badge badge-primary d-inline-flex align-items-center align-middle">
                            <span>${result.Item.VendorAverageRating}</span>
                            <svg xmlns="http://www.w3.org/2000/svg" height="14" width="14" viewBox="0 0 20 20" fill="currentColor">
                                <path d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path>
                            </svg>
                        </span>
                    </a>
                </p>
            `);
            touchSpin();

        }, error: function (error) {
            // Error function
        }
    });
    return false;
}