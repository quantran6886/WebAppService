var myScripts = {
    init: function myfunction() {
        myScripts.RegesterEvent();
        myScripts.LoadData();
    },

    RegesterEvent: function () {

        $("#LoaiDanhMuc").off("change").on("change", function () {
            myScripts.LoadTable();
        });

        $("#btnReset").off("click").on("click", function () {
            myScripts.ResetForm();
        });
    },

    ResetForm: function () {
        toastr.success("Tạo mới thành công","Thành công",{ showMethod: "slideDown", hideMethod: "slideUp", timeOut: 1000 });
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

};
myScripts.init();