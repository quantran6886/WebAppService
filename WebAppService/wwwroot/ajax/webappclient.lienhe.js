const connectionNoti = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connectionNoti.on("ReceiveNotification", function (message) {
});
function CheckBeforeSend() {
    var isMobile = window.innerWidth <= 768;
    var response = grecaptcha.getResponse(isMobile ? widgetIdMobile : widgetIdPC);

    if (response.length === 0) {
        Toastify({
            text: "Vui lòng xác minh trước khi gửi",
            duration: 2000,
            close: true,
            gravity: "top",
            position: "right",
            backgroundColor: "red"
        }).showToast();
        return;
    }

    SendForm(response);
}

function SendForm(token) {
    var ho_ten = $("#ho_ten").val();
    var so_dien_thoai = $("#so_dien_thoai").val();
    var email = $("#email").val();
    var loi_nhan = $("#loi_nhan").val();

    if (ho_ten == "") {
        Toastify({
            text: "Họ tên không được để trống!",
            duration: 2000,
            close: true,
            gravity: "top",
            position: "right",
            backgroundColor: "red"
        }).showToast();
        return;
    }

    if (loi_nhan == "") {
        Toastify({
            text: "Lời nhắn không được để trống!",
            duration: 2000,
            close: true,
            gravity: "top",
            position: "right",
            backgroundColor: "red"
        }).showToast();
        $("#loi_nhan").focus();
        return;
    }

    Swal.fire({
        title: "Thông báo?",
        text: "Bạn có muốn đăng ký lịch khám không?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Đồng ý",
        cancelButtonText: "Hủy"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: "/LienHe/GuiThongBao",
                data: {
                    ho_ten: ho_ten,
                    so_dien_thoai: so_dien_thoai,
                    email: email,
                    loi_nhan: loi_nhan,
                    gRecaptchaResponse: token
                },
                success: function () {
                    alert("Đã gửi!");
                    $("#ho_ten").val('');
                    $("#so_dien_thoai").val('');
                    $("#email").val('');
                    $("#loi_nhan").val('');
                    // Reset captcha nếu cần
                    grecaptcha.reset(widgetIdPC);
                    grecaptcha.reset(widgetIdMobile);
                }
            });
        }
    });
}
