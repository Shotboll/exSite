document.addEventListener("DOMContentLoaded", function () {
    const loader = document.getElementById("pageLoader");

    function showLoader() {
        if (loader) {
            loader.style.display = "flex";
        }
    }

    function processForms() {
        const forms = document.querySelectorAll("form");

        forms.forEach(function (form) {
            form.addEventListener("submit", function () {
                if (form.checkValidity && !form.checkValidity()) {
                    return;
                }

                const buttons = form.querySelectorAll("button[type='submit']");

                buttons.forEach(function (button) {
                    button.disabled = true;
                    button.innerText = "Загрузка...";
                });

                showLoader();
            });
        });
    }

    function processLinks() {
        const links = document.querySelectorAll("a");

        links.forEach(function (link) {
            link.addEventListener("click", function () {
                const href = link.getAttribute("href");

                if (!href) {
                    return;
                }

                if (href.startsWith("#") || href.startsWith("javascript:")) {
                    return;
                }

                if (link.target === "_blank") {
                    return;
                }

                showLoader();
            });
        });
    }

    processForms();
    processLinks();
});