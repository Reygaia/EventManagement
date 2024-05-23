document.addEventListener('DOMContentLoaded', (event) => {
    const email = "example@example.com"; // Giả sử đây là email người dùng đã nhập khi tạo tài khoản
    const createdDate = new Date(); // Ngày tạo tài khoản hiện tại

    // Hiển thị email
    document.getElementById('email').innerText = email;

    // Hiển thị ngày tạo tài khoản
    const options = {
        year: 'numeric', month: '2-digit', day: '2-digit',
        hour: '2-digit', minute: '2-digit', second: '2-digit',
        hour12: true
    };
    document.getElementById('createdDate').innerText = createdDate.toLocaleString('vi-VN', options);
});

//Thay đổi Avatar
function changeAvatar() {
    const avatarInput = document.getElementById('avatarInput');
    avatarInput.click();
    avatarInput.onchange = function () {
        const file = avatarInput.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                document.getElementById('avatarImage').src = e.target.result;
                addActivity('Thay đổi ảnh đại diện');
            };
            reader.readAsDataURL(file);
        }
    };
}

//Thay đổi tên người dùng (Username)
function changeUsername() {
    const usernameInput = document.getElementById('username');
    alert(`Tên người dùng đã được thay đổi thành: ${usernameInput.value}`);
    addActivity('Thay đổi tên người dùng');
}

//Cập nhật các hoạt động của người dùng
function addActivity(activity) {
    const activitiesList = document.getElementById('recentActivities');
    const listItem = document.createElement('li');
    const currentDate = new Date();
    const options = {
        year: 'numeric', month: '2-digit', day: '2-digit',
        hour: '2-digit', minute: '2-digit', second: '2-digit',
        hour12: true
    };
    listItem.innerText = `${activity} - ${currentDate.toLocaleString('vi-VN', options)}`;
    activitiesList.insertBefore(listItem, activitiesList.firstChild);
}
