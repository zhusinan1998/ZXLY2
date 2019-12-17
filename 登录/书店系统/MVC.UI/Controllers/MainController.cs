using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.UI.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            //if (System.Web.HttpContext.Current.Session["user"] == null)
            //{
            //    return RedirectToAction("Login","Login");

            //}else
            //{
                return View();
            ////}
            
        }
    }
}