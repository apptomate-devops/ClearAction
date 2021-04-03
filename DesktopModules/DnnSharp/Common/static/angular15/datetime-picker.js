// https://github.com/Gillardo/bootstrap-ui-datetime-picker
// Version: 2.4.3
// Released: 2016-07-14 
// ===================================================================
// Author: Matt Kruse <matt@mattkruse.com>
// WWW: http://www.mattkruse.com/
//
var MONTH_NAMES = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec');
var DAY_NAMES = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat');
function LZ(x) { return (x < 0 || x > 9 ? "" : "0") + x }

// ------------------------------------------------------------------
// isDate ( date_string, format_string )
// Returns true if date string matches format of format string and
// is a valid date. Else returns false.
// It is recommended that you trim whitespace around the value before
// passing it to this function, as whitespace is NOT ignored!
// ------------------------------------------------------------------
function isDate(val, format) {
    var date = getDateFromFormat(val, format);
    if (date == 0) { return false; }
    return true;
}

// -------------------------------------------------------------------
// compareDates(date1,date1format,date2,date2format)
//   Compare two date strings to see which is greater.
//   Returns:
//   1 if date1 is greater than date2
//   0 if date2 is greater than date1 of if they are the same
//  -1 if either of the dates is in an invalid format
// -------------------------------------------------------------------
function compareDates(date1, dateformat1, date2, dateformat2) {
    var d1 = getDateFromFormat(date1, dateformat1);
    var d2 = getDateFromFormat(date2, dateformat2);
    if (d1 == 0 || d2 == 0) {
        return -1;
    }
    else if (d1 > d2) {
        return 1;
    }
    return 0;
}

// ------------------------------------------------------------------
// formatDate (date_object, format)
// Returns a date in the output format specified.
// The format string uses the same abbreviations as in getDateFromFormat()
// ------------------------------------------------------------------
function formatDate(date, format) {
    format = format + "";
    var result = "";
    var i_format = 0;
    var c = "";
    var token = "";
    var y = date.getYear() + "";
    var M = date.getMonth() + 1;
    var d = date.getDate();
    var E = date.getDay();
    var H = date.getHours();
    var m = date.getMinutes();
    var s = date.getSeconds();
    var yyyy, yy, MMM, MM, dd, hh, h, mm, ss, ampm, HH, H, KK, K, kk, k;
    // Convert real date parts into formatted versions
    var value = new Object();
    if (y.length < 4) { y = "" + (y - 0 + 1900); }
    value["y"] = "" + y;
    value["yyyy"] = y;
    value["yy"] = y.substring(2, 4);
    value["M"] = M;
    value["MM"] = LZ(M);
    value["MMM"] = MONTH_NAMES[M - 1];
    value["NNN"] = MONTH_NAMES[M + 11];
    value["d"] = d;
    value["dd"] = LZ(d);
    value["E"] = DAY_NAMES[E + 7];
    value["EE"] = DAY_NAMES[E];
    value["H"] = H;
    value["HH"] = LZ(H);
    if (H == 0) { value["h"] = 12; }
    else if (H > 12) { value["h"] = H - 12; }
    else { value["h"] = H; }
    value["hh"] = LZ(value["h"]);
    if (H > 11) { value["K"] = H - 12; } else { value["K"] = H; }
    value["k"] = H + 1;
    value["KK"] = LZ(value["K"]);
    value["kk"] = LZ(value["k"]);
    if (H > 11) { value["a"] = "PM"; }
    else { value["a"] = "AM"; }
    value["m"] = m;
    value["mm"] = LZ(m);
    value["s"] = s;
    value["ss"] = LZ(s);
    while (i_format < format.length) {
        c = format.charAt(i_format);
        token = "";
        while ((format.charAt(i_format) == c) && (i_format < format.length)) {
            token += format.charAt(i_format++);
        }
        if (value[token] != null) { result = result + value[token]; }
        else { result = result + token; }
    }
    return result;
}

// ------------------------------------------------------------------
// Utility functions for parsing in getDateFromFormat()
// ------------------------------------------------------------------
function _isInteger(val) {
    var digits = "1234567890";
    for (var i = 0; i < val.length; i++) {
        if (digits.indexOf(val.charAt(i)) == -1) { return false; }
    }
    return true;
}
function _getInt(str, i, minlength, maxlength) {
    for (var x = maxlength; x >= minlength; x--) {
        var token = str.substring(i, i + x);
        if (token.length < minlength) { return null; }
        if (_isInteger(token)) { return token; }
    }
    return null;
}

