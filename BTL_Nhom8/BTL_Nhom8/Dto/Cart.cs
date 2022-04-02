using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_Nhom8.Dto
{
    public class Cart
    {
        public CustomerDto customerDto;
        public List<CartItem> cartLines;

        public Cart() {
            cartLines = new List<CartItem>();
        }

    }
}