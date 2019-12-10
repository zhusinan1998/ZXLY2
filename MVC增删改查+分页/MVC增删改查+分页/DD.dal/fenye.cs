using HPIT.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.dal
{
    public class fenye
    {
        public static fenye Instance = new fenye();
        public user_model context { get; set; }
        public fenye()
        {
            this.context = new user_model();
        }
        public object GetPageData(SearchModel<Login_Info> search, out int count)
        {
            GetPageListParameter<Login_Info,int> par = new GetPageListParameter<Login_Info, int>();
            par.isAsc = true;
            par.orderByLambda = t => t.UserId;
            par.pageIndex = search.PageIndex;
            par.pageSize = search.PageSize;
            par.whereLambda = t => t.UserId != 0;
            user_model Instance = new user_model();
            DBBaseService baseService = new DBBaseService(Instance);
            List<Login_Info> list = baseService.GetSimplePagedData<Login_Info, int>(par, out count);
            return list;
        }
    }

}
