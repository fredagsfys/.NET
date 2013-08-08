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


public class Index_Auto_2fworkouts_2fByReferenceToAndScheduleWodId : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fworkouts_2fByReferenceToAndScheduleWodId()
	{
		this.ViewText = @"from doc in docs.workouts
select new { ScheduleWodId = doc.ScheduleWodId, ReferenceTo = doc.ReferenceTo }";
		this.ForEntityNames.Add("workouts");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "workouts", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				ScheduleWodId = doc.ScheduleWodId,
				ReferenceTo = doc.ReferenceTo,
				__document_id = doc.__document_id
			});
		this.AddField("ScheduleWodId");
		this.AddField("ReferenceTo");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("ScheduleWodId");
		this.AddQueryParameterForMap("ReferenceTo");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("ScheduleWodId");
		this.AddQueryParameterForReduce("ReferenceTo");
		this.AddQueryParameterForReduce("__document_id");
	}
}
