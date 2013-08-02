﻿(function ($) {

    $.fn.addHints = function () {

        $(this).each(function () {
            if ($(this).parent("label").length) {
                return;
            }
            var filename = window.location.href.match(/.*\/(.*)\..*?$/)[1];
            
            var div = $("#tooltip");
            if (div.length == 0) {
                div = $('<div id="tooltip">').toggle(false).appendTo(document.body);
            }

            var text = $(this).text().replace(/^[\s\*]+|[\s*]+$/g, ""); ;
            var hint = SpiderLanguage["HINT_" + text] || SpiderLanguage["HINT_" + filename + "_" + text] || "Tooltip for text <b>" + filename + "_" + text + "</b>";

            //DEBUG

            if (hint) {
                $(this).removeAttr("title");
                $('<a class="hint">ⓘ</a>').appendTo(this)
                    .hover(
                        function (event) {
                            div.html(hint)
                                .stop(true, true)
                                .fadeIn()
                                .position({
                                my: "left top",
                                at: "right bottom",
                                of: event, 
                                offset: "2 2"
                            });
                        },
                        function () {
                            div.stop(true, true).fadeOut();
                        });
            }
        });
    };


    $.expr[":"].containsNoCase = function (el, i, m) {
        var search = m[3];
        if (!search) return false;
        return search.toLowerCase() == $(el).text().toLowerCase();
    };

    /*some fixes for the tab menu - an horizontal line on tabs to cover the display area canvas top shadow line*/
    $.fn.fixTabMenu = function () {
        $(".tabArea A.selected").each(function () {
            var width = parseInt($(this).css("padding-left"), 10) + parseInt($(this).css("padding-right"), 10) + $(this).width();
            $("SPAN", this).css("width", width + "px");
        });
    }

    $.fn.adaptTableWidth = function (tableName, tableWidth, columnsWidth) {
        var table = $("." + tableName);
        if (table.length > 0) {
            if (table.closest("SECTION").hasClass("scrollGrid")) {
                //for ie
                //for other: 

                //table.css("width", tableWidth[tableName]);
                table.css("min-width", tableWidth[tableName]);
                table.css("max-width", tableWidth[tableName]);

                if ($.browser.msie) {
                    //$("TBODY", table).css("width", parseInt(tableWidth[tableName], 10) + 13);
                    $("TBODY", table).css("min-width", parseInt(tableWidth[tableName], 10));
                    $("TBODY", table).css("max-width", parseInt(tableWidth[tableName], 10));
                }
                else {
                    $("TBODY", table).css("width", parseInt(tableWidth[tableName], 10)); // - 2);
                }
                //console.log("thead width:" + parseInt(tableWidth[tableName], 10))
                //$("THEAD", table).css("width", parseInt(tableWidth[tableName], 10));
                $("THEAD", table).css("min-width", parseInt(tableWidth[tableName], 10));
                $("THEAD", table).css("max-width", parseInt(tableWidth[tableName], 10));
                var height = $("TBODY", table).height();
                var correctionHeight = parseInt($("THEAD TH", table)[0].offsetHeight, 10);

                //bug fix chrome
                if ($.browser.safari || $.browser.chrome) {
                    correctionHeight = correctionHeight - 1;
                }

                //if (correctionHeight < 31) {
                //  correctionHeight = 31;
                //}
                $(table).css("height", (height + correctionHeight) + "px");
                $("TBODY", table).css("top", correctionHeight + "px");


                var nrOfColumns = $("THEAD TR TH", table).length;
                $("THEAD TR TH", table).each(function (index) {
                    var colWidth = parseInt(columnsWidth[index], 10);
                    if ($.browser.safari || $.browser.chrome) {//bug fix
                        //$(this).css("width", colWidth + "px");
                        $(this).css("min-width", colWidth + "px");
                        $(this).css("max-width", colWidth + "px");
                    }
                    else {
                        //console.log(colWidth.toString());
                        //$(this).css("width", colWidth + "px");
                        $(this).css("min-width", colWidth + "px");
                        $(this).css("max-width", colWidth + "px");

                    }
                    $("A", this).css("background-position", (colWidth - 20) + "px");
                });
                $("TBODY TR:first-child TD", table).each(function (index) {
                    var colWidth = parseInt(columnsWidth[index], 10);
                    ///if (index == nrOfColumns - 1) colWidth = colWidth - 17; //when we have a scroll situation
                    //if ($.browser.safari || $.browser.chrome) {//bug fix
                    //$(this).css("width", colWidth + "px");
                    //    $(this).css("min-width", colWidth + "px");
                    //    $(this).css("max-width", colWidth + "px");
                    //$(this).css("padding-right", (parseInt($(this).css("padding-right"), 10) + 1) + "px");
                    //}
                    //else {

                    if (index == nrOfColumns - 1)
                        $(this).css("width", "100%");
                    else {

                        //$(this).css("width", colWidth + "px");
                        $(this).css("min-width", colWidth + "px");
                        $(this).css("max-width", colWidth + "px");
                    }
                    //console.log(colWidth);
                    //}
                    $("A", this).css("background-position", (parseInt(columnsWidth[index], 10) - 20) + "px");
                });

            }
            else {
                table.css("width", tableWidth[tableName]);
                $("THEAD TR TH", table).each(function (index) {
                    if ($.browser.safari || $.browser.chrome) {//bug fix
                        $(this).css("width", (parseInt(columnsWidth[index], 10)) + "px"); //+ parseInt($(this).css("padding-left"), 10) + parseInt($(this).css("padding-right"), 10) + parseInt($(this).css("border-right-width"), 10)
                    }
                    else {
                        $(this).css("width", columnsWidth[index]);
                    }
                    $("A", this).css("background-position", (parseInt(columnsWidth[index], 10) - 20) + "px");
                });
            } //else
        } //end if

        $("TR", $(table)).each(function (index) {
            $(this).hover(function (event) {
                $(this).addClass("hover");
            }, function (event) {
                $(this).removeClass("hover");
            });
            $("TD.icon A IMG", $(this)).hover(function (event) {
                if (/empty\.gif/.test($(this).attr("src"))) {

                    $(this).parent("a").remove();
                    /*   $(this).removeAttr('title').parent().removeAttr('title')
                    .hover(function(event) {
                    $(this).css("cursor", "auto");
                    }, function (event) { })
                    .click(function(event) {
                    event.preventDefault(); return false;
                    });*/
                }
                else {
                    $(this).attr("src", $(this).attr("src").replace(/_sel\.png/, ".png").replace(/\.png/, "_sel.png"));
                }
            }, function (event) {
                $(this).attr("src", $(this).attr("src").replace(/_sel\.png/, ".png"));
            });
            $("TD.tooltipFix", $(this)).each(function (index) {
                var title = $("span", $(this)).attr("title");
                $("img", $(this)).attr("title", title);
                $("img", $(this)).attr("alt", title);
            });
        });
    }

    $.fn.composeDialog = function (DEF) {
        DEF.NAME = DEF.staticContent.toUpperCase().replace(/(\W+)/g, "_");
        DEF.staticContent = DEF.staticContent.replace("-area", "") + "-area";
        if ($("#" + DEF.staticContent).length > 0) {
            $("#" + DEF.staticContent).remove();
        }
        var template = $(".tplDlg").html();
        template = "<section id=\"" + DEF.staticContent + "\" class=\"dlgStatic\">" + template + "</section>";
        template = template.replace("{title}", DEF.data.title).replace("{content}", DEF.data.content).replace("{panel}", DEF.data.panel);
        template = template.replace("{sectionID_UP}", DEF.NAME).replace("{sectionID}", DEF.staticContent).replace(/\{dlgID\}/g, DEF.data.id);
        $("body").append(template);

        $(".inDialog", $("#" + DEF.staticContent)).css("width", DEF.data.width);
        $.openDialog(DEF);
    }

    $.fn.composeDialog2 = function (DEF, itemID) {
        DEF.data.id = itemID;
        $.openDialog(DEF);
    }

    $.fn.composeDialogWithPlatform = function (DEF, itemID, platformID) {
        DEF.data.id = itemID;
        DEF.data.platformid = platformID;
        $.openDialog(DEF);
    }

    $.fn.composeDialog3 = function (DEF, listIDs) {
        var ids = '';
        var dataJson = $.parseJSON(listIDs);
        if (dataJson != null) {
            $.each(dataJson, function (key, val) {
                if (val['Item1'].Id != '') {
                    if (key > 0) {
                        ids += ',';
                    }
                    ids += val['Item1'].Id;
                }
            })
            DEF.data.ids = ids;
        }
        $.openDialog(DEF);
    }

    $.fn.composeDialog4 = function (DEF, platformIDs, titleIDs) {
        $.fn.composeDialog441(DEF, platformIDs, titleIDs, null, null);
        /*
        var ids = '';
        var dataJson = $.parseJSON(platformIDs);
        if (dataJson != null) {
        $.each(dataJson, function (key, val) {
        if (val['Item1'].Id != '') {
        if (key > 0) {
        ids += ',';
        }
        ids += val['Item1'].Id;
        }
        })

        DEF.data.platformIds = ids;
        }
        var titleids = '';
        if (titleIDs != null) {
        $.each(titleIDs, function (key, val) {
        if (val.id.match("mainAreaOfPage__lwInfoListEdit_hfID*") == "mainAreaOfPage__lwInfoListEdit_hfID") {
        if (key > 0) {
        titleids += ',';
        }
        titleids += val.value
        }
        })

        DEF.data.titleids = titleids;
        }

        $.openDialog(DEF);
        */
    }

    $.fn.composeDialog441 = function (DEF, platformIDs, titleIDs, lisenceStart, lisenceEnd) {
        DEF.data.lisencestart = lisenceStart;
        DEF.data.lisenceend = lisenceEnd;
        var ids = '';
        var dataJson = $.parseJSON(platformIDs);
        if (dataJson != null) {
            $.each(dataJson, function (key, val) {
                if (val['Item1'].Id != '') {
                    if (key > 0) {
                        ids += ',';
                    }
                    ids += val['Item1'].Id;
                }
            })

            DEF.data.platformIds = ids;
        }
        var titleids = '';
        if (titleIDs != null) {
            $.each(titleIDs, function (key, val) {
                if (val.id.match("mainAreaOfPage__lwInfoListEdit_hfID*") == "mainAreaOfPage__lwInfoListEdit_hfID") {
                    if (key > 0) {
                        titleids += ',';
                    }
                    titleids += val.value
                }
            })

            DEF.data.titleids = titleids;
        }

        $.openDialog(DEF);
    }

    $.fn.ddl_chooseAction = function (val) {
        val = $(val).val();
    }

    //Working with left menu
    $.fn.leftMenuAction = function () {
        /*var prevSelMenuItem = null;
        $("SECTION.left-content MENU > LI").hover(
        function () {
        prevSelMenuItem = $("SECTION.left-content MENU > LI[class~=hover]");
        $("SECTION.left-content MENU > LI").removeClass("hover");
        $(this).addClass("hover");
        },
        function () {
        $(this).removeClass("hover");
        $(prevSelMenuItem).addClass("hover");
        }
        );*/
        var prevSelMenuItem2 = null;
        $("SECTION.suboptions MENU > LI").hover(
		    function () {
		        prevSelMenuItem2 = $("SECTION.suboptions MENU > LI[class^=selected]");
		        $("SECTION.suboptions MENU > LI").removeClass("selected");
		        $(this).addClass("selected");
		    },
		    function () {
		        $(this).removeClass("selected");
		        $(prevSelMenuItem2).addClass("selected");
		    }
	    );
    } //end leftMenuAction

    //working with all checkboxes from one table (no matter which table)
    $.fn.workingWithTableCheckboxes = function () {

        //storing all selected checkboxes from a table (if we have a table with this situation)
        $.fn.listTableCheckBoxes = []; //item format = {id: '0', name: 'name 0'}
        //handling each single checkbox item selection form web browser page
        $('TABLE * input[type=checkbox]:not([name*=All])').filter(function (index, obj) {
            return $(this).parents(".template").length == 0 && ($(this).closest("SECTION").hasClass("custom") ? false : true);
        }).ezMark().click(function (event) {
            if ($(this).attr("checked") != undefined) {//checked
                //if we have a table with check-all input some checking operation will be done
                fillCheckAll(this, "table");
            }
            else {//unchecked 
                //if we have a table with check-all input some checking operation will be done
                unfillCheckAll(this, "table");
            }

            function fillCheckAll(context, parent) {
                var table = $(context).closest(parent);
                if (table.length > 0) {

                    //if some checking number status exist I will count it. Ex: GROUPS AND PROVIDERS MANAGEMENT
                    var countNr = 0;
                    $(context).closest("TR").find("TD.nrCount").each(function () { countNr += parseInt($(this).text(), 10) });

                    var statusItem = $(context).closest("TR").find("td.statusCheck input[type=hidden][name*=Status]").val();
                    if (statusItem == undefined) {
                        statusItem = "false";
                    }
                    //composing the list with selected checkboxes from list
                    $.fn.listTableCheckBoxes.push({ id: $(context).closest("TD").find("input[type=hidden][name*=hfID]").val(), name: $(context).closest("TD").find("input[type=hidden][name*=hfName]").val(), nr: countNr, status: statusItem, platformId: $(context).closest("TD").find("input[type=hidden][name*=hfPlatform]").val() });
                    var currentSelected = $('input[type=checkbox]:not([name*=All]):checked', table).length;
                    var totalElements = $('input[type=checkbox]:not([name*=All])', table).length;
                    var checkAllElem = $('input[type=checkbox][name*=All]', table);
                    if (currentSelected == totalElements) {
                        checkAllElem.attr("checked", "checked");
                        checkAllElem.trigger('change');
                    }
                }
            }

            function unfillCheckAll(context, parent) {
                var table = $(context).closest(parent);
                if (table.length > 0) {

                    //cleaning the list with selected checkboxes from list
                    if ($.fn.listTableCheckBoxes.length > 0) {
                        var temp_array = [];
                        for (var i = 0; i < $.fn.listTableCheckBoxes.length; i++) {
                            if ($(context).closest("TD").find("input[type=hidden][name*=hfID]").val() != $.fn.listTableCheckBoxes[i]['id']) {
                                temp_array.push($.fn.listTableCheckBoxes[i]);
                            }
                        }
                        $.fn.listTableCheckBoxes = []; $.fn.listTableCheckBoxes = temp_array; temp_array = null;
                    } //end if
                    var currentSelected = $('input[type=checkbox]:not([name*=All]):checked', table).length;
                    var totalElements = $('input[type=checkbox]:not([name*=All])', table).length;
                    var checkAllElem = $('input[type=checkbox][name*=All]', table);
                    if (currentSelected != totalElements) {
                        checkAllElem.removeAttr('checked');
                        checkAllElem.trigger('change');
                    }
                }
            }
        });

        //handling the check box element which check and uncheck all the children checkboxes
        $('TABLE * input[type=checkbox][name*=All]').filter(function (index, obj) { return $(this).closest("SECTION").hasClass("custom") ? false : true; }).ezMark().click(function () {
            var table = $(this).closest("TABLE");
            if ($(this).attr('checked') != undefined) {//check all
                $('input[type=checkbox]:not([name*=All])', table).each(function () {
                    $.fn.listTableCheckBoxes = [];
                    var input_name = $('input[type=hidden][name*=hfName]', $(table));
                    var input_id = $('input[type=hidden][name*=hfID]', $(table));
                    var count_nr = $('td.nrCount', $(table));
                    var platform_id = $('input[type=hidden][name*=hfPlatform]', $(table));

                    var statusItem = $('td.statusCheck input:hidden[id*=Status]', $(table));
                    if (input_id.length > 0) {
                        for (var i = 0; i < input_id.length; i++) {
                            var statusItemCheck = '';
                            if ($(statusItem[i]).length > 0) {
                                statusItemCheck = $(statusItem[i]).val();
                            }
                            $.fn.listTableCheckBoxes.push({ id: $(input_id[i]).val(), name: $(input_name[i]).val(), nr: parseInt($(count_nr[i]).text(), 10), status: statusItemCheck, platformId: $(platform_id[i]).val() });
                        }
                    }
                    $(this).attr("checked", "checked");
                    $(this).trigger('change');
                });
            }
            else {//uncheck all
                $('input[type=checkbox]:not([name*=All])', table).each(function () {
                    $(this).removeAttr('checked');
                    $(this).trigger('change');
                });
                $.fn.listTableCheckBoxes = [];
            }
        });
    } //end workingWithTableCheckboxes




    //applying some action on the buttons from filter and table buttons
    $.fn.workingWithPanelButtons = function () {
        //checking if the buttons: DELETE ALL, DEACTIVATE ALL, ACTIVATE ALL have at least one item checked from the grid
        $("a.check").click(function (event) {
            var id = $(this).attr("id");

            var regx_deleteAll = /DeleteAll/;
            var regx_deactivateAll = /DeactivateAll/;
            var regx_activateAll = /ActivateAll/;
            var regx_exportAll = /ExportAll/;
            var regx_deployAll = /DeployingAll/;


            var totallCount = $('table.grid input[type=checkbox]:not([name*=All])').length; //all existing checkboxes, except the first checkbox from head of table.
            var checkedCount = $('table.grid input[type=checkbox]:not([name*=All]):checked').length; //all checked checkboxes , except the first checkbox from head of table

            if (checkedCount > 0) {
                if (regx_deleteAll.test(id)) {
                    var restrictDeletion = false;
                    var statusDeletion = false;
                    for (var i = 0; i < $.fn.listTableCheckBoxes.length; i++) {
                        if ($.fn.listTableCheckBoxes[i]['nr'] > 0) {
                            restrictDeletion = true;
                            break;
                        }
                        else if ($.fn.listTableCheckBoxes[i]['status'].toLowerCase() == "true") {
                            statusDeletion = true;
                            break;
                        }
                    }
                    if (restrictDeletion) {
                        DEF_ALERTRESTRICTELEMENTS.data.targetID = id;
                        DEF_ALERTRESTRICTELEMENTS.data.elementsName = $.fn.listTableCheckBoxes;
                        $.fn.composeDialog(DEF_ALERTRESTRICTELEMENTS);
                    }
                    else if (statusDeletion) {
                        DEF_ALERTDELETEONEITEMSTATUS.data.targetID = 0;
                        DEF_ALERTDELETEONEITEMSTATUS.data.elementsName = [];
                        $.fn.composeDialog(DEF_ALERTDELETEONEITEMSTATUS);
                    }
                    else {

                        if (totallCount == checkedCount) {
                            DEF_ALERTDELETEALLELEMENTS.data.targetID = id;
                            DEF_ALERTDELETEALLELEMENTS.data.elementsName = $.fn.listTableCheckBoxes;
                            $.fn.composeDialog(DEF_ALERTDELETEALLELEMENTS);
                        } else {
                            DEF_ALERTDELETEELEMENTS.data.targetID = id;
                            DEF_ALERTDELETEELEMENTS.data.elementsName = $.fn.listTableCheckBoxes;
                            $.fn.composeDialog(DEF_ALERTDELETEELEMENTS);
                        }
                    }

                }
                else if (regx_deactivateAll.test(id)) {

                    if (totallCount == checkedCount) {
                        DEF_ALERTDEACTIVATEALLELEMENTS.data.targetID = id;
                        DEF_ALERTDEACTIVATEALLELEMENTS.data.elementsName = $.fn.listTableCheckBoxes;
                        $.fn.composeDialog(DEF_ALERTDEACTIVATEALLELEMENTS);
                    }
                    else {
                        DEF_ALERTDEACTIVATEELEMENTS.data.targetID = id;
                        DEF_ALERTDEACTIVATEELEMENTS.data.elementsName = $.fn.listTableCheckBoxes;
                        $.fn.composeDialog(DEF_ALERTDEACTIVATEELEMENTS);
                    }
                }
                else if (regx_activateAll.test(id)) {

                    if (totallCount == checkedCount) {
                        DEF_ALERTACTIVATEALLELEMENTS.data.targetID = id;
                        DEF_ALERTACTIVATEALLELEMENTS.data.elementsName = $.fn.listTableCheckBoxes;
                        $.fn.composeDialog(DEF_ALERTACTIVATEALLELEMENTS);
                    }
                    else {
                        DEF_ALERTACTIVATEELEMENTS.data.targetID = id;
                        DEF_ALERTACTIVATEELEMENTS.data.elementsName = $.fn.listTableCheckBoxes;
                        $.fn.composeDialog(DEF_ALERTACTIVATEELEMENTS);
                    }
                }
                else if (regx_exportAll.test(id)) {
                    //HANDLER FOR EXPORT ALL ITEMS
                    DEF_ALERTEXPORTELEMENTS.data.targetID = id;
                    DEF_ALERTEXPORTELEMENTS.data.elementsName = $.fn.listTableCheckBoxes;
                    $.fn.composeDialog(DEF_ALERTEXPORTELEMENTS);
                }
                else if (regx_deployAll.test(id)) {
                    return true;
                }
            }
            else {
                $.fn.composeDialog(DEF_ALERTSELECTANELEMENT);
            }
            return false;
        });
    } //end workingWithPanelButtons

    $.fn.toggleGroupsByType = function (value) {
        $('.groupRightRow').hide();
        $('.group_type_' + value).show();

        $('.groupRightsChks').hide();
        $('.chkAvailableForType' + value).show();
    }

    //working with all checkboxes from one table (no matter which table)
    $.fn.groupUsersRightsCheckboxes = function () {
        //handling each single checkbox item selection form web browser page
        $('TABLE.rights * input[type=checkbox]:not([name*=All])').ezMark().click(function (event) {
            var entity = /Entity/.test($(this).attr("id"));
            var view = /View/.test($(this).attr("id"));
            var edit = /Edit/.test($(this).attr("id"));
            var tempName = $(this).attr("id");
            if ($(this).attr("checked") != undefined) {//checked
                if (entity) {
                    tempName = tempName.replace(/Entity/, "View")
                    $("#" + tempName).attr("checked", "checked").trigger('change');
                }
                else if (view) {
                    tempName = tempName.replace(/View/, "Entity")
                    $("#" + tempName).attr("checked", "checked").trigger('change');
                }
                else if (edit) {
                    tempName = tempName.replace(/Edit/, "View")
                    $("#" + tempName).attr("checked", "checked").trigger('change');
                    tempName = tempName.replace(/View/, "Entity")
                    $("#" + tempName).attr("checked", "checked").trigger('change');
                }
                //if we have a table with check-all input some checking operation will be done
                fillCheckAll(this, "table");
            }
            else {//unchecked
                if (entity) {
                    tempName = tempName.replace(/Entity/, "View")
                    $("#" + tempName).removeAttr("checked").trigger('change');
                    tempName = tempName.replace(/View/, "Edit")
                    $("#" + tempName).removeAttr("checked").trigger('change');
                }
                else if (view) {
                    tempName = tempName.replace(/View/, "Entity")
                    $("#" + tempName).removeAttr("checked").trigger('change');
                    tempName = tempName.replace(/Entity/, "Edit")
                    $("#" + tempName).removeAttr("checked").trigger('change');
                }
                else if (edit) {
                }

                //if we have a table with check-all input some checking operation will be done
                unfillCheckAll(this, "table");
            }

            function fillCheckAll(context, parent) {
                var table = $(context).closest(parent);
                if (table.length > 0) {
                    //composing the list with selected checkboxes from list
                    var currentSelected = $('input[type=checkbox]:not([name*=All]):checked', table).length;
                    var totalElements = $('input[type=checkbox]:not([name*=All])', table).length;
                    var checkAllElem = $('input[type=checkbox][name*=All]', table);
                    if (currentSelected == totalElements) {
                        checkAllElem.attr("checked", "checked");
                        checkAllElem.trigger('change');
                    }
                }
            } //end fillCheckAll

            function unfillCheckAll(context, parent) {
                var table = $(context).closest(parent);
                if (table.length > 0) {
                    var currentSelected = $('input[type=checkbox]:not([name*=All]):checked', table).length;
                    var totalElements = $('input[type=checkbox]:not([name*=All])', table).length;
                    var checkAllElem = $('input[type=checkbox][name*=All]', table);
                    if (currentSelected != totalElements) {
                        checkAllElem.removeAttr('checked');
                        checkAllElem.trigger('change');
                    }
                }
            } //end unfillCheckAll
        });


        //handling the check box element which check and uncheck all the children checkboxes
        $('TABLE.rights * input[type=checkbox][name*=All]').ezMark().click(function () {
            var table = $(this).closest("TABLE");
            if ($(this).attr('checked') != undefined) {//check all
                $('input[type=checkbox]:not([name*=All])', table).each(function () {
                    $(this).filter(function (index, obj) {
                        return /Edit/.test($(obj).attr("id")) ? false : true;
                    }).attr("checked", "checked");
                    $(this).trigger('change');
                });
            }
            else {//uncheck all
                $('input[type=checkbox]:not([name*=All])', table).each(function () {
                    $(this).removeAttr('checked');
                    $(this).trigger('change');
                });
                $.fn.listTableCheckBoxes = [];
            }
        });
    };

    $.fn.actionLinksFromTable = function () {
        $("TABLE.grid TD A[id*=Delete]").click(function (event) {
            //prevent any action on click event if the "noaction" class was setup on it
            if ($(this).hasClass("noaction")) { event.preventDefault(); return; }
            //do nothing if we have an empty image instead of delete icon image
            if (/empty\.gif/.test($("img", $(this)).attr("src"))) {
                $(this).removeAttr('title');
                $("img", $(this)).removeAttr('title');
                event.preventDefault();
                return false;
            }

            var firstCol = $(this).closest("TR").find("TD:lt(1)");
            var count_nr = parseInt($('td.nrCount', $(this).closest("TR")).text(), 10);

            var status = $('td.statusCheck input:hidden[id*=Status]', $(this).closest("TR"));
            if (status.length > 0) {
                status = $(status).val();
            }
            else {
                status = 'false';
            }
            if (count_nr > 0) {
                DEF_ALERTRESTRICTELEMENT.data.elementsName = [{ id: firstCol.find("input[type=hidden][name*=hfID]").val(), name: firstCol.find("input[type=hidden][name*=hfName]").val()}];
                DEF_ALERTRESTRICTELEMENT.data.targetID = $(this).attr('id');
                $.fn.composeDialog(DEF_ALERTRESTRICTELEMENT);
            }
            else if (status.toLowerCase() == "true") {
                DEF_ALERTDELETEONEITEMSTATUS.data.elementsName = [];
                DEF_ALERTDELETEONEITEMSTATUS.data.targetID = 0;
                $.fn.composeDialog(DEF_ALERTDELETEONEITEMSTATUS);
            }
            else {
                DEF_ALERTDELETEONEITEM.data.elementsName = [{ id: firstCol.find("input[type=hidden][name*=hfID]").val(), name: firstCol.find("input[type=hidden][name*=hfName]").val()}];
                DEF_ALERTDELETEONEITEM.data.targetID = $(this).attr('id');
                $.fn.composeDialog(DEF_ALERTDELETEONEITEM);
            }
            return false;
        });
        $("TABLE.grid TD A[id*=Status]").click(function (event) {
            //prevent any action on click event if the "noaction" class was setup on it
            if ($(this).hasClass("noaction")) { event.preventDefault(); return; }
            var id = $(this).attr('id');
            var deactivate = $(this).find("img").attr('src').indexOf("ok.png") != -1;

            var firstCol = $(this).closest("TR").find("TD:lt(1)");
            var elements = [{ id: firstCol.find("input[type=hidden][name*=hfID]").val(), name: firstCol.find("input[type=hidden][name*=hfName]").val()}];
            if (deactivate) {
                DEF_ALERTDEACTIVATEONEITEM.data.elementsName = elements;
                DEF_ALERTDEACTIVATEONEITEM.data.targetID = id;
                $.fn.composeDialog(DEF_ALERTDEACTIVATEONEITEM);
            }
            else {
                DEF_ALERTACTIVATEONEITEM.data.elementsName = elements;
                DEF_ALERTACTIVATEONEITEM.data.targetID = id;
                $.fn.composeDialog(DEF_ALERTACTIVATEONEITEM);
            }

            return false;
        });
        $("TABLE.grid TD A[id*=Password]").click(function (event) {
            //prevent any action on click event if the "noaction" class was setup on it
            if ($(this).hasClass("noaction")) { event.preventDefault(); return; }
            DEF_ALERTPASSWORDONEITEM.data.targetID = $(this).attr('id');
            var firstCol = $(this).closest("TR").find("TD:lt(1)");
            DEF_ALERTPASSWORDONEITEM.data.elementsName = [{ id: firstCol.find("input[type=hidden][name*=hfID]").val(), name: firstCol.find("input[type=hidden][name*=hfName]").val()}];
            $.fn.composeDialog(DEF_ALERTPASSWORDONEITEM);
            return false;
        });
        $("TABLE.grid TD A[id*=Export]").click(function (event) {

            //HANDLER FOR EXPORT ONE ITEM
            var firstCol = $(this).closest("TR").find("TD:lt(1)");
            if ($(this).hasClass("noaction")) { event.preventDefault(); return; }
            DEF_ALERTEXPORTONEITEM.data.targetID = $(this).attr('id');
            DEF_ALERTEXPORTONEITEM.data.elementsName = [{ id: firstCol.find("input[type=hidden][name*=hfID]").val(), name: firstCol.find("input[type=hidden][name*=hfName]").val()}];
            $.fn.composeDialog(DEF_ALERTEXPORTONEITEM);
            return false;
        });
        $("TABLE.grid TD.popupStatus SPAN[id*=lbStatus]").each(function (index) { $(this).css("cursor", "pointer"); }).click(function (event) {
            //prevent any action on click event if the "noaction" class was setup on it
            if ($(this).hasClass("noaction")) { event.preventDefault(); return; }
            //alert($(this).closest("TR").find("TD:lt(1)").find("input:hidden[name*=hfID]").val());
            //hfPlatformId
            $.fn.composeDialogWithPlatform(DEF_LOADDYNAMICINFO, $(this).closest("TR").find("TD:lt(1)").find("input:hidden[name*=hfID]").val(), $(this).closest("TR").find("TD:lt(1)").find("input:hidden[name*=hfPlatformId]").val());
            return false;
        });
        $("TABLE.grid TD.popupStatusAssetImportStatusHistory SPAN[id*=lbStatus]").each(function (index) { $(this).css("cursor", "pointer"); }).click(function (event) {
            var firstCol = $(this).closest("TR").find("TD:lt(1)");
            var params = new Object();
            params.titleID = firstCol.find("input[type=hidden][name*=hfID]").val();
            if ($(this).hasClass("noaction")) { event.preventDefault(); return; }
            DEF_ASSETIMPORTSTATUSHISTORY.data.targetID = $(this).attr('id');

            AjaxRequest = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/ToDo/ToDoImport.aspx/onAssetImportStatusHistory",
                data: JSON.stringify(params),
                dataType: "json",
                success: function (data, textStatus) {
                    var error = data.d.Error;
                    var statusRow = data.d.statusRow;

                    if (error == 0) {

                        DEF_ASSETIMPORTSTATUSHISTORY.data.elementsName = [{ name: statusRow}];

                        $.fn.composeDialog(DEF_ASSETIMPORTSTATUSHISTORY);

                    }
                    else // error occured
                    {
                        alert('error');
                    }

                }, // error occured
                error: function (data, textStatus) {
                    alert('error');
                }
            });
        });

        $("TABLE.grid TD.popupStatusAssetEncodeStatusHistory SPAN[id*=lbStatus]").each(function (index) { $(this).css("cursor", "pointer"); }).click(function (event) {
            var firstCol = $(this).closest("TR").find("TD:lt(1)");
            var params = new Object();
            params.assetID = firstCol.find("input[type=hidden][name*=hfID]").val();
            if ($(this).hasClass("noaction")) { event.preventDefault(); return; }
            DEF_ASSETIMPORTSTATUSHISTORY.data.targetID = $(this).attr('id');

            AjaxRequest = $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "/DeployExport/EncoderActivity.aspx/onAssetEncodeStatusHistory",
                data: JSON.stringify(params),
                dataType: "json",
                success: function (data, textStatus) {
                    var error = data.d.Error;
                    var statusRow = data.d.statusRow;

                    if (error == 0) {

                        DEF_ASSETIMPORTSTATUSHISTORY.data.elementsName = [{ name: statusRow}];

                        $.fn.composeDialog(DEF_ASSETIMPORTSTATUSHISTORY);

                    }
                    else // error occured
                    {
                        alert('error s');
                    }

                }, // error occured
                error: function (data, textStatus) {
                    alert('error 0');
                }
            });
        });

    } //end actionLinksFromTable

    $.fn.pageExitRestricted = function () {
        $("a, button, input[type=submit]").filter(function (index, obj) {
            return $(this).closest("#barTitle, .multiselect, .pager, .suboptions, .restricted, .cs-scrollbar, .SmartDropDown, .pagerImg, .SmartCollectionV5, .SmartCategorizedTransfer, .nodialog, .SmartCollectionV4").length > 0 ? false : true;
        }).click(function (event) {
            var self = this;
            if (!$(this).data("doNotPrevent") && !$(this).hasClass("doNotPrevent")) {

                var editMode = decodeURI((RegExp('action=' + '(.+?)(&|$)').exec(location.search) || [, null])[1]);
                if (editMode && editMode == '3')
                    return;
                event.preventDefault();
                DEF_ALERTRESTRICTEDPAGE.eventHandler = function (name, value) {
                    if (name == "ON_BUTTON_CLICK") {
                        $(self).data("doNotPrevent", true);
                        if (self.nodeName.toLowerCase() != "a")
                            $(self).click();
                        else {
                            window.location = $(self).attr('href');
                        }
                    }
                    return true;
                };
                $.fn.composeDialog(DEF_ALERTRESTRICTEDPAGE);

            }
        });
    } //end pageExitRestricted

    $.fn.pageReadOnly = function () {
        $("FORM a, FORM input[type=text], FORM select, FORM textarea, FORM input[type=checkbox],FORM input[type=radio]").each(function () {
            var cond = !$(this).hasClass("ExceptionReadOnly");

            switch (this.tagName) {
                case "A":
                    if ($(this).closest("nav.pager").length <= 0 && $(this).closest("div.panel").length <= 0 && $(this).closest("li").length <= 0 && cond) {
                        $(this).addClass("disabled").click(function (event) { event.preventDefault(); })
                    }
                    break;
                case "INPUT":
                    if ($(this).attr("type") == "radio" && cond) {
                        $(this).attr("disabled", "true");
                    }
                    else if ($(this).attr("type") == "checkbox" && cond) {
                        $(this).attr("disabled", "true");
                    }
                    else if ($(this).attr("type") == "text" && cond) {
                        $(this).attr("disabled", "disabled");
                    }
                    break;
                case "SELECT":
                    if ($(this).closest("div.languageSelection").length <= 0 && cond) {
                        $.fn.ddl_settings = { readOnly: true, minItemsNr: 6, callback: function (obj) { } }
                        $(this).attr("disabled", "disabled");
                        if (!(/\{ID\}/).test($(this).attr("id"))) {
                            if ($(this).attr("id") != undefined) {
                                $(this).filter(":not([multiple])").dropDownBox($.fn.ddl_settings);
                            }
                        }
                    }
                    else {
                        $.fn.ddl_settings = { readOnly: false, minItemsNr: 6, callback: function (obj) { } }
                        $(this).filter(":not([multiple])").dropDownBox($.fn.ddl_settings);
                    }
                    break;
                case "TEXTAREA":
                    if (cond)
                        $(this).attr("disabled", "disabled");
                    break;
            }

        });
    } //end pageReadOnly

    $.fn.loadAutoComplete = function () {
        // -- autocpmplete functionality
        return $(this).each(function (index) {
            $(this).keyup(function (event) {
                var keyword = $(this).val();
                $(this).autocomplete({
                    appendTo: ".autocompleteArea",
                    source: '/SearchPeople.aspx?param=' + encodeURIComponent(keyword),
                    minLength: 1,
                    select: function (event, ui) {
                    }
                }).data("autocomplete")._renderItem = function (ul, item) {
                    var itemValue = item.label;
                    var newItemValue = itemValue.replace(new RegExp(
					    "(?![^&;]+;)(?!<[^<>]*)(" +
					    jQuery.ui.autocomplete.escapeRegex(keyword) +
					    ")(?![^<>]*>)(?![^&;]+;)", "gi"
					    ), "<strong>$1</strong>");

                    return $("<li></li>").data("item.autocomplete", item).append("<a>" + newItemValue + "</a>").appendTo(ul);
                }
            });
        });
    },

    $.fn.renderDropDownsRandomNr = 1,
    $.fn.renderDropDownsInside = function () {
        this.each(function (index) {
            $('SELECT:not([multiple])', $(this)).renderDropDowns();

            $('SELECT:not([multiple])', $(this)).each(function () {
                if ($(".ddList", $(this).parent()).length) return;
                $(this).dropDownBox($.fn.ddl_settings)
            });
        });
    },

    $.fn.renderDropDowns = function () {
        return this.each(function (index) {
            var randomNr = $.fn.renderDropDownsRandomNr++;
            if (!(/\{IDX\}/).test($(this).attr("id")) && !(/\{ID\}/).test($(this).attr("id")) && !$(this).parent().hasClass("dropDown") && !($(this).parents(".template").length > 0)) {

                var ID = $(this).attr("id");
                var NAME = $(this).attr("name");

                var HEIGHT = parseInt($(this).css("height"), 10);
                if (HEIGHT <= 100) {
                    HEIGHT = 105;
                }
                var WIDTH = parseInt($(this).css("width"), 10);
                if (WIDTH <= 0) {
                    WIDTH = 290;
                }
                randomNr++;
                if (ID == undefined) {
                    ID = 'select_auto_ID' + randomNr;
                }
                else {
                    ID = ID.replace(/(\s+)/g, "_");
                }
                if (NAME == undefined) {
                    NAME = 'select_auto_NAME' + randomNr;
                }
                else {
                    NAME = NAME.replace(/(\s+)/g, "_");
                }

                $(this).attr("id", ID).attr("name", NAME).css("height", HEIGHT).css("width", WIDTH);
                var htmlAround = '<div class="dropDown" id="' + ID + '_f"><div class="dropDownIn"><span class="value">&nbsp;</span></div></div>';

                var dd_clone = $(this).clone(true);
                var _this = this;
                $(".dropDownIn", $(htmlAround)).each(function (index) {
                    $(dd_clone).insertAfter(this);
                    $(_this).replaceWith($(this).parent());
                });
            }
        });
    } //end function renderDropDowns


    $.fn.showLoader = function () {
        var loaderDiv = $('<div class="loading"></div>').css({ "z-index": 9999 }).appendTo(this);
    };

    $.fn.advanceCustomFilterSettings = function () {
        $('input[type="text"][id*=SearchText]').keyup(function (event) {
            var choices = $(".lsCheck input[type='checkbox']", $(this).closest(".choices"));
            var countChoices = $(choices).length;

            //working with checkboxes
            var exist = false;
            for (var i = 0; i < countChoices; i++) {
                if ($(choices[i]).attr("checked")) {
                    exist = true;
                }
            }
            if ($(this).val().length <= 0) {
                $($(choices).removeAttr("checked")).trigger("change");
            }
            else if (!exist && countChoices > 0) {
                $($(choices[0]).attr("checked", "checked")).trigger("change");
            }

            //if everything is ok with input search text I do a submit
            if (event.keyCode == 13) {
                if ($(this).val().length >= 3) {
                    eval($(".panel a.button:first-child", $(this).closest(".choices")).attr("href").replace("javascript:", ""));
                }
            }
        });
    }

})(jQuery)

