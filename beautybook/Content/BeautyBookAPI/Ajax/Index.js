

function dataLoad() {

    var dataLoad = new Object();
    
    $.ajax({
            type: 'POST',
            url: '' + APIEndPoint + '/api/lookUpCountry/LookUpCountry_All?search',
            headers: { 'Content-Type': 'application/json', "Authorization": '' + DeviceTokenNumber + '' },
            dataType: 'json',
            data: JSON.stringify(dataLoad),

            success: function (result) {
                console.log(result);

                for (i = 0; i < result.Values.length; i++) {
                    $('#countryData').append(`
                        <p>${result.Values[i].Id}${result.Values[i].Name}</p>
                    `);
                }
            },
            error: function (error) {
                // Error function
            }
        });
    return false;

}
