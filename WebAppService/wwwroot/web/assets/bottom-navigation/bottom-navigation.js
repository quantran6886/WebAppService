﻿//const items = document.querySelectorAll(".bottom-nav-menu li a");
//const indicator = document.getElementById("bottom-nav-indicator");
//let gap =
//    items[1].getBoundingClientRect().left -
//    items[0].getBoundingClientRect().right;
//indicator.setAttribute("style", `--gap: ${gap}px;`);

//items.forEach((item, index) => {
//    const indicatorStyle = indicator.getAttribute("style").trim();
//    function menuActive(e) {
//        items.forEach((item) => {
//            //item.classList.remove("active");
//        });
//        //this.classList.add("active");
//        indicator.setAttribute("style", `--item: ${index}; --gap: ${gap}px;`);
//    }
//    item.addEventListener("click", menuActive);
//});

//window.onresize = () => {
//    const indicatorStyle = indicator.getAttribute("style").trim();
//    const indicatorStyleNoGap = indicatorStyle.replace(`--gap: ${gap}px;`, "");

//    gap =
//        items[1].getBoundingClientRect().left -
//        items[0].getBoundingClientRect().right;
//    indicator.setAttribute("style", `--gap: ${gap}px; ${indicatorStyleNoGap}`);
//};
