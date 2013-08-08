using System.Collections.Generic;
using Easyfy.CHS.Model.Projection;
using Raven.Abstractions.Data;

namespace Easyfy.CHS.Data.Raven.Facets
{
  public class AthleteFacets
  {
    public AthleteFacets() {
      FacetKey = "Facets/AthleteFacets";
      FacetList = new List<Facet> {
        new Facet<AthleteListProjection> { Name = o => o.City },
        new Facet<AthleteListProjection> { Name = o => o.Country },
      };
    }

    public string FacetKey { get; set; }
    public List<Facet> FacetList { get; set; }
  }
}