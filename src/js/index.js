// 轮播图组件
window.addEventListener("load", function () {
	lock = false;
	bgColor = [
		"rgb(179, 189, 196)",
		"rgb(180, 183, 166)",
		"rgb(140, 152, 187)",
	]; //背景色
	var mySwiper = new Swiper(".swiper-container", {
		speed: 200,
		allowTouchMove: true, //禁止触摸滑动
		parallax: true, //文字位移视差
		autoplay: true, //是否自动播放轮播图
		// 如果需要分页器
		pagination: {
			el: ".swiper-pagination",
			clickable: true,
		},
		on: {
			transitionStart: function () {
				lock = true; //锁住按钮
				slides = this.slides;
				imgBox = slides.eq(this.previousIndex).find(".img-box"); //图片包装器
				imgPrev = slides.eq(this.previousIndex).find("img"); //当前图片
				imgActive = slides.eq(this.activeIndex).find("img"); //下一张图片
				derection = this.activeIndex - this.previousIndex;
				this.$el.css("background-color", bgColor[this.activeIndex]); //背景颜色动画

				imgBox.transform("matrix(1.0, 0, 0, 1.0, 0, 0)");
				imgPrev
					.transition(1000)
					.transform("matrix(1.1, 0, 0, 1.1, 0, 0)"); //图片缩放视差
				this.slides
					.eq(this.previousIndex)
					.find("h3")
					.transition(1000)
					.css("color", "rgba(255,255,255,0)"); //文字透明动画

				imgPrev.transitionEnd(function () {
					imgActive
						.transition(1300)
						.transform(
							"translate3d(0, 0, 0) matrix(1.2, 0, 0, 1.2, 0, 0)"
						); //图片位移视差
					imgPrev
						.transition(1300)
						.transform(
							"translate3d(" +
								60 * derection +
								"%, 0, 0)  matrix(1.2, 0, 0, 1.2, 0, 0)"
						);
				});
			},
			transitionEnd: function () {
				this.slides
					.eq(this.activeIndex)
					.find(".img-box")
					.transform(" matrix(1, 0, 0, 1, 0, 0)");
				imgActive = this.slides.eq(this.activeIndex).find("img");
				imgActive
					.transition(1000)
					.transform(" matrix(1, 0, 0, 1, 0, 0)");
				this.slides
					.eq(this.activeIndex)
					.find("h3")
					.transition(1000)
					.css("color", "rgba(255,255,255,1)");

				imgActive.transitionEnd(function () {
					lock = false;
				});
				//第一个和最后一个，禁止按钮
				if (this.activeIndex == 0) {
					this.$el.find(".button-prev").addClass("disabled");
				} else {
					this.$el.find(".button-prev").removeClass("disabled");
				}

				if (this.activeIndex == this.slides.length - 1) {
					this.$el.find(".button-next").addClass("disabled");
				} else {
					this.$el.find(".button-next").removeClass("disabled");
				}
			},
			init: function () {
				this.emit("transitionEnd"); //在初始化时触发一次transitionEnd事件
			},
		},
	});

	//不使用自带的按钮组件，使用lock控制按钮锁定时间
	mySwiper.$el.find(".button-next").on("click", function () {
		if (!lock) {
			mySwiper.slideNext();
		}
	});
	mySwiper.$el.find(".button-prev").on("click", function () {
		if (!lock) {
			mySwiper.slidePrev();
		}
	});
});

// 显示/隐藏搜索框并提交
function showSearch() {
	var content = $("#searchText").val();
	if (content && content.trim()) {
		$("#searchBut").click(function () {
			$("#searchForm").submit();
		});
	} else {
		var searchText = $("#searchText");
		if (searchText.css("opacity") == 0) {
			searchText.css({ opacity: 1, width: "200px" });
		} else {
			searchText.css({ opacity: 0, width: "0px" });
		}
	}
}

function isIE() {
	if (!!window.ActiveXObject || "ActiveXObject" in window) {
		return true;
	} else {
		return false;
	}
}
function detectBrowser() {
	var agent = navigator.userAgent.toLowerCase();
	var browser;

	if (
		agent.indexOf("msie") > -1 ||
		agent.indexOf("trident") > -1 ||
		agent.indexOf("edge") > -1
	) {
		browser = "ie";
	} else if (agent.indexOf("firefox") > -1) {
		browser = "firefox";
	} else if (agent.indexOf("opr") > -1) {
		browser = "opera";
	} else if (agent.indexOf("chrome") > -1) {
		browser = "chrome";
	} else if (agent.indexOf("safari") > -1) {
		browser = "safari";
	}

	return browser;
}

$(function () {
	// var $mainVisual = $(".main-visual-slider");
	// var $mainVisualItem = $mainVisual.find(".main-visual-item");
	// if (detectBrowser() === "ie") {
	//   $(".main-visual-slider .overlay").remove();
	// }

	var banner = new Swiper(".slide-banner", {
		speed: 1500,
		loop: true,
		parallax: true,
		lazy: {
			loadPrevNext: true,
		},
		watchSlidesProgress: true,
		spaceBetween: 0,
		effect: "fade",
		fadeEffect: { crossFade: true },
		autoplay: {
			delay: 6000,
			stopOnLastSlide: false,
			disableOnInteraction: false,
		},
		pagination: {
			el: ".banner .pgba",
			clickable: !0,
			bulletActiveClass: "active",
			renderBullet: function (index, className) {
				return '<span class="' + className + '"><i></i></span>';
			},
		},
		navigation: { nextEl: ".banner .next", prevEl: ".banner .prev" },
		on: {
			transitionStart: function () {
				// if (this.realIndex == 3) {delay = 20000;} else {
				delay = this.params.autoplay.delay;
				//}
				anime({
					targets: ".banner .pgba .active i",
					delay: 0,
					width: ["0", "100%"],
					easing: "linear",
					duration: delay,
				});
			},
			slideChangeTransitionStart: function (swiper) {
				var swiper = this;

				if (
					$(".main-visual-slider .swiper-slide-active video").length >
					0
				) {
					// 停止自动切换
					swiper.autoplay.stop();
					// 动态增加id

					setTimeout(function () {
						swiper.autoplay.stop();
						$(
							".main-visual-slider .swiper-slide-active video"
						).attr("id", "video_01");

						var _video = document.getElementById("video_01");

						// 播放视频
						_video.play();
						// 切换后重新播放视频
						_video.currentTime = 0;
						// 静音
						_video.volume = 0;
						// 监听视频播放结束
						_video.addEventListener("ended", function () {
							swiper.slideNext();
							//重新开始轮播banner
							swiper.autoplay.start();
						});
					}, 300);
				}
			},
			slideChangeTransitionEnd: function (swiper) {
				//动态移除id
				$(".main-visual-slider .swiper-slide-active video").attr(
					"id",
					""
				);
			},
		},
	});
	anime({
		targets: ".banner .pgba .active i",
		width: ["0", "100%"],
		easing: "linear",
		duration: 8000,
	});

	var slideNews = new Swiper(".slide-news", {
		speed: 1000,
		delay: 4000,
		spaceBetween: 20,
		loop: true,
		autoplay: true,
		observer: true,
		observeParents: true,
		slidesPerView: 1,
		watchSlidesVisibility: true,
		//   navigation: {
		//      nextEl: '.homea .next',
		//      prevEl: '.homea .prev',
		//    },
		pagination: {
			el: ".homea .pgba",
			clickable: !0,
			bulletActiveClass: "active",
		},
	});
});
