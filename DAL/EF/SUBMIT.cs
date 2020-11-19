namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SUBMIT")]
    public partial class SUBMIT
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(200)]
        public string LINK { get; set; }

        public DateTime? TIME { get; set; }

        public int? USER_ID { get; set; }

        public int? EVENT_ID { get; set; }

        public virtual C_USER C_USER { get; set; }

        public virtual ASSESSMENT ASSESSMENT { get; set; }

        public virtual EVENT EVENT { get; set; }
    }
}
