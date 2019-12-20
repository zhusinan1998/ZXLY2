using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZXLY_DAL
{
    public class Book
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="name">密码</param>
        /// <returns></returns>
        public static User_info seluser(int id,string password)
        {
            Model1 mo = new Model1();
            List<User_info> list = mo.User_info.ToList();
            return list.FirstOrDefault(m => m.Id == id && m.Pwd == password);
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int zcinsert(User_info info)
        {
            Model1 model = new Model1();
            model.User_info.Add(info);
            model.SaveChanges();
            User_info user = model.User_info.FirstOrDefault(r => r.Name == info.Name);
            return user.Id;
        }
        public List<Book_information> Select(string Title = "", int Btid = 0)
        {
            Model1 model = new Model1();
            List<Book_information> list = new List<Book_information>();
            if (Title == "" && Btid == 0)
            {
                //list = model.Book_information.ToList();
                list = model.Database.SqlQuery<Book_information>
                  ("select Book_type.Btid,Book_information.Bid,Book_information.Title,Book_information.Warehousing,Book_information.Borrowing_price,Book_information.Unit_Price,Book_information.Stock,Book_information.Bdalate from Book_type,Book_information where Book_type.Btid = Book_information.Btid and Bdalate=1").ToList();
            }
            else
            if (Btid == 0)
            {
                list = model.Database.SqlQuery<Book_information>
                   ("select Book_type.Btid,Book_information.Bid,Book_information.Title,Book_information.Warehousing,Book_information.Borrowing_price,Book_information.Unit_Price,Book_information.Stock,Book_information.Bdalate from Book_type,Book_information where Book_type.Btid = Book_information.Btid and Title like '%" + Title + "%' and Bdalate=1").ToList();
            }
            else
            if (Title == "")
            {
                list = model.Database.SqlQuery<Book_information>
                   ("select Book_type.Btid,Book_information.Bid,Book_information.Title,Book_information.Warehousing,Book_information.Borrowing_price,Book_information.Unit_Price,Book_information.Stock,Book_information.Bdalate from Book_type,Book_information where Book_type.Btid = Book_information.Btid  and Book_type.Btid=" + Btid + " and Bdalate=1").ToList();
            }
            else
            {
                list = model.Database.SqlQuery<Book_information>
                   ("select Book_type.Btid,Book_information.Bid,Book_information.Title,Book_information.Warehousing,Book_information.Borrowing_price,Book_information.Unit_Price,Book_information.Stock,Book_information.Bdalate from Book_type,Book_information where Book_type.Btid = Book_information.Btid and Title like '%" + Title + "%' and Book_type.Btid=" + Btid + " and Bdalate=1").ToList();
            }
            return list;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Bid">书籍编号</param>
        /// <returns></returns>
        public int Delete(int Bid)
        {
            Model1 model = new Model1();
            int list = model.Database.ExecuteSqlCommand
                  ("Update Book_information set Bdalate=2 where Bid=" + Bid + "");
            return list;
        }
        /// <summary>
        /// 添加购买记录
        /// </summary>
        /// <param name="sales">实体类</param>
        /// <returns></returns>
        public static int goumai(Sales_list sales)
        {
            Model1 model = new Model1();
            model.Sales_list.Add(sales);
            return model.SaveChanges();

        }
        /// <summary>
        /// 添加借阅记录
        /// </summary>
        /// <param name="bo">实体类</param>
        /// <returns></returns>
        public static int jieyue(Borrowing_records bo)
        {
            Model1 model = new Model1();
            model.Borrowing_records.Add(bo);
            return model.SaveChanges();
        }
        /// <summary>
        /// 查询借阅记录
        /// </summary>
        /// <param name="id">人员编号</param>
        /// <returns></returns>
        public static List<Borrowing_records> bor(int id)
        {
            Model1 model = new Model1();
            List<Borrowing_records> list = model.Borrowing_records.Where(b=>b.Id==id).ToList();
            return list;
        }
    }
}
