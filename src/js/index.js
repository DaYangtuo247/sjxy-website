// 大轮播图组件
$(document).ready(function () {
    var swiper = new Swiper(".banner-swiper", {
        loop: true,
        speed: 1200,
        centeredSlides: true,
        allowTouchMove: true,
        watchSlidesProgress: true,
        autoplay: {
            delay: 4000,
            disableOnInteraction: false,
        },
        navigation: {
            prevEl: ".swiper-button-prev",
            nextEl: ".swiper-button-next",
        },
        pagination: {
            el: ".swiper-pagination",
            clickable: true,
            bulletActiveClass: "active",
            renderBullet: function (index, className) {
                return '<span class="' + className + '">' + "<i></i></span>";
            },
        },
        //进度条配置
        on: {
            init: function () {
                this.emit("transitionStart");
            },
            transitionStart: function () {
                delay = this.params.autoplay.delay;
                anime({
                    targets: ".banner-swiper .swiper-pagination .active i",
                    delay: 0,
                    width: ["0", "100%"],
                    easing: "linear",
                    duration: delay,
                });
            },
            slideChangeTransitionStart: function () {
                var mainVisualImage = this.slides[this.activeIndex].querySelector("img,video");
                anime({
                    targets: mainVisualImage,
                    scale: [1.1, 1],
                    rotate: "0.002deg",
                    duration: 2000,
                    easing: "easeInOutSine",
                });
            },
        },
    });
    //小轮播图组件
    var swiper2 = new Swiper(".news-swiper", {
        loop: true,
        speed: 800,
        autoplay: true,
        navigation: {
            prevEl: ".swiper-button-prev",
            nextEl: ".swiper-button-next",
            hideOnClick: true,
        },
        pagination: {
            el: ".swiper-pagination",
            clickable: true,
        },
    });
});

// 显示/隐藏搜索框并提交
$(document).ready(function () {
    $("#searchBut").on("mousedown", function () {
        var content = $("#searchText").val();
        if (content && content.trim()) {
            $("#searchForm").submit();
        } else {
            var searchText = $("#searchText");
            if (searchText.css("opacity") == 0) {
                searchText.css({ opacity: 1, width: "200px" });
            } else {
                searchText.css({ opacity: 0, width: "0px" });
            }
        }
    });
});

//元素淡入淡出效果
$(window).scroll(function () {
    var scroll = $(window).scrollTop();
    var head_hight = $(".show").outerHeight();
    if (scroll >= head_hight) {
        $(".header").addClass("on");
    } else {
        $(".header").removeClass("on");
    }
    var scrollTop = $(this).scrollTop();
    var homeaTop = $(".homea").outerHeight();
    var homebTop = $(".homeb").outerHeight();
    var homecTop = $(".homec").outerHeight();
    if (scrollTop > (homeaTop + homecTop) / 2) {
        $(".fadeInUpa").addClass("animate__animated animate__fadeInUp");
        $(".fadeInUpRighta").addClass("animate__animated animate__fadeInLeft");
        $(".fadeInUpLefta").each(function (i) {
            $(this)
                .delay(i * 100)
                .queue(function () {
                    $(this).addClass("animate__animated animate__fadeInRight");
                });
        });
    }
    if (scrollTop > (homeaTop + homebTop + homecTop) / 1.4) {
        $(".fadeInUpb").addClass("animate__animated animate__fadeInUp");
        $(".fadeInUpbb").each(function (i) {
            $(this)
                .delay(i * 100)
                .queue(function () {
                    $(this).addClass("animate__animated animate__fadeInUp");
                });
        });
        $(".fadeInUpbbb").each(function (i) {
            $(this)
                .delay(i * 100)
                .queue(function () {
                    $(this).addClass("animate__animated animate__fadeInUp");
                });
        });
    }
    if (scrollTop > homecTop / 4) {
        $(".fadeInUpc").each(function (i) {
            $(this)
                .delay(i * 100)
                .queue(function () {
                    $(this).addClass("animate__animated animate__fadeInUp");
                });
        });
    }
});

// 通知公告添加new标签
$(document).ready(function () {
    var inform = $(".flex .fadeInUpc").toArray();
    $.getJSON(
        "https://worldtimeapi.org/api/timezone/Asia/Shanghai", // 世界时间api接口
        function (data) {
            var datetime = data.datetime;
            var ChinaDate = new Date(String(datetime.slice(0, 10))); // 提取年月日
            console.log("ChinaDate:", ChinaDate);
            for (var i = 0; i < inform.length; i++) {
                var timeTag = $(inform[i]).find(".time");
                var nowdate = timeTag.find("em").text() + "-" + timeTag.find("span").text();
                nowdate = new Date(String(nowdate));
                console.log("nowdate:", nowdate);

                var diffInDays = Math.round((ChinaDate - nowdate) / (1000 * 60 * 60 * 24));
                console.log("diffInDays:", diffInDays);

                if (diffInDays >= 0 && diffInDays <= 3) {
                    $(inform[i]).find("a").append("<div class='new'><img src='./img/New.png'></div>");
                }
            }
        }
    );
});
