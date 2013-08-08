using System.Collections.Generic;
using Easyfy.CHS.Model.Extension;
using Easyfy.CHS.Model.Projection;
using Raven.Abstractions.Data;

namespace Easyfy.CHS.Model.ViewModel
{
  public class WodSearchViewModel
  {
    public WodSearchViewModel() {
      WodList = new List<WodListProjection>();
      FacetResults = new FacetResults();
      CheckboxFilterList = new List<FacetFilter>();
    }
    public List<WodListProjection> WodList { get; set; }
    public FacetResults FacetResults { get; set; }
    public List<FacetFilter> CheckboxFilterList { get; set; }
  }

}