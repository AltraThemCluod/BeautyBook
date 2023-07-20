(function ($) {
    "use strict";
    var BeautyBook = {};

    /*************************
      Predefined Variables
    *************************/

    var $window = $(window),
        $document = $(document)


    // Check if function exists
    $.fn.exists = function () {
        return this.length > 0;
    };

    // BeautyBook.aos = function () {

    //   loadScript(plugin_path + 'aos/aos.js', function () {
    //   });

    // };   

    // TOGGLE PASSWORD
    //BeautyBook.togglePassword = function () {
    //    if ($('.js-toggle-password').exists()) {
    //        $(document).on('click', '.Oldpassword-icon', function () {
    //            $(this).toggleClass("bb-eye bb-eye-off");
    //            
    //            var input = $(".js-toggle-password");
    //            input.attr('type') === 'password' ? input.attr('type', 'text') : input.attr('type', 'password')
    //        });

    //    }
    //};




    $('#passwordCheckbox').change(function () {
        if ($(this).is(':checked')) {
            $('input[type="password"]').addClass('password-toggle');
            $('input[type="password"]').prop('type', 'text');
        } else {
            $('.password-toggle').prop('type', 'password');
        }
    });

    $(".toggle-password").click(function () {
        $(this).toggleClass("bb-eye bb-eye-off");
        var input = $($(this).attr("toggle"));
        if (input.attr("type") == "password") {
            input.attr("type", "text");
        } else {
            input.attr("type", "password");
        }
    });

    BeautyBook.togglePassword = function () {
        if ($('.js-toggle-password').exists()) {

            $(document).on('click', '.Newpassword-icon', function () {
                $(this).toggleClass("bb-eye bb-eye-off");

                var input = $(".js-toggle-password");
                input.attr('type') === 'password' ? input.attr('type', 'text') : input.attr('type', 'password')
            });
        }
    };

    // TOOLTIP
    BeautyBook.toggleTooltip = function () {
        if ($('[data-toggle="tooltip"]').exists()) {
            $('[data-toggle="tooltip"]').tooltip()
        }
    };

    // SELECT SERVICES
    BeautyBook.selectServices = function () {
        if ($('.selecet-services-group').exists()) {
            $(".selecet-services-group .card").on("click", function () {
                $(this).toggleClass("active");
            });
        }
    };

    // SWEET ALERT
    BeautyBook.addEmployeeSA = function () {
        if ($('#addEmployee').exists()) {
            loadScript(plugin_path + 'sweetalert/sweetalert2.min.js', function () {

                $("#addEmployee").click(function () {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Employee Added Successfully',
                        showConfirmButton: false,
                        timer: 1200
                    })
                });
            });
        }
    };

    // BOOTSTRAP TOGGLE
    BeautyBook.bootstrapToggle = function () {
        if ($("[data-toggle='toggle']").exists()) {
            loadScript(plugin_path + 'bootstrap-switch/bootstrap4-toggle.min.js', function () {
                $("[data-toggle='toggle']").bootstrapToggle();
            });
        }
    };

    BeautyBook.summerNote = function () {

        if ($('#emailtemplate').exists()) {
            loadScript(plugin_path + 'summernote/summernote.min.js', function () {
                $('#emailtemplate').summernote({
                    height: 300
                });
            });
        }
        if ($('#EditEmailTemplate').exists()) {
            loadScript(plugin_path + 'summernote/summernote.min.js', function () {
                $('#EditEmailTemplate').summernote({
                    height: 300
                });
            });
        }
        if ($('#TechnicalSupportDiscription').exists()) {
            loadScript(plugin_path + 'summernote/summernote.min.js', function () {
                $('#TechnicalSupportDiscription').summernote({
                    height: 300
                });
            });
        }
    };

    // SIMPLE BAR
    BeautyBook.simpleBar = function () {
        if ($('[data-simplebar]').exists()) {
            loadScript(plugin_path + 'simplebar/simplebar.min.js', function () { });
        }
    };

    // SVG INJECT
    BeautyBook.svgInject = function () {
        if ($('[data-inject]').exists()) {
            loadScript(plugin_path + 'simplebar/simplebar.min.js', function () {
                SVGInject(document.querySelectorAll("[data-inject]"));
            });
        }
    };

    // BOOTSTRAP SELECT
    BeautyBook.bootstrapSelect = function () {
        if ($('.selectpicker').exists()) {
            loadScript(plugin_path + 'bootstrap-select/bootstrap-select.min.js', function () {
                $('.selectpicker').selectpicker();
            });
        }
    };

    // DATE RANGE PICKER
    BeautyBook.dateRangePicker = function () {
        if ($('.datepicker').exists()) {
            loadScript(plugin_path + 'daterangepicker/daterangepicker.js', function () {
                $('input.dateRangePicker').daterangepicker({
                    "singleDatePicker": true,
                    autoUpdateInput: false,
                    "showDropdowns": true,
                    locale: {
                        cancelLabel: 'Clear'
                    }
                });

                $('input.singledatepicker').daterangepicker({
                    "singleDatePicker": true,
                    autoUpdateInput: false,
                    "showDropdowns": true,
                    locale: {
                        cancelLabel: 'Clear'
                    }
                });

                $('input.singledatepicker').attr("placeholder", "mm/dd/yyyy");
                $('input.singledatepicker').on('apply.daterangepicker', function (ev, picker) {

                    $(this).val(picker.startDate.format('MM/DD/YYYY'));
                    $('input.singledatepicker').trigger('keyup');
                });

                $('input.singledatepicker').on('cancel.daterangepicker', function (ev, picker) {
                    $(this).val('');
                });


                $('input.previousdatepickerAppoinment').daterangepicker({
                    "singleDatePicker": true,
                    autoUpdateInput: false,
                    minDate: new Date(),
                    locale: {
                        cancelLabel: 'Clear'
                    }
                });
                $('input.previousdatepickerAppoinment').attr("placeholder", "mm/dd/yyyy");
                $('input.previousdatepickerAppoinment').on('apply.daterangepicker', function (ev, picker) {

                    $(this).val(picker.startDate.format('MM/DD/YYYY'));
                    $('input.previousdatepickerAppoinment').trigger('keyup');
                });

                $('input.previousdatepickerAppoinment').on('cancel.daterangepicker', function (ev, picker) {
                    $(this).val('');
                });

                $('input.previousdatepicker').daterangepicker({
                    "previousdatepicker": true,
                    "showDropdowns": true,
                    autoUpdateInput: false,
                    minDate: new Date(),
                    locale: {
                        cancelLabel: 'Clear'
                    }
                });


                $('input.previousdatepicker').attr("placeholder", "mm/dd/yyyy-mm/dd/yyyy");
                $('input.previousdatepicker').on('apply.daterangepicker', function (ev, picker) {
                    $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
                    $('input.previousdatepicker').trigger('keyup');
                });

                $('input.previousdatepicker').on('cancel.daterangepicker', function (ev, picker) {
                    $(this).val('');
                });

                //Customer since
                $('input.CustomerSinceDatePicker').daterangepicker({
                    "previousdatepicker": true,
                    "showDropdowns": true,
                    autoUpdateInput: false,
                    //minDate: new Date(),
                    locale: {
                        cancelLabel: 'Clear'
                    }
                });


                $('input.CustomerSinceDatePicker').attr("placeholder", "mm/dd/yyyy-mm/dd/yyyy");
                $('input.CustomerSinceDatePicker').on('apply.daterangepicker', function (ev, picker) {
                    $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
                    $('input.CustomerSinceDatePicker').trigger('keyup');
                });

                $('input.CustomerSinceDatePicker').on('cancel.daterangepicker', function (ev, picker) {
                    $(this).val('');
                });

                $('input.dateRangePicker').daterangepicker({
                    "showDropdowns": true,
                });

            });
        }
    };
    // TAGIFY
    BeautyBook.customerTagify = function () {
        if ($('.customer-tags').exists()) {
            loadScript(plugin_path + 'tagify/tagify.min.js', function () {
                var input = document.querySelector('input[name=customer-tags]');
                new Tagify(input)
            });
        }
    };

    // TOUCHSPIN
    BeautyBook.subtractTouchSpin = function () {
        if ($('.subtract-quantity').exists()) {
            loadScript(plugin_path + 'bootstrap-touchspin/jquery.bootstrap-touchspin.min.js', function () {
                $("input[name='subtract-quantity']").TouchSpin({
                    min: 0,
                    max: 1000000000,
                    step: 1,
                    boostat: 5,
                    maxboostedstep: 10,
                });
            });
        }
    };

    BeautyBook.productTouchSpin = function () {
        if ($('.product-quantity').exists()) {
            loadScript(plugin_path + 'bootstrap-touchspin/jquery.bootstrap-touchspin.min.js', function () {
                $("input[name='product-quantity']").TouchSpin({
                    min: 0,
                    max: 1000000000,
                    step: 1,
                    boostat: 5,
                    maxboostedstep: 10,
                });
            });
        }
    };



    // PRODUCT SLIDER
    BeautyBook.productSlider = function () {
        if ($('.slider-for').exists()) {
            loadScript(plugin_path + 'slick/slick.min.js', function () {

                if ($("html").attr("dir") == "rtl") {
                    $('.slider-for').slick({
                        slidesToShow: 1,
                        slidesToScroll: 1,
                        rtl: true,
                        arrows: false,
                        fade: true,
                        asNavFor: '.slider-nav'
                    });

                    $('.slider-nav').slick({
                        slidesToShow: 3,
                        slidesToScroll: 1,
                        asNavFor: '.slider-for',
                        rtl: true,
                        dots: true,
                        centerMode: true,
                        focusOnSelect: true
                    });
                } else {
                    $('.slider-for').slick({
                        slidesToShow: 1,
                        slidesToScroll: 1,
                        ltl: true,
                        arrows: false,
                        fade: true,
                        asNavFor: '.slider-nav'
                    });

                    $('.slider-nav').slick({
                        slidesToShow: 3,
                        slidesToScroll: 1,
                        asNavFor: '.slider-for',
                        ltl: true,
                        dots: true,
                        centerMode: true,
                        focusOnSelect: true
                    });
                }

                // keeps thumbnails active when changing main image, via mouse/touch drag/swipe
                $('.slider-for').on('afterChange', function (event, slick, currentSlide, nextSlide) {
                    //remove all active class
                    $('.slider-nav .slick-slide').removeClass('slick-current');
                    //set active class for current slide
                    $('.slider-nav .slick-slide:not(.slick-cloned)').eq(currentSlide).addClass('slick-current');
                });
            });
        }
    };


    // SWITCH TRIGGER TARGET DIV HIDE & SHOW
    BeautyBook.switchTriggerControl = function () {
        if ($('[data-switch-trigger]').exists()) {
            $('[data-switch-trigger]').change(function () {
                var hiddenId = $(this).attr("data-trigger");
                if ($(this).is(':checked')) {
                    $("#" + hiddenId).show();
                } else {
                    $("#" + hiddenId).hide();
                }
            });

        }
    };

    // MULTI SELECT CHECKBOX
    BeautyBook.dataGroupCheckbox = function () {
        if ($('[data-group]').exists()) {
            $('.child').change(function () {
                // create var for parent .checkall and group
                var group = $(this).data('group'),
                    checkall = $('.selectall[data-group="' + group + '"]');

                // do we have some checked? Some unchecked? Store as boolean variables
                var someChecked = $('.child[data-group="' + group + '"]:checkbox:checked').length > 0;
                var someUnchecked = $('.child[data-group="' + group + '"]:checkbox:not(:checked)').length > 0;

                // if we have some checked and unchecked, set .checkall, of the correct group, to indeterminate. 
                // If all are checked, set .checkall to checked
                checkall.prop("indeterminate", someChecked && someUnchecked);
                checkall.prop("checked", someChecked || !someUnchecked);

                // fire change() when this loads to ensure states are updated on page load
            }).change();

            // clicking .checkall will check all children in the same group.
            $('.selectall').click(function () {
                var group = $(this).data('group');
                $('.child[data-group="' + group + '"]').prop('checked', this.checked).change();
            });

        }
    };




    /****************************************************
    Window load and functions
    ****************************************************/
    var _arr = {};

    function loadScript(scriptName, callback) {
        if (!_arr[scriptName]) {
            _arr[scriptName] = true;
            var body = document.getElementsByTagName('body')[0];
            var script = document.createElement('script');
            script.type = 'text/javascript';
            script.src = scriptName;
            script.onload = callback;
            body.appendChild(script);
        } else if (callback) {
            callback();
        }
    };

    //Window load functions
    //  $window.on("load", function () {
    //    BeautyBook.preloader()
    //  });

    //Document ready functions
    $document.ready(function () {
        BeautyBook.togglePassword();
        BeautyBook.selectServices();
        BeautyBook.addEmployeeSA();
        BeautyBook.bootstrapToggle();
        BeautyBook.simpleBar();
        BeautyBook.svgInject();
        BeautyBook.switchTriggerControl();
        BeautyBook.bootstrapSelect();
        BeautyBook.toggleTooltip();
        BeautyBook.dateRangePicker();
        BeautyBook.dataGroupCheckbox();
        BeautyBook.customerTagify();
        BeautyBook.subtractTouchSpin();
        BeautyBook.productTouchSpin();
        BeautyBook.productSlider();
        BeautyBook.summerNote();
    });

})(jQuery);







/*--------------*/



$(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $('[data-tooltip="tooltip"]').tooltip();
})

