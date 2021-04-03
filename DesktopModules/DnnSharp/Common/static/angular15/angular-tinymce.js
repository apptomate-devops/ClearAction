/**
 * Binds a TinyMCE widget to <textarea> elements.
 */
dnnsfAngular15.module('ui.tinymce', [])
  .value('uiTinymceConfig', {})
  .directive('uiTinymce', ['$rootScope', '$compile', '$timeout', '$window', '$sce', 'uiTinymceConfig', function($rootScope, $compile, $timeout, $window, $sce, uiTinymceConfig) {
    uiTinymceConfig = uiTinymceConfig || {};
    var generatedIds = 0;
    var ID_ATTR = 'ui-tinymce';
    if (uiTinymceConfig.baseUrl) {
      tinymce.baseURL = uiTinymceConfig.baseUrl;
    }

    return {
      require: ['ngModel', '^?form'],
      priority: 599,
      link: function(scope, element, attrs, ctrls) {
        if (!$window.tinymce) {
          return;
        }

        var ngModel = ctrls[0],
          form = ctrls[1] || null;

        var expression, options = {}, tinyInstance,
          updateView = function(editor) {
            var content = editor.getContent({format: options.format}).trim();
            content = $sce.trustAsHtml(content);

            ngModel.$setViewValue(content);
            if (!$rootScope.$$phase) {
              scope.$digest();
            }
          };

        function toggleDisable(disabled) {
          if (disabled) {
            ensureInstance();

            if (tinyInstance) {
              tinyInstance.getBody().setAttribute('contenteditable', false);
            }
          } else {
            ensureInstance();

            if (tinyInstance && !tinyInstance.settings.readonly) {
              tinyInstance.getBody().setAttribute('contenteditable', true);
            }
          }
        }

        // generate an ID
        attrs.$set('id', ID_ATTR + '-' + generatedIds++);

        expression = {};

        dnnsfAngular15.extend(expression, scope.$eval(attrs.uiTinymce));

        //Debounce update and save action
        var debouncedUpdate = (function(debouncedUpdateDelay) {
          var debouncedUpdateTimer;
          return function(ed) {
	        $timeout.cancel(debouncedUpdateTimer);
	         debouncedUpdateTimer = $timeout(function() {
              return (function(ed) {
                if (ed.isDirty()) {
                  ed.save();
                  updateView(ed);
                }
              })(ed);
            }, debouncedUpdateDelay);
          };
        })(400);

        var setupOptions = {
          // Update model when calling setContent
          // (such as from the source editor popup)
          setup: function(ed) {
            ed.on('init', function() {
              ngModel.$render();
              ngModel.$setPristine();
              ngModel.$setUntouched();
              if (form) {
                form.$setPristine();
              }
            });

            // Update model when:
            // - a button has been clicked [ExecCommand]
            // - the editor content has been modified [change]
            // - the node has changed [NodeChange]
            // - an object has been resized (table, image) [ObjectResized]
            ed.on('ExecCommand change NodeChange ObjectResized', function() {
              debouncedUpdate(ed);
            });

            ed.on('blur', function() {
              element[0].blur();
              ngModel.$setTouched();
              scope.$digest();
            });

            ed.on('remove', function() {
              element.remove();
            });

            if (expression.setup) {
              expression.setup(ed, {
                updateView: updateView
              });
            }
          },
          format: expression.format || 'html',
          selector: '#' + attrs.id,
          trusted: true,
          inline: false,
          menu: {},
          //menubar: 'file edit insert view format table tools',
          custom_undo_redo_levels: 10,
          theme: 'modern',
          skin: 'custom',
          plugins: ['autolink lists link image charmap  preview hr anchor code fullscreen table contextmenu paste textcolor colorpicker'],
          style_formats: [
                      {
                          title: "Headers",
                          items: [
                              { title: "Header 1", format: "h1" },
                              { title: "Header 2", format: "h2" },
                              { title: "Header 3", format: "h3" },
                              { title: "Header 4", format: "h4" },
                              { title: "Header 5", format: "h5" },
                              { title: "Header 6", format: "h6" }
                          ]
                      },
            {
                title: "Inline", items: [{ title: "Bold", icon: "bold", format: "bold" }, { title: "Italic", icon: "italic", format: "italic" },
                { title: "_Underline", icon: "underline", format: "underline" }, { title: "Strikethrough", icon: "strikethrough", format: "strikethrough" }, { title: "Superscript", icon: "superscript", format: "superscript" }, { title: "Subscript", icon: "subscript", format: "subscript" }, { title: "Code", icon: "code", format: "code" }]
            },
            { title: "_Blocks", items: [{ title: "Paragraph", format: "p" }, { title: "Blockquote", format: "blockquote" }, { title: "Div", format: "div" }, { title: "Pre", format: "pre" }] },
            {
                title: "Font Size", items: [
                                   { title: '8px', inline: 'span', styles: { fontSize: '12px', 'font-size': '8px' } },
                                   { title: '10px', inline: 'span', styles: { fontSize: '12px', 'font-size': '10px' } },
                                   { title: '12px', inline: 'span', styles: { fontSize: '12px', 'font-size': '12px' } },
                                   { title: '14px', inline: 'span', styles: { fontSize: '12px', 'font-size': '14px' } },
                                   { title: '16px', inline: 'span', styles: { fontSize: '12px', 'font-size': '16px' } }
                ]
            }],
          toolbar1: 'undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
          toolbar2: 'forecolor backcolor | charmap hr table | preview code fullscreen',
          image_advtab: true,
          verify_html: false,
          force_br_newlines: true,
          force_p_newlines: false,
          forced_root_block: ''
        };
        // extend options with initial uiTinymceConfig and
        // options from directive attribute value
        dnnsfAngular15.extend(options, uiTinymceConfig, expression, setupOptions);
        // Wrapped in $timeout due to $tinymce:refresh implementation, requires
        // element to be present in DOM before instantiating editor when
        // re-rendering directive
        $timeout(function() {
          if (options.baseURL){
            tinymce.baseURL = options.baseURL;
          }
          tinymce.init(options);
          toggleDisable(scope.$eval(attrs.ngDisabled));
        });

        ngModel.$formatters.unshift(function(modelValue) {
          return modelValue ? $sce.trustAsHtml(modelValue) : '';
        });

        ngModel.$parsers.unshift(function(viewValue) {
          return viewValue ? $sce.getTrustedHtml(viewValue) : '';
        });

        ngModel.$render = function() {
          ensureInstance();

          var viewValue = ngModel.$viewValue ?
            $sce.getTrustedHtml(ngModel.$viewValue) : '';

          // instance.getDoc() check is a guard against null value
          // when destruction & recreation of instances happen
          if (tinyInstance &&
            tinyInstance.getDoc()
          ) {
            tinyInstance.setContent(viewValue);
            // Triggering change event due to TinyMCE not firing event &
            // becoming out of sync for change callbacks
            tinyInstance.fire('change');
          }
        };

        attrs.$observe('disabled', toggleDisable);

        // This block is because of TinyMCE not playing well with removal and
        // recreation of instances, requiring instances to have different
        // selectors in order to render new instances properly
        scope.$on('$tinymce:refresh', function(e, id) {
          var eid = attrs.id;
          if (dnnsfAngular15.isUndefined(id) || id === eid) {
            var parentElement = element.parent();
            var clonedElement = element.clone();
            clonedElement.removeAttr('id');
            clonedElement.removeAttr('style');
            clonedElement.removeAttr('aria-hidden');
            tinymce.execCommand('mceRemoveEditor', false, eid);
            parentElement.append($compile(clonedElement)(scope));
          }
        });

        scope.$on('$destroy', function() {
          ensureInstance();

          if (tinyInstance) {
            tinyInstance.remove();
            tinyInstance = null;
          }
        });

        function ensureInstance() {
          if (!tinyInstance) {
            tinyInstance = tinymce.get(attrs.id);
          }
        }
      }
    };
  }]);
