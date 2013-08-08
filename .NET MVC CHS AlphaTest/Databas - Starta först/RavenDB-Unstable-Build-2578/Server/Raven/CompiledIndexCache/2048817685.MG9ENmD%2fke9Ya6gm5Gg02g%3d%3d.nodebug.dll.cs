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


public class Index_Auto_2fcontentpages_2fBySortOrderAndTemplate_AreaSortBySortOrder : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fcontentpages_2fBySortOrderAndTemplate_AreaSortBySortOrder()
	{
		this.ViewText = @"from doc in docs.contentpages
select new { SortOrder = doc.SortOrder, Template_Area = doc.Template_Area }";
		this.ForEntityNames.Add("contentpages");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "contentpages", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				SortOrder = doc.SortOrder,
				Template_Area = doc.Template_Area,
				__document_id = doc.__document_id
			});
		this.AddField("SortOrder");
		this.AddField("Template_Area");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("SortOrder");
		this.AddQueryParameterForMap("Template_Area");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("SortOrder");
		this.AddQueryParameterForReduce("Template_Area");
		this.AddQueryParameterForReduce("__document_id");
	}
}
