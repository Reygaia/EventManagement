// Lấy tất cả các thẻ a trong menu
const menuItems = document.querySelectorAll('.sidebar nav ul li a');

// Lắng nghe sự kiện click cho mỗi thẻ a
menuItems.forEach(function(item) {
    item.addEventListener('click', function(event) {
        // Loại bỏ class "active" từ tất cả các thẻ a
        menuItems.forEach(function(item) {
            item.classList.remove('active');
        });
        // Thêm class "active" vào thẻ a được click
        this.classList.add('active');
    });
});
