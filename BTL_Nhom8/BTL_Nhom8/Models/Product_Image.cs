namespace BTL_Nhom8.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_Image
    {
        [Key]
        public int PI_Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int Product_Id { get; set; }

        public virtual Product Product { get; set; }
    }
}
