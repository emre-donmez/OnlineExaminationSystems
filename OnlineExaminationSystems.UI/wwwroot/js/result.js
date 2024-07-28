document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('calculate-button').addEventListener('click', function () {
        fetch('Result/CalculateResult', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify($("#examId").val())
        })
            .then(response => {
                if (response.ok) {
                    toastr.success('Calculeted Results!');
                    setTimeout(() => {
                        window.location.reload();
                    }, 2500);
                }
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
                toastr.error('An error occurred while calculating results.');
            });
    });
});