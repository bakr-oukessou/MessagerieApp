﻿document.addEventListener("DOMContentLoaded", function () {
    const sidebarLinks = document.querySelectorAll('.sidebar-nav .nav-link');
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

            // Remove active class from all links
            sidebarLinks.forEach(link => link.classList.remove('active'));

            // Add active class to the clicked link
            this.classList.add('active');

            // Load the page content
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

    // Handle browser back/forward
    window.addEventListener('popstate', function (e) {
        if (e.state && e.state.page) {
            loadPage(e.state.page);
        }
    });
});

    // Add Modal Functions
    function openAddModal() {
        document.getElementById("addSupplierModal").style.display = "block";
    }

    function closeAddModal() {
        document.getElementById("addSupplierModal").style.display = "none";
    }

    function toggleBlacklistReason(select) {
        const blacklistReasonGroup = document.getElementById("blacklistReasonGroup");
    if (select.value === "true") {
        blacklistReasonGroup.style.display = "block";
        } else {
        blacklistReasonGroup.style.display = "none";
        }
    }

    // Edit Modal Functions
    function openEditModal(id, companyName, isBlacklisted, blacklistReason) {
        document.getElementById("editSupplierModal").style.display = "block";
    document.getElementById("editSupplierId").value = id;
    document.getElementById("editCompanyName").value = companyName;
    document.getElementById("editIsBlacklisted").value = isBlacklisted.toString();
    toggleEditBlacklistReason(document.getElementById("editIsBlacklisted"));
    if (isBlacklisted) {
        document.getElementById("editBlacklistReason").value = blacklistReason;
        }
    }

    function closeEditModal() {
        document.getElementById("editSupplierModal").style.display = "none";
    }

    function toggleEditBlacklistReason(select) {
        const editBlacklistReasonGroup = document.getElementById("editBlacklistReasonGroup");
    if (select.value === "true") {
        editBlacklistReasonGroup.style.display = "block";
        } else {
        editBlacklistReasonGroup.style.display = "none";
        }
    }

    // Close modals when clicking outside
    window.onclick = function (event) {
        if (event.target == document.getElementById("addSupplierModal")) {
        closeAddModal();
        }
    if (event.target == document.getElementById("editSupplierModal")) {
        closeEditModal();
        }
    }
