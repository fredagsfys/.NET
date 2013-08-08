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


public class Index_Auto_2fschedulewods_2fByAffiliateIdAndAffiliateUrlAndDateAndDate_DateSortByDate : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fschedulewods_2fByAffiliateIdAndAffiliateUrlAndDateAndDate_DateSortByDate()
	{
		this.ViewText = @"from doc in docs.schedulewods
select new { AffiliateId = doc.AffiliateId, Date_Date = doc.Date.Date, AffiliateUrl = doc.AffiliateUrl, Date = doc.Date }";
		this.ForEntityNames.Add("schedulewods");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "schedulewods", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				AffiliateId = doc.AffiliateId,
				Date_Date = doc.Date.Date,
				AffiliateUrl = doc.AffiliateUrl,
				Date = doc.Date,
				__document_id = doc.__document_id
			});
		this.AddField("AffiliateId");
		this.AddField("Date_Date");
		this.AddField("AffiliateUrl");
		this.AddField("Date");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("AffiliateId");
		this.AddQueryParameterForMap("Date.Date");
		this.AddQueryParameterForMap("AffiliateUrl");
		this.AddQueryParameterForMap("Date");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("AffiliateId");
		this.AddQueryParameterForReduce("Date.Date");
		this.AddQueryParameterForReduce("AffiliateUrl");
		this.AddQueryParameterForReduce("Date");
		this.AddQueryParameterForReduce("__document_id");
	}
}
