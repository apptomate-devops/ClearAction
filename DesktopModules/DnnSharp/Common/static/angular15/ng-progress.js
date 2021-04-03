(function (dnnsfAngular15) {
    'use strict';

    dnnsfAngular15.module('progressbar', [])
        .directive('progressbar', ['$compile', '$http', function ($compile, $http) {
            return {
                restrict: 'EA',
                scope: {
                    fieldId: "=",
                    fname: "=?",
                    value: "=?",
                    minval: "=?",
                    maxval: "=?",
                    strokecolor: "=?",
                    trailcolor: "=?",
                    strokewidth: "=?",
                    trailwidth: "=?",
                    animation: "=?",
                    animationduration: "=?",
                    ptype: "=?",
                    displaytexttype: '=?',
                    valuecolor: '=?'
                },
                link: function (scope, element, attrs, ctrls) {
                    var bar = undefined;
                    var steps = undefined;
                    if(scope.valuecolor)
                        steps = JSON.parse(scope.valuecolor);

                    var settings = {
                        strokeWidth: scope.strokewidth ? parseInt(scope.strokewidth) : 2,
                        easing: scope.animation ? scope.animation : '',
                        duration: scope.animationduration ? parseInt(scope.animationduration) : 1400,
                        color: scope.strokecolor ? scope.strokecolor : '#ED6A5A',
                        trailColor: scope.trailcolor ? scope.trailcolor : 'gray',
                        trailWidth: scope.trailwidth ? parseInt(scope.trailwidth) : 1,
                        svgStyle: { width: '100%', height: '100%' }
                    };
                    switch(scope.ptype) {
                        case 'Circle':
                            var displayPercentage = {
                                text: {
                                    autoStyleContainer: false
                                },
                                step: function (state, circle) {
                                    circle.path.setAttribute('stroke-width', settings.strokeWidth);
                                    var currentColor = settings.color;
                                    if (steps && steps.length > 0) {
                                        $.each(steps, function (index, step) {
                                            if (parseInt(scope.value) >= parseInt(step.name)) {
                                                currentColor = step.value;
                                            }
                                        });
                                        circle.path.setAttribute('stroke', currentColor);
                                        circle.text != null && (circle.text.style.color = currentColor);
                                    } else {
                                        circle.path.setAttribute('stroke', settings.color);
                                        circle.text != null && (circle.text.style.color = settings.color);
                                    }
                                    var valuep = Math.round(circle.value() * 100);
                                    if (valuep === 0) {
                                        circle.setText('0%');
                                    } else {
                                        circle.setText(valuep + '%');
                                    }
                                }
                            };
                            var displayValue = {
                                text: {
                                    autoStyleContainer: false
                                },
                                step: function (state, circle) {
                                    circle.path.setAttribute('stroke-width', settings.strokeWidth);
                                    var currentColor = settings.color;
                                    if (steps && steps.length > 0) {
                                        $.each(steps, function (index, step) {
                                            if (parseInt(scope.value) >= parseInt(step.name)) {
                                                currentColor = step.value;
                                            }
                                        });
                                        circle.path.setAttribute('stroke', currentColor);
                                        circle.text != null && (circle.text.style.color = currentColor);
                                    } else {
                                        circle.path.setAttribute('stroke', settings.color);
                                        circle.text != null && (circle.text.style.color = settings.color);
                                    }
                                    circle.setText(scope.value);
                                }
                            };
                            var displayNone = {
                                text: {
                                    autoStyleContainer: false
                                },
                                step: function (state, circle) {
                                    circle.path.setAttribute('stroke-width', settings.strokeWidth);
                                    var currentColor = settings.color;
                                    if (steps && steps.length > 0) {
                                        $.each(steps, function (index, step) {
                                            if (parseInt(scope.value) >= parseInt(step.name)) {
                                                currentColor = step.value;
                                            }
                                        });
                                        circle.path.setAttribute('stroke', currentColor);
                                        circle.text != null && (circle.text.style.color = currentColor);
                                    } else {
                                        circle.path.setAttribute('stroke', settings.color);
                                        circle.text != null && (circle.text.style.color = settings.color);
                                    }
                                    circle.setText('');
                                }
                            };
                            var displayTextType = scope.displaytexttype ? scope.displaytexttype : 'None'
                            if (displayTextType == 'Percentage') {
                                settings = $.extend(settings, displayPercentage);
                            }
                            if (displayTextType == 'Value') {
                                settings = $.extend(settings, displayValue);
                            }
                            if (displayTextType == 'None') {
                                settings = $.extend(settings, displayNone);
                            }
                            bar = new ProgressBar.Circle('#' + element[0].id, settings);
                            bar.text.style.fontFamily = '"Raleway", Helvetica, sans-serif';
                            bar.text.style.fontSize = '2rem';
                            break;
                        case 'Semicircle':
                            var displayPercentage = {
                                text: {
                                    autoStyleContainer: false
                                },
                                step: function (state, circle) {
                                    circle.path.setAttribute('stroke-width', settings.strokeWidth);
                                    var currentColor = settings.color;
                                    if (steps && steps.length > 0) {
                                        $.each(steps, function (index, step) {
                                            if (parseInt(scope.value) >= parseInt(step.name)) {
                                                currentColor = step.value;
                                            }
                                        });
                                        circle.path.setAttribute('stroke', currentColor);
                                        circle.text != null && (circle.text.style.color = currentColor);
                                    } else {
                                        circle.path.setAttribute('stroke', settings.color);
                                        circle.text != null && (circle.text.style.color = settings.color);
                                    }
                                    var valuep = Math.round(circle.value() * 100);
                                    if (valuep === 0) {
                                        circle.setText('0%');
                                    } else {
                                        circle.setText(valuep + '%');
                                    }
                                }
                            };
                            var displayValue = {
                                text: {
                                    autoStyleContainer: false
                                },
                                step: function (state, circle) {
                                    circle.path.setAttribute('stroke-width', settings.strokeWidth);
                                    var currentColor = settings.color;
                                    if (steps && steps.length > 0) {
                                        $.each(steps, function (index, step) {
                                            if (parseInt(scope.value) >= parseInt(step.name)) {
                                                currentColor = step.value;
                                            }
                                        });
                                        circle.path.setAttribute('stroke', currentColor);
                                        circle.text != null && (circle.text.style.color = currentColor);
                                    } else {
                                        circle.path.setAttribute('stroke', settings.color);
                                        circle.text != null && (circle.text.style.color = settings.color);
                                    }
                                    circle.setText(scope.value);
                                }
                            };
                            var displayNone = {
                                text: {
                                    autoStyleContainer: false
                                },
                                step: function (state, circle) {
                                    circle.path.setAttribute('stroke-width', settings.strokeWidth);
                                    var currentColor = settings.color;
                                    if (steps && steps.length > 0) {
                                        $.each(steps, function (index, step) {
                                            if (parseInt(scope.value) >= parseInt(step.name)) {
                                                currentColor = step.value;
                                            }
                                        });
                                        circle.path.setAttribute('stroke', currentColor);
                                        circle.text != null && (circle.text.style.color = currentColor);
                                    } else {
                                        circle.path.setAttribute('stroke', settings.color);
                                        circle.text != null && (circle.text.style.color = settings.color);
                                    }
                                    circle.setText('');
                                }
                            };
                            var displayTextType = scope.displaytexttype ? scope.displaytexttype : 'None'
                            if (displayTextType == 'Percentage') {
                                settings = $.extend(settings, displayPercentage);
                            }
                            if (displayTextType == 'Value') {
                                settings = $.extend(settings, displayValue);
                            }
                            if (displayTextType == 'None') {
                                settings = $.extend(settings, displayNone);
                            }
                            bar = new ProgressBar.SemiCircle('#' + element[0].id, settings);
                            bar.text.style.fontFamily = '"Raleway", Helvetica, sans-serif';
                            bar.text.style.fontSize = '2rem';
                            break;
                        default:
                            var displayPercentage = {
                                text: {
                                    style: {
                                        color: '#999',
                                        position: 'absolute',
                                        right: '0',
                                        padding: 0,
                                        margin: 0,
                                        transform: null,
                                        'font-size': '1rem'
                                    },
                                    autoStyleContainer: false
                                },
                                step: (state, bar) => {
                                    bar.path.setAttribute('stroke-width', settings.strokeWidth);
                                    var currentColor = settings.color;
                                    if (steps && steps.length > 0) {
                                        $.each(steps, function (index, step) {
                                            if (parseInt(scope.value) >= parseInt(step.name)) {
                                                currentColor = step.value;
                                            }
                                        });
                                        bar.path.setAttribute('stroke', currentColor);
                                        bar.text != null && (bar.text.style.color = currentColor);
                                    } else {
                                        bar.path.setAttribute('stroke', settings.color);
                                        bar.text != null && (bar.text.style.color = settings.color);
                                    }
                                    bar.setText(Math.round(bar.value() * 100) + ' %');
                                }
                            };
                            var displayValue = {
                                text: {
                                    style: {
                                        color: '#999',
                                        position: 'absolute',
                                        right: '0',
                                        padding: 0,
                                        margin: 0,
                                        transform: null,
                                        'font-size': '1rem'
                                    },
                                    autoStyleContainer: false
                                },
                                step: (state, bar) => {
                                    bar.path.setAttribute('stroke-width', settings.strokeWidth);
                                    var currentColor = settings.color;
                                    if (steps && steps.length > 0) {
                                        $.each(steps, function (index, step) {
                                            if (parseInt(scope.value) >= parseInt(step.name)) {
                                                currentColor = step.value;
                                            }
                                        });
                                        bar.path.setAttribute('stroke', currentColor);
                                        bar.text != null && (bar.text.style.color = currentColor);
                                    } else {
                                        bar.path.setAttribute('stroke', settings.color);
                                        bar.text != null && (bar.text.style.color = settings.color);
                                    }
                                    bar.setText(scope.value);
                                }
                            };
                            var displayNone = {
                                step: (state, bar) => {
                                    bar.path.setAttribute('stroke-width', settings.strokeWidth);
                                    var currentColor = settings.color;
                                    if (steps && steps.length > 0) {
                                        $.each(steps, function (index, step) {
                                            if (parseInt(scope.value) >= parseInt(step.name)) {
                                                currentColor = step.value;
                                            }
                                        });
                                        bar.path.setAttribute('stroke', currentColor);
                                        bar.text != null && (bar.text.style.color = currentColor);
                                    } else {
                                        bar.path.setAttribute('stroke', settings.color);
                                        bar.text != null && (bar.text.style.color = settings.color);
                                    }
                                }
                            };
                            var displayTextType = scope.displaytexttype ? scope.displaytexttype : 'None'
                            if (displayTextType == 'Percentage') {
                                settings = $.extend(settings, displayPercentage);
                            }
                            if (displayTextType == 'Value') {
                                settings = $.extend(settings, displayValue);
                            }
                            if (displayTextType == 'None') {
                                settings = $.extend(settings, displayNone);
                            }
                            bar = new ProgressBar.Line('#' + element[0].id, settings);
                    }

                    scope.maxval = scope.maxval ? scope.maxval : 100;
                    scope.minval = scope.minval ? scope.minval : 0;

                    var realMax = scope.maxval - scope.minval;
                    var value = scope.value != undefined ? (scope.value - scope.minval) : scope.minval;
                    var percentage = 1 / (realMax / value);
                    if (percentage > 1)
                        percentage = 1;
                    if (percentage < 0)
                        percentage = 0;
                    bar.animate(percentage);
                    element.attr('data-val', scope.value);

                    scope.$watch('value', function (newValue, oldValue, scope) {
                        realMax = scope.maxval - scope.minval;
                        value = scope.value != undefined ? (scope.value - scope.minval) : scope.minval;
                        percentage = 1 / (realMax / value);
                        if (percentage > 1)
                            percentage = 1;
                        if (percentage < 0)
                            percentage = 0;
                        bar.animate(percentage);
                        element.attr('data-val', scope.value);
                    });
                }
            };
        }])
})(dnnsfAngular15);