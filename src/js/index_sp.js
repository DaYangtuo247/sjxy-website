function isIE(){
  if(!!window.ActiveXObject || "ActiveXObject" in window){
      return true;
  }else{
      return false;
  }
}
function detectBrowser() {
  var agent = navigator.userAgent.toLowerCase();
  var browser;

  if ((agent.indexOf('msie') > -1) || (agent.indexOf('trident') > -1) || (agent.indexOf('edge') > -1)) {
      browser = 'ie'
  } else if (agent.indexOf('firefox') > -1) {
      browser = 'firefox'
  } else if (agent.indexOf('opr') > -1) {
      browser = 'opera'
  } else if (agent.indexOf('chrome') > -1) {
      browser = 'chrome'
  } else if (agent.indexOf('safari') > -1) {
      browser = 'safari'
  }

  return browser;
}



$(function(){

  // var $mainVisual = $(".main-visual-slider");
  // var $mainVisualItem = $mainVisual.find(".main-visual-item");
  // if (detectBrowser() === "ie") {
  //   $(".main-visual-slider .overlay").remove();
  // }

var banner = new Swiper('.slide-banner', {
speed: 1500,
loop: true,
parallax : true,
lazy: {
  loadPrevNext: true,
},
watchSlidesProgress: true,
spaceBetween: 0,
effect : 'fade',fadeEffect: {crossFade: true},
autoplay: {delay: 6000,stopOnLastSlide: false,disableOnInteraction: false},
pagination: {el: ".banner .pgba",clickable: !0, bulletActiveClass: 'active', renderBullet: function (index, className) {
      return '<span class="' + className + '"><i></i></span>';
    }},
navigation: {nextEl: '.banner .next',prevEl: '.banner .prev',},
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
        duration: delay
      });
    },
    slideChangeTransitionStart : function(swiper){
      var swiper = this;
      
      if($(".main-visual-slider .swiper-slide-active video").length>0){
        // 停止自动切换
        swiper.autoplay.stop();
        // 动态增加id
        
        setTimeout(function(){
          swiper.autoplay.stop();
          $(".main-visual-slider .swiper-slide-active video").attr("id","video_01");
          
          var _video=document.getElementById("video_01");
          
          // 播放视频
          _video.play();
          // 切换后重新播放视频
          _video.currentTime = 0;
          // 静音
          _video.volume = 0;
          // 监听视频播放结束
          _video.addEventListener('ended', function () {  
            swiper.slideNext();
            //重新开始轮播banner
            swiper.autoplay.start();
          });
        }, 300);
      }
    },
    slideChangeTransitionEnd: function(swiper){
      //动态移除id
      $(".main-visual-slider .swiper-slide-active video").attr("id","");
    },
}
});
anime({
  targets: ".banner .pgba .active i",
  width: ["0", "100%"],
  easing: "linear",
  duration:8000
});


var slideNews = new Swiper('.slide-news', {
          speed:1000,
         delay: 4000,
          spaceBetween : 20,
          loop:true,
          autoplay:true,
          observer: true,
          observeParents: true, 
          slidesPerView: 1,
          watchSlidesVisibility: true,
        //   navigation: {
        //      nextEl: '.homea .next',
        //      prevEl: '.homea .prev',
        //    },
           pagination: {el: ".homea .pgba",clickable: !0, bulletActiveClass: 'active'},

    });
})