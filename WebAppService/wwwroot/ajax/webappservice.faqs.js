var myScripts = {
    init: function myfunction() {
        myScripts.ResetForm();
        myScripts.RegesterEvent();
        myScripts.LoadData();
    },

    RegesterEvent: function () {
        $('#tableCauHoi').off('click-row.bs.table').on('click-row.bs.table', function (e, row, $element) {
            $('.table-danger').removeClass('table-danger');
            $($element).addClass('table-danger');
            myScripts.LoadDetail(row.idFaqs);
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
        $("#IdFaqs").val('00000000-0000-0000-0000-000000000000');
        $("#SoThuTu").val("");
        $("#CauHoi").val("");
        $("#CauTraLoi").val("");
        $("#GhiChu").val("");
        toastr.success("Tạo mới thành công", "Thành công", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
    },

    LoadData: function () {
        myScripts.LoadTable();
    },

    LoadTable: function () {
        $.ajax({
            url: '/Admin/FAQS/LoadTable',
            type: "GET",
            dataType: "json",
            data: {
            },
            success: function (response) {
                if (response.status == true) {
                    var lstData = response.lstData;
                    if (lstData != null && lstData.length > 0) {
                        $("#tableCauHoi").bootstrapTable('load', lstData);
                    }
                    else {
                        $("#tableCauHoi").bootstrapTable('removeAll');
                    }
                }
            }
        })
    },

    DeleteData: function () {
        var IdFaqs = $("#IdFaqs").val();

        if (IdFaqs != "" || IdFaqs != null) {
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
                            url: "/Admin/FAQS/DeleteData",
                            data: {
                                IdFaqs: IdFaqs,
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
        var IdFaqs = $("#IdFaqs").val();
        var SoThuTu = $("#SoThuTu").val();
        var CauHoi = $("#CauHoi").val();
        var CauTraLoi = $("#CauTraLoi").val();
        var GhiChu = $("#GhiChu").val();

        if (SoThuTu == "" || SoThuTu == undefined) {
            toastr.error("Bạn chưa nhập thứ tự", "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            $("#SoThuTu").focus();
            return;
        }
        if (CauHoi == "" || CauHoi == undefined) {
            toastr.error("Bạn chưa nhập câu hỏi", "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            $("#CauHoi").focus();
            return;
        }
      
        $.ajax({
            type: "POST",
            url: '/Admin/FAQS/SaveData',
            data: {
                IdFaqs: IdFaqs,
                SoThuTu: SoThuTu,
                CauHoi: CauHoi,
                CauTraLoi: CauTraLoi,
                GhiChu: GhiChu,
            },
            success: function (response) {
                if (response.status == true) {
                    myScripts.LoadTable();
                    myScripts.ResetForm();
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

    LoadDetail: function (idFaqs) {
        if (idFaqs != "" || idFaqs != null) {
            $.ajax({
                url: '/Admin/FAQS/LoadDetail',
                type: "GET",
                dataType: "json",
                data: {
                    IdFaqs: idFaqs,
                },
                success: function (response) {
                    if (response.status == true) {
                        var lstData = response.lstData;

                        if (lstData != null) {
                            $("#IdFaqs").val(lstData.idFaqs);
                            $("#SoThuTu").val(lstData.soThuTu);
                            $("#CauHoi").val(lstData.cauHoi);
                            $("#CauTraLoi").val(lstData.cauTraLoi);
                            $("#GhiChu").val(lstData.ghiChu);
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