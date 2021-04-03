$(window).load(function () {
	/*$('.expand a').click(function(e){
		$('.expand_dtls').slideToggle();
	})*/
    $('.menu a ').click(function (e) {
        e.preventDefault();
        $('.menu-toggle').slideToggle();
    });

    //for popup
    //RG: Instead of adding a click event, will set the tooltip to trigger on click
    /*
    $('.roles h4, .community_calls h4').click(function (e) {
        e.preventDefault();
        var tar = '.' + $(this).attr('id');
        $(tar).lightbox_me({
            centered: true,
        });
    });
    */

    //for menu            
    $('.menu a, span.close').click(function (e) {
        e.preventDefault();
        $('body').toggleClass('menu_open');
    });

    $('.expand_dtls').clone().appendTo('.mob_expand');

    //for tooltip
    //RG: Instead of adding a click event, will set the tooltip to trigger on click

    //setTooltip($('.blogs h4'), 'top', 'click'); //Comment by Kaumik
    setTooltip($('.blogs h4'), 'top', 'hover'); //Added by Kaumik

    //setTooltip($('.connections h4.mine'), 'top', 'click'); //Comment by Kaumik
    setTooltip($('.connections h4.mine'), 'top', 'hover'); // Added by Kaumik

    /*
    $('.blogs h4').click(function (e) {
        e.preventDefault();
        $('body').toggleClass('blog_open')
    });
    $('.connections h4.mine').click(function (e) {
        e.preventDefault();
        $('body').toggleClass('roles_open')
    });
    */
    

    /*added by kusum for (i) info tags*/
    //RG: Instead of adding a click event, will set the tooltip to trigger on click

    //setTooltip($('[tooltip-id="tt-analytics"]'), 'top', 'click');// Comment By Kaumik
    setTooltip($('[tooltip-id="tt-analytics"]'), 'top', 'hover'); // Added By Kaumik

    //setTooltip($('[tooltip-id="require-company"]'), 'top', 'click'); // Comment By Kaumik
    setTooltip($('[tooltip-id="require-company"]'), 'top', 'hover'); // Added By Kaumik

    //setTooltip($('[tooltip-id="tt-openforum"]'), 'top', 'click'); //Comment By Kaumik
    setTooltip($('[tooltip-id="tt-openforum"]'), 'top', 'hover'); // Added By Kaumik

    //setTooltip($('[tooltip-id="tt-rolesimilartomine"]'), 'top', 'click'); //Comment By Kaumik
    setTooltip($('[tooltip-id="tt-rolesimilartomine"]'), 'top', 'hover'); // Added By Kaumik

    //setTooltip($('[tooltip-id="tt-insightsdash"]'), 'top', 'click'); //Comment By Kaumik
    setTooltip($('[tooltip-id="tt-insightsdash"]'), 'top', 'hover'); // Added By Kaumik

    setTooltip($('[tooltip-id="tt-communitydash"]'), 'top', 'hover'); // Added By Kaumik
    setTooltip($('[tooltip-id="tt-webcastdash"]'), 'top', 'hover'); // Added By Kaumik
    setTooltip($('[tooltip-id="tt-openSolveSpacePopup"]'), 'top', 'hover'); // Added By Kaumik

    
    /*
    $('.calls_lt h4.blg').click(function (e) {
        e.preventDefault();
        $('body').toggleClass('blog_open')
    });

    $('.analytics h4').click(function (e) {
        e.preventDefault();
        $('body').toggleClass('analytics_open')
    });

    $('.space_lt h4').click(function (e) {
        e.preventDefault();
        $('body').toggleClass('space_lt_open')
    });
    
    $('.connection_1 h4.forum').click(function (e) {
        e.preventDefault();
        $('body').toggleClass('connection_1_open')
    });

    $('.comcalls_lt h4').click(function (e) {
        e.preventDefault();
        $('body').toggleClass('comcalls_lt_open')
    });

    $('.webcalls_lt h4').click(function (e) {
        e.preventDefault();
        $('body').toggleClass('webcalls_lt_open')
    });
    */

    //equal height
    $('.solve-spaces-middle-nav ul li').matchHeight();
    $('.forum-intro-match-height').matchHeight();
    $('.forum-match-height').matchHeight();
    $('.insights-match-height').matchHeight();
    $('.login .left,.login .right').matchHeight();
    $('.solve-spaces-nav a').matchHeight();
    

    //filtering
    // init Isotope
    var $grid = $('.grid').isotope({
        //default filter 
        filter: '.all'
    });
    // filter items on button click
    $('.filter-button-group').on('click', 'a', function (e) {
        var filterValue = $(this).attr('data-filter');
        $grid.isotope({ filter: filterValue });
    });
    // change is-checked class on buttons
    $('.button-group').each(function (i, buttonGroup) {
        var $buttonGroup = $(buttonGroup);
        $buttonGroup.on('click', 'li', function (e) {
            $buttonGroup.find('.active').removeClass('active');
            $(this).addClass('active');
        });
    });
    //solve spaces sidebar toggle
    $('.other-solve-space-wrapper .other-solve-space-title').click(function (e) {
        e.preventDefault();
        $(this).toggleClass('active');
        $(this).next('.connection_cnt').stop().slideToggle();
    });
    try {
        //custom select menus (hide first element on open, restore on close)
        $(".forum-select, .insights-select").selectmenu().on("selectmenuopen", function (event, ui) {
            $('.ui-selectmenu-menu li.ui-menu-item:first-child').hide();
        }).on("selectmenuclose", function (event, ui) {

            $('.ui-selectmenu-menu li.ui-menu-item:first-child').show();
        });
        $(".forum-select, .insights-select").on("selectmenuselect", function (event, ui) {
            $(this).trigger("change");
        });
    } catch (e) { }
    //forum slide toggles
    $('.forum-see-more').click(function (e) {
        $(this).closest('div.forum-row-wrapper').find('.forum-left-col').height('auto');
        $(this).closest('div.forum-row-wrapper').find('.forum-right-col').height('auto');
        $(this).parent('div').find('.forum-more-top').slideToggle();
        $(this).parent('div').find('.forum-more-bottom').slideToggle('', function () {
            $.fn.matchHeight._maintainScroll = true;
            $('.forum-match-height').matchHeight();
        });
        $(this).parent('div').find('.forum-excerpt').slideToggle();
        $(this).closest('div.forum-row-wrapper').find('.forum-right-status').slideToggle();
        $(this).parent('div').toggleClass('expanded');
        $(this).closest('div.forum-row-wrapper').find('.forum-right-col').toggleClass('expanded');
        if ($(this).hasClass('active')) {
            $(this).removeClass('active');
            $(this).text('See more');
        } else {
            $(this).addClass('active');
            $(this).text('Collapse');
        }
        e.preventDefault();
    });
    $('.forum-see-more-top').click(function (e) {
        e.preventDefault();
        $(this).closest('div.forum-row-wrapper').find('.forum-left-col').height('auto');
        $(this).closest('div.forum-row-wrapper').find('.forum-right-col').height('auto');
        $(this).closest('div.forum-row-wrapper').find('.forum-more-top').slideToggle();
        $(this).closest('div.forum-row-wrapper').find('.forum-more-bottom').slideToggle('', function () {
            $.fn.matchHeight._maintainScroll = true;
            $('.forum-match-height').matchHeight();
        });
        $(this).closest('div.forum-row-wrapper').find('.forum-excerpt').slideToggle();
        $(this).closest('div.forum-row-wrapper').find('.forum-right-status').slideToggle();
        $(this).closest('div.forum-row-wrapper').find('.forum-post').toggleClass('expanded');
        $(this).closest('div.forum-row-wrapper').find('.forum-right-col').toggleClass('expanded');
        $(this).closest('div.forum-row-wrapper').find('.forum-see-more').toggleClass('active');
        $(this).closest('div.forum-row-wrapper').find('.forum-see-more').text('See more');
    });
    //forum tabs
    $(".forum-posts-tab").tabs();
    $(".forum-posts-tab").on("tabsactivate", function (event, ui) {
        $.fn.matchHeight._maintainScroll = true;
        $('.forum-match-height').matchHeight();
    });
    //forum sidebar collapse
    $('.forum-right-toggle-content a.toggle-content').click(function (e) {
        e.preventDefault();
        $(this).parent('div').find('.members-expand').slideToggle();
        if ($(this).parent('div').find('.members-expand').hasClass('active')) {
            $(this).parent('div').find('.members-expand').removeClass('active');
            $(this).text('Expand');
            $(this).removeClass('active');
        } else {
            $(this).parent('div').find('.members-expand').addClass('active');
            $(this).text('Collapse');
            $(this).addClass('active');
        }
    });
    //TinyMCE Forum New Topic
    if (typeof (tinyMCE) !== "undefined") {
        tinymce.init(
            {
                selector: 'textarea.tinymce_textarea',
                plugins: 'lists, link, image',
                branding: false,
                menubar: false,
                toolbar: 'bold link alignjustify indent bullist numlist image removeformat'
            });
    }

    //New FOrum Tag Manager
    if ($.isFunction($.fn.tagsManager)) {
        var tagID = $(".tagsmanager").tagsManager({ tagsContainer: '#ForumTags', tagCloseIcon: 'X' });

        $('#AddForumTag').click(function () {
            $(tagID).tagsManager('pushTag', $(tagID).val());
        });
    }

    //limit checkbox field to three
    var limit = 3;
    $('.limit-3').on('change', function () {
        if ($('.limit-3:checked').length > limit) {
            this.checked = false;
            $(this).parent().parent().removeClass("active");
        }
    });

});