// ------------------------------------------------------------------
// getDateFromFormat( date_string , format_string )
//
// This function takes a date string and a format string. It matches
// If the date string matches the format string, it returns the 
// getTime() of the date. If it does not match, it returns 0.
// ------------------------------------------------------------------
function getDateFromFormat(val, format) {
    val = val + "";
    format = format + "";
    var i_val = 0;
    var i_format = 0;
    var c = "";
    var token = "";
    var token2 = "";
    var x, y;
    var now = new Date();
    var year = now.getYear();
    var month = now.getMonth() + 1;
    var date = 1;
    var hh = now.getHours();
    var mm = now.getMinutes();
    var ss = now.getSeconds();
    var ampm = "";

    while (i_format < format.length) {
        // Get next token from format string
        c = format.charAt(i_format);
        token = "";
        while ((format.charAt(i_format) == c) && (i_format < format.length)) {
            token += format.charAt(i_format++);
        }
        // Extract contents of value based on format token
        if (token == "yyyy" || token == "yy" || token == "y") {
            if (token == "yyyy") { x = 4; y = 4; }
            if (token == "yy") { x = 2; y = 2; }
            if (token == "y") { x = 2; y = 4; }
            year = _getInt(val, i_val, x, y);
            if (year == null) { return 0; }
            i_val += year.length;
            if (year.length == 2) {
                if (year > 70) { year = 1900 + (year - 0); }
                else { year = 2000 + (year - 0); }
            }
        }
        else if (token == "MMM" || token == "NNN") {
            month = 0;
            for (var i = 0; i < MONTH_NAMES.length; i++) {
                var month_name = MONTH_NAMES[i];
                if (val.substring(i_val, i_val + month_name.length).toLowerCase() == month_name.toLowerCase()) {
                    if (token == "MMM" || (token == "NNN" && i > 11)) {
                        month = i + 1;
                        if (month > 12) { month -= 12; }
                        i_val += month_name.length;
                        break;
                    }
                }
            }
            if ((month < 1) || (month > 12)) { return 0; }
        }
        else if (token == "EE" || token == "E") {
            for (var i = 0; i < DAY_NAMES.length; i++) {
                var day_name = DAY_NAMES[i];
                if (val.substring(i_val, i_val + day_name.length).toLowerCase() == day_name.toLowerCase()) {
                    i_val += day_name.length;
                    break;
                }
            }
        }
        else if (token == "MM" || token == "M") {
            month = _getInt(val, i_val, token.length, 2);
            if (month == null || (month < 1) || (month > 12)) { return 0; }
            i_val += month.length;
        }
        else if (token == "dd" || token == "d") {
            date = _getInt(val, i_val, token.length, 2);
            if (date == null || (date < 1) || (date > 31)) { return 0; }
            i_val += date.length;
        }
        else if (token == "hh" || token == "h") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 1) || (hh > 12)) { return 0; }
            i_val += hh.length;
        }
        else if (token == "HH" || token == "H") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 0) || (hh > 23)) { return 0; }
            i_val += hh.length;
        }
        else if (token == "KK" || token == "K") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 0) || (hh > 11)) { return 0; }
            i_val += hh.length;
        }
        else if (token == "kk" || token == "k") {
            hh = _getInt(val, i_val, token.length, 2);
            if (hh == null || (hh < 1) || (hh > 24)) { return 0; }
            i_val += hh.length; hh--;
        }
        else if (token == "mm" || token == "m") {
            mm = _getInt(val, i_val, token.length, 2);
            if (mm == null || (mm < 0) || (mm > 59)) { return 0; }
            i_val += mm.length;
        }
        else if (token == "ss" || token == "s") {
            ss = _getInt(val, i_val, token.length, 2);
            if (ss == null || (ss < 0) || (ss > 59)) { return 0; }
            i_val += ss.length;
        }
        else if (token == "a") {
            if (val.substring(i_val, i_val + 2).toLowerCase() == "am") { ampm = "AM"; }
            else if (val.substring(i_val, i_val + 2).toLowerCase() == "pm") { ampm = "PM"; }
            else { return 0; }
            i_val += 2;
        }
        else {
            if (val.substring(i_val, i_val + token.length) != token) { return 0; }
            else { i_val += token.length; }
        }
    }
    // If there are any trailing characters left in the value, it doesn't match
    if (i_val != val.length) { return 0; }
    // Is date valid for month?
    if (month == 2) {
        // Check for leap year
        if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) { // leap year
            if (date > 29) { return 0; }
        }
        else { if (date > 28) { return 0; } }
    }
    if ((month == 4) || (month == 6) || (month == 9) || (month == 11)) {
        if (date > 30) { return 0; }
    }
    // Correct hours value
    if (hh < 12 && ampm == "PM") { hh = hh - 0 + 12; }
    else if (hh > 11 && ampm == "AM") { hh -= 12; }
    var newdate = new Date(year, month - 1, date, hh, mm, ss);
    return newdate.getTime();
}

