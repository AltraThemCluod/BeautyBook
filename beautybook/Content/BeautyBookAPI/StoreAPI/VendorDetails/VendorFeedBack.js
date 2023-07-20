﻿//getCookie
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
var feedbackOffsetdata = 0;
var feedbackLimitdata = 5;
var feedbackListlength = 0;
function viewMore() {
    if (feedbackListlength > feedbackLimitdata) {
        feedbackOffsetdata += feedbackLimitdata;
        feedBacklist();
    } else {
        $('#viewMorebtn').hide();
    }
}

function feedBacklist() {

    $('#feedBack_listloader').show();

    let FeedBacklist = new Object();
    FeedBacklist.IsPageProvided = true;
    FeedBacklist.Offset = feedbackOffsetdata;
    FeedBacklist.Limit = feedbackLimitdata;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/writeaReview/WriteaReview_All?search=&ProductId=0&VendorId=${getVendorIdatob}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(FeedBacklist),
        crossDomain: true,
        success: function (result) {
             
            if (result.Values.length <= 0) {
                $("#feedBack_list").html(`
                 <div class="border-bottom pb-4 mb-4 text-center">
                    <h6>
                        ${Langaugestore.NoRecordsFound}
                    <h6>
                 </div>
                `);
            } else {

                $('#customerOverview').html(``);
                $('#vendorDetailscard').html(``);

                for (i = 0; i < result.Values.length; i++) {
                    feedbackListlength = result.TotalRecords;
                    $('#feedBack_list').append(`
                        <div class="border-bottom pb-4 mb-4">
                            <h6>
                                <span class="badge badge-info align-items-center mr-2">
                                    <span class="align-middle fs-14">${result.Values[i].RatingNo}</span>
                                    <svg xmlns="http://www.w3.org/2000/svg" height="14" width="14" viewBox="0 0 20 20" fill="currentColor">
                                        <path d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"></path>
                                    </svg>
                                </span>
                                <span>${result.Values[i].Heading}</span>
                            </h6>
                            <p class="text-muted">${result.Values[i].Review}</p>

                            <!-- Reviewer -->
                            <div class="fs-14 text-gray-700">
                                <span class="text-dark font-weight-bold">${result.Values[i].FirtsName} ${result.Values[i].SecondName}</span>
                                <span>- ${result.Values[i].CreatedDateStr}</span>
                            </div>
                        </div>
                    `);
                }
                $('#viewMorebtn').show();
                if (feedbackListlength <= feedbackLimitdata + feedbackOffsetdata) {
                    $('#viewMorebtn').hide();
                }
            }

            $('#customerOverview').append(`
                <div class="card bg-primary text-white p-4 mb-3 sticky-panel-top">
                    <div class="d-block mb-1 display-4">${result.Values[0].CustomersRecommend}%</div>
                    <p class="text-gray-100 mb-0">${Langaugestore.of_customers_recommend_this_product}</p>
                    <hr>
                    <div class="text-gray-100">${result.Values[0].VendorProductTotalRating} ${Langaugestore.Ratings} & ${result.Values[0].VendorProductReviewCount} ${Langaugestore.Reviews}</div>
                </div>
            `);

            //vendor details 
            $('#vendorDetailscard').append(`
                <div class="row justify-content-between">
                    <div class="col-lg-6">
                        <div class="d-flex align-items-start">
                            ${result.Values[0].VendorProfileUrl == "" || result.Values[0].VendorProfileUrl == null ?
                                `<span class="avatar-initials avatar avatar-primary avatar-circle vendor-details-profile">${result.Values[0].VendorFirstname.charAt(0)}${result.Values[0].VendorSecondName.charAt(0)}</span>`
                                :`<img src="${APIEndPoint}/${result.Values[0].VendorProfileUrl}" class="img-fluid rounded border mr-3 vendor-details-profile" alt="" style="width: 5rem;">`
                            }
                            <div class="vendor-basic-details">
                                <h5 class="mb-1">${result.Values[0].VendorFirstname} ${result.Values[0].VendorSecondName}</h5>
                                <div class="text-muted fs-14 mb-2 d-flex align-items-center"><p class="mb-0">${Langaugestore.Seller_since}</p><p class="mb-0 m font-weight-medium text-dark">${result.Values[0].VendorCreatedDateStr}</p></div>
                                <div class="badge badge-info align-items-center">
                                    <span class="align-middle">${result.Values[0].AverageRating}/5</span>
                                    <svg xmlns="http://www.w3.org/2000/svg" height="14" width="14" viewBox="0 0 20 20" fill="currentColor">
                                        <path d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z" />
                                    </svg>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 text-right">
                        <a href="tel:${result.Values[0].VendorPrimaryPhone}" class="btn btn-primary font-weight-medium btn-wide">${Langaugestore.Contact}</a>
                    </div>
                </div>
            `);
            $('#feedBack_listloader').hide();
        }, error: function (error) {
            // Error function
            $('#feedBack_listloader').hide();
        }
    });
    return false;
}