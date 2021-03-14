$(document).ready(function () {

    function getUrl() {
        return window.location.pathname;
    }

    function Alert(title, icon) {
        Swal.fire({
            position: 'top',
            icon: icon,
            title: title,
            showConfirmButton: false,
            timer: 1000
        });
    }
});