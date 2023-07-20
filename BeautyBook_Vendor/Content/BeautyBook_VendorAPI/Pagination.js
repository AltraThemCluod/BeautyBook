//Html table pagination
var noRowlength;
var pageSize = 5;

function tableRownofun() {
    pageSize = parseInt($('#tableRowno').val());
    $('#pagin').html(``);
    paginationDatalist();
}

function showRownofun() {
    $('#showRowno').append(`
        <label class="d-flex align-items-center custom-label mb-0"> Show
            <select class="selectpicker form-control ml-2" id="tableRowno" onchange="tableRownofun()" title="Show" style="width:15% !important;">
                <option value="5" selected>5</option>
                <option value="10">10</option>
                <option value="15">25</option>
                <option value="20">50</option>
                <option value="20">100</option>
            </select>
        </label>
    `);
}

function paginationDatalist() {
    $('#pagin').html(``);

    var pageCount = noRowlength / pageSize;
    for (var i = 0; i < pageCount; i++) {
        $("#pagin").append('<li class="page-item"><a class="page-link" href="javascript:void(0)">' + (i + 1) + '</a></li> ');
    }

    $("#pagin li").first().find("a").addClass("pagination-current")
    showPage = function (page) {
        $(".line-content").hide();
        $(".line-content").each(function (n) {
            if (n >= pageSize * (page - 1) && n < pageSize * page)
                $(this).show();
        });
    }

    showPage(1);

    $("#pagin li a").click(function () {
        $("#pagin li a").removeClass("pagination-current");
        $(this).addClass("pagination-current");
        showPage(parseInt($(this).text()))
    });
}