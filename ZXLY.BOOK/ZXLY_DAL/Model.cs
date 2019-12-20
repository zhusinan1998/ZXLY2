using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZXLY_DAL
{
    public class Model
    {
        public Book_information book {get;set;}
        public User_info user { get; set; }
        public Sales_list sales { get; set; }
        public Borrowing_records borrowing { get; set; }
    }
}
