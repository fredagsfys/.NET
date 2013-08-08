using System.Collections.Generic;
using Easyfy.CHS.Model.Projection;

namespace Easyfy.CHS.Model.ViewModel {
  public class ActivityViewModel {
    public ActivityViewModel() {
      Activity = new List<WorkoutResultProjection>();
    }

    public string Id { get; set; }
    public List<WorkoutResultProjection> Activity { get; set; }
  }
}
