// dnnsfjQuery Mask Plugin v1.14.10
// github.com/igorescobar/dnnsfjQuery-Mask-Plugin
var $jscomp = { scope: {}, findInternal: function (a, f, c) { a instanceof String && (a = String(a)); for (var l = a.length, g = 0; g < l; g++) { var b = a[g]; if (f.call(c, b, g, a)) return { i: g, v: b } } return { i: -1, v: void 0 } } }; $jscomp.defineProperty = "function" == typeof Object.defineProperties ? Object.defineProperty : function (a, f, c) { if (c.get || c.set) throw new TypeError("ES3 does not support getters and setters."); a != Array.prototype && a != Object.prototype && (a[f] = c.value) };
$jscomp.getGlobal = function (a) { return "undefined" != typeof window && window === a ? a : "undefined" != typeof global && null != global ? global : a }; $jscomp.global = $jscomp.getGlobal(this); $jscomp.polyfill = function (a, f, c, l) { if (f) { c = $jscomp.global; a = a.split("."); for (l = 0; l < a.length - 1; l++) { var g = a[l]; g in c || (c[g] = {}); c = c[g] } a = a[a.length - 1]; l = c[a]; f = f(l); f != l && null != f && $jscomp.defineProperty(c, a, { configurable: !0, writable: !0, value: f }) } };
$jscomp.polyfill("Array.prototype.find", function (a) { return a ? a : function (a, c) { return $jscomp.findInternal(this, a, c).v } }, "es6-impl", "es3");
(function (a, f, c) { "function" === typeof define && define.amd ? define(["dnnsfjQuery"], a) : "object" === typeof exports ? module.exports = a(require("dnnsfjQuery")) : a(f || c) })(function (a) {
    var f = function (b, h, e) {
        var d = {
            invalid: [], getCaret: function () { try { var a, n = 0, h = b.get(0), e = document.selection, k = h.selectionStart; if (e && -1 === navigator.appVersion.indexOf("MSIE 10")) a = e.createRange(), a.moveStart("character", -d.val().length), n = a.text.length; else if (k || "0" === k) n = k; return n } catch (A) { } }, setCaret: function (a) {
                try {
                    if (b.is(":focus")) {
                        var p,
                            d = b.get(0); d.setSelectionRange ? d.setSelectionRange(a, a) : (p = d.createTextRange(), p.collapse(!0), p.moveEnd("character", a), p.moveStart("character", a), p.select())
                    }
                } catch (z) { }
            }, events: function () {
                b.on("keydown.mask", function (a) { b.data("mask-keycode", a.keyCode || a.which); b.data("mask-previus-value", b.val()) }).on(a.jMaskGlobals.useInput ? "input.mask" : "keyup.mask", d.behaviour).on("paste.mask drop.mask", function () { setTimeout(function () { b.keydown().keyup() }, 100) }).on("change.mask", function () { b.data("changed", !0) }).on("blur.mask",
                    function () { c === d.val() || b.data("changed") || b.trigger("change"); b.data("changed", !1) }).on("blur.mask", function () { c = d.val() }).on("focus.mask", function (b) { !0 === e.selectOnFocus && a(b.target).select() }).on("focusout.mask", function () { e.clearIfNotMatch && !g.test(d.val()) && d.val("") })
            }, getRegexMask: function () {
                for (var a = [], b, d, e, k, c = 0; c < h.length; c++) (b = m.translation[h.charAt(c)]) ? (d = b.pattern.toString().replace(/.{1}$|^.{1}/g, ""), e = b.optional, (b = b.recursive) ? (a.push(h.charAt(c)), k = { digit: h.charAt(c), pattern: d }) :
                    a.push(e || b ? d + "?" : d)) : a.push(h.charAt(c).replace(/[-\/\\^$*+?.()|[\]{}]/g, "\\$&")); a = a.join(""); k && (a = a.replace(new RegExp("(" + k.digit + "(.*" + k.digit + ")?)"), "($1)?").replace(new RegExp(k.digit, "g"), k.pattern)); return new RegExp(a)
            }, destroyEvents: function () { b.off("input keydown keyup paste drop blur focusout ".split(" ").join(".mask ")) }, val: function (a) { var d = b.is("input") ? "val" : "text"; if (0 < arguments.length) { if (b[d]() !== a) b[d](a); d = b } else d = b[d](); return d }, calculateCaretPosition: function (a, d) {
                var h =
                    d.length, e = b.data("mask-previus-value") || "", k = e.length; 8 === b.data("mask-keycode") && e !== d ? a -= d.slice(0, a).length - e.slice(0, a).length : e !== d && (a = a >= k ? h : a + (d.slice(0, a).length - e.slice(0, a).length)); return a
            }, behaviour: function (e) { e = e || window.event; d.invalid = []; var h = b.data("mask-keycode"); if (-1 === a.inArray(h, m.byPassKeys)) { var h = d.getMasked(), c = d.getCaret(); setTimeout(function (a, b) { d.setCaret(d.calculateCaretPosition(a, b)) }, 10, c, h); d.val(h); d.setCaret(c); return d.callbacks(e) } }, getMasked: function (a, b) {
                var c =
                    [], p = void 0 === b ? d.val() : b + "", k = 0, g = h.length, f = 0, l = p.length, n = 1, v = "push", w = -1, r, u; e.reverse ? (v = "unshift", n = -1, r = 0, k = g - 1, f = l - 1, u = function () { return -1 < k && -1 < f }) : (r = g - 1, u = function () { return k < g && f < l }); for (var y; u() ;) {
                        var x = h.charAt(k), t = p.charAt(f), q = m.translation[x]; if (q) t.match(q.pattern) ? (c[v](t), q.recursive && (-1 === w ? w = k : k === r && (k = w - n), r === w && (k -= n)), k += n) : t === y ? y = void 0 : q.optional ? (k += n, f -= n) : q.fallback ? (c[v](q.fallback), k += n, f -= n) : d.invalid.push({ p: f, v: t, e: q.pattern }), f += n; else {
                            if (!a) c[v](x); t === x ?
                                f += n : y = x; k += n
                        }
                    } p = h.charAt(r); g !== l + 1 || m.translation[p] || c.push(p); return c.join("")
            }, callbacks: function (a) { var f = d.val(), p = f !== c, g = [f, a, b, e], k = function (a, b, d) { "function" === typeof e[a] && b && e[a].apply(this, d) }; k("onChange", !0 === p, g); k("onKeyPress", !0 === p, g); k("onComplete", f.length === h.length, g); k("onInvalid", 0 < d.invalid.length, [f, a, b, d.invalid, e]) }
        }; b = a(b); var m = this, c = d.val(), g; h = "function" === typeof h ? h(d.val(), void 0, b, e) : h; m.mask = h; m.options = e; m.remove = function () {
            var a = d.getCaret(); d.destroyEvents();
            d.val(m.getCleanVal()); d.setCaret(a); return b
        }; m.getCleanVal = function () { return d.getMasked(!0) }; m.getMaskedVal = function (a) { return d.getMasked(!1, a) }; m.init = function (c) {
            c = c || !1; e = e || {}; m.clearIfNotMatch = a.jMaskGlobals.clearIfNotMatch; m.byPassKeys = a.jMaskGlobals.byPassKeys; m.translation = a.extend({}, a.jMaskGlobals.translation, e.translation); m = a.extend(!0, {}, m, e); g = d.getRegexMask(); if (c) d.events(), d.val(d.getMasked()); else {
                e.placeholder && b.attr("placeholder", e.placeholder); b.data("mask") && b.attr("autocomplete",
                    "off"); c = 0; for (var f = !0; c < h.length; c++) { var l = m.translation[h.charAt(c)]; if (l && l.recursive) { f = !1; break } } f && b.attr("maxlength", h.length); d.destroyEvents(); d.events(); c = d.getCaret(); d.val(d.getMasked()); d.setCaret(c)
            }
        }; m.init(!b.is("input"))
    }; a.maskWatchers = {}; var c = function () {
        var b = a(this), c = {}, e = b.attr("data-mask"); b.attr("data-mask-reverse") && (c.reverse = !0); b.attr("data-mask-clearifnotmatch") && (c.clearIfNotMatch = !0); "true" === b.attr("data-mask-selectonfocus") && (c.selectOnFocus = !0); if (l(b, e, c)) return b.data("mask",
            new f(this, e, c))
    }, l = function (b, c, e) { e = e || {}; var d = a(b).data("mask"), h = JSON.stringify; b = a(b).val() || a(b).text(); try { return "function" === typeof c && (c = c(b)), "object" !== typeof d || h(d.options) !== h(e) || d.mask !== c } catch (u) { } }, g = function (a) { var b = document.createElement("div"), c; a = "on" + a; c = a in b; c || (b.setAttribute(a, "return;"), c = "function" === typeof b[a]); return c }; a.fn.mask = function (b, c) {
        c = c || {}; var e = this.selector, d = a.jMaskGlobals, h = d.watchInterval, d = c.watchInputs || d.watchInputs, g = function () {
            if (l(this, b,
                c)) return a(this).data("mask", new f(this, b, c))
        }; a(this).each(g); e && "" !== e && d && (clearInterval(a.maskWatchers[e]), a.maskWatchers[e] = setInterval(function () { a(document).find(e).each(g) }, h)); return this
    }; a.fn.masked = function (a) { return this.data("mask").getMaskedVal(a) }; a.fn.unmask = function () { clearInterval(a.maskWatchers[this.selector]); delete a.maskWatchers[this.selector]; return this.each(function () { var b = a(this).data("mask"); b && b.remove().removeData("mask") }) }; a.fn.cleanVal = function () { return this.data("mask").getCleanVal() };
    a.applyDataMask = function (b) { b = b || a.jMaskGlobals.maskElements; (b instanceof a ? b : a(b)).filter(a.jMaskGlobals.dataMaskAttr).each(c) }; g = {
        maskElements: "input,td,span,div", dataMaskAttr: "*[data-mask]", dataMask: !0, watchInterval: 300, watchInputs: !0, useInput: !/Chrome\/[2-4][0-9]|SamsungBrowser/.test(window.navigator.userAgent) && g("input"), watchDataMask: !1, byPassKeys: [9, 16, 17, 18, 36, 37, 38, 39, 40, 91], translation: {
            0: { pattern: /\d/ }, 9: { pattern: /\d/, optional: !0 }, "#": { pattern: /\d/, recursive: !0 }, A: { pattern: /[a-zA-Z0-9]/ },
            S: { pattern: /[a-zA-Z]/ }
        }
    }; a.jMaskGlobals = a.jMaskGlobals || {}; g = a.jMaskGlobals = a.extend(!0, {}, g, a.jMaskGlobals); g.dataMask && a.applyDataMask(); setInterval(function () { a.jMaskGlobals.watchDataMask && a.applyDataMask() }, g.watchInterval)
}, window.dnnsfjQuery, window.Zepto);




