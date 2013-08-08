using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easyfy.CHS.Data.Raven;
using Easyfy.CHS.Data.Raven.Extensions;
using Easyfy.CHS.Filters;
using Easyfy.CHS.Model.Affiliate;
using Easyfy.CHS.Model.Athlete;

namespace LogMeIn.Raven.Controllers
{
    public class AdminController : RavenController, IActionFilter
    {
        public ActionResult Index()
        {
            if (CurrentUser.Admin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", new { controller = "Home" });
            }
        }

        public ActionResult Affiliates()
        {
            if (CurrentUser.Admin)
            {
                var model = RavenSession.Query<Affiliate>().Customize(o => o.WaitForNonStaleResultsAsOfNow()).ToList();

                return PartialView("Affiliates", model);
            }
            else
            {
                return RedirectToAction("Index", new { controller = "Home" });
            }
        }

        public ActionResult Athletes()
        {
            if (CurrentUser.Admin)
            {
                var model = RavenSession.Query<Athlete>().Customize(o => o.WaitForNonStaleResultsAsOfNow()).ToList();

                return PartialView("Athletes", model);
            }
            else
            {
                return RedirectToAction("Index", new { controller = "Home" });
            }
        }

        public ActionResult DeleteAthlete(string id)
        {
            if (CurrentUser.Admin)
            {
                var athlete = RavenSession.Query<Athlete>().SingleOrDefault(u => u.Username == id);

                if (athlete == null)
                {
                    return View("NotFound");
                }

                try
                {
                    RavenSession.Delete(athlete);
                    RavenSession.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                return RedirectToAction("Index", new { controller = "Admin" });
            }
            else
            {
                return RedirectToAction("Index", new { controller = "Home" });
            }
        }
    }
}
