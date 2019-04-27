namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        public long ID { get; set; }

        public long? IDProduct { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        [StringLength(50)]
        public string ShopName { get; set; }

        public long? IDMerchant { get; set; }
    }
}
