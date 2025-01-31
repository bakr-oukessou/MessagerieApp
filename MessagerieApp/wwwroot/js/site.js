document.addEventListener("DOMContentLoaded", function () {
    const sidebarLinks = document.querySelectorAll('.sidebar .nav-link');
    const mainContent = document.getElementById('main-content');

    // Debug: Check if mainContent exists
    if (!mainContent) {
        console.error('main-content element not found');
        return;
    }

    // Handle sidebar link clicks
    sidebarLinks.forEach(link => {
        link.addEventListener('click', function (event) {
            event.preventDefault();
            const page = this.getAttribute('data-page');
            console.log('Loading page:', page);
            loadPage(page);
        });
    });

    // Function to load page content
    function loadPage(page) {
        fetch(`/${page}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`Network response was not ok: ${response.statusText}`);
                }
                return response.text();
            })
            .then(html => {
                // Extract the content of the main-content div from the fetched HTML
                const parser = new DOMParser();
                const doc = parser.parseFromString(html, 'text/html');
                const newContent = doc.getElementById('main-content');

                if (newContent) {
                    // Update the main content area
                    mainContent.innerHTML = newContent.innerHTML;
                } else {
                    console.error('main-content element not found in fetched HTML');
                }
            })
            .catch(error => {
                console.error('Error loading page:', error);
            });
    }

    // Handle internal link clicks
    document.addEventListener('click', function (e) {
        const link = e.target.closest('a');

        // Check if it's a navigation link
        if (link && link.getAttribute('data-page')) {
            e.preventDefault();
            const page = link.getAttribute('data-page');
            loadPage(page);
        }
    });

    // Update active navigation link
    function updateActiveLink(activePage) {
        // Remove active class from all links
        document.querySelectorAll('.nav-link').forEach(link => {
            link.classList.remove('active');
        });

        // Add active class to the current page link
        const activeLink = document.querySelector(`.nav-link[data-page="${activePage}"]`);
        if (activeLink) {
            activeLink.classList.add('active');
        }
    }

    // Handle browser back/forward
    window.addEventListener('popstate', function (e) {
        if (e.state && e.state.page) {
            loadPage(e.state.page);
        }
    });
});