/*!
 * jQuery Cookie Plugin v1.4.0
 * https://github.com/carhartl/jquery-cookie
 *
 * Copyright 2013 Klaus Hartl
 * Released under the MIT license
 */
(function (factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD. Register as anonymous module.
        define(['dnnsfjQuery'], factory);
    } else {
        // Browser globals.
        factory(dnnsfjQuery);
    }
}(function ($) {

    var pluses = /\+/g;

    function encode(s) {
        return config.raw ? s : encodeURIComponent(s);
    }

    function decode(s) {
        return config.raw ? s : decodeURIComponent(s);
    }

    function stringifyCookieValue(value) {
        return encode(config.json ? JSON.stringify(value) : String(value));
    }

    function parseCookieValue(s) {
        if (s.indexOf('"') === 0) {
            // This is a quoted cookie as according to RFC2068, unescape...
            s = s.slice(1, -1).replace(/\\"/g, '"').replace(/\\\\/g, '\\');
        }

        try {
            // Replace server-side written pluses with spaces.
            // If we can't decode the cookie, ignore it, it's unusable.
            // If we can't parse the cookie, ignore it, it's unusable.
            s = decodeURIComponent(s.replace(pluses, ' '));
            return config.json ? JSON.parse(s) : s;
        } catch (e) { }
    }

    function read(s, converter) {
        var value = config.raw ? s : parseCookieValue(s);
        return $.isFunction(converter) ? converter(value) : value;
    }

    var config = $.cookie = function (key, value, options) {

        // Write
        if (value !== undefined && !$.isFunction(value)) {
            options = $.extend({}, config.defaults, options);

            if (typeof options.expires === 'number') {
                var days = options.expires, t = options.expires = new Date();
                t.setDate(t.getDate() + days);
            }

            return (document.cookie = [
                encode(key), '=', stringifyCookieValue(value),
                options.expires ? '; expires=' + options.expires.toUTCString() : '', // use expires attribute, max-age is not supported by IE
                options.path ? '; path=' + options.path : '',
                options.domain ? '; domain=' + options.domain : '',
                options.secure ? '; secure' : ''
            ].join(''));
        }

        // Read

        var result = key ? undefined : {};

        // To prevent the for loop in the first place assign an empty array
        // in case there are no cookies at all. Also prevents odd result when
        // calling $.cookie().
        var cookies = document.cookie ? document.cookie.split('; ') : [];

        for (var i = 0, l = cookies.length; i < l; i++) {
            var parts = cookies[i].split('=');
            var name = decode(parts.shift());
            var cookie = parts.join('=');

            if (key && key === name) {
                // If second argument (value) is a function it's a converter...
                result = read(cookie, value);
                break;
            }

            // Prevent storing a cookie that we couldn't decode.
            if (!key && (cookie = read(cookie)) !== undefined) {
                result[name] = cookie;
            }
        }

        return result;
    };

    config.defaults = {};

    $.removeCookie = function (key, options) {
        if ($.cookie(key) === undefined) {
            return false;
        }

        // Must not alter options, thus extending a fresh object...
        $.cookie(key, '', $.extend({}, options, { expires: -1 }));
        return !$.cookie(key);
    };

}));


