$(document).on('click', '.editItem', function () {
    debugger;
    var subjectId = $(this).attr('data-id');

    var $tr = $(this).closest('tr');
    var studentId = $tr.find('td[data-studentid]').data('studentid');
    var totalMarks = $tr.find('td[data-totalmarks]').data('totalmarks');

    $("#editId").val(subjectId);
    $("#editStudentId").val(studentId);
    $("#editMarks").val(totalMarks);
    

    $('#editEmployeeModal').modal('toggle');
});

$(document).on('click', '.delItem', function (e) {
    debugger;
    e.preventDefault();
    var subjectId = $(this).attr('data-id');

    $('#delItemOkBtn').attr('href', '/admin/DeassignClass?userId=' + subjectId);
    $('#deleteEmployeeModal').modal('toggle');
});



$(document).on('click', '#updateItem', function (e) {
    e.preventDefault();
    $('#editform').submit();
});