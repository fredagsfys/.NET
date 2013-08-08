using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easyfy.CHS.Data.Raven;
using Easyfy.CHS.Model.Athlete;

namespace Easyfy.CHS.Filters
{
    public class FilterRolesAttribute : ActionFilterAttribute
    {
        public string Roles { get; set; }
        public string Id { get; set; }

        public FilterRolesAttribute(string roles, string id)
        {
            Roles = roles;
            Id = id;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //var id = filterContext.ActionParameters[Id].ToString();
            //var controller = filterContext.Controller as RavenController;
            //Athlete user = controller.CurrentUser;

            //AffiliateRoleReference affiliateRole = user.Affiliates.FirstOrDefault(m => m.FriendlyUrl == id);

            //bool hasAccess = false;
            //foreach (var role in Roles.Split(','))
            //{
            //    if (affiliateRole != null && affiliateRole.AffiliateRoles.Contains(role) || user.Admin)
            //    {
            //        hasAccess = true;
            //    }                
            //}

            //if (!hasAccess)
            //{
            //    filterContext.Result = new ViewResult
            //    {
            //        ViewName = "NotFound",
            //        ViewData = filterContext.Controller.ViewData,
            //        TempData = filterContext.Controller.TempData
            //    };
            //}
        }
    }

    public class HasAccess : ActionFilterAttribute
    {
        public string Id { get; set; }

        public HasAccess(string id)
        {
            Id = id;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var id = filterContext.ActionParameters[Id].ToString();
            var controller = filterContext.Controller as RavenController;
            Athlete current = controller.CurrentUser;

            if (current.Username != id && !current.Admin)
            {
                filterContext.Result = new ViewResult
                                        {
                                            ViewName = "NotFound",
                                            ViewData = filterContext.Controller.ViewData,
                                            TempData = filterContext.Controller.TempData
                                        };
            }
        }
    }
}