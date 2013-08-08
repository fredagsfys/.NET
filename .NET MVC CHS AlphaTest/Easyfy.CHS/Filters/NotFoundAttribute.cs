using System.Linq;
using System.Web.Mvc;
using Easyfy.CHS.Data.Raven;
using Easyfy.CHS.Model.Athlete;
using Easyfy.CHS.Model.Affiliate;
using Easyfy.CHS.Model.Wod;

namespace Easyfy.CHS.Filters
{
    //public class NotFoundAttribute : ActionFilterAttribute
    //{
    //    public string Id { get; set; }
    //    public string ClassType { get; set; }

    //    public NotFoundAttribute(string id, string classType)
    //    {
    //        Id = id;
    //        ClassType = classType;
    //    }

    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        base.OnActionExecuting(filterContext);

    //        var controller = filterContext.Controller as RavenController;
    //        var db = controller.RavenSession;
    //        var id = filterContext.ActionParameters[Id].ToString();
    //        bool exist = true;

    //        switch (ClassType)
    //        {
    //            case "Athlete":
    //                if (db.Query<Athlete>().Customize(o => o.WaitForNonStaleResultsAsOfNow()).SingleOrDefault(m => m.Username == id) == null)
    //                { exist = false; }
    //                break;

    //            case "Affiliate":
    //                if (db.Query<Affiliate>().Customize(o => o.WaitForNonStaleResultsAsOfNow()).SingleOrDefault(m => m.FriendlyUrl == id) == null)
    //                { exist = false; }
    //                break;

    //            //case "Exercise":
    //            //    if (db.Query<ExerciseBase>().Customize(o => o.WaitForNonStaleResultsAsOfNow()).SingleOrDefault(m => m.Id == id) == null)
    //            //    { exist = false; }
    //            //    break;

    //            case "WOD":
    //                if (db.Query<WodBase>().Customize(o => o.WaitForNonStaleResultsAsOfNow()).SingleOrDefault(m => m.Id == id) == null)
    //                { exist = false; }
    //                break;
    //        }

    //        if (!exist)
    //        {
    //            filterContext.Result = new ViewResult
    //                                    {
    //                                        ViewName = "NotFound",
    //                                        ViewData = filterContext.Controller.ViewData,
    //                                        TempData = filterContext.Controller.TempData
    //                                    };
    //        }
    //    }
    //}
}