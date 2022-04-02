

$(document).ready(function () {
    $('#sizeProduct').change(function () {
        var gtri = $(this).children("option:selected").val();

        document.getElementById("settingSize").innerHTML = "<a class=\"btn btn-success\" href=\"/Shop/Index?size=" + gtri + "\">Chọn</a>";
    });

    $('.notaddtocart').click(function () {
        var id = $(this).attr("data-id");
        
        var slt = $(this).attr("data-quantity");
        var quantity = $('#qty_' + id).val();
        if (slt < 1) {
            $('#message_' + id).text("Không thể thêm vào giỏ hàng").show();
            $('#qty_' + id).val(1);
            setTimeout(function () {
                $("#message_" + id).hide();
            }, 2000);
        }
        else if (parseInt(slt) < parseInt(quantity)) {
            alert("Bạn chỉ có thể mua " + slt + " sản phẩm này");
        } else if (parseInt(quantity) < 1) {
            $('#qty_' + id).val(1);
        }
        else {
            $('form[name=cart_'+id+']').submit();
        }
        
    });


});