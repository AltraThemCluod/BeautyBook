

function CoinsAmount() {
    var dataLoad = new Object();
    
    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/masterPOSCoinsBills/MasterPOSCoinsBills_All?search',
        headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
        dataType: 'json',
        data: JSON.stringify(dataLoad),
        success: function (result) {
            console.log(result);
            for (i = 0; i < result.Values.length; i++) {
                $('#CoinsAmount').append(`
                        <div class="col-lg-4 p-1 text-center d-flex" style="align-items: center;" id="CoinsCash">
                        <label class="mb-0" style="font-weight:500;width: 100%;">SAR ${result.Values[i].Amount}</label>
                        <div class="d-flex align-items-center">
                            <input type="number" id="amount_${i}" data-coinsbillid-${i}="${result.Values[i].Id}" value="0" class="form-control coin-cash">
                            <div class="count-arrow">
                                <i class="filter-arrow bb-chevron-up" onclick="PlusMinusAmount(${i},true,${result.Values[i].Amount})"></i>
                                <i class="filter-arrow bb-chevron-down" onclick="PlusMinusAmount(${i},false,${result.Values[i].Amount})"></i>
                            </div>
                        </div>
                    </div>
                `);
            }
        },
        error: function (error) {
            // Error function
        }
    });
    return false;

}