jQuery(function ($) {

    $("select:not([multiple])").renderDropDowns();

    $.fn.leftMenuAction();
    $.fn.fixTabMenu();
    //resetting the checked value fro some special checkboxes
    $('TABLE.gridList input[type=checkbox]').each(function (index) { $(this).removeAttr("checked"); $(this).trigger('change'); });
    $.fn.workingWithTableCheckboxes();
    $.fn.workingWithPanelButtons();
    $.fn.actionLinksFromTable();
    $('TEXTAREA:not(.charCounter)').each(function (index) { $(this).cs(); });

    // 'Character counter input' control related jquery code.
    //    $('.charCounter').each(function (index) {
    //        var csClass = $('TEXTAREA', $(this)).cs();
    //        $(this).charCounter({ maxChars: $('input[type=hidden]', $(this)).val(), incrementBy: 10, obj: csClass, callback: function (csClass) {
    //            csClass[0].scrollbar.slider.bar.draw();
    //        }
    //        })
    //    });

    $(".groupUsers").jScrollPane({ showArrows: false, maintainPosition: false, AutoScroll: false, verticalDragMinHeight: 36, verticalDragMaxHeight: 36, dragMinHeight: 20 });

    $(".datePicker").each(function (index) {
        var settings = { appendText: '(dd.mm.yyyy)', dateFormat: 'dd.mm.yy' };
        if ($(this).hasClass("readonly")) {
            settings = { appendText: '(dd.mm.yyyy)', dateFormat: 'dd.mm.yy', disabled: true };
        }
        $(this).datepicker(settings);
    });



    //styling all checkboxes: except those from table
    $("input[type=checkbox]").filter(function (index, object) {
        return $(this).parents(".template").length == 0 && ($(this).parent().hasClass("ez-checkbox") || $(this).closest("SECTION").hasClass("custom") || (/\{IDX\}/).test($(this).parent().attr("id")) ? false : true);
    }).ezMark().click(function (event) {
        if ($(this).attr("checked") != undefined) { }
        else { }
    });
    $('input[type=radio]').ezMark().click(function () { });
    $.fn.groupUsersRightsCheckboxes();

    /*
    * if we have one page with restricted access regarding some links, buttons click 
    * (ussualy pages wher some information must be added and saved or canceled)
    */
    $("#loading_overlay").css("z-index", "-1").css("display", "none");
    $.fn.hideLoading = $.fn.hideLoading | false;
    if ($.fn.restrictedPage == 1) {
        $.fn.pageExitRestricted();
    }
    else {
        $("a, button, input[type=submit]").filter(function (index, obj) {
            if (($(this).get(0).tagName).toLowerCase() == 'a') {
                if (/empty\.gif/.test($("img", $(this)).attr("src")) || $(this).hasClass("noaction")) {
                    return false;
                }
            }
            return true;
        }).click(function (event) {
            $isIE = false;
            if ($.browser.msie != undefined) {
                $isIE = true;
            }
            if ($(this).attr("href") != "javascript:void(0);" && !$.fn.hideLoading && !$isIE) {
                $("#loading_overlay").css("z-index", "99999").css("display", "block");
            }
        });
    }
    /*
    * if we have one page with readonly access regarding some links, buttons click 
    */
    $.fn.ddl_settings = { readOnly: false, minItemsNr: 6, callback: function (obj) { } }
    if ($.fn.readOnlyData == 1) {
        $.fn.pageReadOnly();
    }
    else {
        $("select:not([multiple])").filter(function (index) {
            if ((/\{ID\}/).test($(this).attr("id"))) {
                return false;
            }
            return true;
        }).dropDownBox($.fn.ddl_settings);
    }

    $.fn.advanceCustomFilterSettings();

    $("A#popupAddProductTitles").click(function (event) {
        $.fn.composeDialog3(DEF_LOADDYNAMICINFOPRODUCTTITLE, $("input[id*=_ftPlatforms][type=hidden]").val());
        return false;
    });


    $("button.validate").click(function (event) {
        $.fn.composeDialog4(DEF_DYNAMICVALIDATE, $("input[id*=_ftPlatforms][type=hidden]").val(), $("input[id*=_lwInfoListView]").val());
        return false;
    });



    $("label, span.above").addHints();


});

function ProductValidatePost(type) {
    var form = document.forms[1];
    $('#mainAreaOfPage__hfValidation').val(type);
    //if (form.action.indexOf('?') < 0) 
    //    form.action = form.action;
    //else 
    //form.action = form.action;
    form.submit();

}

function setGeneratedName(element) {
    var text = element.value;
    if (text) {
        try {
            document.getElementById("_tbGeneratedName").value = text + '_[RuleName]_[ColorDepth]_[Width]x[Height]';
        }
        catch (err) { }
    }
}