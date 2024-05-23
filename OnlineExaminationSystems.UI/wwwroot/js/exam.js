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
