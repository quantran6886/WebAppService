function InitSwiperPopularArticles() {
    const articles = Array.from(document.querySelectorAll("#original-articles article"));
    const wrapper = document.querySelector("#slider-content .swiper-wrapper");

    if (!articles.length || !wrapper) return;

    function chunkArray(arr, size) {
        const result = [];
        for (let i = 0; i < arr.length; i += size) {
            result.push(arr.slice(i, i + size));
        }
        return result;
    }

    const groups = chunkArray(articles, 4);

    groups.forEach(group => {
        const slide = document.createElement("div");
        slide.className = "swiper-slide";
        group.forEach(article => slide.appendChild(article));
        wrapper.appendChild(slide);
    });

    document.getElementById("original-articles").remove();

    new Swiper("#slider-content", {
        slidesPerView: 1,
        spaceBetween: 10,
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev"
        },
        pagination: {
            el: ".swiper-pagination",
            clickable: true
        }
    });
}