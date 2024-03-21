// const uri="/api/login"
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
const uri = "/api/login"


// if (localStorage.getItem('token')) { // != null) {
//     console.log(localStorage.getItem('token'));
//     window.location = "html/home.html";
// }

function login() {

    const name = document.getElementById('name').value.trim();
    const password = document.getElementById('password').value.trim();

    fetch(uri, {
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
            console.log(data);
            saveToken(data);
        })
        .catch(error => console.error('Unable to save token.', error));
}

function saveToken(token) {
    localStorage.setItem("token", token);
    window.location = "html/home.html";
}