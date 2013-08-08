using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easyfy.CHS.Model.Projection
{
  public class WodListProjection
  {
    public string WodId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string WodType { get; set; }
    public string BenchmarkType { get; set; }
    public double Score { get; set; }
    public List<string> ExerciseList { get; set; }
    public List<string> ExerciseSearchField { get; set; }
    public string RoundDescription { get; set; }
    public override string ToString()
    {
      return RoundDescription;
    }

  }

  public class AthleteListProjection
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string AffiliateSearchField { get; set; }
  }

  public class AffiliateListProjection
  {
    public string Id { get; set; }
    public string FriendlyUrl { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string Postal { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
  }
}
