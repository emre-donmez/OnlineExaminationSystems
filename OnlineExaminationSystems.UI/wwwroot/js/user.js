function togglePassword(element) {
    var password = element.getAttribute('data-password');
    if (element.innerText === '●●●●●●') {
        element.innerText = password;
    } else {
        element.innerText = '●●●●●●';
    }
}

function openEditModal(id, name, surname, email, password, roleId) {
    document.getElementById('editModalLabel').innerText = 'Edit User';
    document.getElementById('save-button').onclick = saveEdit;
    $('#editId').val(id);
    $('#editName').val(name);
    $('#editSurname').val(surname);
    $('#editEmail').val(email);
    $('#editPassword').val(password);
    $('#editRoleId').val(roleId);
    $('#editModal').modal('show');
}

function saveEdit() {
    var data = {
        Id: $('#editId').val(),
        Name: $('#editName').val(),
        Surname: $('#editSurname').val(),
        Email: $('#editEmail').val(),
        Password: $('#editPassword').val(),
        RoleId: $('#editRoleId').val()
    };
    fetch('/User/Edit', {
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
            toastr.success('User successfully edited!');
            setTimeout(() => {
                window.location.reload();
            }, 2500);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            toastr.error('An error occurred while editing user.');
        });
}

function openCreateModal() {
    document.getElementById('editModalLabel').innerText = 'Create User';
    document.getElementById('save-button').onclick = saveCreate;
    $('#editId').val('');
    $('#editName').val('');
    $('#editSurname').val('');
    $('#editEmail').val('');
    $('#editPassword').val('');
    $('#editRoleId').val('');
    $('#editModal').modal('show');
}

function saveCreate() {
    var data = {
        Name: $('#editName').val(),
        Surname: $('#editSurname').val(),
        Email: $('#editEmail').val(),
        Password: $('#editPassword').val(),
        RoleId: $('#editRoleId').val()
    };
    fetch('/User/Create', {
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
            toastr.success('User successfully created!');
            setTimeout(() => {
                window.location.reload();
            }, 2500);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            toastr.error('An error occurred while creating user.');
        });
}

function deleteUser(id) {
    if (!confirm('Are you sure you want to delete this user?')) return;

    fetch('/User/Delete', {
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
            toastr.success('User successfully deleted!');
            setTimeout(() => {
                window.location.reload();
            }, 2500);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            toastr.error('An error occurred while deleting user.');
        });
}
