var check = false;

function changeVal(el) {
            var qt = parseFloat(el.parent().children(".qt").html());
            var price = parseFloat(el.parent().children(".price").html());
            var eq = Math.round(price * qt * 100) / 100;

            el.parent().children(".full-price").html(eq + "/-");

            changeTotal();


        }

function changeTotal() {
            var price = 0;

            $(".full-price").each(function (index) {
        price += parseFloat($(".full-price").eq(index).html());
    });

            price = Math.round(price * 100) / 100;
            var tax = Math.round(price * 0.05);
            var shipping = parseFloat($(".shipping span").html());
            var fullPrice = Math.round(price + tax + shipping);

            if (price == 0) {
        fullPrice = 0;
    }

            $(".subtotal span").html(price);
            $(".tax span").html(tax);
            $(".total span").html(fullPrice);

        }

$(function () {

        $(".remove").click(function () {
            var el = $(this);
            el.parent().parent().addClass("removed");
            window.setTimeout(
                function () {
                    el.parent().parent().slideUp('fast', function () {
                        el.parent().parent().remove();
                        if ($(".product").length == 0) {
                            var empty = "<h1>Empty Cart!</h1> <a href='/Home/ByCategory'><h6>Place New Order</h6></a>"
                            $('#productcart').append(empty);
                            $("#checkout").fadeOut(500);
                        }
                        changeTotal();
                        changeTotalNutrition();
                        removeBtn();
                    });
                }, 200);
        });

    $(".qt-plus").click(function () {
        $(this).parent().children(".qt").html(parseInt($(this).parent().children(".qt").html()) + 1);

    $(this).parent().children(".full-price").addClass("added");
                var el = $(this);
                window.setTimeout(function () {el.parent().children(".full-price").removeClass("added"); changeVal(el); }, 150);

            });



            $(".qt-minus").click(function () {

        child = $(this).parent().children(".qt");

    if (parseInt(child.html()) > 1) {
        child.html(parseInt(child.html()) - 1);
    }

                $(this).parent().children(".full-price").addClass("minused");

                var el = $(this);
                window.setTimeout(function () {el.parent().children(".full-price").removeClass("minused"); changeVal(el); }, 150);

            });


            window.setTimeout(function () {$(".is-open").removeClass("is-open")}, 1200);

            $(".btn").click(function () {
        $('#removeid').remove();
        $('#removeqt').remove();
    var total = parseFloat($(".total span").html());
                var totalPrc = "<div id='removeid'><input id='totalPrice' type='hidden' name='total' value='" + total + "' /></div>"
                $('#total_price').append(totalPrc);

                //quantity
                var quantityArr = '';
                $(".qt").each(function () {
                    quantityArr += parseFloat($(this).text())+",";
                });
                var quant = "<div id='removeqt'><input id='quantities' type='hidden' name='quantities' value='" + quantityArr + "' /></div>"
                $('#quantity').append(quant);

                $('#priceform').submit();


            });
        });

$(function () {
            $("#priceform").on("submit", function (e) {
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

$(function() {
            if ($(".product").length == 0) {
            $("#checkout").fadeOut(100);            
    };
});

$(document).on('click', '.qt-minus,.qt-plus', function () {
               
                var i = $(this).parent().children(".qt").html();

                var Cal = $(this).parent().children(".Calories").html();
                var Fat = $(this).parent().children(".Fat").html();
                var Sat = $(this).parent().children(".SaturatedFat").html();
                var Tra = $(this).parent().children(".TransFat").html();
                var Cho = $(this).parent().children(".Cholestrol").html();
                var Sod = $(this).parent().children(".Sodium").html();
                var Car = $(this).parent().children(".Carbohydrates").html();
                var Fib = $(this).parent().children(".Fiber").html();
                var Sug = $(this).parent().children(".Sugar").html();
                var Pro = $(this).parent().children(".Protein").html();

                var totalCal = Math.round(parseInt(Cal) * parseInt(i));
                var totalFat = Math.round(parseInt(Fat) * parseInt(i));
                var totalSat = Math.round(parseInt(Sat) * parseInt(i));
                var totalTra = Math.round(parseInt(Tra) * parseInt(i));
                var totalCho = Math.round(parseInt(Cho) * parseInt(i));
                var totalSod = Math.round(parseInt(Sod) * parseInt(i));
                var totalCar = Math.round(parseInt(Car) * parseInt(i));
                var totalFib = Math.round(parseInt(Fib) * parseInt(i));
                var totalSug = Math.round(parseInt(Sug) * parseInt(i));
                var totalPro = Math.round(parseInt(Pro) * parseInt(i));


                $(this).parent().children(".full-Calories").text(totalCal);
                $(this).parent().children(".full-Fat").text(totalFat);
                $(this).parent().children(".full-SaturatedFat").text(totalSat);
                $(this).parent().children(".full-TransFat").text(totalTra);
                $(this).parent().children(".full-Cholestrol").text(totalCho);
                $(this).parent().children(".full-Sodium").text(totalSod);
                $(this).parent().children(".full-Carbohydrates").text(totalCar);
                $(this).parent().children(".full-Fiber").text(totalFib);
                $(this).parent().children(".full-Sugar").text(totalSug);
                $(this).parent().children(".full-Protein").text(totalPro);

                changeTotalNutrition();

                    });

function changeTotalNutrition() {

                var calories = 0;
                var fat = 0;
                var saturatedFat = 0;
                var transFat = 0;
                var cholestrol = 0;
                var sodium = 0;
                var carbohydrates = 0;
                var fiber = 0;
                var sugar = 0;
                var protein = 0;

            $(".full-Calories").each(function () {
            calories += parseFloat($(this).text());
        });

            $(".full-Fat").each(function () {
            fat += parseFloat($(this).text());
        });
            $(".full-SaturatedFat").each(function () {
            saturatedFat += parseFloat($(this).text());
        });
            $(".full-TransFat").each(function () {
            transFat += parseFloat($(this).text());
        });
            $(".full-Cholestrol").each(function () {
            cholestrol += parseFloat($(this).text());
        });
            $(".full-Sodium").each(function () {
            sodium += parseFloat($(this).text());
        });
            $(".full-Carbohydrates").each(function () {
            carbohydrates += parseFloat($(this).text());
        });
            $(".full-Fiber").each(function () {
            fiber += parseFloat($(this).text());
        });
            $(".full-Sugar").each(function () {
            sugar += parseFloat($(this).text());
        });
            $(".full-Protein").each(function () {
            protein += parseFloat($(this).text());
        });

            $(".totalCalories span").html(calories);
            $(".total-Fat").html(fat);
            $(".total-SaturatedFat").html(saturatedFat);
            $(".total-TransFat").html(transFat);
            $(".total-Cholestrol").html(cholestrol);
            $(".total-Sodium").html(sodium);
            $(".total-Carbohydrates").html(carbohydrates);
            $(".total-Fiber").html(fiber);
            $(".total-Sugar").html(sugar);
            $(".total-Protein").html(protein);
            }

$('.remove').click(function (event) {
   
    event.preventDefault();
    var i = $(this).attr('data-id');

    $.ajax(
        {
            url: "/products/delcartItem/" + i

        }
    )
});