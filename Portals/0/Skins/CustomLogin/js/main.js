$(window).load(function() {
	/*$('.expand a').click(function(e){
		$('.expand_dtls').slideToggle();
	})*/
	$('.menu a ').click(function(e){
		e.preventDefault();
		$('.menu-toggle').slideToggle();
	});

	//for popup
	$('.roles h4, .community_calls h4').click(function(e) {
		e.preventDefault();
		var tar ='.'+$(this).attr('id');
		$(tar).lightbox_me({
			centered:true,
		});	
	});
		
	//for menu            
	$('.menu a, span.close').click(function(e){
		e.preventDefault();
		$('body').toggleClass('menu_open');
	});
	
	$('.expand_dtls').clone().appendTo('.mob_expand');
	
	//for tooltip
	$('.blogs h4').click(function(e){
		e.preventDefault();
		$('body').toggleClass('blog_open')
	});
	$('.connections h4.mine').click(function(e){
		e.preventDefault();
		$('body').toggleClass('roles_open')
	});
	
	//equal height
	$('.solve-spaces-middle-nav ul li').matchHeight();
	$('.forum-intro-match-height').matchHeight();
	$('.forum-match-height').matchHeight();
	$('.insights-match-height').matchHeight();
	$('.login .left,.login .right').matchHeight();
	
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
	  var $buttonGroup = $( buttonGroup );
	  $buttonGroup.on( 'click', 'li', function(e) {
		$buttonGroup.find('.active').removeClass('active');
		$( this ).addClass('active');
	  });
	});
	//solve spaces sidebar toggle
	$('.other-solve-space-wrapper .other-solve-space-title').click(function(e){
		e.preventDefault();
		$(this).toggleClass('active');
		$(this).next('.connection_cnt').stop().slideToggle();
	});
	//custom select menus (hide first element on open, restore on close)
 //   $(".forum-select, .insights-select").selectmenu().on("selectmenuopen", function (event, ui) {
 //       debugger;
	//	$('.ui-selectmenu-menu li.ui-menu-item:first-child').hide();
	//} ).on( "selectmenuclose", function( event, ui ) {
	//	$('.ui-selectmenu-menu li.ui-menu-item:first-child').show();
	//} );
 //   $(".forum-select, .insights-select").on("selectmenuselect", function (event, ui) {
 //       debugger;
 //   });

	//forum slide toggles
	$('.forum-see-more').click(function(e){
		$(this).closest('div.forum-row-wrapper').find('.forum-left-col').height('auto');
		$(this).closest('div.forum-row-wrapper').find('.forum-right-col').height('auto');
		$(this).parent('div').find('.forum-more-top').slideToggle();
		$(this).parent('div').find('.forum-more-bottom').slideToggle('', function(){
			$.fn.matchHeight._maintainScroll = true;
			$('.forum-match-height').matchHeight();
		});
		$(this).parent('div').find('.forum-excerpt').slideToggle();
		$(this).closest('div.forum-row-wrapper').find('.forum-right-status').slideToggle();
		$(this).parent('div').toggleClass('expanded');
		$(this).closest('div.forum-row-wrapper').find('.forum-right-col').toggleClass('expanded');
		if($(this).hasClass('active')) {
			$(this).removeClass('active');
			$(this).text('See more');
		} else {
			$(this).addClass('active');
			$(this).text('Collapse');
		}
		e.preventDefault();
	});
	$('.forum-see-more-top').click(function(e){
		e.preventDefault();
		$(this).closest('div.forum-row-wrapper').find('.forum-left-col').height('auto');
		$(this).closest('div.forum-row-wrapper').find('.forum-right-col').height('auto');
		$(this).closest('div.forum-row-wrapper').find('.forum-more-top').slideToggle();
		$(this).closest('div.forum-row-wrapper').find('.forum-more-bottom').slideToggle('', function(){
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
	$( ".forum-posts-tab" ).on( "tabsactivate", function( event, ui ) {
		$.fn.matchHeight._maintainScroll = true;
		$('.forum-match-height').matchHeight();
    });
	//forum sidebar collapse
	$('.forum-right-toggle-content a.toggle-content').click(function(e){
		e.preventDefault();
		$(this).parent('div').find('.members-expand').slideToggle();
		if($(this).parent('div').find('.members-expand').hasClass('active')){
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
	if (typeof(tinyMCE) !== "undefined") {
    	tinymce.init(
			{selector:'textarea.tinymce_textarea',
			 plugins: 'lists, link, image',
			 branding:false, 
			 menubar:false,
			 toolbar: 'bold link alignjustify indent bullist numlist image removeformat'
			});
	}
	
	//New FOrum Tag Manager
	if ( $.isFunction($.fn.tagsManager) ) {
    	var tagID = $(".tagsmanager").tagsManager({tagsContainer: '#ForumTags', tagCloseIcon: 'X'});
		
		$('#AddForumTag').click(function(){
			$(tagID).tagsManager('pushTag',$(tagID).val());
		});
	}
	
	
});

//START JS FOR SS EXERCISE
$(document).ready(function () {
	"use strict";

	autosize($('textarea'));

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
	
	if ( $.isFunction($.fn.rating) ) {
    	$(".cac-rating").rating({
			showClear:false,
			showCaption:false,
			emptyStar: '<i class="fa fa-star"></i>',
			filledStar: '<i class="fa fa-star"></i>',
			step: 1
		});
	}

});

function closeShare(){
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
	
	$('#ex1').slider({
	formatter: function(value) {
		return 'Current value: ' + value;
	}});
	
	$('#ex2').slider({
	formatter: function(value) {
		return 'Current value: ' + value;
	}});
	
	$('.btn_done').unbind().click(function(){
		
		
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
	
	$('.profile_setup .setup3 .checkbox_group a').unbind().click(function(){
		
		if($(this).hasClass('select_all')) {
			$(this).html('(unselect all)');
			$(this).removeClass('select_all').addClass('unselect_all');
			$("[name='"+this.id+"']").prop('checked', true);
			$("[name='"+this.id+"']").parent().parent().addClass("active");
		}
		else
		{
			$(this).html('(select all)');
			$(this).removeClass('unselect_all').addClass('select_all');
			$("[name='"+this.id+"']").prop('checked', false);
			$("[name='"+this.id+"']").parent().parent().removeClass("active");
		}
		
		return false;	
	});
	
});
//END PROFILE SETUP JS