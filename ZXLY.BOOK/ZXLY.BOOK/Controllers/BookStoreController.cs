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
            return new DeluxeJsonResult(new { Data = sel }, "yyyy-MM-dd");
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
            return new DeluxeJsonResult(new { Data = sel }, "yyyy-MM-dd");
        }
        /// <summary>
        /// 添加销售记录
        /// </summary>
        /// <param name="Bid">书籍编号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult gouwu(int Bid)
        {
            int id = Convert.ToInt32(Session["id"]);
            var time = DateTime.Now.ToString("yyyy-MM-dd");
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
        [HttpPost]
        /// <summary>
        /// 借阅记录
        /// </summary>
        /// <param name="bo">实体类</param>
        /// <returns></returns>
        public JsonResult jieyue(int Bid)
        {
            int id = Convert.ToInt32(Session["id"]);
            List<Borrowing_records> list = Book.bor(id);

            var time = DateTime.Now;//.ToString("yyyy-MM-dd");
            var time2 = time.AddDays(31).ToString("yyyy-MM-dd");
            Borrowing_records bo = new Borrowing_records()
            {
                Bid = Bid,
                Id = id,
                Lend_time = Convert.ToDateTime(time.ToString("yyyy-MM-dd")),
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
                    var json = new { data = time2,shu=1 };
                    return Json(json, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(2);
                }
            }
        }
    }
}