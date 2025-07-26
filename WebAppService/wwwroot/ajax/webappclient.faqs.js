var myClient = {
    currentPage: 1,
    pageSize: 16,

    init: function () {
        myClient.Event();
        myClient.LoadData();
    },

    Event: function () {
        $(document).on('click', '.page-link', function () {
            var page = $(this).data("page");
            myClient.currentPage = page;
            myClient.LoadData();
        });
    },

    LoadData: function () {
        $.ajax({
            url: '/FAQ/LoadData',
            type: "GET",
            data: {
                page: myClient.currentPage,
                pageSize: myClient.pageSize
            },
            dataType: "json",
            success: function (data) {
                if (data.status == true) {

                    if (data.lstData != null) {
                        var template = doT.template($("#news-template").html());
                        $("#list_bai_viet").html(template({ newsList: data.lstData }));
                    }

                    // Phân trang
                    var totalPages = Math.ceil(data.totalRow / myClient.pageSize);
                    myClient.RenderPagination(totalPages);
                }
            }
        });
    },

    RenderPagination: function (totalPages) {
        var pagination = '';
        for (var i = 1; i <= totalPages; i++) {
            pagination += '<li class="page-item ' + (i === myClient.currentPage ? 'active' : '') + '">' +
                '<a class="page-link" href="javascript:void(0)" data-page="' + i + '">' + i + '</a></li>';
        }
        $("#pagination").html('<ul class="pagination justify-content-center">' + pagination + '</ul>');
    },

}
myClient.init();
