using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easyfy.CHS.Model.Athlete;
using Easyfy.CHS.Model.Projection;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Easyfy.CHS.Data.Raven.Indexes {
  public class Workouts_ForUser : AbstractIndexCreationTask<WorkoutResult, WorkoutResultProjection>
  {
    public Workouts_ForUser()
    {
      Map = workouts => from workout in workouts
                        select new
                        {
                          AthleteName = String.Format("{0} {1}", LoadDocument<Athlete>(workout.AthleteId).FirstName, LoadDocument<Athlete>(workout.AthleteId).LastName),
                          workout.AthleteId,
                          workout.Date,
                          workout.Results,
                          WorkoutResultId = workout.Id,
                          AthleteImageUrl = LoadDocument<Athlete>(workout.AthleteId).WallImageUrl
                        };

      Store(o => o.AthleteName, FieldStorage.Yes);
      Store(o => o.AthleteImageUrl, FieldStorage.Yes);
      Store(o => o.WorkoutResultId, FieldStorage.Yes);
      Index(m => m.AthleteId, FieldIndexing.NotAnalyzed);
    }

    //public class Workouts_ForUser : AbstractIndexCreationTask<Workout, WorkoutProjection> {
  //  public Workouts_ForUser() {
  //    Map = workouts => from workout in workouts
  //      from result in workout.WorkoutList
  //      select new {
  //        AthleteName= String.Format("{0} {1}", LoadDocument<Athlete>(workout.AthleteId).FirstName, LoadDocument<Athlete>(workout.AthleteId).LastName ) ,
  //        workout.AthleteId,
  //        result.Date,
  //        result.Results,
  //        result.Comments,
  //        AthleteImageUrl = LoadDocument<Athlete>(workout.AthleteId).WallImageUrl
  //      };

  //    Store(o=>o.AthleteName, FieldStorage.Yes);
  //    Store(o=>o.AthleteImageUrl, FieldStorage.Yes);
  //    Store(o=>o.Date, FieldStorage.Yes);
  //    Index(m => m.AthleteId, FieldIndexing.NotAnalyzed);
  //  }


  }

}