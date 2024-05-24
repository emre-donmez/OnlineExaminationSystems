function openEditModal(id, name, lessonId, questionCount, duration, startedDate) {
    document.getElementById('editModalLabel').innerText = 'Edit Exam';
    document.getElementById('save-button').onclick = saveEdit;
    $('#editId').val(id);
    $('#editName').val(name);
    $('#editLessonId').val(lessonId);
    $('#editQuestionCount').val(questionCount);
    $('#editDuration').val(duration);
    $('#editStartDate').val(startedDate.substring(0, 10)); // Extract date part
    $('#editStartTime').val(startedDate.substring(11, 16)); // Extract time part
    $('#editModal').modal('show');
}

function saveEdit() {
    var data = {
        Id: $('#editId').val(),
        Name: $('#editName').val(),
        LessonId: $('#editLessonId').val(),
        QuestionCount: $('#editQuestionCount').val(),
        Duration: $('#editDuration').val(),
        StartedDate: $('#editStartDate').val() + 'T' + $('#editStartTime').val() + ':00'
    };

    fetch('/Exam/Edit', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            toastr.success('Exam successfully edited!');
            setTimeout(() => {
                window.location.reload();
            }, 2500);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            toastr.error('An error occurred while editing exam.');
        });
}

function deleteExam(id) {
    if (!confirm('Are you sure you want to delete this Exam?')) return;

    fetch('/Exam/Delete', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: parseInt(id) })
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            toastr.success('Exam successfully deleted!');
            setTimeout(() => {
                window.location.reload();
            }, 2500);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            toastr.error('An error occurred while deleting exam.');
        });
}
