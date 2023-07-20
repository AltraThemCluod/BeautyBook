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

var getproductId = new URLSearchParams(window.location.search);
getproductId = parseInt(atob(getproductId.get('productId')));
var getproductIdatob = ~~getproductId;


if (getproductIdatob == 0) {
    $('#productform').show();
}

function RemovecustomImageRender(Object, divSection) {
    
    if ($(Object)[0].files[0] != null && $(Object)[0].files[0] != undefined) {
        $(divSection +" .customImageRender").html("");
    }
}
//appoinmentDetails edit API call in ajax methos
function productDetailsedit() {

    if (getproductIdatob > 0) {

        $('#productformloader').show();
        $('#productform').hide();

        $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + `/api/product/Product_ById?Id=${getproductIdatob}`,
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            crossDomain: true,
            success: function (result) {
                $('#productId').val(result.Item.Id)
                let ProductImageIds = '';
                for (let i = 0; i < result.Item.ProductImage.length; i++) {
                    if (i == 0) {
                        ProductImageIds = result.Item.ProductImage[i].Id.toString();
                    } else {
                        ProductImageIds += ","+result.Item.ProductImage[i].Id.toString();
                    }
                    $(`#ProductImageDiv${i+1}`).prepend(`<div class="customImageRender"><div class="file-drop-preview img-thumbnail rounded">
                        <img src="${APIEndPoint + '/' + result.Item.ProductImage[i].ImageUrl}" alt="Image ${i+1}">
                        </div></div>`);

                    $(`#ProductImageDiv${i+1} .file-drop-message`).html(`Image ${i+1}`);
                }

                $('#productImageId').val(ProductImageIds);
                $('#productName').val(result.Item.ProductName);
                $('#productType').selectpicker('val', result.Item.ProductTypeId);
                $('#productCategory').selectpicker('val', result.Item.ProductCategoryId);
                masterProductCategory();
                $('#productBrand').selectpicker('val', result.Item.ProductBrandId);
                $('#productQty').val(result.Item.ProductQty);
                $('#purchasePrice').val(result.Item.Price);
                $('#productWeight').val(result.Item.Weight);
                $('#productWeightType').selectpicker('val', result.Item.WeightTypeId);
                $('#qtyAlert').val(result.Item.LowQtyAlert);
                //$('#productInformation').summernote('code', atob(result.Item.ProductInformation));
                //$('#returnPolicy').summernote('code', atob(result.Item.ReturnPolicy));
                $('#productInformation').summernote('code', decodeURIComponent(escape(window.atob(result.Item.ProductInformation))));
                $('#returnPolicy').summernote('code', decodeURIComponent(escape(window.atob(result.Item.ReturnPolicy))));
                $('#producttax').val(result.Item.ProductTax);
                $('#ExpiryDate').val(result.Item.ExpiryDate);
                $("#IsExpiryDate").prop("checked", result.Item.IsExpiryDate);

                if (result.Item.IsExpiryDate == true) {
                    $("#IsExpiryDate_Div").show();
                }
                else {
                    $("#IsExpiryDate_Div").hide();
                }


                if (result.Item.ProductHighlights.length > 0) {
                    $('#Product_Top_Highlights > div').first().css("display", "none");
                    $('#Product_Top_Highlights > div:first-child > div > div > div > input').attr("id", "");
                    $('#Product_Top_Highlights > div:first-child > div > div > div > input').attr("name", "");
                }
                for (i = 0; i < result.Item.ProductHighlights.length; i++) {
                    $('#Product_Top_Highlights').append(`
                        <div data-repeater-item>
                            <div class="row align-items-end">
                                <div class="col-xl-4 col-lg-5 col-md-6">
                                    <div class="form-group">
                                        <label class="custom-label">Highlight label</label>
                                        <input class="form-control" value="${result.Item.ProductHighlights[i].HighlightsLabel}" id="HighlightLabel" name="HighlightLabel[]" type="text" placeholder="Enter label name" />
                                    </div>
                                </div>
                                <div class="col-xl-4 col-lg-5 col-md-6">
                                    <div class="form-group">
                                        <label class="custom-label">Highlight discription</label>
                                        <input class="form-control" value="${result.Item.ProductHighlights[i].HighlightsDiscription}" id="HighlightDiscription" name="HighlightDiscription[]" type="text" placeholder="Enter discription" />
                                    </div>
                                </div>
                                <div class="col-xl-1 col-lg-2 col-md-6">
                                    <div class="form-group">
                                        <input class="btn btn-danger" data-repeater-delete type="button" value="Delete" />
                                    </div>
                                </div>
                            </div>
                            <hr>
                        </div>
                    `);
                }

                //Service Highlights input  bind
                if (result.Item.ProductServiceHighlights.length > 0) {
                    $('#Service_Highlights > div').first().css("display", "none");
                    $('#Service_Highlights > div:first-child > div > div > div > input').attr("id", "");
                    $('#Service_Highlights > div:first-child > div > div > div > input').attr("name", "");
                }
                for (i = 0; i < result.Item.ProductServiceHighlights.length; i++) {
                    $('#Service_Highlights').append(`
                        <div data-repeater-item>
                            <div class="row align-items-end">
                                <div class="col-xl-4 col-lg-5 col-md-6">
                                    <div class="form-group">
                                        <label class="custom-label">Highlight discription</label>
                                        <input class="form-control" value="${result.Item.ProductServiceHighlights[i].ServiceHighlightsDiscription}" id="ServiceHighlightDiscription" name="ServiceHighlightDiscription[]" type="text" placeholder="Enter discription" />
                                    </div>
                                </div>
                                <div class="col-xl-1 col-lg-2 col-md-6">
                                    <div class="form-group">
                                        <input class="btn btn-danger" data-repeater-delete type="button" value="Delete" />
                                    </div>
                                </div>
                            </div>
                            <hr>
                        </div>
                    `);
                }

                //Service Highlights input  bind
                if (result.Item.ProductSpecifications.length > 0) {
                    $('#Product_Specifications > div').first().css("display", "none");
                    $('#Product_Specifications > div:first-child > div > div > div > input').attr("id", "");
                    $('#Product_Specifications > div:first-child > div > div > div > input').attr("name", "");
                }
                for (i = 0; i < result.Item.ProductSpecifications.length; i++) {
                    $('#Product_Specifications').append(`
                        <div data-repeater-item>
                            <div class="row align-items-end">
                                <div class="col-xl-4 col-lg-5 col-md-6">
                                    <div class="form-group">
                                        <label class="custom-label">Highlight label</label>
                                        <input class="form-control" value="${result.Item.ProductSpecifications[i].HighlightsLabel}" id="SpecificationsHighlightLabel" name="SpecificationsHighlightLabel[]" type="text" placeholder="Enter label name" />
                                    </div>
                                </div>
                                <div class="col-xl-4 col-lg-5 col-md-6">
                                    <div class="form-group">
                                        <label class="custom-label">Highlight discription</label>
                                        <input class="form-control" value="${result.Item.ProductSpecifications[i].HighlightsDiscription}" id="SpecificationsHighlightDiscription" name="SpecificationsHighlightDiscription[]" type="text" placeholder="@(BeautyBook_Vendor.Resources.Languages.Enter_discription)" />
                                    </div>
                                </div>
                                <div class="col-xl-1 col-lg-2 col-md-6">
                                    <div class="form-group">
                                        <input class="btn btn-danger" data-repeater-delete type="button" value="Delete" />
                                    </div>
                                </div>
                            </div>

                            <hr>
                        </div>
                    `);
                }


                $('#productformloader').hide();
                $('#productform').show();
            }, error: function (error) {
                // Error function
                 
                $('#productformloader').hide();
                $('#productform').show();
            }
        });
    }

    return false;
}