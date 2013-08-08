using System.Linq;
using Easyfy.CHS.Model.Athlete;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Easyfy.CHS.Data.Raven.Indexes
{
  public class Athlete_Search : AbstractIndexCreationTask<Athlete>
  {

    public Athlete_Search()
    {

      Map = athletes => from athlete in athletes
                        select new
                        {
                          athlete.FirstName,
                          athlete.LastName,
                          athlete.Username,
                          athlete.City,
                          athlete.Country,
                          athlete.AffiliateSearchField
                        };

      Index(m => m.AffiliateSearchField, FieldIndexing.Analyzed);
    }
  }
}