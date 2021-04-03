dnnsfAngular15.module('sortable', [])

.directive('sortable', function () {
    return {
        restrict: 'A',
        replace: true,
        scope: {
            ngModel: '=',
            items: '=items',
            updateField: '&'
        },
        templateUrl: dnnsf.commonUrl + '/static/angular15/sortable-input.html',
        link: {
            pre: function (scope, el, attrs) {
                scope.iconPosition = attrs.iconPosition || 'left';
                scope.iconClass = attrs.iconClass;
                scope.name = attrs.name;
                scope.field = attrs.afField;
                $.each(scope.items, function (i, v) {
                    scope.ngModel.push(v.value || v.text)
                })
            },
            post: function (scope, el, attrs) {
                setTimeout(function () {
                    $(el).find('.list-group').sortable({
                        axis: "y",
                        containment: $(el).closest('.form-group'),
                        cursor: "move",
                        handle: attrs.dragByIcon == 'True' ? ".handle" : '',
                        tolerance: "pointer",
                        update: function (event, ui) {
                            scope.ngModel = [];
                            $.each($(el).find('.list-group-item'), function (i, v) {
                                scope.ngModel.push($(v).attr('data-val'))
                            });
                            scope.$apply();
                            scope.updateField({ field: scope.field, val: scope.ngModel });
                        }
                    });
               
                }, 0)
            }
        }
    }
});
