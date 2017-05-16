namespace WCF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Invoiced")]
    public partial class Invoiced
    {
        [Key]
        public int InvoiceID { get; set; }

        public int OrderID { get; set; }

        public int EmployeeID { get; set; }

        public int ShippingOption { get; set; }

        public DateTime InvoiceDate { get; set; }

        public virtual Order Order { get; set; }
    }
}
