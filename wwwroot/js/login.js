// const loginUrl="/api/login"
// function login(){

//     const name= document.getElementById('name').value.trim();
//     const password= document.getElementById('password').value.trim();

//     fetch(uri,{
//         method: 'POST',
//         headers: {
//             'Accept': 'application/json',
//             'Content-Type': 'application/json',
//         },
//         body: JSON.stringify({name:name,password:password})
//     })
//     .then(response =>response.text())
//     .then(data=>{
//         saveToken(data);
//     })
//     .catch(error => console.error('Unable to save token.', error));
// }

// function saveToken(token) {
//     sessionStorage.setItem("token",token);
//     window.location="html/home.html";
// }
// const uri = "/api/login"


// if (localStorage.getItem('token')) { // != null) {
//     console.log(localStorage.getItem('token'));
//     window.location = "html/home.html";
// }

function login() {

    const name = document.getElementById('name').value.trim();
    const password = document.getElementById('password').value.trim();

    fetch(loginUrl, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                Id: 0,
                Name: name,
                Password: password,
                Type: "string"
            })
        })
        .then(response => response.json())
        .then(data => {
            saveToken(data);
        })
        .catch(error => console.error('Unable to save token.', error));
}


function Login(name, password) {
    fetch(loginUrl, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            Id: 0,
            Name: name,
            Password: password,
            Type: "string"
        })
    })
    .then(response => response.json())
    .then(data => {
        saveToken(data);
    })
    .catch(error => console.error('Unable to save token.', error));
}

function handleCredentialResponse(response) {
    if (response.credential) {
        var idToken = response.credential;
        var decodedToken = parseJwt(idToken);
        var userId = decodedToken.sub;
        var userName = decodedToken.name;
        Login(userName, userId);
    } else {
        alert('Google Sign-In was cancelled.');
    }
}

function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}

function saveToken(token) {
    localStorage.setItem("token", token);
    window.location = "html/home.html";

}