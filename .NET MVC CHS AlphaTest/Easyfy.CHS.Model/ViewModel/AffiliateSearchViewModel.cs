using System.Collections.Generic;
using Easyfy.CHS.Model.Extension;
using Easyfy.CHS.Model.Projection;
using Raven.Abstractions.Data;

namespace Easyfy.CHS.Model.ViewModel
{
  public class AffiliateSearchViewModel
  {
    public AffiliateSearchViewModel() {
      AffiliateList = new List<AffiliateListProjection>();
      FacetResults = new FacetResults();
      CheckboxFilterList = new List<FacetFilter>();
    }

    public List<AffiliateListProjection> AffiliateList { get; set; }
    public FacetResults FacetResults { get; set; }
    public List<FacetFilter> CheckboxFilterList { get; set; }
  }
}
