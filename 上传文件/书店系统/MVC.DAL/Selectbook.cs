using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;

namespace MVC.DAL
{
    public class Selectbook
    {
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
        public static List<User_info> seluser()
        {
            Bookstore book = new Bookstore();
            return book.User_info.ToList();
        }
        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        public static User_info seluser(int id,string password)
        {
            return seluser().FirstOrDefault(m => m.Id == id && m.Pwd == password);
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int insertuser(User_info user)
        {
            Bookstore book = new Bookstore();
            book.User_info.Add(user);
            return book.SaveChanges();
        }
        /// <summary>
        /// 查询书籍信息
        /// </summary>
        /// <param name="id">书籍编号</param>
        /// <param name="bookname">书籍名称</param>
        /// <returns></returns>
        public static List<Book_information> selbook(int id=0,string bookname="")
        {
            Bookstore book = new Bookstore();
            if (id==0 && bookname=="")
            {
                List<Book_information> bookinf = book.Database.SqlQuery<Book_information>("select * from Book_information").ToList();
                return bookinf;
            }else if(id!=0 && bookname!="")
            {
                List<Book_information> bookinf = book.Database.SqlQuery<Book_information>("select * from Book_information where Title like '%'+@Title+'%' and Bid=@id", new SqlParameter("@Title", bookname),new SqlParameter("@id",id)).ToList();
                return bookinf;
            }else if(id!=0&& bookname=="")
            {
                List<Book_information> bookinf = book.Database.SqlQuery<Book_information>("select * from Book_information where Bid=@id", new SqlParameter("@id", id)).ToList();
                return bookinf;
            }else
            {
                List<Book_information> bookinf = book.Database.SqlQuery<Book_information>("select * from Book_information where Title like '%'+@Title+'%'", new SqlParameter("@Title", bookname)).ToList();
                return bookinf;
            }
        }
    }
}
