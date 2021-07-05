$(document).on('click', '.editItem', function () {
    //$('#exampleModalLong').remove();
    debugger;
    var usertId = $(this).attr('data-id');

    $.ajax(
        {
            url: "/admin/getuserbyid?userId=" + usertId
        }
    ).done(function (result) {
        debugger;

        $("#editUserId").val(result.UserId);
        $("#editFirstName").val(result.FirstName);
        $("#editLastName").val(result.LastName);
        $("#editEmail").val(result.Email);
        $("#editPhone").val(result.PhoneNumber);

        $('#editEmployeeModal').modal('toggle');
    });
});

$(document).on('click', '.delItem', function (e) {
    debugger;
    e.preventDefault();
    var usertId = $(this).attr('data-id');
    var subjectId = $("#editUserId").val();

    $('#delItemOkBtn').attr('href', '/admin/deleteuserbyid?userId=' + usertId + '&subjectId=' + subjectId);
    $('#deleteEmployeeModal').modal('toggle');
});



$(document).on('click', '#updateItem', function (e) {
    e.preventDefault();
    $('#editform').submit();
});