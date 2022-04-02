namespace BTL_Nhom8.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        [Column(TypeName = "text")]
        public string content { get; set; }

        [Column(TypeName = "text")]
        public string img { get; set; }

        [Column(TypeName = "date")]
        public DateTime? create_at { get; set; }

        [Column(TypeName = "date")]
        public DateTime? update_at { get; set; }
    }
}
