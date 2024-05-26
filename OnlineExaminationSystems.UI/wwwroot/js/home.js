document.addEventListener('DOMContentLoaded', function () {
    var userName = JSON.parse(localStorage.getItem('jwt')).unique_name;

    if (userName) {
        document.getElementById("welcomeMessage").innerHTML = "Welcome to Our Website, " + userName + "!";
    } else {
        document.getElementById("welcomeMessage").innerHTML = "Welcome to Our Website!";
    }
});