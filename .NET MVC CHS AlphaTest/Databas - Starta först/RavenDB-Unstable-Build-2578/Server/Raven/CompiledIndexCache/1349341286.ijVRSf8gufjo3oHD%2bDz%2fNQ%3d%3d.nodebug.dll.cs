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


public class Index_Affiliate_2fSearch : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Affiliate_2fSearch()
	{
		this.ViewText = @"docs.Affiliates.Select(affiliate => new {
    Name = affiliate.Name,
    City = affiliate.City,
    Country = affiliate.Country,
    Image = affiliate.Image
})";
		this.ForEntityNames.Add("Affiliates");
		this.AddMapDefinition(docs => docs.Where(__document => string.Equals(__document["@metadata"]["Raven-Entity-Name"], "Affiliates", System.StringComparison.InvariantCultureIgnoreCase)).Select((Func<dynamic, dynamic>)(affiliate => new {
			Name = affiliate.Name,
			City = affiliate.City,
			Country = affiliate.Country,
			Image = affiliate.Image,
			__document_id = affiliate.__document_id
		})));
		this.AddField("Name");
		this.AddField("City");
		this.AddField("Country");
		this.AddField("Image");
		this.AddField("__document_id");
	}
}
