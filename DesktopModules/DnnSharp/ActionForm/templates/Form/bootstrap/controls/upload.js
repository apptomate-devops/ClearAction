;
(dnnsfjQuery || $).ajax({
    type: "GET",
    url: dnnsf.commonUrl + '/static/upload/jquery.fileupload-all.min.js?v=' + dnnsf.version,
    dataType: "script",
    cache: true,
    async: false
});
(dnnsfjQuery || $)(document).bind('drop dragover', function (e) {
    e.preventDefault(); //preventing the default browser action when the file is dropped outside of our dropZone
});

dnnsfAngular15.module('afControls')

.config([
    '$httpProvider', 'fileUploadProvider',
    function ($httpProvider, fileUploadProvider) {
        delete $httpProvider.defaults.headers.common['X-Requested-With'];
        fileUploadProvider.defaults.redirect = window.location.href.replace(
            /\/[^\/]*$/,
            '/cors/result.html?%s'
        );
        //if (isOnGitHub) {
        // Demo settings:
        dnnsfAngular15.extend(fileUploadProvider.defaults, {
            // Enable image resizing, except for Android and Opera,
            // which actually support image resizing, but fail to
            // send Blob objects via XHR requests:
            disableImageResize: /Android(?!.*Chrome)|Opera/
                .test(window.navigator.userAgent)//,
            //maxFileSize: 999000,
            //acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i
        });
        //}
    }
])

