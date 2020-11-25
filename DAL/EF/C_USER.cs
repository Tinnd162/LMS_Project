namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("_USER")]
    public partial class C_USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_USER()
        {
            SUBMITs = new HashSet<SUBMIT>();
            SUBJECTs = new HashSet<SUBJECT>();
            SUBJECTs1 = new HashSet<SUBJECT>();
            ROLEs = new HashSet<ROLE>();
        }

        [StringLength(20)]
        public string ID { get; set; }

        [StringLength(20)]
        public string FIRST_NAME { get; set; }

        [StringLength(20)]
        public string LAST_NAME { get; set; }

        [StringLength(20)]
        public string MIDDLE_NAME { get; set; }

        public int? PHONE_NO { get; set; }

        public bool? SEX { get; set; }

        public DateTime? DoB { get; set; }

        [StringLength(50)]
        public string MAIL { get; set; }

        [StringLength(50)]
        public string PASSWORD { get; set; }

        public DateTime? LASTVISITDATE { get; set; }

        [StringLength(20)]
        public string CLASS_ID { get; set; }

        [StringLength(20)]
        public string FACULTY_ID { get; set; }

        public virtual CLASS CLASS { get; set; }

        public virtual FACULTY FACULTY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUBMIT> SUBMITs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUBJECT> SUBJECTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUBJECT> SUBJECTs1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROLE> ROLEs { get; set; }
    }
}
