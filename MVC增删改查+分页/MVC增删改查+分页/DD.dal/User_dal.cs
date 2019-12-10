using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.dal
{
    public class User_dal
    {
        public static List<Login_Info> getlist()
        {
            user_model model = new user_model();
            List<Login_Info> list = model.Login_Info.ToList();
            return list;
        }
        public static List<Login_role> roleget() {
            user_model model = new user_model();
            List<Login_role> list = model.Login_role.ToList();
            return list;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddUser(Login_Info model)
        {
            user_model Umodel = new user_model();
            Umodel.Login_Info.Add(model);
            return Umodel.SaveChanges();
        }
        /// <summary>
        /// 根据id详情
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static Login_Info details(int id)
        {
            user_model Umodel = new user_model();

            return Umodel.Login_Info.FirstOrDefault(p => p.UserId == id);

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int Update(Login_Info model)
        {
            user_model Umodel = new user_model();
            Umodel.Entry(model).State = System.Data.Entity.EntityState.Modified;
            return Umodel.SaveChanges();
        }
        public static int deleteinfo(int id) {
            user_model model = new user_model();
            var book = model.Login_Info.FirstOrDefault(p=>p.UserId==id);
            model.Login_Info.Remove(book);
            return model.SaveChanges();
        }
        /// <summary>
        /// 多表连接删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int delete(int id) {
            user_model model = new user_model();
            Login_role role = model.Login_role.FirstOrDefault(p=>p.Roleid==id);
            if (role!=null) {
                model.Login_Info.RemoveRange(role.Login_Info);
            }
            model.Login_role.Remove(role);
            return model.SaveChanges();
        }
        /// <summary>
        /// 查询角色表
        /// </summary>
        /// <returns></returns>
        public static List<Login_role> Roleslist()
        {
            user_model Umodel = new user_model();
            return Umodel.Login_role.ToList();
        }
       
    }
}
