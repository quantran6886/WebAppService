function createSlider(selector) {
    const slides = document.querySelectorAll(selector);
    let current = 0;

    function showSlide(index) {
        slides.forEach((slide) => {
            slide.classList.remove('active');
            const caption = slide.querySelector('.caption');
            if (caption) caption.style.opacity = 0;
        });

        const currentSlide = slides[index];
        currentSlide.classList.add('active');

        const caption = currentSlide.querySelector('.caption');
        if (caption && caption.classList.length > 1) {
            const animClass = caption.classList[1];
            caption.classList.remove(animClass);
            void caption.offsetWidth; // force reflow
            caption.classList.add(animClass);
            caption.style.opacity = 1;
        }
    }

    function nextSlide() {
        current = (current + 1) % slides.length;
        showSlide(current);
    }

    if (slides.length > 0) {
        setInterval(nextSlide, 6000);
    }
}

if (window.matchMedia("(min-width: 768px) and (max-width: 1023px)").matches) {
    createSlider('.is-tablet .slide');
} else if (window.matchMedia("(min-width: 1024px)").matches) {
    createSlider('.is-web .slide');
} else {
    createSlider('.is-mobile .slide');
}