var myScripts = {
    init: function myfunction() {
        myScripts.RegesterEvent();
        myScripts.LoadData();
    },

    RegesterEvent: function () {
        $('#tableDanhMucHeThong').off('click-row.bs.table').on('click-row.bs.table', function (e, row, $element) {
            $('.table-danger').removeClass('table-danger');
            $($element).addClass('table-danger');
            myScripts.LoadDetail(row.idHeThong);
        });

        $("#LoaiDanhMuc").off("change").on("change", function () {
            myScripts.ResetForm();
            myScripts.LoadTable();
        });

        $("#btnReset").off("click").on("click", function () {
            myScripts.ResetForm();
        });

        $("#btnSaveData").off("click").on("click", function () {
            myScripts.SaveData();
        });

        $("#btnRemoveData").off("click").on("click", function () {
            myScripts.DeleteData();
        });
    },

    ResetForm: function () {
        $("#IdHeThong").val(0);
        $("#ThuTuTG").val("");
        $("#TenGoi").val("");
        $("#GhiChu").val("");
        toastr.success("Tạo mới thành công", "Thành công", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
    },

    LoadData: function () {
        myScripts.LoadTable();
    },

    LoadTable: function () {
        var LoaiDanhMuc = $("#LoaiDanhMuc").val();
        $.ajax({
            url: '/Admin/DanhMucHeThong/LoadTable',
            type: "GET",
            dataType: "json",
            data: {
                LoaiDanhMuc: LoaiDanhMuc,
            },
            success: function (response) {
                if (response.status == true) {
                    var lstData = response.lstData;

                    if (lstData != null && lstData.length > 0) {
                        $("#tableDanhMucHeThong").bootstrapTable('load', lstData);

                    }
                    else {
                        $("#tableDanhMucHeThong").bootstrapTable('removeAll');
                    }
                }
            }
        })
    },

    DeleteData: function () {
        var IdHeThong = $("#IdHeThong").val();

        if (IdHeThong > 0) {
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
                            url: "/Admin/DanhMucHeThong/DeleteData",
                            data: {
                                IdHeThong: IdHeThong,
                            },
                            success: function (response) {
                                if (response.status == true) {
                                    myScripts.ResetForm();
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

    SaveData: function () {
        var IdHeThong = $("#IdHeThong").val();
        if (IdHeThong == "" || IdHeThong == undefined) {
            IdHeThong = 0;
        }
        var ThuTuTG = $("#ThuTuTG").val();
        var TenGoi = $("#TenGoi").val();
        var GhiChu = $("#GhiChu").val();
        var LoaiDanhMuc = $("#LoaiDanhMuc").val();
        if (ThuTuTG == "" || ThuTuTG == undefined) {
            toastr.error("Bạn chưa nhập thứ tự", "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            $("#ThuTuTG").focus();
            return;
        }
        if (TenGoi == "" || TenGoi == undefined) {
            toastr.error("Bạn chưa nhập tên gọi", "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            $("#ThuTuTG").focus();
            return;
        }
        if (LoaiDanhMuc == "" || LoaiDanhMuc == undefined) {
            toastr.error("Bạn chưa chọn loại danh mục", "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            $("#LoaiDanhMuc").focus();
            return;
        }
        $.ajax({
            type: "POST",
            url: '/Admin/DanhMucHeThong/SaveData',
            data: {
                IdHeThong: IdHeThong,
                ThuTuTG: ThuTuTG,
                TenGoi: TenGoi,
                GhiChu: GhiChu,
                LoaiDanhMuc: LoaiDanhMuc,
            },
            success: function (response) {
                if (response.status == true) {
                    myScripts.LoadTable();
                    toastr.success("Cập nhập thành công", "Thành công", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                } else {
                    toastr.error(response.message, "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                }
            },
            error: function (response) {
                toastr.error(response.message, "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            }
        });
    },

    LoadDetail: function (idHeThong) {
        if (idHeThong > 0) {
            $.ajax({
                url: '/Admin/DanhMucHeThong/LoadDetail',
                type: "GET",
                dataType: "json",
                data: {
                    IdHeThong: idHeThong,
                },
                success: function (response) {
                    if (response.status == true) {
                        var lstData = response.lstData;

                        if (lstData != null) {
                            $("#IdHeThong").val(lstData.idHeThong);
                            $("#ThuTuTG").val(lstData.thuTuTg);
                            $("#TenGoi").val(lstData.tenGoi);
                            $("#GhiChu").val(lstData.ghiChu);
                            $("#LoaiDanhMuc").val(lstData.loaiDanhMuc);
                        }
                    } else {
                        toastr.error(response.message, "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                    }
                }
            })
        }
    },


};
myScripts.init();