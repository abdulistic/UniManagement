    $(document).ready(function () {

        $(".largeGrid").click(function () {
            $(this).find('a').addClass('active');
            $('.smallGrid a').removeClass('active');

            $('.product').addClass('large').each(function () {
            });
            setTimeout(function () {
                $('.info-large').show();
            }, 200);
            setTimeout(function () {

                $('.view_gallery').trigger("click");
            }, 400);

            return false;
        });

    $(".smallGrid").click(function () {
        $(this).find('a').addClass('active');
    $('.largeGrid a').removeClass('active');

                $('div.product').removeClass('large');
                $(".make3D").removeClass('animate');
                $('.info-large').fadeOut("fast");
                setTimeout(function () {
        $('div.flip-back').trigger("click");
    }, 400);
                return false;
            });

            $(".smallGrid").click(function () {
        $('.product').removeClass('large');
    return false;
            });

            $('.colors-large a').click(function () { return false; });


            $('.product').each(function (i, el) {

        // Lift card and show stats on Mouseover
        $(el).find('.make3D').hover(function () {
            $(this).parent().css('z-index', "20");
            $(this).addClass('animate');
            $(this).find('div.carouselNext, div.carouselPrev').addClass('visible');
        }, function () {
            $(this).removeClass('animate');
            $(this).parent().css('z-index', "1");
            $(this).find('div.carouselNext, div.carouselPrev').removeClass('visible');
        });

     //Flip card to the back side
    //    $(el).find('.view_gallery').click(function () {
            
    //    $(el).find('div.carouselNext, div.carouselPrev').removeClass('visible');
    //$(el).find('.make3D').addClass('flip-10');
    //                setTimeout(function () {
    //    $(el).find('.make3D').removeClass('flip-10').addClass('flip90').find('div.shadow').show().fadeTo(80, 1, function () {
    //        $(el).find('.product-front, .product-front div.shadow').hide();
    //    });
    //}, 50);

    //                setTimeout(function () {
    //    $(el).find('.make3D').removeClass('flip90').addClass('flip190');
    //$(el).find('.product-back').show().find('div.shadow').show().fadeTo(90, 0);
    //                    setTimeout(function () {
    //    $(el).find('.make3D').removeClass('flip190').addClass('flip180').find('div.shadow').hide();
    //setTimeout(function () {
    //    $(el).find('.make3D').css('transition', '100ms ease-out');
    //$(el).find('.cx, .cy').addClass('s1');
    //                            setTimeout(function () {$(el).find('.cx, .cy').addClass('s2'); }, 100);
    //                            setTimeout(function () {$(el).find('.cx, .cy').addClass('s3'); }, 200);
    //                            $(el).find('div.carouselNext, div.carouselPrev').addClass('visible');
    //                        }, 100);
    //                    }, 100);
    //                }, 150);
    //            });

                 //Flip card back to the front side
    //            $(el).find('.flip-back').click(function () {

    //    $(el).find('.make3D').removeClass('flip180').addClass('flip190');
    //setTimeout(function () {
    //    $(el).find('.make3D').removeClass('flip190').addClass('flip90');

    //$(el).find('.product-back div.shadow').css('opacity', 0).fadeTo(100, 1, function () {
    //    $(el).find('.product-back, .product-back div.shadow').hide();
    //$(el).find('.product-front, .product-front div.shadow').show();
    //                    });
    //                }, 50);

    //                setTimeout(function () {
    //    $(el).find('.make3D').removeClass('flip90').addClass('flip-10');
    //$(el).find('.product-front div.shadow').show().fadeTo(100, 0);
    //                    setTimeout(function () {
    //    $(el).find('.product-front div.shadow').hide();
    //$(el).find('.make3D').removeClass('flip-10').css('transition', '100ms ease-out');
    //                        $(el).find('.cx, .cy').removeClass('s1 s2 s3');
    //                    }, 100);
    //                }, 150);

    //            });

                makeCarousel(el);
            });

            $('.add-cart-large').each(function (i, el) {
        $(el).click(function () {
            var carousel = $(this).parent().parent().find(".carousel-container");
            var img = carousel.find('img').eq(carousel.attr("rel"))[0];
            var position = $(img).offset();

            var productName = $(this).parent().find('h4').get(0).innerHTML;

            $("body").append('<div class="floating-cart"></div>');
            var cart = $('div.floating-cart');
            $("<img src='" + img.src + "' class='floating-image-large' />").appendTo(cart);

            $(cart).css({ 'top': position.top + 'px', "left": position.left + 'px' }).fadeIn("slow").addClass('moveToCart');
            setTimeout(function () { $("body").addClass("MakeFloatingCart"); }, 800);

            setTimeout(function () {
                $('div.floating-cart').remove();
                $("body").removeClass("MakeFloatingCart");


                var cartItem = "<div class='cart-item mycart'><div class='img-wrap'><img src='" + img.src + "' alt='' /></div><span>" + productName + "</span><strong>$39</strong><div class='cart-item-border'></div><div class='delete-item'></div></div>";

                $("#cart .empty").hide();
                $("#cart").append(cartItem);
                $("#checkout").fadeIn(500);

                $("#cart .cart-item").last()
                    .addClass("flash")
                    .find(".delete-item").click(function () {
                        $(this).parent().fadeOut(300, function () {
                            $(this).remove();
                            if ($("#cart .cart-item").size() == 0) {
                                $("#cart .empty").fadeIn(500);
                                $("#checkout").fadeOut(500);
                            }
                        })
                    });
                setTimeout(function () {
                    $("#cart .cart-item").last().removeClass("flash");
                }, 10);

            }, 1000);


        });
    })

            /* ----  Image Gallery Carousel   ---- */
            function makeCarousel(el) {


                var carousel = $(el).find('.carousel ul');
                var carouselSlideWidth = 315;
                var carouselWidth = 0;
                var isAnimating = false;
                var currSlide = 0;
                $(carousel).attr('rel', currSlide);

                // building the width of the casousel
                $(carousel).find('li').each(function () {
        carouselWidth += carouselSlideWidth;
    });
                $(carousel).css('width', carouselWidth);

                // Load Next Image
                $(el).find('div.carouselNext').on('click', function () {
                    var currentLeft = Math.abs(parseInt($(carousel).css("left")));
                    var newLeft = currentLeft + carouselSlideWidth;
                    if (newLeft == carouselWidth || isAnimating === true) { return; }
                    $(carousel).css({
        'left': "-" + newLeft + "px",
                        "transition": "300ms ease-out"
                    });
                    isAnimating = true;
                    currSlide++;
                    $(carousel).attr('rel', currSlide);
                    setTimeout(function () {isAnimating = false; }, 300);
                });

                // Load Previous Image
                $(el).find('div.carouselPrev').on('click', function () {
                    var currentLeft = Math.abs(parseInt($(carousel).css("left")));
                    var newLeft = currentLeft - carouselSlideWidth;
                    if (newLeft < 0 || isAnimating === true) { return; }
                    $(carousel).css({
        'left': "-" + newLeft + "px",
                        "transition": "300ms ease-out"
                    });
                    isAnimating = true;
                    currSlide--;
                    $(carousel).attr('rel', currSlide);
                    setTimeout(function () {isAnimating = false; }, 300);
                });
            }

            $('.sizes a span, .categories a span').each(function (i, el) {
        $(el).append('<span class="x"></span><span class="y"></span>');

    $(el).parent().on('click', function () {
                    if ($(this).hasClass('checked')) {
        $(el).find('.y').removeClass('animate');
    setTimeout(function () {
        $(el).find('.x').removeClass('animate');
    }, 50);
                        $(this).removeClass('checked');
                        return false;
                    }

                    $(el).find('.x').addClass('animate');
                    setTimeout(function () {
        $(el).find('.y').addClass('animate');
    }, 100);
                    $(this).addClass('checked');
                    return false;
                });
            });

            $('.add_to_cart').click(function () {
                var productCard = $(this).parent();
                var position = productCard.offset();
                var productImage = $(productCard).find('img').get(0).src;
                var productId = $(productCard).find('.product_id').get(0).innerHTML;
                var productName = $(productCard).find('.product_name').get(0).innerHTML;
                var productPrice = $(productCard).find('.product_price').get(0).innerHTML;


                $("body").append('<div class="floating-cart"></div>');
                var cart = $('div.floating-cart');
                productCard.clone().appendTo(cart);
                $(cart).css({'top': position.top + 'px', "left": position.left + 'px' }).fadeIn("slow").addClass('moveToCart');
                setTimeout(function () {$("body").addClass("MakeFloatingCart"); }, 800);
                setTimeout(function () {
        $('div.floating-cart').remove();
    $("body").removeClass("MakeFloatingCart");

                    var productExtras = '';
                    productExtras += $('#Extra_check_box').html();
                    //$('#removeid').remove();
                    var cartItem = "<div class='cart-item mycart' style='margin-left:0'><div class='img-wrap'><img src='" + productImage + "' alt='' /></div><small>" + productName + "</small><div class='cart-item-border'></div></div>";


                    $("#cart .empty").hide();
                    $("#cart").append(cartItem);
                    
                    $("#checkout").fadeIn(500);

                    $("#cart .cart-item").last()
                        .addClass("flash")
                        .find(".delete-item").click(function () {
        $(this).parent().fadeOut(300, function () {
            $(this).remove();
            if ($("#cart .cart-item").size() == 0) {
                $("#cart .empty").fadeIn(500);
                $("#checkout").fadeOut(500);
            }
        })
    });
                    setTimeout(function () {
        $("#cart .cart-item").last().removeClass("flash");
    }, 10);

                }, 1000);
            });



            $('#cartButton').click(function () {
                var pid = '';
                var counter = 0;
                $('.cartproductid').each(function () {
        pid += $(this).text() + ",";
    });
                $("#cartButton").attr("href", "/Products/CartItem/" + pid);
    });

    });

        $(function () {

            $("#Category").change(function () {

                $("#remove").remove();

                var cid = $(this).select("option:selected").val();

                $.ajax(
                    {
                        url: "/home/category/" + cid
                    }
                ).done(function (result) {

                    $("#mycategories").html(result);
                    Card();
                    LoadModal();
                    ShowModal();
                    AppendAttributes();
                });

            });
        });
   
        function LoadModal(){
            $('.view_gallery').click(function () {
                $("#modal-name").remove();
                $('#removeid').remove();

                var productCard = $(this).parent();
                var prodcartId = $(productCard).find('.product_id').text();
                var prodcartname = $(productCard).find('.product_name').text();
                var cartItemId = "<div id='removeid'><input id='title' type='hidden' name='id' value='" + prodcartId + "' /><input id='title' type='hidden' name='name' value='" + prodcartname + "' /></div>"
                $('#prodId').append(cartItemId);


                $.ajax(
                    {
                        url: "/products/viewdetails/" + prodcartId
                    }
                ).done(function (result2) {
                    $("#details").html(result2);
                    $('#button2').trigger('click');
                });
            });

        };

        $(function () {
            $('.view_gallery').click(function () {
                $("#modal-name").remove();
                $('#removeid').remove();

                var productCard = $(this).parent();
                var prodcartId = $(productCard).find('.product_id').text();
                var prodcartname = $(productCard).find('.product_name').text();
                var cartItemId = "<div id='removeid'><input id='title' type='hidden' name='id' value='" + prodcartId + "' /><input id='title' type='hidden' name='name' value='" + prodcartname + "' /></div>"
                $('#prodId').append(cartItemId);


                $.ajax(
                    {
                        url: "/products/viewdetails/" + prodcartId
                    }
                ).done(function (result2) {
                    $("#details").html(result2);
                    $('#button2').trigger('click');

                });
            });

        });

        $(document).on('click', '#button2', function () {
            $('html').css('overflow', 'hidden');
            // $("html").attr("style", "overflow: hidden");
            $(".btn-default").click(function (e) {


                e.preventDefault();
                dataModal = $(this).attr("data-modal");
                $("#" + dataModal).css({ "display": "block" });

            });
            //$("#yourModal").modal("hide");
            $(".close-modal, .modal-sandbox").click(function () {
                $(".modal").fadeOut(400);
                $('html').css('overflow', 'scroll');
            });
        });

        function ShowModal() {
            $("#button2").click(function () {
                $('html').css('overflow', 'hidden');
            });
            $(".btn-default").click(function (e) {
                e.preventDefault();
                dataModal = $(this).attr("data-modal");
                $("#" + dataModal).css({ "display": "block" });


            });
            //$("#yourModal").modal("hide");
            $(".close-modal, .modal-sandbox").click(function () {
                $(".modal").fadeOut(500);
                $('html').css('overflow', 'scroll');

            });
        };

        $(document).ready(function () {
            $('.add_to_cart').click(function () {
                $('#removeid').remove();
                $('#modal-name').remove();

                var productCard = $(this).parent();
                var prodcartId = $(productCard).find('.product_id').text();
                var prodcartname = $(productCard).find('.product_name').text();
                var cartItemId = "<div id='removeid'><input id='title' type='hidden' name='id' value='" + prodcartId + "' /><input id='title' type='hidden' name='name' value='" + prodcartname + "' /></div>"

                //$("#cartprodId").attr("value", prodcartId);
                //$("#cartprodName").attr("value", prodcartname);
                $('#prodId').append(cartItemId);
                $('#cartform').submit();
                $('#cartform').on("submit", function (e) {
                    //e.preventDefault();
                    return false;
                });
            });
            $('#submitID').hide();
        });

        function AppendAttributes() {
            $('.add_to_cart').click(function () {
                $('#removeid').remove();
                $('#modal-name').remove();

                var productCard = $(this).parent();
                var prodcartId = $(productCard).find('.product_id').text();
                var prodcartname = $(productCard).find('.product_name').text();
                var cartItemId = "<div id='removeid'><input id='title' type='hidden' name='id' value='" + prodcartId + "' /><input id='title' type='hidden' name='name' value='" + prodcartname + "' /></div>"

                //$("#cartprodId").attr("value", prodcartId);
                //$("#cartprodName").attr("value", prodcartname);
                $('#prodId').append(cartItemId);
                $('#cartform').submit();
                $('#cartform').on("submit", function (e) {
                    //e.preventDefault();
                    return false;
                });
            });
            $('#submitID').hide();
        };

        $(function () {
            $("#cartform").on("submit", function (e) {
                e.preventDefault();
                $.ajax({
                    type: "post",
                    url: this.action,
                    data: $(this).serialize(),
                    success: function (response) {
                        if (response == "done") {
                            // alert("Form submitted successfully!");
                        } else {
                            //alert("Form submission failed!");
                        }
                    },
                    error: function (response) {
                        //alert(response);
                    }
                });
            });
        });

        $(document).on('click', '#button2', function () {
            $('.add_to_cart_custm').click(function () {
                var productCard = $(this).parent();
                var position = productCard.offset();
                var productImage = $(productCard).find('.modal_image').get(0).src;
                var productId = $(productCard).find('.product_id_modal').get(0).innerHTML;
                var productName = $(productCard).find('.product_modal_name').get(0).innerHTML;
                var productPrice = $(productCard).find('.product_modal_price').get(0).innerHTML;
                $('#cartform').submit();

                $("body").append('<div class="floating-cart"></div>');
                var cart = $('div.floating-cart');
                productCard.clone().appendTo(cart);
                $(cart).css({ 'top': position.top + 'px', "left": position.left + 'px' }).fadeIn("slow").addClass('moveToCart');
                setTimeout(function () { $("body").addClass("MakeFloatingCart"); }, 800);
                setTimeout(function () {
                    $('div.floating-cart').remove();
                    $("body").removeClass("MakeFloatingCart");
                    //var checkboxValues = $(".extras").val();
                    //alert(checkboxValues);

                    //var productExtras = $('#Extra_check_box').html();
                    var cartItem = "<div class='cart-item mycart' style='margin-left:0'><div class='img-wrap'><img src='" + productImage + "' alt='' /></div><small>" + productName + "</small><div class='cart-item-border'></div></div>";

                    $("#cart .empty").hide();
                    $("#cart").append(cartItem);
                    //alert(productExtras);
                    //$("#details").append(productExtras);
                    $("#checkout").fadeIn(500);
                    $(".modal").fadeOut(500);
                    $('html').css('overflow', 'scroll');

                    $("#cart .cart-item").last()
                        .addClass("flash")
                        .find(".delete-item").click(function () {
                            $(this).parent().fadeOut(300, function () {
                                $(this).remove();
                                if ($("#cart .cart-item").size() == 0) {
                                    $("#cart .empty").fadeIn(500);
                                    $("#checkout").fadeOut(500);
                                }
                            })
                        });
                    setTimeout(function () {
                        $("#cart .cart-item").last().removeClass("flash");
                    }, 10);

                }, 1000);
            });
        });

        function Card() {
                $(".largeGrid").click(function () {
                    $(this).find('a').addClass('active');
                    $('.smallGrid a').removeClass('active');
                    $('.product').addClass('large').each(function () {
                    });
                    setTimeout(function () {
                        $('.info-large').show();
                    }, 200);
                    setTimeout(function () {

                        $('.view_gallery').trigger("click");
                    }, 400);

                    return false;
                });

                $(".smallGrid").click(function () {
                    $(this).find('a').addClass('active');
                    $('.largeGrid a').removeClass('active');

                    $('div.product').removeClass('large');
                    $(".make3D").removeClass('animate');
                    $('.info-large').fadeOut("fast");
                    setTimeout(function () {
                        $('div.flip-back').trigger("click");
                    }, 400);
                    return false;
                });

                $(".smallGrid").click(function () {
                    $('.product').removeClass('large');
                    return false;
                });

                $('.colors-large a').click(function () { return false; });


                $('.product').each(function (i, el) {

                    // Lift card and show stats on Mouseover
                    $(el).find('.make3D').hover(function () {
                        $(this).parent().css('z-index', "20");
                        $(this).addClass('animate');
                        $(this).find('div.carouselNext, div.carouselPrev').addClass('visible');
                    }, function () {
                        $(this).removeClass('animate');
                        $(this).parent().css('z-index', "1");
                        $(this).find('div.carouselNext, div.carouselPrev').removeClass('visible');
                    });

                    // Flip card to the back side
                    //$(el).find('.view_gallery').click(function () {

                    //    $(el).find('div.carouselNext, div.carouselPrev').removeClass('visible');
                    //    $(el).find('.make3D').addClass('flip-10');
                    //    setTimeout(function () {
                    //        $(el).find('.make3D').removeClass('flip-10').addClass('flip90').find('div.shadow').show().fadeTo(80, 1, function () {
                    //            $(el).find('.product-front, .product-front div.shadow').hide();
                    //        });
                    //    }, 50);

                    //    setTimeout(function () {
                    //        $(el).find('.make3D').removeClass('flip90').addClass('flip190');
                    //        $(el).find('.product-back').show().find('div.shadow').show().fadeTo(90, 0);
                    //        setTimeout(function () {
                    //            $(el).find('.make3D').removeClass('flip190').addClass('flip180').find('div.shadow').hide();
                    //            setTimeout(function () {
                    //                $(el).find('.make3D').css('transition', '100ms ease-out');
                    //                $(el).find('.cx, .cy').addClass('s1');
                    //                setTimeout(function () { $(el).find('.cx, .cy').addClass('s2'); }, 100);
                    //                setTimeout(function () { $(el).find('.cx, .cy').addClass('s3'); }, 200);
                    //                $(el).find('div.carouselNext, div.carouselPrev').addClass('visible');
                    //            }, 100);
                    //        }, 100);
                    //    }, 150);
                    //});

                    // Flip card back to the front side
                    //$(el).find('.flip-back').click(function () {

                    //    $(el).find('.make3D').removeClass('flip180').addClass('flip190');
                    //    setTimeout(function () {
                    //        $(el).find('.make3D').removeClass('flip190').addClass('flip90');

                    //        $(el).find('.product-back div.shadow').css('opacity', 0).fadeTo(100, 1, function () {
                    //            $(el).find('.product-back, .product-back div.shadow').hide();
                    //            $(el).find('.product-front, .product-front div.shadow').show();
                    //        });
                    //    }, 50);

                    //    setTimeout(function () {
                    //        $(el).find('.make3D').removeClass('flip90').addClass('flip-10');
                    //        $(el).find('.product-front div.shadow').show().fadeTo(100, 0);
                    //        setTimeout(function () {
                    //            $(el).find('.product-front div.shadow').hide();
                    //            $(el).find('.make3D').removeClass('flip-10').css('transition', '100ms ease-out');
                    //            $(el).find('.cx, .cy').removeClass('s1 s2 s3');
                    //        }, 100);
                    //    }, 150);

                    //});

                    makeCarousel(el);
                });

                $('.add-cart-large').each(function (i, el) {
                    $(el).click(function () {
                        var carousel = $(this).parent().parent().find(".carousel-container");
                        var img = carousel.find('img').eq(carousel.attr("rel"))[0];
                        var position = $(img).offset();

                        var productName = $(this).parent().find('h4').get(0).innerHTML;

                        $("body").append('<div class="floating-cart"></div>');
                        var cart = $('div.floating-cart');
                        $("<img src='" + img.src + "' class='floating-image-large' />").appendTo(cart);

                        $(cart).css({ 'top': position.top + 'px', "left": position.left + 'px' }).fadeIn("slow").addClass('moveToCart');
                        setTimeout(function () { $("body").addClass("MakeFloatingCart"); }, 800);

                        setTimeout(function () {
                            $('div.floating-cart').remove();
                            $("body").removeClass("MakeFloatingCart");


                            var cartItem = "<div class='cart-item mycart'><div class='img-wrap'><img src='" + img.src + "' alt='' /></div><span>" + productName + "</span><strong>$39</strong><div class='cart-item-border'></div><div class='delete-item'></div></div>";

                            $("#cart .empty").hide();
                            $("#cart").append(cartItem);
                            $("#checkout").fadeIn(500);

                            $("#cart .cart-item").last()
                                .addClass("flash")
                                .find(".delete-item").click(function () {
                                    $(this).parent().fadeOut(300, function () {
                                        $(this).remove();
                                        if ($("#cart .cart-item").size() == 0) {
                                            $("#cart .empty").fadeIn(500);
                                            $("#checkout").fadeOut(500);
                                        }
                                    })
                                });
                            setTimeout(function () {
                                $("#cart .cart-item").last().removeClass("flash");
                            }, 10);

                        }, 1000);


                    });
                });

                /* ----  Image Gallery Carousel   ---- */
                function makeCarousel(el) {


                    var carousel = $(el).find('.carousel ul');
                    var carouselSlideWidth = 315;
                    var carouselWidth = 0;
                    var isAnimating = false;
                    var currSlide = 0;
                    $(carousel).attr('rel', currSlide);

                    // building the width of the casousel
                    $(carousel).find('li').each(function () {
                        carouselWidth += carouselSlideWidth;
                    });
                    $(carousel).css('width', carouselWidth);

                    // Load Next Image
                    $(el).find('div.carouselNext').on('click', function () {
                        var currentLeft = Math.abs(parseInt($(carousel).css("left")));
                        var newLeft = currentLeft + carouselSlideWidth;
                        if (newLeft == carouselWidth || isAnimating === true) { return; }
                        $(carousel).css({
                            'left': "-" + newLeft + "px",
                            "transition": "300ms ease-out"
                        });
                        isAnimating = true;
                        currSlide++;
                        $(carousel).attr('rel', currSlide);
                        setTimeout(function () { isAnimating = false; }, 300);
                    });

                    // Load Previous Image
                    $(el).find('div.carouselPrev').on('click', function () {
                        var currentLeft = Math.abs(parseInt($(carousel).css("left")));
                        var newLeft = currentLeft - carouselSlideWidth;
                        if (newLeft < 0 || isAnimating === true) { return; }
                        $(carousel).css({
                            'left': "-" + newLeft + "px",
                            "transition": "300ms ease-out"
                        });
                        isAnimating = true;
                        currSlide--;
                        $(carousel).attr('rel', currSlide);
                        setTimeout(function () { isAnimating = false; }, 300);
                    });
                }

                $('.sizes a span, .categories a span').each(function (i, el) {
                    $(el).append('<span class="x"></span><span class="y"></span>');

                    $(el).parent().on('click', function () {
                        if ($(this).hasClass('checked')) {
                            $(el).find('.y').removeClass('animate');
                            setTimeout(function () {
                                $(el).find('.x').removeClass('animate');
                            }, 50);
                            $(this).removeClass('checked');
                            return false;
                        }

                        $(el).find('.x').addClass('animate');
                        setTimeout(function () {
                            $(el).find('.y').addClass('animate');
                        }, 100);
                        $(this).addClass('checked');
                        return false;
                    });
                });

                $('.add_to_cart').click(function () {
                    var productCard = $(this).parent();
                    var position = productCard.offset();
                    var productImage = $(productCard).find('img').get(0).src;
                    var productId = $(productCard).find('.product_id').get(0).innerHTML;
                    var productName = $(productCard).find('.product_name').get(0).innerHTML;
                    var productPrice = $(productCard).find('.product_price').get(0).innerHTML;

                    $("body").append('<div class="floating-cart"></div>');
                    var cart = $('div.floating-cart');
                    productCard.clone().appendTo(cart);
                    $(cart).css({ 'top': position.top + 'px', "left": position.left + 'px' }).fadeIn("slow").addClass('moveToCart');
                    setTimeout(function () { $("body").addClass("MakeFloatingCart"); }, 800);
                    setTimeout(function () {
                        $('div.floating-cart').remove();
                        $("body").removeClass("MakeFloatingCart");

                        // var productExtras = $('#Extra_check_box').html();
                        var cartItem = "<div class='cart-item mycart' style='margin-left:0'><div class='img-wrap'><img src='" + productImage + "' alt='' /></div><small>" + productName + "</small><div class='cart-item-border'></div></div>";

                        $("#cart .empty").hide();
                        $("#cart").append(cartItem);
                        $("#checkout").fadeIn(500);

                        $("#cart .cart-item").last()
                            .addClass("flash")
                            .find(".delete-item").click(function () {
                                $(this).parent().fadeOut(300, function () {
                                    $(this).remove();
                                    if ($("#cart .cart-item").size() == 0) {
                                        $("#cart .empty").fadeIn(500);
                                        $("#checkout").fadeOut(500);
                                    }
                                })
                            });
                        setTimeout(function () {
                            $("#cart .cart-item").last().removeClass("flash");
                        }, 10);

                    }, 1000);
                });



                $('#cartButton').click(function () {
                    var pid = '';
                    var counter = 0;
                    $('.cartproductid').each(function () {

                        pid += $(this).text() + ",";

                    });
                    $("#cartButton").attr("href", "/Products/CartItem/" + pid);


                });

        };

        $(document).on('change', '.extras', function () {
           
            var i = $(this).attr('id');
            var x = $(this).is(":checked");

            $.ajax(
                {
                    url: "/products/extranutritiondetail/" + i
                }
            ).done(function (result) {

                $("#extrasdiv").html(result);
                if (x) {

                addNutrition();
                }
                else {
                    subtractNutrition();
                };
                   
                });
        });

        function addNutrition() {

           var fullCal = $('#fCalories').html();
           var fullFat = $('#fFat').html();
           var fullSat = $('#fSaturatedFat').html();
           var fullTra = $('#fTransFat').html();
           var fullCho = $('#fCholestrol').html();
           var fullSod = $('#fSodium').html();
           var fullCar = $('#fCarbohydrates').html();
           var fullFib = $('#fFiber').html();
           var fullSug = $('#fSugar').html();
           var fullPro = $('#fProtein').html();

           var Cal = $('#Calories').html();
           var Fat = $('#Fat').html();
           var Sat = $('#SaturatedFat').html();
           var Tra = $('#TransFat').html();
           var Cho = $('#Cholestrol').html();
           var Sod = $('#Sodium').html();
           var Car = $('#Carbohydrates').html();
           var Fib = $('#Fiber').html();
           var Sug = $('#Sugar').html();
           var Pro = $('#Protein').html();

           var totalCal = Math.round(parseInt(fullCal) + parseInt(Cal));
           var totalFat = Math.round(parseInt(fullFat) + parseInt(Fat));
           var totalSat = Math.round(parseInt(fullSat) + parseInt(Sat));
           var totalTra = Math.round(parseInt(fullTra) + parseInt(Tra));
           var totalCho = Math.round(parseInt(fullCho) + parseInt(Cho));
           var totalSod = Math.round(parseInt(fullSod) + parseInt(Sod));
           var totalCar = Math.round(parseInt(fullCar) + parseInt(Car));
           var totalFib = Math.round(parseInt(fullFib) + parseInt(Fib));
           var totalSug = Math.round(parseInt(fullSug) + parseInt(Sug));
           var totalPro = Math.round(parseInt(fullPro) + parseInt(Pro));

           $('#fCalories').text(totalCal);
           $('#fFat').text(totalFat);
           $('#fSaturatedFat').text(totalSat);
           $('#fTransFat').text(totalTra);
           $('#fCholestrol').text(totalCho);
           $('#fSodium').text(totalSod);
           $('#fCarbohydrates').text(totalCar);
           $('#fFiber').text(totalFib);
           $('#fSugar').text(totalSug);
           $('#fProtein').text(totalPro);
        };

        function subtractNutrition() {

            var fullCal = $('#fCalories').html();
            var fullFat = $('#fFat').html();
            var fullSat = $('#fSaturatedFat').html();
            var fullTra = $('#fTransFat').html();
            var fullCho = $('#fCholestrol').html();
            var fullSod = $('#fSodium').html();
            var fullCar = $('#fCarbohydrates').html();
            var fullFib = $('#fFiber').html();
            var fullSug = $('#fSugar').html();
            var fullPro = $('#fProtein').html();

            var Cal = $('#Calories').html();
            var Fat = $('#Fat').html();
            var Sat = $('#SaturatedFat').html();
            var Tra = $('#TransFat').html();
            var Cho = $('#Cholestrol').html();
            var Sod = $('#Sodium').html();
            var Car = $('#Carbohydrates').html();
            var Fib = $('#Fiber').html();
            var Sug = $('#Sugar').html();
            var Pro = $('#Protein').html();

            var totalCal = Math.round(parseInt(fullCal) - parseInt(Cal));
            var totalFat = Math.round(parseInt(fullFat) - parseInt(Fat));
            var totalSat = Math.round(parseInt(fullSat) - parseInt(Sat));
            var totalTra = Math.round(parseInt(fullTra) - parseInt(Tra));
            var totalCho = Math.round(parseInt(fullCho) - parseInt(Cho));
            var totalSod = Math.round(parseInt(fullSod) - parseInt(Sod));
            var totalCar = Math.round(parseInt(fullCar) - parseInt(Car));
            var totalFib = Math.round(parseInt(fullFib) - parseInt(Fib));
            var totalSug = Math.round(parseInt(fullSug) - parseInt(Sug));
            var totalPro = Math.round(parseInt(fullPro) - parseInt(Pro));

            $('#fCalories').text(totalCal);
            $('#fFat').text(totalFat);
            $('#fSaturatedFat').text(totalSat);
            $('#fTransFat').text(totalTra);
            $('#fCholestrol').text(totalCho);
            $('#fSodium').text(totalSod);
            $('#fCarbohydrates').text(totalCar);
            $('#fFiber').text(totalFib);
            $('#fSugar').text(totalSug);
            $('#fProtein').text(totalPro);
        };

$(function () {
    if ($("#cart .cart-item").size() != 0) {
        $("#cart .empty").hide();
        $("#checkout").fadeIn(50);
    }

});