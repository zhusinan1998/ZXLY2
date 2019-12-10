using HPIT.Data.Core;
using HPIT.Survey.Portal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DD.dal;
namespace DD.EF.Controllers
{
    public class InfoController : Controller
    {
        // GET: Info
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public DeluxeJsonResult QueryPageData(SearchModel<DD.dal.Login_Info> search)
        {
            int total = 0;
            var result = dal.fenye.Instance.GetPageData(search, out total);
            var totalPages = total % search.PageSize == 0 ? total / search.PageSize : total / search.PageSize + 1;
            return new DeluxeJsonResult(new{Data = result,Total = total,TotalPages = totalPages}, "yyyy-MM-dd HH: mm");
        }
    }
}