using System;
using System.Collections.Generic;

namespace Easyfy.CHS.Model.Athlete {
  public class Goal {
    //Vikt - träna 5ggr i veckan
    public List<OtherGoal> OtherGoals { get; set; }

    //Woddar - tid eller reps
    public List<WodGoal> WodGoals { get; set; }

    //Övningar - unbroken, vikt, 3'a 100 kg i knäböj, 1'a 40 kg i pull-ups, springa 400m under 1 minut
    public List<ExerciseGoal> ExerciseGoals { get; set; }

  }

  public class OtherGoal {
    public double Goal { get; set; }
    public OtherGoalType OtherGoalType { get; set; }
    public DateTime Created { get; set; }
    public DateTime TimeGoalEnd { get; set; }
    public DateTime? GoalSucceeded { get; set; }
  }

  public enum OtherGoalType {
    Weight,
    WeeklyTraining,
    Measurement
  }

  public class ExerciseGoal {
    public string ExerciseReference { get; set; }
    public double Reps { get; set; }
    public double Goal { get; set; }
    public DateTime Created { get; set; }
    public DateTime TimeGoalEnd { get; set; }
    public DateTime? GoalSucceeded { get; set; }
    public ExerciseGoalType ExerciseGoalType { get; set; }
  }

  public enum ExerciseGoalType {
    Unbroken,
    MaxWeight,
    Time
  }

  public class WodGoal {
    public string WodReference { get; set; }
    public bool AsRxd { get; set; }
    public double Goal { get; set; }
    public DateTime Created { get; set; }
    public DateTime TimeGoalEnd { get; set; }
    public DateTime? GoalSucceeded { get; set; }
  }
}