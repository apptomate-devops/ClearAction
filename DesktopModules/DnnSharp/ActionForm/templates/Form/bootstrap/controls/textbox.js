
;
dnnsfAngular15.module('afControls').directive('afTextbox', ['$compile', '$timeout', '$parse', '$sce', '$http', '$templateCache',
    function ($compile, $timeout, $parse, $sce, $http, $templateCache) {
        return {
            restrict: 'A',
            scope: {
                settings: '=',
                field: '=',
                registerToEvent: '&'
            },
            //templateUrl: "textbox.html",
            link: function (scope, element, attrs) {
                console.log(scope.settings);

                var templateUrl = scope.settings.FormTemplateBaseUrl + "/controls/textbox.html";// $parse(attrs.template)(scope);
                $http.get(templateUrl, { cache: $templateCache }).success(function (tplContent) {
                    element.replaceWith($compile(tplContent)(scope));
                });
            }
        };
    }]);

