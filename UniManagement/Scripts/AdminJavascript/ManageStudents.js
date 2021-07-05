$(document).on('click', '.editItem', function () {
    debugger;
    var subjectId = $(this).attr('data-id');

    var $tr = $(this).closest('tr');
    var serialNumber = $tr.find('td[data-className]').data('className');

    $("#editId").val(subjectId);
    $('#editClass').find("option:contains(" + serialNumber + ")").attr('selected', 'selected');

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