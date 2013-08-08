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


public class Index_Auto_2fschedulewods_2fByAffiliateIdAndAffiliateUrlAndDateSortByDate : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fschedulewods_2fByAffiliateIdAndAffiliateUrlAndDateSortByDate()
	{
		this.ViewText = @"from doc in docs.schedulewods
select new { AffiliateUrl = doc.AffiliateUrl, Date = doc.Date, AffiliateId = doc.AffiliateId }";
		this.ForEntityNames.Add("schedulewods");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "schedulewods", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				AffiliateUrl = doc.AffiliateUrl,
				Date = doc.Date,
				AffiliateId = doc.AffiliateId,
				__document_id = doc.__document_id
			});
		this.AddField("AffiliateUrl");
		this.AddField("Date");
		this.AddField("AffiliateId");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("AffiliateUrl");
		this.AddQueryParameterForMap("Date");
		this.AddQueryParameterForMap("AffiliateId");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("AffiliateUrl");
		this.AddQueryParameterForReduce("Date");
		this.AddQueryParameterForReduce("AffiliateId");
		this.AddQueryParameterForReduce("__document_id");
	}
}
