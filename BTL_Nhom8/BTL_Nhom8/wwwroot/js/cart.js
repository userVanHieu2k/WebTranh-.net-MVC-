
$(document).ready(function () {
    $(".qty-minus").click(function () {

        var id = $(this).attr("data-id");
        var quantity = parseInt($('#' + id).val()) - 1;
        if (quantity < 1) {
            var MSG = confirm("Bạn có chắc muốn xóa sản phẩm này?");
            if (MSG) {
                delteItemCart(id);
            } else {
                quantity = 1;
                document.getElementById(id).value = 1;
            }

        }

        else document.getElementById(id).value = quantity;
        tinhtien(id, quantity);
    });

    $(".qty-plus").click(function () {

        var id = $(this).attr("data-id");

        var quantity = parseInt($('#' + id).val()) + 1;
        document.getElementById(id).value = quantity;
        tinhtien(id, quantity);
    });
    function tinhtien(id, quantity) {
        $.ajax({
            type: 'GET',
            data: { id: id, quantity: quantity },
            url: '/Cart/EditItem',
            success: function (ketqua) {
                if (ketqua.status == true) {
                    document.getElementById("tongtien_" + id).innerHTML = ketqua.sl;
                }
            }
        });
    }

    function delteItemCart(id) {
        $.ajax({
            type: 'GET',
            data: { id: id },
            url: '/Cart/DeleteItem',
            success: function (ketqua) {
                if (ketqua.status == true) {
                    $('#row_' + id).remove();
                    $('#giohang').text("(" + ketqua.cartline + ")");
                }
            }
        });
    }


    $(".action").click(function () {
        var id = $(this).attr("data-id");
        delteItemCart(id);
    })

    $("#DeleteAllCartItem").click(function () {
        var MSG = confirm("Bạn có chắc muốn xóa tất cả sản phẩm trong giỏ hàng?");
        if (MSG) {
            $.ajax({
                type: 'GET',
                url: '/Cart/DeleteAllItem',
                success: function (ketqua) {
                    if (ketqua.status == true) {
                        $('.rowCartLine').remove();
                        $('#giohang').text("(0)");
                    }
                }
            });
        }

    });

    $("#submitCartItem").click(function () {
        var listProduct = $('.qty-text');
        var cartItem = [];
        $.each(listProduct, function (e, item) {
            cartItem.push({
                quantity: $(item).val(),
                productDto: {
                    Id: $(item).data('id')
                }
            });
        });
        var carItemModel = JSON.stringify(cartItem);
        $('#checkCartItem input').val(carItemModel);
        $('#checkCartItem').submit();
    });
});