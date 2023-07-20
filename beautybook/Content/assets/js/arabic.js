$(document).ready(function () {
    debugger;
    $('.arabic-lan .navbar-collapse .rtl-drop').addClass('dropright');
    $('.arabic-lan #salonsServiceListing_length').addClass('text-left-to-right');
    $('.arabic-lan #salonsServiceListing_filter').addClass('text-right-to-left');
    $('.arabic-lan .app-content-title > div .mr-auto').addClass('ml-auto');
    $('.arabic-lan .app-content-title .mr-auto').removeClass('mr-auto');
    $('.arabic-lan .app-content-title div a i').addClass('bb-arrow-right ml-3');
    $('.arabic-lan .app-content-title div a i').removeClass('bb-arrow-left mr-3');
    $('.arabic-lan .side-drp').addClass('mr-auto');
    $('.arabic-lan .side-drp').removeClass('ml-auto');
    $('.arabic-lan-signin form .input-label-secondary').addClass('float-left');
    $('.arabic-lan-signin form .input-label-secondary').removeClass('float-right');
    $('.arabic-lan #invoiceAddressDetails .invoice-payto').attr('dir','auto');
    $('.arabic-lan #invoiceAddressDetails .invoice-invoicedto').attr('dir', 'auto');
    $('.arabic-lan-EmailVerification #ResetPasswordEmail div a i').addClass('bb-chevron-right');
    $('.arabic-lan-EmailVerification #ResetPasswordEmail div a i').removeClass('bb-chevron-left');
});

function Arrow() {
    $('.arabic-lan #changePassword div a i').addClass('bb-chevron-right');
    $('.arabic-lan #changePassword div a i').removeClass('bb-chevron-left');
}

function dirAuto() {
    $('.arabic-lan #invoiceAddressDetails .invoice-payto').attr('dir', 'auto');
    $('.arabic-lan #invoiceAddressDetails .invoice-invoicedto').attr('dir', 'auto');
}