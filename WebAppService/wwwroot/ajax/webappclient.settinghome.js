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

                    var lstConfig = data.lstConfig;
                    if (lstConfig != null) {
                        $('.txtEmail').html(lstConfig.email);
                        $('.txtHotline').html(lstConfig.hotline);
                        $('.txtSoDienThoai').html(lstConfig.soDienThoai);
                        $('.txtWebsite').html(lstConfig.website);
                        $('.txtDiaChiPhongKham').html(lstConfig.diaChiPhongKham);
                        $('.urlMap').html(lstConfig.urlMap);
                        if (lstConfig.zalo != null && lstConfig.zalo != "") {
                            $('.txtZalo').attr('href', lstConfig.zalo);
                        } else {
                            $('.txtZalo').attr('href', '');
                        }
                        if (lstConfig.facebook != null && lstConfig.facebook != "") {
                            $('.txtFacebook').attr('href', lstConfig.facebook);
                        } else {
                            $('.txtFacebook').attr('href', '');
                        }
                        if (lstConfig.urlBaner != null && lstConfig.urlBaner != "") {
                            $('.urlBaner').css('background-image', `url('${lstConfig.urlBaner}')`);
                        } else {
                            $('.urlBaner').css('background-image', "url('/web/assets/imgs/farme.png')");
                        }
                    }

                    var loadPoster = data.loadPoster;
                    const POSTER_KEY = 'poster_dialog_v1';
                    if (loadPoster != null) {
                        $('.poster-hero').css('background-image', `url('${loadPoster.image}')`);
                        $('.txtposter-title').html(loadPoster.seoTittile);
                        $('.poster-desc').html(loadPoster.seoDescription);
                        $(function () {
                            const dialog = document.getElementById('posterDialog');
                            try {
                                if (!localStorage.getItem(POSTER_KEY)) {
                                    dialog.showModal();
                                }
                            } catch (e) {
                                dialog.showModal();
                            }

                            $('#closeDialog').on('click', closeDialog);
                            $('#ctaRegister').on('click', () => {
                                closeDialog();
                                window.location.href = '/lien-he';
                            });

                            $('#dontShow').on('click', () => {
                                try {
                                    localStorage.setItem(POSTER_KEY, '1');
                                } catch (e) { }
                                closeDialog();
                            });

                            function closeDialog() {
                                dialog.classList.add('closing');
                                setTimeout(() => dialog.close(), 250);
                            }
                        });
                    } else {
                        try {
                            localStorage.removeItem(POSTER_KEY);
                        } catch (e) { }
                    }
                }
            },
            error: function () {
                console.warn("Lỗi khi tải menu");
            }
        });
    },

    bindEvents: function () {
        $(document).off('click', '.translate-btn').on('click', '.translate-btn', function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            var lang = $(this).data('lang');
            currentLang = lang;
            mySettingHome.fcTranslate(lang);
        });
    },

    getLanguage: function () {
        var match = document.cookie.match(/googtrans=\/vi\/(\w+)/);
        if (match && match[1]) {
            return match[1];
        }
        return 'vi'; 
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
    $('.slicknav_menu').hide();
    $('#navigation').removeAttr('style').show();
    $('#navigation ul').removeAttr('style');

    $('#navigation').slicknav({
        label: '',
        prependTo: ".main-header-navigation"
    });
    $('.slicknav_menu').show();
}