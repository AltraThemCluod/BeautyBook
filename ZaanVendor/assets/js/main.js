$(document).ready(function(){
	var showHeaderAt = 150;
	var win = $(window), body = $('body');
	if(win.width() > 400){
		win.on('scroll', function(e){
			if(win.scrollTop() > showHeaderAt) {
				body.addClass('fixed');
			}
			else {
				body.removeClass('fixed');
			}
		});
	}


	// ================================================ testimonial carousel ================================================ //

	$('.carousel').slick({
		slidesToShow: 1,
		dots:false,
		prevArrow: '<img class="prev-arrow" src="./assets/images/PrevArrow.png" width="30"/>',
    	nextArrow: '<img class="next-arrow" src="./assets/images/NextArrow.png" width="30"/>'
	});

	$('.product-carousel').slick({
		slidesToShow: 1,
		dots:true,
		prevArrow: '<img class="product-prev-arrow" src="./assets/images/PrevArrow.png" width="30"/>',
    	nextArrow: '<img class="product-next-arrow" src="./assets/images/NextArrow.png" width="30"/>'
	});

	$(document).on("scroll", function(){
     	if
          ($(document).scrollTop() > 86){
     	  $("header").addClass("shrink");
     	}
     	else
     	{
     		$("header").removeClass("shrink");
     	}
     });

});