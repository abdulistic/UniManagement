$(document).on('click', '.editItem', function () {
    //$('#exampleModalLong').remove();
    debugger;
    var classId = $(this).attr('data-id');

    $.ajax(
        {
            url: "/admin/getclassbyid?classId=" + classId
        }
    ).done(function (result) {
        debugger;

        $("#editId").val(result.ClassId);
        $("#editName").val(result.ClassName);

        $('#editEmployeeModal').modal('toggle');
    });
});

$(document).on('click', '.delItem', function (e) {
    debugger;
    e.preventDefault();
    var classId = $(this).attr('data-id');

    $('#delItemOkBtn').attr('href', '/admin/deleteclassbyid?classId=' + classId);
    $('#deleteEmployeeModal').modal('toggle');
});



$(document).on('click', '#updateItem', function (e) {
    e.preventDefault();
    $('#editform').submit();
});