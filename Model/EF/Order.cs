namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public long ID { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalPrice { get; set; }

        public int? Status { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
