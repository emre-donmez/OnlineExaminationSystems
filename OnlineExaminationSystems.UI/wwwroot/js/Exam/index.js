document.addEventListener('DOMContentLoaded', function () {
    var startButtons = document.querySelectorAll('.start-exam');
    startButtons.forEach(function (button) {
        button.addEventListener('click', function (event) {
            var confirmStart = confirm("The exam will begin. Are you sure?");
            if (!confirmStart) {
                event.preventDefault();
            }
        });
    });
});