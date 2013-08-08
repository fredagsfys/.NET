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


public class Index_Auto_2fexercisebases_2fByLengthAndLengthTypeAndName : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fexercisebases_2fByLengthAndLengthTypeAndName()
	{
		this.ViewText = @"from doc in docs.exercisebases
select new { LengthType = doc.LengthType, Length = doc.Length, Name = doc.Name }";
		this.ForEntityNames.Add("exercisebases");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "exercisebases", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				LengthType = doc.LengthType,
				Length = doc.Length,
				Name = doc.Name,
				__document_id = doc.__document_id
			});
		this.AddField("LengthType");
		this.AddField("Length");
		this.AddField("Name");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("LengthType");
		this.AddQueryParameterForMap("Length");
		this.AddQueryParameterForMap("Name");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("LengthType");
		this.AddQueryParameterForReduce("Length");
		this.AddQueryParameterForReduce("Name");
		this.AddQueryParameterForReduce("__document_id");
	}
}
