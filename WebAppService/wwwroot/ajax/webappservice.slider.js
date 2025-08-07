var files;
var videos;
var myScripts = {
    init: function () {
        myScripts.LoadTable();
        myScripts.Event();
    },

    Event: function () {
        $("#btnNewForm").off("click").on("click", function () {
            myScripts.SaveNewForm();
            toastr.success("Mời bạn nhập thông tin", "Thành công", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
        });
     
    },

    SaveNewForm: function () {
        $.ajax({
            type: 'Post',
            url: '/Admin/Slider/SaveNewForm',
            datatype: 'json',
            success: function (response) {
                if (response.status) {
                    toastr.success("Cập nhập thành công", "Thành Công", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                    myScripts.LoadTable();
                }
                else {
                    toastr.error(response.message, "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                }
            },
        });
    },

    LoadTable: function () {
        $.ajax({
            url: '/Admin/Slider/LoadTable',
            type: "GET",
            dataType: "json",
            data: {
            },
            success: function (response) {
                if (response.status == true) {
                    var lstData = response.lstData;
                    if (lstData != null && lstData.length > 0) {
                        $("#tableSlider").bootstrapTable('load', lstData);
                    }
                    else {
                        $("#tableSlider").bootstrapTable('removeAll');
                    }
                    //$('[data-bs-toggle="tooltip"]').tooltip();
                }
            }
        })
    },
};
myScripts.init();



window.ev_SapXep = {
    'change input': function (e, value, row, index) {
        row.sapXep = $(e.target).val();
        $.ajax({
            url: '/Admin/Slider/SaveData',
            type: 'POST',
            dataType: 'json',
            data: {
                MaTrang: row.maTrang,
                SapXep: row.sapXep,
                TxtCard1: row.txtCard1,
                CbGiaoDien: row.cbGiaoDien,
            },
            success: function (response) {
                if (response.status) {
                    toastr.success("Cập nhập thành công", "Thành Công", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                    myScripts.LoadTable();
                }
                else {
                    toastr.error(response.message, "Lỗi", { showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
                }
            },
        });
    }
};

$(document).on('change', '.cbGiaoDienSelect', function () {
    const $this = $(this);
    const selectedValue = $this.val();
    const maTrang = $this.data('matrang');
    const sapXep = $this.data('sapxep');
    const txtCard1 = $this.data('txtcard1');

    console.log("Đã chọn: ", selectedValue, maTrang, sapXep, txtCard1); // debug

    $.ajax({
        url: '/Admin/Slider/SaveData',
        type: 'POST',
        dataType: 'json',
        data: {
            MaTrang: maTrang,
            CbGiaoDien: selectedValue,
            SapXep: sapXep,
            TxtCard1: txtCard1
        },
        success: function (response) {
            if (response.status) {
                toastr.success("Cập nhật phân loại thiết bị thành công", "Thành Công");
            } else {
                toastr.error(response.message, "Lỗi");
            }
        }
    });
});

$(document).on('change', 'input[type="file"]', function () {
    var fileInput = this;
    var maTrang = $(this).attr('id').split('_')[1];

    var formData = new FormData();
    formData.append('files', fileInput.files[0]);
    formData.append('MaTrang', maTrang);

    $.ajax({
        url: '/Admin/Slider/UploadFile',
        type: 'POST',
        processData: false,
        contentType: false,
        data: formData,
        success: function (response) {
            if (response.status) {
                toastr.success("Tải ảnh thành công");
                myScripts.LoadTable();
            } else {
                toastr.error(response.message || "Lỗi khi tải ảnh");
            }
        }
    });
});


var myController = {
    deleteUrl: function (maTrang) {
        if (!confirm("Bạn có chắc muốn xóa ảnh không?")) return;

        $.ajax({
            url: '/Admin/Slider/DeleteFile',
            type: 'POST',
            dataType: 'json',
            data: { MaTrang: maTrang },
            success: function (res) {
                if (res.status) {
                    toastr.success("Xóa ảnh thành công", "Thành Công");
                    myScripts.LoadTable();
                } else {
                    toastr.error(res.message, "Lỗi");
                }
            },
            error: function () {
                toastr.error("Lỗi khi xóa ảnh", "Lỗi");
            }
        });
    }
};


function fm_Image(e, value, row, index) {
    return [
        '<div style="width:80px;justify-self: center;" class="">',
        '<a class="image-popup-vertical-fit" target="_blank" href="' + value.image + '">',
        ' <img class="d-block position-relative w-100" src="' + value.image + '" width="100" />',
        ' </a>',
        '</div>'
    ].join('');
};

function fm_UrlFile(e, value, row, index) {
    if (value.txtCard1 != "" && value.txtCard1 != null) {
        return [
            '<div style="margin-bottom:0px;width:200px">',
            '<a style="margin-left:5px" title="Tải về" href="' + value.txtCard1 + '" download="">Đã có ảnh <i class="fa fa-download"></i></a>',
            '<a style="margin-left:5px;color:red;" title="Xóa tệp đính kèm" href="javascript:myController.deleteUrl(\'' + value.maTrang + '\')"><i class="ti ti-trash"></i></a>',
            '</div>'
        ].join('');
    } else {
        return [
            '<div class="input-group" style="margin-bottom:0px;width:200px">',
            '<input type="file" class="btn-outline-info btn-sm waves-effect waves-light upload-file" id="txtCard1_' + value.maTrang + '" />',
            '</div>'
        ].join('');
    }
}


function fm_SapXep(e, value, row, index) {
    if (value.sapXep != "" && value.sapXep != null) {
        return [
            '<div style="text-align:center;width:110px">',
            ' <input class="form-control" type="number" id="SapXep_' + value.maTrang + '" value="' + value.sapXep + '" />',
            '</div>'
        ].join('');
    } else {
        return [
            '<div style="text-align:center;width:110px">',
            ' <input class="form-control" type="number" id="SapXep_' + value.maTrang + '" />',
            '</div>'
        ].join('');
    }
}

function fm_CbGiaoDien(e, value, row, index) {
    return [
        '<select class="form-control cbGiaoDienSelect" data-maTrang="' + value.maTrang + '" data-sapXep="' + value.sapXep + '" data-txtCard1="' + value.txtCard1 + '">',
        '<option value="">Chọn phân loại</option>',
        '<option value="PC"' + (value.cbGiaoDien === 'PC' ? ' selected' : '') + '>Máy tính</option>',
        '<option value="Mobile"' + (value.cbGiaoDien === 'Mobile' ? ' selected' : '') + '>Điện thoại</option>',
        '</select>'
    ].join('');
}
