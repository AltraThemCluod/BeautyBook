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
var SalonId = getCookie("SalonId");
var UserId = getCookie("UserId");


//Salon service list api call
function getsalonservicedata() {
    $('#loader').show();

    var SalonServicesData = new Object();
    SalonServicesData.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/lookUpServices/LookUpServices_AllServices?SalonId=${atob(SalonId)}&PackageId=0`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(SalonServicesData),
        crossDomain: true,
        async:false,
        success: function (result) {

            $('#loader').hide();

            $("#salonserviceslist").html(`
                <thead>
                    <tr>
                        <th scope="col" class="bg-primary text-white w-lg-60">${Langaugestore.Service_Name}</th>
                        <th scope="col" class="bg-primary text-white">${Langaugestore.Original_Price}</th>
                        <th scope="col" class="bg-primary text-white">${Langaugestore.Price}</th>
                    </tr>
                </thead>
            `);

            if (result.Values.length <= 0) {
                $('#PackagesGrid').html(`
                    <tr>
                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
                    </tr>
                `)
            } else {
                $("#PackagesGrid").html(``);
            }

            for (i = 0; i < result.Values.length; i++) {
                var abc = (result.Values[i].ParentId)
                if (abc == 0) {
                    $('#salonserviceslist').append(`
                        <tbody>
                            <tr>
                                <th scope="row" class="bg-gray-100" colspan="3">
                                    <label class="m-0" for="${result.Values[i].Id}">${result.Values[i].Name}</label>   
                                </th>
                            </tr>
                        </tbody>
                    `);
                }
                if (abc = result.Values[i].ParentId) {
                    $('#salonserviceslist').append(`
                        <tbody>
                            <tr>
                                <th scope="row" class="font-weight-normal">
                                    <label class="mb-0 ml-5" for="${result.Values[i].Id}">${result.Values[i].Name}</label>
                                </th>
                                <td data-price-id="${result.Values[i].Id}">SAR ${result.Values[i].Price}</td>
                                <td><input data-service="${result.Values[i].Id}" type="text" id="offerprice${result.Values[i].Id}" class="form-control selectedvalue" placeholder="00"></td>
                            </tr>
                        </tbody>
                    `);
                }
            }

            
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}


//Salon service packages api call
function packageOfferPrice() {
    
    var PackageOfferPrice = new Object();
    PackageOfferPrice.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: APIEndPoint + `/api/servicePackage/ServicePackage_All?search&SalonId=${atob(SalonId)}`,
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(PackageOfferPrice),
        crossDomain: true,
        async: false,
        success: function (result) {
            
            console.log('result', result);

            if (result.Values.length <= 0) {
                $('#PackagesGrid').html(`
                    <tr>
                        <td colspan="100" class="text-center" style="font-size:18px;font-weight:500;">${Langaugestore.NoRecordsFound}</td>
                    </tr>
                `)
            } else {
                $("#PackagesGrid").html(``);
            }

            for (i = 0; i < result.Values.length; i++) {
                $('#PackagesGrid').append(`
                    <tr>
                        <th scope="row" class="font-weight-normal">${result.Values[i].PackageName}</th>
                        <td>SAR ${result.Values[i].CustomPrice}</td>
                        <td><input data-service="${result.Values[i].Id}" type="text" id="PackageOffer${result.Values[i].Id}" class="form-control PackageOffersPrice" placeholder="00"></td>
                    </tr>
                `);
            }

        }, error: function (error) {
            // Error function
        }
    });
    return false;
}