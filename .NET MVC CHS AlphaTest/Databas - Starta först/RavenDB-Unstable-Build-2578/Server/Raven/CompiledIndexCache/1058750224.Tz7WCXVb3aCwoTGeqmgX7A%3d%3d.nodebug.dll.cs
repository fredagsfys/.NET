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


public class Index_Auto_2fAthleteAchievements_2fByTitle : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fAthleteAchievements_2fByTitle()
	{
		this.ViewText = @"from doc in docs.AthleteAchievements
select new { Title = doc.Title }";
		this.ForEntityNames.Add("AthleteAchievements");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "AthleteAchievements", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				Title = doc.Title,
				__document_id = doc.__document_id
			});
		this.AddField("Title");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("Title");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("Title");
		this.AddQueryParameterForReduce("__document_id");
	}
}
