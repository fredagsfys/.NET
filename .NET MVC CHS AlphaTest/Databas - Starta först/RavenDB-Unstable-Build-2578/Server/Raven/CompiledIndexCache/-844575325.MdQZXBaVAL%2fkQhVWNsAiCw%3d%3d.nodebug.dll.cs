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


public class Index_Auto_2fAthletes_2fByAffiliates_FriendlyUrlAndOAuthAccounts_ProviderAndOAuthAccounts_ProviderUserId : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fAthletes_2fByAffiliates_FriendlyUrlAndOAuthAccounts_ProviderAndOAuthAccounts_ProviderUserId()
	{
		this.ViewText = @"from doc in docs.Athletes
from docAffiliatesItem in ((IEnumerable<dynamic>)doc.Affiliates).DefaultIfEmpty()
select new { Affiliates_FriendlyUrl = docAffiliatesItem.FriendlyUrl, OAuthAccounts_ProviderUserId = doc.OAuthAccounts_ProviderUserId, OAuthAccounts_Provider = doc.OAuthAccounts_Provider }";
		this.ForEntityNames.Add("Athletes");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "Athletes", System.StringComparison.InvariantCultureIgnoreCase)
			from docAffiliatesItem in ((IEnumerable<dynamic>)doc.Affiliates).DefaultIfEmpty()
			select new {
				Affiliates_FriendlyUrl = docAffiliatesItem.FriendlyUrl,
				OAuthAccounts_ProviderUserId = doc.OAuthAccounts_ProviderUserId,
				OAuthAccounts_Provider = doc.OAuthAccounts_Provider,
				__document_id = doc.__document_id
			});
		this.AddField("Affiliates_FriendlyUrl");
		this.AddField("OAuthAccounts_ProviderUserId");
		this.AddField("OAuthAccounts_Provider");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("FriendlyUrl");
		this.AddQueryParameterForMap("OAuthAccounts_ProviderUserId");
		this.AddQueryParameterForMap("OAuthAccounts_Provider");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("FriendlyUrl");
		this.AddQueryParameterForReduce("OAuthAccounts_ProviderUserId");
		this.AddQueryParameterForReduce("OAuthAccounts_Provider");
		this.AddQueryParameterForReduce("__document_id");
	}
}
