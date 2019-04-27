namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderAdvertisement")]
    public partial class OrderAdvertisement
    {
        public long ID { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public int? Status { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
