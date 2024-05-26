function openEditModal(id, name, userId) {
    document.getElementById('editModalLabel').innerText = 'Edit Lesson';
    document.getElementById('save-button').onclick = saveEdit;
    $('#editId').val(id);
    $('#editName').val(name);
    $('#editUserId').val(userId);
    $('#editModal').modal('show');
}

function saveEdit() {
    var data = {
        Id: $('#editId').val(),
        Name: $('#editName').val(),
        UserId: $('#editUserId').val()
    };
    fetch('/Lesson/Edit', {
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
            toastr.success('Lesson successfully edited!');
            setTimeout(() => {
                window.location.reload();
            }, 2500);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            toastr.error('An error occurred while editing lesson.');
        });
}

function openCreateModal() {
    document.getElementById('editModalLabel').innerText = 'Create Lesson';
    document.getElementById('save-button').onclick = saveCreate;
    $('#editId').val('');
    $('#editName').val('');
    $('#editUserId').val('');
    $('#editModal').modal('show');
}

function saveCreate() {
    var data = {
        Name: $('#editName').val(),
        UserId: $('#editUserId').val()       
    };
    fetch('/Lesson/Create', {
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
            toastr.success('Lesson successfully created!');
            setTimeout(() => {
                window.location.reload();
            }, 2500);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            toastr.error('An error occurred while creating lesson.');
        });
}

function deleteEntity(id) {
    if (!confirm('Are you sure you want to delete this lesson?')) return;

    fetch('/Lesson/Delete', {
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
            toastr.success('Lesson successfully deleted!');
            setTimeout(() => {
                window.location.reload();
            }, 2500);
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            toastr.error('An error occurred while deleting lesson.');
        });
}

let users = [];
let lessonId;
const modalContainer = document.getElementById('modal-container');

async function openStudentsModal(id) {
    $('#studentsModal').modal('show');
    lessonId = id;
    fillModal();
}

async function fillModal() {
    const students = await getStudents();
    modalContainer.innerHTML = '';
    students.forEach(student => { createInput(student) });
}

let removedEnrollmentsIds = [];
function createInput(enrollment) {
    removedEnrollmentsIds = [];
    var user = enrollment.user;
    var inputElement = document.createElement("input");
    inputElement.className = "form-control";
    inputElement.type = "text";
    inputElement.disabled = true;
    inputElement.readOnly = true;
    inputElement.value = user.email + ' | ' + user.name + ' ' + user.surname;

    const deleteBtn = document.createElement('button');
    deleteBtn.className = 'btn btn-sm btn-danger';
    deleteBtn.textContent = 'Delete';
    deleteBtn.addEventListener('click', function () {
        const container = this.parentNode;
        removedEnrollmentsIds.push(enrollment.id);
        container.parentNode.removeChild(container);
    });

    const container = document.createElement('div');
    container.className = 'input-group mb-2';
    container.appendChild(inputElement);
    container.appendChild(deleteBtn);
    modalContainer.appendChild(container)
}

async function getStudents() {
    try {
        const response = await fetch(`/Enrollment/GetStudentsByLessonId?lessonId=${lessonId}`);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
        toastr.error('An error occurred while creating lesson.');
        return null; 
    }
}

function createSelect() {
    var copiedSelect = document.createElement('select');
    copiedSelect.className = 'form-control form-select mt-3';
    users.filter(user => user.RoleId == 2).forEach(user => {
        var copiedOption = document.createElement('option');
        copiedOption.value = user.Id;
        copiedOption.text = user.Email + ' | ' + user.Name + ' ' + user.Surname;
        copiedSelect.appendChild(copiedOption);
    });

    modalContainer.appendChild(copiedSelect);
}

document.getElementById('save-student-modal-button').addEventListener('click', async () => {

    if (!confirm('Are you sure you want to save changes?')) return;

    var enrollments = [];
    var copiedSelects = document.querySelectorAll('#modal-container select');
    copiedSelects.forEach(function (select) {
        var selectedOption = select.options[select.selectedIndex];
        if (selectedOption) {
            enrollments.push(new Enrollment(selectedOption.value));
        }
    });
    if (removedEnrollmentsIds.length !== 0) {
    try {
        const response = await fetch('/Enrollment/Delete', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(removedEnrollmentsIds)
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        toastr.success('Student successfully deleted to lesson!');
        setTimeout(() => {
            window.location.reload();
        }, 2500);
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
        toastr.error('An error occurred while deleting student to lesson.');
    }
    }
    if (enrollments.length !== 0) {
    try {
        const response = await fetch('/Enrollment/Create', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(enrollments)
        });
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        toastr.success('Student successfully added to lesson!');
        setTimeout(() => {
            window.location.reload();
        }, 2500);
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
        toastr.error('An error occurred while adding student to lesson.');
        }
    }
});
class Enrollment {
    constructor(userId) {
        this.LessonId = lessonId;
        this.UserId = userId;
    }
}

document.addEventListener('DOMContentLoaded', function () {
    var message = document.getElementById('users').getAttribute('data-users');
    users = JSON.parse(message);
});
