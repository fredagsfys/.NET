using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easyfy.CHS.Model.Affiliate;
using Easyfy.CHS.Model.Athlete;
using System.Web.Mvc;

namespace Easyfy.CHS.ViewModels
{
    public class AthleteAffiliateViewModel
    {
        public AthleteAffiliateViewModel()
        {
            AffiliateList = new List<Affiliate>();
            AthleteList = new List<Athlete>();
        }

        public Athlete Athlete { get; set; }
        public Affiliate Affiliate { get; set; }

        public List<Affiliate> AffiliateList { get; set; }
        public List<Athlete> AthleteList { get; set; }
        public List<ScheduledWod> ScheduledWod { get; set; }
    }
}