$(document).ready(function () {
    //Prevent Page Reload on all # links
    $("a[href='#']").click(function (e) {
        e.preventDefault();
    });

    //Grid td tooltip
    //$(function () {
    //    setTimeout(function () {
    //        $('table tbody tr td').each(function () {
    //            this.setAttribute('title', $(this).text());
    //        });
    //    }, 1500);
    //});

    //placeholder
    $("[placeholder]").each(function () {
        $(this).attr("data-placeholder", this.placeholder);
        $(this).bind("focus", function () {
            this.placeholder = '';
        });
        $(this).bind("blur", function () {
            this.placeholder = $(this).attr("data-placeholder");
        });
    });

    //First Field Focus
    $(function () {
        setTimeout(function () {
            $(':input:text:enabled:visible:first').focus();
            $('.modal.fade').off('shown.bs.modal').on('shown.bs.modal', function () {
                $(this).find('input:enabled:visible:first').focus();
            });
        }, 200);
    });
});

function addRequestVerificationToken(data) {
    data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
    return data;
}

//function bindSortingArrow() {
//    $(".dataTable thead th").each(function (i, th) {
//        var html = $(th).html(),
//            cls = $(th).attr('class');
//        if (cls.indexOf('sorting_disabled') == -1) {
//            switch (cls) {
//                case 'sorting_asc':
//                    $(th).html(html + spanAsc); break;
//                case 'sorting_desc':
//                    $(th).html(html + spanDesc); break;
//                default:
//                    $(th).html(html + spanSorting); break;
//            }
//        }
//    });
//}

function ShowMessageToastr(type, message, IsConfirmation, YesResponseMethod, NoResponseMethod) {
    // If message is not confirmation type
    if (IsConfirmation != true) {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": true,
            "progressBar": true,
            "positionClass": "toast-top-center",
            "preventDuplicates": false,
            "showDuration": "300",
            "hideDuration": "100",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

        if (type == null || type == 'undefined') {
            type = 'success';
        }

        if (message != null && message != 'undefined' || message != '') {
            if (type.toLowerCase() == 'danger') {
                toastr.error(message);
            }
            else if (type == 'success') {
                toastr.success(message);
            }
            else if (type == 'warning') {
                toastr.warning(message);
            }
            else {
                toastr.info(message);
            }

            $('#toast-container').addClass('nopacity');
        }
    } else {
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": true,
            "progressBar": false,
            "positionClass": "toast-top-center",
            "preventDuplicates": false,
            "showMethod": "fadeIn",
            "timeOut": "0",
            "extendedTimeOut": "0",
            "showEasing": "swing",
            "onclick": null,
            "tapToDismiss": false,
            "hideMethod": 'noop',
            allowHtml: true,
            onShown: function (toast) {
                $("#confirmationRevertYes").click(function () {
                    eval(YesResponseMethod);
                    $('.toast').remove();
                });

                $("#confirmationRevertNo").click(function () {
                    eval(NoResponseMethod);
                    $('.toast').remove();
                });
            }
        }

        if (type == null || type == 'undefined') {
            type = 'success';
        }
        if (message != null && message != 'undefined' || message != '') {
            if (type.toLowerCase() == 'danger') {
                toastr.error(message + "<br /><br /><button type='button' id='confirmationRevertYes' style='margin-left:30px;' class='btn btn-primary btn-md'>Yes</button><button type='button' id='confirmationRevertNo' style='padding-left:15px;margin-left:10px;' class='btn btn-default btn-md'>No</button>");
            }
            else if (type == 'success') {
                toastr.success(message + "<br /><br /><button type='button' id='confirmationRevertYes' style='margin-left:30px;' class='btn btn-primary btn-md'>Yes</button><button type='button' id='confirmationRevertNo' style='padding-left:15px;margin-left:10px;' class='btn btn-default btn-md'>No</button>");
            }
            else if (type == 'warning') {
                toastr.warning(message + "<br /><br /><button type='button' id='confirmationRevertYes' style='margin-left:30px;' class='btn btn-primary btn-md'>Yes</button><button type='button' id='confirmationRevertNo' style='padding-left:15px;margin-left:10px' class='btn btn-default btn-md'>No</button>");
            }
            else {
                toastr.info(message + "<br /><br /><button type='button' id='confirmationRevertYes' style='margin-left:30px;' class='btn btn-primary btn-md'>Yes</button><button type='button' id='confirmationRevertNo' style='padding-left:15px;margin-left:10px' class='btn btn-default btn-md'>No</button>");
            }

            $('#toast-container').addClass('nopacity');
        }
    }
}
