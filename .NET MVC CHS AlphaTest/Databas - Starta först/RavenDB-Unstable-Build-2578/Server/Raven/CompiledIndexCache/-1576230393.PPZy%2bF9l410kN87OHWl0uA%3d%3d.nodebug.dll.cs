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


public class Index_Auto_2fAffiliates_2fByFriendlyUrl : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fAffiliates_2fByFriendlyUrl()
	{
		this.ViewText = @"from doc in docs.Affiliates
select new { FriendlyUrl = doc.FriendlyUrl }";
		this.ForEntityNames.Add("Affiliates");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "Affiliates", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				FriendlyUrl = doc.FriendlyUrl,
				__document_id = doc.__document_id
			});
		this.AddField("FriendlyUrl");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("FriendlyUrl");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("FriendlyUrl");
		this.AddQueryParameterForReduce("__document_id");
	}
}
