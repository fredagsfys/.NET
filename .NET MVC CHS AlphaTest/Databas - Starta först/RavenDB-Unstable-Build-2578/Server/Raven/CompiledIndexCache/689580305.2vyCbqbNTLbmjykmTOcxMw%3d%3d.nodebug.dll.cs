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


public class Index_Auto_2fworkouts_2fByDateAndReferenceToAndScheduleWodIdSortByDate : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fworkouts_2fByDateAndReferenceToAndScheduleWodIdSortByDate()
	{
		this.ViewText = @"from doc in docs.workouts
select new { ReferenceTo = doc.ReferenceTo, Date = doc.Date, ScheduleWodId = doc.ScheduleWodId }";
		this.ForEntityNames.Add("workouts");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "workouts", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				ReferenceTo = doc.ReferenceTo,
				Date = doc.Date,
				ScheduleWodId = doc.ScheduleWodId,
				__document_id = doc.__document_id
			});
		this.AddField("ReferenceTo");
		this.AddField("Date");
		this.AddField("ScheduleWodId");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("ReferenceTo");
		this.AddQueryParameterForMap("Date");
		this.AddQueryParameterForMap("ScheduleWodId");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("ReferenceTo");
		this.AddQueryParameterForReduce("Date");
		this.AddQueryParameterForReduce("ScheduleWodId");
		this.AddQueryParameterForReduce("__document_id");
	}
}
