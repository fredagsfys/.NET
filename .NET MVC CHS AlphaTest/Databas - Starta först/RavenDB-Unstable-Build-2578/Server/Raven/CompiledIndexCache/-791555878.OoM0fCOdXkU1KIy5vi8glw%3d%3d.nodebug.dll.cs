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


public class Index_Auto_2fstats_2fByUserReference : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fstats_2fByUserReference()
	{
		this.ViewText = @"from doc in docs.stats
select new { UserReference = doc.UserReference }";
		this.ForEntityNames.Add("stats");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "stats", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				UserReference = doc.UserReference,
				__document_id = doc.__document_id
			});
		this.AddField("UserReference");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("UserReference");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("UserReference");
		this.AddQueryParameterForReduce("__document_id");
	}
}
