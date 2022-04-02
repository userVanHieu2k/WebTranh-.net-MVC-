using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTL_Nhom8.Areas.Admin.Dto
{
    public class OrderDto
    {
        public int Order_Id { get; set; }
        public string Customer_Name { get; set; }
        [Required(ErrorMessage = "Điện thoại không được để trống")]
        public string Customer_Phone { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string Customer_Address { get; set; }
        public string Order_Date { get; set; }
        public string Customer_Email { get; set; }
        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public bool Status { get; set; }
        public long Total_Amount { get; set; }
    }
}