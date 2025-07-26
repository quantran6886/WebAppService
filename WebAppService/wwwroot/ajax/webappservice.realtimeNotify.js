function NotiFication() {
    $.ajax({
        url: '/Admin/Home/GetListNotification',
        type: "GET",
        dataType: "json",
        success: function (response) {
            if (response.status == true) {
                var data = response.lstData;
                if (data.length > 0) {
                    var template = doT.template($("#news-template").html());
                    $("#message_noti").html(template({ newsList: data }));
                }
            } else {
                toastr.error(response.message, "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            }
        }
    });
};
const connectionNoti = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connectionNoti.on("ReceiveNotification", function (message) {
    toastr.info("Bạn có thông báo mới từ " + message, "info", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 3000 });
    NotiFication();
});

connectionNoti.start();
NotiFication();
