document.getElementById("loginForm").addEventListener("submit", function (event) {
    event.preventDefault();

    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;

    var loginData = {
        Email: email,
        Password: password
    };
    fetch("/Login/Login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(loginData)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Login failed");
            }
            return response.json();
        })
        .then(token => {
            localStorage.setItem('jwt', JSON.stringify(decodeJWT(token)));
            window.location.href = "/Home/Index"; 
        })
        .catch(error => {
            console.error("Login error:", error);
            alert("Login failed: " + error.message);
        });
});

function decodeJWT(token) {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(atob(base64).split('').map((c) => {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}