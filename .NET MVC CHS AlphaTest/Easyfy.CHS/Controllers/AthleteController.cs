using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.AspNet;
using Easyfy.CHS.Data.Raven;
using Easyfy.CHS.Data.Raven.Extensions;
using Easyfy.CHS.Data.Raven.Indexes;
using Easyfy.CHS.Model.Athlete;
using Easyfy.CHS.Model.Extension;
using Easyfy.CHS.Model.Projection;
using Easyfy.CHS.Model.ViewModel;
using FlexProviders.Membership;
using Easyfy.CHS.Infrastructure;
using Facebook;
using Easyfy.CHS.Model.Affiliate;
using Easyfy.CHS.ViewModels;
using Newtonsoft.Json;
using Easyfy.CHS.Filters;
using Raven.Client;

namespace Easyfy.CHS.Controllers
{
  public class AthleteController : RavenController
  {
    private readonly IFlexMembershipProvider _membershipProvider;
    private readonly IFlexOAuthProvider _oAuthProvider;
    private readonly ISecurityEncoder _encoder = new DefaultSecurityEncoder();

    public AthleteController(IFlexMembershipProvider membership, IFlexOAuthProvider oauth)
    {
      _membershipProvider = membership;
      _oAuthProvider = oauth;
    }

    //
    // GET: /Athlete/
    public ActionResult Index() {
      int pageSize = 5;
      int page = 1;

      RavenQueryStatistics stats;

      var model = new AthleteSearchViewModel {
        AthleteList = RavenSession.Query<Athlete, Athlete_Search>()
                      .Statistics(out stats)
                      .Skip((page - 1) * pageSize)
                      .Take(pageSize)
                      .AsProjection<AthleteListProjection>()
                      .ToList(),

        FacetResults = RavenSession.Query<Athlete, Athlete_Search>().Take(100)
                        .AsProjection<AthleteListProjection>()
                        .ToFacets("Facets/AthleteFacets"),

        TotalPages = (int) Math.Ceiling((decimal)stats.TotalResults / pageSize),
        PageSize = pageSize,
        Page = page
      };

      return View("Index", model);
    }

    public ActionResult SearchAthlete(string athleteTerm, string athleteCheckbox, int page, int pageSize) {

      if (!athleteTerm.EndsWith("*")) athleteTerm = athleteTerm + "*";

      var filters = new List<FacetFilter>();
      if (!string.IsNullOrEmpty(athleteCheckbox))
      {
        var splitCheckboxes = athleteCheckbox.Split(',');
        filters.AddRange(splitCheckboxes.Select(box => new FacetFilter { Key = box.Split(':')[0], Value = box.Split(':')[1] }));
      }

      var model = RavenSession.AutoSearchAthletes(athleteTerm, filters, page, pageSize);

      return PartialView("_ListofAthletes", model);
    }

    //
    // GET: /Athlete/5
    public ActionResult RegistrationDetails(string id, string provider, string providerUserId, string accessToken, string email)
    {

      EditAthleteViewModel model = new EditAthleteViewModel();
      Athlete athlete = RavenSession.Query<Athlete>().Customize(o => o.WaitForNonStaleResultsAsOfNow()).SingleOrDefault(u => u.Username == id);

      // Fetching facebook data
      var client = new FacebookClient(accessToken);
      dynamic result = client.Get("me?fields=id,email,first_name,last_name,birthday,gender,locale,link,username,timezone,location,picture");
      model = JsonConvert.DeserializeObject<EditAthleteViewModel>(result.ToString());

      model.Username = id;
      model.FirstName = result.first_name;
      model.LastName = result.last_name;
      model.Email = email;
      model.Gender = result.gender;
      model.Birth = result.birthday != null ? result.birthday : DateTime.Now;
      model.ProfilePicture = String.Format("http://graph.facebook.com/{0}/picture?type=large", result.username);
      model.ShowPicture = true;
      if (result.location != null)
      {
        string[] locations = result.location.name.Split(',');
        model.Country = locations[1];
        model.City = locations[0];
      }

      _oAuthProvider.OAuthLogin(provider, providerUserId, persistCookie: false);

      athlete.FirstName = result.first_name;
      athlete.LastName = result.last_name;
      athlete.Email = email;
      RefreshCurrentUser(athlete);

      RavenSession.Store(athlete, athlete.Id);
      RavenSession.SaveChanges();
      Session["NewAthlete"] = model;
      ViewBag.ReturnUrl = Url.Action("Details");
      return RedirectToAction("FirstTimeView", "Athlete", new { id = model.Username });
    }

