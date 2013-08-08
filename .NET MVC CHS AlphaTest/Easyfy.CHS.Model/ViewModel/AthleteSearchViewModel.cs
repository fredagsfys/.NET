using System.Collections.Generic;
using Easyfy.CHS.Model.Extension;
using Easyfy.CHS.Model.Projection;
using Raven.Abstractions.Data;

namespace Easyfy.CHS.Model.ViewModel
{
  public class AthleteSearchViewModel
  {
    public AthleteSearchViewModel() {
      AthleteList = new List<AthleteListProjection>();
      FacetResults = new FacetResults();
      CheckboxFilterList = new List<FacetFilter>();
    }

    public List<AthleteListProjection> AthleteList { get; set; }
    public FacetResults FacetResults { get; set; }
    public List<FacetFilter> CheckboxFilterList { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int Page { get; set; }
  }
}
