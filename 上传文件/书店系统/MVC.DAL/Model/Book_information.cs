namespace MVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Book_information
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book_information()
        {
            Sales_list = new HashSet<Sales_list>();
        }

        [Key]
        [DisplayName("书籍编号")]
        public int Bid { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("书籍名称")]
        public string Title { get; set; }
        [DisplayName("书籍类型")]
        public int? Btid { get; set; }

        public DateTime Warehousing { get; set; }
        [DisplayName("上架时间")]
        public string Warehousingstring
        {
            get
            {
                return Warehousing.ToString("yyyy-MM-dd");
            }
        }
        [DisplayName("借阅价格")]
        public double Borrowing_price { get; set; }
        [DisplayName("单价")]
        public double Unit_Price { get; set; }
        [DisplayName("库存")]
        public int Stock { get; set; }

        //public virtual Book_type Book_type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sales_list> Sales_list { get; set; }
    }
}
