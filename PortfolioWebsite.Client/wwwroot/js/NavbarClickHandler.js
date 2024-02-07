document.addEventListener('DOMContentLoaded', function () {

    window.toggleHamburger = function () {
        var toggler = document.querySelector('.navbar-toggler');
        var navbarCollapse = document.getElementById('navbarCollapse');
        let lastScrollTop = 0;

        // Function to collapse the menu
        function collapseMenu() {
            if (navbarCollapse.classList.contains('show')) {
                toggler.click(); // Simulate a click on the toggler to collapse the navbar
            }
        }

        // Toggle the menu when the toggler button is clicked
        toggler.addEventListener('click', function (event) {
            event.stopPropagation(); // Prevent the event from bubbling up to the document level

            if (this.classList.contains('rotate')) {
                this.classList.remove('rotate');
                this.classList.add('rotate-back');
            } else {
                this.classList.remove('rotate-back');
                this.classList.add('rotate');
            }
        });

        // Listen for clicks outside the navigation menu to collapse it
        document.addEventListener('click', function (event) {
            var isClickInsideNav = toggler.contains(event.target) || navbarCollapse.contains(event.target);

            if (!isClickInsideNav) {
                collapseMenu();
            }
        });



        // Collapse the menu when a link is clicked
        var navLinks = document.querySelectorAll('.navbar-nav a.nav-link');
        navLinks.forEach(function (link) {
            link.addEventListener('click', function () {
                collapseMenu();
            });
        });

        var cartMenuLinks = document.querySelectorAll('.cart-menu');
        cartMenuLinks.forEach(function (link) {
            link.addEventListener('click', function () {
                collapseMenu();
            });
        });
    }
});




