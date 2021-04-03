/**
 * Compare two software version numbers (e.g. 1.7.1)
 * Returns:
 *
 *  0 if they're identical
 *  negative if v1 < v2
 *  positive if v1 > v2
 *  Nan if they in the wrong format
 *
 *  E.g.:
 *
 *  assert(version_number_compare("1.7.1", "1.6.10") > 0);
 *  assert(version_number_compare("1.7.1", "1.7.10") < 0);
 *
 *  "Unit tests": http://jsfiddle.net/ripper234/Xv9WL/28/
 *
 *  Taken from http://stackoverflow.com/a/6832721/11236
 */
function af_compareVersionNumbers(v1, v2) {

    var v1parts = v1.split('.');
    var v2parts = v2.split('.');

    function isPositiveInteger(x) {
        // http://stackoverflow.com/a/1019526/11236
        return /^\d+$/.test(x);
    }
    // First, validate both numbers are true version numbers
    function validateParts(parts) {
        for (var i = 0; i < parts.length; ++i) {
            if (!isPositiveInteger(parts[i])) {
                return false;
            }
        }
        return true;
    }
    if (!validateParts(v1parts) || !validateParts(v2parts)) {
        return NaN;
    }
    for (var i = 0; i < v1parts.length; ++i) {
        if (v2parts.length === i) {
            return 1;
        }

        if (v1parts[i] === v2parts[i]) {
            continue;
        }
        if (Number(v1parts[i]) > Number(v2parts[i])) {
            return 1;
        }
        return -1;
    }
    if (v1parts.length != v2parts.length) {
        return -1;
    }
    return 0;
}

dnnsfjQuery(document).ready(function () {
    dnnsfjQuery(".af-init-onchange").each(function () {
        if (dnnsfjQuery(this).val())
            dnnsfjQuery(this).change();
    });
});

function afShowform(className) {
    console.warn('This function is obsolete, please use dnnsf.api.actionform.openInPopup("formName"), formName can be set in general settings');
    eval('showFormPopup' + $('.' + className.toLowerCase()).attr('data-moduleid') + '()');
}

function formPopupEvents(mid) {
    $('#dnn' + mid + 'popup').on('hidden.bs.modal', function () {
        dnnsf.api.actionForm.isFormPopupOpen['formPopup' + mid] = false;
        $('#dnn' + mid + 'popup').off('hidden.bs.modal');
        $('#dnn' + mid + 'popup').off('shown.bs.modal');
    });
}

function showFormPopup(mid, settings) {
    if (window.innerWidth > 767) {
        $('#dnn' + mid + 'popup')
            .on('shown.bs.modal', function () {
                formPopupEvents(mid);
            }).modal()
            .find('.modal-dialog:first').css({
                'width': settings.width
            });
        $('.modal-dialog:first').children().attr('style', settings.background);
    } else {
        $('#dnn' + mid + 'popup')
            .on('shown.bs.modal', function () {
                formPopupEvents(mid);
            }).modal()
            .find('.modal-dialog:first').removeAttr('style');
        $('.modal-dialog:first').children().attr('style', settings.background);
    }

}

function showFormInline(mid, parent, url) {
    var target = dnnsfjQuery('[href="javascript: showFormInline' + mid + '();"]').attr('target');
    if (target === undefined || target === null || target === '' || target === '_self') {
        dnnsfjQuery("#" + parent + " .frontEndTemplate").slideUp('fast');
        dnnsfjQuery("#" + parent + " .form-root").slideDown('fast');
    } else if (target === '_blank') {
        var w = window.open();
        w.location = url;
    }
}
function hideFormInline(mid, parent) {
    dnnsfjQuery("#" + parent + " .frontEndTemplate").slideDown();
    dnnsfjQuery("#" + parent + " .form-root").slideUp();
}

function checkIfInitialized(mid, opt, reinitForm) {
    if (reinitForm || $('#dnn' + mid + 'popup').length === 0) {
        initForm(opt, function () { showFormPopup(mid, opt.popupSettings) });
    } else {
        showFormPopup(mid, opt.popupSettings);
    }
}

dnnsf.api.actionForm = {
    isFormPopupOpen: {},
    openPopupByName: function (name, qs, reinitForm) {
        var mid = $('[af-name="' + name + '"]').attr('data-moduleid');
        if (!dnnsf.api.actionForm.isFormPopupOpen['formPopup' + mid]) {
            dnnsf.api.actionForm.isFormPopupOpen['formPopup' + mid] = true;
            var opt = dnnsf['af-' + mid].options;
            opt.qs = qs;
            opt.openMode == 'Manual';
            opt.manualMode = 'Popup';
            checkIfInitialized(mid, opt, reinitForm);
        }
    },
    openPopupById: function (mid, qs, reinitForm) {
        if (!dnnsf.api.actionForm.isFormPopupOpen['formPopup' + mid]) {
            dnnsf.api.actionForm.isFormPopupOpen['formPopup' + mid] = true;
            var opt = dnnsf['af-' + mid].options;
            opt.qs = qs;
            opt.manualMode = 'Popup';
            opt.openMode = "Manual";
            checkIfInitialized(mid, opt, reinitForm);
        }
    },
    closePopupById: function (mid) {
        dnnsfjQuery('#dnn' + mid + 'popup').modal('hide');
    },
    initForm: function (mid, qs) {
        var formRoot = $('#dnn' + mid + 'root');
        var opt = dnnsf['af-' + mid].options;
        opt.qs = qs;
        opt.openMode = "Always";
        initForm(opt);
    },
    showFormInline: function (mid, qs) {
        var opt = dnnsf['af-' + mid].options;
        opt.qs = qs;
        opt.openMode == 'Manual' && (opt.manualMode = 'Inline', initForm(opt));
        showFormInline(opt.moduleId, opt.rootElementClientId, opt.ctlUrl)
    },
    hideFormInline: function (mid) {
        var opt = dnnsf['af-' + mid].options;
        hideFormInline(opt.moduleId, opt.rootElementClientId)
    },
    showFormLoading: function (mid, time) {
        $('#dnn_ctr' + mid + '_Main_pnlContent > .common-loading-container').show();

        // setTimeout(function () {
        //     $('#dnn_ctr' + mid + '_Main_pnlContent > .common-loading-container').hide();
        //     dnnsf.events.broadcast('loadForm', { 'loading': false, moduleId: mid });
        // }, time || 0);
    },
    showTabsProLoading: function (mid, time) {
        dnnsf.events.broadcast('loadForm', { 'loading': true, moduleId: mid });

        // setTimeout(function () {
        //     $('#dnn_ctr' + mid + '_Main_pnlContent > .common-loading-container').hide();
        //     dnnsf.events.broadcast('loadForm', { 'loading': false, moduleId: mid });
        // }, time || 0);
    }
}