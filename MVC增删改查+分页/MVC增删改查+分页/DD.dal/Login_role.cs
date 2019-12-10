namespace DD.dal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Login_role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Login_role()
        {
            Login_Info = new HashSet<Login_Info>();
        }

        [Key]
        public int Roleid { get; set; }

        [Required]
        [StringLength(20)]
        public string Rolename { get; set; }

        [Required]
        [StringLength(200)]
        public string Roleaction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Login_Info> Login_Info { get; set; }
    }
}
