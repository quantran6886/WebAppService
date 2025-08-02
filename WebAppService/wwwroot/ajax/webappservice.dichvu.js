var files;
var myScripts = {
    init: function () {
        myScripts.LoadData();
        myScripts.Event();
    },

    Event: function () {
        $("#btnNewForm").off("click").on("click", function () {
            myScripts.ResetForm();
            toastr.success("Mời bạn nhập thông tin", "Thành công", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
        });

        $("#btnSaveData").off("click").on("click", function () {
            myScripts.SaveData();
        });

        $("#btnRemoveUpload").off("click").on("click", function () {
            $("#isThayDoi").val("false");
            $('#imageHoSo').attr('src', '/root/unnamed.png');
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

        $("#CbNhomBaiViet1").off("change").on("change", function () {
            $("#CbLoaiBaiDang").val("1");
            $("#CbNhomBaiViet2").val("").trigger('chosen:updated');
        });

        $("#CbNhomBaiViet2").off("change").on("change", function () {
            $("#CbLoaiBaiDang").val("2");
            $("#CbNhomBaiViet1").val("").trigger('chosen:updated');
        });
        
    },

    LoadData: function () {
        $.ajax({
            url: '/Admin/DichVu/LoadData',
            type: 'GET',
            datatype: 'json',
            success: function (response) {
                if (response.status) {
                    var lstNhomBaiViet1 = response.lstNhomBaiViet1;
                    var lstNhomBaiViet2 = response.lstNhomBaiViet2;
                    if (lstNhomBaiViet1 != null) {
                        var option = "<option value=''>--- Chọn dịch vụ chuyên khoa ---</option>";
                        $.each(lstNhomBaiViet1, function (index, val) {
                            option += "<option value='" + val.tenGoi + "'>" + val.tenGoi + "</option>";
                        });
                        $("#CbNhomBaiViet1").html(option);
                        $("#CbNhomBaiViet1").trigger('chosen:updated');
                    }
                    if (lstNhomBaiViet2 != null) {
                        var option = "<option value=''>--- Chọn dịch vụ đặc biêt ---</option>";
                        $.each(lstNhomBaiViet2, function (index, val) {
                            option += "<option value='" + val.tenGoi + "'>" + val.tenGoi + "</option>";
                        });
                        $("#CbNhomBaiViet2").html(option);
                        $("#CbNhomBaiViet2").trigger('chosen:updated');
                    }
                    myScripts.LoadTable();
                }
            },
        });
    },

    LoadTable: function () {
        $.ajax({
            url: '/Admin/DichVu/LoadTable',
            type: "GET",
            dataType: "json",
            data: {
            },
            success: function (response) {
                if (response.status == true) {
                    var lstData = response.lstData;
                    if (lstData != null && lstData.length > 0) {
                        $("#tableDichVu").bootstrapTable('load', lstData);
                    }
                    else {
                        $("#tableDichVu").bootstrapTable('removeAll');
                    }
                    $('[data-bs-toggle="tooltip"]').tooltip();
                }
            }
        })
    },

    LoadDetail: function (idDichVu) {
        if (idDichVu != "") {
            $.ajax({
                url: '/Admin/DichVu/LoadDetail',
                type: "GET",
                dataType: "json",
                data: {
                    IdDichVu: idDichVu,
                },
                success: function (response) {
                    if (response.status == true) {
                        var lstData = response.lstData;

                        if (lstData != null) {
                            $("#IdDichVu").val(lstData.idDichVu);
                            $("#TieuDeBaiViet").val(lstData.tieuDeBaiViet);
                            $("#MoTaNgan").val(lstData.moTaNgan);
                            $("#IsCongKhai").prop("checked", lstData.isCongKhai);
                            $("#IsBaiVietNoiBat").prop("checked", lstData.isBaiVietNoiBat);
                            if (lstData.cbLoaiBaiDang == "1") {
                                $("#CbNhomBaiViet1").val(lstData.cbNhomBaiViet).trigger('chosen:updated');
                                $("#CbNhomBaiViet2").val("").trigger('chosen:updated');
                                $("#CbLoaiBaiDang").val("1");
                            } else if (lstData.cbLoaiBaiDang == "2") {
                                $("#CbNhomBaiViet2").val(lstData.cbNhomBaiViet).trigger('chosen:updated');
                                $("#CbNhomBaiViet1").val("").trigger('chosen:updated');
                            } else {
                                $("#CbNhomBaiViet1").val("").trigger('chosen:updated');
                                $("#CbNhomBaiViet2").val("").trigger('chosen:updated');
                                $("#CbLoaiBaiDang").val("2");
                            }
                            if (lstData.urlImage != null && lstData.urlImage != "") {
                                $('#imageHoSo').attr('src', lstData.urlImage);
                            } else {
                                $('#imageHoSo').attr('src', '/root/unnamed.png');
                            }
                            if (lstData.noiDung != null) {
                                tinymce.get('editor').setContent(lstData.noiDung);
                            } else {
                                tinymce.get('editor').setContent("");
                            }
                            $("#isThayDoi").val("false");
                            $("#bsBaiViet").modal('show');
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
        $("#IdDichVu").val(0);
        $("#TieuDeBaiViet").val("");
        $("#MoTaNgan").val("");
        $("#CbLoaiBaiDang").val("");
        $("#CbNhomBaiViet1").val("").trigger('chosen:updated');
        $("#CbNhomBaiViet2").val("").trigger('chosen:updated');
        $("#IsBaiVietNoiBat").prop("checked", false);
        $("#IsCongKhai").prop("checked", false);
        $("#isThayDoi").val("false");
        tinymce.get('editor').setContent("");
        $('#imageHoSo').attr('src', '/root/unnamed.png');
    },

    SaveData: function () {
        var IdDichVu = $("#IdDichVu").val();
        var TieuDeBaiViet = $("#TieuDeBaiViet").val();
        var MoTaNgan = $("#MoTaNgan").val();
        var CbLoaiBaiDang = $("#CbLoaiBaiDang").val();
        var CbNhomBaiViet1 = $("#CbNhomBaiViet1").val();
        var CbNhomBaiViet2 = $("#CbNhomBaiViet2").val();
        var IsBaiVietNoiBat = $("#IsBaiVietNoiBat").prop("checked");
        var IsCongKhai = $("#IsCongKhai").prop("checked");
        var editor = encodeURIComponent(tinymce.get('editor').getContent());

        if (TieuDeBaiViet == "") {
            toastr.error("Bạn chưa nhập tên tiêu đề", "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            $("#TieuDeBaiViet").focus();
            return;
        }
        if ((CbNhomBaiViet1 == "" || CbNhomBaiViet1 == undefined) && (CbNhomBaiViet2 == "" || CbNhomBaiViet2 == undefined)) {
            toastr.error("Bạn chưa chọn phân loại dịch vụ", "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            return;
        }

        var objdata = {
            IdDichVu: IdDichVu != 0 ? IdDichVu : '00000000-0000-0000-0000-000000000000',
            TieuDeBaiViet: TieuDeBaiViet,
            MoTaNgan: MoTaNgan,
            CbLoaiBaiDang: CbLoaiBaiDang,
            CbNhomBaiViet: CbNhomBaiViet1 != "" ? CbNhomBaiViet1 : CbNhomBaiViet2 != "" ? CbNhomBaiViet2 : "",
            IsBaiVietNoiBat: IsBaiVietNoiBat,
            IsCongKhai: IsCongKhai,
            NoiDung: editor,
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
                    url: '/Admin/DichVu/SaveData',
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (response) {
                        if (response.status == true) {
                            toastr.success("Cập nhập thành công", "Thành Công", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                            myScripts.LoadTable();
                            $("#bsBaiViet").modal('hide');
                        } else {
                            notify('error', 'Có Lỗi', response.message);
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
                url: '/Admin/DichVu/SaveData',
                datatype: 'json',
                data: {
                    strData: JSON.stringify(objdata),
                },
                success: function (response) {
                    if (response.status) {
                        toastr.success("Cập nhập thành công", "Thành Công", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                        myScripts.LoadTable();
                        $("#bsBaiViet").modal('hide');
                    }
                    else {
                        toastr.error(response.message, "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                    }
                },
            });
        }
    },

    DeleteData: function (idDichVu) {
        if (idDichVu != "") {
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
                            url: "/Admin/DichVu/DeleteData",
                            data: {
                                IdDichVu: idDichVu,
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
function fm_Image(e, value, row, index) {
    if (value.urlImage != "") {
        return [
            '<div style="width:120px;justify-self: center;" class="">',
            '<a class="image-popup-vertical-fit" target="_blank" href="' + value.urlImage + '">',
            ' <img class="d-block position-relative w-100" src="' + value.urlImage + '" width="100" />',
            ' </a>',
            '</div>'
        ].join('');
    } else {
        return [
            '<div style="width: 100%">',

            '</div>'
        ].join('');
    }
};

function fm_editBVNB(e, value, row, index) {
    if (value.isBaiVietNoiBat == true) {
        return [
            '<div style="width: 100%;text-align: center;">',
            '<iconify-icon icon="solar:verified-check-bold-duotone" class="text-success" width="35px" height="35px"></iconify-icon>',
            '</div>'
        ].join('');
    } else {
        return [
            '<div style="width: 100%;text-align: center;">',
            '<iconify-icon icon="solar:close-circle-bold-duotone" class="text-danger" width="35px" height="35px"></iconify-icon>',
            '</div>'
        ].join('');
    }
};

function fm_editBVCK(e, value, row, index) {
    if (value.isCongKhai == true) {
        return [
            '<div style="width: 100%;text-align: center;">',
            '<iconify-icon icon="solar:verified-check-bold-duotone" class="text-success" width="35px" height="35px"></iconify-icon>',
            '</div>'
        ].join('');
    } else {
        return [
            '<div style="width: 100%;text-align: center;">',
            '<iconify-icon icon="solar:close-circle-bold-duotone" class="text-danger" width="35px" height="35px"></iconify-icon>',
            '</div>'
        ].join('');
    }
};
function fm_editData(e, value, row, index) {
    return [
        '<div style="width: 100%">',
        '<a href="javascript:myScripts.LoadDetail(' + "'" + value.idDichVu + "'" + ')" class="hvr-shrink me-2" data-bs-toggle="tooltip" data-bs-placement="top" title="Sửa dữ liệu"><iconify-icon icon="solar:gallery-edit-line-duotone" class="text-primary" width="23px" height="23px"></iconify-icon></a>' +
        '<a href="javascript:myScripts.DeleteData(' + "'" + value.idDichVu + "'" + ')" class="hvr-shrink" data-bs-toggle="tooltip" title="Xóa dữ liệu"><iconify-icon icon="solar:trash-bin-trash-outline" class="text-danger" width="25px" height="25px"></iconify-icon></a>' +
        '</div>'
    ].join('');
};