using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZXLY.BOOK.Controllers
{
    public class BookStoreController : Controller
    {
        // GET: BookStore
        public ActionResult Index()
        {
            ViewBag.name = System.Web.HttpContext.Current.Session["user"].ToString();
            return View();
        }
        public ActionResult UserIndex() {
            ViewBag.name = System.Web.HttpContext.Current.Session["user"].ToString();
            return View();
        }
    }
}