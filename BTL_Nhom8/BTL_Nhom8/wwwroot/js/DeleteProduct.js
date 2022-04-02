$(document).ready(function () {
    $(".deleteProduct").click(function (e) {
        e.preventDefault();
        var id = $(this).attr("id");
        var MSG = confirm("Bạn có chắc muốn xóa sản phẩm này?");
        if (MSG) {
            $.ajax({
                type: 'POST',
                url: '/Product/DeleteProduct',
                data: { id: id },
                success: function () {
                    setTimeout(function () { location.reload(); }, 1000);
                },
                error: function () {
                    alert("Có lỗi khi xóa!");
                }
            });
        }
    });
});