var afApp = (function ($, angular) {
    try {
        if (!angular.module('afControls').hasController('afApplyLayout'))
            throw '';
    } catch (err) {
        angular.module('afControls').directive('afApplyLayout', ['$compile', function ($compile) {
            return {
                restrict: 'A',
                replace: false,
                scope: true,
                link: function (scope, element, attrs) {

                    scope.settings = scope.$parent.$eval(attrs.settings);
                    scope.field = scope.$parent.$eval(attrs.field);
                    scope.form = scope.$parent.$eval(attrs.form);
                    scope.parse = function (val) {
                        if ($.isNumeric(val))
                            return parseFloat(val);
                        if (typeof val === 'boolean')
                            return val;
                        // remove comma - this only works for locales that use comma for thousands
                        val = val.toString().replace(/,(\d{3})/g, '$1');
                        if ($.isNumeric(val))
                            return parseFloat(val);

                        return val;
                    };

                    element.parent().addClass('field-container af-slide ');
                    if (scope.field.CssClass != '')
                        element.parent().addClass('form-group-' + scope.field.CssClass);
                    element.addClass(scope.field.CssClass);

                    if (scope.field.ColOffset > 0 && (scope.settings.LabelAlign.Value == 4 || scope.settings.LabelAlign.Value == 5))
                        element.parent().addClass('col-sm-offset-' + scope.field.ColOffset);

                    if (scope.settings.LabelWidth.Value > 0 && scope.settings.LabelAlign.Value != 4 && scope.settings.LabelAlign != 5)
                        element.parent().addClass('col-sm-' + (scope.field.ColSpan - scope.settings.LabelWidth.Value));
                    else
                        element.parent().addClass('col-sm-' + scope.field.ColSpan);

                    if (scope.field.BindShowCompiled && !element.attr('data-initialized')) {
                        if (scope.field.BindPreserveLayout) {
                            element.attr('data-ng-style', "{visibility: " + scope.field.BindShowCompiled + " ? 'visible' : 'hidden'}");
                        } else {
                            element.attr('data-ng-show', scope.field.BindShowCompiled);
                        }
                    }

                    // recompile the directive so dynamically added directives such as ngShow work
                    if (!element.attr('data-initialized')) {
                        element.attr('data-initialized', 'true');
                        $compile(element)(scope);
                    }
                }
            };
        }]);
    };

    // --------------------------------------------------------------------------------------------------------------------------------
    /// Start Action Form template specific code

    function ActionFormCtrl($scope, $http, $timeout, $sce, $cookieStore, $element, dataSources, dnnsf) {
        var $ = dnnsfjQuery;
        $scope.dnnsf = dnnsf;
        $scope.$sce = $sce;
        $scope.Math = Math;

        $scope.testTags = [];

        $scope.form = {
            fields: []
        };

        // get the field based on the html element.
        $scope.getField = function (htmlElement) {
            var field = _.find(_.values($scope.form.fields), function (_field) {
                return _field.id === htmlElement.id;
            });
            if (!field) return null;
            return field;
        };

        $scope.controls = [];
        $scope.registerControl = function (control) {
            $scope.controls.push(control);
        };

        $scope.parse = function (val) {
            if (val === undefined || val === null)
                return false;

            if ($.isNumeric(val))
                return parseFloat(val);

            // remove comma - this only works for locales that use comma for thousands
            val = val.constructor === Array ? val.toString() : val.toString().replace(/,(\d{3})/g, '$1');
            if ($.isNumeric(val))
                return parseFloat(val);

            if (val.toLowerCase() == "true")
                return true;

            if (val.toLowerCase() == "false")
                return false;

            return val;
        };

        $scope.load = function (mid) {
            var formRoot = $('#dnn' + mid + 'root');
            var res = dnnsf['af-' + mid];
            if (res.Data.error) {
                alert('Error: ' + res.Data.error);
                return;
            }

            // todo: handle errors
            $scope.form = res.Data;
            $scope.settings = res.Settings;
            $scope.$$ = $; // just a hack to bypass angular

            //closing the dropdown(multiple choice - dropdown with checkboxes) when clicking outside the container
            $(document).mouseup(function (e) {
                var container = $(".field-container.checkbox-list .panel.panel-default");
                if (!container.is(e.target)
                    && container.has(e.target).length === 0) {
                    $.each($scope.form.fields, function (i, field) {
                        if (field.show) {
                            setTimeout(function () {
                                field.show = false;
                                $scope.$apply();
                            }, 0);
                        }
                    });
                }
            });

            $.each(res.Settings.Fields, function (i, f) {
                res.Settings.Fields[f.TitleCompacted] = f;
            });

            if (res.Data.response) {
                parseFormResponse2($('#' + res.Data.baseId + "root"), undefined, res.Data.response);
            }

            $.each($scope.form.fields, function (k, o) {

                // try to cast type to numbers, otherwise they won't fit in the HTML number field
                //if ($.isNumeric(o.value))
                //    o.value = parseFloat(o.value);

                if (o.onChange) {
                    // bind onChange, handle false to remove form-button class which will prevent postback - a bit of a hackish solution until we migrate everything to angular
                    o.onChange = eval(
                        '( function(form, item) { ' +
                        '   try { ' +
                        '       var scope = $scope = angular.element(\'#' + res.Data.baseId + 'root\').scope(); ' +
                        '       var refresh = function() { if (!scope.$$phase) scope.$apply(); }; ' +
                        '       var r = (function() { ' + o.onChange + ' }).call($(\'#' + o.id + '\')); ' +
                        '       if($(\'#' + o.id + '\')[0]){ ' +
                        '       $(\'#' + o.id + '\')[0].preventSubmit = false;' +
                        '       if (r === false) { ' +
                        '           $(\'#' + o.id + '\')[0].preventSubmit = true; return false; ' +
                        '       }} ' +
                        '   } catch (e) { console.log(\'Error running Action Form on change script\', e); }' +
                        '})');
                }

                if (o.options) {
                    // this is a dropdown, initialize ddValue and tbValue
                    o.ddValue = o.tbValue = o.value;

                    // add a getValue function on dropdowns, which will be used by validators to get the proper value for validation
                    if (_.find(['closed-multiple-dropdown', 'closed-multiple-checkbox', 'dropdown-checkboxes'],
                        function (type) { return o.type == type; })
                    ) {
                        o.getValue = function (value) {
                            return value.indexOf('/') === 0 ? value.substring(1) : value;
                        }
                    }

                    if (o.type == 'closed-multiple-checkbox' || o.type == 'dropdown-checkboxes') {
                        var selItems = o.value;
                        $.each(o.options, function (k, oItem) {
                            oItem.selected = $.inArray(oItem.value, selItems) != -1;
                        });
                    } else {
                        for (var i = 0; i < o.options.length; i++)
                            if (o.options[i].value == o.value) {
                                o.selected = o.options[i]; break;
                            }
                    }

                    // cast option values to numbers
                    //$.each(o.options, function (i, opt) {
                    //    if ($.isNumeric(opt.value))
                    //        opt.value = parseFloat(opt.value);
                    //});

                    // if it has "other" option and value doesn't exist in dropdown, switch dd to other
                    var other = $.grep(o.options, function (oOpt, iOpt) { return oOpt.filter == 'other'; });
                    other = other.length ? other[0] : null;

                    if (other && o.value && $.grep(o.options, function (oOpt, iOpt) { return oOpt.value == o.value; }).length == 0) {
                        o.ddValue = other.value;
                        o.otherValue = o.value;
                        o.selected = _.filter(o.options, function (o) { return o.filter == "other"; })[0];
                    }

                    if (o.type == 'address-region') {
                        $scope.$watch('form.fields.' + o.name + '.value', function () {
                            o.ddValue = o.value;
                            o.tbValue = o.value;
                            if (o.value && o.countryField) {
                                var selectedRegion = _.find($scope.countries[o.countryField].regions, function (obj) {
                                    return obj.value == o.value;
                                });
                                o.viewValue = selectedRegion ? selectedRegion.text : o.value;
                            } else
                                o.viewValue = o.value;
                                
                        }, true);
                    }
                    // if it's a checkbox list, this is also needed
                    if (o.type == 'closed-multiple-checkbox' || o.type == 'dropdown-checkboxes') {
                        $scope.concatValues(o);
                        // also, watch for changes

                        $scope.$watch('form.fields.' + o.name + '.value', function () {
                            var selItems = o.value;
                            $.each(o.options, function (k, oItem) {
                                oItem.selected = $.inArray(oItem.value, selItems) != -1;
                            });
                        }, true);

                        o.checkAll = function () {
                            $.each(o.options, function (k, oItem) {
                                if (o.visible !== false)
                                    oItem.selected = true;
                            });
                        };

                        o.uncheckAll = function () {
                            $.each(o.options, function (k, oItem) {
                                if (o.visible !== false)
                                    oItem.selected = false;
                            });
                        };
                    } else {
                        $scope.$watch('form.fields.' + o.name + '.value', function (newValue, oldValue) {
                            // this allows setting the value of dropdown directly in the model
                            // but watch out if we have multiple entries with same value filtered away
                            for (var i = 0; i < o.options.length; i++)
                                if (o.options[i].value == newValue) {
                                    o.selected = o.options[i];
                                    break;
                                }
                        });
                    }

                    if (o.linkedTo) {

                        var fnFetchData = function (value) {
                            $timeout(function () {
                                // get list from server
                                var saveData = getFormData($element.closest('.form-root'));

                                o.$_loading = true;
                                $http({
                                    method: 'POST',
                                    url: $scope.form.getItemsUrl + "&fieldId=" + $scope.form.fields[o.name].fieldId,
                                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                                    data: $.param(saveData)
                                }).success(function (data, status) {
                                    o.options = data;
                                    var found = false;
                                    for (var i = 0; i < o.options.length; i++)
                                        if (o.tbValue.constructor === Array ? $.inArray(o.options[i].value, o.tbValue) != -1 : o.options[i].value == o.tbValue) {
                                            o.selected = o.options[i];
                                            if (o.tbValue.constructor === Array) {
                                                $.each(o.tbValue, function (i, v) {
                                                    $.each(o.options, function (j, k) {
                                                        k.value == v && (o.options[j].selected = true);
                                                    });
                                                });
                                            }
                                            o.value = o.tbValue; //need it to get 
                                            found = true;
                                            break;
                                        }
                                    if (!found)
                                        o.value = '';
                                    $scope.concatValues(o);
                                    o.$_loading = false;
                                });
                            });
                        };

                        $.each($.map(o.linkedTo.split(','), function (x) { return x.trim() }), function (i, field) {
                            $scope.$watch('form.fields.' + field + '.value', function (newValue, oldValue) {

                                if (typeof newValue != 'undefined' && !angular.equals(newValue, oldValue)) {
                                    console.log($scope.form.fields[field]);
                                    fnFetchData(newValue);
                                }

                            });
                            $scope.$watch('form.fields.' + field + '.options', function (newValue, oldValue) {

                                if (typeof newValue != 'undefined' && $scope.form.fields[field].linkedTo) {
                                    fnFetchData(newValue);
                                }

                            }, true);
                        });
                    }

                    // also group options by filters for easy iteration
                    if (o.options.length) {

                        // this is used by the permission grid
                        o.optionsFilters = []; // the object below loses order, so we're going to use this one for iterations
                        $.each(o.options, function (ii, oo) {
                            if ($.inArray(oo.filter, o.optionsFilters) == -1)
                                o.optionsFilters.push(oo.filter);
                        });

                        o.optionsNames = [];// the object below loses order, so we're going to use this one for iterations
                        o.optionsByName = {};
                        $.each(o.options, function (ii, oo) {
                            if (!o.optionsByName[oo.text]) {
                                o.optionsNames.push(oo.text);
                                o.optionsByName[oo.text] = [];
                            }
                            o.optionsByName[oo.text].push(oo);
                        });
                    }

                }
            });

            // done intializing fields, call on load handler if any
            if ($scope.form.onLoad) {
                eval('( function(form) { var $scope = scope = this; try { ' + $scope.form.onLoad + '; } catch (e) { console.log(\'Error running Action Form on load script\', e); } } )')
                    .call($scope, $scope.form);
            }

            if ($scope.form.SaveInCookie) {
                var saveInCookiesTimer;
                function saveInCookies() {
                    $timeout.cancel(saveInCookiesTimer);
                    saveInCookiesTimer = $timeout(function () {

                        var saveData = getFormData($element.closest('.form-root'));

                        //Check if this affects multi file upload field
                        //var saveData = {};
                        //$.each($scope.form.fields, function (k, o) {
                        //    if (o.hasValue && o.type != 'upload.multi')
                        //        saveData[k] = o.value;
                        //});

                        $http({
                            method: 'POST',
                            url: $scope.form.submitUrl + "&event=autosave&submission=" + ($scope.form.submissionKey || ''),
                            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                            data: $.param(saveData)
                        }).success(function (data, status) {
                            $cookieStore.put($scope.form.SaveInCookie, data.submissionKey, { path: '/', expires: 365 });
                        });

                    }, 500);
                }

                $scope.$watch('form.fields', saveInCookies, true);
            }
            $scope.showLoader = false;
            formRoot.find('.c-form.hidden').removeClass('hidden');
        };

        var _find = function (v, callback) {
            var n = v.length;
            for (var i = 0; i < n; i++) {
                if (callback(v[i]))
                    return v[i];
            };
            return undefined;
        };

        $scope.concatValues = function (ctl) {
            if (!ctl.options)
                return;
            var vals = [];
            var texts = [];
            $.each(ctl.options, function (k, o) {
                if (o.selected && o.visible !== false) {
                    vals.push(o.value);
                    texts.push(o.text);
                }
            });
            ctl.value = vals;
            if (ctl.selectedItemsText)
                texts.length == 0 ? ctl.text = 'None selected' : ctl.text = texts.length + ' selected';
            else
                texts.length == 0 ? ctl.text = 'None selected' : ctl.text = texts.join(',');
        };

        // holds a list of region fields bound to each country
        $scope.countries = {};

        $scope.wireRegion = function (regionField, countryField) {
            if (!$scope.countries[countryField])
                $scope.countries[countryField] = { regionFields: [] };
            $scope.countries[countryField].regionFields.push(regionField);
            $scope.loadRegions(countryField, function () {

                // select current region by code or by name
                var val = $scope.form.fields[regionField].value;
                var regions = $scope.countries[countryField].regions;
                if (regions.length) {
                    for (var i = 0; i < regions.length; i++)
                        if (regions[i].value == val) {
                            //$scope.form.fields[regionField].selected = regions[i];
                            return;
                        }

                    // check by text
                    for (var i = 0; i < regions.length; i++)
                        if (regions[i].text == val) {
                            $scope.form.fields[regionField].ddValue = regions[i].value;
                            //$scope.form.fields[regionField].selected = regions[i];
                            return;
                        }
                }
            });
        }

        $scope.loadRegions = function (countryField, fnDone) {

            dnnsf.log('loadRegions', countryField, $scope.countries[countryField], $scope.form.fields[countryField]);
            // check if we actually have regions mapped to this country
            //if (!$scope.countries[countryField] || !$scope.form.fields[countryField])
            //    return;

            $scope.$watch('form.fields.' + countryField, function () {

                if (!$scope.countries[countryField] || !$scope.form.fields[countryField])
                    return;

                $scope.countries[countryField].loading = true;
                dataSources.get({
                    'DataSource': 'Regions',
                    'ListId': $scope.form.fields[countryField].value,
                    'JsonLowercaseNames': true
                }, function (data) {
                    $scope.countries[countryField].loading = false;
                    $scope.countries[countryField].regions = data;

                    if ($scope.countries[countryField].regions.length) {
                        // reset all textboxes values
                        $.each($scope.countries[countryField].regionFields, function (i, o) {
                            $scope.form.fields[o].countryField = countryField;
                            if ($scope.form.fields[o])
                                $scope.form.fields[o].tbValue = "";
                        });
                    } else {
                        // reset all dropdown values
                        $.each($scope.countries[countryField].regionFields, function (i, o) {
                            if ($scope.form.fields[o])
                                $scope.form.fields[o].ddValue = "";
                            $scope.form.fields[o].tbValue = "";
                        });
                    }
                    fnDone && fnDone();
                });
            }, true);

        };

        $scope.showSelected = function (node, selected, name) {
            if (selected) {
                $scope.form.fields[name].value = node.value;
                $scope.form.fields[name].text = node.text;
            } else {
                $scope.form.fields[name].value = "";
                $scope.form.fields[name].text = "";
            }
        }

        $scope.closeDropdown = function (e, name) {
            $scope.form.fields[name].showdrop = false;
            $scope.$apply();
        }

        $scope.uptStarRating = function (value, name) {
            $scope.form.fields[name].value = value;
        }

        $scope.updateField = function (field, val) { //used for dropdown with autocomplete and sortable-input
            $scope.form.fields[field].value = val;
            $scope.form.fields[field].onChange && $scope.form.fields[field].onChange($scope.form, field);
        }
    }
    ActionFormCtrl.$inject = ['$scope', '$http', '$timeout', '$sce', '$cookieStore', '$element', 'dataSources', 'dnnsf'];

    var initForm = function (options, fnDone) {
        var formRoot;
        options.apiUrl = '//' + options.alias + "/DesktopModules/DnnSharp/ActionForm/API";
        options.adminApiUrl = options.virtualDirectory + "/DesktopModules/DnnSharp/ActionForm/AdminApi.ashx";
        //openMode: Always, Page, Popup, Inline, Manual
        var $ = dnnsfjQuery;
        var queryString = $.extend({},
            dnnsf.getUrlParts(location.search).query,
            dnnsf['af-' + options.moduleId] && !$.isEmptyObject(dnnsf['af-' + options.moduleId].passQs) && dnnsf['af-' + options.moduleId].passQs,
            options.qs && !$.isEmptyObject(options.qs) && options.qs);
        dnnsf['af-' + options.moduleId] = { options: options };
        $('#' + options.rootElementClientId).addClass(options.cssName);
        $('#' + options.rootElementClientId).attr({ 'af-name': options.popupSettings.name, 'data-moduleid': options.moduleId }); //for openPopupByName()
        if (options.openMode != "Always" && !options.manualMode) {
            return function () {
                window['showFormPopup' + options.moduleId] = function () {
                    dnnsf.api.actionForm.openPopupById(options.moduleId);
                };
                window['hideFormPopup' + options.moduleId] = function () {
                    $('#dnn' + options.moduleId + 'popup').modal('hide')
                }
                window['showFormInline' + options.moduleId] = function () {
                    dnnsf.api.actionForm.initForm(options.moduleId);
                    showFormInline(options.moduleId, options.rootElementClientId);
                }
                window['hideFormInline' + options.moduleId] = function () {
                    hideFormInline(options.moduleId, options.rootElementClientId);
                }
                if (!options.manualMode && options.openMode != "Always" && options.openMode != "Manual")
                    $('#' + options.rootElementClientId).html($('<div class="frontEndTemplate"></div>').html(options.frontEndTemplate));
            }()
        } else {
            setTimeout(function () {
                options.showLoading && $('#' + options.rootElementClientId + ' > .common-loading-container').show();
                options.tabsProLoading && dnnsf.events.broadcast('loadForm', { 'loading': true, moduleId: options.moduleId });
            }, 0);

            (!options.manualMode && options.openMode != "Always") &&
                $('#' + options.rootElementClientId).html($('<div class="frontEndTemplate"></div>').html(options.frontEndTemplate));
        }
        // ^^backward compatibility for showFormPopup, hideFormPopup, showFormInline, hideFormInline functions^^
        // getSettings
        $.ajax({
            url: options.apiUrl + '/settings/initializeForm?' +
            '_mid=' + options.moduleId +
            '&_tabId=' + options.tabId +
            '&tabId=' + options.tabId +
            '&_portalId=' + options.portalId +
            '&_alias=' + options.alias +
            '&openMode=' + (options.openMode == "Manual" ? options.manualMode : options.openMode) +
            (!$.isEmptyObject(queryString) ? ('&' + $.param(queryString)) : '') +
            (options.dnnPageQuery ? ('&' + options.dnnPageQuery) : '') +
            '&_url=' + encodeURIComponent(document.URL)
            ,
            type: "post",
            data: options.requestForm,
            async: true,
            success: function (res) {
                parseFormResponse(res.ActionResult, {
                    error: function (err) {
                        options.hasErrors = true;
                        var pnlMessage = $('<div class="alert alert-danger"></div>').html(res.ActionResult.Error);
                        $('#' + options.rootElementClientId).html($('<div class="frontEndTemplate"></div>').append(pnlMessage));
                    }
                });
                //if (res.ActionResult.error) {
                //    options.hasErrors = true;
                //    var pnlMessage = $('<div class="alert alert-danger"></div>').html(res.ActionResult.error);
                //    $('#' + options.rootElementClientId).html($('<div class="frontEndTemplate"></div>').append(pnlMessage));
                //    return;
                //};
                dnnsf['af-' + options.moduleId] = $.extend(dnnsf['af-' + options.moduleId], res)
                if (!$.isEmptyObject(res.ActionResult) && res.ActionResult.Content) {
                    formRoot = $(res.ActionResult.Content);
                    var pnlMessage = $('<div class="alert alert-info"></div>').append(formRoot);
                    $('#' + options.rootElementClientId).append(pnlMessage);
                } else {
                    $('#' + options.rootElementClientId).find('#dnn' + options.moduleId + 'root').length ?
                        $('#dnn' + options.moduleId + 'root').replaceWith(res.Html) :
                        $('#' + options.rootElementClientId).append($.parseHTML(res.Html));
                    dnnsf.loadJsFromHtml(res.Html);
                    formRoot = $('#dnn' + options.moduleId + 'root');
                }
                initFormController();
                fnDone && fnDone();
            },
            error: function (err) {
                console.error(this.url, err)
                options.hasErrors = true;
                var pnlMessage = $('<div class="alert alert-info"></div>').html(err.responseText);
                $('#' + options.rootElementClientId).html($('<div class="frontEndTemplate"></div>').append(pnlMessage));
            }
        });

        function initFormController() {

            if (options.hasErrors) return;
            if (!formRoot.length || formRoot[0].initialized)
                return;

            formRoot[0].onFormSubmit = formRoot[0].onFormSubmit || [];
            formRoot[0].initialized = true;

            // init common features
            dnnsf.init($, options);
            dnnsf.localization = localization = dnnsf['af-' + options.moduleId].Localization;
            // call localization inside fileupload-validate
            dnnsf.useLocalization && dnnsf.useLocalization();

            var app = angular.module('ActionForm' + formRoot.attr('id'), ['ngAnimate', 'siyfion.sfTypeahead', 'bootstrap-tagsinput', 'ngSanitize', 'dnnsf', 'afControls', 'ui.bootstrap', 'ui.bootstrap.datetimepicker', 'angucomplete', 'treeControl', 'ngstars', 'loadOnDemand']);

            app.config(['$loadOnDemandProvider', function ($loadOnDemandProvider) {
                var modules = [
                    { name: 'barcode' },
                    { name: 'sortable' },
                    { name: 'slider' },
                    { name: 'progressbar' },
                    { name: 'beefree' },
                    { name: 'trumbowyg' }
                ];
                $loadOnDemandProvider.config(modules);
            }]);

            app.controller('ActionFormCtrl', ActionFormCtrl);

            app.directive('hasRepeaters', [function () {
                return {
                    restrict: 'A',
                    priority: Number.MIN_SAFE_INTEGER,
                    scope: false,
                    link: function (scope) {
                        setTimeout(function () {
                            if (!scope.$root.repeaters) {
                                options.showLoading && $('#' + options.rootElementClientId + ' > .common-loading-container').hide();
                                options.tabsProLoading && dnnsf.events.broadcast('loadForm', { 'loading': false, moduleId: scope.settings.ModuleId });
                            }
                        }, 0)
                    }
                }
            }])

            app.directive('repeatDone', [function () {
                return {
                    restrict: 'AE',
                    scope: false,
                    link: {
                        pre: function (scope, element, attrs) {
                            scope.$root.repeaters = true;
                        },
                        post: function (scope, element, attrs) {
                            if (scope.$last) { // all are rendered
                                options.showLoading && $('#' + options.rootElementClientId + ' > .common-loading-container').hide();
                                options.tabsProLoading && dnnsf.events.broadcast('loadForm', { 'loading': false, moduleId: scope.settings.ModuleId });
                            }
                        }
                    }
                }
            }]);

            app.factory('dataSources', ['$http', 'dnnsf', function ($http, dnnsf) {
                return {
                    get: function (settings, fnReady) {
                        var p = $.extend({}, settings, { Method: 'GetData', tabId: options.tabId, mid: options.moduleId, alias: options.alias });
                        $http({
                            method: 'GET',
                            url: options.adminApiUrl + '?' + $.param(p),
                            cache: true
                        }).success(function (data, status) {
                            fnReady && fnReady(data);
                        });
                    }
                };
            }]);


            // the default cookieStore does not support path or expiration
            app.provider('$cookieStore', [function () {
                var self = this;
                self.defaultOptions = {};

                self.setDefaultOptions = function (options) {
                    self.defaultOptions = options;
                };

                self.$get = function () {
                    return {
                        get: function (name) {
                            var jsonCookie = $.cookie(name);
                            if (jsonCookie) {
                                return angular.fromJson(jsonCookie);
                            }
                        },
                        put: function (name, value, options) {
                            options = $.extend({}, self.defaultOptions, options);
                            $.cookie(name, angular.toJson(value), options);
                        },
                        remove: function (name, options) {
                            options = $.extend({}, self.defaultOptions, options);
                            $.removeCookie(name, options);
                        }
                    };
                };
            }]);

            app.directive('dropdownWatch', ['$interval', function ($interval) { // resize "dropdown with checkboxes" based on content width and height
                return {
                    restrict: 'A',
                    scope: {
                        dropdownName: '=',
                        disableCheckboxes: '@' // don't change this to '=' because then it won't be treated as a string
                    },
                    controller: function ($scope, $element) {
                        $scope.dropdownName.show = false;
                        var dropdownPanel = $element.parent().parent().next().children('.panel');
                        var disabled = $element[0].disabled;

                        if (disabled && $scope.disableCheckboxes === "True") {
                            $element.prop("disabled", false);
                        }
                        $scope.$watch("dropdownName.show", function (newVal, oldVal) {
                            if (!newVal)
                                return;
                            if (disabled && $scope.disableCheckboxes === "True") {
                                dropdownPanel.find('a')
                                    .parent().addClass("dnnsf-disabled-checkboxes");//  disable 'select all' and 'clear all'

                                dropdownPanel.find('.normalCheckBox').each(function () {
                                    $(this).prop("disabled", true).addClass("disabled")
                                        .parent().addClass("not-allowed");
                                });
                            }

                            var dropdownOpen = $interval(function () {
                                if (dropdownPanel.width() > 0) {
                                    resizeDropdown(dropdownPanel, $scope.dropdownName);
                                    $interval.cancel(dropdownOpen);
                                }
                            }, 50); //interval
                        });

                        function resizeDropdown(dropdownPanel, dropdownName) {
                            if (dropdownPanel.attr('data-window-width') == $(window).width())
                                return;
                            if (dropdownName.show) {
                                dropdownPanel.css('opacity', '0');
                                if (dropdownPanel.attr('data-window-width')) {
                                    dropdownPanel.width(dropdownPanel.width() - 15); // this eliminates endless adding of padding when you resize the window
                                    dropdownPanel.attr('style', '');
                                }
                                dropdownPanel.css('display', 'table');
                                var inputWidth = dropdownPanel.parent().prev().children().first().width();
                                var contentWidth = dropdownPanel.width() + 15; // magic number for padding
                                var rect = dropdownPanel[0].getBoundingClientRect();
                                var windowWidth = $(window).width();

                                dropdownPanel.attr('data-window-width', windowWidth);
                                dropdownPanel.css('display', 'inline-block'); // i took the width from display:table 
                                // and make it inline-block with that width
                                if (inputWidth > contentWidth) { // if the input is bigger than the dropdown panel make the panel input-sized
                                    dropdownPanel.width(inputWidth);
                                    dropdownPanel.parent().width(inputWidth);
                                    dropdownPanel.css('opacity', '1');
                                }
                                else {
                                    if (rect.right > windowWidth) { // if the dropdown is bigger than the window
                                        contentWidth = contentWidth - (rect.right - windowWidth) - 15; // magic number for padding
                                        dropdownPanel.css('overflow-x', 'scroll');
                                    }
                                    dropdownPanel.width(contentWidth);
                                    dropdownPanel.parent().width(contentWidth);
                                    dropdownPanel.css('opacity', '1');
                                }
                            }// show
                        }; //resize
                    } //controller
                }; //directive return
            }]); //directive

            app.directive('maxNumber', [function () {
                return {
                    restrict: 'A',
                    require: 'ngModel',
                    link: function (scope, element, attrs, ngModel) {
                        if (typeof parseFloat(attrs.maxNumber) == 'number') {
                            scope.$watch(attrs.ngModel, function (newVal, oldVal) {
                                if (newVal > parseFloat(attrs.maxNumber)) {
                                    ngModel.$setViewValue(parseFloat(attrs.maxNumber));
                                    ngModel.$render();
                                    $(element).closest('.form-group').addClass('has-error');
                                    setTimeout(function () {
                                        $(element).closest('.form-group').removeClass('has-error');
                                    }, 1000);
                                }
                            })
                        }
                    }
                }
            }]);
            // wrapper for masked input
            app.directive('inputMask', function () {
                return {
                    restrict: 'A',
                    link: function (scope, el, attrs) {
                        delete $.jMaskGlobals.translation['#'];
                        $(el).mask(attrs.inputMask, {
                            translation: {
                                '9': {
                                    pattern: /\d/, optional: true
                                },
                                'a': {
                                    pattern: /[a-zA-Z]/
                                },
                                '*': {
                                    pattern: /[0-9a-zA-Z]/
                                }
                            },
                            placeholder: attrs.inputMask.replace(/[0-9a-zA-Z]/g, '_')
                        });

                        el.on('keyup blur', function () {
                            el.trigger('change');
                        });
                    }
                };
            });

            // wrapper for masked input
            app.directive('onblur', function () {
                return {
                    restrict: 'A',
                    scope: {
                        onblur: '&'
                    },
                    link: function (scope, el, attrs) {
                        $(el).parents('.element-area:first').click(function (e) {
                            e.stopPropagation();
                        });
                        $(document).click(function () {
                            scope.onblur();
                            scope.$apply();
                        });
                    }
                };
            });

            // DOM wathcher
            app.directive('domWatch', function () {
                return {
                    restrict: 'A',
                    link: function (scope, el, attrs) {

                        $(el).on(attrs.domWatch, function () {
                            scope.$eval(attrs.ngModel + "='" + el.val() + "'");
                            scope.$apply();
                        });
                    }
                };
            });

            // this directive knows to bind a value to a html control, but only when this value exists
            app.directive('afBindvalue', ['$compile', '$timeout', '$parse', function ($compile, $timeout, $parse) {
                return {
                    restrict: 'A',
                    scope: false,
                    require: 'ngModel',
                    link: function (scope, element, attrs, ngModel) {

                        var options = null;
                        if (attrs.afBindfrom)
                            options = scope.$eval(attrs.afBindfrom);

                        // if it's not an input element, define a new render function
                        if (element.filter(':input').length == 0) {
                            ngModel.$render = function () {
                                if (ngModel.$viewValue === undefined || ngModel.$viewValue === null)
                                    return;
                                if (!element.hasClass('model-only'))
                                    element.html(ngModel.$viewValue);
                            };
                        }

                        scope.$watch(attrs.afBindvalue, function (value) {

                            if (!scope.form.fields || !scope.form.fields[attrs.afField])
                                return;

                            var field = scope.form.fields[attrs.afField];
                            if (field.touched)
                                return;

                            if (attrs.afBindfrom) {
                                var options = scope.$eval(attrs.afBindfrom);
                                if (!options)
                                    return;
                                var optionsFound = $.grep(options, function (o) { return o.value === value });
                                if (optionsFound.length > 0) {
                                    ngModel.$setViewValue(optionsFound[0]);
                                };
                            }
                            else {
                                ngModel.$setViewValue(value);
                            }
                            ngModel.$render();
                        });
                    }
                };
            }]);

            // initialize rich edits
            app.directive('afRichedit', ['$compile', '$timeout', '$parse', function ($compile, $timeout, $parse) {
                return {
                    require: 'ngModel',
                    link: function (scope, elm, attrs, ngModel) {
                        var fnInitRichEdit = function () {

                            if (!$(elm).is(':visible')) {
                                $timeout(fnInitRichEdit, 200);
                                return;
                            }

                            $(elm).wysiwyg({
                                autoGrow: false,
                                maxHeight: 600,
                                initialMinHeight: 50,
                                initialContent: '',
                                brIE: false,
                                replaceDivWithP: true,
                                events: {
                                    save: function () {
                                        try {
                                            ngModel.$setViewValue(this.getContent());
                                        } catch (e) {
                                        }
                                    }
                                }
                            });

                            // localize wysiwyg
                            $('.wysiwyg [role="menuitem"]').each(function () {
                                var l = localization['wysiwyg.' + $(this).attr('class')];
                                l && $(this).attr('title', l);
                            });


                            ngModel.$render = function () {
                                $(elm).wysiwyg('setContent', ngModel.$viewValue || '');
                            };
                            $(elm).width('100%');
                        };

                        $timeout(fnInitRichEdit, 100);

                    }
                };
            }]);

            // dnnsfAngularLock is just a quick hack to skip angualr initialization in some situations
            !window.dnnsfAngularLock && angular.bootstrap(formRoot, ['ActionForm' + formRoot.attr('id')]);
            var $_scope = angular.element(formRoot).scope();
            //formRoot[0].onFormSubmit = $_scope._registerToEvent['submit'];

            // --------------------------------------------------------------------------------------------------------------------------------
            // Start jquery plugin e
            $.fn.popover && formRoot.find('span.popupOnHover').popover({ trigger: 'hover' });

            // setup validation plugin
            formRoot.validate && formRoot.validate({
                errorElement: 'span',
                errorClass: 'text-danger',
                highlight: function (element, errorClass) {
                    $(element).parents('.field-container:first').addClass('has-error');
                },
                unhighlight: function (element, errorClass) {
                    var fieldsGroup = element.attributes['class'] ? element.attributes['class'].value.match(/(group\d+-onerequired\d+)/) : '',
                        onerequired = fieldsGroup ? $(element).closest('.form-root').find('.form-group .' + fieldsGroup[0] + ':not(.required)') : '';
                    if (onerequired.length && !$(element).hasClass('required')) {
                        onerequired.parent().removeClass('has-error');
                        $(element).next('.text-danger').hide();
                    }
                    else $(element).parents('.field-container:first').removeClass('has-error').find('.text-danger').hide();
                },
                errorPlacement: function (error, element) {
                    var errPlace = element.find('.err-placeholder');
                    if (!errPlace.size())
                        errPlace = element.closest('.field-container').find('.err-placeholder');

                    if (errPlace.size()) {
                        if (errPlace.find('span.text-danger').text() != error.text())
                            errPlace.append(error);
                    } else {
                        if (element.is(':checkbox') || element.is(':radio')) {
                            element.parent().append(error);
                        } else {
                            element.next().is('.text-danger') ? element.next().replaceWith(error) : error.insertAfter(element.filter(function () {
                                return !element.closest('.field-container').hasClass('ng-hide')
                            }));
                        }
                        var tabParent = element.closest('.tab-pane');
                        tabParent.length && !tabParent.hasClass('active') && $('[href="#' + tabParent.attr('id') + '"]').addClass('has-error');
                    }

                },
                ignore: '.ignore,:hidden,:disabled',
                onkeyup: function (element) { $(element).valid(); }// for 'true' you need a function, 'false' works
            });

            // validate required upload files
            $.validator && $.validator.addMethod("required-file", function (value, element) {
                return $(element).hasClass('has-file');
            });

            // validate required upload files
            $.validator && $.validator.addMethod("required-fromclass", function (value, element) {
                return $(element).hasClass('afvalid');
            });

            $.validator && $.validator.addMethod("required-cblist", function (value, element) {
                var group = $(element).attr('data-validation-group');
                var valid = false;
                $('[data-validation-group="' + group + '"]').each(function () {
                    if (this.checked)
                        valid = true;
                })
                return valid;
            });

            $.validator && $.validator.addMethod("required-ddwithcb", function (value, element) {
                var cboxes = $(element).closest('.field-container').find(':checkbox');
                var valid = false;
                $.each(cboxes, function (i, v) {
                    if ($(v).is(':checked'))
                        valid = true;
                });
                return valid;
            });

            // initialize password confirm
            formRoot.find('[data-password-confirm]').each(function () {
                $(this).rules("add", {
                    equalTo: '#' + $(this).attr('data-password-confirm'),
                    messages: {
                        equalTo: localization['validation.passwordNoMatch']
                    }
                });
            });

            // fix width when printing
            if (formRoot.closest('#Table1').length) {
                formRoot.closest('#Table1').addClass('container')
                    .parent().addClass(formRoot.attr('data-rootclass'));
                $('body').addClass('bstrap30 bstrap3-material');
            }

            //fix for when in pop-up
            if (formRoot.closest('.container').length == 0) {
                formRoot.closest('.phFormTemplate').addClass('container');
            }

            function parseVar(strVar) {
                if (!isNaN(parseInt(strVar)))
                    return parseInt(strVar);

                if (!isNaN(parseFloat(strVar)))
                    return parseFloat(strVar);

                if (strVar[0] == '[') {
                    // looks like an array
                    return eval(strVar.replace('\n', ''));
                }

                if (strVar == "false")
                    return false;

                if (strVar == "true")
                    return true;

                // just return it as string
                return strVar;
            }


            // load localized error messages
            for (var key in localization) {
                if (key.indexOf('validation.') == 0) {
                    var relKey = key.substr('validation.'.length);
                    if ($.validator)
                        $.validator.messages[relKey] = localization[key].indexOf('{0}') == -1 ? localization[key] : $.validator.format(localization[key]);
                } else if (key.indexOf('jquery.datepicker.') == 0 && $.datepicker) {
                    var relKey = key.substr('jquery.datepicker.'.length);
                    var s = {};
                    s[relKey] = parseVar(localization[key]);
                    $.datepicker.setDefaults(s);
                }
            }

            //// load strings from the server
            //$.getJSON(formRoot.attr("")

            // fix for datepicker, onchange does not trigger validation, so call onkeyup manually
            formRoot.find('.datepicker').change(function () {
                $(this).keyup();
            });

            $('.modal').on('shown.bs.modal', function () {
                var x = 0;

                var checkModals = setInterval(function () {
                    $('.modal:visible').each(function () {
                        var popup = $(this);
                        popup.find('.modal-dialog:first').css('z-index', popup.find('.modal-backdrop:first').css('z-index') + 1);
                        popup.after(popup.find('.modal-dialog:first').siblings('.modal-backdrop'));
                    });
                    if (++x === 5) {
                        window.clearInterval(checkModals);
                    }
                }, 1000);
            });

            //// initialize input masks
            //formRoot.find('[data-mask]').each(function () {
            //    $(this).mask($(this).attr('data-mask'));
            //});


            // intialize date pickers
            formRoot.find(".datepicker").each(function () {
                var opts = {
                    dateFormat: $(this).attr('data-dateformat'),
                    changeMonth: $(this).attr('data-changemonth') == 'true',
                    changeYear: $(this).attr('data-changeyear') == 'true',
                    //Fix of IE
                    fixFocusIE: false,
                    onSelect: function (dateText, inst) {
                        this.fixFocusIE = true;
                    },
                    onClose: function (dateText, inst) {
                        this.fixFocusIE = true;
                        inst.input.trigger('change');
                    },
                    beforeShow: function (input, inst) {
                        var result = true;
                        this.fixFocusIE = false;
                        return result;
                    }
                };

                if ($(this).attr('data-yearrange'))
                    opts["yearRange"] = $(this).attr('data-yearrange');


                // merge other options
                if ($(this).attr('data-opts')) {
                    opts = $.extend(opts, eval('(' + $(this).attr('data-opts') + ')'));
                }

                $(this).datepicker(opts);
                var theme = $(this).attr('data-theme');
                $('#ui-datepicker-div').each(function () {
                    if ($(this).parent("." + theme).size() == 0)
                        $(this).wrap('<div class="' + theme + '"></div>');
                });
            });

            //colorpicker to .colorpickertext class to add colorpicker to text 
            formRoot.find(".colorpickertext").each(function () {
                $(this).ColorPicker({
                    onSubmit: function (hsb, hex, rgb, el) {
                        $(el).val('#' + hex);
                        $(el).ColorPickerHide();
                    },
                    onBeforeShow: function () {
                        $(this).ColorPickerSetColor(this.value);
                    },
                    onHide: function (hsb) {
                        $('#' + hsb.id + ' > .colorpicker_submit').trigger('click');
                    }
                });
            });


            // initialize autocomplete
            formRoot.find('[data-autocomplete]').each(function () {
                $(this).devbridgeAutocomplete({
                    paramName: 'query',
                    serviceUrl: $(this).attr('data-autocomplete'),
                    transformResult: function (response) {
                        return {
                            suggestions: $.map($.parseJSON(response), function (dataItem) {
                                return { value: dataItem.Text, data: dataItem.Value };
                            })
                        };
                    },
                    onSelect: function (suggestion) {
                        $_scope.form.fields[$(this).attr('data-af-field')].value = suggestion.value;
                        $_scope.form.fields[$(this).attr('data-af-field')].data = suggestion.data;
                        $_scope.$apply();
                    }
                });
            });

            // init file upload
            formRoot.find('.file-upload').each(function () {
                this["aform"] = formRoot;
            });

            if (window.aform_incFileUplad) {
                if (!$().fileupload)
                    return;

                formRoot.find('.file-upload').each(function () {

                    if (!this.aform)
                        return; // not one of our fields

                    var $root = $(this).parents('.fileupload-root:first');

                    // redefined formRoot in this context
                    var formRoot = this.aform;
                    var _this = $(this);
                    $root.find('.files').empty().append($('<p/>').text(angular.element(formRoot).scope().form.fields[_this.attr('data-af-field')].value));

                    // hack for DNN 7 to leave our upload field alone
                    var btn = $root.find('.fileinput-button');
                    if (btn.find('.dnnInputFileWrapper').size() > 0) {
                        btn.find('input').appendTo(btn);
                        btn.find('.dnnInputFileWrapper').remove();
                    } else {
                        if (btn.find('input')[0])
                            btn.find('input')[0].wrapper = 'hack';
                    }

                })

                // once is enough
                window.aform_incFileUplad = false;
            }

            //This is a fix for Xcilion skin for stoping changing the portal on EnterKey
            $(document).on('keydown', 'input:text.preventdefault', function (evt) {
                if (evt.keyCode == 13) {
                    evt.preventDefault();
                    evt.stopImmediatePropagation();
                }
            });

            $('body').on('keydown', 'input:text:not(.preventdefault),input:password:not(.preventdefault)', function (evt) {
                var btn = $(evt.currentTarget).closest('.form-root').find('.submit[data-default-button=on]:first');
                if (evt.keyCode == 13 && btn.length) {
                    btn.click();
                    evt.preventDefault();
                }
            });

            // reset buttons, sometimes firefox leaves them disabled after refresh
            try { formRoot.find('.button').button('reset'); } catch (e) { }

            formRoot.on('click', ".form-button", function () {
                submitForm(this);
            });

            function submitForm(el, fnDone, qs) {
                var connectedForms = {};
                var btnSettings = $_scope.settings.Fields[$(el).attr('data-name')];
                if (!btnSettings) { //submit from tabsPro event
                    btnSettings = {
                        'isConnected': false
                    }
                }
                else {
                    var tokenizedConnectedForms = $_scope.form.fields[btnSettings.TitleCompacted].connectedForms;
                    btnSettings.isConnected = tokenizedConnectedForms && tokenizedConnectedForms.length > 0;
                    btnSettings.isConnected && (connectedForms = tokenizedConnectedForms);
                }
                var _this = el;
                // reset
                formRoot.find(".server-error").html("").hide();
                var causesValidation = $(_this).attr('data-validation') == 'on';
                var fieldsToValidate = formRoot.find('input,textarea,select,.checkbox-list').not(':hidden,:disabled,.ignore');
                $.each(connectedForms, function (i, v) {
                    var formControls = {};
                    formControls.fields = angular.element('#dnn' + v.FormId + 'root').scope().controls;
                    if (formControls.fields.length)
                        $_scope.controls = _.uniqBy($_scope.controls.concat(formControls || []), 'field')
                    // formControls.fields.length && $_scope.controls.push(formControls);
                    var fields = $('#dnn' + v.FormId + 'root').find('.field-container').not('.ng-hide').find('input,textarea,select,.checkbox-list').not(':disabled,.ignore');
                    fields.each(function (i, v) {
                        fieldsToValidate.push(v);
                    })
                });
                if (causesValidation) {
                    fieldsToValidate.each(function (index, input) {
                        if (!$(input).attr('keyup-listener')) { // we don't need duplicate listeners
                            $(input).attr('keyup-listener', 'true')
                            input.addEventListener("keyup", function () { // add an event listener because the plugin doesn't work as it should
                                fieldsToValidate.valid(); // after submitting we want validation on keyup
                            });
                        }
                    });
                }
                if (causesValidation && fieldsToValidate.size() && !fieldsToValidate.valid()) {
                    formRoot.find('.has-error:first').find('input,textarea,select').focus();
                    fnDone && fnDone({ value: false, refresh: angular.element(formRoot).scope().settings.TabsPro_RefreshTabStateOnLeave.Value });
                    return false;
                }

                if (_this.preventSubmit)
                    return;
                // check if we need to start uploads
                //formRoot[0].toUpload = 0;
                formRoot[0].$btn = $(_this);
                if (qs && !$.isEmptyObject(qs)) {
                    formRoot[0].qs = qs;
                }
                formRoot.bind('fileuploadsubmit', function (e, data) {
                    // Sending the all form fields in fileUpload request
                    var currentData = getFormData(formRoot);
                    data.formData = currentData;
                });

                var $btn = $(_this);
                $($btn).data('connectedForms', connectedForms);
                try { $btn.hasClass('af-btn-loading') && $btn.button('loading'); } catch (e) { }
                var btns = formRoot.find('.submit').not($btn);
                $.each(btns, function (i, v) {
                    $(v).attr('disabled') && $(v).data('disabled', true);
                    !$btn.data().tabEvent && $(v).attr('disabled', 'disabled');
                })
                $btn.data().tabEvent && $btn.data('tabEvent', false);
                //formRoot.find('.submit').not($btn).attr('disabled', 'disabled');
                formRoot.find('.submit-progress').css('visibility', 'visible').stop(true, true).fadeIn();

                var abortSubmit = false;
                var waitFor = 0;
                $.each($_scope.controls, function (i, control) {
                    if (control.fields && control.fields.length) {
                        $.each(control.fields, function (i, v) {
                            if (v.onSubmit)
                                waitFor++;
                        })
                    }
                    else {
                        if (control.onSubmit)
                            waitFor++;
                    }
                });

                if (formRoot[0].onFormSubmit.length)
                    for (var i in formRoot[0].onFormSubmit)
                        formRoot[0].onFormSubmit[i]($btn);
                if (!waitFor) {
                    formRoot[0].submitData($btn, fnDone, btnSettings);
                }
                else if (waitFor && (!formRoot.find(".table-striped.files tr.file-table").length && !formRoot.find("[submit-data]").length && !btnSettings.isConnected)) {
                    //sending with no file uploaded
                    return formRoot[0].submitData($btn, fnDone, btnSettings);
                } else {
                    var submitControl = function (control) {
                        if (!control.onSubmit)
                            return
                        control.onSubmit(function () {
                            if (abortSubmit)
                                return;

                            waitFor--;
                            if (waitFor == 0)
                                formRoot[0].submitData($btn, fnDone, btnSettings);
                        }, function (error) {
                            if (abortSubmit)
                                return;
                            formRoot.find(".server-error").html(error).show();
                            afResetButton(formRoot, $btn);
                            abortSubmit = true;
                        });
                    }

                    $.each($_scope.controls, function (i, control) {
                        if (control.fields && control.fields.length) {
                            $.each(control.fields, function (i, v) {
                                submitControl(v);
                            })
                        }
                        else
                            submitControl(control);
                    });
                }
            }

            formRoot[0].submitData = function ($btn, fnDone, btnSettings) {
                isConnected = btnSettings.isConnected;
                if (formRoot[0].qs && !$.isEmptyObject(formRoot[0].qs)) {
                    var submitUrl = dnnsf.getUrlParts($btn.attr('data-submiturl'))
                    submitUrl.query = $.extend(submitUrl.query, formRoot[0].qs);
                    $btn.attr('data-submiturl', submitUrl.getUrl(submitUrl));
                }
                var list = [],
                    deleteUrl = "";

                if (formRoot[0].submitting)
                    return;

                //getting data for submit
                var data = getFormData(formRoot);
                if (isConnected) {
                    data = { '$_thisForm': data };
                    $.each($($btn).data('connectedForms'), function (i, v) {
                        var fields = {}
                        fields[v.FormName] = getFormData($('#dnn' + v.FormId + 'root'));
                        $.extend(data, fields)
                    });
                }

                //delete files
                $.each($_scope.controls, function (i, control) {
                    if (!control.deleteFiles)
                        return;
                    list = control.deleteFiles();
                    deleteUrl = control.deleteUrl();

                    if (list) {
                        $.each(list, function (i, filename) {
                            $.ajax({
                                url: deleteUrl + '&f=' + filename,
                                type: "post",
                                data: list,
                                dataType: "json"
                            }).done(function (data) {
                                formRoot[0].submitting = false;
                                parseFormResponse2(formRoot, $btn, data);
                            });
                        });
                    }
                });

                var submitFormData = function (onDone) {
                    formRoot[0].submitting = true;
                    var xhr = new XMLHttpRequest();
                    var executed = [];
                    var executedOnDone = false;
                    if (isConnected) {
                        xhr.open("POST", options.apiUrl + "/MultiForm/Submit?" + $btn.attr("data-submitquery") + '&referrer=' + encodeURIComponent(document.referrer) + '&_url=' + encodeURIComponent(document.URL), true);
                        xhr.setRequestHeader("Content-type", "application/json");
                    } else {
                        xhr.open("POST", $btn.attr("data-submiturl") + '&referrer=' + encodeURIComponent(document.referrer) + '&_url=' + encodeURIComponent(document.URL), true);
                        xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
                    }

                    xhr.onprogress = function () {
                        var response = xhr.response.split("\n");
                        if (response[response.length - 1] == "")
                            response.pop();

                        $.each(response, function (i, v) {
                            if (executed.indexOf(i) != -1)
                                return;

                            try {
                                var responseObj = JSON.parse(v);
                            } catch (e) {
                                // json is incomplete. The next flush will get the rest of it.
                                return;
                            }

                            if (isConnected && responseObj.validationErrors) {
                                responseObj.connectedForms = $($btn).data('connectedForms');
                            }

                            parseFormResponse2(formRoot, $btn, responseObj);
                            if (onDone && responseObj.validationErrors) {
                                onDone(responseObj);
                                executedOnDone = true;
                            }
                            executed.push(i);
                        })
                    }
                    xhr.onreadystatechange = function () {
                        formRoot[0].submitting = false;
                        var response = xhr.response.split("\n");
                        if (xhr.readyState == 4) {
                            if (onDone) {
                                response = _.filter(response, function (value, key) {
                                    return executed.indexOf(key) == -1;
                                });
                                !executedOnDone && onDone(response.length && JSON.parse(response[0]));
                            }
                        }
                    }
                    if (isConnected)
                        xhr.send(JSON.stringify(data));
                    else
                        xhr.send($.param(data));
                }

                var event = dnnsf.getUrlParts($btn.attr("data-submiturl")).query['event'];
                switch (event) {
                    //case 'click':
                    //break;
                    //case 'TabsPro_OnTabEnter':
                    //break;
                    case 'TabsPro_OnTabLeave':
                        submitFormData(function (data) {
                            if (data && data.validationErrors != undefined) {
                                fnDone({ value: false, refresh: angular.element(formRoot).scope().settings.TabsPro_RefreshTabStateOnLeave.Value });
                            } else {
                                if (!angular.element(formRoot).scope()) {
                                    var waitForInitialization = setInterval(function () {
                                        if (angular.element(formRoot.selector).scope()) {
                                            fnDone({ value: true, refresh: angular.element(formRoot.selector).scope().settings.TabsPro_RefreshTabStateOnLeave.Value });
                                            clearInterval(waitForInitialization);
                                        }
                                    }, 5)
                                } else
                                    fnDone({ value: true, refresh: angular.element(formRoot).scope().settings.TabsPro_RefreshTabStateOnLeave.Value });

                            }
                        })
                        break;
                    default:
                        submitFormData();
                }

            };

            dnnsf.events.listen('OnTabsLeave', options.moduleId, function (data, fnDone) {
                var isTarget = false;
                $.each(data.targetModuleList, function (index, moduleId) {
                    if (options.moduleId == moduleId)
                        isTarget = true;
                })
                if (isTarget) {
                    var submitUrl = dnnsf.getUrlParts($_scope.form.submitUrl)
                    submitUrl.query = $.extend(submitUrl.query, { 'event': 'TabsPro_OnTabLeave' });
                    var btn = document.createElement("button");
                    //formRoot.append(btn);
                    $(btn).attr('class', 'form-button');
                    $(btn).attr('data-submiturl', submitUrl.getUrl(submitUrl));
                    if ($_scope.settings.TabsPro_IgnoreValidationOnLeave.Value) {
                        $(btn).attr('data-validation', 'off');
                    } else {
                        $(btn).attr('data-validation', 'on');
                    }
                    $(btn).data('tabEvent', true);
                    //$(btn).remove();
                    submitForm(btn, fnDone);
                } else {
                    fnDone();
                }
            });

            dnnsf.events.listen('OnTabsEnter', options.moduleId, function (data, fnDone) {
                var newQs = { 'event': 'TabsPro_OnTabEnter' };
                if (data.qs && !$.isEmptyObject(data.qs)) {
                    $.extend(newQs, data.qs);
                    dnnsf['af-' + options.moduleId].passQs = data.qs;
                }
                var isTarget = false;
                $.each(data.targetModuleList, function (index, moduleId) {
                    if (options.moduleId == moduleId)
                        isTarget = true;
                })
                if (isTarget) {
                    var submitUrl = dnnsf.getUrlParts($_scope.form.submitUrl)
                    submitUrl.query = $.extend(submitUrl.query, newQs);
                    var btn = document.createElement("button");
                    $(btn).attr('class', 'form-button');
                    $(btn).attr('data-submiturl', submitUrl.getUrl(submitUrl));
                    $(btn).attr('data-validation', 'off');
                    $(btn).data('tabEvent', true);
                    submitForm(btn, fnDone, data.qs);
                } else {
                    fnDone();
                }
            });

            dnnsf.events.listen('ActionFormPing', options.moduleId, function (data, fnDone) {
                $.each(data.targetModuleList, function (index, moduleId) {
                    if (options.moduleId == moduleId) {
                        fnDone(options.moduleId);
                    } else {
                        fnDone();
                    }
                })
            });
        };
    }

    function getFormData(formRoot) {
        var $_scope = angular.element(formRoot).scope();
        var data = {};
        formRoot.find(':input,[data-val],.value-node').each(function () {
            //formRoot.find('input').filter(':text,:password,:hidden').add(formRoot.find("select,textarea")).each(function () {
            if (!$(this).attr("name") || $(this).closest('.ng-hide').length)
                return;

            // initialize to empty
            var name = $(this).attr("name").replace(/dnn\d+/, "");
            if (!data[name])
                data[name] = '';

            // for radios, only set if selected
            if ($(this).attr('type') == 'radio') {
                if (this.checked)
                    data[name] = $(this).val();
            } else if ($(this).attr('data-val') || $(this).attr('data-val') === '') {
                data[name] = $(this).attr('data-val');
            }
            else if ($(this).hasClass('value-node')) {
                data[name] = $(this).html();
            } else {
                data[name] = $(this).val();
            }

        });

        var checkboxes = _.map(formRoot.find('[type="checkbox"][name]:visible:not([name=""])'), function (x) { return [$(x).attr('name').replace(/dnn\d+/, ''), $(x)] });
        var part_checkboxes = _.partition(checkboxes, function (x) { return x[0].indexOf('-') == -1 });
        _.each(part_checkboxes[0], function (x) { data[x[0]] = x[1].is(':checked') ? 'True' : 'False' });
        var checkbox_lists = _.groupBy(part_checkboxes[1], function (x) { return x[0].substr(0, x[0].indexOf('-')) });
        _.each(checkbox_lists, function (y, name) { data[name] = JSON.stringify(_.map(_.filter(y, function (x) { return x[1].is(':checked') }), function (x) { return x[1].val() })) });

        formRoot.find(":radio:checked:visible").each(function () {
            data[$(this).attr("name").replace(/dnn\d+/, "")] = $(this).val();
        });

        formRoot.find(".itemwithqty input:visible").each(function () {
            data[$(this).attr("name").replace(/dnn\d+/, "")] = $('#' + $(this).attr("id") + 'Qty').val() + ' ' + $(this).val();
        });

        formRoot.find('iframe').each(function () {
            if (!$(this).closest(".g-recaptcha").length && dnnsf.canAccessIFrame(this) && this.contentWindow.getContent)
                data[$(this).attr("name").replace(/dnn\d+/, "")] = this.contentWindow.getContent();
        });

        // finally, call controls
        $.each($_scope.controls, function (i, control) {
            if (!control.getValue)
                return;
            data[control.field.TitleCompacted] = control.getValue();
        });

        return data;
    }

    function afResetButton(formRoot, $btn) {
        if ($btn) {
            try { $btn.button('reset'); } catch (e) { }
            var btns = formRoot.find('.submit').not($btn);
            $.each(btns, function (i, v) {
                if ($(v).data('disabled')) {
                    $(v).data('disabled', false)
                } else {
                    $(v).removeAttr('disabled');
                }
            })
            //formRoot.find('.submit').not($btn).removeAttr('disabled');
        }
        formRoot.find('.submit-progress').stop(true, true).fadeOut(function () {
            $(this).css('visibility', 'hidden');
        });
    }

    function parseFormResponse2(formRoot, $btn, data) {

        var $ = dnnsfjQuery;

        parseFormResponse(data, {
            executeJsFunction: function (fnName) {
                window.parent[fnName](window.frameElement);
                setTimeout(function () {
                    afResetButton(formRoot, $btn);
                }, 500);
            },
            executeJsCode: function (jsCode) {
                if (!form)
                    var form = formRoot.scope() && formRoot.scope().form;
                eval(jsCode);
                setTimeout(function () {
                    afResetButton(formRoot, $btn);
                }, 500);
            },
            error: function (err) {
                formRoot.find(".server-error").html(err).show();
                afResetButton(formRoot, $btn);
                $('.g-recaptcha', formRoot).each(function () {
                    grecaptcha.reset($(this).attr('data-widgetid'));
                });
            },
            validationErrors: function (_data) {
                var displayErrors = function (errors, mid) {
                    $.each(errors, function (i, err) {
                        var fieldId = mid + err.fieldId;
                        var parent = $('#dnn' + fieldId).closest('.field-container');
                        if (!parent.length)
                            parent = $('[name="dnn' + fieldId + '"]').closest('.field-container');
                        var tabParent = parent.closest('.tab-pane');
                        tabParent.length && !tabParent.hasClass('active') && $('[href="#' + tabParent.attr('id') + '"]').addClass('has-error');
                        parent.addClass('has-error');
                        parent.find('.text-danger').length ?
                            parent.find('.text-danger').html(err.message).show() :
                            parent.append('<span class="text-danger">' + err.message + '</span>');
                    });

                    if ($('.has-error').length) {
                        $('html,body').animate({
                            scrollTop: $('.has-error').first().offset().top - 100
                        })
                    }
                }
                var mid = $(formRoot).parent().attr('data-moduleid');
                if (_data.validationErrors.constructor === Array) {
                    //is current form
                    displayErrors(_data.validationErrors, mid);
                } else {
                    //conected forms
                    $.each(_data.validationErrors, function (i, v) {
                        if (v.length) {
                            var form;

                            if (i != '$_thisForm') {
                                form = _data.connectedForms.find(function (j, k) {
                                    return j.FormName.toLowerCase() == i;
                                })
                            }
                            displayErrors(v, form ? form.FormId : mid);
                        }
                    });
                }


                setTimeout(function () {
                    afResetButton(formRoot, $btn);
                    $('.g-recaptcha', formRoot).each(function () {
                        grecaptcha.reset($(this).attr('data-widgetid'));
                    });
                }, 500);
                return;
            },
            redirect: function (url, isPushState) {
                if (data.forceDownload) {
                    var urlPath = dnnsf.getUrlParts(url).pathname;
                    var fileNameAndExtension = urlPath.substring(urlPath.lastIndexOf("/") + 1);
                    if (!window.ActiveXObject) {
                        var save = document.createElement('a');
                        save.href = url;
                        save.target = '_blank';
                        save.download = fileNameAndExtension || 'unknown';

                        var evt = new MouseEvent('click', {
                            'view': window,
                            'bubbles': true,
                            'cancelable': false
                        });
                        save.dispatchEvent(evt);

                        (window.URL || window.webkitURL).revokeObjectURL(save.href);
                    }

                        // for IE < 11
                    else if (!!window.ActiveXObject && document.execCommand) {
                        var _window = window.open(url, '_blank');
                        _window.document.close();
                        _window.document.execCommand('SaveAs', true, fileNameAndExtension || url)
                        _window.close();
                    }
                    setTimeout(function () {
                        afResetButton(formRoot, $btn);
                    }, 500);
                    return;
                }

                if (!data.popup && !data.newTab) {

                    if (isPushState) {
                        //$('.angrid').scope().$location.url(url);
                        setTimeout(function () {
                            window.history.pushState({}, '', url + location.hash);//added location.hash for tabsPro
                        }, 500)
                        afResetButton(formRoot, $btn);
                    } else {
                        window.location = url;
                        if (url.indexOf('#') == 0) {
                            setTimeout(function () {
                                afResetButton(formRoot, $btn);
                            }, 500);
                        }
                    }

                    return;
                }

                // handle new tab
                if (data.newTab) {
                    window.open(url, '_blank');
                    setTimeout(function () {
                        afResetButton(formRoot, $btn);
                    }, 500);
                    return;
                }

                // open popup
                //set modal and append to body
                var popup = $('<div class="af-modal modal fade">' +
                    '<div class="modal-dialog modal-lg">' +
                    '<div class="modal-content">' +
                    '<div class="modal-header">' +
                    '<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
                    '<h4 class="modal-title">' + data.popupTitle + '</h4>' +
                    '</div>' +
                    '<div class="modal-body">' +
                    '<iframe width="100%" src="' + url + '" frameborder="0" scrolling="yes"></iframe>' +
                    '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>').appendTo('body');

                //console.log(popup.modal); // removed unnecesary logs
                popup.modal({
                    backdrop: true
                });

                popup.on('hidden.bs.modal', function () {
                    //stop resizing
                    window.clearInterval(resizeInterval);
                    //remove from DOM
                    popup.remove();
                    //remove backdrop
                    $('.modal-backdrop').remove();
                });

                //resize iframe so it has no scroll bar
                var __prevHeight = 0;
                var resizeInterval = setInterval(function () {
                    var iframe = $('.af-modal:visible').find('iframe');
                    try {
                        var bodyHeight = iframe[0].contentWindow.document.body.scrollHeight;// the corect way to get iframe height
                        //if (bodyHeight < __prevHeight) {
                        //    bodyHeight = bodyHeight - 50;// Decreseaing in height is slow because something somewhere is intercating with them. Here is a little boost ...
                        //}
                        if (bodyHeight != __prevHeight) {
                            __prevHeight = bodyHeight;
                            iframe.height(Math.max(200, bodyHeight));// minimum 200px
                        }
                    } catch (e) {
                        iframe.height(window.innerHeight - 240);// better save the sorry
                    }
                }, 1000);

                setTimeout(function () {
                    afResetButton(formRoot, $btn);
                }, 500);
            },

            message: function (msg, type) {
                if (!type || type == 'success') {
                    formRoot.find(".c-form").slideUp();
                    formRoot.find(".submit-confirm2").hide();
                    $('html, body').animate({
                        scrollTop: formRoot.offset().top - 200
                    }, 500);
                }
                formRoot.find(".submit-confirm").html(msg).show();
                var scope = formRoot.find(".submit-confirm").scope();

                angular.element(formRoot).injector().invoke(function ($compile) {
                    $compile(formRoot.find(".submit-confirm").contents())(scope);
                });

                afResetButton(formRoot, $btn);
            },
            appendHtml: function (appendHtml, appendTo, reset) {
                $(appendTo).append(appendHtml);
                reset && afResetButton(formRoot, $btn);
            },
            data: function (data) {
                dnnsf['af-' + data.baseId.replace('dnn', '')].Data = data;
                angular.element(formRoot).scope().load(data.baseId.replace('dnn', ''));
                //angular.element(formRoot).scope().load(data, angular.element(formRoot).scope().settings);
                angular.element(formRoot).scope().$apply();
                angular.element(formRoot).scope().$broadcast('updateFormData');
                afResetButton(formRoot, $btn);
            },
            noOp: function () {
                setTimeout(function () {
                    afResetButton(formRoot, $btn);
                }, 500);
            }
        });
    }

    function parseFormResponse(data, handlers) {

        var $ = dnnsfjQuery;

        // initialize with default handlers, unless provieded by caller
        handlers = $.extend({
            keepOnPage: function (url) {
                window.location.reload(true);
            },
            redirect: function (url) {
                window.location = url;
            },
            appendHtml: function (appendHtml, appendTo) {
                $(appendTo).append(appendHtml);
            },
            error: function (err) { },
            message: function (msg, type) { },
            data: function (msg, type) { },
            executeJsCode: function (jsCode) {
                try {
                    eval(jsCode)
                } catch (e) {
                    console.error(e);
                }
            }
        }, handlers);

        // parse response and call handlers
        if (data.functionName) {
            handlers.executeJsFunction && handlers.executeJsFunction(data.functionName);
        } if (data.JsCode) {
            handlers.executeJsCode && handlers.executeJsCode(data.JsCode);
        } else if (data.Error || data.error) {
            handlers.error && handlers.error(data.Error || data.error);
        } else if (data.validationErrors) {
            handlers.validationErrors && handlers.validationErrors(data);
        } else if (data.Content) {
            handlers.message && handlers.message(data.Content, data.Type);
        } else if (data.KeepOnPage) {
            handlers.KeepOnPage && handlers.KeepOnPage(data.Url);
        } else if (data.Url) {
            handlers.redirect && handlers.redirect(data.Url, data.PushState);
        } else if (data.appendHtml) {
            handlers.appendHtml && handlers.appendHtml(data.appendHtml, data.appendTo, data.reset);
        } else if (data.data) {
            handlers.data && handlers.data(data.data);
        } else if (data.noOp) {
            handlers.noOp && handlers.noOp();
        }
    }


    return {
        initForm: initForm,
        getFormData: getFormData,
        afResetButton: afResetButton,
        parseFormResponse: parseFormResponse
    }
})(dnnsfjQuery, window.dnnsfAngular15);

