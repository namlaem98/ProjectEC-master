namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("advertisement")]
    public partial class advertisement
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        public DateTime? ActiveDate { get; set; }

        public bool? Status { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        public long? Merchant { get; set; }

        public long? CTR { get; set; }
    }
}