// SSP Private Forum, comment count
var nIntervId;
nIntervId = setInterval(function () { CA_GetCommentCountAndSetToSolveSpaceTop() }, 1000);

var CA_IsDirty = false;
//START JS FOR SS EXERCISE
$(document).ready(function () {
    "use strict";
    try {
        autosize($('textarea'));
    }catch(e){}
    $('[rel="popover"]').popover({
        html: true,
        placement: 'bottom',
        content: function () {
            return $($(this).data('contentwrapper')).html();
        }
    });


    //below fixes a Bootstrap bug.  If removed, you have to click twice to show the share popovers
    $('[rel="popover"]').on('hidden.bs.popover', function (e) {
        $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };
    });


    $('[rel="popover"]').on('show.bs.popover', function (e) {
        $("#share_dropdown").html($(this).attr('sharevia-title'));
    });

    if ($.isFunction($.fn.rating)) {
        $(".cac-rating").rating({
            showClear: false,
            showCaption: false,
            emptyStar: '<i class="fa fa-star"></i>',
            filledStar: '<i class="fa fa-star"></i>',
            step: 1
        });
    }

   // Sachin: to warn user if they leave the solvespace without saving it , this will be called only if the 
    // url belongs to Solvespace pages
    if ((window.location.href.toLowerCase().indexOf('/surveys/') > -1) && (window.location.href.toLowerCase().indexOf('summary')==-1)) {
        $('body').on('change keyup keydown', 'input:checkbox, input:radio, input:text, textarea, select', function (e) {
            CA_IsDirty = true;
        });

        $(window).on('beforeunload', function () {
             if (CA_IsDirty == true) {
                return 'You have unsaved changes on this page. Are you sure you want to leave? To save Changes, choose STAY and click the next button below.';
            }
        });
    }

    // Sachin : Session Timeout init
    /*$.ajax({
        url: "/DesktopModules/ClearAction_SummaryActions/rh.asmx/GetUserInactiveInfo",
        dataType: "json", type: "POST", contentType: "application/json; charset=utf-8",
        success: function (d) {
            CA_CheckUserInactivity(d.d.UserSessionInMin, d.d.PromptBeforeInMin);
        }
    });*/
});

