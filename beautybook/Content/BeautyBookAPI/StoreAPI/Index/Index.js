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

//masterProductType dropdown API call in ajax methos
function ManageStore() {

    $('#manage_Store_loader').show();

    let manageStore = new Object();
    manageStore.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/masterProductType/MasterProductType_All?search&IsAllow=true',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(manageStore),
        crossDomain: true,
        success: function (result) {
             
            console.log(BeautyBookAdminUrl);
            $("#manage_Store").html(``);

            //$('#manage_Store').append(`
            //    <div class="col mb-3">
            //        <a class="card hover-shadow-lg" href="/Store/StoreInside">
            //            <img class="card-img-top" src="https://images.pexels.com/photos/3847489/pexels-photo-3847489.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940" alt="">
            //            <div class="card-body">
            //                <h5 class="mb-0 textline-truncate" title="All">All</h5>
            //            </div>
            //        </a>
            //    </div>
            //`);
            for (i = 0; i < result.Values.length; i++) {
                if (result.Values.length > 0) {
                    $('#manage_Store').append(`
                        <div class="col mb-3">
                            <a class="card hover-shadow-lg"
                                href="${result.Values[i].Name == "All" ? "/Store/StoreInside" : `/Store/StoreInside?typeId=${btoa(result.Values[i].Id)}`}"
                            >
                                <img class="card-img-top" onerror="this.src='/Content/assets/images/DefaultProduct.jpg'" src="${BeautyBookAdminUrl}${result.Values[i].ProductTypeImage}" alt="">
                                <div class="card-body">
                                    <h5 class="mb-0 textline-truncate" title="${result.Values[i].Name}">${result.Values[i].Name}</h5>
                                </div>
                            </a>
                        </div>
                    `);
                }
            }
            

            $('#manage_Store_loader').hide();

        }, error: function (error) {
            // Error function
            $('#manage_Store_loader').hide();
        }
    });
    return false;
}