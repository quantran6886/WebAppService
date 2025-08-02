var files;
var myScripts = {
    init: function () {
        myScripts.LoadData();
        myScripts.Event();
    },

    Event: function () {

        $("#btnNewForm").off("click").on("click", function () {
            myScripts.ResetForm();
        });

        $("#btnSaveData").off("click").on("click", function () {
            myScripts.SaveData();
        });

        $("#btnRemoveUpload").off("click").on("click", function () {
            $("#isThayDoi").val("false");
            $('#imageHoSo').attr('src', '/root/Profile_avatar.png');
        });

        $("#btnUpload").off('click').on('click', function () {
            $("#duong_dan_file").click();
            $("#duong_dan_file").off('change').on('change', function (e) {
                files = e.target.files;
                if (files.length > 0) {
                    for (var x = 0; x < files.length; x++) {
                        if (files[x].size >= 52428800) {
                            toastr.error("Chỉ được upload file dưới 50mb", "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                            $("#isThayDoi").val("false");
                            return;
                        } else {
                            myScripts.readURL(this);
                            $("#isThayDoi").val("true");
                        }
                    }
                }
            });
        });
    },

    LoadData: function () {
        $.ajax({
            url: '/Admin/DanhSachNhanSu/LoadData',
            type: 'GET',
            datatype: 'json',
            success: function (response) {
                if (response.status) {
                    var lstChucDanh = response.lstChucDanh;
                    var lstDonViKhoa = response.lstDonViKhoa;
                    var lstChuyenKhoa = response.lstChuyenKhoa;
                    if (lstChucDanh != null) {
                        var option = "<option value=''>-- Chọn chức danh --</option>";
                        $.each(lstChucDanh, function (index, val) {
                            option += "<option value='" + val.tenGoi + "'>" + val.tenGoi + "</option>";
                        });
                        $("#ChucDanh").html(option);
                        $("#ChucDanh").trigger('chosen:updated');
                    }
                    if (lstDonViKhoa != null) {
                        var option = "<option value=''>-- Chọn đơn vị --</option>";
                        $.each(lstDonViKhoa, function (index, val) {
                            option += "<option value='" + val.tenGoi + "'>" + val.tenGoi + "</option>";
                        });
                        $("#DonViKhoa").html(option);
                        $("#DonViKhoa").trigger('chosen:updated');
                    }
                    if (lstChuyenKhoa != null) {
                        var option = "<option value=''>-- Chọn chuyên khoa --</option>";
                        $.each(lstChuyenKhoa, function (index, val) {
                            option += "<option value='" + val.tenGoi + "'>" + val.tenGoi + "</option>";
                        });
                        $("#ChucVu").html(option);
                        $("#ChucVu").trigger('chosen:updated');
                    }
                    myScripts.LoadTable();
                }
            },
        });
    },

    LoadTable: function () {
        $.ajax({
            url: '/Admin/DanhSachNhanSu/LoadTable',
            type: "GET",
            dataType: "json",
            data: {
            },
            success: function (response) {
                if (response.status == true) {
                    var lstData = response.lstData;
                    if (lstData != null && lstData.length > 0) {
                        $("#tableDanhSachNhanSu").bootstrapTable('load', lstData);
                    }
                    else {
                        $("#tableDanhSachNhanSu").bootstrapTable('removeAll');
                    }
                    $('[data-bs-toggle="tooltip"]').tooltip();
                }
            }
        })
    },

    LoadDetail: function (idNhanSu) {
        if (idNhanSu != "") {
            $.ajax({
                url: '/Admin/DanhSachNhanSu/LoadDetail',
                type: "GET",
                dataType: "json",
                data: {
                    IdNhanSu: idNhanSu,
                },
                success: function (response) {
                    if (response.status == true) {
                        var lstData = response.lstData;

                        if (lstData != null) {
                            $("#IdNhanSu").val(lstData.idNhanSu);
                            $("#HoTen").val(lstData.hoTen);
                            $("#NgaySinh").val(lstData.ngaySinh);
                            $("#ChucDanh").val(lstData.chucDanh).trigger('chosen:updated');
                            $("#DonViKhoa").val(lstData.donViKhoa).trigger('chosen:updated');
                            $("#ChucVu").val(lstData.chucVu).trigger('chosen:updated');
                            $("#BangCapHocVi").val(lstData.bangCapHocVi);
                            $("#NgonNgu").val(lstData.ngonNgu);
                            if (lstData.kinhNghiemLamViec != null) {
                                tinymce.get('editor').setContent(lstData.kinhNghiemLamViec);
                            } else {
                                tinymce.get('editor').setContent("");
                            }
                            if (lstData.urlImage != null && lstData.urlImage != "") {
                                $('#imageHoSo').attr('src', lstData.urlImage);
                            } else {
                                $('#imageHoSo').attr('src', '/root/Profile_avatar.png');
                            }
                            $("#isThayDoi").val("false");
                            $("#bsHoSoCanBo").modal('show');
                        }
                    } else {
                        toastr.error(response.message, "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                    }
                }
            })
        }
    },

    readURL: function (input) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imageHoSo').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    },

    ResetForm: function () {
        $("#IdNhanSu").val(0);
        $("#HoTen").val("");
        $("#NgaySinh").val("");
        $("#ChucDanh").val("").trigger('chosen:updated');
        $("#DonViKhoa").val("").trigger('chosen:updated');
        $("#ChucVu").val("").trigger('chosen:updated');
        $("#BangCapHocVi").val("");
        $("#NgonNgu").val("");
        $("#KinhNghiemLamViec").val("");
        tinymce.get('editor').setContent("");
        $("#isThayDoi").val("false");
        $('#imageHoSo').attr('src', '/root/Profile_avatar.png');
    },

    SaveData: function () {
        var IdNhanSu = $("#IdNhanSu").val();
        var HoTen = $("#HoTen").val();
        var NgaySinh = $("#NgaySinh").val();
        var ChucDanh = $("#ChucDanh").val();
        var DonViKhoa = $("#DonViKhoa").val();
        var ChucVu = $("#ChucVu").val();
        var BangCapHocVi = $("#BangCapHocVi").val();
        var NgonNgu = $("#NgonNgu").val();
        var editor = encodeURIComponent(tinymce.get('editor').getContent());

        if (HoTen == "") {
            toastr.error("Bạn chưa nhập họ tên", "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            $("#HoTen").focus();
            return;
        }
        if (NgaySinh == "") {
            toastr.error("Bạn chưa chọn ngày sinh", "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            $("#NgaySinh").focus();
            return;
        }
        if (ChucDanh == "") {
            toastr.error("Bạn chưa chọn chức danh", "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            $("#ChucDanh").focus();
            return;
        }

        var objdata = {
            IdNhanSu: IdNhanSu != 0 ? IdNhanSu : '00000000-0000-0000-0000-000000000000',
            HoTen: HoTen,
            NgaySinh: NgaySinh,
            ChucDanh: ChucDanh,
            DonViKhoa: DonViKhoa,
            ChucVu: ChucVu,
            BangCapHocVi: BangCapHocVi,
            NgonNgu: NgonNgu,
            KinhNghiemLamViec: editor,
        }
        if (files != undefined && files.length > 0) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append('files', files[x]);

                }
                data.append("strData", JSON.stringify(objdata));
                data.append('isThayDoi', $("#isThayDoi").val());

                $.ajax({
                    type: "POST",
                    url: '/Admin/DanhSachNhanSu/SaveData',
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (response) {
                        if (response.status == true) {
                            toastr.success("Cập nhập thành công", "Thành Công", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                            myScripts.LoadTable();
                            $("#bsHoSoCanBo").modal('hide');
                        } else {
                            toastr.error("Có lỗi dữ liệu", "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                        }
                    },
                    error: function (response) {
                        toastr.error(response.message, "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                    }
                });
            } else {
                alert("Upload file thất bại!");
            }
        }
        else {
            $.ajax({
                type: 'Post',
                url: '/Admin/DanhSachNhanSu/SaveData',
                datatype: 'json',
                data: {
                    strData: JSON.stringify(objdata),
                },
                success: function (response) {
                    if (response.status) {
                        toastr.success("Cập nhập thành công", "Thành Công", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                        myScripts.LoadTable();
                        $("#bsHoSoCanBo").modal('hide');
                    }
                    else {
                        toastr.error(response.message, "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                    }
                },
            });
        }
    },

    DeleteData: function (idNhanSu) {

        if (idNhanSu != "") {
            const swalWithBootstrapButtons = Swal.mixin({
                customClass: {
                    confirmButton: "btn bg-secondary-subtle text-secondary",
                    cancelButton: "me-6 btn bg-secondary-subtle text-secondary",
                },
                buttonsStyling: false,
            });
            swalWithBootstrapButtons
                .fire({
                    title: "Xác nhận?",
                    text: "Bạn có chắc chắn muốn xóa bản ghi này không?",
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
                            url: "/Admin/DanhSachNhanSu/DeleteData",
                            data: {
                                IdNhanSu: idNhanSu,
                            },
                            success: function (response) {
                                if (response.status == true) {
                                    myScripts.LoadTable();
                                    toastr.success("Xóa thành công 1 bản ghi", "Thành công", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                                } else {
                                    toastr.error(response.message, "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                                }
                            },
                            error: function (response) {
                                toastr.error(response.message, "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                            }
                        });
                    }
                });
        } else {
            alert("Bạn chưa chọn bản ghi nào để xóa");
        }
    },

};
myScripts.init();
function fm_editData(e, value, row, index) {
    return [
        '<div style="width: 100%">',
        '<a href="javascript:myScripts.LoadDetail(' + "'" + value.idNhanSu + "'" + ')" class="hvr-shrink me-2" data-bs-toggle="tooltip" data-bs-placement="top" title="Sửa dữ liệu"><i class="ti ti-pencil text-primary fs-6"></i></a>' +
        '<a href="javascript:myScripts.DeleteData(' + "'" + value.idNhanSu + "'" + ')" class="hvr-shrink" data-bs-toggle="tooltip" title="Xóa dữ liệu"><i class="ti ti-trash text-danger fs-6"></i> </a>' +
        '</div>'
    ].join('');
};