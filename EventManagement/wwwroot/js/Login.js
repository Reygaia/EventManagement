async function loginUser() {
    // Get the email and password from the input fields
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;

    // Create the request body
    const requestBody = {
        email: email,
        password: password
    };

    // Send a POST request to the API endpoint
    try {
        const response = await fetch('/api/authenticate/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestBody)
        });

        // Check if the response is successful
        if (response.ok) {
            const data = await response.json();
            console.log(data); // Do something with the response data
            localStorage.setItem('accessToken', data.accessToken);
        } else {
            const errorMessage = await response.text();
            console.error('Error:', errorMessage);
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

document.getElementById('loginButton').addEventListener('click', loginUser);