var initForm = afApp.initForm,
    getFormData = afApp.getFormData,
    afResetButton = afApp.afResetButton,
    parseFormResponse = afApp.parseFormResponse;


function browseGrid(settings) {
    $('body').append('<div class="loader-wrapper" id="modalLoader"><div class="loader"></div></div>');
    $.get(window.dnnsf.commonUrl + '/static/dnnsf/tpl/gridModal.html', function (data) {
        var iframeData = data.replace('gridUrl', settings.url);
        iframeData = iframeData.replace('popupHeight', $(window).height() - 150 + 'px');
        $('body').append(iframeData);
        $('#gridFrame').load(function () {
            $('#gridFrame').contents().find('body').css({ 'overflow': 'auto' });
            $('#gridFrame').contents().find('body table').css({ 'width': '90%', 'margin': '0 auto' });
            setTimeout(function () {
                dnnsfjQuery('#gridModal').modal('show');
                $('#modalLoader').remove();
            }, 500);
            dnnsfjQuery('#gridModal').on('shown.bs.modal', function (e) {
                getData();
            });
        });
        dnnsfjQuery('#gridModal').on('hidden.bs.modal', function (e) {
            $('#gridModal').remove();
        });
    });
    function getData() {
        var iframe = window.frames['gridFrame'].document;
        $('body', iframe).on('click', '.grid-item', function () {
            var gridScope = window.frames['gridFrame'].angular.element($(this).closest('.item-value')).scope();
            $.each(settings.mappings, function (e, f) {
                var elem = $('* [data-ng-model^="form.fields.' + e + '"]');
                var parent = angular.element(elem).closest('.form-root').scope();
                parent.form.fields[e].value = gridScope.item[f];
                parent.form.fields[e].onChange && parent.form.fields[e].onChange(parent.form);
                parent.$apply();
            });
            dnnsfjQuery('#gridModal').modal('hide');
        })
            .on('mouseenter', '.grid-item', function () {
                $(this).closest('.item-value').addClass('hover-item');
            })
            .on('mouseleave', '.grid-item', function () {
                $(this).closest('.item-value').removeClass('hover-item');
            });
    };
}
