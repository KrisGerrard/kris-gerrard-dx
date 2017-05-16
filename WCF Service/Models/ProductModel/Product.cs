namespace WCF_Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int ProductID { get; set; }

        public int RetailerID { get; set; }

        public int SubCategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(255)]
        public string ProductDescription { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int UnitsInStock { get; set; }

        public byte[] Picture { get; set; }

        public bool? IsDiscontinued { get; set; }

        public virtual ProductSubCategory ProductSubCategory { get; set; }
    }
}
