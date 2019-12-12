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
            if (System.Web.HttpContext.Current.Session["user"] == null)
            {
                return RedirectToAction("Index", "Lofin");
            }
            else
            {
                return View();
            }

        }
    }
}