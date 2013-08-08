using System.Collections.Generic;
using System.Web.Mvc;
using Easyfy.CHS.Model.Athlete;
using Easyfy.CHS.Model.Wod;

namespace Easyfy.CHS.ViewModels {
  public class WodViewModel {
    public WodViewModel() {
      WodBase = new WodBase();
    }

    public WodBase WodBase { get; set; }
    public Result Result { get; set; }

    public string AffiliateId { get; set; }
    public string AffiliateFriendlyUrl { get; set; }
    public string AffiliateName { get; set; }

    public SelectList WODTypeValue {
      get {
        var options = new Dictionary<WodType, string> {
          {WodType.RestDay, "Rest Day"},
          {WodType.RepsWod, "Reps WOD"},
          {WodType.NotForTimeWod, "Not for time"},
          {WodType.MinuteWod, "Minute WOD"},
          {WodType.RunningWod, "Running WOD"},
          {WodType.TabataWod, "Tabata WOD"},
          {WodType.TimeWod, "Time WOD"},
        };
        return new SelectList(options, "Key", "Value");
      }
    }

    public SelectList BenchmarkTypeValue {
      get {
        var options = new Dictionary<BenchmarkType, string> {
          {BenchmarkType.NotBenchMark, "Not bench mark"},
          {BenchmarkType.Heroes, "Heroes"},
          {BenchmarkType.Miscellaneous, "Miscellaneous"},
          {BenchmarkType.Girls, "Girls"},
        };
        return new SelectList(options, "Key", "Value");
      }
    }

    public SelectList ResultAggregationValue {
      get {
        var options = new Dictionary<ResultAggregation, string> {
          {ResultAggregation.None, "None"},
          {ResultAggregation.Min, "Min"},
          {ResultAggregation.Max, "Max"},
          {ResultAggregation.Sum, "Sum"},
        };
        return new SelectList(options, "Key", "Value");
      }
    }

    public SelectList ResultTypeValue {
      get {
        var options = new Dictionary<ResultType, string> {
          {ResultType.Reps, "Reps"},
          {ResultType.Time, "Time"},
          {ResultType.Weight, "Weight"}
        };
        return new SelectList(options, "Key", "Value");
      }
    }

    public SelectList WeightUnitValue {
      get {
        var options = new Dictionary<WeightUnit, string> {
          {WeightUnit.Gram, "Gram"},
          {WeightUnit.Hektogram, "Hektogram"},
          {WeightUnit.Kilogram, "Kilogram"},
          {WeightUnit.Ounce, "Ounce"},
          {WeightUnit.Pound, "Pound"},
          {WeightUnit.Stone, "Stone"},
          {WeightUnit.Tonne, "Tonne"}

        };
        return new SelectList(options, "Key", "Value");
      }
    }
  }
}