using Raven.Abstractions;
using Raven.Database.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using Raven.Database.Linq.PrivateExtensions;
using Lucene.Net.Documents;
using System.Globalization;
using System.Text.RegularExpressions;
using Raven.Database.Indexing;


public class Index_Scheduled_2fView : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Scheduled_2fView()
	{
		this.ViewText = @"docs.ScheduleWods.Select(wod => new {
    Id = wod.__document_id,
    AffiliateId = wod.AffiliateId,
    Date = wod.Date.Date
})";
		this.ForEntityNames.Add("ScheduleWods");
		this.AddMapDefinition(docs => docs.Where(__document => string.Equals(__document["@metadata"]["Raven-Entity-Name"], "ScheduleWods", System.StringComparison.InvariantCultureIgnoreCase)).Select((Func<dynamic, dynamic>)(wod => new {
			Id = wod.__document_id,
			AffiliateId = wod.AffiliateId,
			Date = wod.Date.Date,
			__document_id = wod.__document_id
		})));
		this.AddField("Id");
		this.AddField("AffiliateId");
		this.AddField("Date");
		this.AddField("__document_id");
	}
}
