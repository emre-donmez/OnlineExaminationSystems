document.addEventListener('DOMContentLoaded', function () {
    const navItems = [
        { url: '/Home/Index', text: 'Home' }
    ];

    var jwt = JSON.parse(localStorage.getItem('jwt'));
    var role = jwt.role;
    var userId = jwt.nameid;

    if (role === '1') {
        navItems.push({
            type: 'form',
            action: window.location.origin + '/Lesson/Academician',
            method: 'post',
            userId: userId,
            buttonText: 'Lesson'
        });
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
        if (item.type === 'form') {
            const li = document.createElement('li');
            li.className = 'nav-item';
            const form = document.createElement('form');
            form.setAttribute('action', item.action);
            form.setAttribute('method', item.method);

            const input = document.createElement('input');
            input.setAttribute('type', 'hidden');
            input.setAttribute('name', 'userId');
            input.setAttribute('value', item.userId);

            const button = document.createElement('button');
            button.setAttribute('type', 'submit');
            button.className = 'btn btn-link nav-link text-dark';
            button.textContent = item.buttonText;

            form.appendChild(input);
            form.appendChild(button);
            li.appendChild(form);
            navUl.appendChild(li);
        } else {
            const li = document.createElement('li');
            li.className = 'nav-item';
            const a = document.createElement('a');
            a.className = 'nav-link text-dark';
            a.setAttribute('href', item.url);
            a.textContent = item.text;
            li.appendChild(a);
            navUl.appendChild(li);
        }
    });
});

function logout() {
    localStorage.clear();
    window.location.href = '/Login/Index';
}