function CA_CheckUserInactivity(UserSessionInMin, PromptBeforeInMin){
    var PromptBeforeInSec = PromptBeforeInMin * 60;
     try {
         $(document).idle({
             onIdle: function () {
                 $.timeoutDialog({ timeout: PromptBeforeInSec, countdown: PromptBeforeInSec, logout_redirect_url: '?ctl=logoff', restart_on_yes: true, keep_alive_url: '/KeepAlive.aspx', title: 'Your login is about to time out' });
             },
             idle: ((1000 * 60 * (UserSessionInMin - PromptBeforeInMin)))
         });
    }
    catch (e) { }
}
function closeShare() {
    "use strict";
    $('[rel="popover"]').popover('hide');
}
//END JS FOR SS EXERCISE




//START PROFILE SETUP JS
$(document).ready(function () {

    "use strict";

    $('#linkedin_button').click(function () {

        $('#firstname').val('Cindy');
        $('#lastname').val('Rhodes');
        $('#title').val('Campaign Manager');
        $('#company').val('RightNow Software');
        $('#location').val('Sunnyvale, CA');


        $('#bio').val('Driven marketer, focused on strategic implementation, project management, creative direction, and branding. Thriving in creative and collaborative environments where a flurry of brainstorming turns into widely creative marketing campaigns. \r\n\r\nSpecialties: Marketing and Brand Strategy, Campaign Management, Website Strategy and Design, New Product Launches, ROI Analysis, Event Marketing, Traditional and Digital Advertising Strategy');
        $('#linkedin').val('https://www.linkedin.com/in/cindyrhodes');
        $('#twitter').val('@cindyrhodes');
        $('#facebook').val('');
        $('#education').val('University of Texas, Austin\r\nBS in Marketing');
        $('#work').val("SeniorCampaign Manager\r\nCompany Name\r\nRightNow Software\r\nDates Employed\r\nJan 2015 – Present\r\nEmployment Duration\r\n2 yr 10 mo\r\nLocation\r\nSunnyvale, CA\r\nResponsible for driving key campaigns from concept through release, working alongside Field and Global Marketing, Sales, Product Marketing and Operations. Coordination of targeting, planning, strategy, execution, campaign analysis, business insights, demand gen mix, cross-team collaboration, virtual team leadership and leadership reviews, and KPI and budget management.\r\n\r\nMarketing Technology Project Manager\r\nCompany Name\r\nSnapStream\r\nDates Employed\r\nMay 2013 – Dec 2014\r\nEmployment Duration\r\n1 yr 8 mo\r\nLocation\r\nAustin, Texas Area");
        $('#memorable').val('The birth of my twin sons.');
        $('#wish').val('Better insight on how all divisions within our department work. ');
        $('#collaborate').val('Love to collaborate with anyone and everyone who is responsible for customer success. Especially like working with the copywriters and the analytics groups.');
        $('#associations').val('AMA');
        $('#honors').val('PR News’ Social Media Icon Awards -Twitter | Best Marketing Campaign Honorable Mention');
        return false;

    });

    
    $('.btn_done').unbind().click(function () {


        $('#collapseOne, #collapseTwo, #collapseThree, #collapseFour, #collapseFive').not($(this).attr('href')).collapse('hide');


        $($(this).attr('href')).collapse('show');
    });


});

