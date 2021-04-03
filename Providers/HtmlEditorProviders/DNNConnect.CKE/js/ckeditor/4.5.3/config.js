/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
};

// RG: Force CKEditor "not" to add a breakline (<br/>) before/after p.ul,ol,li
CKEDITOR.on('instanceReady', function (ev) {
    ev.editor.dataProcessor.writer.setRules('p',
        {
            indent: false,
            breakBeforeOpen: false,
            breakAfterOpen: false,
            breakBeforeClose: false,
            breakAfterClose: false
        });
});
CKEDITOR.on('instanceReady', function (ev) {
    ev.editor.dataProcessor.writer.setRules('ul',
        {
            indent: false,
            breakBeforeOpen: false,
            breakAfterOpen: false,
            breakBeforeClose: false,
            breakAfterClose: false
        });
});
CKEDITOR.on('instanceReady', function (ev) {
    ev.editor.dataProcessor.writer.setRules('li',
        {
            indent: false,
            breakBeforeOpen: false,
            breakAfterOpen: false,
            breakBeforeClose: false,
            breakAfterClose: false
        });
});
CKEDITOR.on('instanceReady', function (ev) {
    ev.editor.dataProcessor.writer.setRules('ol',
        {
            indent: false,
            breakBeforeOpen: false,
            breakAfterOpen: false,
            breakBeforeClose: false,
            breakAfterClose: false
        });
});
config.enterMode = CKEDITOR.ENTER_BR;
config.shiftEnterMode = CKEDITOR.ENTER_BR;