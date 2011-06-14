/// <reference path="jquery-1.5.1-vsdoc.js" />
/// <reference path="jquery-ui-1.8.11.js" />
/// <reference path="jquery.tmpl.js" />

$(document).ready(function () {

    $(":input[data-autocomplete]").each(function () {
        $(this).autocomplete({ source: $(this).attr("data-autocomplete") });
    });

    $("#searchForm").submit(function () {
                $.getJSON(
                    $(this).attr("action"),
                    $(this).serialize(),
                    function (data) {
                var result = $("#searchTemplate").tmpl(data);
                $("#searchResults").empty().append(result);
            }
                );
        return false;
    });
})