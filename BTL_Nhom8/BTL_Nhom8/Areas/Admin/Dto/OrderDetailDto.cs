using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_Nhom8.Areas.Admin.Dto
{
    public class OrderDetailDto
    {
        public string Product_Name { get; set; }
        public long Product_Price { get; set; }
        public int Product_Quantity { get; set; }

    }
}