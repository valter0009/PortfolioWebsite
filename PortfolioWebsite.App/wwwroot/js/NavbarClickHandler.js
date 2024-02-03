function setupOutsideClickHandler(navbarId, dotNetHelper) {
    window.addEventListener('click', function (event) {
        var navbar = document.getElementById(navbarId);
        if (navbar && !navbar.contains(event.target)) {
            dotNetHelper.invokeMethodAsync('CollapseNavbar');
        }
    });
}