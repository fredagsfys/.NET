using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Easyfy.CHS.Model.Wod;

namespace Easyfy.CHS.Model.Athlete {
  public class Result {

    public string WodId { get; set; }
    public string WodName { get; set; }

    public DateTime Date { get; set; }

    // SchedueledWodId is needed for Affiliate result list.
    public string ScheduledWodId { get; set; }
    public ResultType ResultType { get; set; }

    public double UnbrokenReps { get; set; }

    [Required]
    [DisplayName("Result:")]
    public double Value { get; set; }

    [DisplayName("Check if you performed the WOD as recommended:")]
    public bool Recommended { get; set; }

    public string Thought { get; set; }


  }
}