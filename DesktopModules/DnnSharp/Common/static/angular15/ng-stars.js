(function (angular) {
    'use strict';

    angular.module('ngstars', [])
        .directive('starRating', ['$compile', '$http', function ($compile, $http) {
            return {
                restrict: 'EA',
                scope: {
                    fieldId: "=",
                    fname: "=?",
                    starsno: "=?",
                    step: "=?",
                    size: "=?",
                    minimumrating: "=?",
                    maximumrating: "=?",
                    selectedstarcolor: "=?",
                    unselectedstarcolor: "=?",
                    starcaptions: "=?",
                    starcaptionclasses: "=?",
                    showcaption: "=?",
                    showclear: "=?",
                    value: "=?",
                    isenabled: "=?",
                    onStarsChange: "&"
                },
                link: function (scope, element, attrs, ctrls) {

                    //parse data for starcaption and starcaption clases
                    var starCap = {};
                    if (scope.starcaptions)
                    $.each(scope.starcaptions, function (i) {
                        starCap[scope.starcaptions[i].value] = scope.starcaptions[i].name;
                    });

                    var starCapCls = {};
                    if (scope.starcaptionclasses)
                    $.each(scope.starcaptionclasses, function (i) {
                        starCapCls[scope.starcaptionclasses[i].value] = scope.starcaptionclasses[i].name;
                    });

                    //init
                    element.rating({
                        stars: scope.starsno ? scope.starsno : dnnsfjQuery.fn.rating.defaults.stars,
                        min: scope.minimumrating ? scope.minimumrating : 0 ,
                        max: scope.maximumrating ? scope.maximumrating : (scope.starno ? scope.starno : dnnsfjQuery.fn.rating.defaults.stars),
                        step: scope.step ? scope.step : 0.5 ,
                        size: scope.size ? scope.size : dnnsfjQuery.fn.rating.defaults.size,
                        showClear: scope.showclear == 'True' ? false : true,
                        disabled: scope.isenabled == 'True' ? false : true,
                        showCaption: scope.showcaption == 'True' ? false : true,
                        starCaptionClasses: starCapCls ? starCapCls : dnnsfjQuery.fn.rating.defaults.starCaptionClasses,
                        starCaptions: starCap ? starCap : dnnsfjQuery.fn.rating.defaults.starCaptions
                    });

                    element.attr('data-val', scope.value || scope.minimumrating || 0);
                    element.parent().attr('style', 'color:' + scope.unselectedstarcolor)
                    element.parent().children().first().css('color', scope.selectedstarcolor)

                    //onchange
                    $('[name="' + element[0].name + '"]').on('rating.change', function (event, value, caption) {
                        scope.onStarsChange({ 'value': value, 'name': scope.fname });
                        $(this).attr('data-val', value);
                        $(this).attr('value', value);
                    });
                }
            };
        }])
})(dnnsfAngular15 || angular);