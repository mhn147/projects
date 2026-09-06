// TODO: review
function setupThemeToggle() {
    const toggle = document.getElementById('theme-toggle');
    const sunIcon = document.getElementById('icon-sun');
    const moonIcon = document.getElementById('icon-moon');

    function updateIcon(theme) {
        sunIcon.style.display = theme === 'dark' ? 'none' : 'inline-block';
        moonIcon.style.display = theme === 'dark' ? 'inline-block' : 'none';
    }

    updateIcon(document.documentElement.getAttribute('data-bs-theme'));

    toggle.addEventListener('click', function () {
        const current = document.documentElement.getAttribute('data-bs-theme');
        const next = current === 'dark' ? 'light' : 'dark';
        document.documentElement.setAttribute('data-bs-theme', next);
        localStorage.setItem('theme', next);
        updateIcon(next);
    });

    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', (e) => {
        if (!localStorage.getItem('theme')) {
            const theme = e.matches ? 'dark' : 'light';
            document.documentElement.setAttribute('data-bs-theme', theme);
            updateIcon(theme);
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {
    setupThemeToggle();
});