$(window).load(function() {
	
	
if($('body').hasClass('value-exchange')){	
	function parallaxZoom(id) {
		if($(id).isOnScreen()) {
			var size = $(id).css("background-size");
			console.log(size);
			if(size == 'cover'){
				size = 100;
				console.log(size);
			} else if(size >= '120') {
				//do nothing already too large
			} else {
				var newSize = size.replace('%', '');
				size = newSize*1.001;
			}
			$(id).css({
				backgroundSize: size+'%'
			});
		} else {
			$(id).css({
				backgroundSize: "cover"
			});
		}
	}
	function parallaxZoomReset(id) {
		$(id).css({
			backgroundSize: "cover"
		});
	}
	$(window).scroll(function() {
		if($(window).width()>=1425){
			parallaxZoom('#zoom-one');
			parallaxZoom('#zoom-two');
			parallaxZoom('#zoom-three');
			parallaxZoom('#zoom-four');
			parallaxZoom('#zoom-five');
		}
	});
	$(window).resize(function() {
		parallaxZoomReset('#zoom-one');
		parallaxZoomReset('#zoom-two');
		parallaxZoomReset('#zoom-three');
		parallaxZoomReset('#zoom-four');
		parallaxZoomReset('#zoom-five');
		if($(window).width()>=944){
			$('.mobile-nav').hide();
			$('.mobile-trigger').removeClass('active');
		}
	});
	$.fn.isOnScreen = function(){
		var viewport = {};
		viewport.top = $(window).scrollTop();
		viewport.bottom = viewport.top + $(window).height();
		var bounds = {};
		bounds.top = this.offset().top;
		bounds.bottom = bounds.top + this.outerHeight();
		return ((bounds.top <= viewport.bottom) && (bounds.bottom >= viewport.top));
	};
	}
	//masonry
	$('.grid').masonry({
		// set itemSelector so .grid-sizer is not used in layout
		itemSelector: '.grid-item',
		// use element for option
		columnWidth: '.grid-sizer',
		percentPosition: true
	});
	/*mobile menu*/
	$('.mobile-trigger').click(function(e){
		e.preventDefault();
		$('.mobile-nav').slideToggle();
		$(this).toggleClass('active');
	});
	/*footer menu*/
	$('.footer-menu').click(function(e){
		e.preventDefault();
		$('.footer-menu-wrapper').slideToggle();
		$(this).toggleClass('active');
	});
});