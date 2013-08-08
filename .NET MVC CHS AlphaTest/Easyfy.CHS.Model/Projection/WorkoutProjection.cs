using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Easyfy.CHS.Model.Athlete;

namespace Easyfy.CHS.Model.Projection
{
  //public class WorkoutProjection
  //{
  //  public WorkoutProjection()
  //  {
  //    Comments = new List<Comment>();
  //    Results = new List<Result>();
  //  }

  //  public DateTime Date { get; set; }
  //  public List<Result> Results { get; set; }
  //  public List<Comment> Comments { get; set; }
  //  public string AthleteId { get; set; }
  //  public string AthleteName { get; set; }
  //  public string AthleteImageUrl { get; set; }
  //}

  public class WorkoutResultProjection
  {
    public WorkoutResultProjection()
    {
      Comments = new List<Comment>();
      Results = new List<Result>();
    }
    public string WorkoutResultId { get; set; }
    public DateTime Date { get; set; }
    public List<Result> Results { get; set; }
    public List<Comment> Comments { get; set; }
    public string AthleteId { get; set; }
    public string AthleteName { get; set; }
    public string AthleteImageUrl { get; set; }
  }
}
