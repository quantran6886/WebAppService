const connectionNoti = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connectionNoti.on("ReceiveNotification", function (message) {
    Swal.fire({
        icon: 'info',
        title: 'Thông báo',
        text: message
    });
});

connectionNoti.start();