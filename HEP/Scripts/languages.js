var mySiteNamespace = {};

mySiteNamespace.switchLanguage = function (lang) {
    $.cookie('language', lang);
    window.location.reload();
};

$(document).ready(function () {
    // attach mySiteNamespace.switchLanguage to click events based on css classes
    $('.lang-russian').click(function () { mySiteNamespace.switchLanguage('ru'); });
     
});
