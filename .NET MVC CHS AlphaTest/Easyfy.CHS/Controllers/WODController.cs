using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Easyfy.CHS.Data.Raven;
using Easyfy.CHS.Data.Raven.Extensions;
using Easyfy.CHS.Data.Raven.Indexes;
using Easyfy.CHS.Model.Affiliate;
using Easyfy.CHS.Model.Athlete;
using Easyfy.CHS.Model.Exercise;
using Easyfy.CHS.Model.Extension;
using Easyfy.CHS.Model.Projection;
using Easyfy.CHS.Model.ViewModel;
using Easyfy.CHS.Model.Wod;
using Easyfy.CHS.ViewModels;
using Easyfy.CHS.Helpers;
using Easyfy.CHS.Filters;
using Raven.Client;
using Microsoft.AspNet.SignalR;
using Easyfy.CHS.Infrastructure;

namespace Easyfy.CHS.Controllers {
  public class WodController : RavenController {

    public ActionResult Index() {
      var model = new WodSearchViewModel {
        WodList = RavenSession.Query<WodBase, Wod_Search>().Take(100)
                              .AsProjection<WodListProjection>()
                              .ToList(),
        FacetResults = RavenSession.Query<WodBase, Wod_Search>().Take(100)
        .AsProjection<WodListProjection>()
        .ToFacets("Facets/WodFacets")
      };


      return View("Wods", model);
    }
    
    public ActionResult SearchWod(string term, string checkboxValue) {
      
      // Searchword
      if (!term.EndsWith("*")) term = term.Trim() + "*";

      // filter query
      var filters = new List<FacetFilter>();
      if (!string.IsNullOrEmpty(checkboxValue)) {
        var splitCheckboxes = checkboxValue.Split(',');
        filters.AddRange(splitCheckboxes.Select(box => new FacetFilter {Key = box.Split(':')[0], Value = box.Split(':')[1]}));
      }

      // Get result
      var model = RavenSession.AutoSearchWods(term, filters);
      
      return PartialView("_ListofWods", model);
    }

      // GET WOD/Create
    [FilterRolesAttribute("Owner,Coach", "id")]
    public ActionResult CreateWOD(string id) {
      var affiliate = RavenSession.Query<Affiliate>().SingleOrDefault(a => a.FriendlyUrl == id);

      var model = new WodViewModel() {
        AffiliateId = affiliate.Id,
        AffiliateName = affiliate.Name,
        AffiliateFriendlyUrl = affiliate.FriendlyUrl
      };

      return PartialView("_CreateWOD", model);
    }

    // POST WOD/Create
    public ActionResult CreateRound(string exerciseId, string sortOrder) {
      var model = new ExerciseRound();
      var round = new Round();

      var item = RavenSession.Load<ExerciseBase>(exerciseId);

      model.ExerciseId = item.Id;
      model.ExerciseName = item.ExerciseType == ExerciseType.Metabolic
                             ? item.Name + " " + item.Length + " " + item.LengthTypeToString
                             : item.Name;
      model.ExerciseType = item.ExerciseType;

      round.ExerciseRounds.Add(model);

      return PartialView("_CreateRound", round);
    }

    public ActionResult CreateExerciseRound(string exerciseId, string containerPrefix) {
      ViewData["ContainerPrefix"] = containerPrefix;

      var model = new ExerciseRound();

      var item = RavenSession.Load<ExerciseBase>(exerciseId);

      model.ExerciseId = item.Id;
      model.ExerciseName = item.ExerciseType == ExerciseType.Metabolic
                             ? item.Name + " " + item.Length + " " + item.LengthTypeToString
                             : item.Name;
      model.ExerciseType = item.ExerciseType;

      return PartialView("_CreateExerciseRound", model);
    }

