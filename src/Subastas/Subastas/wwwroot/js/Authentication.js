document.addEventListener("DOMContentLoaded", function () {
    setTimeout(function () {
        var alerts = document.querySelectorAll('.fade-out');
        alerts.forEach(function (alert) {
            alert.style.transition = "opacity 1s ease-in-out";
            alert.style.opacity = "0";
            setTimeout(function () {
                alert.style.display = "none";
            }, 1000);
        });
    }, 2500); // 3000 ms = 3 seconds
});