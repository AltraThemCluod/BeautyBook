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
var salonId = getCookie("SalonId");

var gettypeId = new URLSearchParams(window.location.search);
gettypeId = parseInt(atob(gettypeId.get('typeId')));
var gettypeIdatob = ~~gettypeId;

function addTowishlist(productId) {
     
    var AddToWishlist = new Object();
    AddToWishlist.Id = 0;
    AddToWishlist.ProductId = productId;
    AddToWishlist.SalonId = parseInt(atob(salonId));

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/productWishlist/ProductWishlist_Upsert`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(AddToWishlist),
        crossDomain: true,
        success: function (result) {

            if (result.Code == 200) {
                WishlistProductcount();
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
                setTimeout(function () {
                    window.location.href = '/Store/Wishlist';
                }, 3000);
            }
        }, error: function (error) {
             
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            });
            setTimeout(function () {
                window.location.href = '/Store/Wishlist';
            }, 3000);
        }
    });
}

//product list api call
function wishlist() {

    $('#viewProductlistloader').show();


    let viewProductList = new Object();
    viewProductList.IsPageProvided = true;

     
    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/productWishlist/ProductWishlist_All?search=&SalonId=${atob(salonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(viewProductList),
        crossDomain: true,
        async: false,
        success: function (result) {
             

            if (result.Values.length <= 0) {
                $('#wishlist').removeClass('row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-xxl-5');
                $('#wishlist').html(`
                    <div class="col-lg-12">
                        <div class="card p-3 text-center">
                            ${Langaugestore.No_Wishlist_found}
                        </div>
                    </div>
                `)
            } else {
                $("#wishlist").html(``);
                $('#wishlist').addClass('row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-xxl-5');
            }

             
            for (i = 0; i < result.Values.length; i++) {
                $('#wishlist').append(`
                <div class="col mb-3">
                    <div class="product">
                        <div class="product_img">
                            <img class="img-fluid product-defualt-img" onerror="this.src='/Content/assets/images/DefaultProduct.jpg'" src="${APIEndPoint}/${result.Values[i].ProductThumbnailImage}" alt="">
                        </div>

                        <div class="product_info">
                            <h6 class="product_title" style="text-transform:uppercase;"><a href="/Store/ViewProduct?productId=${btoa(result.Values[i].Id)}&typeId=${btoa(gettypeIdatob)}">${result.Values[i].ProductName}</a></h6>

                            <div class="product_price">
                                <span class="price">SAR ${result.Values[i].OfferPrice != null && result.Values[i].OfferPrice > 0 ? `${result.Values[i].OfferPrice}` : `${result.Values[i].ProductPrice}`}</span>
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
                                    <a href="javascript:void(0)" onclick="addTocart(${result.Values[i].ProductId});" class="btn btn-white"><i class="bb-shopping-cart"></i></a>
                                </li>

                                <li>
                                    <a href="javascript:void(0)" onclick="deleteWishlistConfirm(${result.Values[i].Id});" class="btn btn-white"><i class="bb-trash-2"></i></a>
                                </li>
                            </ul>
                        </div>
                    </div>
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

//deleteProductconfirm function
function deleteWishlistConfirm(productId) {
     
    Swal.fire({
        title: 'Are you sure you want to remove this product from wishlist?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.isConfirmed) {
            DeleteWishlistSwal(productId);
        }
    })
}

//deleteProduct function

function DeleteWishlistSwal(productId) {
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/productWishlist/ProductWishlist_Delete?Id=' + productId + '&DeletedBy=' + atob(UserId) + '',
        headers: { "Authorization": '' + DeviceTokenNumber + '' },
        success: function (result) {
            if (result.Code == 200) {
                wishlist();
                WishlistProductcount();
                if (result.Code == 200) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: result.Message,
                        showConfirmButton: false,
                        timer: 3000
                    })
                }
            }
        }, error: function (error) {

            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
        }
    });
}