    [HttpPost]
    public ActionResult CreateWOD(WodViewModel model) {
      try {
        model.WodBase.AffiliateReference = new AffiliateReference {
          FriendlyUrl = model.AffiliateFriendlyUrl,
          Id = model.AffiliateId,
          Name = model.AffiliateName
        };

        RavenSession.Store(model.WodBase);
        RavenSession.SaveChanges();

        return RedirectToAction("Details", "Affiliate", new {id = model.AffiliateFriendlyUrl});
      }
      catch (Exception e) {
        return View("Error", e.Message);
      }
    }

    [HttpPost]
    public ActionResult FindDistinctExercise(string term) {
      try {
        var list = RavenSession.Query<ExerciseBase>().Customize(z => z.WaitForNonStaleResultsAsOfLastWrite()).ToList();
        var exercieses =
          Json(
            list.Where(u => u.Name.ToLower().Contains(term))
                .Select(u => string.Format("{0} ({1})", u.Name, u.ExerciseType))
                .Distinct()
                .OrderBy(s => s)
                .ToList(), JsonRequestBehavior.AllowGet);

        return exercieses;
      }
      catch (Exception ex) {
        ModelState.AddModelError(String.Empty, ex.Message);
      }
      return View("Error");
    }

    public JsonResult AllExercises(string q, int limit) {
      var exercises = CacheHelper.Cacheify("exercise", "allexercises", 10,
                                           () =>
                                           RavenSession.Query<ExerciseBase>()
                                                       .Customize(z => z.WaitForNonStaleResultsAsOfLastWrite())
                                                       .ToList());

      var metabolicList = (from e in exercises.ToList()
                           where e.ExerciseType == ExerciseType.Metabolic
                           select new ExerciseJsonResult {
                             Name =
                               e.Name + " " + e.Length + " " +
                               e.LengthTypeToString,
                             ExerciseType = e.ExerciseType.ToString(),
                             ExerciseId = e.Id
                           }).ToList();

      var exerciseList = (from e in exercises.ToList()
                          where e.ExerciseType != ExerciseType.Metabolic
                          select new ExerciseJsonResult {
                            Name = e.Name,
                            ExerciseType = e.ExerciseType.ToString(),
                            ExerciseId = e.Id
                          }).ToList();


      var merged = new List<ExerciseJsonResult>(metabolicList);
      merged.AddRange(exerciseList);

      var filtered = (from e in merged.ToList()
                      where e.Name.ToLower().Contains(q.ToLower()) || e.ExerciseType.ToLower().Contains(q.ToLower())
                      orderby e.Name
                      select new ExerciseJsonResult {
                        Name = e.Name,
                        ExerciseType = e.ExerciseType,
                        ExerciseId = e.ExerciseId

                      }).ToList();



      return Json(filtered.ToList(), JsonRequestBehavior.AllowGet);

    }

