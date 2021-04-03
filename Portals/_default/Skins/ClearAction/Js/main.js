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

    setTimeout(function () { $('.solve-spaces-grid li').matchHeight(); }, 1000);
    
	//filtering
	// init Isotope
	var $grid = $('.solve-spaces-grid').isotope({
	  // options
	});
	// filter items on button click
    $('.filter-button-group').on('click', 'a', function (e) {
	  var filterValue = $(this).attr('data-filter');
      $grid.isotope({ filter: filterValue });
      return true;
	});
	// change is-checked class on buttons
	$('.button-group').each( function( i, buttonGroup ) {
	  var $buttonGroup = $( buttonGroup );
      $buttonGroup.on('click', 'li', function (e) {
		$buttonGroup.find('.active').removeClass('active');
        $(this).addClass('active');
        return true;
	  });
	});
	//solve spaces sidebar toggle
	$('.other-solve-space-wrapper .other-solve-space-title').click(function(e){
		e.preventDefault();
		$(this).toggleClass('active');
		$(this).next('.connection_cnt').stop().slideToggle();
	});
});