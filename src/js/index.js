// 轮播图组件
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
		pagination: {
			el: ".swiper-pagination",
			clickable: true,
			bulletActiveClass: "active",
			renderBullet: function (index, className) {
				return '<span class="' + className + '">' + "<i></i></span>";
			},
		},
		// 进度条配置
		on: {
			// 手动触发transitionStart事件以显示进度条
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
				var mainVisualImage =
					this.slides[this.activeIndex].querySelector("img,video");
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
