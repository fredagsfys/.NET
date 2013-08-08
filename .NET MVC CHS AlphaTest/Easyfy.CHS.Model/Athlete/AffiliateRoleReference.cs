using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easyfy.CHS.Model.Athlete
{
  public class AffiliateRoleReference : AffiliateReference {
    //REVIEW: Ändrat till string eftersom GymRoles innehåller ett strängvärde och inget mer
    public List<string> AffiliateRoles { get; set; }
  }

  public class AffiliateReference {
    public string Id { get; set; }
    public string Name { get; set; }
    public string FriendlyUrl { get; set; }
  }
}