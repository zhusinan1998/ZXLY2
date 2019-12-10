using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DD.dal;
namespace DD.EF.Controllers
{
    public class USERController : Controller
    {
        // GET: USER
        public ActionResult Index()
        {
            List<Login_Info> loing = User_dal.getlist();
            return View(loing);
        }
        /// <summary>
        /// 添加 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            //两种不同类型list的转换 new SelectListItem()是实例化一个新对象
            ViewBag.Rolename = User_dal.Roleslist().Select(r => new SelectListItem()
            { Text = r.Rolename, Value = r.Roleid.ToString() }
            ).ToList();
            return View();
        }
        public ActionResult CreateModel(Login_Info model) {
            if (User_dal.AddUser(model)>0) {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
        /// <summary>
        /// 传值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Update(int id) {
            Login_Info match = User_dal.details(id);
            //下拉框设置
            ViewBag.Rolename = User_dal.Roleslist().Select(r => new SelectListItem()
            { Text = r.Rolename, Value = r.Roleid.ToString() }
            ).ToList();
            return View(match);
        }

        public ActionResult Details(int id)
        {
            Login_Info match = User_dal.details(id);
            //下拉框设置
            //ViewBag.Rolename = User_dal.Roleslist().Select(r => new SelectListItem()
            //{ Text = r.Rolename, Value = r.Roleid.ToString() }
            //).ToList();
            return View(match);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult UpdateModel(Login_Info model) {
            if (User_dal.Update(model)>0) {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Update");
            }
        }
        /// <summary>
        /// 删除方法一无dal层 无外表连接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public ActionResult Delete(int id) {
        //    user_model model = new user_model();
        //    var user = model.Login_Info.Where(p => p.UserId == id);
        //    model.Login_Info.Remove(user.ToList()[0]);
        //    model.SaveChanges();
        //    return View("Index", model.Login_Info.ToList());
        //}

        public ActionResult Delete(int id) {
            User_dal.deleteinfo(id);
            return RedirectToAction("Index");
        }
    }
}