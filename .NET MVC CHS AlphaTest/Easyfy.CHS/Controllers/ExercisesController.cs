using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easyfy.CHS.Data.Raven;
using Easyfy.CHS.Filters;
using Easyfy.CHS.Helpers;
using Easyfy.CHS.Model.Exercise;
using Easyfy.CHS.ViewModels;

namespace Easyfy.CHS.Controllers
{
    public class ExercisesController : RavenController
    {
        public JsonResult AllExercises(string q, int limit)
        {
            var exercises = RavenSession.Query<ExerciseBase>().ToList(); //CacheHelper.Cacheify("exercise", "allexercises", 10, () => RavenSession.Query<ExerciseBase>().ToList());

            var metabolicList = (from e in exercises.ToList()
                                    where e.ExerciseType == ExerciseType.Metabolic
                                    select new ExerciseJsonResult
                                    {
                                    Name =
                                        e.Name + " " + e.Length + " " +
                                        e.LengthTypeToString,
                                    ExerciseType = e.ExerciseType.ToString(),
                                    ExerciseId = e.Id
                                    }).ToList();

            var exerciseList = (from e in exercises.ToList()
                                where e.ExerciseType != ExerciseType.Metabolic
                                select new ExerciseJsonResult
                                {
                                    Name = e.Name,
                                    ExerciseType = e.ExerciseType.ToString(),
                                    ExerciseId = e.Id
                                }).ToList();

            var merged = new List<ExerciseJsonResult>(metabolicList);
            merged.AddRange(exerciseList);

            List<ExerciseJsonResult> filtered = (from e in merged.ToList()
                                                where e.Name.ToLower().Contains(q.ToLower()) || e.ExerciseType.ToLower().Contains(q.ToLower())
                                                orderby e.Name
                                                select new ExerciseJsonResult
                                                {
                                                    Name = e.Name,
                                                    ExerciseType = e.ExerciseType,
                                                    ExerciseId = e.ExerciseId
                                                }).ToList();

            return this.Json(filtered.ToList(), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public string AddExercise(FormCollection items)
        {
            if (CurrentUser.Admin)
            {
                string result = "ok";
                try
                {
                    CreateExerciseInternal(items);
                }
                catch (Exception e)
                {
                    result = e.Message;
                }

                return result;
            }

            return null;
        }

        private void CreateExerciseInternal(FormCollection items)
        {
            var x = new ExerciseBase();
            x.Name = items["exerciseName"];

            switch (items["exerciseType"])
            {
                case "Metabolic":
                x.ExerciseType = ExerciseType.Metabolic;
                x.Length = double.Parse(items["exerciseLength"]);

                if (items["exerciseMetabolicUnit"] == "m") x.LengthType = LengthType.Meter;
                else if (items["exerciseMetabolicUnit"] == "mile") x.LengthType = LengthType.Mile;
                else if (items["exerciseMetabolicUnit"] == "foot") x.LengthType = LengthType.Foot;
                else if (items["exerciseMetabolicUnit"] == "km") x.LengthType = LengthType.KiloMeter;
                break;
                case "Gymnastic":
                x.ExerciseType = ExerciseType.Gymnastic;
                break;
                case "WeightLift":
                x.ExerciseType = ExerciseType.WeightLift;
                break;
                case "RestPeriod":
                x.ExerciseType = ExerciseType.RestPeriod;
                break;
            }

            RavenSession.Store(x);
            RavenSession.SaveChanges();
        }

        public ActionResult GetExercises()
        {
            var results = RavenSession.Query<ExerciseBase>().Customize(o=>o.WaitForNonStaleResultsAsOfNow()).ToList();
            return View("ExerciseListEdit", results);
        }

        public ActionResult RemoveExercise(string id)
        {
            if (CurrentUser.Admin)
            {
                try
                {
                    // Set default status
                    //TempData["Status"] = String.Format("<p class=\"error\"><span>An error occur when deleting!</span></p>");

                    // Delete reclamation
                    // Set status
                    //TempData["Status"] = String.Format("<p class=\"success\"><span>Success of deleting exercise!</span></p>");
                    var deleteExercise = RavenSession.Load<ExerciseBase>(
                    RavenSession.Advanced.DocumentStore.Conventions.FindFullDocumentKeyFromNonStringIdentifier(id,
                                                                                                                typeof(
                                                                                                                    ExerciseBase),
                                                                                                                false));
                    if (deleteExercise != null)
                    {
                        RavenSession.Delete(deleteExercise);
                    }

                    RavenSession.SaveChanges();

                    return RedirectToAction("Edit");
                }
                catch (Exception ex)
                {
                    // Return view
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                return RedirectToAction("Edit");
            }

            return RedirectToAction("Index", new { controller = "Home" });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public string UpdateExercise(string id, string name)
        {
            if (CurrentUser.Admin)
            {
                string result = "ok";

                if (String.IsNullOrEmpty(name))
                {
                    return "Don't forget the name";
                }

                try
                {
                    UpdateExerciseInternal(id, name, null, null);
                }
                catch (Exception e)
                {
                    result = e.Message;
                }

                return result;
            }

            return null;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public string UpdateMetabolicExercise(string id, string name, string length, string lengthunit)
        {
            if (CurrentUser.Admin)
            {
                string result = "ok";

                if (String.IsNullOrEmpty(name))
                {
                    return "Don't forget the name";
                }

                try
                {
                    UpdateExerciseInternal(id, name, length, lengthunit);
                }
                catch (Exception e)
                {
                    result = e.Message;
                }

                return result;
            }

            return null;
        }

        private void UpdateExerciseInternal(string id, string name, string length, string lengthUnit)
        {
            var x = RavenSession.Load<ExerciseBase>(RavenSession.Advanced.DocumentStore.Conventions.FindFullDocumentKeyFromNonStringIdentifier(id, typeof(ExerciseBase), false));
            x.Name = name;

            if (x.ExerciseType == ExerciseType.Metabolic)
            {
                x.Length = double.Parse(length);

                if (lengthUnit == "m") x.LengthType = LengthType.Meter;
                else if (lengthUnit == "mile") x.LengthType = LengthType.Mile;
                else if (lengthUnit == "foot") x.LengthType = LengthType.Foot;
                else if (lengthUnit == "km") x.LengthType = LengthType.KiloMeter;
            }

            RavenSession.SaveChanges();
        }

        public ActionResult Edit()
        {
            if (CurrentUser.Admin)
            {
                var model = new ExerciseViewModel();

                model.Exercises = RavenSession.Query<ExerciseBase>().ToList(); // CacheHelper.Cacheify("exercise", "allexercises", 10, () => RavenSession.Query<ExerciseBase>().ToList());

                return View("Edit", model);
            }

            return RedirectToAction("Index", new { controller = "Home" });
        }

        public ActionResult Index()
        {
            var model = new ExerciseViewModel();
            model.Exercises = RavenSession.Query<ExerciseBase>().ToList();

            return View(model);
        }
    }
}