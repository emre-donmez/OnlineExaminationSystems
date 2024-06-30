document.getElementById('exam-end-button').addEventListener('click', function () {
    let answers = [];
    const questionCount = document.getElementById('questionCount').value;
    for (let i = 0; i < questionCount; i++) {
        const selectedOption = document.querySelector('input[name="question_' + i + '"]:checked');
        if (selectedOption) {
            const questionId = selectedOption.getAttribute('data-question-id');
            const selectedAnswer = selectedOption.value;
            answers.push(new Answer(questionId, selectedAnswer));
        }
    }

    fetch('/Student/Exam/SubmitExam', {
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
                window.location.href = "/Student/Lesson";
            }, 2500);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            toastr.error('An error occurred while submitting exam.');
        });
});

class Answer {
    constructor(questionId, givenAnswer) {
        this.questionId = questionId;
        this.givenAnswer = givenAnswer;
    }
};