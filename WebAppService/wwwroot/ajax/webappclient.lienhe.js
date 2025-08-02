const connectionNoti = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connectionNoti.on("ReceiveNotification", function (message) {
});
function CheckBeforeSend() {
    var response = grecaptcha.getResponse();
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
    SendForm();
}

//function SendForm() {
//    var ho_ten = $("#ho_ten").val();
//    var so_dien_thoai = $("#so_dien_thoai").val();
//    var email = $("#email").val();
//    var loi_nhan = $("#loi_nhan").val();

//    if (ho_ten == "") {
//        Toastify({
//            text: "Họ tên không được để trống!",
//            duration: 2000,
//            close: true,
//            gravity: "top",
//            position: "right",
//            backgroundColor: "red"
//        }).showToast();
//        $("#ho_ten").focus();
//        return;
//    }
//    if (loi_nhan == "") {
//        Toastify({
//            text: "Họ tên không được để trống!",
//            duration: 2000,
//            close: true,
//            gravity: "top",
//            position: "right",
//            backgroundColor: "red"
//        }).showToast();
//        $("#loi_nhan").focus();
//        return;
//    }

//    var data = {
//        ho_ten: ho_ten,
//        so_dien_thoai: so_dien_thoai,
//        email: email,
//        loi_nhan: loi_nhan,
//        gRecaptchaResponse: grecaptcha.getResponse()
//    };

//    $.ajax({
//        type: "POST",
//        url: "/verify",
//        data: data,
//        success: function (res) {
//            Swal.fire({
//                title: 'Thành công!',
//                text: 'Dữ liệu đã được gửi.',
//                icon: 'success',
//                confirmButtonText: 'OK'
//            });
//        },
//        error: function () {
//            alert("Lỗi kết nối tới server.");
//        }
//    });
//}
function SendForm() {
    var ho_ten = $("#ho_ten").val();
    var so_dien_thoai = $("#so_dien_thoai").val();
    var email = $("#email").val();
    var loi_nhan = $("#loi_nhan").val();
    if (ho_ten == "") {
        alert("Họ tên không được để trống!");
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
            text: "Họ tên không được để trống!",
            duration: 2000,
            close: true,
            gravity: "top",
            position: "right",
            backgroundColor: "red"
        }).showToast();
        $("#loi_nhan").focus();
        return;
    }

    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: "swal2-cancel btn main-menu border-radius-5 d-lg-inline btn-orrange p-20",
            cancelButton: "swal2-cancel btn main-menu border-radius-5 d-lg-inline btn-orrange p-20 mr-10",
        },
        buttonsStyling: false,
    });

    swalWithBootstrapButtons
        .fire({
            title: "Thông báo?",
            text: "Bạn có muốn đang ký lịch khám không?",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Đồng ý",
            cancelButtonText: "Hủy",
            reverseButtons: true,
        })
        .then((result) => {
            if (result.isConfirmed) {
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
        });
}