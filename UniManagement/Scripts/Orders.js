$(function () {
    function LoadModal() {
        $('.custDetailBtn').click(function () {
         
            var prodcartId = $(this).attr('data-id');

            $.ajax(
                {
                    url: "/users/cutomerdetails/" + prodcartId
                }
            ).done(function (result2) {
                $("#details").html(result2);
                $('#BtnModal').trigger('click');
            });
        });
    };
});