// Callback IDs:
// 1 - Paginating

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
    var Images = {
        init: function (options, elem) {
            var self = this;

            self.elem = elem;
            self.$elem = $(elem); // -- jQuery version of 'this'

            // -- query DOM for the elements of this control and cache those elements
            self.$nav = $('DIV.page_navigation', self.$elem);
            self.$hidden = $('INPUT[type=hidden]', self.$elem);

            // -- all Items for the dropdowns
            self.allItems = options;

            // -- default options are overwritten with the user passed options
            self.options = $.extend({}, $.fn.smartImagesPlugin.options, options);

            self.bindEvents();
        },

        // -- private helpers start

        // -- private helpers end

        bindEvents: function () {
            var self = this;

            $('A', self.$nav).each(function() {
                $(this).on('click', function() {
                    $('li', self.$elem).each(function(i, v){
                        if($(this).get(0).style.display == 'list-item')
                        {
                            self.$hidden.val(i);                    
                        }
                    });

                    self.callback(self.$elem, 1);
                });
            });
        },

        changeHidden: function (rowId, field, id, text) {
            var self = this;
            // -- create json object from hidden field value
            var currentJsonString = self.$hidden.val();
            var jsonObj = [];
            if ($.trim(currentJsonString) != 'null') {
                jsonObj = JSON.parse(currentJsonString);
            }

            // -- search for the row which will be changed
            var indexFound = -1;
            $.each(jsonObj, function (i, v) {
                if (v.Id == rowId) {
                    indexFound = i;
                }
            });

            // -- if row is found, than update it
            if (indexFound != -1) {
                var row = jsonObj[indexFound];
                // -- check which field of the row needs to be updated
                if (field == 'firstItem') {
                    row.FirstItem.Id = id;
                    row.FirstItem.Keyword = text;
                } else if (field == 'secondItem') {
                    row.SecondItem.Id = id;
                    row.SecondItem.Keyword = text;
                } else if (field == 'position') {
                    // -- if the position is updated then the id holds the new position
                    row.Position = id;
                }
            }

            // -- update hidden field value
            var newJsonString = 'null';
            if (jsonObj.length != 0) {
                newJsonString = JSON.stringify(jsonObj);
            }
            self.$hidden.val(newJsonString);
        },

        // callback for extending functionality
        callback: function(elem, optype) {
        }
    };

    $.fn.smartImagesPlugin = function (options) {
        // -- maintain chaining
        return this.each(function () {
            // -- create instance
            var images = Object.create(Images);
            // -- pass in the options(object or string) and the element on which this plugin is operating
            images.init(options, this);
            // -- if the user needs to change the plugin it can access it. e.g.: $.data( $('div.someClass')[0], 'smartImagesPlugin' )
            $.data(this, 'smartImagesPlugin', images);
        });
    };

    // -- options are immutable
    $.fn.smartImagesPlugin.options = {
        placeHolder: 0
    };

})(jQuery, window, document);
