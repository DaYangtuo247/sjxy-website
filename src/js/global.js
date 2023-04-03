// 显示/隐藏搜索框并提交
$(window).scroll(function() {
    var scroll = $(window).scrollTop();
    var head_hight = $(".show").outerHeight();
    if (scroll >= head_hight) {
        $(".header").addClass("on");
    } else {
        $(".header").removeClass("on");
    }
});

$(document).ready(function() {
    $("#searchBut").on("mousedown", function() {
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