using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BTL_Nhom8.Helper
{
    public class MyFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["HoTen"])))
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                     { "controller", "Home" },
                     { "action", "Login" }
                });
                if (filterContext.HttpContext.Request.UrlReferrer != null)
                {
                    filterContext.HttpContext.Session["url-redirect"] =
                    filterContext.HttpContext.Request.UrlReferrer.ToString();
                }
                else
                {
                    filterContext.HttpContext.Session["url-redirect"] =
                    filterContext.HttpContext.Request.Url.AbsoluteUri;
                }
                
            } 
            
        }

    }
}