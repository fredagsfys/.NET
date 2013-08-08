using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Easyfy.CHS.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
 	        base.OnActionExecuting(filterContext);

            HttpContext ctx = HttpContext.Current;
            if (ctx.Session != null)
            {
                if (ctx.Session.IsNewSession)
                {
                    var sessioncookie = ctx.Request.Headers["Cookie"];
                    if (sessioncookie != null && sessioncookie.IndexOf("ASP.NET_SessionId") >= 0)
                    {
                        string redirectOnSuccess = filterContext.HttpContext.Request.Url.PathAndQuery;
                        string redirectUrl = string.Format("?ReturnUrl={0}", redirectOnSuccess);
                        string loginUrl = FormsAuthentication.LoginUrl + redirectUrl;

                        if (ctx.Request.IsAuthenticated)
                        {
                            FormsAuthentication.SignOut();
                        }

                        RedirectResult rr = new RedirectResult(loginUrl);
                        filterContext.Result = rr;
                    }
                }
            }
        }
    }
}