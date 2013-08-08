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


public class Index_Auto_2fWorkoutResults_2fByAthleteIdAndDateSortByDate : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fWorkoutResults_2fByAthleteIdAndDateSortByDate()
	{
		this.ViewText = @"from doc in docs.WorkoutResults
select new { AthleteId = doc.AthleteId, Date = doc.Date }";
		this.ForEntityNames.Add("WorkoutResults");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "WorkoutResults", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				AthleteId = doc.AthleteId,
				Date = doc.Date,
				__document_id = doc.__document_id
			});
		this.AddField("AthleteId");
		this.AddField("Date");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("AthleteId");
		this.AddQueryParameterForMap("Date");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("AthleteId");
		this.AddQueryParameterForReduce("Date");
		this.AddQueryParameterForReduce("__document_id");
	}
}