// ------------------------------------------------------------------
// parseDate( date_string [, prefer_euro_format] )
//
// This function takes a date string and tries to match it to a
// number of possible date formats to get the value. It will try to
// match against the following international formats, in this order:
// y-M-d   MMM d, y   MMM d,y   y-MMM-d   d-MMM-y  MMM d
// M/d/y   M-d-y      M.d.y     MMM-d     M/d      M-d
// d/M/y   d-M-y      d.M.y     d-MMM     d/M      d-M
// A second argument may be passed to instruct the method to search
// for formats like d/M/y (european format) before M/d/y (American).
// Returns a Date object or null if no patterns match.
// ------------------------------------------------------------------
function parseDate(val) {
    var preferEuro = (arguments.length == 2) ? arguments[1] : false;
    generalFormats = new Array('y-M-d', 'MMM d, y', 'MMM d,y', 'y-MMM-d', 'd-MMM-y', 'MMM d');
    monthFirst = new Array('M/d/y', 'M-d-y', 'M.d.y', 'MMM-d', 'M/d', 'M-d');
    dateFirst = new Array('d/M/y', 'd-M-y', 'd.M.y', 'd-MMM', 'd/M', 'd-M');
    var checkList = new Array('generalFormats', preferEuro ? 'dateFirst' : 'monthFirst', preferEuro ? 'monthFirst' : 'dateFirst');
    var d = null;
    for (var i = 0; i < checkList.length; i++) {
        var l = window[checkList[i]];
        for (var j = 0; j < l.length; j++) {
            d = getDateFromFormat(val, l[j]);
            if (d != 0) { return new Date(d); }
        }
    }
    return null;
}
dnnsfAngular15.module('ui.bootstrap.datetimepicker', ['ui.bootstrap.dateparser', 'ui.bootstrap.position'])
    .constant('uiDatetimePickerConfig', {
        dateFormat: 'yyyy-MM-dd HH:mm',
        defaultTime: '00:00:00',
        html5Types: {
            date: 'yyyy-MM-dd',
            'datetime-local': 'yyyy-MM-ddTHH:mm:ss.sss',
            'month': 'yyyy-MM'
        },
        initialPicker: 'date',
        reOpenDefault: false,
        enableDate: true,
        enableTime: true,
        buttonBar: {
            show: true,
            now: {
                show: true,
                text: 'Now'
            },
            today: {
                show: true,
                text: 'Today'
            },
            clear: {
                show: true,
                text: 'Clear'
            },
            date: {
                show: true,
                text: 'Date'
            },
            time: {
                show: true,
                text: 'Time'
            },
            close: {
                show: true,
                text: 'Close'
            }
        },
        closeOnDateSelection: true,
        closeOnTimeNow: true,
        appendToBody: true,
        altInputFormats: [],
        ngModelOptions: {},
        saveAs: false,
        readAs: false
    })
    .controller('DateTimePickerController', ['$scope', '$element', '$attrs', '$compile', '$parse', '$document', '$timeout', '$uibPosition', 'dateFilter', 'uibDateParser', 'uiDatetimePickerConfig', '$rootScope',
        function ($scope, $element, $attrs, $compile, $parse, $document, $timeout, $uibPosition, dateFilter, uibDateParser, uiDatetimePickerConfig, $rootScope) {
            var dateFormat = uiDatetimePickerConfig.dateFormat,
                ngModel, ngModelOptions, $popup, cache = {}, watchListeners = [],
                closeOnDateSelection = dnnsfAngular15.isDefined($attrs.closeOnDateSelection) ? $scope.$parent.$eval($attrs.closeOnDateSelection) : uiDatetimePickerConfig.closeOnDateSelection,
                closeOnTimeNow = dnnsfAngular15.isDefined($attrs.closeOnTimeNow) ? $scope.$parent.$eval($attrs.closeOnTimeNow) : uiDatetimePickerConfig.closeOnTimeNow,
                appendToBody = dnnsfAngular15.isDefined($attrs.datepickerAppendToBody) ? $scope.$parent.$eval($attrs.datepickerAppendToBody) : uiDatetimePickerConfig.appendToBody,
                altInputFormats = dnnsfAngular15.isDefined($attrs.altInputFormats) ? $scope.$parent.$eval($attrs.altInputFormats) : uiDatetimePickerConfig.altInputFormats,
                saveAs = dnnsfAngular15.isDefined($attrs.saveAs) ? $scope.$parent.$eval($attrs.saveAs) || $attrs.saveAs : uiDatetimePickerConfig.saveAs,
                readAs = dnnsfAngular15.isDefined($attrs.readAs) ? $scope.$parent.$eval($attrs.readAs) : uiDatetimePickerConfig.readAs;

            this.init = function (_ngModel) {
                ngModel = _ngModel;
                ngModelOptions = ngModel.$options || uiDatetimePickerConfig.ngModelOptions;

                $scope.buttonBar = dnnsfAngular15.isDefined($attrs.buttonBar) ? $scope.$parent.$eval($attrs.buttonBar) : uiDatetimePickerConfig.buttonBar;

                // determine which pickers should be available. Defaults to date and time
                $scope.enableDate = dnnsfAngular15.isDefined($scope.enableDate) ? $scope.enableDate : uiDatetimePickerConfig.enableDate;
                $scope.enableTime = dnnsfAngular15.isDefined($scope.enableTime) ? $scope.enableTime : uiDatetimePickerConfig.enableTime;

                // determine default picker
                $scope.initialPicker = dnnsfAngular15.isDefined($attrs.initialPicker) ? $attrs.initialPicker : ($scope.enableDate ? uiDatetimePickerConfig.initialPicker : 'time');

                // determine the picker to open when control is re-opened
                $scope.reOpenDefault = dnnsfAngular15.isDefined($attrs.reOpenDefault) ? $attrs.reOpenDefault : uiDatetimePickerConfig.reOpenDefault;

                // check if an illegal combination of options exists
                if ($scope.initialPicker == 'date' && !$scope.enableDate) {
                    throw new Error("datetimePicker can't have initialPicker set to date and have enableDate set to false.");
                }

                // default picker view
                $scope.showPicker = !$scope.enableDate ? 'time' : $scope.initialPicker;

                var isHtml5DateInput = false;

                if (uiDatetimePickerConfig.html5Types[$attrs.type]) {
                    dateFormat = uiDatetimePickerConfig.html5Types[$attrs.type];
                    isHtml5DateInput = true;
                } else {
                    dateFormat = $attrs.datetimePicker || uiDatetimePickerConfig.dateFormat;
                    $attrs.$observe('datetimePicker', function (value) {
                        var newDateFormat = value || uiDatetimePickerConfig.dateFormat;

                        if (newDateFormat !== dateFormat) {
                            dateFormat = newDateFormat;
                            ngModel.$modelValue = null;

                            if (!dateFormat) {
                                throw new Error('datetimePicker must have a date format specified.');
                            }
                        }
                    });
                }

                if (!dateFormat) {
                    throw new Error('datetimePicker must have a date format specified.');
                }

                // popup element used to display calendar
                var popupEl = dnnsfAngular15.element('' +
                    '<div date-picker-wrap>' +
                    '<div uib-datepicker></div>' +
                    '</div>' +
                    '<div time-picker-wrap>' +
                    '<div uib-timepicker style="margin:0 auto"></div>' +
                    '</div>');

                if (ngModelOptions) {
                    timezone = ngModelOptions.timezone;
                    $scope.ngModelOptions = dnnsfAngular15.copy(ngModelOptions);
                    $scope.ngModelOptions.timezone = null;
                    if ($scope.ngModelOptions.updateOnDefault === true) {
                        $scope.ngModelOptions.updateOn = $scope.ngModelOptions.updateOn ?
                        $scope.ngModelOptions.updateOn + ' default' : 'default';
                    }

                    popupEl.attr('ng-model-options', 'ngModelOptions');
                } else {
                    timezone = null;
                }

                // get attributes from directive
                popupEl.attr({
                    'ng-model': 'date',
                    'ng-change': 'dateSelection(date)'
                });

                // datepicker element
                var datepickerEl = dnnsfAngular15.element(popupEl.children()[0]);

                if (!$scope.datepickerOptions) {
                    $scope.datepickerOptions = {};
                }

                if (isHtml5DateInput) {
                    if ($attrs.type === 'month') {
                        $scope.datepickerOptions.datepickerMode = 'month';
                        $scope.datepickerOptions.minMode = 'month';
                    }
                }

                datepickerEl.attr('datepicker-options', 'datepickerOptions');

                // set datepickerMode to day by default as need to create watch
                // else disabled cannot pass in mode
                if (!dnnsfAngular15.isDefined($scope.datepickerOptions.datepickerMode)) {
                    $scope.datepickerOptions.datepickerMode = 'day';
                }

                // timepicker element
                var timepickerEl = dnnsfAngular15.element(popupEl.children()[1]);

                if (!$scope.timepickerOptions)
                    $scope.timepickerOptions = {};

                for (var key in $scope.timepickerOptions) {
                    // Template url needs to be passed as string rather than scope variable
                    // as it is not assessed by bootstrap ui
                    if(key == "templateUrl")
                        timepickerEl.attr(cameltoDash(key), $scope.timepickerOptions.templateUrl);
                    else
                        timepickerEl.attr(cameltoDash(key), 'timepickerOptions.' + key);
                }

                // watch attrs - NOTE: minDate and maxDate are used with datePicker and timePicker.  By using the minDate and maxDate
                // with the timePicker, you can dynamically set the min and max time values.  This cannot be done using the min and max values
                // with the timePickerOptions
                dnnsfAngular15.forEach(['minDate', 'maxDate', 'initDate'], function (key) {
                    if ($scope.datepickerOptions[key]) {
                        if (key == 'minDate') {
                            timepickerEl.attr('min', 'datepickerOptions.minDate');
                        } else if (key == 'maxDate')
                            timepickerEl.attr('max', 'datepickerOptions.maxDate');
                    }
                });

                // do not check showWeeks attr, as should be used via datePickerOptions

                if (!isHtml5DateInput) {
                    // Internal API to maintain the correct ng-invalid-[key] class
                    ngModel.$$parserName = 'datetime';
                    ngModel.$validators.datetime = validator;
                    ngModel.$parsers.unshift(parseDate);
                    ngModel.$formatters.push(function (value) {
                        if (ngModel.$isEmpty(value)) {
                            $scope.date = value;
                            return value;
                        }

                        $scope.date = uibDateParser.fromTimezone(value, ngModelOptions.timezone);

                        dateFormat = dateFormat.replace(/M!/, 'MM')
                            .replace(/d!/, 'dd');

                        return uibDateParser.filter($scope.date, dateFormat);
                    });
                } else {
                    ngModel.$formatters.push(function (value) {
                        $scope.date = uibDateParser.fromTimezone(value, ngModelOptions.timezone);
                        return value;
                    });
                }

                if (saveAs) {
                    // If it is determined closure var's need to be exposed to the parser, don't add the formatter here.
                    // Instead just call the method from within the stock parser with this context and/or any needed closure variables.
                    if (dnnsfAngular15.isFunction(saveAs))
                        ngModel.$parsers.push(saveAs);
                    else
                        ngModel.$parsers.push(saveAsParser);

                    // Assuming if saveAs is !false, we'll want to convert, either pass the function, or the stock str/num -> Date obj formatter.
                    if (dnnsfAngular15.isFunction(readAs))
                        ngModel.$formatters.push(readAs);
                    else
                        ngModel.$formatters.push(readAsFormatter);
                }
                // Detect changes in the view from the text box
                ngModel.$viewChangeListeners.push(function () {
                    $scope.date = parseDateString(ngModel.$viewValue);
                });

                $element.bind('keydown', inputKeydownBind);

                $popup = $compile(popupEl)($scope);
                // Prevent jQuery cache memory leak (template is now redundant after linking)
                popupEl.remove();

                if (appendToBody) {
                    $document.find('body').append($popup);
                } else {
                    $element.after($popup);
                }

                function readAsFormatter(value) {
                    if (ngModel.$isEmpty(value))
                        return value;

                    var d = new Date(value);
                    if (dnnsfAngular15.isDate(d) && !isNaN(d))
                        return d;

                    return value;
                }

                function saveAsParser(value) {
                    if (!value || dnnsfAngular15.isString(value) || !dnnsfAngular15.isDate(value) || isNaN(value))
                        return value;

                    if (saveAs === 'ISO')
                        return value.toISOString();

                    if (saveAs === 'json')
                        return value.toJSON();

                    if (saveAs === 'number')
                        return value.valueOf();

                    if (!isHtml5DateInput) {
                        dateFormat = dateFormat.replace(/M!/, 'MM')
                            .replace(/d!/, 'dd');
                        return uibDateParser.filter(uibDateParser.fromTimezone(value, ngModelOptions.timezone), dateFormat);
                    } else {
                        return uibDateParser.fromTimezone(value, ngModelOptions.timezone).toLocaleString();
                    }
                }
            };

            // get text
            $scope.getText = function (key) {
                return $scope.buttonBar[key].text || uiDatetimePickerConfig.buttonBar[key].text;
            };

            $scope.keydown = function (evt) {
                if (evt.which === 27) {
                    $scope.close(false);
                    $element[0].focus();
                }
            };

            // determine if button is to be shown or not
            $scope.doShow = function (key) {
                if (dnnsfAngular15.isDefined($scope.buttonBar[key].show))
                    return $scope.buttonBar[key].show;
                else
                    return uiDatetimePickerConfig.buttonBar[key].show;
            };
            function formatLocalDate(date) {
            
                var now = isNaN(new Date(date)) ? new Date(getDateFromFormat(date, $attrs.datetimePicker)) : new Date(date),
                    tzo = -now.getTimezoneOffset(),
                    dif = tzo >= 0 ? '+' : '-',
                    pad = function(num) {
                    var norm = Math.abs(Math.floor(num));
                    return (norm < 10 ? '0' : '') + norm;
                    };
                return now.getFullYear() +
                    '-' + pad(now.getMonth() + 1) +
                    '-' + pad(now.getDate()) +
                    'T' + pad(now.getHours()) +
                    ':' + pad(now.getMinutes()) +
                    ':' + pad(now.getSeconds()) +
                    dif + pad(tzo / 60) +
                    ':' + pad(tzo % 60);
            }

            keepInView = function () {
                var _picker = $('.datetime-picker-dropdown');
                var rect = _picker[0].getBoundingClientRect();
                if (rect.bottom >= (window.innerHeight || document.documentElement.clientHeight)) {
                    _picker.css({ "top": rect.bottom - _picker.height() })
                }

                if (rect.right >= (window.innerWidth || document.documentElement.clientWidth)) {
                    _picker.css({ "right": "0", "left": "auto" });
                }
            }

            // Inner change
            $scope.dateSelection = function (dt, opt) {

                // check if timePicker is being shown and merge dates, so that the date
                // part is never changed, only the time
                if ($scope.enableTime && $scope.showPicker === 'time') {

                    // only proceed if dt is a date
                    if (dt || dt != null) {
                        // check if our $scope.date is null, and if so, set to todays date
                        if (!dnnsfAngular15.isDefined($scope.date) || $scope.date == null) {
                            $scope.date = new Date();
                        }

                        // dt will not be undefined if the now or today button is pressed
                        if (dt && dt != null) {
                            // get the existing date and update the time
                            var date = new Date($scope.date);
                            date.setHours(dt.getHours());
                            date.setMinutes(dt.getMinutes());
                            date.setSeconds(dt.getSeconds());
                            date.setMilliseconds(dt.getMilliseconds());
                            dt = date;
                        }
                    }
                }

                if (dnnsfAngular15.isDefined(dt)) {
                    if (!$scope.date) {
                        var defaultTime = dnnsfAngular15.isDefined($attrs.defaultTime) ? $attrs.defaultTime : uiDatetimePickerConfig.defaultTime;
                        var t = new Date('2001-01-01 ' + defaultTime);

                        if (!isNaN(t) && dt != null) {
                            dt.setHours(t.getHours());
                            dt.setMinutes(t.getMinutes());
                            dt.setSeconds(t.getSeconds());
                            dt.setMilliseconds(t.getMilliseconds());
                        }
                    }
                    $scope.date = dt;
                }

                var date = $scope.date ? dateFilter($scope.date, dateFormat) : null;
                var dateTimeOffset = formatLocalDate(date);
                $element.val(date);
                $element.attr('data-val', dateTimeOffset);
                ngModel.$setViewValue(date);

                if (closeOnDateSelection) {
                    // do not close when using timePicker as make impossible to choose a time
                    if ($scope.showPicker != 'time' && date != null) {
                        // if time is enabled, swap to timePicker
                        if ($scope.enableTime) {
                            $scope.open('time');
                        } else {
                            $scope.close(false);
                        }
                    } else if (closeOnTimeNow && $scope.showPicker === 'time' && date != null && opt === 'now') {
                        $scope.close(false);
                    }
                }

            };

            $scope.$watch('isOpen', function (value) {
                $scope.dropdownStyle = {
                    display: value ? 'block' : 'none'
                };

                if (value) {
                    cache['openDate'] = $scope.date;

                    var position = appendToBody ? $uibPosition.offset($element) : $uibPosition.position($element);

                    if ($element.closest('.modal')) {
                        position.top > 200 && (position.top -= 200);
                    }

                    if (appendToBody) {
                        $scope.dropdownStyle.top = (position.top + $element.prop('offsetHeight')) + 'px';
                    } else {
                        $scope.dropdownStyle.top = undefined;
                    }

                    $scope.dropdownStyle.left = position.left + 'px';

                    $timeout(function () {
                        $scope.$broadcast('uib:datepicker.focus');
                        $document.bind('click', documentClickBind);
                    }, 0, false);

                    $scope.open($scope.showPicker);
                } else {
                    $document.unbind('click', documentClickBind);
                }
            });

            $scope.isDisabled = function (date) {
                if (date === 'today' || date === 'now')
                    date = uibDateParser.fromTimezone(new Date(), timezone);

                var dates = {};
                dnnsfAngular15.forEach(['minDate', 'maxDate'], function (key) {
                    if (!$scope.datepickerOptions[key]) {
                        dates[key] = null;
                    } else if (dnnsfAngular15.isDate($scope.datepickerOptions[key])) {
                        dates[key] = uibDateParser.fromTimezone(new Date($scope.datepickerOptions[key]), timezone);
                    } else {
                        dates[key] = new Date(dateFilter($scope.datepickerOptions[key], 'medium'));
                    }
                });

                return $scope.datepickerOptions &&
                    dates.minDate && $scope.compare(date, dates.minDate) < 0 ||
                    dates.maxDate && $scope.compare(date, dates.maxDate) > 0;
            };

            $scope.compare = function (date1, date2) {
                return new Date(date1.getFullYear(), date1.getMonth(), date1.getDate()) - new Date(date2.getFullYear(), date2.getMonth(), date2.getDate());
            };

            $scope.select = function (opt, evt) {
                if (dnnsfAngular15.isDefined(evt)) {
                    evt.preventDefault();
                    evt.stopPropagation();
                }
				
                var date = null;
                var isNow = opt === 'now';

                if (opt === 'today' || opt == 'now') {
                    var now = new Date();
                    if (dnnsfAngular15.isDate($scope.date)) {
                        date = new Date($scope.date);
                        date.setFullYear(now.getFullYear(), now.getMonth(), now.getDate());
                        date.setHours(now.getHours(), now.getMinutes(), now.getSeconds(), now.getMilliseconds());
                    } else {
                        date = now;
                    }
                }

                $scope.dateSelection(date, opt);
            };

            $scope.open = function (picker, evt) {
                if (dnnsfAngular15.isDefined(evt)) {
                    evt.preventDefault();
                    evt.stopPropagation();
                }

                // need to delay this, else timePicker never shown
                $timeout(function () {
                    $scope.showPicker = picker;
                    keepInView();
                }, 0);

                // in order to update the timePicker, we need to update the model reference!
                // as found here https://dnnsfAngular15-ui.github.io/bootstrap/#/timepicker
                if (picker == 'time') {
                    $timeout(function () {
                        $scope.date = parseDateString(ngModel.$viewValue);
                    }, 50);
                }
            };

            $scope.close = function (closePressed, evt) {
                if (dnnsfAngular15.isDefined(evt)) {
                    evt.preventDefault();
                    evt.stopPropagation();
                }
				
                $scope.isOpen = false;

                // if enableDate and enableTime are true, reopen the picker in date mode first
                if ($scope.enableDate && $scope.enableTime)
                    $scope.showPicker = $scope.reOpenDefault === false ? 'date' : $scope.reOpenDefault;

                // if a on-close-fn has been defined, lets call it
                // we only call this if closePressed is defined!
                if (dnnsfAngular15.isDefined(closePressed)) {
                    $scope.whenClosed({args: {closePressed: closePressed, openDate: cache['openDate'] || null, closeDate: $scope.date}});
                } else {
                    $element[0].focus();
                }
            };

            $scope.$on('$destroy', function () {
                if ($scope.isOpen === true) {
                    if (!$rootScope.$$phase) {
                        $scope.$apply(function () {
                            $scope.close();
                        });
                    }
                }

                watchListeners.forEach(function (a) {
                    a();
                });
                $popup.remove();
                $element.unbind('keydown', inputKeydownBind);
                $document.unbind('click', documentClickBind);
            });

            function documentClickBind(evt) {
                var popup = $popup[0];
                var dpContainsTarget = $element[0].contains(evt.target);

                // The popup node may not be an element node
                // In some browsers (IE only) element nodes have the 'contains' function
                var popupContainsTarget = popup.contains !== undefined && popup.contains(evt.target);

                if ($scope.isOpen && !(dpContainsTarget || popupContainsTarget)) {
                    $scope.$apply(function () {
                        $scope.close(false);
                    });
                }
            }

            function inputKeydownBind(evt) {
                if (evt.which === 27 && $scope.isOpen) {
                    evt.preventDefault();
                    evt.stopPropagation();
                    $scope.$apply(function () {
                        $scope.close(false);
                    });
                    $element[0].focus();
                } else if (evt.which === 40 && !$scope.isOpen) {
                    evt.preventDefault();
                    evt.stopPropagation();
                    $scope.$apply(function () {
                        $scope.isOpen = true;
                    });
                }
            }

            function cameltoDash(string) {
                return string.replace(/([A-Z])/g, function ($1) {
                    return '-' + $1.toLowerCase();
                });
            }

            function parseDateString(viewValue) {
                var date = uibDateParser.parse(viewValue, dateFormat, $scope.date);
                if (isNaN(date)) {
                    for (var i = 0; i < altInputFormats.length; i++) {
                        date = uibDateParser.parse(viewValue, altInputFormats[i], $scope.date);
                        if (!isNaN(date)) {
                            return date;
                        }
                    }
                }
                return date;
            }

            function parseDate(viewValue) {
                if (dnnsfAngular15.isNumber(viewValue) && !isNaN(viewValue)) {
                    // presumably timestamp to date object
                    viewValue = new Date(viewValue);
                }

                if (!viewValue) {
                    return null;
                }

                if (dnnsfAngular15.isDate(viewValue) && !isNaN(viewValue)) {
                    return viewValue;
                }

                if (dnnsfAngular15.isString(viewValue)) {
                    var date = parseDateString(viewValue);
                    if (!isNaN(date)) {
                        return uibDateParser.toTimezone(date, ngModelOptions.timezone);
                    }

                    return undefined;
                } else {
                    return undefined;
                }
            }

            function validateMinMax(value) {
                if ($scope.datepickerOptions.minDate && value < $scope.datepickerOptions.minDate) {
                    return false;
                } else if ($scope.datepickerOptions.maxDate && value > $scope.datepickerOptions.maxDate) {
                    return false;
                } else {
                    return true;
                }
            }

            function validator(modelValue, viewValue) {
                var value = modelValue || viewValue;

                if (!($attrs.ngRequired || $attrs.required) && !value) {
                    return true;
                }

                if (dnnsfAngular15.isNumber(value)) {
                    value = new Date(value);
                }

                if (!value) {
                    return true;
                } else if (dnnsfAngular15.isDate(value) && !isNaN(value)) {
                    return validateMinMax(value);
                } else if (dnnsfAngular15.isDate(new Date(value)) && !isNaN(new Date(value).valueOf())) {
                    return validateMinMax(new Date(value));
                } else if (dnnsfAngular15.isString(value)) {
                    return !isNaN(parseDateString(viewValue)) && validateMinMax(parseDateString(viewValue));
                } else {
                    return false;
                }
            }

        }])
    .directive('datetimePicker', function () {
        return {
            restrict: 'A',
            require: ['ngModel', 'datetimePicker'],
            controller: 'DateTimePickerController',
            scope: {
                isOpen: '=?',
                datepickerOptions: '=?',
                timepickerOptions: '=?',
                enableDate: '=?',
                enableTime: '=?',
                initialPicker: '=?',
                reOpenDefault: '=?',
                whenClosed: '&'
            },
            link: function (scope, element, attrs, ctrls) {
            	var initialValue = dnnsfAngular15.element(element).scope().form.fields[attrs.afField].value;
            	ctrls[0].$modelValue = ctrls[0].$viewValue = initialValue;
            	$(element).val(initialValue);
                var ngModel = ctrls[0],
                ctrl = ctrls[1];
                ctrl.init(ngModel);
            }
        };
    })
    .directive('datePickerWrap', function () {
        return {
            restrict: 'EA',
            replace: true,
            transclude: true,
            templateUrl: 'template/date-picker.html'
        };
    })

    .directive('timePickerWrap', function () {
        return {
            restrict: 'EA',
            replace: true,
            transclude: true,
            templateUrl: 'template/time-picker.html'
        };
    });

