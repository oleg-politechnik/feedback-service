/// <reference path="jquery-ui-1.8.11.js" />
/// <reference path="jquery-1.5.1-vsdoc.js" />

$(document).ready(function () {


    /**
    * Set up feedback box on left side
    */
    $('#feedback-link').attr("id", "feedback-badge");
    $('#feedback-badge').empty().hide()
    //    .href("#")
    .feedbackBadge({
        css3Safe: $.browser.safari ? true : false, //this trick prevents old safari browser versions to scroll properly
        onClick: function () {
            return false;
        }
    });

    $('body').append("<div id='feedback-overlay'></div>");
    $('body').append("<div id='feedback-widget'><a href='#' id='feedback-widget-close' style='float: right;'>Закрыть</a><div id='feedback-widget-contents'></div></div>");
    $("#feedback-widget-contents").append("<img src=/Content/images/20-0.gif>");
    var speed = 200;
    var overlay = $("#feedback-overlay");
    var widget = $("#feedback-widget");
    widget.hide();





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

        overlay.height(Math.max($(window).height(), $(widget).height() + 600));
        overlay.width($(window).width());

        $("#feedback-widget")
    };

    var showOverlay = function () {
        $("#feedback-widget-contents").load('/Feedback/Create/00000000-0000-0000-0000-000000000001',
        function (response, status, xhr) {
            if (status == "error") {
                $("#feedback-widget-contents").addClass("feedback-error")
                .html("Беда: " + xhr.status + " " + xhr.statusText);
            }
        });

        resizeOverlay();
        overlay.fadeIn(speed);
        widget.css('top', $('#feedback-badge').css('top'));
        widget.show(); //.attr('display', 'block');

        //        $("#feedback-widget").top($(window).height());
        //        $("#feedback-widget").left($(window).width()/2);
    };

    var hideOverlay = function () {
        overlay.fadeOut(speed);
        $("#feedback-widget").hide();
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