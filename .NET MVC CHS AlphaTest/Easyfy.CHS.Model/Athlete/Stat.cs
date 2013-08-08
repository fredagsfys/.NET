using System;
using System.Collections.Generic;
using Easyfy.CHS.Model.Exercise;
using Easyfy.CHS.Model.Wod;

namespace Easyfy.CHS.Model.Athlete
{
    public class Stat
    {
      public Stat() {
	    WodStats = new Dictionary<string, List<WodStatistics>>();
        ExerciseStats = new Dictionary<string, List<ExerciseStatistics>>();
        EventStatistics = new Dictionary<string, List<EventStatistics>>();
      }
      public string UserReference { get; set; }
			public Dictionary<string, List<WodStatistics>> WodStats { get; set; } 
      public Dictionary<string, List<ExerciseStatistics>> ExerciseStats { get; set; }
      public Dictionary<string, List<EventStatistics>> EventStatistics { get; set; } 
    }

	public class WodStatisticsList : List<WodStatistics> {
		public string WodName { get; set; }
	}

  public class ExerciseStatistics {
    public DateTime TimeStamp { get; set; }
    public double Reps { get; set; }
    public double Weight { get; set; }
    public string ExerciseGroup { get; set; } //Slå ihop alla squats till squatgroup
  }

  public class EventStatistics {
    public DateTime TimeStamp { get; set; }
  }

  public class WodStatistics {
      public DateTime TimeStamp { get; set; }
      public double Result { get; set; }
      public ResultType ResultType { get; set; }
	    public string WodName { get; set; }
  }

  public class AthleteStatistics {
    public AthleteStatistics()
    {
      ExerciseTotalStats = new List<ExerciseTotalStatistics>();
      WodTotalStats = new List<WodTotalStatistics>();
    }
    public string UserReference { get; set; }
    public List<ExerciseTotalStatistics> ExerciseTotalStats { get; set; }
    public List<WodTotalStatistics> WodTotalStats { get; set; }
  }

  public class ExerciseTotalStatistics {
    public DateTime TimeStamp { get; set; }
    public double TotalReps { get; set; }
    public double TotalWeight { get; set; }
    public double UnbrokenReps { get; set; }
    public double MaxWeight { get; set; }
    public string ExerciseGroup { get; set; } //Slå ihop alla squats till squatgroup
    public ExerciseType ExerciseType { get; set; }
  }

  public class WodTotalStatistics {
    public string WodId { get; set; }
    public string WodName { get; set; } 
    public DateTime TimeStamp { get; set; }
    public double AggregatedResult { get; set; }
    public double BestResult { get; set; }
    public int Count { get; set; }
    public ResultType ResultType { get; set; }
    public WodType WodType { get; set; }
  }
}