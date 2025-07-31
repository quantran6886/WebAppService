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
