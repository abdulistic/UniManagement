$(document).on('click', '.editItem', function () {
    //$('#exampleModalLong').remove();
    debugger;
    var subjectId = $(this).attr('data-id');

    $.ajax(
        {
            url: "/admin/getsubjectbyid?subjectId=" + subjectId
        }
    ).done(function (result) {
        debugger;

        $("#editId").val(result.SubjectId);
        $("#editName").val(result.SubjectName);
        $('#editTeacher').val(result.TeacherId);
        $('#editClass').val(result.ClassId);
        //$('#editTeacher>option:eq(' + result.TeacherId + ')').attr('selected', true);
        //$('#editClass>option:eq(' + result.ClassId + ')').attr('selected', true);

        $('#editEmployeeModal').modal('toggle');
    });
});

$(document).on('click', '.delItem', function (e) {
    debugger;
    e.preventDefault();
    var subjectId = $(this).attr('data-id');

    $('#delItemOkBtn').attr('href', '/admin/deletesubjectbyid?subjectId=' + subjectId);
    $('#deleteEmployeeModal').modal('toggle');
});



$(document).on('click', '#updateItem', function (e) {
    e.preventDefault();
    $('#editform').submit();
});