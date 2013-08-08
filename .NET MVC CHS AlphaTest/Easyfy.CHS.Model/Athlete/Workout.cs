using System;
using System.Collections.Generic;

namespace Easyfy.CHS.Model.Athlete
{
  public class WorkoutResult {
    public WorkoutResult() {
      Results = new List<Result>();
    }
    public string Id { get; set; }
    public string AthleteId { get; set; }
    public DateTime Date { get; set; }

    public List<Result> Results { get; set; }
  }
}
