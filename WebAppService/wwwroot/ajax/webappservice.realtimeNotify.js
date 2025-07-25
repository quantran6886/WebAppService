function NotiFication() {
    $.ajax({
        url: '/Admin/Home/GetListNotification',
        type: "GET",
        dataType: "json",
        success: function (response) {
            if (response.status == true) {
                var lstData = response.lstData;
                if (lstData != null) {

                }
            } else {
                toastr.error(response.message, "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            }
        }
    });
};
Noification();
const connectionNoti = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connectionNoti.on("ReceiveNotification", function (message) {
    Noification();
    toastr.error("Bạn có thông báo mới", "info", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
});

connectionNoti.start();