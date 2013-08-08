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
    CurrentRank = athlete.CurrentRank,
    City = athlete.City,
    Created = athlete.AuditInfo.CreatedOn,
    Country = athlete.Country,
    AffiliateSearchField = athlete.AffiliateSearchField,
    Affiliates = athlete.Affiliates,
    ListImageUrl = athlete.ListImageUrl,
    FriendlyUrl = athlete.FriendlyUrl
})
results.Select(athlete => new {
    FirstName = athlete.FirstName,
    LastName = athlete.LastName,
    Username = athlete.Username,
    City = athlete.City,
    CurrentRank = athlete.CurrentRank,
    Created = athlete.Created,
    Country = athlete.Country,
    AffiliateSearchField = athlete.AffiliateSearchField,
    Affiliates = athlete.Affiliates,
    ListImageUrl = athlete.ListImageUrl,
    FriendlyUrl = athlete.FriendlyUrl,
    Score = athlete[""@metadata""].Value(""Temp-Index-Score"")
})";
		this.ForEntityNames.Add("Athletes");
		this.AddMapDefinition(docs => docs.Where(__document => string.Equals(__document["@metadata"]["Raven-Entity-Name"], "Athletes", System.StringComparison.InvariantCultureIgnoreCase)).Select((Func<dynamic, dynamic>)(athlete => new {
			FirstName = athlete.FirstName,
			LastName = athlete.LastName,
			Username = athlete.Username,
			CurrentRank = athlete.CurrentRank,
			City = athlete.City,
			Created = athlete.AuditInfo.CreatedOn,
			Country = athlete.Country,
			AffiliateSearchField = athlete.AffiliateSearchField,
			Affiliates = athlete.Affiliates,
			ListImageUrl = athlete.ListImageUrl,
			FriendlyUrl = athlete.FriendlyUrl,
			__document_id = athlete.__document_id
		})));
		this.TransformResultsDefinition = (Database, results) => results.Select((Func<dynamic, dynamic>)(athlete => new {
			FirstName = athlete.FirstName,
			LastName = athlete.LastName,
			Username = athlete.Username,
			City = athlete.City,
			CurrentRank = athlete.CurrentRank,
			Created = athlete.Created,
			Country = athlete.Country,
			AffiliateSearchField = athlete.AffiliateSearchField,
			Affiliates = athlete.Affiliates,
			ListImageUrl = athlete.ListImageUrl,
			FriendlyUrl = athlete.FriendlyUrl,
			Score = athlete["@metadata"].Value("Temp-Index-Score")
		}));
		this.AddField("FirstName");
		this.AddField("LastName");
		this.AddField("Username");
		this.AddField("CurrentRank");
		this.AddField("City");
		this.AddField("Created");
		this.AddField("Country");
		this.AddField("AffiliateSearchField");
		this.AddField("Affiliates");
		this.AddField("ListImageUrl");
		this.AddField("FriendlyUrl");
		this.AddField("__document_id");
	}
}
