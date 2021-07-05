var country = '';
var province = '';
var city = '';

$(document).on('click', '#registerStudent', function () {
    validateForm();
    var counter = 0;
 
        $(".error").each(function (index) {
        counter +=1;
    });

    if (counter <= 0) {
        $('#signupform').submit();
    }
    
});

function validateForm() {
    var firstname = $('#firstname').val();
    var lastname = $('#lastname').val();
    var email = $('#email').val();
    var address = $('#address').val();
    var password = $('#password').val();
    var confirmPass = $('#confirmPass').val();

    var image = $('#image').val();






    var nameReg = /^[A-Za-z]+$/;
    var numberReg = /^[0-9]+$/;
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    var usernameReg = /[A-Za-z][A-Za-z0-9._]{5,14}/;
    var passReg = /^(?=.*\d).{6,20}$/;
    var confirmpassReg = password === confirmPass;



    var inputVal = new Array(firstname, lastname, email, confirmPass, password, country, province, city, image, address);

    var inputMessage = new Array("First Name", "Last Name", "Email", "Confirm Password", "Password", "Country", "Province", "City", "Image", "Address");

    $('.error').remove();




    if (inputVal[0] === "") {
        $('#fnameLabel').after('<span class="error"><small> Please enter your ' + inputMessage[0] + '</small></span>');
    }
    else if (!nameReg.test(firstname)) {
        $('#fnameLabel').after('<span class="error"><small> No Digits and spaces letters only</small></span>');
    }

    if (inputVal[1] === "") {
        $('#lnameLabel').after('<span class="error"><small> Please enter your ' + inputMessage[1] + '</small></span>');
    }
    else if (!nameReg.test(lastname)) {
        $('#lnameLabel').after('<span class="error"><small>No Digits and spaces letters only</small></span>');
    }

    if (inputVal[2] === "") {
        $('#emailLabel').after('<span class="error"> <small>Please enter your ' + inputMessage[2] + '</small></span>');
    }
    else if (!emailReg.test(email)) {
        $('#emailLabel').after('<span class="error"> <small>Please enter a valid email address</small></span>');
    }

    if (inputVal[3] === "") {
        $('#confirmPassLabel').after('<span class="error"><small> Please enter your ' + inputMessage[3] + '</small></span>');
    }
    else if (!confirmpassReg) {
        $('#confirmPassLabel').after('<span class="error"><small> Password does not match </small></span>');
    }

    if (inputVal[4] === "") {
        $('#passwordLabel').after('<span class="error"><small> Please enter your ' + inputMessage[4] + '</small></span>');
    }
    else if (!passReg.test(password)) {
        $('#passwordLabel').after('<span class="error"><small> Must contains one digit from 0-9 and length at least 6 characters and maximum of 20 </small></span>');
    }

    if (inputVal[5] === "") {
        $('#nationalityLabel').after('<span class="error"><small> Please select your ' + inputMessage[5] + '</small></span>');
    }

    if ($('#Provinces').length > 0) {

        if (inputVal[6] === "") {
            $('#provinceLabel').after('<span class="error"><small> Please select your ' + inputMessage[6] + '</small></span>');
        }
    }
    if ($('#City').length > 0) {
        if (inputVal[7] === "") {
            $('#cityLabel').after('<span class="error"><small> Please select your ' + inputMessage[7] + '</small></span>');
        }
    }
    if (inputVal[8] === "") {
        $('#imageLabel').after('<span class="error"><small> Please select your ' + inputMessage[8] + '</small></span>');
    }
    if (inputVal[9] === "") {
        $('#addressLabel').after('<span class="error"><small> Please select your ' + inputMessage[9] + '</small></span>');
    }





};

$(document).on('click', '#signupBtn', function (e) {
    var pid = $('#City').select("option:selected").val();
    $.ajax(
        {
            url: "/users/getusercity/" + pid
        }
    ).done(function (result) {
        $('#signupBtn').submit();
    });

});

$(function () {
    $("#Countries").change(function () {

        $("#City").remove();

        var cid = $(this).select("option:selected").val();

        $.ajax(
            {
                url: "/locations/provinces/" + cid
            }
        ).done(function (result) {

            $("#province").html(result);
        });

    });

    
});

$(document).on('change', '#Provinces', function () {
    var pid = $(this).select("option:selected").val();
    
    $.ajax(
        {
            url: "/locations/cities/" + pid
        }
    ).done(function (result) {

        $("#city").html(result);
        });


});

//$(document).on('change', '#City', function () {
//    debugger;
//    var cid = $(this).select("option:selected").val();

//    $.ajax(
//        {
//            url: "/home/cityid/" + cid
//        }
//    )
//});


$(document).on("change", "#Countries", function () {

    country = $(this).val();
});
$(document).on("change", "#Provinces", function () {
  
    province = $(this).val();
});
$(document).on("change", "#City", function () {
   
    city = $(this).val();
});