using HPIT.Data.Core;
using HPIT.Survey.Portal.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXLY_DAL;
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
        [HttpPost]
        public DeluxeJsonResult SeleUser(SearchModel<Book_information> search)
        {
            //List<Book_information> list = Book.selelist();
            ////实例化Json
            //JsonResult jsonResult = new JsonResult();
            ////序列化
            //string json = JsonConvert.SerializeObject(list);
            //jsonResult.Data= new { Data = list };
            ////var json = new { data = list };
            ////string json1 = JsonConvert.SerializeObject(json);
            //jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            //return jsonResult;
            int total = 0;
            var result =fenye.Instance.GetPageData(search, out total);
            var totalPages = total % search.PageSize == 0 ? total / search.PageSize : total / search.PageSize + 1;
            return new DeluxeJsonResult(new { Data = result, Total = total, TotalPages = totalPages }, "yyyy-MM-dd HH: mm");
        }
        /// <summary>
        /// 查询书籍信息
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="Btid"></param>
        /// <returns></returns>
        public ActionResult Select(string Title = "", int Btid = 0)
        {
            Book cla = new Book();
            List<Book_information> info = cla.Select(Title, Btid);
            JsonResult json = new JsonResult();
            json.Data = new { Data = info };
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return new DeluxeJsonResult(new { Data = info }, "yyyy-MM-dd HH: mm");
        }

    }
}