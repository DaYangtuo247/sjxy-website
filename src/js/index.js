$(function () {
    new Swiper('.swiper', {
        loop: true, // 无限循环属性
        // 分页器
        pagination: {
            el: '.swiper-pagination',
            clickable: true
        },

        //自动切换
        autoplay: {
            delay: 3000,
            stopOnLastSlide: false,
            disableOnInteraction: true,
        },
    })
})
function showSearch() {
    let searchBut = document.getElementById('searchBut'),
        searchText = document.getElementById('searchText');
    if (searchText.style.opacity == 0) {
        searchText.style.opacity = 1;
        searchText.style.width = '240px';
    } else {
        searchText.style.opacity = 0;
        searchText.style.width = '0px';
    }
    $("#searchBut").click(function () {
        $("#searchForm").submit();
    });
}