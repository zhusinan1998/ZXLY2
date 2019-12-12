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
    }
}
