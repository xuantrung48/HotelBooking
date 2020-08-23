/*  ---------------------------------------------------
  Template Name: Hiroto
  Description:  Hiroto Hotel HTML Template
  Author: Colorlib
  Author URI: https://colorlib.com
  Version: 1.0
  Created: Colorlib
---------------------------------------------------------  */

'use strict';

(function ($) {

    /*------------------
        Preloader
    --------------------*/
    $(window).on('load', function () {
        $(".loader").fadeOut();
        $("#preloder").delay(200).fadeOut("slow");
    });

    /*------------------
        Background Set
    --------------------*/
    $('.set-bg').each(function () {
        var bg = $(this).data('setbg');
        $(this).css('background-image', 'url(' + bg + ')');
    });

    //Canvas Menu
    $(".canvas__open").on('click', function () {
        $(".offcanvas-menu-wrapper").addClass("active");
        $(".offcanvas-menu-overlay").addClass("active");
    });

    $(".offcanvas-menu-overlay").on('click', function () {
        $(".offcanvas-menu-wrapper").removeClass("active");
        $(".offcanvas-menu-overlay").removeClass("active");
    });

    /*------------------
		Navigation
	--------------------*/
    $(".menu__class").slicknav({
        appendTo: '#mobile-menu-wrap',
        allowParentLinks: true
    });

    /*--------------------------
        Gallery Slider
    ----------------------------*/
    $(".gallery__slider").owlCarousel({
        loop: true,
        margin: 10,
        items: 4,
        dots: false,
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true,
        responsive: {
            992: {
                items: 4
            },
            768: {
                items: 3
            },
            576: {
                items: 2
            },
            0: {
                items: 1
            }
        }
    });

    /*--------------------------
        Room Pic Slider
    ----------------------------*/
    $(".room__pic__slider").owlCarousel({
        loop: true,
        margin: 0,
        items: 1,
        dots: false,
        nav: true,
        navText: ["<i class='arrow_carrot-left'></i>", "<i class='arrow_carrot-right'></i>"],
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: false
    });

    /*--------------------------
        Room Details Pic Slider
    ----------------------------*/
    $(".room__details__pic__slider").owlCarousel({
        loop: true,
        margin: 10,
        items: 2,
        dots: false,
        nav: true,
        navText: ["<i class='arrow_carrot-left'></i>", "<i class='arrow_carrot-right'></i>"],
        autoHeight: false,
        autoplay: false,
        mouseDrag: false,
        responsive: {
            576: {
                items: 2
            },
            0: {
                items: 1
            }
        }
    });
    
    /*--------------------------
        Testimonial Slider
    ----------------------------*/
    var testimonialSlider = $(".testimonial__slider");
    testimonialSlider.owlCarousel({
        loop: true,
        margin: 30,
        items: 1,
        dots: true,
        nav: true,
        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: true,
        mouseDrag: false,
        onInitialized: function(e) {
        	    var a = this.items().length;
                $("#snh-1").html("<span>01</span><span>" + "0" + a + "</span>");
                var presentage = Math.round((100 / a));
                $('.slider__progress span').css("width", presentage + "%");
                
            }
        }).on("changed.owl.carousel", function(e) {
            var b = --e.item.index, a = e.item.count;
            $("#snh-1").html("<span> "+ "0" +(1 > b ? b + a : b > a ? b - a : b) + "</span><span>" + "0" + a + "</span>");

            var current = e.page.index + 1;
            var presentage = Math.round((100 / e.page.count) * current);
            $('.slider__progress span').css("width", presentage + "%");
    });
    
    
    /*--------------------------
        Logo Slider
    ----------------------------*/
    $(".logo__carousel").owlCarousel({
        loop: true,
        margin: 100,
        items: 5,
        dots: false,
        smartSpeed: 1200,
        autoHeight: false,
        autoplay: false,
        responsive: {
            992: {
                items: 5
            },
            768: {
                items: 3
            },
            320: {
                items: 2
            },
            0: {
                items: 1
            }
        }
    });

    /*--------------------------
        Select
    ----------------------------*/
    $("select").niceSelect();
    

    /*--------------------------
        Datepicker
    ----------------------------*/
    var today = new Date(); 
    var idd = today.getDate(); 
    if (idd < 10)
        idd = '0' + idd;
    var imm = today.getMonth() + 1; 
    if (imm < 10)
        imm = '0' + imm;
    var iyyyy = today.getFullYear();

    today = idd + '-' + imm + '-' + iyyyy; 

    $(".check__in").val(today);

    var tomorrow = new Date((new Date()).valueOf() + 1000 * 3600 * 24);

    var odd = tomorrow.getDate();
    if (odd < 10)
        odd = '0' + odd;
    var omm = tomorrow.getMonth() + 1;
    if (omm < 10)
        omm = '0' + omm;
    var oyyyy = tomorrow.getFullYear();
    tomorrow = odd + '-' + omm + '-' + oyyyy;

    $(".check__in").val(today);
    $(".check__out").val(tomorrow);

    $(".check__in").datepicker({
        dateFormat: 'dd-mm-yy',
        minDate: 0
    });

    $(".check__out").datepicker({
        dateFormat: 'dd-mm-yy',
        minDate: 1
    });

    $(".check__in").on('change', function () {
        var checkInDate = new Date(convertDateFormat($(".check__in").val()));
        var checkOutDate = new Date(convertDateFormat($(".check__out").val()));
        var minCheckOutDate = new Date((checkInDate).valueOf() + 1000 * 3600 * 24);
        var dateString;
        if (checkInDate >= checkOutDate) {
            var dd = minCheckOutDate.getDate();
            if (dd < 10)
                dd = '0' + dd;
            var mm = minCheckOutDate.getMonth() + 1;
            if (mm < 10)
                mm = '0' + mm;
            var yyyy = minCheckOutDate.getFullYear();

            dateString = dd + '-' + mm + '-' + yyyy;

            $(".check__out").val(dateString);
        }

        $(".check__out").datepicker("option", {
            minDate: minCheckOutDate
        });

    })

})(jQuery);