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


public class Index_Auto_2fAthletes_2fByOAuthAccounts_ProviderAndOAuthAccounts_ProviderUserId : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fAthletes_2fByOAuthAccounts_ProviderAndOAuthAccounts_ProviderUserId()
	{
		this.ViewText = @"from doc in docs.Athletes
from docOAuthAccountsItem in ((IEnumerable<dynamic>)doc.OAuthAccounts).DefaultIfEmpty()
select new { OAuthAccounts_ProviderUserId = docOAuthAccountsItem.ProviderUserId, OAuthAccounts_Provider = docOAuthAccountsItem.Provider }";
		this.ForEntityNames.Add("Athletes");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "Athletes", System.StringComparison.InvariantCultureIgnoreCase)
			from docOAuthAccountsItem in ((IEnumerable<dynamic>)doc.OAuthAccounts).DefaultIfEmpty()
			select new {
				OAuthAccounts_ProviderUserId = docOAuthAccountsItem.ProviderUserId,
				OAuthAccounts_Provider = docOAuthAccountsItem.Provider,
				__document_id = doc.__document_id
			});
		this.AddField("OAuthAccounts_ProviderUserId");
		this.AddField("OAuthAccounts_Provider");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("ProviderUserId");
		this.AddQueryParameterForMap("Provider");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("ProviderUserId");
		this.AddQueryParameterForReduce("Provider");
		this.AddQueryParameterForReduce("__document_id");
	}
}
