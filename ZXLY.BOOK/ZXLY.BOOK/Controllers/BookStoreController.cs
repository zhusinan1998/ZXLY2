using HPIT.Data.Core;
using HPIT.Survey.Portal.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXLY_DAL;
using ZXLY_DAL.biao;

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
        public ActionResult UserIndex()
        {

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
            var result = fenye.Instance.GetPageData(search, out total);
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
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Bid">书籍编号</param>
        /// <returns></returns>
        public JsonResult Delete(int Bid)
        {
            Book cla = new Book();
            int sc = cla.Delete(Bid);
            if (sc > 0)
            {
                return Json(sc, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        }
        [HttpPost]
        /// <summary>
        /// 查询购买记录
        /// </summary>
        /// <returns></returns>
        public JsonResult selSales_list()
        {
            int id = Convert.ToInt32(Session["id"]);
            Model1 model = new Model1();
            Model db = new Model();
            var sel = (from bookinf in model.Book_information
                       join sales in model.Sales_list on bookinf.Bid equals sales.Bid
                       join userinfo in model.User_info on sales.Id equals userinfo.Id into userinfo
                       from useri in userinfo
                       select new Model
                       {
                           book = bookinf,
                           sales = sales,
                           user = useri
                       }).ToList().Where(m => m.user.Id == id);
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return new DeluxeJsonResult(new { Data = sel }, "yyyy-MM-dd HH:mm");
        }
        /// <summary>
        /// 查询借阅记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult selBorrowing_records()
        {
            int id = Convert.ToInt32(Session["id"]);
            Model1 model = new Model1();
            Model db = new Model();
            var sel = (from bookinf in model.Book_information
                       join borrowing in model.Borrowing_records on bookinf.Bid equals borrowing.Bid
                       join user in model.User_info on borrowing.Id equals user.Id
                       select new Model
                       {
                           book = bookinf,
                           user = user,
                           borrowing = borrowing
                       }).ToList().Where(m => m.user.Id == id);
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return new DeluxeJsonResult(new { Data = sel }, "yyyy-MM-dd HH:mm");
        }
        /// <summary>
        /// 添加购买记录
        /// </summary>
        /// <param name="Bid">书籍编号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult gouwu(int Bid)
        {
            Book book = new Book();
            int id = Convert.ToInt32(Session["id"]);
            Book_information bookinf =book.Select(Bid);
            if (bookinf.Stock<=0)
            {
                return Json(-1);
            }
            else
            {
                var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                Sales_list sales = new Sales_list()
                {
                    Bid = Bid,
                    Id = id,
                    purchase_time = Convert.ToDateTime(time)
                };
                if (Book.goumai(sales) > 0)
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpPost]
        /// <summary>
        /// 借阅记录
        /// </summary>
        /// <param name="bo">实体类</param>
        /// <returns></returns>
        public JsonResult jieyue(int Bid)
        {
            Book book = new Book();
            int id = Convert.ToInt32(Session["id"]);
            List<Borrowing_records> list = Book.bor(id);
            Book_information bookinf = book.Select(Bid);
            if (bookinf.Stock <= 0)
            {
                return Json(-1);
            }
            else
            {
                var time = DateTime.Now;//.ToString("yyyy-MM-dd");
                var time2 = time.AddDays(31).ToString("yyyy-MM-dd HH:mm");
                Borrowing_records bo = new Borrowing_records()
                {
                    Bid = Bid,
                    Id = id,
                    Lend_time = Convert.ToDateTime(time.ToString("yyyy-MM-dd HH:mm")),
                    Return_time = Convert.ToDateTime(time2)
                };
                if (list.Count > 9)
                {
                    return Json(0);
                }
                else
                {
                    if (Book.jieyue(bo) > 0)
                    {
                        var json = new { data = time2, shu = 1 };
                        return Json(json, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(2);
                    }
                }
            }
        }

        [HttpPost]
        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="Bid"></param>
        /// <returns></returns>
        public JsonResult Selectid(int Bid)
        {
            Book bk = new Book();
            Book_information id = bk.Selectid(Bid);
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            //var json = new {  };
            return new DeluxeJsonResult(new { data = id }, "yyyy-MM-dd HH:mm");
        }
        //[HttpPost]
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public JsonResult Update(Book_information info)
        {
            Book bk = new Book();
            int xg = bk.Update(info);
            if (xg > 0)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public JsonResult Insert(Book_information info)
        {

            int tj = Book.Insert(info);
            if (tj > 0)
            {
                return Json(tj);
            }
            else
            {
                return Json(0);
            }
        }
        /// <summary>
        /// 分页查询书籍信息
        /// </summary>
        /// <param name="searchmodel"></param>
        /// <param name="Btid"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult sel(SearchModel<Book_information> searchmodel,int Btid=0, string Title = "")
        {
            int total = 0;
            var list =Book.selbookinfo(searchmodel,out total,Btid, Title);
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var totalPages = total % searchmodel.PageSize == 0 ? total / searchmodel.PageSize : total / searchmodel.PageSize + 1;
            return new DeluxeJsonResult(new { Data = list, Total = total, TotalPages = totalPages }, "yyyy-MM-dd HH:mm");
        }

        public ActionResult Selectlexing(int Btid = 1)
        {
            Book cla = new Book();
            List<infobook> info = cla.Selectleixing(Btid);
            var result = from p in info
                         select new
                         {
                             name = p.Title,
                             value = p.Stock
                         };
            return Json(result);

        }


    }
}