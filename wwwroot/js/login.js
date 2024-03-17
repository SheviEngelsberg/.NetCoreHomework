
const uri="/api/login"
const login= ()=>{
    const user={
        id:0,
        name: document.getElementById('name'),
        password: document.getElementById('password'),
        type:"user"
    }
    fetch(uri,{
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(user)
    })
    .then(response =>response.text())
    .then(data=>{
        saveToken(data);
    })
    .catch(error => console.error('Unable to save token.', error));
}

function saveToken(token) {
    sessionStorage.setItem("token",token);
}


