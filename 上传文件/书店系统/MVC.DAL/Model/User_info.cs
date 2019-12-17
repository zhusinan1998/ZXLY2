namespace MVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User_info()
        {
            Borrowing_records = new HashSet<Borrowing_records>();
            Sales_list = new HashSet<Sales_list>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Pwd { get; set; }

        public int sex { get; set; }

        public int Tel { get; set; }

        [Required]
        [StringLength(20)]
        public string Email { get; set; }

        [Required]
        [StringLength(40)]
        public string shenfen { get; set; }

        [Required]
        [StringLength(50)]
        public string addre { get; set; }

        public int Roles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Borrowing_records> Borrowing_records { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sales_list> Sales_list { get; set; }
    }
}
