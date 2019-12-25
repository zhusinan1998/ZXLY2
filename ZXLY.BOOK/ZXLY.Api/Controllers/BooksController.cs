using HPIT.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZXLY_DAL;

namespace ZXLY.Api.Controllers
{
    public class BooksController : ApiController
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object Get()
        {
            Book book = new Book();
            List<Book_information> list = book.Select();
            var data = new { Data = list };
            return data;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="bookinf">实体类</param>
        /// <returns></returns>
        [HttpPost]
        public int Post(Book_information bookinf)
        {
            return Book.Insert(bookinf);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="bookinf">实体类</param>
        /// <returns></returns>
        [HttpPut]
        public int Put(Book_information bookinf)
        {
            Book book = new Book();
            return book.Update(bookinf);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Bid">书籍编号</param>
        /// <returns></returns>
        //[HttpDelete]
        public int Delete(int Bid)
        {
            Book book = new Book();
            return book.Delete(Bid);
        }
    }
}
