﻿/**
* Feedback Floating Badge
*
* Display a floating Feedback Badge on page
*
* @author Dumitru Glavan
* @link http://dumitruglavan.com
* @version 1.0
* @requires jQuery v1.3.2 or later
*
* Examples and documentation at: http://dumitruglavan.com/jquery-feedback-badge/
* Official jQuery plugin page: http://plugins.jquery.com/project/feedback-badge
* Find source on GitHub: https://github.com/doomhz/jQuery-Feedback-Badge
*
* Dual licensed under the MIT and GPL licenses:
* http://www.opensource.org/licenses/mit-license.php
* http://www.gnu.org/licenses/gpl.html
*
*/
; (function ($) {
    $.fn.feedbackBadge = function (options) {
        this.config = { css: { 'position': 'absolute',
            'float': 'left',
            'display': 'none',
            'zIndex': '999'
        },
            'animate': true,
            'css3Safe': false,
            'float': 'left'
        };
        $.extend(this.config, options);
        this.config.css.float = this.config.float;

        this.window = $(window);
        var self = this;

        if (this.config.css3Safe) {
            this.badgeHeight = this.height();
            this.windowHeight = self.window.height();
            this.topDistance = ~ ~(+(this.windowHeight - this.badgeHeight) / 2); //parseInt((this.windowHeight - this.badgeHeight) / 2);
            this.config.css.position = 'absolute';

            if (typeof (this.config.css.marginTop) == 'undefined') {
                this.config.css.marginTop = this.getTopMiddleDistance(true);
            }

            if (this.config.animate) {
                self.window.scroll(function () {
                    self.stop().animate({ 'margin-top': self.getTopMiddleDistance(true) }, 1000);
                });
            } else {
                self.window.scroll(function () {
                    self.css('margin-top', self.getTopMiddleDistance(true));
                });
            }

            self.window.resize(function () {
                self.badgeHeight = self.height();
                self.windowHeight = self.window.height();
                self.topDistance = ~ ~(+(self.windowHeight - self.badgeHeight) / 2); //parseInt((self.windowHeight - self.badgeHeight) / 2);
                self.css('margin-top', self.getTopMiddleDistance(true));
            });
        } else {
            this.config.css.position = 'fixed';
            if (typeof (this.config.css.top) == 'undefined') {
                this.config.css.top = this.getTopMiddleDistance();
            }
        }

        if (typeof (this.config.onClick) == 'function') {
            $(this).bind('click', this.config.onClick);
        }

        $(this).css(this.config.css).prependTo('body').show();

        return this;
    },

$.fn.getTopMiddleDistance = function (inPixels) {
    if (inPixels) {
        return (this.topDistance + this.window.scrollTop() + 'px');
    } else {
        var badgeHeightPerc = $(this).height() * 100 / this.window.height();
        return ~ ~(+(100 - badgeHeightPerc) / 2) + '%'; //parseInt((100 - badgeHeightPerc) / 2) + '%';
    }
}
})(jQuery);