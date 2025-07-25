var myClient = {

    init: function () {
        myClient.Event();
        myClient.LoadDetail();
    },

    Event: function () {

    },

    LoadDetail: function () {
        $.ajax({
            url: '/Admin/Home/LoadDetail',
            type: "GET",
            dataType: "json",
            data: {
                CbGiaoDien: "1",
            },
            success: function (response) {
                if (response.status == true) {
                    var lstData = response.lstData;

                    if (lstData != null && lstData.noiDung != null) {
                        $("#seo_1").html(lstData.noiDung);
                    }
                } else {
                    toastr.error(response.message, "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                }
            }
        })
    },
}
myClient.init();