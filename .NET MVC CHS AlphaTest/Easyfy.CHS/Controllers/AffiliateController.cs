using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Easyfy.CHS.Data.Raven;
using Easyfy.CHS.Data.Raven.Extensions;
using Easyfy.CHS.Data.Raven.Indexes;
using Easyfy.CHS.Helpers;
using Easyfy.CHS.Infrastructure;
using Easyfy.CHS.Model.Affiliate;
using Easyfy.CHS.Model.Athlete;
using Easyfy.CHS.Model.Exercise;
using Easyfy.CHS.Model.Extension;
using Easyfy.CHS.Model.Projection;
using Easyfy.CHS.Model.ViewModel;
using Easyfy.CHS.Model.Wod;
using Easyfy.CHS.ViewModels;
using Easyfy.CHS.Filters;
using Raven.Client;

namespace Easyfy.CHS.Controllers
{
  public class AffiliateController : RavenController
  {
    public ActionResult Index()
    {
      var model = new AffiliateSearchViewModel
        {

          AffiliateList = RavenSession.Query<Affiliate, Affiliate_Search>().Take(100)
                                      .AsProjection<AffiliateListProjection>()
                                      .ToList(),

          FacetResults = RavenSession.Query<Affiliate, Affiliate_Search>().Take(100)
                                     .AsProjection<AffiliateListProjection>()
                                     .ToFacets("Facets/AffiliateFacets"),
        };

      return View("Index", model);
    }

    public ActionResult SearchAffiliate(string affiliateTerm, string checkboxValue)
    {
      if (!affiliateTerm.EndsWith("*")) affiliateTerm = affiliateTerm.Trim() + "*";

      var filters = new List<FacetFilter>();
      if (!string.IsNullOrEmpty(checkboxValue))
      {
        var splitCheckboxes = checkboxValue.Split(',');
        filters.AddRange(
          splitCheckboxes.Select(box => new FacetFilter {Key = box.Split(':')[0], Value = box.Split(':')[1]}));
      }

      var list = RavenSession.AutoSearchAffiliate(affiliateTerm, filters);

      return PartialView("_ListofAffiliates", list);
    }

    //
    // GET: /Gym/5
    [Authorize]
    [SessionExpireAttribute]
    public ActionResult Details(string id)
    {
      var model = new AthleteAffiliateViewModel
        {
          Affiliate = RavenSession.Query<Affiliate>()
                                  .Customize(o => o.WaitForNonStaleResultsAsOfNow())
                                  .SingleOrDefault(u => u.FriendlyUrl == id),
          Athlete = CurrentUser
        };
      return View("Details", model);
    }

    //
    // GET: /Gym/Create
    [Authorize]
    [SessionExpireAttribute]
    public ActionResult Create()
    {
      var model = new Affiliate();
      return PartialView("_Create", model);
    }

    // POST: /Gym/Create
    [HttpPost]
    [Authorize]
    [SessionExpireAttribute]
    public ActionResult Create(Affiliate affiliate)
    {
      if (ModelState.IsValid)
      {
        try
        {
          affiliate.Name = FormatConverter.StringToChange(affiliate.Name, true);
          int add_to_name = 1;
          while (RavenSession.Query<Affiliate>().SingleOrDefault(u => u.Name == affiliate.Name) != null)
          {
            affiliate.Name = add_to_name > 1
                               ? affiliate.Name =
                                 affiliate.Name.Substring(0, affiliate.Name.Length - 1) + add_to_name.ToString()
                               : affiliate.Name = affiliate.Name + "_" + add_to_name.ToString();

            add_to_name++;
          }
          affiliate.FriendlyUrl = SlugConverter.TitleToSlug(affiliate.Name);
          affiliate.Address = FormatConverter.StringToChange(affiliate.Address, false);
          affiliate.City = FormatConverter.StringToChange(affiliate.City, false);
          affiliate.Country = FormatConverter.StringToChange(affiliate.Country, false);
          affiliate.Postal = FormatConverter.NumberToChange(affiliate.Postal);


          RavenSession.Store(affiliate);

          CurrentUser.Affiliates.Add(new AffiliateRoleReference
            {
              AffiliateRoles = new List<string> {"Owner"},
              Id = affiliate.Id,
              Name = affiliate.Name,
              FriendlyUrl = affiliate.FriendlyUrl
            });

          RavenSession.Store(CurrentUser, CurrentUser.Id);
          RavenSession.SaveChanges();

          return RedirectToAction("Details", new {controller = "Affiliate", id = affiliate.FriendlyUrl});
        }
        catch (Exception ex)
        {
          // Felhantering
          ModelState.AddModelError(string.Empty, ex.Message);
        }
      }
      return PartialView("_Create", affiliate);
    }

