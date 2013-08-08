using System.Linq;
using Easyfy.CHS.Data.Raven.Extensions;
using Easyfy.CHS.Model.Projection;
using Easyfy.CHS.Model.Wod;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Easyfy.CHS.Data.Raven.Indexes
{
  public class Wod_Search : AbstractIndexCreationTask<WodBase, WodListProjection>
  {

    public Wod_Search() {

      Map = wods => from wod in wods
        select new {
          WodId = wod.Id,
          wod.Name,
          wod.Description,
          WodType = wod.WodType.ToString(),
          BenchmarkType = wod.BenchmarkType.ToString(),
          wod.ExerciseSearchField,
          wod.ExerciseList,
          wod.RoundDescription
        };

      TransformResults = (result, wods) =>
                         from wod in wods
                         select new {
                           wod.WodId,
                           wod.Name,
                           wod.Description,
                           wod.WodType,
                           wod.BenchmarkType,
                           wod.ExerciseSearchField,
                           wod.ExerciseList,
                           wod.RoundDescription,
                           Score = MetadataFor(wod).Value<double>("Temp-Index-Score")
                         };

      Index(m => m.ExerciseList, FieldIndexing.Default);
      Index(m => m.Name, FieldIndexing.Analyzed);
      Index(m => m.WodType, FieldIndexing.Analyzed);
      Index(m=>m.BenchmarkType, FieldIndexing.Analyzed);
      Index(m => m.ExerciseSearchField, FieldIndexing.Analyzed);

      Analyze(m => m.ExerciseSearchField, "WhitespaceAnalyzer");
    }
  }
  
}