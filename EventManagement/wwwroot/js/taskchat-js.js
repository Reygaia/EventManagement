document.addEventListener('DOMContentLoaded', function () {
    const chatMessages = document.getElementById('chat-messages');
    const chatTitle = document.getElementById('chat-title');
    const messageInput = document.getElementById('message-input');
    const sendMessageBtn = document.getElementById('send-message-btn');
    const taskList = document.getElementById('task-list');

    const contextMenu = document.getElementById('context-menu');
    const channelContextMenu = document.getElementById('channel-context-menu');
    const categoryContextMenu = document.getElementById('category-context-menu');

    const createChannelBtn = document.getElementById('create-channel-btn');
    const createCategoryBtn = document.getElementById('create-category-btn');
    const deleteChannelBtn = document.getElementById('delete-channel-btn');
    const createChannelInCategoryBtn = document.getElementById('create-channel-in-category-btn');
    const deleteCategoryBtn = document.getElementById('delete-category-btn');

    let selectedTaskElement = null;
    let selectedCategoryElement = null;

    const chats = {

    };

    const loadTasks = () => {
        for (const taskId in chats) {
            const taskElement = document.createElement('div');
            taskElement.classList.add('task');
            taskElement.setAttribute('data-task', taskId);
            taskElement.textContent = `Task ${taskId.slice(-1)}: ${taskId}`;
            addTaskListeners(taskElement);
            taskList.appendChild(taskElement);
        }
    };

    const addTaskListeners = (task) => {
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

        task.addEventListener('contextmenu', function (e) {
            e.preventDefault();
            hideAllContextMenus();
            selectedTaskElement = this;
            selectedCategoryElement = null;
            channelContextMenu.style.top = `${e.pageY}px`;
            channelContextMenu.style.left = `${e.pageX}px`;
            channelContextMenu.style.display = 'block';
        });
    };

    const addCategoryListeners = (category) => {
        category.addEventListener('click', function () {
            const tasksInCategory = this.nextElementSibling;
            if (tasksInCategory && tasksInCategory.classList.contains('category-tasks')) {
                tasksInCategory.style.display = tasksInCategory.style.display === 'none' ? 'block' : 'none';
            }
        });

        category.addEventListener('contextmenu', function (e) {
            e.preventDefault();
            hideAllContextMenus();
            selectedCategoryElement = this;
            selectedTaskElement = null;
            categoryContextMenu.style.top = `${e.pageY}px`;
            categoryContextMenu.style.left = `${e.pageX}px`;
            categoryContextMenu.style.display = 'block';
        });
    };

    taskList.addEventListener('contextmenu', function (e) {
        if (e.target === taskList) {
            e.preventDefault();
            hideAllContextMenus();
            selectedTaskElement = null;
            selectedCategoryElement = null;
            contextMenu.style.top = `${e.pageY}px`;
            contextMenu.style.left = `${e.pageX}px`;
            contextMenu.style.display = 'block';
        }
    });

    document.addEventListener('click', function () {
        hideAllContextMenus();
    });

    const hideAllContextMenus = () => {
        contextMenu.style.display = 'none';
        channelContextMenu.style.display = 'none';
        categoryContextMenu.style.display = 'none';
    };

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

    createChannelBtn.addEventListener('click', function () {
        const channelName = prompt('Enter the name of the new channel:');
        if (channelName) {
            const newTask = document.createElement('div');
            newTask.classList.add('task');
            newTask.setAttribute('data-task', `task${Object.keys(chats).length + 1}`);
            newTask.textContent = channelName;
            taskList.appendChild(newTask);

            chats[`task${Object.keys(chats).length + 1}`] = [];
            addTaskListeners(newTask);
        }
    });

    createCategoryBtn.addEventListener('click', function () {
        const categoryName = prompt('Enter the name of the new category:');
        if (categoryName) {
            const newCategory = document.createElement('div');
            newCategory.classList.add('category');
            newCategory.textContent = categoryName;
            taskList.appendChild(newCategory);

            const tasksInCategory = document.createElement('div');
            tasksInCategory.classList.add('category-tasks');
            tasksInCategory.style.display = 'none';
            taskList.appendChild(tasksInCategory);

            addCategoryListeners(newCategory);
        }
    });

    deleteChannelBtn.addEventListener('click', function () {
        if (selectedTaskElement) {
            const taskId = selectedTaskElement.getAttribute('data-task');
            delete chats[taskId];
            selectedTaskElement.remove();
            selectedTaskElement = null;
            chatTitle.textContent = 'Select a task to chat';
            chatMessages.innerHTML = '';
        }
    });

    createChannelInCategoryBtn.addEventListener('click', function () {
        if (selectedCategoryElement) {
            const channelName = prompt('Enter the name of the new channel:');
            if (channelName) {
                const newTask = document.createElement('div');
                newTask.classList.add('task');
                newTask.setAttribute('data-task', `task${Object.keys(chats).length + 1}`);
                newTask.textContent = channelName;
                selectedCategoryElement.nextElementSibling.appendChild(newTask);

                chats[`task${Object.keys(chats).length + 1}`] = [];
                addTaskListeners(newTask);
            }
        }
    });

    deleteCategoryBtn.addEventListener('click', function () {
        if (selectedCategoryElement) {
            selectedCategoryElement.nextElementSibling.remove();
            selectedCategoryElement.remove();
            selectedCategoryElement = null;
        }
    });

    loadTasks();
});
