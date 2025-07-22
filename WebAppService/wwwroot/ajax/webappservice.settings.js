function ServiceLogout() {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "btn bg-secondary-subtle text-secondary",
            cancelButton: "me-6 btn bg-secondary-subtle text-secondary",
        },
        buttonsStyling: false,
    });

    swalWithBootstrapButtons
        .fire({
            title: "Thông báo?",
            text: "Bạn có chắc chắn muốn đăng xuất không?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Đăng xuất",
            cancelButtonText: "Hủy",
            reverseButtons: true,
        })
        .then((result) => {
            if (result.isConfirmed) {
                // Lấy token từ form (nếu có)
                $.ajax({
                    type: "POST",
                    url: "/Admin/Login/Logout",
                    data: {},
                    success: function (response) {
                        Swal.fire({
                            title: "Đăng xuất thành công!",
                            text: "Bạn sẽ được chuyển về trang đăng nhập.",
                            icon: "success",
                            timer: 2000,
                            showConfirmButton: false
                        }).then(() => {
                            window.location.href = "/Admin/Login/Login";
                        });
                    },
                    error: function () {
                        Swal.fire({
                            title: "Lỗi!",
                            text: "Không thể đăng xuất. Vui lòng thử lại.",
                            icon: "error",
                        });
                    }
                });
            }
        });
}

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/userStatusHub")
    .build();

connection.start();

// Khi user online
connection.on("UserOnline", function (userId, ip, computer) {
    $.ajax({
        url: '/Admin/UserStatus/LoadData',
        type: "GET",
        dataType: "json",
        data: {
            userId: userId,
        },
        success: function (response) {
            if (response.status == true) {
                var lstData = response.lstData;
                if (lstData != null) {
                    var temple = $("#data-template_chat").html();
                    var html = "";
                    $.each(lstData, function (i, item) {
                        if (item.isOnline) {
                            html += Mustache.render(temple,
                                {
                                    HoTen: item.userName,
                                    IpAdress: item.ipAddress + item.computerName,
                                    isOnline: "bg-success",
                                    Time: item.lastActive
                                })
                        } else {
                            html += Mustache.render(temple,
                                {
                                    HoTen: item.userName,
                                    IpAdress: item.ipAddress + item.computerName,
                                    isOnline: "bg-danger",
                                    Time: item.lastActive
                                })
                        }
                    });
                    $("#chatTemlate").html(html);
                } else {
                    $("#chatTemlate").html("");
                }
            }
        }
    })
});

// Khi user offline
connection.on("UserOffline", function (userId) {
    $.ajax({
        url: '/Admin/UserStatus/LoadData',
        type: "GET",
        dataType: "json",
        data: {
            userId: userId,
        },
        success: function (response) {
            if (response.status == true) {
                var lstData = response.lstData;
                if (lstData != null) {
                    var temple = $("#data-template_chat").html();
                    var html = "";
                    $.each(lstData, function (i, item) {
                        if (item.isOnline) {
                            html += Mustache.render(temple,
                                {
                                    HoTen: item.userName,
                                    IpAdress: item.ipAddress + item.computerName,
                                    isOnline: "bg-success",
                                    Time: item.lastActive
                                })
                        } else {
                            html += Mustache.render(temple,
                                {
                                    HoTen: item.userName,
                                    IpAdress: item.ipAddress + item.computerName,
                                    isOnline: "bg-danger",
                                    Time: item.lastActive
                                })
                        }
                    });
                    $("#chatTemlate").html(html);
                } else {
                    $("#chatTemlate").html("");
                }
            }
        }
    })
});

// Khi tắt tab hoặc mất mạng -> set trạng thái offline
window.addEventListener("beforeunload", function () {
    connection.stop();
});

setInterval(function () {
    fetch("/Admin/UserStatus/CheckInactiveUsers")
        .then(response => response.text())
        .then(data => console.log(data));
}, 300000);