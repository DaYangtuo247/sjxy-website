// 大轮播图组件
$(document).ready(function() {
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
            renderBullet: function(index, className) {
                return '<span class="' + className + '">' + "<i></i></span>";
            },
        },
        //进度条配置
        on: {
            init: function() {
                this.emit("transitionStart");
            },
            transitionStart: function() {
                delay = this.params.autoplay.delay;
                anime({
                    targets: ".banner-swiper .swiper-pagination .active i",
                    delay: 0,
                    width: ["0", "100%"],
                    easing: "linear",
                    duration: delay,
                });
            },
            slideChangeTransitionStart: function() {
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
        pagination: {
            el: ".min-swiper-pagination",
            clickable: true,
        },
    });
});



//元素淡入淡出效果
$(window).scroll(function() {
    var scrollTop = $(this).scrollTop();
    var homeaTop = $(".homea").outerHeight();
    var homebTop = $(".homeb").outerHeight();
    var homecTop = $(".homec").outerHeight();
    if (scrollTop > (homeaTop + homecTop) / 2) {
        $(".fadeInUpa").addClass("animate__animated animate__fadeInUp");
        $(".fadeInUpRighta").addClass("animate__animated animate__fadeInLeft");
        $(".fadeInUpLefta").each(function(i) {
            $(this)
                .delay(i * 100)
                .queue(function() {
                    $(this).addClass("animate__animated animate__fadeInRight");
                });
        });
    }
    if (scrollTop > (homeaTop + homebTop + homecTop) / 1.4) {
        $(".fadeInUpb").addClass("animate__animated animate__fadeInUp");
        $(".fadeInUpbb").each(function(i) {
            $(this)
                .delay(i * 100)
                .queue(function() {
                    $(this).addClass("animate__animated animate__fadeInUp");
                });
        });
        $(".fadeInUpbbb").each(function(i) {
            $(this)
                .delay(i * 100)
                .queue(function() {
                    $(this).addClass("animate__animated animate__fadeInUp");
                });
        });
    }
    if (scrollTop > homecTop / 4) {
        $(".fadeInUpc").each(function(i) {
            $(this)
                .delay(i * 100)
                .queue(function() {
                    $(this).addClass("animate__animated animate__fadeInUp");
                });
        });
    }
});

// 通知公告添加new标签
$(document).ready(function() {
    var inform = $(".flex .fadeInUpc").toArray();
    $.getJSON(
        "https://worldtimeapi.org/api/timezone/Asia/Shanghai", // 世界时间api接口
        function(data) {
            var datetime = data.datetime;
            var ChinaDate = new Date(String(datetime.slice(0, 10))); // 提取年月日
            for (var i = 0; i < inform.length; i++) {
                var timeTag = $(inform[i]).find(".time");
                var nowdate = timeTag.find("em").text() + "-" + timeTag.find("span").text();
                nowdate = new Date(String(nowdate));

                var diffInDays = Math.round((ChinaDate - nowdate) / (1000 * 60 * 60 * 24));

                if (diffInDays >= 0 && diffInDays <= 3) {
                    $(inform[i]).find("a").append("<div class='new'><img src='./img/New.png'></div>");
                }
            }
        }
    );
});

var Builders = [
    { 姓名: "吴奇灵", 角色: "全栈", 年级专业: "20级信计", 联系邮箱: "2473605320@qq.com" },
    { 姓名: "王仲旸", 角色: "前端", 年级专业: "20级信计", 联系邮箱: "448319283@qq.com" },
    { 姓名: "程 婧", 角色: "前端", 年级专业: "20级计科", 联系邮箱: "3114729830@qq.com" },
    { 姓名: "黄志旺", 角色: "后端", 年级专业: "20级大数据", 联系邮箱: "2944535274@qq.com" },
];
console.log("version: 0.1, 最后维护日期：2023-xx-xx");
console.table(Builders);