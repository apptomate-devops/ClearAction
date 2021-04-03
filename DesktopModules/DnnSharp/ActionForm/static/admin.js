;

//function g_clean(obj) {
//    for (var property in obj) {
//        if (!isNaN(property) || obj.hasOwnProperty(property)) {
//            if (typeof obj[property] == "object" || typeof obj[property] == "array")
//                g_clean(obj[property]);
//            else if (property[0] == "$")
//                delete obj[property];
//        }
//    }
//}


function g_localize(o) {
    return o ? o['default'] : "";
}

function g_localizeMaybeJson(o) {
    var val = g_localize(o);
    // TODO: find a better way to determine json arrays and objects - for example with a regex
    if (val && (val[0] == '[' && val[val.length - 1] == ']') || (val[0] == '{' && val[val.length - 1] == '}'))
        val = $.parseJSON(val);
    return val;
}
(function (angular) {
var app = angular.module('ActionForm', ['ngAnimate', 'ui.sortable', 'ui.spanresize', 'ui.tinymce', 'ngDragDrop', 'dnnsf', 'dnnsf.components']);
app.controller('FormCtrl', FormCtrl);
app.controller('EventCtrl', EventCtrl);

function StringToBool(o) {
    //  JSON values aren't necessary strings
    if (typeof (o) !== 'string')
        return o;

    if (o.toLowerCase() == 'true')
        return true;
    else if (o.toLowerCase() == 'false')
        return false;
    else
        return o;
}


//  Parameters specific end

// this is for sharing data
app.factory('sharedData', ['$http', 'dnnsf', function ($http, dnnsf) {
    return {
        serverValidators: [],
        resourceVersion: g_resourceVersion,
        eventActions: {
        },

        // filter fields by type(s)
        byType: function (type) {

            // make sure types is an array
            var types = dnnsf.eval(type);
            types = dnnsf.toArray(types);

            return function (item) {
                return types.length == 0 || $.inArray(item.InputTypeStr, types) != -1;
            };
        },

        notParentButton: function (action) {
            return function (item) {
                return item != action.$_field;
            };
        },

        notInArray: function (arr, currentItem) {
            return function (input) {
                var i = $.inArray(input.BoundName, arr);
                if (i != currentItem && i != -1)
                    return false;
                return true;
            };
        },

        exceptItem: function (item) {
            return function (input) {
                return item != input.BoundName;
            };
        },

        labelAlign: ['Template Default', 'Left', 'Center', 'Right', 'Top', 'Inside'],

        labelSpacing: {
            0: 'Loose',
            1: 'Normal',
            2: 'Compact'
        },

        loadCount: false,


        loadAddons: function () {

            if (this.addonsLoading)
                return;

            this.addonsLoading = true;
            var _this = this;
            $http({
                method: 'GET',
                url: '//www.dnnsharp.com/DesktopModules/DnnSharp/DnnApiEndpoint/Api.ashx?method=ActionFormExtensions'
            }).success(function (data, status) {
                _this.addons = data;
            });
        }

    };
}]);


function FormCtrl($scope, $http, sharedData, $timeout, $sce, dnnsf) {

    // init data
    $scope.$sce = $sce;
    $scope.localize = function (o) {
        return g_localize(o);
    };
    $scope.sharedData = sharedData;
    $scope.sharedData.form = {
        fields: []
    };
    $scope.dnnsf = dnnsf;

    $scope.mode = 'edit';
    //$scope.mode = 'layout';

    // this is an array of handlers that will get called on save
    // for example, individual events can register here when changed in order to perform save when the master Save button is clicked
    $scope.onSave = [];

    $http({
        method: 'GET',
        url: dnnsf.adminApi('GetLicenseStatus', { returnUrl: window.location.href })
    }).success(function (data, status) {
        $scope.licenseStatus = data;
    });


    // start defs
    $scope.templates = g_templates;
    $scope.jQueryThemes = g_jQueryThemes;
    $scope.fieldDefs = g_fieldDefs;
    $scope.predefFields = g_predefFields;
    $scope.validatorDefs = g_validatorDefs;
    $scope.groupValidatorDefs = g_groupValidatorDefs;

    console.log('FieldDefs', $scope.fieldDefs);

    // load field defs and organize by group
    $scope.fieldDefGroups = {};
    $scope.commonFields = [];
    $.each($scope.fieldDefs, function (k, o) {
        if (o.Settings.IsCommon)
            $scope.commonFields.push(o);

        for (var i = 0; i < o.Categories.length; i++) {
            $scope.fieldDefGroups[o.Categories[i]] = $scope.fieldDefGroups[o.Categories[i]] || [];
            $scope.fieldDefGroups[o.Categories[i]].push(o);
        }
    });

    // load addons from server
    $scope.sharedData.loadAddons();

    // load action defs and organize by group
    $scope.actionDefs = g_actionDefs;
    $scope.actionDefGroups = {};
    $.each(g_actionDefs, function (i, o) {
        if (!$scope.actionDefGroups[o.Settings['Group']])
            $scope.actionDefGroups[o.Settings['Group']] = [];
        $scope.actionDefGroups[o.Settings['Group']].push(o);
    });

    // watch modifications to enable the Save button and display "not saved"s warning
    $scope.modified = false;
    var fnUnloadModified = function () { return 'Your changes are not saved.' }
    $scope.notifyModified = function (bModified) {

        //if ($scope.sharedData.lockChange)
        //    return;

        dnnsf.log('Modified change, load count ' + $scope.sharedData.loadCount, dnnsf.stackTrace());
        // only change this flag if we're finished loading
        if ($scope.sharedData.loadCount != 0) {
            $scope.modified = false;
            return;
        }

        dnnsf.log('modified: ' + $scope.modified + ' => ' + bModified);
        $scope.modified = bModified;
        if (bModified) {
            if (window.addEventListener) {
                window.addEventListener('beforeunload', fnUnloadModified);
            } else {
                window.attachEvent('onbeforeunload', fnUnloadModified);
            }
        } else {
            if (window.removeEventListener) {
                window.removeEventListener('beforeunload', fnUnloadModified);
            } else {
                window.detachEvent('onbeforeunload', fnUnloadModified);
            }
        }
    };


    function prepareSettings() {
        $scope.watchAll && $scope.watchAll(); // this clears previous watch

        dnnsf.log('preparing data');
        $scope.prepareFields($scope.sharedData.form.Fields);

        $scope.watchAll = $scope.$watch('sharedData.form', function (newValue, oldValue) {

            dnnsf.log('watchAllFields triggers...');
            if (angular.equals(newValue, oldValue)) {
                dnnsf.log('form not modified.');
                return;
            }
            dnnsf.log('form modified.');
            $scope.notifyModified(true);

        }, true); // this last true does a "deep" watch, otherwise angularJs would compare by reference, which never changes in this case here (arrays)
    }

    $scope.prepareFields = function (fields) {

        $scope.sharedData.serverValidators = [];


        $.each(fields, function (i, o) {
            //$scope.modified = o.FormFieldId > 0 ? false : $scope.modified;
            o.$_uid = dnnsf.uniqueId('field', o.FormFieldId);
            o.AutoName = !o.Name;
            o.BoundName = o.BoundName || o.Name;
            $scope.computeName(o);

            // parse server validators
            if (o.CustomValidator1 && $scope.validatorDefs[o.CustomValidator1] && $scope.validatorDefs[o.CustomValidator1].IsServerSideValidation)
                $scope.sharedData.serverValidators.push({ title: o.Title + ' Field: ' + $scope.validatorDefs[o.CustomValidator1].Title + ' Failed', value: o.FormFieldId + '-' + $scope.validatorDefs[o.CustomValidator1].Title });
            if (o.CustomValidator2 && $scope.validatorDefs[o.CustomValidator2] && $scope.validatorDefs[o.CustomValidator2].IsServerSideValidation)
                $scope.sharedData.serverValidators.push({ title: o.Title + ' Field: ' + $scope.validatorDefs[o.CustomValidator2].Title + ' Failed', value: o.FormFieldId + '-' + $scope.validatorDefs[o.CustomValidator2].Title });
            if (o.ValidationGroup && $scope.groupValidatorDefs[o.GroupValidator] && $scope.groupValidatorDefs[o.GroupValidator].IsServerSideValidation)
                $scope.sharedData.serverValidators.push({ title: o.ValidationGroup + ': ' + $scope.groupValidatorDefs[o.GroupValidator].Title + ' Failed', value: o.ValidationGroup + '-' + $scope.groupValidatorDefs[o.GroupValidator].Title });

            dnnsf.initParameters($scope.fieldDefs[o.InputTypeStr], o, true);

        });

        $scope.sharedData.serverValidators = $.unique($scope.sharedData.serverValidators);
        //$scope.buildGrid();
    };

    $scope.appendData = function (item, pId) {
        // append all fields
        var data = '';
        $.each($scope.sharedData.form.Fields, function (i, field) {
            if (field.Def && field.Def.Settings.HasValue == 'false') //.InputTypeStr == 'button' || field.InputTypeStr == 'image-button' || field.InputTypeStr == 'button-group'
                //|| field.InputTypeStr == 'progressbar' ||field.InputTypeStr == 'captcha' || field.InputTypeStr == 'upload.single')
                return;
            data += field.Title + ': [' + field.BoundName + ']';

        });
        item.Parameters[pId] = (item.Parameters[pId] || '') + data;
    };


    $scope.isDuplicate = function (item) {
        for (var i = 0; i < $scope.sharedData.form.Fields.length; i++)
            if (item != $scope.sharedData.form.Fields[i] && $scope.sharedData.form.Fields[i].BoundName == item.BoundName) {
                return true;
            }
    };


    $scope.save = function () {
        //g_clean($scope.sharedData.form.Fields);
        $timeout(function () {
            $scope.notifyModified(false);
            $scope.saving = true;
            $scope.mode = 'edit';
        });

        var data = angular.copy($scope.sharedData.form);

        for (var i = 0; i < data.Fields.length; i++) {
            data.Fields[i] = _.omit(data.Fields[i], 'Def');
            if (data.Fields[i].Actions)
                for (var j = 0; j < data.Fields[i].Actions.length; j++) {
                    data.Fields[i].Actions[j] = _.omit(data.Fields[i].Actions[j], 'Definition');
            };
        }

        for (var i = 0; i < data.AllActions.length; i++)
            data.AllActions[i] = _.omit(data.AllActions[i], 'Definition');

        for (var i = 0; i < data.OnPreInit.length; i++)
            data.OnPreInit[i] = _.omit(data.OnPreInit[i], 'Definition');

        for (var i = 0; i < data.OnInit.length; i++)
            data.OnInit[i] = _.omit(data.OnInit[i], 'Definition');
  

        $http({
            method: 'POST',
            url: dnnsf.adminApi('Save'),
            data: angular.toJson(data)
        }).success(function (data, status) {

            //// update our local copy, since the server may have done some alteration
            $scope.sharedData.form = data;
            //// reset modfied states
            prepareSettings();
            //angular.copy(data, $scope.sharedData.form);

            // call other handlers
            $scope.onSave = $.unique($scope.onSave);
            for (var i = 0; i < $scope.onSave.length; i++)
                $scope.onSave[i]();
            $scope.onSave = [];

            // modified will be set to true as the child EventsCtrl generats and update when setting up the actions
            $timeout(function () {
                $scope.saving = false;
                $scope.notifyModified(false);
            });

            $scope.scrollToTop();
            //$scope.modified = false;
            $scope.checkAWeberAction();
        });

        // delete entries right away (meaning don't wait for the response)
        $scope.sharedData.form.Fields = $.grep($scope.sharedData.form.Fields, function (e) { return !e.IsDeleted });
    };

    //AWeber
    $scope.checkAWeberAction = function () {
        var f = $scope.sharedData.form.Fields;
        for (var i = 0; i < f.length; i++) {
            if (f[i].InputTypeStr == "button" && f[i].Actions.length > 0) {
                var ac = f[i].Actions;
                for (var j = 0; j < ac.length; j++) {
                    if ((ac[j].ActionType === "SubscribeToAWeber" || ac[j].ActionType === "UnsubscribeFromAWeber") && !ac[j].IsDeleted && (ac[j].Parameters.AccessKey === "" || ac[j].Parameters.AccessKey === undefined)) {
                        console.log($scope.dnnsf);
                        window.location.href = $scope.dnnsf.appUrl + "/AWeberAuthorize.aspx?AWeberConsumerKey=" + ac[j].Definition.Settings.ConsumerKey + "&AWeberConsumerSecret=" + ac[j].Definition.Settings.ConsumerSecret + "&action=" + ac[j].Id + "&mid=" + $scope.sharedData.form.ModuleId + "&tabid=" + $scope.sharedData.form.TabId;
                    }
                }
            }
        }
    }

    $scope.scrollToTop = function () {
        $('html, body').animate({
            scrollTop: 0
        }, 500);
        window.top.postMessage(JSON.stringify({
            type: dnnsf.urlParam('comm-prefix') + "-scroll",
            offset: 0
        }), "*");
    };

    function loadForm() {
        // Load initial data from the server
        $scope.sharedData.loadCount++;
        dnnsf.log('fetching data...');
        $http({
            method: 'GET',
            url: dnnsf.adminApi('Get')
        }).success(function (data, status) {

            if (data.error)
                alert(data.error);

            console.log('data retrieved', data);
            $scope.sharedData.form = data;

            dnnsf.log('preparing settings');
            prepareSettings();
            $scope.sharedData.loadCount--;

            $scope.formLoaded = true;
        });
    }
    loadForm();

    $scope.saveDisabled = false;
    $scope.invalidCharacters = false;
    var invalidFields = {};
    $scope.computeName = function (field) {

        // make source BoundName is initialized - it doesn't come from the server, we're just using it on client sid
        if (!field.AutoName) {
            if (field.BoundName !== '' && !field.BoundName.match(/^[a-z]/i)) {
                if (field.BoundName.match(/^[a-z0-9_]*$/i)) {
                    invalidFields[field.$_uid] = true;
                    field.$_invalidCharacters = false;
                    field.$_nonLatin = true;
                    $scope.saveDisabled = true;
                } else {
                    invalidFields[field.$_uid] = true;
                    field.$_invalidCharacters = true;
                    field.$_nonLatin = true;
                    $scope.saveDisabled = true;
                }
            }

            if (field.BoundName !== '' && field.BoundName.match(/^[a-z][a-z0-9_]*$/i)) {
                field.Name = field.BoundName;
                invalidFields[field.$_uid] = false;
                field.$_invalidCharacters = false;
                field.$_nonLatin = false;
                $scope.saveDisabled = _.any(invalidFields, function (value) {
                    return value;
                });
            } else if (field.BoundName !== '' && field.BoundName.match(/^[a-z]/i)) {
                invalidFields[field.$_uid] = true;
                field.$_invalidCharacters = true;
                field.$_nonLatin = false;
                $scope.saveDisabled = true;
            }
            return;
        }

        field.Name = null;////its saving null into table
        field.BoundName = field.Title.replace(/((^[^a-z]*)|([^a-z0-9_]*))/gi, "");
        //field.Name = field.BoundName;

        if (!field.BoundName || field.BoundName == '') {
            invalidFields[field.$_uid] = true;
            $scope.saveDisabled = true;
        } else {
            invalidFields[field.$_uid] = false;
            field.$_invalidCharacters = false;
            field.$_nonLatin = false;
            $scope.saveDisabled = _.any(invalidFields, function (value) {
                return value;
            })
        }
    };

    $scope.addField = function (def) {
        $timeout(function () {
            // if it's a button, apply an initial offset equal to label width so it aligns with the controls
            var offset = 0;
            var title = def.Title;
            if (def.Name == 'button') {
                title = 'Submit';
                if ($scope.sharedData.form.LabelAlign.Value != 'top' && $scope.sharedData.form.LabelAlign.Value != 'inside')
                    offset = $scope.sharedData.form.LabelWidth.Value;
            }

            var field = {
                $_uid: dnnsf.uniqueId('newField'),
                $_isOpen: true,
                $_isLoaded: true,
                _hasFocus: true,
                AutoName: true,
                "Title": title,
                "Name": "",
                "InputTypeStr": def.Name,
                "InputData": "",
                "IsEnabled": true,
                RowIndex: $scope.highestRow() + 1,
                ColSpan: 12 - offset,
                ColOffset: offset,
                Parameters: {},
                Actions: []
            };

            dnnsf.initParameters(def, field);
            if (field.InputTypeStr == 'open-number') {
                field.CustomValidator1 = 'Integer Number';
            };
            $scope.sharedData.form.Fields.push(field);
            $scope.computeName(field);

        }, 50);
    };

    $scope.changeFieldType = function (field, def) {
        field.InputTypeStr = def.Name;
        dnnsf.initParameters(def, field);
    };

    $scope.cloneField = function (field, predefField) {
        //if (!$scope.grid)
        //    $scope.buildGrid();
        var newField = $.extend({}, angular.copy(field), {
            $_uid: dnnsf.uniqueId('newField'),
            $_isOpen: true,
            $_isLoaded: true,
            _hasFocus: true,
            FormFieldId: -1,
            RowIndex: $scope.highestRow() + 1,
            ColSpan: 12,
            ColOffset: 0
        });

        if (predefField)
            newField.AutoName = true;

        // reset action IDs
        newField.Actions && $.each(newField.Actions, function (i, action) {
            action.Id = -1;
            action.$_uid = dnnsf.uniqueId('newAction');
        });

        if (!predefField)
            newField.Title += ' Copy';
        dnnsf.initParameters(newField.Def, newField);

        $scope.computeName(newField);
        $scope.sharedData.form.Fields.push(newField);
    };

    $scope.hasButtons = function () {

        if (!$scope.sharedData.form || !$scope.sharedData.form.Fields)
            return false;

        return $.grep($scope.sharedData.form.Fields, function (o, i) {
            if (o.InputTypeStr == 'button' || o.InputTypeStr == 'image-button')
                return true;
        }).length > 0;
    };


    // START Grid code
    // TODO: maybe move to a different controller for better encapsulation or even a reusable component/directive
    $scope.grid = [];

    $scope.highestRow = function () {
        // get highest row 
        var maxRow = 0;
        $.each($scope.sharedData.form.Fields, function (i, o) {
            if (o.RowIndex > maxRow)
                maxRow = o.RowIndex;
        });
        return maxRow;
    };

    $scope.getFixedButtons = function () {
        var actionButtons = [];
        $.each($scope.sharedData.form.Fields, function (iField, field) {

            if (field.InputTypeStr == 'button') {
                $.each(field.Actions, function (iaction, action) {
                    if (action.ActionType == 'ShowMessage' && action.Parameters.ButtonsList)
                        $.merge(actionButtons, action.Parameters.ButtonsList);
                });
            } else if (field.InputTypeStr == 'button-group' && field.Parameters.ButtonsList) {
                $.merge(actionButtons, field.Parameters.ButtonsList);
            }
        });

        for (var eventName in $scope.sharedData.eventActions) {
            $.each($.grep($scope.sharedData.eventActions[eventName], function (action) { return action.ActionType == 'ShowMessage'; }), function (iaction, action) {
                if (action.Parameters.ButtonsList)
                    $.merge(actionButtons, action.Parameters.ButtonsList);
            });
        }
        return actionButtons;
    };

    $scope.buildGrid = function () {

        var grid = [];
        // build list of ShowMessage actions that we have to look at in order to remove some buttons from the layout view

        var actionButtons = $scope.getFixedButtons();
        dnnsf.log("Exclude action buttons from layout ...");
        dnnsf.log(actionButtons);

        $.each($scope.sharedData.form.Fields, function (i, o) {

            // only allow form buttons to be positioned through the layout tool 
            if (o.InputTypeStr == 'button' && $.inArray(o.BoundName, actionButtons) != -1)
                return;

            //if (o.Parameters && o.Parameters['ShowIn'] && o.Parameters['ShowIn'] != 'Form') {
            //    o.ViewOrder = 9999; // not sure we really need this
            //    return;
            //}

            var toAdd = o.RowIndex - grid.length;
            for (var irow = 0; irow <= toAdd; irow++)
                grid.push([]);

            // append this column
            grid[o.RowIndex].push({
                span: o.ColSpan,
                row: o.RowIndex,
                field: o
            });
        });

        $scope.grid = $scope.fixGrid(grid);
    };

    // fills with empty cells to meet the 12 grid system
    $scope.fixGrid = function (grid) {
        for (var irow = 0; irow < grid.length; irow++)
            $scope.fixGridRow(irow, undefined, grid);
        return $scope.updateRowIndexes(grid);
    };

    $scope.siblingFields = function (col) {
        return $.grep($scope.grid[col.row], function (c) { return c != col && c.field.InputTypeStr; });
    };

    $scope.makeFullWidth = function (col) {
        // TODO: take into account other fields on same row
        col.field.ColOffset = 0;
        col.field.ColSpan = 12;
        $scope.fixGridRow(col.row, undefined, $scope.grid);
    };

    $scope.nextNonEmptyCell = function (list, after) {
        var iCell = $.inArray(after, list);
        if (iCell == -1)
            return null;

        while (++iCell < list.length) {
            if (list[iCell].field.InputTypeStr)
                return list[iCell];
        }

        return null;
    };

    $scope.fieldDroped = function (event, ui, dragModel, dropModel) {

        var nextFieldAfterClearedOne = $scope.nextNonEmptyCell($scope.grid[dragModel.$modelValue.field.RowIndex], dragModel.$modelValue);
        if (nextFieldAfterClearedOne)
            nextFieldAfterClearedOne.field.ColOffset += dragModel.$modelValue.span;

        // take row index and span of current cell
        dropModel.$modelValue.field = dragModel.$modelValue.field;
        dropModel.$modelValue.field.RowIndex = dropModel.$modelValue.row;
        dropModel.$modelValue.field.ColSpan = dropModel.$modelValue.span;
        dropModel.$modelValue.field.ColOffset = 0;
        dragModel.$modelValue.field = {};

        // also get the offset, it's the span of the previous empty cell(s)
        var row = $scope.grid[dropModel.$modelValue.field.RowIndex];
        var iCell = $.inArray(dropModel.$modelValue, row);
        var iEmptyCell = iCell;
        while (iEmptyCell > 0 && !row[iEmptyCell - 1].field.InputTypeStr) {
            dropModel.$modelValue.field.ColOffset += row[iEmptyCell - 1].span;
            iEmptyCell--;
        }

        // reset the offset for following field, since we've filled the gap
        if (iCell != row.length - 1 && row[iCell + 1].field.InputTypeStr)
            row[iCell + 1].field.ColOffset = 0;

        $scope.fixGridRow(dropModel.$modelValue.field.RowIndex, undefined, $scope.grid);
        if (dropModel.$modelValue.field.RowIndex != dragModel.$modelValue.row)
            $scope.fixGridRow(dragModel.$modelValue.row, undefined, $scope.grid);
        //$scope.$apply();

        $scope.grid = $scope.updateRowIndexes($scope.grid);
        $scope.notifyModified(true);
    };

    $scope.moveOnNewRow = function (col) {
        // remove from old row and give the space to someone else
        var nextFieldAfterClearedOne = $scope.nextNonEmptyCell($scope.grid[col.field.RowIndex], col);
        if (nextFieldAfterClearedOne)
            nextFieldAfterClearedOne.field.ColOffset += col.span;

        var iCell = $.inArray(col, $scope.grid[col.field.RowIndex]);
        $scope.grid[col.field.RowIndex].splice(iCell, 1);
        $scope.fixGridRow(col.field.RowIndex);

        col.field.ColOffset = 0;
        col.field.ColSpan = 12;
        col.field.RowIndex++;
        $scope.grid.splice(col.field.RowIndex, 0, [col]);
        $scope.fixGridRow(col.field.RowIndex, undefined, $scope.grid);

        $scope.grid = $scope.updateRowIndexes($scope.grid);
        $scope.notifyModified(true);
    };

    $scope.fieldResized = function (col, diff, position) {

        var row = $scope.grid[col.field.RowIndex];
        var iCell = $.inArray(col, row);

        if (position == 'left') {

            // take space from previous empty space
            if (diff > 0) {
                var effDiff = diff;
                //if (col.field.ColOffset + effDiff >= col.field.ColSpan)
                //    effDiff = col.field.ColSpan - col.field.ColOffset - 1;

                col.field.ColOffset += effDiff;
                col.field.ColSpan -= effDiff;
            } else {
                var effDiff = diff;
                if (col.field.ColOffset + effDiff < 0)
                    effDiff = -col.field.ColOffset;
                col.field.ColOffset += effDiff;
                col.field.ColSpan -= effDiff;
            }

        } else {

            // increase width
            // but take it out from ColOffset of next field
            var iNextCell = iCell + 1;
            var nextCell = row[iNextCell++];
            while (!nextCell.field.InputTypeStr && iNextCell < row.length)
                nextCell = row[iNextCell++];
            if (!nextCell.field.InputTypeStr) {

                // how much width is available for current field?
                var occupiedSpans = 0;
                for (var i = 0; i < iCell; i++) {
                    if (row[i].field.InputTypeStr) {
                        occupiedSpans += row[i].field.ColOffset + row[i].field.ColSpan;
                    }
                }
                occupiedSpans += row[iCell].field.ColOffset;

                col.field.ColSpan = Math.max(1, Math.min(col.field.ColSpan + diff, 12 - occupiedSpans));
            } else {

                var effDiff = Math.min(diff, nextCell.field.ColOffset);
                if (col.field.ColSpan + effDiff <= 0)
                    effDiff = -col.field.ColSpan - 1;

                col.field.ColSpan += effDiff;
                nextCell.field.ColOffset -= effDiff;
            }
        }
        $scope.fixGridRow(col.field.RowIndex, undefined, $scope.grid);
        $scope.notifyModified(true);
    };

    $scope.rowOrderChanged = function (event, ui) {
        $scope.grid = $scope.updateRowIndexes($scope.grid);
        $scope.notifyModified(true);
    };

    $scope.editOrderChanged = function (event, ui) {
        // update row indexes from fields
    };

    $scope.updateRowIndexes = function (grid) {

        // first, remove empty rows
        grid = $.grep(grid, function (r) {
            return r.length != 1 || r[0].field.InputTypeStr;
        });

        $.each(grid, function (irow, row) {
            $.each(row, function (icol, col) {
                col.row = irow;
                col.field.RowIndex = irow;
                col.field.ViewOrder = irow * 12 + $scope.colNumber(grid, col);
            });
        });

        $scope.sharedData.form.Fields.sort(function (a, b) { return a.ViewOrder > b.ViewOrder ? 1 : (a.ViewOrder < b.ViewOrder ? -1 : 0) });
        return grid;
    };

    $scope.colNumber = function (grid, ofCol) {
        var colNum = 0;
        var row = grid[ofCol.row];
        for (var icol = 0; icol < row.length; icol++) {
            var col = row[icol];
            if (col == ofCol)
                return colNum + col.field.ColOffset;

            if (col.field.InputTypeStr)
                colNum += col.field.ColOffset;
            colNum += col.span;

        }
        return -1; // ? no found ?
    };

    $scope.fixGridRow = function (irow, model, grid) {
        grid = grid || $scope.grid;

        var toAdd = irow - grid.length;
        for (var i = 0; i <= toAdd; i++)
            grid.push([]);

        // remove all empty cells
        grid[irow] = $.grep(grid[irow], function (c) { return c.field.InputTypeStr; });

        //if (!grid[irow].length) {
        //    grid.splice(irow, 1);
        //    return;
        //}

        // limit total size to 12 cols
        var totalCols = 0;
        $.each(grid[irow], function (icol, c) {
            totalCols += c.field.ColOffset + c.field.ColSpan;
        });

        dnnsf.log('A total of ' + totalCols + ' found on row ' + irow);

        if (totalCols > 12) {
            // we have a problem, first, drop the offsets
            totalCols = 0;
            $.each(grid[irow], function (icol, c) {
                c.field.ColOffset = 0;
                totalCols += c.field.ColSpan;
            });

            if (totalCols > 12) {
                var adjust = Math.ceil((totalCols - 12) / grid[irow].length);
                $.each(grid[irow], function (icol, c) {
                    c.field.ColSpan -= adjust;
                });
            }
        }

        // now fill new empties
        var totalRow = 0;
        var iAdjust = 0;
        $.each($.extend([], grid[irow]), function (icol, c) {

            c.span = c.field.ColSpan;
            c.row = c.field.RowIndex;
            c.field.ViewOrder = c.field.RowIndex * 12 + $scope.colNumber(grid, c);

            // apend space before?
            if (c.field.ColOffset) {
                grid[irow].splice(icol + iAdjust, 0, { row: irow, span: c.field.ColOffset, field: {} });
                iAdjust++;
            }
            totalRow += c.field.ColOffset + c.field.ColSpan;
        });

        grid[irow].push({ row: irow, span: totalRow > 12 ? 0 : 12 - totalRow, field: {} });

        //don't let the fields disappear
        $.each(grid[irow], function (icol, c) {
            if (c.field.ColSpan <= 0) {
                c.field.ColOffset = 0;
                totalCols = 2;
                c.field.ColSpan = 2;
            }
        });

    };
}
// for minification purposes
FormCtrl.$inject = ['$scope', '$http', 'sharedData', '$timeout', '$sce', 'dnnsf', '$filter'];



// The scope for EventCtrl will inherit from scope of FormCtrl
function EventCtrl($scope, $http, sharedData, $timeout, dnnsf) {

    $scope.actions = [];

    $scope.actionDefs = g_actionDefs;
    $scope.actionDefGroups = {};    //  group actions by category
    $scope.dnnsf = dnnsf;

    $.each($scope.actionDefs, function (i, o) {

        o.Groups && $.each(o.Groups, function (ig, group) {
            if (!$scope.actionDefGroups[group])
                $scope.actionDefGroups[group] = [];
            $scope.actionDefGroups[group].push(o);
        });

        if (!$scope.actionDefGroups[o.Settings['Group']])
            $scope.actionDefGroups[o.Settings['Group']] = [];

        $scope.actionDefGroups[o.Settings['Group']].push(o);
    });

    $scope.init = function (eventName, title, field, actionList) {

        $scope.actions.watchAllActions && $scope.actions.watchAllActions(); // this clears previous watch
        $scope.eventName = eventName;
        $scope.field = field;
        $scope.eventFriendlyTitle = title;

        if (field && !field.Actions) {
            dnnsf.log('Loading ' + eventName + ' actions from action list ', actionList);
            $scope.actions = actionList;
            $scope.prepareActions();
        } else
            if (field) {
                dnnsf.log('Loading ' + eventName + ' actions for field ', field);
                $scope.actions = field.Actions || [];
                $scope.prepareActions();
            } else {
                dnnsf.log('Loading actions for event ' + eventName);
                loadActions(eventName);
            }

    };
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ moved to dnnsfComponents 
    //$scope.addAction = function (actionType) {
    //    $timeout(function () {
    //        var action = {
    //            Id: -1,
    //            FieldId: $scope.field ? $scope.field.FormFieldId : null,
    //            $_uid: dnnsf.uniqueId('newAction'),
    //            $_field: $scope.field,
    //            EventName: $scope.eventName,
    //            Parameters: {},
    //            ActionType: actionType.Id,
    //            $_isOpen: true,
    //            $_isLoaded: true,
    //            $_isFocus: true
    //        };

    //        // copy defaults
    //        dnnsf.initParameters(actionType, action);
    //        $scope.actions.push(action);
    //    }, 50);
    //};
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ end
    $scope.deleteAction = function (action) {
        action.IsDeleted = true;
    };

    $scope.prepareActions = function () {

        dnnsf.log('preparing actions', $scope.actions);

        // reset modfied states
        $scope.actions.watchAllActions && $scope.actions.watchAllActions(); // this clears previous watch
        //$scope.sharedData.lockChange = true;

        $.each($scope.actions, function (iAction, action) {
            action.$_uid = dnnsf.uniqueId('action', action.Id);
            //action.$_field = $scope.field;

            var actionType = $scope.actionDefs[action.ActionType];
            if (typeof actionType == "undefined")
                return;

            dnnsf.initParameters(actionType, action, true);

            //// copy defaults
            //$.each($.grep(actionType.Parameters, function (e) { return e.DefaultValue; }), function (i, p) {
            //    if (action.Parameters[p.Id])
            //        return;


            //  //  var val = g_localizeMaybeJson(p.DefaultValue);
            //  //  action.Parameters[p.Id] = val;
            //});
        });
        $timeout(function () {
            //$scope.sharedData.lockChange = false;
        });

        // setup a watch to show the save button
        $scope.actions.watchAllActions = $scope.$watch('actions', function (newValue, oldValue) {

            dnnsf.log('check actions changed for ' + $scope.eventName);
            if (angular.equals(newValue, oldValue))
                return;

            // only call save if not already handled by a button
            if (!$scope.field && $.inArray($scope.save, $scope.onSave) == -1) {
                dnnsf.log("onSave handler registered for saving actions of " + $scope.eventName);
                $scope.onSave.push($scope.save);
            }
            dnnsf.log('actions changed for ' + $scope.eventName);
            // yes, this cope inherits from ActionForm, but there's an assignment happening 
            //      - this means for objects we get references but for primitive types we get copies
            // http://stackoverflow.com/questions/14049480/what-are-the-nuances-of-scope-prototypal-prototypical-inheritance-in-angularjs
            //++$scope.notifyModified(true);
        }, true); // this last true does a "deep" watch, otherwise angularJs would compare by reference, which never changes in this case here (arrays)

        // also resent fields ctrl, if it exist as a parent scope (i.e. we're doing the actions under a button)
        //$timeout(function () {
        //    $scope.notifyModified(false);
        //});
    };

    function loadActions(eventName) {
        $scope.sharedData.loadCount++;
        $http({
            method: 'GET',
            url: dnnsf.adminApi('GetEventActions', { eventName: eventName })
        }).success(function (data, status) {
            dnnsf.log('got ' + data.length + ' actions for event ' + eventName);
            $scope.actions = data;
            $scope.prepareActions();
            $scope.sharedData.loadCount--;
            $scope.sharedData.eventActions[eventName] = $scope.actions;
        });
    }

    $scope.checkFinal = function (action) {
        if (!$scope.actionDefs[action.ActionType])
            return false;
        return $scope.actionDefs[action.ActionType].Final && !action.Condition && $.inArray(action, $scope.actions) != $scope.actions.length - 1;
    };

    $scope.save = function () {

        if ($scope.field)
            return; // save already handled
        $http({
            method: 'POST',
            url: dnnsf.adminApi('SaveEventActions', { eventName: $scope.eventName }),
            data: angular.toJson($scope.actions)
        }).success(function (data, status) {
            // update our local copy, since the server may have done some alteration

            $scope.actions = data;
            $.each($scope.actions, function (intIndex, objValue) {
                objValue.$_uid = dnnsf.uniqueId('action', objValue.Id);
            });

            // setup a watch to show the save button
            $scope.prepareActions();
        });

        // delete entries right away
        $scope.actions = $.grep($scope.actions, function (e) { return !e.IsDeleted });

    };
}
// for minification purposes
EventCtrl.$inject = ['$scope', '$http', 'sharedData', '$timeout', 'dnnsf'];


// Initialize UI
$(function () {

    // initialize tooltips
    $(document).on('mouseover', '.help', function () {

        if ($(this).attr('data-original-title'))
            return;

        $(this).popover({
            html: true,
            placement: 'top'
        });
    });

    // fix affix - when clicking buttons in the menu the active item sometimes ends up being different than what was clicked
    $(window).hashchange(function () {
        var hash = location.hash;
        $('#navbar').find('li').removeClass('active');
        $('#navbar').find("[href='" + hash + "']").parent().addClass('active');
    });

    // tranform btn-link bootstrap buttons to some oher btn- when hovered
    $(document).on("mouseenter", ".btn-link-animate-trigger", function () {
        $(this).find(".btn-link-animate").each(function () {
            $(this).removeClass("btn-link").addClass("btn-" + $(this).attr("data-link-animate"))
                .stop(true, false).animate({ opacity: 1 })
                .find("i").addClass("glyphicon-white");
        });

    }).on("mouseleave", ".btn-link-animate-trigger", function () {
        $(this).find(".btn-link-animate").each(function () {
            $(this).addClass("btn-link").removeClass("btn-" + $(this).attr("data-link-animate"))
                .stop(true, false).animate({ opacity: 0.7 })
                .find("i").removeClass("glyphicon-white");
        });
    }).on('click', '.dropdown-submenu>a', function () {
        return false;
    });

    $('a[href*="#"]').click(function () {
        var l = $(this).attr('href');
        if (l == '#')
            return;

        // extract hash
        var hash = l.substr(l.indexOf('#'));
        var page = l.substr(0, l.indexOf('#'));
        if (page.toLowerCase() && page.toLowerCase() != window.location.pathname.toLowerCase())
            return;

        $('html, body').animate({
            scrollTop: $(hash).offset().top - 30
        }, 500);

        setTimeout(function () {
            $('#navbar').find('li').removeClass('active');
            $('#navbar').find("[href='" + l + "']").parent().addClass('active');
        },500);

        // also post message to top window to do the scroll
        window.top.postMessage(JSON.stringify({
            type: dnnsf.urlParam('comm-prefix') + "-scroll",
            offset: $(hash).offset().top
        }), "*");


        return false;
    });

    // this autosizes admin iframe so it doesn't have a scrollbar
    if (window.postMessage && window.top) {
        var __prevHeight = 0;
        setInterval(function () {
            var bodyHeight = $('body').height() + 50;
            if (bodyHeight != __prevHeight) {
                __prevHeight = bodyHeight;
                window.top.postMessage(JSON.stringify({
                    type: dnnsf.urlParam('comm-prefix') + "-height",
                    height: __prevHeight
                }), "*");
            }
        }, 200);
    }

    if (window.top != window)
        $('.visible-in-iframe').show();
    else
        $('.visible-in-iframe').hide();


});
})(dnnsfAngular15);