    public ActionResult WodReportResult(string wodId, string scheduledId) {
      var model = new WodViewModel();
      var wod = RavenSession.Load<WodBase>(wodId);
      //var scheduledWod = RavenSession.Load<ScheduledWod>(scheduledId);

      model.Result = new Result();

      model.WodBase = wod;
      model.Result.ScheduledWodId = scheduledId;
      model.Result.WodId = wod.Id;
      model.Result.WodName = wod.Name;
      model.Result.Date = DateTime.Now;
      model.Result.ResultType = wod.DefaultResultType;

      return PartialView("_ReportWODResult", model);
    }
    //public ActionResult FixedDates()
    //{
    //    var results = RavenSession.Query<WorkoutResult>().ToList();
    //    foreach (var workoutResult in results)
    //    {
    //        workoutResult.Date = DateTime.Today.AddDays(-results.IndexOf(workoutResult));
    //    }
    //    RavenSession.SaveChanges();
    //    return Content("OK");
    //}
    [HttpPost]
    public ActionResult CreateWodResult(Result result) {
      try
      {
        var user = RavenSession.Load<Athlete>(CurrentUser.Id);
        var stats = RavenSession.Query<Stat>().FirstOrDefault(u => u.UserReference == user.Id) ??
                    new Stat {UserReference = user.Id};
        var athleteStat = RavenSession.Query<AthleteStatistics>().FirstOrDefault(u => u.UserReference == user.Id) ??
                   new AthleteStatistics { UserReference = user.Id };
        var scheduledWod = RavenSession.Load<ScheduledWod>(result.ScheduledWodId);
        var currentWod = RavenSession.Load<WodBase>(result.WodId);

        var date = scheduledWod.Date;
        var workoutResult =
          RavenSession.Query<WorkoutResult>()
                      .FirstOrDefault(u => u.AthleteId == user.Id && u.Date.Date == date) ?? new WorkoutResult{AthleteId = user.Id};

        workoutResult.Date = DateTime.UtcNow;
        workoutResult.Results.Add(result);
        
        stats = WodStatistics(result, stats);
        stats = ExerciseStatistics(result, stats, currentWod);

        //Metoder för att räkna ut tot/best stat
        athleteStat = WodTotalStatistics(athleteStat, result, currentWod);
        athleteStat = ExerciseTotalStatistics(athleteStat, result, currentWod);

        if(workoutResult.Id == null)
          RavenSession.Store(stats);
        RavenSession.Store(workoutResult);
        RavenSession.SaveChanges();

        ActivityViewModel activityViewModel = new ActivityViewModel();
        activityViewModel.Activity.Add(new WorkoutResultProjection
        {
            WorkoutResultId = workoutResult.Id,
            Date = workoutResult.Date,
            Results = workoutResult.Results,
            AthleteId = CurrentUser.Id,
            AthleteName = CurrentUser.FirstName + ' ' + CurrentUser.LastName,
            AthleteImageUrl = user.WallImageUrl
        });
        

        var context = GlobalHost.ConnectionManager.GetHubContext<AthleteHub>();
        var data = this.RenderPartialViewToString("~/Views/Athlete/_WallPost.cshtml", activityViewModel);
        context.Clients.Group(CurrentUser.Id).message(data);

      }
			catch (Exception ex) {
				ModelState.AddModelError(String.Empty, ex.Message);
			}
      return RedirectToAction("Results", "Athlete", new {id = CurrentUser.Username});
    }

    private AthleteStatistics WodTotalStatistics(AthleteStatistics athleteStat, Result result, WodBase currentWod)
    {
      var wodTotStat = athleteStat.WodTotalStats.FirstOrDefault(u => u.WodId == result.WodId) ??
                       new WodTotalStatistics
                         {
                           WodName = result.WodName,
                           TimeStamp = result.Date,
                           ResultType = result.ResultType,
                           WodType = currentWod.WodType,
                         };
      if (wodTotStat.WodId == null)
      {
        wodTotStat.WodId = result.WodId;
        wodTotStat.BestResult = result.Value;
        wodTotStat.AggregatedResult = result.Value;
        wodTotStat.Count = 1;

        athleteStat.WodTotalStats.Add(wodTotStat);
        RavenSession.Store(athleteStat);
      }
      else
      {
        wodTotStat.AggregatedResult += result.Value;
        wodTotStat.Count += 1;

        switch (result.ResultType)
        {
          case ResultType.Time:
            if (wodTotStat.BestResult > result.Value)
              wodTotStat.BestResult = result.Value;
            break;
          default:
            if (wodTotStat.BestResult < result.Value)
              wodTotStat.BestResult = result.Value;
            break;
        }
      }
      return athleteStat;
    }

