document.addEventListener("DOMContentLoaded", function () {
    var current = window.location.pathname.split("/").pop().toLowerCase();
    document.querySelectorAll(".sidebar-nav a.nav-link").forEach(function (link) {
        var target = link.getAttribute("href").split("/").pop().toLowerCase();
        if (target === current) {
            link.classList.add("active");
            var group = link.closest(".nav-group");
            if (group) group.classList.add("has-active");
        }
    });
});