dnnsfAngular15.module('ui.bootstrap.datetimepicker').run(['$templateCache', function($templateCache) {
  'use strict';

  $templateCache.put('template/date-picker.html',
    "<ul class=\"dropdown-menu dropdown-menu-left datetime-picker-dropdown bstrap30 bstrap3-material\" ng-if=\"isOpen && showPicker == 'date'\" ng-style=dropdownStyle style=left:inherit ng-keydown=keydown($event) ng-click=\"$event.preventDefault(); $event.stopPropagation()\"><li style=\"padding:0 5px 5px 5px\" class=date-picker-menu><div ng-transclude></div></li><li style=padding:5px ng-if=buttonBar.show><span class=\"btn-group pull-left\" style=margin-right:10px ng-if=\"doShow('today') || doShow('clear')\"><button type=button class=\"btn btn-sm btn-info\" ng-if=\"doShow('today')\" ng-click=\"select('today', $event)\" ng-disabled=\"isDisabled('today')\">{{ getText('today') }}</button> <button type=button class=\"btn btn-sm btn-danger\" ng-if=\"doShow('clear')\" ng-click=\"select('clear', $event)\">{{ getText('clear') }}</button></span> <span class=\"btn-group pull-right\" ng-if=\"(doShow('time') && enableTime) || doShow('close')\"><button type=button class=\"btn btn-sm btn-default\" ng-if=\"doShow('time') && enableTime\" ng-click=\"open('time', $event)\">{{ getText('time')}}</button> <button type=button class=\"btn btn-sm btn-success\" ng-if=\"doShow('close')\" ng-click=\"close(true, $event)\">{{ getText('close') }}</button></span> <span class=clearfix></span></li></ul>"
  );


  $templateCache.put('template/time-picker.html',
    "<ul class=\"dropdown-menu dropdown-menu-left datetime-picker-dropdown bstrap30 bstrap3-material\" ng-if=\"isOpen && showPicker == 'time'\" ng-style=dropdownStyle style=\"left:inherit;min-height:0\" ng-keydown=keydown($event) ng-click=\"$event.preventDefault(); $event.stopPropagation()\"><li style=\"padding:0 5px 5px 5px\" class=time-picker-menu><div ng-transclude></div></li><li style=padding:5px ng-if=buttonBar.show><span class=\"btn-group pull-left\" style=margin-right:10px ng-if=\"doShow('now') || doShow('clear')\"><button type=button class=\"btn btn-sm btn-info\" ng-if=\"doShow('now')\" ng-click=\"select('now', $event)\" ng-disabled=\"isDisabled('now')\">{{ getText('now') }}</button> <button type=button class=\"btn btn-sm btn-danger\" ng-if=\"doShow('clear')\" ng-click=\"select('clear', $event)\">{{ getText('clear') }}</button></span> <span class=\"btn-group pull-right\" ng-if=\"(doShow('date') && enableDate) || doShow('close')\"><button type=button class=\"btn btn-sm btn-default\" ng-if=\"doShow('date') && enableDate\" ng-click=\"open('date', $event)\">{{ getText('date')}}</button> <button type=button class=\"btn btn-sm btn-success\" ng-if=\"doShow('close')\" ng-click=\"close(true, $event)\">{{ getText('close') }}</button></span> <span class=clearfix></span></li></ul>"
  );

}]);
