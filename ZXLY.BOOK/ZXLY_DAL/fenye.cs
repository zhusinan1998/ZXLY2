using HPIT.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZXLY_DAL
{
  public  class fenye
    {
        public static fenye Instance = new fenye();
        public Model1 context { get; set; }
        public fenye()
        {
            this.context = new Model1();
        }
        public object GetPageData(SearchModel<Book_information> search, out int count)
        {
            GetPageListParameter<Book_information, int> par = new GetPageListParameter<Book_information, int>();
            par.isAsc = true;
            par.orderByLambda = t => t.Bid;
            par.pageIndex = search.PageIndex;
            par.pageSize = search.PageSize;
            par.whereLambda = t => t.Bid != 0;
            Model1 Instance = new Model1();
            DBBaseService baseService = new DBBaseService(Instance);
            List<Book_information> list = baseService.GetSimplePagedData<Book_information, int>(par, out count);
            return list;
        }
    }

}