    //
    // GET: Gym/Edit/5
    [Authorize]
    [SessionExpireAttribute]
    [FilterRolesAttribute("Owner", "id")]
    public ActionResult Edit(string id)
    {
      var model = RavenSession.Query<Affiliate>().SingleOrDefault(u => u.FriendlyUrl == id);

      return PartialView("_Edit", model);
    }

    //
    // POST: Gym/Edit/5
    [HttpPost]
    [Authorize]
    [SessionExpireAttribute]
    [FilterRolesAttribute("Owner", "id")]
    public ActionResult Edit(string id, Affiliate affiliate)
    {
      try
      {
        Affiliate current = RavenSession.Query<Affiliate>().SingleOrDefault(u => u.FriendlyUrl == affiliate.FriendlyUrl);
        current.Name = FormatConverter.StringToChange(affiliate.Name, true);
        current.Address = FormatConverter.StringToChange(affiliate.Address, false);
        current.Postal = FormatConverter.NumberToChange(affiliate.Postal);
        current.City = FormatConverter.StringToChange(affiliate.City, false);
        current.Country = FormatConverter.StringToChange(affiliate.Country, false);
        current.Description = affiliate.Description;
        RavenSession.SaveChanges();
        return PartialView("_AffiliateInfo", current);
      }
      catch (Exception ex)
      {
        // Felhantering
        ModelState.AddModelError(string.Empty, ex.Message);
      }
      return View(User);
    }

    //
    // POST: Gym/Delete/5
    [Authorize]
    [SessionExpireAttribute]
    [FilterRolesAttribute("Owner", "id")]
    [HttpGet]
    public ActionResult Delete(string id)
    {
      Affiliate affiliate = RavenSession.Query<Affiliate>().SingleOrDefault(u => u.FriendlyUrl == id);
      var athletes =
        RavenSession.Query<Athlete>().Where(o => o.Affiliates.Any(x => x.FriendlyUrl == affiliate.FriendlyUrl));

      if (affiliate == null)
      {
        return View("NotFound");
      }

      try
      {
        foreach (var athlete in athletes)
        {
          var affiliatesForAthlete = athlete.Affiliates.FirstOrDefault(o => o.Id == affiliate.Id);
          if (affiliatesForAthlete != null)
          {
            athlete.Affiliates.Remove(affiliatesForAthlete);
          }
          if (athlete.Username == CurrentUser.Username)
          {
            RefreshCurrentUser(athlete);
          }
        }
        RavenSession.Delete(affiliate);
        RavenSession.SaveChanges();
      }
      catch (Exception ex)
      {
        // Felhantering
        ModelState.AddModelError(string.Empty, ex.Message);
      }
      return RedirectToAction("Index", new {controller = "Affiliate"});
    }

