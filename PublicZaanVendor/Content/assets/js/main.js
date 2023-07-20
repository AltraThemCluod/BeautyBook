$(document).ready(function () {
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

	
	$('#slick1').slick({
		dots: true,
		prevArrow: '<button class="button.slick-next.slick-arrow" src="../Content/assets/images/NextArrow.svg" width="60"/>',
		nextArrow: '<img class="product-next-arrow" src="../Content/assets/images/PrevArrow.svg" width="60"/>'
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


function startProduct() {
	$("#startProduct").modal("show");
}