    private AthleteStatistics ExerciseTotalStatistics(AthleteStatistics athleteStat, Result result, WodBase currentWod)
    {
      var calcRound = currentWod.Blubb();

      foreach (var item in calcRound.ExerciseRounds)
      {
        var ExrSingleWeight = currentWod.Rounds.SelectMany(o => o.ExerciseRounds)
                          .FirstOrDefault(f => f.ExerciseName == item.ExerciseName)
                          .Weight;
        var exrTotStat = athleteStat.ExerciseTotalStats.FirstOrDefault(u => u.ExerciseGroup == item.ExerciseName) ??
                         new ExerciseTotalStatistics()
                           {
                             ExerciseType = item.ExerciseType,
                             TimeStamp = result.Date,
                           };
        if (exrTotStat.ExerciseGroup == null)
        {
          exrTotStat.ExerciseGroup = item.ExerciseName;
          exrTotStat.UnbrokenReps = result.UnbrokenReps;
          exrTotStat.TotalWeight = item.Weight;
          exrTotStat.TotalReps = item.Reps;
          //Måste få ut vikten på övningen fast den nu är summerad, ExrSingleWeight är alltså 43kg på en Fran i övning Thruster.
            exrTotStat.MaxWeight = ExrSingleWeight;
                

          athleteStat.ExerciseTotalStats.Add(exrTotStat);
        }
        else
        {
          if (result.UnbrokenReps > exrTotStat.UnbrokenReps)
            exrTotStat.UnbrokenReps = result.UnbrokenReps;
          //Måste få ut vikten på övningen fast den nu är summerad, ExrSingleWeight är alltså 43kg på en Fran i övning Thruster.
          if (ExrSingleWeight > exrTotStat.MaxWeight)
            exrTotStat.MaxWeight = item.Weight;

          exrTotStat.TotalWeight += item.Weight;
          exrTotStat.TotalReps += item.Reps;
        }
      }

      return athleteStat;
    }

    private Stat ExerciseStatistics(Result result, Stat stats, WodBase currentWod)
    {
      //Om det inte är resulttype reps så sätts värdet 1.
      var calcRep = result.Value == 0 ? 1 : result.Value;
      //Är rounds mer än 1 så måste alla övningar med samma namn summeras ihop.
      if (currentWod.Rounds.Count > 1)
      {
        //var eRound = currentWod.Blubb();
        var calcRound = currentWod.Blubb();
        foreach (var item in calcRound.ExerciseRounds)
        {

          var reference = item.ExerciseId;

          var exercise = new ExerciseStatistics
            {
              TimeStamp = DateTime.Now,
              ExerciseGroup = item.ExerciseName,
              Weight = item.Weight,
              Reps = item.Reps*calcRep
            };
          var exerciseList = new List<ExerciseStatistics> {exercise};

          if (stats.ExerciseStats.ContainsKey(reference))
            stats.ExerciseStats[reference].Add(exercise);

          else
            stats.ExerciseStats.Add(reference, exerciseList);
        }
      }
        //Om rounds är 1 så är de funkis :)
      else
        foreach (var item in currentWod.Rounds.SelectMany(u => u.ExerciseRounds))
        {

          var reference = item.ExerciseId;

          var exercise = new ExerciseStatistics
            {
              TimeStamp = DateTime.Now,
              ExerciseGroup = item.ExerciseName,             
              Reps = item.Reps*calcRep,
              Weight = item.Weight*(item.Reps*calcRep)
            };
          var exerciseList = new List<ExerciseStatistics> {exercise};

          if (stats.ExerciseStats.ContainsKey(reference))
            stats.ExerciseStats[reference].Add(exercise);

          else
            stats.ExerciseStats.Add(reference, exerciseList);
        }
      return stats;
    }

    private Stat WodStatistics(Result result, Stat stats) {
      var reference = result.WodId;
      var wodList = new List<WodStatistics>();
			var wod = new WodStatistics
			  {
			    TimeStamp = DateTime.Now,
			    ResultType = result.ResultType,
			    WodName = result.WodName,
			    Result = result.Value
			  };

      wodList.Add(wod);
	    if (stats.WodStats.ContainsKey(reference))
		    stats.WodStats[reference].Add(wod);
	    else
		    stats.WodStats.Add(reference, wodList);

      return stats;
    }
  }
}