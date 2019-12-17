using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using HPIT.Data.Core;
using System.Web.Mvc;
using HPIT.Survey.Portal.Filters;


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

        [AllowAnonymous]
        public DeluxeJsonResult FileList(SearchModel<FileModel> search)
        {
            int total = 0;
            string filePath = Server.MapPath("~/上传文件/");
            DirectoryInfo dire = new DirectoryInfo(filePath);
            FileInfo[] allfile = dire.GetFiles();
            var data = from file in allfile
                       select new
                       {
                           filename = file.Name,
                           path = file.DirectoryName,
                           time = file.LastWriteTime,
                           fullname = file.FullName
                       };
            total = allfile.Length;
            var totalPages = total % search.PageSize == 0 ? total / search.PageSize : total / search.PageSize + 1;
            JsonResult json = new JsonResult();
            var pagedata = data.Skip((search.PageIndex - 1) * search.PageSize).Take(search.PageSize);
            //json.Data = new  {  Data = pagedata, Total = total, Totalpages = totalPages };
            //json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return new DeluxeJsonResult(new { Data = pagedata, Total = total, TotalPages = totalPages }, "yyyy-MM-dd HH: mm"); 
        }

        [AllowAnonymous]
        public FileResult Download(string filename)
        {
            string filePath = Server.MapPath("~/上传文件/" + filename);
            return File(filePath, "text/plain", filename);
        }
    }
}