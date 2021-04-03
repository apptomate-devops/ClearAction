;

var $ = dnnsfjQuery;

function g_localize(o) {
    return o ? (o.hasOwnProperty('default') ? o['default'] : o) : "";
}

function g_localizeMaybeJson(o) {
    var val = g_localize(o);
    // TODO: find a better way to determine json arrays and objects - for example with a regex
    if (val && (val[0] == '[' && val[val.length - 1] == ']') || (val[0] == '{' && val[val.length - 1] == '}'))
        val = $.parseJSON(val);
    return val;
}

var app = dnnsfAngular15.module('app', ['ngRoute', 'dnnsf', 'dnnsf.components', 'ngSanitize', 'ui.bootstrap'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
      when('/', { controller: 'CredentialListCtrl', templateUrl: 'static/credential-store/views/main.html?v=' + g_resourceVersion }).
      //when('/settings', { controller: ActionGridCtrl, templateUrl: 'static/settings/main.html?v=' + g_resourceVersion }).
      //when('/datasource', { controller: DataSourceCtl, templateUrl: 'static/settings/datasource.html?v=' + g_resourceVersion }).
      //when('/method/:methodId/edit', { controller: ApiMethodCtrl, templateUrl: 'static/settings/edit-method.html?v=' + g_resourceVersion }).
      //when('/method/:methodId/test', { controller: ApiMethodCtrl, templateUrl: 'static/settings/test-method.html?v=' + g_resourceVersion }).
      //when('/method/new', { controller: ApiMethodCtrl, templateUrl: 'static/settings/edit-method.html?v=' + g_resourceVersion }).
      otherwise({ redirectTo: '/' });
}]);



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

function Parameters_AssignDefaultValues(defaultParameters, parameters) {
    for (var i = 0; i < defaultParameters.length; i++) {

        var p = defaultParameters[i];
        if (typeof parameters[p.Id] != 'undefined')
            continue; // this parameter already has a value

        parameters[p.Id] = p.DefaultValue ? g_localizeMaybeJson(p.DefaultValue) : '';
        //  maybe we have non-localized default settings
        if (!parameters[p.Id] && !p.DefaultValue && p.Settings && p.Settings['Defaults']) {
            var val = p.Settings['Defaults'];
            //  also see g_localizeMaybeJson; it will throw exception if not json
            if (val && (val[0] == '[' || val[0] == '{')) {
                var processBool = (val[0] != '[');
                val = $.parseJSON(val);
                //  we only can have arrays of bools ?
                if (!processBool) {
                    parameters[p.Id] = {};
                    for (var j = 0; val.length > j; ++j)
                        parameters[p.Id][val[j]] = true;
                    return;
                }
            }
            parameters[p.Id] = val;
        }

        // booleans will come as strings, convert them to true bool
        if (typeof (parameters[p.Id]) === 'object') {
            $.each(parameters[p.Id], function (key, value) {
                parameters[p.Id][key] = StringToBool(value);
            });
        } else {
            parameters[p.Id] = StringToBool(parameters[p.Id]);
        }

        // dnnsf.log(parameters[p.Id]);
    }
}

//  Parameters specific end

function inIframe() {
    try {
        return window.self !== window.top;
    } catch (e) {
        return true;
    }
};

app.directive('smoothScroll', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            element.bind('click', function (e) {
                if (inIframe()) {
                    window.parent.$('html,body').animate({ scrollTop: window.parent.$('iframe').offset().top + $(attr.smoothScroll).offset().top - 40 }, 500);
                } else {
                    $('html,body').animate({ scrollTop: $(attr.smoothScroll).offset().top - 40 }, 500);
                }
            });
        }
    };
});

