using System.Collections.Generic;
using Easyfy.CHS.Model.Projection;
using Raven.Abstractions.Data;

namespace Easyfy.CHS.Data.Raven.Facets
{
  public class AffiliateFacets
  {
    public AffiliateFacets() {
      FacetKey = "Facets/AffiliateFacets";
      FacetList = new List<Facet> {
        new Facet<AffiliateListProjection> { Name = o => o.City },
        new Facet<AffiliateListProjection> { Name = o => o.Country }
      };
    }

    public string FacetKey { get; set; }
    public List<Facet> FacetList { get; set; }
  }
}