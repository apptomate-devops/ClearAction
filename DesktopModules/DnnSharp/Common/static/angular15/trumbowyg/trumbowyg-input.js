(function (angular) {

    angular.module('trumbowyg', [])

        .directive('trumbowyg', function () {
            'use strict';
            return {
                transclude: true,
                restrict: 'EA',
                require: '?ngModel',
                scope: {
                    ngModel: '=',
                    updateField: '&',
                    theme: '@',
                    btns: '@',
                    btnsGrps: '=',
                    btnsDef: '=',
                    initialValue: '@',
                    lang: '@'
                },
                link: {
                    pre: function (scope, element, attrs) {
                        scope.editorConfig = {};
                        scope.groups = {};
                        var customButtons = {};
                        scope.name = attrs.name;
                        scope.field = attrs.afField;

                        // Dark Theme checkbox
                        (scope.theme === 'True') && element.closest('.af-slide').addClass('trumbowyg-dark');

                        // button groups as object with array values
                        _.forEach(scope.btnsGrps, function (group) {
                            var groupName = group.name;
                            scope.groups[groupName] = group.value.split(" ");
                        });

                        var btns = _.forEach(attrs.btns.split('|'), function (item, index, array) {
                            if (array[index].slice(0, 7) === 'btnGrp-') {
                                array[index] = item;
                            } else {
                                array[index] = _.forEach(item.split(','), function (item, index, array) {
                                    array[index] = item.trim();
                                });
                            }
                        })

                        _.forEach(btns, function (item, index, array) {
                            if (item.slice(0, 7) === 'btnGrp-') {
                                var splitArray = item.split(',');
                                array[index] = splitArray[0];
                                for (var i = 1; i < splitArray.length; i++) {
                                    array.splice(index + 1, 0, splitArray[i]);
                                }
                            }
                        })

                        // custom buttons 
                        _.forEach(scope.btnsDef, function (item, index, array) {
                            var buttonsArray = [];
                            _.forEach(item.DropdownButtons.Value.split(' '), function (item, index, array) {
                                buttonsArray.push(item);
                            })
                            customButtons[item.DropdownName.Value] = {
                                "ico": item.DropdownIcon.Value,
                                "dropdown": buttonsArray
                            }
                        });

                        scope.groups && (scope.editorConfig.btnsGrps = scope.groups);
                        btns && (scope.editorConfig.btns = btns);
                        customButtons && (scope.editorConfig.btnsDef = customButtons);
                    },
                    post: function (scope, element, attrs, ngModelCtrl) {

                        var options = angular.extend({
                            fullscreenable: true,
                            closable: false,
                            lang: scope.lang,
                        }, scope.editorConfig);

                        ngModelCtrl.$render = function () {
                            angular.element(element).trumbowyg('html', ngModelCtrl.$viewValue);
                        };
                        angular.element(element).trumbowyg(options).on('tbwchange', function () {
                            scope.updateField({ field: scope.field, val: angular.element(element).trumbowyg('html') });
                        }).on('tbwpaste', function () {
                            scope.updateField({ field: scope.field, val: angular.element(element).trumbowyg('html') });
                        });

                        scope.$parent.$watch(attrs.ngdisabled, function (newVal) {
                            angular.element(element).trumbowyg(newVal ? 'disable' : 'enable');
                        });

                        var elementHeight = parseInt(element[0].style.height);

                        if (elementHeight > 0) {
                            $(element).parent().find('textarea')[0].style.height = (elementHeight - 38).toString() + 'px'; // 38 is the buttonbar height
                            $(element).parent().attr('style', element.attr('style'));
                            element.removeAttr('style');
                            element[0].style.height = (parseInt($(element).parent()[0].style.height) - 40).toString() + 'px';
                        } else {
                            $(element).parent().attr('style', element.attr('style')).height('auto');
                            element.removeAttr('style');
                        }

                        // Initial Value
                        scope.ngModel = scope.ngModel || scope.initialValue;
                        element.html(scope.ngModel);
                        scope.$on('updateFormData', function () {
                            element.html(scope.ngModel);
                        });
                        scope.$apply();
                    }
                }
            }
        });
})(dnnsfAngular15 || angular);
