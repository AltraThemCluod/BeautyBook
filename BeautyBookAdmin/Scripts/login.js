$(document).ready(function () {
    $('#frmLogin').validate({
        errorClass: 'help-block scrollTop', // You can change the animation class for a different entrance animation - check animations page
        errorElement: 'div',
        errorPlacement: function (error, e) {
            e.parents('.field-wrapper').append(error);
        },
        highlight: function (e) {

            $(e).closest('.field-wrapper').removeClass('has-success has-error').addClass('has-error');
            $(e).closest('.help-block').remove();
        },
        success: function (e) {
            e.closest('.field-wrapper').removeClass('has-success has-error');
            e.closest('.help-block').remove();
        },
        rules: {
            'Email': {
                required: true,
                email: true
            },
            'Password': {
                required: true,
                minlength: 6
            }
        },
        messages: {
            'Email': 'Please enter valid email address',
            'Password': {
                required: 'Please provide a password',
                minlength: 'Your password must be at least 6 characters long'
            }
        }
    });
});