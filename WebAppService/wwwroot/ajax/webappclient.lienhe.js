const connectionNoti = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connectionNoti.on("ReceiveNotification", function (message) {
});

function SendForm() {
    var ho_ten = $("#ho_ten").val();
    var so_dien_thoai = $("#so_dien_thoai").val();
    var email = $("#email").val();
    var loi_nhan = $("#loi_nhan").val();
    if (ho_ten == "") {
        alert("Họ tên không được để trống!");
        $("#ho_ten").focus();
        return;
    }
    if (loi_nhan == "") {
        alert("Nội dung không được để trống!");
        $("#loi_nhan").focus();
        return;
    }

    $.ajax({
        type: "POST",
        url: "/LienHe/GuiThongBao",
        data: {
            ho_ten: ho_ten,
            so_dien_thoai: so_dien_thoai,
            email: email,
            loi_nhan: loi_nhan
        },
        success: function () {
            alert("Đã gửi!");
            $("#ho_ten").val('');
            $("#so_dien_thoai").val('');
            $("#email").val('');
            $("#loi_nhan").val('');
        }
    });
}