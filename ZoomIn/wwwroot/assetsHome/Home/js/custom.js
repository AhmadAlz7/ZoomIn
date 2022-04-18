/******************************************
    Version: 1.0
/****************************************** */

(function($) {
    "use strict";

	// Scroll to top  		
	if ($('#scroll-to-top').length) {
		var scrollTrigger = 100, // px
			backToTop = function () {
				var scrollTop = $(window).scrollTop();
				if (scrollTop > scrollTrigger) {
					$('#scroll-to-top').addClass('show');
				} else {
					$('#scroll-to-top').removeClass('show');
				}
			};
		backToTop();
		$(window).on('scroll', function () {
			backToTop();
		});
		$('#scroll-to-top').on('click', function (e) {
			e.preventDefault();
			$('html,body').animate({
				scrollTop: 0
			}, 700);
		});
	};
	
	// Banner 
	
    $('.heading').height( $(window).height() );
	$('.parallaxie').parallaxie();
	
	
	
    // LOADER
    $(window).load(function() {
        $("#preloader").on(500).fadeOut();
        $(".preloader").on(600).fadeOut("slow");
	});


	// Gallery Filters - other
	var Container = $('.container');
	Container.imagesLoaded(function () {
		var portfolio = $('.gallery-menu-other');
		portfolio.on('click', 'button', function () {
			$(this).addClass('active').siblings().removeClass('active');
			var filterValue = $(this).attr('data-filter');
			$grid.isotope({
				filter: filterValue
			});
		});
		var $grid = $('.gallery-list-other').isotope({
			itemSelector: '.gallery-grid-other'
		});
	});


	// Gallery Filters - Main Gallery
	var Container = $('#gallery');
	Container.imagesLoaded(function () {

		// quick search regex
		var qsRegex;
		var buttonFilter;

		// init Isotope
		var $grid = $('.gallery-list-main').isotope({
			itemSelector: '.gallery-grid-main',
			filter: function () {
				var $this = $(this);
				var searchResult = qsRegex ? $this.text().match(qsRegex) : true;
				var buttonResult = buttonFilter ? $this.is(buttonFilter) : true;
				return searchResult && buttonResult;
			}/*,
			getSortData: {
				itemPopularityClass: '.itemPopularityClass parseInt',
				itemRecencyClass: function( $elem ) {
					return $elem.find('.itemRecencyClass').attr('data-time');
				},
				itemPriceClass: '.itemPriceClass parseInt'
			},
			sortBy: itemPopularityClass,
			sortAscending: true*/
		});

		// use value of search field to filter
		var $quicksearch = $('#searchInput').keyup(debounce(function () {
			qsRegex = new RegExp($quicksearch.val(), 'gi');
			$grid.isotope();
		}));

		// bind category button click - filter by category
		$('#categoryFilterGroup').on('click', 'button', function () {
			buttonFilter = $(this).attr('data-filter');
			$grid.isotope();
		});

		// bind sort button click - sort by
		$('#gallerySorts').on('click', 'button', function () {

			var sortByValue = $(this).attr('data-sort-by');
			var direction = $(this).attr('data-sort-direction');
			var isAscending = (direction == 'asc');
			$grid.isotope({ sortBy: sortByValue, sortAscending: isAscending });

			var newDirection = (isAscending) ? 'desc' : 'asc';
			$(this).attr('data-sort-direction', newDirection);

			var icon = $(this).find('.fas');
			icon.toggleClass('fa-sort-up fa-sort-down');
		});


		// change active class on buttons
		$('.gallery-menu-2').each(function (i, buttonGroup) {
			var $buttonGroup = $(buttonGroup);
			$buttonGroup.on('click', 'button', function () {
				$(this).addClass('active').siblings().removeClass('active');
			});
		});

		// change active class on buttons
		$('.gallery-menu-3').each(function (i, buttonGroup) {
			var $buttonGroup = $(buttonGroup);
			$buttonGroup.on('click', 'button', function () {
				$(this).addClass('active').siblings().removeClass('active');
			});
		});


		// debounce so filtering doesn't happen every millisecond
		function debounce(fn, threshold) {
			var timeout;
			threshold = threshold || 100;
			return function debounced() {
				clearTimeout(timeout);
				var args = arguments;
				var _this = this;
				function delayed() {
					fn.apply(_this, args);
				}
				timeout = setTimeout(delayed, threshold);
			};
		}

	});

	//Like
	$('#userLikeClick').click(function () {
		$('#likeIcon').toggleClass('far fa-heart text-muted fas fa-heart text-danger');
		var itemId = $(this).attr('itemId');
		var userId = $(this).attr('userId');
		$.ajax({
			type: "POST",
			url: "/Home/LikeItem",
			data: { itemId: itemId, userId: userId }
		});
    });
	
	
    // CONTACT
    jQuery(document).ready(function() {
        $('#contactform').submit(function() {
            var action = $(this).attr('action');
            $("#message").slideUp(750, function() {
                $('#message').hide();
                $('#submit')
                    .after('<img src="images/ajax-loader.gif" class="loader" />')
                    .attr('disabled', 'disabled');
                $.post(action, {
                        first_name: $('#first_name').val(),
                        last_name: $('#last_name').val(),
                        email: $('#email').val(),
                        phone: $('#phone').val(),
                        select_service: $('#select_service').val(),
                        select_price: $('#select_price').val(),
                        comments: $('#comments').val(),
                        verify: $('#verify').val()
                    },
                    function(data) {
                        document.getElementById('message').innerHTML = data;
                        $('#message').slideDown('slow');
                        $('#contactform img.loader').fadeOut('slow', function() {
                            $(this).remove()
                        });
                        $('#submit').removeAttr('disabled');
                        if (data.match('success') != null) $('#contactform').slideUp('slow');
                    }
                );
            });
            return false;
        });
    });
	
	// Map
	$(document).ready(function(){
        $('.gmaps').gmaps();
      });
	  
	
	

})(jQuery);
