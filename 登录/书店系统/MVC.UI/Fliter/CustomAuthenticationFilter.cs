//using HPIT.Survey.Portal.Common;
//using HPIT.Data.Core;
//using MVC.DAL;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Mvc.Filters;
//using System.Web.Routing;
//using Newtonsoft.Json;
//using System;
//using MVC.UI.comm;

//namespace MVCLearn.Filters
//{
//    public class CustomAuthenticationFilter : IAuthenticationFilter
//    {
//        public void OnAuthentication(AuthenticationContext filterContext)
//        {
//            //如果action 和 controller的添加的特性里面包含匿名的，则直接过滤掉。
//            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
//                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true))
//            {
//                return;
//            }
//            var Url = new UrlHelper(filterContext.RequestContext);
//            var urlstr = Url.Action("Login", "Login");
//            filterContext.Result = new RedirectResult(urlstr); //指定返回重定向登录界面
//            HttpCookie cookie = filterContext.HttpContext.Request.Cookies.Get("name");
//            if (cookie == null)
//            {
//                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
//                                       {
//                                           {"controller","Login"},
//                                           {"action","Login"},
//                                           {"returnUrl",filterContext.HttpContext.Request.RawUrl }
//                                       });
//            }
//            else
//            {
//                var value = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(cookie.Value));
//               
//                DeluxeUser.CurrentMember = JsonConvert.DeserializeObject<Book_information>(value);
//            }
//        }

//        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
//        {
//            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
//            {
//                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
//                                       {
//                                           {"controller","Login"},
//                                           {"action","Login"},
//                                           {"returnUrl",filterContext.HttpContext.Request.RawUrl }
//                                       });
//            }
//        }
//    }
//}