using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Easyfy.CHS.Model.Affiliate;
using Easyfy.CHS.Model.Athlete;
using Easyfy.CHS.Model.Exercise;

namespace Easyfy.CHS.Model.Wod
{
  public class WodBase
  {
    private List<ExerciseInfo> _exerciseInfos;
    private List<string> _exerciseList;

    public WodBase()
    {
      AffiliateReference = new AffiliateReference();
    }

    public double Score { get; set; }

    public AffiliateReference AffiliateReference { get; set; }

    public string Id { get; set; }

    public WodType WodType { get; set; }

    [DisplayName("Benchmark: ")]
    [Required(ErrorMessage = "Please choose a benchmark type.")]
    public BenchmarkType BenchmarkType { get; set; }

    [DisplayName("Name: ")]
    [Required(ErrorMessage = "Please enter a name for your WOD.")]
    public string Name { get; set; }

    [DisplayName("Description: ")]
    public string Description { get; set; }

    [DisplayName("Time:")]
    [Required(ErrorMessage = "Please enter the time.")]
    [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Must be atleast 1.")]
    public TimeSpan? Time { get; set; }

    public ResultAggregation DefaultResultAggregation { get; set; }

    [DisplayName("Result type: ")]
    [Required(ErrorMessage = "Please choose a result type.")]
    public ResultType DefaultResultType { get; set; }

    [DisplayName("Lower is better")]
    public Boolean LowerIsBetter { get; set; }

    [DisplayName("Round: ")]
    [Required(ErrorMessage = "You must have aleast one round.")]
    public List<Round> Rounds { get; set; }

    public WeightUnit WeightUnit { get; set; }

    public string HashCode { get; set; }

    public AuditInformation AuditInfo { get; set; }

    public List<string> ExerciseList
    {
      get { return GetAllExercies(); }
      set { _exerciseList = value; }
    }

    public List<string> ExerciseSearchField
    {
      get { return GetAllExercies(true); }
    }

    public string RoundDescription {
      get { return this.ToString(); }
    }

    // Override ToString to render all exercices.
    // TODO maby fix so description and title also will be include in this method.
    public override string ToString() {
      var result = new StringBuilder();

      foreach (var item in GetExercises()) {
        if (!string.IsNullOrEmpty(item.LapInfo))
          result.Append(item.LapInfo + "<br />");
        foreach (var e in item.ExerciseInfoList) {
          result.Append(e + "<br />");
        }
      }

      return result.ToString();
    }

    public string GetTitleString()
    {
      var result = new StringBuilder();
      result.Append(!String.IsNullOrWhiteSpace(Name) ? Name : "WOD");

      switch (WodType)
      {
        case WodType.MinuteWod:
          result.Append(" (for reps, minute wod)");
          break;
        case WodType.RunningWod:
          result.Append(" (for reps, running wod)");
          break;
        case WodType.MaxWod:
          result.Append(" (for maximum weight)");
          break;
        case WodType.RepsWod:
          result.Append(" (for reps)");
          break;
        case WodType.TimeWod:
        case WodType.TabataWod:
          result.Append(" (for time)");
          break;
        case WodType.RestDay:
          break;
        default:
          break;
      }

      return result.ToString();
    }

    public string GetResultInformation()
    {
      var result = new StringBuilder();
      result.Append("Result: ");

      switch (DefaultResultAggregation)
      {
        case ResultAggregation.Sum:
          result.Append("Report the total ");
          break;
        case ResultAggregation.Min:
          result.Append("Report the least ");
          break;
        case ResultAggregation.Max:
          result.Append("Report the top ");
          break;
        case ResultAggregation.None:
          result.Append("Report the total ");
          break;

      }

      switch (DefaultResultType)
      {
        case ResultType.Reps:
          result.Append("no of reps");
          break;
        case ResultType.Time:
          result.Append("time spent");
          break;
        case ResultType.Weight:
          result.Append("weight used");
          break;
      }

      if (WodType == WodType.RestDay)
      {
        result = new StringBuilder();
      }

      return result.ToString();
    }

    public string GetHeroDescription()
    {
      return BenchmarkType != BenchmarkType.NotBenchMark ? BenchmarkType.ToString() : "";
    }

    public List<ExerciseInfo> GetExercises()
    {
      if (_exerciseInfos != null)
        return _exerciseInfos;

      bool include = false;
      var exercises = new List<ExerciseInfo>();

      if (WodType == WodType.RestDay)
        return exercises;

      foreach (var round in Rounds)
      {
        var info = new ExerciseInfo();
        //1. more than one round? then include laps
        if (Rounds.Count > 1 || (Rounds.Count == 1 && Rounds[0].Laps > 1 && Rounds[0].ExerciseRounds.Count > 1)) include = true;

        //2. if one round - but more than one lap - also include laps
        if (include) info.LapInfo = String.Format((round.Laps > 1 ? "{0} laps of:" : "{0} lap of:"), round.Laps);
        List<ExerciseRound> exerciseRounds = round.ExerciseRounds.OrderBy(x => x.SortOrder).ToList();
        foreach (var exerciseRound in exerciseRounds)
        {
          var name = exerciseRound.ExerciseName;
          string exercise = String.Empty;

          switch (exerciseRound.ExerciseType)
          {
            case ExerciseType.Metabolic:
              exercise = String.Format("{0}{1}{2}{3}"
                                       , AddTimePart(exerciseRound)
                                       , (exerciseRound.IsMax) ? "Max speed " : ""
                                       , exerciseRound.Length+ " " +exerciseRound.LengthTypeToString+ " "+ exerciseRound.ExerciseName
                                       , AddExtraWeightPart(exerciseRound));
              break;
            case ExerciseType.Gymnastic:
              exercise = String.Format("{0}{1}{2}{3}"
                                       , AddTimePart(exerciseRound)
                                       , AddRepsPart(exerciseRound, false, 0)
                                       , name
                                       , AddExtraWeightPart(exerciseRound));
              break;
            case ExerciseType.WeightLift:
              exercise = AddTimePart(exerciseRound) + AddRepsPart(exerciseRound, (Rounds.Count == 1 && Rounds[0].Laps > 1 && Rounds[0].ExerciseRounds.Count == 1), ((Rounds.Count == 1 && Rounds[0].Laps > 1 && Rounds[0].ExerciseRounds.Count == 1) ? Rounds[0].Laps : 0)) + AddWeightPart(exerciseRound) + name;
              break;
            case ExerciseType.RestPeriod:
              exercise = name + " " + exerciseRound.Time.Seconds + " seconds";
              break;
          }
          info.ExerciseInfoList.Add(exercise.Trim());
        }

        exercises.Add(info);
      }

      _exerciseInfos = exercises;
      return exercises;
    }

    private string AddRepsPart(ExerciseRound exerciseRound, bool onlyOneExerciseAndMoreLaps, int? laps)
    {
      if (WodType == WodType.MinuteWod)
        return string.Empty;

      if (exerciseRound.Reps == 0)
      {
        if (exerciseRound.IsMax)
          return "Max reps ";
        else
          return String.Empty;
      }

      if (onlyOneExerciseAndMoreLaps)
      {
        var result = new StringBuilder();
        for (int i = 0; i < laps; i++)
        {
          result.Append("1");
          if (i < laps - 1)
            result.Append("-");
        }
        return result.ToString() + " - ";
      }

      return String.Format((exerciseRound.Reps > 1 ? "{0} reps - " : "{0} rep - "), exerciseRound.Reps);
    }

    private string AddTimePart(ExerciseRound round)
    {
      if (round.Time.Ticks > 0) return String.Format("During {0}: ", round.Time);
      return "";
    }

    private string AddWeightPart(ExerciseRound round)
    {
      string weightPart = String.Empty;
      string weightunit = round.WeightTypeToString;
      if (round.IsBodyWeight)
      {
        weightPart =
          (round.BodyWeightPercentage == 100
             ? ", bodyweight "
             : String.Format(" {0:P0} bodyweight ", ((double)round.BodyWeightPercentage / 100)));
      }
      else if (round.IsMax && round.Weight < 1) //      else if (round.IsMax && round.Reps == 1 && round.Weight < 1)
      {
        weightPart = " Max weight ";
      }
      else if (round.IsMax && round.Weight > 0)
      {
        weightPart = String.Format("{0} {1} ", round.Weight, weightunit);
      }
      else
      {
        weightPart = String.Format("{0} {1} ", round.Weight, weightunit);
      }

      return weightPart;
    }

    private string AddExtraWeightPart(ExerciseRound round)
    {
      if (round.IsWeightExercise && round.Weight > 0)
      {
        string weightunit = round.WeightTypeToString;
        return String.Format(", {0} {1} extra weight", round.Weight, weightunit);
      }
      return "";
    }

    public List<string> GetAllExercies(bool toLower = false) {
      var list = new List<string>();
      foreach (var round in Rounds) {
        foreach (var exRound in round.ExerciseRounds) {
          string addName = string.Empty;
          // Metabolic exercise
          if (exRound.ExerciseType == ExerciseType.Metabolic)
            addName = exRound.Length + " " + exRound.LengthTypeToString + " " + exRound.ExerciseName;
          else { // Other exercise
            addName = exRound.ExerciseName;
          }

          if (toLower)
            addName = addName.ToLower();
          // Only add if not allready in list
          if (list.All(o => o != addName)) {
            list.Add(addName);
          }
          
        } 
      }

      return list;

    }

    public bool HasExercise(string exerciseId)
    {
      return Rounds.SelectMany(o => o.ExerciseRounds).Select(o => o.ExerciseId).Any(o => o == exerciseId);
    }

    public List<string> GetDistinctExercises()
    {
      return Rounds.SelectMany(o => o.ExerciseRounds).Select(o => o.ExerciseId).Distinct().ToList();
    }

    public ExerciseRound GetExerciseRound(string exerciseId)
    {
      return Rounds.SelectMany(o => o.ExerciseRounds).FirstOrDefault(o => o.ExerciseId == exerciseId);
    }

    public int GetTotalNumberOfRepsForExercise(string exerciseId)
    {
      return Rounds.Aggregate(0, (current, round) => current + (round.Laps * round.ExerciseRounds.Where(o => o.ExerciseId == exerciseId).Sum(o => o.Reps)));
    }

    public int GetTotalWeightForExercise(string exerciseId)
    {
      return Rounds.Aggregate(0, (current, round) => current + (round.Laps * round.ExerciseRounds.Where(o => o.ExerciseId == exerciseId).Sum(o => o.Reps * (int)o.Weight)));
    }

    public Round Blubb() {

      var newRound = new Round();
      foreach (var distinctExercise in GetDistinctExercises())
      {
        var round = GetExerciseRound(distinctExercise);
        var er = new ExerciseRound
          {
            Reps = GetTotalNumberOfRepsForExercise(distinctExercise),
            ExerciseId = round.ExerciseId,
            Weight = GetTotalWeightForExercise(distinctExercise),
            ExerciseName = round.ExerciseName
          };
        newRound.ExerciseRounds.Add(er);
      }
      return newRound;
    }

  }
}
