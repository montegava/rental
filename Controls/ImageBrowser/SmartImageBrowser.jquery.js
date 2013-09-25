// Callback IDs:
// 1 - Dropdown changed

// Utility
// -- prototypal inheritence is not supported by all browsers, this makes it compatible.
if (typeof Object.create !== 'function') {
    Object.create = function (obj) {
        function F() { };
        F.prototype = obj;
        return new F();
    };
}

// -- $ will always refer to jQuery
// -- create a local scope for the 'window' and the 'document'
// -- 'undefined' will always be the jQuery undefined, cannot be overwritten
(function ($, window, document, undefined) {

    // -- store logic
    var ImageBrowser = {
        init: function (options, elem) {
            var self = this;

            self.elem = elem;
            self.$elem = $(elem); // -- jQuery version of 'this'

            // -- query DOM for the elements of this control and cache those elements
            self.$hidden = $('INPUT[type=hidden]', self.$elem);
            self.$imgBrowser = $('INPUT[type=file]', self.$elem);
            self.$contentDiv = $('DIV.imgpath', self.$elem);

            // -- default options are overwritten with the user passed options
            self.options = $.extend({}, $.fn.smartImageBrowserPlugin.options, options);

            if (self.options.isReadOnly != 1) {
                self.bindEvents();
            } else {
            }
        },

        // -- private helpers start

        // -- private helpers end

        bindEvents: function () {
            var self = this;

            // -- add dropdown change handler
            self.$imgBrowser.on('change', function() {
                var imgPath = $(this).val();
                var img = $('IMG', self.$contentDiv);
                img.attr('src', imgPath);
                self.updateHidden(imgPath);
            });
        },

        updateHidden: function (path) {
            var self = this;
            self.$hidden.val(path);
        }
    };

    $.fn.smartImageBrowserPlugin = function (options) {
        // -- maintain chaining
        return this.each(function () {
            // -- create instance
            var imageBrowser = Object.create(ImageBrowser);
            // -- pass in the options(object or string) and the element on which this plugin is operating
            imageBrowser.init(options, this);
            // -- if the user needs to change the plugin it can access it. e.g.: $.data( $('div.someClass')[0], 'smartImageBrowserPlugin' )
            $.data(this, 'smartImageBrowserPlugin', imageBrowser);
        });
    };

    // -- options are immutable
    $.fn.smartImageBrowserPlugin.options = {
        isReadOnly: 0
    };

})(jQuery, window, document);
