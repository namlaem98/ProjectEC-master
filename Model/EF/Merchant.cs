namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Merchant")]
    public partial class Merchant
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(10)]
        public string CMND { get; set; }

        [StringLength(50)]
        public string ShopAddress { get; set; }
    }
}
