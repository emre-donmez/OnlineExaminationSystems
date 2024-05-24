document.getElementById('exam-end-button').addEventListener('click', function () {
    const radioButtons = document.querySelectorAll('.form-check-input[type="radio"]:checked');
    const userId = document.getElementById('userId').value;
    let answers = [];

    radioButtons.forEach(radio => {
        const questionId = radio.getAttribute('data-question-id');
        const selectedAnswer = radio.value;
        answers.push(new Answer(userId, questionId, selectedAnswer));
    });

    fetch('/Student/SubmitExam', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(answers)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            toastr.success('Exam done!');
            setTimeout(() => {
                window.location.reload();
            }, 2500);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            toastr.error('An error occurred while submitting exam.');
        });
});

class Answer{
    constructor(userId, questionId, givenAnswer) {
        this.userId = userId;
        this.questionId = questionId;
        this.givenAnswer = givenAnswer;
    }
};

function openEditModal(id,name,lessonId,questionCount,duration,startedDate) {
    document.getElementById('editModalLabel').innerText = 'Edit Exam';
    document.getElementById('save-button').onclick = saveEdit;
    $('#editId').val(id);
    $('#editName').val(name);
    $('#editLessonId').val(lessonId);
    $('#editQuestionCount').val(questionCount);
    $('#editDuration').val(duration);
    $('#editStartDate').val(new Date(startedDate).toISOString().split('T')[0]);
    $('#editModal').modal('show');
}
function saveEdit() {
    var data = {
        Id: $('#editId').val(),
        Name: $('#editName').val(),
        LessonId: $('#editLessonId').val(),
        QuestionCount: $('#editQuestionCount').val(),
        Duration: $('#editDuration').val(),
        StartedDate: $('#editStartDate').val()
    };
    fetch('/Exam/Edit', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                $('#editModal').modal('hide');
                location.reload(); 
            } else { 
                alert('Error: ' + data.message);
            }
        })
        .catch(error => console.error('Error:', error));
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


