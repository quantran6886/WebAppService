var mySettingHome = {
    init: function () {
        this.LoadMenu();
        this.bindEvents();
    },

    LoadMenu: function () {
        $.ajax({
            url: '/Home/LoadMenu',
            type: "GET",
            dataType: "json",
            success: function (data) {
                if (data.status === true) {
                    var htmlChuyenKhoa = '';
                    var htmlChuyenKhoamoblie = '<li><a class="sub-menu-lst" href="/chuyen-khoa"  style="font-weight: 500 !important; font-family: Inter;">Tất cả</a></li>';
                    var htmlDichVu = '';
                    var htmlDichVumoblie = '<li><a class="sub-menu-lst" href="/dich-vu-dac-biet"  style="font-weight: 500 !important; font-family: Inter;">Tất cả</a></li>';
                    var htmlBaiViet = '';
                    var htmlBaiVietmoblie = '<li><a class="sub-menu-lst" href="/tin-tuc"  style="font-weight: 500 !important; font-family: Inter;">Tất cả</a></li>';
                    data.lstChuyenKhoa.forEach(function (item) {
                        htmlChuyenKhoa += '<li><a class="sub-menu-lst" href="' + item.link + '">' + item.tenGoi + '</a></li>';
                        htmlChuyenKhoamoblie += '<li><a class="sub-menu-lst-mb" href="' + item.link + '" style="font-weight: 500 !important; font-family: Inter;">' + item.tenGoi + '</a></li>';
                    });
                    data.lstDichVuDacBiet.forEach(function (item) {
                        htmlDichVu += '<li><a class="sub-menu-lst" href="' + item.link + '">' + item.tenGoi + '</a></li>';
                        htmlDichVumoblie += '<li><a class="sub-menu-lst-mb" href="' + item.link + '" style="font-weight: 500 !important; font-family: Inter;">' + item.tenGoi + '</a></li>';
                    });
                    data.lstBaiViet.forEach(function (item) {
                        htmlBaiViet += '<li><a class="sub-menu-lst" href="' + item.link + '">' + item.tenGoi + '</a></li>';
                        htmlBaiVietmoblie += '<li><a class="sub-menu-lst-mb" href="' + item.link + '" style="font-weight: 500 !important; font-family: Inter;">' + item.tenGoi + '</a></li>';
                    });
                    $('#menuChuyenKhoa').html(htmlChuyenKhoa);
                    $('#menuDichVuDacBiet').html(htmlDichVu);
                    $('#menuBaiViet').html(htmlBaiViet);
                    $('.slicknav_nav > li:contains("Chuyên Khoa") ul.sub-menu').html(htmlChuyenKhoamoblie);
                    $('.slicknav_nav > li:contains("Dịch Vụ Đặc Biệt") ul.sub-menu').html(htmlDichVumoblie);
                    $('.slicknav_nav > li:contains("Tin Tức") ul.sub-menu').html(htmlBaiVietmoblie);
                    reInitSlickNav();
                }
            },
            error: function () {
                console.warn("Lỗi khi tải menu");
            }
        });
    },

    bindEvents: function () {
        $('.translate-btn').on('click', function () {
            var lang = $(this).data('lang');
            mySettingHome.fcTranslate(lang);
        });
    },

    fcTranslate: function (lang) {
        var fromLang = 'vi';
        var toLang = lang;

        if (toLang === 'vi') {
            document.cookie = "googtrans=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
            document.cookie = "googtrans=; expires=Thu, 01 Jan 1970 00:00:00 UTC; domain=" + window.location.hostname + "; path=/;";
            setTimeout(function () { location.reload(); }, 300);
        } else {
            document.cookie = "googtrans=/" + fromLang + "/" + toLang + "; path=/;";
            document.cookie = "googtrans=/" + fromLang + "/" + toLang + "; domain=" + window.location.hostname + "; path=/;";
            setTimeout(function () { location.reload(); }, 300);
        }
    }

};

$(document).ready(function () {
    mySettingHome.init();
});
function reInitSlickNav() {
    // Xóa slicknav cũ nếu có
    $('.slicknav_menu').hide();
    $('#navigation').removeAttr('style').show();
    $('#navigation ul').removeAttr('style');

    // Gọi lại slicknav
    $('#navigation').slicknav({
        label: '',
        prependTo: ".main-header-navigation"
    });
    $('.slicknav_menu').show();
}