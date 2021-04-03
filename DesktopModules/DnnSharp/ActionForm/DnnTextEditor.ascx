<%@ Control Language="C#" AutoEventWireup="True" Inherits="avt.ActionForm.DnnTextEditor" EnableViewState = "true" CodeBehind="DnnTextEditor.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/texteditor.ascx" %>

<style>
    .dnnTextEditor {
        margin-bottom: 0;
    }
    body {
        background-color: transparent !important;
    }
    .dnnFormItem p {
        margin-bottom: 0;
    }
    iframe {
        margin: 7px;
    }
    #Table1 {
        width: 100%;
    }
</style>

<dnn:texteditor id="txtContent" runat="server" width="100%" ChooseMode="false"></dnn:texteditor>

<script>
    window.getContent = function () {
        switch(true) {
            case (window.CKEDITOR != undefined):
                return CKEDITOR.instances[$('textarea').attr('id')].getData();
                break;
            case ($('.RadEditor').length !=-1):
                var editor = $telerik.findEditor($('.RadEditor').attr('id'));
                return editor.get_html(true);
                break;
            default:
                var frame = $('.reContentCell iframe'); // this if for DNN text editor which has multiple iframes
                if (!frame.length)
                    frame = $('iframe:first').length ? $('iframe:first') : $('body').find('textarea.cke_source');
                var content = frame[0].contentWindow ? $('body', frame[0].contentWindow.document).html() : frame.val();
                return content == "<br>" || content == "<p><br></p>" ? "" : content;
        }
    };

    $(function () {
        
        $(window.frameElement).prev().fadeOut();

        var initEditor = setInterval(function () {

            if (!$('iframe').length || !$('body', $('iframe')[0].contentWindow.document).length)
                return;

            // add padding inside iframes
            $('body', $('iframe')[0].contentWindow.document).css({ 'padding': '10px 10px' });;

            // set height of parent iframe
            $(window.frameElement).height($('body').height());

            if (!$('body', $('iframe')[0].contentWindow.document).html())
                $('body', $('iframe')[0].contentWindow.document).append('<br>');

            // load content into iframe
            if (!window.once && $('body', $('iframe')[0].contentWindow.document).html()) {
                window.once = true;
                $(window.frameElement).height(1);
                return $('body', $('iframe')[0].contentWindow.document).html($(window.frameElement).attr('data-content'));
            }
            if ($('body', $('iframe')[0].contentWindow.document).html() === $(window.frameElement).attr('data-content'))
                stopInterval();

        }, 200);

        stopInterval = function () {
            clearInterval(initEditor);
        }
    });

</script>
