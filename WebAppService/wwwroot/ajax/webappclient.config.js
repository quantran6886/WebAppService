    function hideGoogleTranslateBar() {
            const frame = document.querySelector('iframe.goog-te-banner-frame');
            if (frame) {
                frame.style.display = 'none';
                document.body.style.top = '0px';
            }
        }
        window.addEventListener('load', function () {
            setTimeout(hideGoogleTranslateBar, 1000);
        });
        function showLoading() {
            $('#loadingOverlay').fadeIn(200);
        }

        function hideLoading() {
            $('#loadingOverlay').fadeOut(200);
        }

        window.onload = function () {
            const menuIcon = document.getElementById("menuToggle");
            const logoWrapper = document.getElementById("menuMDesk");
            const logoImage = document.getElementById("logoImage");
            const logoImageTablet = document.getElementById("logoImageTablet");
            const headerMenu = document.getElementById("header-menu");
            if (menuIcon && logoWrapper && logoImage && logoImageTablet) {
                menuIcon.addEventListener("click", function () {
                    const isActive = this.classList.toggle("active");
                    logoWrapper.classList.toggle("header-colored");

                    // đổi ảnh logo desktop
                    const currentSrc = logoImage.getAttribute("src");
                    const altSrc = logoImage.getAttribute("data-alt-src");
                    logoImage.setAttribute("src", altSrc);
                    logoImage.setAttribute("data-alt-src", currentSrc);

                    // đổi ảnh logo tablet
                    const currentSrc2 = logoImageTablet.getAttribute("src");
                    const altSrc2 = logoImageTablet.getAttribute("data-alt-src");
                    logoImageTablet.setAttribute("src", altSrc2);
                    logoImageTablet.setAttribute("data-alt-src", currentSrc2);

                    // Nếu là tablet/mobile
                    if (window.innerWidth < 992) {
                        if (isActive) {
                            // Khi mở menu → đổi sang đen
                            headerMenu.classList.remove("background-white");
                            headerMenu.classList.add("background-sunmedical");
                        } else {
                            // Khi đóng menu → đổi lại trắng
                            headerMenu.classList.remove("background-sunmedical");
                            headerMenu.classList.add("background-white");
                        }
                    }
                });

            } else {
            console.warn("Không tìm thấy phần tử:", {
                menuIcon,
                logoWrapper,
                logoImage
            });
            }
        };