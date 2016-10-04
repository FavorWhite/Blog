$(function () {
    $("[source]").each(function () {
        var target = $(this);
        target.autocomplete({ source: target.attr("source") });
    });
});