using System.Web.Mvc;
using Easyfy.CHS.Model.Athlete;
using Raven.Client;

namespace Easyfy.CHS.Data.Raven {
  public class RavenController : Controller {
    public IDocumentSession RavenSession { get; private set; }

    public Athlete CurrentUser
    {
        get { return Session["CurrentUser"] as Athlete ?? (Athlete)(Session["CurrentUser"] = new Athlete()); }
    }

    public void RefreshCurrentUser(Athlete athlete)
    {
        Session["CurrentUser"] = athlete;
    }

    protected override void OnActionExecuting(ActionExecutingContext filterContext) {
      RavenSession = RavenStore.GetSession();
      base.OnActionExecuting(filterContext);
    }
  }
}
