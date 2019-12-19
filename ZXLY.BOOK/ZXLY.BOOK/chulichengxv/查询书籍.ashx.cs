using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZXLY_DAL;

namespace ZXLY.BOOK.chulichengxv
{
    /// <summary>
    /// 查询书籍 的摘要说明
    /// </summary>
    public class 查询书籍 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            //List<Book_information> list = Book.selelist();
            //var json = new { data = list };
            //string json1 = JsonConvert.SerializeObject(json);
            //context.Response.Write(json1);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}