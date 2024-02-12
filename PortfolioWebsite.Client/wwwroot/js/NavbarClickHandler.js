document.addEventListener("DOMContentLoaded", () => {
  const toggleHamburger = () => {
    const toggler = document.querySelector(".navbar-toggler");
    const navbarCollapse = document.getElementById("navbarCollapse");

    const collapseMenu = () => {
      if (navbarCollapse.classList.contains("show")) {
        toggler.click();
      }
    };

    toggler.addEventListener("click", function (event) {
      event.stopPropagation();

      if (this.classList.contains("rotate")) {
        this.classList.remove("rotate");
        this.classList.add("rotate-back");
      } else {
        this.classList.remove("rotate-back");
        this.classList.add("rotate");
      }
    });

    const isClickInsideNav = (event) => {
      return (
        toggler.contains(event.target) || navbarCollapse.contains(event.target)
      );
    };

    const addClickListener = (selector, handler) => {
      document.querySelectorAll(selector).forEach((link) => {
        link.addEventListener("click", handler);
      });
    };

    document.addEventListener("click", (event) => {
      if (!isClickInsideNav(event)) {
        collapseMenu();
      }
    });

    addClickListener(".click-collapse", collapseMenu);
  };

  window.toggleHamburger = toggleHamburger;
  toggleHamburger();
});