.directive('afMultiUpload', ['$compile', '$timeout', '$parse', '$sce', '$http', '$templateCache', '$interval',
function ($compile, $timeout, $parse, $sce, $http, $templateCache, $interval) {
    return {
        restrict: 'A',
        scope: {
            settings: '=',
            field: '=',
            registerControl: '&',
            registerToEvent: '&',
            selection: '='
        },
        //templateUrl: "textbox.html",
        link: function (scope, element, attrs) {
            scope.dnnsf = dnnsf;
            var templateUrl;
            var element0;
            scope.field.files = [];
            if (attrs.singlefileupload != undefined) {
                templateUrl = scope.settings.FormTemplateBaseUrl + "/controls/upload-single.html";
            } else {
                templateUrl = scope.settings.FormTemplateBaseUrl + "/controls/upload.html";
            }
            scope.fieldName = scope.field.Title;

            $http.get(templateUrl, { cache: $templateCache }).success(function (tplContent) {
                element0 = $compile(tplContent)(scope);
                element.replaceWith(element0);

                scope.options = {
                    url: scope.settings.UploadUrl + '&fieldid=' + scope.field.FormFieldId,
                    urlDelete: scope.settings.UploadUrl + '&fieldid=' + scope.field.FormFieldId + '&method=delete'
                };

                scope.loadingFiles = true;
                scope.getOptions();
                scope.singleFile = '';
                scope.getExistingFiles();
            });

            //inserting existing files in a list || can be changed to be in scope.queue
            scope.getExistingFiles = function () {
                var folderPath = scope.field.Parameters['Folder'];
                if (folderPath.hasOwnProperty('IsExpression')) {
                    if (folderPath.IsExpression && !folderPath.Value)
                        folderPath = folderPath.Expression;
                    else
                        folderPath = folderPath.Value;
                }

                // If no folder is set in the settings
                // the files will be uploaded to the Temp folder
                // and then deleted after the actions are processed,
                // so we should not try to preview any of them here.
                if (!folderPath) {
                    scope.selection = scope.loadedFiles = [];
                    return;
                }
                var baseUrl = scope.settings.PortalFolder + folderPath;

                if (scope.selection) {
                    scope.loadedFiles = [];
                    if (attrs.singlefileupload != undefined) {
                        //populating loaded file for single file upload field
                        var file = {
                            name: scope.selection,
                            baseUrl: baseUrl + '/' + scope.selection,
                            state: "existing"
                        };
                        scope.loadedFiles.push(file);
                        scope.selection = scope.loadedFiles;
                    } else {
                        //populating loaded files for multiple file upload field
                        scope.loaded = dnnsfAngular15.fromJson(scope.selection);
                        for (var i = 0; i < scope.loaded.length; i++) {
                            var file = {
                                name: scope.loaded[i],
                                baseUrl: baseUrl + '/' + scope.loaded[i],
                                state: "existing"
                            };
                            scope.loadedFiles.push(file);
                        }
                        console.log("loadedFiles: ", scope.loadedFiles);
                        scope.selection = scope.loadedFiles;
                    }
                }
            }

            scope.getOptions = function () {
                scope.loadingFiles = false;
                scope.queue = [];
                //$http.get(scope.options.url)
                //    .then(
                //        function (response) {
                //            scope.loadingFiles = false;
                //            scope.queue = response.data.files || [];
                //        },
                //        function () {
                //            scope.loadingFiles = false;
                //        }
                //    );
            };

            scope.clearExistingFile = function (file, $index) {
                scope.loadedFiles[$index].state = "delete";
                scope.selection[$index].state = "delete";
            };

            scope.clearSingleFile = function () {
                scope.singleFile = {};
            };
   
            scope.$on('fileuploadadd', function (a, b) {
                scope.$watch(function () {
                    return scope.processing && scope.processing();
                }, function (newVal, oldVal) {
                    var formRoot = $(element0).closest('.form-root');
                    var $btn = formRoot[0].$btn;
                    newVal ? formRoot.find('.submit').not($btn).attr('disabled', 'disabled') : formRoot.find('.submit').not($btn).removeAttr('disabled');
                });
                if (attrs.singlefileupload != undefined) {
                    setTimeout(function () {
                        console.log("added new file in queue!", scope.queue);
                        if (scope.queue.length > 1) {
                            scope.queue.splice(0, 1);
                            console.log("scope.queue: ", scope.queue);
                            scope.singleFile = scope.queue[scope.queue.length - 1];
                        } else {
                            scope.singleFile = scope.queue[scope.queue.length - 1];
                        }
                        scope.$apply();
                    },0);
                } else {
                    setTimeout(function () {
                        scope.files = scope.files || [];
                        scope.files.push(b.files[0].name);
                        scope.$apply();
                    }, 0);
                }
            });

            scope.$on('fileuploadstart', function () {
                scope.toUpload = scope.queue.length;
                scope.files = [];
                scope.single = "";
            });

            scope.$on('remove', function () {
                $(element0).closest('.form-root').find('div.alert.server-error').hide();
            });

            scope.$on('fileuploaddone', function (event, data) {
                if (data.result.error) {
                    scope.fnCallOnError(data.result.error);
                    //var formRoot = $(element0).closest('.form-root');
                    //var $btn = formRoot[0].$btn;
                    //formRoot.find(".server-error").html(data.result.error).show();
                    //scope.queue = [];
                    //afResetButton(formRoot, $btn);
                    //$('.g-recaptcha', formRoot).each(function () {
                    //    grecaptcha.reset($(this).attr('data-widgetid'));
                    //});
                } else {
                    scope.toUpload--;
                    scope.field.files.push(data.result[0].relativeurl);
                    scope.single = {
                        name: data.result[0].relativeurl,
                        state: "new"
                    };
    
                    if (scope.toUpload == 0)
                        scope.field.fnCallWhenDone();
                }
            });

            if (!scope.field.isInitialized) {
                scope.field.isInitialized = true;
                scope.registerControl({
                    control: {
                        field: scope.field,
                        onSubmit: function (fnCallWhenDone, fnCallOnError) {
                            scope.fnCallWhenDone = scope.field.fnCallWhenDone = fnCallWhenDone;//need it for drag and drop
                            scope.fnCallOnError = fnCallOnError;
                            var isErr = false;
                            $.each(scope.queue, function (i, file) {
                                !isErr && file.error && fnCallOnError(file.error);
                            });

                            !isErr && scope.submit();
                            scope.queue = [];
                            scope.selection = [];
                            scope.field.files = [];
                        },
                        getValue: function () {
                            if (!scope.selection)
                                scope.selection = [];
                            if (attrs.singlefileupload != undefined) {
                                if (!scope.single && !scope.selection.length) {
                                    scope.single = {
                                        name: "",
                                        state: ""
                                    };
                                    return JSON.stringify(scope.single);
                                } else if (!scope.single && scope.selection.length) {
                                    scope.single = scope.selection[0];
                                    return JSON.stringify(scope.single);
                                } else {
                                    return JSON.stringify(scope.single);
                                }
                            } else {
                                dnnsfAngular15.forEach(scope.field.files, function (name, key) {
                                    var file = {
                                        name: name,
                                        state: "new"
                                    };
                                    scope.selection.push(file);
                                });
                                return JSON.stringify(_.uniqBy(scope.selection.concat(scope.loadedFiles || []), 'name'));
                            }
                        },
                        deleteFiles: function () {
                            if (scope.loadedFiles) {
                                //Clearing existing files from scope.selection
                                var deleteSelection = [];
                                for (var i = 0; i < scope.loadedFiles.length; i++) {
                                    if (scope.loadedFiles[i].state == "delete") {
                                        deleteSelection.push(scope.loadedFiles[i].name);
                                    }
                                }
                                return deleteSelection;
                            }
                        },
                        deleteUrl: function () {
                            if (scope.loadedFiles) {
                                return scope.options.urlDelete;
                            }
                        }
                    }
                });
            }

            scope.registerToEvent({
                eventName: 'submit',
                fn: function (fnCallWhenDone, fnCallOnError) {
                    scope.fnCallWhenDone = fnCallWhenDone;
                    scope.fnCallOnError = fnCallOnError;
                    scope.submit();
                }
            });

            var stop;
            scope.fnInit = function () {
                if (dnnsfAngular15.isDefined(stop)) return;

                stop = $interval(function () {
                    if (scope.option) {
                        if (scope.field.Parameters['FileExt']) {
                            scope.field.Parameters['FileExt'] && scope.option('acceptFileTypes', new RegExp('(\\.|\\/)(' + scope.field.Parameters['FileExt'].replace(/,/g, '|') + ')$', 'i'));
                        }
                        if (scope.field.Parameters['FileLimit'] && scope.field.Parameters['FileLimit'] / 10000 < 1) {
                            scope.field.Parameters['FileLimit'] = scope.field.Parameters['FileLimit'] * 1000000;
                            scope.field.Parameters['FileLimit'] && scope.option('maxFileSize', scope.field.Parameters['FileLimit']);
                        }
                        $interval.cancel(stop);
                    }
                }, 100);
            }
            scope.fnInit();
        }
    };
}]);

