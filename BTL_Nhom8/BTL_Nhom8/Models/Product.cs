namespace BTL_Nhom8.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            DetailProduct_Order = new HashSet<DetailProduct_Order>();
            Product_Image = new HashSet<Product_Image>();
        }

        [Key]
        public int Product_Id { get; set; }

        public int Category_Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public long Price { get; set; }

        [Required]
        [StringLength(255)]
        public string Short_Description { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Detail_Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Avatar { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailProduct_Order> DetailProduct_Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_Image> Product_Image { get; set; }
    }
}
