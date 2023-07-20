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

//setCookie
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

var DeviceTokenNumber = getCookie("DeviceToken&Type");
var SalonId = getCookie("SalonId");


var ServiceDetailsArray = [];

function PosOrderSession(serviceImageUrl, type, checkbox, evId, LookUpServicesName, Price) {
    debugger;
    var detailsObject = {
        ServiceImageUrl: serviceImageUrl,
        PosType: type,
        ServiceId: evId,
        CategoryId: 0,
        LookUpServicesName: LookUpServicesName,
        AssignToUser : 0,
        Price: Price
    }

    if (checkbox.checked == true) {
        ServiceDetailsArray.push(detailsObject);

        if (type != 2) {
            CalculatePaymentAmount(detailsObject.Price, true);
        }

        updateOrderDetails();
    }
    else if (checkbox.checked == false) {

        if (type != 2) {
            CalculatePaymentAmount(detailsObject.Price, false);
        }

        var Index = ServiceDetailsArray.findIndex(x => x.PosType === detailsObject.PosType && x.ServiceId === detailsObject.ServiceId);

        ServiceDetailsArray.splice(Index, 1);

        updateOrderDetails();
    }

    BindOrderDetails(true);
}

function BindOrderDetails(isAddedd) {

    var ServiceDetailsCokkieArray = getCookie("ServiceDetailsCokkieArray");

    $("#posOrderDetails").html("");

    if (ServiceDetailsCokkieArray != undefined && ServiceDetailsCokkieArray != "" && ServiceDetailsCokkieArray != null)
    {
        ServiceDetailsCokkieArray = JSON.parse(ServiceDetailsCokkieArray);

        if (ServiceDetailsCokkieArray.length > 0) {

            ServiceDetailsArray = ServiceDetailsCokkieArray;

            //Orderdetails list length > 3 add and remove scroll class
            if (ServiceDetailsCokkieArray.length > 3)
            {
                $("#posOrderDetails").addClass("pos-orderdetails");
            }
            else
            {
                $("#posOrderDetails").removeClass("pos-orderdetails");
            }

            for (var i = 0; i < ServiceDetailsCokkieArray.length; i++) {
                $("#posOrderDetails").append(`
                    <div class="card_box_profile_card d-flex mt-3 position-relative m-2 align-items-center">
                        <div class="img_box">
                            <img onerror="this.src='/Content/assets/images/DefaultProduct.jpg'" src="${ServiceDetailsCokkieArray[i].ServiceImageUrl}" alt="Card Image" width="100" height="100" class="img-fluid object-cover-h-100 rounded">
                        </div>
                        <div class="content_box_card" style="width:60%;">
                            <div>
                                <span class="profile_title text-truncate cursor-pointer" title="${ServiceDetailsCokkieArray[i].LookUpServicesName}">
                                    ${ServiceDetailsCokkieArray[i].LookUpServicesName}
                                </span>
                                <div class="price text-left fs-14">
                                    ${Langaugestore.Price} : ${ServiceDetailsCokkieArray[i].Price} SAR
                                </div>
                                <select class="form-control" id="orderDetailsAssignto_${i}" onchange="updateAssignedUser(this,${ServiceDetailsArray[i].ServiceId} , ${ServiceDetailsArray[i].PosType})">
                                    <option value="0">${Langaugestore.Select_a_assigned_user}</option>
                                </select>
                            </div>
                            <!-- <img src="../Content/assets/images/Mask.png" alt="Icon" class="img-fluid order-details-remove"> -->
                        </div>
                    </div>
                `)

                //Assign employee drop down function call
                AssignEmployee(i);

                if (isAddedd != true)
                {
                    CalculatePaymentAmount(ServiceDetailsCokkieArray[i].Price, true);
                }

                $("#orderDetailsAssignto_" + i).val(ServiceDetailsCokkieArray[i].AssignToUser);
            }
        }
        else {
            $("#posOrderDetails").append(`<h5 class="mb-0 text-muted text-center p-3">${Langaugestore.Order_details_not_found}</h5>`)
        }
    }
    else {
        $("#posOrderDetails").append(`<h5 class="mb-0 text-muted text-center p-3">${Langaugestore.Order_details_not_found}</h5>`)
    }

}

function AssignEmployee(evId)
{
    var CustomerData = new Object();
    CustomerData.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/pOSDetails/POSAssignEmployee_All?LookUpUserTypeId=3&SalonId=' + atob(SalonId) + '',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(CustomerData),
        crossDomain: true,
        async: false,
        success: function (result) {
            $("#orderDetailsAssignto_" + evId).html(``);

            $('#orderDetailsAssignto_' + evId).append(`<option value="0">${Langaugestore.Select_a_assigned_user}</option>`);
            for (i = 0; i < result.Values.length; i++) {
                $('#orderDetailsAssignto_' + evId).append(`
                    <option value="${result.Values[i].Id}">${result.Values[i].UserName}</option>
                `);
                $('.selectpicker').selectpicker("refresh");
            }
        }, error: function (error) {
            // Error function
        }
    });
    return false;
}


