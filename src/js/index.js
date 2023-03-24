$(function () {
	new Swiper(".swiper", {
		loop: true, // 无限循环属性
		// 分页器
		pagination: {
			el: ".swiper-pagination",
			clickable: true,
		},

		//自动切换
		autoplay: {
			delay: 3000,
			stopOnLastSlide: false,
			disableOnInteraction: true,
		},
	});
});

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
