using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BTL_Nhom8.Dto
{
    public class AccountDto
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email không được để trống !")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống !")]
        [StringLength(50)]
        public string Password { get; set; }
    }
}