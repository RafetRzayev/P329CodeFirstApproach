$(document).ready(function () {
    var skip = 4;
    // HEADER

    $(document).on('click', '#loadMore', function () {
        var productCount = $("#productCount").val();

        $.ajax({
            url: "/product/loadProducts?skip="+skip,
            type: "GET",
            success: function (response) {
                $("#productRow").append(response);
                skip += 4;

                if (skip >= productCount)
                    $("#loadMore").remove();
            },
            error: function (xhr) {

            }
        });
    })


    $(document).on('click', '#deleteCategory', function () {
        var categoryId = $(this).prev().val();

        $.ajax({
            url: "/adminpanel/category/delete/" + categoryId,
            type: "POST",
            success: function (response) {
                console.log(response);
            },
            error: function (xhr) {

            }
        });
    })

    $(document).on('click', '#AddToBasket', function () {

        var productId = $(this).prev().val();
        $.ajax({
            url: "/home/addtobasket?id=" + productId,
            type: "GET",
            success: function (response) {
                $("#basketPartial").empty();
                $("#basketPartial").append(response);
            },
            error: function (xhr) {

            }
        });
    })

    $(document).on('keyup', '#input-search', function () {
        var searchedProduct = $(this).val();
        $.ajax({
            url: "/home/search?searchedProduct=" + searchedProduct,
            type: "GET",
            success: function (response) {
                $("#searchedProduct li").slice(1).remove();

                $("#searchedProduct").append(response);
            },
            error: function (xhr) {

            }
        });
    })

    $(document).on('click', '#search', function () {
        $(this).next().toggle();
    })

    $(document).on('click', '#mobile-navbar-close', function () {
        $(this).parent().removeClass("active");

    })
    $(document).on('click', '#mobile-navbar-show', function () {
        $('.mobile-navbar').addClass("active");

    })

    $(document).on('click', '.mobile-navbar ul li a', function () {
        if ($(this).children('i').hasClass('fa-caret-right')) {
            $(this).children('i').removeClass('fa-caret-right').addClass('fa-sort-down')
        }
        else {
            $(this).children('i').removeClass('fa-sort-down').addClass('fa-caret-right')
        }
        $(this).parent().next().slideToggle();
    })

    // SLIDER

    $(document).ready(function(){
        $(".slider").owlCarousel(
            {
                items: 1,
                loop: true,
                autoplay: true
            }
        );
      });

    // PRODUCT

    $(document).on('click', '.categories', function(e)
    {
        e.preventDefault();
        $(this).next().next().slideToggle();
    })

    $(document).on('click', '.category li a', function (e) {
        e.preventDefault();
        let category = $(this).attr('data-id');
        let products = $('.product-item');
        
        products.each(function () {
            if(category == $(this).attr('data-id'))
            {
                $(this).parent().fadeIn();
            }
            else
            {
                $(this).parent().hide();
            }
        })
        if(category == 'all')
        {
            products.parent().fadeIn();
        }
    })

    // ACCORDION 

    $(document).on('click', '.question', function()
    {   
       $(this).siblings('.question').children('i').removeClass('fa-minus').addClass('fa-plus');
       $(this).siblings('.answer').not($(this).next()).slideUp();
       $(this).children('i').toggleClass('fa-plus').toggleClass('fa-minus');
       $(this).next().slideToggle();
       $(this).siblings('.active').removeClass('active');
       $(this).toggleClass('active');
    })

    // TAB

    $(document).on('click', 'ul li', function()
    {   
        $(this).siblings('.active').removeClass('active');
        $(this).addClass('active');
        let dataId = $(this).attr('data-id');
        $(this).parent().next().children('p.active').removeClass('active');

        $(this).parent().next().children('p').each(function()
        {
            if(dataId == $(this).attr('data-id'))
            {
                $(this).addClass('active')
            }
        })
    })

    $(document).on('click', '.tab4 ul li', function()
    {   
        $(this).siblings('.active').removeClass('active');
        $(this).addClass('active');
        let dataId = $(this).attr('data-id');
        $(this).parent().parent().next().children().children('p.active').removeClass('active');

        $(this).parent().parent().next().children().children('p').each(function()
        {
            if(dataId == $(this).attr('data-id'))
            {
                $(this).addClass('active')
            }
        })
    })

    // INSTAGRAM

    $(document).ready(function(){
        $(".instagram").owlCarousel(
            {
                items: 4,
                loop: true,
                autoplay: true,
                responsive:{
                    0:{
                        items:1
                    },
                    576:{
                        items:2
                    },
                    768:{
                        items:3
                    },
                    992:{
                        items:4
                    }
                }
            }
        );
      });

      $(document).ready(function(){
        $(".say").owlCarousel(
            {
                items: 1,
                loop: true,
                autoplay: true
            }
        );
      });
})