    public ActionResult FirstTimeView(string id)
    {
      // Hämta ut användare med id
      var model = Session["NewAthlete"] as EditAthleteViewModel;

      return View("RegistrationDetails", model);
    }

    //
    // GET: /Athlete/username
    [Authorize]
    [SessionExpireAttribute]
    public ActionResult Details(string id)
    {
      var model = new AthleteAffiliateViewModel();
      model.Affiliate = RavenSession.Query<Affiliate>().SingleOrDefault(u => u.FriendlyUrl == id);
      model.Athlete = RavenSession.Query<Athlete>().SingleOrDefault(u => u.Username == id);
      model.ScheduledWod = RavenSession.Query<ScheduledWod>().ToList();

      return View("Details", model);
    }

    //
    // GET: Athlete/username/Edit
    [Authorize]
    [SessionExpireAttribute]
    [HasAccess("id")]
    public ActionResult Edit(string id)
    {
      var athlete = RavenSession.Query<Athlete>().SingleOrDefault(u => u.Username == id);

      var model = new EditAthleteViewModel
      {
        Username = athlete.Username,
        FirstName = athlete.FirstName != null ? athlete.FirstName : String.Empty,
        LastName = athlete.LastName != null ? athlete.LastName : String.Empty,
        Email = athlete.Email != null ? athlete.Email : String.Empty,
        City = athlete.City != null ? athlete.City : String.Empty,
        Country = athlete.Country != null ? athlete.Country : String.Empty,
        Weight = athlete.Weight != null ? athlete.Weight : String.Empty,
        Height = athlete.Height != null ? athlete.Height : String.Empty,
        Gender = athlete.Gender,
        Birth = athlete.Birth,
        ProfilePicture = athlete.ProfilePicture,
        ShowPicture = true
      };
      return PartialView("_Edit", model);

    }

    //
    // POST: Athlete/username/Edit
    [HttpPost]
    [Authorize]
    [SessionExpireAttribute]
    [HasAccess("id")]
    public ActionResult Edit(string id, EditAthleteViewModel athlete, string ReturnUrl)
    {
      if (ModelState.IsValid)
      {
        try
        {
          Athlete current = RavenSession.Query<Athlete>().SingleOrDefault(u => u.Username == athlete.Username);


          current.FirstName = athlete.FirstName != null ? FormatConverter.StringToChange(athlete.FirstName, false) : String.Empty;
          current.LastName = athlete.LastName != null ? FormatConverter.StringToChange(athlete.LastName, false) : String.Empty;
          current.Email = athlete.Email != null ? athlete.Email : String.Empty;
          current.City = athlete.City != null ? FormatConverter.StringToChange(athlete.City, false) : String.Empty;
          current.Country = athlete.Country != null ? FormatConverter.StringToChange(athlete.Country, false) : String.Empty;
          current.Weight = athlete.Weight != null ? athlete.Weight.Replace(".", ",") : String.Empty;
          current.Height = athlete.Height != null ? athlete.Height.Replace(",", String.Empty).Replace(".", String.Empty) : String.Empty;
          current.Gender = athlete.Gender;
          current.Birth = athlete.Birth;
          current.ProfilePicture = athlete.ShowPicture != false ? athlete.ProfilePicture : null;

          RavenSession.Store(current, current.Id);

          if (CurrentUser.Username == current.Username)
          { RefreshCurrentUser(current); }

          RavenSession.SaveChanges();

          if (ReturnUrl == "_PersonalInfo")
          { return PartialView("_PersonalInfo", current); }
          else
          { return RedirectToAction("Details", new { controller = "Athlete", id = current.Username }); }
        }
        catch (Exception ex)
        {
          // Felhantering
          ModelState.AddModelError(string.Empty, ex.Message);
        }
      }
      return RedirectToAction("Details", new { controller = "Athlete", id = athlete.Username });
    }

