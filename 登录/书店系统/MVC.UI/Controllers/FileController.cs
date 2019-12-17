using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.UI.Controllers
{
    public class FileController : Controller
    {
        [AllowAnonymous]
        // GET: Flit
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult file(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var item in files)
            {
                var filename = Path.GetFileName(item.FileName);
                var path = Path.Combine(Server.MapPath("~/上传文件"), filename);
                item.SaveAs(path);
            }
            return RedirectToAction("Index");
        }
    }
}