    [Authorize]
    [SessionExpireAttribute]
    public ActionResult JoinAffiliate(string id, string gymid, string name, string role)
    {
      try
      {
        //Kollar så att gym inte redan finns på en athlete.
        if (!CurrentUser.Affiliates.Exists(s => s.FriendlyUrl == id))
        {
          CurrentUser.Affiliates.Add(
            new AffiliateRoleReference
              {
                AffiliateRoles = new List<string> {role},
                Id = gymid,
                Name = name,
                FriendlyUrl = id
              });

          RavenSession.Store(CurrentUser, CurrentUser.Id);
          RefreshCurrentUser(CurrentUser);
          RavenSession.SaveChanges();
        }
        else
        {
          CurrentUser.Affiliates.FirstOrDefault(m => m.FriendlyUrl == id).AffiliateRoles.Add(role);
          RavenSession.Store(CurrentUser, CurrentUser.Id);
          RefreshCurrentUser(CurrentUser);
          RavenSession.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        // Felhantering
        ModelState.AddModelError(string.Empty, ex.Message);
      }

      return RedirectToAction("Details", new {controller = "Affiliate", id = id});
    }

    [Authorize]
    [SessionExpireAttribute]
    public ActionResult LeaveAffiliate(string id, string gymid, string role)
    {
      try
      {
        CurrentUser.Affiliates.Find(x => x.FriendlyUrl == id).AffiliateRoles.Remove(role);

        if (CurrentUser.Affiliates.Find(x => x.FriendlyUrl == id).AffiliateRoles.Count == 0)
        {
          CurrentUser.Affiliates.Remove((CurrentUser.Affiliates.FirstOrDefault(x => x.FriendlyUrl == id)));
        }

        RavenSession.Store(CurrentUser, CurrentUser.Id);
        RefreshCurrentUser(CurrentUser);
        RavenSession.SaveChanges();
      }
      catch (Exception ex)
      {
        //Felhantering
        ModelState.AddModelError(string.Empty, ex.Message);
      }

      return RedirectToAction("Details", new {controller = "Affiliate", id = id});
    }

    [Authorize]
    [SessionExpireAttribute]
    public ActionResult ShowAffiliateAthletes(string id)
    {
      Affiliate affiliate = RavenSession.Query<Affiliate>().SingleOrDefault(u => u.FriendlyUrl == id);

      var model = new AthleteAffiliateViewModel
        {
          AthleteList =
            RavenSession.Query<Athlete>()
                        .Where(o => o.Affiliates.Any(x => x.FriendlyUrl == affiliate.FriendlyUrl))
                        .ToList(),
          Affiliate = affiliate
        };

      return PartialView("_ShowAffiliateAthletes", model);
    }

    //
    // GET: Athlete/username/Delete
    [Authorize]
    [SessionExpireAttribute]
    [FilterRolesAttribute("Owner", "id")]
    public ActionResult RemoveAffiliateAthlete(string id, string Username)
    {
      try
      {
        Athlete athlete = RavenSession.Query<Athlete>().SingleOrDefault(u => u.Username == Username);

        AffiliateRoleReference roleReference = athlete.Affiliates.Find(x => x.FriendlyUrl == id);
        athlete.Affiliates.Remove(roleReference);

        if (athlete.Username == CurrentUser.Username)
        {
          RefreshCurrentUser(athlete);
        }
        RavenSession.SaveChanges();

        return RedirectToAction("Details", "Affiliate", id);
      }
      catch (Exception ex)
      {
        //Felhantering
        ModelState.AddModelError(string.Empty, ex.Message);
      }

      return RedirectToAction("Details", "Affiliate", id);

    }

    [Authorize]
    [SessionExpireAttribute]
    [FilterRolesAttribute("Owner", "id")]
    public ActionResult UpdateAffiliateRole(string id, string username, string role)
    {
      try
      {
        Athlete athlete = RavenSession.Query<Athlete>().SingleOrDefault(u => u.Username == username);
        AffiliateRoleReference current = athlete.Affiliates.Find(x => x.FriendlyUrl == id);

        if (!current.AffiliateRoles.Any(role.Contains))
        {
          current.AffiliateRoles.Add(role);
        }
        else
        {
          current.AffiliateRoles.Remove(role);
        }

        if (current.AffiliateRoles.Count == 0)
        {
          athlete.Affiliates.Remove(current);
        }


        if (athlete.Username == CurrentUser.Username)
        {
          RefreshCurrentUser(athlete);
        }
        RavenSession.SaveChanges();

        return RedirectToAction("Details", "Affiliate", id);
      }
      catch (Exception ex)
      {
        //Felhantering
        ModelState.AddModelError(string.Empty, ex.Message);
      }

      return RedirectToAction("Details", "Affiliate", id);
    }

    [Authorize]
    [SessionExpireAttribute]
    [FilterRolesAttribute("Owner,Coach", "id")]
    public ActionResult ScheduleWod(string affiliateUrl)
    {
      var model = new ScheduledWod();
      var affiliate = RavenSession.Query<Affiliate>().SingleOrDefault(a => a.FriendlyUrl == affiliateUrl);
      if (affiliate == null)
        return new EmptyResult();
      model.AffiliateId = affiliate.Id;
      model.AffiliateUrl = affiliate.FriendlyUrl;

      return View("_ScheduleWod", model);
    }

    public ActionResult AddWod(string wodId)
    {
      var wod = RavenSession.Load<WodBase>(wodId);
      var model = new List<WorkoutWod>();
      if (wod != null)
      {
        var workout = new WorkoutWod
          {
            WodId = wod.Id,
            WodName = wod.Name,
            WodDescription = wod.Description,
            WodTitleDescription = wod.GetTitleString(),
            WodResultDescription = wod.GetResultInformation(),
            WodHeroesDescription = wod.GetHeroDescription(),
            WodExerciseInfo = wod.GetExercises()
          };
        model.Add(workout);
        return PartialView("_AddWod", model);
      }
      return RedirectToAction("Details", "Affiliate");
    }

    [Authorize]
    [SessionExpireAttribute]
    [HttpPost]
    public ActionResult ScheduleWod(ScheduledWod scheduledWod)
    {
      try
      {
        var wod = RavenSession.Query<WodBase>().SingleOrDefault(w => w.Name == scheduledWod.WodName);

        if (wod != null) {
          scheduledWod.Id = null;
          scheduledWod.WodList.Add(
              new WorkoutWod
              {
                WodId = wod.Id,
                WodName = wod.Name,
                WodDescription = wod.Description,
                WodTitleDescription = wod.GetTitleString(),
                WodResultDescription = wod.GetResultInformation(),
                WodHeroesDescription = wod.GetHeroDescription(),
                WodExerciseInfo = wod.GetExercises()
              }
          );
        }

        RavenSession.Store(scheduledWod);
        RavenSession.SaveChanges();
      }
      catch (Exception ex)
      {
        ModelState.AddModelError(string.Empty, ex.Message);
      }

      return RedirectToAction("Details", "Affiliate", scheduledWod.Id);
    }

    [Authorize]
    public ActionResult ScheduledWods(string affiliateUrl) {
      var affiliate = RavenSession.Query<Affiliate>().FirstOrDefault(o => o.FriendlyUrl == affiliateUrl);
      if(affiliate == null)
        return new EmptyResult();

      var model = RavenSession.Query<ScheduledWod>().Where(o => o.AffiliateId == affiliate.Id).ToList();

      return PartialView("_Wod", model);
    }

    public ActionResult FindDistinctWod(string q, int limit)
    {
      var wods = CacheHelper.Cacheify("wods", "allwods", 10,
                                           () =>
                                           RavenSession.Query<WodBase>()
                                                       .Customize(z => z.WaitForNonStaleResultsAsOfLastWrite())
                                                       .ToList());
      var wodList = (from e in wods.ToList()
                          select new WodJsonResult
                          {
                            Name = e.Name,
                            WodType = e.WodType.ToString(),
                            WodId = e.Id
                          }).ToList();

      var merged = new List<WodJsonResult>(wodList);

      var filtered = (from e in merged.ToList()
                      where e.Name.ToLower().Contains(q.ToLower()) || e.WodType.ToLower().Contains(q.ToLower())
                      orderby e.Name
                      select new WodJsonResult
                      {
                        Name = e.Name,
                        WodType = e.WodType,
                        WodId = e.WodId
                      }).ToList();



      return Json(filtered.ToList(), JsonRequestBehavior.AllowGet);

    }
  }
}
