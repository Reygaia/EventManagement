document.getElementById('menuIcon').addEventListener('click', function () {
    var sidebar = document.getElementById('sidebar');
    sidebar.classList.toggle('open');
    document.querySelector('.main-content').classList.toggle('expanded');
});

document.getElementById('profilePic').addEventListener('click', function () {
    var profileDropdown = document.getElementById('profileDropdown');
    profileDropdown.style.display = profileDropdown.style.display === 'block' ? 'none' : 'block';
});

document.getElementById('addIcon').addEventListener('click', function () {
    var addDropdown = document.getElementById('addDropdown');
    addDropdown.style.display = addDropdown.style.display === 'block' ? 'none' : 'block';
});

document.getElementById('joinEvent').addEventListener('click', function () {
    document.getElementById('joinEventModal').style.display = 'block';
    document.getElementById('addDropdown').style.display = 'none';
});

document.getElementById('createEvent').addEventListener('click', function () {
    document.getElementById('createEventModal').style.display = 'block';
    document.getElementById('addDropdown').style.display = 'none';
});

var modals = document.querySelectorAll('.modal');
modals.forEach(function (modal) {
    modal.querySelector('.close').addEventListener('click', function () {
        modal.style.display = 'none';
    });
    modal.querySelector('.cancel').addEventListener('click', function () {
        modal.style.display = 'none';
    });
});

window.addEventListener('click', function (event) {
    if (event.target.classList.contains('modal')) {
        event.target.style.display = 'none';
    }
});

document.querySelector('.nav .has-dropdown > a').addEventListener('click', function () {
    var parent = this.parentElement;
    parent.classList.toggle('open');
});
