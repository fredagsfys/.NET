using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Easyfy.CHS.Model.Affiliate {
  public class ScheduledWod {
    public ScheduledWod() {
      Date = DateTime.Now;
      WodList = new List<WorkoutWod>();
    }

    public string Id { get; set; }

    [Required(ErrorMessage = "Required")]
    //[RegularExpression(@"^([0-9]{4}-[0-9]{2}-[0-9]{2})$", ErrorMessage = "Wrong format on date.")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Required")]
    public string Description { get; set; }

    //Affiliate
    public string AffiliateId { get; set; }
    public string AffiliateUrl { get; set; }

    public List<WorkoutWod> WodList { get; set; }
    [Required(ErrorMessage = "Required")]
    public string WodName { get; set; }
  }

  public class WorkoutWod
  {
    public string WodId { get; set; }
    public string WodName { get; set; }
    public string WodTitleDescription { get; set; }
    public string WodDescription { get; set; }
    public string WodResultDescription { get; set; }
    public string WodTimeDescription { get; set; }
    public string WodHeroesDescription { get; set; }
    public List<ExerciseInfo> WodExerciseInfo { get; set; }
  }
}
