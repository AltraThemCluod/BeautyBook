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
var userId = getCookie("UserId");


var getProductId = new URLSearchParams(window.location.search);
getProductId = parseInt(atob(getProductId.get('productId')));
var getProductIdatob = ~~getProductId;

function submitreview() {
    $('#addbtn').hide();
    $('#btnloading').show();

    var WriteReview = new Object();

    WriteReview.RatingNo = $('#ratingofreview').val();
    WriteReview.Heading = $('#heading').val();
    WriteReview.Review = $('#review').val();
    WriteReview.UserId = atob(userId);
    WriteReview.ProductId = getProductIdatob;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/writeaReview/WriteaReview_Upsert',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        crossDomain: true,
        data: JSON.stringify(WriteReview),
        success: function (result) {
            if (result.Code == 200) {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: result.Message,
                    showConfirmButton: false,
                    timer: 3000
                })
            }
            $('#addbtn').show();
            $('#btnloading').hide();
            $("#reviewModal").modal("hide");
            $('#ratingofreview').val(null);
            $('#heading').val('');
            $('#review').val('');
            $('.selectpicker').selectpicker("refresh");
            feedBacklist();
        }, error: function (error) {
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: error.responseJSON.Message,
                showConfirmButton: false,
                timer: 3000
            })
            $('#addbtn').show();
            $('#btnloading').hide();
        }
    });
    return false;
}


//Data load more function
var feedbackOffsetdata = 0;
var feedbackLimitdata = 5;
var feedbackListlength = 0;
var Isloadmore = false;
function viewMore() {
     
    
    if (feedbackListlength > feedbackLimitdata) {
        feedbackOffsetdata += feedbackLimitdata;
        Isloadmore = true;
        feedBacklist();
    } else {
        $('#viewMorebtn').hide();
    }
}

function feedBacklist() {

    $('#manage_Store_loader').show();

    let FeedBacklist = new Object();
    FeedBacklist.IsPageProvided = true;
    FeedBacklist.Offset = feedbackOffsetdata;
    FeedBacklist.Limit = feedbackLimitdata;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/writeaReview/WriteaReview_All?search=&ProductId=${getProductIdatob}&VendorId=0`,
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
                if (Isloadmore == false) {
                    $('#feedBack_list').html(``);
                }
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
            }
            if (feedbackListlength <= feedbackLimitdata + feedbackOffsetdata) {
                $('#viewMorebtn').hide();
            }
            $('#manage_Store_loader').hide();

        }, error: function (error) {
            // Error function
            $('#manage_Store_loader').hide();
        }
    });
    return false;
}