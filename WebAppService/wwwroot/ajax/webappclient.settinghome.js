var mySettingHome = {
    init: function () {
        this.bindEvents();
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

        document.cookie = "googtrans=/" + fromLang + "/" + toLang + "; path=/";
        document.cookie = "googtrans=/" + fromLang + "/" + toLang + "; domain=" + window.location.hostname + "; path=/";

        showLoading();

        setTimeout(function () {
            location.reload();
        }, 500);
    }
};

$(document).ready(function () {
    mySettingHome.init();
});
