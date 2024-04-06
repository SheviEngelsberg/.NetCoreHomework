const todoUrl = '/api/todo';
const userUrl = '/api/user'
let tasks = [];
const token = localStorage.getItem("token");
const Authorization = `Bearer ${token}`;

getTasks();
IsAdmin();

function getUserById() {
    fetch(userUrl, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': Authorization
        },

    })
        .then(response => {
            if (response.status != 200) {
                throw new Error('Failed to fetch data');
            }
            return response.json();
        })
        .then(data =>{
            document.getElementById('editUser-name').value =data.name;
            document.getElementById('editUser-password').value=data.password;
        })
        .catch(error => {
            console.error('Unable to get items.', error);
            window.location.href = "../index.html";
        });
}

function getTasks() {
    fetch(todoUrl, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': Authorization
        },

    })
        .then(response => {
            if (response.status != 200) {
                throw new Error('Failed to fetch data');
            }
            return response.json();
        })
        .then(data => displayTasks(data))
        .catch(error => {
            console.error('Unable to get items.', error);
            window.location.href = "../index.html";
        });
}

function addTask() {

    const addNameTextbox = document.getElementById('add-name');

    const task = {
        id: 0,
        name: addNameTextbox.value.trim(),
        isDone: false,
        userId: 0
    };

    fetch(todoUrl, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': Authorization
        },
        body: JSON.stringify(task)
    })
        .then(response => response.json())
        .then(() => {
            getTasks();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteTask(id) {
    fetch(`${todoUrl}/${id}`, {

        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': Authorization
        },
    })
        .then(() => getTasks())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const task = tasks.find(t => t.id === id);
    document.getElementById('edit-name').value = task.name;
    document.getElementById('edit-id').value = task.id;
    document.getElementById('edit-isDone').checked = task.isDone;
    document.getElementById('editForm').style.display = 'block';
}

function updateTask() {

    const taskId = document.getElementById('edit-id').value;
    const task = {
        Id: taskId,
        Name: document.getElementById('edit-name').value.trim(),
        IsDone: document.getElementById('edit-isDone').checked,
        UserId: 0
    };
    fetch(`${todoUrl}/${taskId}`, {

        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': Authorization

        },
        body: JSON.stringify(task)
    })
        .then(() => getTasks())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function displayCount(taskCount) {
    const name = ( taskCount === 1) ? 'task' : 'tasks';
    document.getElementById('counter').innerText = ` You have ${taskCount} ${name} on your task list! Successfully✨`;
}

function displayTasks(data) {
    const tBody = document.getElementById('Tasks');
    tBody.innerHTML = '';

    displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(task => {
        let isDoneCheckbox = document.createElement('input');
        isDoneCheckbox.type = 'checkbox';
        isDoneCheckbox.disabled = true;
        isDoneCheckbox.checked = task.isDone;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${task.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteTask(${task.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isDoneCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(task.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    tasks = data;
}
function IsAdmin() {
    fetch('/Admin', {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': Authorization
        },
        body: JSON.stringify()
    })
        .then(res => {
            if (res.status === 200)
                usersButten();
        })
        .catch()
}
const usersButten = () => {
    const linkToUsers = document.getElementById('forAdmin');
    linkToUsers.hidden = false;
}
function displayUserDetails() {
    const UserDetails = document.getElementById('editUserForm');
    UserDetails.hidden = false;

}
function closeeditUserInput() {
    document.getElementById('editUserForm').hidden = true;
}

function updateUser() {
    getUserById();
    const user = {
        Id: 0,
        Name: document.getElementById('editUser-name').value.trim(),
        Password: document.getElementById('editUser-password').value.trim(),
        Type: ""
    };
    fetch(userUrl, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': Authorization
        },
        body: JSON.stringify(user)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to update user');
            }        })
        .then(()=>
            {
                alert('update...');
                closeeditUserInput();

            }
         )
        .catch(error => {
            console.error('Unable to update user.', error);
            alert('Failed to update user. Please try again.');
        });
}