$(document).ready(function () {
    "use strict";
    $('input').click(function () {
        $('input:not(:checked)').parent().parent().removeClass("active");
        $('input:checked').parent().parent().addClass("active");
    });

    $('.profile_setup .setup3 .checkbox_group a').unbind().click(function () {

        if ($(this).hasClass('select_all')) {
            $(this).html('(unselect all)');
            $(this).removeClass('select_all').addClass('unselect_all');
            $("[name='" + this.id + "']").prop('checked', true);
            $("[name='" + this.id + "']").parent().parent().addClass("active");
        }
        else {
            $(this).html('(select all)');
            $(this).removeClass('unselect_all').addClass('select_all');
            $("[name='" + this.id + "']").prop('checked', false);
            $("[name='" + this.id + "']").parent().parent().removeClass("active");
        }

        return false;
    });

});
//END PROFILE SETUP JS
// RG: hide email and name from the feedback form. Also change the tab caption and message textbox
$(document).ready(function () {
    $("#contactform #name").parent().hide();
    $("#contactform #email").parent().hide();
    $("#contactform p.disclaimer").hide();
    $("#contactform textarea.message").attr('rows', '12');
    $("#contactable #contactable_inner p").text("Feedback");
});

//RG : This function will set the tooltip
//el - element reference. This is the element that the tooltip will point to
//pos - position of tooltip relative to el: top | bottom | left | right
//trigg - the trigger that will cause tooltip to show: click | hover | focus
function setTooltip(el, pos, trigg, displayImmediate) {
    // get tooltip content from the next container with class ".popup" relative to the element (el)
    var ttId = el.attr('tooltip-id');
    var content = $("#" + ttId).html();
    var tt = el.tooltip(
        {
            title: content,
            placement: pos,
            html: true,
            trigger: trigg
        }
    );
    if (displayImmediate == true) {
        tt.show();
    }
    return tt;
}



//Appinsights tracking code: Ajit 
//dated : 07th March

$(document).ready(function () {


    //Add try- catch block so it will not generated any exception if module is not activated.
    try
    {
        var appInsightsUserName = dnn.vars.dnn_current_userid;//get user logged in ID 

        appInsights.setAuthenticatedUserContext(appInsightsUserName.replace(/[,;=| ]+/g, "_"));
        window.appInsights = appInsights;
        appInsights.trackPageView();


    }
catch(e) {
         
    }

   
   
     
});

//RG: filter buttons for insights and digital events do not use <ul><li><a> buttons. 
//The backend code generates input buttons intead. HAve to mimic the a (link) behavior on mouse over. 
$(".filter-bar ul.button-group li").mouseover(function () {
    if (!$(this).hasClass("active")) {
        $(this).find("input.btnctrl").css("color", "#ffffff");
    }
});
$(".filter-bar ul.button-group li").mouseout(function () {
    if (!$(this).hasClass("active")) {
        $(this).find("input.btnctrl").css("color", "#5093c3;");
    }
});

    function CA_GetCommentCountAndSetToSolveSpaceTop() {
        var CA_SS_SSID = GetCurrentSSID();
        if (CA_SS_SSID != 0) {

            $.ajax({
                url: "/DesktopModules/ClearAction_SummaryActions/rh.asmx/GetForumCommentCount",
                data: JSON.stringify({ SSID: CA_SS_SSID }),
                dataType: "json", type: "POST", contentType: "application/json; charset=utf-8",
                success: function (d) {
                    $(".comment_count").text(d.d);
                    $("#CA_dvCommentCount").text(d.d);
                    clearInterval(nIntervId);
                }
            });

        }
    }

function GetCurrentSSID() {

    var CurrentSSIDValue = 0;
    var elementList = document.querySelectorAll('[data-af-field]');

    for (var i = 0; i < elementList.length; i++) {
        var dataValue = elementList[i].getAttribute("data-af-field")
        if (dataValue == "SSID") {
            CurrentSSIDValue = elementList[i].getAttribute("value")
        }
    }
    return CurrentSSIDValue
}