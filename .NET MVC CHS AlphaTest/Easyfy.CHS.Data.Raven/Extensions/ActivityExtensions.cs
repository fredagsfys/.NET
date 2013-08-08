using System.Collections.Generic;
using System.Linq;
using Easyfy.CHS.Data.Raven.Facets;
using Easyfy.CHS.Data.Raven.Indexes;
using Easyfy.CHS.Model.Affiliate;
using Easyfy.CHS.Model.Athlete;
using Easyfy.CHS.Model.Projection;
using Easyfy.CHS.Model.ViewModel;
using Easyfy.CHS.Model.Wod;
using Raven.Client;
using Raven.Client.Linq;

namespace Easyfy.CHS.Data.Raven.Extensions
{
  public static class ActivityExtensions
  {
    public static List<WorkoutResultProjection> GetWorkoutsForWall(this IDocumentSession session, Athlete currentUser) {

      // Add me and all I follow to this list
      var list = new List<string> {currentUser.Id};
      list.AddRange(currentUser.Follows.Select(follow => follow.Id));

      var model = session.Query<WorkoutResult, Workouts_ForUser>()
        .Where(o=>o.AthleteId.In(list))
        .OrderByDescending(o => o.Date)
        .AsProjection<WorkoutResultProjection>()
        .Take(50)
        .OrderByDescending(x => x.Date)
        .ToList();

      return model;
    }


    //public static List<WorkoutResultProjection> GetWorkoutsForWall(this IDocumentSession session, Athlete currentUser)
    //{

    //   Add me and all I follow to this list
    //  var list = new List<string> { currentUser.Id };
    //  list.AddRange(currentUser.Follows.Select(follow => follow.Id));

    //  var model = session.Query<Workout, Workouts_ForUser>()
    //    .Where(o => o.AthleteId.In(list))
    //    .AsProjection<WorkoutProjection>()
    //                 .OrderByDescending(o => o.Date)
    //                 .Take(50)
    //                 .ToList();

    //  return model;
    //}

   
  }
}