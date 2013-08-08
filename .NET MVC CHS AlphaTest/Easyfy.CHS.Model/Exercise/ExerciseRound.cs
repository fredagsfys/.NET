using System;
using Easyfy.CHS.Model.Wod;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Easyfy.CHS.Model.Exercise {
  public class ExerciseRound {
    public ExerciseRound() {}

    public ExerciseRound(ExerciseBase exercise) {
      ExerciseName = exercise.Name;
      IsWeightExercise = exercise.ExerciseType == ExerciseType.WeightLift;
      ExerciseType = exercise.ExerciseType;
      ExerciseId = exercise.Id;
      RepsType = RepsType.Fix;
      Reps = 0;
      Time = new TimeSpan(0);
      SortOrder = 0;
      Weight = 0;
      WeightUnit = WeightUnit.Pound;
      WeightType = WeightType.Fix;
      WeightExtra = false;
      Length = exercise.Length;
      LengthType = exercise.LengthType;
      IsBodyWeight = false;
      BodyWeightPercentage = 0;
      IsMax = false;
    }

    public string ExerciseName { get; set; }
    public string ExerciseId { get; set; }
    public ExerciseType ExerciseType { get; set; }
    public RepsType RepsType { get; set; }

    [DisplayName("Reps: ")]
    [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Must be atleast 1.")]
    public int Reps { get; set; }

    [DisplayName("Time:")]
    [Range(1.0, Double.MaxValue, ErrorMessage = "Must be atleast 1.")]
    public TimeSpan Time { get; set; }

    public int SortOrder { get; set; }
    public bool IsWeightExercise { get; set; }

    [DisplayName("Weight:")]
    [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Must be atleast 1.")]
    public double Weight { get; set; }

    public WeightUnit WeightUnit { get; set; }
    public WeightType WeightType { get; set; }
    public bool WeightExtra { get; set; }
    public double Length { get; set; }
    public LengthType LengthType { get; set; }
    public bool IsBodyWeight { get; set; }

    [DisplayName("Bodyweight:")]
    [Required(ErrorMessage = "Please enter your bodyweight.")]
    public int BodyWeightPercentage { get; set; }

    public bool IsMax { get; set; }




    [JsonIgnore]
    public string WeightTypeToString {
      get {
        if (this.WeightUnit == WeightUnit.Kilogram)
          return "kg";
        else if (this.WeightUnit == WeightUnit.Pound)
          return (this.Weight > 1 ? "pounds" : "pound");
        else
          return "";
      }
    }

    [JsonIgnore]
    public string LengthTypeToString {
      get {
        if (this.LengthType == LengthType.Meter)
          return "m";
        else if (this.LengthType == LengthType.KiloMeter)
          return "km";
        else if (this.LengthType == LengthType.Mile)
          return (this.Length > 1 ? "miles" : "mile");
        else if (this.LengthType == LengthType.Foot)
          return (this.Length > 1 ? "feet" : "foot");
        else
          return "";
      }
    }
  }
}
