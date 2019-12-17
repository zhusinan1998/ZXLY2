using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HPIT.Survey.Portal.Filters;
using MVC.DAL;

namespace MVC.UI.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult sel(int id = 0, string name = "")
        {
            List<Book_information> list = Selectbook.selbook(id,name);
            JsonResult json = new JsonResult();
            json.Data = new { Data = list };
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return new DeluxeJsonResult(new { Data = list }, "yyyy-MM-dd HH: mm");
        }
    }
}