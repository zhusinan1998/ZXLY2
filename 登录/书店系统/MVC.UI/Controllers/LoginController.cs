using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.DAL;
using Newtonsoft.Json;
using log4net;
using HPIT.Data.Core;

namespace MVC.UI.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult seluser(int id,string name)
        {
            User_info user = Selectbook.seluser(id,name);
            JsonResult jsonResult = new JsonResult();
            if (user==null)
            {
                return Json(0);
            }else
            {
                string json = JsonConvert.SerializeObject(user);
                System.Web.HttpContext.Current.Session["user"] = json;
                //HttpCookie cookie = new HttpCookie("name", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(json)));
                //Response.Cookies.Add(cookie);
                jsonResult.Data = new { data = user, state = "200" };
                LogHelper.Default.WriteInfo(user.Name+"登录");
                return Json(jsonResult,JsonRequestBehavior.AllowGet);
            }
        }

    }
}