$(document).on('click', '.editFoodItem', function () {
        $('#exampleModalLong').remove();
        var prodcartId = $(this).attr('data-id');

        $.ajax(
            {
                url: "/products/edititem/" + prodcartId
            }
        ).done(function (result) {
            $("#editModal").html(result);
            $('#BtnModal').trigger('click');
        });
    });

$(document).on('click', '.delFoodItem', function (e) {
    e.preventDefault();
    var prodcartId = $(this).attr('data-id');

    $('#delItemOkBtn').attr('href', '/products/delproduct/' + prodcartId);
    $('#delBtnModal').trigger('click');
});



$(document).on('click', '#updateItem', function (e) {
    e.preventDefault();
    $('#editform').submit();
});
