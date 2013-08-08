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


public class Index_Athlete_2fSearch : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Athlete_2fSearch()
	{
		this.ViewText = @"docs.Athletes.Select(athlete => new {
    FirstName = athlete.FirstName,
    LastName = athlete.LastName,
    Username = athlete.Username,
    City = athlete.City,
    Country = athlete.Country,
    AffiliateSearchField = athlete.AffiliateSearchField
})";
		this.ForEntityNames.Add("Athletes");
		this.AddMapDefinition(docs => docs.Where(__document => string.Equals(__document["@metadata"]["Raven-Entity-Name"], "Athletes", System.StringComparison.InvariantCultureIgnoreCase)).Select((Func<dynamic, dynamic>)(athlete => new {
			FirstName = athlete.FirstName,
			LastName = athlete.LastName,
			Username = athlete.Username,
			City = athlete.City,
			Country = athlete.Country,
			AffiliateSearchField = athlete.AffiliateSearchField,
			__document_id = athlete.__document_id
		})));
		this.AddField("FirstName");
		this.AddField("LastName");
		this.AddField("Username");
		this.AddField("City");
		this.AddField("Country");
		this.AddField("AffiliateSearchField");
		this.AddField("__document_id");
	}
}