// this is for sharing data
app.factory('sharedData', ['$http', '$timeout', 'dnnsf', function ($http, $timeout, dnnsf) {

    // watch modifications to enable the Save button and display "not saved"s warning
    var fnUnloadModified = function () { return 'Your changes are not saved.' }

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

        inArray: function (arr) {
            return function (input) {
                var i = $.inArray(input.BoundName, arr);
                if (i == -1)
                    return false;
                return true;
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

        loaded: false,
        loading: false,
        saving: false,
        modified: false,
        saveDisabled: false,
        invalidFields: {},

        _prepareSettings: function () {

            // init all fields
            var _this = this;
            _this.actionListFields = [];
            if (this.config.Fields) {

                var fnGetFields = function () {
                    return $.map(_this.config.Fields, function (o, i) { return { BoundName: o.Name, Title: o.Name }; });
                };

                $.each(_this.config.Fields, function (i, field) {
                    if (field.IsForm) {
                        _this.config.HasForm = true;
                        $.each(field.FormFields, function (j, formField) {
                            _this.actionListFields.push(formField);
                        })
                    } else {
                        _this.actionListFields.push(field);

                        $.each(field.Validation, function (iVld, vld) {
                            vld.$_uid = 'vld' + vld.Id;
                            vld.getFields = fnGetFields;
                        });
                    }

                });
                if (this.config.HasForm)
                    $.each(this.actionListFields, function (i, o) { _this._prepareField(o, 'field'); });
                $.each(this.config.Fields, function (i, o) { _this._prepareField(o, 'field'); });
                $.each(this.config.ItemButtons.List, function (i, o) { _this._prepareField(o, 'itemButton'); });
                $.each(this.config.GridButtons.List, function (i, o) { _this._prepareField(o, 'gridButton'); });
            }
        },

        _prepareField: function (field, baseId) {
            field.BoundName = field.BoundName || field.RefName || field.TitleCompacted;
            // generate unique IDs for all
            field.$_uid = dnnsf.uniqueId(baseId);
            field.AutoName = (field.Title && field.Id == field.Title.replace(/((^[^a-z]*)|([^a-z0-9_]*))/gi, ""));
        },

        load: function (fnReady) {

            // check if already loading the settings
            if (this.loading) {
                dnnsf.log('load called, but already loading');
                return;
            }

            // if already loaded, call handler, but not instantly
            if (this.loaded && fnReady) {
                $timeout(function () {
                    fnReady(this.config);
                });
                return;
            }

            this.loading = true;
            var _this = this;

            // Load initial data from the server
            dnnsf.log('fetching data...');
            $http({
                method: 'GET',
                url: dnnsf.adminApi('Get')
            }).success(function (data, status) {
                if (data.error)
                    alert(data.error);

                dnnsf.log('data retrieved');
                _this.config = data;

                _this.loaded = true;
                _this.loading = false;

                _this._prepareSettings();
                fnReady && fnReady(_this.config);
            });

        },

        save: function (fnReady, fnErr) {

            var _this = this;

            $timeout(function () {
                _this.notifyModified(false);
                _this.saving = true;
            });

            // flush deleted actions
            this.config.OnDeleteBulk.List = $.grep(this.config.OnDeleteBulk.List, function (item) { return !item.IsDeleted; });
            this.config.OnDeleteItem.List = $.grep(this.config.OnDeleteItem.List, function (item) { return !item.IsDeleted; });
            this.config.ItemButtons.List = $.grep(this.config.ItemButtons.List, function (item) { return !item.IsDeleted; });
            this.config.GridButtons.List = $.grep(this.config.GridButtons.List, function (item) { return !item.IsDeleted; });
            $.each(this.config.ItemButtons.List, function (i, btn) {
                btn.ItemActions = $.grep(btn.ItemActions, function (item) { return !item.IsDeleted; });
            });
            $.each(this.config.GridButtons.List, function (i, btn) {
                btn.ItemActions = btn.ItemActions ? $.grep(btn.ItemActions, function (item) { return !item.IsDeleted; }) : [];
                btn.GridActions = btn.GridActions ? $.grep(btn.GridActions, function (item) { return !item.IsDeleted; }) : [];
            });



            this.error = "";

            $.each(this.config.Fields, function (i, field) {
                if (field.FieldTypeStr == 'Form') {
                    $.each(field.FormFields, function (i, formField) {
                        formField.FormId = field.Id;
                    });
                };
            });
            console.log("data for request: ", this.config);
            localStorage.removeItem('preferredTemplate-' + this.config.ModuleId);
            $http({
                method: 'POST',
                url: dnnsf.adminApi('Save'),
                data: dnnsfAngular15.toJson(this.config)
            }).success(function (data, status) {
                console.log("returned data: ", data);
                if (data.Error || data.error) {
                    _this.error = data.Error || data.error;
                    fnErr && fnErr(_this.error);
                    alert(_this.error);
                    return;
                }

                //// update our local copy, since the server may have done some alteration
                _this.config = data;
                _this._prepareSettings();

                // modified will be set to true as the child EventsCtrl generats and update when setting up the actions
                $timeout(function () {
                    _this.saving = false;
                    _this.notifyModified(false);
                    fnReady && fnReady(_this.config);
                });

            });

        },

        notifyModified: function (bModified) {

            dnnsf.log('modified: ' + this.bModified + ' => ' + bModified);
            this.modified = bModified;

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
        }
    };
}]);

app.controller('CredentialListCtrl', ['$scope', '$http', 'sharedData', '$timeout', '$sce', 'dnnsf', function ($scope, $http, sharedData, $timeout, $sce, dnnsf) {
    // init data
    $scope.$sce = $sce;
    $scope.dnnsf = dnnsf;
    $scope.sharedData = sharedData;
    console.log("sharedData", sharedData);
    $scope.dnnsf = dnnsf;
    $scope.localize = g_localize;

    $scope.vldDefs = '';
    $scope.formatterDefs = '';

    $scope.bsIcons = ['glyphicon-asterisk', 'glyphicon-plus', 'glyphicon-euro', 'glyphicon-minus', 'glyphicon-cloud', 'glyphicon-envelope', 'glyphicon-pencil', 'glyphicon-glass', 'glyphicon-music', 'glyphicon-search', 'glyphicon-heart', 'glyphicon-star', 'glyphicon-star-empty', 'glyphicon-user', 'glyphicon-film', 'glyphicon-th-large', 'glyphicon-th', 'glyphicon-th-list', 'glyphicon-ok', 'glyphicon-remove', 'glyphicon-zoom-in', 'glyphicon-zoom-out', 'glyphicon-off', 'glyphicon-signal', 'glyphicon-cog', 'glyphicon-trash', 'glyphicon-home', 'glyphicon-file', 'glyphicon-time', 'glyphicon-road', 'glyphicon-download-alt', 'glyphicon-download', 'glyphicon-upload', 'glyphicon-inbox', 'glyphicon-play-circle', 'glyphicon-repeat', 'glyphicon-refresh', 'glyphicon-list-alt', 'glyphicon-lock', 'glyphicon-flag', 'glyphicon-headphones', 'glyphicon-volume-off', 'glyphicon-volume-down', 'glyphicon-volume-up', 'glyphicon-qrcode', 'glyphicon-barcode', 'glyphicon-tag', 'glyphicon-tags', 'glyphicon-book', 'glyphicon-bookmark', 'glyphicon-print', 'glyphicon-camera', 'glyphicon-font', 'glyphicon-bold', 'glyphicon-italic', 'glyphicon-text-height', 'glyphicon-text-width', 'glyphicon-align-left', 'glyphicon-align-center', 'glyphicon-align-right', 'glyphicon-align-justify', 'glyphicon-list', 'glyphicon-indent-left', 'glyphicon-indent-right', 'glyphicon-facetime-video', 'glyphicon-picture', 'glyphicon-map-marker', 'glyphicon-adjust', 'glyphicon-tint', 'glyphicon-edit', 'glyphicon-share', 'glyphicon-check', 'glyphicon-move', 'glyphicon-step-backward', 'glyphicon-fast-backward', 'glyphicon-backward', 'glyphicon-play', 'glyphicon-pause', 'glyphicon-stop', 'glyphicon-forward', 'glyphicon-fast-forward', 'glyphicon-step-forward', 'glyphicon-eject', 'glyphicon-chevron-left', 'glyphicon-chevron-right', 'glyphicon-plus-sign', 'glyphicon-minus-sign', 'glyphicon-remove-sign', 'glyphicon-ok-sign', 'glyphicon-question-sign', 'glyphicon-info-sign', 'glyphicon-screenshot', 'glyphicon-remove-circle', 'glyphicon-ok-circle', 'glyphicon-ban-circle', 'glyphicon-arrow-left', 'glyphicon-arrow-right', 'glyphicon-arrow-up', 'glyphicon-arrow-down', 'glyphicon-share-alt', 'glyphicon-resize-full', 'glyphicon-resize-small', 'glyphicon-exclamation-sign', 'glyphicon-gift', 'glyphicon-leaf', 'glyphicon-fire', 'glyphicon-eye-open', 'glyphicon-eye-close', 'glyphicon-warning-sign', 'glyphicon-plane', 'glyphicon-calendar', 'glyphicon-random', 'glyphicon-comment', 'glyphicon-magnet', 'glyphicon-chevron-up', 'glyphicon-chevron-down', 'glyphicon-retweet', 'glyphicon-shopping-cart', 'glyphicon-folder-close', 'glyphicon-folder-open', 'glyphicon-resize-vertical', 'glyphicon-resize-horizontal', 'glyphicon-hdd', 'glyphicon-bullhorn', 'glyphicon-bell', 'glyphicon-certificate', 'glyphicon-thumbs-up', 'glyphicon-thumbs-down', 'glyphicon-hand-right', 'glyphicon-hand-left', 'glyphicon-hand-up', 'glyphicon-hand-down', 'glyphicon-circle-arrow-right', 'glyphicon-circle-arrow-left', 'glyphicon-circle-arrow-up', 'glyphicon-circle-arrow-down', 'glyphicon-globe', 'glyphicon-wrench', 'glyphicon-tasks', 'glyphicon-filter', 'glyphicon-briefcase', 'glyphicon-fullscreen', 'glyphicon-dashboard', 'glyphicon-paperclip', 'glyphicon-heart-empty', 'glyphicon-link', 'glyphicon-phone', 'glyphicon-pushpin', 'glyphicon-usd', 'glyphicon-gbp', 'glyphicon-sort', 'glyphicon-sort-by-alphabet', 'glyphicon-sort-by-alphabet-alt', 'glyphicon-sort-by-order', 'glyphicon-sort-by-order-alt', 'glyphicon-sort-by-attributes', 'glyphicon-sort-by-attributes-alt', 'glyphicon-unchecked', 'glyphicon-expand', 'glyphicon-collapse-down', 'glyphicon-collapse-up', 'glyphicon-log-in', 'glyphicon-flash', 'glyphicon-log-out', 'glyphicon-new-window', 'glyphicon-record', 'glyphicon-save', 'glyphicon-open', 'glyphicon-saved', 'glyphicon-import', 'glyphicon-export', 'glyphicon-send', 'glyphicon-floppy-disk', 'glyphicon-floppy-saved', 'glyphicon-floppy-remove', 'glyphicon-floppy-save', 'glyphicon-floppy-open', 'glyphicon-credit-card', 'glyphicon-transfer', 'glyphicon-cutlery', 'glyphicon-header', 'glyphicon-compressed', 'glyphicon-earphone', 'glyphicon-phone-alt', 'glyphicon-tower', 'glyphicon-stats', 'glyphicon-sd-video', 'glyphicon-hd-video', 'glyphicon-subtitles', 'glyphicon-sound-stereo', 'glyphicon-sound-dolby', 'glyphicon-sound-5-1', 'glyphicon-sound-6-1', 'glyphicon-sound-7-1', 'glyphicon-copyright-mark', 'glyphicon-registration-mark', 'glyphicon-cloud-download', 'glyphicon-cloud-upload', 'glyphicon-tree-conifer', 'glyphicon-tree-deciduous'];

    $scope.entries = {};
    $scope.credentialGroups = [];
    $scope.groupsMarkedForDeletion = [];
    $scope.groupsMarkedForSave = [];

    $http.get(dnnsf.adminApi('GetCredentialTypeDef', { TypeName: dnnsf.urlParam('type') }, { apiUrl: dnnsf.commonUrl + '/CommonApi.ashx' }))
        .then(function (response) {
            $scope.credentialTypeDef = response.data;
        }, function (error) {
            console.error('HTTP Error:', error);
        });

    $scope.loadGroups = function () {
        $http.get(dnnsf.adminApi('GetCredentialGroups', { TypeName: dnnsf.urlParam('type') }, { apiUrl: dnnsf.commonUrl + '/CommonApi.ashx' }))
            .then(function (response) {
                $.each(response.data, function (idx, group) {
                    group.Children = null;
                });
                $scope.credentialGroups = response.data;
            }, function (error) {
                console.error('HTTP Error:', error);
            });
    };
    $scope.loadGroups();

    $scope.toggleGroup = function (group) {
        if (group.$tOut)
            $timeout.cancel(group.$tOut);

        if (group.isOpen) {
            $scope.closeGroup(group);
            group.$tOut = $timeout(function () { group.isOpen = false; }, 300);
        }
        else
            group.isOpen = true;
    }

    $scope.toggleEntry = function (entry) {
        if (entry.$tOut)
            $timeout.cancel(entry.$tOut);

        if (entry.isOpen) {
            $scope.closeEntry(entry.Value);
            entry.$tOut = $timeout(function () { entry.isOpen = false; }, 300);
        }
        else
            entry.isOpen = true;
    }

    $scope.getEntries = function (group) {
        if (!group) return [];
        if (group.Children)
            return group.Children;
        if (!group.Value || group.Value == "")
            return [];

        group.Children = [];
        $http.get(dnnsf.adminApi('GetCredentialEntries', { GroupId: group.Value }, { apiUrl: dnnsf.commonUrl + '/CommonApi.ashx' }))
            .then(function (response) {
                group.Children = response.data;
            }, function (error) {
                console.error('HTTP Error:', error);
            });
        return group.Children;
    }

    $scope.closeGroup = function (group) {
        $.each(group.Children, function (idx, entry) {
            $scope.closeEntry(entry.Value);
        });
        group.Children = null;
    }

    $scope.addGroup = function () {
        var grp = { Text: 'New Group', Value: '', Children: [] };
        $scope.credentialGroups.push(grp);
        $scope.markGroupForSave(grp);
    }

    $scope.getEntryData = function (entry) {
        var entryId = entry.Value;
        if (!entryId || entryId == "")
            return $scope.entries['$_' + entry.$$hashKey] || ($scope.entries['$_' + entry.$$hashKey] = {});

        if ($scope.entries[entryId])
            return $scope.entries[entryId];

        $scope.entries[entryId] = {};
        $http.get(dnnsf.adminApi('GetEntryData', { entryId: entryId }, { apiUrl: dnnsf.commonUrl + '/CommonApi.ashx' }))
            .then(function (response) {
                $scope.entries[entryId] = response.data;
            }, function (error) {
                console.error('HTTP Error:', error);
            });
        return $scope.entries[entryId];
    }

    $scope.saveEntry = function (group, entry) {
        if (!entry.Text)
            return;
        if ($scope.groupsMarkedForSave.indexOf(group) > -1) {
            $scope.saveGroup(group, function (response) {
                $scope.saveEntry(group, entry);
            });
            return;
        }

        var model = $scope.getEntryData(entry);
        $http.post(dnnsf.adminApi('SaveEntry', { groupId: group.Value, entryId: entry.Value, entryName: entry.Text }, { apiUrl: dnnsf.commonUrl + '/CommonApi.ashx' }), model)
            .then(function (response) {
                // clean and update entry
                delete $scope.entries[entry.Value];
                $scope.entries[response.data.entryId] = response.data.model;
                entry.Value = response.data.entryId;
                entry.Text = response.data.entryName;

                // toggle the accordion as a sign of successfull save
                $scope.toggleEntry(entry);
                var grpIdx = $scope.credentialGroups.indexOf(group);
                var entIdx = group.Children.indexOf(entry);
                $('#CredentialsEntry-' + grpIdx + '-' + entIdx).collapse('toggle');
            }, function (error) {
                console.error('HTTP Error:', error);
            });
    }

    $scope.deleteEntry = function (group, idx) {
        var entry = group.Children[idx];
        delete $scope.entries[entry.Value];
        if (!entry.Value || entry.Value == '') {
            group.Children.splice(idx, 1)
            return;
        }
        $http.post(dnnsf.adminApi('DeleteEntry', { entryId: entry.Value }, { apiUrl: dnnsf.commonUrl + '/CommonApi.ashx' }))
           .then(function (response) {
               group.Children.splice(idx, 1);
           }, function (error) {
               console.error('HTTP Error:', error);
           });
    }

    $scope.markGroupForDelete = function (group) {
        var saveIdx = $scope.groupsMarkedForSave.indexOf(group);
        if (saveIdx > -1)
            $scope.groupsMarkedForSave.splice(saveIdx, 1);

        var idx = $scope.groupsMarkedForDeletion.indexOf(group);
        if (idx == -1)
            $scope.groupsMarkedForDeletion.push(group);
        else
            $scope.groupsMarkedForDeletion.splice(idx, 1);
    };

    $scope.markGroupForSave = function (group) {
        var delIdx = $scope.groupsMarkedForDeletion.indexOf(group);
        if (delIdx > -1) return;

        var idx = $scope.groupsMarkedForSave.indexOf(group);
        if (idx == -1)
            $scope.groupsMarkedForSave.push(group);
    };

    $scope.updateGroups = function () {
        $scope.saving = true;
        if ($scope.groupsMarkedForDeletion.length)
            $scope.deleteGroups();
        if ($scope.groupsMarkedForSave.length)
            $scope.saveGroups();

        $timeout(function () { $scope.saving = false; }, 400);
    };

    $scope.saveGroup = function (group, successCallback) {
        $http.post(dnnsf.adminApi('SaveGroup', { typeName: dnnsf.urlParam('type'), groupId: group.Value, groupName: group.Text }, { apiUrl: dnnsf.commonUrl + '/CommonApi.ashx' }))
               .then(function (response) {
                   var idx = $scope.groupsMarkedForSave.indexOf(group);
                   if (idx > -1)
                       $scope.groupsMarkedForSave.splice(idx, 1);
                   group.Value = response.data.groupId;
                   group.Text = response.data.groupName;
                   successCallback && successCallback(response);
               }, function (error) {
                   console.error('HTTP Error:', error);
               });
    }

    $scope.saveGroups = function () {
        $.each($scope.groupsMarkedForSave, function (idx, group) {
            $scope.saveGroup(group, function () {
                if (!$scope.groupsMarkedForSave.length && !$scope.groupsMarkedForDeletion.length) {
                    $scope.entries = {};
                    $scope.loadGroups();
                }
            });
        });
    }

    $scope.deleteGroups = function () {
        $.each($scope.groupsMarkedForDeletion, function (idx, group) {

            if (!group.Value || group.Value == '') {
                var idx = $scope.credentialGroups.indexOf(group);
                if (idx > -1)
                    $scope.credentialGroups.splice(idx, 1);
                return;
            }

            $http.post(dnnsf.adminApi('DeleteGroup', { groupId: group.Value }, { apiUrl: dnnsf.commonUrl + '/CommonApi.ashx' }))
               .then(function (response) {
                   var idx = $scope.groupsMarkedForDeletion.indexOf(group);
                   if (idx > -1)
                       $scope.groupsMarkedForDeletion.splice(idx, 1);

                   if (!$scope.groupsMarkedForSave.length && !$scope.groupsMarkedForDeletion.length) {
                       $scope.entries = {};
                       $scope.loadGroups();
                   }
               }, function (error) {
                   console.error('HTTP Error:', error);
               });
        });
    }

    $scope.addEntry = function (group) {
        group.Children.push({
            Text: 'New Entry',
            Value: ''
        });
    }

    $scope.closeEntry = function (entryId) {
        $scope.entries[entryId] = null;
    }

}]);


// Initialize UI
$(function () {

    // fix affix - when clicking buttons in the menu the active item sometimes ends up being different than what was clicked
    $(window).hashchange(function () {
        var hash = location.hash;
        $('#navbar').find('li').removeClass('active');
        $('#navbar').find('[href="' + hash + '"]').parent().addClass('active');
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
            scrollTop: $(hash).offset().top - 20
        }, 500);

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

