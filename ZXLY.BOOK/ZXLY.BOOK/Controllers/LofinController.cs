using HPIT.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXLY_DAL;

namespace ZXLY.BOOK.Controllers
{
    public class LofinController : Controller
    {
        [AllowAnonymous]
        // GET: Lofin
        public ActionResult Index()
        {

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult login(int id,string password)
        {
            User_info user = Book.seluser(id, password);
            JsonResult jsonResult = new JsonResult();
            if (user==null)
            {
                return Json(0);
            }
            else
            {
                System.Web.HttpContext.Current.Session["user"] = user;
                jsonResult.Data = new { data = user };
                LogHelper.Default.WriteInfo(user.Name + "登录");
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Indexindert(User_info info)
        {
            Book cla = new Book();
            int zc = cla.zcinsert(info);
            if (zc > 0)
            {
                var shu = new { data = zc };
                return Json(shu);
            }
            else
            {
                return null;
            }
        }
    }
}