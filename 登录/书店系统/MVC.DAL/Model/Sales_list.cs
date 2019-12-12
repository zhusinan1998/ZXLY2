namespace MVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sales_list
    {
        [Key]
        public int Slid { get; set; }

        public int Bid { get; set; }

        public int Id { get; set; }

        public DateTime? purchase_time { get; set; }

        //public virtual Book_information Book_information { get; set; }

        //public virtual User_info User_info { get; set; }
    }
}