    //
    // GET: /Athlete/username/Achievements
    [Authorize]
    [SessionExpireAttribute]
    public ActionResult Achievements(string id)
    {
      var model = RavenSession.Query<Athlete>().SingleOrDefault(u => u.Username == id);

      return View("Achievements", model);
    }

    //
    // GET: /Athlete/username/Goals
    [Authorize]
    [SessionExpireAttribute]
    public ActionResult Goals(string id)
    {
      var athlete = RavenSession.Query<Athlete>().SingleOrDefault(u => u.Username == id);


      return View("Goals", athlete);
    }

    //
    // GET: /Athlete/username/Diary
    [Authorize]
    [SessionExpireAttribute]
    public ActionResult Diary(string id)
    {
      var model = RavenSession.Query<Athlete>().SingleOrDefault(u => u.Username == id);

      return View("Diary", model);
    }

    //
    // GET: /Athlete/username/Records
    [Authorize]
    [SessionExpireAttribute]
    public ActionResult Records(string id)
    {
      var model = RavenSession.Query<Athlete>().SingleOrDefault(u => u.Username == id);

      return View("Records", model);
    }

    //
    // GET: /Athlete/username/Results
    [Authorize]
    [SessionExpireAttribute]
    public ActionResult Results(int? id)
    {
      var page = id ?? 0;

      if (Request.IsAjaxRequest())
      {
          return PartialView("_Result", GetPaginatedResult(page));
      }
        var model = RavenSession.Query<WorkoutResult>().Where(u => u.AthleteId == CurrentUser.Id).OrderByDescending(o => o.Date).Take(2).ToList();
        return View("Results", model);
    }

    private List<WorkoutResult> GetPaginatedResult(int page)
    {
      var skipRecord = page*2;
       var list = RavenSession.Query<WorkoutResult>()
                           .Where(u => u.AthleteId == CurrentUser.Id)
                           .OrderByDescending(o => o.Date).Skip(skipRecord).Take(2).ToList();
        return list;
    }

    [Authorize]
    [SessionExpireAttribute]
    public ActionResult Activity(string id) {
      var model = new ActivityViewModel();

      var athlete = RavenSession.Query<Athlete>().SingleOrDefault(u => u.Username == id);
      model.Id = id;
      model.Activity = RavenSession.GetWorkoutsForWall(athlete);


      return PartialView("Activity", model);
    }

    //
    // GET: /Athlete/username/Stats
    [Authorize]
    [SessionExpireAttribute]
    public ActionResult Stats(string id)
    {
      var model = RavenSession.Query<Athlete>().SingleOrDefault(u => u.Username == id);

      return View("Stats", model);
    }


    //
    // GET: /Athlete/username/Follows
    [Authorize]
    [SessionExpireAttribute]
    public ActionResult Follows(string id, string followId, string name, string lName)
    {
        try
        {

            CurrentUser.Follows.Add(new FollowReference
            {
                Id = followId,
                UserName = id,
                FirstName = name,
                LastName = lName

            });

            RavenSession.Store(CurrentUser, CurrentUser.Id);
            RefreshCurrentUser(CurrentUser);
            RavenSession.SaveChanges();
        }
        catch (Exception ex)
        {
            //Felhantering
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        return RedirectToAction("Details", new { controller = "Athlete", id = id });
    }
    //
    // GET: /Athlete/username/Follows
    [Authorize]
    [SessionExpireAttribute]
    public ActionResult UnFollows(string id, string followId)
    {
        try
        {
            FollowReference follower = CurrentUser.Follows.Find(x => x.Id == followId);
            CurrentUser.Follows.Remove(follower);

            RavenSession.Store(CurrentUser, CurrentUser.Id);
            RefreshCurrentUser(CurrentUser);
            RavenSession.SaveChanges();
        }
        catch (Exception ex)
        {
            //Felhantering
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        return RedirectToAction("Details", new { controller = "Athlete", id = id });

    }
  }
}
