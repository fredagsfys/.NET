using System.Collections.Generic;
using Easyfy.CHS.Data.Raven.Extensions;
using Easyfy.CHS.Model.Projection;
using Raven.Abstractions.Data;

namespace Easyfy.CHS.Data.Raven.Facets
{
  public class WodFacets
  {
    public WodFacets() {
      FacetKey = "Facets/WodFacets";
      FacetList = new List<Facet> {
        new Facet<WodListProjection> { Name = o => o.BenchmarkType },
        new Facet<WodListProjection> { Name = o => o.WodType},
        new Facet<WodListProjection> { Name = o => o.ExerciseList }
      };
    }

    public string FacetKey { get; set; }
    public List<Facet> FacetList { get; set; }
  }
}