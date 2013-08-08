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


public class Index_Auto_2fScheduledWods_2fByAffiliateId : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fScheduledWods_2fByAffiliateId()
	{
		this.ViewText = @"from doc in docs.ScheduledWods
select new { AffiliateId = doc.AffiliateId }";
		this.ForEntityNames.Add("ScheduledWods");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "ScheduledWods", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				AffiliateId = doc.AffiliateId,
				__document_id = doc.__document_id
			});
		this.AddField("AffiliateId");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("AffiliateId");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("AffiliateId");
		this.AddQueryParameterForReduce("__document_id");
	}
}
