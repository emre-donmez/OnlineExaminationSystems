function openEditModal(id, questionText, Option1, Option2, Option3, CorrectAnswer) {
    document.getElementById('editModalLabel').innerText = 'Edit Question';
    document.getElementById('save-button').onclick = saveEdit;
    $('#editId').val(id);
    $('#editQuestionText').val(questionText);
    $('#editOption1').val(Option1);
    $('#editOption2').val(Option2);
    $('#editOption3').val(Option3);
    $('#editCorrectAnswer').val(CorrectAnswer);
    $('#editModal').modal('show');
}
function saveEdit() {
    var data = {
        Id: $('#editId').val(),
        QuestionText: $('#editQuestionText').val(),
        Option1: $('#editOption1').val(),
        Option2: $('#editOption2').val(),
        Option3: $('#editOption3').val(),
        CorrectAnswer: $('#editCorrectAnswer').val(),
        ExamId: $('#editExamId').val()
    };
    fetch('/Academician/Question/Edit', {
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
            toastr.success('Question successfully edited!');
            setTimeout(() => {
                window.location.reload();
            }, 2500);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            toastr.error('An error occurred while editing question.');
        });
}

function deleteQuestion(id) {
    if (!confirm('Are you sure you want to delete this question?')) return;

    fetch(`/Academician/Question/Delete`, {
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
            toastr.success('Question successfully deleted!');
            setTimeout(() => {
                window.location.reload();
            }, 2500);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            toastr.error('An error occurred while deleting question.');
        });
}

function openCreateModal() {
    document.getElementById('editModalLabel').innerText = 'Create Question';
    document.getElementById('save-button').onclick = saveCreate;
    $('#editId').val();
    $('#editQuestionText').val();
    $('#editOption1').val();
    $('#editOption2').val();
    $('#editOption3').val();
    $('#editCorrectAnswer').val();
    $('#editModal').modal('show');
}
function saveCreate() {
    var data = {
        Id: $('#editId').val(),
        QuestionText: $('#editQuestionText').val(),
        Option1: $('#editOption1').val(),
        Option2: $('#editOption2').val(),
        Option3: $('#editOption3').val(),
        CorrectAnswer: $('#editCorrectAnswer').val(),
        ExamId: $('#editExamId').val()
    };
    fetch('/Academician/Question/Create', {
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
            toastr.success('Question successfully edited!');
            setTimeout(() => {
                window.location.reload();
            }, 2500);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            toastr.error('An error occurred while editing question.');
        });
}