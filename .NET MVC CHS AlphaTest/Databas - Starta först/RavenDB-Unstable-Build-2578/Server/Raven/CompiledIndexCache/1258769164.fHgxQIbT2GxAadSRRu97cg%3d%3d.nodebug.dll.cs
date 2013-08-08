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


public class Index_Auto_2fresourcecollections_2fByLanguage : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fresourcecollections_2fByLanguage()
	{
		this.ViewText = @"from doc in docs.resourcecollections
select new { Language = doc.Language }";
		this.ForEntityNames.Add("resourcecollections");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "resourcecollections", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				Language = doc.Language,
				__document_id = doc.__document_id
			});
		this.AddField("Language");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("Language");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("Language");
		this.AddQueryParameterForReduce("__document_id");
	}
}
