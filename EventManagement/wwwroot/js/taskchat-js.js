document.addEventListener('DOMContentLoaded', function () {
    const tasks = document.querySelectorAll('.task');
    const chatMessages = document.getElementById('chat-messages');
    const chatTitle = document.getElementById('chat-title');
    const messageInput = document.getElementById('message-input');
    const sendMessageBtn = document.getElementById('send-message-btn');

    const chats = {
        task1: [
            { username: 'User1', message: 'Completed the proposal draft.' },
            { username: 'User2', message: 'Great! I will review it.' },
        ],
        task2: [
            { username: 'User3', message: 'Reviewed the codebase, found some issues.' },
            { username: 'User4', message: 'Please share the details.' },
        ],
        task3: [
            { username: 'User5', message: 'Started designing the new feature.' },
            { username: 'User6', message: 'Looking forward to seeing it.' },
        ],
        task4: [
            { username: 'User7', message: 'Writing unit tests for the new module.' },
            { username: 'User8', message: 'Ensure to cover edge cases.' },
        ],
    };

    tasks.forEach(task => {
        task.addEventListener('click', function () {
            const taskId = this.getAttribute('data-task');
            const chatData = chats[taskId];
            chatTitle.textContent = this.textContent;
            chatMessages.innerHTML = '';

            if (chatData) {
                chatData.forEach(chat => {
                    const messageElement = document.createElement('div');
                    messageElement.classList.add('message');
                    messageElement.innerHTML = `<span class="username">${chat.username}:</span> <span class="message-content">${chat.message}</span>`;
                    chatMessages.appendChild(messageElement);
                });
            }
        });
    });

    const sendMessage = () => {
        const message = messageInput.value.trim();
        if (message !== '') {
            const messageElement = document.createElement('div');
            messageElement.classList.add('message');
            messageElement.innerHTML = `<span class="username">You:</span> <span class="message-content">${message}</span>`;
            chatMessages.appendChild(messageElement);
            messageInput.value = ''; // Clear input field after sending message
            chatMessages.scrollTop = chatMessages.scrollHeight; // Scroll to the bottom
        }
    };

    sendMessageBtn.addEventListener('click', sendMessage);
    messageInput.addEventListener('keypress', function (e) {
        if (e.key === 'Enter') {
            sendMessage();
        }
    });
});
