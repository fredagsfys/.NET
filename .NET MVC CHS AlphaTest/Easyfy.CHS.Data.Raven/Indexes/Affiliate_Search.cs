using System.Linq;
using Easyfy.CHS.Data.Raven.Extensions;
using Easyfy.CHS.Model.Affiliate;
using Easyfy.CHS.Model.Projection;
using Raven.Client.Indexes;

namespace Easyfy.CHS.Data.Raven.Indexes
{
  public class Affiliate_Search : AbstractIndexCreationTask<Affiliate, AffiliateListProjection>
  {
    public Affiliate_Search()
    {
      Map = affiliates => from affiliate in affiliates
                          select new
                          {
                            affiliate.Name,
                            affiliate.City,
                            affiliate.Country
                          };

    }
  }
}