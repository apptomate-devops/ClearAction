dnnsfjQuery(function () {
    var $ = dnnsfjQuery;
    for (var i = 0; i < 10; i++) {
        (function (igroup) {
            $.validator && $.validator.addMethod('group' + igroup + '-AtLeastOnePositiveInteger', function (value, element) {
                var isValid = false;
                $(element).parents('.c-form:first').find('.group' + igroup + '-AtLeastOnePositiveInteger').each(function () {
                    if (!isValid && isPositiveInteger($(this).val())) {
                        isValid = true;
                    }
                });

                // validate the rest of the controls in the group, but watch out for recursion
                if (!window['group' + igroup + '-AtLeastOnePositiveInteger-check']) {
                    window['group' + igroup + '-AtLeastOnePositiveInteger-check'] = true;
                    $(element).parents('.c-form:first').find('.group' + igroup + '-AtLeastOnePositiveInteger').each(function () {
                        if ($(this).valid()) { // using the structure of tabspro, repair this if it changes
                            $(this).closest('.tab-content').prev().find('.tabButton.active > .tabLink').removeClass('has-error');
                        } else {
                            $(this).closest('.tab-content').prev().find('.tabButton.active > .tabLink').addClass('has-error');
                        }
                    });
                    window['group' + igroup + '-AtLeastOnePositiveInteger-check'] = false;
                }

                return isValid;
            }, 'At least one strictly positive integer is required');
        })(i);
    }

    function isPositiveInteger(str) {
        numberShifted = str >>> 0;
        return numberShifted > 0 && numberShifted === parseFloat(str);
    }
});