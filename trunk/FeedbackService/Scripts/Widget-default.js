/// <reference path="jquery-ui-1.8.11.js" />
/// <reference path="jquery-1.5.1-vsdoc.js" />

$(document).ready(function () {


    /**
    * Set up feedback box on left side
    */
    $('#feedback-badge').feedbackBadge({
        css3Safe: $.browser.safari ? true : false, //this trick prevents old safari browser versions to scroll properly
        onClick: function () {

            return false;
        }
    });

    var speed = 200;
    var overlay = $("#feedback-overlay");
    //overlay.hide();
    //    $(".feedback-widget").load('google.com', function (response, status, xhr) {
    //        if (status == "error") {
    //            var msg = "Sorry but there was an error: ";
    //            $("#feedback-error").html(msg + xhr.status + " " + xhr.statusText);
    //        }
    //    });

    $("#feedback-widget").load('/Feedback/Create/00000000-0000-0000-0000-000000000001', function (response, status, xhr) {
        if (status == "error") {
            var msg = "Sorry but there was an error: ";
            $("#feedback-error").html(msg + xhr.status + " " + xhr.statusText);
        }
    });



    //    GitHubAPI.Repos = function (username, callback) {
    //        //requestURL = "http://github.com/api/v2/json/repos/show/"+"&callback=?" + username + "?callback=?";
    //        $.getJSON("http://services.digg.com/stories/top?appkey=http%3A%2F%2Fmashup.com&type=javascript&callback=?",
    //         function (json, status) {

    //             $("#feedback-error").html(msg + xhr.status + " " + xhr.statusText);

    //            callback(json.repositories, status);
    //        });
    //    }
    // Do your magic in here when you click the badge
    // Now I just show a simple popup, you could use the jQuery UI dialog
    //    var div = $('<div id="popup"></div>');
    //    div.load('feedback_form.html', function () { $(window).scroll(); });
    //    $('body').prepend(div);

    //    //After ataching the popup to the dom - load the form by ajax
    //    $('#feedback-form').live('submit', function () {
    //        //Do your magic in here when the form submit button is clicked
    //        alert('Magic!');
    //        return false;
    //    });
    //    $('#close-bt').live('click', function () {
    //        //Do your magic in here when the form cancel button is clicked
    //        div.remove();
    //    });



    var resizeOverlay = function () {
        overlay.height($(window).height());
        overlay.width($(window).width());
    };

    var showOverlay = function () {
        $("body").addClass('feedback-page');


        resizeOverlay();
        overlay.fadeIn(speed);
    };

    var hideOverlay = function () {
        overlay.fadeOut(speed);
        $("body").removeClass('feedback-page');
    };

    $(window).bind('resize', resizeOverlay);
    $("#feedback-badge").bind('click', showOverlay);
    $('#feedback-widget-close').click(hideOverlay);
    //overlay.click(hideOverlay);












    //    });

    //    $(":input[data-autocomplete]").each(function () {
    //        $(this).autocomplete({ source: $(this).attr("data-autocomplete") });
    //    });

    //    $("#searchForm").submit(function () {
    //        $.getJSON(
    //                    $(this).attr("action"),
    //                    $(this).serialize(),
    //                    function (data) {
    //                        var result = $("#searchTemplate").tmpl(data);
    //                        $("#searchResults").empty().append(result);
    //                    }
    //                );
    //        return false;
    //    });
})