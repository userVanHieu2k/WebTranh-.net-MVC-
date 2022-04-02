$(document).ready(function () {
    $(".deleteAccount").click(function (e) {
        e.preventDefault();
        var id = $(this).attr("id");
        var MSG = confirm("Bạn có chắc muốn xóa tài khoản này?");
        if (MSG) {
            $.ajax({
                type: 'POST',
                url: '/Account/DeleteAccount',
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

    $(".editAccount").click(function (e) {
        e.preventDefault();
        var id = $(this).attr("id");
        var MSG = confirm("Bạn có chắc muốn sửa trạng thái này?");
        if (MSG) {
            $.ajax({
                type: 'POST',
                url: '/Account/EditStatus',
                data: { id: id },
                success: function () {
                    setTimeout(function () { location.reload(); }, 1000);
                },
                error: function () {
                    alert("Có lỗi khi sửa!");
                }
            });
        }
    });
});
