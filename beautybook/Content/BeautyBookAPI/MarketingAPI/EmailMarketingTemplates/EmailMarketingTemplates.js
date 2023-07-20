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

//EmailMarketingList API call in ajax method
function ChooseEmailMarketingTemplates() {
    $('#ChooseEmailloader').show();
     
    var chooseEmailMarketingTemplates = new Object();
    chooseEmailMarketingTemplates.IsPageProvided = true;

    $.ajax({
        type: 'POST',
        url: '' + APIEndPoint + '/api/emailMarketingTemplates/EmailMarketingTemplates_All?search=',
        headers: { 'Content-Type': 'application/json'},
        dataType: 'json',
        data: JSON.stringify(chooseEmailMarketingTemplates),
        crossDomain: true,
        success: function (result) {
            if (result.Values.length == 0) {
                $('#emailTemplatelisting').html(`
                    <div class="p-3 w-100">
                        <div class="card col-lg-12">
                            <div class="card-body text-center" for="EmailTemplate">
                                ${Langaugestore.NoRecordsFound}
                            </div>
                        </div>
                    </div>
                `)
            } else {
                $('#emailTemplatelisting').html(``);
            }

            for (i = 0; i < result.Values.length; i++) {
                $('#emailTemplatelisting').append(`
                    <div class="col-lg-2 p-2 selecet-services-group">
                        <input type="radio" class="EmailTemplate" onchange="ChooseEmailMarketingTemplatesButton(${result.Values[i].Id});" name="EmailTemplate" id="EmailTemplate${result.Values[i].Id}" value="${result.Values[i].Id}" style="display:none;"/>
                        <div class="card shadow-1 hover-shadow-lg h-100 ChooseEmail" id="ChooseEmail${result.Values[i].Id}">
                            <label class="card-body mb-0" for="EmailTemplate${result.Values[i].Id}">
                                <img class="img-fluid mb-3" src="http://salonadmin.soboft.com/${result.Values[i].Image}" alt=""
                                    style="height:300px;width:100%;object-fit:cover;border-radius:10px;"
                                >
                                <h5 class="card-title mb-0 text-center mr-auto">${result.Values[i].Name}</h5>
                            </label>
                        </div>
                    </div>
                `);
            }
            $('#ChooseEmailloader').hide();
        }, error: function (error) {
             
            $('#ChooseEmailloader').hide();
        }
    });
    return false;
}

//ChooseEmailMarketingTemplatesButton disable 
function ChooseEmailMarketingTemplatesButton(templateId) {
     
    var ChooseEmailMarketingTemplatesVal = $('input[name=EmailTemplate]:checked').val()

    $('.ChooseEmail').removeClass('active')

    $(`#ChooseEmail${parseInt(templateId)}`).addClass('active')

   
}

//NextStepEmailTemplate in template editor
function NextStepEmailTemplate() {
    window.open(`/Marketing/EmailTemplateEditor?TemplateId=${btoa($('input[name=EmailTemplate]:checked').val())}`, '_blank');
}


//ChooseEmailTemplate modal open
function ChooseEmailTemplate() {
    $('#chooseEmailTemplate').modal('show');
}