//update AssignedUser
function updateAssignedUser(event,sid,type)
{
    if (parseInt(event.value) > 0) {
        var Index = ServiceDetailsArray.findIndex(x => x.PosType === type && x.ServiceId === sid);

        ServiceDetailsArray[Index].AssignToUser = parseInt(event.value);

        updateOrderDetails();
    }
}

//update orderdetails data in cookie
function updateOrderDetails() {
    debugger;
    setCookie("ServiceDetailsCokkieArray", JSON.stringify(ServiceDetailsArray), 30);
}

//checked Allready Service
function checkedAllreadyService(type) {
    
    var ServiceDetailsCokkieArray = getCookie("ServiceDetailsCokkieArray");

    if (ServiceDetailsCokkieArray != undefined && ServiceDetailsCokkieArray != "" && ServiceDetailsCokkieArray != null) {
        ServiceDetailsCokkieArray = JSON.parse(ServiceDetailsCokkieArray);

        if (ServiceDetailsCokkieArray.length > 0) {

            ServiceDetailsArray = ServiceDetailsCokkieArray;

            for (var i = 0; i < ServiceDetailsCokkieArray.length; i++)
            {
                if (type == 1)
                {
                    var targetImageId = "#salonServiceCheckbox_" + ServiceDetailsCokkieArray[i].PosType + "_" + ServiceDetailsCokkieArray[i].ServiceId + "";
                    $(targetImageId).addClass("image-checkbox-checked");

                    var targetId = "#salonService_" + ServiceDetailsCokkieArray[i].PosType + "_" + ServiceDetailsCokkieArray[i].ServiceId + "";
                    $(targetId).prop("checked", true);
                }

                if (type == 2)
                {
                    var targetImageId = "#packageServiceCheckbox_" + ServiceDetailsCokkieArray[i].PosType + "_" + ServiceDetailsCokkieArray[i].ServiceId + "";
                    $(targetImageId).addClass("image-checkbox-checked");
                    
                    var targetId = "#packageService_" + ServiceDetailsCokkieArray[i].PosType + "_" + ServiceDetailsCokkieArray[i].ServiceId + "";
                    $(targetId).prop("checked", true);
                }

                if (type == 3)
                {
                    var targetImageId = "#offerServiceCheckbox_" + ServiceDetailsCokkieArray[i].PosType + "_" + ServiceDetailsCokkieArray[i].ServiceId + "";
                    $(targetImageId).addClass("image-checkbox-checked");

                    var targetId = "#offerService_" + ServiceDetailsCokkieArray[i].PosType + "_" + ServiceDetailsCokkieArray[i].ServiceId + "";
                    $(targetId).prop("checked", true);
                }
            }
        }
    }
}


//calculate payment amount
$("#DiscountSales").focusout(function () {
    CalculatePaymentAmount(0, true, this);
});


var ServiceAmount = 0;
var ServiceAmountSalesTax = 0;
var Total = 0;
function CalculatePaymentAmount(amount, event , dsamount , type)
{

    debugger;

    if (type == 2) {
        $("#posSubTotal").text(0);
        $("#posSubTotalinp").val(0);
        $("#posSubTotalSalesTax").text(0);
        $("#posSubTotalSalesTaxinp").val(0);
        $("#posTotal").text(0);
        $("#posTotalinp").val(0);
        ServiceAmount = 0;
        ServiceAmountSalesTax = 0;
        Total = 0;
    }
    
    if (event == true) {
        //Sub total
        ServiceAmount = ServiceAmount + amount;
        $("#posSubTotal").text(ServiceAmount.toFixed(2));
        $("#posSubTotalinp").val(ServiceAmount);

        var ServiceAmountDiscount = ServiceAmount;
        if (dsamount != undefined && dsamount.value != null && dsamount.value != "") {
            ServiceAmountDiscount = (ServiceAmount - (ServiceAmount / 100 * parseInt(dsamount.value)));
        }

        //Sub total 15 % tax
        ServiceAmountSalesTax = ServiceAmountDiscount / 100 * 15;
        $("#posSubTotalSalesTax").text(ServiceAmountSalesTax.toFixed(2));
        $("#posSubTotalSalesTaxinp").val(ServiceAmountSalesTax);

        //Sub total + Sub total 15 % tax
        Total = ServiceAmountDiscount + ServiceAmountSalesTax;
        $("#posTotal").text(Total.toFixed(2));
        $("#posTotalinp").val(Total);
    }
    else if (event == false) {
        //Sub total
        ServiceAmount = ServiceAmount - amount;
        $("#posSubTotal").text(ServiceAmount.toFixed(2));
        $("#posSubTotalinp").val(ServiceAmount);

        //Sub total 15 % tax
        ServiceAmountSalesTax = ServiceAmount / 100 * 15;
        $("#posSubTotalSalesTax").text(ServiceAmountSalesTax.toFixed(2));
        $("#posSubTotalSalesTaxinp").val(ServiceAmountSalesTax);

        //Sub total + Sub total 15 % tax
        Total = ServiceAmount + ServiceAmountSalesTax;
        $("#posTotal").text(Total.toFixed(2));
        $("#posTotalinp").val(Total);
    }
}