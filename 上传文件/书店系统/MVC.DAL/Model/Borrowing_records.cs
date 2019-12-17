namespace MVC.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Borrowing_records
    {
        [Key]
        public int Brid { get; set; }

        public int Id { get; set; }

        public DateTime Lend_time { get; set; }

        public DateTime Return_time { get; set; }
        public string Return_timestring()
        {
            get{
                return Return_time.ToString("yyyy-MM-dd");
            };
        }

        public virtual User_info User_info { get; set; }
    }
}
