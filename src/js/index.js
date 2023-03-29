// 轮播图组件
	// lock = false;
	// var swiper = new Swiper(".mySwiper", {
	// 	spaceBetween: 30,
	// 	centeredSlides: true,
	// 	speed: 1000,
	// 	allowTouchMove: true, //触摸滑动
	// 	autoplay: true, //是否自动播放轮播图
	// 	pagination:'swiper-pagination',//分页容器的css选择器
	// 	paginationType:'custom',//自定义-分页器样式类型

	// 	pagination: {
	// 		el: ".swiper-pagination",
	// 		clickable: true,
	// 		obeserver:true,
	// 		observeParents:true,
	// 		type:'custom',
	// 	    renderBullet: function (index, pagination) {
	// 			switch(index){
	// 			  case 0:text='1';break;
	// 			  case 1:text='2';break;
	// 			  case 2:text='3';break;
	// 			  case 3:text='4';break;
	// 			  case 4:text='5';break;
	// 			}
	// 			return '<span class="' + pagination + '">' + text + '</span>';
	// 		  },
	// 	},
	// 	navigation: {
	// 		nextEl: ".swiper-button-next",
	// 		prevEl: ".swiper-button-prev",
	// 	},
	// });

//给每个页码绑定跳转事件	
$('.swiper-pagination').on('click','li',function(){
	let index = this.innerHTML;
	mySwiper.slideTo(index-1,500,false);
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

	var swiper = new Swiper(".mySwiper", {
		
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
