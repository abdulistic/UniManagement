$(document).on('click', '.editItem', function () {
    debugger;
    var subjectId = $(this).attr('data-id');

    var $tr = $(this).closest('tr');
    var TestName = $tr.find('.testName').text();
    var TotalMarks = $tr.find('.totalMarks').text();
    var CreatedOn = $tr.find('.createdOn').text();

    var today = new Date(CreatedOn);
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!

    var yyyy = today.getFullYear();
    if (dd < 10) { dd = '0' + dd }
    if (mm < 10) { mm = '0' + mm }
    today = yyyy + '-' + mm + '-' + dd;

    $("#editId").val(subjectId);
    $('#editName').val(TestName);
    $('#editDate').val(today);
    $('#editMarks').val(TotalMarks);

    $('#editEmployeeModal').modal('toggle');
});

$(document).on('click', '.delItem', function (e) {
    debugger;
    e.preventDefault();
    var testId = $(this).attr('data-id');
    var subjectId = $("#subjectId").val();

    $('#delItemOkBtn').attr('href', '/Teacher/DeleteTestById?id=' + testId + '&subjectId=' + subjectId);
    $('#deleteEmployeeModal').modal('toggle');
});



$(document).on('click', '#updateItem', function (e) {
    e.preventDefault();
    $('#editform').submit();
});