document.addEventListener('DOMContentLoaded', () => {
    const toggleHamburger = () => {
        const toggler = document.querySelector('.navbar-toggler');
        const navbarCollapse = document.getElementById('navbarCollapse');

        const collapseMenu = () => {
            if (navbarCollapse.classList.contains('show')) {
                toggler.click(); // Simulate a click to collapse the navbar
            }
        };

        const toggleRotation = () => {
            toggler.classList.toggle('rotate');
            toggler.classList.toggle('rotate-back');
        };

        const isClickInsideNav = (event) => {
            return toggler.contains(event.target) || navbarCollapse.contains(event.target);
        };

        const addClickListener = (selector, handler) => {
            document.querySelectorAll(selector).forEach(link => {
                link.addEventListener('click', handler);
            });
        };

        // Toggle the menu and rotation on toggler click
        toggler.addEventListener('click', (event) => {
            event.stopPropagation(); // Prevent bubbling up
            toggleRotation();
        });

        // Collapse the menu on click outside
        document.addEventListener('click', (event) => {
            if (!isClickInsideNav(event)) {
                collapseMenu();
            }
        });

        // Collapse the menu when a navbar or cart menu link is clicked

        addClickListener('.click-collapse', collapseMenu);
    };

    window.toggleHamburger = toggleHamburger; // Expose to global scope if needed
    toggleHamburger(); // Initialize the functionality
});