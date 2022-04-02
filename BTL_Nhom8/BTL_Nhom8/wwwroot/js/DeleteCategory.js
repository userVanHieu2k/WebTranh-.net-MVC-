$(document).ready(function () {
    $(".deleCate").click(function (e) {
        e.preventDefault();
        var id = $(this).attr("id");
        var MSG = confirm("Bạn có chắc muốn xóa thể loại này?");
        if (MSG) {
            $.ajax({
                type: 'POST',
                url: '/Category/DeleteCategory',
                data: { id: id },
                success: function (result) {
                    if (result == true) {
                        setTimeout(function () { location.reload(); }, 1000);
                    } else {
                        alert("Thể loại này còn chứa sản phẩm không thể xóa!");
                    }

                },
                error: function () {
                    alert("Có lỗi khi xóa!");
                }
            });
        }
    });
});