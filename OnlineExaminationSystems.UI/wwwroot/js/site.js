document.addEventListener('DOMContentLoaded', function () {
    const navItems = [
        { url: '/Home/Index', text: 'Home' }        
    ];

    var role = JSON.parse(localStorage.getItem('jwt')).role;

    if (role === '1') {


    }
    else if (role === '2') {

    }
    else if (role === '3') {
        navItems.push({ url: '/User/Index', text: 'User' });
        navItems.push({ url: '/Lesson/Index', text: 'Lesson' });
    } else {
        return;
    }

    const navUl = document.getElementById('navbarNav');

    navItems.forEach(item => {
        const li = document.createElement('li');
        li.className = 'nav-item';
        const a = document.createElement('a');
        a.className = 'nav-link text-dark';
        a.setAttribute('href', item.url);
        a.textContent = item.text;
        li.appendChild(a);
        navUl.appendChild(li);
    });
});

function logout() {
    localStorage.clear();
    window.location.href = '/Login/Index';
}