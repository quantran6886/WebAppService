    const toggle = document.getElementById('toggleMenu');
    const nav = document.getElementById('mobileNav');

    toggle.addEventListener('click', () => {
        nav.classList.toggle('active');
    });
