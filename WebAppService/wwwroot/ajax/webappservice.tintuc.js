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
        $("#btnAutoCode").on("click", function () {
            var title = $("#TieuDeBaiViet").val();
            if (title.trim() === "") {
                alert("Vui lòng nhập tiêu đề bài viết!");
                return;
            }
            var slug = toSlug(title);
            var finalSlug = slug + "-" + randomCode(4);
            $("#SeoUrl").val(finalSlug);
        });
    },

    LoadData: function () {
        $.ajax({
            url: '/Admin/TinTucBaiViet/LoadData',
            type: 'GET',
            datatype: 'json',
            success: function (response) {
                if (response.status) {
                    var lstNhomBaiViet = response.lstNhomBaiViet;
                    var lstDanhMucBaiDang = response.lstDanhMucBaiDang;
                    if (lstNhomBaiViet != null) {
                        var option = "<option value=''>Chọn nhóm bài viết</option>";
                        $.each(lstNhomBaiViet, function (index, val) {
                            option += "<option value='" + val.tenGoi + "'>" + val.tenGoi + "</option>";
                        });
                        $("#CbNhomBaiViet").html(option);
                        $("#CbNhomBaiViet").trigger('chosen:updated');
                    }
                    if (lstDanhMucBaiDang != null) {
                        var option = "<option value=''>Chọn danh mục bài đăng</option>";
                        $.each(lstDanhMucBaiDang, function (index, val) {
                            option += "<option value='" + val.tenGoi + "'>" + val.tenGoi + "</option>";
                        });
                        $("#CbLoaiBaiDang").html(option);
                        $("#CbLoaiBaiDang").trigger('chosen:updated');
                    }
                    myScripts.LoadTable();
                }
            },
        });
    },

    LoadTable: function () {
        $.ajax({
            url: '/Admin/TinTucBaiViet/LoadTable',
            type: "GET",
            dataType: "json",
            data: {
            },
            success: function (response) {
                if (response.status == true) {
                    var lstData = response.lstData;
                    if (lstData != null && lstData.length > 0) {
                        $("#tableTinTucBaiViet").bootstrapTable('load', lstData);
                    }
                    else {
                        $("#tableTinTucBaiViet").bootstrapTable('removeAll');
                    }
                    $('[data-bs-toggle="tooltip"]').tooltip();
                }
            }
        })
    },

    LoadDetail: function (idBaiViet) {
        if (idBaiViet != "") {
            $.ajax({
                url: '/Admin/TinTucBaiViet/LoadDetail',
                type: "GET",
                dataType: "json",
                data: {
                    IdBaiViet: idBaiViet,
                },
                success: function (response) {
                    if (response.status == true) {
                        var lstData = response.lstData;

                        if (lstData != null) {
                            $("#IdBaiViet").val(lstData.idBaiViet);
                            $("#SapXep").val(lstData.sapXep);
                            $("#TieuDeBaiViet").val(lstData.tieuDeBaiViet);
                            $("#MoTaNgan").val(lstData.moTaNgan);
                            $("#TieuDeNgan").val(lstData.tieuDeNgan);
                            $("#SeoUrl").val(lstData.seoUrl);
                            $("#IsCongKhai").prop("checked", lstData.isCongKhai);
                            $("#IsBaiVietNoiBat").prop("checked", lstData.isBaiVietNoiBat);
                            $("#CbNhomBaiViet").val(lstData.cbNhomBaiViet).trigger('chosen:updated');
                            $("#CbLoaiBaiDang").val(lstData.cbLoaiBaiDang).trigger('chosen:updated');
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
        $("#IdBaiViet").val(0);
        $("#TieuDeBaiViet").val("");
        $("#MoTaNgan").val("");
        $("#CbNhomBaiViet").val("").trigger('chosen:updated');
        $("#TieuDeNgan").val("");
        $("#SeoUrl").val("");
        $("#CbLoaiBaiDang").val("").trigger('chosen:updated');
        $("#IsBaiVietNoiBat").prop("checked", false);
        $("#IsCongKhai").prop("checked", false);
        $("#isThayDoi").val("false");
        tinymce.get('editor').setContent("");
        $('#imageHoSo').attr('src', '/root/unnamed.png');
    },

    SaveData: function () {
        var IdBaiViet = $("#IdBaiViet").val();
        var SapXep = $("#SapXep").val();
        var TieuDeBaiViet = $("#TieuDeBaiViet").val();
        var MoTaNgan = $("#MoTaNgan").val();
        var CbNhomBaiViet = $("#CbNhomBaiViet").val();
        var TieuDeNgan = $("#TieuDeNgan").val();
        var CbLoaiBaiDang = $("#CbLoaiBaiDang").val();
        var SeoUrl = $("#SeoUrl").val();
        var IsBaiVietNoiBat = $("#IsBaiVietNoiBat").prop("checked");
        var IsCongKhai = $("#IsCongKhai").prop("checked");
        var editor = encodeURIComponent(tinymce.get('editor').getContent());

        if (TieuDeBaiViet == "") {
            toastr.error("Bạn chưa nhập tên tiêu đề", "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            $("#TieuDeBaiViet").focus();
            return;
        }
        if (CbNhomBaiViet == "") {
            toastr.error("Bạn chưa chọn nhóm bài viết", "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            $("#CbNhomBaiViet").focus();
            return;
        }
        if (SeoUrl == "" || SeoUrl == undefined) {
            toastr.error("Bạn chưa nhập đường dẫn seo url", "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
            $("#SeoUrl").focus();
            return;
        }
        var objdata = {
            IdBaiViet: IdBaiViet != 0 ? IdBaiViet : '00000000-0000-0000-0000-000000000000',
            TieuDeBaiViet: TieuDeBaiViet,
            MoTaNgan: MoTaNgan,
            CbNhomBaiViet: CbNhomBaiViet,
            IsBaiVietNoiBat: IsBaiVietNoiBat,
            IsCongKhai: IsCongKhai,
            NoiDung: editor,
            TieuDeNgan: TieuDeNgan,
            CbLoaiBaiDang: CbLoaiBaiDang,
            SapXep: SapXep,
            SeoUrl: SeoUrl,
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
                    url: '/Admin/TinTucBaiViet/SaveData',
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
                url: '/Admin/TinTucBaiViet/SaveData',
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

    DeleteData: function (idBaiViet) {
        if (idBaiViet != "") {
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
                            url: "/Admin/TinTucBaiViet/DeleteData",
                            data: {
                                idBaiViet: idBaiViet,
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
        '<a href="javascript:myScripts.LoadDetail(' + "'" + value.idBaiViet + "'" + ')" class="hvr-shrink me-2" data-bs-toggle="tooltip" data-bs-placement="top" title="Sửa dữ liệu"><iconify-icon icon="solar:gallery-edit-line-duotone" class="text-primary" width="23px" height="23px"></iconify-icon></a>' +
        '<a href="javascript:myScripts.DeleteData(' + "'" + value.idBaiViet + "'" + ')" class="hvr-shrink" data-bs-toggle="tooltip" title="Xóa dữ liệu"><iconify-icon icon="solar:trash-bin-trash-outline" class="text-danger" width="25px" height="25px"></iconify-icon></a>' +
        '</div>'
    ].join('');
};
function toSlug(str) {
    str = str.toLowerCase();
    str = str.normalize('NFD').replace(/[\u0300-\u036f]/g, "");
    str = str.replace(/đ/g, 'd');
    str = str.replace(/[^a-z0-9\s-]/g, '');
    str = str.trim().replace(/\s+/g, '-');
    str = str.replace(/-+/g, '-');
    return str;
}
function randomCode(length) {
    var chars = '0123456789';
    var result = '';
    for (var i = 0; i < length; i++) {
        result += chars.charAt(Math.floor(Math.random() * chars.length));
    }
    return result;
}