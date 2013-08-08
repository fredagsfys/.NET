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


public class Index_Wod_2fSearch : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Wod_2fSearch()
	{
		this.ViewText = @"docs.WodBases.Select(wod => new {
    Id = wod.__document_id,
    Name = wod.Name,
    Title = wod.Title,
    Date = wod.AuditInfo.ModifiedOn,
    AffiliateReferenceId = wod.AffiliateReference.Id,
    AffiliateName = wod.AffiliateReference.Name,
    Description = wod.Description,
    WodType = wod.WodType.ToString(),
    BenchmarkType = wod.BenchmarkType.ToString(),
    ExerciseSearchField = wod.ExerciseSearchField,
    ExerciseList = wod.ExerciseList,
    RoundDescription = wod.RoundDescription
})
results.Select(wod => new {
    Id = wod.Id,
    Name = wod.Name,
    Description = wod.Description,
    Date = wod.Date,
    Title = wod.Title,
    AffiliateReferenceId = wod.AffiliateReferenceId,
    AffiliateName = wod.AffiliateName,
    WodType = wod.WodType,
    BenchmarkType = wod.BenchmarkType,
    ExerciseSearchField = wod.ExerciseSearchField,
    ExerciseList = wod.ExerciseList,
    RoundDescription = wod.RoundDescription,
    Score = wod[""@metadata""].Value(""Temp-Index-Score"")
})";
		this.ForEntityNames.Add("WodBases");
		this.AddMapDefinition(docs => docs.Where(__document => string.Equals(__document["@metadata"]["Raven-Entity-Name"], "WodBases", System.StringComparison.InvariantCultureIgnoreCase)).Select((Func<dynamic, dynamic>)(wod => new {
			Id = wod.__document_id,
			Name = wod.Name,
			Title = wod.Title,
			Date = wod.AuditInfo.ModifiedOn,
			AffiliateReferenceId = wod.AffiliateReference.Id,
			AffiliateName = wod.AffiliateReference.Name,
			Description = wod.Description,
			WodType = wod.WodType.ToString(),
			BenchmarkType = wod.BenchmarkType.ToString(),
			ExerciseSearchField = wod.ExerciseSearchField,
			ExerciseList = wod.ExerciseList,
			RoundDescription = wod.RoundDescription,
			__document_id = wod.__document_id
		})));
		this.TransformResultsDefinition = (Database, results) => results.Select((Func<dynamic, dynamic>)(wod => new {
			Id = wod.Id,
			Name = wod.Name,
			Description = wod.Description,
			Date = wod.Date,
			Title = wod.Title,
			AffiliateReferenceId = wod.AffiliateReferenceId,
			AffiliateName = wod.AffiliateName,
			WodType = wod.WodType,
			BenchmarkType = wod.BenchmarkType,
			ExerciseSearchField = wod.ExerciseSearchField,
			ExerciseList = wod.ExerciseList,
			RoundDescription = wod.RoundDescription,
			Score = wod["@metadata"].Value("Temp-Index-Score")
		}));
		this.AddField("Id");
		this.AddField("Name");
		this.AddField("Title");
		this.AddField("Date");
		this.AddField("AffiliateReferenceId");
		this.AddField("AffiliateName");
		this.AddField("Description");
		this.AddField("WodType");
		this.AddField("BenchmarkType");
		this.AddField("ExerciseSearchField");
		this.AddField("ExerciseList");
		this.AddField("RoundDescription");
		this.AddField("__document_id");
	}
}
